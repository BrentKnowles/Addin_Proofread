// WriteThink.cs
//
// Copyright (c) 2013 Brent Knowles (http://www.brentknowles.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// Review documentation at http://www.yourothermind.com for updated implementation notes, license updates
// or other general information/
// 
// Author information available at http://www.brentknowles.com or http://www.amazon.com/Brent-Knowles/e/B0035WW7OW
// Full source code: https://github.com/BrentKnowles/YourOtherMind
//###
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using RulesMess;
using CoreUtilities;

// ToDo: Merge this class with yomspellchecker

namespace WriteThinker
{
    /// <summary>
    /// Purposes
    /// - breaking a body of text into database rows so 
    ///   SQL operations can be applied to analyze the text
    /// 
    /// Feature #1
    /// - She Said/He Said: select a character and see the lines attributed to them
    ///     with a confidence level. 
    ///     Purpose: Allows the writer to look for consistency issues with dialog
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// ACCURACY
    ///  Erased: 75% dialog
    /// Collector: 54% dialog
    /// 
    /// 
    /// BUGS
    ///   - There was a bug where if there was not a SPACE between a . and the next sentence that sentence would be skipped
    ///      HOWEVER: It appears the bug only happens when loading from a text file, which only happens during test. Pasting the offensive text
    ///        into a Keeper note we were unable to reproduce the problem (WILL NOT FIX)
    ///
    /// </summary>
    public class WriteThink
    {

        int nSeconds = 0; // time to run last 'rebuild'

        public DataTable table = null; // the table assigned when created
        public DataView view = null;
        public CharacterInDialogClass[] characters = null;
        public  KnowledgeBase kb = null;
        private string GETSPEAKER_NameExpression = "";
        Regex nameRegex = null;

        private string GETSPEAKER_RegexForPronouns = "";
        Regex pronounRegex = null;

        private string GETSPEAKER_HeSaidExpression = "";
        Regex hesaids = null;


        private int GETSPEAKER_CONTEXTWEIGHT = 0;

        static Regex passiveworddetector = new Regex
    (@"\b(am|is|are|was|were|be|been|can|could|shall|should|may|might|must|will|would|seem|became|did|do|does|being)\b", RegexOptions.Compiled);


        #region StaticUtilities
        /// <summary>
        /// Replaces � with "
        /// 
        /// Like God intended.
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        static public string ReplaceFunkyQuotesWithNormal(string sSource)
        {
            sSource = sSource.Replace('�', '\"');
            sSource = sSource.Replace('�', '\"');
            sSource = sSource.Replace('�', '\'');
            sSource = sSource.Replace('�', '\'');
            return sSource;
        }
        #endregion


        /// <summary>
        /// shoudl only be called after initializaiton
        /// 
        /// will generally be used for unit tests to make sure changes
        /// in parser still mathc previous results
        /// </summary>
        public int GetLines(DataTable computetable, string sFilter)
        {
            if (computetable == null)
            {
                throw new Exception("table not initialized");
            }
            return (int)computetable.Compute("count(id)", sFilter);

        }

        /// <summary>
        /// replaces the datatable and view with new one
        /// whichever function calls this will have to call fadmin.LoadTable
        /// afterwards
        /// </summary>
        /// <param name="sFile"></param>
        public void LoadFromFile(string sFile)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(sFile, XmlReadMode.ReadSchema);
            table = ds.Tables[0];
            ds = null;
        }
      

        /// <summary>
        /// loads an admingrid form
        /// </summary>
        /// <param name="nSeconds"></param>
        public void AdminGrid(int nSeconds)
        {
            if (table == null || view == null)
            {
                throw new Exception("table via LoadTextIntoDatabase needs to be set before AdmiNGrid()");
            }
            fAdminGrid grid = new fAdminGrid();

           
            grid.LoadTable(view);
           
            
            //ToDo: hook this up to console command
            grid.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            grid.labeltimer.Text = nSeconds.ToString();
            grid.writethink = this;
            grid.Show();
            
        }


        /// <summary>
        /// checks the sentence for passivity
        /// 
        /// Note: This is a crude estimate, not very accurate (Not accurate at all)
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        static public bool IsPassive(string sSource, bool bDialog)
        {
            if (bDialog == true) return false;

            MatchCollection matches = passiveworddetector.Matches(sSource);
            if (matches.Count > 0)
            {

                // look through the passive potentials
                foreach (Match match in matches)
                {
                    int nStart = sSource.IndexOf(match.Value) + match.Value.Length;
                    int nLength = 15;
                    if (nLength > (sSource.Length - nStart))
                    {
                        nLength = sSource.Length - nStart;
                    }
                    string neartext = sSource.Substring(nStart, nLength);
                    if (neartext.IndexOf("ed") > -1)
                    {
                        return true;
                    }
                }


                return false;
            }
            return false;
        }

        /// <summary>
        /// returns true if sSource is a fragment
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        static public bool IsFragment(string sSource)
        {
            return false;
        }


        /// <summary>
        ///  counts the word
        /// 
        /// I was doing an inline calculation but it was
        /// not entirely accurate
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        static public int GetWordCount(string sSource)
        {
/*
            int c = 0;
            for (int i = 1; i < sSource.Length; i++)
            {
                if (char.IsWhiteSpace(sSource[i - 1]) == true)
                {
                    if (char.IsLetterOrDigit(sSource[i]) == true ||
                        char.IsPunctuation(sSource[i]))
                    {
                        c++;
                    }
                }
                else if (i >= 3 &&
                    char.IsLetterOrDigit(sSource[i]) &&
                    char.IsPunctuation(sSource[i - 1]) &&
                    char.IsPunctuation(sSource[i - 2]) &&
                    char.IsLetterOrDigit(sSource[i - 3]))
                {
                    c++;
                }
            }
            if (sSource.Length > 2)
            {
                c++;
            }
            return c;*/
            
            int w;
            //Regex reg = new Regex(@"\w+"); // worked but was a little high
            Regex reg = new Regex(@"[\S]+");
            MatchCollection mc = reg.Matches(sSource);
            if (mc.Count > 0)
                w = mc.Count;
            else
                w = 0;
            return w;

        }

        static public int GetSyllables(string sSource)
        {
            sSource = sSource.ToLower().Trim();
            string pattern = "[aeiouy]+";
            int count = Regex.Matches(sSource, pattern).Count;
            if (sSource.EndsWith("e"))
            {
                if (Regex.IsMatch(sSource, "[aeiouy][^aeiouy]e$") == true)
                {
                    count -= 1;
                }

            }
            if (count < 1)
            {
                count = 1;
            }
            return count;
        }

        #region englishdata

        //ToDo: These should probably be pulled from a database?
        string[] saidisms = new string[20] { "read", "laughed", "voice", "said", "shouted", "shouts", "asked", "asks", 
            "responds", "responded", "remarked", "remarks", "spoke","whisper","whispered", "objected","scream", "screamed", "demand", "demanded" };
        #endregion

        private string BuildExpressionForHeSaid(string[] caps)
        {
            // dynamically build a regex match expression
            // for detecting "who said" stuff in dialog
            //ToDo: Will probably want to grab all the capital letters and find matches for them too

        //    string s(\bhe shouted\b)

            string sResult = "";

            string[] identifiers = new string[pronouns.Length + caps.Length];
            Array.Copy(caps, identifiers, caps.Length);
            Array.Copy(pronouns, 0, identifiers, caps.Length , pronouns.Length);
            // remember I
            foreach (string s in identifiers)
            {
                string sMultiple = ""; // we set this to | which in regex means OR but only if more
               
                foreach (string saidism in saidisms)
                {
                    if (sResult != "")
                    {
                        sMultiple = "|";
                    }
                    sResult = String.Format("{0}{1}\\b{2} {3}\\b", sResult, sMultiple, s, saidism);
                }
            }

            return sResult;
        }
        public string[] GetCapitalLetters(string sSource)
        {
            Regex caps = new Regex(@"(\b[A-Z]\w*)");
            MatchCollection matches = caps.Matches(sSource);
            string[] capsfound = new string[matches.Count];
            int nCount = 0;
            foreach(Match match in matches)
            {

                capsfound[nCount] = match.Value;
                nCount++;
            }
            return capsfound;
        }



        /*DataTable femaleNames = null;
        DataTable maleNames = null;
        DataTable lastNames = null;

        /// <summary>
        ///  This worked but I decided to have the user define characte details instead
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        private string[] GetCapitalLettersFilteredForNames(string sSource)
        {
            // only load Female names once
            if (femaleNames == null)
            {
                DataSet ds = new DataSet();
                ds.ReadXml("C:\\Documents and Settings\\brent\\My Documents\\Keeper\\Data\\Words\\femalenames.xml");
                femaleNames = ds.Tables[0];
            }
            // only load Female names once
            if (maleNames == null)
            {
                DataSet ds = new DataSet();
                ds.ReadXml("C:\\Documents and Settings\\brent\\My Documents\\Keeper\\Data\\Words\\malenames.xml");
                maleNames = ds.Tables[0];
            }
            // only load Female names once
            if (lastNames == null)
            {
                DataSet ds = new DataSet();
                ds.ReadXml("C:\\Documents and Settings\\brent\\My Documents\\Keeper\\Data\\Words\\lastnames.xml");
                lastNames = ds.Tables[0];
            }


            // grabs the capital letters but then removes any that do not
            // appear on any name list


            // ToDo: Need to integrate properly with Keeper? (Once word lists are moved into YOM)
            string[] caps = GetCapitalLetters(sSource);
            if (caps.Length > 0)
            {
                for (int i = caps.Length - 1; i >= 0; i--)
                {
                    bool bFound = false;
                    // walk backwards
                    // delete entries with no match
                    foreach (DataRow row in femaleNames.Rows)
                    {
                        if (row["Word"].ToString() == caps[i])
                        {
                            bFound = true;
                            break; // we found it, okay
                        }
                        
                    } // foreach

                    if (bFound == false)
                    {
                        foreach (DataRow row in maleNames.Rows)
                        {
                            if (row["Word"].ToString() == caps[i])
                            {
                                bFound = true;
                                break; // we found it, okay
                            }
                        }
                    }

                    if (bFound == false)
                    {
                        foreach (DataRow row in lastNames.Rows)
                        {
                            if (row["Word"].ToString() == caps[i])
                            {
                                bFound = true;
                                break; // we found it, okay
                            }
                        }
                    }


                    if (bFound == false)
                    {
                        caps[i] = "";
                    }

                }
                
            }
            return caps;
        }
        */

        /// <summary>
        /// WIll look thru the passed in character string
        /// </summary>
        private string[] CharacterNames
        {
            get { return new string[2] { "Isabel", "Meredith" }; }

        }

    /// <summary>
    /// generates a regex for pronoune, one of the last
    /// ditch efforts if we still have not found a speaker
    /// </summary>
    /// <returns></returns>
         public string GetRegexForPronouns()
        {
            string sResult = "";
           
            foreach (string s in pronouns)
            {
                string sMultiple = "";

                if (sResult != "")
                {
                    sMultiple = "|";
                }
                sResult = String.Format("{0}{1}\\b{2}\\b", sResult, sMultiple, s);
            }
            return sResult;
        }
        /// <summary>
        /// tries to guesstimate the speaker of sSource dialog line
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="sBefore"></param>
        /// <param name="sAfter"></param>
        /// <param name="confidence"></param>
        /// <returns></returns>
        private string GetSpeaker(string sSource, string sBefore, string sAfter, 
            string sBeforeSpeaker, out double confidence)
        {

            string sContext = sBefore + " " + sAfter;

            if (characters == null)
            {
                throw new Exception("GetSPeaker - we need a character array");
            }

            if (pronouns == null)
            {
                throw new Exception("pronouns not defined? DId you build the AI table?");
            }

            if (kb == null)
            {
                throw new Exception("knowledge base needs to be built");
            }

            if (GETSPEAKER_RegexForPronouns == "")
            {
                throw new Exception("pronouns were not defined.");
            }

            if (hesaids == null)
            {
                throw new Exception("we need the hesaid regex built");
            }
            if (pronounRegex == null)
            {
                throw new Exception("we need the pronoun regex built");
            }
            


            if (sSource.IndexOf("Listen,") > -1)
            {
              //  int breaker = 9;
            }

            // This worked to get he and shes and whatnot but I wanted to try something
            // more elabroate
            //Regex caps = new Regex(@"(\bhe|\bI\b|\bit\b|\bHe\b|\bShe\b|\bshe\b|(\b[A-Z]\w*))");
          
           
            MatchCollection matches = hesaids.Matches(sContext);

            confidence = 0.0;
          
            
            ArrayList output = new ArrayList();

            foreach (Match match in matches)
            {
                // remove the 'said' bit from the string
                string sValue = match.Value;

                //ToDo: Potential slow (but only about one second
                foreach (string s in saidisms)
                {
                    if (sValue.IndexOf(s) > -1) // this did not improve speed
                    {
                        sValue = sValue.Replace(s, "");
                    }
                }
                
                sValue = sValue.Trim();
                output.Add(sValue);

                
            }

            
            // now check for pronouns
          ///  Regex pronouns = new Regex(@"(\bhe|\bI\b|\bit\b|\bHe\b|\bShe\b|\bshe\b)");
          //  matches = pronouns.Matches(sContext);

            

            /////////////////////////
            // 
            // Rules Reference
            // in case I get confused
            //
            // 1. he/she said will trump  (includes pronoun he/she but only if followed with a saidism)
            // 2. then we look for a previous speaker (i.e., if line of dialog before me had a speaker, use that)
            // 2. then we look for a Name
            // 3. then we look for the most likely name
            // ToDo: 4 - just look for presence of pronouns
            // ToDo: 5. If we still do not have a match we try to match a Pronoun with a Name
            //
            //////////////////////////////////////////////////////
            



            //////////////////////////////////////
            //
            // NO MATCH
            //
            // 1. Now we get into advanced stuff trying to dig up the talker
            //
            //////////////////////////////////////

           // if (matches.Count == 0 /*&& bFoundExactlyOne == false*/)
            {
                // a. previous speaker. If there is a previous speaker lets try that
                if (sBeforeSpeaker != "" && sBeforeSpeaker != "none")
                {
                   // sOutput = sBeforeSpeaker;
                    output.Add(sBeforeSpeaker);
                 //   bFoundExactlyOne = true;
                    confidence = 0.40;
                }

                // Was there a name of a 'Character' on the previous or next line?
                // if so, add to output
                
                
               
                
                MatchCollection nameMatches = nameRegex.Matches(sBefore + sAfter);
                if (nameMatches.Count > 0)
                {
                    // we found a copy name matches
                    foreach (Match match in nameMatches)
                    {
                        output.Add(match.Value);
                    }
                   
                }

              
            }


            //////////////////////////////////////////////
            //
            // Pronouns
            //
            // Just look for presence of pronouns if still can't find matches
            //
            /////////////////////////////////////////////
            if (output.Count == 0)
            {

                
                MatchCollection pronounMatches = pronounRegex.Matches( (sBefore + sAfter).ToLower());
                if (pronounMatches.Count > 0)
                {
                    // we found a copy name matches
                    foreach (Match match in pronounMatches)
                    {
                        output.Add(match.Value);
                    }
                    if (output.Count == 1)
                    {
                        confidence = 0.14;
                       /* bFoundExactlyOne = true;*/
                    }
                    else if (output.Count > 1)
                    {
                        confidence = 0.08;
                    }
                }
            }



            if (output.Count == 1)
            {
                confidence = 0.90;

            }
         


            ///////////////////////////////////////////////
            //
            // Pronoun Replacement
            //
            // We replace pronouns with proper nouns
            //
            // so at this point we have hesaid with He, Her, et cetera
            // we replace each with a name
            //
            //
            //
            //////////////////////////////////////////////

         



            // a. If IsPronoun
            for (int matchCount = 0; matchCount < output.Count; matchCount++)
            {


                Relationship bestRelationship = kb.GetMostLikelyDeducedFact(
                     output[matchCount].ToString().ToLower(),
                     sContext,
                     GETSPEAKER_CONTEXTWEIGHT);

                 // now we REPLACE WITH a relationship match if there is one
                if (bestRelationship != null && output[matchCount].ToString() != bestRelationship.B)
                {
                    string sMes = "";// String.Format("Dialog: {0} Context: {1}", sSource, sContext);
                    lg.Instance.Line("WriteThink->GetSpeaker", ProblemType.MESSAGE,
                        output[matchCount] + " has been replaced by "+ bestRelationship.B + " "+ 
                        sMes
                        );
                    output[matchCount] = bestRelationship.B;
                    confidence = 0.03;
                }


     
            } // for


            /////////////////////////////////////////
            //
            // TOO MANY MATCHES
            //
            // 1. Now we isolate who might have (logically) spoken
            //
            //   A. Are they the same?
            //   B. Now the same? Pick most likely
            //
            ////////////////////////////////////////////

            // - Too many still? Consolidate down to one if possible
            if (output.Count > 1)
            {
                // Are they all the same? If so, pass with a reasonable confidence
                // sort and if first and lst entry are the same we are just one
                output.Sort();
                if (output[0].ToString() == output[output.Count - 1].ToString())
                {
                    string svalue = output[0].ToString();
                    output.Clear();
                    output.Add(svalue); // add value back into cleared list
                    confidence = 0.50;
                }
            }


            if (output.Count > 1) // only should trigger if we hav emore than 1
            {

                {

                    // now we attempt to see which pronoun, noun, was mentioned most and use that
                    Hashtable hash = new Hashtable();
                    foreach (string s in output)
                    {
                        int nValue = 1;
                        if (hash.ContainsKey(s))
                        {
                            nValue = (int)hash[s];
                            nValue++;
                            hash[s] = nValue;
                        }
                        else
                            hash.Add(s, nValue);
                    }
                    string sMaxValue = "";
                    int nMaxValue = -1;
                    foreach (DictionaryEntry dict in hash)
                    {
                        if ((int)dict.Value > nMaxValue)
                        {
                            sMaxValue = dict.Key.ToString();
                            nMaxValue = (int)dict.Value;
                        }
                    }

                    // okay now we have a max value
                    output.Clear();
                    if (sMaxValue == "I")
                    {
                    //    int breaker = 9;
                    }
                    output.Add(sMaxValue);

                    Console.WriteLine(output.ToString());
                    confidence = 0.15;

                }
            }

            if (output.Count < 1)
            {
                lg.Instance.Line("WriteThink->GetSpeaker", ProblemType.MESSAGE,"no match found " + "pick most popular");
                output.Add(CharacterInDialogClass.GetMostLikelyName(characters, CharacterInDialogClass.gender.any));
            }

            string sOutput = "";
            // transfoer to string
            foreach (string s in output)
            {
                if (output.Count > 1)
                {
                    throw new Exception("At this point there should only be one match");
                }

                // convert returned name to NAME (i.e., an alias of doctor turns into Rutger)

                string sdelimiter = "";
                if (sOutput != "")
                {
                    sdelimiter = ", ";
                }
                sOutput = sOutput + sdelimiter +  CharacterInDialogClass.GetNameFromAlias(s, characters);
            }

            return sOutput;



            
            
        }

        

        /// <summary>
        /// Used to break the dialog apart correctly
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private ArrayList ParseEachLine(string[] lines)
        {
            ArrayList lines2  = new ArrayList();
            bool bMultilinedialog = false;

            foreach (string s in lines)
            {
                try
                {
                    if (s.ToLower().IndexOf("like it") >= 0)
                    {
                      //  int breaker = 9;
                    }
                    Regex r = new Regex("\"");
                    MatchCollection match = r.Matches(s);

                    if (match.Count == 1)
                    {
                        // only one quote means a sentence that has been split
                        // across multiple sentences
                        // just add it and go into dialog mode

                        if (bMultilinedialog == false)
                        {
                            bMultilinedialog = true;
                        }
                        else
                        {
                            bMultilinedialog = false;
                        }



                        // if there's a space after quote we go on our own line
                        int nPos = s.IndexOf("\"");



                        if (nPos + 1 >= s.Length)
                        {
                            // July 2010 - this should not come up often but specific files ending with a two sentence statement
                            // at the end of the file could cause a crash.
                            nPos = nPos - 1;
                        }
                        

                        if (s[nPos + 1] == ' ')
                        {

                            // it might also be in the middle of another string
                            // so we look near it for a space
                            // if so we split into two string
                            lines2.Add(s.Substring(0, nPos + 1));
                            lines2.Add(s.Substring(nPos + 1, s.Length - nPos - 1));

                        }
                        else
                            if (nPos > 0 && s[nPos - 1] == '.')
                            {
                                // if we have a ." match then we keep 
                                // the quote as part of the first line
                                int nquoteposition = s.IndexOf("\"");
                                lines2.Add(s.Substring(0, nquoteposition + 1));
                                lines2.Add(s.Substring(nquoteposition + 1, s.Length - nquoteposition - 1));
                            }
                            else
                            {
                                int nquoteposition = s.IndexOf("\"");
                                lines2.Add(s.Substring(0, nquoteposition));

                                // june 11 2009 - my intention here is for the 'second line to get the quote?
                                // so as in He sat down. "This is the end

                                lines2.Add(s.Substring(nquoteposition, s.Length - nquoteposition));

                                // just add line
                                //    lines2.Add(s);

                                // no, copy everything before quote as one string
                                // and everything after as one
                            }
                    }
                    else
                        if (s.IndexOf("\"") > 1)
                        {



                            // go through the substring and every 2 " characters we split
                            int quote = 0;
                            int start = 0;
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] == '\"')
                                {
                                    quote++;

                                    if (bMultilinedialog == true)
                                    {
                                        // if we are multi line and got here
                                        // we have a situation where we have 3 or another
                                        // uneven number of quotes
                                        // 
                                        // basically we have not closed the previous
                                        // string correctly.
                                        // in this case we do act like there's an extra quote
                                        quote++;
                                        bMultilinedialog = false;
                                    }

                                    if (quote == 1)
                                    {
                                        // the start of the sentence needs to be
                                        // removed -- the bit before the quote starts
                                        string sSub = s.Substring(start, i - start);
                                        //if (sSub != "" && sSub != " ")
                                        {
                                            lines2.Add(sSub);
                                        }
                                        start = i;
                                    }
                                    if (quote == 2)
                                    {
                                        // peel off substring and 
                                        // add
                                        lines2.Add(s.Substring(start, i - start + 1));
                                        quote = 0;
                                        start = i + 1;
                                    }

                                } // a quote
                                // if at end of strign copy the 'rest'
                                if (i == s.Length - 1)
                                {
                                    int nLengthS = i - start;
                                    if (nLengthS < 0)
                                    {
                                        nLengthS = 0; // sometimes we had less than zero which is a crash
                                    }
                                    string sSub = s.Substring(start, nLengthS);
                                    //if (sSub != "" && sSub != " ")
                                    {
                                        lines2.Add(sSub);
                                        if (sSub.IndexOf("\"") >= 0)
                                        {
                                            // trailing quote found
                                            bMultilinedialog = true;
                                        }
                                    }
                                }

                            }

                            /* string[] newlines = s.Split('\"');
                             foreach (string s1 in newlines)
                             {
                                 if (s1 != "" && s1 != " ")
                                 {
                                     // add quotes back in
                                     lines2.Add("\"" + s1 + "\"");
                                 }
                             }*/
                        }
                        else
                        {
                            if (bMultilinedialog == true)
                            {
                                lines2.Add("\"" + s);
                            }
                            else
                            {
                                lines2.Add(s);
                            }
                        }
                } // try
                catch (Exception ex)
                {
                    CoreUtilities.NewMessage.Show(String.Format("Error: {0} with message {1}", s, ex.ToString()));
                }
            }// loop
            return lines2;
        }
        
        string[] pronouns = null;

        public string speakerrules = "";

        /// <summary>
        /// ToDo: These will be from a file
        /// 
        /// Loads the base AI tables and performs deduction
        /// 
        /// Observations:
        ///  1. Too many 'is speakers' degrades qualtiy, especially if that
        ///     word isn't being equated to anything (which makes sense, if I say
        ///     banjo is speaker but then never defined banjo as a character
        ///     it will appear as a hesaidshesaid but never be attached to a character
        ///     --> The more character attachments we can make then the more likely quality will improve
        /// </summary>
        public void BuildAITables()
        {
            if (speakerrules == "")
            {
                throw new Exception("speaker rules has not been set (file");
            }
            kb = new KnowledgeBase();
            kb.LoadFromXmlTable(speakerrules);
            
            // my pronoun list is areally a list of speakers
            //static string[] pronouns = new string[7] { "I", "friend", "companion", "he", "she", "it","his" };
         /*   kb.Add(new Relationship("he", "is", "speaker"));
            kb.Add(new Relationship("his", "is", "speaker"));
            kb.Add(new Relationship("she", "is", "speaker"));
            kb.Add(new Relationship("her", "is", "speaker"));
            kb.Add(new Relationship("friend", "is", "speaker"));
            kb.Add(new Relationship("companion", "is", "speaker"));
        //    kb.Add(new Relationship("it", "is", "speaker")); = this is so seldom true and it screws up

            kb.Add(new Relationship("read", "has", "speaker")); // no purpose, but making sure only 'is' creates a is speaker rule

            kb.Add(new Relationship("his", "is", "malepronoun"));
            kb.Add(new Relationship("she", "is", "femalepronoun"));
            kb.Add(new Relationship("he", "is", "malepronoun")); // so these would stay in
            kb.Add(new Relationship("her", "is", "femalepronoun")); // so these would stay in

            // we put i in lowercase because all gets converted to this
            kb.Add(new Relationship("i", "is", "pov"));
            kb.Add(new Relationship("my", "is", "pov"));
            
            */
            kb.Add(new Relationship("malepronoun", "is", 
                CharacterInDialogClass.GetMostLikelyName(characters, 
                CharacterInDialogClass.gender.male), "", 75, 0, false));
            kb.Add(new Relationship("femalepronoun", "is",
                CharacterInDialogClass.GetMostLikelyName(characters, 
                CharacterInDialogClass.gender.female), "", 75, 0, false));
            kb.Add(new Relationship("pov", "is",
                CharacterInDialogClass.GetMostLikelyName(characters, 
                CharacterInDialogClass.gender.any), "", 75, 0, false));

            

            string[] malenames = CharacterInDialogClass.GetAllCharacterNames(characters, CharacterInDialogClass.gender.male);
            foreach (string s in malenames)
            {
                kb.Add(new Relationship("malepronoun", "is",s, "", 50, 0, false));
            }

            string[] femalenames = CharacterInDialogClass.GetAllCharacterNames(characters, CharacterInDialogClass.gender.female);
            foreach (string s in femalenames)
            {
                kb.Add(new Relationship("femalepronoun", "is",s, "", 50, 0, false));
            }

            // now we add all the other males/females but with a lower weight

            kb.Deduction();
            
            pronouns = kb.GetDefinedFor("speaker");
            //new string[7] { "I", "friend", "companion", "he", "she", "it","his" };
            // Optmiziation
            // Moving some of these queries up here so they don't get executed
            // every loop
            GETSPEAKER_NameExpression = CharacterInDialogClass.GetRegexForNames(characters);
            nameRegex = new Regex(GETSPEAKER_NameExpression);

            GETSPEAKER_RegexForPronouns = GetRegexForPronouns();
             pronounRegex = new Regex(GETSPEAKER_RegexForPronouns);

            string[] sCaps = CharacterInDialogClass.GetAllCharacterNames(characters, CharacterInDialogClass.gender.any);

            GETSPEAKER_HeSaidExpression = BuildExpressionForHeSaid(sCaps);
            hesaids = new Regex(GETSPEAKER_HeSaidExpression, RegexOptions.IgnoreCase);


            Relationship[] contextWeight = kb[new RelationKey("pronouncontextweight", KnowledgeBase.DEDUCED_IS)];

            if (contextWeight == null)
            {
                throw new Exception("pronouncontextweight must be defined inrules datable with an integer value");
            }

            GETSPEAKER_CONTEXTWEIGHT = Int32.Parse(contextWeight[0].B);

        }

        DateTime timer;


        /// <summary>
        /// returns the length of time it has taken LoadTextStringIntoDatabase to run
        /// needs to be called right after that calld
        /// </summary>
        /// <returns></returns>
        public int GetSecondsElapsedSinceLoadTextStringIntoDatabase()
        {
            TimeSpan span = DateTime.Now - timer;
            nSeconds = span.Seconds;
            return span.Seconds;
        }
	
    
        public DataTable LoadTextStringIntoDatabase(string sText, CharacterInDialogClass[] _characters)
        {
            return LoadTextStringIntoDatabase(sText, _characters, "", false);
        }
        /// <summary>
        /// returns first numberofwords
        /// </summary>
        /// <param name="input"></param>
        /// <param name="numberWords"></param>
        /// <returns></returns>
        public static string FirstWords(string input, int numberWords)
        {
            try
            {
                // Number of words we still want to display.
                int words = numberWords;
                // Loop through entire summary.
                for (int i = 0; i < input.Length; i++)
                {
                    // Increment words on a space.
                    if (input[i] == ' ' || input[i] == ',' || input[i]=='-' || ';' == input[i] || ':'==input[i])
                    {
                        words--;
                    }
                    // If we have no more words to display, return the substring.
                    if (words == 0)
                    {
                        return input.Substring(0, i);
                    }
                }
            }
            catch (Exception)
            {
                // Log the error.
            }
            return string.Empty;
        }
	


        /// <summary>
        /// loads text into the database as oppose to LoadTextIntoDatabase which
        /// takes a file
        /// 
        /// 
        /// </summary>
        /// <param name="sTextString"></param>
        /// <param name="rtf">CUT: if passed in will attempt to do character matching by color (Experimental Spetember 2011)</param>
        /// <param name="KeepTable"> if true we don't rebuild the TABLE, for use with color matching</param>
        /// <param name="OverrideCharacter">Name of the character to override, with color matching system</param>
        /// <param name="_characters"></param>
        /// <returns></returns>
        public DataTable LoadTextStringIntoDatabase(string sText, CharacterInDialogClass[] _characters, string OverrideCharacter, bool KeepTable)
        {
            timer = DateTime.Now;
          
            this.characters = _characters;
            // Hving null characters is okay, though it will produce almost meaningless results
             if (characters == null)
            {
                 // create a simple character array with one entry
                this.characters = new CharacterInDialogClass[1];
                this.characters[0] = new CharacterInDialogClass();
                this.characters[0].Name = "default";
             //   throw new Exception("Need character information to parse dialog");
            }

           


            sText = CleanupImportString(sText);

            if (sText == null)
            {
                throw new Exception("text string was null");
            }


            if (false == KeepTable)
            {
                table = new DataTable();
                DataColumn auto = new DataColumn("ID", typeof(int));

                auto.AutoIncrement = true;
                auto.AutoIncrementSeed = 1;// DateTime.Now.Millisecond;
                auto.AutoIncrementStep = 1;
                table.Columns.Add(auto);
                table.PrimaryKey = new DataColumn[] { auto };


                DataColumn text = new DataColumn("Text");
                table.Columns.Add(text);


                table.Columns.Add("Confidence", typeof(double));

                table.Columns.Add("dialog", typeof(bool), "Text Like '%\"%'");
                table.Columns.Add("contraction", typeof(bool), "Text Like '%\'\'%'");
                table.Columns.Add("hesaidshesaid");
                DataColumn charLen = new DataColumn("charLen", typeof(int), "Len(Text)");

                table.Columns.Add(charLen);
                DataColumn wordLen = new DataColumn("wordLen", typeof(int));// "Len(Text)/5");
                table.Columns.Add(wordLen);

                table.Columns.Add("ispassive", typeof(bool));
                table.Columns.Add("isfragment", typeof(bool));
                table.Columns.Add("syllables", typeof(int));
                table.Columns.Add("startswithverb", typeof(bool)); // August 2012
                table.Columns.Add("hidelinebyline", typeof(bool)); // November 2012 - to hide during a line by line search
            } // keep table

            // precleanup: convert quotes
            sText = ReplaceFunkyQuotesWithNormal(sText);







           // a regex with some handling for abbreviations

          //    Regex.Split(sText, @"(?<=['""A-Za-z0-9][\.\!\?])\s+(?=[A-Z])");
                //worked but flawed 

            // this is suppose to be faster (using compiled with a constant string)
                Regex splitter = new Regex(@"(?:(?<=\.|\!|\?)(?<!Mr\.|\...|Dr\.)(?<!U\.S\.A\.)\s+(?=[A-Z]))", RegexOptions.Compiled);
                
                
                string[] lines = splitter.Split(sText);

            /* Nope, won't work, doesn't match
                if ("" != rtf)
                {
                    rtflines = splitter.Split(rtf); // september 2011 (color coding)
                }
            */



            // * Parse # 2 - now we extract dialog
            ArrayList lines2 = ParseEachLine(lines);

            //ArrayList lines2 = new ArrayList(lines);


            // we have to do 2 loops 
            // the first loop removes blanks
            for (int deletepass = lines2.Count - 1; deletepass > -1; deletepass--)
            {
                lines2[deletepass] = lines2[deletepass].ToString().Replace("bbb.", "");
                lines2[deletepass] = lines2[deletepass].ToString().Replace("bbb", "");
                lines2[deletepass] = lines2[deletepass].ToString().Trim();
                if (lines2[deletepass].ToString() == "" || lines2[deletepass].ToString() == " ")
                {
                    lg.Instance.Line("WriteThink->LoadTextStringIntoDatabase", ProblemType.MESSAGE, lines2[deletepass].ToString() + " deleting");
                    lines2.Remove(lines2[deletepass]);

                }
            }
            // tried doing a dialog regex but realized that 
            // failed too because they would neve rmatch
            // because the strings are split in one but not the other based on periods

            // set up knowledge table
            // ToDo: Should be loading from file
            if (false == KeepTable)
            {
                BuildAITables();
            }
 
            int nCount = -1;

         

            // Major pass; primarily grabbing Speaker but also passive and stuff
            foreach (string s in lines2)
            {
                nCount++;


                /* My attempt a COLOR PASS: Ignore this until I get back to it. I don't think I want to dit
                // september 2011
              //  move this code to where dialog is
                if (true)
                {
                    int indexofrtf = rtf.IndexOf(s.Replace("\"", "").Trim()); // remove quote marks because they are removed
                    string rtf_temp = "";
                    if (indexofrtf > -1)
                    {
                        rtf_temp = rtf.Substring(indexofrtf-4, s.Length + 4);
                    }
                }*/

                string snewline = s;
                //  string snewline = s.Replace("bbb.", "");
                //  snewline = snewline.Replace("bbb", "");
              //  if (snewline != "" && snewline != " ")  no longer needed, we do this in a seperate loop
                {





                    // add core string to datatable
                    DataRow row = table.NewRow();
                    row["hidelinebyline"] = false;
                    row["Text"] = snewline;
                    row["hesaidshesaid"] = "none";
                    row["isfragment"] = IsFragment(snewline);
                    row["wordLen"] = GetWordCount(snewline);

                    bool bIsDialog = snewline.IndexOf("\"") > -1;

                    row["ispassive"] = IsPassive(snewline, bIsDialog);
                    double confidence = 0.0;
                    string sPrevious = "";
                    string sNext = "";
                    string sBeforeSpeaker = "";


                    if (Layout.LayoutDetails.Instance.WordSystemInUse.IsVerb(FirstWords(snewline, 1)))
                    {
                        row["startswithverb"] = true;
                    }
                    else
                    {
                        row["startswithverb"] = false;
                    }


                    // * Code for the color coding
                    if ("" != OverrideCharacter)
                    {
                        // we don't do the next section
                        // if we are overriding a character we ASSUME 
                        // we passed dialog in.
                        row["hesaidshesaid"] = OverrideCharacter;
                        confidence = 1.0;
                    }
                    else

                    // don't calculate hesaid unless there is actual dialog
                    if (bIsDialog == true)
                    {
                        if (nCount > 0)
                        {
                            // don't grab lines looking for context if they were dialog lines

                            sPrevious = lines2[nCount - 1].ToString();
                            if (sPrevious.IndexOf("\"") > -1)
                            {
                                sPrevious = ""; // 
                            }
                            sBeforeSpeaker = table.Rows[table.Rows.Count - 1]["hesaidshesaid"].ToString();

                        }
                        if (nCount < lines2.Count - 1)
                        {

                            sNext = lines2[nCount + 1].ToString();
                            if (sNext.IndexOf("\"") > -1)
                            {
                                sNext = ""; // 
                            }

                        }
                        row["hesaidshesaid"] = GetSpeaker(snewline, sPrevious, sNext, sBeforeSpeaker, out confidence);
                    }
                    row["Confidence"] = confidence;
                    row["syllables"] = GetSyllables(snewline);
                    table.Rows.Add(row);
                    row = null;
                }
            }
            return table;
        }
        /// <summary>
        /// used for cleaning out tokens from the string
        /// </summary>
        /// <param name="sLine"></param>
        /// <returns></returns>
        public string CleanupImportString(string sLine)
        {
          
            sLine = sLine.Replace("\t", " bbb. "); // remove tabs
            sLine = sLine.Replace("\n", " bbb. "); // remove tabs
            sLine = sLine.Replace("\r", " bbb. "); // remove tabs
           
            return sLine;
        }

        /// <summary>
        /// Loadsa a file  into database
        /// </summary>
        /// <param name="sTextFile"></param>
        /// <returns></returns>
        public DataTable LoadTextIntoDatabase(string sTextFile, CharacterInDialogClass[] _characters)
        {
            if (File.Exists(sTextFile) == false)
            {
                throw new Exception(String.Format("File {0} does not exist."));
            }

            // load the text into one giant string


            // the encoding is important because it gets rid of characters
            StreamReader file = new StreamReader(sTextFile, System.Text.Encoding.Default);
            string sText = "";
            if (file != null)
            {
                string sLine = file.ReadLine();
                while (sLine != null)
                {
                   // sLine = CleanupImportString(sLine); moved this into LoadTextInto
                    if (sLine == "" || sLine == " ")
                    {
                        sLine = " bbb. "; // need to insert this to have proper parabreaks
                    }
                    sText = sText + sLine;
                    sLine = file.ReadLine();
                }
            }

            file.Close();

            return LoadTextStringIntoDatabase(sText, _characters);

           

        }
        /// <summary>
        /// returns the number of lines of dialog
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sFilter"></param>
        /// <returns></returns>
        public int GetLinesOfDialog(DataTable table, string sFilter)
        {
            return (int)table.Compute("count(id)", "dialog=true");
        }

        /// <summary>
        /// /shows a stat panel for the current text
        /// </summary>
        public void ShowStatPanel(System.Drawing.Icon icon)
        {

            
            // all routines here need to use Table
            // so we can filter it at a later point

            string sFilter = "";

            DataTable computeTable = this.table;
            if (view != null)
            {
                computeTable = view.ToTable();
            }
            fStatPanel panel = new fStatPanel();
            panel.Icon = icon;
            if (computeTable.Rows.Count > 0)
            {
                // retrievals
                int nMinLineLength = (int)computeTable.Compute("min(wordLen)", sFilter);
                int nMaxLineLength = (int)computeTable.Compute("max(wordLen)", sFilter);
                int nNumberSentences = GetLines(computeTable, sFilter);
                int nPassiveSentences = (int)computeTable.Compute("count(id)", "ispassive=true");

                int nNumberDialog = GetLinesOfDialog(computeTable, sFilter);
                long nNumberDialogWords = 0;
                try
                {
                    nNumberDialogWords = (long)computeTable.Compute("sum(wordLen)", "dialog=true");
                }
                catch (Exception)
                {
                }

                long nTotalSyllables = (long)computeTable.Compute("sum(syllables)", sFilter);
                long nWords = (long)computeTable.Compute("sum(wordLen)", sFilter);


                // calculations
                float percentdialog = ((float)nNumberDialog / (float)nNumberSentences) * 100;
                float percentdialogwords = ((float)nNumberDialogWords / (float)nWords) * 100;
                float avgwordspersentence = (float)nWords / (float)nNumberSentences;
                float avgsyllable = (float)nTotalSyllables / (float)nNumberSentences;

                double readingease = 206.835 - (1.015 * ((double)nWords / (double)nNumberSentences)) -
                    (84.6 * ((double)nTotalSyllables / (double)nWords));



                double gradelevel = (0.39 * ((double)nWords / (double)nNumberSentences)) +
                    (11.8 * ((double)nTotalSyllables / (double)nWords)) - 15.59;

                float percentpassive = ((float)nPassiveSentences / (float)nNumberSentences) * 100;

                string sFormat = "0.00";
              
                panel.statPanel1.labelMinLine.Text = nMinLineLength.ToString();
                panel.statPanel1.labellongestsentence.Text = nMaxLineLength.ToString();
                panel.statPanel1.labelnumsentences.Text = nNumberSentences.ToString();
                panel.statPanel1.labelpercentdialog.Text = percentdialog.ToString(sFormat);
                panel.statPanel1.labelwords.Text = nWords.ToString();
                panel.statPanel1.labelreadingease.Text = readingease.ToString(sFormat);
                panel.statPanel1.labelgrade.Text = gradelevel.ToString(sFormat);
                panel.statPanel1.labelpassive.Text = String.Format("{0} %", percentpassive.ToString(sFormat));
                panel.statPanel1.labelavgsyl.Text = avgsyllable.ToString(sFormat);
                panel.statPanel1.labelpercentdialogwords.Text = percentdialogwords.ToString(sFormat);
                panel.statPanel1.labelavgwordsper.Text = avgwordspersentence.ToString(sFormat);
              //  panel.statPanel1.labelDebugTimeTaken.Text = nSeconds.ToString();
                panel.ShowDialog();
            }
            else
            {
                panel.statPanel1.Visible = false;
                panel.labelNoText.Visible = true;
                panel.ShowDialog();

            }
            
        }

        /// <summary>
        /// compares our current table with a grade table that is passed in.
        /// 
        /// We are looking at the hesaidshesaid dialog lines.
        /// 
        /// ** We are assuming a perfectly Hand-graded gradeTable. **
        /// 
        /// 
        /// </summary>
        /// <param name="gradeTable"></param>
        /// <returns>a percent fail. If there are 100 lines and 20 of them different then we return 80</returns>
        public double Grade(DataTable gradeTable)
        {
            double Total = 0; // table.Rows.Count;
            double Mistakes = 0;
            double result = 0.0;

            if (gradeTable.Rows.Count != table.Rows.Count)
            {
                lg.Instance.Line("WriteThink->Grade", ProblemType.MESSAGE, "grade tables do not match " + "failed");
                return 0.0;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ((bool)table.Rows[i]["dialog"] == true)
                {
                    Total++; // we only count dialog lines
                    string sNew = table.Rows[i]["hesaidshesaid"].ToString();
                    string sBenchmark = gradeTable.Rows[i]["hesaidshesaid"].ToString();
                    if (sNew != sBenchmark)
                    {
                        lg.Instance.Line("WriteThink->Grade", ProblemType.MESSAGE,

                            String.Format("New: {0}, Old{1}", sNew, sBenchmark) + " "+
                            table.Rows[i]["Text"].ToString());
                        Mistakes++;
                    }
                }
            }
            result = ((Total - Mistakes) / Total) * 100.0;
            return result;

        }
    } // class
    #region archive
    /*
     * 
 //  { ",\"", ".\"", "?\"", "!\"", "." },
            // initial seperate one
            // split into an array based on '.'
            //string[] lines = sText.Split(new string[1] {"." }, StringSplitOptions.RemoveEmptyEntries);
          
          //  string[] lines = sText.Split(new string[1] { "\\""":" }, 
          //      StringSplitOptions.RemoveEmptyEntries);

         /*   string[] lines =
                Regex.Split(sText, @"(?<=['A-Za-z0-9][""\.\!\?])\s+(?=[A-Z])");

            // - extracting dialog tes t-- not 100% happy with my rough approach
            // This works for simple dialog lines Regex dialog = new Regex("\"([^\"]+)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
             Regex dialog = new Regex("\"([^\"]+)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
             MatchCollection matches = dialog.Matches(sText);

            string[] dialogregex = new string[matches.Count];
           for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                dialogregex[i] = matches[i].Value;
            }




            sText = sText.Replace("\"", "");//if I remove quotes linefeeds work properly
            // seems to catch dialog
            // step2 would be to toss this into an array
            // and then when we do the MERGE below we integrate

   // MERGE INTEGRATE (dialog with non-dialog)
            // 1. look through Match list from Dialog-Regex
            // 2. If this string exists in 's' then we remove it from s
            // 3. and add string from Dialog-Regex
            // 4. Trick? Knowing where the line should appear?

            foreach (string dialogstring in dialogregex)
            {
                // can only do this once I actually break stirngs properly
                // remove quotes for purpose of comparision
                string stempdialogstring = dialogstring.Replace('\"', ' ').Trim();
              

                // #1 easy comparision
                // if identical match, replace, quotes back in
                int nPossibleMatch = lines2.IndexOf(stempdialogstring);
               // string sourcelines = lines2[nPossibleMatch].ToString().Replace("bbb.", "").Replace("bbb", "");
                if (nPossibleMatch > -1)
                {
                    lines2[nPossibleMatch] = dialogstring;
                }

                // #2 - partial match

            }
*/
    #endregion

}


// dialogReview.cs
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Layout;
using CoreUtilities;
using System.Text.RegularExpressions;
using MefAddIns;
namespace WriteThinker
{
    public partial class dialogReview : Form
    {

		Color GrammarColor1 = Color.PaleVioletRed;
		Color GrammarColor2 = Color.LightYellow;

        public dialogReview()
        {
			this.Icon = LayoutDetails.Instance.MainFormIcon;
			FormUtils.SizeFormsForAccessibility(this, LayoutDetails.Instance.MainFormFontSize);
            InitializeComponent();
			richTextBoxExPreview.LineSpace(RichTextExtended.LineSpaceTypes.Double, true);
            checkBoxShowParts.Checked = true;


		
        }

        private DataView view = null; // this view is filled in Setup from the view created in a writeThinker object
        private CharacterInDialogClass[] characters = null;

        /// <summary>
        /// sets up the list of buttons
        /// </summary>
        /// <param name="characters"></param>
        public bool Setup(CharacterInDialogClass[] _characters, DataView _view, string version)
        {

			panelForDialog.Visible = false;
			panelForGrammar.Visible = false;

			System.Windows.Forms.ToolTip  tt= new System.Windows.Forms.ToolTip();
			tt.SetToolTip(ButtonVersion, Loc.Instance.GetString ("Press this button to record that you have proofread the entire layout. This will record a transaction for you."));

			ButtonVersion.Text =  version;
            if (null == _characters)
            {
                MessageBox.Show("There were no characters marked with colors. Exiting the Dialog Review.");
                return false;
            }
            view = _view;
            characters = _characters;
            foreach (CharacterInDialogClass character in characters)
            {
              /*  Button newButton = new Button();
                newButton.FlatStyle = FlatStyle.Popup;
                newButton.Text = character.Name;
                newButton.Dock = DockStyle.Top;
                newButton.Click += new EventHandler(newButton_Click);
                newButton.Text = newButton.Text + " (" + CountLines(character.Name) + ")";*/
               Button newButton = CreateAButton(character.Name + " (" + CountLines(character.Name) + ")",Color.LightBlue);

                panelButtons.Controls.Add(newButton);
                newButton.Tag = character.Name;
                
                newButton = null;
               
                
            }
            panelButtons.Dock = DockStyle.Fill;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Button CreateAButton(string Text, Color color)
        {
            Button newButton = new Button();
            newButton.FlatStyle = FlatStyle.Popup;
            newButton.Text = Text;
            newButton.BackColor = color;
            
            newButton.Dock = DockStyle.Top;
            newButton.Click += new EventHandler(newButton_Click);
           // newButton.Text = newButton.Text + " (" + CountLines(character.Name) + ")";
            return newButton;
        }
        /// <summary>
        /// takes background color and tests if it matches the Colors associated with grammar
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool IsAGrammarButton(Color color)
        {
			if (GrammarColor1== color || GrammarColor2 == color)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  when the button is clicked we show the dialog for this character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void newButton_Click(object sender, EventArgs e)
        {

            // november 2012
            // when CHANGING modes we reset the last found button
            LAST_POSITION_FOUND = 0;

            (sender as Button).ImageAlign = ContentAlignment.MiddleLeft;
			(sender as Button).Image =  FileUtils.GetImage_ForDLL("resources.cross.png");


            //(sender as Button).ForeColor = Color.LightGray; // basically just modify the text once button pressed to help give feeling of PROGRESS

            if ((sender as Button).BackColor == Color.Beige)
            {
                panelForDialog.Visible = false;
                panelForGrammar.Visible = true;
                panelForGrammar.Dock = DockStyle.Fill;
                labelForGrammar.Text = "Line by Line Proofread";
                SelectCharacter("*", "(id > 0) and (hidelinebyline=false)");
            }
            else
            if ((sender as Button).BackColor == verbcolor)
            {
                
                  panelForDialog.Visible = false;
                panelForGrammar.Visible = true;
                panelForGrammar.Dock = DockStyle.Fill;
                labelForGrammar.Text = "Verbs";

                SelectCharacter("*", "startswithverb=true");
            }
            else
            // Grammar
            if (IsAGrammarButton((sender as Button).BackColor))
            {
                panelForDialog.Visible = false;
                panelForGrammar.Visible = true;
                panelForGrammar.Dock = DockStyle.Fill;
                labelForGrammar.Text = (sender as Button).Tag.ToString();// NOPE did not work either.Replace("'","''"); // nov 2012 fixing who's problem screwing up rowfilter
                panelForGrammar.BringToFront();
               // SelectCharacter((sender as Button).Tag.ToString());
                SelectCharacter("**" + (sender as Button).Text.Replace("'","''") + "**");

            }
            else
            {
                panelForGrammar.Visible = false;
                panelForDialog.Visible = true;
                panelForDialog.Dock = DockStyle.Fill;
            // march 2011 - changed this to using tag
            SelectCharacter( (sender as Button).Tag.ToString());
            }
          
        }

        /// <summary>
        /// wrapper function, seperating functionality
        /// </summary>
        /// <param name="sCharacter"></param>
        /// <returns></returns>
        string SetupView(string sCharacter)
        {
            string srowfilter = "";
            foreach (CharacterInDialogClass character in characters)
            {
                if (character.Name == sCharacter)
                {                                                //   = character.GetNames();

                    string[] names = character.GetNames();
                    foreach (string s in names)
                    {
                        labelAlias.Text = labelAlias.Text + " " + s;
                        string sSeperator = "";
                        if (srowfilter != "")
                        {
                            sSeperator = " OR ";
                        }

                        // to escape rowfilter properly
                        string sAlias = s.Replace("\'", "\'\'");

                        srowfilter = String.Format("{0} {1} hesaidshesaid='{2}'"
                        , srowfilter, sSeperator, sAlias);
                    }
                }
            }
            if ("" == srowfilter)
            {
                // for grammar matching (august 2012) just try to buiold a basic 
                srowfilter = String.Format("hesaidshesaid='{0}'", sCharacter);
            }
            return srowfilter;
        }

        /// <summary>
        /// March 2011 
        /// for ease I wanted the count of the 
        /// </summary>
        int CountLines(string sCharacter)
        {
            if (view == null)
            {
                throw new Exception("SelectCharacter  Setup needs to be called before clicking a charactrer button");
            }
            if (characters == null)
            {
                throw new Exception("SelectCharacter characters array not defined");
            }


                string srowfilter = SetupView(sCharacter);
                view.RowFilter = srowfilter;
                return view.Count;
        }
        void SelectCharacter(string sCharacter)
        {
            SelectCharacter(sCharacter, "");
        }
        /// <summary>
        /// selects the character and shows their dialog in the list
        /// </summary>
        /// <param name="sCharacter"></param>
        void SelectCharacter(string sCharacter, string overriderowfilter)
        {
            this.Text = sCharacter;
            if (view == null)
            {
                throw new Exception("SelectCharacter  Setup needs to be called before clicking a charactrer button");
            }
            if (characters == null)
            {
                throw new Exception("SelectCharacter characters array not defined");
            }





            string srowfilter = "";
            if (overriderowfilter != "") 
               srowfilter = overriderowfilter;
            else
                srowfilter = SetupView(sCharacter);

            // buildrowfilter using Alias
            labelAlias.Text = "";

            //November 2012 http://aspnetresources.com/blog/apostrophe_in_rowfilter
            // must escape the apostophe character else causes a crash
            //NOPE: This brok eexisting queries. logic needs to be in the button press, I tink
          // srowfilter = srowfilter.Replace("'", "''");

            view.RowFilter = srowfilter;
           
            listDialog.Visible = false;
            listDialog.BeginUpdate();

            listDialog.DataSource = view;
      
            listDialog.DisplayMember = "Text";
            listDialog.ValueMember = "Id";

            labelLinesValue.Text = listDialog.Items.Count.ToString();

            listDialog.EndUpdate();
            listDialog.Visible = true;

            // select first in list
            if (listDialog.Items.Count > 0)
            {
                //deselect reselect
                listDialog.SelectedIndex = -1;
                listDialog.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Loads and formats the text for parts of speech
        /// </summary>
        private void FillPartOfSpeechTextBox(string text)
        {
            // adding spaces to give the parts of speech "more room" to show up legibly.
            richTextBoxExPreview.Text = text.Replace(" ", "  ") ;
            
            richTextBoxExPreview.Invalidate(); // force repaint
        }

        // location of last search position, so that we don't backtrack. Reset when switching between categories
        int LAST_POSITION_FOUND = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listDialog_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)(listDialog.SelectedItem);

            
            
            if (row != null)
            {
               
                FillPartOfSpeechTextBox(row["Text"].ToString());
                richTextBoxExPreview.BackColor = Color.White;
                if (FindDoubleLetterError(row["Text"].ToString()) > -1)
                {
                    richTextBoxExPreview.BackColor = Color.Red;
                    richTextBoxExPreview.Text = "Two Capital Letters!" + Environment.NewLine+richTextBoxExPreview.Text;
                }
                if ((bool)row["dialog"] == true && FindFirstUpper(row["Text"].ToString()) > -1)
                {
                    richTextBoxExPreview.BackColor = Color.Red;
                    richTextBoxExPreview.Text = "DIRECT ADDRESS ERROR!" + Environment.NewLine + richTextBoxExPreview.Text;


                }
                
                labelconfidence.Text = row["confidence"].ToString();

                labelPassive.Text = String.Format("Passive: {0}", row["IsPassive"].ToString());
                labelSyllables.Text = String.Format("Syllables: {0}", row["syllables"].ToString());

                string sPreviousLine = "";
                string sNextLine = "";
                // look up id
                int id = (int)row["id"] ;
                
                if (id > 0)
                    id = id - 1;
                DataRow foundrow = view.Table.Rows.Find(id);
                if (foundrow != null)
                {
                    sPreviousLine = foundrow["Text"].ToString();
                }

                id = id + 1; // reset
                if (id < view.Table.Rows.Count - 1)
                {
                    id++;
                }
                 foundrow = view.Table.Rows.Find(id);
                if (foundrow != null)
                {
                    sNextLine = foundrow["Text"].ToString();
                }

                labelPrevious.Text = sPreviousLine;
                labelNext.Text = sNextLine;


                // should find it in the source text, if available.
                string sTextToFind = row["Text"].ToString();



                // August 2012  - instead of removing " I'm not icing that when a sentence starts with a " we sometimes have failure
                if (sTextToFind.Length > 0)
                {
                    if (sTextToFind[0] == '"')
                    {
                        //sTextToFind[0] = ' '; // put a blank instead
                        sTextToFind = sTextToFind.TrimStart(new char[1] { '"' });
                        //sTextToFind = sTextToFind.Trim();
                    }
                }

                // August 2012 -- I'm removing this "cleanup". I don't understand why I'd want to remove " marks because I need 'em
                //#
                // sep 2009
                // do a little cleanup on strings so they are more findeable
                //#
                //sTextToFind = sTextToFind.Replace("\"", "");
                //sTextToFind = sTextToFind.Replace("�", "'");

                int nPositionFound = OnFindDialogLine(sTextToFind, LAST_POSITION_FOUND);
                if (nPositionFound > -1)
                {
                    // we record the last position we found up to
                    // then we pass this in the next search so we NEVER backtrack
                    LAST_POSITION_FOUND = nPositionFound;
                }
            }
        }

        private void dialogReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            // in case someone else is using the view, we clean up the row filter
            view.RowFilter = "";
            findDialogLine("", -1);
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {

        }

        public delegate int FindDialogLine(string line, int nLastPosition);
        public event FindDialogLine findDialogLine;

        /// <summary>
        /// November 2012 - cahgnig this to return the line number we found things on
        /// </summary>
        /// <param name="line"></param>
        /// <param name="nLastPosition">If greater than 0 then we search starting at this position. If -1 then we clear the current Selected TExt (generally on form closing</param>
        /// <returns>-1 if not found</returns>
        protected virtual int OnFindDialogLine(string line, int nLastPosition)
        {
            if (findDialogLine != null && line != null)
            {

                return findDialogLine(line, nLastPosition);
            }
            else
            {
               // DatabaseLog.Log("OnInterfaceBeingUsedHasChanged", "", "Warning, OnInterfaceBeingUsed, oSender or interfaceBeingUsedHasChanged are null");
            }
            return -1;
        }

        private void dialogReview_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            CoreUtilities.NewMessage.Show("The dialog parser is not 100% accurate. To improve accuracy set up characters on your StickIt page as described in the online help. Also, be very thorough that all quotation marks are accounted for. One misplaced quotation mark will create many errors here.");
            e.Cancel = true;
        }

        private void dialogReview_Load(object sender, EventArgs e)
        {
            labelLinesValue.Text = "";
            labelNext.Text = "";
            labelPrevious.Text = "";
            labelconfidence.Text = "";
        }

        private void groupcontext_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        /// <summary>
        /// copies the lines to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            string send = "";
            foreach (DataRowView row in listDialog.SelectedItems)
            {
                send = send + "\r\n" + String.Format("{0}: {1}", this.Text, row["text"].ToString());

            }

            Clipboard.SetText(send);
        }

		// http://www.codeproject.com/KB/recipes/wildcardtoregex.aspx
		public static string WildcardToRegex(string pattern)
		{
			return "^" + Regex.Escape(pattern).
				Replace(@"\*", ".*").
					Replace(@"\?", ".") + "$";
		}

        /// <summary>
        /// does the full dialog preview THING + 
        /// </summary>
        /// <param name="advice"></param>
        public void FullProofread(List<Advice> advices, string Text)
        {
            this.Cursor = Cursors.WaitCursor;
            // append custom stuff to the end of the advice array?

     


            // Optimization: try to do ONE resize (i.e., anticipate all my additions NOW
            //Array.Resize(ref advices, advices.Count + 1);
            Advice adder = new Advice();
             adder.sPattern = "***ly";
            adder.sAdvice = "Trim when possible";
            adder.bOverUsedPhrase = false;
            		advices.Add (adder);


			adder = new Advice();
			adder.sPattern = "****ing";
			adder.sAdvice = "Trim when possible";
			adder.bOverUsedPhrase = false;
			advices.Add (adder);

            // how to set colors

            foreach (Advice advice in advices)
            {
                string sPattern = advice.sPattern;
                Color colorToUse = Color.PaleVioletRed;

                System.Text.RegularExpressions.Regex regex ;

				// useful toolf or figuring out regex http://regexpal.com/
				if (sPattern.IndexOf("****") > -1)
				{
					// we do a different regex if passing my wild card in
				//	sPattern = sPattern.Replace("****", "").Trim();
				//	sPattern = WildcardToRegex("then *ing");

					//\bthen\b\s\b.*ing\b"
					// February 2013 - In case I forget the \w{0,20} makes sure there's only a reasonable amount of space/words between the THEN and the ING
					string code = @"\b(then|Then|and|is)\b\s\b\w{0,20}ing\b";
				//	NewMessage.Show (sPattern);
					regex = 

						new System.Text.RegularExpressions.Regex(code,
					                                                 System.Text.RegularExpressions.RegexOptions.IgnoreCase |
					                                                 System.Text.RegularExpressions.RegexOptions.Multiline);
					colorToUse = GrammarColor2;
					sPattern = "ING forms";
				}
				else
                if (sPattern.IndexOf("***") > -1)
                {
                    // we do a different regex if passing my wild card in
                    sPattern = sPattern.Replace("***", "").Trim();
                    regex = new System.Text.RegularExpressions.Regex(@"" + sPattern + @"",
               System.Text.RegularExpressions.RegexOptions.IgnoreCase |
               System.Text.RegularExpressions.RegexOptions.Multiline);
					colorToUse = GrammarColor2;
                }
                else
                {
                    regex = new System.Text.RegularExpressions.Regex(@"\b" + sPattern + @"\b",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                  System.Text.RegularExpressions.RegexOptions.Multiline);
                }
                System.Text.RegularExpressions.MatchCollection matches = regex.Matches(Text, 0);
                string search = "";
                if (matches.Count > 0)
                {
                    Button newButton = CreateAButton(sPattern, colorToUse);
                    panelButtons.Controls.Add(newButton);
                    newButton.Tag = advice.sAdvice;
                    newButton.Text = newButton.Text + "(" + matches.Count.ToString() + ")";
                    search = newButton.Text; // will be used in the following FOREACH to create a search item
                    newButton.BringToFront(); // push down the list
                    newButton = null;
                }

            //    doesn't seem to be finding any words with \b
                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    // grab the line from the index to the first period??
                    int nEnd = Text.IndexOf('.', match.Index);
                    if (nEnd == -1) nEnd = Text.Length;
                    string sub = Text.Substring(match.Index, nEnd - match.Index);
                    sub = sub.Trim();
                    if (sub != null && sub != "" && sub != " ")
                    {
     DataRow row = view.Table.NewRow();
                    row["Text"] = sub;
                    row["hesaidshesaid"] = "**" +search/*+ search.Replace("'","''")*/ + "**";
                    row["isfragment"] = false;
                    row["wordLen"] = 0;
                    row["ispassive"] = false;
                    row["Confidence"] = 100;
                    row["startswithverb"] = false;
                    row["syllables"] = WriteThink.GetSyllables(sub);
                    row["hidelinebyline"] = true;
                    view.Table.Rows.Add(row);
                    }

                


                }
        
               
            }

            int verbcount = 0;
            // need to query the table somehow and get a list of all entries that are verb
            foreach (DataRow row in view.Table.Rows)
            {
                try
                {
                    if ((bool)row["startswithverb"] == true)
                    {
                        verbcount++; // we are already in the database We jsut have to be clever and look only for "startswithverb" in newbutton
                        /* DataRow newrow = view.Table.NewRow();
                         newrow["Text"] = sub;
                         newrow["hesaidshesaid"] = "verbs";
                         newrow["isfragment"] = false;
                         newrow["wordLen"] = 0;
                         newrow["ispassive"] = false;
                         newrow["Confidence"] = 100;
                         newrow["syllables"] = WriteThink.GetSyllables(sub);
                         view.Table.Rows.Add(newrow);*/
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("verb test failed");
                }
            }
            if (verbcount > 0)
            {
                Button newButton = CreateAButton("Verbs at Start", verbcolor);
                panelButtons.Controls.Add(newButton);
                newButton.Tag = "verbs";
                newButton.Text = newButton.Text + "(" + verbcount.ToString() + ")";
                newButton.BringToFront(); // push down the list
                newButton = null;
                //okay, these are not Added as Adice, they have to be custom added at end
            }


            Button linebyline = CreateAButton("Line Proofread", Color.Beige);
            linebyline.Tag = "line";
            panelButtons.Controls.Add(linebyline);
            
            linebyline.BringToFront();
            
                
            linebyline = null;

            // at very end dock the panel
            panelButtons.Dock = DockStyle.Fill;
            this.Cursor = Cursors.Default;
        }
        Color verbcolor = Color.Violet;

        /// <summary>
        /// Looks for error like THis wherein one of the words has a double first letter
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int FindDoubleLetterError(string text)
        {
            for (int i = 0; i < text.Length; i++)
                if (Char.IsUpper(text[i]))
                {
                    if ( (i+1) < text.Length && (i+2) < text.Length)
                    {
                        if (Char.IsUpper(text[i+1]))
                        {
                            // two capital letters
                            if (!Char.IsUpper(text[i+2]))
                            {
                                // two caps followed by a NON caps is likely an error
                                return i;
                            }
                        }
                    }
                }
            return -1;
        }

        /// <summary>
        /// Looking for Direct Address issues i.e.,  "Over here Tom" should be "Over here, Tom"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int FindFirstUpper(string text)
        {
            for (int i = 0; i < text.Length; i++)
                if (Char.IsUpper(text[i]))
                {
                    if (i > 2)
                    {
                        // there must be TWO spaces before this letter to do my test
                        if (text[i - 1] == ' ' && text[i - 2] != ',')
                        {
                            // we have found a cap[ital with only a space before tween
                            // now we make sure this is not hte letter I
                            if (i < (text.Length - 1))
                            {
                                if (text[i + 1] != ' ' && text[i + 1] != '\'' && text[i+1] !=',' && text[i+1] != '.')
                                {
                                     // we are not the letter I.
                                    return i;
                                }
                            }
                            
                        }
                    }

                    
                }

            return -1;
        }

        private void checkBoxShowParts_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxExPreview.ShowPartsOfSpeechMode = (sender as CheckBox).Checked;
            richTextBoxExPreview.Invalidate();
        }
    }
}
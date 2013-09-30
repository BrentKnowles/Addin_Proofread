// CharacterInDialogClass.cs
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
using System.Collections;
using System.Drawing;

namespace WriteThinker
{
    /// <summary>
    /// a mini class for handling character information
    /// within the dialog parser
    /// 
    /// 
    /// </summary>
    public class CharacterInDialogClass
    {
        public enum gender {male, female, other, any};
        public string Name;
        public gender Gender;
        /// <summary>
        /// A number from 1 to 100
        /// The higher the number the more likely this charracter
        /// will be chosen in the case of a 'tie'
        /// when anlaysizing the dialog
        /// </summary>
        public int Tilt;

        public Color color = Color.Black; // for marking characters by colors Oct 29 2011

        public string[] Alias; // will be included in list of names; during final
                               // name-assignment they will be rolled into Name

        public string Lines=""; // This is ONLY used for the color detection code. We add to this data while parsing the text( October 2011)
        /// <summary>
        /// go through the arry and find maximum tilt
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        static public string GetMostLikelyName(CharacterInDialogClass[] characters, gender sex)
        {
            int nMax = -1;
            string sName = "";
            foreach (CharacterInDialogClass character in characters)
            {
                // we can searcha ll as well
                if (character.Tilt > nMax && (character.Gender == sex || sex == gender.any) )
                {
                    nMax = character.Tilt;
                    sName = character.Name;
                }
            }
            return sName;
        }

        /// <summary>
        /// returns Name + Alias
        /// </summary>
        /// <returns></returns>
        public string[] GetNames()
        {
            if (Alias != null)
            {
                string[] names = new string[Alias.Length + 1];
                names[0] = Name;
                Array.Copy(Alias, 0, names, 1, Alias.Length);
//				string debug = "";
//				foreach (string sss in names)
//				{
//					debug = debug + sss;
//				}
//				CoreUtilities.NewMessage.Show (debug);
                return names;
            }
            else
            {
                return new string[1] { Name };
            }
        }

        /// <summary>
        /// Returns the names of all characters + all alias in the passed in array
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static string[] GetAllCharacterNames(CharacterInDialogClass[] characters, gender testGender)
        {
            if (characters == null)
            {
                throw new Exception("GetAllCharacterNames You must pass in a valid character array");
            }

            ArrayList names = new ArrayList();
            foreach (CharacterInDialogClass character in characters)
            {
                if (character == null)
                {
                    throw new Exception("GetAllCharacterNames - somehow you have an invalid character in your list. Did you forget to iterate the [x] indexer when assigning characters?");
                }
                if (character.Gender == testGender || testGender == gender.any)
                {
                    string[] _names = character.GetNames();

                    foreach (string name in _names)
                    {
                        names.Add(name);
                    }
                }
            }
            if (names.Count <= 0)
            {
                //throw new Exception("GetAllCharacterNames - no names in character list!?");
                // this might be valid sometimes (i.e., no female names)
            }

            string[] allnames = new string[names.Count];
            names.CopyTo(allnames);

            return allnames;


        }

        /// <summary>
        /// Goes througha nd looks for a match for the alias
        /// , returning the first found
        /// case insensitive
        /// Will return directly if sAlias = name
        /// </summary>
        /// <param name="characters"></param>
        static public string GetNameFromAlias(string sAlias, CharacterInDialogClass[] characters)
        {
            
            if (sAlias == null || sAlias == "")
            {
                //throw new Exception("Alias null or blank");
                return ""; // t his is actually okay, just means there were no alias
            }
            if (characters == null || characters.Length <= 0)
            {
                throw new Exception("No characters");
            }
            string sAliasCheck = sAlias.ToLower();
            foreach (CharacterInDialogClass character in characters)
            {

                if (character.Name.ToLower() == sAliasCheck)
                {
                    return sAlias;
                }

                if (character.Alias != null)
                {
                    foreach (string s in character.Alias)
                    {
                        if (s.ToLower() == sAliasCheck)
                        {
                            // an alias matched, return the name
                            return character.Name;
                        }
                    }
                }
            }
            return sAlias; // if not match return myself
        }

        /// <summary>
        /// returns a regex expression that can be used
        /// to filter a string for the presence of any of these names
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        static public string GetRegexForNames(CharacterInDialogClass[] characters)
        {
            string sResult = "";
            string[] names = GetAllCharacterNames(characters, gender.any);
            foreach (string s in names)
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

    }
}

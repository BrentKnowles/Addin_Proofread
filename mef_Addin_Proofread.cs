// mef_Addin_Proofread.cs
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
namespace MefAddIns
{

	using MefAddIns.Extensibility;
	using System.ComponentModel.Composition;
	using System;
	using System.Windows.Forms;
	using CoreUtilities;
	using System.IO;
	using System.Collections.Generic;
	using Layout;
	using WriteThinker;
	using System.Data;
	using System.Collections;
	using System.Drawing;
	using AddIn_Proofread;

	using HotKeys;
	/// <summary>
	/// Provides an implementation of a supported language by implementing ISupportedLanguage. 
	/// Moreover it uses Export attribute to make it available thru MEF framework.
	/// </summary>
	[Export(typeof(mef_IBase))]
	public class Addin_Proofread : PlugInBase, mef_IBase
	{
		
		#region variables
		WriteThinker.WriteThink writeThink = null;
		dialogReview review_form =null;
		dialogReview review
		{
			get { return review_form; }
			set { review_form = value; }
			
		}

		Layout.RichTextExtended CurrentTextBox=null;
		#endregion
		public const string EXTRAWORDS = "list_extrawords";
		public Addin_Proofread()
		{
			guid = "Proofread";
		}
		
		public string Author
		{
			get { return @"Brent Knowles"; }
		}
		public string Version
		{
			// 1.6 Englex improvements, flexible look up + STat Panel Return (F3)
			// 1.5 Key navigation
			// 1.4 improving look of Englex hookup
			// 1.3 added Englex API hookup
			// 1.2. Making characters linkable
			// 1.1. added toggle view to character and icon for storyboard
			get { return @"1.6.0.0"; }
		}
		public string Description
		{
			get { return "A variety of proofreading tools."; }
		}
		public string Name
		{
			get { return @"Proofread"; }
		}
		

		

		public override bool DeregisterType ()
		{
			
		//do not actually remove the addin type since we force shutdown
			
			//NewMessage.Show ("need to remove from list");
			return true;
			//Layout.LayoutDetails.Instance.AddToList(typeof(NoteDataXML_Picture.NoteDataXML_Pictures), "Picture");
		}
	

		
		mef_IBase myAddInOnMainFormForHotKeys = null;
		Action<mef_IBase> myRunnForHotKeys=null;
		public override void AssignHotkeys (ref List<HotKeys.KeyData> Hotkeys, ref mef_IBase addin, Action<mef_IBase> Runner)
		{
			
			base.AssignHotkeys (ref Hotkeys, ref addin, Runner);
			myAddInOnMainFormForHotKeys = addin;
			myRunnForHotKeys=Runner;
			Hotkeys.Add (new KeyData (Loc.Instance.GetString ("Proofread"), HotkeyAction, Keys.Alt, Keys.P, Constants.BLANK, true, guid));
			Hotkeys.Add (new KeyData (Loc.Instance.GetString ("Toggle In Proofread"), ToggleInside,Keys.None, Keys.F2, "dialogReview", true, guid+"2"));
			Hotkeys.Add (new KeyData (Loc.Instance.GetString ("Toggle Outside Proofread"), ToggleOutside,Keys.None, Keys.F1, "dialogReview", true, guid+"3"));
			Hotkeys.Add (new KeyData (Loc.Instance.GetString ("Stat Panel"), StatPanel,Keys.None, Keys.F3, "dialogReview", true, guid+"4"));
			
		}
		public void StatPanel (bool b)
		{
			if (writeThink != null && review != null) {
				review.AlwaysOnTop.Checked = false;
				writeThink.ShowStatPanel(review.Icon);
			}
		}
		public void ToggleInside (bool b)
		{

			if (review != null) {
				review.HandleToggleInside();
			}

		}
		public void ToggleOutside (bool b)
		{
			if (review != null) {
				//review.Text="Toggle Outside";
				Application.OpenForms [0].Activate ();
				((appframe.MainFormBase)Application.OpenForms [0]).LastMainForm = review;
			}

		}
		public void HotkeyAction(bool b)
		{
			if (myRunnForHotKeys != null && myAddInOnMainFormForHotKeys != null)
				myRunnForHotKeys(myAddInOnMainFormForHotKeys);
			
		}

		public const string SYSTEM_GRAMMAR="list_grammar";

		public override void RegisterType()
		{
			LayoutDetails.Instance.AddTo_TransactionsLIST(typeof(Transactions.TransactionUpdateProofreadVersion));
			Layout.LayoutDetails.Instance.AddToList(typeof(NoteDataXML_Character), Loc.Instance.GetString ("Character") , PlugInBase.AddInFolderName);

			// Build default table of grammar

			string TableName = SYSTEM_GRAMMAR;
			LayoutPanels.NoteDataXML_Panel PanelContainingTables = LayoutPanel.GetPanelToAddTableTo (TableName);
			if (PanelContainingTables != null) {
				
				// create the note
				NoteDataXML_Table	randomTables = new NoteDataXML_Table(100, 100 , new ColumnDetails[4]{new ColumnDetails("id",100), 
					new ColumnDetails("pattern",100), new ColumnDetails("advice",100), new ColumnDetails("overused",100)});

				randomTables.Caption = TableName;
				
				
				PanelContainingTables.AddNote (randomTables);
				randomTables.CreateParent (PanelContainingTables.GetPanelsLayout ());
				
				randomTables.AddRow(new object[4]{
					"1", "1.0", @"The first row of this table is a version number. Feel free to edit it when major changes are made to this list. On each Layout you can record the last grammar version you have checked it against.", "0"}
				);
				
				randomTables.AddRow(new object[4]{
					"2", "Among", @"When more than two things or persons are involved, among is usually called for.", "0"}
				);
				randomTables.AddRow(new object[4]{
					"3", "As to whether", @"Whether is sufficient.", "1"}
				);

				//		LayoutDetails.Instance.TableLayout.SaveLayout();
				PanelContainingTables.GetPanelsLayout ().SaveLayout ();
				//NewMessage.Show("Making new");
				// now we reload the system version
				LayoutDetails.Instance.TableLayout.LoadLayout (LayoutDetails.TABLEGUID, true, null);
				//BringToFrontAndShow ();
			}


		}
		public void RespondToMenuOrHotkey<T>(T form) where T: System.Windows.Forms.Form, MEF_Interfaces.iAccess 
		{
	//		NewMessage.Show ("Hook hotkey up to -- just called ActionParam");
			
	
			// not used for this addin
			//NewMessage.Show ("Spellchecking");
			
			if (LayoutDetails.Instance.CurrentLayout != null && LayoutDetails.Instance.CurrentLayout.CurrentTextNote != null) {
				
				//	NewMessage.Show ("Do Something!");	
				
				
				
				
				Proofread(LayoutDetails.Instance.CurrentLayout.CurrentTextNote.GetRichTextBox());
				
				
			} else {
				NewMessage.Show (Loc.Instance.GetString ("Please select a note first."));
			}
			
	
			return;
		}
	
		public void ActionWithParamForNoteTextActions (object param)
		{
			
			if (LayoutDetails.Instance.CurrentLayout != null && LayoutDetails.Instance.CurrentLayout.CurrentTextNote != null) {
				
				//	NewMessage.Show ("Do Something!");	
				
				//Accepting that when ProofreadByColor we do not get a QuickLink for the form
				
				
				ProofreadByColor(LayoutDetails.Instance.CurrentLayout.CurrentTextNote.GetRichTextBox());
				
				
			} else {
				NewMessage.Show (Loc.Instance.GetString ("Please select a note first."));
			}
		}

		private string PathToSpeakerRules ()
		{   string fullpath = Path.Combine (LayoutDetails.Instance.Path, "dictionary");
			fullpath = Path.Combine (fullpath, "speakerrules.xml");
			return fullpath;
		}
		private void Setup ()
		{

			if (File.Exists (PathToSpeakerRules ()) == false) {
				System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly ();

				FileUtils.PreparePullResource(_assembly, "resources.speakerrules.xml", PathToSpeakerRules ());
			}
		}


		private NoteDataXML_Character ConvertLinkToCharacter (NoteDataXML_LinkNote link)
		{
			NoteDataXML_Character character = new NoteDataXML_Character (link);
			// probably unnecessary
			character.Data1 = link.Data1;

			RichTextBox box = new RichTextBox ();
			box.Rtf = link.Data1;
			string sText = box.Text;

			string sGender = General.SubStringBetween (sText, "Gender: ", "\n");
			if (sGender == "") {
				character.Caption = "error_proofread";
			} else {
				character.Gender = sGender;
				
				string sColor = General.SubStringBetween (sText, "Color: ", "\n").ToLower ().Trim ();
				
				character.ColorName = (sColor);
				
				int nPriority = 0;
				string sPriority = General.SubStringBetween (sText, "Priority: ", "\n");
				try {
					nPriority = Int32.Parse (sPriority);
				} catch (Exception) {
					
				}
				character.Priority = nPriority;
				
					
				// now grab the alias Alias: \\par
				
				string sAlias = General.SubStringBetween (sText, "Alias: ", "\n");
				character.Alias = sAlias;
				box.Dispose ();
			}
			return character;
		}

		public WriteThinker.CharacterInDialogClass ParseTextForCharacter (NoteDataXML_Character characterNote)
		{
			WriteThinker.CharacterInDialogClass character = new WriteThinker.CharacterInDialogClass();
			if (characterNote.Gender == "Male")
				character.Gender = CharacterInDialogClass.gender.male;
			else
				if (characterNote.Gender == "Female")
				character.Gender = CharacterInDialogClass.gender.female;
			else
				character.Gender = CharacterInDialogClass.gender.other;


			character.color = characterNote.CharacterColor;
			character.Tilt = characterNote.Priority;
			character.Name = characterNote.Caption;

			string sAlias =characterNote.Alias;
			if (sAlias.IndexOf(",") == -1)
			{
				if (sAlias != "")
				{
					character.Alias = new string[1] { sAlias.Trim() };
				}
			}
			else
				if (sAlias != "")
			{
				character.Alias = sAlias.Split(',');
			}
			else
			{
				character.Alias = null;
			}

			return character;
		}

		/// <summary>
		/// the text comes in as richtext and we return a character, if one found
		/// </summary>
		/// <param name="sText"></param>
		/// <returns></returns>
//		public WriteThinker.CharacterInDialogClass ParseTextForCharacter(string sText, string sCaption)
//		{
//			//ToDO: This is deprecated, replaced with Character note
//			int nGender = sText.IndexOf("Gender: ");
//			if (nGender > -1)
//			{
//				WriteThinker.CharacterInDialogClass character = new WriteThinker.CharacterInDialogClass();
//				
//				character.Gender = WriteThinker.CharacterInDialogClass.gender.male;
//				// the text is RichTextFormat!
//				string sGender = General.SubStringBetween(sText, "Gender: ", "\n");
//				if (sGender.ToLower().Trim() == "female")
//				{
//					character.Gender = WriteThinker.CharacterInDialogClass.gender.female;
//				}
//				else
//					if (sGender.ToLower().Trim() == "other")
//				{
//					character.Gender = WriteThinker.CharacterInDialogClass.gender.other;
//				}
//				
//				string sColor = General.SubStringBetween(sText, "Color: ", "\n").ToLower().Trim();
//				
//				character.color = System.Drawing.Color.FromName(sColor);
//				
//				int nPriority = 0;
//				string sPriority = General.SubStringBetween(sText, "Priority: ", "\n");
//				try
//				{
//					nPriority = Int32.Parse(sPriority);
//				}
//				catch (Exception)
//				{
//					
//				}
//				character.Tilt = nPriority;
//				
//				character.Name = sCaption;
//				
//				
//				// now grab the alias Alias: \\par
//				
//				string sAlias = General.SubStringBetween(sText, "Alias: ", "\n");
//				if (sAlias.IndexOf(",") == -1)
//				{
//					if (sAlias != "")
//					{
//						character.Alias = new string[1] { sAlias.Trim() };
//					}
//				}
//				else
//					if (sAlias != "")
//				{
//					character.Alias = sAlias.Split(',');
//				}
//				else
//				{
//					character.Alias = null;
//				}
//				
//				return character;
//			}
//			return null;
//			
//		}
		private CharacterInDialogClass[] GetCharacterArray()
		{
		


			// parse through all notes, extracting character information
			ArrayList characters_temp = new ArrayList();
			WriteThinker.CharacterInDialogClass character = null;
		//	System.Windows.Forms.RichTextBox box = new System.Windows.Forms.RichTextBox();
			
			foreach (NoteDataInterface note in LayoutDetails.Instance.CurrentLayout.GetAllNotes())
			{
				// throw new Exception("need to fix word count");
				if (note != null)
				{
					//if (ap.ShapeType == Appearance.shapetype.Note)
					if (note is NoteDataXML_Character || note is NoteDataXML_LinkNote)
					{
						//if (note.Data1 != null && note.Data1 != Constants.BLANK)
						{
							NoteDataInterface NoteToUse = note;
							// if a link we need to convert it to a character note first
							if (note is NoteDataXML_LinkNote)
							{
								try
								{
									NoteToUse = ConvertLinkToCharacter(note as NoteDataXML_LinkNote);
								}
								catch (Exception ex)
								{
									NewMessage.Show (ex.ToString ());
								}
							}

							// we don't add if this linknote was not actually linked toa  character
							if (NoteToUse.Caption != "error_proofread")
							{
							//	box.Rtf = "";
								//box.Rtf = note.Data1;
								// We NEED to convert the RTF to text for easier parsing and allowing formatting like bolds
								character = ParseTextForCharacter(NoteToUse as NoteDataXML_Character);
								
								if (character != null)
								{
									characters_temp.Add(character);
								}
							}
						}
					}
					
					
				}
				
			}
			//box = null;
			
			
			// this probably should be part of BASE NOTE OBJECT???
			WriteThinker.CharacterInDialogClass[] characters = new WriteThinker.CharacterInDialogClass[characters_temp.Count];
			characters_temp.CopyTo(characters);
			
			
			return characters;

		}


		/// <summary>
		/// calculates stats for the STATISTICS popup and for Dialog Review
		/// </summary>
		private void GenerateWordData(RichTextBox RichText)
		{
			RichText.Parent.Cursor = Cursors.WaitCursor;
			if (writeThink == null)
			{
				writeThink = new WriteThinker.WriteThink();

				writeThink.speakerrules = PathToSpeakerRules ();

			}
			
			CharacterInDialogClass[] characters =GetCharacterArray();
			if (characters != null)
			{
				writeThink.LoadTextStringIntoDatabase(RichText.Text, characters);
			}
			else
			{
				// no character info
				writeThink.LoadTextStringIntoDatabase(RichText.Text, null);
			}
		
			
			
			
			
			writeThink.GetSecondsElapsedSinceLoadTextStringIntoDatabase();
			
			writeThink.view = new DataView(writeThink.table);
			RichText.Parent.Cursor = Cursors.Default;
		}
		public override object ActiveForm ()
		{
			return review;
		}

		 /// <summary>
        /// Builds a full proofread experience
        ///  - Dialog Review
        ///  - Grammar
        ///  - Ly words
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void Proofread (Layout.RichTextExtended textBox)
		{
			CurrentTextBox = textBox;
			Setup ();

			NoteDataXML_Table extraDetails = (NoteDataXML_Table) LayoutDetails.Instance.CurrentLayout.FindNoteByName (EXTRAWORDS);
//			if (extraDetails != null) {
//			//	extraDetails = (NoteDataXML_Table)LayoutDetails.Instance.CurrentLayout.GoToNote(extraDetails);
//			}


			//DataTable extraDetails =   page_Visual.GetExtraGrammarNotes(); // grab data table


        

			string version = "0.0";
			List<Advice> advice = Advice.ConvertTableToList(ref version);// (RichBoxLinks.Advice[])General.DeSerialize(EditFileToUse, typeof(RichBoxLinks.Advice[]));

            if (extraDetails != null)
            {
                if (extraDetails.GetRows().Count > -0)
                {
					List<DataRow> rows = extraDetails.GetRows();
					for (int i = 0; i < rows.Count; i++)
                    {

					
						{
                        Advice newAdvice = new Advice();
                        newAdvice.sPattern = rows[i][1].ToString(); // assuming column 0 is an id column??
                        newAdvice.sAdvice = rows[i][2].ToString();
                        newAdvice.bOverUsedPhrase = false;
                        advice.Add(newAdvice);
						}
                    }
                }
            }

           
			GenerateWordData(CurrentTextBox);

            //  writeThink.AdminGrid(0); // eventually hook up only to console command?
            if (review == null)
            {
                review = new dialogReview();
            }
            else
            {
                review.Close();
                review = new dialogReview();

            }
			review.FormClosing+= HandleFormClosing;
            review.findDialogLine += new dialogReview.FindDialogLine(review_findDialogLine);


            if (review.Setup(writeThink.characters, writeThink.view, version) == true)
            {
                review.FullProofread(advice, CurrentTextBox.Text); // needs to be after setup if going to use view.table
              //  review.Icon = RichText.Icons;
                review.Show();
            }
        }

        void HandleFormClosing (object sender, FormClosingEventArgs e)
        {
			RemoveQuickLinks();
        }
		/// <summary>
		/// Find this line
		/// </summary>
		/// <param name="line"></param>
		/// <returns>nLastPosition unless a later position is found</returns>
		int review_findDialogLine(string line, int nLastPosition)
		{
			// remove quotes since different formats use different types of quotes
			// ToDo _ removing quote does not help
			
			//line = line.Replace("\"", "");
			// if fancy apostophe available then swap it
			/*  if (RichText.Text.IndexOf("’") > 0)
            {
                if (line[0] == '\"')
                {
                    line = "“" + line.Substring(1, line.Length-1);
                }
                line = line.Replace("\"", "");
                line = line.Replace("\'", "’");
            }*/
			if (-1 == nLastPosition)
			{
				// means clear SELECTED text and exit;
				CurrentTextBox.SelectionStart = 0;
				CurrentTextBox.SelectionLength = 0;
				return -1;
			}
			
			
		//	if (SetupForFind(line) == true)
			{
				//  throw new Exception("boo");
				LayoutDetails.Instance.CurrentLayout.GetFindbar().DoFind(line,false,CurrentTextBox, nLastPosition);
				// NOVEMBER 2012 Need to be smart: Don't want to completely rewrite functionality. Grab the array of found items. Iterate through it. Don't rewrite those old functions
				// February 2013 - disabled this, seeing if existing findbar features are sufficient
//				if (founditems != null && founditems.Length > 0)
//				{
//					// we have items. Now search for positions > nLastPosition
//					for (int i = 0; i < founditems.Length; i++)
//					{
//						if ((int)founditems[i] > nLastPosition)
//						{
//							// go to this position
//							CurrentTextBox.SelectionStart = (int)founditems[i];
//							nLastPosition = (int)founditems[i]; // We reset nLastPosition to this one
//							break;
//						}
//					}
//				}
				
				// if any exists then parse the list of found items in the array
			}
			
			return nLastPosition;
			
		}



		private void ProofreadByColor(Layout.RichTextExtended textBox)
		{
			CurrentTextBox = textBox;
			Setup ();

			string version = "bycolor";
			CurrentTextBox.Parent.Cursor = Cursors.WaitCursor;
	

			GenerateWordData(CurrentTextBox);
			
			//  writeThink.AdminGrid(0); // eventually hook up only to console command?
			if (review == null)
			{
				review = new dialogReview();
			}
			else
			{
				review.Close();
				review = new dialogReview();
				
			}
			review.FormClosing+= HandleFormClosing;
			review.findDialogLine += new dialogReview.FindDialogLine(review_findDialogLine);


		
			
			
			

			{
				
				// * I think the algorithm would be different in that I would be added text directly to the database, associated with a specific character in the
				// array
				
				// We scroll through the RichText, outputting only colored text
				
				
				int i = 0;
				
				Color cdefault = Color.Black;
				Color lastColor = Color.Black;
				CurrentTextBox.bSuspendUpdateSelection = true;
				CurrentTextBox.BeginUpdate();
				
				
				
				
				
				//RichBoxLinks.progressForm progress = new RichBoxLinks.progressForm(RichText.Text.Length, "Analyzing Dialog");
				

				LayoutDetails.Instance.Progress.Value = 0;
				LayoutDetails.Instance.Progress.Step = 1;
				LayoutDetails.Instance.Progress.Maximum = CurrentTextBox.Text.Length;
				for (i = 0; i < CurrentTextBox.Text.Length; i++)
				{
					LayoutDetails.Instance.Progress.PerformStep();
					CurrentTextBox.SelectionStart = i;
					CurrentTextBox.SelectionLength = 1;
					if (0 == i)
					{
						// the first background color becomes the DEFAULT
						cdefault = CurrentTextBox.SelectionBackColor;
					}
					if (CurrentTextBox.SelectionBackColor != cdefault)
					{
						
						foreach (CharacterInDialogClass character in writeThink.characters)
						{
							
							
							
							if (character.color == CurrentTextBox.SelectionBackColor)
							{
								
								if (lastColor != CurrentTextBox.SelectionBackColor)
								{
									character.Lines = character.Lines + Environment.NewLine; // if we've changed colors, just coming to here, insert a linefeed
								}
								
								character.Lines = character.Lines + CurrentTextBox.SelectedText;
								lastColor = CurrentTextBox.SelectionBackColor;
							}
						}
						
						
					}
					else
					{
						lastColor = cdefault; // reset so we put linefeeds in where needed
					}
				}
				CurrentTextBox.bSuspendUpdateSelection = false;
				CurrentTextBox.EndUpdate();
				//progress.Close();
				// uncomment this for final code CharacterInDialogClass[] characters = RichText.GetCharacterArray();
				
				int count = 0;
				
				foreach (CharacterInDialogClass character in writeThink.characters)
				{
					if (character.Lines != "")
					{
						count++;
						bool keepdatabase = false;
						if (count > 1)
						{
							keepdatabase = true;
						}
						writeThink.LoadTextStringIntoDatabase(character.Lines, writeThink.characters, character.Name, keepdatabase);
					}
					
				}
				
				
				
			} // characters null
			
			writeThink.GetSecondsElapsedSinceLoadTextStringIntoDatabase();
			
			writeThink.view = new DataView(writeThink.table);
			CurrentTextBox.Parent.Cursor = Cursors.Default;
			
	
			
			
			if (review.Setup(writeThink.characters, writeThink.view, version) == true)
			{
				//review.Icon = RichText.Icons;
				review.Show();
			}
		}


		public override string dependencymainapplicationversion { get { return "1.1.0.0"; }}
		
		//override string GUID{ get { return  "notedataxml_picture"; };
		public PlugInAction CalledFrom { 
			get
			{
				PlugInAction action = new PlugInAction();
				//	action.HotkeyNumber = -1;
				action.MyMenuName = Loc.Instance.GetString ("Proofread");

				action.ParentMenuName = "TextEditContextStrip";
				action.IsOnContextStrip = true;
				action.IsOnAMenu = true;


				action.IsANote = true;
				action.IsNoteAction = true;
				action.QuickLinkShows = true;
				action.NoteActionMenuOverride = Loc.Instance.GetString ("Proofread by Color");
				action.ToolTip =Loc.Instance.GetString("Proofreading tools.");
				//action.Image = FileUtils.GetImage_ForDLL("camera_add.png");
				action.GUID = GUID;
				return action;
			} 
		}
		
		
		
	}
}
// NoteDataXML_Character.cs
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
using CoreUtilities;
using System.Windows.Forms;
using System.Drawing;
using CoreUtilities.Links;
using Layout;
using System.Xml.Serialization;
namespace AddIn_Proofread
{
	public class NoteDataXML_Character  : Layout.NoteDataXML_RichText
	{
		public override int defaultHeight { get { return 500; } }
		public override int defaultWidth { get { return 300; } }
		#region variables
		public override bool IsLinkable { get { return true; }}
		
		public override string GetLinkData ()
		{
			string returnvalue = this.Data1;
			try {
				// now add custom stuff to front/top of it.
				System.Windows.Forms.RichTextBox box = new RichTextBox ();
				box.Rtf = this.Data1;
				string gender = String.Format ("Gender: {0}\n", this.gender);
				string color = String.Format ("Color: {0}\n", this.colorName);
				string priority = String.Format ("Priority: {0}\n", this.priority);
				string alias = String.Format ("Alias: {0}\n", this.alias);

				box.SelectionStart = 0;
				Clipboard.SetText (gender + color + priority + alias);
				box.Paste ();
				box.SelectedText = "\n";
				returnvalue = box.Rtf;

			} catch (Exception ex) {
				NewMessage.Show (ex.ToString ());
			}

			return returnvalue;
		}
		
#endregion
		
		#region interface
		TableLayoutPanel TablePanel = null;
		ComboBox ColorCombo= null;
		Label ColorLabel = null;
#endregion


		int view = 0;
		/// <summary>
		/// Gets or sets the view.
		/// 0 = Character Details On Top
		/// 1 = Character Details to the Left
		/// 2 = Character Details to the Right
		/// </summary>
		/// <value>
		/// The view.
		/// </value>
		public int View {
			get {
				return view;
			}
			set {
				view = value;
			}
		}

		string gender="Male";

		public string Gender {
			get {
				return gender;
			}
			set {
				gender = value;
			}
		}
	
		string alias=Constants.BLANK;

		public string Alias {
			get {
				return alias;
			}
			set {
				alias = value;
			}
		}
		int priority = 50;

		public int Priority {
			get {
				return priority;
			}
			set {
				priority = value;
			}
		}

		Color characterColor = Color.White;
		// what is reade
		[XmlIgnore]
		public Color CharacterColor {
			get {
				return Color.FromName (colorName);
			}
//			set {
//				characterColor = value;
//			}
		}
//
		string colorName = Color.White.Name;

		public string ColorName {
			get {
				return colorName;
			}
			set {
				colorName = value;
			}
		}
		protected override string GetIcon ()
		{
			return @"%*user.png";
		}
//		int characterColorInt = Color.White.ToArgb();
//		public  int CharacterColorInt // what is stored
//		{
//			get
//			{
//				return characterColorInt;
//			}
//			set
//			{
//				characterColorInt =(value);
//				
//			}
//		}


		public override void Dispose ()
		{
		
			
			base.Dispose();
			
		}
		private void CommonConstructor ()
		{
			Caption = Loc.Instance.GetString("Character");
		}
		public NoteDataXML_Character () : base()
		{
			CommonConstructor();
		}
		public NoteDataXML_Character(int height, int width):base(height, width)
		{
			CommonConstructor();
		}

		void SetupForView ()
		{
			if (TablePanel != null) {
				switch (view) {
				case 0: TablePanel.Dock = DockStyle.Top;
					break;
				case 1: TablePanel.Dock = DockStyle.Left; break;
				case 2: TablePanel.Dock = DockStyle.Right; break;
				}
			}
		}
		
		protected override void DoBuildChildren (LayoutPanelBase Layout)
		{
			base.DoBuildChildren (Layout);
		
			
			
			CaptionLabel.Dock = DockStyle.Top;
	
			TablePanel = new TableLayoutPanel ();
			TablePanel.RowCount = 4;
			TablePanel.ColumnCount = 2;
			TablePanel.Dock = DockStyle.Top;
			ParentNotePanel.Controls.Add (TablePanel);
			TablePanel.BringToFront ();
			TablePanel.AutoSize = true;
			//
			// GENDER
			//

			Label GenderLabel = new Label ();
			GenderLabel.Text = Loc.Instance.GetString ("Gender");
			GenderLabel.Click += (object sender, EventArgs e) => BringToFrontAndShow ();
			ComboBox GenderCombo = new ComboBox ();
			GenderCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			GenderCombo.Items.Add ("Male");
			GenderCombo.Items.Add ("Female");
			GenderCombo.Items.Add ("Other");

		
			if (Gender == Constants.BLANK) {
				GenderCombo.SelectedIndex = 0;
			} else {
				for (int i = 0; i < GenderCombo.Items.Count; i++) {
					//NewMessage.Show (String.Format ("Comparing {0} to {1}", GenderCombo.Items [i].ToString (), Gender));
					if (GenderCombo.Items [i].ToString () == Gender) {
						GenderCombo.SelectedIndex = i;
						break;
					}
				}
			}
			GenderCombo.SelectedIndexChanged += HandleGenderSelectedIndexChanged;

			//
			// PRIORITY
			//

			Label PriorityLabel = new Label ();
			PriorityLabel.Text = Loc.Instance.GetString ("Priority");
			PriorityLabel.Click += (object sender, EventArgs e) => BringToFrontAndShow ();


			NumericUpDown PriorityPicker = new NumericUpDown ();
			PriorityPicker.Maximum = 100;
			PriorityPicker.Minimum = 0;
			PriorityPicker.Value = Priority;
			PriorityPicker.ValueChanged += HandleValueChanged;

			ToolTip tipster = new ToolTip ();
			tipster.SetToolTip (PriorityPicker, 
			                   Loc.Instance.GetString ("The higher the more likely Proofreader will select this character as the current speaker."));


			//
			// Alias
			//

			Label AliasLabel = new Label ();
			AliasLabel.Text = Loc.Instance.GetString ("Alias");
			AliasLabel.Click += (object sender, EventArgs e) => BringToFrontAndShow ();
			TextBox AliasEdit = new TextBox ();
			tipster.SetToolTip (AliasEdit, Loc.Instance.GetString ("Enter a comma separated list of alternate names for this character to assist the Proofreader in determining the current speaker."));

			AliasEdit.Text = Alias;
			AliasEdit.TextChanged += HandleTextChanged;
			//
			// COLOR
			//
			 ColorLabel = new Label ();
			ColorLabel.Text = Loc.Instance.GetString ("Color");
			ColorLabel.Click += (object sender, EventArgs e) => BringToFrontAndShow ();
			 ColorCombo = new ComboBox ();
			ColorCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			int count = 0;
			int matchcount = 0;
			ColorCombo.DisplayMember = "Name";


		//	CharacterColor = Color.FromArgb(characterColorInt);
		//	NewMessage.Show("Loading Color " + CharacterColor.Name);
			foreach (Color c in LayoutDetails.Instance.HighlightColorList) {
				ColorCombo.Items.Add (c);
				//NewMessage.Show ("Comparing " + c.Name + " to " + CharacterColor.Name);
				if (c.Name ==  ColorName)
				{
					//NewMessage.Show ("Match " + c.Name);
					matchcount = count;
				}
				count++;
			}

			ColorCombo.SelectedIndex = matchcount;
			ColorCombo.SelectedIndexChanged+= HandleSelectedColorIndexChanged;

			ColorLabel.BackColor = (Color)ColorCombo.SelectedItem;
			ColorLabel.ForeColor = TextUtils.InvertColor(ColorLabel.BackColor);


			ToolStripButton ToggleView = new ToolStripButton();
			ToggleView.Text = Loc.Instance.GetString ("Toggle View");
			ToggleView.Click+= HandleToggleViewClick;

			TablePanel.Controls.Add(GenderLabel,0,0);
			TablePanel.Controls.Add(GenderCombo,1,0);
			TablePanel.Controls.Add(PriorityLabel,0,1);
			TablePanel.Controls.Add(PriorityPicker,1,1);
			TablePanel.Click+= (object sender, EventArgs e) => BringToFrontAndShow();
			TablePanel.Controls.Add(AliasLabel,0,2);
			TablePanel.Controls.Add(AliasEdit,1,2);

			TablePanel.Controls.Add (ColorLabel,0, 3);
			TablePanel.Controls.Add (ColorCombo,1, 3);

		
			properties.DropDownItems.Add (ToggleView);
			SetupForView();

			richBox.BringToFront();

		}

		void HandleToggleViewClick (object sender, EventArgs e)
		{
			View++;
			if (View > 2) View = 0;
			SetupForView ();
		}

		void HandleSelectedColorIndexChanged (object sender, EventArgs e)
		{
			if ((sender as ComboBox).SelectedItem != null) {
			//	NewMessage.Show ("Changing color to ", (sender as ComboBox).SelectedItem.ToString());
				ColorName =  ((Color)(sender as ComboBox).SelectedItem).Name;
				ColorLabel.BackColor = (Color)(sender as ComboBox).SelectedItem;
				ColorLabel.ForeColor = TextUtils.InvertColor(ColorLabel.BackColor);
			}

		}

		void HandleTextChanged (object sender, EventArgs e)
		{
			Alias = (sender as TextBox).Text;
		}

		void HandleValueChanged (object sender, EventArgs e)
		{
			Priority = (int)(sender as NumericUpDown).Value;
		}

		void HandleGenderSelectedIndexChanged (object sender, EventArgs e)
		{
			if ((sender as ComboBox).SelectedItem != null) {Gender = (sender as ComboBox).Text;}
		}
		protected override void DoChildAppearance (AppearanceClass app)
		{
			base.DoChildAppearance (app);

			TablePanel.BackColor = app.mainBackground;

		}
	public override void Save ()
		{
			base.Save ();
			//CharacterColorInt = CharacterColor.ToArgb();
		}
		
	
		/// <summary>
		/// Registers the type.
		/// </summary>
		public override string RegisterType()
		{
			return Loc.Instance.GetString("Character");
		}

		public NoteDataXML_Character(NoteDataInterface Note) : base(Note)
		{

		}
		public override void CopyNote (NoteDataInterface Note)
		{
			base.CopyNote (Note);
			if (Note is NoteDataXML_Character)
			{
				this.Gender = (Note as NoteDataXML_Character).Gender;
				this.Priority = (Note as NoteDataXML_Character).Priority;
				this.Alias = (Note as NoteDataXML_Character).Alias;
				this.ColorName =  (Note as NoteDataXML_Character).ColorName;
			}
		}

		protected override AppearanceClass UpdateAppearance ()
		{
			AppearanceClass app =  base.UpdateAppearance ();
			if (app != null)
			{
				TablePanel.BackColor = app.mainBackground;
				TablePanel.ForeColor = app.secondaryForeground;

			}
			return app;
		}

	

	}
}


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
		
		#region variables
		public override bool IsLinkable { get { return true; }}
		

		
#endregion
		
		#region interface
		TableLayoutPanel TablePanel = null;

#endregion
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
			Label ColorLabel = new Label ();
			ColorLabel.Text = Loc.Instance.GetString ("Color");
			ColorLabel.Click += (object sender, EventArgs e) => BringToFrontAndShow ();
			ComboBox ColorCombo = new ComboBox ();
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



			TablePanel.Controls.Add(GenderLabel,0,0);
			TablePanel.Controls.Add(GenderCombo,1,0);
			TablePanel.Controls.Add(PriorityLabel,0,1);
			TablePanel.Controls.Add(PriorityPicker,1,1);
			TablePanel.Click+= (object sender, EventArgs e) => BringToFrontAndShow();
			TablePanel.Controls.Add(AliasLabel,0,2);
			TablePanel.Controls.Add(AliasEdit,1,2);

			TablePanel.Controls.Add (ColorLabel,0, 3);
			TablePanel.Controls.Add (ColorCombo,1, 3);

		

			richBox.BringToFront();

		}

		void HandleSelectedColorIndexChanged (object sender, EventArgs e)
		{
			if ((sender as ComboBox).SelectedItem != null) {
			//	NewMessage.Show ("Changing color to ", (sender as ComboBox).SelectedItem.ToString());
				ColorName =  ((Color)(sender as ComboBox).SelectedItem).Name;
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

		

	}
}


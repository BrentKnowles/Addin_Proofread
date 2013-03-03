using System;
using CoreUtilities;
using System.Windows.Forms;
using System.Drawing;
using CoreUtilities.Links;
using Layout;
namespace AddIn_Proofread
{
	public class NoteDataXML_Character  : Layout.NoteDataXML_RichText
	{
		
		#region variables
		public override bool IsLinkable { get { return true; }}
		

		
#endregion
		
		#region interface
	
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
	
			TableLayoutPanel GenderPanel = new TableLayoutPanel();
			GenderPanel.RowCount = 4;
			GenderPanel.ColumnCount = 2;
			GenderPanel.Dock = DockStyle.Top;
			ParentNotePanel.Controls.Add(GenderPanel);
			GenderPanel.BringToFront();


			Label GenderLabel = new Label();
			GenderLabel.Text = Loc.Instance.GetString ("Gender");

			ComboBox GenderCombo = new ComboBox();
			GenderCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			GenderCombo.Items.Add ("Male");
			GenderCombo.Items.Add ("Female");
			GenderCombo.Items.Add ("Other");

			GenderPanel.Controls.Add(GenderLabel,0,0);
			GenderPanel.Controls.Add(GenderCombo,1,0);



			richBox.BringToFront();

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


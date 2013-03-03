using System;
using CoreUtilities;
namespace Transactions
{
	public class TransactionUpdateProofreadVersion : TransactionBase
	{
		public TransactionUpdateProofreadVersion (DateTime date, string LayoutGuid, string Version) : base()
		{
			RowData[TransactionsTable.TYPE.Index] = 3002;
			RowData[TransactionsTable.DATE.Index] = date;
			RowData[TransactionsTable.DATA1_LAYOUTGUID.Index] = LayoutGuid;
			RowData[TransactionsTable.DATA2.Index] = Version;
		}
		public TransactionUpdateProofreadVersion(object[] items): base(items)
		{
			// all children need this form of the constructor
		}
		// This will probably never be seen because the note is gone, but just in case
		public override string Display {
			get {
				return Loc.Instance.GetStringFmt("Last Proofread Version Updated: {1} On: {0}",DateAsFriendlyString(), RowData[TransactionsTable.DATA2.Index] );
			}
		}

	}
}


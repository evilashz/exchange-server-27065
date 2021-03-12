using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000AF RID: 175
	public class MailboxList : DisposableBase
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00023EB4 File Offset: 0x000220B4
		public MailboxList(Context context, Column[] columns, StoreDatabase database, MailboxList.ListType listType)
		{
			IMailboxListRestriction mailboxListRestrictionFromListType = MailboxList.GetMailboxListRestrictionFromListType(listType);
			this.tableOperator = MailboxList.GetTableOperator(context, columns, database, mailboxListRestrictionFromListType);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00023EDE File Offset: 0x000220DE
		public MailboxList(Context context, Column[] columns, StoreDatabase database, IMailboxListRestriction mailboxListRestriction)
		{
			this.tableOperator = MailboxList.GetTableOperator(context, columns, database, mailboxListRestriction);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00023EF6 File Offset: 0x000220F6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxList>(this);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00023EFE File Offset: 0x000220FE
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.reader != null)
				{
					this.reader.Dispose();
					this.reader = null;
				}
				if (this.tableOperator != null)
				{
					this.tableOperator.Dispose();
					this.tableOperator = null;
				}
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00023F37 File Offset: 0x00022137
		public Reader OpenList()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("MailboxList");
			}
			this.reader = this.tableOperator.ExecuteReader(false);
			return this.reader;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00023F64 File Offset: 0x00022164
		private static TableOperator GetTableOperator(Context context, Column[] columns, StoreDatabase database, IMailboxListRestriction mailboxListRestriction)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(database);
			return Factory.CreateTableOperator(context.Culture, context, mailboxTable.Table, mailboxListRestriction.Index(mailboxTable), columns, mailboxListRestriction.Filter(context), null, 0, 0, KeyRange.AllRows, false, true);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00023FA4 File Offset: 0x000221A4
		private static IMailboxListRestriction GetMailboxListRestrictionFromListType(MailboxList.ListType listType)
		{
			IMailboxListRestriction result = null;
			switch (listType)
			{
			case MailboxList.ListType.Active:
				result = new MailboxListRestrictionActive();
				break;
			case MailboxList.ListType.ActiveAndDisabled:
				result = new MailboxListRestrictionActiveAndDisabled();
				break;
			case MailboxList.ListType.ActiveAndDisconnected:
				result = new MailboxListRestrictionActiveAndDisconnected();
				break;
			case MailboxList.ListType.SoftDeleted:
				result = new MailboxListRestrictionSoftDeleted();
				break;
			case MailboxList.ListType.FinalCleanup:
				result = new MailboxListRestrictionFinalCleanup();
				break;
			default:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Unknown ListType value");
				break;
			}
			return result;
		}

		// Token: 0x04000434 RID: 1076
		private Reader reader;

		// Token: 0x04000435 RID: 1077
		private TableOperator tableOperator;

		// Token: 0x020000B0 RID: 176
		public enum ListType
		{
			// Token: 0x04000437 RID: 1079
			Active,
			// Token: 0x04000438 RID: 1080
			ActiveAndDisabled,
			// Token: 0x04000439 RID: 1081
			ActiveAndDisconnected,
			// Token: 0x0400043A RID: 1082
			SoftDeleted,
			// Token: 0x0400043B RID: 1083
			FinalCleanup
		}
	}
}

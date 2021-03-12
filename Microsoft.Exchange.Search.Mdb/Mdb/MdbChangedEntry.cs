using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200001A RID: 26
	internal sealed class MdbChangedEntry
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000666C File Offset: 0x0000486C
		internal MdbChangedEntry(MdbChangedType changedType, MdbInfo mailboxDatabaseInfo)
		{
			this.changedType = changedType;
			this.mailboxDatabaseInfo = mailboxDatabaseInfo;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00006682 File Offset: 0x00004882
		internal MdbChangedType ChangedType
		{
			get
			{
				return this.changedType;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000668A File Offset: 0x0000488A
		internal MdbInfo MailboxDatabaseInfo
		{
			get
			{
				return this.mailboxDatabaseInfo;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006692 File Offset: 0x00004892
		public override string ToString()
		{
			return string.Format("{0} has changed to {1}.", this.MailboxDatabaseInfo, this.ChangedType);
		}

		// Token: 0x04000060 RID: 96
		private readonly MdbChangedType changedType;

		// Token: 0x04000061 RID: 97
		private readonly MdbInfo mailboxDatabaseInfo;
	}
}

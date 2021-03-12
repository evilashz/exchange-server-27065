using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000491 RID: 1169
	[DataContract]
	public class SupervisionListEntryRow : BaseRow
	{
		// Token: 0x06003A4E RID: 14926 RVA: 0x000B07D8 File Offset: 0x000AE9D8
		public SupervisionListEntryRow(SupervisionListEntry supervisionListEntry) : base(supervisionListEntry.ToIdentity(), supervisionListEntry)
		{
			this.SupervisionListEntry = supervisionListEntry;
		}

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000B07EE File Offset: 0x000AE9EE
		// (set) Token: 0x06003A50 RID: 14928 RVA: 0x000B07F6 File Offset: 0x000AE9F6
		public SupervisionListEntry SupervisionListEntry { get; private set; }

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000B07FF File Offset: 0x000AE9FF
		// (set) Token: 0x06003A52 RID: 14930 RVA: 0x000B080C File Offset: 0x000AEA0C
		[DataMember]
		public string EntryName
		{
			get
			{
				return this.SupervisionListEntry.EntryName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x06003A53 RID: 14931 RVA: 0x000B0813 File Offset: 0x000AEA13
		// (set) Token: 0x06003A54 RID: 14932 RVA: 0x000B0820 File Offset: 0x000AEA20
		public string Tag
		{
			get
			{
				return this.SupervisionListEntry.Tag;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}

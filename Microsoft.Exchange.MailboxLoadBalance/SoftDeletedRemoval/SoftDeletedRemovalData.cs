using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x02000103 RID: 259
	[DataContract]
	internal class SoftDeletedRemovalData : IExtensibleDataObject
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00015FB5 File Offset: 0x000141B5
		public SoftDeletedRemovalData(DirectoryIdentity sourceDatabase, DirectoryIdentity targetDatabase, DirectoryIdentity mailboxIdentity, long itemCount, DateTime? disconnectDate)
		{
			this.TargetDatabase = targetDatabase;
			this.MailboxIdentity = mailboxIdentity;
			this.ItemCount = itemCount;
			this.DisconnectDate = disconnectDate;
			this.SourceDatabase = sourceDatabase;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00015FE2 File Offset: 0x000141E2
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00015FEA File Offset: 0x000141EA
		[DataMember]
		public DateTime? DisconnectDate { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00015FF3 File Offset: 0x000141F3
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00015FFB File Offset: 0x000141FB
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00016004 File Offset: 0x00014204
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0001600C File Offset: 0x0001420C
		[DataMember]
		public long ItemCount { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00016015 File Offset: 0x00014215
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001601D File Offset: 0x0001421D
		[DataMember]
		public DirectoryIdentity MailboxIdentity { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00016026 File Offset: 0x00014226
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001602E File Offset: 0x0001422E
		[DataMember]
		public DirectoryIdentity SourceDatabase { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00016037 File Offset: 0x00014237
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0001603F File Offset: 0x0001423F
		[DataMember]
		public DirectoryIdentity TargetDatabase { get; private set; }
	}
}

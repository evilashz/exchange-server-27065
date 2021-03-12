using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000037 RID: 55
	[DataContract]
	internal sealed class ContainerMailboxInformation
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00004551 File Offset: 0x00002751
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00004559 File Offset: 0x00002759
		[DataMember(IsRequired = true)]
		public Guid MailboxGuid { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00004562 File Offset: 0x00002762
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000456A File Offset: 0x0000276A
		[DataMember(IsRequired = true)]
		public byte[] PersistableTenantPartitionHint { get; set; }
	}
}

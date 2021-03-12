using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009B RID: 155
	[DataContract]
	internal sealed class NameValuePair
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000AF33 File Offset: 0x00009133
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0000AF3B File Offset: 0x0000913B
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000AF44 File Offset: 0x00009144
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0000AF4C File Offset: 0x0000914C
		[DataMember]
		public string Value { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000AF55 File Offset: 0x00009155
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000AF5D File Offset: 0x0000915D
		[DataMember]
		public DateTime LastWrite { get; set; }
	}
}

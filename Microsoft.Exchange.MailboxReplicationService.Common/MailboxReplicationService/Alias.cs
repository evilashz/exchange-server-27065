using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009C RID: 156
	[DataContract]
	internal sealed class Alias
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000AF6E File Offset: 0x0000916E
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0000AF76 File Offset: 0x00009176
		[DataMember]
		public string MservKeyName { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000AF7F File Offset: 0x0000917F
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000AF87 File Offset: 0x00009187
		[DataMember]
		public DateTime? Created { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000AF90 File Offset: 0x00009190
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000AF98 File Offset: 0x00009198
		[DataMember]
		public DateTime LastWrite { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000AFA1 File Offset: 0x000091A1
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000AFA9 File Offset: 0x000091A9
		[DataMember]
		public bool ManagedShouldExistInMserv { get; set; }
	}
}

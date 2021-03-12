using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000064 RID: 100
	[DataContract]
	internal sealed class PropProblemData
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00009282 File Offset: 0x00007482
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000928A File Offset: 0x0000748A
		[DataMember(IsRequired = true)]
		public int PropTag { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00009293 File Offset: 0x00007493
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000929B File Offset: 0x0000749B
		[DataMember(IsRequired = true)]
		public int Scode { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000092A4 File Offset: 0x000074A4
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x000092AC File Offset: 0x000074AC
		[DataMember]
		public int Index { get; set; }
	}
}

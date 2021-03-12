using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F1 RID: 2545
	[DataContract]
	public class ItemId
	{
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x00100CE8 File Offset: 0x000FEEE8
		// (set) Token: 0x060047F0 RID: 18416 RVA: 0x00100CF0 File Offset: 0x000FEEF0
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x00100CF9 File Offset: 0x000FEEF9
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x00100D01 File Offset: 0x000FEF01
		[DataMember]
		public string ChangeKey { get; set; }
	}
}

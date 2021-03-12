using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F2 RID: 2546
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LoadExtensionCustomPropertiesParameters
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x060047F4 RID: 18420 RVA: 0x00100D12 File Offset: 0x000FEF12
		// (set) Token: 0x060047F5 RID: 18421 RVA: 0x00100D1A File Offset: 0x000FEF1A
		[DataMember(IsRequired = true, Order = 1)]
		public string ExtensionId { get; set; }

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x00100D23 File Offset: 0x000FEF23
		// (set) Token: 0x060047F7 RID: 18423 RVA: 0x00100D2B File Offset: 0x000FEF2B
		[DataMember(IsRequired = true, Order = 2)]
		public string ItemId { get; set; }
	}
}

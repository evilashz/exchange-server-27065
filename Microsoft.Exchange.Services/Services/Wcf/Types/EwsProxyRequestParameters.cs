using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E3 RID: 2531
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EwsProxyRequestParameters
	{
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x00100410 File Offset: 0x000FE610
		// (set) Token: 0x06004770 RID: 18288 RVA: 0x00100418 File Offset: 0x000FE618
		[DataMember(IsRequired = true, Order = 1)]
		public string Body { get; set; }

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06004771 RID: 18289 RVA: 0x00100421 File Offset: 0x000FE621
		// (set) Token: 0x06004772 RID: 18290 RVA: 0x00100429 File Offset: 0x000FE629
		[DataMember(IsRequired = true, Order = 2)]
		public string Token { get; set; }

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x00100432 File Offset: 0x000FE632
		// (set) Token: 0x06004774 RID: 18292 RVA: 0x0010043A File Offset: 0x000FE63A
		[DataMember(IsRequired = true, Order = 3)]
		public string ExtensionId { get; set; }
	}
}

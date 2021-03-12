using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E4 RID: 2532
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetExtensibilityContextParameters
	{
		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x0010044B File Offset: 0x000FE64B
		// (set) Token: 0x06004777 RID: 18295 RVA: 0x00100453 File Offset: 0x000FE653
		[DataMember(IsRequired = true, Order = 1)]
		public FormFactor FormFactor { get; set; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x0010045C File Offset: 0x000FE65C
		// (set) Token: 0x06004779 RID: 18297 RVA: 0x00100464 File Offset: 0x000FE664
		[DataMember(IsRequired = true, Order = 2)]
		public string ClientLanguage { get; set; }
	}
}

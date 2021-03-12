using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B0D RID: 2829
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnifiedMessagingInfo : OptionsPropertyChangeTracker
	{
		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x0600504D RID: 20557 RVA: 0x00109726 File Offset: 0x00107926
		// (set) Token: 0x0600504E RID: 20558 RVA: 0x0010972E File Offset: 0x0010792E
		public string EnableTemplate { get; set; }

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x0600504F RID: 20559 RVA: 0x00109737 File Offset: 0x00107937
		// (set) Token: 0x06005050 RID: 20560 RVA: 0x0010973F File Offset: 0x0010793F
		public string DisableTemplate { get; set; }

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06005051 RID: 20561 RVA: 0x00109748 File Offset: 0x00107948
		// (set) Token: 0x06005052 RID: 20562 RVA: 0x00109750 File Offset: 0x00107950
		public string CallForwardingType { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E8 RID: 2536
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernGroupExternalResources
	{
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600479D RID: 18333 RVA: 0x0010067F File Offset: 0x000FE87F
		// (set) Token: 0x0600479E RID: 18334 RVA: 0x00100687 File Offset: 0x000FE887
		[DataMember]
		public string SharePointUrl { get; set; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x00100690 File Offset: 0x000FE890
		// (set) Token: 0x060047A0 RID: 18336 RVA: 0x00100698 File Offset: 0x000FE898
		[DataMember]
		public string DocumentsUrl { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B4 RID: 948
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ComplianceConfiguration
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000767C9 File Offset: 0x000749C9
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x000767D1 File Offset: 0x000749D1
		[DataMember]
		public RmsTemplateType[] RmsTemplates { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x000767DA File Offset: 0x000749DA
		// (set) Token: 0x06001E5C RID: 7772 RVA: 0x000767E2 File Offset: 0x000749E2
		[DataMember]
		public MessageClassificationType[] MessageClassifications { get; set; }
	}
}

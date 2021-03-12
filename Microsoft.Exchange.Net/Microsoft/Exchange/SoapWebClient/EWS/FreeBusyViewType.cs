using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000361 RID: 865
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum FreeBusyViewType
	{
		// Token: 0x04001449 RID: 5193
		None = 1,
		// Token: 0x0400144A RID: 5194
		MergedOnly = 2,
		// Token: 0x0400144B RID: 5195
		FreeBusy = 4,
		// Token: 0x0400144C RID: 5196
		FreeBusyMerged = 8,
		// Token: 0x0400144D RID: 5197
		Detailed = 16,
		// Token: 0x0400144E RID: 5198
		DetailedMerged = 32
	}
}

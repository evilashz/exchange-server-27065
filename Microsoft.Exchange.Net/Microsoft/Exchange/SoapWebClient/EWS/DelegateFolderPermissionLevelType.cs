using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002CF RID: 719
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DelegateFolderPermissionLevelType
	{
		// Token: 0x0400123E RID: 4670
		None,
		// Token: 0x0400123F RID: 4671
		Editor,
		// Token: 0x04001240 RID: 4672
		Reviewer,
		// Token: 0x04001241 RID: 4673
		Author,
		// Token: 0x04001242 RID: 4674
		Custom
	}
}

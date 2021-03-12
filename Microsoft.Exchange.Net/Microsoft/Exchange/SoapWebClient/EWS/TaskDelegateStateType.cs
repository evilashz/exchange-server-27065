using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000243 RID: 579
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum TaskDelegateStateType
	{
		// Token: 0x04000F6F RID: 3951
		NoMatch,
		// Token: 0x04000F70 RID: 3952
		OwnNew,
		// Token: 0x04000F71 RID: 3953
		Owned,
		// Token: 0x04000F72 RID: 3954
		Accepted,
		// Token: 0x04000F73 RID: 3955
		Declined,
		// Token: 0x04000F74 RID: 3956
		Max
	}
}

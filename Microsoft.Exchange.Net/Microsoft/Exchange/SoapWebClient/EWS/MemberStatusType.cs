using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200026D RID: 621
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MemberStatusType
	{
		// Token: 0x0400101F RID: 4127
		Unrecognized,
		// Token: 0x04001020 RID: 4128
		Normal,
		// Token: 0x04001021 RID: 4129
		Demoted
	}
}

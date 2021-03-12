using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000462 RID: 1122
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum GroupMemberIdentifierType
	{
		// Token: 0x04001723 RID: 5923
		ExternalDirectoryObjectId,
		// Token: 0x04001724 RID: 5924
		LegacyExchangeDN,
		// Token: 0x04001725 RID: 5925
		SmtpAddress
	}
}

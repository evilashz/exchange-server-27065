using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C8 RID: 712
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum InvalidRecipientResponseCodeType
	{
		// Token: 0x0400121B RID: 4635
		OtherError,
		// Token: 0x0400121C RID: 4636
		RecipientOrganizationNotFederated,
		// Token: 0x0400121D RID: 4637
		CannotObtainTokenFromSTS,
		// Token: 0x0400121E RID: 4638
		SystemPolicyBlocksSharingWithThisRecipient,
		// Token: 0x0400121F RID: 4639
		RecipientOrganizationFederatedWithUnknownTokenIssuer
	}
}

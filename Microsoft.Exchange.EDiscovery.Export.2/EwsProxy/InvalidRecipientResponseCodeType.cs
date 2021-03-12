using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E7 RID: 487
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum InvalidRecipientResponseCodeType
	{
		// Token: 0x04000DC9 RID: 3529
		OtherError,
		// Token: 0x04000DCA RID: 3530
		RecipientOrganizationNotFederated,
		// Token: 0x04000DCB RID: 3531
		CannotObtainTokenFromSTS,
		// Token: 0x04000DCC RID: 3532
		SystemPolicyBlocksSharingWithThisRecipient,
		// Token: 0x04000DCD RID: 3533
		RecipientOrganizationFederatedWithUnknownTokenIssuer
	}
}

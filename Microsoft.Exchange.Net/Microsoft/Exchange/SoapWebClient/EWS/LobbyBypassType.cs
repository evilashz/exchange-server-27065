using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200022B RID: 555
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum LobbyBypassType
	{
		// Token: 0x04000E33 RID: 3635
		Disabled,
		// Token: 0x04000E34 RID: 3636
		EnabledForGatewayParticipants
	}
}

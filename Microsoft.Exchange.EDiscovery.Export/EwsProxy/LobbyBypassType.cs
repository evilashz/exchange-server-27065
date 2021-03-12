using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200014A RID: 330
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum LobbyBypassType
	{
		// Token: 0x040009E1 RID: 2529
		Disabled,
		// Token: 0x040009E2 RID: 2530
		EnabledForGatewayParticipants
	}
}

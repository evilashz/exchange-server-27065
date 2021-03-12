using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005FF RID: 1535
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum LobbyBypass
	{
		// Token: 0x04001B9C RID: 7068
		Disabled,
		// Token: 0x04001B9D RID: 7069
		EnabledForGatewayParticipants
	}
}

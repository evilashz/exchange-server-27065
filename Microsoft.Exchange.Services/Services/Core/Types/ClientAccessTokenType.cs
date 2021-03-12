using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005CB RID: 1483
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ClientAccessTokenType
	{
		// Token: 0x04001ADE RID: 6878
		CallerIdentity,
		// Token: 0x04001ADF RID: 6879
		ExtensionCallback,
		// Token: 0x04001AE0 RID: 6880
		ScopedToken
	}
}

using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F4 RID: 244
	[XmlType(TypeName = "ClientExtensionProvidedTo", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ClientExtensionProvidedTo
	{
		// Token: 0x040004CB RID: 1227
		Everyone,
		// Token: 0x040004CC RID: 1228
		SpecificUsers
	}
}

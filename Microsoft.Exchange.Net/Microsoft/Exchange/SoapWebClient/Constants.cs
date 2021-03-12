using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006C5 RID: 1733
	internal static class Constants
	{
		// Token: 0x04001F35 RID: 7989
		public const string MessagesNamespace = "http://schemas.microsoft.com/exchange/services/2006/messages";

		// Token: 0x04001F36 RID: 7990
		public const string TypesNamespace = "http://schemas.microsoft.com/exchange/services/2006/types";

		// Token: 0x04001F37 RID: 7991
		public static readonly XmlNamespaceDefinition[] EwsNamespaces = new XmlNamespaceDefinition[]
		{
			new XmlNamespaceDefinition("exm", "http://schemas.microsoft.com/exchange/services/2006/messages"),
			new XmlNamespaceDefinition("ext", "http://schemas.microsoft.com/exchange/services/2006/types")
		};
	}
}

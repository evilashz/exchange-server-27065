using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols
{
	// Token: 0x0200082B RID: 2091
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class WellKnownUserAgent
	{
		// Token: 0x06002C57 RID: 11351 RVA: 0x00064C71 File Offset: 0x00062E71
		public static string GetEwsNegoAuthUserAgent(string value)
		{
			return "ExchangeInternalEwsClient-" + value;
		}

		// Token: 0x040026D7 RID: 9943
		public const string EwsNegoAuthUserAgentPrefix = "ExchangeInternalEwsClient-";
	}
}

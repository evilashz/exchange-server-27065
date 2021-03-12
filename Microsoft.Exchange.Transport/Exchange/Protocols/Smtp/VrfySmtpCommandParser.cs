using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000490 RID: 1168
	internal static class VrfySmtpCommandParser
	{
		// Token: 0x06003537 RID: 13623 RVA: 0x000D92E1 File Offset: 0x000D74E1
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001B17 RID: 6935
		public const string CommandKeyword = "VRFY";
	}
}

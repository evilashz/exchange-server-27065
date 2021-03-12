using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000478 RID: 1144
	internal static class ExpnSmtpCommandParser
	{
		// Token: 0x0600349B RID: 13467 RVA: 0x000D6A62 File Offset: 0x000D4C62
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001AC2 RID: 6850
		public const string CommandKeyword = "EXPN";
	}
}

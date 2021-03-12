using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000486 RID: 1158
	internal static class NoopSmtpCommandParser
	{
		// Token: 0x06003504 RID: 13572 RVA: 0x000D829D File Offset: 0x000D649D
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001AF1 RID: 6897
		public const string CommandKeyword = "NOOP";
	}
}

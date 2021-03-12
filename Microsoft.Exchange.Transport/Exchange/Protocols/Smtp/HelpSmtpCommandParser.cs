using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200047C RID: 1148
	internal static class HelpSmtpCommandParser
	{
		// Token: 0x060034A7 RID: 13479 RVA: 0x000D6C98 File Offset: 0x000D4E98
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state, out string helpArgs)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			context.TrimLeadingWhitespace();
			context.GetCommandArguments(out helpArgs);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001ACA RID: 6858
		public const string CommandKeyword = "HELP";
	}
}

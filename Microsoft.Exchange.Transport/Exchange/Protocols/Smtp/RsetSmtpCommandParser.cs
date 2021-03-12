using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200048E RID: 1166
	internal static class RsetSmtpCommandParser
	{
		// Token: 0x06003534 RID: 13620 RVA: 0x000D922B File Offset: 0x000D742B
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			if (context.HasArguments)
			{
				return ParseResult.InvalidArguments;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001B11 RID: 6929
		public const string CommandKeyword = "RSET";
	}
}

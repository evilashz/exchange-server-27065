using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000489 RID: 1161
	internal static class QuitSmtpCommandParser
	{
		// Token: 0x06003511 RID: 13585 RVA: 0x000D83AC File Offset: 0x000D65AC
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

		// Token: 0x04001AF8 RID: 6904
		public const string CommandKeyword = "QUIT";
	}
}

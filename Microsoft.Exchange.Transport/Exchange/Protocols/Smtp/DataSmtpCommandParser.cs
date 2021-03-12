using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000477 RID: 1143
	internal static class DataSmtpCommandParser
	{
		// Token: 0x0600349A RID: 13466 RVA: 0x000D6A08 File Offset: 0x000D4C08
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			ArgumentValidator.ThrowIfNull("state.TransportMailItem", state.TransportMailItem);
			if (state.TransportMailItem.BodyType == BodyType.BinaryMIME)
			{
				return ParseResult.BadCommandSequence;
			}
			if (context.HasArguments)
			{
				return ParseResult.InvalidArguments;
			}
			return ParseResult.MoreDataRequired;
		}

		// Token: 0x04001AC1 RID: 6849
		public const string CommandKeyword = "DATA";
	}
}

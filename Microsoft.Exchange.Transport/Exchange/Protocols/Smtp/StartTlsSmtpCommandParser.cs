using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200048F RID: 1167
	internal static class StartTlsSmtpCommandParser
	{
		// Token: 0x06003535 RID: 13621 RVA: 0x000D9258 File Offset: 0x000D7458
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state, SecureState startTlsOrAnonymousTls)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			if (context.HasArguments)
			{
				return StartTlsSmtpCommandParser.InvalidArguments;
			}
			if ((startTlsOrAnonymousTls == SecureState.StartTls && !state.IsStartTlsSupported) || (startTlsOrAnonymousTls == SecureState.AnonymousTls && !state.IsAnonymousTlsSupported))
			{
				return StartTlsSmtpCommandParser.UnrecognizedCommand;
			}
			return StartTlsSmtpCommandParser.ParsingComplete;
		}

		// Token: 0x04001B12 RID: 6930
		public const string AnonymousTlsCommandKeyword = "X-ANONYMOUSTLS";

		// Token: 0x04001B13 RID: 6931
		public const string StartTlsCommandKeyword = "STARTTLS";

		// Token: 0x04001B14 RID: 6932
		public static readonly ParseResult ParsingComplete = new ParseResult(ParsingStatus.Complete, SmtpResponse.Empty, false);

		// Token: 0x04001B15 RID: 6933
		public static readonly ParseResult InvalidArguments = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidArguments, false);

		// Token: 0x04001B16 RID: 6934
		public static readonly ParseResult UnrecognizedCommand = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.UnrecognizedCommand, false);
	}
}

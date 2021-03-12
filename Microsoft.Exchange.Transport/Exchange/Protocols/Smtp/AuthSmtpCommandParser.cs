using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000475 RID: 1141
	internal static class AuthSmtpCommandParser
	{
		// Token: 0x0600348B RID: 13451 RVA: 0x000D63AC File Offset: 0x000D45AC
		public static ParseResult ParseAuthMechanism(CommandContext context, SmtpInSessionState sessionState, AuthCommand command, out AuthParseOutput parseOutput)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			Offset offset;
			if (!context.GetNextArgumentOffset(out offset))
			{
				context.LogReceivedCommand(sessionState.ProtocolLogSession);
				sessionState.Tracer.TraceError(0L, "No auth mechanism specified after auth or x-exps verb");
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			byte[] array = new byte[offset.End - context.OriginalOffset];
			Buffer.BlockCopy(context.Command, context.OriginalOffset, array, 0, array.Length);
			sessionState.ProtocolLogSession.LogReceive(array);
			if (command == AuthCommand.XExps && !sessionState.ReceiveConnector.HasExchangeServerAuthMechanism)
			{
				sessionState.Tracer.TraceError(0L, "X-EXPS command can only appear if Exchange Server auth mechanisms is set");
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			if (SmtpInSessionUtils.IsAuthenticated(sessionState.RemoteIdentity))
			{
				switch (sessionState.AuthMethod)
				{
				case MultilevelAuthMechanism.TLSAuthLogin:
				case MultilevelAuthMechanism.Login:
				case MultilevelAuthMechanism.NTLM:
				case MultilevelAuthMechanism.GSSAPI:
				case MultilevelAuthMechanism.MUTUALGSSAPI:
					parseOutput = null;
					return ParseResult.AuthAlreadySpecified;
				}
			}
			if (AuthSmtpCommandParser.IsAuthMechanism(context, offset, AuthSmtpCommandParser.Login))
			{
				return AuthSmtpCommandParser.AuthLoginDetected(context, sessionState.ReceiveConnector, sessionState.SecureState, command, sessionState.ProtocolLogSession, out parseOutput);
			}
			if (AuthSmtpCommandParser.IsAuthMechanism(context, offset, AuthSmtpCommandParser.ExchangeAuth))
			{
				return AuthSmtpCommandParser.ExchangeAuthDetected(context, sessionState, command, out parseOutput);
			}
			if (AuthSmtpCommandParser.IsAuthMechanism(context, offset, AuthSmtpCommandParser.GSSAPI))
			{
				return AuthSmtpCommandParser.GssapiAuthDetected(context, sessionState, command, out parseOutput);
			}
			if (AuthSmtpCommandParser.IsAuthMechanism(context, offset, AuthSmtpCommandParser.NTLM))
			{
				return AuthSmtpCommandParser.NtlmAuthDetected(context, sessionState, command, out parseOutput);
			}
			sessionState.Tracer.TraceError(0L, "Auth mechanism not supported");
			parseOutput = null;
			return ParseResult.AuthUnrecognized;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000D652F File Offset: 0x000D472F
		private static bool IsAuthMechanism(CommandContext context, Offset nextArgumentOffset, byte[] mechanism)
		{
			return BufferParser.CompareArg(mechanism, context.Command, nextArgumentOffset.Start, nextArgumentOffset.Length);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000D654C File Offset: 0x000D474C
		private static ParseResult AuthLoginDetected(CommandContext context, ReceiveConnector receiveConnector, SecureState secureState, AuthCommand command, IProtocolLogSession protocolLogSession, out AuthParseOutput parseOutput)
		{
			if (command == AuthCommand.XExps || !receiveConnector.HasBasicAuthAuthMechanism)
			{
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			bool flag = secureState == SecureState.StartTls || secureState == SecureState.AnonymousTls;
			bool hasBasicAuthRequireTlsAuthMechanism = receiveConnector.HasBasicAuthRequireTlsAuthMechanism;
			MultilevelAuthMechanism multilevelAuthMechanism;
			if (flag)
			{
				multilevelAuthMechanism = MultilevelAuthMechanism.TLSAuthLogin;
			}
			else
			{
				if (hasBasicAuthRequireTlsAuthMechanism)
				{
					protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Inbound ExchangeAuth negotiation failed because connection is not TLS");
					parseOutput = null;
					return ParseResult.AuthUnrecognized;
				}
				multilevelAuthMechanism = MultilevelAuthMechanism.Login;
			}
			context.TrimLeadingWhitespace();
			parseOutput = new AuthParseOutput(SmtpAuthenticationMechanism.Login, multilevelAuthMechanism, context, null);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000D65C0 File Offset: 0x000D47C0
		private static ParseResult ExchangeAuthDetected(CommandContext context, SmtpInSessionState sessionState, AuthCommand command, out AuthParseOutput parseOutput)
		{
			if (command == AuthCommand.Auth || !SmtpInSessionUtils.ShouldExpsExchangeAuthBeAdvertised(sessionState.ReceiveConnector.AuthMechanism, sessionState.SecureState, sessionState.Configuration.TransportConfiguration.ProcessTransportRole))
			{
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			string exchangeAuthHashAlgorithm;
			if (!context.GetNextArgument(out exchangeAuthHashAlgorithm))
			{
				parseOutput = null;
				sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Inbound ExchangeAuth negotiation failed because client did not specify hash algorithm");
				return ParseResult.AuthUnrecognized;
			}
			context.TrimLeadingWhitespace();
			if (context.Length == 0)
			{
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			parseOutput = new AuthParseOutput(SmtpAuthenticationMechanism.ExchangeAuth, MultilevelAuthMechanism.MUTUALGSSAPI, context, exchangeAuthHashAlgorithm);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000D6650 File Offset: 0x000D4850
		private static ParseResult GssapiAuthDetected(CommandContext context, SmtpInSessionState sessionState, AuthCommand command, out AuthParseOutput parseOutput)
		{
			if (command == AuthCommand.XExps)
			{
				if (!SmtpInSessionUtils.DoesRoleSupportInboundXExpsGssapi(sessionState.Configuration.TransportConfiguration.ProcessTransportRole))
				{
					parseOutput = null;
					return ParseResult.AuthUnrecognized;
				}
			}
			else if (command == AuthCommand.Auth && (!sessionState.ReceiveConnector.HasIntegratedAuthMechanism || !sessionState.ReceiveConnector.EnableAuthGSSAPI))
			{
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			context.TrimLeadingWhitespace();
			parseOutput = new AuthParseOutput(SmtpAuthenticationMechanism.Gssapi, MultilevelAuthMechanism.GSSAPI, context, null);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000D66BE File Offset: 0x000D48BE
		private static ParseResult NtlmAuthDetected(CommandContext context, SmtpInSessionState sessionState, AuthCommand command, out AuthParseOutput parseOutput)
		{
			if (command == AuthCommand.Auth && !sessionState.IsIntegratedAuthSupported)
			{
				parseOutput = null;
				return ParseResult.AuthUnrecognized;
			}
			context.TrimLeadingWhitespace();
			parseOutput = new AuthParseOutput(SmtpAuthenticationMechanism.Ntlm, MultilevelAuthMechanism.NTLM, context, null);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001AB5 RID: 6837
		public const string ExpsCommandKeyword = "X-EXPS";

		// Token: 0x04001AB6 RID: 6838
		public const string AuthCommandKeyword = "AUTH";

		// Token: 0x04001AB7 RID: 6839
		public const string LoginKeyword = "login";

		// Token: 0x04001AB8 RID: 6840
		public const string ExchangeAuthKeyword = "exchangeauth";

		// Token: 0x04001AB9 RID: 6841
		public const string GSSAPIKeyword = "gssapi";

		// Token: 0x04001ABA RID: 6842
		public const string NTLMKeyword = "ntlm";

		// Token: 0x04001ABB RID: 6843
		public static readonly byte[] Login = Util.AsciiStringToBytes("login");

		// Token: 0x04001ABC RID: 6844
		public static readonly byte[] ExchangeAuth = Util.AsciiStringToBytes("exchangeauth");

		// Token: 0x04001ABD RID: 6845
		public static readonly byte[] GSSAPI = Util.AsciiStringToBytes("gssapi");

		// Token: 0x04001ABE RID: 6846
		public static readonly byte[] NTLM = Util.AsciiStringToBytes("ntlm");
	}
}

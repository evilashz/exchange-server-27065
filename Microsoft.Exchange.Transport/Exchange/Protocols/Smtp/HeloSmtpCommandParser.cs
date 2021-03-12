using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200047B RID: 1147
	internal static class HeloSmtpCommandParser
	{
		// Token: 0x060034A1 RID: 13473 RVA: 0x000D6AB8 File Offset: 0x000D4CB8
		public static ParseResult Parse(CommandContext context, SmtpInSessionState sessionState, HeloOrEhlo heloOrEhlo, out HeloEhloParseOutput parseOutput)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ParseResult result = HeloSmtpCommandParser.ValidateTlsCipherKeySize(sessionState);
			if (result.IsFailed)
			{
				parseOutput = null;
				return result;
			}
			SmtpReceiveCapabilities tlsDomainCapabilities;
			result = HeloSmtpCommandParser.ValidateRemoteTlsCertificate(sessionState, out tlsDomainCapabilities);
			if (result.IsFailed)
			{
				parseOutput = null;
				return result;
			}
			result = HeloSmtpCommandParser.ValidateAuthenticatedViaDirectTrustCertificate(sessionState, heloOrEhlo);
			if (result.IsFailed)
			{
				parseOutput = null;
				return result;
			}
			string heloDomain;
			result = HeloSmtpCommandParser.ValidateArguments(context, sessionState, heloOrEhlo, out heloDomain);
			if (result.IsFailed)
			{
				parseOutput = null;
				return result;
			}
			parseOutput = new HeloEhloParseOutput(heloDomain, tlsDomainCapabilities);
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000D6B44 File Offset: 0x000D4D44
		public static bool IsValidHeloDomain(string helloDomain, HeloOrEhlo heloOrEhlo, bool allowUtf8)
		{
			return ((allowUtf8 && heloOrEhlo != HeloOrEhlo.Helo) || !RoutingAddress.IsUTF8Address(helloDomain)) && (RoutingAddress.IsValidDomain(helloDomain) || RoutingAddress.IsDomainIPLiteral(helloDomain) || HeloCommandEventArgs.IsValidIpv6WindowsAddress(helloDomain));
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000D6B6E File Offset: 0x000D4D6E
		private static ParseResult ValidateAuthenticatedViaDirectTrustCertificate(SmtpInSessionState sessionState, HeloOrEhlo heloOrEhlo)
		{
			if (heloOrEhlo == HeloOrEhlo.Ehlo && sessionState.SecureState == SecureState.AnonymousTls && sessionState.TlsRemoteCertificateInternal != null && sessionState.RemoteIdentity == SmtpConstants.AnonymousSecurityIdentifier)
			{
				sessionState.DisconnectReason = DisconnectReason.DroppedSession;
				return ParseResult.AuthTempFailureDisconnect;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000D6BAC File Offset: 0x000D4DAC
		private static ParseResult ValidateTlsCipherKeySize(SmtpInSessionState sessionState)
		{
			if (sessionState.IsSecureSession && sessionState.NetworkConnection.TlsCipherKeySize < 128)
			{
				sessionState.Tracer.TraceError<int>((long)sessionState.GetHashCode(), "Negotiated TLS cipher strength is too weak at {0} bits.", sessionState.NetworkConnection.TlsCipherKeySize);
				sessionState.DisconnectReason = DisconnectReason.DroppedSession;
				return ParseResult.TlsCipherKeySizeTooShortDisconnect;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000D6C07 File Offset: 0x000D4E07
		public static ParseResult ValidateRemoteTlsCertificate(SmtpInSessionState sessionState, out SmtpReceiveCapabilities tlsDomainCapabilities)
		{
			if (sessionState.SecureState == SecureState.StartTls && !sessionState.TryCalculateTlsDomainCapabilitiesFromRemoteTlsCertificate(out tlsDomainCapabilities))
			{
				sessionState.DisconnectReason = DisconnectReason.DroppedSession;
				return ParseResult.CertificateValidationFailureDisconnect;
			}
			tlsDomainCapabilities = SmtpReceiveCapabilities.None;
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000D6C30 File Offset: 0x000D4E30
		private static ParseResult ValidateArguments(CommandContext context, SmtpInSessionState sessionState, HeloOrEhlo heloOrEhlo, out string heloDomain)
		{
			context.TrimLeadingWhitespace();
			if (context.GetCommandArguments(out heloDomain))
			{
				if (!HeloSmtpCommandParser.IsValidHeloDomain(heloDomain, heloOrEhlo, sessionState.ReceiveConnector.SmtpUtf8Enabled))
				{
					heloDomain = string.Empty;
					if (heloOrEhlo != HeloOrEhlo.Helo)
					{
						return ParseResult.InvalidEhloDomain;
					}
					return ParseResult.InvalidHeloDomain;
				}
			}
			else if (sessionState.ReceiveConnector.RequireEHLODomain)
			{
				if (heloOrEhlo != HeloOrEhlo.Helo)
				{
					return ParseResult.EhloDomainRequired;
				}
				return ParseResult.HeloDomainRequired;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001AC8 RID: 6856
		public const string HeloCommandKeyword = "HELO";

		// Token: 0x04001AC9 RID: 6857
		public const string EhloCommandKeyword = "EHLO";
	}
}

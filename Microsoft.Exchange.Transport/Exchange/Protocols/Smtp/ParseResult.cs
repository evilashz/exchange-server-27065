using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000460 RID: 1120
	internal struct ParseResult : IEquatable<ParseResult>
	{
		// Token: 0x060033DE RID: 13278 RVA: 0x000D11FB File Offset: 0x000CF3FB
		public ParseResult(ParsingStatus parsingStatus, SmtpResponse smtpResponse, bool disconnectClient = false)
		{
			this = default(ParseResult);
			this.ParsingStatus = parsingStatus;
			this.SmtpResponse = smtpResponse;
			this.DisconnectClient = disconnectClient;
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000D1219 File Offset: 0x000CF419
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x000D1221 File Offset: 0x000CF421
		public ParsingStatus ParsingStatus { get; private set; }

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000D122A File Offset: 0x000CF42A
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000D1232 File Offset: 0x000CF432
		public SmtpResponse SmtpResponse { get; private set; }

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000D123B File Offset: 0x000CF43B
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000D1243 File Offset: 0x000CF443
		public bool DisconnectClient { get; private set; }

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000D124C File Offset: 0x000CF44C
		public bool IsFailed
		{
			get
			{
				return this.ParsingStatus == ParsingStatus.Error || this.ParsingStatus == ParsingStatus.ProtocolError || this.ParsingStatus == ParsingStatus.IgnorableProtocolError;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000D126A File Offset: 0x000CF46A
		public bool IsIgnorableFailure
		{
			get
			{
				return this.ParsingStatus == ParsingStatus.IgnorableProtocolError;
			}
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000D1275 File Offset: 0x000CF475
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.ParsingStatus, this.SmtpResponse, this.DisconnectClient);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000D12A2 File Offset: 0x000CF4A2
		public override bool Equals(object other)
		{
			return other is ParseResult && this.Equals((ParseResult)other);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000D12BC File Offset: 0x000CF4BC
		public override int GetHashCode()
		{
			return 17 + 31 * this.SmtpResponse.GetHashCode() + 31 * this.ParsingStatus.GetHashCode() + 31 * this.DisconnectClient.GetHashCode();
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000D1309 File Offset: 0x000CF509
		public static bool operator ==(ParseResult lhs, ParseResult rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000D1313 File Offset: 0x000CF513
		public static bool operator !=(ParseResult lhs, ParseResult rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000D1320 File Offset: 0x000CF520
		public bool Equals(ParseResult other)
		{
			return this.ParsingStatus.Equals(other.ParsingStatus) && this.SmtpResponse.Equals(other.SmtpResponse) && this.DisconnectClient.Equals(other.DisconnectClient);
		}

		// Token: 0x04001A3E RID: 6718
		public static readonly ParseResult ParsingComplete = new ParseResult(ParsingStatus.Complete, SmtpResponse.Empty, false);

		// Token: 0x04001A3F RID: 6719
		public static readonly ParseResult MoreDataRequired = new ParseResult(ParsingStatus.MoreDataRequired, SmtpResponse.Empty, false);

		// Token: 0x04001A40 RID: 6720
		public static readonly ParseResult InvalidArguments = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidArguments, false);

		// Token: 0x04001A41 RID: 6721
		public static readonly ParseResult UnrecognizedParameter = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.UnrecognizedParameter, false);

		// Token: 0x04001A42 RID: 6722
		public static readonly ParseResult RequiredArgumentsNotPresent = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.RequiredArgumentsNotPresent, false);

		// Token: 0x04001A43 RID: 6723
		public static readonly ParseResult CommandNotImplemented = new ParseResult(ParsingStatus.Complete, SmtpResponse.CommandNotImplemented, false);

		// Token: 0x04001A44 RID: 6724
		public static readonly ParseResult CommandNotImplementedProtocolError = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.CommandNotImplemented, false);

		// Token: 0x04001A45 RID: 6725
		public static readonly ParseResult NotAuthorized = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.NotAuthorized, false);

		// Token: 0x04001A46 RID: 6726
		public static readonly ParseResult BadCommandSequence = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.BadCommandSequence, false);

		// Token: 0x04001A47 RID: 6727
		public static readonly ParseResult InvalidHeloDomain = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidHeloDomain, false);

		// Token: 0x04001A48 RID: 6728
		public static readonly ParseResult HeloDomainRequired = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.HeloDomainRequired, false);

		// Token: 0x04001A49 RID: 6729
		public static readonly ParseResult InvalidEhloDomain = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidEhloDomain, false);

		// Token: 0x04001A4A RID: 6730
		public static readonly ParseResult EhloDomainRequired = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.EhloDomainRequired, false);

		// Token: 0x04001A4B RID: 6731
		public static readonly ParseResult AuthTempFailure = new ParseResult(ParsingStatus.Error, SmtpResponse.AuthTempFailure, false);

		// Token: 0x04001A4C RID: 6732
		public static readonly ParseResult AuthTempFailure2 = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.AuthTempFailure, false);

		// Token: 0x04001A4D RID: 6733
		public static readonly ParseResult AuthUnrecognized = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.AuthUnrecognized, false);

		// Token: 0x04001A4E RID: 6734
		public static readonly ParseResult AuthUnsuccessful = new ParseResult(ParsingStatus.Error, SmtpResponse.AuthUnsuccessful, false);

		// Token: 0x04001A4F RID: 6735
		public static readonly ParseResult AuthAlreadySpecified = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.AuthAlreadySpecified, false);

		// Token: 0x04001A50 RID: 6736
		public static readonly ParseResult MailFromAlreadySpecified = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.MailFromAlreadySpecified, false);

		// Token: 0x04001A51 RID: 6737
		public static readonly ParseResult MessageRateLimitExceeded = new ParseResult(ParsingStatus.Error, SmtpResponse.MessageRateLimitExceeded, false);

		// Token: 0x04001A52 RID: 6738
		public static readonly ParseResult MessageTooLarge = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.MessageTooLarge, false);

		// Token: 0x04001A53 RID: 6739
		public static readonly ParseResult UnsupportedBodyType = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.UnsupportedBodyType, false);

		// Token: 0x04001A54 RID: 6740
		public static readonly ParseResult InvalidAddress = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidAddress, false);

		// Token: 0x04001A55 RID: 6741
		public static readonly ParseResult LongAddress = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.LongAddress, false);

		// Token: 0x04001A56 RID: 6742
		public static readonly ParseResult Utf8Address = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.Utf8Address, false);

		// Token: 0x04001A57 RID: 6743
		public static readonly ParseResult TooManyRecipients = new ParseResult(ParsingStatus.Error, SmtpResponse.TooManyRecipients, false);

		// Token: 0x04001A58 RID: 6744
		public static readonly ParseResult TooManyRelatedErrors = new ParseResult(ParsingStatus.Error, SmtpResponse.TooManyRelatedErrors, false);

		// Token: 0x04001A59 RID: 6745
		public static readonly ParseResult TooManyAuthenticationErrors = new ParseResult(ParsingStatus.Error, SmtpResponse.TooManyAuthenticationErrors, false);

		// Token: 0x04001A5A RID: 6746
		public static readonly ParseResult RcptRelayNotPermitted = new ParseResult(ParsingStatus.Error, SmtpResponse.RcptRelayNotPermitted, false);

		// Token: 0x04001A5B RID: 6747
		public static readonly ParseResult RequireTlsToSendMail = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.RequireTLSToSendMail, false);

		// Token: 0x04001A5C RID: 6748
		public static readonly ParseResult OrarNotAuthorized = new ParseResult(ParsingStatus.Error, SmtpResponse.OrarNotAuthorized, false);

		// Token: 0x04001A5D RID: 6749
		public static readonly ParseResult RDstNotAuthorized = new ParseResult(ParsingStatus.Error, SmtpResponse.RDstNotAuthorized, false);

		// Token: 0x04001A5E RID: 6750
		public static readonly ParseResult UserLookupFailed = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.UserLookupFailed, false);

		// Token: 0x04001A5F RID: 6751
		public static readonly ParseResult UnableToObtainIdentity = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.UnableToObtainIdentity, false);

		// Token: 0x04001A60 RID: 6752
		public static readonly ParseResult XProxyAcceptedAuthenticated = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.XProxyAcceptedAuthenticated, false);

		// Token: 0x04001A61 RID: 6753
		public static readonly ParseResult XProxyAccepted = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.XProxyAccepted, false);

		// Token: 0x04001A62 RID: 6754
		public static readonly ParseResult ProxyHopCountExceeded = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.ProxyHopCountExceeded, false);

		// Token: 0x04001A63 RID: 6755
		public static readonly ParseResult InvalidSenderAddress = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidSenderAddress, false);

		// Token: 0x04001A64 RID: 6756
		public static readonly ParseResult UnableToAcceptAnonymousSession = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.UnableToAcceptAnonymousSession, false);

		// Token: 0x04001A65 RID: 6757
		public static readonly ParseResult SubmitDenied = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.SubmitDenied, false);

		// Token: 0x04001A66 RID: 6758
		public static readonly ParseResult InsufficientResource = new ParseResult(ParsingStatus.Error, SmtpResponse.InsufficientResource, false);

		// Token: 0x04001A67 RID: 6759
		public static readonly ParseResult DomainSecureDisabled = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.DomainSecureDisabled, false);

		// Token: 0x04001A68 RID: 6760
		public static readonly ParseResult NotAuthenticated = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.NotAuthenticated, false);

		// Token: 0x04001A69 RID: 6761
		public static readonly ParseResult CertificateValidationFailure = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.CertificateValidationFailure, false);

		// Token: 0x04001A6A RID: 6762
		public static readonly ParseResult XMessageEPropNotFoundInMailCommand = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.XMessageEPropNotFoundInMailCommand, false);

		// Token: 0x04001A6B RID: 6763
		public static readonly ParseResult LongSenderAddress = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.LongSenderAddress, false);

		// Token: 0x04001A6C RID: 6764
		public static readonly ParseResult Utf8SenderAddress = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.Utf8SenderAddress, false);

		// Token: 0x04001A6D RID: 6765
		public static readonly ParseResult SmtpUtf8ArgumentNotProvided = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.SmtpUtf8ArgumentNotProvided, false);

		// Token: 0x04001A6E RID: 6766
		public static readonly ParseResult OrgQueueQuotaExceeded = new ParseResult(ParsingStatus.Error, SmtpResponse.OrgQueueQuotaExceeded, false);

		// Token: 0x04001A6F RID: 6767
		public static readonly ParseResult ServiceInactiveDisconnect = new ParseResult(ParsingStatus.Error, SmtpResponse.ServiceInactive, true);

		// Token: 0x04001A70 RID: 6768
		public static readonly ParseResult CertificateValidationFailureDisconnect = new ParseResult(ParsingStatus.Error, SmtpResponse.CertificateValidationFailure, true);

		// Token: 0x04001A71 RID: 6769
		public static readonly ParseResult TlsCipherKeySizeTooShortDisconnect = new ParseResult(ParsingStatus.Error, SmtpResponse.AuthTempFailureTLSCipherTooWeak, true);

		// Token: 0x04001A72 RID: 6770
		public static readonly ParseResult AuthTempFailureDisconnect = new ParseResult(ParsingStatus.Error, SmtpResponse.AuthTempFailure, true);

		// Token: 0x04001A73 RID: 6771
		public static readonly ParseResult DataTransactionFailed = new ParseResult(ParsingStatus.Error, SmtpResponse.DataTransactionFailed, false);

		// Token: 0x04001A74 RID: 6772
		public static readonly ParseResult InvalidLastChunk = new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.InvalidArguments, true);

		// Token: 0x04001A75 RID: 6773
		public static readonly ParseResult IgnorableRcpt2InvalidArguments = new ParseResult(ParsingStatus.IgnorableProtocolError, SmtpResponse.Rcpt2ToOkButInvalidArguments, false);

		// Token: 0x04001A76 RID: 6774
		public static readonly ParseResult IgnorableRcpt2InvalidAddress = new ParseResult(ParsingStatus.IgnorableProtocolError, SmtpResponse.Rcpt2ToOkButInvalidAddress, false);

		// Token: 0x04001A77 RID: 6775
		public static readonly ParseResult IgnorableValidRcpt2ButDifferentFromRcptAddress = new ParseResult(ParsingStatus.IgnorableProtocolError, SmtpResponse.Rcpt2ToOkButRcpt2AddressDifferentFromRcptAddress, false);
	}
}

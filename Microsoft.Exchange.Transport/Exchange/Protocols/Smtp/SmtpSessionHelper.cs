using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004AF RID: 1199
	internal static class SmtpSessionHelper
	{
		// Token: 0x06003613 RID: 13843 RVA: 0x000DE034 File Offset: 0x000DC234
		public static void RejectMessage(SmtpResponse response, string sourceContext, IExecutionControl executionControl, TransportMailItem transportMailItem, IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, ulong sessionId, ReceiveConnector receiveConnector, IProtocolLogSession protocolLogSession, IMessageTrackingLog messageTrackingLog)
		{
			ArgumentValidator.ThrowIfInvalidValue<SmtpResponse>("response", response, (SmtpResponse r) => !r.Equals(SmtpResponse.Empty));
			ArgumentValidator.ThrowIfNull("executionControl", executionControl);
			ArgumentValidator.ThrowIfNull("transportMailItem", transportMailItem);
			ArgumentValidator.ThrowIfNull("localEndPoint", localEndPoint);
			ArgumentValidator.ThrowIfNull("remoteEndPoint", remoteEndPoint);
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("messageTrackingLog", messageTrackingLog);
			executionControl.HaltExecution();
			List<MailRecipient> list = new List<MailRecipient>(transportMailItem.Recipients.Count);
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients.AllUnprocessed)
			{
				list.Add(mailRecipient);
				mailRecipient.Ack(AckStatus.Fail, response);
			}
			transportMailItem.UpdateCachedHeaders();
			protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Rejected by agent: {0}", new object[]
			{
				sourceContext
			});
			AckDetails ackDetails = new AckDetails(localEndPoint, null, sessionId.ToString("X16", NumberFormatInfo.InvariantInfo), receiveConnector.Id.ToString(), remoteEndPoint.Address);
			messageTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.SMTP, SmtpSessionHelper.EnforceAgentNameInTrackingContext(sourceContext, executionControl), transportMailItem, list, ackDetails, response, null);
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000DE184 File Offset: 0x000DC384
		public static void DiscardMessage(string sourceContext, IExecutionControl executionControl, TransportMailItem transportMailItem, IProtocolLogSession protocolLogSession, IMessageTrackingLog messageTrackingLog)
		{
			ArgumentValidator.ThrowIfNull("executionControl", executionControl);
			ArgumentValidator.ThrowIfNull("transportMailItem", transportMailItem);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("messageTrackingLog", messageTrackingLog);
			executionControl.HaltExecution();
			protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Discarded by agent: {0}", new object[]
			{
				sourceContext
			});
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients.AllUnprocessed)
			{
				messageTrackingLog.TrackRecipientFail(MessageTrackingSource.SMTP, transportMailItem, mailRecipient.Email, SmtpResponse.AgentDiscardedMessage, SmtpSessionHelper.EnforceAgentNameInTrackingContext(sourceContext, executionControl), null);
			}
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000DE238 File Offset: 0x000DC438
		public static CertificateValidationStatus ValidateCertificate(string domain, X509Certificate2 tlsRemoteCertificate, SecureState secureState, ChainValidityStatus tlsRemoteCertificateChainValidationStatus, ICertificateValidator certificateValidator, IProtocolLogSession protocolLogSession, out string matchedCertDomain)
		{
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			SmtpDomainWithSubdomains smtpDomainWithSubdomains;
			if (!SmtpDomainWithSubdomains.TryParse(domain, out smtpDomainWithSubdomains))
			{
				throw new ArgumentException("Given domain is not valid Smtp domain");
			}
			if (smtpDomainWithSubdomains.IsStar)
			{
				throw new ArgumentException("Given domain cannot be \"*\"");
			}
			if (tlsRemoteCertificate == null)
			{
				matchedCertDomain = string.Empty;
				return CertificateValidationStatus.EmptyCertificate;
			}
			if (secureState == SecureState.AnonymousTls)
			{
				matchedCertDomain = string.Empty;
				return CertificateValidationStatus.ExchangeServerAuthCertificate;
			}
			matchedCertDomain = certificateValidator.FindBestMatchingCertificateFqdn(new MatchableDomain(smtpDomainWithSubdomains), tlsRemoteCertificate, MatchOptions.None, protocolLogSession);
			if (matchedCertDomain == null)
			{
				matchedCertDomain = string.Empty;
				return CertificateValidationStatus.SubjectMismatch;
			}
			return SmtpSessionHelper.ConvertChainValidityStatusToCertValidationStatus(tlsRemoteCertificateChainValidationStatus);
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000DE2CC File Offset: 0x000DC4CC
		public static CertificateValidationStatus ConvertChainValidityStatusToCertValidationStatus(ChainValidityStatus chainValidityStatus)
		{
			if (chainValidityStatus <= (ChainValidityStatus)2148081683U)
			{
				switch (chainValidityStatus)
				{
				case ChainValidityStatus.Valid:
					return CertificateValidationStatus.Valid;
				case ChainValidityStatus.ValidSelfSigned:
					return CertificateValidationStatus.ValidSelfSigned;
				case ChainValidityStatus.EmptyCertificate:
					return CertificateValidationStatus.EmptyCertificate;
				case ChainValidityStatus.SubjectMismatch:
					return CertificateValidationStatus.SubjectMismatch;
				default:
					switch (chainValidityStatus)
					{
					case (ChainValidityStatus)2148081680U:
						return CertificateValidationStatus.Revoked;
					case (ChainValidityStatus)2148081682U:
						return CertificateValidationStatus.NoRevocationCheck;
					case (ChainValidityStatus)2148081683U:
						return CertificateValidationStatus.RevocationOffline;
					}
					break;
				}
			}
			else
			{
				if (chainValidityStatus == (ChainValidityStatus)2148098052U)
				{
					return CertificateValidationStatus.SignatureFailure;
				}
				if (chainValidityStatus == (ChainValidityStatus)2148098073U)
				{
					return CertificateValidationStatus.BasicConstraintsError;
				}
				switch (chainValidityStatus)
				{
				case (ChainValidityStatus)2148204801U:
					return CertificateValidationStatus.CertificateExpired;
				case (ChainValidityStatus)2148204802U:
					return CertificateValidationStatus.ValidityPeriodNesting;
				case (ChainValidityStatus)2148204803U:
					return CertificateValidationStatus.WrongRole;
				case (ChainValidityStatus)2148204806U:
					return CertificateValidationStatus.PurposeError;
				case (ChainValidityStatus)2148204809U:
					return CertificateValidationStatus.UntrustedRoot;
				case (ChainValidityStatus)2148204810U:
					return CertificateValidationStatus.InternalChainFailure;
				case (ChainValidityStatus)2148204812U:
					return CertificateValidationStatus.CertificateRevoked;
				case (ChainValidityStatus)2148204813U:
					return CertificateValidationStatus.UntrustedTestRoot;
				case (ChainValidityStatus)2148204814U:
					return CertificateValidationStatus.RevocationFailure;
				case (ChainValidityStatus)2148204815U:
					return CertificateValidationStatus.NoCNMatch;
				case (ChainValidityStatus)2148204816U:
					return CertificateValidationStatus.WrongUsage;
				}
			}
			return CertificateValidationStatus.Other;
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000DE3AD File Offset: 0x000DC5AD
		private static string EnforceAgentNameInTrackingContext(string trackingContext, IExecutionControl executionControl)
		{
			if (string.IsNullOrEmpty(trackingContext))
			{
				return executionControl.ExecutingAgentName;
			}
			if (trackingContext.Contains(executionControl.ExecutingAgentName))
			{
				return trackingContext;
			}
			return string.Format("{0}; {1}", executionControl.ExecutingAgentName, trackingContext);
		}
	}
}

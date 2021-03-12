using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003FD RID: 1021
	internal static class CertificateExpiryCheck
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x000BBDC1 File Offset: 0x000B9FC1
		public static bool HasCertificateExpired(IX509Certificate2 certificate, DateTime utcNow)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return DateTime.Compare(utcNow, certificate.NotAfter) > 0;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000BBDDD File Offset: 0x000B9FDD
		public static bool IsCertificateExpiringSoon(IX509Certificate2 certificate, DateTime utcNow, TimeSpan? howSoon = null)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return DateTime.Compare(utcNow.Add((howSoon != null) ? howSoon.Value : CertificateExpiryCheck.ExpiryWarningTimeSpan), certificate.NotAfter) > 0;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000BBE18 File Offset: 0x000BA018
		public static void LogExpiredCertificate(IX509Certificate2 certificate, SmtpSessionCertificateUse certificateUse, string certificateFqdn, IExEventLog eventLog, DateTime utcNow)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			switch (certificateUse)
			{
			case SmtpSessionCertificateUse.DirectTrust:
				eventLog.LogEvent(TransportEventLogConstants.Tuple_InternalTransportCertificateExpired, certificate.Thumbprint, new object[]
				{
					certificate.Thumbprint
				});
				return;
			case SmtpSessionCertificateUse.STARTTLS:
				eventLog.LogEvent(TransportEventLogConstants.Tuple_STARTTLSCertificateExpired, certificate.Thumbprint, new object[]
				{
					certificateFqdn,
					certificate.Thumbprint
				});
				return;
			case SmtpSessionCertificateUse.RemoteDirectTrust:
				eventLog.LogEvent(TransportEventLogConstants.Tuple_RemoteInternalTransportCertificateExpired, certificateFqdn, new object[]
				{
					certificateFqdn
				});
				return;
			case SmtpSessionCertificateUse.RemoteSTARTTLS:
				eventLog.LogEvent(TransportEventLogConstants.Tuple_RemoteSTARTTLSCertificateExpired, certificateFqdn, new object[]
				{
					certificateFqdn
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000BBED8 File Offset: 0x000BA0D8
		public static void PublishLamNotificationForExpiredCertificate(IX509Certificate2 certificate, SmtpSessionCertificateUse certificateUse, string certificateFqdn)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			if (certificateUse != SmtpSessionCertificateUse.STARTTLS)
			{
				return;
			}
			EventNotificationItem.PublishPeriodic(ExchangeComponent.Transport.Name, "STARTTLSCertficateExpired", null, string.Format("There is no valid SMTP Transport Layer Security (TLS) certificate for the FQDN of '{0}'.", certificateFqdn ?? string.Empty), "STARTTLSCertficateExpired", TimeSpan.FromMinutes(5.0), ResultSeverityLevel.Error, false);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000BBF38 File Offset: 0x000BA138
		public static void LogCertificateExpiringSoon(IX509Certificate2 certificate, SmtpSessionCertificateUse certificateUse, string certificateFqdn, IExEventLog eventLog, DateTime utcNow)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			switch (certificateUse)
			{
			case SmtpSessionCertificateUse.DirectTrust:
				eventLog.LogEvent(TransportEventLogConstants.Tuple_InternalTransportCertificateExpiresSoon, certificate.Thumbprint, new object[]
				{
					certificate.Thumbprint,
					Util.CalculateHoursBetween(utcNow, certificate.NotAfter)
				});
				return;
			case SmtpSessionCertificateUse.STARTTLS:
				eventLog.LogEvent(TransportEventLogConstants.Tuple_STARTTLSCertificateExpiresSoon, certificate.Thumbprint, new object[]
				{
					certificateFqdn,
					certificate.Thumbprint,
					Util.CalculateHoursBetween(utcNow, certificate.NotAfter)
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000BBFE4 File Offset: 0x000BA1E4
		public static void PublishLamNotificationForCertificateExpiringSoon(IX509Certificate2 certificate, SmtpSessionCertificateUse certificateUse, string certificateFqdn, DateTime utcNow)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			if (certificateUse != SmtpSessionCertificateUse.STARTTLS)
			{
				return;
			}
			EventNotificationItem.PublishPeriodic(ExchangeComponent.Transport.Name, "STARTTLSCertificateExpiresSoon", null, string.Format("The STARTTLS certificate will expire soon: subject: {0}, thumbprint: {1}, hours remaining: {2}.", certificateFqdn ?? string.Empty, certificate.Thumbprint, Util.CalculateHoursBetween(utcNow, certificate.NotAfter)), "STARTTLSCertificateExpiresSoon", TimeSpan.FromMinutes(5.0), ResultSeverityLevel.Error, false);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000BC058 File Offset: 0x000BA258
		public static bool CheckCertificateExpiry(X509Certificate2 certificate, ExEventLog eventLogger, SmtpSessionCertificateUse use, string fqdn)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			return CertificateExpiryCheck.CheckCertificateExpiry(new X509Certificate2Wrapper(certificate), new ExEventLogWrapper(eventLogger), use, fqdn, DateTime.UtcNow);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000BC088 File Offset: 0x000BA288
		public static bool CheckCertificateExpiry(IX509Certificate2 certificate, IExEventLog eventLog, SmtpSessionCertificateUse use, string fqdn, DateTime utcNow)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			if (CertificateExpiryCheck.HasCertificateExpired(certificate, utcNow))
			{
				CertificateExpiryCheck.LogExpiredCertificate(certificate, use, fqdn, eventLog, utcNow);
				CertificateExpiryCheck.PublishLamNotificationForExpiredCertificate(certificate, use, fqdn);
				return true;
			}
			if (CertificateExpiryCheck.IsCertificateExpiringSoon(certificate, utcNow, null))
			{
				CertificateExpiryCheck.LogCertificateExpiringSoon(certificate, use, fqdn, eventLog, utcNow);
				CertificateExpiryCheck.PublishLamNotificationForCertificateExpiringSoon(certificate, use, fqdn, utcNow);
			}
			return false;
		}

		// Token: 0x040016FF RID: 5887
		private const string componentKeyStartTlsCertExpired = "STARTTLSCertficateExpired";

		// Token: 0x04001700 RID: 5888
		private const string componentKeyStartTlsExpiresSoon = "STARTTLSCertificateExpiresSoon";

		// Token: 0x04001701 RID: 5889
		public static readonly TimeSpan ExpiryWarningTimeSpan = TimeSpan.FromDays(90.0);
	}
}

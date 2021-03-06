using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007CD RID: 1997
	internal static class SslValidationHelper
	{
		// Token: 0x06002941 RID: 10561 RVA: 0x00058588 File Offset: 0x00056788
		static SslValidationHelper()
		{
			CertificateValidationManager.RegisterCallback("Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper.NoSsl", new RemoteCertificateValidationCallback(SslValidationHelper.ServerCertificateValidatorIgnoreSslErrors));
			CertificateValidationManager.RegisterCallback("Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper", new RemoteCertificateValidationCallback(SslValidationHelper.ServerCertificateValidator), true);
			CertificateValidationManager.RegisterCallback("Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper.Revocation", new RemoteCertificateValidationCallback(SslValidationHelper.ServerCertificateValidatorVerifyRevocation), true);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000585E3 File Offset: 0x000567E3
		public static void SetComponentId(SslValidationOptions option, HttpWebRequest request)
		{
			if (option == SslValidationOptions.NoSslValidation)
			{
				CertificateValidationManager.SetComponentId(request, "Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper.NoSsl");
				return;
			}
			if (option == SslValidationOptions.BasicCertificateValidation)
			{
				CertificateValidationManager.SetComponentId(request, "Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper");
				return;
			}
			CertificateValidationManager.SetComponentId(request, "Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper.Revocation");
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00058620 File Offset: 0x00056820
		private static void TrackSslError(object sender, SslError error)
		{
			HttpWebRequest httpWebRequest = sender as HttpWebRequest;
			if (sender != null)
			{
				SslValidationHelper.sslValidationErrors.AddOrUpdate(httpWebRequest.ConnectionGroupName, error, (string key, SslError existingItem) => error);
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00058670 File Offset: 0x00056870
		public static SslError ReadSslError(string connectionGroupName)
		{
			SslError result;
			SslValidationHelper.sslValidationErrors.TryRemove(connectionGroupName, out result);
			return result;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0005868C File Offset: 0x0005688C
		private static bool ServerCertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			X509Certificate2 x509Certificate = new X509Certificate2(certificate);
			string host = (sender as HttpWebRequest).Host;
			ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<SslPolicyErrors>(0L, "Certificate validation errors: {0}", sslPolicyErrors);
			if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<string, string>(0L, "The ceritificate name mismatches the target host: host: {0}, certificate subject: {1}", host, x509Certificate.Subject);
				SslValidationHelper.TrackSslError(sender, new SslError
				{
					SslErrorType = SslErrorType.NameMismatch,
					SslPolicyErrors = sslPolicyErrors,
					Description = string.Format("The request target '{0}' doesn't match the names on the certificate: {1}", host, x509Certificate.Subject)
				});
				return false;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = x509Certificate.NotAfter.ToUniversalTime();
			DateTime dateTime2 = x509Certificate.NotBefore.ToUniversalTime();
			if (dateTime < utcNow || dateTime2 > utcNow)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<DateTime, DateTime, DateTime>(0L, "The ceritificate is expired: Now: {0}, NotBefore: {1}, NotAfter: {2}", utcNow, dateTime2, dateTime);
				SslValidationHelper.TrackSslError(sender, new SslError
				{
					SslErrorType = SslErrorType.ExpiredCertificate,
					SslPolicyErrors = sslPolicyErrors,
					Description = string.Format("The certificate on '{0}' is expired. It is valid between {2} and {3}", new object[]
					{
						host,
						x509Certificate.Subject,
						dateTime2,
						dateTime
					})
				});
				return false;
			}
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<SslPolicyErrors>(0L, "An unknown SSL policy error has ocurred: {0}", sslPolicyErrors);
				SslValidationHelper.TrackSslError(sender, new SslError
				{
					SslErrorType = SslErrorType.Unknown,
					SslPolicyErrors = sslPolicyErrors,
					Description = string.Format("An unknown SSL policy error has been detected: {0}", sslPolicyErrors)
				});
				return false;
			}
			return true;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0005880F File Offset: 0x00056A0F
		private static bool ServerCertificateValidatorIgnoreSslErrors(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			ExTraceGlobals.MonitoringWebClientTracer.TraceDebug(0L, "Certificate validation is being ignored");
			return true;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x00058824 File Offset: 0x00056A24
		private static bool ServerCertificateValidatorVerifyRevocation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (!SslValidationHelper.ServerCertificateValidator(sender, certificate, chain, sslPolicyErrors))
			{
				return false;
			}
			ExTraceGlobals.MonitoringWebClientTracer.TraceDebug(0L, "Verfying certificate revocation");
			chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
			chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EndCertificateOnly;
			chain.ChainPolicy.UrlRetrievalTimeout = TimeSpan.FromSeconds(10.0);
			try
			{
				chain.Build(new X509Certificate2(certificate));
			}
			catch (Exception arg)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<Exception>(0L, "Failure occured while trying to check certificate revocation. Failure will be ignored: {0}", arg);
				return true;
			}
			foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
			{
				if ((x509ChainStatus.Status & X509ChainStatusFlags.Revoked) != X509ChainStatusFlags.NoError)
				{
					SslValidationHelper.TrackSslError(sender, new SslError
					{
						SslErrorType = SslErrorType.Revoked,
						SslPolicyErrors = sslPolicyErrors,
						X509ChainStatusFlags = x509ChainStatus.Status,
						Description = string.Format("The certificate has been revoked. {0}. Certificate subject: {1}", x509ChainStatus.StatusInformation, certificate.Subject)
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400248D RID: 9357
		private const string CertificateValidationComponentId = "Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper";

		// Token: 0x0400248E RID: 9358
		private const string CertificateValidationComponentIdNoSsl = "Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper.NoSsl";

		// Token: 0x0400248F RID: 9359
		private const string CertificateValidationComponentIdRevocation = "Microsoft.Exchange.Net.MonitoringWebClient.HttpWebRequestWrapper.Revocation";

		// Token: 0x04002490 RID: 9360
		private static ConcurrentDictionary<string, SslError> sslValidationErrors = new ConcurrentDictionary<string, SslError>();
	}
}

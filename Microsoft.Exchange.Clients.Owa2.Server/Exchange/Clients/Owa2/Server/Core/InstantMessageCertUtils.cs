using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000131 RID: 305
	internal static class InstantMessageCertUtils
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x000235F8 File Offset: 0x000217F8
		internal static void GetIMCertInfo(string thumbprint, out string certificateIssuer, out byte[] certificateSerial)
		{
			certificateIssuer = null;
			certificateSerial = null;
			string thumbprint2 = thumbprint.Replace(" ", string.Empty);
			X509Certificate2 x509Certificate = InstantMessageCertUtils.FindCertByThumbprint(thumbprint2);
			if (x509Certificate != null && InstantMessageCertUtils.IsValid(x509Certificate))
			{
				InstantMessageCertUtils.DoesCertificateExpireSoon(x509Certificate);
				certificateIssuer = x509Certificate.Issuer;
				certificateSerial = x509Certificate.GetSerialNumber();
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00023644 File Offset: 0x00021844
		private static X509Certificate2 FindCertByThumbprint(string thumbprint)
		{
			X509Certificate2 x509Certificate = null;
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				x509Certificate = ((x509Certificate2Collection.Count > 0) ? x509Certificate2Collection[0] : null);
				if (x509Certificate == null)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate with thumbprint {0} could not be found.", new object[]
					{
						thumbprint
					});
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMCertificateNotFound, string.Empty, new object[]
					{
						thumbprint
					});
					OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, Feature.InstantMessage.ToString(), string.Format("IM Certificate with thumbprint {0} could not be found.", thumbprint), ResultSeverityLevel.Error);
				}
			}
			catch (CryptographicException ex)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "Cryptographic exception is thrown: {0}", new object[]
				{
					ex
				});
			}
			catch (SecurityException ex2)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "Security exception is thrown: {0}", new object[]
				{
					ex2
				});
			}
			catch (ArgumentException ex3)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "Argument exception is thrown: {0}", new object[]
				{
					ex3
				});
			}
			finally
			{
				x509Store.Close();
			}
			return x509Certificate;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000237D0 File Offset: 0x000219D0
		private static bool IsExpired(X509Certificate2 cert)
		{
			bool result = false;
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.CurrentTimeZone, cert.NotAfter);
			if (ExDateTime.Compare(exDateTime, ExDateTime.Now) <= 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate has expired.");
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMCertificateExpired, string.Empty, new object[]
				{
					exDateTime
				});
				OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, Feature.InstantMessage.ToString(), "IM Certificate has expired.", ResultSeverityLevel.Error);
				result = true;
			}
			return result;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002385C File Offset: 0x00021A5C
		private static bool IsInvalidDate(X509Certificate2 cert)
		{
			bool result = false;
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.CurrentTimeZone, cert.NotBefore);
			if (ExDateTime.Compare(exDateTime, ExDateTime.Now) >= 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate has not become valid yet.");
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMCertificateInvalidDate, string.Empty, new object[]
				{
					exDateTime
				});
				OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, Feature.InstantMessage.ToString(), "IM Certificate has not become valid yet.", ResultSeverityLevel.Error);
				result = true;
			}
			return result;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000238E8 File Offset: 0x00021AE8
		private static bool HasPrivateKey(X509Certificate2 cert)
		{
			bool result = true;
			if (!cert.HasPrivateKey)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate does not have a private key.");
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMCertificateNoPrivateKey, string.Empty, new object[0]);
				OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, Feature.InstantMessage.ToString(), "IM Certificate does not have a private key.", ResultSeverityLevel.Error);
				result = false;
			}
			return result;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00023950 File Offset: 0x00021B50
		private static bool IsValid(X509Certificate2 cert)
		{
			bool result = false;
			if (cert.HasPrivateKey && !InstantMessageCertUtils.IsExpired(cert) && !InstantMessageCertUtils.IsInvalidDate(cert))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002397C File Offset: 0x00021B7C
		private static void DoesCertificateExpireSoon(X509Certificate2 cert)
		{
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.CurrentTimeZone, cert.NotAfter);
			TimeSpan threshold = new TimeSpan(30, 0, 0, 0);
			if (ExDateTime.Compare(exDateTime, ExDateTime.Now) >= 0 && ExDateTime.Compare(exDateTime, ExDateTime.Now, threshold) == 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate will expire soon.");
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMCertificateWillExpireSoon, string.Empty, new object[]
				{
					exDateTime
				});
			}
		}
	}
}

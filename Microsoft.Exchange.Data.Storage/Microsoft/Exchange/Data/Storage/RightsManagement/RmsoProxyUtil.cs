using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B56 RID: 2902
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RmsoProxyUtil
	{
		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x06006937 RID: 26935 RVA: 0x001C38B2 File Offset: 0x001C1AB2
		public static Uri OriginalCertServerUrl
		{
			get
			{
				return RmsoProxyUtil.originalCertServerUrl;
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x06006938 RID: 26936 RVA: 0x001C38B9 File Offset: 0x001C1AB9
		public static Uri OriginalLicenseServerUrl
		{
			get
			{
				return RmsoProxyUtil.originalLicenseServerUrl;
			}
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x001C38C0 File Offset: 0x001C1AC0
		public static Uri GetCertificationServerRedirectUrl(Uri serviceUrl)
		{
			if (serviceUrl == null)
			{
				return null;
			}
			if (serviceUrl != RmsoProxyUtil.originalCertServerUrl)
			{
				RmsoProxyUtil.originalCertServerUrl = serviceUrl;
			}
			else if (DateTime.UtcNow < RmsoProxyUtil.certServerUrlExpirationTimeUTC)
			{
				return RmsoProxyUtil.checkedCertServerUrl;
			}
			RmsoProxyUtil.checkedCertServerUrl = RmsoProxyUtil.CheckRedirectUrl(serviceUrl, true);
			RmsoProxyUtil.certServerUrlExpirationTimeUTC = DateTime.UtcNow.AddSeconds(300.0);
			return RmsoProxyUtil.checkedCertServerUrl;
		}

		// Token: 0x0600693A RID: 26938 RVA: 0x001C3930 File Offset: 0x001C1B30
		public static Uri GetLicenseServerRedirectUrl(Uri licenseUrl)
		{
			if (licenseUrl == null)
			{
				return null;
			}
			if (licenseUrl != RmsoProxyUtil.originalLicenseServerUrl)
			{
				RmsoProxyUtil.originalLicenseServerUrl = licenseUrl;
			}
			else if (DateTime.UtcNow < RmsoProxyUtil.licenseUrlExpirationTimeUTC)
			{
				return RmsoProxyUtil.checkedLicenseServerUrl;
			}
			RmsoProxyUtil.checkedLicenseServerUrl = RmsoProxyUtil.CheckRedirectUrl(licenseUrl, false);
			RmsoProxyUtil.licenseUrlExpirationTimeUTC = DateTime.UtcNow.AddSeconds(300.0);
			return RmsoProxyUtil.checkedLicenseServerUrl;
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x001C39A0 File Offset: 0x001C1BA0
		private static Uri CheckRedirectUrl(Uri originalUrl, bool isCertificationServerUrl)
		{
			Uri uri = originalUrl;
			string text = uri.AbsoluteUri;
			string name = isCertificationServerUrl ? "SOFTWARE\\Microsoft\\ExchangeServer\\V15\\IRM\\CertificationServerRedirection\\" : "SOFTWARE\\Microsoft\\ExchangeServer\\V15\\IRM\\LicenseServerRedirection\\";
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					string[] valueNames = registryKey.GetValueNames();
					if (valueNames == null || valueNames.Length == 0)
					{
						return originalUrl;
					}
					foreach (string text2 in valueNames)
					{
						if (!string.IsNullOrWhiteSpace(text2) && registryKey.GetValueKind(text2) == RegistryValueKind.String && text.ToLower().Contains(text2.ToLower()))
						{
							text = text.ToLower().Replace(text2.ToLower(), ((string)registryKey.GetValue(text2)).ToLower());
							try
							{
								uri = new Uri(text);
								break;
							}
							catch (UriFormatException ex)
							{
								RmsClientManagerLog.LogUrlMalFormatException(ex, text2, originalUrl.AbsoluteUri);
								RmsoProxyUtil.Tracer.TraceError(0L, ex.Message);
								throw ex;
							}
						}
					}
				}
			}
			return uri;
		}

		// Token: 0x04003BDB RID: 15323
		private const string CertficationServerRedirectionPath = "SOFTWARE\\Microsoft\\ExchangeServer\\V15\\IRM\\CertificationServerRedirection\\";

		// Token: 0x04003BDC RID: 15324
		private const string LicenseServerRedirectionPath = "SOFTWARE\\Microsoft\\ExchangeServer\\V15\\IRM\\LicenseServerRedirection\\";

		// Token: 0x04003BDD RID: 15325
		private const int DefaultUrlCacheTimeSpanInSeconds = 300;

		// Token: 0x04003BDE RID: 15326
		private static Uri originalCertServerUrl;

		// Token: 0x04003BDF RID: 15327
		private static Uri checkedCertServerUrl;

		// Token: 0x04003BE0 RID: 15328
		private static DateTime certServerUrlExpirationTimeUTC;

		// Token: 0x04003BE1 RID: 15329
		private static Uri originalLicenseServerUrl;

		// Token: 0x04003BE2 RID: 15330
		private static Uri checkedLicenseServerUrl;

		// Token: 0x04003BE3 RID: 15331
		private static DateTime licenseUrlExpirationTimeUTC;

		// Token: 0x04003BE4 RID: 15332
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;
	}
}

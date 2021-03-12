using System;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000061 RID: 97
	internal static class DomainServicesHelper
	{
		// Token: 0x060002CB RID: 715 RVA: 0x000071A4 File Offset: 0x000053A4
		private static DomainServices GetDomainServicesProxy(out string url)
		{
			int num = -1;
			DomainServices result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\"))
			{
				if (registryKey == null)
				{
					throw new DomainServicesHelperConfigException(Strings.ErrorReadingRegistryKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\"));
				}
				string text = (string)registryKey.GetValue("CertSubject", null);
				object value = registryKey.GetValue("PartnerId", num);
				url = (string)registryKey.GetValue("WlcdUrl", null);
				int num2;
				if (value == null || !(value is int))
				{
					num2 = -1;
				}
				else
				{
					num2 = (int)value;
				}
				if (text == null)
				{
					throw new DomainServicesHelperConfigException(Strings.ErrorReadingRegistryValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\", "CertSubject"));
				}
				if (num2 == -1)
				{
					throw new DomainServicesHelperConfigException(Strings.ErrorReadingRegistryValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\", "PartnerId"));
				}
				if (url == null)
				{
					throw new DomainServicesHelperConfigException(Strings.ErrorReadingRegistryValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\", "WlcdUrl"));
				}
				X509Certificate2 x509Certificate = null;
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				try
				{
					x509Store.Open(OpenFlags.OpenExistingOnly);
					foreach (X509Certificate2 x509Certificate2 in x509Store.Certificates)
					{
						if (x509Certificate2.Subject == text)
						{
							if (!x509Certificate2.HasPrivateKey)
							{
								throw new DomainServicesHelperConfigException(Strings.ErrorCertificateHasNoPrivateKey(text));
							}
							if (x509Certificate2.NotAfter > (DateTime)ExDateTime.Now && x509Certificate2.NotBefore < (DateTime)ExDateTime.Now && x509Certificate2.Verify())
							{
								x509Certificate = x509Certificate2;
								break;
							}
							throw new DomainServicesHelperConfigException(Strings.ErrorCertificateHasExpired(text));
						}
					}
					x509Store.Close();
				}
				catch (SecurityException innerException)
				{
					throw new DomainServicesHelperConfigException(Strings.ErrorOpeningCertificateStore(StoreName.My.ToString()), innerException);
				}
				if (x509Certificate == null)
				{
					throw new DomainServicesHelperConfigException(Strings.ErrorCertificateNotFound(text));
				}
				result = new DomainServices
				{
					PartnerAuthHeaderValue = new PartnerAuthHeader(),
					PartnerAuthHeaderValue = 
					{
						PartnerId = num2
					},
					ClientCertificates = 
					{
						x509Certificate
					},
					Url = url
				};
			}
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000073CC File Offset: 0x000055CC
		internal static DomainServices GetDomainServicesProxy()
		{
			string text;
			return DomainServicesHelper.GetDomainServicesProxy(out text);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000073E4 File Offset: 0x000055E4
		internal static DomainServices ConnectToDomainServices()
		{
			string url;
			DomainServices domainServicesProxy = DomainServicesHelper.GetDomainServicesProxy(out url);
			try
			{
				using (DomainServicesPerformanceData.DomainServicesConnection.StartRequestTimer())
				{
					domainServicesProxy.TestConnection("foo");
				}
			}
			catch (SoapException se)
			{
				throw DomainServicesHelper.GetWLCDPartnerAccessExceptionToThrow(url, se);
			}
			catch (WebException ex)
			{
				throw new WLCDPartnerAccessException(Strings.ErrorWLCDPartnerAccessException(url, ex.ToString()), ex);
			}
			return domainServicesProxy;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007468 File Offset: 0x00005668
		internal static WLCDPartnerAccessException GetWLCDPartnerAccessExceptionToThrow(string url, SoapException se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			WLCDPartnerAccessException ex = DomainServicesHelper.WrapIfKnownSoapException(se);
			if (ex == null)
			{
				LocalizedString message = Strings.ErrorWLCDPartnerAccessException(url, se.ToString());
				ex = new WLCDPartnerAccessException(message, se);
			}
			return ex;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000074A4 File Offset: 0x000056A4
		internal static WLCDPartnerAccessException WrapIfKnownSoapException(SoapException se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("SoapException should never be null.");
			}
			WLCDPartnerAccessException result = null;
			LocalizedString message = LocalizedString.Empty;
			string innerText;
			if (se.Detail != null && se.Detail.HasChildNodes && (innerText = se.Detail.FirstChild.InnerText) != null)
			{
				if (!(innerText == "1001"))
				{
					if (!(innerText == "1002"))
					{
						if (!(innerText == "1003"))
						{
							if (!(innerText == "1004"))
							{
								if (innerText == "1005")
								{
									message = Strings.ErrorInvalidManagementCertificate(se.ToString());
									result = new WLCDPartnerAccessException(message, se);
								}
							}
							else
							{
								message = Strings.ErrorMemberNotAuthorized(se.ToString());
								result = new WLCDPartnerAccessException(message, se);
							}
						}
						else
						{
							message = Strings.ErrorPartnerNotAuthorized(se.ToString());
							result = new WLCDPartnerAccessException(message, se);
						}
					}
					else
					{
						message = Strings.ErrorInvalidPartnerCert(se.ToString(), "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\");
						result = new WLCDPartnerAccessException(message, se);
					}
				}
				else
				{
					message = Strings.ErrorInvalidPartnerSpecified(se.ToString(), "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeWlcd\\");
					result = new WLCDPartnerAccessException(message, se);
				}
			}
			return result;
		}

		// Token: 0x040000CC RID: 204
		private static readonly string[] DomainConfigIds = new string[]
		{
			"ByodExchange",
			"ExchangeTest",
			"WlcdTestExchange",
			"OfficeLiveExchange"
		};
	}
}

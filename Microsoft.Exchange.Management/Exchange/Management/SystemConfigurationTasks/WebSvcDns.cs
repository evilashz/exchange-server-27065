using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using Microsoft.BDM.Pets.DNSManagement;
using Microsoft.BDM.Pets.SharedLibrary.Enums;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB6 RID: 2742
	internal sealed class WebSvcDns
	{
		// Token: 0x060060F7 RID: 24823 RVA: 0x00194AA4 File Offset: 0x00192CA4
		internal WebSvcDns(Uri endpointUrl, string authorizationCertificateSubject)
		{
			this.authorizationCertificateSubject = authorizationCertificateSubject;
			this.endpointUrl = endpointUrl;
			this.IntializeWebServiceBinding();
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x00194AC0 File Offset: 0x00192CC0
		internal WSHttpBinding Wsb
		{
			get
			{
				return this.wsb;
			}
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x00194AC8 File Offset: 0x00192CC8
		private DNSWebSvcClient OpenDnsServiceClient()
		{
			DNSWebSvcClient dnswebSvcClient = new DNSWebSvcClient(this.wsb, new EndpointAddress(this.endpointUrl, new AddressHeader[0]));
			dnswebSvcClient.ClientCredentials.ClientCertificate.Certificate = TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(this.authorizationCertificateSubject);
			dnswebSvcClient.Open();
			return dnswebSvcClient;
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x00194B18 File Offset: 0x00192D18
		internal void RegisterDomain(string domainName, string mxRecordParentDomain, string cnameRecordValue, string textRecordValue)
		{
			string domainName2 = "autodiscover." + domainName;
			string arg = WebSvcDns.CalculateDomainUniqueKey(domainName);
			string value = string.Format("{0}.{1}{2}", arg, mxRecordParentDomain, ",10");
			DNSWebSvcClient dnswebSvcClient = null;
			try
			{
				Guid zoneGuid = Guid.Empty;
				dnswebSvcClient = this.OpenDnsServiceClient();
				if (dnswebSvcClient.IsDomainAvailable(domainName))
				{
					zoneGuid = dnswebSvcClient.AddZone(domainName);
				}
				else
				{
					Zone zoneByDomainName = dnswebSvcClient.GetZoneByDomainName(domainName);
					zoneGuid = zoneByDomainName.ZoneGUID;
				}
				dnswebSvcClient.AddResourceRecord(zoneGuid, domainName2, 3600, ResourceRecordType.DNS_TYPE_CNAME, cnameRecordValue, true);
				dnswebSvcClient.AddResourceRecord(zoneGuid, domainName, 3600, ResourceRecordType.DNS_TYPE_MX, value, true);
				dnswebSvcClient.AddResourceRecord(zoneGuid, domainName, 3600, ResourceRecordType.DNS_TYPE_TEXT, textRecordValue, true);
			}
			finally
			{
				if (dnswebSvcClient != null)
				{
					dnswebSvcClient.Close();
				}
			}
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x00194BD8 File Offset: 0x00192DD8
		internal void DeregisterDomain(string domainName)
		{
			DNSWebSvcClient dnswebSvcClient = null;
			try
			{
				dnswebSvcClient = this.OpenDnsServiceClient();
				if (!dnswebSvcClient.IsDomainAvailable(domainName))
				{
					dnswebSvcClient.DeleteZoneByDomainName(domainName);
				}
			}
			finally
			{
				if (dnswebSvcClient != null)
				{
					dnswebSvcClient.Close();
				}
			}
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x00194C1C File Offset: 0x00192E1C
		private void IntializeWebServiceBinding()
		{
			this.wsb = new WSHttpBinding
			{
				Name = "WSHttpBinding_IDNSWebSvc",
				CloseTimeout = TimeSpan.Parse("00:01:00"),
				OpenTimeout = TimeSpan.Parse("00:01:00"),
				ReceiveTimeout = TimeSpan.Parse("00:10:00"),
				SendTimeout = TimeSpan.Parse("00:01:00"),
				BypassProxyOnLocal = false,
				TransactionFlow = false,
				HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
				MaxBufferPoolSize = 524288L,
				MaxReceivedMessageSize = 65536L,
				MessageEncoding = WSMessageEncoding.Text,
				TextEncoding = Encoding.UTF8,
				UseDefaultWebProxy = true,
				AllowCookies = false,
				ReaderQuotas = 
				{
					MaxDepth = 32,
					MaxStringContentLength = 8192,
					MaxArrayLength = 16384,
					MaxBytesPerRead = 4096,
					MaxNameTableCharCount = 16384
				},
				ReliableSession = 
				{
					Ordered = true,
					InactivityTimeout = TimeSpan.Parse("00:10:00"),
					Enabled = false
				},
				Security = 
				{
					Mode = SecurityMode.TransportWithMessageCredential,
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.None,
						ProxyCredentialType = HttpProxyCredentialType.None,
						Realm = ""
					},
					Message = 
					{
						ClientCredentialType = MessageCredentialType.Certificate,
						NegotiateServiceCredential = true,
						AlgorithmSuite = SecurityAlgorithmSuite.Default,
						EstablishSecurityContext = false
					}
				}
			};
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x00194DD4 File Offset: 0x00192FD4
		internal static string CalculateDomainUniqueKey(string domainName)
		{
			StringBuilder stringBuilder = new StringBuilder(domainName.Replace("-", string.Empty).Replace('.', '-'));
			StringBuilder stringBuilder2 = new StringBuilder();
			int num = -1;
			int num2 = 0;
			int num3;
			while ((num3 = domainName.IndexOf('-', num + 1)) != -1)
			{
				int num4 = (num3 + 4) / 4 - (num + 4) / 4;
				if (num4 > 0)
				{
					if (num2 != 0)
					{
						stringBuilder2.Append((char)(97 + num2));
					}
					if (num4 > 1)
					{
						stringBuilder2.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[]
						{
							num4 - 1
						});
					}
					num2 = 0;
				}
				num = num3;
				num2 |= 1 << num3 % 4;
			}
			if (num2 != 0)
			{
				stringBuilder2.Append((char)(97 + num2));
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.Append('0');
				stringBuilder.Append(stringBuilder2);
			}
			if (stringBuilder.Length > 63)
			{
				stringBuilder.Length = 63;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003558 RID: 13656
		private readonly string authorizationCertificateSubject;

		// Token: 0x04003559 RID: 13657
		private Uri endpointUrl;

		// Token: 0x0400355A RID: 13658
		private WSHttpBinding wsb;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008E3 RID: 2275
	internal static class Configuration
	{
		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060050A6 RID: 20646 RVA: 0x00151107 File Offset: 0x0014F307
		public static string OnPremGetOrgRel
		{
			get
			{
				return Configuration.GetValue<string>("OnPremGetOrgRel", "On Premises to Exchange Online Organization Relationship");
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x00151118 File Offset: 0x0014F318
		public static string TenantGetOrgRel
		{
			get
			{
				return Configuration.GetValue<string>("TenantGetOrgRel", "Exchange Online to on premises Organization Relationship");
			}
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00151129 File Offset: 0x0014F329
		public static string InboundConnectorName(string orgConfigGuid)
		{
			if (string.IsNullOrEmpty(orgConfigGuid))
			{
				throw new ArgumentNullException();
			}
			return Configuration.GetValue<string>("InboundConnectorName", string.Format("Inbound from {0}", orgConfigGuid));
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0015114E File Offset: 0x0014F34E
		public static string OutboundConnectorName(string orgConfigGuid)
		{
			if (string.IsNullOrEmpty(orgConfigGuid))
			{
				throw new ArgumentNullException();
			}
			return Configuration.GetValue<string>("OutboundConnectorName", string.Format("Outbound to {0}", orgConfigGuid));
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060050AA RID: 20650 RVA: 0x00151174 File Offset: 0x0014F374
		public static bool EnableLogging
		{
			get
			{
				bool flag = true;
				return Configuration.GetValue<int>("EnableLogging", flag ? 1 : 0) != 0;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x0015119C File Offset: 0x0014F39C
		public static bool DisableCertificateChecks
		{
			get
			{
				bool flag = false;
				return Configuration.GetValue<int>("DisableCertificateChecks", flag ? 1 : 0) != 0;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x001511C2 File Offset: 0x0014F3C2
		public static string FederatedTrustIdentity
		{
			get
			{
				return Configuration.GetValue<string>("FederatedTrustIdentity", "Microsoft Federation Gateway");
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x001511DC File Offset: 0x0014F3DC
		public static List<Uri> OnPremiseAcceptedTokenIssuerUriList
		{
			get
			{
				string[] defaultValue = new string[]
				{
					"uri:WindowsLiveIDINT",
					"urn:federation:MicrosoftOnlineINT",
					"urn:federation:MicrosoftOnline"
				};
				List<Uri> result;
				try
				{
					result = (from x in Configuration.GetValue<string[]>("OnPremiseAcceptedTokenIssuerUriList", defaultValue)
					select new Uri(x)).ToList<Uri>();
				}
				catch (UriFormatException)
				{
					throw new LocalizedException(HybridStrings.ErrorHybridRegistryInvalidUri("OnPremiseAcceptedTokenIssuerUriList"));
				}
				return result;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x0015126C File Offset: 0x0014F46C
		public static List<Uri> TenantAcceptedTokenIssuerUriList
		{
			get
			{
				string[] defaultValue = new string[]
				{
					"urn:federation:MicrosoftOnlineINT",
					"urn:federation:MicrosoftOnline"
				};
				List<Uri> result;
				try
				{
					result = (from x in Configuration.GetValue<string[]>("TenantAcceptedTokenIssuerUriList", defaultValue)
					select new Uri(x)).ToList<Uri>();
				}
				catch (UriFormatException)
				{
					throw new LocalizedException(HybridStrings.ErrorHybridRegistryInvalidUri("TenantAcceptedTokenIssuerUriList"));
				}
				return result;
			}
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x001512EC File Offset: 0x0014F4EC
		public static string PowerShellEndpoint(int instance)
		{
			string[] defaultTable = new string[]
			{
				"ps.outlook.com",
				"ps.partner.outlook.cn",
				"exchangelabs.live-int.com"
			};
			return Configuration.GetValue<string>("TenantPowerShellEndpoint", defaultTable, instance);
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x00151326 File Offset: 0x0014F526
		public static SmtpX509Identifier FopeCertificateName()
		{
			return Configuration.FopeCertificateName(0);
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00151330 File Offset: 0x0014F530
		public static SmtpX509Identifier FopeCertificateName(int instance)
		{
			string[] defaultTable = new string[]
			{
				"CN=MSIT Machine Auth CA 2, DC=redmond, DC=corp, DC=microsoft, DC=com",
				"CN=CNNIC SSL, O=CNNIC SSL, C=CN",
				"CN=MS Passport Test Sub CA, DC=redmond, DC=corp, DC=microsoft, DC=com"
			};
			string[] defaultTable2 = new string[]
			{
				"CN=mail.protection.outlook.com, OU=Forefront Online Protection for Exchange, O=Microsoft, L=Redmond, S=WA, C=US",
				"CN=*.mail.protection.partner.outlook.cn, OU=Office365, O=Shanghai Blue Cloud Technology Co. Ltd, L=Shanghai, S=Shanghai, C=CN",
				"CN=*.mail.o365filtering-int.com, OU=Forefront Online Protection for Exchange, O=Microsoft, L=Redmond, S=Washington, C=US"
			};
			string[] defaultTable3 = new string[]
			{
				"AcceptCloudServicesMail",
				"AcceptCloudServicesMail",
				"AcceptCloudServicesMail"
			};
			string value = Configuration.GetValue<string>("FopeCertificateIssuer", defaultTable, instance);
			string value2 = Configuration.GetValue<string>("FopeCertificateSubject", defaultTable2, instance);
			string value3 = Configuration.GetValue<string>("FopeCertificatePermissions", defaultTable3, instance);
			return SmtpX509Identifier.Parse(string.Format("<I>{0}<S>{1}:{2}", value, value2, value3));
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x001513EC File Offset: 0x0014F5EC
		public static SmtpReceiveDomainCapabilities TlsDomainCapabilities(int instance)
		{
			string defaultValue = "AcceptCloudServicesMail";
			string arg = Configuration.FopeCertificateDomain(instance);
			string value = Configuration.GetValue<string>("FopeCertificatePermissions", defaultValue);
			return SmtpReceiveDomainCapabilities.Parse(string.Format("{0}:{1}", arg, value));
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x00151424 File Offset: 0x0014F624
		public static string FopeCertificateDomain(int instance)
		{
			string[] defaultTable = new string[]
			{
				"mail.protection.outlook.com",
				"*.mail.protection.partner.outlook.cn",
				"*.mail.o365filtering-int.com"
			};
			return Configuration.GetValue<string>("FopeCertificateDomain", defaultTable, instance);
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00151460 File Offset: 0x0014F660
		public static string SignupDomainSuffix(int instance)
		{
			string[] defaultTable = new string[]
			{
				"onmicrosoft.com",
				"partner.onmschina.cn",
				"msol-test.com"
			};
			return Configuration.GetValue<string>("SignupDomainSuffix", defaultTable, instance);
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0015149C File Offset: 0x0014F69C
		public static string TargetOwaPrefix(int instance)
		{
			string[] defaultTable = new string[]
			{
				"http://outlook.com/owa/",
				"http://partner.outlook.cn/owa/",
				"http://outlook.com/owa/"
			};
			return Configuration.GetValue<string>("TargetOwaPrefix", defaultTable, instance);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x001514D8 File Offset: 0x0014F6D8
		public static bool RequiresFederationTrust(int instance)
		{
			int[] defaultTable = new int[]
			{
				1,
				0,
				1
			};
			return Configuration.GetValue<int>("RequiresFederationTrust", defaultTable, instance) != 0;
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00151508 File Offset: 0x0014F708
		public static bool RequiresIntraOrganizationConnector(int instance)
		{
			int[] array = new int[3];
			array[1] = 1;
			int[] defaultTable = array;
			return Configuration.GetValue<int>("RequiresIntraOrganizationConnector", defaultTable, instance) != 0;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00151534 File Offset: 0x0014F734
		public static bool RestrictIOCToSP1OrGreater(int instance)
		{
			int[] defaultTable = new int[]
			{
				1,
				0,
				1
			};
			return Configuration.GetValue<int>("RestrictIOCToSP1OrGreater", defaultTable, instance) != 0;
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x00151564 File Offset: 0x0014F764
		public static string OAuthConfigurationUrl(int instance)
		{
			string[] defaultTable = new string[]
			{
				"http://go.microsoft.com/fwlink/?LinkID=320386&clcid=0x409",
				"http://go.microsoft.com/fwlink/?LinkID=392853&clcid=0x409",
				"http://go.microsoft.com/fwlink/?LinkID=320386&clcid=0x409"
			};
			return Configuration.GetValue<string>("OAuthConfigurationUrl", defaultTable, instance);
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0015159E File Offset: 0x0014F79E
		public static T GetValue<T>(string name, T[] defaultTable, int instance)
		{
			if (instance < 0 || instance >= defaultTable.Length)
			{
				instance = 0;
			}
			return Configuration.GetValue<T>(name, defaultTable[instance]);
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x001515BC File Offset: 0x0014F7BC
		private static T GetValue<T>(string name, T defaultValue)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Update-HybridConfiguration"))
				{
					if (registryKey != null)
					{
						return (T)((object)registryKey.GetValue(name, defaultValue));
					}
				}
			}
			catch (IOException)
			{
			}
			return defaultValue;
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB9 RID: 2745
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigurationInformationProvider : IConfigurationInformationProvider
	{
		// Token: 0x06006409 RID: 25609 RVA: 0x001A7DE8 File Offset: 0x001A5FE8
		public ConfigurationInformationProvider(IPerTenantRMSTrustedPublishingDomainConfiguration perTenantconfig)
		{
			this.serverLicensorCertificate = new ConfigurationInformationProvider.ServerLicensorCertInformation(ConfigurationInformationProvider.GetTrustedDomainChainArrayFromCompressedString(perTenantconfig.CompressedSLCCertChain));
			this.serverLicensorCertificate.IsValidated = true;
			List<string[]> list = new List<string[]>(perTenantconfig.CompressedTrustedDomainChains.Count);
			foreach (string compressedCertChainString in perTenantconfig.CompressedTrustedDomainChains)
			{
				list.Add(ConfigurationInformationProvider.GetTrustedDomainChainArrayFromCompressedString(compressedCertChainString));
			}
			this.trustedUserDomains = new ConfigurationInformationProvider.TrustedDomainInformation(list);
			this.trustedUserDomains.IsValidated = true;
			this.trustedPublishingDomains = this.trustedUserDomains;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string compressedTemplateString in perTenantconfig.CompressedRMSTemplates)
			{
				string templateFromCompressedString = ConfigurationInformationProvider.GetTemplateFromCompressedString(compressedTemplateString);
				Guid templateGuidFromLicense;
				try
				{
					templateGuidFromLicense = DrmClientUtils.GetTemplateGuidFromLicense(templateFromCompressedString);
				}
				catch (RightsManagementException ex)
				{
					throw new ConfigurationProviderException(true, "ConfigurationInformationProvider failed to parse template data for tenant ", ex);
				}
				dictionary.Add(templateGuidFromLicense.ToString("B"), templateFromCompressedString);
			}
			this.rightsTemplateInformation = new ConfigurationInformationProvider.RightsTemplateInformation(dictionary);
			this.licensingIntranetDistributionPointUrl = perTenantconfig.IntranetLicensingUrl;
			this.licensingExtranetDistributionPointUrl = perTenantconfig.ExtranetLicensingUrl;
			this.certificationIntranetDistributionPointUrl = perTenantconfig.IntranetCertificationUrl;
			this.certificationExtranetDistributionPointUrl = perTenantconfig.ExtranetCertificationUrl;
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x0600640A RID: 25610 RVA: 0x001A7F5C File Offset: 0x001A615C
		public IServerLicensorCertInformation ServerLicensorCertificate
		{
			get
			{
				return this.serverLicensorCertificate;
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x001A7F64 File Offset: 0x001A6164
		public ITrustedDomainInformation TrustedUserDomains
		{
			get
			{
				return this.trustedUserDomains;
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x0600640C RID: 25612 RVA: 0x001A7F6C File Offset: 0x001A616C
		public ITrustedDomainInformation TrustedPublishingDomains
		{
			get
			{
				return this.trustedPublishingDomains;
			}
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x0600640D RID: 25613 RVA: 0x001A7F74 File Offset: 0x001A6174
		public IRightsTemplateInformation RightsTemplateList
		{
			get
			{
				return this.rightsTemplateInformation;
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x0600640E RID: 25614 RVA: 0x001A7F7C File Offset: 0x001A617C
		public TimeSpan RightsAccountCertificateValidityTime
		{
			get
			{
				return ConfigurationInformationProvider.RightsAccountCertificateValidityTimeSpan;
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x001A7F83 File Offset: 0x001A6183
		public Uri LicensingIntranetDistributionPointUrl
		{
			get
			{
				return this.licensingIntranetDistributionPointUrl;
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06006410 RID: 25616 RVA: 0x001A7F8B File Offset: 0x001A618B
		public Uri LicensingExtranetDistributionPointUrl
		{
			get
			{
				return this.licensingExtranetDistributionPointUrl;
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06006411 RID: 25617 RVA: 0x001A7F93 File Offset: 0x001A6193
		public Uri CertificationIntranetDistributionPointUrl
		{
			get
			{
				return this.certificationIntranetDistributionPointUrl;
			}
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x06006412 RID: 25618 RVA: 0x001A7F9B File Offset: 0x001A619B
		public Uri CertificationExtranetDistributionPointUrl
		{
			get
			{
				return this.certificationExtranetDistributionPointUrl;
			}
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x001A7FA4 File Offset: 0x001A61A4
		private static string GetTemplateFromCompressedString(string compressedTemplateString)
		{
			string result;
			try
			{
				RmsTemplateType rmsTemplateType;
				result = RMUtil.DecompressTemplate(compressedTemplateString, out rmsTemplateType);
			}
			catch (FormatException ex)
			{
				throw new ConfigurationProviderException(true, ex);
			}
			catch (InvalidRpmsgFormatException ex2)
			{
				throw new ConfigurationProviderException(true, ex2);
			}
			return result;
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x001A7FEC File Offset: 0x001A61EC
		private static string[] GetTrustedDomainChainArrayFromCompressedString(string compressedCertChainString)
		{
			string[] result;
			try
			{
				XrmlCertificateChain xrmlCertificateChain = RMUtil.DecompressSLCCertificate(compressedCertChainString);
				result = xrmlCertificateChain.ToStringArray();
			}
			catch (FormatException ex)
			{
				throw new ConfigurationProviderException(true, "ConfigurationInformationProvider failed to read TPD from compressed form", ex);
			}
			catch (InvalidRpmsgFormatException ex2)
			{
				throw new ConfigurationProviderException(true, "ConfigurationInformationProvider failed to read TPD from compressed form", ex2);
			}
			return result;
		}

		// Token: 0x040038BF RID: 14527
		private static readonly TimeSpan RightsAccountCertificateValidityTimeSpan = TimeSpan.FromDays(365.0);

		// Token: 0x040038C0 RID: 14528
		private readonly IServerLicensorCertInformation serverLicensorCertificate;

		// Token: 0x040038C1 RID: 14529
		private readonly ITrustedDomainInformation trustedUserDomains;

		// Token: 0x040038C2 RID: 14530
		private readonly ITrustedDomainInformation trustedPublishingDomains;

		// Token: 0x040038C3 RID: 14531
		private readonly IRightsTemplateInformation rightsTemplateInformation;

		// Token: 0x040038C4 RID: 14532
		private readonly Uri licensingIntranetDistributionPointUrl;

		// Token: 0x040038C5 RID: 14533
		private readonly Uri licensingExtranetDistributionPointUrl;

		// Token: 0x040038C6 RID: 14534
		private readonly Uri certificationIntranetDistributionPointUrl;

		// Token: 0x040038C7 RID: 14535
		private readonly Uri certificationExtranetDistributionPointUrl;

		// Token: 0x02000ABA RID: 2746
		private class ServerLicensorCertInformation : IServerLicensorCertInformation
		{
			// Token: 0x06006416 RID: 25622 RVA: 0x001A8059 File Offset: 0x001A6259
			public ServerLicensorCertInformation(string[] xrmlChain)
			{
				if (xrmlChain == null || xrmlChain.Length == 0)
				{
					throw new ArgumentNullException("xrmlChain is null");
				}
				this.certificateChain = new XrmlCertificateChain(xrmlChain);
			}

			// Token: 0x17001BA0 RID: 7072
			// (get) Token: 0x06006417 RID: 25623 RVA: 0x001A8080 File Offset: 0x001A6280
			public XrmlCertificateChain CertificateChain
			{
				get
				{
					return this.certificateChain;
				}
			}

			// Token: 0x17001BA1 RID: 7073
			// (get) Token: 0x06006418 RID: 25624 RVA: 0x001A8088 File Offset: 0x001A6288
			// (set) Token: 0x06006419 RID: 25625 RVA: 0x001A8090 File Offset: 0x001A6290
			public ISizeTraceableItem Issuer
			{
				get
				{
					return this.issuer;
				}
				set
				{
					this.issuer = value;
				}
			}

			// Token: 0x17001BA2 RID: 7074
			// (get) Token: 0x0600641A RID: 25626 RVA: 0x001A8099 File Offset: 0x001A6299
			// (set) Token: 0x0600641B RID: 25627 RVA: 0x001A80A1 File Offset: 0x001A62A1
			public ISizeTraceableItem IssuerAsPrincipal
			{
				get
				{
					return this.issuerAsPrincipal;
				}
				set
				{
					this.issuerAsPrincipal = value;
				}
			}

			// Token: 0x17001BA3 RID: 7075
			// (get) Token: 0x0600641C RID: 25628 RVA: 0x001A80AA File Offset: 0x001A62AA
			// (set) Token: 0x0600641D RID: 25629 RVA: 0x001A80B2 File Offset: 0x001A62B2
			public bool IsValidated
			{
				get
				{
					return this.validated;
				}
				set
				{
					this.validated = value;
				}
			}

			// Token: 0x040038C8 RID: 14536
			private readonly XrmlCertificateChain certificateChain;

			// Token: 0x040038C9 RID: 14537
			private ISizeTraceableItem issuer;

			// Token: 0x040038CA RID: 14538
			private ISizeTraceableItem issuerAsPrincipal;

			// Token: 0x040038CB RID: 14539
			private bool validated;
		}

		// Token: 0x02000ABB RID: 2747
		private class TrustedDomainInformation : ITrustedDomainInformation
		{
			// Token: 0x0600641E RID: 25630 RVA: 0x001A80BC File Offset: 0x001A62BC
			public TrustedDomainInformation(IList<string[]> trustedDomainChains)
			{
				this.trustedDomains = new List<XrmlCertificateChain>(trustedDomainChains.Count);
				foreach (string[] array in trustedDomainChains)
				{
					this.trustedDomains.Add(new XrmlCertificateChain(array));
				}
			}

			// Token: 0x17001BA4 RID: 7076
			// (get) Token: 0x0600641F RID: 25631 RVA: 0x001A8128 File Offset: 0x001A6328
			public List<XrmlCertificateChain> TrustedDomains
			{
				get
				{
					return this.trustedDomains;
				}
			}

			// Token: 0x17001BA5 RID: 7077
			// (get) Token: 0x06006420 RID: 25632 RVA: 0x001A8130 File Offset: 0x001A6330
			// (set) Token: 0x06006421 RID: 25633 RVA: 0x001A8138 File Offset: 0x001A6338
			public ISizeTraceableItem TrustedPrincipalList
			{
				get
				{
					return this.trustedPrincipals;
				}
				set
				{
					this.trustedPrincipals = value;
				}
			}

			// Token: 0x17001BA6 RID: 7078
			// (get) Token: 0x06006422 RID: 25634 RVA: 0x001A8141 File Offset: 0x001A6341
			// (set) Token: 0x06006423 RID: 25635 RVA: 0x001A8149 File Offset: 0x001A6349
			public bool IsValidated
			{
				get
				{
					return this.validated;
				}
				set
				{
					this.validated = value;
				}
			}

			// Token: 0x040038CC RID: 14540
			private readonly List<XrmlCertificateChain> trustedDomains;

			// Token: 0x040038CD RID: 14541
			private ISizeTraceableItem trustedPrincipals;

			// Token: 0x040038CE RID: 14542
			private bool validated;
		}

		// Token: 0x02000ABC RID: 2748
		private class RightsTemplateInformation : IRightsTemplateInformation
		{
			// Token: 0x06006424 RID: 25636 RVA: 0x001A8152 File Offset: 0x001A6352
			public RightsTemplateInformation(Dictionary<string, string> rightTemplates)
			{
				this.rightTemplates = rightTemplates;
			}

			// Token: 0x17001BA7 RID: 7079
			// (get) Token: 0x06006425 RID: 25637 RVA: 0x001A8161 File Offset: 0x001A6361
			public Dictionary<string, string> RightsTemplates
			{
				get
				{
					return this.rightTemplates;
				}
			}

			// Token: 0x17001BA8 RID: 7080
			// (get) Token: 0x06006426 RID: 25638 RVA: 0x001A8169 File Offset: 0x001A6369
			// (set) Token: 0x06006427 RID: 25639 RVA: 0x001A8171 File Offset: 0x001A6371
			public VerifiedRightsTemplateItems VerifiedItems
			{
				get
				{
					return this.verifiedRightsTemplateItems;
				}
				set
				{
					this.verifiedRightsTemplateItems = value;
				}
			}

			// Token: 0x040038CF RID: 14543
			private Dictionary<string, string> rightTemplates;

			// Token: 0x040038D0 RID: 14544
			private VerifiedRightsTemplateItems verifiedRightsTemplateItems;
		}
	}
}

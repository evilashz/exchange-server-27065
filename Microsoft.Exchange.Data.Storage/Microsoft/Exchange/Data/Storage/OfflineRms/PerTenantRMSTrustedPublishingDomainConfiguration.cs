using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ACC RID: 2764
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PerTenantRMSTrustedPublishingDomainConfiguration : TenantConfigurationCacheableItem<RMSTrustedPublishingDomain>, IPerTenantRMSTrustedPublishingDomainConfiguration
	{
		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x001AA0C8 File Offset: 0x001A82C8
		public override long ItemSize
		{
			get
			{
				return (long)this.estimatedSize;
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x001AA0D1 File Offset: 0x001A82D1
		public Uri IntranetLicensingUrl
		{
			get
			{
				return this.intranetLicensingUrl;
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x001AA0D9 File Offset: 0x001A82D9
		public Uri ExtranetLicensingUrl
		{
			get
			{
				return this.extranetLicensingUrl;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06006483 RID: 25731 RVA: 0x001AA0E1 File Offset: 0x001A82E1
		public Uri IntranetCertificationUrl
		{
			get
			{
				return this.intranetCertificationUrl;
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x001AA0E9 File Offset: 0x001A82E9
		public Uri ExtranetCertificationUrl
		{
			get
			{
				return this.extranetCertificationUrl;
			}
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x001AA0F1 File Offset: 0x001A82F1
		public string CompressedSLCCertChain
		{
			get
			{
				return this.compressedSLCCertChain;
			}
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x001AA0F9 File Offset: 0x001A82F9
		public int ActiveCryptoMode
		{
			get
			{
				return this.activeCryptoMode;
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x06006487 RID: 25735 RVA: 0x001AA101 File Offset: 0x001A8301
		public Dictionary<string, PrivateKeyInformation> PrivateKeys
		{
			get
			{
				return this.privateKeys;
			}
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x001AA109 File Offset: 0x001A8309
		public IList<string> CompressedRMSTemplates
		{
			get
			{
				return this.compressedRMSTemplates;
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x06006489 RID: 25737 RVA: 0x001AA111 File Offset: 0x001A8311
		public IList<string> CompressedTrustedDomainChains
		{
			get
			{
				return this.compressedTrustedDomainChains;
			}
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x001AA11C File Offset: 0x001A831C
		public override void ReadData(IConfigurationSession session)
		{
			RMSTrustedPublishingDomain[] array = session.Find<RMSTrustedPublishingDomain>(null, QueryScope.SubTree, null, null, 0);
			if (array == null || array.Length == 0)
			{
				throw new RightsManagementServerException(ServerStrings.FailedToLocateTPDConfig(session.SessionSettings.CurrentOrganizationId.ToString()), false);
			}
			this.compressedTrustedDomainChains = new List<string>(array.Length);
			this.compressedRMSTemplates = new List<string>();
			this.privateKeys = new Dictionary<string, PrivateKeyInformation>(array.Length, StringComparer.OrdinalIgnoreCase);
			foreach (RMSTrustedPublishingDomain rmstrustedPublishingDomain in array)
			{
				if (string.IsNullOrEmpty(rmstrustedPublishingDomain.SLCCertChain))
				{
					throw new DataValidationException(new PropertyValidationError(new LocalizedString("SLCCertChain is null from AD for tenant " + base.OrganizationId), RMSTrustedPublishingDomainSchema.SLCCertChain, null));
				}
				if (string.IsNullOrEmpty(rmstrustedPublishingDomain.PrivateKey))
				{
					throw new DataValidationException(new PropertyValidationError(new LocalizedString("PrivateKey is null from AD for tenant " + base.OrganizationId), RMSTrustedPublishingDomainSchema.PrivateKey, null));
				}
				if (string.IsNullOrEmpty(rmstrustedPublishingDomain.KeyId))
				{
					throw new DataValidationException(new PropertyValidationError(new LocalizedString("KeyId is null from AD for tenant " + base.OrganizationId), RMSTrustedPublishingDomainSchema.KeyId, null));
				}
				if (string.IsNullOrEmpty(rmstrustedPublishingDomain.KeyIdType))
				{
					throw new DataValidationException(new PropertyValidationError(new LocalizedString("KeyIdType is null from AD for tenant " + base.OrganizationId), RMSTrustedPublishingDomainSchema.KeyIdType, null));
				}
				if (rmstrustedPublishingDomain.IntranetLicensingUrl == null || string.IsNullOrEmpty(rmstrustedPublishingDomain.IntranetLicensingUrl.OriginalString))
				{
					throw new DataValidationException(new PropertyValidationError(new LocalizedString("IntranetLicensingUrl is null from AD for tenant " + base.OrganizationId), RMSTrustedPublishingDomainSchema.IntranetLicensingUrl, null));
				}
				if (rmstrustedPublishingDomain.ExtranetLicensingUrl == null || string.IsNullOrEmpty(rmstrustedPublishingDomain.ExtranetLicensingUrl.OriginalString))
				{
					throw new DataValidationException(new PropertyValidationError(new LocalizedString("ExtranetLicensingUrl is null from AD for tenant " + base.OrganizationId), RMSTrustedPublishingDomainSchema.ExtranetLicensingUrl, null));
				}
				if (rmstrustedPublishingDomain.Default)
				{
					this.intranetLicensingUrl = rmstrustedPublishingDomain.IntranetLicensingUrl;
					this.estimatedSize += rmstrustedPublishingDomain.IntranetLicensingUrl.OriginalString.Length * 2;
					this.extranetLicensingUrl = rmstrustedPublishingDomain.ExtranetLicensingUrl;
					this.estimatedSize += rmstrustedPublishingDomain.ExtranetLicensingUrl.OriginalString.Length * 2;
					if (rmstrustedPublishingDomain.IntranetCertificationUrl != null && !string.IsNullOrEmpty(rmstrustedPublishingDomain.IntranetCertificationUrl.OriginalString))
					{
						this.intranetCertificationUrl = rmstrustedPublishingDomain.IntranetCertificationUrl;
						this.estimatedSize += rmstrustedPublishingDomain.IntranetCertificationUrl.OriginalString.Length * 2;
					}
					if (rmstrustedPublishingDomain.ExtranetCertificationUrl != null && !string.IsNullOrEmpty(rmstrustedPublishingDomain.ExtranetCertificationUrl.OriginalString))
					{
						this.extranetCertificationUrl = rmstrustedPublishingDomain.ExtranetCertificationUrl;
						this.estimatedSize += rmstrustedPublishingDomain.ExtranetCertificationUrl.OriginalString.Length * 2;
					}
					this.compressedSLCCertChain = rmstrustedPublishingDomain.SLCCertChain;
					this.estimatedSize += rmstrustedPublishingDomain.SLCCertChain.Length * 2;
					this.activeCryptoMode = PerTenantRMSTrustedPublishingDomainConfiguration.CryptoModeFromCompressedSLC(this.compressedSLCCertChain);
				}
				if (rmstrustedPublishingDomain.RMSTemplates != null && rmstrustedPublishingDomain.RMSTemplates.Count > 0)
				{
					foreach (string text in rmstrustedPublishingDomain.RMSTemplates)
					{
						if (string.IsNullOrEmpty(text))
						{
							throw new DataValidationException(new PropertyValidationError(new LocalizedString("Template contains empty string for " + base.OrganizationId), RMSTrustedPublishingDomainSchema.ExtranetLicensingUrl, null));
						}
						this.CompressedRMSTemplates.Add(text);
						this.estimatedSize += text.Length;
					}
				}
				PrivateKeyInformation privateKeyInformation = new PrivateKeyInformation(rmstrustedPublishingDomain.KeyId, rmstrustedPublishingDomain.KeyIdType, rmstrustedPublishingDomain.KeyContainerName, rmstrustedPublishingDomain.KeyNumber, rmstrustedPublishingDomain.CSPName, rmstrustedPublishingDomain.CSPType, rmstrustedPublishingDomain.PrivateKey, rmstrustedPublishingDomain.Default);
				this.estimatedSize += 8;
				this.estimatedSize += rmstrustedPublishingDomain.KeyId.Length * 2;
				this.estimatedSize += rmstrustedPublishingDomain.KeyIdType.Length * 2;
				this.estimatedSize += rmstrustedPublishingDomain.PrivateKey.Length * 2;
				if (!string.IsNullOrEmpty(rmstrustedPublishingDomain.CSPName))
				{
					this.estimatedSize += rmstrustedPublishingDomain.CSPName.Length * 2;
				}
				if (!string.IsNullOrEmpty(rmstrustedPublishingDomain.KeyContainerName))
				{
					this.estimatedSize += rmstrustedPublishingDomain.KeyContainerName.Length * 2;
				}
				this.privateKeys[privateKeyInformation.Identity] = privateKeyInformation;
				this.compressedTrustedDomainChains.Add(rmstrustedPublishingDomain.SLCCertChain);
				this.estimatedSize += rmstrustedPublishingDomain.SLCCertChain.Length * 2;
			}
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x001AA5F0 File Offset: 0x001A87F0
		private static int CryptoModeFromCompressedSLC(string compressedSLCCertChain)
		{
			XrmlCertificateChain xrmlCertificateChain = RMUtil.DecompressSLCCertificate(compressedSLCCertChain);
			return xrmlCertificateChain.GetCryptoMode();
		}

		// Token: 0x04003927 RID: 14631
		private Uri intranetLicensingUrl;

		// Token: 0x04003928 RID: 14632
		private Uri extranetLicensingUrl;

		// Token: 0x04003929 RID: 14633
		private Uri intranetCertificationUrl;

		// Token: 0x0400392A RID: 14634
		private Uri extranetCertificationUrl;

		// Token: 0x0400392B RID: 14635
		private string compressedSLCCertChain;

		// Token: 0x0400392C RID: 14636
		private int activeCryptoMode = 1;

		// Token: 0x0400392D RID: 14637
		private Dictionary<string, PrivateKeyInformation> privateKeys;

		// Token: 0x0400392E RID: 14638
		private List<string> compressedRMSTemplates;

		// Token: 0x0400392F RID: 14639
		private List<string> compressedTrustedDomainChains;

		// Token: 0x04003930 RID: 14640
		private int estimatedSize;
	}
}

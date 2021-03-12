using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.OfflineRms;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200072A RID: 1834
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharedServerBoxRacIdentityGenerator : IPerTenantRMSTrustedPublishingDomainConfiguration
	{
		// Token: 0x06004127 RID: 16679 RVA: 0x0010B654 File Offset: 0x00109854
		public SharedServerBoxRacIdentityGenerator(string slcCertChainCompressed, RMSTrustedPublishingDomain oldDefaultTPD, string sharedKey)
		{
			if (string.IsNullOrEmpty(slcCertChainCompressed))
			{
				throw new ArgumentNullException("slcCertChainCompressed");
			}
			this.compressedCertChain = slcCertChainCompressed;
			this.GetPrivateKeys(oldDefaultTPD, slcCertChainCompressed);
			this.originalSharedKey = sharedKey;
			this.compressedTrustedDomains = new List<string>(2);
			this.compressedTrustedDomains.Add(this.compressedCertChain);
			if (oldDefaultTPD != null)
			{
				this.compressedTrustedDomains.Add(oldDefaultTPD.SLCCertChain);
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x0010B6C1 File Offset: 0x001098C1
		public Uri IntranetLicensingUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x0010B6C4 File Offset: 0x001098C4
		public Uri ExtranetLicensingUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x0010B6C7 File Offset: 0x001098C7
		public Uri IntranetCertificationUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x0600412B RID: 16683 RVA: 0x0010B6CA File Offset: 0x001098CA
		public Uri ExtranetCertificationUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x0010B6CD File Offset: 0x001098CD
		public string CompressedSLCCertChain
		{
			get
			{
				return this.compressedCertChain;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x0010B6D5 File Offset: 0x001098D5
		public Dictionary<string, PrivateKeyInformation> PrivateKeys
		{
			get
			{
				return this.privateKeys;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x0010B6DD File Offset: 0x001098DD
		public IList<string> CompressedRMSTemplates
		{
			get
			{
				return SharedServerBoxRacIdentityGenerator.EmptyList;
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x0010B6E4 File Offset: 0x001098E4
		public IList<string> CompressedTrustedDomainChains
		{
			get
			{
				return this.compressedTrustedDomains;
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x0010B6EC File Offset: 0x001098EC
		private bool HasCryptoModeChanged(RMSTrustedPublishingDomain oldDefaultTPD, string newSlcCertChainCompressed)
		{
			XrmlCertificateChain xrmlCertificateChain = RMUtil.DecompressSLCCertificate(oldDefaultTPD.SLCCertChain);
			XrmlCertificateChain xrmlCertificateChain2 = RMUtil.DecompressSLCCertificate(newSlcCertChainCompressed);
			return xrmlCertificateChain.GetCryptoMode() != xrmlCertificateChain2.GetCryptoMode();
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x0010B720 File Offset: 0x00109920
		private void GetPrivateKeys(RMSTrustedPublishingDomain oldDefaultTPD, string slcCertChainCompressed)
		{
			if (oldDefaultTPD == null || string.IsNullOrEmpty(oldDefaultTPD.PrivateKey) || this.HasCryptoModeChanged(oldDefaultTPD, slcCertChainCompressed))
			{
				this.resealKey = false;
				this.privateKeys = SharedServerBoxRacIdentityGenerator.EmptyPrivateKeys;
				return;
			}
			this.privateKeys = new Dictionary<string, PrivateKeyInformation>(1, StringComparer.OrdinalIgnoreCase);
			PrivateKeyInformation privateKeyInformation = new PrivateKeyInformation(oldDefaultTPD.KeyId, oldDefaultTPD.KeyIdType, oldDefaultTPD.KeyContainerName, oldDefaultTPD.KeyNumber, oldDefaultTPD.CSPName, oldDefaultTPD.CSPType, oldDefaultTPD.PrivateKey, false);
			this.privateKeys.Add(privateKeyInformation.Identity, privateKeyInformation);
			this.resealKey = true;
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x0010B7B5 File Offset: 0x001099B5
		public string GenerateSharedKey()
		{
			if (this.resealKey && !string.IsNullOrEmpty(this.originalSharedKey))
			{
				return ServerManager.ResealRACKey(this, this.originalSharedKey);
			}
			return ServerManager.GenerateAndSealRACKey(this);
		}

		// Token: 0x04002923 RID: 10531
		private static readonly Dictionary<string, PrivateKeyInformation> EmptyPrivateKeys = new Dictionary<string, PrivateKeyInformation>();

		// Token: 0x04002924 RID: 10532
		private static readonly IList<string> EmptyList = new List<string>();

		// Token: 0x04002925 RID: 10533
		private readonly string compressedCertChain;

		// Token: 0x04002926 RID: 10534
		private Dictionary<string, PrivateKeyInformation> privateKeys;

		// Token: 0x04002927 RID: 10535
		private readonly string originalSharedKey;

		// Token: 0x04002928 RID: 10536
		private bool resealKey;

		// Token: 0x04002929 RID: 10537
		private List<string> compressedTrustedDomains;
	}
}

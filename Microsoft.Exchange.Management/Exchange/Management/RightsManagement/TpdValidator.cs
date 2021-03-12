using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.OfflineRms;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000713 RID: 1811
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TpdValidator
	{
		// Token: 0x0600407C RID: 16508 RVA: 0x00107C90 File Offset: 0x00105E90
		public TpdValidator(bool internalLicensingEnabled, Uri intranetLicensingUrl, Uri extranetLicensingUrl, Uri intranetCertificationUrl, Uri extranetCertificationUrl, SwitchParameter rmsOnlineSwitch, SwitchParameter defaultSwitch, SwitchParameter refreshTemplatesSwitch)
		{
			this.internalLicensingEnabled = internalLicensingEnabled;
			this.intranetLicensingUrl = intranetLicensingUrl;
			this.extranetLicensingUrl = extranetLicensingUrl;
			this.intranetCertificationUrl = intranetCertificationUrl;
			this.extranetCertificationUrl = extranetCertificationUrl;
			this.rmsOnlineSwitch = rmsOnlineSwitch;
			this.defaultSwitch = defaultSwitch;
			this.refreshTemplatesSwitch = refreshTemplatesSwitch;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x00107CE0 File Offset: 0x00105EE0
		public string ValidateTpdSuitableForImport(TrustedDocDomain tpd, string tpdName, out object failureTarget, IConfigurationSession configurationSession = null, string existingTpdKeyId = null, string existingTpdKeyType = null, Uri existingTpdIntranetLicensingUrl = null, Uri existingTpdExtranetLicensingUrl = null, SecureString tpdFilePassword = null)
		{
			RmsUtil.ThrowIfParameterNull(tpd, "tpd");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			RmsUtil.ThrowIfKeyInformationInvalid(tpd, tpdName, out failureTarget);
			RmsUtil.ThrowIfSlcCertificateChainInvalid(tpd, tpdName, out failureTarget);
			RmsUtil.ThrowIfTpdCspDoesNotMatchCryptoMode(tpd, tpdName, out failureTarget);
			RmsUtil.ThrowIfTpdUsesUnauthorizedCryptoModeOnFips(tpd, tpdName, out failureTarget);
			string result;
			using (TrustedPublishingDomainPrivateKeyProvider trustedPublishingDomainPrivateKeyProvider = this.CreatePrivateKeyProvider(tpdName, tpd.m_ttdki, tpdFilePassword, out result, out failureTarget))
			{
				TrustedPublishingDomainImportUtilities tpdImportUtilities = this.CreateTpdImportUtilities(tpd, trustedPublishingDomainPrivateKeyProvider);
				RmsUtil.ThrowIfSlcCertificateDoesNotChainToProductionHeirarchyCertificate(tpdImportUtilities, tpdName, out failureTarget);
				if (this.refreshTemplatesSwitch)
				{
					RmsUtil.ThrowIfUrlWasSpecified(this.intranetLicensingUrl, this.refreshTemplatesSwitch, out failureTarget);
					RmsUtil.ThrowIfUrlWasSpecified(this.extranetLicensingUrl, this.refreshTemplatesSwitch, out failureTarget);
					RmsUtil.ThrowIfUrlWasSpecified(this.intranetCertificationUrl, this.refreshTemplatesSwitch, out failureTarget);
					RmsUtil.ThrowIfUrlWasSpecified(this.extranetCertificationUrl, this.refreshTemplatesSwitch, out failureTarget);
					RmsUtil.ThrowIfDefaultWasSpecified(this.defaultSwitch, out failureTarget);
					RmsUtil.ThrowIfImportedKeyIdAndTypeDoNotMatchExistingTPD(tpdName, tpd.m_ttdki.strID, existingTpdKeyId, out failureTarget);
					RmsUtil.ThrowIfImportedKeyIdAndTypeDoNotMatchExistingTPD(tpdName, tpd.m_ttdki.strIDType, existingTpdKeyType, out failureTarget);
				}
				else
				{
					RmsUtil.ThrowIfTpdDoesNotHavePrivateKeyIfInternalLicensingEnabled(tpd, tpdName, this.internalLicensingEnabled, out failureTarget);
					if (!this.rmsOnlineSwitch)
					{
						RmsUtil.ThrowIfImportedTPDsKeyIdIsNotUnique(configurationSession, tpd.m_ttdki.strID, tpd.m_ttdki.strIDType, out failureTarget);
					}
					RmsUtil.ThrowIfIsNotWellFormedRmServiceUrl(this.intranetLicensingUrl, out failureTarget);
					RmsUtil.ThrowIfIsNotWellFormedRmServiceUrl(this.extranetLicensingUrl, out failureTarget);
					RmsUtil.ThrowIfIsNotWellFormedRmServiceUrl(this.intranetCertificationUrl, out failureTarget);
					RmsUtil.ThrowIfIsNotWellFormedRmServiceUrl(this.extranetCertificationUrl, out failureTarget);
				}
				RmsUtil.ThrowIfRightsTemplatesInvalid(tpd.m_astrRightsTemplates, tpdName, tpdImportUtilities, this.refreshTemplatesSwitch ? existingTpdIntranetLicensingUrl : this.intranetLicensingUrl, this.refreshTemplatesSwitch ? existingTpdExtranetLicensingUrl : this.extranetLicensingUrl, out failureTarget);
			}
			return result;
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x00107EA0 File Offset: 0x001060A0
		private TrustedPublishingDomainPrivateKeyProvider CreatePrivateKeyProvider(string tpdName, KeyInformation keyInfo, SecureString tpdFilePassword, out string dkmEncryptedPrivateKey, out object failureTarget)
		{
			dkmEncryptedPrivateKey = null;
			failureTarget = null;
			if (!this.refreshTemplatesSwitch && !string.IsNullOrEmpty(keyInfo.strEncryptedPrivateKey))
			{
				return this.CreateKeyProviderAndDkmProtectKey(tpdName, keyInfo, tpdFilePassword, out dkmEncryptedPrivateKey, out failureTarget);
			}
			return null;
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x00107ED4 File Offset: 0x001060D4
		private TrustedPublishingDomainPrivateKeyProvider CreateKeyProviderAndDkmProtectKey(string tpdName, KeyInformation keyInfo, SecureString tpdFilePassword, out string dkmEncryptedPrivateKey, out object failureTarget)
		{
			failureTarget = null;
			byte[] bytes = this.DecryptPrivateKey(keyInfo, tpdFilePassword);
			ExchangeGroupKey exchangeGroupKey = new ExchangeGroupKey(null, "Microsoft Exchange DKM");
			Exception ex;
			if (!exchangeGroupKey.TryByteArrayToEncryptedString(bytes, out dkmEncryptedPrivateKey, out ex))
			{
				failureTarget = tpdName;
				throw new FailedToDkmProtectPrivateKeyException(ex);
			}
			Dictionary<string, PrivateKeyInformation> dictionary = new Dictionary<string, PrivateKeyInformation>(1, StringComparer.OrdinalIgnoreCase);
			PrivateKeyInformation privateKeyInformation = new PrivateKeyInformation(keyInfo.strID, keyInfo.strIDType, keyInfo.strKeyContainerName, keyInfo.nKeyNumber, keyInfo.strCSPName, keyInfo.nCSPType, dkmEncryptedPrivateKey, true);
			dictionary.Add(privateKeyInformation.Identity, privateKeyInformation);
			return new TrustedPublishingDomainPrivateKeyProvider(null, dictionary);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x00107F64 File Offset: 0x00106164
		protected virtual byte[] DecryptPrivateKey(KeyInformation keyInfo, SecureString tpdFilePassword)
		{
			IPrivateKeyDecryptor privateKeyDecryptor = this.CreatePrivateKeyDecryptor(tpdFilePassword);
			byte[] result;
			try
			{
				result = privateKeyDecryptor.Decrypt(keyInfo.strEncryptedPrivateKey);
			}
			catch (PrivateKeyDecryptionFailedException e)
			{
				throw new FailedToDecryptPrivateKeyException(e);
			}
			return result;
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x00107FA4 File Offset: 0x001061A4
		protected virtual TrustedPublishingDomainImportUtilities CreateTpdImportUtilities(TrustedDocDomain tpd, TrustedPublishingDomainPrivateKeyProvider privateKeyProvider)
		{
			return RmsUtil.CreateTpdImportUtilities(new XrmlCertificateChain(tpd.m_strLicensorCertChain), privateKeyProvider);
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x00107FB7 File Offset: 0x001061B7
		private IPrivateKeyDecryptor CreatePrivateKeyDecryptor(SecureString tpdFilePassword)
		{
			if (this.rmsOnlineSwitch)
			{
				return new RmsOnlinePrivateKeyDecryptor();
			}
			return new OnPremisePrivateKeyDecryptor(tpdFilePassword);
		}

		// Token: 0x040028D7 RID: 10455
		private readonly bool internalLicensingEnabled;

		// Token: 0x040028D8 RID: 10456
		private readonly Uri intranetLicensingUrl;

		// Token: 0x040028D9 RID: 10457
		private readonly Uri extranetLicensingUrl;

		// Token: 0x040028DA RID: 10458
		private readonly Uri intranetCertificationUrl;

		// Token: 0x040028DB RID: 10459
		private readonly Uri extranetCertificationUrl;

		// Token: 0x040028DC RID: 10460
		private readonly SwitchParameter rmsOnlineSwitch;

		// Token: 0x040028DD RID: 10461
		private readonly SwitchParameter defaultSwitch;

		// Token: 0x040028DE RID: 10462
		private readonly SwitchParameter refreshTemplatesSwitch;
	}
}

using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.OfflineRms;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;
using Microsoft.RightsManagementServices.Online;
using Microsoft.RightsManagementServices.Provider;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x020000FD RID: 253
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class RmsUtil
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0001F890 File Offset: 0x0001DA90
		public static void ThrowIfParameterNull(object param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001F89C File Offset: 0x0001DA9C
		public static void ThrowIfStringParameterNullOrEmpty(string s, string paramName)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentException(paramName);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001F8AD File Offset: 0x0001DAAD
		public static void ThrowIfGuidEmpty(Guid guid, string paramName)
		{
			if (guid.Equals(Guid.Empty))
			{
				throw new ArgumentException(paramName);
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		public static void ThrowIfTenantInfoisNull(TenantInfo[] tenantInfo, Guid externalDirectoryOrgId)
		{
			if (tenantInfo == null)
			{
				throw new ImportTpdException(string.Format("RMS Online returned a null TenantInfo reference for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		public static void ThrowIfZeroOrMultipleTenantInfoObjectsReturned(TenantInfo[] tenantInfo, Guid externalDirectoryOrgId)
		{
			if (tenantInfo.Length == 0 || tenantInfo.Length > 1)
			{
				throw new ImportTpdException(string.Format("RMS Online returned zero or multiple TenantInfo objects when exactly one was requested for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001F904 File Offset: 0x0001DB04
		public static void ThrowIfErrorInfoObjectReturned(TenantInfo tenantInfo, Guid externalDirectoryOrgId)
		{
			if (tenantInfo.ErrorInfo != null)
			{
				throw new RmsOnlineImportTpdException(string.Format("RMS Online returned an error for tenant with external directory organization ID {0}", externalDirectoryOrgId), tenantInfo.ErrorInfo.ErrorCode);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001F92F File Offset: 0x0001DB2F
		public static void ThrowIfTenantInfoDoesNotIncludeActiveTPD(TenantInfo tenantInfo, Guid externalDirectoryOrgId)
		{
			if (tenantInfo.ActivePublishingDomain == null)
			{
				throw new ImportTpdException(string.Format("RMS Online returned a TenantInfo containing no active TPD for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001F950 File Offset: 0x0001DB50
		public static void ThrowIfTpdDoesNotIncludeKeyInformation(TrustedDocDomain tpd, Guid externalDirectoryOrgId)
		{
			if (tpd.m_ttdki == null)
			{
				throw new ImportTpdException(string.Format("RMS Online returned a TPD containing no key information for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001F971 File Offset: 0x0001DB71
		public static void ThrowIfTpdDoesNotIncludeSLC(TrustedDocDomain tpd, Guid externalDirectoryOrgId)
		{
			if (tpd.m_strLicensorCertChain == null)
			{
				throw new ImportTpdException(string.Format("RMS Online returned a TPD containing no SLC for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001F992 File Offset: 0x0001DB92
		public static void ThrowIfTpdDoesNotIncludeTemplates(TrustedDocDomain tpd, Guid externalDirectoryOrgId)
		{
			if (tpd.m_astrRightsTemplates == null)
			{
				throw new ImportTpdException(string.Format("RMS Online returned a TPD containing no templates for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001F9B3 File Offset: 0x0001DBB3
		public static void ThrowIfTenantInfoDoesNotIncludeLicensingUrls(TenantInfo tenantInfo, Guid externalDirectoryOrgId)
		{
			if (null == tenantInfo.LicensingIntranetDistributionPointUrl || null == tenantInfo.LicensingExtranetDistributionPointUrl)
			{
				throw new ImportTpdException(string.Format("RMS Online did not return intranet/extranet licensing URLs for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001F9E8 File Offset: 0x0001DBE8
		public static void ThrowIfTenantInfoDoesNotIncludeCertificationUrls(TenantInfo tenantInfo, Guid externalDirectoryOrgId)
		{
			if (null == tenantInfo.CertificationIntranetDistributionPointUrl || null == tenantInfo.CertificationExtranetDistributionPointUrl)
			{
				throw new ImportTpdException(string.Format("RMS Online did not return intranet/extranet certification URLs for tenant with external directory organization ID {0}", externalDirectoryOrgId), null);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001FA1D File Offset: 0x0001DC1D
		public static void ThrowIfClientCredentialsIsNull(TenantManagementServiceClient proxy)
		{
			if (proxy.ClientCredentials == null)
			{
				throw new ImportTpdException("proxy.ClientCredentials is unexpectedly null", null);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001FA33 File Offset: 0x0001DC33
		public static void ThrowIfCertificateCollectionIsNullOrEmpty(X509Certificate2Collection certificates, string exceptionText)
		{
			if (certificates == null || certificates.Count == 0)
			{
				throw new ImportTpdException(exceptionText, null);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001FA48 File Offset: 0x0001DC48
		public static void ThrowIfKeyInformationInvalid(TrustedDocDomain tpd, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpd, "tpd");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			if (tpd.m_ttdki == null)
			{
				failureTarget = tpdName;
				throw new NoKeyInformationInImportedTrustedPublishingDomainException();
			}
			RmsUtil.ThrowIfKeyIdInvalid(tpd.m_ttdki, tpdName, out failureTarget);
			RmsUtil.ThrowIfKeyTypeInvalid(tpd.m_ttdki, tpdName, out failureTarget);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001FA98 File Offset: 0x0001DC98
		public static void ThrowIfSlcCertificateChainInvalid(TrustedDocDomain tpd, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpd, "tpd");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			failureTarget = null;
			if (tpd.m_strLicensorCertChain == null || tpd.m_strLicensorCertChain.Length == 0 || string.IsNullOrEmpty(tpd.m_strLicensorCertChain[0]))
			{
				failureTarget = tpdName;
				throw new NoSLCCertChainInImportedTrustedPublishingDomainException();
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		public static void ThrowIfTpdCspDoesNotMatchCryptoMode(TrustedDocDomain tpd, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpd, "tpd");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			failureTarget = null;
			int cryptoMode = RmsUtil.CryptoModeFromTpd(tpd);
			RmsUtil.CSP_TYPE csp_TYPE;
			if (!RmsUtil.TryCspEnumFromInteger(tpd.m_ttdki.nCSPType, out csp_TYPE))
			{
				failureTarget = tpdName;
				throw new InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(csp_TYPE.ToString(), cryptoMode);
			}
			switch (cryptoMode)
			{
			case 1:
				if (csp_TYPE != RmsUtil.CSP_TYPE.PROV_RSA_FULL && csp_TYPE != RmsUtil.CSP_TYPE.PROV_RSA_AES)
				{
					failureTarget = tpdName;
					throw new InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(csp_TYPE.ToString(), cryptoMode);
				}
				break;
			case 2:
				if (csp_TYPE != RmsUtil.CSP_TYPE.PROV_RSA_AES)
				{
					failureTarget = tpdName;
					throw new InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(csp_TYPE.ToString(), cryptoMode);
				}
				break;
			default:
				failureTarget = tpdName;
				throw new InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(csp_TYPE.ToString(), cryptoMode);
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		public static void ThrowIfTpdUsesUnauthorizedCryptoModeOnFips(TrustedDocDomain tpd, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpd, "tpd");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			failureTarget = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa\\FIPSAlgorithmPolicy\\", false))
			{
				object value;
				if (registryKey != null && (value = registryKey.GetValue("Enabled")) != null && (int)value == 1)
				{
					int num = RmsUtil.CryptoModeFromTpd(tpd);
					if (num == 1)
					{
						failureTarget = tpdName;
						throw new InvalidFipsCryptoModeInImportedTrustedPublishingDomainException(num);
					}
				}
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001FC24 File Offset: 0x0001DE24
		public static void ThrowIfSlcCertificateDoesNotChainToProductionHeirarchyCertificate(TrustedPublishingDomainImportUtilities tpdImportUtilities, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpdImportUtilities, "tpdImportUtilities");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			try
			{
				failureTarget = null;
				tpdImportUtilities.ValidateTrustedPublishingDomain();
			}
			catch (ValidationException ex)
			{
				failureTarget = tpdName;
				throw new FailedToValidateSLCCertChainException(ex.ErrorCode);
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001FC74 File Offset: 0x0001DE74
		public static void ThrowIfUrlWasSpecified(Uri url, SwitchParameter refreshTemplatesSwitch, out object failureTarget)
		{
			failureTarget = null;
			if (null != url)
			{
				failureTarget = refreshTemplatesSwitch;
				throw new RmsUrlsCannotBeSetException();
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001FC90 File Offset: 0x0001DE90
		public static void ThrowIfDefaultWasSpecified(SwitchParameter defaultSwitch, out object failureTarget)
		{
			failureTarget = null;
			if (defaultSwitch)
			{
				failureTarget = defaultSwitch;
				throw new CannotSetDefaultTPDException();
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001FCAB File Offset: 0x0001DEAB
		public static void ThrowIfImportedKeyIdAndTypeDoNotMatchExistingTPD(string tpdName, string importedKeyIdOrType, string existingKeyIdOrType, out object failureTarget)
		{
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(importedKeyIdOrType, "importedKeyIdOrType");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(existingKeyIdOrType, "existingKeyIdOrType");
			failureTarget = null;
			if (!string.Equals(importedKeyIdOrType, existingKeyIdOrType, StringComparison.OrdinalIgnoreCase))
			{
				failureTarget = existingKeyIdOrType;
				throw new KeyNoMatchException(tpdName);
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
		public static void ThrowIfTpdDoesNotHavePrivateKeyIfInternalLicensingEnabled(TrustedDocDomain tpd, string tpdName, bool internalLicensingEnabled, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpd, "tpd");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			RmsUtil.ThrowIfParameterNull(tpd.m_ttdki, "tpd.m_ttdki");
			failureTarget = null;
			if (internalLicensingEnabled && string.IsNullOrEmpty(tpd.m_ttdki.strEncryptedPrivateKey))
			{
				failureTarget = tpdName;
				throw new NoPrivateKeyInImportedTrustedPublishingDomainException();
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001FD3C File Offset: 0x0001DF3C
		public static void ThrowIfIsNotWellFormedRmServiceUrl(Uri url, out object failureTarget)
		{
			failureTarget = null;
			if (null != url && !RMUtil.IsWellFormedRmServiceUrl(url))
			{
				failureTarget = url;
				throw new RmsUrlIsInvalidException(url);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		public static void ThrowIfRightsTemplatesInvalid(IEnumerable<string> templates, string tpdName, TrustedPublishingDomainImportUtilities tpdImportUtilities, Uri intranetLicensingUrl, Uri extranetLicensingUrl, out object failureTarget)
		{
			failureTarget = null;
			if (templates != null)
			{
				RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
				RmsUtil.ThrowIfParameterNull(tpdImportUtilities, "tpdImportUtilities");
				RmsUtil.ThrowIfParameterNull(intranetLicensingUrl, "intranetLicensingUrl");
				RmsUtil.ThrowIfParameterNull(extranetLicensingUrl, "extranetLicensingUrl");
				foreach (string template in templates)
				{
					Uri templateDistributionPoint;
					Uri templateDistributionPoint2;
					Guid templateGuid;
					try
					{
						DrmClientUtils.ParseTemplate(template, out templateDistributionPoint, out templateDistributionPoint2, out templateGuid);
					}
					catch (RightsManagementException innerException)
					{
						failureTarget = tpdName;
						throw new InvalidTemplateException(innerException);
					}
					RmsUtil.ThrowIfRightsTemplateInvalid(tpdImportUtilities, tpdName, template, templateGuid, out failureTarget);
					RmsUtil.ThrowIfTemplateDistributionPointInvalid(templateDistributionPoint, RmsUtil.TemplateDistributionPointType.Intranet, templateGuid, intranetLicensingUrl, extranetLicensingUrl, out failureTarget);
					RmsUtil.ThrowIfTemplateDistributionPointInvalid(templateDistributionPoint2, RmsUtil.TemplateDistributionPointType.Extranet, templateGuid, intranetLicensingUrl, extranetLicensingUrl, out failureTarget);
				}
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001FE28 File Offset: 0x0001E028
		public static void ThrowIfImportedTPDsKeyIdIsNotUnique(IConfigurationSession session, string keyIdBeingImported, string keyIdTypeBeingImported, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(session, "session");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(keyIdBeingImported, "keyIdBeingImported");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(keyIdTypeBeingImported, "keyIdTypeBeingImported");
			failureTarget = null;
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RMSTrustedPublishingDomainSchema.KeyId, keyIdBeingImported),
				new ComparisonFilter(ComparisonOperator.Equal, RMSTrustedPublishingDomainSchema.KeyIdType, keyIdTypeBeingImported)
			});
			if (RmsUtil.TPDExists(session, filter))
			{
				failureTarget = keyIdBeingImported;
				throw new DuplicateTPDKeyIdException(keyIdTypeBeingImported, keyIdBeingImported);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001FE9C File Offset: 0x0001E09C
		public static bool TPDExists(IConfigurationSession session, string keyIdBeingImported, string keyIdTypeBeingImported)
		{
			RmsUtil.ThrowIfParameterNull(session, "session");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(keyIdBeingImported, "keyIdBeingImported");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(keyIdTypeBeingImported, "keyIdTypeBeingImported");
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RMSTrustedPublishingDomainSchema.KeyId, keyIdBeingImported),
				new ComparisonFilter(ComparisonOperator.Equal, RMSTrustedPublishingDomainSchema.KeyIdType, keyIdTypeBeingImported)
			});
			return RmsUtil.TPDExists(session, filter);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001FF00 File Offset: 0x0001E100
		public static bool TPDExists(IConfigurationSession session, QueryFilter filter = null)
		{
			RmsUtil.ThrowIfParameterNull(session, "session");
			RMSTrustedPublishingDomain[] array = session.Find<RMSTrustedPublishingDomain>(null, QueryScope.SubTree, filter, null, 1);
			return array.Length != 0;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001FF2D File Offset: 0x0001E12D
		public static bool IsKnownException(Exception exception)
		{
			return exception is RightsManagementException || exception is ExchangeConfigurationException;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001FF42 File Offset: 0x0001E142
		public static bool AreRmsOnlinePreRequisitesMet(IRMConfiguration irmConfiguration)
		{
			RmsUtil.ThrowIfParameterNull(irmConfiguration, "irmConfiguration");
			return RMUtil.IsWellFormedRmServiceUrl(irmConfiguration.RMSOnlineKeySharingLocation);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001FF5C File Offset: 0x0001E15C
		public static string TemplateNamesFromTemplateArray(string[] templateXrMLArray)
		{
			RmsUtil.ThrowIfParameterNull(templateXrMLArray, "templateXrMLArray");
			List<string> list = new List<string>();
			foreach (string templateXrml in templateXrMLArray)
			{
				RmsTemplate rmsTemplate = RmsTemplate.CreateServerTemplateFromTemplateDefinition(templateXrml, RmsTemplateType.Archived);
				list.Add(rmsTemplate.Name);
			}
			return string.Join(", ", list.ToArray());
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		public static bool TryExtractDecryptionCertificateSKIFromEncryptedXml(string encryptedData, out string requiredCertificateSKI, out Exception exception)
		{
			RmsUtil.ThrowIfParameterNull(encryptedData, "encryptedData");
			requiredCertificateSKI = null;
			exception = null;
			try
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				xmlDocument.LoadXml(encryptedData);
				using (XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("X509SKI"))
				{
					if (elementsByTagName.Count > 0)
					{
						byte[] value = Convert.FromBase64String(elementsByTagName[0].InnerText);
						requiredCertificateSKI = BitConverter.ToString(value);
						return true;
					}
				}
				exception = new XmlException("X509SKI node not found in encrypted XML document");
			}
			catch (FormatException ex)
			{
				exception = ex;
			}
			catch (XmlException ex2)
			{
				exception = ex2;
			}
			return false;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002006C File Offset: 0x0001E26C
		public static TrustedPublishingDomainImportUtilities CreateTpdImportUtilities(XrmlCertificateChain slcCertificate, TrustedPublishingDomainPrivateKeyProvider privateKeyProvider)
		{
			RmsUtil.ThrowIfParameterNull(slcCertificate, "slcCertificate");
			if (privateKeyProvider == null)
			{
				return new TrustedPublishingDomainImportUtilities(slcCertificate);
			}
			return new TrustedPublishingDomainImportUtilities(slcCertificate, privateKeyProvider);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002008C File Offset: 0x0001E28C
		public static TrustedDocDomain ConvertFromRmsOnlineTrustedDocDomain(TrustedDocDomain rmsoTPD)
		{
			RmsUtil.ThrowIfParameterNull(rmsoTPD, "rmsoTPD");
			return new TrustedDocDomain
			{
				m_ttdki = RmsUtil.ConvertFromRmsOnlineKeyInformation(rmsoTPD.m_ttdki),
				m_strLicensorCertChain = rmsoTPD.m_strLicensorCertChain,
				m_astrRightsTemplates = rmsoTPD.m_astrRightsTemplates
			};
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000200D4 File Offset: 0x0001E2D4
		public static bool TryExtractUrlsFromTenantConfiguration(XElement tenantConfigurationElement, out Uri intranetCertificationUrl, out Uri extranetCertificationUrl, out Uri intranetLicensingUrl, out Uri extranetLicensingUrl, out Exception exception)
		{
			RmsUtil.ThrowIfParameterNull(tenantConfigurationElement, "tenantConfigurationElement");
			intranetCertificationUrl = null;
			extranetCertificationUrl = null;
			intranetLicensingUrl = null;
			extranetLicensingUrl = null;
			exception = null;
			try
			{
				XmlReader reader = tenantConfigurationElement.CreateReader();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/TenantConfiguration/CertificationIntranetDistributionPointUrl");
				XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/TenantConfiguration/CertificationExtranetDistributionPointUrl");
				XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/TenantConfiguration/LicensingIntranetDistributionPointUrl");
				XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/TenantConfiguration/LicensingExtranetDistributionPointUrl");
				if (xmlNode != null && xmlNode2 != null && xmlNode3 != null && xmlNode4 != null)
				{
					intranetCertificationUrl = new Uri(xmlNode.InnerText);
					extranetCertificationUrl = new Uri(xmlNode2.InnerText);
					intranetLicensingUrl = new Uri(xmlNode3.InnerText);
					extranetLicensingUrl = new Uri(xmlNode4.InnerText);
					return true;
				}
				exception = new XmlException("Unable to extract certification/licensing URLs from TenantConfiguration XML");
			}
			catch (XmlException ex)
			{
				exception = ex;
			}
			catch (UriFormatException ex2)
			{
				exception = ex2;
			}
			return false;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public static string GenerateRmsOnlineTpdName(string existingDefaultTpdName, string newTpdNameRoot)
		{
			RmsUtil.ThrowIfStringParameterNullOrEmpty(newTpdNameRoot, "newTpdNameRoot");
			if (string.IsNullOrEmpty(existingDefaultTpdName))
			{
				return string.Format("{0}{1}{2}", newTpdNameRoot, " - ", "1");
			}
			int num = 0;
			if (existingDefaultTpdName.Length > " - ".Length && string.Compare(existingDefaultTpdName, 0, newTpdNameRoot, 0, newTpdNameRoot.Length, true) == 0)
			{
				int num2 = existingDefaultTpdName.LastIndexOf(" - ", StringComparison.Ordinal);
				if (-1 != num2 && existingDefaultTpdName.Length > num2 + " - ".Length)
				{
					int.TryParse(existingDefaultTpdName.Substring(num2 + " - ".Length), out num);
				}
			}
			return string.Format("{0}{1}{2}", newTpdNameRoot, " - ", num + 1);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000202C8 File Offset: 0x0001E4C8
		public static Guid GetExternalDirectoryOrgIdThrowOnFailure(IConfigurationSession session, OrganizationId orgId)
		{
			Guid externalOrganizationId = Guid.Empty;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ExchangeConfigurationUnit exchangeConfigurationUnit = session.Read<ExchangeConfigurationUnit>(orgId.ConfigurationUnit);
				if (exchangeConfigurationUnit != null)
				{
					Guid.TryParse(exchangeConfigurationUnit.ExternalDirectoryOrganizationId, out externalOrganizationId);
				}
			});
			if (!adoperationResult.Succeeded || externalOrganizationId == Guid.Empty)
			{
				throw new ImportTpdException("Unable to lookup ExternalDirectoryOrganizationId for organization", null);
			}
			return externalOrganizationId;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00020332 File Offset: 0x0001E532
		private static void ThrowIfKeyIdInvalid(KeyInformation keyInfo, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(keyInfo, "keyInfo");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			failureTarget = null;
			if (string.IsNullOrEmpty(keyInfo.strID))
			{
				failureTarget = tpdName;
				throw new NoKeyIDInImportedTrustedPublishingDomainException();
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00020363 File Offset: 0x0001E563
		private static void ThrowIfKeyTypeInvalid(KeyInformation keyInfo, string tpdName, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(keyInfo, "keyInfo");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			failureTarget = null;
			if (string.IsNullOrEmpty(keyInfo.strIDType))
			{
				failureTarget = tpdName;
				throw new NoKeyIDTypeInImportedTrustedPublishingDomainException();
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00020394 File Offset: 0x0001E594
		private static void ThrowIfRightsTemplateInvalid(TrustedPublishingDomainImportUtilities tpdImportUtilities, string tpdName, string template, Guid templateGuid, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(tpdImportUtilities, "tpdImportUtilities");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(tpdName, "tpdName");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(template, "template");
			failureTarget = null;
			if (Guid.Empty == templateGuid)
			{
				failureTarget = tpdName;
				throw new InvalidTemplateException();
			}
			try
			{
				tpdImportUtilities.ValidateRightsTemplate(template);
			}
			catch (ValidationException ex)
			{
				failureTarget = tpdName;
				throw new FailedToValidateTemplateException(templateGuid, ex.ErrorCode);
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002040C File Offset: 0x0001E60C
		private static void ThrowIfTemplateDistributionPointInvalid(Uri templateDistributionPoint, RmsUtil.TemplateDistributionPointType templateDistributionPointType, Guid templateGuid, Uri intranetLicensingUrl, Uri extranetLicensingUrl, out object failureTarget)
		{
			RmsUtil.ThrowIfParameterNull(templateDistributionPointType, "templateDistributionPointType");
			RmsUtil.ThrowIfParameterNull(intranetLicensingUrl, "intranetLicensingUrl");
			RmsUtil.ThrowIfParameterNull(extranetLicensingUrl, "extranetLicensingUrl");
			failureTarget = null;
			if (templateDistributionPoint != null && Uri.Compare(templateDistributionPoint, intranetLicensingUrl, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0 && Uri.Compare(templateDistributionPoint, extranetLicensingUrl, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0)
			{
				Uri uri = (templateDistributionPointType == RmsUtil.TemplateDistributionPointType.Intranet) ? intranetLicensingUrl : extranetLicensingUrl;
				failureTarget = uri;
				throw new FailedToMatchTemplateDistributionPointToLicensingUriException(templateGuid, templateDistributionPoint, uri);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00020480 File Offset: 0x0001E680
		private static KeyInformation ConvertFromRmsOnlineKeyInformation(KeyInformation rmsoKeyInfo)
		{
			RmsUtil.ThrowIfParameterNull(rmsoKeyInfo, "rmsoKeyInfo");
			return new KeyInformation
			{
				strID = rmsoKeyInfo.strID,
				strIDType = rmsoKeyInfo.strIDType,
				nCSPType = rmsoKeyInfo.nCSPType,
				strCSPName = rmsoKeyInfo.strCSPName,
				strKeyContainerName = rmsoKeyInfo.strKeyContainerName,
				nKeyNumber = rmsoKeyInfo.nKeyNumber,
				strEncryptedPrivateKey = rmsoKeyInfo.strEncryptedPrivateKey
			};
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000204F3 File Offset: 0x0001E6F3
		private static bool TryCspEnumFromInteger(int cspTypeIndex, out RmsUtil.CSP_TYPE cspTypeEnum)
		{
			cspTypeEnum = RmsUtil.CSP_TYPE.PROV_CSP_UNKNOWN;
			if (Enum.IsDefined(typeof(RmsUtil.CSP_TYPE), cspTypeIndex))
			{
				cspTypeEnum = (RmsUtil.CSP_TYPE)cspTypeIndex;
				return true;
			}
			return false;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00020518 File Offset: 0x0001E718
		private static int CryptoModeFromTpd(TrustedDocDomain tpd)
		{
			string compressedCerts = RMUtil.CompressSLCCertificateChain(tpd.m_strLicensorCertChain);
			XrmlCertificateChain xrmlCertificateChain = RMUtil.DecompressSLCCertificate(compressedCerts);
			return xrmlCertificateChain.GetCryptoMode();
		}

		// Token: 0x0400037F RID: 895
		private const string intranetCertificationUrlXpath = "/TenantConfiguration/CertificationIntranetDistributionPointUrl";

		// Token: 0x04000380 RID: 896
		private const string extranetCertificationUrlXpath = "/TenantConfiguration/CertificationExtranetDistributionPointUrl";

		// Token: 0x04000381 RID: 897
		private const string intranetLicensingUrlXpath = "/TenantConfiguration/LicensingIntranetDistributionPointUrl";

		// Token: 0x04000382 RID: 898
		private const string extranetLicensingXpath = "/TenantConfiguration/LicensingExtranetDistributionPointUrl";

		// Token: 0x04000383 RID: 899
		private const string FIPSRegistryKeyLocation = "System\\CurrentControlSet\\Control\\Lsa\\FIPSAlgorithmPolicy\\";

		// Token: 0x020000FE RID: 254
		private enum CSP_TYPE
		{
			// Token: 0x04000385 RID: 901
			PROV_CSP_UNKNOWN,
			// Token: 0x04000386 RID: 902
			PROV_RSA_FULL,
			// Token: 0x04000387 RID: 903
			PROV_RSA_SIG,
			// Token: 0x04000388 RID: 904
			PROV_DSS,
			// Token: 0x04000389 RID: 905
			PROV_FORTEZZA,
			// Token: 0x0400038A RID: 906
			PROV_MS_EXCHANGE,
			// Token: 0x0400038B RID: 907
			PROV_SSL,
			// Token: 0x0400038C RID: 908
			PROV_RSA_SCHANNEL = 12,
			// Token: 0x0400038D RID: 909
			PROV_DSS_DH,
			// Token: 0x0400038E RID: 910
			PROV_EC_ECDSA_SIG,
			// Token: 0x0400038F RID: 911
			PROV_EC_ECNRA_SIG,
			// Token: 0x04000390 RID: 912
			PROV_EC_ECDSA_FULL,
			// Token: 0x04000391 RID: 913
			PROV_EC_ECNRA_FULL,
			// Token: 0x04000392 RID: 914
			PROV_DH_SCHANNEL,
			// Token: 0x04000393 RID: 915
			PROV_SPYRUS_LYNKS = 20,
			// Token: 0x04000394 RID: 916
			PROV_RNG,
			// Token: 0x04000395 RID: 917
			PROV_INTEL_SEC,
			// Token: 0x04000396 RID: 918
			PROV_REPLACE_OWF,
			// Token: 0x04000397 RID: 919
			PROV_RSA_AES
		}

		// Token: 0x020000FF RID: 255
		public enum TemplateDistributionPointType
		{
			// Token: 0x04000399 RID: 921
			Intranet,
			// Token: 0x0400039A RID: 922
			Extranet
		}
	}
}

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200047E RID: 1150
	internal class FederationTrustSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06003389 RID: 13193 RVA: 0x000CEF98 File Offset: 0x000CD198
		internal static GetterDelegate CertificateGetterDelegate(ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag propertyBag)
			{
				byte[] array = propertyBag[propertyDefinition] as byte[];
				if (array == null || array.Length == 0)
				{
					return null;
				}
				object result;
				try
				{
					result = new X509Certificate2(array);
				}
				catch (CryptographicException ex)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(propertyDefinition.Name, ex.Message), propertyDefinition, propertyBag[ADObjectSchema.Id]), ex);
				}
				return result;
			};
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000CF038 File Offset: 0x000CD238
		internal static SetterDelegate CertificateSetterDelegate(ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					propertyBag[propertyDefinition] = value;
					return;
				}
				X509Certificate2 x509Certificate = value as X509Certificate2;
				if (x509Certificate == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExArgumentOutOfRangeException(propertyDefinition.Name, value.ToString()), propertyDefinition, propertyBag[ADObjectSchema.Id]), null);
				}
				propertyBag[propertyDefinition] = x509Certificate.Export(X509ContentType.SerializedCert);
			};
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000CF060 File Offset: 0x000CD260
		internal static object PartnerTypeGetter(IPropertyBag propertyBag)
		{
			string text = propertyBag[FederationTrustSchema.RawTokenIssuerType] as string;
			if (string.IsNullOrEmpty(text))
			{
				return FederationTrust.PartnerSTSType.LiveId;
			}
			int num = Array.IndexOf<string>(FederationTrustSchema.partnerSTSTypeProgIds, text.Trim().ToUpper());
			if (0 > num)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(FederationTrustSchema.RawTokenIssuerType.Name, text), FederationTrustSchema.RawTokenIssuerType, propertyBag[ADObjectSchema.Id]));
			}
			return (FederationTrust.PartnerSTSType)num;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000CF0D8 File Offset: 0x000CD2D8
		internal static void PartnerTypeSetter(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				propertyBag[FederationTrustSchema.RawTokenIssuerType] = value;
				return;
			}
			try
			{
				propertyBag[FederationTrustSchema.RawTokenIssuerType] = FederationTrustSchema.partnerSTSTypeProgIds[(int)value];
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExArgumentOutOfRangeException(FederationTrustSchema.RawTokenIssuerType.Name, value.ToString()), FederationTrustSchema.RawTokenIssuerType, propertyBag[ADObjectSchema.Id]), null);
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000CF150 File Offset: 0x000CD350
		internal static object NamespaceProvisionerTypeGetter(IPropertyBag propertyBag)
		{
			string text = propertyBag[FederationTrustSchema.RawAdminDescription] as string;
			string namespaceProvisioner = FederationTrustProvisioningControl.GetNamespaceProvisioner(text);
			if (string.IsNullOrEmpty(namespaceProvisioner))
			{
				return FederationTrust.NamespaceProvisionerType.ExternalProcess;
			}
			int num = Array.IndexOf<string>(FederationTrustSchema.namespaceProvisionerProgIds, namespaceProvisioner.Trim().ToUpper());
			if (0 > num)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(FederationTrustSchema.RawAdminDescription.Name, text), FederationTrustSchema.RawAdminDescription, propertyBag[ADObjectSchema.Id]));
			}
			return (FederationTrust.NamespaceProvisionerType)num;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000CF1D0 File Offset: 0x000CD3D0
		internal static void NamespaceProvisionerTypeSetter(object value, IPropertyBag propertyBag)
		{
			try
			{
				propertyBag[FederationTrustSchema.RawAdminDescription] = FederationTrustProvisioningControl.PutNamespaceProvisioner(FederationTrustSchema.namespaceProvisionerProgIds[(int)value], propertyBag[FederationTrustSchema.RawAdminDescription] as string);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExArgumentOutOfRangeException(FederationTrustSchema.RawAdminDescription.Name, value.ToString()), FederationTrustSchema.RawAdminDescription, propertyBag[ADObjectSchema.Id]), null);
			}
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000CF250 File Offset: 0x000CD450
		internal static object AdministratorProvisioningIdGetter(IPropertyBag propertyBag)
		{
			string text = propertyBag[FederationTrustSchema.RawAdminDescription] as string;
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return FederationTrustProvisioningControl.GetAdministratorProvisioningId(text);
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000CF282 File Offset: 0x000CD482
		internal static void AdministratorProvisioningIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[FederationTrustSchema.RawAdminDescription] = FederationTrustProvisioningControl.PutAdministratorProvisioningId(value as string, propertyBag[FederationTrustSchema.RawAdminDescription] as string);
		}

		// Token: 0x0400239B RID: 9115
		public static readonly ADPropertyDefinition RawAdminDescription = new ADPropertyDefinition("RawAdminDescription", ExchangeObjectVersion.Exchange2010, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400239C RID: 9116
		public static readonly ADPropertyDefinition ApplicationIdentifier = new ADPropertyDefinition("ApplicationIdentifier", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedApplicationId", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 128)
		}, null, null);

		// Token: 0x0400239D RID: 9117
		public static readonly ADPropertyDefinition AdministratorProvisioningId = new ADPropertyDefinition("AdministratorProvisioningId", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawAdminDescription
		}, null, new GetterDelegate(FederationTrustSchema.AdministratorProvisioningIdGetter), new SetterDelegate(FederationTrustSchema.AdministratorProvisioningIdSetter), null, null);

		// Token: 0x0400239E RID: 9118
		public static readonly ADPropertyDefinition ApplicationUri = new ADPropertyDefinition("ApplicationUri", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedApplicationUri", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.RelativeOrAbsolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.RelativeOrAbsolute)
		}, null, null);

		// Token: 0x0400239F RID: 9119
		public static readonly ADPropertyDefinition PolicyReferenceUri = new ADPropertyDefinition("PolicyReferenceUri", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedPolicyReferenceURI", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023A0 RID: 9120
		public static readonly ADPropertyDefinition TokenIssuerMetadataEpr = new ADPropertyDefinition("TokenIssuerMetadataEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTokenIssuerMetadataEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040023A1 RID: 9121
		public static readonly ADPropertyDefinition MetadataPollInterval = new ADPropertyDefinition("MetadataPollInterval", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchFedMetadataPollInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(1440.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneMinute, EnhancedTimeSpan.FromDays(365.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneMinute)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023A2 RID: 9122
		public static readonly ADPropertyDefinition RawTokenIssuerType = new ADPropertyDefinition("RawTokenIssuerType", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedTokenIssuerType", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023A3 RID: 9123
		public static readonly ADPropertyDefinition TokenIssuerType = new ADPropertyDefinition("TokenIssuerType", ExchangeObjectVersion.Exchange2010, typeof(FederationTrust.PartnerSTSType), null, ADPropertyDefinitionFlags.Calculated, FederationTrust.PartnerSTSType.LiveId, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawTokenIssuerType
		}, null, new GetterDelegate(FederationTrustSchema.PartnerTypeGetter), new SetterDelegate(FederationTrustSchema.PartnerTypeSetter), null, null);

		// Token: 0x040023A4 RID: 9124
		public static readonly ADPropertyDefinition TokenIssuerUri = new ADPropertyDefinition("TokenIssuerUri", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTokenIssuerURI", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040023A5 RID: 9125
		public static readonly ADPropertyDefinition TokenIssuerEpr = new ADPropertyDefinition("TokenIssuerEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTokenIssuerEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040023A6 RID: 9126
		public static readonly ADPropertyDefinition WebRequestorRedirectEpr = new ADPropertyDefinition("WebRequestorRedirectEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedWebRequestorRedirectEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040023A7 RID: 9127
		public static readonly ADPropertyDefinition MetadataEpr = new ADPropertyDefinition("MetadataEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedMetadataEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040023A8 RID: 9128
		public static readonly ADPropertyDefinition MetadataPutEpr = new ADPropertyDefinition("MetadataPutEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedMetadataPutEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040023A9 RID: 9129
		public static readonly ADPropertyDefinition RawOrgCertificate = new ADPropertyDefinition("RawOrgCertificate", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchFedOrgCertificate", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023AA RID: 9130
		public static readonly ADPropertyDefinition OrgCertificate = new ADPropertyDefinition("OrgCertificate", ExchangeObjectVersion.Exchange2010, typeof(X509Certificate2), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawOrgCertificate
		}, null, FederationTrustSchema.CertificateGetterDelegate(FederationTrustSchema.RawOrgCertificate), FederationTrustSchema.CertificateSetterDelegate(FederationTrustSchema.RawOrgCertificate), null, null);

		// Token: 0x040023AB RID: 9131
		public static readonly ADPropertyDefinition OrgPrivCertificate = new ADPropertyDefinition("OrgPrivCertificate", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedOrgPrivCertificate", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x040023AC RID: 9132
		public static readonly ADPropertyDefinition RawOrgNextCertificate = new ADPropertyDefinition("RawOrgNextCertificate", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchFedOrgNextCertificate", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023AD RID: 9133
		public static readonly ADPropertyDefinition OrgNextCertificate = new ADPropertyDefinition("OrgNextCertificate", ExchangeObjectVersion.Exchange2010, typeof(X509Certificate2), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawOrgNextCertificate
		}, null, FederationTrustSchema.CertificateGetterDelegate(FederationTrustSchema.RawOrgNextCertificate), FederationTrustSchema.CertificateSetterDelegate(FederationTrustSchema.RawOrgNextCertificate), null, null);

		// Token: 0x040023AE RID: 9134
		public static readonly ADPropertyDefinition OrgNextPrivCertificate = new ADPropertyDefinition("OrgNextPrivCertificate", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedOrgNextPrivCertificate", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x040023AF RID: 9135
		public static readonly ADPropertyDefinition OrgPrevPrivCertificate = new ADPropertyDefinition("OrgPrevPrivCertificate", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedOrgPrevPrivCertificate", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x040023B0 RID: 9136
		public static readonly ADPropertyDefinition RawOrgPrevCertificate = new ADPropertyDefinition("RawOrgPrevCertificate", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchFedOrgPrevCertificate", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023B1 RID: 9137
		public static readonly ADPropertyDefinition OrgPrevCertificate = new ADPropertyDefinition("OrgPrevCertificate", ExchangeObjectVersion.Exchange2010, typeof(X509Certificate2), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawOrgPrevCertificate
		}, null, FederationTrustSchema.CertificateGetterDelegate(FederationTrustSchema.RawOrgPrevCertificate), FederationTrustSchema.CertificateSetterDelegate(FederationTrustSchema.RawOrgPrevCertificate), null, null);

		// Token: 0x040023B2 RID: 9138
		public static readonly ADPropertyDefinition RawTokenIssuerCertificate = new ADPropertyDefinition("RawTokenIssuerCertificate", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchFedTokenIssuerCertificate", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023B3 RID: 9139
		public static readonly ADPropertyDefinition TokenIssuerCertificate = new ADPropertyDefinition("TokenIssuerCertificate", ExchangeObjectVersion.Exchange2010, typeof(X509Certificate2), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawTokenIssuerCertificate
		}, null, FederationTrustSchema.CertificateGetterDelegate(FederationTrustSchema.RawTokenIssuerCertificate), FederationTrustSchema.CertificateSetterDelegate(FederationTrustSchema.RawTokenIssuerCertificate), null, null);

		// Token: 0x040023B4 RID: 9140
		public static readonly ADPropertyDefinition RawTokenIssuerPrevCertificate = new ADPropertyDefinition("RawTokenIssuerPrevCertificate", ExchangeObjectVersion.Exchange2010, typeof(byte[]), "msExchFedTokenIssuerPrevCertificate", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023B5 RID: 9141
		public static readonly ADPropertyDefinition TokenIssuerPrevCertificate = new ADPropertyDefinition("TokenIssuerPrevCertificate", ExchangeObjectVersion.Exchange2010, typeof(X509Certificate2), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawTokenIssuerPrevCertificate
		}, null, FederationTrustSchema.CertificateGetterDelegate(FederationTrustSchema.RawTokenIssuerPrevCertificate), FederationTrustSchema.CertificateSetterDelegate(FederationTrustSchema.RawTokenIssuerPrevCertificate), null, null);

		// Token: 0x040023B6 RID: 9142
		public static readonly ADPropertyDefinition TokenIssuerCertReference = new ADPropertyDefinition("TokenIssuerCertReference", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedTokenIssuerCertReference", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023B7 RID: 9143
		public static readonly ADPropertyDefinition TokenIssuerPrevCertReference = new ADPropertyDefinition("TokenIssuerPrevCertReference", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedTokenIssuerPrevCertReference", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023B8 RID: 9144
		public static readonly ADPropertyDefinition NamespaceProvisioner = new ADPropertyDefinition("NamespaceProvisioner", ExchangeObjectVersion.Exchange2010, typeof(FederationTrust.NamespaceProvisionerType), null, ADPropertyDefinitionFlags.Calculated, FederationTrust.NamespaceProvisionerType.LiveDomainServices, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FederationTrustSchema.RawAdminDescription
		}, null, new GetterDelegate(FederationTrustSchema.NamespaceProvisionerTypeGetter), new SetterDelegate(FederationTrustSchema.NamespaceProvisionerTypeSetter), null, null);

		// Token: 0x040023B9 RID: 9145
		private static string[] namespaceProvisionerProgIds = new string[]
		{
			"WINDOWSLIVEDOMAINSERVICES",
			"WINDOWSLIVEDOMAINSERVICES2",
			"EXTERNALPROCESS"
		};

		// Token: 0x040023BA RID: 9146
		private static string[] partnerSTSTypeProgIds = new string[]
		{
			"WINDOWSLIVEID"
		};
	}
}

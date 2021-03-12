using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000696 RID: 1686
	internal static class OAuthConfigHelper
	{
		// Token: 0x06004E66 RID: 20070 RVA: 0x00120864 File Offset: 0x0011EA64
		public static string GetOrganizationRealm(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (OAuthConfigHelper.isMultiTenancyEnabled && organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				throw new InvalidOperationException("Should not query the global Realm property in a Datacenter or Hosting deployement.");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 53, "GetOrganizationRealm", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			string result;
			if (OAuthConfigHelper.isMultiTenancyEnabled)
			{
				OrganizationId currentOrganizationId = tenantOrTopologyConfigurationSession.SessionSettings.CurrentOrganizationId;
				ExchangeConfigurationUnit exchangeConfigurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(currentOrganizationId.ConfigurationUnit);
				result = exchangeConfigurationUnit.ExternalDirectoryOrganizationId;
			}
			else
			{
				AuthConfig authConfig = AuthConfig.Read(tenantOrTopologyConfigurationSession);
				if (!string.IsNullOrEmpty(authConfig.Realm))
				{
					result = authConfig.Realm;
				}
				else
				{
					result = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain().DomainName.ToString();
				}
			}
			return result;
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x00120924 File Offset: 0x0011EB24
		public static string GetTenantId(OrganizationId organizationId)
		{
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				return null;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 94, "GetTenantId", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnit = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
			return exchangeConfigurationUnit.ExternalDirectoryOrganizationId;
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00120970 File Offset: 0x0011EB70
		public static string GetServiceName()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 108, "GetServiceName", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			AuthConfig authConfig = AuthConfig.Read(tenantOrTopologyConfigurationSession);
			if (authConfig == null)
			{
				throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidAuthSettings(string.Empty));
			}
			return (string)authConfig[AuthConfigSchema.ServiceName];
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x001209C6 File Offset: 0x0011EBC6
		public static X509Certificate2 GetCurrentSigningKey()
		{
			return OAuthConfigHelper.GetSigningKey(AuthConfigSchema.CurrentCertificateThumbprint);
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x001209D2 File Offset: 0x0011EBD2
		public static X509Certificate2 GetPreviousSigningKey()
		{
			return OAuthConfigHelper.GetSigningKey(AuthConfigSchema.PreviousCertificateThumbprint);
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x001209DE File Offset: 0x0011EBDE
		public static X509Certificate2 GetNextSigningKey()
		{
			return OAuthConfigHelper.GetSigningKey(AuthConfigSchema.NextCertificateThumbprint);
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x001209EC File Offset: 0x0011EBEC
		private static X509Certificate2 GetSigningKey(ADPropertyDefinition certProperty)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 151, "GetSigningKey", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			AuthConfig authConfig = AuthConfig.Read(tenantOrTopologyConfigurationSession);
			if (authConfig == null)
			{
				throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidAuthSettings(string.Empty));
			}
			string text = (string)authConfig[certProperty];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			X509Store x509Store = null;
			X509Certificate2 result;
			try
			{
				x509Store = new X509Store(StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, text, false);
				if (x509Certificate2Collection.Count == 0)
				{
					throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidPrivateCertificate(text));
				}
				if (!x509Certificate2Collection[0].HasPrivateKey)
				{
					throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidPrivateCertificate(text));
				}
				result = x509Certificate2Collection[0];
			}
			catch (CryptographicException innerException)
			{
				throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidPrivateCertificate(text), innerException);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return result;
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x00120AE8 File Offset: 0x0011ECE8
		public static PartnerApplication[] GetRootOrgPartnerApplications()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 204, "GetRootOrgPartnerApplications", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			return tenantOrTopologyConfigurationSession.Find<PartnerApplication>(null, QueryScope.SubTree, null, null, ADGenericPagedReader<PartnerApplication>.DefaultPageSize);
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x00120B28 File Offset: 0x0011ED28
		public static PartnerApplication GetPartnerApplication(OrganizationId organizationId, string applicationIdentifier)
		{
			if (string.IsNullOrEmpty(applicationIdentifier))
			{
				throw new ArgumentNullException("applicationIdentifier");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			PartnerApplication partnerApplication = OAuthConfigHelper.GetPartnerApplication(sessionSettings, applicationIdentifier);
			if (partnerApplication == null && !organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				ADSessionSettings.FromRootOrgScopeSet();
				partnerApplication = OAuthConfigHelper.GetPartnerApplication(sessionSettings, applicationIdentifier);
			}
			if (partnerApplication == null)
			{
				throw new PartnerApplicationNotFoundException(DirectoryStrings.ErrorPartnerApplicationNotFound(applicationIdentifier));
			}
			return partnerApplication;
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x00120BB8 File Offset: 0x0011EDB8
		public static OrganizationId ResolveOrganizationByRealm(string realm)
		{
			if (string.IsNullOrEmpty(realm))
			{
				throw new ArgumentNullException("realm");
			}
			OrganizationId result = null;
			if (OAuthConfigHelper.isMultiTenancyEnabled)
			{
				try
				{
					Guid externalDirectoryOrganizationId;
					ADSessionSettings adsessionSettings;
					if (Guid.TryParse(realm, out externalDirectoryOrganizationId))
					{
						adsessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
					}
					else
					{
						SmtpDomain smtpDomain;
						if (!SmtpDomain.TryParse(realm, out smtpDomain))
						{
							throw new RealmFormatInvalidException(DirectoryStrings.ErrorRealmFormatInvalid(realm));
						}
						adsessionSettings = ADSessionSettings.FromTenantAcceptedDomain(realm);
					}
					return adsessionSettings.CurrentOrganizationId;
				}
				catch (CannotResolveExternalDirectoryOrganizationIdException innerException)
				{
					throw new RealmNotFoundException(DirectoryStrings.ErrorRealmNotFound(realm), innerException);
				}
				catch (CannotResolveTenantNameException innerException2)
				{
					throw new RealmNotFoundException(DirectoryStrings.ErrorRealmNotFound(realm), innerException2);
				}
			}
			result = OrganizationId.ForestWideOrgId;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 306, "ResolveOrganizationByRealm", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			if (tenantOrTopologyConfigurationSession.GetAcceptedDomainByDomainName(realm) == null)
			{
				AuthConfig authConfig = AuthConfig.Read(tenantOrTopologyConfigurationSession);
				if (!realm.Equals(authConfig.Realm))
				{
					if (OAuthConfigHelper.GetAuthServers().FirstOrDefault((AuthServer server) => realm.Equals(server.Realm, StringComparison.OrdinalIgnoreCase)) == null)
					{
						throw new RealmNotFoundException(DirectoryStrings.ErrorRealmNotFound(realm));
					}
				}
			}
			return result;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x00120D30 File Offset: 0x0011EF30
		public static AuthServer[] GetAuthServers()
		{
			return OAuthConfigHelper.GetAuthServerByType(AuthServerType.MicrosoftACS);
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x00120D38 File Offset: 0x0011EF38
		public static AuthServer GetFacebookAuthServer()
		{
			AuthServer[] authServerByType = OAuthConfigHelper.GetAuthServerByType(AuthServerType.Facebook);
			if (authServerByType.Length == 0)
			{
				throw new AuthServerNotFoundException(DirectoryStrings.ErrorAuthServerTypeNotFound(AuthServerType.Facebook.ToString()));
			}
			return authServerByType[0];
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00120D6C File Offset: 0x0011EF6C
		public static AuthServer GetLinkedInAuthServer()
		{
			AuthServer[] authServerByType = OAuthConfigHelper.GetAuthServerByType(AuthServerType.LinkedIn);
			if (authServerByType.Length == 0)
			{
				throw new AuthServerNotFoundException(DirectoryStrings.ErrorAuthServerTypeNotFound(AuthServerType.LinkedIn.ToString()));
			}
			return authServerByType[0];
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x00120DA0 File Offset: 0x0011EFA0
		public static AuthServer[] GetAuthServerByType(AuthServerType type)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 374, "GetAuthServerByType", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			return tenantOrTopologyConfigurationSession.Find<AuthServer>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AuthServerSchema.Type, type), null, ADGenericPagedReader<AuthServer>.DefaultPageSize);
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00120DF0 File Offset: 0x0011EFF0
		public static AuthServer GetAuthServer(string issuerIdentifier)
		{
			if (string.IsNullOrEmpty(issuerIdentifier))
			{
				throw new ArgumentNullException("issuerIdentifier");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 400, "GetAuthServer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			AuthServer[] array = tenantOrTopologyConfigurationSession.Find<AuthServer>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AuthServerSchema.IssuerIdentifier, issuerIdentifier), null, 1);
			if (array.Length == 0)
			{
				throw new AuthServerNotFoundException(DirectoryStrings.ErrorAuthServerNotFound(issuerIdentifier));
			}
			return array[0];
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x00120E60 File Offset: 0x0011F060
		private static PartnerApplication GetPartnerApplication(ADSessionSettings sessionSettings, string applicationIdentifier)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 427, "GetPartnerApplication", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OAuth\\OAuthConfigHelper.cs");
			PartnerApplication[] array = tenantOrTopologyConfigurationSession.Find<PartnerApplication>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, PartnerApplicationSchema.ApplicationIdentifier, applicationIdentifier), null, 2);
			if (array.Length == 1)
			{
				return array[0];
			}
			if (array.Length == 2)
			{
				throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorDuplicatePartnerApplicationId(applicationIdentifier));
			}
			return null;
		}

		// Token: 0x04003560 RID: 13664
		private static readonly bool isMultiTenancyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
	}
}

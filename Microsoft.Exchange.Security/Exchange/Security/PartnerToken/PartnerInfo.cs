using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Security.PartnerToken
{
	// Token: 0x02000095 RID: 149
	internal class PartnerInfo
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x0002935F File Offset: 0x0002755F
		private PartnerInfo()
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00029367 File Offset: 0x00027567
		private PartnerInfo(string userPrincipalName, OrganizationId organizationId, string externalOrganizationId, string[] roleGroupExternalObjectIds)
		{
			this.UPN = userPrincipalName;
			this.OrganizationId = organizationId;
			this.ExternalOrganizationId = externalOrganizationId;
			this.RoleGroupExternalObjectIds = roleGroupExternalObjectIds;
			this.knownPartnership = new Dictionary<OrganizationId, bool>(1024);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0002939C File Offset: 0x0002759C
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000293A4 File Offset: 0x000275A4
		public string UPN { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000293AD File Offset: 0x000275AD
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000293B5 File Offset: 0x000275B5
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000293BE File Offset: 0x000275BE
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000293C6 File Offset: 0x000275C6
		public string ExternalOrganizationId { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000293CF File Offset: 0x000275CF
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x000293D7 File Offset: 0x000275D7
		public string[] RoleGroupExternalObjectIds { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000293E0 File Offset: 0x000275E0
		public static PartnerInfo Invalid
		{
			get
			{
				return PartnerInfo.invalidPartnerInfo;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000293E8 File Offset: 0x000275E8
		public static PartnerInfo CreateFromADObjectId(ADObjectId objectId, OrganizationId organizationId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				ExTraceGlobals.PartnerTokenTracer.TraceDebug(0L, "[PartnerInfo::CreateFromADObjectId] we do not create partner info for the first org user");
				return PartnerInfo.Invalid;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 159, "CreateFromADObjectId", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\PartnerToken\\PartnerInfo.cs");
			ADUser aduser = tenantOrRootOrgRecipientSession.FindADUserByObjectId(objectId);
			if (aduser == null)
			{
				ExTraceGlobals.PartnerTokenTracer.TraceDebug<ADObjectId>(0L, "[PartnerInfo::CreateFromADObjectId] fail to find ADUser for objectId {0}", objectId);
				return PartnerInfo.Invalid;
			}
			string userPrincipalName = aduser.UserPrincipalName;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 177, "CreateFromADObjectId", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\PartnerToken\\PartnerInfo.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
			if (exchangeConfigurationUnit == null)
			{
				ExTraceGlobals.PartnerTokenTracer.TraceWarning<OrganizationId>(0L, "[PartnerInfo::CreateFromADObjectId] failed to read the configuration unit for organization {0}", organizationId);
				return PartnerInfo.Invalid;
			}
			string[] roleGroupExternalObjectIds = null;
			if (!PartnerInfo.TryGetRoleGroupExternalDirectoryObjectId(tenantOrRootOrgRecipientSession, aduser, out roleGroupExternalObjectIds))
			{
				ExTraceGlobals.PartnerTokenTracer.TraceDebug<string, ADObjectId>(0L, "[PartnerInfo::CreateFromADObjectId] user {0} (objectId {1}) has no partner managed groups.", userPrincipalName, objectId);
				return PartnerInfo.Invalid;
			}
			return new PartnerInfo(userPrincipalName, organizationId, exchangeConfigurationUnit.ExternalDirectoryOrganizationId, roleGroupExternalObjectIds);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0002950C File Offset: 0x0002770C
		public bool HasPartnerRelationship(OrganizationId delegatedOrganizationId)
		{
			if (delegatedOrganizationId == null)
			{
				return true;
			}
			bool flag2;
			lock (this.knownPartnership)
			{
				if (!this.knownPartnership.TryGetValue(delegatedOrganizationId, out flag2))
				{
					flag2 = PartnerToken.FindLinkedRoleGroupInDelegatedOrganization(delegatedOrganizationId, this.ExternalOrganizationId, this.RoleGroupExternalObjectIds);
					this.knownPartnership.Add(delegatedOrganizationId, flag2);
				}
			}
			return flag2;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00029584 File Offset: 0x00027784
		public XmlElement CreateSamlToken(string assertionId, string targetTenant, byte[] binarySecret, TimeSpan tokenLifetime)
		{
			ExternalAuthentication current = ExternalAuthentication.GetCurrent();
			SecurityTokenService securityTokenService = current.GetSecurityTokenService(this.OrganizationId);
			XmlElement result;
			using (X509SecurityToken x509SecurityToken = new X509SecurityToken(securityTokenService.Certificate))
			{
				SecurityKeyIdentifier securityKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					x509SecurityToken.CreateKeyIdentifierClause<X509SubjectKeyIdentifierClause>()
				});
				SecurityKey securityKey = x509SecurityToken.SecurityKeys[0];
				SecurityKeyIdentifier proofKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					new EncryptedKeyIdentifierClause(securityKey.EncryptKey("http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p", binarySecret), "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p", securityKeyIdentifier, PartnerInfo.KeyName)
				});
				SamlAssertion samlAssertion = this.CreateSamlAssertion(assertionId, targetTenant, current.TokenValidator.TargetUri, tokenLifetime, proofKeyIdentifier);
				samlAssertion.SigningCredentials = new SigningCredentials(securityKey, "http://www.w3.org/2000/09/xmldsig#rsa-sha1", "http://www.w3.org/2000/09/xmldsig#sha1", securityKeyIdentifier);
				SamlSecurityToken samlSecurityToken = new SamlSecurityToken(samlAssertion);
				XmlElement xmlFromSecurityToken = SecurityTokenService.GetXmlFromSecurityToken(samlSecurityToken);
				if (ExTraceGlobals.PartnerTokenTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.PartnerTokenTracer.TraceDebug<string, string>(0L, "[PartnerInfo.CreateSamlToken] partner token was generated for {0} as {1}", this.UPN, xmlFromSecurityToken.OuterXml);
				}
				result = xmlFromSecurityToken;
			}
			return result;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002969C File Offset: 0x0002789C
		private SamlAssertion CreateSamlAssertion(string assertionId, string targetTenant, Uri targetUri, TimeSpan tokenLifetime, SecurityKeyIdentifier proofKeyIdentifier)
		{
			DateTime utcNow = DateTime.UtcNow;
			SamlConditions samlConditions = new SamlConditions(utcNow, utcNow.Add(tokenLifetime));
			samlConditions.Conditions.Add(new SamlAudienceRestrictionCondition(new Uri[]
			{
				targetUri
			}));
			SamlSubject samlSubject = new SamlSubject("http://schemas.xmlsoap.org/claims/UPN", null, this.UPN, new string[]
			{
				SamlConstants.HolderOfKey
			}, null, proofKeyIdentifier);
			SamlAttributeStatement samlAttributeStatement = new SamlAttributeStatement(samlSubject, new SamlAttribute[]
			{
				new SamlAttribute(new Claim("http://schemas.microsoft.com/exchange/services/2006/partnertoken/targettenant", targetTenant, Rights.PossessProperty)),
				new SamlAttribute(new Claim("http://schemas.microsoft.com/exchange/services/2006/partnertoken/linkedpartnerorganizationid", this.ExternalOrganizationId, Rights.PossessProperty)),
				new SamlAttribute("http://schemas.microsoft.com/exchange/services/2006/partnertoken", "linkedpartnergroupid", this.RoleGroupExternalObjectIds)
			});
			return new SamlAssertion(assertionId, "http://schemas.microsoft.com/exchange/2010/autodiscover/getusersettings", utcNow, samlConditions, null, new SamlStatement[]
			{
				samlAttributeStatement
			});
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00029788 File Offset: 0x00027988
		public static XmlElement GetTokenReference(string assertionId)
		{
			return SecurityTokenService.CreateSamlAssertionSecurityTokenReference(SecurityTokenService.CreateXmlDocument(), assertionId);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00029798 File Offset: 0x00027998
		private static bool TryGetRoleGroupExternalDirectoryObjectId(IRecipientSession recipientSession, ADRawEntry userEntry, out string[] roleGroupExternalObjectIds)
		{
			roleGroupExternalObjectIds = null;
			string[] array = recipientSession.GetTokenSids(userEntry, AssignmentMethod.SecurityGroup).ToArray();
			string arg = (string)userEntry[ADUserSchema.UserPrincipalName];
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.PartnerTokenTracer.TraceWarning<string>(0L, "[PartnerInfo::TryGetRoleGroupExternalDirectoryObjectId] user {0} has no security group assigned.", arg);
				return false;
			}
			if (ExTraceGlobals.PartnerTokenTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.PartnerTokenTracer.TraceDebug<string, string>(0L, "[PartnerInfo::TryGetRoleGroupExternalDirectoryObjectId] user {0} has group sids {1}.", arg, string.Join(",", array));
			}
			ADObjectId[] array2 = recipientSession.ResolveSidsToADObjectIds(array);
			Result<ADGroup>[] array3 = recipientSession.ReadMultipleADGroups(array2);
			List<string> list = new List<string>();
			for (int i = 0; i < array3.Length; i++)
			{
				if (array3[i].Error != null)
				{
					ExTraceGlobals.PartnerTokenTracer.TraceWarning<ADObjectId, ProviderError>(0L, "[PartnerInfo::TryGetRoleGroupExternalDirectoryObjectId] failed to read the group information for sid {0}, error: {1}", array2[i], array3[i].Error);
				}
				else
				{
					ADGroup data = array3[i].Data;
					ExTraceGlobals.PartnerTokenTracer.TraceDebug<SecurityIdentifier, ADGroup>(0L, "[PartnerInfo::TryGetRoleGroupExternalDirectoryObjectId] group information for sid {0} is read as {1}", data.Sid, data);
					if (!string.IsNullOrEmpty(data.ExternalDirectoryObjectId) && data.RawCapabilities != null && data.RawCapabilities.Contains(Capability.Partner_Managed))
					{
						ExTraceGlobals.PartnerTokenTracer.TraceDebug<string, SecurityIdentifier>(0L, "[PartnerInfo::TryGetRoleGroupExternalDirectoryObjectId] add group {0} with sid {1}", data.Name, data.Sid);
						list.Add(data.ExternalDirectoryObjectId);
					}
					else
					{
						ExTraceGlobals.PartnerTokenTracer.TraceDebug<string, SecurityIdentifier>(0L, "[PartnerInfo::TryGetRoleGroupExternalDirectoryObjectId] skip group {0} with sid {1} which is not partner_managed", data.Name, data.Sid);
					}
				}
			}
			if (list.Count > 0)
			{
				roleGroupExternalObjectIds = list.ToArray();
				return true;
			}
			return false;
		}

		// Token: 0x04000554 RID: 1364
		internal static readonly string KeyName = "#exch";

		// Token: 0x04000555 RID: 1365
		private Dictionary<OrganizationId, bool> knownPartnership;

		// Token: 0x04000556 RID: 1366
		private static PartnerInfo invalidPartnerInfo = new PartnerInfo();

		// Token: 0x04000557 RID: 1367
		private static PropertyDefinition[] userProperties = new PropertyDefinition[]
		{
			ADObjectSchema.OrganizationId,
			ADObjectSchema.Id,
			ADUserSchema.UserPrincipalName
		};

		// Token: 0x04000558 RID: 1368
		internal static PropertyDefinition[] groupProperties = new PropertyDefinition[]
		{
			ADObjectSchema.RawName,
			ADObjectSchema.Name,
			ADGroupSchema.LinkedPartnerGroupAndOrganizationId,
			ADObjectSchema.Id,
			ADObjectSchema.ExchangeVersion
		};
	}
}

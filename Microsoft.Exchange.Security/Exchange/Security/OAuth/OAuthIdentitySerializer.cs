using System;
using System.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000DC RID: 220
	internal sealed class OAuthIdentitySerializer
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00034F84 File Offset: 0x00033184
		public static string SerializeOAuthIdentity(OAuthIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			SerializedOAuthIdentity serializedOAuthIdentity = new SerializedOAuthIdentity();
			if (!identity.IsAppOnly)
			{
				serializedOAuthIdentity.UserDn = identity.ActAsUser.DistinguishedName;
			}
			if (identity.OAuthApplication.PartnerApplication != null)
			{
				serializedOAuthIdentity.PartnerApplicationDn = identity.OAuthApplication.PartnerApplication.DistinguishedName;
				serializedOAuthIdentity.PartnerApplicationAppId = identity.OAuthApplication.PartnerApplication.ApplicationIdentifier;
				serializedOAuthIdentity.PartnerApplicationRealm = identity.OAuthApplication.PartnerApplication.Realm;
			}
			if (identity.OAuthApplication.OfficeExtension != null)
			{
				serializedOAuthIdentity.OfficeExtensionId = identity.OAuthApplication.OfficeExtension.ExtensionId;
				serializedOAuthIdentity.Scope = identity.OAuthApplication.OfficeExtension.Scope;
			}
			if (identity.OAuthApplication.V1ProfileApp != null)
			{
				serializedOAuthIdentity.V1ProfileAppId = identity.OAuthApplication.V1ProfileApp.AppId;
				serializedOAuthIdentity.Scope = identity.OAuthApplication.V1ProfileApp.Scope;
			}
			if (identity.OAuthApplication.IsFromSameOrgExchange != null)
			{
				serializedOAuthIdentity.IsFromSameOrgExchange = identity.OAuthApplication.IsFromSameOrgExchange.Value.ToString();
			}
			if (identity.OrganizationId.ConfigurationUnit != null)
			{
				serializedOAuthIdentity.OrganizationName = identity.OrganizationId.ConfigurationUnit.Parent.Name;
				serializedOAuthIdentity.OrganizationIdBase64 = CommonAccessTokenAccessor.SerializeOrganizationId(identity.OrganizationId);
			}
			return serializedOAuthIdentity.SerializeToJson();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000350F8 File Offset: 0x000332F8
		public static CommonAccessToken ConvertToCommonAccessToken(OAuthIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			string value = OAuthIdentitySerializer.SerializeOAuthIdentity(identity);
			CommonAccessToken commonAccessToken = new CommonAccessToken(AccessTokenType.OAuth);
			commonAccessToken.ExtensionData["OAuthData"] = value;
			return commonAccessToken;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00035134 File Offset: 0x00033334
		public static OAuthIdentity ConvertFromCommonAccessToken(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "[OAuthIdentitySerializer::ConvertFromCommonAccessToken] Token type is {0}.", token.TokenType);
			if (!AccessTokenType.OAuth.ToString().Equals(token.TokenType, StringComparison.OrdinalIgnoreCase))
			{
				string message = string.Format("Unexpect token type {0}.", token.TokenType);
				throw new OAuthIdentityDeserializationException(message);
			}
			string text = token.ExtensionData["OAuthData"];
			ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "[OAuthIdentitySerializer::ConvertFromCommonAccessToken] OAuthData: {0}.", text);
			if (string.IsNullOrEmpty(text))
			{
				throw new OAuthIdentityDeserializationException("The access token does not contain OAuthData.");
			}
			return OAuthIdentitySerializer.DeserializeOAuthIdentity(text);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000351D4 File Offset: 0x000333D4
		public static OAuthIdentity DeserializeOAuthIdentity(string blob)
		{
			if (string.IsNullOrEmpty(blob))
			{
				throw new ArgumentNullException("blob");
			}
			ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "[OAuthIdentitySerializer::DeserializeOAuthIdentity] Deserializing OAuth identity string blob {0}", blob);
			SerializedOAuthIdentity serializedId = null;
			Exception ex = null;
			try
			{
				serializedId = blob.DeserializeFromJson<SerializedOAuthIdentity>();
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.OAuthTracer.TraceError<Exception>(0L, "[OAuthIdentitySerializer::DeserializeOAuthIdentity] Unable to deserialize the OAuth identity. Error: {0}", ex);
				throw new OAuthIdentityDeserializationException("Unable to deserialize the OAuth identity.", ex);
			}
			OAuthIdentity result = null;
			try
			{
				result = OAuthIdentitySerializer.RehydrateOAuthIdentity(serializedId);
			}
			catch (ADOperationException ex4)
			{
				ex = ex4;
			}
			catch (ADTransientException ex5)
			{
				ex = ex5;
			}
			catch (DataValidationException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ExTraceGlobals.OAuthTracer.TraceError<Exception>(0L, "[OAuthIdentitySerializer::DeserializeOAuthIdentity] Error occurred during directory operation. Error: {0}", ex);
				throw new OAuthIdentityDeserializationException("Error occurred during directory operation.", ex);
			}
			return result;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000352C4 File Offset: 0x000334C4
		private static OAuthIdentity RehydrateOAuthIdentity(SerializedOAuthIdentity serializedId)
		{
			OrganizationId organizationId = OrganizationId.ForestWideOrgId;
			ADSessionSettings adsessionSettings;
			if (!string.IsNullOrEmpty(serializedId.OrganizationIdBase64))
			{
				organizationId = CommonAccessTokenAccessor.DeserializeOrganizationId(serializedId.OrganizationIdBase64);
				try
				{
					adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
					goto IL_82;
				}
				catch (CannotResolveTenantNameException inner)
				{
					throw new OAuthIdentityDeserializationException(string.Format("Cannot resolve tenant {0}.", serializedId.OrganizationName), inner);
				}
			}
			if (!string.IsNullOrEmpty(serializedId.OrganizationName))
			{
				try
				{
					adsessionSettings = ADSessionSettings.FromTenantCUName(serializedId.OrganizationName);
					organizationId = adsessionSettings.CurrentOrganizationId;
					goto IL_82;
				}
				catch (CannotResolveTenantNameException inner2)
				{
					throw new OAuthIdentityDeserializationException(string.Format("Cannot resolve tenant {0}.", serializedId.OrganizationName), inner2);
				}
			}
			adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IL_82:
			OAuthApplication oauthApplication;
			if (!string.IsNullOrEmpty(serializedId.V1ProfileAppId) && !string.IsNullOrEmpty(serializedId.PartnerApplicationAppId))
			{
				PartnerApplication partnerApplication = OAuthIdentitySerializer.PartnerApplicationCache.Instance.Get(new OAuthIdentitySerializer.PartnerApplicationCacheKey(serializedId));
				oauthApplication = new OAuthApplication(new V1ProfileAppInfo(serializedId.V1ProfileAppId, serializedId.Scope, null), partnerApplication);
			}
			else if (!string.IsNullOrEmpty(serializedId.V1ProfileAppId))
			{
				oauthApplication = new OAuthApplication(new V1ProfileAppInfo(serializedId.V1ProfileAppId, serializedId.Scope, null));
			}
			else if (!string.IsNullOrEmpty(serializedId.OfficeExtensionId))
			{
				oauthApplication = new OAuthApplication(new OfficeExtensionInfo(serializedId.OfficeExtensionId, serializedId.Scope));
			}
			else
			{
				oauthApplication = new OAuthApplication(OAuthIdentitySerializer.PartnerApplicationCache.Instance.Get(new OAuthIdentitySerializer.PartnerApplicationCacheKey(serializedId)));
			}
			if (!string.IsNullOrEmpty(serializedId.IsFromSameOrgExchange))
			{
				oauthApplication.IsFromSameOrgExchange = new bool?(string.Equals(bool.TrueString, serializedId.IsFromSameOrgExchange));
			}
			ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "[OAuthIdentitySerializer::RehydrateOAuthIdentity] The resolved OAuthApplication object is {0}.", oauthApplication.Id);
			MiniRecipient miniRecipient = null;
			if (!string.IsNullOrEmpty(serializedId.UserDn))
			{
				miniRecipient = OAuthIdentitySerializer.UserCache.Instance.Get(new OAuthIdentitySerializer.UserCacheKey(serializedId));
				ExTraceGlobals.OAuthTracer.TraceDebug<ObjectId>(0L, "[OAuthIdentitySerializer::RehydrateOAuthIdentity] The resolved user object is {0}.", miniRecipient.Identity);
			}
			return OAuthIdentity.Create(organizationId, oauthApplication, OAuthActAsUser.CreateFromMiniRecipient(organizationId, miniRecipient));
		}

		// Token: 0x020000DD RID: 221
		internal sealed class PartnerApplicationCacheKey
		{
			// Token: 0x06000795 RID: 1941 RVA: 0x000354B8 File Offset: 0x000336B8
			public PartnerApplicationCacheKey(SerializedOAuthIdentity serializedOAuthIdentity) : this(serializedOAuthIdentity.PartnerApplicationDn, serializedOAuthIdentity.PartnerApplicationAppId, serializedOAuthIdentity.PartnerApplicationRealm)
			{
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x000354D2 File Offset: 0x000336D2
			public PartnerApplicationCacheKey(string dn, string appId, string realm)
			{
				this.partnerApplicationDn = dn;
				this.partnerApplicationAppId = appId;
				this.partnerApplicationRealm = realm;
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x000354EF File Offset: 0x000336EF
			public string PartnerApplicationDn
			{
				get
				{
					return this.partnerApplicationDn;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000798 RID: 1944 RVA: 0x000354F7 File Offset: 0x000336F7
			public string PartnerApplicationAppId
			{
				get
				{
					return this.partnerApplicationAppId;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000799 RID: 1945 RVA: 0x000354FF File Offset: 0x000336FF
			public string PartnerApplicationRealm
			{
				get
				{
					return this.partnerApplicationRealm;
				}
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x00035508 File Offset: 0x00033708
			public override bool Equals(object obj)
			{
				OAuthIdentitySerializer.PartnerApplicationCacheKey partnerApplicationCacheKey = obj as OAuthIdentitySerializer.PartnerApplicationCacheKey;
				return partnerApplicationCacheKey != null && (string.Equals(this.partnerApplicationDn, partnerApplicationCacheKey.partnerApplicationDn, StringComparison.OrdinalIgnoreCase) && string.Equals(this.partnerApplicationAppId, partnerApplicationCacheKey.partnerApplicationAppId, StringComparison.OrdinalIgnoreCase)) && string.Equals(this.partnerApplicationRealm, partnerApplicationCacheKey.partnerApplicationRealm, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600079B RID: 1947 RVA: 0x00035560 File Offset: 0x00033760
			public override int GetHashCode()
			{
				int num = this.partnerApplicationDn.GetHashCodeCaseInsensitive();
				num ^= this.partnerApplicationAppId.GetHashCodeCaseInsensitive();
				if (this.partnerApplicationRealm != null)
				{
					num ^= this.partnerApplicationRealm.GetHashCodeCaseInsensitive();
				}
				return num;
			}

			// Token: 0x0400071D RID: 1821
			private readonly string partnerApplicationDn;

			// Token: 0x0400071E RID: 1822
			private readonly string partnerApplicationAppId;

			// Token: 0x0400071F RID: 1823
			private readonly string partnerApplicationRealm;
		}

		// Token: 0x020000DE RID: 222
		internal sealed class PartnerApplicationCache : LazyLookupTimeoutCache<OAuthIdentitySerializer.PartnerApplicationCacheKey, PartnerApplication>
		{
			// Token: 0x0600079C RID: 1948 RVA: 0x0003559E File Offset: 0x0003379E
			private PartnerApplicationCache() : base(2, OAuthIdentitySerializer.PartnerApplicationCache.cacheSize.Value, false, OAuthIdentitySerializer.PartnerApplicationCache.cacheTimeToLive.Value)
			{
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600079D RID: 1949 RVA: 0x000355C7 File Offset: 0x000337C7
			public static OAuthIdentitySerializer.PartnerApplicationCache Instance
			{
				get
				{
					return OAuthIdentitySerializer.PartnerApplicationCache.instance;
				}
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x00035608 File Offset: 0x00033808
			protected override PartnerApplication CreateOnCacheMiss(OAuthIdentitySerializer.PartnerApplicationCacheKey key, ref bool shouldAdd)
			{
				shouldAdd = true;
				ADObjectId adobjectId = new ADObjectId(key.PartnerApplicationDn);
				ADSessionSettings adsessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(adobjectId);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, adsessionSettings, 454, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\OAuthIdentitySerializer.cs");
				PartnerApplication partnerApplication = tenantOrTopologyConfigurationSession.Read<PartnerApplication>(adobjectId);
				if (partnerApplication == null && adsessionSettings.CurrentOrganizationId == OrganizationId.ForestWideOrgId)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<ADObjectId>(0L, "[PartnerApplicationCache:CreateOnCacheMiss] Could not find the PartnerApplication object {0}. Trying to search by ApplicationId and Realm.", adobjectId);
					if (DateTime.UtcNow - this.lastRetrieveTime > OAuthIdentitySerializer.PartnerApplicationCache.cacheTimeToLive.Value)
					{
						ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[PartnerApplicationCache:CreateOnCacheMiss] Refreshing datacenter level PAs");
						this.firstOrgPartnerApps = tenantOrTopologyConfigurationSession.Find<PartnerApplication>(null, QueryScope.SubTree, null, null, ADGenericPagedReader<PartnerApplication>.DefaultPageSize);
						this.lastRetrieveTime += OAuthIdentitySerializer.PartnerApplicationCache.cacheTimeToLive.Value;
					}
					if (this.firstOrgPartnerApps != null)
					{
						PartnerApplication[] array = (from pa in this.firstOrgPartnerApps
						where OAuthCommon.IsIdMatch(pa.ApplicationIdentifier, key.PartnerApplicationAppId) && OAuthCommon.IsRealmMatch(pa.Realm, key.PartnerApplicationRealm)
						select pa).ToArray<PartnerApplication>();
						if (array.Length != 1)
						{
							ExTraceGlobals.OAuthTracer.TraceError<int, string, string>(0L, "[PartnerApplicationCache:CreateOnCacheMiss] Found {0} matched Partner Application object with id '{1}', realm '{2}'", array.Length, key.PartnerApplicationAppId, key.PartnerApplicationRealm);
						}
						partnerApplication = array[0];
					}
				}
				if (partnerApplication == null)
				{
					throw new OAuthIdentityDeserializationException(string.Format("Unabled to retrieve Partner Application object '{0}'.", key.PartnerApplicationDn));
				}
				return partnerApplication;
			}

			// Token: 0x04000720 RID: 1824
			private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("BackendPartnerApplicationCacheTimeToLive", TimeSpanUnit.Minutes, TimeSpan.FromHours(30.0), ExTraceGlobals.OAuthTracer);

			// Token: 0x04000721 RID: 1825
			private static IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("BackendPartnerApplicationCacheMaxItems", 500, ExTraceGlobals.OAuthTracer);

			// Token: 0x04000722 RID: 1826
			private static OAuthIdentitySerializer.PartnerApplicationCache instance = new OAuthIdentitySerializer.PartnerApplicationCache();

			// Token: 0x04000723 RID: 1827
			private DateTime lastRetrieveTime = DateTime.MinValue;

			// Token: 0x04000724 RID: 1828
			private PartnerApplication[] firstOrgPartnerApps;
		}

		// Token: 0x020000DF RID: 223
		internal sealed class UserCacheKey
		{
			// Token: 0x060007A0 RID: 1952 RVA: 0x000357D3 File Offset: 0x000339D3
			public UserCacheKey(SerializedOAuthIdentity serializedOAuthIdentity)
			{
				this.organizationName = serializedOAuthIdentity.OrganizationName;
				this.userDn = serializedOAuthIdentity.UserDn;
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x000357F4 File Offset: 0x000339F4
			public override bool Equals(object obj)
			{
				OAuthIdentitySerializer.UserCacheKey userCacheKey = obj as OAuthIdentitySerializer.UserCacheKey;
				return userCacheKey != null && string.Equals(this.organizationName, userCacheKey.organizationName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.userDn, userCacheKey.userDn, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x00035838 File Offset: 0x00033A38
			public override int GetHashCode()
			{
				int num = this.userDn.GetHashCodeCaseInsensitive();
				if (this.organizationName != null)
				{
					num ^= this.userDn.GetHashCodeCaseInsensitive();
				}
				return num;
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x00035868 File Offset: 0x00033A68
			public MiniRecipient ResolveMiniRecipient()
			{
				ADSessionSettings sessionSettings = null;
				if (string.IsNullOrEmpty(this.organizationName))
				{
					sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				}
				else
				{
					try
					{
						sessionSettings = ADSessionSettings.FromTenantCUName(this.organizationName);
					}
					catch (CannotResolveTenantNameException inner)
					{
						throw new OAuthIdentityDeserializationException(string.Format("Cannot resolve tenant {0}.", this.organizationName), inner);
					}
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 582, "ResolveMiniRecipient", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\OAuthIdentitySerializer.cs");
				MiniRecipient miniRecipient = tenantOrRootOrgRecipientSession.ReadMiniRecipient(new ADObjectId(this.userDn), null);
				if (miniRecipient == null)
				{
					throw new OAuthIdentityDeserializationException(string.Format("Unabled to retrieve user '{0}'.", this.userDn));
				}
				return miniRecipient;
			}

			// Token: 0x04000725 RID: 1829
			private readonly string organizationName;

			// Token: 0x04000726 RID: 1830
			private readonly string userDn;
		}

		// Token: 0x020000E0 RID: 224
		internal sealed class UserCache : LazyLookupTimeoutCache<OAuthIdentitySerializer.UserCacheKey, MiniRecipient>
		{
			// Token: 0x060007A4 RID: 1956 RVA: 0x0003590C File Offset: 0x00033B0C
			private UserCache() : base(2, OAuthIdentitySerializer.UserCache.cacheSize.Value, false, OAuthIdentitySerializer.UserCache.cacheTimeToLive.Value)
			{
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0003592A File Offset: 0x00033B2A
			public static OAuthIdentitySerializer.UserCache Instance
			{
				get
				{
					return OAuthIdentitySerializer.UserCache.instance;
				}
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x00035931 File Offset: 0x00033B31
			protected override MiniRecipient CreateOnCacheMiss(OAuthIdentitySerializer.UserCacheKey key, ref bool shouldAdd)
			{
				shouldAdd = true;
				return key.ResolveMiniRecipient();
			}

			// Token: 0x04000727 RID: 1831
			private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("BackendUserCacheTimeToLive", TimeSpanUnit.Minutes, TimeSpan.FromHours(30.0), ExTraceGlobals.OAuthTracer);

			// Token: 0x04000728 RID: 1832
			private static IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("BackendUserCacheMaxItems", 2000, ExTraceGlobals.OAuthTracer);

			// Token: 0x04000729 RID: 1833
			private static readonly TimeSpan AbsoluteLiveTime = TimeSpan.FromMinutes(30.0);

			// Token: 0x0400072A RID: 1834
			private static OAuthIdentitySerializer.UserCache instance = new OAuthIdentitySerializer.UserCache();
		}
	}
}

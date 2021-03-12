using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000212 RID: 530
	internal static class PswsAuthZHelper
	{
		// Token: 0x06001273 RID: 4723 RVA: 0x0003B5D8 File Offset: 0x000397D8
		internal static IIdentity GetExecutingAuthZUser(UserToken userToken)
		{
			Microsoft.Exchange.Configuration.Core.AuthenticationType authenticationType = userToken.AuthenticationType;
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<Microsoft.Exchange.Configuration.Core.AuthenticationType>(0L, "[PswsAuthZHelper.GetExecutingAuthZUser] authenticationType = \"{0}\".", authenticationType);
			IIdentity identity = HttpContext.Current.Items["X-Psws-CurrentLogonUser"] as IIdentity;
			if (identity is SidOAuthIdentity)
			{
				AuthZLogger.SafeAppendGenericInfo("PswsLogonUser", "SidOAuthIdentity");
				return identity;
			}
			if (identity is WindowsTokenIdentity)
			{
				AuthZLogger.SafeAppendGenericInfo("PswsLogonUser", "WindowsTokenIdentity");
				return ((WindowsTokenIdentity)identity).ToSerializedIdentity();
			}
			return AuthZPluginHelper.ConstructAuthZUser(userToken, authenticationType);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003B65C File Offset: 0x0003985C
		internal static PswsAuthZUserToken GetAuthZPluginUserToken(UserToken userToken)
		{
			ExAssert.RetailAssert(userToken != null, "[PswsAuthorization.GetAuthZPluginUserToken] userToken can't be null.");
			Microsoft.Exchange.Configuration.Core.AuthenticationType authenticationType = userToken.AuthenticationType;
			SecurityIdentifier userSid = userToken.UserSid;
			DelegatedPrincipal delegatedPrincipal = userToken.DelegatedPrincipal;
			ExAssert.RetailAssert(userSid != null, "The user sid is invalid (null).");
			PartitionId partitionId = userToken.PartitionId;
			string text = AuthenticatedUserCache.CreateKeyForPsws(userSid, userToken.AuthenticationType, partitionId);
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "[PswsAuthZHelper.GetAuthZPluginUserToken] User cache key = \"{0}\".", text);
			AuthZPluginUserToken authZPluginUserToken;
			if (!AuthenticatedUserCache.Instance.TryGetValue(text, out authZPluginUserToken))
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug(0L, "[PswsAuthZHelper.GetAuthZPluginUserToken] User not found in cache.");
				IIdentity identity = HttpContext.Current.Items["X-Psws-CurrentLogonUser"] as IIdentity;
				SerializedIdentity serializedIdentity = null;
				if (identity is WindowsTokenIdentity)
				{
					serializedIdentity = ((WindowsTokenIdentity)identity).ToSerializedIdentity();
				}
				ADRawEntry adrawEntry = ExchangeAuthorizationPlugin.FindUserEntry(userSid, null, serializedIdentity, partitionId);
				ExAssert.RetailAssert(adrawEntry != null, "UnAuthorized. Unable to find the user.");
				bool condition = (adrawEntry is MiniRecipient || adrawEntry is ADUser) && (bool)adrawEntry[ADRecipientSchema.RemotePowerShellEnabled];
				ExAssert.RetailAssert(condition, "UnAuthorized. PSWS not enabled user.");
				authZPluginUserToken = new AuthZPluginUserToken(delegatedPrincipal, adrawEntry, authenticationType, userSid.Value);
				AuthenticatedUserCache.Instance.AddUserToCache(text, authZPluginUserToken);
			}
			return new PswsAuthZUserToken(authZPluginUserToken.DelegatedPrincipal, authZPluginUserToken.UserEntry, authenticationType, authZPluginUserToken.DefaultUserName, userToken.UserName);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0003B7B4 File Offset: 0x000399B4
		internal static ExchangeRunspaceConfigurationSettings BuildRunspaceConfigurationSettings(string connectionString, UserToken userToken, NameValueCollection collection)
		{
			Uri uri = new Uri(connectionString, UriKind.Absolute);
			ExchangeRunspaceConfigurationSettings exchangeRunspaceConfigurationSettings = ExchangeRunspaceConfigurationSettings.CreateConfigurationSettingsFromNameValueCollection(uri, collection, ExchangeRunspaceConfigurationSettings.ExchangeApplication.PswsClient);
			if (string.IsNullOrEmpty(exchangeRunspaceConfigurationSettings.TenantOrganization))
			{
				exchangeRunspaceConfigurationSettings.TenantOrganization = userToken.ManagedOrganization;
			}
			return exchangeRunspaceConfigurationSettings;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0003B7F0 File Offset: 0x000399F0
		internal static string GetPswsMembershipId(UserToken userToken, NameValueCollection collection)
		{
			ExAssert.RetailAssert(userToken != null, "[PswsAuthorization.GetPswsMembershipId] userToken can't be null.");
			string friendlyName = userToken.Organization.GetFriendlyName();
			ExchangeRunspaceConfigurationSettings exchangeRunspaceConfigurationSettings = PswsAuthZHelper.BuildRunspaceConfigurationSettings("https://www.outlook.com/Psws/Service.svc", userToken, collection);
			CultureInfo cultureInfo;
			PswsAuthZHelper.TryParseCultureInfo(collection, out cultureInfo);
			string text = userToken.ManagedOrganization;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = exchangeRunspaceConfigurationSettings.TenantOrganization;
			}
			string result = string.Format("Name:{0};AT:{1};UserOrg:{2};ManOrg:{3};SL:{4};FSL:{5};CA:{6};EDK:{7};Cul:{8};Proxy:{9}", new object[]
			{
				PswsAuthZHelper.GetUserNameForCache(userToken),
				userToken.AuthenticationType,
				friendlyName,
				text,
				exchangeRunspaceConfigurationSettings.CurrentSerializationLevel,
				exchangeRunspaceConfigurationSettings.ProxyFullSerialization,
				exchangeRunspaceConfigurationSettings.ClientApplication,
				exchangeRunspaceConfigurationSettings.EncodeDecodeKey,
				(cultureInfo == null) ? "null" : cultureInfo.Name,
				exchangeRunspaceConfigurationSettings.IsProxy
			});
			AuthZLogger.SafeSetLogger(PswsMetadata.IsProxy, exchangeRunspaceConfigurationSettings.IsProxy);
			AuthZLogger.SafeSetLogger(PswsMetadata.ClientApplication, exchangeRunspaceConfigurationSettings.ClientApplication);
			AuthZLogger.SafeSetLogger(PswsMetadata.ProxyFullSerialzation, exchangeRunspaceConfigurationSettings.ProxyFullSerialization);
			AuthZLogger.SafeSetLogger(PswsMetadata.SerializationLevel, exchangeRunspaceConfigurationSettings.CurrentSerializationLevel);
			AuthZLogger.SafeSetLogger(PswsMetadata.CultureInfo, (cultureInfo == null) ? "null" : cultureInfo.Name);
			AuthZLogger.SafeSetLogger(PswsMetadata.TenantOrganization, text);
			return result;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0003B964 File Offset: 0x00039B64
		internal static bool TryParseCultureInfo(NameValueCollection headers, out CultureInfo cultureInfo)
		{
			cultureInfo = null;
			string text = headers.Get("X-CultureInfo");
			if (!string.IsNullOrWhiteSpace(text))
			{
				try
				{
					cultureInfo = new CultureInfo(text);
					return true;
				}
				catch (CultureNotFoundException ex)
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceError<string, CultureNotFoundException>(0L, "[PswsAuthZHelper.TryParseCultureInfo] Invalid culture info \"{0}\". Exception: {1}", text, ex);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_InvalidCultureInfo, text, new object[]
					{
						text,
						ex.ToString()
					});
					AuthZLogger.SafeAppendGenericError("InvalidCultureInfo", text, false);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0003B9EC File Offset: 0x00039BEC
		private static string GetUserNameForCache(UserToken userToken)
		{
			if (userToken.UserSid != null)
			{
				return userToken.UserSid.ToString();
			}
			return userToken.UserName;
		}

		// Token: 0x04000477 RID: 1143
		private const string MembershipIdFormat = "Name:{0};AT:{1};UserOrg:{2};ManOrg:{3};SL:{4};FSL:{5};CA:{6};EDK:{7};Cul:{8};Proxy:{9}";

		// Token: 0x04000478 RID: 1144
		private const string DummyUri = "https://www.outlook.com/Psws/Service.svc";

		// Token: 0x04000479 RID: 1145
		internal const string EncodeDecodeKeyHeaderName = "X-EncodeDecode-Key";

		// Token: 0x0400047A RID: 1146
		internal const string CultureInfoHeaderKey = "X-CultureInfo";

		// Token: 0x0400047B RID: 1147
		internal const string CurrentLogonUserKey = "X-Psws-CurrentLogonUser";
	}
}

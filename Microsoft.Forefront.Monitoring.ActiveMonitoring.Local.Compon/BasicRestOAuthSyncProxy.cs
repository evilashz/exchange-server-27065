using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Hygiene.Deployment.Common;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200002F RID: 47
	public class BasicRestOAuthSyncProxy : IPolicySyncWebserviceClient, IDisposable
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000A32C File Offset: 0x0000852C
		public BasicRestOAuthSyncProxy(IHygieneLogger logger, string wsUrlBase, string acsUrl, X509Certificate2 acsTenantCertificate, string acsResourcePartnerId)
		{
			this.logger = logger;
			this.wsUrlBase = wsUrlBase;
			this.acsUrl = acsUrl;
			this.acsTenantCertificate = acsTenantCertificate;
			this.acsResourcePartnerId = acsResourcePartnerId;
			this.logger.LogMessage("FAM.BasicRestOAuthSyncProxy.ctor()");
			this.logger.LogVerbose(string.Format("wsUrlBase: '{0}'", wsUrlBase));
			this.logger.LogVerbose(string.Format("acsUrl: '{0}'", acsUrl));
			this.logger.LogVerbose(string.Format("acsTenantCertificate.Subject: '{0}'", (acsTenantCertificate != null) ? acsTenantCertificate.Subject : "(null)"));
			this.logger.LogVerbose(string.Format("acsResourcePartnerId: '{0}'", acsResourcePartnerId));
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000A3E9 File Offset: 0x000085E9
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000A3F1 File Offset: 0x000085F1
		public GetChangesRequest InvalidGetChangesRequest { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000A3FA File Offset: 0x000085FA
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000A402 File Offset: 0x00008602
		public NetHelpersWebResponse MostRecentWebResponse { get; set; }

		// Token: 0x06000150 RID: 336 RVA: 0x0000A40B File Offset: 0x0000860B
		public static BasicRestOAuthSyncProxy Create(IHygieneLogger logger, string wsUrlBase, string acsUrl, X509Certificate2 acsTenantCertificate, string acsResourcePartnerId)
		{
			return new BasicRestOAuthSyncProxy(logger, wsUrlBase, acsUrl, acsTenantCertificate, acsResourcePartnerId);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A418 File Offset: 0x00008618
		public static string GetTenantIdFromRequest(GetChangesRequest request)
		{
			string result = null;
			foreach (TenantCookie tenantCookie in ((IEnumerable<TenantCookie>)request.TenantCookies))
			{
				result = tenantCookie.TenantId.ToString();
			}
			return result;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A478 File Offset: 0x00008678
		public static string GetTenantIdFromRequest(PublishStatusRequest request)
		{
			string result = null;
			foreach (UnifiedPolicyStatus unifiedPolicyStatus in request.ConfigurationStatuses)
			{
				result = unifiedPolicyStatus.TenantId.ToString();
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A4D8 File Offset: 0x000086D8
		public static IEnumerable<Type> GetJsonKnownTypes()
		{
			return new Type[]
			{
				typeof(PolicyChangeBatch),
				typeof(PolicyConfiguration),
				typeof(RuleConfiguration),
				typeof(AssociationConfiguration),
				typeof(BindingConfiguration),
				typeof(IEnumerable<PolicyChangeBatch>),
				typeof(IEnumerable<PolicyConfiguration>),
				typeof(SyncCallerContext),
				typeof(TenantCookieCollection),
				typeof(TenantCookie)
			};
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A571 File Offset: 0x00008771
		public void Dispose()
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A574 File Offset: 0x00008774
		public PolicyChangeBatch GetChanges(GetChangesRequest request)
		{
			string requestBody = JsonHelpers.SerializeJson(BasicRestOAuthSyncProxy.GetJsonKnownTypes(), typeof(GetChangesRequest), request);
			if (this.InvalidGetChangesRequest != null)
			{
				requestBody = JsonHelpers.SerializeJson(BasicRestOAuthSyncProxy.GetJsonKnownTypes(), typeof(GetChangesRequest), this.InvalidGetChangesRequest);
			}
			NetHelpersWebResponse netHelpersWebResponse = this.DoPolicySyncRequest("/GetChanges", BasicRestOAuthSyncProxy.GetTenantIdFromRequest(request), requestBody);
			this.MostRecentWebResponse = netHelpersWebResponse;
			if (netHelpersWebResponse == null)
			{
				return null;
			}
			this.logger.LogVerbose(string.Format("netResponse.Content: '{0}'", netHelpersWebResponse.Content));
			PolicyChangeBatch result;
			try
			{
				PolicyChangeBatch policyChangeBatch = (PolicyChangeBatch)JsonHelpers.DeserializeJson(BasicRestOAuthSyncProxy.GetJsonKnownTypes(), typeof(PolicyChangeBatch), netHelpersWebResponse.Content);
				result = policyChangeBatch;
			}
			catch (SerializationException ex)
			{
				this.logger.LogError(string.Format("Received exception: '{0}'", ex.Message));
				this.logger.LogError(string.Format("netResponse.Content: '{0}'", netHelpersWebResponse.Content));
				result = null;
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A668 File Offset: 0x00008868
		public void PublishStatus(PublishStatusRequest request)
		{
			string requestBody = JsonHelpers.SerializeJson(BasicRestOAuthSyncProxy.GetJsonKnownTypes(), typeof(PublishStatusRequest), request);
			NetHelpersWebResponse mostRecentWebResponse = this.DoPolicySyncRequest("/PublishStatus", BasicRestOAuthSyncProxy.GetTenantIdFromRequest(request), requestBody);
			this.MostRecentWebResponse = mostRecentWebResponse;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public PolicyConfigurationBase GetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects)
		{
			string text = JsonHelpers.SerializeJson(BasicRestOAuthSyncProxy.GetJsonKnownTypes(), typeof(SyncCallerContext), callerContext);
			text = "{\"callerContext\":{" + text + "}";
			string urlSuffix = string.Format("/GetObject?tenantId={0}&objectType={1}&objectId={2}&includeDeletedObjects={3}", new object[]
			{
				tenantId,
				objectType,
				objectId,
				includeDeletedObjects
			});
			NetHelpersWebResponse netHelpersWebResponse = this.DoPolicySyncRequest(urlSuffix, tenantId.ToString(), text);
			this.MostRecentWebResponse = netHelpersWebResponse;
			if (netHelpersWebResponse == null)
			{
				return null;
			}
			Type typeFromHandle;
			switch (objectType)
			{
			case ConfigurationObjectType.Policy:
				typeFromHandle = typeof(PolicyConfiguration);
				break;
			case ConfigurationObjectType.Rule:
				typeFromHandle = typeof(RuleConfiguration);
				break;
			case ConfigurationObjectType.Association:
				typeFromHandle = typeof(AssociationConfiguration);
				break;
			case ConfigurationObjectType.Binding:
				typeFromHandle = typeof(BindingConfiguration);
				break;
			case ConfigurationObjectType.Scope:
				typeFromHandle = typeof(ScopeConfiguration);
				break;
			default:
				throw new NotImplementedException("Ahhh!");
			}
			string text2 = netHelpersWebResponse.Content.Replace("{\"GetObjectResult\":", string.Empty);
			text2 = text2.Substring(0, text2.Length - 1);
			this.logger.LogVerbose(string.Format("Trimmed response: '{0}'", text2));
			PolicyConfigurationBase result;
			try
			{
				PolicyConfigurationBase policyConfigurationBase = (PolicyConfigurationBase)JsonHelpers.DeserializeJson(BasicRestOAuthSyncProxy.GetJsonKnownTypes(), typeFromHandle, text2);
				result = policyConfigurationBase;
			}
			catch (SerializationException ex)
			{
				this.logger.LogError(string.Format("Received exception: '{0}'", ex.Message));
				this.logger.LogError(string.Format("netResponse.Content: '{0}'", netHelpersWebResponse.Content));
				result = null;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A858 File Offset: 0x00008A58
		public IAsyncResult BeginGetChanges(GetChangesRequest request, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A864 File Offset: 0x00008A64
		public PolicyChangeBatch EndGetChanges(IAsyncResult asyncResult)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A870 File Offset: 0x00008A70
		public PolicyChange GetSingleTenantChanges(TenantCookie tenantCookie)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A87C File Offset: 0x00008A7C
		public IAsyncResult BeginGetSingleTenantChanges(TenantCookie tenantCookie, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A888 File Offset: 0x00008A88
		public PolicyChange EndGetSingleTenantChanges(IAsyncResult asyncResult)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A894 File Offset: 0x00008A94
		public IAsyncResult BeginGetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public PolicyConfigurationBase EndGetObject(IAsyncResult asyncResult)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public IAsyncResult BeginPublishStatus(PublishStatusRequest request, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void EndPublishStatus(IAsyncResult asyncResult)
		{
			throw new NotImplementedException("Not implemented.");
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		private NetHelpersWebResponse DoPolicySyncRequest(string urlSuffix, string tenantId, string requestBody)
		{
			OAuthTokenRequest oauthTokenRequest = OAuthHelpers.RequestTokenFromAcs(this.logger, this.acsUrl, this.acsTenantCertificate, new Uri(this.wsUrlBase).Host, this.acsResourcePartnerId, tenantId);
			if (string.IsNullOrEmpty(oauthTokenRequest.AcsTokenResultString))
			{
				this.logger.LogError("Null token from Acs");
				this.logger.LogError(string.Format("TenantId: '{0}'", tenantId));
				this.logger.LogError(string.Format("Audience: '{0}'", oauthTokenRequest.Audience));
				this.logger.LogError(string.Format("Issuer: '{0}'", oauthTokenRequest.Issuer));
				this.logger.LogError(string.Format("Resource: '{0}'", oauthTokenRequest.Resource));
				this.logger.LogError(string.Format("AcsUrl: '{0}'", oauthTokenRequest.AcsUrl));
				this.logger.LogError(string.Format("acsTenantCertificate.Subject: '{0}'", this.acsTenantCertificate.Subject));
				this.logger.LogError(string.Format("NetResponse.StatusCode: '{0}'", oauthTokenRequest.AcsNetResponse.WebResponse.StatusCode));
				this.logger.LogError(string.Format("jwtToken: '{0}'", oauthTokenRequest.JwtInputToken.ToString()));
				return null;
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(this.wsUrlBase + urlSuffix));
			httpWebRequest.Accept = "application/json";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers[HttpRequestHeader.Authorization] = string.Format("Bearer {0}", oauthTokenRequest.AcsTokenResultString);
			NetHelpersWebResponse netHelpersWebResponse = NetHelpers.DoWebRequest(this.logger, httpWebRequest, requestBody);
			if (netHelpersWebResponse.WebResponse.StatusCode != HttpStatusCode.OK)
			{
				this.logger.LogError("Status code from Policy Sync web request was not Ok");
				this.logger.LogError(string.Format("TenantId: '{0}'", tenantId));
				this.logger.LogError(string.Format("Audience: '{0}'", oauthTokenRequest.Audience));
				this.logger.LogError(string.Format("Issuer: '{0}'", oauthTokenRequest.Issuer));
				this.logger.LogError(string.Format("Resource: '{0}'", oauthTokenRequest.Resource));
				this.logger.LogError(string.Format("AcsUrl: '{0}'", oauthTokenRequest.AcsUrl));
				this.logger.LogError(string.Format("AcsTokenResultString: '{0}'", oauthTokenRequest.AcsTokenResultString));
				this.logger.LogError(string.Format("acsTenantCertificate.Subject: '{0}'", this.acsTenantCertificate.Subject));
				this.logger.LogError(string.Format("NetResponse.StatusCode: '{0}'", netHelpersWebResponse.WebResponse.StatusCode));
				this.logger.LogError(string.Format("jwtToken: '{0}'", oauthTokenRequest.JwtInputToken.ToString()));
			}
			return netHelpersWebResponse;
		}

		// Token: 0x040000DD RID: 221
		private IHygieneLogger logger = new NullHygieneLogger();

		// Token: 0x040000DE RID: 222
		private string wsUrlBase;

		// Token: 0x040000DF RID: 223
		private X509Certificate2 acsTenantCertificate;

		// Token: 0x040000E0 RID: 224
		private string acsUrl;

		// Token: 0x040000E1 RID: 225
		private string acsResourcePartnerId;
	}
}

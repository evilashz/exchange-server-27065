using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000135 RID: 309
	internal class UnifiedPolicyConfiguration
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00030CBC File Offset: 0x0002EEBC
		protected UnifiedPolicyConfiguration()
		{
			this.configurationFromFile = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00030CDC File Offset: 0x0002EEDC
		public static UnifiedPolicyConfiguration GetInstance()
		{
			if (UnifiedPolicyConfiguration.instance == null)
			{
				lock (UnifiedPolicyConfiguration.lockObject)
				{
					if (UnifiedPolicyConfiguration.instance == null)
					{
						if (ExPolicyConfigProvider.IsFFOOnline)
						{
							Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.ManagementHelper");
							Type type = assembly.GetType("Microsoft.Exchange.Hygiene.ManagementHelper.UnifiedPolicyConfigurationHelper");
							UnifiedPolicyConfiguration.instance = (UnifiedPolicyConfiguration)Activator.CreateInstance(type);
						}
						else
						{
							UnifiedPolicyConfiguration.instance = new UnifiedPolicyConfiguration();
						}
					}
				}
			}
			return UnifiedPolicyConfiguration.instance;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00030D64 File Offset: 0x0002EF64
		public static void ForceRecreationForTest()
		{
			lock (UnifiedPolicyConfiguration.lockObject)
			{
				UnifiedPolicyConfiguration.instance = null;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00030DA4 File Offset: 0x0002EFA4
		public Uri GetExoPswsHostUrl()
		{
			Uri uri = this.GetUrlFromConfigFile("UP_PswsHostUrl");
			if (uri == null)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 160, "GetExoPswsHostUrl", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\UnifiedPolicyConfiguration.cs");
				ServiceEndpoint endpoint = topologyConfigurationSession.GetEndpointContainer().GetEndpoint(ServiceEndpointId.ExchangeLoginUrl);
				uri = endpoint.Uri;
			}
			return uri;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00030E00 File Offset: 0x0002F000
		public Uri GetSyncSvrBaseUrl()
		{
			Uri urlFromConfigFile = this.GetUrlFromConfigFile("UP_SyncSvcUrl");
			if (urlFromConfigFile == null)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 183, "GetSyncSvrBaseUrl", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\UnifiedPolicyConfiguration.cs");
				ServiceEndpoint endpoint = topologyConfigurationSession.GetEndpointContainer().GetEndpoint("FfoDataService");
				return new Uri(string.Format("{0}://{1}:{2}/PolicySync/PolicySync.svc", endpoint.Uri.Scheme, endpoint.Uri.Host, endpoint.Uri.Port));
			}
			return urlFromConfigFile;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00030E8C File Offset: 0x0002F08C
		public virtual void GetTenantSharePointUrls(IConfigurationSession configurationSession, out Uri spRootUrl, out Uri spAdminUrl)
		{
			ArgumentValidator.ThrowIfNull("configurationSession", configurationSession);
			spRootUrl = null;
			spAdminUrl = null;
			string organizationIdKey = this.GetOrganizationIdKey(configurationSession);
			if (!string.IsNullOrEmpty(organizationIdKey))
			{
				spRootUrl = this.GetUrlFromConfigFile("UP_SPRootSite_" + organizationIdKey);
				spAdminUrl = this.GetUrlFromConfigFile("UP_SPAdminSite_" + organizationIdKey);
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00030EE0 File Offset: 0x0002F0E0
		public virtual Workload GetTenantSupportedWorkload(IConfigurationSession configurationSession)
		{
			Workload result = Workload.None;
			string organizationIdKey = this.GetOrganizationIdKey(configurationSession);
			string stringFromConfigFile = this.GetStringFromConfigFile("UP_TenantWorkloads_" + organizationIdKey);
			if (!string.IsNullOrEmpty(stringFromConfigFile))
			{
				Enum.TryParse<Workload>(stringFromConfigFile, out result);
			}
			return result;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00030F1C File Offset: 0x0002F11C
		public ICredentials GetCredentials(IConfigurationSession configurationSession, ADUser actAsUser = null)
		{
			ArgumentValidator.ThrowIfNull("configurationSession", configurationSession);
			ICredentials credentials = null;
			string organizationIdKey = this.GetOrganizationIdKey(configurationSession);
			if (!string.IsNullOrEmpty(organizationIdKey))
			{
				string stringFromConfigFile = this.GetStringFromConfigFile("UP_TenantAdminCred_" + organizationIdKey);
				int num = stringFromConfigFile.IndexOf(':');
				if (num > 0)
				{
					credentials = new NetworkCredential(stringFromConfigFile.Substring(0, num), stringFromConfigFile.Substring(num + 1));
				}
			}
			if (credentials == null)
			{
				Organization orgContainer = configurationSession.GetOrgContainer();
				if (orgContainer != null && orgContainer.OrganizationId != null)
				{
					if (actAsUser != null)
					{
						credentials = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(orgContainer.OrganizationId, actAsUser, null);
					}
					else
					{
						credentials = OAuthCredentials.GetOAuthCredentialsForAppToken(orgContainer.OrganizationId, "PlaceHolder");
					}
				}
			}
			return credentials;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00030FC1 File Offset: 0x0002F1C1
		public virtual IEnumerable<string> GetUnifiedPolicyPreReqState(IConfigurationSession configurationSession)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00030FC8 File Offset: 0x0002F1C8
		public virtual void SetUnifiedPolicyPreReqState(IConfigurationSession configurationSession, IEnumerable<string> prerequisiteList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		public TimeSpan GetPolicyPendingStatusTimeout()
		{
			string stringFromConfigFile = this.GetStringFromConfigFile("UP_PendingStatusTimeoutInSeconds");
			int num;
			if (int.TryParse(stringFromConfigFile, out num) && num > 0)
			{
				return TimeSpan.FromSeconds((double)num);
			}
			return TimeSpan.FromHours(1.0);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00031010 File Offset: 0x0002F210
		public virtual string GetIntuneResourceUrl(IConfigurationSession configurationSession)
		{
			string stringFromConfigFile = this.GetStringFromConfigFile("IntuneResourceURL");
			if (!string.IsNullOrEmpty(stringFromConfigFile))
			{
				return stringFromConfigFile;
			}
			return null;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00031034 File Offset: 0x0002F234
		public virtual string GetIntuneEndpointUrl(IConfigurationSession configurationSession)
		{
			string stringFromConfigFile = this.GetStringFromConfigFile("IntuneEndPointURL");
			if (!string.IsNullOrEmpty(stringFromConfigFile))
			{
				return stringFromConfigFile;
			}
			return null;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00031058 File Offset: 0x0002F258
		public string GetOrganizationIdKey(IConfigurationSession configurationSession)
		{
			string result = string.Empty;
			Organization orgContainer = configurationSession.GetOrgContainer();
			if (orgContainer != null && orgContainer.OrganizationId != null)
			{
				result = orgContainer.OrganizationId.ToExternalDirectoryOrganizationId();
			}
			return result;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00031090 File Offset: 0x0002F290
		private Uri GetUrlFromConfigFile(string key)
		{
			Uri result = null;
			string stringFromConfigFile = this.GetStringFromConfigFile(key);
			if (!string.IsNullOrEmpty(stringFromConfigFile))
			{
				Uri.TryCreate(stringFromConfigFile, UriKind.Absolute, out result);
			}
			return result;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000310BC File Offset: 0x0002F2BC
		private string GetStringFromConfigFile(string key)
		{
			string result = string.Empty;
			KeyValueConfigurationElement keyValueConfigurationElement = this.configurationFromFile.AppSettings.Settings[key];
			if (keyValueConfigurationElement != null && !string.IsNullOrEmpty(keyValueConfigurationElement.Value))
			{
				result = keyValueConfigurationElement.Value;
			}
			return result;
		}

		// Token: 0x04000451 RID: 1105
		private const string SyncSvcUrlKey = "UP_SyncSvcUrl";

		// Token: 0x04000452 RID: 1106
		private const string ExoPswsHostUrlKey = "UP_PswsHostUrl";

		// Token: 0x04000453 RID: 1107
		private const string PendingStatusTimeoutInSecondsKey = "UP_PendingStatusTimeoutInSeconds";

		// Token: 0x04000454 RID: 1108
		private const string IntuneResourceURL = "IntuneResourceURL";

		// Token: 0x04000455 RID: 1109
		private const string IntuneEndPointURL = "IntuneEndPointURL";

		// Token: 0x04000456 RID: 1110
		private const string SPRootSiteKeyPrefix = "UP_SPRootSite_";

		// Token: 0x04000457 RID: 1111
		private const string SPAdminSiteKeyPrefix = "UP_SPAdminSite_";

		// Token: 0x04000458 RID: 1112
		private const string TenantAdminCredKeyPrefix = "UP_TenantAdminCred_";

		// Token: 0x04000459 RID: 1113
		private const string TenantSupportedWorkloadKeyPrefix = "UP_TenantWorkloads_";

		// Token: 0x0400045A RID: 1114
		private static UnifiedPolicyConfiguration instance;

		// Token: 0x0400045B RID: 1115
		private static object lockObject = new object();

		// Token: 0x0400045C RID: 1116
		private Configuration configurationFromFile;
	}
}

using System;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AADClientFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static bool IsAADEnabled()
		{
			bool result;
			try
			{
				result = (AADClientFactory.graphUrl.Value != null);
			}
			catch (LocalizedException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002108 File Offset: 0x00000308
		public static AADClient Create(ADUser user)
		{
			if (AADClientFactory.graphUrl.Value == null)
			{
				AADClientFactory.Tracer.TraceDebug(0L, "No GraphURL available, cannot create AADClient");
				return null;
			}
			return new AADClient(AADClientFactory.graphUrl.Value, AADClientFactory.GetTenantContextId(user.OrganizationId), AADClientFactory.GetActAsUserCredentials(user), GraphProxyVersions.Version14);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002158 File Offset: 0x00000358
		public static IAadClient CreateIAadClient(ADUser user)
		{
			if (AADClientTestHooks.GraphApi_GetAadClient != null)
			{
				return AADClientTestHooks.GraphApi_GetAadClient();
			}
			if (AADClientFactory.graphUrl.Value == null)
			{
				AADClientFactory.Tracer.TraceDebug(0L, "No GraphURL available, cannot create AADClient");
				return null;
			}
			return new AADClient(AADClientFactory.graphUrl.Value, AADClientFactory.GetTenantContextId(user.OrganizationId), AADClientFactory.GetActAsUserCredentials(user), GraphProxyVersions.Version14);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021B7 File Offset: 0x000003B7
		public static AADClient Create(OrganizationId organizationId, GraphProxyVersions apiVersion = GraphProxyVersions.Version14)
		{
			if (AADClientFactory.graphUrl.Value == null)
			{
				AADClientFactory.Tracer.TraceDebug(0L, "No GraphURL available, cannot create AADClient");
				return null;
			}
			return new AADClient(AADClientFactory.graphUrl.Value, AADClientFactory.GetTenantContextId(organizationId), AADClientFactory.GetAppCredentials(organizationId), apiVersion);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021F4 File Offset: 0x000003F4
		public static AADClient Create(string smtpAddress, GraphProxyVersions apiVersion = GraphProxyVersions.Version14)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("smtpAddress", smtpAddress);
			SmtpAddress smtpAddress2 = new SmtpAddress(smtpAddress);
			if (!smtpAddress2.IsValidAddress)
			{
				AADClientFactory.Tracer.TraceDebug<string>(0L, "SMTP address {0} is not valid, cannot create AADClient", smtpAddress);
				return null;
			}
			OrganizationId organizationId = OrganizationId.FromAcceptedDomain(smtpAddress2.Domain);
			return AADClientFactory.Create(organizationId, apiVersion);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002248 File Offset: 0x00000448
		private static string GetTenantContextId(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return Guid.Empty.ToString();
			}
			return new Guid(organizationId.ToExternalDirectoryOrganizationId()).ToString();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002290 File Offset: 0x00000490
		private static string GetGraphUrl()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 168, "GetGraphUrl", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\Common\\AADClientFactory.cs");
			ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
			ServiceEndpoint endpoint;
			try
			{
				endpoint = endpointContainer.GetEndpoint(ServiceEndpointId.AADGraphAPI);
			}
			catch (ServiceEndpointNotFoundException)
			{
				AADClientFactory.Tracer.TraceDebug(0L, "Unable to get the URL for the Graph API because the service endpoint was not found");
				return null;
			}
			if (endpoint.Uri == null)
			{
				AADClientFactory.Tracer.TraceError(0L, "ServiceEndpoint for Graph API was found but the URL is not present");
				throw new ServiceEndpointNotFoundException(ServiceEndpointId.AADGraphAPI);
			}
			string text = endpoint.Uri.ToString();
			AADClientFactory.Tracer.TraceDebug<string>(0L, "Retrieved GraphURL from ServiceEndpoint: {0}", text);
			return text;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002348 File Offset: 0x00000548
		private static ICredentials GetAppCredentials(OrganizationId organizationId)
		{
			ICredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(organizationId, "dummyRealm");
			AADClientFactory.Tracer.TraceDebug<OrganizationId, ICredentials>(0L, "Created app credentials for {0}: {1}", organizationId, oauthCredentialsForAppToken);
			return oauthCredentialsForAppToken;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002378 File Offset: 0x00000578
		private static ICredentials GetActAsUserCredentials(ADUser user)
		{
			ICredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(user.OrganizationId, user, null);
			AADClientFactory.Tracer.TraceDebug<string, ICredentials>(0L, "Created user credentials for {0}: {1}", user.UserPrincipalName, oauthCredentialsForAppActAsToken);
			return oauthCredentialsForAppActAsToken;
		}

		// Token: 0x04000001 RID: 1
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x04000002 RID: 2
		private static Lazy<string> graphUrl = new Lazy<string>(new Func<string>(AADClientFactory.GetGraphUrl), LazyThreadSafetyMode.PublicationOnly);
	}
}

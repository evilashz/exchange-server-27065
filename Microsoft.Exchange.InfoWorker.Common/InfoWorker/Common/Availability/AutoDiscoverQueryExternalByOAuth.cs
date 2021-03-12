using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000068 RID: 104
	internal sealed class AutoDiscoverQueryExternalByOAuth : AutoDiscoverQuery
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public AutoDiscoverQueryExternalByOAuth(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri autoDiscoverUrl, ProxyAuthenticator proxyAuthenticator, AutoDiscoverQueryItem[] queryItems, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest, QueryList queryList) : this(application, clientContext, requestLogger, autoDiscoverUrl, new AutoDiscoverAuthenticator(proxyAuthenticator), queryItems, 0, createAutoDiscoverRequest, queryList)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		private AutoDiscoverQueryExternalByOAuth(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri autoDiscoverUrl, AutoDiscoverAuthenticator authenticator, AutoDiscoverQueryItem[] queryItems, int redirectionDepth, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest, QueryList queryList) : base(application, clientContext, requestLogger, autoDiscoverUrl, authenticator, queryItems, redirectionDepth, createAutoDiscoverRequest, AutodiscoverType.External, queryList)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		protected override AutoDiscoverQuery CreateAutoDiscoverQuery(Uri autoDiscoverUrl, AutoDiscoverQueryItem[] queryItems, int redirectionDepth)
		{
			QueryList queryListFromQueryItems = base.GetQueryListFromQueryItems(queryItems);
			return new AutoDiscoverQueryExternalByOAuth(base.Application, base.ClientContext, base.RequestLogger, autoDiscoverUrl, base.Authenticator, queryItems, redirectionDepth, base.CreateAutoDiscoverRequest, queryListFromQueryItems);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000CA04 File Offset: 0x0000AC04
		protected override AutoDiscoverQuery CreateAutoDiscoverQuery(string domain, AutoDiscoverQueryItem[] queryItems, int redirectionDepth)
		{
			AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Search for OrganizationRelationship for domain {1}", TraceContext.Get(), domain);
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(base.ClientContext.OrganizationId);
			IntraOrganizationConnector intraOrganizationConnector = organizationIdCacheValue.GetIntraOrganizationConnector(domain);
			if (intraOrganizationConnector == null)
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: IntraOrganizationConnector lookup for domain {1} found nothing", TraceContext.Get(), domain);
				throw new AutoDiscoverFailedException(Strings.descConfigurationInformationNotFound(domain), 58684U);
			}
			if (intraOrganizationConnector.DiscoveryEndpoint == null)
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, string, ADObjectId>((long)this.GetHashCode(), "{0}: IntraOrganizationConnector lookup for domain {1} found {2}, but it doesn't have DiscoveryEndpoint set", TraceContext.Get(), domain, intraOrganizationConnector.Id);
				throw new AutoDiscoverFailedException(Strings.descMisconfiguredIntraOrganizationConnector(intraOrganizationConnector.Id.ToString()), 34108U);
			}
			AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string, ADObjectId>((long)this.GetHashCode(), "{0}: IntraOrganizationConnector lookup for domain {1} found {2}", TraceContext.Get(), domain, intraOrganizationConnector.Id);
			QueryList queryListFromQueryItems = base.GetQueryListFromQueryItems(queryItems);
			return new AutoDiscoverQueryExternalByOAuth(base.Application, base.ClientContext, base.RequestLogger, intraOrganizationConnector.DiscoveryEndpoint, base.Authenticator, queryItems, redirectionDepth, base.CreateAutoDiscoverRequest, queryListFromQueryItems);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		protected override void SetResult(AutoDiscoverQueryItem queryItem, WebServiceUri webServiceUri)
		{
			WebServiceUri webServiceUri2 = new WebServiceUri(webServiceUri, null, queryItem.EmailAddress);
			base.AddWebServiceUriToCache(queryItem, webServiceUri2);
			AutoDiscoverResult result;
			if (!base.Application.IsVersionSupportedForExternalUser(webServiceUri.ServerVersion))
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, int, Type>((long)this.GetHashCode(), "{0}: Remote server version {1} is considered a legacy server by {2} application for external user.", TraceContext.Get(), webServiceUri.ServerVersion, base.Application.GetType());
				result = new AutoDiscoverResult(base.Application.CreateExceptionForUnsupportedVersion(queryItem.RecipientData, webServiceUri.ServerVersion));
			}
			else
			{
				result = new AutoDiscoverResult(webServiceUri2, base.Authenticator.ProxyAuthenticator);
			}
			queryItem.SetResult(result);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		protected override void SetResult(AutoDiscoverQueryItem queryItem, LocalizedString exceptionString, uint locationIdentifier)
		{
			WebServiceUri webServiceUri = new WebServiceUri(null, exceptionString, queryItem.EmailAddress);
			base.AddWebServiceUriToCache(queryItem, webServiceUri);
			queryItem.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(exceptionString, locationIdentifier)));
		}
	}
}

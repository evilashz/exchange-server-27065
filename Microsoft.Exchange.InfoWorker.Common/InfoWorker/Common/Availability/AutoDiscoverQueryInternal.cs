using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000069 RID: 105
	internal sealed class AutoDiscoverQueryInternal : AutoDiscoverQuery
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public AutoDiscoverQueryInternal(Application application, ClientContext clientContext, RequestLogger requestLogger, TargetForestConfiguration targetForestConfiguration, AutoDiscoverQueryItem[] queryItems, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest, QueryList queryList) : this(application, clientContext, requestLogger, targetForestConfiguration.AutoDiscoverUrl, new AutoDiscoverAuthenticator(targetForestConfiguration.GetCredentialCache(targetForestConfiguration.AutoDiscoverUrl), targetForestConfiguration.Credentials), queryItems, 0, createAutoDiscoverRequest, targetForestConfiguration, queryList)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CC30 File Offset: 0x0000AE30
		private AutoDiscoverQueryInternal(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri autoDiscoverUrl, AutoDiscoverAuthenticator authenticator, AutoDiscoverQueryItem[] queryItems, int redirectionDepth, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest, TargetForestConfiguration targetForestConfiguration, QueryList queryList) : base(application, clientContext, requestLogger, autoDiscoverUrl, authenticator, queryItems, redirectionDepth, createAutoDiscoverRequest, AutodiscoverType.Internal, queryList)
		{
			this.targetForestConfiguration = targetForestConfiguration;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		protected override AutoDiscoverQuery CreateAutoDiscoverQuery(Uri autoDiscoverUrl, AutoDiscoverQueryItem[] queryItems, int redirectionDepth)
		{
			QueryList queryListFromQueryItems = base.GetQueryListFromQueryItems(queryItems);
			return new AutoDiscoverQueryInternal(base.Application, base.ClientContext, base.RequestLogger, autoDiscoverUrl, new AutoDiscoverAuthenticator(this.targetForestConfiguration.GetCredentialCache(autoDiscoverUrl), this.targetForestConfiguration.Credentials), queryItems, redirectionDepth, base.CreateAutoDiscoverRequest, this.targetForestConfiguration, queryListFromQueryItems);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		protected override AutoDiscoverQuery CreateAutoDiscoverQuery(string domain, AutoDiscoverQueryItem[] queryItems, int redirectionDepth)
		{
			AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Search for TargetForestConfiguration for domain {1}", TraceContext.Get(), domain);
			TargetForestConfiguration targetForestConfiguration = TargetForestConfigurationCache.FindByDomain(base.ClientContext.OrganizationId, domain);
			if (targetForestConfiguration.Exception != null)
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, string, LocalizedException>((long)this.GetHashCode(), "{0}: Found TargetForestConfiguration lookup for domain {1}, but it is in failed state due exception: {2}", TraceContext.Get(), domain, targetForestConfiguration.Exception);
				throw targetForestConfiguration.Exception;
			}
			QueryList queryListFromQueryItems = base.GetQueryListFromQueryItems(queryItems);
			return new AutoDiscoverQueryInternal(base.Application, base.ClientContext, base.RequestLogger, targetForestConfiguration.AutoDiscoverUrl, new AutoDiscoverAuthenticator(targetForestConfiguration.GetCredentialCache(targetForestConfiguration.AutoDiscoverUrl), targetForestConfiguration.Credentials), queryItems, redirectionDepth, base.CreateAutoDiscoverRequest, targetForestConfiguration, queryListFromQueryItems);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CD68 File Offset: 0x0000AF68
		protected override void SetResult(AutoDiscoverQueryItem queryItem, WebServiceUri webServiceUri)
		{
			WebServiceUri webServiceUri2 = new WebServiceUri(webServiceUri, this.targetForestConfiguration.Credentials, queryItem.EmailAddress);
			base.AddWebServiceUriToCache(queryItem, webServiceUri2);
			queryItem.SetResult(this.GetResult(queryItem.RecipientData, webServiceUri2));
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		protected override void SetResult(AutoDiscoverQueryItem queryItem, LocalizedString exceptionString, uint locationIdentifier)
		{
			WebServiceUri webServiceUri = new WebServiceUri(this.targetForestConfiguration.Credentials, exceptionString, queryItem.EmailAddress);
			base.AddWebServiceUriToCache(queryItem, webServiceUri);
			queryItem.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(exceptionString, locationIdentifier)));
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		private AutoDiscoverResult GetResult(RecipientData recipientData, WebServiceUri webServiceUri)
		{
			if (!base.Application.IsVersionSupported(webServiceUri.ServerVersion))
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, int, Type>((long)this.GetHashCode(), "{0}: Remote server version {1} is considered a legacy server by {2} application.", TraceContext.Get(), webServiceUri.ServerVersion, base.Application.GetType());
				return new AutoDiscoverResult(base.Application.CreateExceptionForUnsupportedVersion(recipientData, webServiceUri.ServerVersion));
			}
			AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, Uri, EmailAddress>((long)this.GetHashCode(), "{0}: Found availability service {1} that can fill request for mailbox {2}", TraceContext.Get(), webServiceUri.Uri, (recipientData != null) ? recipientData.EmailAddress : null);
			SerializedSecurityContext serializedSecurityContext = null;
			InternalClientContext internalClientContext = base.ClientContext as InternalClientContext;
			if (this.targetForestConfiguration.IsPerUserAuthorizationSupported && internalClientContext != null)
			{
				serializedSecurityContext = internalClientContext.SerializedSecurityContext;
			}
			ProxyAuthenticator proxyAuthenticatorForAutoDiscover = this.targetForestConfiguration.GetProxyAuthenticatorForAutoDiscover(webServiceUri.Uri, serializedSecurityContext, base.ClientContext.MessageId);
			return new AutoDiscoverResult(webServiceUri, proxyAuthenticatorForAutoDiscover);
		}

		// Token: 0x040001A4 RID: 420
		private TargetForestConfiguration targetForestConfiguration;
	}
}

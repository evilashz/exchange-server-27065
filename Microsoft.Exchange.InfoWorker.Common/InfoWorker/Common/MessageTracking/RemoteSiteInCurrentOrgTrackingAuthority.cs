using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002BE RID: 702
	internal class RemoteSiteInCurrentOrgTrackingAuthority : ADAuthenticationTrackingAuthority
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0005AC05 File Offset: 0x00058E05
		public ADObjectId SiteADObjectId
		{
			get
			{
				return this.siteADObjectId;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0005AC0D File Offset: 0x00058E0D
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0005AC18 File Offset: 0x00058E18
		public static RemoteSiteInCurrentOrgTrackingAuthority Create(ADObjectId siteADObjectId, DirectoryContext directoryContext, int minServerVersionRequested, bool useServersCache)
		{
			string text = ServerCache.Instance.GetDefaultDomain(directoryContext.OrganizationId);
			int version = 0;
			Uri uri = null;
			if (useServersCache)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(0, "Creating remote tracking authority via ServersCache for site: {0}", siteADObjectId);
				try
				{
					MiniServer anyBackEndServerFromASite = ServersCache.GetAnyBackEndServerFromASite(siteADObjectId, minServerVersionRequested, false);
					version = anyBackEndServerFromASite.VersionNumber;
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, ServerVersion>(0, "Found remote server {0} running {1}.", anyBackEndServerFromASite.Fqdn, anyBackEndServerFromASite.AdminDisplayVersion);
					if (anyBackEndServerFromASite.MajorVersion >= 15)
					{
						BackEndServer backEndServer = new BackEndServer(anyBackEndServerFromASite.Fqdn, version);
						uri = BackEndLocator.GetBackEndWebServicesUrl(backEndServer);
					}
					else
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Server found was E14, finding new tracking authority via ServiceTopology.", new object[0]);
						uri = ServerCache.Instance.GetCasServerUri(siteADObjectId, minServerVersionRequested, out version);
					}
					goto IL_118;
				}
				catch (BackEndLocatorException ex)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Failed to acquire EWS URI from BackEndLocator with exception: {0}", ex.ToString());
					TrackingFatalException.RaiseET(ErrorCode.CASUriDiscoveryFailure, siteADObjectId.ToString());
					goto IL_118;
				}
				catch (ServerHasNotBeenFoundException ex2)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Failed to locate remote backend server from ServersCache with exception: {0}", ex2.ToString());
					TrackingFatalException.RaiseET(ErrorCode.CASUriDiscoveryFailure, siteADObjectId.ToString());
					goto IL_118;
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Creating remote tracking authority via ServiceTopology.", new object[0]);
			uri = ServerCache.Instance.GetCasServerUri(siteADObjectId, minServerVersionRequested, out version);
			IL_118:
			if (null == uri)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "No suitable authority URI found.", new object[0]);
				TrackingFatalException.RaiseET(ErrorCode.CASUriDiscoveryFailure, siteADObjectId.ToString());
			}
			return new RemoteSiteInCurrentOrgTrackingAuthority(text, TrackingAuthorityKind.RemoteSiteInCurrentOrg, siteADObjectId, uri, version);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0005AD90 File Offset: 0x00058F90
		public static RemoteSiteInCurrentOrgTrackingAuthority Create(ServerInfo serverInfo, DirectoryContext directoryContext)
		{
			int version = serverInfo.AdminDisplayVersion.ToInt();
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, ServerVersion>(0, "Creating remote tracking authority via BackEndLocator for server {0} running {1}.", serverInfo.Key, serverInfo.AdminDisplayVersion);
			string text = ServerCache.Instance.GetDefaultDomain(directoryContext.OrganizationId);
			BackEndServer backEndServer = new BackEndServer(serverInfo.Key, version);
			Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(backEndServer);
			if (null == backEndWebServicesUrl)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "No suitable authority URI found.", new object[0]);
				TrackingFatalException.RaiseET(ErrorCode.CASUriDiscoveryFailure, serverInfo.ServerSiteId.ToString());
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<Uri>(0, "Using EWS URI: {0}", backEndWebServicesUrl);
			return new RemoteSiteInCurrentOrgTrackingAuthority(text, TrackingAuthorityKind.RemoteSiteInCurrentOrg, serverInfo.ServerSiteId, backEndWebServicesUrl, version);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0005AE44 File Offset: 0x00059044
		public static RemoteSiteInCurrentOrgTrackingAuthority Create(ADObjectId siteADObjectId, DirectoryContext directoryContext, ADUser user)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Creating remote tracking authority via BackEndLocator for user.", new object[0]);
			string text = ServerCache.Instance.GetDefaultDomain(directoryContext.OrganizationId);
			BackEndServer backEndServer = BackEndLocator.GetBackEndServer(user);
			Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(backEndServer);
			int version = backEndServer.Version;
			if (null == backEndWebServicesUrl)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "No suitable authority URI found.", new object[0]);
				TrackingFatalException.RaiseET(ErrorCode.CASUriDiscoveryFailure, siteADObjectId.ToString());
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<Uri>(0, "Using EWS URI: {0}", backEndWebServicesUrl);
			return new RemoteSiteInCurrentOrgTrackingAuthority(text, TrackingAuthorityKind.RemoteSiteInCurrentOrg, siteADObjectId, backEndWebServicesUrl, version);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0005AED5 File Offset: 0x000590D5
		public override bool IsAllowedScope(SearchScope scope)
		{
			return scope == SearchScope.Forest || scope == SearchScope.Organization || scope == SearchScope.World;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0005AEE5 File Offset: 0x000590E5
		public override string ToString()
		{
			return string.Format("Type=RemoteSiteInCurrentOrgTrackingAuthority,Site={0}", this.siteADObjectId);
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0005AEF7 File Offset: 0x000590F7
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Site;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0005AEFA File Offset: 0x000590FA
		public override string Domain
		{
			get
			{
				return this.defaultDomain;
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0005AF02 File Offset: 0x00059102
		private RemoteSiteInCurrentOrgTrackingAuthority(string defaultDomain, TrackingAuthorityKind responsibleTracker, ADObjectId siteADObjectId, Uri casServerUri, int serverVersion) : base(responsibleTracker, casServerUri)
		{
			this.defaultDomain = defaultDomain;
			this.siteADObjectId = siteADObjectId;
			this.serverVersion = serverVersion;
		}

		// Token: 0x04000D06 RID: 3334
		private string defaultDomain;

		// Token: 0x04000D07 RID: 3335
		private ADObjectId siteADObjectId;

		// Token: 0x04000D08 RID: 3336
		private int serverVersion;
	}
}

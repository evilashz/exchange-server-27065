using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F5 RID: 757
	internal class TrackingDiscovery
	{
		// Token: 0x06001652 RID: 5714 RVA: 0x00067E94 File Offset: 0x00066094
		public TrackingDiscovery(DirectoryContext directoryContext)
		{
			this.directoryContext = directoryContext;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00067EA4 File Offset: 0x000660A4
		public bool IsCrossForestDomain(string domain)
		{
			try
			{
				RequestLogger requestLogger = new RequestLogger();
				ConfigurationReader.Start(requestLogger);
				TargetForestConfiguration targetForestConfiguration = TargetForestConfigurationCache.FindByDomain(this.directoryContext.OrganizationId, domain);
				if (targetForestConfiguration.Exception != null)
				{
					this.directoryContext.Errors.Add(ErrorCode.UnexpectedErrorPermanent, string.Empty, string.Format("Cross-forest autodiscover failure for domain {0}", domain), targetForestConfiguration.Exception.ToString());
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, LocalizedException>(this.GetHashCode(), "forestConfig.Exception reading address-space for domain {0}, exception {1}.", domain, targetForestConfiguration.Exception);
					return false;
				}
			}
			catch (AddressSpaceNotFoundException arg)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, AddressSpaceNotFoundException>(0, "Domain {0} is not recognized as remote forest, exception {1}.", domain, arg);
				return false;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Domain {0} is recognized as remote forest.", domain);
			return true;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00067F64 File Offset: 0x00066164
		public TrackingAuthority FindUserLocation(TrackedUser user)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress>(this.GetHashCode(), "Trying to get location for user: {0}", user.SmtpAddress);
			if (!user.IsMailbox)
			{
				bool flag = false;
				TrackingAuthority trackingAuthority = this.FindLocationByDomainAndServer(user.SmtpAddress.Domain, string.Empty, user.SmtpAddress, true, out flag);
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Looked up non-mailbox user via domain part. Authority for user is {0}", Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind]);
				return trackingAuthority;
			}
			ServerInfo userServer = ServerCache.Instance.GetUserServer(user.ADUser);
			if (ServerStatus.LegacyExchangeServer == userServer.Status)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<ADUser>(this.GetHashCode(), "Legacy server found for user: {0}", user.ADUser);
				return LegacyExchangeServerTrackingAuthority.Instance;
			}
			if (ServerStatus.NotFound == userServer.Status)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADUser>(this.GetHashCode(), "Server not found for user: {0}", user.ADUser);
				return LegacyExchangeServerTrackingAuthority.Instance;
			}
			if (!userServer.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ServerInfo>(this.GetHashCode(), "Server {0} is not searchable", userServer);
				return LegacyExchangeServerTrackingAuthority.Instance;
			}
			if (userServer.ServerSiteId == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADUser>(this.GetHashCode(), "Null siteId for user: {0}", user.ADUser);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Site attribute was missing for user's {0} server {1}", new object[]
				{
					user.ADUser.Id,
					userServer.Key
				});
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MessageTracking.UseBackEndLocator.Enabled)
			{
				return this.GetAuthorityForSiteUser(userServer.ServerSiteId, user.ADUser);
			}
			return this.GetAuthorityForSite(userServer.ServerSiteId);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00068100 File Offset: 0x00066300
		public TrackingAuthority FindLocationForOrgServer(ServerInfo serverInfo)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Trying to get authority for Server: {0}", serverInfo.Key);
			if (serverInfo.ServerSiteId == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Null siteId for Server: {0}", serverInfo.Key);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Site attribute was missing for server {0}", new object[]
				{
					serverInfo.Key
				});
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MessageTracking.UseBackEndLocator.Enabled && serverInfo.AdminDisplayVersion.Major >= 15)
			{
				return this.GetAuthorityForSiteByServer(serverInfo);
			}
			return this.GetAuthorityForSite(serverInfo.ServerSiteId);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000681B4 File Offset: 0x000663B4
		public TrackingAuthority FindLocationByDomainAndServer(string domain, string serverName, SmtpAddress recipientProxy, bool allowChildDomains, out bool serverNotFound)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "Trying to locate domain\\server: {0}\\{1}", domain, serverName);
			serverNotFound = false;
			if (ServerCache.Instance.IsDomainAuthoritativeForOrganization(this.directoryContext.OrganizationId, domain))
			{
				if (!string.IsNullOrEmpty(serverName))
				{
					return this.GetAuthorityForServerInDomain(serverName, out serverNotFound);
				}
				if (ServerCache.Instance.IsRemoteTrustedOrg(this.directoryContext.OrganizationId, domain))
				{
					return RemoteTrustedOrgTrackingAuthority.Create(domain, recipientProxy);
				}
				return UndefinedTrackingAuthority.Instance;
			}
			else
			{
				if (ServerCache.Instance.IsDomainInternalRelayForOrganization(this.directoryContext.OrganizationId, domain) && !string.IsNullOrEmpty(serverName) && ServerCache.Instance.FindMailboxOrHubServer(serverName, 34UL).Status != ServerStatus.NotFound)
				{
					return this.GetAuthorityForServerInDomain(serverName, out serverNotFound);
				}
				if (ServerCache.Instance.IsRemoteTrustedOrg(this.directoryContext.OrganizationId, domain))
				{
					return RemoteTrustedOrgTrackingAuthority.Create(domain, recipientProxy);
				}
				if (this.IsCrossForestDomain(domain))
				{
					return RemoteForestTrackingAuthority.Create(domain, recipientProxy);
				}
				string defaultDomain = ServerCache.Instance.GetDefaultDomain(this.directoryContext.OrganizationId);
				if (allowChildDomains && domain.EndsWith("." + defaultDomain, StringComparison.OrdinalIgnoreCase))
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "AllowChildDomains is true, and domain is subdomain of default. Considering domain to be part of local forest", new object[0]);
					return this.GetAuthorityForServerInDomain(serverName, out serverNotFound);
				}
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No org-relationship or availability address-space for domain/server", new object[0]);
				return RemoteOrgTrackingAuthority.Instance;
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00068313 File Offset: 0x00066513
		public TrackingAuthority GetAuthorityForSite(ADObjectId siteId)
		{
			return this.GetAuthorityForSite(siteId, Globals.E14Version);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00068324 File Offset: 0x00066524
		public TrackingAuthority GetAuthorityForSite(ADObjectId siteId, int minimumCasVersionRequested)
		{
			ADObjectId localServerSiteId = ServerCache.Instance.GetLocalServerSiteId(this.directoryContext);
			if (siteId.Equals(localServerSiteId))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "Using local tracking authority for site: {0}", siteId);
				return CurrentSiteTrackingAuthority.Instance;
			}
			List<ServerInfo> casServers = ServerCache.Instance.GetCasServers(siteId);
			if (casServers == null || casServers.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "No suitable CAS servers found in site: {0}", siteId);
				return LegacyExchangeServerTrackingAuthority.Instance;
			}
			bool enabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MessageTracking.UseBackEndLocator.Enabled;
			return RemoteSiteInCurrentOrgTrackingAuthority.Create(siteId, this.directoryContext, minimumCasVersionRequested, enabled);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000683C8 File Offset: 0x000665C8
		public TrackingAuthority GetAuthorityForSiteByServer(ServerInfo serverInfo)
		{
			ADObjectId id = LocalSiteCache.LocalSite.Id;
			if (serverInfo.ServerSiteId.Equals(id))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Using local tracking authority for requested server.", new object[0]);
				return CurrentSiteTrackingAuthority.Instance;
			}
			List<ServerInfo> casServers = ServerCache.Instance.GetCasServers(serverInfo.ServerSiteId);
			if (casServers == null || casServers.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "No suitable CAS servers found in site: {0}", serverInfo.ServerSiteId);
				return LegacyExchangeServerTrackingAuthority.Instance;
			}
			return RemoteSiteInCurrentOrgTrackingAuthority.Create(serverInfo, this.directoryContext);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00068458 File Offset: 0x00066658
		public TrackingAuthority GetAuthorityForSiteUser(ADObjectId siteId, ADUser user)
		{
			ADObjectId id = LocalSiteCache.LocalSite.Id;
			if (siteId.Equals(id))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Using local tracking authority for requested user.", new object[0]);
				return CurrentSiteTrackingAuthority.Instance;
			}
			List<ServerInfo> casServers = ServerCache.Instance.GetCasServers(siteId);
			if (casServers == null || casServers.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "No suitable CAS servers found in site: {0}", siteId);
				return LegacyExchangeServerTrackingAuthority.Instance;
			}
			return RemoteSiteInCurrentOrgTrackingAuthority.Create(siteId, this.directoryContext, user);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000684D8 File Offset: 0x000666D8
		public Dictionary<ADObjectId, IList<ServerInfo>> GetUserMailboxLocationsBySite(ADUser user)
		{
			Dictionary<ADObjectId, IList<ServerInfo>> dictionary = new Dictionary<ADObjectId, IList<ServerInfo>>(5);
			ServerInfo userServer = ServerCache.Instance.GetUserServer(user);
			List<ServerInfo> dagServers = ServerCache.Instance.GetDagServers(userServer);
			foreach (ServerInfo item in dagServers)
			{
				if (item.ServerSiteId == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Skipping server: {0}; ServerSiteId is null", item.Key);
				}
				else
				{
					IList<ServerInfo> list = null;
					if (!dictionary.TryGetValue(item.ServerSiteId, out list))
					{
						list = new List<ServerInfo>(4);
						dictionary[item.ServerSiteId] = list;
					}
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Mailbox server for user (could be DAG): {0}", item.Key);
					list.Add(item);
				}
			}
			return dictionary;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000685BC File Offset: 0x000667BC
		public IList<TrackingAuthority> GetAuthoritiesByPriority(TrackedUser user)
		{
			List<TrackingAuthority> list = new List<TrackingAuthority>(5);
			if (!user.IsMailbox)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "User not a mailbox, using regular FindUserLocation", new object[0]);
				TrackingAuthority item = this.FindUserLocation(user);
				list.Add(item);
				return list;
			}
			ServerInfo userServer = ServerCache.Instance.GetUserServer(user.ADUser);
			if (!userServer.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "User server is legacy, using regular FindUserLocation", new object[0]);
				TrackingAuthority item2 = this.FindUserLocation(user);
				list.Add(item2);
				return list;
			}
			ADObjectId serverSiteId = userServer.ServerSiteId;
			Dictionary<ADObjectId, IList<ServerInfo>> userMailboxLocationsBySite = this.GetUserMailboxLocationsBySite(user.ADUser);
			IList<ServerInfo> list2 = null;
			if (!userMailboxLocationsBySite.TryGetValue(serverSiteId, out list2))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<SmtpAddress>(this.GetHashCode(), "Unexpected: no authorities for user: {0}", user.SmtpAddress);
				return list;
			}
			int num = user.IsArbitrationMailbox ? Constants.E14SP1ModerationReferralSupportVersion : Globals.E14Version;
			TrackingAuthority trackingAuthority;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MessageTracking.UseBackEndLocator.Enabled)
			{
				trackingAuthority = this.GetAuthorityForSiteUser(serverSiteId, user.ADUser);
			}
			else
			{
				trackingAuthority = this.GetAuthorityForSite(serverSiteId, num);
			}
			if (trackingAuthority == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId, int>(this.GetHashCode(), "No authority found for primary mailbox site {0} with minVersion {1}", serverSiteId, num);
			}
			else
			{
				list.Add(trackingAuthority);
			}
			userMailboxLocationsBySite.Remove(serverSiteId);
			foreach (KeyValuePair<ADObjectId, IList<ServerInfo>> keyValuePair in userMailboxLocationsBySite)
			{
				trackingAuthority = this.GetAuthorityForSite(keyValuePair.Key, num);
				if (trackingAuthority == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId, int>(this.GetHashCode(), "No authority found for DAG mailbox site {0} with minVersion {1}", serverSiteId, num);
					break;
				}
				list.Add(trackingAuthority);
			}
			return list;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0006878C File Offset: 0x0006698C
		private TrackingAuthority GetAuthorityForServerInDomain(string serverName, out bool serverNotFound)
		{
			ServerInfo serverInfo = ServerInfo.NotFound;
			serverNotFound = false;
			if (!string.IsNullOrEmpty(serverName))
			{
				serverInfo = ServerCache.Instance.FindMailboxOrHubServer(serverName, 34UL);
			}
			if (serverInfo.Status == ServerStatus.NotFound)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Could not find server: {0}", serverName);
				serverNotFound = true;
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Server {0} not found", new object[]
				{
					serverName
				});
			}
			if (serverInfo.Status == ServerStatus.Searchable)
			{
				return this.FindLocationForOrgServer(serverInfo);
			}
			return null;
		}

		// Token: 0x04000E76 RID: 3702
		private DirectoryContext directoryContext;

		// Token: 0x04000E77 RID: 3703
		private static readonly Random random = new Random();
	}
}

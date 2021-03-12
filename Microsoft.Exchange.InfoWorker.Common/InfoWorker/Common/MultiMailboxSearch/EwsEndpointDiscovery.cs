using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E4 RID: 484
	internal class EwsEndpointDiscovery : IEwsEndpointDiscovery
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x000359C7 File Offset: 0x00033BC7
		public EwsEndpointDiscovery(List<MailboxInfo> mailboxes, OrganizationId orgId, CallerInfo callerInfo)
		{
			this.mailboxes = mailboxes;
			this.orgId = orgId;
			this.mailboxGroups = new Dictionary<GroupId, List<MailboxInfo>>(5);
			this.callerInfo = callerInfo;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000359F0 File Offset: 0x00033BF0
		protected List<MailboxInfo> Mailboxes
		{
			get
			{
				return this.mailboxes;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x000359F8 File Offset: 0x00033BF8
		protected Dictionary<GroupId, List<MailboxInfo>> MailboxGroups
		{
			get
			{
				return this.mailboxGroups;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00035A00 File Offset: 0x00033C00
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x00035A08 File Offset: 0x00033C08
		protected Func<OrganizationId, OrganizationIdCacheValue> GetOrgIdCacheValue { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00035A11 File Offset: 0x00033C11
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x00035A19 File Offset: 0x00033C19
		protected Func<OrganizationIdCacheValue, string, IntraOrganizationConnector> GetIntraOrganizationConnector { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00035A22 File Offset: 0x00033C22
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00035A2A File Offset: 0x00033C2A
		protected Func<OrganizationIdCacheValue, string, OrganizationRelationship> GetOrganizationRelationShip { get; set; }

		// Token: 0x06000C85 RID: 3205 RVA: 0x00035A48 File Offset: 0x00033C48
		public Dictionary<GroupId, List<MailboxInfo>> FindEwsEndpoints(out long localDiscoverTime, out long autoDiscoverTime)
		{
			Stopwatch stopwatch = new Stopwatch();
			localDiscoverTime = 0L;
			autoDiscoverTime = 0L;
			stopwatch.Start();
			List<MailboxInfo> list;
			string text;
			this.FilterLocalForestMailboxes(out list, out text);
			stopwatch.Stop();
			localDiscoverTime = stopwatch.ElapsedMilliseconds;
			Factory.Current.MailboxGroupGeneratorTracer.TracePerformance<Guid, long>((long)this.GetHashCode(), "Correlation Id:{0}. Mapping local mailboxes to servers took {1}ms", this.callerInfo.QueryCorrelationId, localDiscoverTime);
			if (list == null || list.Count == 0)
			{
				return this.mailboxGroups;
			}
			string searchId = string.Empty;
			Match match = Regex.Match(this.callerInfo.UserAgent, "SID=([a-fA-F0-9\\-]*)");
			if (match.Success && match.Groups != null && match.Groups.Count > 1)
			{
				searchId = match.Groups[1].Value;
			}
			Uri url = null;
			EndPointDiscoveryInfo endPointDiscoveryInfo;
			bool flag = RemoteDiscoveryEndPoint.TryGetDiscoveryEndPoint(this.orgId, text, this.GetOrgIdCacheValue, this.GetIntraOrganizationConnector, this.GetOrganizationRelationShip, out url, out endPointDiscoveryInfo);
			if (endPointDiscoveryInfo != null && endPointDiscoveryInfo.Status != EndPointDiscoveryInfo.DiscoveryStatus.Success)
			{
				SearchEventLogger.Instance.LogSearchErrorEvent(searchId, endPointDiscoveryInfo.Message);
			}
			if (!flag)
			{
				Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Unable to find the discovery end point for domain {1}", this.callerInfo.QueryCorrelationId, text);
				GroupId key = new GroupId(new MultiMailboxSearchException(Strings.CouldNotFindOrgRelationship(text)));
				this.mailboxGroups.Add(key, list);
				return this.mailboxGroups;
			}
			Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, string, Uri>((long)this.GetHashCode(), "Correlation Id:{0}. EWS endpoint for domain {1} is {2}", this.callerInfo.QueryCorrelationId, text, EwsWsSecurityUrl.FixForAnonymous(url));
			OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(this.orgId, text);
			stopwatch.Restart();
			List<MailboxInfo> list2 = (from mailboxInfo in list
			where mailboxInfo.IsArchive
			select mailboxInfo).ToList<MailboxInfo>();
			List<MailboxInfo> list3 = (from mailboxInfo in list
			where !mailboxInfo.IsArchive
			select mailboxInfo).ToList<MailboxInfo>();
			if (list2.Count > 0)
			{
				Factory.Current.MailboxGroupGeneratorTracer.TracePerformance<Guid, int>((long)this.GetHashCode(), "Correlation Id:{0}. Mbx Count:{1}. Autodiscover started for cross premise archive mailboxes.", this.callerInfo.QueryCorrelationId, list2.Count);
				this.DoAutodiscover(list2, EwsWsSecurityUrl.FixForAnonymous(url), oauthCredentialsForAppToken);
				Factory.Current.MailboxGroupGeneratorTracer.TracePerformance<Guid, int>((long)this.GetHashCode(), "Correlation Id:{0}. Mbx Count:{1}. Autodiscover completed for cross premise archive mailboxes.", this.callerInfo.QueryCorrelationId, list2.Count);
			}
			if (list3.Count > 0)
			{
				Factory.Current.MailboxGroupGeneratorTracer.TracePerformance<Guid, int>((long)this.GetHashCode(), "Correlation Id:{0}. Mbx Count:{1}. Autodiscover started for cross premise primary mailboxes.", this.callerInfo.QueryCorrelationId, list3.Count);
				this.DoAutodiscover(list3, EwsWsSecurityUrl.FixForAnonymous(url), oauthCredentialsForAppToken);
				Factory.Current.MailboxGroupGeneratorTracer.TracePerformance<Guid, int>((long)this.GetHashCode(), "Correlation Id:{0}. Mbx Count:{1}. Autodiscover completed for cross premise primary mailboxes.", this.callerInfo.QueryCorrelationId, list3.Count);
			}
			stopwatch.Stop();
			autoDiscoverTime = stopwatch.ElapsedMilliseconds;
			Factory.Current.MailboxGroupGeneratorTracer.TracePerformance<Guid, long>((long)this.GetHashCode(), "Correlation Id:{0}. Autodiscover call took {1}ms", this.callerInfo.QueryCorrelationId, autoDiscoverTime);
			return this.mailboxGroups;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00035D60 File Offset: 0x00033F60
		private static bool ValidateCrossPremiseDomain(MailboxInfo mailbox, string domain)
		{
			return string.Equals(domain, mailbox.GetDomain(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00035D70 File Offset: 0x00033F70
		private void FilterLocalForestMailboxes(out List<MailboxInfo> crossPremiseMailboxes, out string crossPremiseDomain)
		{
			crossPremiseDomain = null;
			crossPremiseMailboxes = new List<MailboxInfo>();
			List<MailboxInfo> list = new List<MailboxInfo>(this.mailboxes.Count);
			for (int i = 0; i < this.mailboxes.Count; i++)
			{
				if (!this.mailboxes[i].IsRemoteMailbox)
				{
					list.Add(this.mailboxes[i]);
				}
				if (this.mailboxes[i].IsCrossPremiseMailbox)
				{
					if (crossPremiseDomain == null)
					{
						crossPremiseDomain = this.mailboxes[i].GetDomain();
					}
					if (!EwsEndpointDiscovery.ValidateCrossPremiseDomain(this.mailboxes[i], crossPremiseDomain))
					{
						Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Domain for mailbox {1} does not match {2}", this.callerInfo.QueryCorrelationId, this.mailboxes[i].DistinguishedName, crossPremiseDomain);
						this.AddMailboxToGroup(this.mailboxes[i], new GroupId(new Exception("The domain was invalid for this cross-premise mailbox")));
					}
					crossPremiseMailboxes.Add(this.mailboxes[i]);
				}
				if (this.mailboxes[i].IsCrossForestMailbox)
				{
					this.AddMailboxToGroup(this.mailboxes[i], new GroupId(new NotSupportedException(Strings.CrossForestNotSupported)));
				}
			}
			this.CreateGroupsForLocalMailboxes(list);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00035EC8 File Offset: 0x000340C8
		protected virtual void CreateGroupsForLocalMailboxes(List<MailboxInfo> localMailboxes)
		{
			Dictionary<Guid, BackEndServer> dictionary = new Dictionary<Guid, BackEndServer>(localMailboxes.Count);
			for (int i = 0; i < localMailboxes.Count; i++)
			{
				Guid key = (localMailboxes[i].Type == MailboxType.Primary) ? localMailboxes[i].MdbGuid : localMailboxes[i].ArchiveDatabase;
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, null);
				}
			}
			for (int j = 0; j < localMailboxes.Count; j++)
			{
				Guid guid = (localMailboxes[j].Type == MailboxType.Primary) ? localMailboxes[j].MdbGuid : localMailboxes[j].ArchiveDatabase;
				try
				{
					BackEndServer backEndServer = null;
					dictionary.TryGetValue(guid, out backEndServer);
					if (backEndServer == null)
					{
						Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, Guid>((long)this.GetHashCode(), "Correlation Id:{0}. Retrieving backend servers for database {1} and all the databases in the DAG", this.callerInfo.QueryCorrelationId, guid);
						int k = EwsEndpointDiscovery.MailboxServerLocatorRetryCount;
						while (k > 0)
						{
							using (MailboxServerLocator mailboxServerLocator = MailboxServerLocator.CreateWithResourceForestFqdn(guid, null))
							{
								Stopwatch stopwatch = new Stopwatch();
								stopwatch.Start();
								IAsyncResult asyncResult = mailboxServerLocator.BeginGetServer(null, null);
								bool flag = asyncResult.AsyncWaitHandle.WaitOne(EwsEndpointDiscovery.MailboxServerLocatorTimeout);
								if (flag)
								{
									BackEndServer value = mailboxServerLocator.EndGetServer(asyncResult);
									stopwatch.Stop();
									Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryMailboxServerLocatorTime, null, new object[]
									{
										this.callerInfo.QueryCorrelationId.ToString(),
										guid.ToString(),
										stopwatch.ElapsedMilliseconds
									});
									dictionary[guid] = value;
									foreach (KeyValuePair<Guid, BackEndServer> keyValuePair in mailboxServerLocator.AvailabilityGroupServers)
									{
										if (dictionary.ContainsKey(keyValuePair.Key))
										{
											Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, Guid, Guid>((long)this.GetHashCode(), "Correlation Id:{0}. While queried backend for {1}, also retrieved backend for {2}", this.callerInfo.QueryCorrelationId, guid, keyValuePair.Key);
											dictionary[keyValuePair.Key] = keyValuePair.Value;
										}
									}
									break;
								}
								stopwatch.Stop();
								Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryServerLocatorTimeout, null, new object[]
								{
									guid.ToString(),
									this.callerInfo.QueryCorrelationId.ToString(),
									EwsEndpointDiscovery.MailboxServerLocatorRetryCount - k + 1
								});
								k--;
							}
						}
					}
					if (dictionary[guid] != null)
					{
						this.AddMailboxToGroup(localMailboxes[j], dictionary[guid]);
					}
					else
					{
						Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, Guid, SmtpAddress>((long)this.GetHashCode(), "Correlation Id:{0}. Couldn't find the backend for database {1}. So adding an error group for mailbox {2}", this.callerInfo.QueryCorrelationId, guid, localMailboxes[j].PrimarySmtpAddress);
						this.AddMailboxToGroup(localMailboxes[j], new GroupId(new DatabaseLocationUnavailableException(Strings.DatabaseLocationUnavailable(localMailboxes[j].PrimarySmtpAddress.ToString()))));
					}
				}
				catch (MailboxServerLocatorException error)
				{
					Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, Guid>((long)this.GetHashCode(), "Correlation Id:{0}. Encountered an Exception while querying backend for database {1}", this.callerInfo.QueryCorrelationId, guid);
					this.AddMailboxToGroup(localMailboxes[j], new GroupId(error));
				}
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00036298 File Offset: 0x00034498
		private void AddMailboxToGroup(MailboxInfo mailbox, GroupId groupId)
		{
			List<MailboxInfo> list = null;
			if (!this.mailboxGroups.TryGetValue(groupId, out list))
			{
				list = new List<MailboxInfo>(1);
				this.mailboxGroups.Add(groupId, list);
			}
			list.Add(mailbox);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000362D4 File Offset: 0x000344D4
		private void AddMailboxToGroup(MailboxInfo mailbox, BackEndServer server)
		{
			Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(server);
			if (string.Equals(LocalServerCache.LocalServerFqdn, server.Fqdn, StringComparison.OrdinalIgnoreCase))
			{
				Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Mailbox {1} is a local mailbox", this.callerInfo.QueryCorrelationId, mailbox.ToString());
				this.AddMailboxToGroup(mailbox, new GroupId(GroupType.Local, backEndWebServicesUrl, LocalServerCache.LocalServer.VersionNumber, mailbox.GetDomain()));
				return;
			}
			Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Mailbox {1} is mapped to service {2}", this.callerInfo.QueryCorrelationId, mailbox.ToString(), backEndWebServicesUrl.ToString());
			this.AddMailboxToGroup(mailbox, new GroupId(GroupType.CrossServer, backEndWebServicesUrl, server.Version, mailbox.GetDomain()));
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00036394 File Offset: 0x00034594
		private void DoAutodiscover(List<MailboxInfo> crossPremiseMailboxes, Uri autodiscoverUrl, ICredentials credentials)
		{
			IAutodiscoveryClient autodiscoveryClient = null;
			try
			{
				IEnumerable<IEnumerable<MailboxInfo>> source = this.BatchData<MailboxInfo>(crossPremiseMailboxes, 90);
				int num = 0;
				foreach (IEnumerable<MailboxInfo> source2 in source.ToList<IEnumerable<MailboxInfo>>())
				{
					List<MailboxInfo> list = source2.ToList<MailboxInfo>();
					num++;
					autodiscoveryClient = Factory.Current.CreateUserSettingAutoDiscoveryClient(list, autodiscoverUrl, credentials, this.callerInfo);
					IAsyncResult asyncResult = autodiscoveryClient.BeginAutodiscover(null, null);
					if (!asyncResult.AsyncWaitHandle.WaitOne(60000))
					{
						Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, int, int>((long)this.GetHashCode(), "Correlation Id:{0}. Batch Number:{1}. Cross Premise Mailboxes Count:{2}. Autodiscover timed out.", this.callerInfo.QueryCorrelationId, num, list.Count<MailboxInfo>());
						autodiscoveryClient.CancelAutodiscover();
					}
					else
					{
						Factory.Current.MailboxGroupGeneratorTracer.TraceDebug<Guid, int, int>((long)this.GetHashCode(), "Correlation Id:{0}.  Batch Number:{1}. Cross Premise Mailboxes Count:{2}. Autodiscover succeeded. Merging results", this.callerInfo.QueryCorrelationId, num, list.Count<MailboxInfo>());
					}
					Dictionary<GroupId, List<MailboxInfo>> dictionary = autodiscoveryClient.EndAutodiscover(asyncResult);
					foreach (KeyValuePair<GroupId, List<MailboxInfo>> keyValuePair in dictionary)
					{
						List<MailboxInfo> list2;
						if (!this.mailboxGroups.TryGetValue(keyValuePair.Key, out list2))
						{
							this.mailboxGroups.Add(keyValuePair.Key, keyValuePair.Value);
						}
						else
						{
							list2.AddRange(keyValuePair.Value);
						}
					}
				}
			}
			finally
			{
				autodiscoveryClient.CancelAutodiscover();
				IDisposable disposable = autodiscoveryClient as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00036780 File Offset: 0x00034980
		private IEnumerable<IEnumerable<T>> BatchData<T>(IEnumerable<T> collection, int batchSize)
		{
			List<T> nextbatch = new List<T>(batchSize);
			foreach (T item in collection)
			{
				nextbatch.Add(item);
				if (nextbatch.Count == batchSize)
				{
					yield return nextbatch;
					nextbatch = new List<T>(batchSize);
				}
			}
			if (nextbatch.Count > 0)
			{
				yield return nextbatch;
			}
			yield break;
		}

		// Token: 0x04000902 RID: 2306
		private const int DefaultNumberOfGroups = 5;

		// Token: 0x04000903 RID: 2307
		private const int AutoDiscoverTimeout = 60000;

		// Token: 0x04000904 RID: 2308
		private static readonly TimeSpan MailboxServerLocatorTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000905 RID: 2309
		private static int MailboxServerLocatorRetryCount = 3;

		// Token: 0x04000906 RID: 2310
		private static readonly Predicate<WebServicesService> ServiceVersionFilter = (WebServicesService service) => service.ServerVersionNumber >= Server.E15MinVersion && !service.IsFrontEnd;

		// Token: 0x04000907 RID: 2311
		private readonly List<MailboxInfo> mailboxes;

		// Token: 0x04000908 RID: 2312
		private readonly OrganizationId orgId;

		// Token: 0x04000909 RID: 2313
		private Dictionary<GroupId, List<MailboxInfo>> mailboxGroups;

		// Token: 0x0400090A RID: 2314
		private readonly CallerInfo callerInfo;
	}
}

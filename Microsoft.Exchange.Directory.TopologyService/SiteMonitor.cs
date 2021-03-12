using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000014 RID: 20
	internal class SiteMonitor
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00006128 File Offset: 0x00004328
		internal SiteMonitor(TopologyDiscoveryWorkProvider workQueue)
		{
			ArgumentValidator.ThrowIfNull("workQueue", workQueue);
			this.workQueue = workQueue;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006144 File Offset: 0x00004344
		public void Start()
		{
			this.shouldScheduleWork = true;
			this.workQueue.ScheduleWork(new SiteMonitor.SiteMonitorWorkItem(), DateTime.UtcNow.Add(TimeSpan.FromMinutes(1.0)), new Action<IWorkItemResult>(this.OnWorkItemCompleted));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000618F File Offset: 0x0000438F
		public void Stop()
		{
			this.shouldScheduleWork = false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006198 File Offset: 0x00004398
		protected void OnWorkItemCompleted(IWorkItemResult result)
		{
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfTypeInvalid<WorkItemResult<object>>("result", result);
			DateTime dateTime = DateTime.UtcNow;
			switch (result.ResultType)
			{
			case ResultType.None:
			case ResultType.TimedOut:
			case ResultType.Succeeded:
				dateTime = dateTime.Add(ConfigurationData.Instance.SiteMonitorFrequency);
				break;
			case ResultType.Abandoned:
			case ResultType.Failed:
				dateTime = dateTime.Add(ConfigurationData.Instance.SiteMonitorFrequencyOnFailure);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_SITEMON_EVENT_CHECK_FAILED, "SITEMON_EVENT_CHECK_FAILED", new object[]
				{
					(result.Exception != null) ? result.Exception.Message : string.Empty
				});
				break;
			default:
				dateTime = dateTime.Add(ConfigurationData.Instance.SiteMonitorFrequency);
				break;
			}
			if (this.shouldScheduleWork)
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "Site Monitor. Next execution on {0}", dateTime);
				this.workQueue.ScheduleWork(new SiteMonitor.SiteMonitorWorkItem(), dateTime, new Action<IWorkItemResult>(this.OnWorkItemCompleted));
			}
		}

		// Token: 0x04000049 RID: 73
		private bool shouldScheduleWork;

		// Token: 0x0400004A RID: 74
		private TopologyDiscoveryWorkProvider workQueue;

		// Token: 0x02000015 RID: 21
		protected class SiteMonitorWorkItem : WorkItem<object>
		{
			// Token: 0x0600009D RID: 157 RVA: 0x00006290 File Offset: 0x00004490
			public SiteMonitorWorkItem()
			{
				this.id = string.Format("SiteMonitorWorkItem-{0}", DateTime.UtcNow.ToString());
				base.Data = SiteMonitor.SiteMonitorWorkItem.EmptyResult;
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600009E RID: 158 RVA: 0x000062D1 File Offset: 0x000044D1
			public override string Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600009F RID: 159 RVA: 0x000062D9 File Offset: 0x000044D9
			public override TimeSpan TimeoutInterval
			{
				get
				{
					return ConfigurationData.Instance.SiteMonitorTimeout;
				}
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x000062E8 File Offset: 0x000044E8
			protected override void DoWork(CancellationToken cancellationToken)
			{
				string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
				string siteName = NativeHelpers.GetSiteName(true);
				TopologyDiscoverySession topologyDiscoverySession = new TopologyDiscoverySession(false, ADSessionSettings.FromRootOrgScopeSet());
				ADObjectId siteADObjectId = topologyDiscoverySession.GetSiteADObjectId(siteName);
				if (cancellationToken.IsCancellationRequested)
				{
					base.ResultType = ResultType.Abandoned;
					return;
				}
				Server server = topologyDiscoverySession.FindServerByFqdn(localComputerFqdn);
				if (server == null)
				{
					ExTraceGlobals.TopologyTracer.TraceError<string>((long)this.GetHashCode(), "Local computer not found in AD. ServerName {0}", localComputerFqdn);
					base.ResultType = ResultType.Failed;
					throw new SiteMonitorException(Strings.ComputerNameNotFoundInAD(localComputerFqdn));
				}
				if (ADObjectId.Equals(server.ServerSite, siteADObjectId))
				{
					base.ResultType = ResultType.Succeeded;
					return;
				}
				string text = (server.ServerSite != null) ? server.ServerSite.Name : string.Empty;
				server.ServerSite = siteADObjectId;
				if (cancellationToken.IsCancellationRequested)
				{
					base.ResultType = ResultType.Abandoned;
					return;
				}
				topologyDiscoverySession.Save(server);
				ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Site Monitor - Site Name Updated Old {0} New {1}", text, siteName);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_SITEMON_EVENT_SITE_UPDATED, null, new object[]
				{
					text,
					siteName
				});
				base.ResultType = ResultType.Succeeded;
			}

			// Token: 0x0400004B RID: 75
			private const string WorkIdFormat = "SiteMonitorWorkItem-{0}";

			// Token: 0x0400004C RID: 76
			private static readonly object EmptyResult = new object();

			// Token: 0x0400004D RID: 77
			private readonly string id;
		}
	}
}

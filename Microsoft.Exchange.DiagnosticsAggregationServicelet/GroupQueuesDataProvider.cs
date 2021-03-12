using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x0200000C RID: 12
	internal class GroupQueuesDataProvider : IGroupQueuesDataProvider
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000039AA File Offset: 0x00001BAA
		public GroupQueuesDataProvider(DiagnosticsAggregationLog log)
		{
			this.log = log;
			this.currentGroupServers = new HashSet<ADObjectId>();
			this.currentGroupServerToQueuesMap = new Dictionary<ADObjectId, ServerQueuesSnapshot>();
			this.timer = null;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000039D8 File Offset: 0x00001BD8
		public void Start()
		{
			this.RefreshCurrentGroupServerToQueuesMap();
			TimeSpan timeSpan = DiagnosticsAggregationServicelet.TransportSettings.QueueDiagnosticsAggregationInterval;
			this.timer = new GuardedTimer(new TimerCallback(this.OnTimerFired), null, timeSpan, timeSpan);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003A15 File Offset: 0x00001C15
		public void Stop()
		{
			if (this.timer != null)
			{
				this.timer.Dispose(true);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003A2C File Offset: 0x00001C2C
		public IDictionary<ADObjectId, ServerQueuesSnapshot> GetCurrentGroupServerToQueuesMap()
		{
			IDictionary<ADObjectId, ServerQueuesSnapshot> result;
			lock (this)
			{
				result = new Dictionary<ADObjectId, ServerQueuesSnapshot>(this.currentGroupServerToQueuesMap);
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003A70 File Offset: 0x00001C70
		private void OnTimerFired(object state)
		{
			this.RefreshCurrentGroupServerToQueuesMap();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A78 File Offset: 0x00001C78
		private void RefreshCurrentGroupServerToQueuesMap()
		{
			ADNotificationAdapter.TryRunADOperation(new ADOperation(this.RefreshCurrentGroupServers), 2);
			lock (this)
			{
				ADObjectId[] array = new ADObjectId[this.currentGroupServerToQueuesMap.Keys.Count];
				this.currentGroupServerToQueuesMap.Keys.CopyTo(array, 0);
				foreach (ADObjectId adobjectId in array)
				{
					if (!this.currentGroupServers.Contains(adobjectId))
					{
						this.currentGroupServerToQueuesMap.Remove(adobjectId);
					}
				}
			}
			foreach (ADObjectId adobjectId2 in this.currentGroupServers)
			{
				string uri = string.Format(CultureInfo.InvariantCulture, DiagnosticsAggregationHelper.DiagnosticsAggregationEndpointFormat, new object[]
				{
					adobjectId2.Name,
					DiagnosticsAggregationServicelet.TransportSettings.DiagnosticsAggregationServicePort
				});
				Exception ex = null;
				DiagnosticsAggregationServiceClient diagnosticsAggregationServiceClient = null;
				try
				{
					diagnosticsAggregationServiceClient = new DiagnosticsAggregationServiceClient(DiagnosticsAggregationServicelet.GetTcpBinding(), new EndpointAddress(uri));
				}
				catch (UriFormatException ex2)
				{
					ex = ex2;
				}
				LocalViewRequest localViewRequest = new LocalViewRequest(RequestType.Queues);
				localViewRequest.QueueLocalViewRequest = new QueueLocalViewRequest();
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				GroupQueuesDataProvider.GetLocalViewAsyncState asyncState = new GroupQueuesDataProvider.GetLocalViewAsyncState
				{
					Client = diagnosticsAggregationServiceClient,
					Server = adobjectId2,
					Stopwatch = stopwatch,
					RequestSessionId = localViewRequest.ClientInformation.SessionId
				};
				try
				{
					if (diagnosticsAggregationServiceClient != null)
					{
						diagnosticsAggregationServiceClient.BeginGetLocalView(localViewRequest, new AsyncCallback(this.OnGetLocalViewCompleted), asyncState);
					}
				}
				catch (EndpointNotFoundException ex3)
				{
					ex = ex3;
				}
				catch (InsufficientMemoryException ex4)
				{
					ex = ex4;
				}
				catch (CommunicationException ex5)
				{
					ex = ex5;
				}
				catch (TimeoutException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					WcfUtils.DisposeWcfClientGracefully(diagnosticsAggregationServiceClient, false);
					stopwatch.Stop();
					this.UpdateSnapshotForServer(adobjectId2, localViewRequest.ClientInformation.SessionId, stopwatch.Elapsed, null, ex.Message);
					if (ex is InsufficientMemoryException)
					{
						lock (this)
						{
							this.log.Log(DiagnosticsAggregationEvent.OutOfResources, "running out of ephemeral ports, will stop making further web service requests", new object[0]);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003D70 File Offset: 0x00001F70
		private void OnGetLocalViewCompleted(IAsyncResult ar)
		{
			GroupQueuesDataProvider.GetLocalViewAsyncState getLocalViewAsyncState = (GroupQueuesDataProvider.GetLocalViewAsyncState)ar.AsyncState;
			DiagnosticsAggregationServiceClient client = getLocalViewAsyncState.Client;
			ADObjectId server = getLocalViewAsyncState.Server;
			Stopwatch stopwatch = getLocalViewAsyncState.Stopwatch;
			LocalViewResponse response = null;
			string errorMessage = null;
			try
			{
				response = client.EndGetLocalView(ar);
			}
			catch (FaultException<DiagnosticsAggregationFault> faultException)
			{
				errorMessage = faultException.Detail.ToString();
			}
			catch (CommunicationException ex)
			{
				errorMessage = ex.Message;
			}
			catch (TimeoutException ex2)
			{
				errorMessage = ex2.Message;
			}
			finally
			{
				WcfUtils.DisposeWcfClientGracefully(client, false);
			}
			stopwatch.Stop();
			this.UpdateSnapshotForServer(server, getLocalViewAsyncState.RequestSessionId, stopwatch.Elapsed, response, errorMessage);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003E38 File Offset: 0x00002038
		private void UpdateSnapshotForServer(ADObjectId server, uint requestSessionId, TimeSpan operationDuration, LocalViewResponse response, string errorMessage)
		{
			lock (this)
			{
				ServerQueuesSnapshot serverQueuesSnapshot = null;
				bool flag2 = this.currentGroupServerToQueuesMap.TryGetValue(server, out serverQueuesSnapshot);
				if (flag2)
				{
					serverQueuesSnapshot = serverQueuesSnapshot.Clone();
				}
				else
				{
					serverQueuesSnapshot = new ServerQueuesSnapshot(server);
				}
				if (response == null)
				{
					serverQueuesSnapshot.UpdateFailure(errorMessage);
					this.log.LogOperationToServer(DiagnosticsAggregationEvent.LocalViewRequestSentFailed, requestSessionId, server.Name, new TimeSpan?(operationDuration), errorMessage);
				}
				else
				{
					serverQueuesSnapshot.UpdateSuccess(response);
					this.log.LogOperationToServer(DiagnosticsAggregationEvent.LocalViewResponseReceived, requestSessionId, server.Name, new TimeSpan?(operationDuration), "");
				}
				this.currentGroupServerToQueuesMap[server] = serverQueuesSnapshot;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003EF0 File Offset: 0x000020F0
		private void RefreshCurrentGroupServers()
		{
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 306, "RefreshCurrentGroupServers", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\DiagnosticsAggregation\\Program\\GroupQueuesDataProvider.cs");
			Server localServer = DiagnosticsAggregationServicelet.LocalServer;
			this.currentGroupServers = DiagnosticsAggregationHelper.GetGroupForServer(localServer, session);
			this.currentGroupServers.Remove(localServer.Id);
		}

		// Token: 0x04000047 RID: 71
		private DiagnosticsAggregationLog log;

		// Token: 0x04000048 RID: 72
		private ISet<ADObjectId> currentGroupServers;

		// Token: 0x04000049 RID: 73
		private IDictionary<ADObjectId, ServerQueuesSnapshot> currentGroupServerToQueuesMap;

		// Token: 0x0400004A RID: 74
		private GuardedTimer timer;

		// Token: 0x0200000D RID: 13
		private class GetLocalViewAsyncState
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600005C RID: 92 RVA: 0x00003F43 File Offset: 0x00002143
			// (set) Token: 0x0600005D RID: 93 RVA: 0x00003F4B File Offset: 0x0000214B
			public DiagnosticsAggregationServiceClient Client { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00003F54 File Offset: 0x00002154
			// (set) Token: 0x0600005F RID: 95 RVA: 0x00003F5C File Offset: 0x0000215C
			public ADObjectId Server { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00003F65 File Offset: 0x00002165
			// (set) Token: 0x06000061 RID: 97 RVA: 0x00003F6D File Offset: 0x0000216D
			public Stopwatch Stopwatch { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00003F76 File Offset: 0x00002176
			// (set) Token: 0x06000063 RID: 99 RVA: 0x00003F7E File Offset: 0x0000217E
			public uint RequestSessionId { get; set; }
		}
	}
}

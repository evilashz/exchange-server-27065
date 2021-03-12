using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueDigest;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x0200006B RID: 107
	internal class GetQueueDigestWebServiceImpl : GetQueueDigestImpl
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public GetQueueDigestWebServiceImpl(GetQueueDigestAdapter cmdlet, ITopologyConfigurationSession session, ADObjectId localSiteId, int webServicePortNumber)
		{
			this.cmdlet = cmdlet;
			this.session = session;
			this.localSiteId = localSiteId;
			this.webServicePortNumber = webServicePortNumber;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E874 File Offset: 0x0000CA74
		public override void ResolveForForest()
		{
			foreach (Server server in this.GetAllServersInForest())
			{
				this.ResolveServer(server);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public override void ResolveDag(DatabaseAvailabilityGroup dag)
		{
			foreach (Server server in this.GetAllServersInForest())
			{
				if (dag.Id.Equals(server.DatabaseAvailabilityGroup))
				{
					this.ResolveServer(server);
				}
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E924 File Offset: 0x0000CB24
		public override void ResolveAdSite(ADSite adSite)
		{
			foreach (Server server in this.GetAllServersInForest())
			{
				if (adSite.Id.Equals(server.ServerSite))
				{
					this.ResolveServer(server);
				}
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E984 File Offset: 0x0000CB84
		public override void ResolveServer(Server server)
		{
			bool flag = ServerComponentStates.IsServerOnline(server.ComponentStates);
			if (!flag || !server.IsHubTransportServer || (!server.IsE15OrLater && !this.cmdlet.IncludeE14Servers))
			{
				return;
			}
			if (server.ServerSite != null)
			{
				this.serverToSiteMap[server.Id] = server.ServerSite;
			}
			if (server.DatabaseAvailabilityGroup != null || server.ServerSite != null)
			{
				GroupOfServersKey key = (server.DatabaseAvailabilityGroup != null) ? GroupOfServersKey.CreateFromDag(server.DatabaseAvailabilityGroup) : GroupOfServersKey.CreateFromSite(server.ServerSite, server.MajorVersion);
				if (!this.dagToServersMap.ContainsKey(key))
				{
					this.dagToServersMap[key] = new HashSet<ADObjectId>();
				}
				this.dagToServersMap[key].Add(server.Id);
				return;
			}
			this.serversNotBelongingToAnyDag.Add(server.Id);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public override void ProcessRecord()
		{
			this.aggregator = new QueueAggregator(this.cmdlet.GroupBy, this.cmdlet.DetailsLevel);
			this.webServiceRequestsPending = this.serversNotBelongingToAnyDag.Count + this.dagToServersMap.Count;
			this.webServiceRequestsDone = new AutoResetEvent(this.webServiceRequestsPending == 0);
			this.pendingRequestsToDag = new int[this.dagToServersMap.Count];
			List<ADObjectId>[] array = new List<ADObjectId>[this.dagToServersMap.Count];
			int num = 0;
			foreach (HashSet<ADObjectId> serversInDag in this.dagToServersMap.Values)
			{
				List<ADObjectId> serversToConnectPreferringLocalToSite = this.GetServersToConnectPreferringLocalToSite(serversInDag, 3);
				array[num] = serversToConnectPreferringLocalToSite;
				this.pendingRequestsToDag[num] = serversToConnectPreferringLocalToSite.Count;
				num++;
			}
			foreach (ADObjectId adobjectId in this.serversNotBelongingToAnyDag)
			{
				this.cmdlet.WriteDebug(string.Format("connecting to server {0}", adobjectId.ToString()));
				this.InvokeWebService(adobjectId, new HashSet<ADObjectId>
				{
					adobjectId
				}, false, -1, this.webServicePortNumber);
			}
			for (int i = 0; i < 3; i++)
			{
				num = 0;
				foreach (KeyValuePair<GroupOfServersKey, HashSet<ADObjectId>> keyValuePair in this.dagToServersMap)
				{
					List<ADObjectId> list = array[num];
					HashSet<ADObjectId> value = keyValuePair.Value;
					if (this.pendingRequestsToDag[num] > 0 && i < list.Count)
					{
						this.cmdlet.WriteDebug(string.Format("connecting to server {0} for group {1} attempt # {2}", list[i].Name, keyValuePair.Key.ToString(), i + 1));
						this.InvokeWebService(list[i], value, true, num, this.webServicePortNumber);
					}
					num++;
				}
			}
			this.webServiceRequestsDone.WaitOne();
			uint maxRecords = this.cmdlet.ResultSize.IsUnlimited ? uint.MaxValue : this.cmdlet.ResultSize.Value;
			this.Display(this.aggregator.GetResultSortedByMessageCount(maxRecords));
			foreach (LocalizedString text in this.verboseMessages)
			{
				this.cmdlet.WriteVerbose(text);
			}
			foreach (LocalizedString text2 in this.debugMessages)
			{
				this.cmdlet.WriteDebug(text2);
			}
			foreach (LocalizedString text3 in this.warningMessages)
			{
				this.cmdlet.WriteWarning(text3);
			}
			if (this.failedToConnectServers.Count != 0)
			{
				this.WriteWarningForConnectionFailure(this.failedToConnectServers);
			}
			if (this.failedToConnectDags.Count != 0)
			{
				this.WriteWarningForConnectionFailure(this.failedToConnectDags);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000EE00 File Offset: 0x0000D000
		public List<AggregatedQueueInfo> GetAggregationResult()
		{
			return this.aggregator.GetResultSortedByMessageCount(uint.MaxValue);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000EE10 File Offset: 0x0000D010
		private void InvokeWebService(ADObjectId serverToConnectTo, HashSet<ADObjectId> serversToInclude, bool isConnectingToDag, int dagIndex, int portNumber)
		{
			string uri = string.Format(CultureInfo.InvariantCulture, DiagnosticsAggregationHelper.DiagnosticsAggregationEndpointFormat, new object[]
			{
				serverToConnectTo.Name,
				portNumber
			});
			Exception ex = null;
			IDiagnosticsAggregationService diagnosticsAggregationService = null;
			try
			{
				diagnosticsAggregationService = this.cmdlet.CreateWebServiceClient(GetQueueDigestWebServiceImpl.GetWebServiceBinding(this.cmdlet.Timeout), new EndpointAddress(uri));
			}
			catch (UriFormatException ex2)
			{
				ex = ex2;
			}
			List<string> list = new List<string>();
			if (serversToInclude != null && serversToInclude.Count != 0)
			{
				foreach (ADObjectId adobjectId in serversToInclude)
				{
					list.Add(adobjectId.Name);
				}
			}
			AggregatedViewRequest aggregatedViewRequest = new AggregatedViewRequest(RequestType.Queues, list, uint.MaxValue);
			aggregatedViewRequest.QueueAggregatedViewRequest = new QueueAggregatedViewRequest(this.cmdlet.GroupBy, this.cmdlet.DetailsLevel, this.cmdlet.Filter);
			GetQueueDigestWebServiceImpl.WebServiceRequestAsyncState webServiceRequestAsyncState = new GetQueueDigestWebServiceImpl.WebServiceRequestAsyncState
			{
				Client = diagnosticsAggregationService,
				ServerToConnectTo = serverToConnectTo,
				ServersToInclude = serversToInclude,
				IsConnectingToDag = isConnectingToDag,
				DagIndex = dagIndex,
				WebServicePortNumber = portNumber
			};
			try
			{
				if (diagnosticsAggregationService != null)
				{
					diagnosticsAggregationService.BeginGetAggregatedView(aggregatedViewRequest, new AsyncCallback(this.OnInvokeWebServiceCompleted), webServiceRequestAsyncState);
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
				webServiceRequestAsyncState.FailedOnBegin = true;
				webServiceRequestAsyncState.FailedOnBeginException = ex;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadProcForBeginGetAggregatedViewFailed), new AsyncResult(null, webServiceRequestAsyncState));
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		private void OnInvokeWebServiceCompleted(IAsyncResult ar)
		{
			GetQueueDigestWebServiceImpl.WebServiceRequestAsyncState webServiceRequestAsyncState = (GetQueueDigestWebServiceImpl.WebServiceRequestAsyncState)ar.AsyncState;
			IDiagnosticsAggregationService client = webServiceRequestAsyncState.Client;
			ADObjectId serverToConnectTo = webServiceRequestAsyncState.ServerToConnectTo;
			int dagIndex = webServiceRequestAsyncState.DagIndex;
			string text = null;
			LocalizedString? localizedString = null;
			AggregatedViewResponse aggregatedViewResponse = null;
			try
			{
				if (webServiceRequestAsyncState.FailedOnBegin)
				{
					text = webServiceRequestAsyncState.FailedOnBeginException.ToString();
				}
				else
				{
					aggregatedViewResponse = client.EndGetAggregatedView(ar);
				}
			}
			catch (FaultException<DiagnosticsAggregationFault> faultException)
			{
				text = string.Format("{0}: {1}", faultException.Detail.ErrorCode, faultException.Detail.Message);
			}
			catch (CommunicationException ex)
			{
				if (GetQueueDigestWebServiceImpl.IsQuotaExceeded(ex))
				{
					localizedString = new LocalizedString?(Strings.GetQueueDigestQuotaExceeded(serverToConnectTo.Name));
				}
				text = ex.ToString();
			}
			catch (TimeoutException ex2)
			{
				text = ex2.ToString();
			}
			finally
			{
				this.cmdlet.DisposeWebServiceClient(client);
			}
			bool flag = false;
			lock (this)
			{
				if (!webServiceRequestAsyncState.IsConnectingToDag || this.pendingRequestsToDag[dagIndex] != 0)
				{
					if (aggregatedViewResponse != null)
					{
						this.aggregator.AddAggregatedQueues(aggregatedViewResponse.QueueAggregatedViewResponse.AggregatedQueues);
						if (this.cmdlet.IsVerbose)
						{
							foreach (ServerSnapshotStatus serverSnapshotStatus in aggregatedViewResponse.SnapshotStatusOfServers)
							{
								this.verboseMessages.Add(new LocalizedString(string.Format("{0}: {1}", serverToConnectTo.Name, serverSnapshotStatus.ToString())));
							}
						}
						if (webServiceRequestAsyncState.IsConnectingToDag)
						{
							this.pendingRequestsToDag[dagIndex] = 0;
						}
					}
					else if (webServiceRequestAsyncState.IsConnectingToDag)
					{
						this.pendingRequestsToDag[dagIndex]--;
						if (this.pendingRequestsToDag[dagIndex] > 0)
						{
							flag = true;
						}
						else
						{
							this.failedToConnectDags.Add(serverToConnectTo.Name);
						}
					}
					else
					{
						this.failedToConnectServers.Add(serverToConnectTo.Name);
					}
					if (text != null)
					{
						this.debugMessages.Add(new LocalizedString(string.Format("{0}: {1}", serverToConnectTo.Name, text)));
					}
					if (localizedString != null)
					{
						this.warningMessages.Add(localizedString.Value);
					}
					if (!flag)
					{
						this.webServiceRequestsPending--;
					}
					if (this.webServiceRequestsPending == 0)
					{
						this.webServiceRequestsDone.Set();
					}
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		private void ThreadProcForBeginGetAggregatedViewFailed(object state)
		{
			this.OnInvokeWebServiceCompleted((AsyncResult)state);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		private List<ADObjectId> GetServersToConnectPreferringLocalToSite(HashSet<ADObjectId> serversInDag, int numEntries)
		{
			List<ADObjectId> list = new List<ADObjectId>(numEntries);
			List<ADObjectId> list2 = new List<ADObjectId>(serversInDag.Count);
			List<ADObjectId> list3 = new List<ADObjectId>(serversInDag.Count);
			foreach (ADObjectId adobjectId in serversInDag)
			{
				ADObjectId adobjectId2 = null;
				if (this.serverToSiteMap.TryGetValue(adobjectId, out adobjectId2) && adobjectId2.Equals(this.localSiteId))
				{
					list2.Add(adobjectId);
				}
				else
				{
					list3.Add(adobjectId);
				}
			}
			if (list2.Count > 0)
			{
				RoutingUtils.ShuffleList<ADObjectId>(list2);
				foreach (ADObjectId item in list2)
				{
					list.Add(item);
					if (list.Count == numEntries)
					{
						break;
					}
				}
			}
			if (list.Count < numEntries)
			{
				RoutingUtils.ShuffleList<ADObjectId>(list3);
				foreach (ADObjectId item2 in list3)
				{
					list.Add(item2);
					if (list.Count == numEntries)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000F430 File Offset: 0x0000D630
		private IEnumerable<Server> GetAllServersInForest()
		{
			if (this.allServersInForest == null)
			{
				ADPagedReader<Server> adpagedReader = this.session.FindPaged<Server>(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
					this.cmdlet.IncludeE14Servers ? DiagnosticsAggregationHelper.IsE14OrHigherQueryFilter : DiagnosticsAggregationHelper.IsE15OrHigherQueryFilter
				}), null, 0);
				this.allServersInForest = new List<Server>();
				foreach (Server item in adpagedReader)
				{
					this.allServersInForest.Add(item);
				}
			}
			return this.allServersInForest;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		private void WriteWarningForConnectionFailure(List<string> failedToConnectToServer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in failedToConnectToServer)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(value);
			}
			this.cmdlet.WriteWarning(Strings.GetQueueDigestFailedToConnectTo(stringBuilder.ToString()));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000F568 File Offset: 0x0000D768
		private void Display(IEnumerable<AggregatedQueueInfo> aggregatedQueues)
		{
			foreach (AggregatedQueueInfo aggregatedQueueInfo in aggregatedQueues)
			{
				QueueDigestPresentationObject sendToPipeline = QueueDigestPresentationObject.Create(aggregatedQueueInfo);
				this.cmdlet.WriteObject(sendToPipeline);
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000F5BC File Offset: 0x0000D7BC
		private static Binding GetWebServiceBinding(EnhancedTimeSpan timeout)
		{
			if (GetQueueDigestWebServiceImpl.webServiceBinding == null)
			{
				GetQueueDigestWebServiceImpl.webServiceBinding = new NetTcpBinding
				{
					Security = 
					{
						Transport = 
						{
							ProtectionLevel = ProtectionLevel.EncryptAndSign,
							ClientCredentialType = TcpClientCredentialType.Windows
						},
						Message = 
						{
							ClientCredentialType = MessageCredentialType.Windows
						}
					},
					MaxReceivedMessageSize = (long)((int)ByteQuantifiedSize.FromMB(5UL).ToBytes()),
					OpenTimeout = timeout,
					CloseTimeout = timeout,
					SendTimeout = timeout,
					ReceiveTimeout = timeout
				};
			}
			return GetQueueDigestWebServiceImpl.webServiceBinding;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F65E File Offset: 0x0000D85E
		private static bool IsQuotaExceeded(CommunicationException communicationException)
		{
			return communicationException.InnerException != null && communicationException.InnerException is QuotaExceededException;
		}

		// Token: 0x0400015E RID: 350
		private const int MaxRequestsPerDag = 3;

		// Token: 0x0400015F RID: 351
		private static Binding webServiceBinding;

		// Token: 0x04000160 RID: 352
		private List<Server> allServersInForest;

		// Token: 0x04000161 RID: 353
		private Dictionary<ADObjectId, ADObjectId> serverToSiteMap = new Dictionary<ADObjectId, ADObjectId>();

		// Token: 0x04000162 RID: 354
		private Dictionary<GroupOfServersKey, HashSet<ADObjectId>> dagToServersMap = new Dictionary<GroupOfServersKey, HashSet<ADObjectId>>();

		// Token: 0x04000163 RID: 355
		private HashSet<ADObjectId> serversNotBelongingToAnyDag = new HashSet<ADObjectId>();

		// Token: 0x04000164 RID: 356
		private ADObjectId localSiteId;

		// Token: 0x04000165 RID: 357
		private AutoResetEvent webServiceRequestsDone;

		// Token: 0x04000166 RID: 358
		private int webServiceRequestsPending;

		// Token: 0x04000167 RID: 359
		private int[] pendingRequestsToDag;

		// Token: 0x04000168 RID: 360
		private QueueAggregator aggregator;

		// Token: 0x04000169 RID: 361
		private List<string> failedToConnectServers = new List<string>();

		// Token: 0x0400016A RID: 362
		private List<string> failedToConnectDags = new List<string>();

		// Token: 0x0400016B RID: 363
		private List<LocalizedString> warningMessages = new List<LocalizedString>();

		// Token: 0x0400016C RID: 364
		private List<LocalizedString> debugMessages = new List<LocalizedString>();

		// Token: 0x0400016D RID: 365
		private List<LocalizedString> verboseMessages = new List<LocalizedString>();

		// Token: 0x0400016E RID: 366
		private GetQueueDigestAdapter cmdlet;

		// Token: 0x0400016F RID: 367
		private readonly int webServicePortNumber;

		// Token: 0x04000170 RID: 368
		private ITopologyConfigurationSession session;

		// Token: 0x0200006C RID: 108
		internal class WebServiceRequestAsyncState
		{
			// Token: 0x1700016A RID: 362
			// (get) Token: 0x060003DB RID: 987 RVA: 0x0000F678 File Offset: 0x0000D878
			// (set) Token: 0x060003DC RID: 988 RVA: 0x0000F680 File Offset: 0x0000D880
			public IDiagnosticsAggregationService Client { get; set; }

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x060003DD RID: 989 RVA: 0x0000F689 File Offset: 0x0000D889
			// (set) Token: 0x060003DE RID: 990 RVA: 0x0000F691 File Offset: 0x0000D891
			public ADObjectId ServerToConnectTo { get; set; }

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x060003DF RID: 991 RVA: 0x0000F69A File Offset: 0x0000D89A
			// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
			public HashSet<ADObjectId> ServersToInclude { get; set; }

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000F6AB File Offset: 0x0000D8AB
			// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
			public bool IsConnectingToDag { get; set; }

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000F6BC File Offset: 0x0000D8BC
			// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
			public int DagIndex { get; set; }

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000F6CD File Offset: 0x0000D8CD
			// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
			public bool FailedOnBegin { get; set; }

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000F6DE File Offset: 0x0000D8DE
			// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000F6E6 File Offset: 0x0000D8E6
			public Exception FailedOnBeginException { get; set; }

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000F6EF File Offset: 0x0000D8EF
			// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000F6F7 File Offset: 0x0000D8F7
			public int WebServicePortNumber { get; set; }
		}
	}
}

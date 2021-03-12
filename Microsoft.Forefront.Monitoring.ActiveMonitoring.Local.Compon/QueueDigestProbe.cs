using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Management.QueueDigest;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001F5 RID: 501
	public class QueueDigestProbe : ProbeWorkItem
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0002629A File Offset: 0x0002449A
		internal bool IsDataCenter
		{
			get
			{
				return LocalEndpointManager.IsDataCenter;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x000262A1 File Offset: 0x000244A1
		internal string JobReport
		{
			get
			{
				return string.Format("QueueDigestProbe Job Report{0}{0}{1}", Environment.NewLine, this.jobDetails.ToString());
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000262C0 File Offset: 0x000244C0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 114, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\QueueDigest\\Probes\\QueueDigestProbe.cs");
				if (!this.ShouldRun())
				{
					this.TraceDebug("QueueDigestProbe on {0} skipped because ShouldRun() returned false", new object[]
					{
						Environment.MachineName
					});
				}
				else
				{
					this.cancellationToken = cancellationToken;
					this.TraceDebug("QueueDigestProbe on {0} starts querying queue stats", new object[]
					{
						Environment.MachineName
					});
					this.GetConfiguration();
					this.DoProbeWork();
					WTFDiagnostics.TraceDebug(ExTraceGlobals.QueueDigestTracer, new TracingContext(), this.JobReport, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\QueueDigest\\Probes\\QueueDigestProbe.cs", 134);
				}
			}
			catch (Exception ex)
			{
				this.TraceDebug("QueueDigestProbe failded due to an exception: {0}", new object[]
				{
					ex.ToString()
				});
				throw;
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0002639C File Offset: 0x0002459C
		private bool ShouldRun()
		{
			Server server = this.session.FindLocalServer();
			IList<Database> list;
			if (!server.IsMailboxServer || !this.TryLoadAdObjects<Database>(this.session.GetDatabaseAvailabilityGroupContainerId(), out list))
			{
				return false;
			}
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance == null || instance.MailboxDatabaseEndpoint == null)
			{
				throw new SmtpConnectionProbeException("No MailboxDatabaseEndpoint for Backend found on this server");
			}
			return instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count != 0;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0002643C File Offset: 0x0002463C
		private bool TryLoadAdObjects<T>(ADObjectId rootId, out IList<T> objects) where T : ADConfigurationObject, new()
		{
			List<T> results = new List<T>();
			bool result = ADNotificationAdapter.TryReadConfigurationPaged<T>(() => this.session.FindPaged<T>(rootId, QueryScope.SubTree, null, null, ADGenericPagedReader<T>.DefaultPageSize), delegate(T configObject)
			{
				results.Add(configObject);
			});
			objects = results;
			return result;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00026490 File Offset: 0x00024690
		private void GetConfiguration()
		{
			if (!this.IsDataCenter && base.Definition.Name.Equals("WellKnownDestinationProbe", StringComparison.OrdinalIgnoreCase))
			{
				this.queues = QueueConfiguration.GetRemoteDomains(this.session);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(base.Definition.ExtensionAttributes))
				{
					throw new XmlException("ExtensionAttributes in probe definition is null");
				}
				XmlDocument xmlDocument = new SafeXmlDocument();
				xmlDocument.LoadXml(base.Definition.ExtensionAttributes);
				XmlNode xmlNode = Utils.CheckNode(xmlDocument.SelectSingleNode("//Queues"), "Queues");
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode queueNode = (XmlNode)obj;
					QueueConfiguration queueConfiguration = new QueueConfiguration(queueNode);
					queueConfiguration.ResolveParameters();
					this.queues.Add(queueConfiguration);
				}
			}
			this.TraceDebug("Work definition processed. QueueCount={0}", new object[]
			{
				(this.queues == null) ? 0 : this.queues.Count
			});
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000265CC File Offset: 0x000247CC
		private void DoProbeWork()
		{
			foreach (QueueConfiguration queueConfiguration in this.queues)
			{
				TransportConfigContainer transportConfigContainer = queueConfiguration.Session.FindSingletonConfigurationObject<TransportConfigContainer>();
				queueConfiguration.Aggregator = new QueueAggregator(queueConfiguration.AggregatedBy, queueConfiguration.DetailsLevel);
				if (queueConfiguration.ServersNotBelongingToAnyDag != null && queueConfiguration.ServersNotBelongingToAnyDag.ToList<ADObjectId>().Count > 0)
				{
					List<ADObjectId> list = (from serverObject in queueConfiguration.ServersNotBelongingToAnyDag
					where serverObject.Name.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase)
					select serverObject).ToList<ADObjectId>();
					if (list.Count > 0)
					{
						throw new Exception(string.Format("This server {0} is not part of any DAG", Environment.MachineName));
					}
				}
				queueConfiguration.WebServiceRequestsPending = queueConfiguration.DagToServersMap.Count;
				queueConfiguration.WebServiceRequestsDone = new AutoResetEvent(queueConfiguration.WebServiceRequestsPending == 0);
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				foreach (KeyValuePair<GroupOfServersKey, List<ADObjectId>> serversInDag in queueConfiguration.DagToServersMap)
				{
					List<ADObjectId> list2;
					HashSet<ADObjectId> hashSet;
					queueConfiguration.GetServersToConnectPreferingServersSpecified(serversInDag, out list2, out hashSet);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ADObjectId adobjectId in list2)
					{
						stringBuilder.Append(adobjectId.Name + ", ");
					}
					this.TraceDebug("Connecting to servers {0} at port {1}", new object[]
					{
						stringBuilder.ToString(),
						transportConfigContainer.DiagnosticsAggregationServicePort
					});
					stringBuilder.Clear();
					foreach (ADObjectId adobjectId2 in hashSet)
					{
						stringBuilder.Append(adobjectId2.Name + ", ");
					}
					this.TraceDebug("Queue Digest to include servers {0}", new object[]
					{
						stringBuilder.ToString()
					});
					this.InvokeWebServiceAsync(list2, hashSet, true, 1, transportConfigContainer.DiagnosticsAggregationServicePort, queueConfiguration);
				}
				queueConfiguration.WebServiceRequestsDone.WaitOne(-1, this.cancellationToken.IsCancellationRequested);
				this.CheckCancellation();
				switch (queueConfiguration.QueueType)
				{
				case QueueType.WellKnownDestination:
					this.ProcessWellKnownDestinationQueueStats(queueConfiguration);
					break;
				case QueueType.Aggregated:
					this.ProcessAggregatedQueueStats(queueConfiguration);
					break;
				}
				stopwatch.Stop();
				this.TraceDebug("TotalStatsQueryTime={0}", new object[]
				{
					stopwatch.Elapsed
				});
				this.CheckCancellation();
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000268E8 File Offset: 0x00024AE8
		private void CheckCancellation()
		{
			if (this.cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000268FD File Offset: 0x00024AFD
		private bool IsDesiredWellKnwonDestinationStats(QueueConfiguration config, TransportQueueStatistics queueStats)
		{
			return queueStats.NextHopDomain.ToLower() == config.Destination.ToLower();
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002691C File Offset: 0x00024B1C
		private bool IsDesiredAggregatedQueueStats(QueueConfiguration config, TransportQueueStatistics queueStats)
		{
			return queueStats.MessageCount > config.MessageCountGreaterThan && (string.IsNullOrWhiteSpace(config.Destination) || !(config.Destination.ToLower() != queueStats.NextHopDomain.ToLower())) && (config.DeliveryType == DeliveryType.Undefined || !(config.DeliveryType.ToString().ToLower() != queueStats.DeliveryType.ToLower()));
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00026998 File Offset: 0x00024B98
		private void ProcessWellKnownDestinationQueueStats(QueueConfiguration config)
		{
			int num = 0;
			foreach (AggregatedQueueInfo aggregatedQueueInfo in config.Aggregator.GetResultSortedByMessageCount(config.ResultSize))
			{
				this.ProcessWellKnownDestinationMessageCount(config, aggregatedQueueInfo.MessageCount, aggregatedQueueInfo.DeferredMessageCount, aggregatedQueueInfo.LockedMessageCount, this.ToString(aggregatedQueueInfo));
				this.AppendJobDetails(config, aggregatedQueueInfo);
				num++;
			}
			if (config.FailedToConnectServers.Count != 0)
			{
				this.TraceDebug("Failed to connected to servers: {0}", new object[]
				{
					string.Join(",", config.FailedToConnectServers)
				});
			}
			if (config.FailedToConnectDags.Count != 0)
			{
				this.TraceDebug("Failed to connected to dags: {0}", new object[]
				{
					string.Join(",", config.FailedToConnectDags)
				});
			}
			if (num == 0)
			{
				this.ActionOnNoQueueStats(config);
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00026A90 File Offset: 0x00024C90
		private void ProcessAggregatedQueueStats(QueueConfiguration config)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			List<AggregatedQueueInfo> list = new List<AggregatedQueueInfo>();
			foreach (AggregatedQueueInfo aggregatedQueueInfo in config.Aggregator.GetResultSortedByMessageCount(config.ResultSize))
			{
				if (!config.ShouldExemptPoisonQueues || !aggregatedQueueInfo.GroupByValue.Equals("Poison Message", StringComparison.InvariantCultureIgnoreCase))
				{
					if (aggregatedQueueInfo.MessageCount > config.MessageCountGreaterThan)
					{
						list.Add(aggregatedQueueInfo);
						num2 += aggregatedQueueInfo.MessageCount;
						if (num3 == 0 || aggregatedQueueInfo.MessageCount < num3)
						{
							num3 = aggregatedQueueInfo.MessageCount;
						}
					}
					this.AppendJobDetails(config, aggregatedQueueInfo);
					num++;
				}
			}
			if (config.FailedToConnectServers.Count != 0)
			{
				this.TraceDebug("Failed to connected to servers: {0}", new object[]
				{
					string.Join(",", config.FailedToConnectServers)
				});
			}
			if (config.FailedToConnectDags.Count != 0)
			{
				this.TraceDebug("Failed to connected to dags: {0}", new object[]
				{
					string.Join(",", config.FailedToConnectDags)
				});
			}
			if (num == 0)
			{
				this.ActionOnNoQueueStats(config);
				return;
			}
			int averageMessageCount = (list.Count == 0) ? 0 : (num2 / list.Count);
			this.ProcessAggregatedQueueMessageCount(config, num2, averageMessageCount);
			foreach (AggregatedQueueInfo aggregatedQueueInfo2 in list)
			{
				this.ProcessAggregatedQueueMessageCount(config, aggregatedQueueInfo2.MessageCount, aggregatedQueueInfo2.DeferredMessageCount, aggregatedQueueInfo2.LockedMessageCount, averageMessageCount, num3, this.ToString(aggregatedQueueInfo2));
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00026C4C File Offset: 0x00024E4C
		private void InvokeWebServiceAsync(List<ADObjectId> serversToConnect, HashSet<ADObjectId> serversToInclude, bool isConnectingToDag, int connectionAttempt, int portNumber, QueueConfiguration config)
		{
			ADObjectId adobjectId = serversToConnect[0];
			string uri = string.Format(CultureInfo.InvariantCulture, DiagnosticsAggregationHelper.DiagnosticsAggregationEndpointFormat, new object[]
			{
				adobjectId.Name,
				portNumber
			});
			DiagnosticsAggregationServiceClient diagnosticsAggregationServiceClient = new DiagnosticsAggregationServiceClient(this.GetWebServiceBinding(config), new EndpointAddress(uri));
			List<string> list = new List<string>();
			if (serversToInclude != null && serversToInclude.Count != 0)
			{
				foreach (ADObjectId adobjectId2 in serversToInclude)
				{
					list.Add(adobjectId2.Name);
				}
			}
			AggregatedViewRequest aggregatedViewRequest = new AggregatedViewRequest(RequestType.Queues, list, config.ServerSideResultSize);
			aggregatedViewRequest.QueueAggregatedViewRequest = new QueueAggregatedViewRequest(config.AggregatedBy, config.DetailsLevel, config.Filter);
			QueueDigestProbe.WebServiceRequestAsyncState asyncState = new QueueDigestProbe.WebServiceRequestAsyncState
			{
				Client = diagnosticsAggregationServiceClient,
				ServersToConnect = serversToConnect,
				ServersToInclude = serversToInclude,
				IsConnectingToDag = isConnectingToDag,
				ConnectionAttempt = connectionAttempt,
				WebServicePortNumber = portNumber,
				Config = config
			};
			diagnosticsAggregationServiceClient.BeginGetAggregatedView(aggregatedViewRequest, new AsyncCallback(this.OnInvokeWebServiceCompleted), asyncState);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00026D8C File Offset: 0x00024F8C
		private Binding GetWebServiceBinding(QueueConfiguration config)
		{
			if (config.WebServiceBinding == null)
			{
				EnhancedTimeSpan enhancedTimeSpan = EnhancedTimeSpan.FromSeconds(20.0);
				config.WebServiceBinding = new NetTcpBinding
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
					OpenTimeout = enhancedTimeSpan,
					CloseTimeout = enhancedTimeSpan,
					SendTimeout = enhancedTimeSpan,
					ReceiveTimeout = enhancedTimeSpan
				};
			}
			return config.WebServiceBinding;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00026E40 File Offset: 0x00025040
		private void OnInvokeWebServiceCompleted(IAsyncResult ar)
		{
			QueueDigestProbe.WebServiceRequestAsyncState webServiceRequestAsyncState = (QueueDigestProbe.WebServiceRequestAsyncState)ar.AsyncState;
			DiagnosticsAggregationServiceClient client = webServiceRequestAsyncState.Client;
			ADObjectId adobjectId = webServiceRequestAsyncState.ServersToConnect[0];
			QueueConfiguration config = webServiceRequestAsyncState.Config;
			webServiceRequestAsyncState.ServersToConnect.RemoveAt(0);
			AggregatedViewResponse aggregatedViewResponse = null;
			try
			{
				aggregatedViewResponse = client.EndGetAggregatedView(ar);
			}
			catch (FaultException<DiagnosticsAggregationFault> faultException)
			{
				this.TraceDebug("DiagnosticsAggregationFault: {0}, {1}", new object[]
				{
					faultException.Detail.ErrorCode,
					faultException.Detail.Message
				});
			}
			catch (CommunicationException ex)
			{
				if (ex.InnerException != null && ex.InnerException is QuotaExceededException)
				{
					this.TraceDebug("CommunicationException: {0} quota exceeded", new object[]
					{
						adobjectId.Name
					});
				}
				this.TraceDebug(ex.ToString(), new object[0]);
			}
			catch (TimeoutException ex2)
			{
				this.TraceDebug("TimeoutException: {0}", new object[]
				{
					ex2.ToString()
				});
			}
			catch (Exception ex3)
			{
				this.TraceDebug("Exception: {0}", new object[]
				{
					ex3.ToString()
				});
			}
			finally
			{
				WcfUtils.DisposeWcfClientGracefully(client, false);
			}
			bool flag = false;
			lock (this)
			{
				if (aggregatedViewResponse != null)
				{
					config.Aggregator.AddAggregatedQueues(aggregatedViewResponse.QueueAggregatedViewResponse.AggregatedQueues);
					using (List<ServerSnapshotStatus>.Enumerator enumerator = aggregatedViewResponse.SnapshotStatusOfServers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ServerSnapshotStatus serverSnapshotStatus = enumerator.Current;
							this.TraceDebug("{0}: {1}", new object[]
							{
								adobjectId.Name,
								serverSnapshotStatus.ToString()
							});
						}
						goto IL_1EF;
					}
				}
				if (webServiceRequestAsyncState.IsConnectingToDag)
				{
					if (webServiceRequestAsyncState.ConnectionAttempt <= 3 && webServiceRequestAsyncState.ServersToConnect.Count > 0)
					{
						flag = true;
					}
					else
					{
						config.FailedToConnectDags.Add(adobjectId.Name);
					}
				}
				else
				{
					config.FailedToConnectServers.Add(adobjectId.Name);
				}
				IL_1EF:
				if (!flag)
				{
					config.WebServiceRequestsPending--;
				}
				if (config.WebServiceRequestsPending == 0)
				{
					config.WebServiceRequestsDone.Set();
				}
			}
			if (flag)
			{
				this.InvokeWebServiceAsync(webServiceRequestAsyncState.ServersToConnect, webServiceRequestAsyncState.ServersToInclude, webServiceRequestAsyncState.IsConnectingToDag, webServiceRequestAsyncState.ConnectionAttempt + 1, webServiceRequestAsyncState.WebServicePortNumber, webServiceRequestAsyncState.Config);
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000270F8 File Offset: 0x000252F8
		private void ProcessWellKnownDestinationMessageCount(QueueConfiguration config, int messageCount, int deferredMessageCount, int lockedMessageCount, string statsDetails)
		{
			if (config.MessageCountThreshold > 0 && messageCount > config.MessageCountThreshold)
			{
				string text = string.Format("MessageCount of this destination has exceeded the configured threshold: {0}. QueueStats: {1}. Configuration: {2}", config.Destination, statsDetails, config.ToString());
				this.PublishEventNotification(config, text);
				this.TraceDebug(text, new object[0]);
			}
			if (config.DeferredMessageCountThreshold > 0 && deferredMessageCount > config.DeferredMessageCountThreshold)
			{
				string text2 = string.Format("DeferredMessageCount of this destination has exceeded the configured threshold: {0}. QueueStats: {1}. Configuration: {2}", config.Destination, statsDetails, config.ToString());
				this.PublishEventNotification(config, text2);
				this.TraceDebug(text2, new object[0]);
			}
			if (config.LockedMessageCountThreshold > 0 && lockedMessageCount > config.LockedMessageCountThreshold)
			{
				string text3 = string.Format("LockedMessageCount of this destination has exceeded the configured threshold: {0}. QueueStats: {1}. Configuration: {2}", config.Destination, statsDetails, config.ToString());
				this.PublishEventNotification(config, text3);
				this.TraceDebug(text3, new object[0]);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x000271C8 File Offset: 0x000253C8
		private void ProcessAggregatedQueueMessageCount(QueueConfiguration config, int totalMessageCount, int averageMessageCount)
		{
			if (config.TotalMessageCountThreshold > 0 && totalMessageCount > config.TotalMessageCountThreshold)
			{
				string text = string.Format("Total message count {0} has exceeded the configured threshod. Configuration: {1}", totalMessageCount, config.ToString());
				this.PublishEventNotification(config, text);
				this.TraceDebug(text, new object[0]);
				return;
			}
			if (config.AverageMessageCountThreshold > 0 && averageMessageCount > config.AverageMessageCountThreshold)
			{
				string text2 = string.Format("Message count average {0} has exceeded the configured threshod. Configuration: {1}", averageMessageCount, config.ToString());
				this.PublishEventNotification(config, text2);
				this.TraceDebug(text2, new object[0]);
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00027254 File Offset: 0x00025454
		private void ProcessAggregatedQueueMessageCount(QueueConfiguration config, int messageCount, int deferredMessageCount, int lockedMessageCount, int averageMessageCount, int lowestMessageCount, string statsDetails)
		{
			if (config.MessageCountThreshold > 0 && messageCount > config.MessageCountThreshold)
			{
				string text = string.Format("MessageCount has exceeded the configured threshold: {0}. QueueStats: {1}. Configuration: {2}", messageCount, statsDetails, config.ToString());
				this.PublishEventNotification(config, text);
				this.TraceDebug(text, new object[0]);
			}
			if (config.DeferredMessageCountThreshold > 0 && deferredMessageCount > config.DeferredMessageCountThreshold)
			{
				string text2 = string.Format("DeferredMessageCount has exceeded the configured threshold: {0}. QueueStats: {1}. Configuration: {2}", deferredMessageCount, statsDetails, config.ToString());
				this.PublishEventNotification(config, text2);
				this.TraceDebug(text2, new object[0]);
			}
			if (config.LockedMessageCountThreshold > 0 && lockedMessageCount > config.LockedMessageCountThreshold)
			{
				string text3 = string.Format("LockedMessageCountThreshold has exceeded the configured threshold: {0}. QueueStats: {1}. Configuration: {2}", lockedMessageCount, statsDetails, config.ToString());
				this.PublishEventNotification(config, text3);
				this.TraceDebug(text3, new object[0]);
			}
			if (averageMessageCount > 0 && config.ExceedsAverageByNumber > 0)
			{
				int num = messageCount - averageMessageCount;
				if (num > config.ExceedsAverageByNumber)
				{
					string text4 = string.Format("MessageCount has exceeded average by number: {0}. QueueStats: {1}. Configuration: {2}", num, statsDetails, config.ToString());
					this.PublishEventNotification(config, text4);
					this.TraceDebug(text4, new object[0]);
					return;
				}
			}
			if (averageMessageCount > 0 && config.ExceedsAverageByPercent > 0)
			{
				int num2 = (messageCount - averageMessageCount) * 100 / averageMessageCount;
				if (num2 > config.ExceedsAverageByPercent)
				{
					string text5 = string.Format("MessageCount has exceeded the average by percent: {0}. QueueStats: {1}. Configuration: {2}", num2, statsDetails, config.ToString());
					this.PublishEventNotification(config, text5);
					this.TraceDebug(text5, new object[0]);
					return;
				}
			}
			if (config.ExceedsLowestByNumber > 0)
			{
				int num3 = messageCount - lowestMessageCount;
				if (num3 > config.ExceedsLowestByNumber)
				{
					string text6 = string.Format("MessageCount has exceeded the lowest by number: {0}. QueueStats: {1}. Configuration: {2}", num3, statsDetails, config.ToString());
					this.PublishEventNotification(config, text6);
					this.TraceDebug(text6, new object[0]);
					return;
				}
			}
			if (config.ExceedsLowestByPercent > 0)
			{
				int num4 = (messageCount - lowestMessageCount) * 100 / lowestMessageCount;
				if (num4 > config.ExceedsLowestByPercent)
				{
					string text7 = string.Format("MessageCount has exceeded the lowest by percent: {0}. QueueStats: {1}. Configuration: {2}", num4, statsDetails, config.ToString());
					this.PublishEventNotification(config, text7);
					this.TraceDebug(text7, new object[0]);
				}
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00027468 File Offset: 0x00025668
		private void PublishEventNotification(QueueConfiguration config, string message)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(config.EventNotificationServiceName, config.EventNotificationComponent, config.EventNotificationTag, message, config.EventNotificationSeverityLevel);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002749C File Offset: 0x0002569C
		private void ActionOnNoQueueStats(QueueConfiguration config)
		{
			this.TraceDebug("No queue stats available for this configuration: {0}", new object[]
			{
				config.ToString()
			});
			if (config.RaiseWarningOnNoStats)
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(config.EventNotificationServiceName, "DestinationQueueStatsNotAvailable", config.EventNotificationTagForNoStats, string.Format("No queue stats available for this destination: {0}", config.ToString()), ResultSeverityLevel.Warning);
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000274FC File Offset: 0x000256FC
		private void AppendJobDetails(QueueConfiguration config, TransportQueueStatistics stats)
		{
			this.jobDetails.AppendFormat("QueueConfiguration: {0}{1}", config.ToString(), Environment.NewLine);
			this.jobDetails.AppendFormat("QueueStats: {0}{1}{1}", this.ToString(stats), Environment.NewLine);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00027537 File Offset: 0x00025737
		private void AppendJobDetails(QueueConfiguration config, AggregatedQueueInfo stats)
		{
			this.jobDetails.AppendFormat("QueueConfiguration: {0}{1}", config.ToString(), Environment.NewLine);
			this.jobDetails.AppendFormat("QueueStats: {0}{1}{1}", this.ToString(stats), Environment.NewLine);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00027574 File Offset: 0x00025774
		private string ToString(TransportQueueStatistics stats)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DeferredMessageCount={0}, ", stats.DeferredMessageCount);
			stringBuilder.AppendFormat("DeliveryType={0}, ", stats.DeliveryType);
			stringBuilder.AppendFormat("Identity={0}, ", stats.Identity);
			stringBuilder.AppendFormat("IncomingRate={0}, ", stats.IncomingRate);
			stringBuilder.AppendFormat("LastError={0}, ", stats.LastError);
			stringBuilder.AppendFormat("LockedMessageCount={0}, ", stats.LockedMessageCount);
			stringBuilder.AppendFormat("MessageCount={0}, ", stats.MessageCount);
			stringBuilder.AppendFormat("NextHopCategory={0}, ", stats.NextHopCategory);
			stringBuilder.AppendFormat("NextHopDomain={0}, ", stats.NextHopDomain);
			stringBuilder.AppendFormat("NextHopKey={0}", stats.NextHopKey);
			stringBuilder.AppendFormat("OutgoingRate={0}, ", stats.OutgoingRate);
			stringBuilder.AppendFormat("QueueCount={0}, ", stats.QueueCount);
			stringBuilder.AppendFormat("RiskLevel={0}, ", stats.RiskLevel);
			stringBuilder.AppendFormat("ServerName={0}, ", stats.ServerName);
			stringBuilder.AppendFormat("Status={0}, ", stats.Status);
			stringBuilder.AppendFormat("TlsDomain={0}, ", stats.TlsDomain);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000276CC File Offset: 0x000258CC
		private string ToString(AggregatedQueueInfo stats)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DeferredMessageCount={0}, ", stats.DeferredMessageCount);
			stringBuilder.AppendFormat("IncomingRate={0}, ", stats.IncomingRate);
			stringBuilder.AppendFormat("LockedMessageCount={0}, ", stats.LockedMessageCount);
			stringBuilder.AppendFormat("MessageCount={0}, ", stats.MessageCount);
			stringBuilder.AppendFormat("OutgoingRate={0}, ", stats.OutgoingRate);
			foreach (AggregatedQueueVerboseDetails aggregatedQueueVerboseDetails in stats.VerboseDetails)
			{
				stringBuilder.AppendFormat("DeliveryType={0}, ", aggregatedQueueVerboseDetails.DeliveryType);
				stringBuilder.AppendFormat("LastError={0}, ", aggregatedQueueVerboseDetails.LastError);
				stringBuilder.AppendFormat("NextHopCategory={0}, ", aggregatedQueueVerboseDetails.NextHopCategory);
				stringBuilder.AppendFormat("NextHopConnector={0}, ", aggregatedQueueVerboseDetails.NextHopConnector);
				stringBuilder.AppendFormat("NextHopDomain={0}, ", aggregatedQueueVerboseDetails.NextHopDomain);
				stringBuilder.AppendFormat("QueueIdentity={0}, ", aggregatedQueueVerboseDetails.QueueIdentity);
				stringBuilder.AppendFormat("RiskLevel={0}, ", aggregatedQueueVerboseDetails.RiskLevel);
				stringBuilder.AppendFormat("ServerIdentity={0}, ", aggregatedQueueVerboseDetails.ServerIdentity);
				stringBuilder.AppendFormat("Status={0}, ", aggregatedQueueVerboseDetails.Status);
				stringBuilder.AppendFormat("TlsDomain={0}, ", aggregatedQueueVerboseDetails.TlsDomain);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00027858 File Offset: 0x00025A58
		private void TraceDebug(string format, params object[] args)
		{
			string text = string.Format(format, args);
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0}] ", text);
			WTFDiagnostics.TraceDebug(ExTraceGlobals.QueueDigestTracer, new TracingContext(), text, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\QueueDigest\\Probes\\QueueDigestProbe.cs", 933);
		}

		// Token: 0x0400074B RID: 1867
		private const int MaxRetryAttempts = 3;

		// Token: 0x0400074C RID: 1868
		private CancellationToken cancellationToken;

		// Token: 0x0400074D RID: 1869
		private List<QueueConfiguration> queues = new List<QueueConfiguration>();

		// Token: 0x0400074E RID: 1870
		private StringBuilder jobDetails = new StringBuilder();

		// Token: 0x0400074F RID: 1871
		private ITopologyConfigurationSession session;

		// Token: 0x020001F6 RID: 502
		private class WebServiceRequestAsyncState
		{
			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06000F57 RID: 3927 RVA: 0x000278CC File Offset: 0x00025ACC
			// (set) Token: 0x06000F58 RID: 3928 RVA: 0x000278D4 File Offset: 0x00025AD4
			public DiagnosticsAggregationServiceClient Client { get; set; }

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06000F59 RID: 3929 RVA: 0x000278DD File Offset: 0x00025ADD
			// (set) Token: 0x06000F5A RID: 3930 RVA: 0x000278E5 File Offset: 0x00025AE5
			public List<ADObjectId> ServersToConnect { get; set; }

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000278EE File Offset: 0x00025AEE
			// (set) Token: 0x06000F5C RID: 3932 RVA: 0x000278F6 File Offset: 0x00025AF6
			public HashSet<ADObjectId> ServersToInclude { get; set; }

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x06000F5D RID: 3933 RVA: 0x000278FF File Offset: 0x00025AFF
			// (set) Token: 0x06000F5E RID: 3934 RVA: 0x00027907 File Offset: 0x00025B07
			public bool IsConnectingToDag { get; set; }

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00027910 File Offset: 0x00025B10
			// (set) Token: 0x06000F60 RID: 3936 RVA: 0x00027918 File Offset: 0x00025B18
			public int ConnectionAttempt { get; set; }

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00027921 File Offset: 0x00025B21
			// (set) Token: 0x06000F62 RID: 3938 RVA: 0x00027929 File Offset: 0x00025B29
			public int WebServicePortNumber { get; set; }

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00027932 File Offset: 0x00025B32
			// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0002793A File Offset: 0x00025B3A
			public QueueConfiguration Config { get; set; }
		}
	}
}

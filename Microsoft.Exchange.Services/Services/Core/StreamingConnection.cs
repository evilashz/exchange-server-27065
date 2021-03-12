using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200023A RID: 570
	internal sealed class StreamingConnection : IDisposeTrackable, IDisposable, ISubscriptionEventHandler
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00048B94 File Offset: 0x00046D94
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00048B9B File Offset: 0x00046D9B
		public static int PeriodicConnectionCheckInterval
		{
			get
			{
				return StreamingConnection.periodicConnectionCheckInterval;
			}
			set
			{
				StreamingConnection.periodicConnectionCheckInterval = value;
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00048BA4 File Offset: 0x00046DA4
		internal static void CreateConnection(CallContext callContext, string[] subscriptionIds, TimeSpan connectionLifetime, CompleteRequestAsyncCallback endRequestCallback)
		{
			StreamingConnection streamingConnection = new StreamingConnection(callContext, endRequestCallback);
			if (subscriptionIds != null)
			{
				streamingConnection.LogSubscriptionInfo("CrteConn", subscriptionIds, new string[]
				{
					RequestDetailsLogger.FormatSubscriptionLogDetails("cnt", subscriptionIds.Count<string>()),
					RequestDetailsLogger.FormatSubscriptionLogDetails("LifeTime", connectionLifetime.TotalSeconds)
				});
			}
			lock (StreamingConnection.openConnections)
			{
				StreamingConnection.openConnections.Add(streamingConnection);
			}
			PerformanceMonitor.UpdateActiveStreamingConnectionsCounter((long)StreamingConnection.openConnections.Count);
			lock (streamingConnection.lockObject)
			{
				streamingConnection.Initialize(subscriptionIds, connectionLifetime);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00048C80 File Offset: 0x00046E80
		internal static List<StreamingConnection> OpenConnections
		{
			get
			{
				List<StreamingConnection> result;
				lock (StreamingConnection.openConnections)
				{
					result = new List<StreamingConnection>(StreamingConnection.openConnections);
				}
				return result;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00048CC8 File Offset: 0x00046EC8
		internal List<StreamingSubscription> Subscriptions
		{
			get
			{
				List<StreamingSubscription> result;
				lock (this.lockObject)
				{
					result = ((this.subscriptions == null) ? null : new List<StreamingSubscription>(this.subscriptions));
				}
				return result;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00048D1C File Offset: 0x00046F1C
		internal string CreatorSmtpAddress
		{
			get
			{
				string result;
				lock (this.lockObject)
				{
					if (this.isDisposed)
					{
						result = null;
					}
					else
					{
						result = ((this.callContext.AccessingPrincipal == null) ? string.Empty : this.callContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
					}
				}
				return result;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00048D9C File Offset: 0x00046F9C
		private StreamingConnection(CallContext context, CompleteRequestAsyncCallback endRequestCallback)
		{
			this.callContext = context;
			this.subscriptions = new List<StreamingSubscription>();
			this.subscriptionsToNotify = new Queue<StreamingSubscription>();
			this.subscriptionsInError = new Dictionary<ServiceError, List<string>>();
			this.endRequestCallback = endRequestCallback;
			this.responseWriter = EwsResponseWireWriter.Create(this.callContext);
			this.disposeTracker = this.GetDisposeTracker();
			if (this.callContext != null && this.callContext.AccessingADUser != null && this.callContext.AccessingADUser.OrganizationId != null && this.callContext.AccessingADUser.OrganizationId.OrganizationalUnit != null)
			{
				this.organizationId = this.callContext.AccessingADUser.OrganizationId.OrganizationalUnit.Name;
			}
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				this.correlationGuid = currentActivityScope.GetProperty(EwsMetadata.CorrelationGuid);
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00048E85 File Offset: 0x00047085
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamingConnection>(this);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00048E8D File Offset: 0x0004708D
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00048EBC File Offset: 0x000470BC
		private void Initialize(string[] subscriptionIds, TimeSpan connectionLifetime)
		{
			int i = 0;
			while (i < subscriptionIds.Length)
			{
				string text = subscriptionIds[i];
				StreamingSubscription streamingSubscription;
				try
				{
					SubscriptionId subscriptionId = SubscriptionId.Parse(text);
					if (!subscriptionId.ServerFQDN.Equals(LocalServer.GetServer().Fqdn, StringComparison.OrdinalIgnoreCase) && subscriptionIds.Length > 1)
					{
						ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "StreamingConnection.Initialize. Subscription [{0}] is from another CAS: {1}", text, subscriptionId.ServerFQDN);
						this.QueueError(text, ServiceErrors.GetServiceError(new ProxyRequestNotAllowedException()));
						goto IL_14F;
					}
					streamingSubscription = (Microsoft.Exchange.Services.Core.Subscriptions.Singleton.Get(subscriptionId.ToString()) as StreamingSubscription);
				}
				catch (InvalidSubscriptionException ex)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, InvalidSubscriptionException>((long)this.GetHashCode(), "StreamingConnection::Initialize. Invalid subscription id [{0}] : {1}", text, ex);
					this.QueueError(text, ServiceErrors.GetServiceError(ex));
					goto IL_14F;
				}
				catch (SubscriptionNotFoundException ex2)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, SubscriptionNotFoundException>((long)this.GetHashCode(), "StreamingConnection::Initialize. Subscription [{0}] not found: {1}", text, ex2);
					this.QueueError(text, ServiceErrors.GetServiceError(ex2));
					goto IL_14F;
				}
				goto IL_D7;
				IL_14F:
				i++;
				continue;
				IL_D7:
				if (streamingSubscription == null)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "StreamingConnection::Initialize. Subscription [{0}] is not a StreamingSubscription.", text);
					this.QueueError(text, ServiceErrors.GetServiceError(new InvalidSubscriptionException()));
					goto IL_14F;
				}
				if (!streamingSubscription.CheckCallerHasRights(this.callContext))
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "StreamingConnection::Initialize. Caller does not have rights on subscription [{0}].", text);
					this.QueueError(text, ServiceErrors.GetServiceError(new SubscriptionAccessDeniedException()));
					goto IL_14F;
				}
				this.subscriptions.Add(streamingSubscription);
				streamingSubscription.RegisterConnection(this);
				goto IL_14F;
			}
			if (!this.CheckSubscriptionsLeftToService())
			{
				this.TryEndConnection(true);
				return;
			}
			List<string> list = new List<string>(this.subscriptions.Count);
			foreach (StreamingSubscription streamingSubscription2 in this.subscriptions)
			{
				if (streamingSubscription2.MailboxId != null)
				{
					list.Add(streamingSubscription2.MailboxId.SmtpAddress);
				}
				else
				{
					list.Add("null");
				}
			}
			this.callContext.ProtocolLog.AppendGenericInfo("SubscribedMailboxes", string.Join("/", list.ToArray()));
			this.connectionExpires = ExDateTime.UtcNow.Add(connectionLifetime);
			this.heartbeatTimer = new Timer(delegate(object a)
			{
				this.EnqueueTask(this.CreateConnectionStatusTask());
			}, this, 0, StreamingConnection.PeriodicConnectionCheckInterval);
			this.connectionTimer = new Timer(delegate(object a)
			{
				this.TryEndConnection(true);
			}, this, (int)connectionLifetime.TotalMilliseconds, -1);
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "StreamingConnection::Initialize. Connection established successfully.");
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00049160 File Offset: 0x00047360
		private bool CheckSubscriptionsLeftToService()
		{
			bool flag = true;
			lock (this.lockObject)
			{
				flag = (this.subscriptions != null && this.subscriptions.Count > 0);
			}
			if (!flag)
			{
				this.LogSubscriptionInfo("NoSubLeft", null, null);
			}
			return flag;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000491C8 File Offset: 0x000473C8
		private void LogSubscriptionInfo(string action, IEnumerable<string> subscriptionIds, params string[] details)
		{
			RequestDetailsLogger.LogSubscriptionInfo(this.correlationGuid, this.organizationId, action, subscriptionIds, details);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000491DE File Offset: 0x000473DE
		private void LogSubscriptionInfo(string action, string subscriptionIds, params string[] details)
		{
			RequestDetailsLogger.LogSubscriptionInfo(this.correlationGuid, this.organizationId, action, subscriptionIds, details);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000491F4 File Offset: 0x000473F4
		public void EventsAvailable(StreamingSubscription subscription)
		{
			lock (this.lockObject)
			{
				if (!this.isDisposed)
				{
					try
					{
						CallContext.SetCurrent(this.callContext);
						if (subscription.IsDisposed)
						{
							this.LogSubscriptionInfo("EventsAvailableSubDisposed", subscription.SubscriptionId, new string[0]);
						}
						else
						{
							if (ExTraceGlobals.SubscriptionsTracer.IsTraceEnabled(TraceType.DebugTrace) && !this.subscriptions.Contains(subscription))
							{
								ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "StreamingConnection.EventsAvailable. Subscription points to this connection, but this connection does not point back.");
							}
							int count = this.subscriptionsToNotify.Count;
							bool flag2 = count == 0;
							this.subscriptionsToNotify.Enqueue(subscription);
							this.LogSubscriptionInfo("EventsAvailable", subscription.SubscriptionId, new string[]
							{
								RequestDetailsLogger.FormatSubscriptionLogDetails("ntfyCnt", count)
							});
							if (flag2)
							{
								this.EnqueueTask(this.CreateNotificationTask());
							}
						}
					}
					finally
					{
						CallContext.SetCurrent(null);
					}
				}
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0004930C File Offset: 0x0004750C
		public void DisconnectSubscription(StreamingSubscription sub, LocalizedException exception)
		{
			lock (this.lockObject)
			{
				if (this.isDisposed)
				{
					return;
				}
				try
				{
					CallContext.SetCurrent(this.callContext);
					if (sub != null)
					{
						bool flag2 = this.subscriptions.Remove(sub);
						if (flag2)
						{
							this.QueueError(sub.SubscriptionId, ServiceErrors.GetServiceError(exception));
						}
						if (!this.CheckSubscriptionsLeftToService())
						{
							this.TryEndConnection(true);
						}
					}
				}
				finally
				{
					CallContext.SetCurrent(null);
				}
			}
			if (sub != null)
			{
				this.LogSubscriptionInfo("DisCntSub", sub.SubscriptionId, new string[]
				{
					(exception != null) ? exception.Message : string.Empty
				});
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000493D8 File Offset: 0x000475D8
		private TaskExecuteResult ExecuteNotificationTask()
		{
			this.processingNotifications = true;
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "StreamingConnection.ExecuteNotificationTask. Executing notification task.");
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
			try
			{
				int i = 0;
				if (this.connectionState != StreamingConnection.ConnectionState.Active)
				{
					foreach (KeyValuePair<string, int> keyValuePair in list)
					{
						this.LogSubscriptionInfo("NtfCnnCls", keyValuePair.Key, new string[]
						{
							string.Format("evtCnt:{0}", keyValuePair.Value)
						});
					}
					return TaskExecuteResult.ProcessingComplete;
				}
				List<EwsNotificationType> list2 = new List<EwsNotificationType>();
				while (i < 100)
				{
					lock (this.lockObject)
					{
						if (this.connectionState != StreamingConnection.ConnectionState.Active)
						{
							return TaskExecuteResult.ProcessingComplete;
						}
						if (this.subscriptionsToNotify.Count == 0)
						{
							break;
						}
						StreamingSubscription streamingSubscription = this.subscriptionsToNotify.Peek();
						try
						{
							int num;
							EwsNotificationType events = streamingSubscription.GetEvents(100 - i, out num);
							if (num != 0)
							{
								list2.Add(events);
								i += num;
							}
							list.Add(new KeyValuePair<string, int>(streamingSubscription.SubscriptionId, num));
							if (!streamingSubscription.CheckForEventsLater())
							{
								this.subscriptionsToNotify.Dequeue();
							}
						}
						catch (LocalizedException ex)
						{
							if (!ex.Data.Contains(StreamingConnection.IsNonFatalSubscriptionExceptionKey) || (string)ex.Data[StreamingConnection.IsNonFatalSubscriptionExceptionKey] != bool.TrueString)
							{
								this.subscriptions.Remove(streamingSubscription);
							}
							if (object.Equals(streamingSubscription, this.subscriptionsToNotify.Peek()))
							{
								this.subscriptionsToNotify.Dequeue();
							}
							this.QueueError(streamingSubscription.SubscriptionId, ServiceErrors.GetServiceError(ex));
							if (!this.CheckSubscriptionsLeftToService())
							{
								this.TryEndConnection(true);
							}
						}
					}
				}
				foreach (KeyValuePair<string, int> keyValuePair2 in list)
				{
					this.LogSubscriptionInfo("SndNtf", keyValuePair2.Key, new string[]
					{
						string.Format("evtCnt:{0}", keyValuePair2.Value)
					});
				}
				if (list2.Count > 0)
				{
					this.SendNotifications(list2);
					PerformanceMonitor.UpdateStreamedEventsCounter((long)i);
				}
			}
			finally
			{
				this.processingNotifications = false;
				if (this.connectionState == StreamingConnection.ConnectionState.Closing)
				{
					this.TryEndConnection(true);
				}
			}
			TaskExecuteResult result;
			lock (this.lockObject)
			{
				if (this.subscriptionsToNotify.Count != 0 && this.connectionState == StreamingConnection.ConnectionState.Active)
				{
					result = TaskExecuteResult.StepComplete;
				}
				else
				{
					result = TaskExecuteResult.ProcessingComplete;
				}
			}
			return result;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0004971C File Offset: 0x0004791C
		private TaskExecuteResult ExecuteErrorTask()
		{
			this.processingErrors = true;
			bool flag = false;
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "StreamingConnection.ExecuteErrorTask. Executing error task.");
			try
			{
				if (this.connectionState == StreamingConnection.ConnectionState.Closed)
				{
					this.LogSubscriptionInfo("ErrCnnCls", string.Empty, new string[]
					{
						string.Format("ErrSubCnt={0}", this.subscriptionsInError.Count)
					});
					return TaskExecuteResult.ProcessingComplete;
				}
				KeyValuePair<ServiceError, List<string>> keyValuePair;
				lock (this.lockObject)
				{
					if (this.subscriptionsInError.Count == 0)
					{
						return TaskExecuteResult.ProcessingComplete;
					}
					keyValuePair = this.subscriptionsInError.ElementAt(0);
					this.subscriptionsInError.Remove(keyValuePair.Key);
				}
				GetStreamingEventsSoapResponse getStreamingEventsSoapResponse = new GetStreamingEventsSoapResponse();
				getStreamingEventsSoapResponse.Body = StreamingConnection.CreateErrorResponse(keyValuePair.Key, keyValuePair.Value);
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, int>((long)this.GetHashCode(), "StreamingConnection.ExecuteErrorTask. Sending error '{0}' for {1} subscriptions.", keyValuePair.Key.MessageText, keyValuePair.Value.Count);
				this.LogSubscriptionInfo("ErrNtf", keyValuePair.Value, new string[]
				{
					keyValuePair.Key.MessageText
				});
				this.WriteResponseToWire(getStreamingEventsSoapResponse, true);
			}
			finally
			{
				this.processingErrors = false;
				lock (this.lockObject)
				{
					flag = (this.subscriptionsInError.Count > 0);
				}
				if (!flag && this.connectionState == StreamingConnection.ConnectionState.Closing)
				{
					this.TryEndConnection(true);
				}
			}
			if (flag)
			{
				return TaskExecuteResult.StepComplete;
			}
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0004990C File Offset: 0x00047B0C
		private void WriteResponseToWire(GetStreamingEventsSoapResponse soapResponse, bool tryEndConnectionOnFailure)
		{
			Exception ex = null;
			if (this.responseWriter != null)
			{
				try
				{
					this.responseWriter.WriteResponseToWire(soapResponse, !tryEndConnectionOnFailure);
				}
				catch (HttpException ex2)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<HttpException>((long)this.GetHashCode(), "StreamingConnection.WriteResponseToWire. Encountered exception: {0}", ex2);
					if (tryEndConnectionOnFailure)
					{
						this.TryEndConnection(false);
					}
					ex = ex2;
				}
				catch (InvalidOperationException ex3)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<InvalidOperationException>((long)this.GetHashCode(), "StreamingConnection.WriteResponseToWire. Encountered exception: {0}", ex3);
					if (tryEndConnectionOnFailure)
					{
						this.TryEndConnection(false);
					}
					ex = ex3;
				}
				if (ex != null)
				{
					this.LogSubscriptionInfo("WrtRspFailed", null, new string[]
					{
						RequestDetailsLogger.FormatSubscriptionLogDetails("ex", ex),
						RequestDetailsLogger.FormatSubscriptionLogDetails("tryEndCnn", tryEndConnectionOnFailure)
					});
				}
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000499D8 File Offset: 0x00047BD8
		private TaskExecuteResult ExecuteConnectionStatusTask()
		{
			if (this.connectionState == StreamingConnection.ConnectionState.Active && ExDateTime.Now.AddSeconds(1.0).CompareTo(this.connectionExpires) < 0)
			{
				GetStreamingEventsSoapResponse getStreamingEventsSoapResponse = new GetStreamingEventsSoapResponse();
				getStreamingEventsSoapResponse.Body = StreamingConnection.CreateConnectionResponse(ConnectionStatus.OK);
				this.LogSubscriptionInfo("ConnStatus", null, null);
				this.WriteResponseToWire(getStreamingEventsSoapResponse, true);
			}
			else if (this.connectionState == StreamingConnection.ConnectionState.Closing)
			{
				this.LogSubscriptionInfo("ConnStatusCls", null, null);
				this.TryEndConnection(true);
			}
			else
			{
				string action = "ConnStatusOther";
				string subscriptionIds = null;
				string[] array = new string[1];
				string[] array2 = array;
				int num = 0;
				int num2 = (int)this.connectionState;
				array2[num] = num2.ToString();
				this.LogSubscriptionInfo(action, subscriptionIds, array);
			}
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00049A84 File Offset: 0x00047C84
		private void EnqueueTask(ITask task)
		{
			lock (this.lockObject)
			{
				if (task != null && !this.isDisposed && !UserWorkloadManager.Singleton.TrySubmitNewTask(task))
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), ".StreamingConnection.EnqueueTask. UserWorkloadManager rejected a request to enqueue a task. Shutting down the connection.");
					this.TryEndConnection(false);
				}
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00049AF4 File Offset: 0x00047CF4
		private void QueueError(string currentSubscriptionId, ServiceError serviceError)
		{
			bool flag2;
			lock (this.lockObject)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<ServiceError, string>((long)this.GetHashCode(), "StreamingConnection.QueueError. Queuing error {0} for subscription {1}.", serviceError, currentSubscriptionId);
				flag2 = (this.subscriptionsInError.Count == 0);
				List<string> list;
				if (!this.subscriptionsInError.TryGetValue(serviceError, out list))
				{
					list = new List<string>();
					this.subscriptionsInError.Add(serviceError, list);
				}
				if (!string.IsNullOrEmpty(currentSubscriptionId))
				{
					list.Add(currentSubscriptionId);
				}
			}
			this.LogSubscriptionInfo("QErr", currentSubscriptionId, new string[]
			{
				serviceError.MessageText
			});
			if (flag2)
			{
				this.EnqueueTask(this.CreateErrorTask());
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00049BB8 File Offset: 0x00047DB8
		private ITask CreateNotificationTask()
		{
			return this.CreateTask(new StreamingConnectionTask.StreamingConnectionExecuteDelegate(this.ExecuteNotificationTask), "Notification");
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00049BD1 File Offset: 0x00047DD1
		private ITask CreateErrorTask()
		{
			return this.CreateTask(new StreamingConnectionTask.StreamingConnectionExecuteDelegate(this.ExecuteErrorTask), "Error");
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00049BEA File Offset: 0x00047DEA
		private ITask CreateConnectionStatusTask()
		{
			return this.CreateTask(new StreamingConnectionTask.StreamingConnectionExecuteDelegate(this.ExecuteConnectionStatusTask), "ConnectionStatus");
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00049C04 File Offset: 0x00047E04
		private ITask CreateTask(StreamingConnectionTask.StreamingConnectionExecuteDelegate executeCallback, string taskType)
		{
			ITask result;
			lock (this.lockObject)
			{
				if (!this.isDisposed && this.callContext.Budget != null)
				{
					try
					{
						return new StreamingConnectionTask(this, this.callContext, executeCallback, taskType)
						{
							State = null
						};
					}
					catch (OverBudgetException ex)
					{
						ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[StreamingConnection::CreateTask] Failed to create task due to OverBudgetException. (Message: {0}) (Snapshot: {1})", ex.Message, ex.Snapshot);
						return null;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00049CA8 File Offset: 0x00047EA8
		private void SendNotifications(List<EwsNotificationType> notifications)
		{
			this.WriteResponseToWire(new GetStreamingEventsSoapResponse
			{
				Body = this.CreateNotificationResponse(notifications)
			}, true);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00049CD0 File Offset: 0x00047ED0
		private GetStreamingEventsResponse CreateNotificationResponse(List<EwsNotificationType> notifications)
		{
			GetStreamingEventsResponse getStreamingEventsResponse = new GetStreamingEventsResponse();
			GetStreamingEventsResponseMessage getStreamingEventsResponseMessage = new GetStreamingEventsResponseMessage(ServiceResultCode.Success, null);
			getStreamingEventsResponseMessage.AddNotifications(notifications);
			getStreamingEventsResponse.AddResponse(getStreamingEventsResponseMessage);
			return getStreamingEventsResponse;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00049CFC File Offset: 0x00047EFC
		internal static GetStreamingEventsResponse CreateConnectionResponse(ConnectionStatus status)
		{
			GetStreamingEventsResponse getStreamingEventsResponse = new GetStreamingEventsResponse();
			GetStreamingEventsResponseMessage getStreamingEventsResponseMessage = new GetStreamingEventsResponseMessage(ServiceResultCode.Success, null);
			getStreamingEventsResponseMessage.SetConnectionStatus(status);
			getStreamingEventsResponse.AddResponse(getStreamingEventsResponseMessage);
			return getStreamingEventsResponse;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00049D28 File Offset: 0x00047F28
		internal static GetStreamingEventsResponse CreateErrorResponse(ServiceError error, IEnumerable<string> idsInError)
		{
			GetStreamingEventsResponse getStreamingEventsResponse = new GetStreamingEventsResponse();
			GetStreamingEventsResponseMessage getStreamingEventsResponseMessage = new GetStreamingEventsResponseMessage(ServiceResultCode.Error, error);
			getStreamingEventsResponseMessage.AddErrorSubscriptionIds(idsInError);
			getStreamingEventsResponse.AddResponse(getStreamingEventsResponseMessage);
			return getStreamingEventsResponse;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00049D52 File Offset: 0x00047F52
		internal void TryEndConnection(LocalizedException ex)
		{
			this.QueueError(null, ServiceErrors.GetServiceError(ex, ExchangeVersion.Current));
			this.TryEndConnection(true);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00049D70 File Offset: 0x00047F70
		internal void TryEndConnection(bool waitForErrors)
		{
			bool flag = false;
			HttpException ex = null;
			lock (this.lockObject)
			{
				if (!this.isDisposed)
				{
					if (this.connectionState == StreamingConnection.ConnectionState.Active)
					{
						this.connectionState = StreamingConnection.ConnectionState.Closing;
					}
					if (this.processingNotifications || this.processingErrors)
					{
						return;
					}
					if (waitForErrors && this.callContext.HttpContext.Response.IsClientConnected && this.subscriptionsInError.Count > 0)
					{
						return;
					}
					if (this.connectionState != StreamingConnection.ConnectionState.Closed)
					{
						GetStreamingEventsSoapResponse getStreamingEventsSoapResponse = new GetStreamingEventsSoapResponse();
						getStreamingEventsSoapResponse.Body = StreamingConnection.CreateConnectionResponse(ConnectionStatus.Closed);
						try
						{
							ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "StreamingConnection.TryEndConnection. Writing disconnect response.");
							this.WriteResponseToWire(getStreamingEventsSoapResponse, false);
							this.responseWriter.FinishWritesAndCompleteResponse(this.endRequestCallback);
							flag = true;
						}
						catch (HttpException ex2)
						{
							ExTraceGlobals.SubscriptionsTracer.TraceDebug<HttpException>((long)this.GetHashCode(), "StreamingConnection.TryEndConnection. Exception occurred while closing connection: {0}", ex2);
							ex = ex2;
							this.endRequestCallback(ex2);
						}
						this.Dispose();
					}
				}
			}
			if (flag)
			{
				this.LogSubscriptionInfo("EndConnSuccess", null, null);
				return;
			}
			if (ex != null)
			{
				this.LogSubscriptionInfo("EndConnFailed", null, new string[]
				{
					RequestDetailsLogger.FormatSubscriptionLogDetails("ex", ex)
				});
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00049ED0 File Offset: 0x000480D0
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00049EEC File Offset: 0x000480EC
		private void Dispose(bool suppressFinalize)
		{
			if (suppressFinalize)
			{
				GC.SuppressFinalize(this);
			}
			lock (this.lockObject)
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					lock (StreamingConnection.openConnections)
					{
						StreamingConnection.openConnections.Remove(this);
					}
					PerformanceMonitor.UpdateActiveStreamingConnectionsCounter((long)StreamingConnection.openConnections.Count);
					this.connectionState = StreamingConnection.ConnectionState.Closed;
					if (this.heartbeatTimer != null)
					{
						this.heartbeatTimer.Dispose();
						this.heartbeatTimer = null;
					}
					if (this.connectionTimer != null)
					{
						this.connectionTimer.Dispose();
						this.connectionTimer = null;
					}
					if (this.subscriptions != null)
					{
						foreach (StreamingSubscription streamingSubscription in this.subscriptions)
						{
							streamingSubscription.UnregisterConnection(this);
						}
						this.subscriptions = null;
					}
					if (this.responseWriter != null)
					{
						this.responseWriter.Dispose();
						this.responseWriter = null;
					}
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0004A030 File Offset: 0x00048230
		public bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x04000B6A RID: 2922
		private const int MaxEventsPerTask = 100;

		// Token: 0x04000B6B RID: 2923
		internal const int DefaultPeriodicConnectionCheckInterval = 45000;

		// Token: 0x04000B6C RID: 2924
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000B6D RID: 2925
		internal static string IsNonFatalSubscriptionExceptionKey = "IsNonFatalSubscriptionException";

		// Token: 0x04000B6E RID: 2926
		private static int periodicConnectionCheckInterval = 45000;

		// Token: 0x04000B6F RID: 2927
		private static List<StreamingConnection> openConnections = new List<StreamingConnection>();

		// Token: 0x04000B70 RID: 2928
		private List<StreamingSubscription> subscriptions;

		// Token: 0x04000B71 RID: 2929
		private CallContext callContext;

		// Token: 0x04000B72 RID: 2930
		private Queue<StreamingSubscription> subscriptionsToNotify;

		// Token: 0x04000B73 RID: 2931
		private Dictionary<ServiceError, List<string>> subscriptionsInError;

		// Token: 0x04000B74 RID: 2932
		private ExDateTime connectionExpires;

		// Token: 0x04000B75 RID: 2933
		private Timer heartbeatTimer;

		// Token: 0x04000B76 RID: 2934
		private Timer connectionTimer;

		// Token: 0x04000B77 RID: 2935
		private EwsResponseWireWriter responseWriter;

		// Token: 0x04000B78 RID: 2936
		private CompleteRequestAsyncCallback endRequestCallback;

		// Token: 0x04000B79 RID: 2937
		private StreamingConnection.ConnectionState connectionState;

		// Token: 0x04000B7A RID: 2938
		private bool processingNotifications;

		// Token: 0x04000B7B RID: 2939
		private bool processingErrors;

		// Token: 0x04000B7C RID: 2940
		private object lockObject = new object();

		// Token: 0x04000B7D RID: 2941
		private string organizationId;

		// Token: 0x04000B7E RID: 2942
		private string correlationGuid;

		// Token: 0x04000B7F RID: 2943
		private bool isDisposed;

		// Token: 0x0200023B RID: 571
		private enum ConnectionState
		{
			// Token: 0x04000B81 RID: 2945
			Active,
			// Token: 0x04000B82 RID: 2946
			Closing,
			// Token: 0x04000B83 RID: 2947
			Closed
		}
	}
}

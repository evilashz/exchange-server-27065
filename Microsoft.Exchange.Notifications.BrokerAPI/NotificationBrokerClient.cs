using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.NotificationsBroker;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000004 RID: 4
	internal class NotificationBrokerClient : DisposeTrackableBase, INotificationBrokerClient, IDisposable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000220A File Offset: 0x0000040A
		public NotificationBrokerClient() : this(Consumer.Current.ConsumerId)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000221C File Offset: 0x0000041C
		public NotificationBrokerClient(ConsumerId consumerId)
		{
			this.consumerId = consumerId;
			this.logger = NotificationBrokerClient.GetDefaultLogger();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002268 File Offset: 0x00000468
		public void Subscribe(BrokerSubscription subscription)
		{
			ArgumentValidator.ThrowIfNull("subscription", subscription);
			this.LogLatency("Subscribe", delegate(NotificationBrokerClientLogEvent logEvent)
			{
				this.SubscribeInternal(subscription, logEvent);
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022CC File Offset: 0x000004CC
		public void Unsubscribe(BrokerSubscription subscription)
		{
			ArgumentValidator.ThrowIfNull("subscription", subscription);
			this.LogLatency("Unsubscribe", delegate(NotificationBrokerClientLogEvent logEvent)
			{
				this.UnsubscribeInternal(subscription, logEvent);
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002330 File Offset: 0x00000530
		public void StartNotificationCallbacks(Action<BrokerNotification> notificationCallback)
		{
			ArgumentValidator.ThrowIfNull("notificationCallback", notificationCallback);
			this.LogLatency("StartNotificationCallbacks", delegate(NotificationBrokerClientLogEvent logEvent)
			{
				this.StartNotificationCallbacksInternal(notificationCallback, logEvent);
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002381 File Offset: 0x00000581
		public void StopNotificationCallbacks()
		{
			this.LogLatency("StopNotificationCallbacks", delegate(NotificationBrokerClientLogEvent logEvent)
			{
				this.StopNotificationCallbacksInternal(logEvent);
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000239C File Offset: 0x0000059C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				lock (this.mutex)
				{
					this.StopNotificationCallbacks();
					while (this.rpcClientPool.Count > 0)
					{
						this.rpcClientPool.Dequeue().Dispose();
					}
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002400 File Offset: 0x00000600
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationBrokerClient>(this);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002408 File Offset: 0x00000608
		private static IExtensibleLogger GetDefaultLogger()
		{
			if (NotificationBrokerClient.defaultLogger == null)
			{
				lock (NotificationBrokerClient.defaultLoggerLock)
				{
					if (NotificationBrokerClient.defaultLogger == null)
					{
						NotificationBrokerClient.defaultLogger = new NotificationBrokerClientLogger();
						AppDomain.CurrentDomain.DomainUnload += NotificationBrokerClient.DisposeDefaultLogger;
					}
				}
			}
			return NotificationBrokerClient.defaultLogger;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002474 File Offset: 0x00000674
		private static void DisposeDefaultLogger(object sender, EventArgs e)
		{
			lock (NotificationBrokerClient.defaultLoggerLock)
			{
				if (NotificationBrokerClient.defaultLogger != null)
				{
					NotificationBrokerClient.defaultLogger.Dispose();
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024C0 File Offset: 0x000006C0
		private static void GetNextNotificationCallback(IAsyncResult asyncResult)
		{
			NotificationBrokerClient notificationBrokerClient = (NotificationBrokerClient)asyncResult.AsyncState;
			notificationBrokerClient.OnNotification(asyncResult);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024E0 File Offset: 0x000006E0
		private void LogLatency(string action, Action<NotificationBrokerClientLogEvent> operation)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("action", action);
			NotificationBrokerClientLogEvent notificationBrokerClientLogEvent = new NotificationBrokerClientLogEvent(this.consumerId, action);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				operation(notificationBrokerClientLogEvent);
			}
			finally
			{
				notificationBrokerClientLogEvent.Latency = stopwatch.ElapsedMilliseconds;
				this.logger.LogEvent(notificationBrokerClientLogEvent);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002540 File Offset: 0x00000740
		private void SubscribeInternal(BrokerSubscription subscription, NotificationBrokerClientLogEvent logEvent)
		{
			logEvent.SubscriptionId = new Guid?(subscription.SubscriptionId);
			NotificationsBrokerRpcClient notificationsBrokerRpcClient = null;
			try
			{
				notificationsBrokerRpcClient = this.GetRpcClient();
				string subscription2 = subscription.ToJson();
				ICancelableAsyncResult asyncResult = notificationsBrokerRpcClient.BeginSubscribe(null, null, subscription2, null, null);
				BrokerStatus brokerStatus = notificationsBrokerRpcClient.EndSubscribe(asyncResult);
				logEvent.Status = new BrokerStatus?(brokerStatus);
				if (brokerStatus != BrokerStatus.Success)
				{
					throw new NotificationsBrokerStatusException(brokerStatus);
				}
			}
			catch (Exception exception)
			{
				logEvent.Exception = exception;
				if (notificationsBrokerRpcClient != null)
				{
					notificationsBrokerRpcClient.Dispose();
					notificationsBrokerRpcClient = null;
				}
				throw;
			}
			finally
			{
				if (notificationsBrokerRpcClient != null)
				{
					this.ReleaseRpcClient(notificationsBrokerRpcClient);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025DC File Offset: 0x000007DC
		private void UnsubscribeInternal(BrokerSubscription subscription, NotificationBrokerClientLogEvent logEvent)
		{
			logEvent.SubscriptionId = new Guid?(subscription.SubscriptionId);
			NotificationsBrokerRpcClient notificationsBrokerRpcClient = null;
			try
			{
				notificationsBrokerRpcClient = this.GetRpcClient();
				subscription.Parameters = null;
				subscription.Receiver = null;
				string subscription2 = subscription.ToJson();
				ICancelableAsyncResult asyncResult = notificationsBrokerRpcClient.BeginUnsubscribe(null, null, subscription2, null, null);
				BrokerStatus brokerStatus = notificationsBrokerRpcClient.EndUnsubscribe(asyncResult);
				logEvent.Status = new BrokerStatus?(brokerStatus);
				if (brokerStatus != BrokerStatus.Success)
				{
					throw new NotificationsBrokerStatusException(brokerStatus);
				}
			}
			catch (Exception exception)
			{
				logEvent.Exception = exception;
				if (notificationsBrokerRpcClient != null)
				{
					notificationsBrokerRpcClient.Dispose();
					notificationsBrokerRpcClient = null;
				}
				throw;
			}
			finally
			{
				if (notificationsBrokerRpcClient != null)
				{
					this.ReleaseRpcClient(notificationsBrokerRpcClient);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002684 File Offset: 0x00000884
		private void StartNotificationCallbacksInternal(Action<BrokerNotification> notificationCallback, NotificationBrokerClientLogEvent logEvent)
		{
			lock (this.mutex)
			{
				if (this.notificationCallback != null)
				{
					throw new NotificationsBrokerException(ClientAPIStrings.CallbackAlreadyRegistered);
				}
				this.hangingRpcClient = this.GetRpcClient();
				this.notificationCallback = notificationCallback;
				this.CallGetNextNotification();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026EC File Offset: 0x000008EC
		private void StopNotificationCallbacksInternal(NotificationBrokerClientLogEvent logEvent)
		{
			lock (this.mutex)
			{
				if (this.retryTimer != null)
				{
					this.retryTimer.Dispose(true);
					this.retryTimer = null;
				}
				this.notificationCallback = null;
				if (this.hangingAsyncResult != null)
				{
					this.hangingAsyncResult.Cancel();
					this.hangingAsyncResult = null;
				}
				if (this.hangingRpcClient != null)
				{
					this.ReleaseRpcClient(this.hangingRpcClient);
					this.hangingRpcClient = null;
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002780 File Offset: 0x00000980
		private NotificationsBrokerRpcClient GetRpcClient()
		{
			NotificationsBrokerRpcClient result;
			lock (this.mutex)
			{
				NotificationsBrokerRpcClient notificationsBrokerRpcClient;
				if (this.rpcClientPool.Count == 0)
				{
					RpcBindingInfo rpcBindingInfo = new RpcBindingInfo();
					rpcBindingInfo.ProtocolSequence = "ncalrpc";
					rpcBindingInfo.DefaultOmittedProperties();
					notificationsBrokerRpcClient = new NotificationsBrokerRpcClient(rpcBindingInfo);
				}
				else
				{
					notificationsBrokerRpcClient = this.rpcClientPool.Dequeue();
				}
				result = notificationsBrokerRpcClient;
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027FC File Offset: 0x000009FC
		private void ReleaseRpcClient(NotificationsBrokerRpcClient rpcClient)
		{
			lock (this.mutex)
			{
				if (this.rpcClientPool.Count < 100)
				{
					this.rpcClientPool.Enqueue(rpcClient);
				}
				else
				{
					rpcClient.Dispose();
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000285C File Offset: 0x00000A5C
		private void CallGetNextNotification()
		{
			lock (this.mutex)
			{
				this.hangingAsyncResult = this.hangingRpcClient.BeginGetNextNotification(null, null, (int)this.consumerId, this.lastNotificationId, new CancelableAsyncCallback(NotificationBrokerClient.GetNextNotificationCallback), this);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B6C File Offset: 0x00000D6C
		private void OnNotification(IAsyncResult asyncResult)
		{
			string notificationJson = null;
			Action<BrokerNotification> callbackToCall = null;
			BrokerNotification notification = null;
			this.LogLatency("ReceiveNotification", delegate(NotificationBrokerClientLogEvent logEvent)
			{
				lock (this.mutex)
				{
					if (this.hangingRpcClient != null)
					{
						BrokerStatus brokerStatus = BrokerStatus.None;
						try
						{
							brokerStatus = this.hangingRpcClient.EndGetNextNotification((ICancelableAsyncResult)asyncResult, out notificationJson);
							this.hangingAsyncResult = null;
							if (brokerStatus == BrokerStatus.Success)
							{
								notification = BrokerNotification.FromJson(notificationJson);
								this.lastNotificationId = notification.NotificationId;
								callbackToCall = this.notificationCallback;
								logEvent.SubscriptionId = new Guid?(notification.SubscriptionId);
								logEvent.NotificationId = new Guid?(notification.NotificationId);
								logEvent.SequenceId = new int?(notification.SequenceNumber);
							}
						}
						catch (RpcException ex)
						{
							logEvent.Exception = ex;
							brokerStatus = BrokerStatus.UnknownError;
							ExTraceGlobals.ClientTracer.TraceError<RpcException>((long)this.GetHashCode(), "NotificationsBroker.OnNotification exception: {0}", ex);
						}
						logEvent.Status = new BrokerStatus?(brokerStatus);
						if (this.notificationCallback != null)
						{
							if (brokerStatus == BrokerStatus.Success)
							{
								this.CallGetNextNotification();
							}
							else if (this.retryTimer == null)
							{
								this.retryTimer = new GuardedTimer(delegate(object param0)
								{
									lock (this.mutex)
									{
										if (this.notificationCallback != null)
										{
											this.CallGetNextNotification();
										}
										this.retryTimer.Dispose(false);
										this.retryTimer = null;
									}
								}, null, TimeSpan.FromSeconds(1.0), TimeSpan.FromMilliseconds(-1.0));
							}
						}
					}
				}
			});
			if (callbackToCall != null && notificationJson != null)
			{
				this.LogLatency("ProcessNotification", delegate(NotificationBrokerClientLogEvent logEvent)
				{
					logEvent.SubscriptionId = new Guid?(notification.SubscriptionId);
					logEvent.NotificationId = new Guid?(notification.NotificationId);
					logEvent.SequenceId = new int?(notification.SequenceNumber);
					this.notificationCallback(notification);
				});
			}
		}

		// Token: 0x04000003 RID: 3
		private static readonly object defaultLoggerLock = new object();

		// Token: 0x04000004 RID: 4
		private static NotificationBrokerClientLogger defaultLogger = null;

		// Token: 0x04000005 RID: 5
		private readonly IExtensibleLogger logger;

		// Token: 0x04000006 RID: 6
		private object mutex = new object();

		// Token: 0x04000007 RID: 7
		private Queue<NotificationsBrokerRpcClient> rpcClientPool = new Queue<NotificationsBrokerRpcClient>();

		// Token: 0x04000008 RID: 8
		private NotificationsBrokerRpcClient hangingRpcClient;

		// Token: 0x04000009 RID: 9
		private ConsumerId consumerId;

		// Token: 0x0400000A RID: 10
		private Guid lastNotificationId;

		// Token: 0x0400000B RID: 11
		private Action<BrokerNotification> notificationCallback;

		// Token: 0x0400000C RID: 12
		private ICancelableAsyncResult hangingAsyncResult;

		// Token: 0x0400000D RID: 13
		private GuardedTimer retryTimer;
	}
}

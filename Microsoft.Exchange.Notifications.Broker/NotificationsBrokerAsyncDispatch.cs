using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001A RID: 26
	internal sealed class NotificationsBrokerAsyncDispatch : INotificationsBrokerAsyncDispatch
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000066F0 File Offset: 0x000048F0
		public ICancelableAsyncResult BeginSubscribe(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string subscriptionJson, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "BeginSubscribe start");
			ExTraceGlobals.ServiceTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Subscription JSON: {0}", subscriptionJson);
			ICancelableAsyncResult result = this.AsyncBeginSubscribe(subscriptionJson).AsApm(asyncCallback, asyncState);
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "BeginSubscribe end");
			return result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000694C File Offset: 0x00004B4C
		public async Task<BrokerStatus> AsyncBeginSubscribe(string subscriptionJson)
		{
			BrokerStatus result;
			using (BrokerLogger.StartEvent("Subscribe"))
			{
				BrokerStatus status = BrokerStatus.None;
				try
				{
					BrokerLogger.Set(LogField.RequestTime, ExDateTime.UtcNow.ToString("O"));
					await this.AsyncSubscribe(subscriptionJson);
					status = BrokerStatus.Success;
				}
				catch (Exception ex)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Exception caught: {0}", ex.ToString());
					BrokerLogger.Set(LogField.Exception, ex);
					status = BrokerStatus.UnknownError;
				}
				finally
				{
					BrokerLogger.Set(LogField.BrokerStatus, status);
				}
				result = status;
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006B20 File Offset: 0x00004D20
		public async Task AsyncSubscribe(string subscriptionJson)
		{
			BrokerSubscription subscription = BrokerSubscription.FromJson(subscriptionJson);
			BrokerLogger.Set(LogField.ConsumerId, subscription.ConsumerId);
			if (Generator.Singleton.MailboxIsHostedLocally(subscription.Sender.DatabaseGuid, subscription.Sender.MailboxGuid))
			{
				Generator.Singleton.Subscribe(subscription);
			}
			else
			{
				RemoteCommandStatus status = await RemoteConduit.Singleton.ForwardSubscribeAsync(subscription);
				BrokerLogger.Set(LogField.RemoteConduitStatus, status.ToString());
				if (status != RemoteCommandStatus.Success)
				{
					throw new NotificationsBrokerStatusException(BrokerStatus.UnknownError);
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006B70 File Offset: 0x00004D70
		public BrokerStatus EndSubscribe(ICancelableAsyncResult asyncResult)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "EndSubscribe start");
			CancelableAsyncResultWrapper<BrokerStatus> cancelableAsyncResultWrapper = asyncResult as CancelableAsyncResultWrapper<BrokerStatus>;
			Task<BrokerStatus> task = cancelableAsyncResultWrapper.Task;
			BrokerStatus result = task.Result;
			ExTraceGlobals.ServiceTracer.TraceDebug<BrokerStatus>((long)this.GetHashCode(), "EndSubscribe end, status = {0}", result);
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public ICancelableAsyncResult BeginUnsubscribe(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string subscriptionJson, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "BeginUnsubscribe start");
			ExTraceGlobals.ServiceTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Subscription JSON: {0}", subscriptionJson);
			ICancelableAsyncResult result = this.AsyncBeginUnsubscribe(subscriptionJson).AsApm(asyncCallback, asyncState);
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "BeginUnsubscribe end");
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006E1C File Offset: 0x0000501C
		public async Task<BrokerStatus> AsyncBeginUnsubscribe(string subscriptionJson)
		{
			BrokerStatus result;
			using (BrokerLogger.StartEvent("Unsubscribe"))
			{
				BrokerStatus status = BrokerStatus.None;
				try
				{
					BrokerLogger.Set(LogField.RequestTime, ExDateTime.UtcNow.ToString("O"));
					await this.AsyncUnsubscribe(subscriptionJson);
					status = BrokerStatus.Success;
				}
				catch (Exception ex)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Exception caught: {0}", ex.ToString());
					BrokerLogger.Set(LogField.Exception, ex);
					status = BrokerStatus.UnknownError;
				}
				finally
				{
					BrokerLogger.Set(LogField.BrokerStatus, status);
				}
				result = status;
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006FA8 File Offset: 0x000051A8
		public async Task AsyncUnsubscribe(string subscriptionJson)
		{
			BrokerSubscription subscription = BrokerSubscription.FromJson(subscriptionJson);
			BrokerLogger.Set(LogField.ConsumerId, subscription.ConsumerId);
			if (Generator.Singleton.MailboxIsHostedLocally(subscription.Sender.DatabaseGuid, subscription.Sender.MailboxGuid))
			{
				Generator.Singleton.Unsubscribe(subscription);
			}
			else
			{
				await RemoteConduit.Singleton.ForwardUnsubscribeAsync(subscription);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006FF8 File Offset: 0x000051F8
		public BrokerStatus EndUnsubscribe(ICancelableAsyncResult asyncResult)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "EndUnsubscribe start");
			CancelableAsyncResultWrapper<BrokerStatus> cancelableAsyncResultWrapper = asyncResult as CancelableAsyncResultWrapper<BrokerStatus>;
			Task<BrokerStatus> task = cancelableAsyncResultWrapper.Task;
			BrokerStatus result = task.Result;
			ExTraceGlobals.ServiceTracer.TraceDebug<BrokerStatus>((long)this.GetHashCode(), "EndUnsubscribe end, status = {0}", result);
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007194 File Offset: 0x00005394
		public ICancelableAsyncResult BeginGetNextNotification(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, int consumerId, Guid ackNotificationId, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<int, string>((long)this.GetHashCode(), "BeginGetNextNotification start, consumerId = {0}, ackNotificationId = {1}", consumerId, ackNotificationId.ToString("D"));
			BrokerAsyncResult asyncResult = new BrokerAsyncResult();
			asyncResult.AsyncState = asyncState;
			asyncResult.Callback = asyncCallback;
			ActivityScope scopeForGetNextEvent = null;
			try
			{
				scopeForGetNextEvent = BrokerLogger.StartEvent("GetNextNotification");
				BrokerLogger.Set(LogField.RequestTime, ExDateTime.UtcNow.ToString("O"));
				BrokerLogger.Set(LogField.ConsumerId, consumerId.ToString());
				if (this.hangingRequests[consumerId] != null)
				{
					ExTraceGlobals.ServiceTracer.TraceWarning((long)this.GetHashCode(), "Terminating a previous hanging call");
					BrokerAsyncResult brokerAsyncResult = this.hangingRequests[consumerId];
					this.hangingRequests[consumerId] = null;
					brokerAsyncResult.Result = BrokerStatus.Cancelled;
					brokerAsyncResult.IsCompleted = true;
					brokerAsyncResult.Callback(brokerAsyncResult);
				}
				this.hangingRequests[consumerId] = asyncResult;
				ActivityContext.ClearThreadScope();
				LocalMultiQueue.Singleton.Get((ConsumerId)consumerId, ackNotificationId, delegate(BrokerNotification notification)
				{
					IActivityScope activityScope = null;
					try
					{
						ExTraceGlobals.ServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "LocalMultiQueue.Singleton.Get callback, consumerId = {0}", consumerId);
						activityScope = ActivityContext.GetCurrentActivityScope();
						ActivityContext.SetThreadScope(scopeForGetNextEvent);
						this.hangingRequests[consumerId] = null;
						asyncResult.NotificationJson = notification.ToJson();
						asyncResult.Result = BrokerStatus.Success;
						asyncResult.IsCompleted = true;
						asyncResult.Callback(asyncResult);
					}
					catch (Exception ex2)
					{
						ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "Calling RPC callback with error code because we caught an exception");
						BrokerLogger.Set(LogField.Exception, ex2.ToString());
						asyncResult.Result = BrokerStatus.UnknownError;
						asyncResult.IsCompleted = true;
						asyncResult.Callback(asyncResult);
						throw;
					}
					finally
					{
						scopeForGetNextEvent.Dispose();
						scopeForGetNextEvent = null;
						if (activityScope != null)
						{
							ActivityContext.SetThreadScope(activityScope);
							activityScope = null;
						}
						ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "LocalMultiQueue.Singleton.Get callback end");
					}
				});
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Exception caught: {0}", ex.ToString());
				BrokerLogger.Set(LogField.Exception, ex.ToString());
				if (scopeForGetNextEvent != null)
				{
					scopeForGetNextEvent.Dispose();
				}
			}
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "BeginGetNextNotification end");
			return asyncResult;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007348 File Offset: 0x00005548
		public BrokerStatus EndGetNextNotification(ICancelableAsyncResult asyncResult, out string notificationJson)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "EndGetNextNotification start");
			BrokerAsyncResult brokerAsyncResult = asyncResult as BrokerAsyncResult;
			if (!brokerAsyncResult.IsCompleted)
			{
				brokerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			notificationJson = brokerAsyncResult.NotificationJson;
			ExTraceGlobals.ServiceTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Notification JSON: {0}", notificationJson);
			ExTraceGlobals.ServiceTracer.TraceDebug<BrokerStatus>((long)this.GetHashCode(), "EndGetNextNotification end, status = {0}", brokerAsyncResult.Result);
			return brokerAsyncResult.Result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000073CC File Offset: 0x000055CC
		public void StopGetNextNotificationCalls()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "StopGetNextNotificationCalls start");
			for (int i = 0; i < 3; i++)
			{
				if (this.hangingRequests[i] != null)
				{
					BrokerAsyncResult brokerAsyncResult = this.hangingRequests[i];
					this.hangingRequests[i] = null;
					ExTraceGlobals.ServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "Cancelling consumerId {0}", i);
					brokerAsyncResult.Result = BrokerStatus.Cancelled;
					brokerAsyncResult.IsCompleted = true;
					brokerAsyncResult.Callback(brokerAsyncResult);
				}
			}
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "StopGetNextNotificationCalls end");
		}

		// Token: 0x0400007D RID: 125
		private BrokerAsyncResult[] hangingRequests = new BrokerAsyncResult[3];
	}
}

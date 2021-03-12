using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000237 RID: 567
	internal sealed class PushSubscription : SubscriptionBase, IDisposable
	{
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00048058 File Offset: 0x00046258
		public PushSubscription(PushSubscriptionRequest subscriptionRequest, IdAndSession[] folderIds, Guid subscriptionOwnerObjectGuid, Subscriptions subscriptions) : base(subscriptionRequest, folderIds, subscriptionOwnerObjectGuid)
		{
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "PushSubscription constructor called: HashCode {0}", this.GetHashCode());
			this.budgetKey = CallContext.Current.Budget.Owner;
			this.statusFrequencyInMillisecs = 60000 * subscriptionRequest.StatusFrequency;
			this.clientUrl = subscriptionRequest.Url;
			this.callerData = subscriptionRequest.CallerData;
			this.subscriptions = subscriptions;
			StoreSession storeSessionForOperation = base.GetStoreSessionForOperation(folderIds);
			this.mdbGuid = storeSessionForOperation.MdbGuid;
			this.retryTimer = new Timer(new TimerCallback(PushSubscription.RetryTimerCallback), this, -1, -1);
			this.currentState = PushSubscription.NotificationState.Idle;
			this.RegisterEventHandler(30000L);
			this.requestVersion = ExchangeVersion.Current;
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "PushSubscription constructor exit: HashCode {0}, SubscriptionId {1}", this.GetHashCode(), base.SubscriptionId);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00048140 File Offset: 0x00046340
		private static void EventAvailableOrTimerCallback(object state, bool timedOut)
		{
			PushSubscription pushSubscription = (PushSubscription)state;
			pushSubscription.DoEventAvailableOrTimer();
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0004815C File Offset: 0x0004635C
		private static void ProcessResultCallback(object state, SendNotificationResult result, Exception exception)
		{
			PushSubscription pushSubscription = (PushSubscription)state;
			if (result != null && result.SubscriptionStatus != SubscriptionStatus.Invalid)
			{
				pushSubscription.ProcessNotificationResult(result);
				PerformanceMonitor.UpdatePushStatusCounter(true);
				return;
			}
			pushSubscription.HandleFailedNotice(exception);
			PerformanceMonitor.UpdatePushStatusCounter(false);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00048198 File Offset: 0x00046398
		private static void RetryTimerCallback(object state)
		{
			PushSubscription pushSubscription = (PushSubscription)state;
			pushSubscription.TrySendNotificationAsync();
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000481B4 File Offset: 0x000463B4
		private static string GetExceptionDetailsForLogging(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (exception != null)
			{
				stringBuilder.Append(string.Format("{0}: {1} ", exception.GetType().Name, exception.Message));
				if (exception is WebException)
				{
					WebException ex = (WebException)exception;
					HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
					if (httpWebResponse != null)
					{
						stringBuilder.Append(string.Format("StatusCode: {0} {1} ", httpWebResponse.StatusCode, httpWebResponse.StatusDescription));
					}
					else
					{
						stringBuilder.Append(string.Format("Status: {0} ", ex.Status.ToString()));
					}
				}
				else if (exception is SoapException)
				{
					SoapException ex2 = (SoapException)exception;
					stringBuilder.Append(string.Format("Code: {0} Detail: {1} ", ex2.Code, ex2.Detail.InnerText));
				}
				stringBuilder.Append(exception.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00048298 File Offset: 0x00046498
		private void RegisterEventHandler(long timeoutInMillisecs)
		{
			WaitHandle eventAvailableWaitHandle = base.EventQueue.EventAvailableWaitHandle;
			WaitOrTimerCallback callBack = new WaitOrTimerCallback(PushSubscription.EventAvailableOrTimerCallback);
			this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(eventAvailableWaitHandle, callBack, this, timeoutInMillisecs, true);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000482D0 File Offset: 0x000464D0
		private void DoEventAvailableOrTimer()
		{
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string, PushSubscription.NotificationState>((long)this.GetHashCode(), "DoEventAvailableOrTimer called: SubscriptionId {0}, State {1}", base.SubscriptionId, this.currentState);
			lock (this.lockObject)
			{
				if (this.currentState != PushSubscription.NotificationState.Idle)
				{
					ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "DoEventAvailableOrTimer: Subscription {0} was *not* idle", base.SubscriptionId);
					return;
				}
				this.currentState = PushSubscription.NotificationState.Sending;
				this.notificationData = null;
				this.retryInterval = 0;
				this.failedSendCount = 0;
			}
			ExchangeVersion.ExecuteWithSpecifiedVersion(this.requestVersion, new ExchangeVersion.ExchangeVersionDelegate(this.TrySendNotificationAsync));
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string, PushSubscription.NotificationState>((long)this.GetHashCode(), "DoEventAvailableOrTimer exit: SubscriptionId {0}, State {1}", base.SubscriptionId, this.currentState);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000483AC File Offset: 0x000465AC
		private void TrySendNotificationAsync()
		{
			this.ChangeRetryTimer(-1, -1);
			this.BeginSendNotification();
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000483BC File Offset: 0x000465BC
		private void HandleFailedNotice(Exception sendException)
		{
			if (this.failedSendCount == 0)
			{
				string exceptionDetailsForLogging = PushSubscription.GetExceptionDetailsForLogging(sendException);
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationFailedFirstTime, exceptionDetailsForLogging, new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					exceptionDetailsForLogging
				});
				ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "PushSubscription::HandleFailedNotice: Failed to send notification for SubscriptionId {0} to endpoint [{2}]: Details: {1}", base.SubscriptionId, exceptionDetailsForLogging, this.clientUrl);
				this.retryInterval = 30000;
				this.failedSendCount = 1;
				this.ChangeRetryTimer(this.retryInterval, -1);
				return;
			}
			this.failedSendCount++;
			this.retryInterval *= 2;
			if (this.retryInterval > this.statusFrequencyInMillisecs)
			{
				this.ChangeRetryTimer(-1, -1);
				this.RemoveSubscription(false);
				string exceptionDetailsForLogging2 = PushSubscription.GetExceptionDetailsForLogging(sendException);
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushSubscriptionFailedFinal, exceptionDetailsForLogging2, new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					this.failedSendCount,
					exceptionDetailsForLogging2
				});
				ExTraceGlobals.PushSubscriptionTracer.TraceError<string, string, string>((long)this.GetHashCode(), "PushSubscription::HandleFailedNotice: too many retry attempts, giving up on SubscriptionId {0} for endpoint [{2}] Details: {1}", base.SubscriptionId, exceptionDetailsForLogging2, this.clientUrl);
				return;
			}
			string exceptionDetailsForLogging3 = PushSubscription.GetExceptionDetailsForLogging(sendException);
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationFailedRetry, exceptionDetailsForLogging3, new object[]
			{
				base.SubscriptionId,
				this.clientUrl,
				this.failedSendCount,
				exceptionDetailsForLogging3
			});
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "PushSubscription::HandleFailedNotice: {0} retry attempt failed on SubscriptionId {1} for endpoint [{3}] Details: {2}", new object[]
			{
				this.failedSendCount,
				base.SubscriptionId,
				exceptionDetailsForLogging3,
				this.clientUrl
			});
			this.ChangeRetryTimer(this.retryInterval, -1);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00048590 File Offset: 0x00046790
		private void ProcessNotificationResult(SendNotificationResult result)
		{
			if (result.SubscriptionStatus == SubscriptionStatus.Unsubscribe)
			{
				this.RemoveSubscription(true);
				return;
			}
			if (this.currentState == PushSubscription.NotificationState.Error)
			{
				this.RemoveSubscription(false);
				return;
			}
			if (this.currentState == PushSubscription.NotificationState.Sending)
			{
				this.currentState = PushSubscription.NotificationState.Idle;
				this.notificationData = null;
				this.ChangeRetryTimer(-1, -1);
				this.RegisterEventHandler((long)this.statusFrequencyInMillisecs);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000485EC File Offset: 0x000467EC
		private void RemoveSubscription(bool isUnsubscribe)
		{
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "RemoveSubscription: SubscriptionId {0}, IsUnsubscribe {1}", base.SubscriptionId, isUnsubscribe);
			if (isUnsubscribe)
			{
				PerformanceMonitor.UpdateUnsubscribeCounter();
			}
			lock (this.lockObject)
			{
				this.currentState = PushSubscription.NotificationState.Dead;
				try
				{
					this.subscriptions.Delete(base.SubscriptionId);
				}
				catch (SubscriptionNotFoundException)
				{
					ExTraceGlobals.PushSubscriptionTracer.TraceError<string>((long)this.GetHashCode(), "RemoveSubscription: Tried to remove subscription {0}. It had already been deleted.", base.SubscriptionId);
				}
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00048690 File Offset: 0x00046890
		private void BeginSendNotification()
		{
			if (this.notificationData == null)
			{
				this.notificationData = this.BuildNotificationMessage();
			}
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "BeginSendNotification: SubscriptionId {0}", base.SubscriptionId);
			NotificationServiceClient notificationServiceClient = new NotificationServiceClient(this.clientUrl, this.callerData);
			notificationServiceClient.Timeout = 60000;
			notificationServiceClient.ResponseLimitInBytes = 1024;
			if (this.requestVersion.Supports(ExchangeVersion.Exchange2007SP1))
			{
				notificationServiceClient.RequestServerVersionValue = new NotificationRequestServerVersion(this.requestVersion.Version);
			}
			notificationServiceClient.SendNotificationAsync(this.notificationData, new NotificationServiceClient.SendNotificationResultCallback(PushSubscription.ProcessResultCallback), this);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00048738 File Offset: 0x00046938
		private SendNotificationResponse BuildNotificationMessage()
		{
			SendNotificationResponse sendNotificationResponse = new SendNotificationResponse();
			ServiceResult<EwsNotificationType>[] serviceResults = ExceptionHandler<EwsNotificationType>.Execute(new ExceptionHandler<EwsNotificationType>.CreateServiceResults(this.BuildNotification), new ExceptionHandler<EwsNotificationType>.GenerateMessageXmlForServiceError(this.AddSubscriptionIdToServiceError));
			sendNotificationResponse.BuildForResults<EwsNotificationType>(serviceResults);
			return sendNotificationResponse;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00048774 File Offset: 0x00046974
		private ServiceResult<EwsNotificationType>[] BuildNotification()
		{
			ServiceResult<EwsNotificationType>[] result;
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)2833657149U);
				using (IEwsBudget ewsBudget = EwsBudget.Acquire(this.budgetKey))
				{
					try
					{
						ewsBudget.CheckOverBudget();
						ewsBudget.StartLocal("PushSubscription.ServiceResult", default(TimeSpan));
						ServiceResult<EwsNotificationType>[] array = new ServiceResult<EwsNotificationType>[1];
						EwsNotificationType events = this.GetEvents(base.LastWatermarkSent);
						array[0] = new ServiceResult<EwsNotificationType>(events);
						result = array;
					}
					finally
					{
						ewsBudget.LogEndStateToIIS();
					}
				}
			}
			catch (EventNotFoundException ex)
			{
				ExTraceGlobals.PushSubscriptionTracer.TraceDebug<EventNotFoundException>((long)this.GetHashCode(), "BuildNotification caught exception: {0}, subscription marked for deletion", ex);
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationReadEventsFailed, base.MailboxId.MailboxGuid.ToString(), new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					ex
				});
				this.currentState = PushSubscription.NotificationState.Error;
				throw;
			}
			catch (FinalEventException ex2)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceDebug<FinalEventException, Event>((long)this.GetHashCode(), "BuildNotification caught exception: {0}, FinalEvent: {1}, subscription marked for deletion", ex2, ex2.FinalEvent);
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationReadEventsFailed, base.MailboxId.MailboxGuid.ToString(), new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					ex2
				});
				this.currentState = PushSubscription.NotificationState.Error;
				throw;
			}
			catch (ReadEventsFailedException ex3)
			{
				if (ex3.InnerException != null)
				{
					ExTraceGlobals.PushSubscriptionTracer.TraceDebug<Exception>((long)this.GetHashCode(), "BuildNotification caught exception: {0}, subscription marked for deletion", ex3.InnerException);
				}
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationReadEventsFailed, this.mdbGuid.ToString(), new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					ex3
				});
				this.currentState = PushSubscription.NotificationState.Error;
				throw;
			}
			catch (ReadEventsFailedTransientException ex4)
			{
				if (ex4.InnerException != null)
				{
					ExTraceGlobals.PushSubscriptionTracer.TraceDebug<Exception>((long)this.GetHashCode(), "BuildNotification caught exception: {0}, subscription marked for deletion", ex4.InnerException);
				}
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationReadEventsFailed, this.mdbGuid.ToString(), new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					ex4
				});
				this.currentState = PushSubscription.NotificationState.Error;
				throw;
			}
			catch (OverBudgetException ex5)
			{
				ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "BuildNotification caught overbudget exception.  Subscription marked for deletion.  Budget snapshot: {0}", ex5.Snapshot);
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_PushNotificationReadEventsFailed, base.MailboxId.MailboxGuid.ToString(), new object[]
				{
					base.SubscriptionId,
					this.clientUrl,
					ex5
				});
				this.currentState = PushSubscription.NotificationState.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00048A6C File Offset: 0x00046C6C
		private void ChangeRetryTimer(int dueTime, int period)
		{
			lock (this.lockObject)
			{
				if (this.retryTimer != null)
				{
					this.retryTimer.Change(dueTime, period);
				}
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00048ABC File Offset: 0x00046CBC
		private void AddSubscriptionIdToServiceError(ServiceError serviceError, Exception exception)
		{
			serviceError.AddConstantValueProperty("SubscriptionId", base.SubscriptionId);
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00048ACF File Offset: 0x00046CCF
		protected override int EventQueueSize
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00048AD4 File Offset: 0x00046CD4
		protected override void Dispose(bool isDisposing)
		{
			ExTraceGlobals.PushSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "Dispose: SubscriptionId {0}", base.SubscriptionId);
			if (!this.isDisposed && isDisposing)
			{
				lock (this.lockObject)
				{
					this.currentState = PushSubscription.NotificationState.Dead;
					if (this.retryTimer != null)
					{
						this.retryTimer.Change(-1, -1);
						this.retryTimer.Dispose();
						this.retryTimer = null;
					}
					if (this.registeredWaitHandle != null)
					{
						this.registeredWaitHandle.Unregister(base.EventQueue.EventAvailableWaitHandle);
						this.registeredWaitHandle = null;
					}
					base.Dispose(isDisposing);
				}
			}
		}

		// Token: 0x04000B52 RID: 2898
		private const int PushEventQueueSize = 25;

		// Token: 0x04000B53 RID: 2899
		private const int RetryIntervalInMillisecs = 30000;

		// Token: 0x04000B54 RID: 2900
		private const int RequestTimeoutInMillisecs = 60000;

		// Token: 0x04000B55 RID: 2901
		private const int FirstTimeHearbeatInMillisecs = 30000;

		// Token: 0x04000B56 RID: 2902
		private const int SendNotificationResponseLimitInBytes = 1024;

		// Token: 0x04000B57 RID: 2903
		private const string SubscriptionIdErrorName = "SubscriptionId";

		// Token: 0x04000B58 RID: 2904
		private readonly string callerData;

		// Token: 0x04000B59 RID: 2905
		private BudgetKey budgetKey;

		// Token: 0x04000B5A RID: 2906
		private int statusFrequencyInMillisecs;

		// Token: 0x04000B5B RID: 2907
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x04000B5C RID: 2908
		private Timer retryTimer;

		// Token: 0x04000B5D RID: 2909
		private string clientUrl;

		// Token: 0x04000B5E RID: 2910
		private Subscriptions subscriptions;

		// Token: 0x04000B5F RID: 2911
		private PushSubscription.NotificationState currentState;

		// Token: 0x04000B60 RID: 2912
		private int retryInterval;

		// Token: 0x04000B61 RID: 2913
		private int failedSendCount;

		// Token: 0x04000B62 RID: 2914
		private SendNotificationResponse notificationData;

		// Token: 0x04000B63 RID: 2915
		private ExchangeVersion requestVersion;

		// Token: 0x04000B64 RID: 2916
		private Guid mdbGuid;

		// Token: 0x02000238 RID: 568
		private enum NotificationState
		{
			// Token: 0x04000B66 RID: 2918
			Idle,
			// Token: 0x04000B67 RID: 2919
			Sending,
			// Token: 0x04000B68 RID: 2920
			Error,
			// Token: 0x04000B69 RID: 2921
			Dead
		}
	}
}

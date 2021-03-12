using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeleteEngine : ExecutionEngine
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000F37F File Offset: 0x0000D57F
		private DeleteEngine()
		{
			this.connectSubscriptionCleanup = new ConnectSubscriptionCleanup(SubscriptionManager.Instance);
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000F397 File Offset: 0x0000D597
		internal static DeleteEngine Instance
		{
			get
			{
				return DeleteEngine.instance;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public override IAsyncResult BeginExecution(AggregationWorkItem workItem, AsyncCallback callback, object callbackState)
		{
			FrameworkPerfCounterHandler.Instance.OnDeleteStarted();
			if (workItem.SyncEngineState == null)
			{
				workItem.SyncEngineState = this.CreateSyncEngineState(workItem);
			}
			string format = string.Format("Entering DeleteEngine.BeginExecution: SubscriptionType:{0}", workItem.SubscriptionType);
			workItem.SyncEngineState.SyncLogSession.LogInformation((TSLID)1128UL, ExecutionEngine.Tracer, format, new object[0]);
			AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult = new AsyncResult<AggregationWorkItem, SyncEngineResultData>(null, workItem, callback, callbackState, workItem.SubscriptionPoisonContext);
			asyncResult.SetCompletedSynchronously();
			this.DeleteSubscription(asyncResult);
			return asyncResult;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F428 File Offset: 0x0000D628
		public override AsyncOperationResult<SyncEngineResultData> EndExecution(IAsyncResult asyncResultParameter)
		{
			AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult = (AsyncResult<AggregationWorkItem, SyncEngineResultData>)asyncResultParameter;
			AggregationWorkItem state = asyncResult.State;
			SyncLogSession syncLogSession = state.SyncEngineState.SyncLogSession;
			syncLogSession.LogDebugging((TSLID)1143UL, ExecutionEngine.Tracer, "EndExecution: WaitForCompletion enter...", new object[0]);
			AsyncOperationResult<SyncEngineResultData> asyncOperationResult = asyncResult.WaitForCompletion();
			syncLogSession.LogDebugging((TSLID)1154UL, ExecutionEngine.Tracer, "EndExecution: WaitForCompletion exit, result={0}", new object[]
			{
				asyncOperationResult
			});
			FrameworkPerfCounterHandler.Instance.OnDeleteCompletion(asyncOperationResult);
			syncLogSession.LogInformation((TSLID)1155UL, ExecutionEngine.Tracer, "Exiting DeleteEngine.EndExecution", new object[0]);
			return asyncOperationResult;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		private SyncEngineState CreateSyncEngineState(AggregationWorkItem workItem)
		{
			return new SyncEngineState(SubscriptionInformationLoader.Instance, workItem.SyncLogSession, workItem.IsRecoverySyncMode, workItem.SubscriptionPoisonStatus, workItem.SyncHealthData, workItem.SubscriptionPoisonCallstack, workItem.LegacyDN, TransportMailSubmitter.Instance, SyncMode.DeleteSubscriptionMode, workItem.ConnectionStatistics, workItem.Subscription, RemoteServerHealthChecker.Instance);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F524 File Offset: 0x0000D724
		private void DeleteSubscription(object context)
		{
			AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult = (AsyncResult<AggregationWorkItem, SyncEngineResultData>)context;
			AggregationWorkItem state = asyncResult.State;
			SyncEngineState syncEngineState = state.SyncEngineState;
			SyncLogSession syncLogSession = syncEngineState.SyncLogSession;
			syncLogSession.LogInformation((TSLID)1156UL, ExecutionEngine.Tracer, "Entering DeleteEngine.DeleteSubscription", new object[0]);
			try
			{
				if (this.TryLoadMailboxSessionAndSubscription(asyncResult))
				{
					this.TryCleanupSubscription(asyncResult);
				}
			}
			finally
			{
				syncLogSession.LogDebugging((TSLID)1157UL, ExecutionEngine.Tracer, "Exiting DeleteEngine.DeleteSubscription", new object[0]);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		private void TryCleanupSubscription(AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult)
		{
			AggregationWorkItem state = asyncResult.State;
			SyncEngineState syncEngineState = state.SyncEngineState;
			MailboxSession mailboxSession = syncEngineState.SyncMailboxSession.MailboxSession;
			ISyncWorkerData userMailboxSubscription = syncEngineState.UserMailboxSubscription;
			IConnectSubscriptionCleanup connectSubscriptionCleanup = this.CleanupAssociatedWith(userMailboxSubscription.SubscriptionType);
			try
			{
				connectSubscriptionCleanup.Cleanup(mailboxSession, (IConnectSubscription)userMailboxSubscription, false);
			}
			catch (LocalizedException ex)
			{
				syncEngineState.SyncLogSession.LogError((TSLID)1158UL, ExecutionEngine.Tracer, "Hit exception in cleanup: {0}.", new object[]
				{
					ex
				});
				this.IndicateFailure(syncEngineState.StartSyncTime, asyncResult, ex);
			}
			this.IndicateSuccess(syncEngineState.StartSyncTime, asyncResult);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F664 File Offset: 0x0000D864
		private bool TryLoadMailboxSessionAndSubscription(AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult)
		{
			return this.TryLoadMailboxSessionAndSetOrgId(asyncResult) && this.TryLoadAndSetUserMailboxSubscription(asyncResult);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F678 File Offset: 0x0000D878
		private bool TryLoadMailboxSessionAndSetOrgId(AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult)
		{
			AggregationWorkItem state = asyncResult.State;
			SyncEngineState syncEngineState = state.SyncEngineState;
			SyncMailboxSession syncMailboxSession = syncEngineState.SyncMailboxSession;
			OrganizationId organizationId;
			bool flag;
			ISyncException ex;
			if (!syncEngineState.SubscriptionInformationLoader.TryLoadMailboxSession(state, syncMailboxSession, out organizationId, out flag, out ex))
			{
				this.IndicateFailure(syncEngineState.StartSyncTime, asyncResult, (Exception)ex);
				return false;
			}
			syncEngineState.SetOrganizationId(organizationId);
			return true;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		private bool TryLoadAndSetUserMailboxSubscription(AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult)
		{
			AggregationWorkItem state = asyncResult.State;
			SyncEngineState syncEngineState = state.SyncEngineState;
			SyncMailboxSession syncMailboxSession = syncEngineState.SyncMailboxSession;
			ISyncWorkerData userMailboxSubscription;
			ISyncException ex;
			bool flag;
			if (!syncEngineState.SubscriptionInformationLoader.TryLoadSubscription(state, syncMailboxSession, out userMailboxSubscription, out ex, out flag))
			{
				this.IndicateFailure(syncEngineState.StartSyncTime, asyncResult, (Exception)ex);
				return false;
			}
			syncEngineState.SetUserMailboxSubscription(userMailboxSubscription);
			return true;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F728 File Offset: 0x0000D928
		internal IConnectSubscriptionCleanup CleanupAssociatedWith(AggregationSubscriptionType subscriptionType)
		{
			if (subscriptionType == AggregationSubscriptionType.Facebook || subscriptionType == AggregationSubscriptionType.LinkedIn)
			{
				return this.connectSubscriptionCleanup;
			}
			string message = string.Format("Invalid SubscriptionType:{0}, only Facebook and LinkedIn subscriptions are supported", subscriptionType);
			throw new ArgumentException(message);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F760 File Offset: 0x0000D960
		private void IndicateSuccess(ExDateTime startSyncTime, AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult)
		{
			SyncEngineResultData result = new SyncEngineResultData(startSyncTime, true);
			asyncResult.ProcessCompleted(result);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F77C File Offset: 0x0000D97C
		private void IndicateFailure(ExDateTime startSyncTime, AsyncResult<AggregationWorkItem, SyncEngineResultData> asyncResult, Exception exception)
		{
			SyncEngineResultData result = new SyncEngineResultData(startSyncTime, false);
			asyncResult.ProcessCompleted(result, exception);
		}

		// Token: 0x040001B8 RID: 440
		private static readonly DeleteEngine instance = new DeleteEngine();

		// Token: 0x040001B9 RID: 441
		private readonly ConnectSubscriptionCleanup connectSubscriptionCleanup;
	}
}

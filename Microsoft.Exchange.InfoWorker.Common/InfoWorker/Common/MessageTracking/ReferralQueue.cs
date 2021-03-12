using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002D9 RID: 729
	internal class ReferralQueue
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x000617D8 File Offset: 0x0005F9D8
		internal ReferralQueue(DirectoryContext directoryContext)
		{
			this.directoryContext = directoryContext;
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00061835 File Offset: 0x0005FA35
		internal Dictionary<string, int> UnitTestHelper_UniqueAuthoritiesInQueue
		{
			get
			{
				return this.uniqueAuthoritiesInQueue;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0006183D File Offset: 0x0005FA3D
		internal int UnitTestHelper_UniqueAuthoritiesInQueueNotPending
		{
			get
			{
				return this.uniqueAuthoritiesInQueueNotPending;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00061845 File Offset: 0x0005FA45
		internal HashSet<string> UnitTestHelper_PendingAuthorities
		{
			get
			{
				return this.pendingAuthorities;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0006184D File Offset: 0x0005FA4D
		internal bool UnitTestHelper_ShouldCreateWorkerThread
		{
			get
			{
				return this.ShouldCreateWorkerThread();
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00061855 File Offset: 0x0005FA55
		private bool IsWorkerAvailable
		{
			get
			{
				return this.currentWorkerCount < 5;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00061860 File Offset: 0x0005FA60
		private bool IsDone
		{
			get
			{
				return this.referralQueue.Count == 0 && this.pendingAuthorities.Count == 0;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0006187F File Offset: 0x0005FA7F
		private bool IsMoreReferralsReadyToProcess
		{
			get
			{
				return this.uniqueAuthoritiesInQueueNotPending != 0;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0006188D File Offset: 0x0005FA8D
		private bool IsMoreReferralsReadyToProcessOrDone
		{
			get
			{
				return this.IsMoreReferralsReadyToProcess || this.IsDone;
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000618A0 File Offset: 0x0005FAA0
		internal void BeginWorker(ReferralQueue.State stateParam)
		{
			this.WaitForEvent(this.workerAvailableEvent);
			if (!this.ShouldCreateWorkerThread())
			{
				try
				{
					stateParam.WorkerMethod(stateParam.WorkerState);
					return;
				}
				finally
				{
					this.pendingAuthorities.Remove(stateParam.AuthorityKey);
					this.UpdateOnQueueChange();
				}
			}
			this.currentWorkerCount++;
			ReferralQueue.UpdateEvent(this.workerAvailableEvent, this.IsWorkerAvailable);
			bool flag = true;
			try
			{
				TrackingEventBudget.AcquireThread();
				flag = false;
			}
			finally
			{
				if (flag)
				{
					TrackingEventBudget.ReleaseThread();
				}
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerMethodWrapper), stateParam);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00061950 File Offset: 0x0005FB50
		internal void Enqueue(ReferralQueue.ReferralData referralData)
		{
			this.referralQueue.Enqueue(referralData);
			string key = referralData.Authority.ToString();
			int num;
			if (this.uniqueAuthoritiesInQueue.TryGetValue(key, out num))
			{
				num++;
			}
			else
			{
				num = 1;
			}
			this.uniqueAuthoritiesInQueue[key] = num;
			this.UpdateOnQueueChange();
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000619A4 File Offset: 0x0005FBA4
		internal bool DeQueue(out ReferralQueue.ReferralData referralData)
		{
			this.WaitForEvent(this.moreReferralsOrDoneEvent);
			if (!this.IsMoreReferralsReadyToProcess)
			{
				referralData = default(ReferralQueue.ReferralData);
				return false;
			}
			referralData = this.GetNextReferralThatIsNotPending().Value;
			string item = referralData.Authority.ToString();
			this.pendingAuthorities.Add(item);
			this.UpdateDequeue(referralData);
			this.UpdateOnQueueChange();
			return true;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00061A0E File Offset: 0x0005FC0E
		internal void Clear()
		{
			this.referralQueue.Clear();
			this.uniqueAuthoritiesInQueue.Clear();
			this.UpdateOnQueueChange();
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00061A2C File Offset: 0x0005FC2C
		private static void UpdateEvent(ManualResetEvent eventObject, bool conditionValue)
		{
			if (conditionValue)
			{
				eventObject.Set();
				return;
			}
			eventObject.Reset();
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00061A40 File Offset: 0x0005FC40
		private void WorkerMethodWrapper(object stateObject)
		{
			ReferralQueue.State state = (ReferralQueue.State)stateObject;
			bool flag = false;
			bool flag2 = false;
			TrackingError trackingError = null;
			try
			{
				this.directoryContext.Acquire();
				flag = true;
				if (this.directoryContext.DiagnosticsContext.DiagnosticsLevel == DiagnosticsLevel.Etw)
				{
					CommonDiagnosticsLogTracer traceWriter = new CommonDiagnosticsLogTracer();
					TraceWrapper.SearchLibraryTracer.Register(traceWriter);
					flag2 = true;
				}
				state.WorkerMethod(state.WorkerState);
			}
			catch (TrackingTransientException ex)
			{
				if (!ex.IsAlreadyLogged)
				{
					trackingError = ex.TrackingError;
				}
			}
			catch (TrackingFatalException ex2)
			{
				if (ex2.IsAlreadyLogged)
				{
					trackingError = ex2.TrackingError;
				}
			}
			catch (TransientException ex3)
			{
				trackingError = new TrackingError(ErrorCode.UnexpectedErrorTransient, string.Empty, ex3.Message, ex3.ToString());
			}
			catch (DataSourceOperationException ex4)
			{
				trackingError = new TrackingError(ErrorCode.UnexpectedErrorPermanent, string.Empty, "Error from Active Directory provider", ex4.ToString());
			}
			catch (DataValidationException ex5)
			{
				trackingError = new TrackingError(ErrorCode.UnexpectedErrorPermanent, string.Empty, "Validation Error from Active Directory provider", ex5.ToString());
			}
			finally
			{
				if (flag)
				{
					if (trackingError != null)
					{
						TraceWrapper.SearchLibraryTracer.TraceError<TrackingError>(this.GetHashCode(), "Error in woker thread while processing referral, {0}", trackingError);
						this.directoryContext.Errors.Errors.Add(trackingError);
					}
					this.pendingAuthorities.Remove(state.AuthorityKey);
					this.currentWorkerCount--;
					ReferralQueue.UpdateEvent(this.workerAvailableEvent, this.IsWorkerAvailable);
					this.UpdateOnQueueChange();
					if (flag2)
					{
						TraceWrapper.SearchLibraryTracer.Unregister();
					}
					this.directoryContext.Yield();
					TrackingEventBudget.ReleaseThread();
				}
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00061C04 File Offset: 0x0005FE04
		private bool ShouldCreateWorkerThread()
		{
			int num = this.uniqueAuthoritiesInQueueNotPending + this.pendingAuthorities.Count;
			return num > 1;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00061C28 File Offset: 0x0005FE28
		private ReferralQueue.ReferralData? GetNextReferralThatIsNotPending()
		{
			for (int i = 0; i < this.referralQueue.Count; i++)
			{
				ReferralQueue.ReferralData referralData = this.referralQueue.Dequeue();
				string item = referralData.Authority.ToString();
				if (!this.pendingAuthorities.Contains(item))
				{
					return new ReferralQueue.ReferralData?(referralData);
				}
				this.referralQueue.Enqueue(referralData);
			}
			return null;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00061C90 File Offset: 0x0005FE90
		private int GetUniqueAuthoritiesInQueueThatAreNotPending()
		{
			int num = this.uniqueAuthoritiesInQueue.Count;
			foreach (string item in this.uniqueAuthoritiesInQueue.Keys)
			{
				if (this.pendingAuthorities.Contains(item))
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00061D00 File Offset: 0x0005FF00
		private void UpdateDequeue(ReferralQueue.ReferralData item)
		{
			string key = item.Authority.ToString();
			int num = this.uniqueAuthoritiesInQueue[key];
			if (num == 1)
			{
				this.uniqueAuthoritiesInQueue.Remove(key);
				return;
			}
			this.uniqueAuthoritiesInQueue[key] = num - 1;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00061D48 File Offset: 0x0005FF48
		private void UpdateOnQueueChange()
		{
			this.uniqueAuthoritiesInQueueNotPending = this.GetUniqueAuthoritiesInQueueThatAreNotPending();
			ReferralQueue.UpdateEvent(this.moreReferralsOrDoneEvent, this.IsMoreReferralsReadyToProcessOrDone);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00061D68 File Offset: 0x0005FF68
		private void WaitForEvent(ManualResetEvent eventObject)
		{
			bool flag = false;
			try
			{
				this.directoryContext.Yield();
				flag = true;
				eventObject.WaitOne();
			}
			finally
			{
				if (flag)
				{
					this.directoryContext.Acquire();
				}
			}
		}

		// Token: 0x04000D9F RID: 3487
		private const int MaxWorkers = 5;

		// Token: 0x04000DA0 RID: 3488
		private DirectoryContext directoryContext;

		// Token: 0x04000DA1 RID: 3489
		private int currentWorkerCount;

		// Token: 0x04000DA2 RID: 3490
		private Queue<ReferralQueue.ReferralData> referralQueue = new Queue<ReferralQueue.ReferralData>();

		// Token: 0x04000DA3 RID: 3491
		private Dictionary<string, int> uniqueAuthoritiesInQueue = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04000DA4 RID: 3492
		private HashSet<string> pendingAuthorities = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04000DA5 RID: 3493
		private int uniqueAuthoritiesInQueueNotPending;

		// Token: 0x04000DA6 RID: 3494
		private ManualResetEvent workerAvailableEvent = new ManualResetEvent(true);

		// Token: 0x04000DA7 RID: 3495
		private ManualResetEvent moreReferralsOrDoneEvent = new ManualResetEvent(true);

		// Token: 0x020002DA RID: 730
		internal struct ReferralData
		{
			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060014FE RID: 5374 RVA: 0x00061DAC File Offset: 0x0005FFAC
			// (set) Token: 0x060014FF RID: 5375 RVA: 0x00061DB4 File Offset: 0x0005FFB4
			internal Node Node { get; set; }

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x06001500 RID: 5376 RVA: 0x00061DBD File Offset: 0x0005FFBD
			// (set) Token: 0x06001501 RID: 5377 RVA: 0x00061DC5 File Offset: 0x0005FFC5
			internal TrackingAuthority Authority { get; set; }
		}

		// Token: 0x020002DB RID: 731
		internal class State
		{
			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x06001502 RID: 5378 RVA: 0x00061DCE File Offset: 0x0005FFCE
			// (set) Token: 0x06001503 RID: 5379 RVA: 0x00061DD6 File Offset: 0x0005FFD6
			internal object WorkerState { get; set; }

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x06001504 RID: 5380 RVA: 0x00061DDF File Offset: 0x0005FFDF
			// (set) Token: 0x06001505 RID: 5381 RVA: 0x00061DE7 File Offset: 0x0005FFE7
			internal string AuthorityKey { get; set; }

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x06001506 RID: 5382 RVA: 0x00061DF0 File Offset: 0x0005FFF0
			// (set) Token: 0x06001507 RID: 5383 RVA: 0x00061DF8 File Offset: 0x0005FFF8
			internal WaitCallback WorkerMethod { get; set; }
		}
	}
}

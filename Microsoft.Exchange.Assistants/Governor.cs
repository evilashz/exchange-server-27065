using System;
using System.Threading;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002D RID: 45
	internal abstract class Governor : Base, IDisposable
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00006D70 File Offset: 0x00004F70
		public Governor(Governor parentGovernor)
		{
			this.lastRunTime = (DateTime)ExDateTime.Now;
			this.status = GovernorStatus.Running;
			this.parentGovernor = parentGovernor;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006D96 File Offset: 0x00004F96
		public GovernorStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006D9E File Offset: 0x00004F9E
		protected DateTime LastRunTime
		{
			get
			{
				return this.lastRunTime;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public GovernorStatus GetHierarchyStatus()
		{
			Governor governor = this;
			GovernorStatus governorStatus;
			do
			{
				governorStatus = governor.Status;
				if (governorStatus != GovernorStatus.Running)
				{
					break;
				}
				governor = governor.parentGovernor;
			}
			while (governor != null);
			return governorStatus;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006DCD File Offset: 0x00004FCD
		public void Dispose()
		{
			if (this.retryTimer != null)
			{
				this.retryTimer.Dispose();
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public bool ReportResult(AIException exception)
		{
			bool flag = true;
			ExTraceGlobals.GovernorTracer.TraceDebug<Governor, AIException>((long)this.GetHashCode(), "{0}: ReportResult: {1}", this, exception);
			AITransientException ex = null;
			if (exception is AITransientException && this.IsFailureRelevant((AITransientException)exception))
			{
				ex = (AITransientException)exception;
				ExTraceGlobals.GovernorTracer.TraceDebug<Governor>((long)this.GetHashCode(), "{0}: Exception is relevant", this);
			}
			lock (this)
			{
				switch (this.status)
				{
				case GovernorStatus.Running:
					if (ex != null)
					{
						this.numberConsecutiveFailures = 0U;
						this.lastRunTime = DateTime.UtcNow;
						this.ReportFailure(ex);
						flag = false;
					}
					break;
				case GovernorStatus.Retry:
					if (ex == null)
					{
						this.LogRecovery(exception);
						this.EnterRun();
					}
					else if (ex.RetrySchedule.FinalAction != FinalAction.RetryForever && this.lastRunTime + ex.RetrySchedule.TimeToGiveUp <= DateTime.UtcNow)
					{
						this.numberConsecutiveFailures += 1U;
						this.LogGiveUp(exception);
						this.EnterRun();
					}
					else
					{
						this.ReportFailure(ex);
						flag = false;
					}
					break;
				case GovernorStatus.Failure:
					if (ex != null)
					{
						flag = false;
						this.LogFailure(GovernorStatus.Failure, ex);
					}
					break;
				}
			}
			if (this.parentGovernor != null)
			{
				flag &= this.parentGovernor.ReportResult(exception);
			}
			return flag;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006F3C File Offset: 0x0000513C
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableGovernor queryableGovernor = queryableObject as QueryableGovernor;
			if (queryableGovernor != null)
			{
				queryableGovernor.Status = this.status.ToString();
				queryableGovernor.LastRunTime = this.lastRunTime;
				queryableGovernor.NumberConsecutiveFailures = (long)((ulong)this.numberConsecutiveFailures);
			}
		}

		// Token: 0x06000166 RID: 358
		protected abstract bool IsFailureRelevant(AITransientException exception);

		// Token: 0x06000167 RID: 359 RVA: 0x00006F89 File Offset: 0x00005189
		protected virtual void Run()
		{
		}

		// Token: 0x06000168 RID: 360
		protected abstract void Retry();

		// Token: 0x06000169 RID: 361 RVA: 0x00006F8B File Offset: 0x0000518B
		protected virtual void OnFailure()
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006F8D File Offset: 0x0000518D
		protected virtual void Log30MinuteWarning(AITransientException exception, TimeSpan nextRetryInterval)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006F8F File Offset: 0x0000518F
		private static void InternalTimerCallback(object state)
		{
			((Governor)state).RetryTimerCallback();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006F9C File Offset: 0x0000519C
		private TimeSpan GetNextRetryInterval(RetrySchedule retrySchedule)
		{
			return retrySchedule.GetRetryInterval(this.numberConsecutiveFailures - 1U);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006FAC File Offset: 0x000051AC
		private void ReportFailure(AITransientException transientException)
		{
			this.numberConsecutiveFailures += 1U;
			this.LogFailure(this.status, transientException);
			this.status = GovernorStatus.Failure;
			ExTraceGlobals.GovernorTracer.TraceDebug<Governor>((long)this.GetHashCode(), "{0}: Starting timer", this);
			TimeSpan nextRetryInterval = this.GetNextRetryInterval(transientException.RetrySchedule);
			this.retryTimer = new Timer(Governor.timerCallback, this, nextRetryInterval, TimeSpan.Zero);
			this.OnFailure();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000701C File Offset: 0x0000521C
		private void EnterRun()
		{
			this.status = GovernorStatus.Running;
			this.logged30MinuteWarning = false;
			this.Run();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007034 File Offset: 0x00005234
		private void RetryTimerCallback()
		{
			ExTraceGlobals.GovernorTracer.TraceDebug<Governor>((long)this.GetHashCode(), "{0}: Retry timer firing -- time to retry", this);
			lock (this)
			{
				this.retryTimer.Dispose();
				this.retryTimer = null;
				this.status = GovernorStatus.Retry;
				this.LogRetry();
				this.Retry();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000070A8 File Offset: 0x000052A8
		private void LogFailure(GovernorStatus oldStatus, AITransientException exception)
		{
			TimeSpan nextRetryInterval = this.GetNextRetryInterval(exception.RetrySchedule);
			ExTraceGlobals.GovernorTracer.TraceDebug((long)this.GetHashCode(), "{0}: {1}->Failure. {2} attempts in timespan {3}. Next retry time: now+{4}. Exception: {5}.", new object[]
			{
				this,
				oldStatus,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime,
				nextRetryInterval,
				exception
			});
			base.LogEvent(AssistantsEventLogConstants.Tuple_GovernorFailure, null, new object[]
			{
				this,
				oldStatus,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime,
				nextRetryInterval,
				exception
			});
			if (!this.logged30MinuteWarning && DateTime.UtcNow - this.lastRunTime > TimeSpan.FromMinutes(30.0))
			{
				this.Log30MinuteWarning(exception, nextRetryInterval);
				this.logged30MinuteWarning = true;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000071B0 File Offset: 0x000053B0
		private void LogRecovery(AIException exception)
		{
			ExTraceGlobals.GovernorTracer.TraceDebug((long)this.GetHashCode(), "{0}: Retry->Running. {1} attempts in timespan {2}. Exception: {3}.", new object[]
			{
				this,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime,
				exception
			});
			base.LogEvent(AssistantsEventLogConstants.Tuple_GovernorRecovery, null, new object[]
			{
				this,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime,
				exception
			});
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000724C File Offset: 0x0000544C
		private void LogGiveUp(AIException exception)
		{
			ExTraceGlobals.GovernorTracer.TraceDebug((long)this.GetHashCode(), "{0}: Retry->Running. Giving up after {1} attempts in timespan {2}. Exception: {3}", new object[]
			{
				this,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime,
				exception
			});
			base.LogEvent(AssistantsEventLogConstants.Tuple_GovernorGiveUp, null, new object[]
			{
				this,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime,
				exception
			});
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000072E8 File Offset: 0x000054E8
		private void LogRetry()
		{
			ExTraceGlobals.GovernorTracer.TraceDebug<Governor, uint, TimeSpan>((long)this.GetHashCode(), "{0}: Failure->Retry. {1} attempts in timespan {2}.", this, this.numberConsecutiveFailures, DateTime.UtcNow - this.lastRunTime);
			base.LogEvent(AssistantsEventLogConstants.Tuple_GovernorRetry, null, new object[]
			{
				this,
				this.numberConsecutiveFailures,
				DateTime.UtcNow - this.lastRunTime
			});
		}

		// Token: 0x0400013E RID: 318
		private static TimerCallback timerCallback = new TimerCallback(Governor.InternalTimerCallback);

		// Token: 0x0400013F RID: 319
		private GovernorStatus status;

		// Token: 0x04000140 RID: 320
		private DateTime lastRunTime;

		// Token: 0x04000141 RID: 321
		private uint numberConsecutiveFailures;

		// Token: 0x04000142 RID: 322
		private Timer retryTimer;

		// Token: 0x04000143 RID: 323
		private Governor parentGovernor;

		// Token: 0x04000144 RID: 324
		private bool logged30MinuteWarning;
	}
}

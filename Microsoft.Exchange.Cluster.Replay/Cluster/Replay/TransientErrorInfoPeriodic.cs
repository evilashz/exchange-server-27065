using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000232 RID: 562
	internal class TransientErrorInfoPeriodic
	{
		// Token: 0x0600155D RID: 5469 RVA: 0x000553A0 File Offset: 0x000535A0
		public TransientErrorInfoPeriodic(TimeSpan successTransitionSuppression, TimeSpan successPeriodicInterval, TimeSpan failureTransitionSuppression, TimeSpan failurePeriodicInterval, TransientErrorInfo.ErrorType initialState)
		{
			this.m_successTransitionSuppression = successTransitionSuppression;
			this.m_successPeriodicInterval = successPeriodicInterval;
			this.m_failureTransitionSuppression = failureTransitionSuppression;
			this.m_failurePeriodicInterval = failurePeriodicInterval;
			this.m_internalSuppressionInfo = new TransientErrorInfo();
			this.SetCurrentSuppressedState(initialState);
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000553DC File Offset: 0x000535DC
		public TimeSpan CurrentActualStateDuration
		{
			get
			{
				if (this.m_lastActualStateTransitionAfterSuppressionUtc == null)
				{
					return TimeSpan.Zero;
				}
				return DateTime.UtcNow.Subtract(this.m_lastActualStateTransitionAfterSuppressionUtc.Value);
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00055414 File Offset: 0x00053614
		public bool ReportSuccessPeriodic(out TransientErrorInfo.ErrorType currentState)
		{
			currentState = this.m_suppressedCurrentState;
			if (!this.m_internalSuppressionInfo.ReportSuccess(this.m_successTransitionSuppression))
			{
				return !this.ShouldSuppress(TransientErrorInfo.ErrorType.Success);
			}
			if (this.SetCurrentSuppressedState(TransientErrorInfo.ErrorType.Success))
			{
				currentState = this.m_suppressedCurrentState;
				this.m_lastPeriodicStateReturnedUtc = new DateTime?(DateTime.UtcNow);
				return true;
			}
			return !this.ShouldSuppress(TransientErrorInfo.ErrorType.Success);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00055474 File Offset: 0x00053674
		public bool ReportFailurePeriodic(out TransientErrorInfo.ErrorType currentState)
		{
			currentState = this.m_suppressedCurrentState;
			if (!this.m_internalSuppressionInfo.ReportFailure(this.m_failureTransitionSuppression))
			{
				return !this.ShouldSuppress(TransientErrorInfo.ErrorType.Failure);
			}
			if (this.SetCurrentSuppressedState(TransientErrorInfo.ErrorType.Failure))
			{
				currentState = this.m_suppressedCurrentState;
				this.m_lastPeriodicStateReturnedUtc = new DateTime?(DateTime.UtcNow);
				return true;
			}
			return !this.ShouldSuppress(TransientErrorInfo.ErrorType.Failure);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000554D4 File Offset: 0x000536D4
		private bool ShouldSuppress(TransientErrorInfo.ErrorType intendedState)
		{
			if (this.m_suppressedCurrentState != TransientErrorInfo.ErrorType.Unknown)
			{
				intendedState = this.m_suppressedCurrentState;
			}
			TimeSpan t = this.m_successTransitionSuppression;
			TimeSpan timeSpan = this.m_successPeriodicInterval;
			if (intendedState == TransientErrorInfo.ErrorType.Failure)
			{
				t = this.m_failureTransitionSuppression;
				timeSpan = this.m_failurePeriodicInterval;
			}
			if (this.CurrentActualStateDuration >= t)
			{
				if (this.m_lastPeriodicStateReturnedUtc == null)
				{
					this.m_lastPeriodicStateReturnedUtc = new DateTime?(DateTime.UtcNow);
					return false;
				}
				if (timeSpan == TransientErrorInfoPeriodic.InfiniteTimeSpan)
				{
					return true;
				}
				TimeSpan t2 = DateTime.UtcNow.Subtract(this.m_lastPeriodicStateReturnedUtc.Value);
				if (t2 >= timeSpan)
				{
					this.m_lastPeriodicStateReturnedUtc = new DateTime?(DateTime.UtcNow);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00055582 File Offset: 0x00053782
		private bool SetCurrentSuppressedState(TransientErrorInfo.ErrorType newState)
		{
			if (newState != this.m_suppressedCurrentState)
			{
				this.m_suppressedCurrentState = newState;
				this.m_lastActualStateTransitionAfterSuppressionUtc = new DateTime?(DateTime.UtcNow);
				this.m_lastPeriodicStateReturnedUtc = null;
				return true;
			}
			return false;
		}

		// Token: 0x0400085F RID: 2143
		public static readonly TimeSpan InfiniteTimeSpan = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x04000860 RID: 2144
		private readonly TimeSpan m_successTransitionSuppression;

		// Token: 0x04000861 RID: 2145
		private readonly TimeSpan m_successPeriodicInterval;

		// Token: 0x04000862 RID: 2146
		private readonly TimeSpan m_failureTransitionSuppression;

		// Token: 0x04000863 RID: 2147
		private readonly TimeSpan m_failurePeriodicInterval;

		// Token: 0x04000864 RID: 2148
		private DateTime? m_lastPeriodicStateReturnedUtc;

		// Token: 0x04000865 RID: 2149
		private DateTime? m_lastActualStateTransitionAfterSuppressionUtc;

		// Token: 0x04000866 RID: 2150
		private TransientErrorInfo.ErrorType m_suppressedCurrentState;

		// Token: 0x04000867 RID: 2151
		private TransientErrorInfo m_internalSuppressionInfo;
	}
}

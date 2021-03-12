using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay.Monitoring;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000230 RID: 560
	internal class TransientErrorInfo
	{
		// Token: 0x0600154D RID: 5453 RVA: 0x000551AC File Offset: 0x000533AC
		public static TransientErrorInfo ConstructFromPersisted(TransientErrorInfoPersisted errorInfo)
		{
			TransientErrorInfo transientErrorInfo = new TransientErrorInfo();
			transientErrorInfo.m_currentErrorState = StateTransitionInfo.ConvertErrorTypeFromSerializable(errorInfo.CurrentErrorState);
			transientErrorInfo.m_lastErrorState = transientErrorInfo.m_currentErrorState;
			DateTimeHelper.ParseIntoDateTimeIfPossible(errorInfo.LastSuccessTransitionUtc, ref transientErrorInfo.m_lastSuccessTransitionUtc);
			DateTimeHelper.ParseIntoDateTimeIfPossible(errorInfo.LastFailureTransitionUtc, ref transientErrorInfo.m_lastFailureTransitionUtc);
			return transientErrorInfo;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000551FF File Offset: 0x000533FF
		public TransientErrorInfo.ErrorType CurrentErrorState
		{
			get
			{
				return this.m_currentErrorState;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00055208 File Offset: 0x00053408
		public TimeSpan SuccessDuration
		{
			get
			{
				if (this.m_currentErrorState != TransientErrorInfo.ErrorType.Success)
				{
					return TimeSpan.Zero;
				}
				return DateTime.UtcNow.Subtract(this.m_lastSuccessTransitionUtc);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00055238 File Offset: 0x00053438
		public TimeSpan FailedDuration
		{
			get
			{
				if (this.m_currentErrorState != TransientErrorInfo.ErrorType.Failure)
				{
					return TimeSpan.Zero;
				}
				return DateTime.UtcNow.Subtract(this.m_lastFailureTransitionUtc);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00055267 File Offset: 0x00053467
		public DateTime LastSuccessTransitionUtc
		{
			get
			{
				return this.m_lastSuccessTransitionUtc;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005526F File Offset: 0x0005346F
		public DateTime LastFailureTransitionUtc
		{
			get
			{
				return this.m_lastFailureTransitionUtc;
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00055277 File Offset: 0x00053477
		public void ReportSuccess()
		{
			this.m_numSuccessiveFailures = 0U;
			this.m_numSuccessivePasses += 1U;
			this.UpdateErrorState(TransientErrorInfo.ErrorType.Success);
			this.UpdateTransitionTimesIfNecessary();
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0005529B File Offset: 0x0005349B
		public bool ReportSuccess(TimeSpan suppressDuration)
		{
			this.ReportSuccess();
			return suppressDuration.Equals(TimeSpan.Zero) || this.SuccessDuration >= suppressDuration;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000552BF File Offset: 0x000534BF
		public bool ReportSuccess(int numSuccessivePasses)
		{
			this.ReportSuccess();
			return (ulong)this.m_numSuccessivePasses >= (ulong)((long)numSuccessivePasses);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000552D5 File Offset: 0x000534D5
		public void ReportFailure()
		{
			this.m_numSuccessivePasses = 0U;
			this.m_numSuccessiveFailures += 1U;
			this.UpdateErrorState(TransientErrorInfo.ErrorType.Failure);
			this.UpdateTransitionTimesIfNecessary();
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000552F9 File Offset: 0x000534F9
		public bool ReportFailure(TimeSpan suppressDuration)
		{
			this.ReportFailure();
			return suppressDuration.Equals(TimeSpan.Zero) || this.FailedDuration >= suppressDuration;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0005531D File Offset: 0x0005351D
		public bool ReportFailure(int numSuccessiveFailures)
		{
			this.ReportFailure();
			return (ulong)this.m_numSuccessiveFailures >= (ulong)((long)numSuccessiveFailures);
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00055333 File Offset: 0x00053533
		private bool IsTransitioningState
		{
			get
			{
				return this.m_lastErrorState != this.m_currentErrorState;
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00055346 File Offset: 0x00053546
		private void UpdateErrorState(TransientErrorInfo.ErrorType errorState)
		{
			this.m_lastErrorState = this.m_currentErrorState;
			this.m_currentErrorState = errorState;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0005535C File Offset: 0x0005355C
		private void UpdateTransitionTimesIfNecessary()
		{
			if (this.IsTransitioningState)
			{
				DateTime utcNow = DateTime.UtcNow;
				if (this.m_currentErrorState == TransientErrorInfo.ErrorType.Success)
				{
					this.m_lastSuccessTransitionUtc = utcNow;
					return;
				}
				if (this.m_currentErrorState == TransientErrorInfo.ErrorType.Failure)
				{
					this.m_lastFailureTransitionUtc = utcNow;
				}
			}
		}

		// Token: 0x04000855 RID: 2133
		private uint m_numSuccessiveFailures;

		// Token: 0x04000856 RID: 2134
		private uint m_numSuccessivePasses;

		// Token: 0x04000857 RID: 2135
		private DateTime m_lastFailureTransitionUtc;

		// Token: 0x04000858 RID: 2136
		private DateTime m_lastSuccessTransitionUtc;

		// Token: 0x04000859 RID: 2137
		private TransientErrorInfo.ErrorType m_lastErrorState;

		// Token: 0x0400085A RID: 2138
		private TransientErrorInfo.ErrorType m_currentErrorState;

		// Token: 0x02000231 RID: 561
		internal enum ErrorType : short
		{
			// Token: 0x0400085C RID: 2140
			Unknown,
			// Token: 0x0400085D RID: 2141
			Success,
			// Token: 0x0400085E RID: 2142
			Failure
		}
	}
}

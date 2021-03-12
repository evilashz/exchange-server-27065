using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000235 RID: 565
	internal abstract class TransientPeriodicErrorSuppression<TKey>
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x00055676 File Offset: 0x00053876
		protected TransientPeriodicErrorSuppression(TimeSpan successTransitionSuppression, TimeSpan successPeriodicInterval, TimeSpan failureTransitionSuppression, TimeSpan failurePeriodicInterval, TransientErrorInfo.ErrorType initialState)
		{
			this.m_successTransitionSuppression = successTransitionSuppression;
			this.m_successPeriodicInterval = successPeriodicInterval;
			this.m_failureTransitionSuppression = failureTransitionSuppression;
			this.m_failurePeriodicInterval = failurePeriodicInterval;
			this.m_initialState = initialState;
			this.InitializeTable();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x000556AC File Offset: 0x000538AC
		public bool ReportSuccessPeriodic(TKey key, out TransientErrorInfo.ErrorType currentState)
		{
			TransientErrorInfoPeriodic existingOrNewErrorInfo = this.GetExistingOrNewErrorInfo(key);
			return existingOrNewErrorInfo.ReportSuccessPeriodic(out currentState);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000556C8 File Offset: 0x000538C8
		public bool ReportFailurePeriodic(TKey key, out TransientErrorInfo.ErrorType currentState)
		{
			TransientErrorInfoPeriodic existingOrNewErrorInfo = this.GetExistingOrNewErrorInfo(key);
			return existingOrNewErrorInfo.ReportFailurePeriodic(out currentState);
		}

		// Token: 0x0600156F RID: 5487
		protected abstract void InitializeTable();

		// Token: 0x06001570 RID: 5488 RVA: 0x000556E4 File Offset: 0x000538E4
		private TransientErrorInfoPeriodic GetExistingOrNewErrorInfo(TKey key)
		{
			TransientErrorInfoPeriodic transientErrorInfoPeriodic = null;
			if (!this.m_errorTable.TryGetValue(key, out transientErrorInfoPeriodic))
			{
				transientErrorInfoPeriodic = new TransientErrorInfoPeriodic(this.m_successTransitionSuppression, this.m_successPeriodicInterval, this.m_failureTransitionSuppression, this.m_failurePeriodicInterval, this.m_initialState);
				this.m_errorTable[key] = transientErrorInfoPeriodic;
			}
			return transientErrorInfoPeriodic;
		}

		// Token: 0x04000869 RID: 2153
		private readonly TimeSpan m_successTransitionSuppression;

		// Token: 0x0400086A RID: 2154
		private readonly TimeSpan m_successPeriodicInterval;

		// Token: 0x0400086B RID: 2155
		private readonly TimeSpan m_failureTransitionSuppression;

		// Token: 0x0400086C RID: 2156
		private readonly TimeSpan m_failurePeriodicInterval;

		// Token: 0x0400086D RID: 2157
		private readonly TransientErrorInfo.ErrorType m_initialState;

		// Token: 0x0400086E RID: 2158
		protected Dictionary<TKey, TransientErrorInfoPeriodic> m_errorTable;
	}
}

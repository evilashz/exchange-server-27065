using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000236 RID: 566
	internal class TransientPeriodicDatabaseErrorSuppression : TransientPeriodicErrorSuppression<Guid>
	{
		// Token: 0x06001571 RID: 5489 RVA: 0x00055735 File Offset: 0x00053935
		public TransientPeriodicDatabaseErrorSuppression(TimeSpan successTransitionSuppression, TimeSpan successPeriodicInterval, TimeSpan failureTransitionSuppression, TimeSpan failurePeriodicInterval, TransientErrorInfo.ErrorType initialState) : base(successTransitionSuppression, successPeriodicInterval, failureTransitionSuppression, failurePeriodicInterval, initialState)
		{
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00055744 File Offset: 0x00053944
		protected override void InitializeTable()
		{
			this.m_errorTable = new Dictionary<Guid, TransientErrorInfoPeriodic>(48);
		}
	}
}

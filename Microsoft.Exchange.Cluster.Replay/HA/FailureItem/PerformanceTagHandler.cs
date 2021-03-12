using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000199 RID: 409
	internal class PerformanceTagHandler : TagHandler
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x00045AB4 File Offset: 0x00043CB4
		internal PerformanceTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Performance", watcher, dbfi)
		{
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00045AC3 File Offset: 0x00043CC3
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcPerformance9a);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00045ACF File Offset: 0x00043CCF
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtPerformance9a);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00045ADB File Offset: 0x00043CDB
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcPerformance9b);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00045AE7 File Offset: 0x00043CE7
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00045AEA File Offset: 0x00043CEA
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00045AEC File Offset: 0x00043CEC
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

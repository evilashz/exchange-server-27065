using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A7 RID: 423
	internal class SourceLogCorruptionOutsideRequiredRangeTagHandler : TagHandler
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00045E6B File Offset: 0x0004406B
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcSourceLogCorruptOutsideRequiredRange9a);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00045E77 File Offset: 0x00044077
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcSourceLogCorruptOutsideRequiredRange9b);
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00045E83 File Offset: 0x00044083
		internal SourceLogCorruptionOutsideRequiredRangeTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("SourceLogCorruptionOutsideRequiredRange", watcher, dbfi)
		{
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00045E92 File Offset: 0x00044092
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00045E95 File Offset: 0x00044095
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00045EA7 File Offset: 0x000440A7
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

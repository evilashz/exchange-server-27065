using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000193 RID: 403
	internal class SourceCorruptionTagHandler : TagHandler
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x0004585B File Offset: 0x00043A5B
		internal SourceCorruptionTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Source corruption", watcher, dbfi)
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0004586A File Offset: 0x00043A6A
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SourceCorruption9a);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00045876 File Offset: 0x00043A76
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SourceCorruption9a);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00045882 File Offset: 0x00043A82
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SourceCorruption9b);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0004588E File Offset: 0x00043A8E
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SourceCorruption9b);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0004589A File Offset: 0x00043A9A
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004589D File Offset: 0x00043A9D
		internal override void ActiveRecoveryActionInternal()
		{
			base.MoveAfterSuspendAndFailLocalCopy(false, false, false);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000458A8 File Offset: 0x00043AA8
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

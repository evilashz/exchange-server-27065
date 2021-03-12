using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A6 RID: 422
	internal class SourceLogCorruptionTagHandler : TagHandler
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x00045E2D File Offset: 0x0004402D
		internal SourceLogCorruptionTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("SourceLogCorruption", watcher, dbfi)
		{
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00045E3C File Offset: 0x0004403C
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcSourceLogCorrupt9a);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00045E48 File Offset: 0x00044048
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcSourceLogCorrupt9b);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00045E54 File Offset: 0x00044054
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00045E57 File Offset: 0x00044057
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.HandleSourceLogCorruption(base.Database, Environment.MachineName);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00045E69 File Offset: 0x00044069
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

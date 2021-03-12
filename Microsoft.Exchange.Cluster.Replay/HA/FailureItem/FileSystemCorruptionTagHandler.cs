using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000192 RID: 402
	internal class FileSystemCorruptionTagHandler : TagHandler
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x00045800 File Offset: 0x00043A00
		internal FileSystemCorruptionTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("File System Corruption", watcher, dbfi)
		{
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0004580F File Offset: 0x00043A0F
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcFileSystemCorruption9a);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0004581B File Offset: 0x00043A1B
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtFileSystemCorruption9a);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00045827 File Offset: 0x00043A27
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcFileSystemCorruption9b);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00045833 File Offset: 0x00043A33
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtFileSystemCorruption9b);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0004583F File Offset: 0x00043A3F
		internal override bool RaiseMANotificationItem
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00045842 File Offset: 0x00043A42
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00045845 File Offset: 0x00043A45
		internal override void ActiveRecoveryActionInternal()
		{
			base.MoveAfterSuspendAndFailLocalCopy(true, false, true);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00045850 File Offset: 0x00043A50
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(true, false, true);
		}
	}
}

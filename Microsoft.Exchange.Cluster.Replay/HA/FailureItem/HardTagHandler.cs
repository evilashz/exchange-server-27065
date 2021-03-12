using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000195 RID: 405
	internal class HardTagHandler : TagHandler
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x00045902 File Offset: 0x00043B02
		internal HardTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Hard", watcher, dbfi)
		{
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00045911 File Offset: 0x00043B11
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHard9a);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004591D File Offset: 0x00043B1D
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHard9a);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00045929 File Offset: 0x00043B29
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHard9b);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00045935 File Offset: 0x00043B35
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHard9b);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00045941 File Offset: 0x00043B41
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00045944 File Offset: 0x00043B44
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00045956 File Offset: 0x00043B56
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A5 RID: 421
	internal class LostFlushDetectedTagHandler : TagHandler
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x00045DF3 File Offset: 0x00043FF3
		internal LostFlushDetectedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("LostFlushDetectedTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00045E02 File Offset: 0x00044002
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LostFlushDetected9a);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00045E0E File Offset: 0x0004400E
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LostFlushDetected9a);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00045E1A File Offset: 0x0004401A
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LostFlushDetected9b);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00045E26 File Offset: 0x00044026
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00045E29 File Offset: 0x00044029
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00045E2B File Offset: 0x0004402B
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200018E RID: 398
	internal class ConfigurationTagHandler : TagHandler
	{
		// Token: 0x06000FF7 RID: 4087 RVA: 0x000456D2 File Offset: 0x000438D2
		internal ConfigurationTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Configuration", watcher, dbfi)
		{
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x000456E1 File Offset: 0x000438E1
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcConfiguration9a);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x000456ED File Offset: 0x000438ED
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtConfiguration9a);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x000456F9 File Offset: 0x000438F9
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcConfiguration9b);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00045705 File Offset: 0x00043905
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtConfiguration9b);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00045711 File Offset: 0x00043911
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00045714 File Offset: 0x00043914
		internal override void ActiveRecoveryActionInternal()
		{
			base.MoveAfterSuspendAndFailLocalCopy(false, false, false);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0004571F File Offset: 0x0004391F
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

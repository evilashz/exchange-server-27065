using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200019D RID: 413
	internal class AlertOnlyTagHandler : TagHandler
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x00045BBC File Offset: 0x00043DBC
		internal AlertOnlyTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Alert Only", watcher, dbfi)
		{
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00045BCB File Offset: 0x00043DCB
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_AlertOnly9a);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00045BD7 File Offset: 0x00043DD7
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_AlertOnly9a);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00045BE3 File Offset: 0x00043DE3
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_AlertOnly9b);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00045BEF File Offset: 0x00043DEF
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00045BF2 File Offset: 0x00043DF2
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00045BF4 File Offset: 0x00043DF4
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

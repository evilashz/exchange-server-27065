using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200019C RID: 412
	internal class CatalogReseedTagHandler : TagHandler
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x00045B72 File Offset: 0x00043D72
		internal CatalogReseedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Catalog Reseed", watcher, dbfi)
		{
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00045B81 File Offset: 0x00043D81
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcCatalogReseed9a);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00045B8D File Offset: 0x00043D8D
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtCatalogReseed9a);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00045B99 File Offset: 0x00043D99
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcCatalogReseed9b);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00045BA5 File Offset: 0x00043DA5
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00045BA8 File Offset: 0x00043DA8
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.InitiateMoveForCatalog(base.Database, Environment.MachineName);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00045BBA File Offset: 0x00043DBA
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

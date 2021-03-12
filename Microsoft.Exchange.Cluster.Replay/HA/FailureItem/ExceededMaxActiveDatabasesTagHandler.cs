using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A1 RID: 417
	internal class ExceededMaxActiveDatabasesTagHandler : TagHandler
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x00045D07 File Offset: 0x00043F07
		internal ExceededMaxActiveDatabasesTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("ExceededMaxActiveDatabasesTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00045D16 File Offset: 0x00043F16
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcExceededMaxActiveDatabases9a);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00045D22 File Offset: 0x00043F22
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtExceededMaxActiveDatabases9a);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00045D2E File Offset: 0x00043F2E
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcExceededMaxActiveDatabases9b);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00045D3A File Offset: 0x00043F3A
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00045D3D File Offset: 0x00043F3D
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00045D4F File Offset: 0x00043F4F
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

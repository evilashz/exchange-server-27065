using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A0 RID: 416
	internal class ExceededMaxDatabasesTagHandler : TagHandler
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x00045CA8 File Offset: 0x00043EA8
		internal ExceededMaxDatabasesTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("ExceededMaxDatabasesTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00045CB7 File Offset: 0x00043EB7
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcExceededMaxDatabases9a);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00045CC3 File Offset: 0x00043EC3
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtExceededMaxDatabases9a);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00045CCF File Offset: 0x00043ECF
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcExceededMaxDatabases9b);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00045CDB File Offset: 0x00043EDB
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtExceededMaxDatabases9b);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00045CE7 File Offset: 0x00043EE7
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00045CEA File Offset: 0x00043EEA
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00045CFC File Offset: 0x00043EFC
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

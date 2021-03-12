using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001ED RID: 493
	internal class DatabaseAlertInfoTable<TAlert> : IDatabaseAlertInfoTable, IEnumerable<KeyValuePair<Guid, TAlert>>, IEnumerable where TAlert : MonitoringAlert
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0004EEA2 File Offset: 0x0004D0A2
		protected static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004EEA9 File Offset: 0x0004D0A9
		public DatabaseAlertInfoTable(Func<IHealthValidationResultMinimal, TAlert> createAlertDelegate)
		{
			this.m_createAlertDelegate = createAlertDelegate;
			this.m_alertTable = new Dictionary<Guid, TAlert>(48);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004EEC8 File Offset: 0x0004D0C8
		public void RaiseAppropriateAlertIfNecessary(IHealthValidationResultMinimal result)
		{
			MonitoringAlert existingOrNewAlertInfo = this.GetExistingOrNewAlertInfo(result);
			existingOrNewAlertInfo.RaiseAppropriateAlertIfNecessary(result);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		public void ResetState(Guid dbGuid)
		{
			TAlert talert = default(TAlert);
			if (this.m_alertTable.TryGetValue(dbGuid, out talert))
			{
				talert.ResetState();
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004EF16 File Offset: 0x0004D116
		protected void RemoveDatabaseAlert(Guid dbGuid)
		{
			if (this.m_alertTable.ContainsKey(dbGuid))
			{
				this.m_alertTable.Remove(dbGuid);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004EF34 File Offset: 0x0004D134
		public void Cleanup(HashSet<Guid> currentlyKnownDatabaseGuids)
		{
			List<Guid> list = new List<Guid>();
			foreach (Guid item in this.m_alertTable.Keys)
			{
				if (!currentlyKnownDatabaseGuids.Contains(item))
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				foreach (Guid guid in list)
				{
					this.m_alertTable.Remove(guid);
					DatabaseAlertInfoTable<TAlert>.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "DatabaseAlertInfoTable: Cleanup removed database: {0}", guid);
				}
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004F000 File Offset: 0x0004D200
		IEnumerator<KeyValuePair<Guid, TAlert>> IEnumerable<KeyValuePair<Guid, !0>>.GetEnumerator()
		{
			return this.m_alertTable.GetEnumerator();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004F012 File Offset: 0x0004D212
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.m_alertTable).GetEnumerator();
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0004F020 File Offset: 0x0004D220
		protected virtual MonitoringAlert GetExistingOrNewAlertInfo(IHealthValidationResultMinimal result)
		{
			TAlert talert = default(TAlert);
			Guid identityGuid = result.IdentityGuid;
			if (!this.m_alertTable.TryGetValue(identityGuid, out talert))
			{
				talert = this.m_createAlertDelegate(result);
				this.m_alertTable[identityGuid] = talert;
			}
			return talert;
		}

		// Token: 0x04000783 RID: 1923
		private Dictionary<Guid, TAlert> m_alertTable;

		// Token: 0x04000784 RID: 1924
		private Func<IHealthValidationResultMinimal, TAlert> m_createAlertDelegate;
	}
}

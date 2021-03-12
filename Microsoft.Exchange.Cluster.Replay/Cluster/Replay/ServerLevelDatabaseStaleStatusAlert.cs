using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001FF RID: 511
	internal class ServerLevelDatabaseStaleStatusAlert : MonitoringAlert
	{
		// Token: 0x0600142D RID: 5165 RVA: 0x000517C0 File Offset: 0x0004F9C0
		public ServerLevelDatabaseStaleStatusAlert(string serverName, Guid serverGuid, DatabaseAlertInfoTable<DatabaseStaleStatusAlert> staleAlerts) : base(serverName, serverGuid)
		{
			this.m_staleAlerts = staleAlerts;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000517D1 File Offset: 0x0004F9D1
		protected override TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x000517D8 File Offset: 0x0004F9D8
		protected override TimeSpan DatabaseHealthCheckRedTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusServerLevelRedTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000517E5 File Offset: 0x0004F9E5
		protected override TimeSpan DatabaseHealthCheckGreenPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusGreenPeriodicIntervalInSec);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000517F2 File Offset: 0x0004F9F2
		protected override TimeSpan DatabaseHealthCheckRedPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusRedPeriodicIntervalInSec);
			}
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00051800 File Offset: 0x0004FA00
		protected override bool IsValidationSuccessful(IHealthValidationResultMinimal serverValidationResult)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (KeyValuePair<Guid, DatabaseStaleStatusAlert> keyValuePair in ((IEnumerable<KeyValuePair<Guid, DatabaseStaleStatusAlert>>)this.m_staleAlerts))
			{
				if (keyValuePair.Value.CurrentAlertState == TransientErrorInfo.ErrorType.Failure)
				{
					if (num == 0)
					{
						stringBuilder.Append(keyValuePair.Value.Identity);
					}
					else
					{
						stringBuilder.AppendFormat(", {0}", keyValuePair.Value.Identity);
					}
					num++;
				}
			}
			string text = stringBuilder.ToString();
			if (num >= RegistryParameters.DatabaseHealthCheckStaleStatusServerLevelMinStaleCopies)
			{
				MonitoringAlert.Tracer.TraceError<int, string>((long)this.GetHashCode(), "ServerLevelDatabaseStaleStatusAlert: IsValidationSuccessful() found {0} stale copy status cached entries. Affected dbs: {1}", num, text);
				serverValidationResult.ErrorMessage = text;
				serverValidationResult.ErrorMessageWithoutFullStatus = text;
				return false;
			}
			return true;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000518D4 File Offset: 0x0004FAD4
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			MonitoringAlert.Tracer.TraceDebug((long)this.GetHashCode(), "ServerLevelDatabaseStaleStatusAlert: RaiseGreenEvent() called!");
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000518EC File Offset: 0x0004FAEC
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.ServerLevelDatabaseStaleStatusCheckFailed.Log<string, string>(base.Identity, EventUtil.TruncateStringInput(result.ErrorMessage, 32766));
			MonitoringAlert.Tracer.TraceError((long)this.GetHashCode(), "ServerLevelDatabaseStaleStatusAlert: RaiseRedEvent() called! Recovery will be attempted via Bugcheck.");
			BugcheckHelper.TriggerBugcheckIfRequired(DateTime.UtcNow, "ServerLevelDatabaseStaleStatusAlert is attempting recovery via BugCheck due to hung GetCopyStatus() RPC.");
		}

		// Token: 0x040007C8 RID: 1992
		private DatabaseAlertInfoTable<DatabaseStaleStatusAlert> m_staleAlerts;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001FC RID: 508
	internal class ServerLevelDatabaseRedundancyAlert : MonitoringAlert
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x00050F99 File Offset: 0x0004F199
		public ServerLevelDatabaseRedundancyAlert(string serverName, Guid serverGuid, DatabaseAlertInfoTable<DatabaseRedundancyAlert> oneCopyAlerts) : base(serverName, serverGuid)
		{
			this.m_oneCopyAlerts = oneCopyAlerts;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00050FAA File Offset: 0x0004F1AA
		protected override TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00050FB1 File Offset: 0x0004F1B1
		protected override TimeSpan DatabaseHealthCheckRedTransitionSuppression
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		protected override bool IsValidationSuccessful(IHealthValidationResultMinimal serverValidationResult)
		{
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (KeyValuePair<Guid, DatabaseRedundancyAlert> keyValuePair in ((IEnumerable<KeyValuePair<Guid, DatabaseRedundancyAlert>>)this.m_oneCopyAlerts))
			{
				if (keyValuePair.Value.CurrentAlertState == TransientErrorInfo.ErrorType.Failure)
				{
					if (!flag)
					{
						stringBuilder.Append(keyValuePair.Value.Identity);
						flag = true;
					}
					else
					{
						stringBuilder.AppendFormat(", {0}", keyValuePair.Value.Identity);
					}
				}
				else if (keyValuePair.Value.CurrentAlertState == TransientErrorInfo.ErrorType.Success)
				{
					flag2 = true;
				}
			}
			if (flag)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				foreach (KeyValuePair<Guid, DatabaseRedundancyAlert> keyValuePair2 in ((IEnumerable<KeyValuePair<Guid, DatabaseRedundancyAlert>>)this.m_oneCopyAlerts))
				{
					if (keyValuePair2.Value.CurrentAlertState == TransientErrorInfo.ErrorType.Failure)
					{
						stringBuilder.AppendFormat("{0} - {1}", keyValuePair2.Value.Identity, keyValuePair2.Value.ErrorMessageWithoutFullStatus);
						stringBuilder.AppendLine();
					}
				}
				serverValidationResult.ErrorMessage = stringBuilder.ToString();
				serverValidationResult.ErrorMessageWithoutFullStatus = serverValidationResult.ErrorMessage;
			}
			this.m_suppressEventsDueToStartup = (!flag2 && !flag);
			return !flag;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0005111C File Offset: 0x0004F31C
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			if (!this.m_suppressEventsDueToStartup)
			{
				ReplayEventLogConstants.Tuple_MonitoringDatabaseRedundancyServerCheckPassed.LogEvent(null, new object[]
				{
					base.Identity
				});
				new EventNotificationItem("msexchangerepl", "DatabaseRedundancy", "OneCopyServerEvent", ResultSeverityLevel.Informational)
				{
					StateAttribute1 = base.Identity,
					StateAttribute2 = string.Empty
				}.Publish(false);
			}
			this.WriteLastRunTime();
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00051188 File Offset: 0x0004F388
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			if (!this.m_suppressEventsDueToStartup)
			{
				ReplayEventLogConstants.Tuple_MonitoringDatabaseRedundancyServerCheckFailed.LogEvent(null, new object[]
				{
					base.Identity,
					EventUtil.TruncateStringInput(result.ErrorMessage, 32766)
				});
				new EventNotificationItem("msexchangerepl", "DatabaseRedundancy", "OneCopyServerEvent", ResultSeverityLevel.Critical)
				{
					StateAttribute1 = base.Identity,
					StateAttribute2 = EventUtil.TruncateStringInput(result.ErrorMessage, 16383)
				}.Publish(false);
			}
			this.WriteLastRunTime();
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0005126C File Offset: 0x0004F46C
		private void WriteLastRunTime()
		{
			Exception ex = RegistryUtil.RunRegistryFunction(delegate()
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States"))
				{
					registryKey.SetValue("OneCopyMonitorLastRun", DateTime.UtcNow.ToString());
				}
			});
			if (ex != null)
			{
				MonitoringAlert.Tracer.TraceError<Exception>((long)this.GetHashCode(), "ServerLevelDatabaseRedundancyAlert: WriteLastRunTime() failed with: {0}", ex);
				ReplayCrimsonEvents.MonitoringServerCheckFailedToWriteLastRunTime.LogPeriodic<string, Exception>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
			}
		}

		// Token: 0x040007B9 RID: 1977
		private const string MonitorStateValueName = "OneCopyMonitorLastRun";

		// Token: 0x040007BA RID: 1978
		private DatabaseAlertInfoTable<DatabaseRedundancyAlert> m_oneCopyAlerts;

		// Token: 0x040007BB RID: 1979
		private bool m_suppressEventsDueToStartup;
	}
}

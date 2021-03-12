using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders
{
	// Token: 0x02000474 RID: 1140
	public class SearchEscalateResponder : EscalateResponder
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x000A8F9C File Offset: 0x000A719C
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x000A8FA3 File Offset: 0x000A71A3
		public static string EscalateDailySchedulePattern
		{
			get
			{
				return SearchEscalateResponder.escalateDailySchedulePattern;
			}
			set
			{
				SearchEscalateResponder.escalateDailySchedulePattern = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x000A8FAB File Offset: 0x000A71AB
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x000A8FB3 File Offset: 0x000A71B3
		internal SearchEscalateResponder.EscalateModes EscalateMode { get; set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x000A8FBC File Offset: 0x000A71BC
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x000A8FC4 File Offset: 0x000A71C4
		internal bool UrgentInTraining { get; set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x000A8FCD File Offset: 0x000A71CD
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x000A8FD5 File Offset: 0x000A71D5
		internal bool CollectLogsAfterEscalate { get; set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x000A8FDE File Offset: 0x000A71DE
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x000A8FE6 File Offset: 0x000A71E6
		internal string CollectDumpForProcess { get; set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x000A8FEF File Offset: 0x000A71EF
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x000A8FF7 File Offset: 0x000A71F7
		internal string SearchMonitoringLogPath { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000A9000 File Offset: 0x000A7200
		protected override bool IncludeHealthSetEscalationInfo
		{
			get
			{
				return !LocalEndpointManager.IsDataCenter && base.IncludeHealthSetEscalationInfo;
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000A9014 File Offset: 0x000A7214
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, SearchEscalateResponder.EscalateModes escalateMode = SearchEscalateResponder.EscalateModes.Scheduled, bool urgentInTraining = true)
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, SearchEscalateResponder.EscalateDailySchedulePattern, false);
			responderDefinition.RecurrenceIntervalSeconds = 0;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.AssemblyPath = SearchEscalateResponder.AssemblyPath;
			responderDefinition.TypeName = SearchEscalateResponder.TypeName;
			responderDefinition.Attributes["EscalateMode"] = escalateMode.ToString();
			responderDefinition.Attributes["UrgentInTraining"] = urgentInTraining.ToString();
			if (escalateMode == SearchEscalateResponder.EscalateModes.Urgent)
			{
				if (urgentInTraining)
				{
					responderDefinition.NotificationServiceClass = NotificationServiceClass.UrgentInTraining;
				}
				else
				{
					responderDefinition.NotificationServiceClass = NotificationServiceClass.Urgent;
				}
			}
			else
			{
				responderDefinition.NotificationServiceClass = NotificationServiceClass.Scheduled;
			}
			return responderDefinition;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000A90BF File Offset: 0x000A72BF
		internal override EscalationState GetEscalationState(bool? isHealthy, CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			if (isHealthy != null && !isHealthy.Value)
			{
				this.SetNotificationServiceClass(cancellationToken);
			}
			return base.GetEscalationState(isHealthy, cancellationToken);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000A90E8 File Offset: 0x000A72E8
		protected virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.EscalateMode = attributeHelper.GetEnum<SearchEscalateResponder.EscalateModes>("EscalateMode", false, SearchEscalateResponder.EscalateModes.Scheduled);
			this.UrgentInTraining = attributeHelper.GetBool("UrgentInTraining", false, true);
			this.CollectLogsAfterEscalate = attributeHelper.GetBool("CollectLogsAfterEscalate", false, true);
			this.CollectDumpForProcess = attributeHelper.GetString("CollectDumpForProcess", false, SearchEscalateResponder.DefaultValues.CollectDumpForProcess);
			string installPath = ExchangeSetupContext.InstallPath;
			this.SearchMonitoringLogPath = Path.Combine(installPath, "Logging\\Monitoring\\Search");
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000A9168 File Offset: 0x000A7368
		protected override void AfterEscalate(CancellationToken cancellationToken)
		{
			try
			{
				this.CollectLogs();
			}
			catch (Exception ex)
			{
				SearchMonitoringHelper.LogInfo(this, "Exception caught collecting logs: {0}", new object[]
				{
					ex.ToString()
				});
			}
			try
			{
				this.CollectDump(cancellationToken);
			}
			catch (Exception ex2)
			{
				SearchMonitoringHelper.LogInfo(this, "Exception caught collecting dump: {0}", new object[]
				{
					ex2.ToString()
				});
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000A91E0 File Offset: 0x000A73E0
		private bool IsDatabaseCopyActiveOnLocalServer()
		{
			return SearchMonitoringHelper.IsDatabaseActive(base.Definition.TargetResource);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000A91F4 File Offset: 0x000A73F4
		private void SetNotificationServiceClass(CancellationToken cancellationToken)
		{
			switch (this.EscalateMode)
			{
			case SearchEscalateResponder.EscalateModes.Urgent:
				base.Definition.NotificationServiceClass = NotificationServiceClass.Urgent;
				break;
			case SearchEscalateResponder.EscalateModes.Scheduled:
				base.Definition.NotificationServiceClass = NotificationServiceClass.Scheduled;
				break;
			case SearchEscalateResponder.EscalateModes.UrgentOnActive:
				if (!this.IsDatabaseCopyActiveOnLocalServer())
				{
					SearchMonitoringHelper.LogInfo(this, "Database is inactive. Setting NotificationServiceClass to 'Scheduled'.", new object[0]);
					base.Definition.NotificationServiceClass = NotificationServiceClass.Scheduled;
				}
				else
				{
					SearchMonitoringHelper.LogInfo(this, "Database is active. Setting NotificationServiceClass to 'Urgent'.", new object[0]);
					base.Definition.NotificationServiceClass = NotificationServiceClass.Urgent;
				}
				break;
			case SearchEscalateResponder.EscalateModes.ReadFromProbeResult:
			{
				ProbeResult probeResult = null;
				try
				{
					probeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
				}
				catch (Exception ex)
				{
					SearchMonitoringHelper.LogInfo(this, "Caught exception reading last failed probe result: '{0}'.", new object[]
					{
						ex
					});
				}
				if (probeResult != null)
				{
					NotificationServiceClass notificationServiceClass;
					if (Enum.TryParse<NotificationServiceClass>(probeResult.StateAttribute22, out notificationServiceClass))
					{
						SearchMonitoringHelper.LogInfo(this, "Got NotificationServiceClass from last failed probe result: '{0}'.", new object[]
						{
							notificationServiceClass
						});
						base.Definition.NotificationServiceClass = notificationServiceClass;
					}
					else
					{
						SearchMonitoringHelper.LogInfo(this, "Failed to parse the NotificationServiceClass from StateAttribute22.", new object[0]);
					}
				}
				else
				{
					SearchMonitoringHelper.LogInfo(this, "Failed to read last failed probe result.", new object[0]);
				}
				break;
			}
			}
			if (base.Definition.NotificationServiceClass == NotificationServiceClass.Urgent && this.UrgentInTraining)
			{
				base.Definition.NotificationServiceClass = NotificationServiceClass.UrgentInTraining;
			}
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000A934C File Offset: 0x000A754C
		private void CollectLogs()
		{
			if (!LocalEndpointManager.IsDataCenter || !this.CollectLogsAfterEscalate)
			{
				return;
			}
			string installPath = ExchangeSetupContext.InstallPath;
			string[] array = new string[]
			{
				"Bin\\Search\\Ceres\\Diagnostics\\Logs",
				"Logging\\Search",
				"Logging\\Search\\Crawler",
				"Logging\\Monitoring\\Search",
				"Logging\\Search\\GracefulDegradation"
			};
			DateTime t = DateTime.UtcNow.AddHours(-3.0);
			if (!Directory.Exists(this.SearchMonitoringLogPath))
			{
				Directory.CreateDirectory(this.SearchMonitoringLogPath);
			}
			string[] files = Directory.GetFiles(this.SearchMonitoringLogPath, "*.zip", SearchOption.TopDirectoryOnly);
			if (files.Length >= 10)
			{
				FileInfo fileInfo = new FileInfo(files[0]);
				for (int i = 1; i < files.Length; i++)
				{
					FileInfo fileInfo2 = new FileInfo(files[i]);
					if (fileInfo2.LastWriteTimeUtc < fileInfo.LastWriteTimeUtc)
					{
						fileInfo = fileInfo2;
					}
				}
				fileInfo.Delete();
			}
			string text = Environment.MachineName + "-" + base.Definition.AlertTypeId;
			if (!string.IsNullOrEmpty(base.Definition.TargetResource))
			{
				text = text + "-" + base.Definition.TargetResource;
			}
			string text2 = Path.Combine(this.SearchMonitoringLogPath, text);
			string text3 = Path.Combine(this.SearchMonitoringLogPath, text + ".zip");
			if (Directory.Exists(text2))
			{
				Directory.Delete(text2, true);
			}
			if (File.Exists(text3))
			{
				Directory.Delete(text3);
			}
			Directory.CreateDirectory(text2);
			foreach (string path in array)
			{
				string path2 = Path.Combine(installPath, path);
				if (Directory.Exists(path2))
				{
					string[] files2 = Directory.GetFiles(path2, "*.log", SearchOption.TopDirectoryOnly);
					foreach (string fileName in files2)
					{
						FileInfo fileInfo3 = new FileInfo(fileName);
						if (fileInfo3.LastWriteTimeUtc >= t)
						{
							fileInfo3.CopyTo(Path.Combine(text2, fileInfo3.Name));
						}
					}
				}
			}
			ZipFile.CreateFromDirectory(text2, text3, CompressionLevel.Optimal, false);
			Directory.Delete(text2, true);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000A956C File Offset: 0x000A776C
		private void CollectDump(CancellationToken cancellationToken)
		{
			if (!LocalEndpointManager.IsDataCenter || string.IsNullOrEmpty(this.CollectDumpForProcess))
			{
				return;
			}
			string collectDumpForProcess = this.CollectDumpForProcess;
			Process[] array;
			if (collectDumpForProcess.StartsWith("noderunner", StringComparison.OrdinalIgnoreCase))
			{
				array = SearchMonitoringHelper.GetProcessesForNodeRunner(collectDumpForProcess);
			}
			else
			{
				array = (Process.GetProcessesByName(collectDumpForProcess) ?? new Process[0]);
			}
			foreach (Process process in array)
			{
				this.ThrottledDumpProcess(process, cancellationToken);
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000A9600 File Offset: 0x000A7800
		private void ThrottledDumpProcess(Process process, CancellationToken cancellationToken)
		{
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.WatsonDump, this.CollectDumpForProcess, this, false, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				this.InternalDumpProcess(process, cancellationToken);
			});
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000A9650 File Offset: 0x000A7850
		private void InternalDumpProcess(Process process, CancellationToken cancellationToken)
		{
			ProcessDumpHelper processDumpHelper = new ProcessDumpHelper(new CommonDumpParameters
			{
				Mode = DumpMode.FullDump,
				MaximumDurationInSeconds = 1200,
				Path = this.SearchMonitoringLogPath
			}, cancellationToken);
			string text = processDumpHelper.Generate(process, base.Definition.Name);
			SearchMonitoringHelper.LogInfo(this, "Dump is collected for process {0} at '{1}'.", new object[]
			{
				process.Id,
				text
			});
		}

		// Token: 0x040013C8 RID: 5064
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040013C9 RID: 5065
		private static readonly string TypeName = typeof(SearchEscalateResponder).FullName;

		// Token: 0x040013CA RID: 5066
		private static string escalateDailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday/00:00/17:00";

		// Token: 0x02000475 RID: 1141
		public enum EscalateModes
		{
			// Token: 0x040013D1 RID: 5073
			Urgent,
			// Token: 0x040013D2 RID: 5074
			Scheduled,
			// Token: 0x040013D3 RID: 5075
			UrgentOnActive,
			// Token: 0x040013D4 RID: 5076
			ReadFromProbeResult
		}

		// Token: 0x02000476 RID: 1142
		internal new static class AttributeNames
		{
			// Token: 0x040013D5 RID: 5077
			internal const string EscalateMode = "EscalateMode";

			// Token: 0x040013D6 RID: 5078
			internal const string UrgentInTraining = "UrgentInTraining";

			// Token: 0x040013D7 RID: 5079
			internal const string CollectLogsAfterEscalate = "CollectLogsAfterEscalate";

			// Token: 0x040013D8 RID: 5080
			internal const string CollectDumpForProcess = "CollectDumpForProcess";
		}

		// Token: 0x02000477 RID: 1143
		internal new static class DefaultValues
		{
			// Token: 0x040013D9 RID: 5081
			internal const SearchEscalateResponder.EscalateModes EscalateMode = SearchEscalateResponder.EscalateModes.Scheduled;

			// Token: 0x040013DA RID: 5082
			internal const bool UrgentInTraining = true;

			// Token: 0x040013DB RID: 5083
			internal const bool CollectLogsAfterEscalate = true;

			// Token: 0x040013DC RID: 5084
			internal static readonly string CollectDumpForProcess = string.Empty;
		}
	}
}

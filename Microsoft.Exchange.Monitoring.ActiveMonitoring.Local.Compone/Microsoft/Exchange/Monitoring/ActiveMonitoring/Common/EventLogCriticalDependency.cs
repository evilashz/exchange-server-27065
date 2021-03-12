using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000593 RID: 1427
	internal sealed class EventLogCriticalDependency : ICriticalDependency
	{
		// Token: 0x060023B4 RID: 9140
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, EventLogCriticalDependency.MoveFileFlags dwFlags);

		// Token: 0x060023B5 RID: 9141 RVA: 0x000D5713 File Offset: 0x000D3913
		internal EventLogCriticalDependency(Trace trace, TracingContext traceContext)
		{
			this.trace = trace;
			this.traceContext = traceContext;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000D5734 File Offset: 0x000D3934
		string ICriticalDependency.Name
		{
			get
			{
				return "EventLogCriticalDependency";
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000D573B File Offset: 0x000D393B
		TimeSpan ICriticalDependency.RetestDelay
		{
			get
			{
				return EventLogCriticalDependency.RetestDelay;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000D5742 File Offset: 0x000D3942
		string ICriticalDependency.EscalationService
		{
			get
			{
				return "Exchange";
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000D5749 File Offset: 0x000D3949
		string ICriticalDependency.EscalationTeam
		{
			get
			{
				return "Monitoring";
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000D5750 File Offset: 0x000D3950
		public bool TestCriticalDependency()
		{
			bool result = true;
			foreach (string text in EventLogCriticalDependency.CrimsonChannels)
			{
				using (EventLogReader eventLogReader = new EventLogReader(text, PathType.LogName))
				{
					try
					{
						eventLogReader.Seek(SeekOrigin.End, 0L);
						using (EventRecord eventRecord = eventLogReader.ReadEvent())
						{
							if (eventRecord != null)
							{
								WTFDiagnostics.TraceInformation<string, string, string>(this.trace, this.traceContext, "'{0}': event log '{1}' successfully read. The most recent event has timestamp '{2}'", "EventLogCriticalDependency", text, eventRecord.TimeCreated.ToString(), null, "TestCriticalDependency", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\EventLogCriticalDependency.cs", 176);
							}
							else
							{
								WTFDiagnostics.TraceInformation<string, string>(this.trace, this.traceContext, "'{0}': event log '{1}' appears empty but not corrupt.", "EventLogCriticalDependency", text, null, "TestCriticalDependency", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\EventLogCriticalDependency.cs", 181);
							}
						}
					}
					catch (Exception ex)
					{
						if (ex is EventLogException)
						{
							WTFDiagnostics.TraceError<string, string, string>(this.trace, this.traceContext, "'{0}': event log '{1}' appears corrupt and will be queued for deletion. Reading the log failed with exception: {2}", "EventLogCriticalDependency", text, ex.ToString(), null, "TestCriticalDependency", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\EventLogCriticalDependency.cs", 191);
							this.corruptLogs.Add(text);
						}
						else
						{
							WTFDiagnostics.TraceError<string, string, string>(this.trace, this.traceContext, "'{0}': event log '{1}' failed but the exception does not match a known pattern of corruption: {2}", "EventLogCriticalDependency", text, ex.ToString(), null, "TestCriticalDependency", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\EventLogCriticalDependency.cs", 201);
						}
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000D58FC File Offset: 0x000D3AFC
		public bool FixCriticalDependency()
		{
			bool result = true;
			if (this.corruptLogs.Count == 0)
			{
				return false;
			}
			foreach (string logName in this.corruptLogs)
			{
				string text;
				using (EventLogConfiguration eventLogConfiguration = new EventLogConfiguration(logName))
				{
					text = Environment.ExpandEnvironmentVariables(eventLogConfiguration.LogFilePath);
				}
				if (EventLogCriticalDependency.MoveFileEx(text, null, EventLogCriticalDependency.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT))
				{
					WTFDiagnostics.TraceInformation<string, string>(this.trace, this.traceContext, "'{0}': queued corrupt event log file '{1}' for deletion upon next reboot.", "EventLogCriticalDependency", text, null, "FixCriticalDependency", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\EventLogCriticalDependency.cs", 253);
				}
				else
				{
					result = false;
					WTFDiagnostics.TraceError<string, string>(this.trace, this.traceContext, "'{0}': tried but failed to queue corrupt event log file '{1}' for deletion upon next reboot.", "EventLogCriticalDependency", text, null, "FixCriticalDependency", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\EventLogCriticalDependency.cs", 258);
				}
			}
			return result;
		}

		// Token: 0x04001988 RID: 6536
		private const string Name = "EventLogCriticalDependency";

		// Token: 0x04001989 RID: 6537
		private const string escalationService = "Exchange";

		// Token: 0x0400198A RID: 6538
		private const string escalationTeam = "Monitoring";

		// Token: 0x0400198B RID: 6539
		private static TimeSpan RetestDelay = TimeSpan.FromSeconds(5.0);

		// Token: 0x0400198C RID: 6540
		private static readonly string[] CrimsonChannels = new string[]
		{
			"Microsoft-Exchange-ActiveMonitoring/MaintenanceDefinition",
			"Microsoft-Exchange-ActiveMonitoring/MaintenanceResult",
			"Microsoft-Exchange-ActiveMonitoring/ProbeDefinition",
			"Microsoft-Exchange-ActiveMonitoring/ProbeResult",
			"Microsoft-Exchange-ActiveMonitoring/MonitorDefinition",
			"Microsoft-Exchange-ActiveMonitoring/MonitorResult",
			"Microsoft-Exchange-ActiveMonitoring/ResponderDefinition",
			"Microsoft-Exchange-ActiveMonitoring/ResponderResult",
			"Microsoft-Exchange-ManagedAvailability/InvokeNowRequest",
			"Microsoft-Exchange-ManagedAvailability/InvokeNowResult",
			"Microsoft-Exchange-ManagedAvailability/Monitoring",
			"Microsoft-Exchange-ManagedAvailability/RecoveryActionLogs",
			"Microsoft-Exchange-ManagedAvailability/RecoveryActionResults",
			"Microsoft-Exchange-ManagedAvailability/RemoteActionLogs",
			"Microsoft-Exchange-ManagedAvailability/StartupNotification",
			"Microsoft-Exchange-ManagedAvailability/ThrottlingConfig",
			"Application",
			"System"
		};

		// Token: 0x0400198D RID: 6541
		private List<string> corruptLogs = new List<string>();

		// Token: 0x0400198E RID: 6542
		private Trace trace;

		// Token: 0x0400198F RID: 6543
		private TracingContext traceContext;

		// Token: 0x02000594 RID: 1428
		[Flags]
		private enum MoveFileFlags : uint
		{
			// Token: 0x04001991 RID: 6545
			MOVEFILE_REPLACE_EXISTING = 1U,
			// Token: 0x04001992 RID: 6546
			MOVEFILE_COPY_ALLOWED = 2U,
			// Token: 0x04001993 RID: 6547
			MOVEFILE_DELAY_UNTIL_REBOOT = 4U,
			// Token: 0x04001994 RID: 6548
			MOVEFILE_WRITE_THROUGH = 8U,
			// Token: 0x04001995 RID: 6549
			MOVEFILE_CREATE_HARDLINK = 16U,
			// Token: 0x04001996 RID: 6550
			MOVEFILE_FAIL_IF_NOT_TRACKABLE = 32U
		}
	}
}

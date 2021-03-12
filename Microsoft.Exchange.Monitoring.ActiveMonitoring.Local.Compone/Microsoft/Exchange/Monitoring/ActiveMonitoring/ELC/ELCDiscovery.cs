using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ELC
{
	// Token: 0x0200016D RID: 365
	public sealed class ELCDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x00042E54 File Offset: 0x00041054
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.ELCAssistantTracer, base.TraceContext, "ELCDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ELC\\ELCDiscovery.cs", 209);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				this.WriteTrace("ELCProcessing.DoWork: Skipping workitem generation since not on a mailbox server.");
				return;
			}
			this.CreateTransientContext();
			this.CreatePermanentContext();
			this.CreateMailboxSLAContext();
			this.CreateDumpsterWarningQuota();
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00042EC8 File Offset: 0x000410C8
		private void CreateTransientContext()
		{
			bool flag = bool.Parse(base.Definition.Attributes["ELCTransientMonitorEnabled"]);
			int interval = (int)TimeSpan.Parse(base.Definition.Attributes["ELCTransientMonitorInterval"]).TotalSeconds;
			int recurrence = (int)TimeSpan.Parse(base.Definition.Attributes["ELCTransientMonitorRecurranceInterval"]).TotalSeconds;
			int threshold = int.Parse(base.Definition.Attributes["ELCTransientMonitorMinErrorCount"]);
			string name = ExchangeComponent.Compliance.Name;
			string monitorSampleMask = NotificationItem.GenerateResultName(name, "ELCComponent_Transient", null);
			if (flag)
			{
				this.CreateXFailureMonitor("ELCTransientMonitor", monitorSampleMask, interval, recurrence, threshold);
				string alertMask = string.Format("{0}/{1}", "ELCTransientMonitor", ExchangeComponent.Compliance.Name);
				this.CreateResponder("ELCTransientEscalateResponder", "ELCTransientMonitor", alertMask, Strings.ELCTransientEscalationSubject, Strings.ELCExceptionEscalationMessage);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00042FC8 File Offset: 0x000411C8
		private void CreatePermanentContext()
		{
			bool flag = bool.Parse(base.Definition.Attributes["ELCPermanentMonitorEnabled"]);
			int interval = (int)TimeSpan.Parse(base.Definition.Attributes["ELCPermanentMonitorInterval"]).TotalSeconds;
			int recurrence = (int)TimeSpan.Parse(base.Definition.Attributes["ELCPermanentMonitorRecurranceInterval"]).TotalSeconds;
			int threshold = int.Parse(base.Definition.Attributes["ELCPermanentMonitorMinErrorCount"]);
			string name = ExchangeComponent.Compliance.Name;
			string monitorSampleMask = NotificationItem.GenerateResultName(name, "ELCComponent_Permanent", null);
			if (flag)
			{
				this.CreateXFailureMonitor("ELCPermanentMonitor", monitorSampleMask, interval, recurrence, threshold);
				string alertMask = string.Format("{0}/{1}", "ELCPermanentMonitor", ExchangeComponent.Compliance.Name);
				this.CreateResponder("ELCPermanentEscalateResponder", "ELCPermanentMonitor", alertMask, Strings.ELCPermanentEscalationSubject, Strings.ELCExceptionEscalationMessage);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000430C8 File Offset: 0x000412C8
		private void CreateMailboxSLAContext()
		{
			bool flag = bool.Parse(base.Definition.Attributes["ELCMailboxSLAMonitorEnabled"]);
			int interval = (int)TimeSpan.Parse(base.Definition.Attributes["ELCMailboxSLAMonitorInterval"]).TotalSeconds;
			int recurrence = (int)TimeSpan.Parse(base.Definition.Attributes["ELCMailboxSLAMonitorRecurranceInterval"]).TotalSeconds;
			if (ExEnvironment.IsTest)
			{
				recurrence = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
			}
			int threshold = int.Parse(base.Definition.Attributes["ELCMailboxSLAMonitorMinErrorCount"]);
			string name = ExchangeComponent.Compliance.Name;
			string monitorSampleMask = NotificationItem.GenerateResultName(name, "ELCComponent_LastSuccessTooLongAgo", null);
			if (flag)
			{
				this.CreateXFailureMonitor("ELCMailboxSLAMonitor", monitorSampleMask, interval, recurrence, threshold);
				string alertMask = string.Format("{0}/{1}", "ELCMailboxSLAMonitor", ExchangeComponent.Compliance.Name);
				this.CreateResponder("ELCMailboxSLAEscalateResponder", "ELCMailboxSLAMonitor", alertMask, Strings.ELCMailboxSLAEscalationSubject, Strings.ELCMailboxSLAEscalationMessage);
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000431E8 File Offset: 0x000413E8
		private void CreateDumpsterWarningQuota()
		{
			bool flag = bool.Parse(base.Definition.Attributes["ELCDumpsterWarningExceededMonitorEnabled"]);
			int interval = (int)TimeSpan.Parse(base.Definition.Attributes["ELCDumpsterWarningExceededMonitorInterval"]).TotalSeconds;
			int recurrence = (int)TimeSpan.Parse(base.Definition.Attributes["ELCDumpsterWarningExceededMonitorRecurranceInterval"]).TotalSeconds;
			int threshold = int.Parse(base.Definition.Attributes["ELCDumpsterWarningExceededMonitorMinErrorCount"]);
			string name = ExchangeComponent.Compliance.Name;
			string monitorSampleMask = NotificationItem.GenerateResultName(name, "ELCComponent_DumpsterWarningQuota", null);
			if (flag)
			{
				this.CreateXFailureMonitor("ELCDumpsterWarningExceededMonitor", monitorSampleMask, interval, recurrence, threshold);
				string alertMask = string.Format("{0}/{1}", "ELCDumpsterWarningExceededMonitor", ExchangeComponent.Compliance.Name);
				this.CreateResponder("ELCDumpsterWarningExceededResponder", "ELCDumpsterWarningExceededMonitor", alertMask, Strings.ELCDumpsterWarningEscalationSubject, Strings.ELCDumpsterEscalationMessage);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000432E8 File Offset: 0x000414E8
		private void CreateArchiveDumpsterWarningQuota()
		{
			bool flag = bool.Parse(base.Definition.Attributes["ELCArchiveDumpsterWarningExceededMonitorEnabled"]);
			int interval = (int)TimeSpan.Parse(base.Definition.Attributes["ELCArchiveDumpsterWarningExceededMonitorInterval"]).TotalSeconds;
			int recurrence = (int)TimeSpan.Parse(base.Definition.Attributes["ELCArchiveDumpsterWarningExceededMonitorRecurranceInterval"]).TotalSeconds;
			int threshold = int.Parse(base.Definition.Attributes["ELCArchiveDumpsterWarningExceededMonitorMinErrorCount"]);
			string name = ExchangeComponent.Compliance.Name;
			string monitorSampleMask = NotificationItem.GenerateResultName(name, "ELCComponent_ArchiveWarningQuota", null);
			if (flag)
			{
				this.CreateXFailureMonitor("ELCArchiveDumpsterWarningExceededMonitor", monitorSampleMask, interval, recurrence, threshold);
				string alertMask = string.Format("{0}/{1}", "ELCArchiveDumpsterWarningExceededMonitor", ExchangeComponent.Compliance.Name);
				this.CreateResponder("ELCArchiveDumpsterWarningExceededResponder", "ELCArchiveDumpsterWarningExceededMonitor", alertMask, Strings.ELCArchiveDumpsterWarningEscalationSubject, Strings.ELCArchiveDumpsterEscalationMessage);
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000433E6 File Offset: 0x000415E6
		private void ExceededDumpserWarningQuota()
		{
			bool.Parse(base.Definition.Attributes["ELCDumpsterWarningExceededMonitorEnabled"]);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00043404 File Offset: 0x00041604
		private void CreateXFailureMonitor(string monitorName, string monitorSampleMask, int interval, int recurrence, int threshold)
		{
			TimeSpan timeSpan = TimeSpan.Parse(base.Definition.Attributes["ELCUnrecoverableTransitionTimeSpan"]);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(monitorName, monitorSampleMask, ExchangeComponent.Compliance.Name, ExchangeComponent.Compliance, interval, recurrence, threshold, true);
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			this.WriteTrace(string.Format("ELCProcessing: Created Monitor Definition {0}.", monitorName));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000434A0 File Offset: 0x000416A0
		private void CreateResponder(string responderName, string monitorName, string alertMask, string escalationSubject, string escalationMessage)
		{
			ResponderDefinition definition = EscalateResponder.CreateDefinition(responderName, ExchangeComponent.Compliance.Name, monitorName, alertMask, ExchangeComponent.Compliance.Name, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Compliance.EscalationTeam, escalationSubject, escalationMessage, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			this.WriteTrace(string.Format("ELCProcessing: Created EscalateResponder Definition {0}.", responderName));
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0004350A File Offset: 0x0004170A
		private void WriteTrace(string message)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ELCAssistantTracer, base.TraceContext, message, null, "WriteTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ELC\\ELCDiscovery.cs", 529);
		}

		// Token: 0x04000788 RID: 1928
		private const string TransientMonitorEnabledAttributeName = "ELCTransientMonitorEnabled";

		// Token: 0x04000789 RID: 1929
		private const string TransientMonitorIntervalAttributeName = "ELCTransientMonitorInterval";

		// Token: 0x0400078A RID: 1930
		private const string TransientMonitorRecurranceIntervalAttributeName = "ELCTransientMonitorRecurranceInterval";

		// Token: 0x0400078B RID: 1931
		private const string TransientMonitorMinErrorCountAttributeName = "ELCTransientMonitorMinErrorCount";

		// Token: 0x0400078C RID: 1932
		private const string TransientMonitorName = "ELCTransientMonitor";

		// Token: 0x0400078D RID: 1933
		private const string TransientResponderName = "ELCTransientEscalateResponder";

		// Token: 0x0400078E RID: 1934
		private const string PermanentMonitorEnabledAttributeName = "ELCPermanentMonitorEnabled";

		// Token: 0x0400078F RID: 1935
		private const string PermanentMonitorIntervalAttributeName = "ELCPermanentMonitorInterval";

		// Token: 0x04000790 RID: 1936
		private const string PermanentMonitorRecurranceIntervalAttributeName = "ELCPermanentMonitorRecurranceInterval";

		// Token: 0x04000791 RID: 1937
		private const string PermanentMonitorMinErrorCountAttributeName = "ELCPermanentMonitorMinErrorCount";

		// Token: 0x04000792 RID: 1938
		private const string PermanentMonitorName = "ELCPermanentMonitor";

		// Token: 0x04000793 RID: 1939
		private const string PermanentResponderName = "ELCPermanentEscalateResponder";

		// Token: 0x04000794 RID: 1940
		private const string MailboxSLAMonitorEnabledAttributeName = "ELCMailboxSLAMonitorEnabled";

		// Token: 0x04000795 RID: 1941
		private const string MailboxSLAMonitorIntervalAttributeName = "ELCMailboxSLAMonitorInterval";

		// Token: 0x04000796 RID: 1942
		private const string MailboxSLAMonitorRecurranceIntervalAttributeName = "ELCMailboxSLAMonitorRecurranceInterval";

		// Token: 0x04000797 RID: 1943
		private const string MailboxSLAMonitorMinErrorCountAttributeName = "ELCMailboxSLAMonitorMinErrorCount";

		// Token: 0x04000798 RID: 1944
		private const string MailboxSLAMonitorName = "ELCMailboxSLAMonitor";

		// Token: 0x04000799 RID: 1945
		private const string MailboxSLAResponderName = "ELCMailboxSLAEscalateResponder";

		// Token: 0x0400079A RID: 1946
		private const string ELCDumpsterWarningExceededMonitorEnabledName = "ELCDumpsterWarningExceededMonitorEnabled";

		// Token: 0x0400079B RID: 1947
		private const string ELCDumpsterWarningExceededMonitorIntervalName = "ELCDumpsterWarningExceededMonitorInterval";

		// Token: 0x0400079C RID: 1948
		private const string ELCDumpsterWarningExceededMonitorRecurranceIntervalName = "ELCDumpsterWarningExceededMonitorRecurranceInterval";

		// Token: 0x0400079D RID: 1949
		private const string ELCDumpsterWarningExceededMonitorMinErrorCountName = "ELCDumpsterWarningExceededMonitorMinErrorCount";

		// Token: 0x0400079E RID: 1950
		private const string DumpsterWarningExceededMonitorName = "ELCDumpsterWarningExceededMonitor";

		// Token: 0x0400079F RID: 1951
		private const string DumpsterWarningExceededResponderName = "ELCDumpsterWarningExceededResponder";

		// Token: 0x040007A0 RID: 1952
		private const string ELCArchiveDumpsterWarningExceededMonitorEnabledName = "ELCArchiveDumpsterWarningExceededMonitorEnabled";

		// Token: 0x040007A1 RID: 1953
		private const string ELCArchiveDumpsterWarningExceededMonitorIntervalName = "ELCArchiveDumpsterWarningExceededMonitorInterval";

		// Token: 0x040007A2 RID: 1954
		private const string ELCArchiveDumpsterWarningExceededMonitorRecurranceIntervalName = "ELCArchiveDumpsterWarningExceededMonitorRecurranceInterval";

		// Token: 0x040007A3 RID: 1955
		private const string ELCArchiveDumpsterWarningExceededMonitorMinErrorCountName = "ELCArchiveDumpsterWarningExceededMonitorMinErrorCount";

		// Token: 0x040007A4 RID: 1956
		private const string ArchiveDumpsterWarningExceededMonitorName = "ELCArchiveDumpsterWarningExceededMonitor";

		// Token: 0x040007A5 RID: 1957
		private const string ArchiveDumpsterWarningExceededResponderName = "ELCArchiveDumpsterWarningExceededResponder";

		// Token: 0x040007A6 RID: 1958
		private const string UnrecoverableTimeAttributeName = "ELCUnrecoverableTransitionTimeSpan";

		// Token: 0x040007A7 RID: 1959
		private static TracingContext traceContext = new TracingContext();
	}
}

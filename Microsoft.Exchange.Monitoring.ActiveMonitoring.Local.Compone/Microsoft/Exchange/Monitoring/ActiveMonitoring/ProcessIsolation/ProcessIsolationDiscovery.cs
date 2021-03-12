using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.LogAnalyzer.Analyzers.EventLog;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation
{
	// Token: 0x02000294 RID: 660
	public sealed class ProcessIsolationDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060012AE RID: 4782 RVA: 0x00081634 File Offset: 0x0007F834
		internal static ResponderDefinition CreateEscalateResponder(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string escalationSubject = string.Empty;
			string escalationMessage = string.Empty;
			string value = monitor.Attributes["TriggerType"];
			ProcessTrigger processTrigger;
			Enum.TryParse<ProcessTrigger>(value, out processTrigger);
			string processName = Strings.UncategorizedProcess;
			Component component = ExchangeComponent.Eds;
			ProcessConfiguration processConfiguration = null;
			if (!string.IsNullOrEmpty(monitor.TargetResource))
			{
				processName = monitor.TargetResource;
				processConfiguration = ProcessIsolationDiscovery.GetProcessConfiguration(processName);
				if (processConfiguration != null)
				{
					component = processConfiguration.Component;
				}
			}
			string escalationTeam = component.EscalationTeam;
			NotificationServiceClass notificationServiceClass = NotificationServiceClass.Scheduled;
			switch (processTrigger)
			{
			case ProcessTrigger.PrivateWorkingSetTrigger_Warning:
				escalationSubject = Strings.PrivateWorkingSetExceededWarningThresholdSubject(processName);
				escalationMessage = Strings.PrivateWorkingSetExceededWarningThresholdMessage(processName);
				break;
			case ProcessTrigger.PrivateWorkingSetTrigger_Error:
				escalationSubject = Strings.PrivateWorkingSetExceededErrorThresholdSubject(processName);
				escalationMessage = Strings.PrivateWorkingSetExceededErrorThresholdMessage(processName);
				break;
			case ProcessTrigger.ProcessProcessorTimeTrigger_Warning:
				escalationSubject = Strings.ProcessorTimeExceededWarningThresholdSubject(processName);
				if (processConfiguration != null && processConfiguration.Responders != null && processConfiguration.Responders.ProcessProcessorTimeTriggerWarningResponders != null && processConfiguration.Responders.ProcessProcessorTimeTriggerWarningResponders.ContainsValue(new ResponderDefinitionDelegate(ProcessIsolationDiscovery.ProcessorAffinitize)))
				{
					escalationMessage = Strings.ProcessorTimeExceededWarningThresholdWithAffinitizationMessage(processName);
				}
				else
				{
					escalationMessage = Strings.ProcessorTimeExceededWarningThresholdMessage(processName);
				}
				break;
			case ProcessTrigger.ProcessProcessorTimeTrigger_Error:
				escalationSubject = Strings.ProcessorTimeExceededErrorThresholdSubject(processName);
				if (processConfiguration != null && processConfiguration.Responders != null && processConfiguration.Responders.ProcessProcessorTimeTriggerErrorResponders != null && processConfiguration.Responders.ProcessProcessorTimeTriggerErrorResponders.ContainsValue(new ResponderDefinitionDelegate(ProcessIsolationDiscovery.ProcessorAffinitize)))
				{
					escalationMessage = Strings.ProcessorTimeExceededErrorThresholdWithAffinitizationMessage(processName);
				}
				else
				{
					escalationMessage = Strings.ProcessorTimeExceededErrorThresholdMessage(processName);
				}
				break;
			case ProcessTrigger.ExchangeCrashEventTrigger_Error:
			{
				escalationSubject = Strings.ExchangeCrashExceededErrorThresholdSubject(processName);
				escalationMessage = Strings.ExchangeCrashExceededErrorThresholdMessage(processName);
				object obj = null;
				if (processConfiguration != null && processConfiguration.Parameters != null && processConfiguration.Parameters.TryGetValue("CrashEventNotificationServiceClass", out obj) && obj is NotificationServiceClass)
				{
					notificationServiceClass = (NotificationServiceClass)obj;
				}
				else
				{
					notificationServiceClass = NotificationServiceClass.Urgent;
				}
				break;
			}
			case ProcessTrigger.LongRunningWatsonTrigger_Warning:
			case ProcessTrigger.LongRunningWerMgrTrigger_Warning:
				escalationSubject = Strings.LongRunningWerMgrTriggerWarningThresholdSubject(processName);
				escalationMessage = Strings.LongRunningWerMgrTriggerWarningThresholdMessage(processName);
				notificationServiceClass = NotificationServiceClass.UrgentInTraining;
				break;
			}
			return ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(() => EscalateResponder.CreateDefinition(monitor.Name + "Escalate", ExchangeComponent.ProcessIsolation.Name, monitor.Name, NotificationItem.GenerateResultName(monitor.Name, monitor.TargetResource, null), monitor.TargetResource, status, escalationTeam, escalationSubject, escalationMessage, true, notificationServiceClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false));
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00081905 File Offset: 0x0007FB05
		internal static ResponderDefinition RestartProcess(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.CreateProcessRestartResponder(monitor, status, false);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0008190F File Offset: 0x0007FB0F
		internal static ResponderDefinition DumpAndRestartProcess(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.CreateProcessRestartResponder(monitor, status, true);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000819B4 File Offset: 0x0007FBB4
		internal static ResponderDefinition GetF1Trace(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(() => F1TraceResponder.CreateDefinition(monitor.Name + "F1Trace", ExchangeComponent.ProcessIsolation.Name, monitor.Name, NotificationItem.GenerateResultName(monitor.Name, monitor.TargetResource, null), monitor.TargetResource, status, 1, TimeSpan.FromMinutes(4.0), 0, string.Empty, string.Empty, monitor.TargetResource));
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000819E6 File Offset: 0x0007FBE6
		internal static ResponderDefinition WatsonInformationalWithF1Trace(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.WatsonWithF1Trace(ReportOptions.DoNotCollectDumps, monitor, status);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000819F0 File Offset: 0x0007FBF0
		internal static ResponderDefinition WatsonWithF1Trace(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.WatsonWithF1Trace(ReportOptions.None, monitor, status);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000819FA File Offset: 0x0007FBFA
		internal static ResponderDefinition WatsonInformational(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.Watson("E12IIS", ReportOptions.DoNotCollectDumps, monitor, status);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00081A09 File Offset: 0x0007FC09
		internal static ResponderDefinition WatsonWithDump(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			return ProcessIsolationDiscovery.Watson("E12IIS", ReportOptions.None, monitor, status);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00081A44 File Offset: 0x0007FC44
		internal static ResponderDefinition RestartNodeRunner(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string responderName = SearchStrings.HostControllerServiceRestartNodeResponderName(monitor.Name);
			string alertMask = monitor.ConstructWorkItemResultName();
			ProcessConfiguration processConfiguration = ProcessIsolationDiscovery.GetProcessConfiguration(monitor.TargetResource);
			string nodeName = processConfiguration.Parameters["NodeName"].ToString();
			ResponderDefinition responderDefinition = ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(() => RestartNodeResponder.CreateDefinition(responderName, alertMask, status, nodeName, 0, null));
			responderDefinition.TargetResource = monitor.TargetResource;
			responderDefinition.Enabled = true;
			return responderDefinition;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00081B20 File Offset: 0x0007FD20
		internal static ResponderDefinition ProcessorAffinitize(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string responderName = monitor.Name + "Affinitize";
			string alertMask = monitor.ConstructWorkItemResultName();
			string targetResource = monitor.TargetResource;
			ProcessConfiguration processConfiguration = ProcessIsolationDiscovery.GetProcessConfiguration(targetResource);
			object processorAffinityCountObject;
			if (processConfiguration == null || processConfiguration.Parameters == null || !processConfiguration.Parameters.TryGetValue("ProcessorAffinityCount", out processorAffinityCountObject) || !(processorAffinityCountObject is int))
			{
				throw new InvalidOperationException(string.Format("The '{0}' parameter for process '{1}' is not valid or not found.", "ProcessorAffinityCount", targetResource));
			}
			object avoidProcessorCountObject;
			if (!processConfiguration.Parameters.TryGetValue("AvoidProcessorCount", out avoidProcessorCountObject) || !(avoidProcessorCountObject is int))
			{
				avoidProcessorCountObject = 0;
			}
			return ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(() => ProcessorAffinityResponder.CreateDefinition(responderName, ExchangeComponent.ProcessIsolation.Name, alertMask, monitor.TargetResource, status, (int)processorAffinityCountObject, (int)avoidProcessorCountObject));
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00081C0C File Offset: 0x0007FE0C
		internal static ResponderDefinition KillProcess(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string processName = ProcessIsolationDiscovery.ProcessNameNoInstance(monitor.TargetResource);
			return ProcessKillResponder.CreateDefinition(monitor.Name + "KillProcess", monitor.Name, processName, status, false, "Exchange", true, 60, null, monitor.SampleMask);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00081C54 File Offset: 0x0007FE54
		internal static ResponderDefinition DeleteWatsonTempDumpFiles(MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string[] array = new string[]
			{
				"WER*.TMP.HDMP",
				"WER*.TMP.MDMP",
				"WER*.*.CAB.TMP",
				"WER*.*.CAB",
				"WER*.TMP.WMI.TXT",
				"WER*.TMP.REG.TXT"
			};
			string environmentVariable = Environment.GetEnvironmentVariable("SYSTEMROOT");
			string[] array2 = new string[]
			{
				Path.Combine(environmentVariable, "SERVICEPROFILES\\LOCALSERVICE\\APPDATA\\LOCAL\\TEMP"),
				Path.Combine(environmentVariable, "SERVICEPROFILES\\NETWORKSERVICE\\APPDATA\\LOCAL\\TEMP"),
				Path.Combine(environmentVariable, "TEMP")
			};
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string path in array2)
			{
				foreach (string path2 in array)
				{
					stringBuilder.AppendFormat("{0};", Path.Combine(path, path2));
				}
			}
			return FileDeleteResponder.CreateDefinition(monitor.Name + "DeleteFile", monitor.Name, stringBuilder.ToString(), status, true, 60, null);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00081D60 File Offset: 0x0007FF60
		internal static ProcessConfiguration GetProcessConfiguration(string processName)
		{
			ProcessConfiguration result = null;
			if (ProcessIsolationMonitor.ProcessConfigurationDictionary.TryGetValue(processName, out result) || ProcessIsolationMonitor.ProcessConfigurationDictionary.TryGetValue(ProcessIsolationDiscovery.ProcessNameNoInstance(processName), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00081D98 File Offset: 0x0007FF98
		internal static string GetPidsFromErrorText(string inputString)
		{
			string result = null;
			Match match = ProcessIsolationDiscovery.PidRegEx.Match(inputString);
			if (match.Success && match.Groups.Count > 0)
			{
				result = match.Groups[1].Value;
			}
			return result;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00081DDC File Offset: 0x0007FFDC
		internal static ResponderDefinition CreateDefinitionAndMarkProcessIsolation(Func<ResponderDefinition> createDefinition)
		{
			ResponderDefinition responderDefinition = createDefinition();
			responderDefinition.ResponderCategory = ResponderCategory.ProcessIsolation.ToString();
			return responderDefinition;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00081E04 File Offset: 0x00080004
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProcessIsolationTracer, ProcessIsolationDiscovery.traceContext, "ProcessIsolationDiscovery.DoWork: Started", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessIsolation\\ProcessIsolationDiscovery.cs", 536);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProcessIsolationTracer, ProcessIsolationDiscovery.traceContext, "ProcessIsolationDiscovery.DoWork: Creating non critical process monitors and responders.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessIsolation\\ProcessIsolationDiscovery.cs", 537);
			if (FfoLocalEndpointManager.IsForefrontForOfficeDatacenter)
			{
				ProcessIsolationMonitor.ProcessExclusionList.Add("edgetransport");
			}
			if (ProcessIsolationDiscovery.GetConfigBool("StackTraceAnalysisEnabled", false) && (LocalEndpointManager.IsDataCenter || FfoLocalEndpointManager.IsForefrontForOfficeDatacenter))
			{
				this.CreateSubComponentMonitorsAndResponders(ProcessIsolationMonitor.ProcessConfigurationDictionary, ProcessIsolationMonitor.SubComponentConfigurationList);
			}
			this.CreateMonitorsAndResponders(ProcessIsolationMonitor.ProcessConfigurationDictionary);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProcessIsolationTracer, ProcessIsolationDiscovery.traceContext, "ProcessIsolationDiscovery.DoWork: Creating process monitors and responders.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessIsolation\\ProcessIsolationDiscovery.cs", 550);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00081ECC File Offset: 0x000800CC
		private static bool GetConfigBool(string key, bool defaultValue)
		{
			try
			{
				string value = ConfigurationManager.AppSettings[key];
				bool result;
				if (bool.TryParse(value, out result))
				{
					return result;
				}
			}
			catch
			{
			}
			return defaultValue;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00081F9C File Offset: 0x0008019C
		private static ResponderDefinition Watson(string watsonEventType, ReportOptions reportOptions, MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string commonMonitorName = ProcessIsolationDiscovery.MonitorCommonName(monitor.Name);
			return ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(() => WatsonResponder.CreateDefinition(monitor.Name + "Watson", ExchangeComponent.ProcessIsolation.Name, monitor.Name, NotificationItem.GenerateResultName(monitor.Name, monitor.TargetResource, null), monitor.TargetResource, status, commonMonitorName + "Exception", watsonEventType, reportOptions));
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0008209C File Offset: 0x0008029C
		private static ResponderDefinition WatsonWithF1Trace(ReportOptions reportOptions, MonitorDefinition monitor, ServiceHealthStatus status)
		{
			string commonMonitorName = ProcessIsolationDiscovery.MonitorCommonName(monitor.Name);
			return ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(delegate
			{
				string name = monitor.Name + "WatsonF1Trace";
				string name2 = ExchangeComponent.ProcessIsolation.Name;
				string name3 = monitor.Name;
				string alertMask = NotificationItem.GenerateResultName(monitor.Name, monitor.TargetResource, null);
				string targetResource = monitor.TargetResource;
				ServiceHealthStatus status2 = status;
				string exceptionType = commonMonitorName + "Exception";
				string empty = string.Empty;
				string empty2 = string.Empty;
				TimeSpan duration = TimeSpan.FromMinutes(4.0);
				return WatsonF1TraceResponder.CreateDefinition(name, name2, name3, alertMask, targetResource, status2, exceptionType, reportOptions, empty, empty2, duration);
			});
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000820EC File Offset: 0x000802EC
		private static ResponderDefinition CreateProcessRestartResponder(MonitorDefinition monitor, ServiceHealthStatus status, bool dump)
		{
			ProcessConfiguration processConfiguration = ProcessIsolationDiscovery.GetProcessConfiguration(monitor.TargetResource);
			DumpMode dumpMode = DumpMode.None;
			if (dump)
			{
				dumpMode = DumpMode.FullDump;
			}
			ResponderDefinition responderDefinition;
			if (processConfiguration.ProcessType == ProcessType.AppPool)
			{
				responderDefinition = ProcessIsolationDiscovery.CreateResetIISAppPoolResponder(monitor, status, processConfiguration, dumpMode);
			}
			else
			{
				responderDefinition = ProcessIsolationDiscovery.CreateRestartServiceResponder(monitor, status, processConfiguration, dumpMode);
			}
			ProcessIsolationDiscovery.MonitorCommonName(monitor.Name);
			responderDefinition.TargetResource = monitor.TargetResource;
			responderDefinition.AlertMask = NotificationItem.GenerateResultName(monitor.Name, monitor.TargetResource, null);
			return responderDefinition;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000821E4 File Offset: 0x000803E4
		private static ResponderDefinition CreateRestartServiceResponder(MonitorDefinition monitor, ServiceHealthStatus status, ProcessConfiguration configuration, DumpMode dumpMode)
		{
			string processName = monitor.TargetResource + ".exe";
			string serviceName = ProcessIsolationDiscovery.GetServiceNameForProcess(processName, configuration);
			return ProcessIsolationDiscovery.CreateDefinitionAndMarkProcessIsolation(() => RestartServiceResponder.CreateDefinition(monitor.Name + "Restart", monitor.Name, serviceName, status, 15, 120, 0, false, dumpMode, null, 15.0, 0, ExchangeComponent.ProcessIsolation.Name, null, true, VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.ProcessIsolationRestartServiceResponder.Enabled, null, false));
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00082240 File Offset: 0x00080440
		private static ResponderDefinition CreateResetIISAppPoolResponder(MonitorDefinition monitor, ServiceHealthStatus status, ProcessConfiguration configuration, DumpMode dumpMode)
		{
			return ResetIISAppPoolResponder.CreateDefinition(monitor.Name + "ResetAppPool", monitor.Name, monitor.TargetResource, status, dumpMode, null, 15.0, 0, "Exchange", VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.ProcessIsolationResetIISAppPoolResponder.Enabled, null);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00082298 File Offset: 0x00080498
		private static string GetServiceNameForProcess(string processName, ProcessConfiguration configuration)
		{
			string result = string.Empty;
			if (configuration != null && configuration.Parameters != null)
			{
				object obj = null;
				if (configuration.Parameters.TryGetValue("ServiceName", out obj))
				{
					return (string)obj;
				}
			}
			result = processName;
			List<ProcessIsolationDiscovery.Win32Service> services = ProcessIsolationDiscovery.Win32Service.GetServices();
			foreach (ProcessIsolationDiscovery.Win32Service win32Service in services)
			{
				string fileName = win32Service.FileName;
				if (string.Compare(processName, fileName, true) == 0)
				{
					result = win32Service.Name;
					break;
				}
			}
			return result;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00082334 File Offset: 0x00080534
		private static string MonitorCommonName(string monitorName)
		{
			if (!string.IsNullOrEmpty(monitorName))
			{
				int num = monitorName.IndexOf('.');
				if (num > 0)
				{
					return monitorName.Substring(0, num);
				}
			}
			return monitorName;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00082360 File Offset: 0x00080560
		private static string ProcessNameNoInstance(string processName)
		{
			return ProcessIsolationDiscovery.CounterInstanceRegEx.Replace(processName, string.Empty);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000823C4 File Offset: 0x000805C4
		private void CreateSubComponentMonitorsAndResponders(Dictionary<string, ProcessConfiguration> configuration, List<SubComponentConfiguration> subComponentConfiguration)
		{
			Dictionary<string, ProcessConfiguration> dictionary = new Dictionary<string, ProcessConfiguration>();
			foreach (string text in configuration.Keys)
			{
				StackTraceAnalysisProcessNames processValue;
				if (Enum.TryParse<StackTraceAnalysisProcessNames>(text, true, out processValue))
				{
					ProcessConfiguration processConfiguration = configuration[text];
					List<SubComponentConfiguration> list = new List<SubComponentConfiguration>(from subComponent in subComponentConfiguration
					where subComponent.Process.Equals(processValue)
					select subComponent);
					if (list.Count != 0)
					{
						Dictionary<ProcessTrigger, List<CorrelatedMonitorInfo>> dictionary2 = new Dictionary<ProcessTrigger, List<CorrelatedMonitorInfo>>();
						List<ProcessTrigger> list2 = new List<ProcessTrigger>();
						bool flag = false;
						foreach (SubComponentConfiguration subComponentConfiguration2 in list)
						{
							Component component = (subComponentConfiguration2.EscalationComponent != null) ? subComponentConfiguration2.EscalationComponent : processConfiguration.Component;
							string key = string.Format("{0}_{1}", text, subComponentConfiguration2.SubComponent);
							ProcessConfiguration value = new ProcessConfiguration(component, processConfiguration.ProcessType, processConfiguration.Responders, new Dictionary<string, object>
							{
								{
									"SubComponent",
									subComponentConfiguration2
								}
							});
							dictionary.Add(key, value);
							flag |= subComponentConfiguration2.AddCorrelation;
							if (!list2.Contains(subComponentConfiguration2.TriggerType))
							{
								list2.Add(subComponentConfiguration2.TriggerType);
							}
						}
						if (flag)
						{
							foreach (ProcessTrigger processTrigger in list2)
							{
								string text2 = ProcessIsolationMonitor.BuildMonitorName(processTrigger, string.Format("{0}_{1}", text, "Correlation"));
								string identity = string.Format("{0}\\{1}\\{2}", processConfiguration.Component.Name, text2, text);
								CorrelatedMonitorInfo correlatedMonitorInfo = new CorrelatedMonitorInfo(identity, "*", CorrelatedMonitorInfo.MatchMode.Wildcard);
								dictionary2.Add(processTrigger, new List<CorrelatedMonitorInfo>(new CorrelatedMonitorInfo[]
								{
									correlatedMonitorInfo
								}));
								this.CreateCorrelatedMonitorAndResponder(text2, text, processTrigger);
							}
							if (dictionary2.Count > 0)
							{
								processConfiguration.Parameters.Add("Correlation", dictionary2);
							}
						}
						foreach (IGrouping<ProcessTrigger, SubComponentConfiguration> grouping in from sub in list
						group sub by sub.TriggerType)
						{
							for (int i = 1; i <= 5; i++)
							{
								StackTraceAnalysisComponentNames stackTraceAnalysisComponentNames = (StackTraceAnalysisComponentNames)Enum.Parse(typeof(StackTraceAnalysisComponentNames), string.Format("GenericAgent{0}", i), true);
								string key2 = string.Format("{0}_{1}", text, stackTraceAnalysisComponentNames.ToString());
								ProcessConfiguration value2 = new ProcessConfiguration(processConfiguration.Component, processConfiguration.ProcessType, processConfiguration.Responders, new Dictionary<string, object>
								{
									{
										"SubComponent",
										new SubComponentConfiguration(processValue, stackTraceAnalysisComponentNames, processConfiguration.Component, grouping.Key, true)
									}
								});
								dictionary.Add(key2, value2);
							}
						}
					}
				}
			}
			dictionary.ToList<KeyValuePair<string, ProcessConfiguration>>().ForEach(delegate(KeyValuePair<string, ProcessConfiguration> kv)
			{
				configuration.Add(kv.Key, kv.Value);
			});
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00082788 File Offset: 0x00080988
		private void CreateCorrelatedMonitorAndResponder(string monitorName, string process, ProcessTrigger trigger)
		{
			MonitorDefinition monitorDefinition = ProcessIsolationMonitor.CreateMonitor(process, trigger);
			monitorDefinition.Name = monitorName;
			monitorDefinition.SampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, trigger.ToString(), string.Format("{0}_{1}", process, "Correlation"));
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate Process Isolation health is not impacted by any issues";
			monitorDefinition.AllowCorrelationToMonitor = true;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, TimeSpan.FromHours(4.0))
			};
			ResponderDefinition definition = ProcessIsolationDiscovery.CreateEscalateResponder(monitorDefinition, ServiceHealthStatus.Unhealthy);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, ProcessIsolationDiscovery.traceContext);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, ProcessIsolationDiscovery.traceContext);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00082840 File Offset: 0x00080A40
		private void CreateMonitorsAndResponders(Dictionary<string, ProcessConfiguration> configuration)
		{
			foreach (string text in configuration.Keys)
			{
				ProcessConfiguration processConfiguration = configuration[text];
				SubComponentConfiguration subComponentConfiguration = null;
				if (processConfiguration.Parameters.ContainsKey("SubComponent"))
				{
					subComponentConfiguration = (SubComponentConfiguration)processConfiguration.Parameters["SubComponent"];
				}
				if (processConfiguration.ShouldRunOnLocalServer == null || processConfiguration.ShouldRunOnLocalServer())
				{
					foreach (object obj in ProcessIsolationDiscovery.ProcessTriggerTypes)
					{
						ProcessTrigger processTrigger = (ProcessTrigger)obj;
						if ((ProcessTrigger.ExchangeCrashEventTrigger_Error != processTrigger || !ProcessIsolationDiscovery.CrashAlertExcludedComponents.Contains(processConfiguration.Component)) && (subComponentConfiguration == null || processTrigger == subComponentConfiguration.TriggerType) && !ProcessIsolationDiscovery.DisabledCategories.Contains(processTrigger))
						{
							Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> monitorStateTransitions = ProcessIsolationMonitor.GetMonitorStateTransitions(text, processTrigger);
							if (subComponentConfiguration != null)
							{
								subComponentConfiguration.Process.ToString().ToLower();
							}
							MonitorDefinition monitorDefinition = ProcessIsolationMonitor.CreateMonitor(text, processTrigger);
							monitorDefinition.ServicePriority = 2;
							monitorDefinition.ScenarioDescription = "Validate Process Isolation health is not impacted by any issues";
							monitorDefinition.Enabled = ProcessIsolationDiscovery.EnableMonitor(monitorDefinition);
							if (monitorStateTransitions != null && monitorStateTransitions.Count > 0)
							{
								if (subComponentConfiguration != null)
								{
									monitorDefinition.Name = ProcessIsolationMonitor.BuildSubComponentName(processTrigger, subComponentConfiguration.Process.ToString(), subComponentConfiguration.SubComponent.ToString());
								}
								Dictionary<ProcessTrigger, List<CorrelatedMonitorInfo>> dictionary = null;
								if (processConfiguration.Parameters.ContainsKey("Correlation"))
								{
									dictionary = (Dictionary<ProcessTrigger, List<CorrelatedMonitorInfo>>)processConfiguration.Parameters["Correlation"];
								}
								List<MonitorStateTransition> list = new List<MonitorStateTransition>();
								foreach (KeyValuePair<MonitorStateTransition, ResponderDefinitionDelegate> keyValuePair in monitorStateTransitions)
								{
									if (dictionary != null)
									{
										list.Add(new MonitorStateTransition(keyValuePair.Key.ToState, keyValuePair.Key.TransitionTimeout + TimeSpan.FromMinutes(5.0)));
									}
									else
									{
										list.Add(keyValuePair.Key);
									}
								}
								monitorDefinition.MonitorStateTransitions = list.ToArray();
								if (dictionary != null || subComponentConfiguration != null)
								{
									monitorDefinition.AllowCorrelationToMonitor = true;
								}
								base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, ProcessIsolationDiscovery.traceContext);
								int num = 0;
								foreach (MonitorStateTransition monitorStateTransition in monitorStateTransitions.Keys)
								{
									ResponderDefinitionDelegate responderDefinitionDelegate = monitorStateTransitions[monitorStateTransition];
									if (responderDefinitionDelegate == null)
									{
										responderDefinitionDelegate = new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder);
									}
									ResponderDefinition responderDefinition = responderDefinitionDelegate(monitorDefinition, monitorStateTransition.ToState);
									responderDefinition.TargetHealthState = monitorStateTransition.ToState;
									responderDefinition.ServiceName = monitorDefinition.ServiceName;
									if (dictionary != null && dictionary.ContainsKey(processTrigger))
									{
										responderDefinition.ActionOnCorrelatedMonitors = CorrelatedMonitorAction.GenerateException;
										responderDefinition.CorrelatedMonitors = dictionary[processTrigger].ToArray();
									}
									base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, ProcessIsolationDiscovery.traceContext);
									num++;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00082BE0 File Offset: 0x00080DE0
		private static bool EnableMonitor(MonitorDefinition monitor)
		{
			bool result = true;
			if (ExEnvironment.IsTest || ExEnvironment.IsTestDomain)
			{
				result = false;
				bool flag = ProcessIsolationDiscovery.MonitorEnabled("SOFTWARE\\Microsoft\\ExchangeTest\\v15\\ProcessIsolation\\Monitors");
				if (flag)
				{
					result = true;
				}
				else
				{
					string regKeyPath = string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\ExchangeTest\\v15\\ProcessIsolation\\Monitors", monitor.Name);
					bool flag2 = ProcessIsolationDiscovery.MonitorEnabled(regKeyPath);
					if (flag2)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00082C34 File Offset: 0x00080E34
		private static bool MonitorEnabled(string regKeyPath)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKeyPath))
			{
				if (registryKey != null)
				{
					string value = registryKey.GetValue("Enabled", 0).ToString();
					return "1".Equals(value, StringComparison.OrdinalIgnoreCase);
				}
			}
			return false;
		}

		// Token: 0x04000E0E RID: 3598
		internal const string CrashEventNotificationServiceClassParameterName = "CrashEventNotificationServiceClass";

		// Token: 0x04000E0F RID: 3599
		private const string SubComponentFormat = "{0}_{1}";

		// Token: 0x04000E10 RID: 3600
		private const string FullMonitorNameFormat = "{0}\\{1}\\{2}";

		// Token: 0x04000E11 RID: 3601
		private const string GenericComponetName = "GenericAgent{0}";

		// Token: 0x04000E12 RID: 3602
		private const string WildcardCharacter = "*";

		// Token: 0x04000E13 RID: 3603
		private const string CorrelationParameter = "Correlation";

		// Token: 0x04000E14 RID: 3604
		private const string StackTraceAnalysisEnabledConfigString = "StackTraceAnalysisEnabled";

		// Token: 0x04000E15 RID: 3605
		internal const string ProcessIsolationTestRegKeyPath = "SOFTWARE\\Microsoft\\ExchangeTest\\v15\\ProcessIsolation\\Monitors";

		// Token: 0x04000E16 RID: 3606
		internal const string EnableMonitorInTestSubKey = "Enabled";

		// Token: 0x04000E17 RID: 3607
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000E18 RID: 3608
		private static readonly TracingContext traceContext = new TracingContext();

		// Token: 0x04000E19 RID: 3609
		private static readonly Array ProcessTriggerTypes = Enum.GetValues(typeof(ProcessTrigger));

		// Token: 0x04000E1A RID: 3610
		private static readonly List<ProcessTrigger> DisabledCategories = new List<ProcessTrigger>();

		// Token: 0x04000E1B RID: 3611
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> EscalateOnlyMonitorStateTransition = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E1C RID: 3612
		private static readonly HashSet<Component> CrashAlertExcludedComponents = new HashSet<Component>
		{
			ExchangeComponent.EventAssistants,
			ExchangeComponent.Store
		};

		// Token: 0x04000E1D RID: 3613
		private static readonly Regex PidRegEx = new Regex("Affected_Process_ID=(\\d+(,*\\d+))", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000E1E RID: 3614
		private static readonly Regex CounterInstanceRegEx = new Regex("\\#(\\d+)\\Z", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x02000295 RID: 661
		private class Win32Service
		{
			// Token: 0x060012CF RID: 4815 RVA: 0x00082D55 File Offset: 0x00080F55
			public Win32Service(string name, string fileName)
			{
				this.Name = name;
				this.FileName = fileName;
			}

			// Token: 0x060012D0 RID: 4816 RVA: 0x00082D6C File Offset: 0x00080F6C
			public static List<ProcessIsolationDiscovery.Win32Service> GetServices()
			{
				DateTime utcNow = DateTime.UtcNow;
				if (ProcessIsolationDiscovery.Win32Service.services == null || utcNow - ProcessIsolationDiscovery.Win32Service.discoveryTime > TimeSpan.FromMinutes(5.0))
				{
					ProcessIsolationDiscovery.Win32Service.services = ProcessIsolationDiscovery.Win32Service.InternalGetServices();
					ProcessIsolationDiscovery.Win32Service.discoveryTime = utcNow;
				}
				return ProcessIsolationDiscovery.Win32Service.services;
			}

			// Token: 0x060012D1 RID: 4817 RVA: 0x00082DBC File Offset: 0x00080FBC
			private static List<ProcessIsolationDiscovery.Win32Service> InternalGetServices()
			{
				List<ProcessIsolationDiscovery.Win32Service> list = new List<ProcessIsolationDiscovery.Win32Service>();
				using (ManagementClass managementClass = new ManagementClass("Win32_Service"))
				{
					using (ManagementObjectCollection instances = managementClass.GetInstances())
					{
						foreach (ManagementBaseObject managementBaseObject in instances)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								string text = (string)managementObject.GetPropertyValue("PathName");
								if (text.StartsWith("\""))
								{
									int num = text.IndexOf("\"", 1);
									if (num > 1)
									{
										text = text.Substring(1, num - 1);
									}
								}
								else if (!text.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
								{
									int num = text.IndexOf(" ");
									if (num > 0)
									{
										text = text.Substring(0, num);
									}
								}
								string text2 = Path.GetFileName(text);
								if (!text2.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
								{
									text2 += ".exe";
								}
								string name = (string)managementObject.GetPropertyValue("Name");
								list.Add(new ProcessIsolationDiscovery.Win32Service(name, text2));
							}
						}
					}
				}
				return list;
			}

			// Token: 0x04000E20 RID: 3616
			public static DateTime discoveryTime;

			// Token: 0x04000E21 RID: 3617
			public static List<ProcessIsolationDiscovery.Win32Service> services;

			// Token: 0x04000E22 RID: 3618
			public readonly string Name;

			// Token: 0x04000E23 RID: 3619
			public readonly string FileName;
		}
	}
}

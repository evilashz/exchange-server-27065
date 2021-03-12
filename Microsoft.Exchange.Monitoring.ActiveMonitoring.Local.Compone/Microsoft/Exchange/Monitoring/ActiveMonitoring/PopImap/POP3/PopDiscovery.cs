using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.POP3
{
	// Token: 0x02000290 RID: 656
	public sealed class PopDiscovery : PopImapDiscoveryCommon
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x00080410 File Offset: 0x0007E610
		internal static PopImapAdConfiguration GetConfiguration(TracingContext context)
		{
			PopImapAdConfiguration retVal = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 94, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs");
				retVal = PopImapAdConfiguration.FindOne<Pop3AdConfiguration>(session);
			});
			if (!adoperationResult.Succeeded)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.POP3Tracer, context, "PopDiscovery:: DoWork(): Unable to retrieve Pop Configuration: {0}", adoperationResult.Exception.Message, null, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 104);
			}
			return retVal;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00080474 File Offset: 0x0007E674
		internal ProbeDefinition CreateProbe(IPEndPoint targetEndpoint, MailboxDatabaseInfo dbInfo, string probeTypeName, string probeName, int recurrenceInterval, int timeOut, bool lightMode, bool isMbxProbe, string targetResource, string healthSet)
		{
			WTFDiagnostics.TraceDebug<string, IPEndPoint>(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Creating {0} for {1}", probeName, targetEndpoint, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 140);
			ProbeDefinition probeDefinition = PopImapDiscoveryCommon.CreateProbe(PopDiscovery.AssemblyPath, targetEndpoint, dbInfo, probeTypeName, probeName, recurrenceInterval, timeOut, lightMode, isMbxProbe, targetResource, healthSet);
			probeDefinition.Attributes["IsLocalProbe"] = true.ToString();
			WTFDiagnostics.TraceDebug<string, IPEndPoint>(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Created {0} for {1}", probeName, targetEndpoint, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 162);
			return probeDefinition;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00080508 File Offset: 0x0007E708
		internal MonitorDefinition CreateMonitor(string targetResource, Component component, Type monitorType, string monitorName, int recurrenceInterval, int monitoringInterval, int? monitoringThreshold, string sampleMask)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Creating {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 194);
			MonitorDefinition result = PopImapDiscoveryCommon.CreateMonitor(monitorType.Assembly.Location, targetResource, monitorType, monitorName, recurrenceInterval, monitoringInterval, monitoringThreshold, sampleMask, component);
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Created {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 212);
			return result;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00080584 File Offset: 0x0007E784
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapAdConfiguration configuration = PopDiscovery.GetConfiguration(base.TraceContext);
			if (!PopImapDiscoveryCommon.GetEndpointsFromConfig(configuration, out PopDiscovery.cafeEndpoint, out PopDiscovery.mbxEndpoint))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Failed to Autodetect Pop settings. Using Default values.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 232);
				PopDiscovery.cafeEndpoint = new IPEndPoint(IPAddress.Loopback, PopDiscovery.DefaultCafePort);
				PopDiscovery.mbxEndpoint = new IPEndPoint(IPAddress.Loopback, PopDiscovery.DefaultMbxPort);
			}
			PopDiscovery.endpointManager = LocalEndpointManager.Instance;
			int num = 0;
			int num2 = 0;
			try
			{
				if (PopDiscovery.endpointManager.ExchangeServerRoleEndpoint == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: No Exchange roles installed on server, skipping item creation.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 252);
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, string.Format("PopDiscovery:: DoWork(): ExchangeServerRoleEndpoint initialisation failed.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 261);
				return;
			}
			if (!PopDiscovery.endpointManager.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Mailbox role is not installed on this server, no need to create Pop Mailbox related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 271);
			}
			else
			{
				try
				{
					if (PopDiscovery.endpointManager.MailboxDatabaseEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery:: DoWork(): Could not find MailboxDatabaseEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 284);
						return;
					}
				}
				catch (EndpointManagerEndpointUninitializedException ex2)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, string.Format("PopDiscovery:: DoWork(): MailboxDatabaseEndpoint initialisation failed.  Exception:{0}", ex2.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 290);
					return;
				}
				RegistryKey registryKey = RegistryHelper.OpenKey("SYSTEM\\CurrentControlSet\\services\\", "MSExchangePop3BE", false, false);
				num2 = (int)registryKey.GetValue("Start");
				if (num2 == 2)
				{
					this.CreatePopProtocolContext(true);
					if (PopDiscovery.endpointManager.MailboxDatabaseEndpoint != null)
					{
						foreach (MailboxDatabaseInfo dbInfo in PopDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
						{
							this.CreatePopServiceProbe(true, dbInfo, PopDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count);
						}
						this.CreatePopServiceMonitor(true);
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: Mailbox Pop service is not set to AUTOMATIC, will not create Pop Mailbox related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 320);
				}
			}
			if (!PopDiscovery.endpointManager.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: CAFE role is not installed on this server, no need to create Pop CAFE related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 330);
			}
			else
			{
				try
				{
					if (PopDiscovery.endpointManager.MailboxDatabaseEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery:: DoWork(): Could not find MailboxDatabaseEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 343);
						return;
					}
				}
				catch (Exception ex3)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, string.Format("PopDiscovery:: DoWork(): MailboxDatabaseEndpoint object threw exception.  Exception:{0}", ex3.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 349);
					return;
				}
				RegistryKey registryKey = RegistryHelper.OpenKey("SYSTEM\\CurrentControlSet\\services\\", "MSExchangePop3", false, false);
				num = (int)registryKey.GetValue("Start");
				if (num == 2)
				{
					this.CreatePopProtocolContext(false);
					if (PopDiscovery.endpointManager.MailboxDatabaseEndpoint != null && !VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PopImapDiscoveryCommon.Enabled)
					{
						foreach (MailboxDatabaseInfo dbInfo2 in PopDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe)
						{
							this.CreatePopServiceProbe(false, dbInfo2, PopDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.Count);
						}
						this.CreatePopServiceMonitor(false);
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery.DoWork: CAFE Pop service is not set to AUTOMATIC, will not create Pop CAFE related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 379);
				}
			}
			if (num == 2 || num2 == 2)
			{
				this.CreatePerfAndMonitors();
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000809C0 File Offset: 0x0007EBC0
		private void CreatePopProtocolContext(bool isMbx)
		{
			string text = isMbx ? "MSExchangePop3BE" : "MSExchangePop3";
			ProbeDefinition probeDefinition = this.CreateProbe(isMbx ? PopDiscovery.mbxEndpoint : PopDiscovery.cafeEndpoint, null, PopDiscovery.PopMailboxProbeSSL, isMbx ? string.Format("PopSelfTest{0}", "Probe") : string.Format("PopProxyTest{0}", "Probe"), 60, 115, true, isMbx, text, isMbx ? ExchangeComponent.PopProtocol.Name : ExchangeComponent.PopProxy.Name);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitor(text, isMbx ? ExchangeComponent.PopProtocol : ExchangeComponent.PopProxy, PopDiscovery.OverallConsecutiveProbeFailuresMonitor, isMbx ? string.Format("PopSelfTest{0}", "Monitor") : string.Format("PopProxyTest{0}", "Monitor"), 0, 240, new int?(4), probeDefinition.Name);
			monitorDefinition.IsHaImpacting = true;
			monitorDefinition.MonitorStateTransitions = PopImapDiscoveryCommon.CreateResponderChain(monitorDefinition.Name, text, text, base.Broker, false, isMbx ? PopImapDiscoveryCommon.TargetScope.PST : PopImapDiscoveryCommon.TargetScope.PT, base.TraceContext);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate POP health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00080AF4 File Offset: 0x0007ECF4
		private void CreatePopServiceProbe(bool isMbx, MailboxDatabaseInfo dbInfo, int dbCount)
		{
			int num = 180 * dbCount;
			int num2 = 240 * dbCount;
			if (string.IsNullOrWhiteSpace(dbInfo.MonitoringAccountPassword))
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.POP3Tracer, base.TraceContext, "PopDiscovery:: DoWork(): Ignore mailbox database {0} because it does not have monitoring mailbox", dbInfo.MailboxDatabaseName, null, "CreatePopServiceProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDiscovery.cs", 466);
				return;
			}
			ProbeDefinition definition = this.CreateProbe(isMbx ? PopDiscovery.mbxEndpoint : PopDiscovery.cafeEndpoint, dbInfo, PopDiscovery.PopMailboxProbeSSL, isMbx ? string.Format("PopDeepTest{0}", "Probe") : string.Format("PopCTP{0}", "Probe"), isMbx ? num : num2, 115, false, isMbx, dbInfo.MailboxDatabaseName, isMbx ? ExchangeComponent.PopProtocol.Name : ExchangeComponent.Pop.Name);
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00080BC8 File Offset: 0x0007EDC8
		private void CreatePopServiceMonitor(bool isMbx)
		{
			MonitorDefinition monitorDefinition = PopImapDiscoveryCommon.CreateServiceMonitor(isMbx ? string.Format("PopDeepTest{0}", "Monitor") : string.Format("PopCTP{0}", "Monitor"), isMbx ? string.Format("PopDeepTest{0}", "Probe") : string.Format("PopCTP{0}", "Probe"), isMbx ? ExchangeComponent.PopProtocol : ExchangeComponent.Pop, isMbx);
			if (isMbx)
			{
				monitorDefinition.IsHaImpacting = true;
			}
			monitorDefinition.MonitorStateTransitions = PopImapDiscoveryCommon.CreateResponderChain(monitorDefinition.Name, isMbx ? "MSExchangePop3BE" : "MSExchangePop3", ExchangeComponent.Pop.Name, base.Broker, false, isMbx ? PopImapDiscoveryCommon.TargetScope.MDT : PopImapDiscoveryCommon.TargetScope.CTP, base.TraceContext);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate POP health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00080CA0 File Offset: 0x0007EEA0
		private void CreatePerfAndMonitors()
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueAboveThresholdMonitor.CreateDefinition("AverageCommandProcessingTimeGt60sMonitor", PerformanceCounterNotificationItem.GenerateResultName("MSExchangePop3\\Average Command Processing Time (milliseconds)"), ExchangeComponent.Pop.Name, ExchangeComponent.Pop, 60000.0, 2, true);
			monitorDefinition.TargetResource = ExchangeComponent.Pop.Name;
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate POP health is not impacted by processing delays";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("AverageCommandProcessingTimeGt60sEscalate", ExchangeComponent.Pop.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), Environment.MachineName, ServiceHealthStatus.None, "Pop3, Imap4, ActiveSync", Strings.EscalationSubjectUnhealthy, Strings.EscalationMessageFailuresUnhealthy(Strings.Pop3CommandProcessingTimeEscalationMessage), true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x04000DF1 RID: 3569
		private const string BrickServiceName = "MSExchangePop3BE";

		// Token: 0x04000DF2 RID: 3570
		private const string CafeServiceName = "MSExchangePop3";

		// Token: 0x04000DF3 RID: 3571
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000DF4 RID: 3572
		private static readonly string PopMailboxProbeSSL = typeof(PopMailboxProbeSSL).FullName;

		// Token: 0x04000DF5 RID: 3573
		private static readonly Type OverallConsecutiveProbeFailuresMonitor = typeof(OverallConsecutiveProbeFailuresMonitor);

		// Token: 0x04000DF6 RID: 3574
		private static readonly int DefaultMbxPort = 1995;

		// Token: 0x04000DF7 RID: 3575
		private static readonly int DefaultCafePort = 995;

		// Token: 0x04000DF8 RID: 3576
		private static LocalEndpointManager endpointManager;

		// Token: 0x04000DF9 RID: 3577
		private static IPEndPoint cafeEndpoint;

		// Token: 0x04000DFA RID: 3578
		private static IPEndPoint mbxEndpoint;
	}
}

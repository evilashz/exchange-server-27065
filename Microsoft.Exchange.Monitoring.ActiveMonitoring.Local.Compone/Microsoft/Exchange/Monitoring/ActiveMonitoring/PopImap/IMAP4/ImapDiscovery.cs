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

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.IMAP4
{
	// Token: 0x02000288 RID: 648
	public sealed class ImapDiscovery : PopImapDiscoveryCommon
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x0007D2F0 File Offset: 0x0007B4F0
		internal static PopImapAdConfiguration GetConfiguration(TracingContext context)
		{
			PopImapAdConfiguration retVal = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 94, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs");
				retVal = PopImapAdConfiguration.FindOne<Imap4AdConfiguration>(session);
			});
			if (!adoperationResult.Succeeded)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.IMAP4Tracer, context, "ImapDiscovery:: DoWork(): Unable to retrieve Imap Configuration: {0}", adoperationResult.Exception.Message, null, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 104);
			}
			return retVal;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0007D354 File Offset: 0x0007B554
		internal ProbeDefinition CreateProbe(IPEndPoint targetEndpoint, MailboxDatabaseInfo dbInfo, string probeTypeName, string probeName, int recurrenceInterval, int timeOut, bool lightMode, bool isMbxProbe, string targetResource, string healthSet)
		{
			WTFDiagnostics.TraceDebug<string, IPEndPoint>(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Creating {0} for {1}", probeName, targetEndpoint, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 140);
			ProbeDefinition probeDefinition = PopImapDiscoveryCommon.CreateProbe(ImapDiscovery.AssemblyPath, targetEndpoint, dbInfo, probeTypeName, probeName, recurrenceInterval, timeOut, lightMode, isMbxProbe, targetResource, healthSet);
			probeDefinition.Attributes["IsLocalProbe"] = true.ToString();
			WTFDiagnostics.TraceDebug<string, IPEndPoint>(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Created {0} for {1}", probeName, targetEndpoint, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 161);
			return probeDefinition;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0007D3E8 File Offset: 0x0007B5E8
		internal MonitorDefinition CreateMonitor(string targetResource, Component component, Type monitorType, string monitorName, int recurrenceInterval, int monitoringInterval, int? monitoringThreshold, string sampleMask)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Creating {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 193);
			MonitorDefinition result = PopImapDiscoveryCommon.CreateMonitor(monitorType.Assembly.Location, targetResource, monitorType, monitorName, recurrenceInterval, monitoringInterval, monitoringThreshold, sampleMask, component);
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Created {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 211);
			return result;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0007D464 File Offset: 0x0007B664
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapAdConfiguration configuration = ImapDiscovery.GetConfiguration(base.TraceContext);
			if (!PopImapDiscoveryCommon.GetEndpointsFromConfig(configuration, out ImapDiscovery.cafeEndpoint, out ImapDiscovery.mbxEndpoint))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Failed to Autodetect Imap settings. Using Default values.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 231);
				ImapDiscovery.cafeEndpoint = new IPEndPoint(IPAddress.Loopback, ImapDiscovery.DefaultCafePort);
				ImapDiscovery.mbxEndpoint = new IPEndPoint(IPAddress.Loopback, ImapDiscovery.DefaultMbxPort);
			}
			ImapDiscovery.endpointManager = LocalEndpointManager.Instance;
			int num = 0;
			int num2 = 0;
			try
			{
				if (ImapDiscovery.endpointManager.ExchangeServerRoleEndpoint == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: No Exchange roles installed on server, skipping item creation.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 251);
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, string.Format("ImapDiscovery:: DoWork(): ExchangeServerRoleEndpoint initialisation failed.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 260);
				return;
			}
			if (!ImapDiscovery.endpointManager.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Mailbox role is not installed on this server, no need to create Imap Mailbox related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 270);
			}
			else
			{
				try
				{
					if (ImapDiscovery.endpointManager.MailboxDatabaseEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery:: DoWork(): Could not find MailboxDatabaseEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 283);
						return;
					}
				}
				catch (EndpointManagerEndpointUninitializedException ex2)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, string.Format("ImapDiscovery:: DoWork(): MailboxDatabaseEndpoint initialisation failed.  Exception:{0}", ex2.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 289);
					return;
				}
				RegistryKey registryKey = RegistryHelper.OpenKey("SYSTEM\\CurrentControlSet\\services\\", "MSExchangeImap4BE", false, false);
				num2 = (int)registryKey.GetValue("Start");
				if (num2 == 2)
				{
					this.CreateImapProtocolContext(true);
					if (ImapDiscovery.endpointManager.MailboxDatabaseEndpoint != null)
					{
						foreach (MailboxDatabaseInfo dbInfo in ImapDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
						{
							this.CreateImapServiceProbe(true, dbInfo, ImapDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count);
						}
						this.CreateImapServiceMonitor(true);
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: Mailbox Imap service is not set to AUTOMATIC, will not create Imap Mailbox related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 319);
				}
			}
			if (!ImapDiscovery.endpointManager.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: CAFE role is not installed on this server, no need to create Imap CAFE related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 329);
			}
			else
			{
				try
				{
					if (ImapDiscovery.endpointManager.MailboxDatabaseEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery:: DoWork(): Could not find MailboxDatabaseEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 342);
						return;
					}
				}
				catch (Exception ex3)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, string.Format("ImapDiscovery:: DoWork(): MailboxDatabaseEndpoint object threw exception.  Exception:{0}", ex3.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 348);
					return;
				}
				RegistryKey registryKey = RegistryHelper.OpenKey("SYSTEM\\CurrentControlSet\\services\\", "MSExchangeImap4", false, false);
				num = (int)registryKey.GetValue("Start");
				if (num == 2)
				{
					this.CreateImapProtocolContext(false);
					if (ImapDiscovery.endpointManager.MailboxDatabaseEndpoint != null && !VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PopImapDiscoveryCommon.Enabled)
					{
						foreach (MailboxDatabaseInfo dbInfo2 in ImapDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe)
						{
							this.CreateImapServiceProbe(false, dbInfo2, ImapDiscovery.endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.Count);
						}
						this.CreateImapServiceMonitor(false);
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery.DoWork: CAFE Imap service is not set to AUTOMATIC, will not create Imap CAFE related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 378);
				}
			}
			if (num == 2 || num2 == 2)
			{
				this.CreatePerfAndMonitors();
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0007D8A0 File Offset: 0x0007BAA0
		private void CreateImapProtocolContext(bool isMbx)
		{
			string text = isMbx ? "MSExchangeImap4BE" : "MSExchangeImap4";
			ProbeDefinition probeDefinition = this.CreateProbe(isMbx ? ImapDiscovery.mbxEndpoint : ImapDiscovery.cafeEndpoint, null, ImapDiscovery.ImapMailboxProbeSSL, isMbx ? string.Format("ImapSelfTest{0}", "Probe") : string.Format("ImapProxyTest{0}", "Probe"), 60, 115, true, isMbx, text, isMbx ? ExchangeComponent.ImapProtocol.Name : ExchangeComponent.ImapProxy.Name);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitor(text, isMbx ? ExchangeComponent.ImapProtocol : ExchangeComponent.ImapProxy, ImapDiscovery.OverallConsecutiveProbeFailuresMonitor, isMbx ? string.Format("ImapSelfTest{0}", "Monitor") : string.Format("ImapProxyTest{0}", "Monitor"), 0, 240, new int?(4), probeDefinition.Name);
			monitorDefinition.IsHaImpacting = true;
			monitorDefinition.MonitorStateTransitions = PopImapDiscoveryCommon.CreateResponderChain(monitorDefinition.Name, text, text, base.Broker, true, isMbx ? PopImapDiscoveryCommon.TargetScope.PST : PopImapDiscoveryCommon.TargetScope.PT, base.TraceContext);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate IMAP health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0007D9D4 File Offset: 0x0007BBD4
		private void CreateImapServiceProbe(bool isMbx, MailboxDatabaseInfo dbInfo, int dbCount)
		{
			int num = 180 * dbCount;
			int num2 = 240 * dbCount;
			if (string.IsNullOrWhiteSpace(dbInfo.MonitoringAccountPassword))
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "ImapDiscovery:: DoWork(): Ignore mailbox database {0} because it does not have monitoring mailbox", dbInfo.MailboxDatabaseName, null, "CreateImapServiceProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDiscovery.cs", 465);
				return;
			}
			ProbeDefinition definition = this.CreateProbe(isMbx ? ImapDiscovery.mbxEndpoint : ImapDiscovery.cafeEndpoint, dbInfo, ImapDiscovery.ImapMailboxProbeSSL, isMbx ? string.Format("ImapDeepTest{0}", "Probe") : string.Format("ImapCTP{0}", "Probe"), isMbx ? num : num2, 115, false, isMbx, dbInfo.MailboxDatabaseName, isMbx ? ExchangeComponent.ImapProtocol.Name : ExchangeComponent.Imap.Name);
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0007DAA8 File Offset: 0x0007BCA8
		private void CreateImapServiceMonitor(bool isMbx)
		{
			MonitorDefinition monitorDefinition = PopImapDiscoveryCommon.CreateServiceMonitor(isMbx ? string.Format("ImapDeepTest{0}", "Monitor") : string.Format("ImapCTP{0}", "Monitor"), isMbx ? string.Format("ImapDeepTest{0}", "Probe") : string.Format("ImapCTP{0}", "Probe"), isMbx ? ExchangeComponent.ImapProtocol : ExchangeComponent.Imap, isMbx);
			if (isMbx)
			{
				monitorDefinition.IsHaImpacting = true;
			}
			monitorDefinition.MonitorStateTransitions = PopImapDiscoveryCommon.CreateResponderChain(monitorDefinition.Name, isMbx ? "MSExchangeImap4BE" : "MSExchangeImap4", ExchangeComponent.Imap.Name, base.Broker, true, isMbx ? PopImapDiscoveryCommon.TargetScope.MDT : PopImapDiscoveryCommon.TargetScope.CTP, base.TraceContext);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate IMAP health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0007DB80 File Offset: 0x0007BD80
		private void CreatePerfAndMonitors()
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueAboveThresholdMonitor.CreateDefinition("AverageCommandProcessingTimeGt60sMonitor", PerformanceCounterNotificationItem.GenerateResultName("MSExchangeImap4\\Average Command Processing Time (milliseconds)"), ExchangeComponent.Imap.Name, ExchangeComponent.Imap, 60000.0, 2, true);
			monitorDefinition.TargetResource = ExchangeComponent.Imap.Name;
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate IMAP health is not impacted by processing time";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("AverageCommandProcessingTimeGt60sEscalate", ExchangeComponent.Imap.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), Environment.MachineName, ServiceHealthStatus.None, "Pop3, Imap4, ActiveSync", Strings.EscalationSubjectUnhealthy, Strings.EscalationMessageFailuresUnhealthy(Strings.Imap4CommandProcessingTimeEscalationMessage), true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x04000DCE RID: 3534
		private const string BrickServiceName = "MSExchangeImap4BE";

		// Token: 0x04000DCF RID: 3535
		private const string CafeServiceName = "MSExchangeImap4";

		// Token: 0x04000DD0 RID: 3536
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000DD1 RID: 3537
		private static readonly string ImapMailboxProbeSSL = typeof(ImapMailboxProbeSSL).FullName;

		// Token: 0x04000DD2 RID: 3538
		private static readonly Type OverallConsecutiveProbeFailuresMonitor = typeof(OverallConsecutiveProbeFailuresMonitor);

		// Token: 0x04000DD3 RID: 3539
		private static readonly int DefaultMbxPort = 1993;

		// Token: 0x04000DD4 RID: 3540
		private static readonly int DefaultCafePort = 993;

		// Token: 0x04000DD5 RID: 3541
		private static LocalEndpointManager endpointManager;

		// Token: 0x04000DD6 RID: 3542
		private static IPEndPoint cafeEndpoint;

		// Token: 0x04000DD7 RID: 3543
		private static IPEndPoint mbxEndpoint;
	}
}

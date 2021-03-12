using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000412 RID: 1042
	internal sealed class PushNotificationsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x000917F1 File Offset: 0x0008F9F1
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x000917F9 File Offset: 0x0008F9F9
		private int? MonitoringIntervalOverride { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00091802 File Offset: 0x0008FA02
		// (set) Token: 0x06001A7D RID: 6781 RVA: 0x0009180A File Offset: 0x0008FA0A
		private int? RecurrenceIntervalOverride { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00091813 File Offset: 0x0008FA13
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x0009181B File Offset: 0x0008FA1B
		private bool SkipInstanceOverride { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00091824 File Offset: 0x0008FA24
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x0009182C File Offset: 0x0008FA2C
		private string MonitoringTenantId { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00091835 File Offset: 0x0008FA35
		// (set) Token: 0x06001A83 RID: 6787 RVA: 0x0009183D File Offset: 0x0008FA3D
		private bool IsRegistrationEnabledOnAzureSend { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00091846 File Offset: 0x0008FA46
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x0009184E File Offset: 0x0008FA4E
		private bool IsDeviceRegistrationChannelEnabled { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00091857 File Offset: 0x0008FA57
		// (set) Token: 0x06001A87 RID: 6791 RVA: 0x0009185F File Offset: 0x0008FA5F
		private bool IsChallengeRequestChannelEnabled { get; set; }

		// Token: 0x06001A88 RID: 6792 RVA: 0x00091868 File Offset: 0x0008FA68
		internal static string EscalateResponderNameForEvent(string eventName, string targetResource = "")
		{
			return PushNotificationsDiscovery.EventNameWithSuffix(eventName, "Escalate", targetResource);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00091876 File Offset: 0x0008FA76
		internal static string RecycleResponderNameForEvent(string eventName, string targetResource = "")
		{
			return PushNotificationsDiscovery.EventNameWithSuffix(eventName, "Recycle", targetResource);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00091884 File Offset: 0x0008FA84
		internal static string MonitorNameForEvent(string eventName, string targetResource = "")
		{
			return PushNotificationsDiscovery.EventNameWithSuffix(eventName, "Monitor", targetResource);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00091894 File Offset: 0x0008FA94
		private static string EventNameWithSuffix(string eventName, string suffix, string targetResource)
		{
			bool flag = string.IsNullOrEmpty(targetResource);
			return string.Format("{0}{1}{2}{3}", new object[]
			{
				eventName,
				suffix,
				flag ? string.Empty : "/",
				flag ? string.Empty : targetResource
			});
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x000918E2 File Offset: 0x0008FAE2
		internal static PushNotificationPublisherConfiguration PublisherConfiguration
		{
			get
			{
				return PushNotificationsDiscovery.publisherConfiguration.Value;
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00091900 File Offset: 0x0008FB00
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			try
			{
				if (instance.ExchangeServerRoleEndpoint == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.PushNotificationTracer, base.TraceContext, "PushNotificationsDiscovery:: DoWork(): Could not find ExchangeServerRoleEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PushNotifications\\PushNotificationsDiscovery.cs", 253);
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PushNotificationTracer, base.TraceContext, string.Format("PushNotificationsDiscovery:: DoWork(): Endpoint initialisation failed. Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PushNotifications\\PushNotificationsDiscovery.cs", 263);
				return;
			}
			this.LoadIntervalOverrides();
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled && VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PushNotificationsDiscoveryMbx.Enabled)
			{
				if (instance.MailboxDatabaseEndpoint != null)
				{
					MailboxDatabaseInfo mailboxDatabaseInfo = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.FirstOrDefault<MailboxDatabaseInfo>();
					if (mailboxDatabaseInfo != null && mailboxDatabaseInfo.MonitoringAccountOrganizationId != null)
					{
						this.MonitoringTenantId = mailboxDatabaseInfo.MonitoringAccountOrganizationId.ToExternalDirectoryOrganizationId();
					}
				}
				this.CreateAPNSPublisherChannelMonitors();
				this.CreateWnsPublisherChannelMonitors();
				this.CreateGcmPublisherChannelMonitors();
				this.CreateWebAppPublisherChannelMonitors();
				this.CreateAzurePublisherChannelMonitors();
				this.CreateAzureHubCreationPublisherChannelMonitors();
				this.CreateAzureDeviceRegistrationPublisherChannelMonitors();
				this.CreateAzureChallengeRequestPublisherChannelMonitors();
				this.CreateAzureHubCreationProbe();
				this.CreateNotificationDeliveryMonitors();
				this.CreateDeviceRegistrationProbe();
				this.CreateChallengeRequestProbe();
				this.CreateDatacenterOnPremBackendEndpointProbe();
			}
			if (instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled && VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PushNotificationsDiscoveryCafe.Enabled)
			{
				try
				{
					if (instance.MailboxDatabaseEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.PushNotificationTracer, base.TraceContext, "PushNotificationsDiscovery:: DoWork(): Could not find MailboxDatabaseEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PushNotifications\\PushNotificationsDiscovery.cs", 318);
						return;
					}
				}
				catch (EndpointManagerEndpointUninitializedException ex2)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.PushNotificationTracer, base.TraceContext, string.Format("PushNotificationsDiscovery:: DoWork(): MailboxDatabaseEndpoint initialisation failed.  Exception:{0}", ex2.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PushNotifications\\PushNotificationsDiscovery.cs", 328);
					return;
				}
				MailboxDatabaseInfo mailboxDatabaseInfo2 = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.FirstOrDefault((MailboxDatabaseInfo db) => !string.IsNullOrWhiteSpace(db.MonitoringAccountPassword));
				if (mailboxDatabaseInfo2 != null)
				{
					this.CreateCafeOnPremProbe(mailboxDatabaseInfo2);
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.PushNotificationTracer, base.TraceContext, string.Format("PushNotificationsDiscovery:: DoWork(): No valid Monitoring Mailbox found for CAFE. Skipping CafeProbe creation", new object[0]), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PushNotifications\\PushNotificationsDiscovery.cs", 346);
				}
			}
			if (!LocalEndpointManager.IsDataCenter && instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				this.CreateEnterpriseConnectivityProbe();
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00091B74 File Offset: 0x0008FD74
		private void CreateNotificationDeliveryMonitors()
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.Name = "PushNotificationsPublisherProbe";
			probeDefinition.TargetResource = "MSExchangePushNotificationsAppPool";
			probeDefinition.RecurrenceIntervalSeconds = 900;
			probeDefinition.TypeName = typeof(PublisherProbe).FullName;
			probeDefinition.TimeoutSeconds = 60;
			probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
			if (!string.IsNullOrEmpty(this.MonitoringTenantId))
			{
				probeDefinition.Attributes["TenantId"] = this.MonitoringTenantId;
			}
			this.SetRecurrenceOverride(probeDefinition);
			if (PushNotificationsDiscovery.PublisherConfiguration.HasEnabledPublisherSettings)
			{
				if (PushNotificationsDiscovery.PublisherConfiguration.AzureDeviceRegistrationPublisherSettings.Count<AzureDeviceRegistrationPublisherSettings>() > 0)
				{
					AzureDeviceRegistrationPublisherSettings azureDeviceRegistrationPublisherSettings = PushNotificationsDiscovery.PublisherConfiguration.AzureDeviceRegistrationPublisherSettings.First<AzureDeviceRegistrationPublisherSettings>();
					this.IsDeviceRegistrationChannelEnabled = azureDeviceRegistrationPublisherSettings.Enabled;
				}
				if (PushNotificationsDiscovery.PublisherConfiguration.AzureDeviceRegistrationPublisherSettings.Count<AzureDeviceRegistrationPublisherSettings>() > 0)
				{
					AzureChallengeRequestPublisherSettings azureChallengeRequestPublisherSettings = PushNotificationsDiscovery.PublisherConfiguration.AzureChallengeRequestPublisherSettings.First<AzureChallengeRequestPublisherSettings>();
					this.IsChallengeRequestChannelEnabled = azureChallengeRequestPublisherSettings.Enabled;
					this.IsChallengeRequestChannelEnabled = false;
				}
				foreach (PushNotificationPublisherSettings pushNotificationPublisherSettings in PushNotificationsDiscovery.PublisherConfiguration.PublisherSettings)
				{
					if (!(pushNotificationPublisherSettings.GetType() == typeof(AzureHubCreationPublisherSettings)) && !(pushNotificationPublisherSettings.GetType() == typeof(AzureChallengeRequestPublisherSettings)) && !(pushNotificationPublisherSettings.GetType() == typeof(AzureDeviceRegistrationPublisherSettings)))
					{
						string eventName = "NotificationProcessed";
						string appId = pushNotificationPublisherSettings.AppId;
						string text = PushNotificationsDiscovery.MonitorNameForEvent(eventName, appId);
						ProbeDefinition probeDefinition2 = new ProbeDefinition();
						probeDefinition2.AssemblyPath = Assembly.GetExecutingAssembly().Location;
						probeDefinition2.Name = "PushNotificationsNotificationDeliveryProbe";
						probeDefinition2.TargetResource = appId;
						probeDefinition2.RecurrenceIntervalSeconds = 1800;
						probeDefinition2.TypeName = typeof(NotificationDeliveryProbe).FullName;
						probeDefinition2.TimeoutSeconds = 60;
						probeDefinition2.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
						this.SetRecurrenceOverride(probeDefinition2);
						probeDefinition2.Attributes["TargetAppId"] = appId;
						probeDefinition2.Attributes["SkipInstanceTag"] = this.SkipInstanceOverride.ToString();
						if (pushNotificationPublisherSettings.GetType() == typeof(AzurePublisherSettings))
						{
							probeDefinition2.Attributes["IsAzureApp"] = true.ToString();
							probeDefinition2.Attributes["IsDeviceRegistrationChannelEnabled"] = this.IsDeviceRegistrationChannelEnabled.ToString();
							probeDefinition2.Attributes["IsChallengeRequestChannelEnabled"] = this.IsChallengeRequestChannelEnabled.ToString();
							AzurePublisherSettings azurePublisherSettings = pushNotificationPublisherSettings as AzurePublisherSettings;
							this.IsRegistrationEnabledOnAzureSend = azurePublisherSettings.ChannelSettings.IsRegistrationEnabled;
						}
						base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition2, base.TraceContext);
						MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(text, PushNotificationsDiscovery.EventNameWithSuffix(probeDefinition2.Name, string.Empty, appId), ExchangeComponent.PushNotificationsProtocol.Name, ExchangeComponent.PushNotificationsProtocol, 2, true, 7200);
						monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
						{
							new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
							new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.RecurrenceIntervalOverride ?? probeDefinition2.RecurrenceIntervalSeconds)
						};
						this.SetMonitoringIntervalOverrides(monitorDefinition);
						monitorDefinition.ServicePriority = 2;
						monitorDefinition.ScenarioDescription = "Validate PushNotifications are delivered on timely manner";
						base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
						ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(PushNotificationsDiscovery.RecycleResponderNameForEvent(eventName, appId), text, "MSExchangePushNotificationsAppPool", ServiceHealthStatus.Degraded, DumpMode.None, null, 15.0, 0, "Exchange", true, "Dag");
						responderDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
						this.SetRecurrenceOverride(responderDefinition);
						base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
						string escalationMessage = string.Format("<b>{0}</b><br>{{Probe.StateAttribute12}}<br><br>", Strings.PushNotificationPublisherUnhealthy(appId, Environment.MachineName));
						this.CreateEscalateResponderForMonitor(eventName, ExchangeComponent.PushNotificationsProtocol, escalationMessage, appId);
					}
				}
				probeDefinition.Attributes["IsRegistrationEnabled"] = this.IsRegistrationEnabledOnAzureSend.ToString();
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00091FE8 File Offset: 0x000901E8
		private void CreateDatacenterOnPremBackendEndpointProbe()
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.Name = "PushNotificationsDatacenterOnPremBackendEndpointProbe";
			probeDefinition.TargetResource = "MSExchangePushNotificationsAppPool";
			probeDefinition.RecurrenceIntervalSeconds = 900;
			probeDefinition.TypeName = typeof(DatacenterOnPremBackendEndpointProbe).FullName;
			probeDefinition.TimeoutSeconds = 60;
			probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
			this.SetRecurrenceOverride(probeDefinition);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			string eventName = "EnterpriseNotificationProcessed";
			string name = PushNotificationsDiscovery.MonitorNameForEvent(eventName, "");
			ProbeDefinition probeDefinition2 = new ProbeDefinition();
			probeDefinition2.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition2.Name = "PushNotificationsOnPremNotificationDeliveryProbe";
			probeDefinition2.TargetResource = "MSExchangePushNotificationsAppPool";
			probeDefinition2.RecurrenceIntervalSeconds = 1800;
			probeDefinition2.TypeName = typeof(NotificationDeliveryProbe).FullName;
			probeDefinition2.TimeoutSeconds = 60;
			probeDefinition2.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
			this.SetRecurrenceOverride(probeDefinition2);
			probeDefinition2.Attributes["SkipInstanceTag"] = this.SkipInstanceOverride.ToString();
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition2, base.TraceContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name, probeDefinition2.Name, ExchangeComponent.PushNotificationsProtocol.Name, ExchangeComponent.PushNotificationsProtocol, 2, true, 7200);
			this.SetMonitoringIntervalOverrides(monitorDefinition);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, 0)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate PushNotifications health is not impacted by BE issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string escalationMessage = string.Format("<b>{0}</b><br>{{Probe.StateAttribute12}}<br><br>", Strings.PushNotificationDatacenterBackendEndpointUnhealthy(Environment.MachineName));
			this.CreateEscalateResponderForMonitor(eventName, ExchangeComponent.PushNotificationsProtocol, escalationMessage, "");
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000921C0 File Offset: 0x000903C0
		private void CreateCafeOnPremProbe(MailboxDatabaseInfo dbInfo)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.Name = "PushNotificationsCafeProbe";
			probeDefinition.TargetResource = "MSExchangePushNotificationsAppPool";
			probeDefinition.RecurrenceIntervalSeconds = 600;
			probeDefinition.TypeName = typeof(CafeOnPremProbe).FullName;
			probeDefinition.TimeoutSeconds = 60;
			probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProxy.Name;
			probeDefinition.Attributes["AccountDomain"] = dbInfo.MonitoringAccountDomain;
			this.SetRecurrenceOverride(probeDefinition);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			ProbeIdentity probeIdentity = probeDefinition;
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), ExchangeComponent.PushNotificationsProxy.Name, ExchangeComponent.PushNotificationsProxy, 60.0, TimeSpan.FromSeconds(3600.0), true);
			this.SetMonitoringIntervalOverrides(monitorDefinition);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.RecurrenceIntervalOverride ?? 3600)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate PushNotifications health is not impacted by CAFE issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Escalate", null);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(responderIdentity.Name, responderIdentity.Component.Name, monitorIdentity.Name, monitorIdentity.Name, responderIdentity.TargetResource, ServiceHealthStatus.Unrecoverable, responderIdentity.Component.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.PushNotificationCafeEndpointUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			this.SetRecurrenceOverride(responderDefinition);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000923A0 File Offset: 0x000905A0
		private void CreateEnterpriseConnectivityProbe()
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.Name = "PushNotificationsEnterpriseConnectivityProbe";
			probeDefinition.TypeName = typeof(EnterpriseConnectivityProbe).FullName;
			probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProxy.Name;
			probeDefinition.TimeoutSeconds = 360;
			probeDefinition.Enabled = false;
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00092420 File Offset: 0x00090620
		private void CreateAPNSPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.ApnsPublisherSettings
			select app.AppId, PublisherType.APNS);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0009245D File Offset: 0x0009065D
		private void CreateWnsPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.WnsPublisherSettings
			select app.AppId, PublisherType.WNS);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0009249A File Offset: 0x0009069A
		private void CreateGcmPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.GcmPublisherSettings
			select app.AppId, PublisherType.GCM);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000924D7 File Offset: 0x000906D7
		private void CreateWebAppPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.WebAppPublisherSettings
			select app.AppId, PublisherType.WebApp);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00092514 File Offset: 0x00090714
		private void CreateAzurePublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.AzurePublisherSettings
			select app.AppId, PublisherType.Azure);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00092551 File Offset: 0x00090751
		private void CreateAzureHubCreationPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.AzureHubCreationPublisherSettings
			select app.AppId, PublisherType.AzureHubCreation);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0009258E File Offset: 0x0009078E
		private void CreateAzureDeviceRegistrationPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.AzureDeviceRegistrationPublisherSettings
			select app.AppId, PublisherType.AzureDeviceRegistration);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000925CB File Offset: 0x000907CB
		private void CreateAzureChallengeRequestPublisherChannelMonitors()
		{
			this.CreatePublisherChannelMonitors(from app in PushNotificationsDiscovery.PublisherConfiguration.AzureChallengeRequestPublisherSettings
			select app.AppId, PublisherType.AzureChallengeRequest);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00092600 File Offset: 0x00090800
		private void CreatePublisherChannelMonitors(IEnumerable<string> targetAppIds, PublisherType pubType)
		{
			foreach (string text in targetAppIds)
			{
				string name = PushNotificationsDiscovery.MonitorNameForEvent("PublisherChannelHealth", text);
				ProbeDefinition probeDefinition = new ProbeDefinition();
				probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
				probeDefinition.Name = "PushNotificationsPublisherChannelHealthProbe";
				probeDefinition.TargetResource = text;
				probeDefinition.RecurrenceIntervalSeconds = 3600;
				probeDefinition.TypeName = typeof(PublisherChannelHealthProbe).FullName;
				probeDefinition.TimeoutSeconds = 60;
				probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
				probeDefinition.Attributes["TargetAppId"] = text;
				probeDefinition.Attributes["TargetAppPublisher"] = pubType.ToString();
				probeDefinition.Attributes["SkipInstanceTag"] = this.SkipInstanceOverride.ToString();
				this.SetRecurrenceOverride(probeDefinition);
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name, PushNotificationsDiscovery.EventNameWithSuffix(probeDefinition.Name, string.Empty, text), ExchangeComponent.PushNotificationsProtocol.Name, ExchangeComponent.PushNotificationsProtocol, 2, true, 14400);
				monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
				{
					new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.RecurrenceIntervalOverride ?? probeDefinition.RecurrenceIntervalSeconds)
				};
				this.SetMonitoringIntervalOverrides(monitorDefinition);
				monitorDefinition.ServicePriority = 2;
				monitorDefinition.ScenarioDescription = string.Format("Validate Publisher Channel for {0} is healthy", text);
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				string text2 = "<b>{0}</b><br>The status of each channel events is:<br>{{Probe.StateAttribute12}}<br><br>Error Traces for the failed events: <br>{{Probe.StateAttribute13}}<br><br>";
				text2 = string.Format(text2, Strings.PushNotificationChannelError(pubType.ToString(), text, Environment.MachineName));
				this.CreateEscalateResponderForMonitor("PublisherChannelHealth", ExchangeComponent.PushNotificationsProtocol, text2, text);
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0009280C File Offset: 0x00090A0C
		private void CreateAzureHubCreationProbe()
		{
			foreach (string text in from app in PushNotificationsDiscovery.PublisherConfiguration.AzurePublisherSettings
			select app.AppId)
			{
				ProbeDefinition probeDefinition = new ProbeDefinition();
				probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
				probeDefinition.Name = "PushNotificationsAzureHubCreationProbe";
				probeDefinition.TargetResource = text;
				probeDefinition.RecurrenceIntervalSeconds = 1800;
				probeDefinition.TypeName = typeof(AzureHubCreationProbe).FullName;
				probeDefinition.TimeoutSeconds = 60;
				probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
				probeDefinition.Attributes["TargetAppId"] = text;
				probeDefinition.Attributes["TenantId"] = this.MonitoringTenantId;
				this.SetRecurrenceOverride(probeDefinition);
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00092928 File Offset: 0x00090B28
		private void CreateDeviceRegistrationProbe()
		{
			if (!this.IsDeviceRegistrationChannelEnabled)
			{
				return;
			}
			foreach (string text in from app in PushNotificationsDiscovery.PublisherConfiguration.AzurePublisherSettings
			select app.AppId)
			{
				ProbeDefinition probeDefinition = new ProbeDefinition();
				probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
				probeDefinition.Name = "PushNotificationsDeviceRegistrationProbe";
				probeDefinition.TargetResource = text;
				probeDefinition.RecurrenceIntervalSeconds = 1800;
				probeDefinition.TypeName = typeof(AzureDeviceRegistrationProbe).FullName;
				probeDefinition.TimeoutSeconds = 60;
				probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
				probeDefinition.Attributes["TargetAppId"] = text;
				probeDefinition.Attributes["TenantId"] = this.MonitoringTenantId;
				this.SetRecurrenceOverride(probeDefinition);
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00092A4C File Offset: 0x00090C4C
		private void CreateChallengeRequestProbe()
		{
			if (!this.IsChallengeRequestChannelEnabled)
			{
				return;
			}
			foreach (string text in from app in PushNotificationsDiscovery.PublisherConfiguration.AzurePublisherSettings
			select app.AppId)
			{
				ProbeDefinition probeDefinition = new ProbeDefinition();
				probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
				probeDefinition.Name = "PushNotificationsChallengeRequestProbe";
				probeDefinition.TargetResource = text;
				probeDefinition.RecurrenceIntervalSeconds = 1800;
				probeDefinition.TypeName = typeof(AzureChallengeRequestProbe).FullName;
				probeDefinition.TimeoutSeconds = 60;
				probeDefinition.ServiceName = ExchangeComponent.PushNotificationsProtocol.Name;
				probeDefinition.Attributes["TargetAppId"] = text;
				probeDefinition.Attributes["TenantId"] = this.MonitoringTenantId;
				PushNotificationPlatform pushNotificationPlatform = PushNotificationsMonitoring.CannedAppPlatformSet[text];
				probeDefinition.Attributes["TenantId"] = pushNotificationPlatform.ToString();
				this.SetRecurrenceOverride(probeDefinition);
				probeDefinition.Enabled = false;
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00092B98 File Offset: 0x00090D98
		private void CreateEscalateResponderForMonitor(string eventName, Component exchangeComponent, string escalationMessage, string targetResource = "")
		{
			string text = PushNotificationsDiscovery.MonitorNameForEvent(eventName, targetResource);
			string arg = string.Format("http://aka.ms/decisiontrees?Page=RecoveryHome&service=Exchange&escalationteam=Push%20Notification%20Services&alerttypeid={0}&id=0&alertname=dummy", text);
			string format = "<br><br><a href='{0}'>Battlecards</a><br><a href='{1}'>OneNote Battlecards</a>";
			if (text.ToLower().Contains("publisherchannel"))
			{
				escalationMessage += string.Format(format, arg, "onenote:///\\\\exstore\\files\\userfiles\\servicesoncall\\OnCall%20Battlecard\\New%20Battlecards.one#PublisherChannelHealth%20Unhealthy&section-id={9E24DEE5-34D7-4463-AB20-386D859233BA}&page-id={176FC0F6-8A1C-4E9C-8F38-31F0D678279A}&end");
			}
			else if (text.ToLower().Contains("notificationprocessed"))
			{
				escalationMessage += string.Format(format, arg, "onenote:///\\\\exstore\\files\\userfiles\\servicesoncall\\OnCall%20Battlecard\\New%20Battlecards.one#NotificationProcessedMonitor%20Unhealthy&section-id={9E24DEE5-34D7-4463-AB20-386D859233BA}&page-id={1A62CAB4-5688-4009-9297-D14ADCFBE2EF}&end");
			}
			else if (text.ToLower().Contains("cafemonitor"))
			{
				escalationMessage += string.Format(format, arg, "onenote:///\\\\exstore\\files\\userfiles\\servicesoncall\\OnCall%20Battlecard\\New%20Battlecards.one#Push%20Notifications%20Cafe%20Monitor&section-id={9E24DEE5-34D7-4463-AB20-386D859233BA}&page-id={0243B6CA-4719-49D3-8A93-58D7DEDCFB39}&end");
			}
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(PushNotificationsDiscovery.EscalateResponderNameForEvent(eventName, targetResource), exchangeComponent.Name, text, text, string.Empty, ServiceHealthStatus.Unrecoverable, exchangeComponent.EscalationTeam, Strings.EscalationSubjectUnhealthy, escalationMessage, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			this.SetRecurrenceOverride(responderDefinition);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00092C8C File Offset: 0x00090E8C
		private void SetMonitoringIntervalOverrides(MonitorDefinition monitor)
		{
			this.SetRecurrenceOverride(monitor);
			if (this.MonitoringIntervalOverride != null)
			{
				monitor.MonitoringIntervalSeconds = this.MonitoringIntervalOverride.Value;
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00092CC4 File Offset: 0x00090EC4
		private void SetRecurrenceOverride(WorkDefinition definition)
		{
			definition.RecurrenceIntervalSeconds = (this.RecurrenceIntervalOverride ?? definition.RecurrenceIntervalSeconds);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00092CF8 File Offset: 0x00090EF8
		private void LoadIntervalOverrides()
		{
			string text = this.ReadAttribute("MonitoringIntervalOverride", null);
			string text2 = this.ReadAttribute("RecurrenceIntervalOverride", null);
			string value = this.ReadAttribute("SkipInstanceTag", null);
			if (!string.IsNullOrEmpty(text))
			{
				this.MonitoringIntervalOverride = new int?(int.Parse(text));
			}
			if (!string.IsNullOrEmpty(text2))
			{
				this.RecurrenceIntervalOverride = new int?(int.Parse(text2));
			}
			bool skipInstanceOverride = true;
			if (!string.IsNullOrEmpty(value))
			{
				bool.TryParse(value, out skipInstanceOverride);
			}
			this.SkipInstanceOverride = skipInstanceOverride;
		}

		// Token: 0x04001203 RID: 4611
		public const string SkipInstanceTagProperty = "SkipInstanceTag";

		// Token: 0x04001204 RID: 4612
		public const string MonitoringIntervalOverrideProperty = "MonitoringIntervalOverride";

		// Token: 0x04001205 RID: 4613
		public const string RecurrenceIntervalOverrideProperty = "RecurrenceIntervalOverride";

		// Token: 0x04001206 RID: 4614
		internal const string AccountDomainAttribute = "AccountDomain";

		// Token: 0x04001207 RID: 4615
		internal const string NotificationDeliveryProbeName = "PushNotificationsNotificationDeliveryProbe";

		// Token: 0x04001208 RID: 4616
		internal const string PublisherChannelHealthProbeName = "PushNotificationsPublisherChannelHealthProbe";

		// Token: 0x04001209 RID: 4617
		internal const string AzureHubCreationProbeName = "PushNotificationsAzureHubCreationProbe";

		// Token: 0x0400120A RID: 4618
		internal const string DeviceRegistrationProbeName = "PushNotificationsDeviceRegistrationProbe";

		// Token: 0x0400120B RID: 4619
		internal const string ChallengeRequestProbeName = "PushNotificationsChallengeRequestProbe";

		// Token: 0x0400120C RID: 4620
		internal const string OnPremNotificationDeliveryProbeName = "PushNotificationsOnPremNotificationDeliveryProbe";

		// Token: 0x0400120D RID: 4621
		internal const string PublisherProbeName = "PushNotificationsPublisherProbe";

		// Token: 0x0400120E RID: 4622
		internal const string DatacenterOnPremBackendEndpointProbeName = "PushNotificationsDatacenterOnPremBackendEndpointProbe";

		// Token: 0x0400120F RID: 4623
		internal const string CafeProbeName = "PushNotificationsCafeProbe";

		// Token: 0x04001210 RID: 4624
		internal const string EnterpriseConnectivityProbeName = "PushNotificationsEnterpriseConnectivityProbe";

		// Token: 0x04001211 RID: 4625
		internal const string PushNotificationsAppPool = "MSExchangePushNotificationsAppPool";

		// Token: 0x04001212 RID: 4626
		internal const string PublisherChannelHealthEvent = "PublisherChannelHealth";

		// Token: 0x04001213 RID: 4627
		internal const int DefaultMonitorThreshold = 2;

		// Token: 0x04001214 RID: 4628
		private const string EscalateResponderNameSuffix = "Escalate";

		// Token: 0x04001215 RID: 4629
		private const string RecycleResponderNameSuffix = "Recycle";

		// Token: 0x04001216 RID: 4630
		private const string MonitorNameSuffix = "Monitor";

		// Token: 0x04001217 RID: 4631
		private const int DefaultMonitoringIntervalInSeconds = 3600;

		// Token: 0x04001218 RID: 4632
		private const int DefaultConsecutiveMonitoringIntervalInSeconds = 1800;

		// Token: 0x04001219 RID: 4633
		private const double DefaultSuccessThreshold = 90.0;

		// Token: 0x0400121A RID: 4634
		private const int DefaultProbeTimeoutInSeconds = 60;

		// Token: 0x0400121B RID: 4635
		private const string BattleCardPageUrl = "http://aka.ms/decisiontrees?Page=RecoveryHome&service=Exchange&escalationteam=Push%20Notification%20Services&alerttypeid={0}&id=0&alertname=dummy";

		// Token: 0x0400121C RID: 4636
		private const string OneNotePublisherChannelHealth = "onenote:///\\\\exstore\\files\\userfiles\\servicesoncall\\OnCall%20Battlecard\\New%20Battlecards.one#PublisherChannelHealth%20Unhealthy&section-id={9E24DEE5-34D7-4463-AB20-386D859233BA}&page-id={176FC0F6-8A1C-4E9C-8F38-31F0D678279A}&end";

		// Token: 0x0400121D RID: 4637
		private const string OneNoteNotificationProcessed = "onenote:///\\\\exstore\\files\\userfiles\\servicesoncall\\OnCall%20Battlecard\\New%20Battlecards.one#NotificationProcessedMonitor%20Unhealthy&section-id={9E24DEE5-34D7-4463-AB20-386D859233BA}&page-id={1A62CAB4-5688-4009-9297-D14ADCFBE2EF}&end";

		// Token: 0x0400121E RID: 4638
		private const string OneNoteCafeMonitor = "onenote:///\\\\exstore\\files\\userfiles\\servicesoncall\\OnCall%20Battlecard\\New%20Battlecards.one#Push%20Notifications%20Cafe%20Monitor&section-id={9E24DEE5-34D7-4463-AB20-386D859233BA}&page-id={0243B6CA-4719-49D3-8A93-58D7DEDCFB39}&end";

		// Token: 0x0400121F RID: 4639
		private static readonly Lazy<PushNotificationPublisherConfiguration> publisherConfiguration = new Lazy<PushNotificationPublisherConfiguration>(() => new PushNotificationPublisherConfiguration(false, null));
	}
}

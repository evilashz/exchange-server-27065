using System;
using System.Reflection;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x02000049 RID: 73
	public class EscalateResponder : EscalateResponderBase
	{
		// Token: 0x06000263 RID: 611 RVA: 0x000111E8 File Offset: 0x0000F3E8
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", bool loadEscalationMessageUnhealthyFromResource = false)
		{
			return EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, null, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, dailySchedulePattern, loadEscalationMessageUnhealthyFromResource);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00011214 File Offset: 0x0000F414
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationService, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", bool loadEscalationMessageUnhealthyFromResource = false)
		{
			if (string.IsNullOrWhiteSpace(escalationSubjectUnhealthy))
			{
				throw new ArgumentException("escalationSubjectUnhealthy");
			}
			if (string.IsNullOrWhiteSpace(escalationMessageUnhealthy))
			{
				throw new ArgumentException("escalationMessageUnhealthy");
			}
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = EscalateResponder.AssemblyPath;
			responderDefinition.TypeName = EscalateResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.EscalationService = escalationService;
			responderDefinition.EscalationTeam = escalationTeam;
			responderDefinition.EscalationSubject = escalationSubjectUnhealthy;
			responderDefinition.EscalationMessage = escalationMessageUnhealthy;
			responderDefinition.NotificationServiceClass = notificationServiceClass;
			responderDefinition.MinimumSecondsBetweenEscalates = minimumSecondsBetweenEscalates;
			responderDefinition.DailySchedulePattern = dailySchedulePattern;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.WaitIntervalSeconds = minimumSecondsBetweenEscalates;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes[EscalateResponderBase.LoadFromResourceAttributeValue] = loadEscalationMessageUnhealthyFromResource.ToString();
			EscalateResponderBase.SetActiveMonitoringCertificateSettings(responderDefinition);
			return responderDefinition;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00011314 File Offset: 0x0000F514
		internal override void LogCustomUnhealthyEvent(EscalateResponderBase.UnhealthyMonitoringEvent unhealthyEvent)
		{
			ManagedAvailabilityCrimsonEvents.UnhealthyHealthSet.Log<string, string, string, string>(unhealthyEvent.HealthSet, unhealthyEvent.Subject, unhealthyEvent.Message, unhealthyEvent.Monitor);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00011338 File Offset: 0x0000F538
		internal override string GetFFOForestName()
		{
			return ComputerInformation.DnsPhysicalDomainName;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00011340 File Offset: 0x0000F540
		internal override EscalationEnvironment GetEscalationEnvironment()
		{
			if (this.escalationEnvironment == null)
			{
				this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.OnPrem);
				if (base.Broker.IsLocal() && !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.EscalateResponder.Enabled)
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.OnPrem);
				}
				else if (base.Broker.IsLocal() && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.EscalateResponder.Enabled)
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.Datacenter);
				}
				else if (!base.Broker.IsLocal())
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.OutsideIn);
				}
				if (EscalateResponderBase.IsOBDGallatinMachine)
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.Datacenter);
				}
			}
			return this.escalationEnvironment.Value;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00011416 File Offset: 0x0000F616
		internal override ScopeMappingEndpoint GetScopeMappingEndpoint()
		{
			return ScopeMappingEndpointManager.Instance.GetEndpoint();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011424 File Offset: 0x0000F624
		static EscalateResponder()
		{
			EscalateResponderBase.DefaultEscalationSubject = Strings.DefaultEscalationSubject;
			EscalateResponderBase.DefaultEscalationMessage = Strings.DefaultEscalationMessage;
			EscalateResponderBase.HealthSetEscalationSubjectPrefix = Strings.HealthSetEscalationSubjectPrefix;
			EscalateResponderBase.HealthSetMaintenanceEscalationSubjectPrefix = Strings.HealthSetMaintenanceEscalationSubjectPrefix;
			EscalateResponderBase.EscalationHelper = new HealthSetEscalationLocalHelper();
		}

		// Token: 0x040001B6 RID: 438
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040001B7 RID: 439
		private static readonly string TypeName = typeof(EscalateResponder).FullName;

		// Token: 0x040001B8 RID: 440
		private EscalationEnvironment? escalationEnvironment;
	}
}

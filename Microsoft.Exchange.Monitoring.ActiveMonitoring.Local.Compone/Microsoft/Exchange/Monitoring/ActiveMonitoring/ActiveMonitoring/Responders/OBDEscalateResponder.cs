using System;
using System.Reflection;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.O365.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000DD RID: 221
	public class OBDEscalateResponder : EscalateResponder
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0002ADC4 File Offset: 0x00028FC4
		public new static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", bool loadEscalationMessageUnhealthyFromResource = false)
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, dailySchedulePattern, loadEscalationMessageUnhealthyFromResource);
			responderDefinition.AssemblyPath = OBDEscalateResponder.AssemblyPath;
			responderDefinition.TypeName = OBDEscalateResponder.TypeName;
			return responderDefinition;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0002AE08 File Offset: 0x00029008
		protected override void InvokeNewServiceAlert(Guid alertGuid, string alertTypeId, string alertName, string alertDescription, DateTime raisedTime, string escalationTeam, string service, string alertSource, bool isDatacenter, bool urgent, string environment, string location, string forest, string dag, string site, string region, string capacityUnit, string rack, string alertCategory)
		{
			alertName = alertName.Substring(0, (alertName.Length > 255) ? 255 : alertName.Length);
			if (!Datacenter.IsGallatinDatacenter())
			{
				string environment2 = Settings.IsProductionEnvironment ? "prod" : "ppe";
				SmartAlertsV2Client.NewAlert(alertGuid, alertTypeId, alertName, alertDescription, raisedTime, escalationTeam, service, "LocalActiveMonitoring", urgent, environment2, location, forest, dag, site, region, capacityUnit, rack, base.Definition.NotificationServiceClass, string.IsNullOrEmpty(base.Definition.Account) ? "CN=exouser.outlook.com" : base.Definition.Account);
				return;
			}
			base.InvokeNewServiceAlert(alertGuid, alertTypeId, alertName, alertDescription, raisedTime, escalationTeam, service, alertSource, isDatacenter, urgent, environment, location, forest, dag, site, region, capacityUnit, rack, alertCategory);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0002AED2 File Offset: 0x000290D2
		private DateTime ConvertToPST(DateTime utcTime)
		{
			utcTime = DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);
			return TimeZoneInfo.ConvertTime(utcTime, OBDEscalateResponder.pstTimeZoneInfo);
		}

		// Token: 0x040004B4 RID: 1204
		private const int MaxAlertNameLength = 255;

		// Token: 0x040004B5 RID: 1205
		private const string OBDAlertSource = "LocalActiveMonitoring";

		// Token: 0x040004B6 RID: 1206
		private const string ManagementCertificateSubject = "CN=exouser.outlook.com";

		// Token: 0x040004B7 RID: 1207
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040004B8 RID: 1208
		private static readonly string TypeName = typeof(OBDEscalateResponder).FullName;

		// Token: 0x040004B9 RID: 1209
		private static TimeZoneInfo pstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
	}
}

using System;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000075 RID: 117
	internal abstract class ResponderDefinitionHelper : DefinitionHelperBase
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001A048 File Offset: 0x00018248
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0001A050 File Offset: 0x00018250
		internal string AlertMask { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001A059 File Offset: 0x00018259
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0001A061 File Offset: 0x00018261
		internal int WaitIntervalSeconds { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001A06A File Offset: 0x0001826A
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0001A072 File Offset: 0x00018272
		internal int MinimumSecondsBetweenEscalates { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001A07B File Offset: 0x0001827B
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0001A083 File Offset: 0x00018283
		internal string EscalationSubject { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001A08C File Offset: 0x0001828C
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0001A094 File Offset: 0x00018294
		internal string EscalationMessage { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001A09D File Offset: 0x0001829D
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001A0A5 File Offset: 0x000182A5
		internal string EscalationTeam { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001A0AE File Offset: 0x000182AE
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001A0B6 File Offset: 0x000182B6
		internal NotificationServiceClass NotificationServiceClass { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001A0BF File Offset: 0x000182BF
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0001A0C7 File Offset: 0x000182C7
		internal string Endpoint { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001A0D0 File Offset: 0x000182D0
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0001A0D8 File Offset: 0x000182D8
		internal string Account { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001A0E1 File Offset: 0x000182E1
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001A0E9 File Offset: 0x000182E9
		internal string AccountPassword { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001A0F2 File Offset: 0x000182F2
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001A0FA File Offset: 0x000182FA
		internal string AlertTypeId { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001A103 File Offset: 0x00018303
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001A10B File Offset: 0x0001830B
		internal ServiceHealthStatus TargetHealthState { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001A114 File Offset: 0x00018314
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0001A11C File Offset: 0x0001831C
		internal int Version { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001A125 File Offset: 0x00018325
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0001A12D File Offset: 0x0001832D
		internal string CorrelatedMonitorsXml { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001A136 File Offset: 0x00018336
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0001A13E File Offset: 0x0001833E
		public bool? AlwaysEscalateOnMonitorChanges { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001A147 File Offset: 0x00018347
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001A14F File Offset: 0x0001834F
		internal CorrelatedMonitorAction ActionOnCorrelatedMonitors { get; set; }

		// Token: 0x0600044A RID: 1098 RVA: 0x0001A158 File Offset: 0x00018358
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine("AlertTypeId: " + this.AlertTypeId);
			stringBuilder.AppendLine("AlertMask: " + this.AlertMask);
			stringBuilder.AppendLine("WaitIntervalSeconds: " + this.WaitIntervalSeconds);
			stringBuilder.AppendLine("MinimumSecondsBetweenEscalates: " + this.MinimumSecondsBetweenEscalates);
			stringBuilder.AppendLine("Account: " + this.Account);
			stringBuilder.AppendLine("AccountPassword: " + this.AccountPassword);
			stringBuilder.AppendLine("EscalationSubject: " + this.EscalationSubject);
			stringBuilder.AppendLine("EscalationMessage: " + this.EscalationMessage);
			stringBuilder.AppendLine("EscalationTeam: " + this.EscalationTeam);
			stringBuilder.AppendLine("Endpoint: " + this.Endpoint);
			stringBuilder.AppendLine("TargetHealthState: " + this.TargetHealthState);
			stringBuilder.AppendLine("NotificationServiceClass: " + this.NotificationServiceClass);
			stringBuilder.AppendLine("CorrelatedMonitorsXml: " + this.CorrelatedMonitorsXml);
			stringBuilder.AppendLine("AlwaysEscalateOnMonitorChanges: " + ((this.AlwaysEscalateOnMonitorChanges != null) ? this.AlwaysEscalateOnMonitorChanges.Value.ToString() : "<NULL>"));
			stringBuilder.AppendLine("ActionOnCorrelatedMonitors: " + this.ActionOnCorrelatedMonitors);
			return stringBuilder.ToString();
		}

		// Token: 0x0600044B RID: 1099
		internal abstract ResponderDefinition CreateDefinition();

		// Token: 0x0600044C RID: 1100 RVA: 0x0001A318 File Offset: 0x00018518
		internal override void ReadDiscoveryXml()
		{
			base.ReadDiscoveryXml();
			if (base.TypeName.EndsWith(typeof(EscalateResponder).Name))
			{
				this.EscalationSubject = base.GetMandatoryXmlAttribute<string>("EscalationSubjectUnhealthy");
			}
			else
			{
				this.EscalationSubject = base.GetOptionalXmlAttribute<string>("EscalationSubjectUnhealthy", string.Empty);
			}
			this.AlertTypeId = base.GetOptionalXmlAttribute<string>("AlertTypeId", string.Empty);
			this.AlertMask = base.GetOptionalXmlAttribute<string>("AlertMask", string.Empty);
			this.WaitIntervalSeconds = base.GetOptionalXmlAttribute<int>("WaitIntervalSeconds", 0);
			this.MinimumSecondsBetweenEscalates = base.GetOptionalXmlAttribute<int>("MinimumSecondsBetweenEscalates", 14400);
			this.Account = base.GetOptionalXmlAttribute<string>("Account", string.Empty);
			this.AccountPassword = base.GetOptionalXmlAttribute<string>("AccountPassword", string.Empty);
			this.EscalationTeam = base.GetOptionalXmlAttribute<string>("EscalationTeam", base.Component.EscalationTeam);
			this.EscalationMessage = base.GetOptionalXmlAttribute<string>("EscalationMessageUnhealthy", string.Empty);
			this.Endpoint = base.GetOptionalXmlAttribute<string>("Endpoint", string.Empty);
			this.TargetHealthState = base.GetOptionalXmlEnumAttribute<ServiceHealthStatus>("TargetHealthState", ServiceHealthStatus.None);
			this.NotificationServiceClass = base.GetOptionalXmlEnumAttribute<NotificationServiceClass>("EscalationLevel", NotificationServiceClass.Scheduled);
			this.AlwaysEscalateOnMonitorChanges = base.GetOptionalXmlAttribute<bool?>("AlwaysEscalateOnMonitorChanges", null);
			this.CorrelatedMonitorsXml = string.Empty;
			XmlNode xmlNode = base.DefinitionNode.SelectSingleNode("Correlation");
			if (xmlNode != null)
			{
				this.CorrelatedMonitorsXml = xmlNode.OuterXml;
				this.ActionOnCorrelatedMonitors = DefinitionHelperBase.GetOptionalXmlAttribute<CorrelatedMonitorAction>(xmlNode, "ActionOnCorrelatedMonitors", CorrelatedMonitorAction.LogAndContinue, base.TraceContext);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001A4BC File Offset: 0x000186BC
		internal override string ToString(WorkDefinition workItem)
		{
			ResponderDefinition responderDefinition = (ResponderDefinition)workItem;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString(workItem));
			stringBuilder.AppendLine("AlertTypeId: " + responderDefinition.AlertTypeId);
			stringBuilder.AppendLine("AlertMask: " + responderDefinition.AlertMask);
			stringBuilder.AppendLine("WaitIntervalSeconds: " + responderDefinition.WaitIntervalSeconds);
			stringBuilder.AppendLine("Account: " + responderDefinition.Account);
			stringBuilder.AppendLine("AccountPassword: " + responderDefinition.AccountPassword);
			stringBuilder.AppendLine("EscalationSubject: " + responderDefinition.EscalationSubject);
			stringBuilder.AppendLine("EscalationMessage: " + responderDefinition.EscalationMessage);
			stringBuilder.AppendLine("EscalationTeam: " + responderDefinition.EscalationTeam);
			stringBuilder.AppendLine("Endpoint: " + responderDefinition.Endpoint);
			stringBuilder.AppendLine("TargetHealthState: " + responderDefinition.TargetHealthState);
			stringBuilder.AppendLine("NotificationServiceClass: " + responderDefinition.NotificationServiceClass);
			stringBuilder.AppendLine("MinimumSecondsBetweenEscalates: " + responderDefinition.MinimumSecondsBetweenEscalates);
			return stringBuilder.ToString();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001A614 File Offset: 0x00018814
		protected ResponderDefinition CreateResponderDefinition()
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			base.CreateBaseWorkDefinition(responderDefinition);
			responderDefinition.AlertTypeId = this.AlertTypeId;
			responderDefinition.AlertMask = this.AlertMask;
			responderDefinition.EscalationTeam = this.EscalationTeam;
			responderDefinition.EscalationSubject = this.EscalationSubject;
			responderDefinition.EscalationMessage = this.EscalationMessage;
			responderDefinition.NotificationServiceClass = this.NotificationServiceClass;
			responderDefinition.WaitIntervalSeconds = this.WaitIntervalSeconds;
			responderDefinition.TargetHealthState = this.TargetHealthState;
			responderDefinition.Account = this.Account;
			responderDefinition.AccountPassword = this.AccountPassword;
			responderDefinition.Endpoint = this.Endpoint;
			if (this.AlwaysEscalateOnMonitorChanges != null)
			{
				responderDefinition.AlwaysEscalateOnMonitorChanges = this.AlwaysEscalateOnMonitorChanges.Value;
			}
			responderDefinition.CorrelatedMonitorsXml = this.CorrelatedMonitorsXml;
			responderDefinition.ActionOnCorrelatedMonitors = this.ActionOnCorrelatedMonitors;
			return responderDefinition;
		}
	}
}

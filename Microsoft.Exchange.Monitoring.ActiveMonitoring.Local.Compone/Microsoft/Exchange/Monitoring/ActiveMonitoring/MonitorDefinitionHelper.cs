using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000077 RID: 119
	internal abstract class MonitorDefinitionHelper : DefinitionHelperBase
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001A7B6 File Offset: 0x000189B6
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0001A7BE File Offset: 0x000189BE
		internal string SampleMask { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001A7C7 File Offset: 0x000189C7
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0001A7CF File Offset: 0x000189CF
		internal int MonitoringIntervalSeconds { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001A7D8 File Offset: 0x000189D8
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0001A7E0 File Offset: 0x000189E0
		internal int MinimumErrorCount { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001A7E9 File Offset: 0x000189E9
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0001A7F1 File Offset: 0x000189F1
		internal double MonitoringThreshold { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001A7FA File Offset: 0x000189FA
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001A802 File Offset: 0x00018A02
		internal double SecondaryMonitoringThreshold { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001A80B File Offset: 0x00018A0B
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001A813 File Offset: 0x00018A13
		internal int ServicePriority { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001A81C File Offset: 0x00018A1C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001A824 File Offset: 0x00018A24
		internal ServiceSeverity ServiceSeverity { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001A82D File Offset: 0x00018A2D
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0001A835 File Offset: 0x00018A35
		internal bool IsHaImpacting { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001A83E File Offset: 0x00018A3E
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0001A846 File Offset: 0x00018A46
		internal int InsufficientSamplesIntervalSeconds { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001A84F File Offset: 0x00018A4F
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0001A857 File Offset: 0x00018A57
		internal string StateAttribute1Mask { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001A860 File Offset: 0x00018A60
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0001A868 File Offset: 0x00018A68
		internal int FailureCategoryMask { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001A871 File Offset: 0x00018A71
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001A879 File Offset: 0x00018A79
		internal int Version { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001A882 File Offset: 0x00018A82
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0001A88A File Offset: 0x00018A8A
		internal MonitorStateTransition[] MonitorStateTransitions { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001A893 File Offset: 0x00018A93
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0001A89B File Offset: 0x00018A9B
		internal bool AllowCorrelationToMonitor { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0001A8AC File Offset: 0x00018AAC
		internal string TargetScopes { get; set; }

		// Token: 0x06000470 RID: 1136 RVA: 0x0001A8B8 File Offset: 0x00018AB8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine("SampleMask: " + this.SampleMask);
			stringBuilder.AppendLine("MonitoringIntervalSeconds: " + this.MonitoringIntervalSeconds);
			stringBuilder.AppendLine("MonitoringThreshold: " + this.MonitoringThreshold);
			stringBuilder.AppendLine("SecondaryMonitoringThreshold: " + this.SecondaryMonitoringThreshold);
			stringBuilder.AppendLine("MinimumErrorCount: " + this.MinimumErrorCount);
			stringBuilder.AppendLine("InsufficientSamplesIntervalSeconds: " + this.InsufficientSamplesIntervalSeconds);
			stringBuilder.AppendLine("StateAttribute1Mask: " + this.StateAttribute1Mask);
			stringBuilder.AppendLine("FailureCategoryMask: " + this.FailureCategoryMask);
			stringBuilder.AppendLine("ServicePriority: " + this.ServicePriority);
			stringBuilder.AppendLine("ServiceSeverity: " + this.ServiceSeverity);
			foreach (MonitorStateTransition arg in this.MonitorStateTransitions)
			{
				stringBuilder.AppendLine("MonitorStateTransition: " + arg);
			}
			stringBuilder.AppendLine("AllowCorrelationToMonitor: " + this.AllowCorrelationToMonitor);
			stringBuilder.AppendLine("TargetScopes: " + this.TargetScopes);
			return stringBuilder.ToString();
		}

		// Token: 0x06000471 RID: 1137
		internal abstract MonitorDefinition CreateDefinition();

		// Token: 0x06000472 RID: 1138 RVA: 0x0001AA4C File Offset: 0x00018C4C
		internal override void ReadDiscoveryXml()
		{
			base.ReadDiscoveryXml();
			this.SampleMask = this.GetSampleMask();
			this.StateAttribute1Mask = base.GetOptionalXmlAttribute<string>("StateAttribute1Mask", null);
			this.FailureCategoryMask = base.GetOptionalXmlAttribute<int>("FailureCategoryMask", -1);
			this.MonitoringIntervalSeconds = base.GetOptionalXmlAttribute<int>("MonitoringIntervalSeconds", 0);
			this.MinimumErrorCount = base.GetOptionalXmlAttribute<int>("MinimumErrorCount", 0);
			this.MonitoringThreshold = base.GetOptionalXmlAttribute<double>("MonitoringThreshold", 0.0);
			this.SecondaryMonitoringThreshold = base.GetOptionalXmlAttribute<double>("SecondaryMonitoringThreshold", 0.0);
			this.InsufficientSamplesIntervalSeconds = base.GetOptionalXmlAttribute<int>("InsufficientSamplesIntervalSeconds", (int)TimeSpan.FromHours(8.0).TotalSeconds);
			string optionalXmlAttribute = base.GetOptionalXmlAttribute<string>("ServicePriority", string.Empty);
			if (!string.IsNullOrEmpty(optionalXmlAttribute))
			{
				this.ServicePriority = int.Parse(optionalXmlAttribute);
			}
			optionalXmlAttribute = base.GetOptionalXmlAttribute<string>("ServiceSeverity", string.Empty);
			if (!string.IsNullOrEmpty(optionalXmlAttribute))
			{
				this.ServiceSeverity = (ServiceSeverity)Enum.Parse(typeof(ServiceSeverity), optionalXmlAttribute);
			}
			this.MonitorStateTransitions = this.GetStateTransitions(base.DefinitionNode);
			this.AllowCorrelationToMonitor = base.GetOptionalXmlAttribute<bool>("AllowCorrelationToMonitor", false);
			this.TargetScopes = base.GetOptionalXmlAttribute<string>("TargetScopes", null);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		internal override string ToString(WorkDefinition workItem)
		{
			MonitorDefinition monitorDefinition = (MonitorDefinition)workItem;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString(workItem));
			stringBuilder.AppendLine("ComponentName: " + monitorDefinition.ComponentName);
			stringBuilder.AppendLine("SampleMask: " + monitorDefinition.SampleMask);
			stringBuilder.AppendLine("MonitoringIntervalSeconds: " + monitorDefinition.MonitoringIntervalSeconds);
			stringBuilder.AppendLine("MonitoringThreshold: " + monitorDefinition.MonitoringThreshold);
			stringBuilder.AppendLine("SecondaryMonitoringThreshold: " + monitorDefinition.SecondaryMonitoringThreshold);
			stringBuilder.AppendLine("MinimumErrorCount: " + monitorDefinition.MinimumErrorCount);
			stringBuilder.AppendLine("InsufficientSamplesIntervalSeconds: " + monitorDefinition.InsufficientSamplesIntervalSeconds);
			stringBuilder.AppendLine("StateAttribute1Mask: " + monitorDefinition.StateAttribute1Mask);
			stringBuilder.AppendLine("FailureCategoryMask: " + monitorDefinition.FailureCategoryMask);
			stringBuilder.AppendLine("ServicePriority: " + monitorDefinition.ServicePriority);
			stringBuilder.AppendLine("ServiceSeverity: " + monitorDefinition.ServiceSeverity);
			foreach (MonitorStateTransition arg in monitorDefinition.MonitorStateTransitions)
			{
				stringBuilder.AppendLine("MonitorStateTransition: " + arg);
			}
			stringBuilder.AppendLine("TargetScopes: " + monitorDefinition.TargetScopes);
			return stringBuilder.ToString();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001AD3C File Offset: 0x00018F3C
		protected MonitorDefinition CreateMonitorDefinition()
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			base.CreateBaseWorkDefinition(monitorDefinition);
			monitorDefinition.SampleMask = this.SampleMask;
			monitorDefinition.Component = base.Component;
			monitorDefinition.MonitoringThreshold = this.MonitoringThreshold;
			monitorDefinition.SecondaryMonitoringThreshold = this.SecondaryMonitoringThreshold;
			monitorDefinition.MonitoringIntervalSeconds = this.MonitoringIntervalSeconds;
			monitorDefinition.MinimumErrorCount = this.MinimumErrorCount;
			monitorDefinition.InsufficientSamplesIntervalSeconds = this.InsufficientSamplesIntervalSeconds;
			monitorDefinition.MonitorStateTransitions = this.MonitorStateTransitions;
			monitorDefinition.ServicePriority = this.ServicePriority;
			monitorDefinition.ServiceSeverity = this.ServiceSeverity;
			monitorDefinition.StateAttribute1Mask = this.StateAttribute1Mask;
			monitorDefinition.FailureCategoryMask = this.FailureCategoryMask;
			monitorDefinition.AllowCorrelationToMonitor = this.AllowCorrelationToMonitor;
			monitorDefinition.TargetScopes = this.TargetScopes;
			return monitorDefinition;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001ADFF File Offset: 0x00018FFF
		protected void GetAdditionalProperties(MonitorDefinition monitor)
		{
			monitor.MonitorStateTransitions = this.MonitorStateTransitions;
			monitor.ExtensionAttributes = base.ExtensionAttributes;
			monitor.ParseExtensionAttributes(false);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001AE20 File Offset: 0x00019020
		private string GetSampleMask()
		{
			if (base.DiscoveryContext is PerfCounter)
			{
				return PerformanceCounterNotificationItem.GenerateResultName(((PerfCounter)base.DiscoveryContext).PerfCounterName);
			}
			if (base.DiscoveryContext is NTEvent && ((NTEvent)base.DiscoveryContext).IsInstrumented)
			{
				string mandatoryXmlAttribute = base.GetMandatoryXmlAttribute<string>("EventNotificationServiceName");
				string mandatoryXmlAttribute2 = base.GetMandatoryXmlAttribute<string>("EventNotificationComponent");
				string optionalXmlAttribute = base.GetOptionalXmlAttribute<string>("EventNotificationTag", string.Empty);
				return NotificationItem.GenerateResultName(mandatoryXmlAttribute, mandatoryXmlAttribute2, optionalXmlAttribute);
			}
			if (base.WorkItemType == typeof(ComponentHealthHeartbeatMonitor) || base.WorkItemType == typeof(ComponentHealthPercentFailureMonitor))
			{
				return "*";
			}
			return base.GetMandatoryXmlAttribute<string>("SampleMask");
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001AEE0 File Offset: 0x000190E0
		private MonitorStateTransition[] GetStateTransitions(XmlNode definition)
		{
			List<MonitorStateTransition> list = new List<MonitorStateTransition>();
			XmlNode xmlNode = definition.SelectSingleNode("StateTransitions");
			if (xmlNode != null)
			{
				using (XmlNodeList childNodes = xmlNode.ChildNodes)
				{
					if (childNodes != null)
					{
						foreach (object obj in childNodes)
						{
							XmlNode definition2 = (XmlNode)obj;
							ServiceHealthStatus mandatoryXmlEnumAttribute = DefinitionHelperBase.GetMandatoryXmlEnumAttribute<ServiceHealthStatus>(definition2, "ToState", base.TraceContext);
							int mandatoryXmlAttribute = DefinitionHelperBase.GetMandatoryXmlAttribute<int>(definition2, "TimeoutInSeconds", base.TraceContext);
							MonitorStateTransition monitorStateTransition = new MonitorStateTransition(mandatoryXmlEnumAttribute, mandatoryXmlAttribute);
							WTFDiagnostics.TraceDebug<ServiceHealthStatus, TimeSpan>(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "[Transition] {0} Timeout:{1}", monitorStateTransition.ToState, monitorStateTransition.TransitionTimeout, null, "GetStateTransitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\MonitorDefinitionHelper.cs", 330);
							list.Add(monitorStateTransition);
						}
					}
				}
			}
			return list.ToArray();
		}
	}
}

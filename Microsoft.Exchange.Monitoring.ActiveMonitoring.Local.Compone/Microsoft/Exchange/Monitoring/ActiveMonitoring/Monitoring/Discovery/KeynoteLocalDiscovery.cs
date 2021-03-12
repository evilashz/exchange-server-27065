using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Monitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Monitoring.Discovery
{
	// Token: 0x0200021A RID: 538
	public class KeynoteLocalDiscovery : CentralMaintenanceWorkItem
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x00064BB4 File Offset: 0x00062DB4
		public override Task GenerateWorkItems(CancellationToken cancellationToken)
		{
			string rulesFile = base.Definition.Attributes["RulesFile"];
			if (string.IsNullOrEmpty(rulesFile))
			{
				WTFDiagnostics.TraceInformation<TracingContext>(ExTraceGlobals.MonitoringTracer, base.TraceContext, string.Format("Rules file path is not set.  No more operations for this maintenance workitem.", new object[0]), base.TraceContext, null, "GenerateWorkItems", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\Keynote\\KeynoteLocalDiscovery.cs", 99);
				throw new Exception("keynote measurement alerting configuration rules file is not found");
			}
			Task<XDocument> task = Task.Factory.StartNew<XDocument>(delegate()
			{
				XDocument result;
				try
				{
					string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
					result = XDocument.Load(Path.Combine(directoryName, rulesFile));
				}
				catch (XmlException ex)
				{
					WTFDiagnostics.TraceError<TracingContext>(ExTraceGlobals.MonitoringTracer, this.TraceContext, string.Format("Rules file load failed from path: {0}.  Failure detail: {1}", rulesFile, ex), this.TraceContext, null, "GenerateWorkItems", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\Keynote\\KeynoteLocalDiscovery.cs", 114);
					throw new XmlException(string.Format("XML load failed for rules file path {0}", rulesFile), ex);
				}
				catch (IOException ex2)
				{
					WTFDiagnostics.TraceError<TracingContext>(ExTraceGlobals.MonitoringTracer, this.TraceContext, string.Format("Rules file load failed from path: {0}.  Failure detail: {1}", rulesFile, ex2), this.TraceContext, null, "GenerateWorkItems", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\Keynote\\KeynoteLocalDiscovery.cs", 119);
					throw new XmlException(string.Format("XML load failed for rules file path {0} with IO exception", rulesFile), ex2);
				}
				return result;
			}, cancellationToken, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
			task.ContinueWith(delegate(Task<XDocument> rulesResult)
			{
				if (rulesResult.Result != null)
				{
					using (IEnumerator<XElement> enumerator = rulesResult.Result.Element("KeynoteRules").Elements().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							XElement xelement = enumerator.Current;
							double minAvailabilityThreshold;
							double.TryParse(xelement.Attribute("minAvailabilityThreshold").Value, out minAvailabilityThreshold);
							string targetDatabase = base.Definition.Attributes["TargetDatabase"];
							string targetServer = base.Definition.Attributes["TargetServer"];
							int lookbackMinutes;
							int.TryParse(base.Definition.Attributes["LookBackMinutes"], out lookbackMinutes);
							int num;
							int.TryParse(xelement.Attribute("minISPCountThreshold").Value, out num);
							int aggregationLevel;
							int.TryParse(xelement.Attribute("aggregationLevel").Value, out aggregationLevel);
							string value = xelement.Attribute("target").Value;
							bool flag;
							bool.TryParse(xelement.Attribute("Urgent").Value, out flag);
							MonitorDefinition monitorDefinition = KeynoteLocalDiscovery.CreateMonitorDefinition("KeynoteMeasurements", value, aggregationLevel, num, minAvailabilityThreshold, lookbackMinutes, targetServer, targetDatabase);
							base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
							ResponderDefinition definition = KeynoteEscalateResponder.CreateResponderDefinition(monitorDefinition.ConstructWorkItemResultName(), value, minAvailabilityThreshold, num, flag ? NotificationServiceClass.Urgent : NotificationServiceClass.Scheduled);
							base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
						}
						return;
					}
				}
				WTFDiagnostics.TraceInformation<TracingContext>(ExTraceGlobals.MonitoringTracer, base.TraceContext, string.Format("Rules file load failed.  Hence no maintenance workitems were created.", new object[0]), base.TraceContext, null, "GenerateWorkItems", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\Keynote\\KeynoteLocalDiscovery.cs", 177);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent, TaskScheduler.Default);
			return task;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00064C70 File Offset: 0x00062E70
		private static MonitorDefinition CreateMonitorDefinition(string sampleMask, string target, int aggregationLevel, int minIspCountThreshold, double minAvailabilityThreshold, int lookbackMinutes, string targetServer, string targetDatabase)
		{
			string value = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", targetServer, targetDatabase);
			MonitorDefinition monitorDefinition = KeynoteMeasurementsMonitor.CreateDefinition("Keynote Measurements Monitor", sampleMask, ExchangeComponent.Monitoring.Name, ExchangeComponent.Monitoring, 1, true);
			monitorDefinition.RecurrenceIntervalSeconds = 600;
			monitorDefinition.TimeoutSeconds = 60;
			monitorDefinition.MonitoringIntervalSeconds = 1200;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.WorkItemVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			monitorDefinition.TypeName = typeof(KeynoteMeasurementsMonitor).FullName;
			monitorDefinition.ExecutionLocation = "EXO";
			monitorDefinition.TargetResource = target;
			monitorDefinition.InsufficientSamplesIntervalSeconds = 0;
			monitorDefinition.Attributes["LookBackMinutes"] = lookbackMinutes.ToString();
			monitorDefinition.Attributes["aggregationLevel"] = aggregationLevel.ToString();
			monitorDefinition.Attributes["minISPCountThreshold"] = minIspCountThreshold.ToString();
			monitorDefinition.Attributes["minAvailabilityThreshold"] = minAvailabilityThreshold.ToString();
			monitorDefinition.Attributes["Endpoint"] = value;
			return monitorDefinition;
		}

		// Token: 0x04000B52 RID: 2898
		public const string TargetDatabaseAttributeName = "TargetDatabase";

		// Token: 0x04000B53 RID: 2899
		public const string TargetServerAttributeName = "TargetServer";

		// Token: 0x04000B54 RID: 2900
		public const string MinimumIspCountThresholdAttributeName = "minISPCountThreshold";

		// Token: 0x04000B55 RID: 2901
		public const string MinimumAvailabilityThresholdAttributeName = "minAvailabilityThreshold";

		// Token: 0x04000B56 RID: 2902
		public const string AggregationLevelAttributeName = "aggregationLevel";

		// Token: 0x04000B57 RID: 2903
		public const string LookBackMinutesAttributeName = "LookBackMinutes";

		// Token: 0x04000B58 RID: 2904
		private const string UrgentAttribute = "Urgent";

		// Token: 0x04000B59 RID: 2905
		public const string TargetAttributename = "target";

		// Token: 0x04000B5A RID: 2906
		public const string EndpointAttributeName = "Endpoint";

		// Token: 0x04000B5B RID: 2907
		public const int MonitorRecurrenceSeconds = 600;

		// Token: 0x04000B5C RID: 2908
		public const int ResponderRecurrenceSeconds = 300;

		// Token: 0x04000B5D RID: 2909
		private const string KeynoteMonitorName = "Keynote Measurements Monitor";
	}
}

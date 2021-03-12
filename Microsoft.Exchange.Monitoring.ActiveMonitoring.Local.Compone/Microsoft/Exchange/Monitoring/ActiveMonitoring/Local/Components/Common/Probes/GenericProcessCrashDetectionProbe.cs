using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Common.Probes
{
	// Token: 0x020000CF RID: 207
	public class GenericProcessCrashDetectionProbe : ProbeWorkItem
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00028380 File Offset: 0x00026580
		static GenericProcessCrashDetectionProbe()
		{
			CentralEventLogWatcher.Instance.BeforeEnqueueEvent += delegate(EventRecord eventRecord, CentralEventLogWatcher.EventRecordMini eventRecordMini)
			{
				if (eventRecordMini.EventId == 4999 && string.Equals(eventRecordMini.LogName, "Application") && string.Equals(eventRecordMini.Source, "MSExchange Common"))
				{
					IList<EventProperty> properties = eventRecord.Properties;
					if (properties != null && properties.Count >= 8)
					{
						eventRecordMini.WatsonProcessName = properties[4].Value.ToString();
						eventRecordMini.WatsonExtendedPropertyField1 = properties[5].Value.ToString();
						eventRecordMini.WatsonExtendedPropertyField2 = properties[6].Value.ToString();
						eventRecordMini.WatsonExtendedPropertyField3 = properties[7].Value.ToString();
						if (string.Equals(properties[1].Value.ToString(), "E12N", StringComparison.InvariantCultureIgnoreCase))
						{
							eventRecordMini.IsProcessTerminatingWatson = true;
							return;
						}
						if (properties != null && properties.Count >= 12)
						{
							bool isProcessTerminatingWatson = false;
							if (bool.TryParse(properties[11].Value.ToString(), out isProcessTerminatingWatson))
							{
								eventRecordMini.IsProcessTerminatingWatson = isProcessTerminatingWatson;
								return;
							}
							eventRecordMini.IsProcessTerminatingWatson = true;
							return;
						}
						else
						{
							eventRecordMini.IsProcessTerminatingWatson = true;
						}
					}
				}
			};
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000283D8 File Offset: 0x000265D8
		public static ProbeDefinition CreateDefinition(string name, string targetResource, int recurrenceInterval, string moduleName = null, bool skipInformationalWatson = false)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = GenericProcessCrashDetectionProbe.AssemblyPath;
			probeDefinition.TypeName = GenericProcessCrashDetectionProbe.TypeName;
			probeDefinition.Name = name;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceInterval;
			probeDefinition.TimeoutSeconds = recurrenceInterval / 2;
			probeDefinition.TargetResource = targetResource;
			if (!string.IsNullOrEmpty(moduleName))
			{
				probeDefinition.Attributes["ModuleName"] = moduleName;
			}
			probeDefinition.Attributes["SkipInformationalWatson"] = skipInformationalWatson.ToString();
			return probeDefinition;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00028450 File Offset: 0x00026650
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition definition, Dictionary<string, string> propertyBag)
		{
			definition.TargetResource = propertyBag["TargetResource"];
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002846C File Offset: 0x0002666C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			string targetResource = base.Definition.TargetResource;
			string @string = attributeHelper.GetString("ModuleName", false, null);
			DateTime utcNow = DateTime.UtcNow;
			int recurrenceIntervalSeconds = base.Definition.RecurrenceIntervalSeconds;
			DateTime.UtcNow.AddSeconds((double)(-(double)recurrenceIntervalSeconds));
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ServiceTracer, base.TraceContext, "Starting Process crash detection against {0}", targetResource, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\GenericProcessCrashDetectionProbe.cs", 156);
			try
			{
				string text = base.Definition.ConstructWorkItemResultName();
				CentralEventLogWatcher.ProcessCrashRule rule = new CentralEventLogWatcher.ProcessCrashRule(text, targetResource, @string, attributeHelper.GetBool("SkipInformationalWatson", false, false));
				if (!CentralEventLogWatcher.Instance.IsEventWatchRuleExists(rule))
				{
					TimeSpan timeout = TimeSpan.FromSeconds((double)Math.Min(base.Definition.TimeoutSeconds / 2, 2));
					WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "GenericProcessCrashDetectionProbe:: Registering ServiceName={0} as Rule={1}", targetResource, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Probes\\GenericProcessCrashDetectionProbe.cs", 176);
					CentralEventLogWatcher.Instance.AddEventWatchRule(rule);
					base.Result.StateAttribute13 = string.Format("Rule {0} is added into CentralEventLogWatcher (Success={1}). Waited for {2} secs before trying to get results...", text, CentralEventLogWatcher.Instance.IsEventWatchRuleExists(rule), timeout.TotalSeconds);
					Thread.Sleep(timeout);
				}
				CentralEventLogWatcher.EventRecordMini eventRecordMini = null;
				base.Result.StateAttribute6 = (double)CentralEventLogWatcher.Instance.PopLastEventRecordForRule(text, out eventRecordMini);
				CentralEventLogWatcher.EventProcessorStatus eventProcessorCurrentStatus = CentralEventLogWatcher.Instance.EventProcessorCurrentStatus;
				base.Result.StateAttribute14 = string.Format("EventsProcessed={0}, EventProcessorTimeSpentInMs={1}, EventProcessorLastRun={2}, EventProcessorTimer={3}, EventProcessorsCount={4}", new object[]
				{
					eventProcessorCurrentStatus.EventsProcessedSinceInstanceStart,
					eventProcessorCurrentStatus.LastEventProcessorTimeSpentInMs,
					eventProcessorCurrentStatus.LastEventProcessorRuntime.ToString(),
					eventProcessorCurrentStatus.TimerInterval,
					eventProcessorCurrentStatus.EventProcessorsRunningCount
				});
				if (eventRecordMini != null)
				{
					base.Result.StateAttribute2 = eventRecordMini.WatsonExtendedPropertyField1;
					base.Result.StateAttribute3 = eventRecordMini.WatsonExtendedPropertyField2;
					base.Result.StateAttribute4 = eventRecordMini.WatsonExtendedPropertyField3;
					base.Result.StateAttribute1 = string.Format("Process: '{0}'. \nAdditional properties: '{1}', '{2}', '{3}'.", new object[]
					{
						eventRecordMini.WatsonProcessName,
						base.Result.StateAttribute2,
						base.Result.StateAttribute3,
						base.Result.StateAttribute4
					});
					throw new ProcessCrashingException(targetResource, Environment.MachineName);
				}
				base.Result.StateAttribute1 = string.Format("No relevant crash events found for service {0}", targetResource);
			}
			finally
			{
				base.Result.SampleValue = (double)((int)(DateTime.UtcNow - utcNow).TotalMilliseconds);
			}
		}

		// Token: 0x0400046C RID: 1132
		private const string SkipInformationalWatson = "SkipInformationalWatson";

		// Token: 0x0400046D RID: 1133
		private const string ModuleName = "ModuleName";

		// Token: 0x0400046E RID: 1134
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400046F RID: 1135
		private static readonly string TypeName = typeof(GenericProcessCrashDetectionProbe).FullName;
	}
}

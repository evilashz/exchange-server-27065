using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants.Probes
{
	// Token: 0x0200017B RID: 379
	public class EventAssistantsWatermarksProbe : ProbeWorkItem
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x00046584 File Offset: 0x00044784
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition definition, Dictionary<string, string> propertyBag)
		{
			EventAssistantsDiscovery.PopulateProbeDefinition(definition as ProbeDefinition, propertyBag["TargetResource"], base.GetType(), definition.Name, TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0));
			ProbeDefinition probeDefinition = StoreMonitoringHelpers.GetProbeDefinition(Environment.MachineName, definition.Name, propertyBag["TargetResource"], ExchangeComponent.EventAssistants.Name);
			Dictionary<string, string> dictionary = DefinitionHelperBase.ConvertExtensionAttributesToDictionary(probeDefinition.ExtensionAttributes);
			definition.Attributes["WatermarksVariationThreshold"] = dictionary["WatermarksVariationThreshold"];
			definition.Attributes["WatermarksBehindWarningThreshold"] = dictionary["WatermarksBehindWarningThreshold"];
			definition.Attributes["IncludedAssistantType"] = dictionary["IncludedAssistantType"];
			if (dictionary.ContainsKey("ExcludedAssistantType"))
			{
				definition.Attributes["ExcludedAssistantType"] = dictionary["ExcludedAssistantType"];
			}
			MailboxDatabase mailboxDatabaseFromName = DirectoryAccessor.Instance.GetMailboxDatabaseFromName(propertyBag["TargetResource"]);
			definition.TargetExtension = mailboxDatabaseFromName.Guid.ToString();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000466D8 File Offset: 0x000448D8
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>
			{
				new PropertyInformation("Identity", Strings.EventAssistantsWatermarksHelpString, true)
			};
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00046708 File Offset: 0x00044908
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DateTime utcNow = DateTime.UtcNow;
			string targetResource = base.Definition.TargetResource;
			string targetExtension = base.Definition.TargetExtension;
			Guid guid = new Guid(targetExtension);
			List<string> excludedAssistants = new List<string>();
			try
			{
				base.Result.StateAttribute1 = targetResource;
				base.Result.StateAttribute2 = targetExtension;
				int num = int.Parse(base.Definition.Attributes["WatermarksVariationThreshold"]);
				TimeSpan watermarkBehindWarningThrehold = TimeSpan.Parse(base.Definition.Attributes["WatermarksBehindWarningThreshold"]);
				base.Result.StateAttribute3 = watermarkBehindWarningThrehold.ToString();
				base.Result.StateAttribute6 = (double)num;
				string text = base.Definition.Attributes["IncludedAssistantType"];
				Guid? assistantConsumerGuidFromName = AssistantsCollection.GetAssistantConsumerGuidFromName(text);
				if (assistantConsumerGuidFromName == null)
				{
					throw new InvalidIncludedAssistantTypeException();
				}
				if (base.Definition.Attributes.ContainsKey("ExcludedAssistantType") && !string.IsNullOrWhiteSpace(base.Definition.Attributes["ExcludedAssistantType"]))
				{
					excludedAssistants = base.Definition.Attributes["ExcludedAssistantType"].Split(new char[]
					{
						','
					}).ToList<string>();
				}
				if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(guid))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "Starting database watermarks check against database {0}", targetResource, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksProbe.cs", 126);
					using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Monitoring", Environment.MachineName, null, null, null))
					{
						Guid empty = Guid.Empty;
						Watermark[] watermarksForMailbox = exRpcAdmin.GetWatermarksForMailbox(guid, ref empty, Guid.Empty);
						MapiEventManager mapiEventManager = MapiEventManager.Create(exRpcAdmin, assistantConsumerGuidFromName.Value, guid);
						long eventCounter = mapiEventManager.ReadLastEvent().EventCounter;
						foreach (Watermark watermark in watermarksForMailbox)
						{
							if (eventCounter - watermark.EventCounter > (long)num)
							{
								EventAssistantsWatermarksProbe.WatermarkWithCreateTime[] waterMarkWithCreateTimes = EventAssistantsWatermarksProbe.BuildWaterMarkWithCreateTimes(mapiEventManager, watermarksForMailbox);
								DateTime eventTime = EventAssistantsWatermarksProbe.GetEventTime(mapiEventManager, eventCounter);
								List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime> list = EventAssistantsWatermarksProbe.PopulateProblematicAssistants(waterMarkWithCreateTimes, eventTime, watermarkBehindWarningThrehold, text, excludedAssistants);
								bool flag = EventAssistantsWatermarksProbe.MakeFailureDecision(text, list);
								if (flag)
								{
									base.Result.StateAttribute11 = EventAssistantsWatermarksProbe.FindProblematicAssistant(list);
									base.Result.StateAttribute4 = EventAssistantsWatermarksProbe.BuildFormattedEventCounter(eventCounter, eventTime);
									base.Result.StateAttribute5 = EventAssistantsWatermarksProbe.BuildFormattedWatermarks(list);
									WTFDiagnostics.TraceError<string, string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "Database {0} is behind on watermarks. Last event counter: {1} Watermarks: {2}", targetResource, base.Result.StateAttribute4, base.Result.StateAttribute5, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksProbe.cs", 189);
									throw new WatermarksBehindException(targetResource);
								}
							}
						}
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "Successfully finished database watermarks check against database {0}", targetResource, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksProbe.cs", 202);
						goto IL_2F5;
					}
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.StoreTracer, base.TraceContext, "Skipping database watermarks check against database {0} as it is not mounted locally", targetResource, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsWatermarksProbe.cs", 211);
				IL_2F5:;
			}
			finally
			{
				base.Result.SampleValue = (double)((int)(DateTime.UtcNow - utcNow).TotalMilliseconds);
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00046A64 File Offset: 0x00044C64
		internal static List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime> PopulateProblematicAssistants(EventAssistantsWatermarksProbe.WatermarkWithCreateTime[] waterMarkWithCreateTimes, DateTime lastEventTime, TimeSpan watermarkBehindWarningThrehold, string includedAssistantType, List<string> excludedAssistants)
		{
			List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime> list = new List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime>();
			if (waterMarkWithCreateTimes != null)
			{
				foreach (EventAssistantsWatermarksProbe.WatermarkWithCreateTime watermarkWithCreateTime in waterMarkWithCreateTimes)
				{
					if (watermarkWithCreateTime != null && lastEventTime - watermarkWithCreateTime.CreateTime > watermarkBehindWarningThrehold && AssistantsCollection.Contains(watermarkWithCreateTime.ConsumerGuid) && excludedAssistants != null && !excludedAssistants.Contains(AssistantsCollection.GetAssistantName(watermarkWithCreateTime.ConsumerGuid), StringComparer.InvariantCultureIgnoreCase))
					{
						list.Add(watermarkWithCreateTime);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00046AD8 File Offset: 0x00044CD8
		internal static bool MakeFailureDecision(string includedAssistantType, List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime> lowWatermarks)
		{
			if (lowWatermarks != null && lowWatermarks.Count != 0)
			{
				if (lowWatermarks.Count == 1 && (string.Equals(AssistantsCollection.GetAssistantName(lowWatermarks[0].ConsumerGuid), includedAssistantType) || string.Equals(includedAssistantType, "MultipleAssistants")))
				{
					return true;
				}
				if (lowWatermarks.Count > 1 && string.Equals(includedAssistantType, "MultipleAssistants"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00046B3C File Offset: 0x00044D3C
		internal static string FindProblematicAssistant(List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime> lowWatermarks)
		{
			if (lowWatermarks.Count == 1)
			{
				return AssistantsCollection.GetAssistantName(lowWatermarks[0].ConsumerGuid);
			}
			if (lowWatermarks.Count == AssistantsCollection.complianceAssistants.Count)
			{
				for (int i = 0; i < lowWatermarks.Count; i++)
				{
					if (!AssistantsCollection.complianceAssistants.Contains(AssistantsCollection.GetAssistantName(lowWatermarks[i].ConsumerGuid), StringComparer.InvariantCultureIgnoreCase))
					{
						return "MultipleAssistants";
					}
				}
				return AssistantsCollection.GetAssistantName(lowWatermarks[0].ConsumerGuid);
			}
			return "MultipleAssistants";
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00046BC8 File Offset: 0x00044DC8
		private static EventAssistantsWatermarksProbe.WatermarkWithCreateTime[] BuildWaterMarkWithCreateTimes(MapiEventManager eventManager, Watermark[] watermarks)
		{
			EventAssistantsWatermarksProbe.WatermarkWithCreateTime[] array = new EventAssistantsWatermarksProbe.WatermarkWithCreateTime[watermarks.Length];
			for (int i = 0; i < watermarks.Length; i++)
			{
				DateTime eventTime = EventAssistantsWatermarksProbe.GetEventTime(eventManager, watermarks[i].EventCounter);
				array[i] = new EventAssistantsWatermarksProbe.WatermarkWithCreateTime(watermarks[i].ConsumerGuid, watermarks[i].EventCounter, eventTime);
			}
			return array;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00046C18 File Offset: 0x00044E18
		private static DateTime GetEventTime(MapiEventManager eventManager, long eventCounter)
		{
			MapiEvent[] array = eventManager.ReadEvents(eventCounter, 1);
			if (array != null && array.Length != 0)
			{
				return array[0].CreateTime;
			}
			return DateTime.UtcNow;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00046C44 File Offset: 0x00044E44
		private static string BuildFormattedWatermarks(List<EventAssistantsWatermarksProbe.WatermarkWithCreateTime> watermarks)
		{
			StringBuilder stringBuilder = new StringBuilder(watermarks.Count * 70);
			for (int i = 0; i < watermarks.Count; i++)
			{
				stringBuilder.Append(AssistantsCollection.GetAssistantName(watermarks[i].ConsumerGuid));
				stringBuilder.Append(" : ");
				stringBuilder.Append(EventAssistantsWatermarksProbe.BuildFormattedEventCounter(watermarks[i].EventCounter, watermarks[i].CreateTime));
				if (i != watermarks.Count - 1)
				{
					stringBuilder.Append(", \r\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00046CD6 File Offset: 0x00044ED6
		private static string BuildFormattedEventCounter(long eventCounter, DateTime eventTime)
		{
			return string.Format("{0} : {1}", eventCounter, eventTime);
		}

		// Token: 0x0200017C RID: 380
		public class WatermarkWithCreateTime
		{
			// Token: 0x06000B1A RID: 2842 RVA: 0x00046CF6 File Offset: 0x00044EF6
			public WatermarkWithCreateTime(Guid consumerGuid, long eventCounter, DateTime createTime)
			{
				this.ConsumerGuid = consumerGuid;
				this.EventCounter = eventCounter;
				this.CreateTime = createTime;
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00046D13 File Offset: 0x00044F13
			// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00046D1B File Offset: 0x00044F1B
			public Guid ConsumerGuid { get; private set; }

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00046D24 File Offset: 0x00044F24
			// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00046D2C File Offset: 0x00044F2C
			public long EventCounter { get; private set; }

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00046D35 File Offset: 0x00044F35
			// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00046D3D File Offset: 0x00044F3D
			public DateTime CreateTime { get; private set; }
		}
	}
}

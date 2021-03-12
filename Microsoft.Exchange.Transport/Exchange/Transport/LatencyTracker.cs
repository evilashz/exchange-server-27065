using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000180 RID: 384
	internal class LatencyTracker
	{
		// Token: 0x06001089 RID: 4233 RVA: 0x00042EA2 File Offset: 0x000410A2
		protected LatencyTracker()
		{
			this.latencies = new List<LatencyRecord>();
			this.pendingComponents = new List<PendingLatencyRecord>();
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00042EC0 File Offset: 0x000410C0
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x00042EC7 File Offset: 0x000410C7
		public static TransportAppConfig.LatencyTrackerConfig Configuration
		{
			get
			{
				return LatencyTracker.config;
			}
			set
			{
				LatencyTracker.config = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00042ECF File Offset: 0x000410CF
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x00042ED6 File Offset: 0x000410D6
		public static string ProcessShortName { get; private set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00042EE0 File Offset: 0x000410E0
		public static bool MessageLatencyEnabled
		{
			get
			{
				TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig = LatencyTracker.config;
				return latencyTrackerConfig == null || latencyTrackerConfig.MessageLatencyEnabled;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00042EFE File Offset: 0x000410FE
		public static bool ComponentLatencyTrackingEnabled
		{
			get
			{
				return LatencyTracker.HighPrecisionThresholdInterval != TransportAppConfig.LatencyTrackerConfig.MaxLatency;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00042F10 File Offset: 0x00041110
		public static TimeSpan ServerLatencyThreshold
		{
			get
			{
				TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig = LatencyTracker.config;
				if (latencyTrackerConfig != null)
				{
					return latencyTrackerConfig.ServerLatencyThreshold;
				}
				return TransportAppConfig.LatencyTrackerConfig.DefaultServerLatencyThreshold;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00042F34 File Offset: 0x00041134
		public static TimeSpan HighPrecisionThresholdInterval
		{
			get
			{
				TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig = LatencyTracker.config;
				if (latencyTrackerConfig != null)
				{
					return latencyTrackerConfig.ComponentThresholdInterval;
				}
				return TransportAppConfig.LatencyTrackerConfig.DefaultComponentThresholdInterval;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00042F58 File Offset: 0x00041158
		public static TimeSpan MinInterestingUnknownInterval
		{
			get
			{
				TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig = LatencyTracker.config;
				if (latencyTrackerConfig != null)
				{
					return latencyTrackerConfig.MinInterestingUnknownInterval;
				}
				return TransportAppConfig.LatencyTrackerConfig.DefaultMinInterestingUnknownInterval;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00042F7C File Offset: 0x0004117C
		public static bool TrustExternalPickupReceivedHeaders
		{
			get
			{
				TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig = LatencyTracker.config;
				return latencyTrackerConfig != null && latencyTrackerConfig.TrustExternalPickupReceivedHeaders;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00042F9A File Offset: 0x0004119A
		public static bool ServerLatencyTrackingEnabled
		{
			get
			{
				return LatencyTracker.ServerLatencyThreshold != TransportAppConfig.LatencyTrackerConfig.MaxLatency;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00042FAC File Offset: 0x000411AC
		public static bool TreeLatencyTrackingEnabled
		{
			get
			{
				TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig = LatencyTracker.config;
				return latencyTrackerConfig != null && latencyTrackerConfig.TreeLatencyTrackingEnabled;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00042FCA File Offset: 0x000411CA
		public virtual bool SupportsTreeFormatting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00042FCD File Offset: 0x000411CD
		public virtual bool HasCompletedComponents
		{
			get
			{
				return this.latencies.Count > 0;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00042FDD File Offset: 0x000411DD
		public virtual bool HasPendingComponents
		{
			get
			{
				return this.pendingComponents.Count > 0;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00042FED File Offset: 0x000411ED
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00042FF5 File Offset: 0x000411F5
		public long AggregatedUnderThresholdTicks { get; protected set; }

		// Token: 0x0600109B RID: 4251 RVA: 0x00042FFE File Offset: 0x000411FE
		public static TimeSpan TimeSpanFromTicks(long startTime, long endTime)
		{
			return TimeSpan.FromTicks((endTime - startTime) * (10000000L / Stopwatch.Frequency));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00043015 File Offset: 0x00041215
		public static DateTime DefaultTimeProvider()
		{
			return DateTime.UtcNow;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004301C File Offset: 0x0004121C
		public static long DefaultStopWatchProvider()
		{
			return Stopwatch.GetTimestamp();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00043024 File Offset: 0x00041224
		public static void Start(TransportAppConfig.LatencyTrackerConfig configuration, ProcessTransportRole transportRole)
		{
			LatencyPerformanceCounter.SetCategoryNames(LatencyTracker.perfCounterCategoryMap[transportRole] + " Component Latency", LatencyTracker.perfCounterCategoryMap[transportRole] + " End To End Latency");
			LatencyTracker.config = configuration;
			foreach (LatencyTracker.ComponentDefinition componentDefinition in LatencyTracker.componentsArray)
			{
				componentDefinition.Initialize(transportRole);
			}
			if (transportRole == ProcessTransportRole.Hub || transportRole == ProcessTransportRole.Edge || transportRole == ProcessTransportRole.MailboxDelivery)
			{
				LatencyTracker.endToEndPerformanceCounter = PrioritizedLatencyPerformanceCounter.CreateInstance("Total", LatencyTracker.Configuration.PercentileLatencyExpiryInterval, (long)((ulong)LatencyTracker.Configuration.PercentileLatencyInfinitySeconds), LatencyPerformanceCounterType.EndToEnd);
			}
			LatencyTracker.timer = new GuardedTimer(new TimerCallback(LatencyTracker.PeriodicUpdate), null, LatencyTracker.PeriodicUpdateInterval, LatencyTracker.PeriodicUpdateInterval);
			switch (transportRole)
			{
			case ProcessTransportRole.Hub:
				LatencyTracker.ProcessShortName = "HUB";
				return;
			case ProcessTransportRole.Edge:
				LatencyTracker.ProcessShortName = "EDGE";
				return;
			case ProcessTransportRole.FrontEnd:
				LatencyTracker.ProcessShortName = "FE";
				return;
			case ProcessTransportRole.MailboxSubmission:
				LatencyTracker.ProcessShortName = "SUB";
				return;
			case ProcessTransportRole.MailboxDelivery:
				LatencyTracker.ProcessShortName = "DEL";
				return;
			default:
				LatencyTracker.ProcessShortName = "UNK";
				return;
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00043135 File Offset: 0x00041335
		public static void Stop()
		{
			if (LatencyTracker.timer != null)
			{
				LatencyTracker.timer.Dispose(true);
				LatencyTracker.timer = null;
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0004314F File Offset: 0x0004134F
		public static LatencyTracker CreateInstance(LatencyComponent sourceComponent)
		{
			if (sourceComponent == LatencyComponent.Dumpster || sourceComponent == LatencyComponent.Heartbeat)
			{
				return null;
			}
			if (!LatencyTracker.ComponentLatencyTrackingEnabled)
			{
				return null;
			}
			if (LatencyTracker.TreeLatencyTrackingEnabled)
			{
				return new TreeLatencyTracker();
			}
			return new LatencyTracker();
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00043178 File Offset: 0x00041378
		public static LatencyTracker Clone(LatencyTracker tracker)
		{
			if (tracker != null)
			{
				return tracker.Clone();
			}
			return null;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00043185 File Offset: 0x00041385
		public static void UpdateExternalServerLatency(long latencySeconds)
		{
			if (latencySeconds >= 0L)
			{
				LatencyTracker.components[49].UpdatePerformanceCounter(latencySeconds);
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0004319E File Offset: 0x0004139E
		public static void UpdateExternalPartnerServerLatency(long latencySeconds)
		{
			if (latencySeconds >= 0L)
			{
				LatencyTracker.components[50].UpdatePerformanceCounter(latencySeconds);
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000431B7 File Offset: 0x000413B7
		public static void UpdateTotalEndToEndLatency(DeliveryPriority priority, long latency)
		{
			if (LatencyTracker.endToEndPerformanceCounter == null)
			{
				return;
			}
			LatencyTracker.endToEndPerformanceCounter.AddValue(latency, priority);
			LatencyTracker.endToEndPerformanceCounter.Update();
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000431D7 File Offset: 0x000413D7
		public static void TrackPreProcessLatency(LatencyComponent component, LatencyTracker tracker, DateTime startTime)
		{
			if (tracker == null)
			{
				return;
			}
			tracker.TrackPreProcessLatency((ushort)component, startTime);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x000431E5 File Offset: 0x000413E5
		public static void TrackExternalComponentLatency(LatencyComponent component, LatencyTracker tracker, TimeSpan latency)
		{
			if (tracker == null)
			{
				return;
			}
			tracker.TrackExternalLatency((ushort)component, latency);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000431F3 File Offset: 0x000413F3
		public static void BeginTrackLatency(LatencyComponent component, LatencyTracker tracker)
		{
			if (tracker == null)
			{
				return;
			}
			tracker.BeginTrackLatency((ushort)component, LatencyTracker.StopwatchProvider());
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004320C File Offset: 0x0004140C
		public static void BeginTrackLatency(LatencyComponent eventComponent, int agentSequenceNumber, LatencyTracker tracker)
		{
			if (tracker == null)
			{
				return;
			}
			ushort componentId = LatencyTracker.ToComponentId(eventComponent, agentSequenceNumber);
			tracker.BeginTrackLatency(componentId, LatencyTracker.StopwatchProvider());
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00043236 File Offset: 0x00041436
		public static TimeSpan EndTrackLatency(LatencyComponent component, LatencyTracker tracker, bool shouldAggregate)
		{
			return LatencyTracker.EndTrackLatency(component, component, tracker, shouldAggregate);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00043241 File Offset: 0x00041441
		public static TimeSpan EndTrackLatency(LatencyComponent component, LatencyTracker tracker)
		{
			return LatencyTracker.EndTrackLatency(component, component, tracker, false);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004324C File Offset: 0x0004144C
		public static TimeSpan EndTrackLatency(LatencyComponent pendingComponent, LatencyComponent trackingComponent, LatencyTracker tracker)
		{
			if (tracker != null)
			{
				return tracker.EndTrackLatency((ushort)pendingComponent, (ushort)trackingComponent, LatencyTracker.StopwatchProvider(), false);
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004326A File Offset: 0x0004146A
		public static TimeSpan EndTrackLatency(LatencyComponent pendingComponent, LatencyComponent trackingComponent, LatencyTracker tracker, bool shouldAggregate)
		{
			if (tracker != null)
			{
				return tracker.EndTrackLatency((ushort)pendingComponent, (ushort)trackingComponent, LatencyTracker.StopwatchProvider(), shouldAggregate);
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00043288 File Offset: 0x00041488
		public static TimeSpan EndTrackLatency(LatencyComponent eventComponent, int agentSequenceNumber, LatencyTracker tracker)
		{
			if (tracker == null)
			{
				return TimeSpan.Zero;
			}
			ushort num = LatencyTracker.ToComponentId(eventComponent, agentSequenceNumber);
			return tracker.EndTrackLatency(num, num, LatencyTracker.StopwatchProvider(), false);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000432BC File Offset: 0x000414BC
		public static TimeSpan EndAndBeginTrackLatency(LatencyComponent endingComponent, LatencyComponent beginningComponent, LatencyTracker tracker)
		{
			if (tracker == null)
			{
				return TimeSpan.Zero;
			}
			long num = LatencyTracker.StopwatchProvider();
			TimeSpan result = tracker.EndTrackLatency((ushort)endingComponent, (ushort)endingComponent, num, false);
			tracker.BeginTrackLatency((ushort)beginningComponent, num);
			return result;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000432F1 File Offset: 0x000414F1
		public static void Complete(LatencyTracker tracker)
		{
			if (tracker != null)
			{
				tracker.Complete();
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000432FC File Offset: 0x000414FC
		public static void UpdateTotalPerfCounter(TimeSpan latency, DeliveryPriority priority)
		{
			LatencyTracker.components[120].UpdatePerformanceCounter((long)latency.TotalSeconds, priority);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00043318 File Offset: 0x00041518
		public static bool TryGetTotalLatencyRecord(TimeSpan totalLatency, bool ignoreThreshold, out LatencyRecord totalRecord)
		{
			if (!LatencyTracker.ServerLatencyTrackingEnabled || (!ignoreThreshold && !LatencyTracker.ShouldTrackTotal(totalLatency)))
			{
				totalRecord = LatencyRecord.Empty;
				return false;
			}
			totalRecord = new LatencyRecord(120, totalLatency);
			return true;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00043348 File Offset: 0x00041548
		public static bool TryGetComponentLatencyRecord(LatencyComponent componentId, TimeSpan latency, out LatencyRecord record)
		{
			if (!LatencyTracker.ServerLatencyTrackingEnabled || !LatencyTracker.ShouldTrackComponent(latency, (ushort)componentId))
			{
				record = LatencyRecord.Empty;
				return false;
			}
			record = new LatencyRecord((ushort)componentId, latency);
			return true;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00043375 File Offset: 0x00041575
		public static void SetAgentNames(LatencyAgentGroup agentGroup, string[] agentNames)
		{
			LatencyTracker.agentNames[(int)agentGroup] = agentNames;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00043380 File Offset: 0x00041580
		public static string GetShortName(ushort componentId)
		{
			if (componentId < 1000)
			{
				return LatencyTracker.GetComponentDefinition((LatencyComponent)componentId).ShortName;
			}
			ushort component = componentId / 1000;
			ushort num = componentId % 1000;
			LatencyTracker.ComponentDefinition componentDefinition = LatencyTracker.GetComponentDefinition((LatencyComponent)component);
			string text;
			if (num == 999)
			{
				text = "__TOO_MANY__";
			}
			else if (componentDefinition.AgentGroup > (LatencyAgentGroup)LatencyTracker.agentNames.Length)
			{
				text = "UnknownGroup" + componentDefinition.AgentGroup;
			}
			else
			{
				string[] array = LatencyTracker.agentNames[(int)componentDefinition.AgentGroup];
				if (array != null && (int)num < array.Length)
				{
					text = array[(int)num];
				}
				else
				{
					text = string.Concat(new object[]
					{
						"UnknownAgent",
						componentDefinition.AgentGroup,
						"-",
						num
					});
				}
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				componentDefinition.ShortName,
				'-',
				text
			});
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00043484 File Offset: 0x00041684
		public static string GetAgentName(string shortName)
		{
			if (string.IsNullOrEmpty(shortName))
			{
				return string.Empty;
			}
			string text = string.Empty;
			int num = shortName.IndexOf('-');
			if (num >= 0 && num + 1 < shortName.Length)
			{
				text = shortName.Substring(num + 1);
				text = LatencyTracker.DecodeEscapedUnicode(text);
			}
			return text;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000434D0 File Offset: 0x000416D0
		public static LocalizedString GetFullName(string shortName)
		{
			if (string.IsNullOrEmpty(shortName))
			{
				return LocalizedString.Empty;
			}
			string text = LatencyTracker.GetAgentName(shortName);
			if (string.Equals(text, "__TOO_MANY__", StringComparison.OrdinalIgnoreCase))
			{
				text = Strings.TooManyAgents;
			}
			LatencyTracker.ComponentDefinition componentDefinition;
			if (!LatencyTracker.ShortNameToDefinitionMap.TryGetValue(shortName, out componentDefinition))
			{
				return LocalizedString.Empty;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return Strings.EventAgentComponent(componentDefinition.FullName, text);
			}
			return componentDefinition.FullName;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00043540 File Offset: 0x00041740
		public static LatencyComponent GetDeliveryQueueLatencyComponent(DeliveryType deliveryType)
		{
			LatencyComponent result;
			if (deliveryType == DeliveryType.SmtpDeliveryToMailbox)
			{
				result = LatencyComponent.DeliveryQueueMailbox;
			}
			else if (TransportDeliveryTypes.internalDeliveryTypes.Contains(deliveryType))
			{
				result = LatencyComponent.DeliveryQueueInternal;
			}
			else
			{
				if (!TransportDeliveryTypes.externalDeliveryTypes.Contains(deliveryType))
				{
					throw new InvalidOperationException("cannot determine LatencyComponent from DeliveryType: " + deliveryType.ToString());
				}
				result = LatencyComponent.DeliveryQueueExternal;
			}
			return result;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00043597 File Offset: 0x00041797
		public virtual IEnumerable<LatencyRecord> GetCompletedRecords()
		{
			return new List<LatencyRecord>(this.latencies);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000435A4 File Offset: 0x000417A4
		public virtual IEnumerable<PendingLatencyRecord> GetPendingRecords()
		{
			return new List<PendingLatencyRecord>(this.pendingComponents);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000435B4 File Offset: 0x000417B4
		public virtual void AppendLatencyString(StringBuilder builder, bool useTreeFormat, bool haveTotal, bool enableHeaderFolding)
		{
			if (this.HasCompletedComponents)
			{
				int lastFoldingPoint = builder.Length;
				if (haveTotal)
				{
					builder.Append('|');
					lastFoldingPoint = LatencyFormatter.AddFolding(builder, lastFoldingPoint, enableHeaderFolding);
				}
				this.AppendComponentLatencyString(builder, lastFoldingPoint, enableHeaderFolding, useTreeFormat);
			}
			if (this.HasPendingComponents)
			{
				if (haveTotal || this.HasCompletedComponents)
				{
					builder.Append(';');
				}
				this.AppendPendingComponentLatencyString(builder, useTreeFormat);
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00043618 File Offset: 0x00041818
		protected static bool ShouldTrackComponent(TimeSpan latency, ushort componentId)
		{
			if (!LatencyTracker.ComponentLatencyTrackingEnabled)
			{
				return false;
			}
			if (componentId == 118)
			{
				return LatencyTracker.MinInterestingUnknownInterval == TimeSpan.Zero || LatencyTracker.MinInterestingUnknownInterval < latency;
			}
			return LatencyTracker.HighPrecisionThresholdInterval == TimeSpan.Zero || LatencyTracker.HighPrecisionThresholdInterval < latency;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00043670 File Offset: 0x00041870
		protected static void UpdatePerfCounter(ushort componentId, long latencySeconds)
		{
			if (componentId < 1000)
			{
				LatencyTracker.components[(int)componentId].UpdatePerformanceCounter(latencySeconds);
				return;
			}
			string agentName = LatencyTracker.GetAgentName(LatencyTracker.GetShortName(componentId));
			ILatencyPerformanceCounter latencyPerformanceCounter = LatencyTracker.agentPerformanceCounters.Get(agentName);
			if (latencyPerformanceCounter != null)
			{
				latencyPerformanceCounter.AddValue(latencySeconds);
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000436B9 File Offset: 0x000418B9
		protected virtual void Complete()
		{
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000436BC File Offset: 0x000418BC
		protected virtual LatencyTracker Clone()
		{
			LatencyTracker latencyTracker = new LatencyTracker();
			lock (this.latencies)
			{
				latencyTracker.latencies = new List<LatencyRecord>(this.latencies);
				latencyTracker.pendingComponents = new List<PendingLatencyRecord>(this.pendingComponents);
			}
			return latencyTracker;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00043720 File Offset: 0x00041920
		protected virtual void BeginTrackLatency(ushort componentId, long startTime)
		{
			lock (this.latencies)
			{
				this.pendingComponents.Add(new PendingLatencyRecord(componentId, startTime));
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004376C File Offset: 0x0004196C
		protected virtual TimeSpan EndTrackLatency(ushort pendingComponentId, ushort trackingComponentId, long endTime, bool shouldAggregate)
		{
			lock (this.latencies)
			{
				int num = this.FindPendingRecord(pendingComponentId);
				if (num >= 0)
				{
					TimeSpan timeSpan = LatencyTracker.TimeSpanFromTicks(this.pendingComponents[num].StartTime, endTime);
					LatencyTracker.UpdatePerfCounter(trackingComponentId, (long)(timeSpan.TotalSeconds + 0.5));
					this.AddLatency(trackingComponentId, timeSpan, shouldAggregate);
					this.pendingComponents.RemoveAt(num);
					return timeSpan;
				}
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0004380C File Offset: 0x00041A0C
		protected virtual void TrackPreProcessLatency(ushort componentId, DateTime startTime)
		{
			TimeSpan latencyTimeSpan = LatencyTracker.TimeProvider() - startTime;
			LatencyTracker.UpdatePerfCounter(componentId, (long)latencyTimeSpan.TotalSeconds);
			this.AddLatency(componentId, latencyTimeSpan, false);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00043841 File Offset: 0x00041A41
		protected virtual void TrackExternalLatency(ushort componentId, TimeSpan latency)
		{
			LatencyTracker.UpdatePerfCounter(componentId, (long)latency.TotalSeconds);
			this.AddLatency(componentId, latency, false);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00043891 File Offset: 0x00041A91
		private static string DecodeEscapedUnicode(string input)
		{
			return LatencyTracker.EscapedUnicodeRegex.Replace(input, (Match m) => ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000438BB File Offset: 0x00041ABB
		private static bool ShouldTrackTotal(TimeSpan totalLatency)
		{
			return LatencyTracker.ServerLatencyTrackingEnabled && (LatencyTracker.ServerLatencyThreshold == TimeSpan.Zero || !(totalLatency < LatencyTracker.ServerLatencyThreshold));
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000438EC File Offset: 0x00041AEC
		private static ushort ToComponentId(LatencyComponent eventComponent, int agentSequenceNumber)
		{
			if (eventComponent == LatencyComponent.None || eventComponent > LatencyComponent.MaxMExEventComponent)
			{
				string message = string.Format("eventCoponentId {0} does not fit the event range", (ushort)eventComponent);
				throw new ArgumentOutOfRangeException("eventComponent", message);
			}
			ushort num = (ushort)((agentSequenceNumber > 999) ? 999 : agentSequenceNumber);
			return (ushort)(eventComponent * (LatencyComponent)1000 + num);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004393C File Offset: 0x00041B3C
		private static LatencyTracker.ComponentDefinition GetComponentDefinition(LatencyComponent component)
		{
			LatencyTracker.ComponentDefinition result;
			if (!LatencyTracker.components.TryGetValue((int)component, out result))
			{
				throw new InvalidOperationException(string.Format("LatencyComponent value {0} could not be found Did you forget to update one of the lists?", component));
			}
			return result;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00043978 File Offset: 0x00041B78
		private static void PeriodicUpdate(object state)
		{
			foreach (LatencyTracker.ComponentDefinition componentDefinition in LatencyTracker.componentsArray)
			{
				componentDefinition.UpdatePerformanceCounter();
			}
			LatencyTracker.agentPerformanceCounters.ForEach(delegate(string name, ILatencyPerformanceCounter perfCounter)
			{
				LatencyTracker.UpdateAgentPerfCounter(name, perfCounter);
			});
			if (LatencyTracker.endToEndPerformanceCounter != null)
			{
				LatencyTracker.endToEndPerformanceCounter.Update();
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000439DB File Offset: 0x00041BDB
		private static void UpdateAgentPerfCounter(string agentName, ILatencyPerformanceCounter perfCounter)
		{
			if (perfCounter != null)
			{
				perfCounter.Update();
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000439E8 File Offset: 0x00041BE8
		private int FindPendingRecord(ushort componentId)
		{
			int num = this.pendingComponents.Count;
			while (--num >= 0 && this.pendingComponents[num].ComponentId != componentId)
			{
			}
			return num;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00043A24 File Offset: 0x00041C24
		private void AddLatency(ushort componentId, TimeSpan latencyTimeSpan, bool shouldAggregate)
		{
			if (!LatencyTracker.ShouldTrackComponent(latencyTimeSpan, componentId))
			{
				if (LatencyTracker.ComponentLatencyTrackingEnabled)
				{
					this.AggregatedUnderThresholdTicks += latencyTimeSpan.Ticks;
				}
				return;
			}
			TimeSpan timeSpan = latencyTimeSpan;
			lock (this.latencies)
			{
				if (shouldAggregate && this.latencies.Count > 0 && this.latencies[this.latencies.Count - 1].ComponentId == componentId)
				{
					this.latencies[this.latencies.Count - 1] = new LatencyRecord(componentId, this.latencies[this.latencies.Count - 1].Latency + timeSpan);
				}
				else if (this.latencies.Count == 1000)
				{
					TimeSpan latency = this.latencies[999].Latency;
					this.latencies[999] = new LatencyRecord(122, timeSpan + latency);
				}
				else
				{
					this.latencies.Add(new LatencyRecord(componentId, timeSpan));
				}
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00043B60 File Offset: 0x00041D60
		private void AppendPendingComponentLatencyString(StringBuilder builder, bool useTreeFormat)
		{
			if (useTreeFormat)
			{
				return;
			}
			bool flag = true;
			foreach (PendingLatencyRecord pendingLatencyRecord in this.GetPendingRecords())
			{
				if (!flag)
				{
					builder.Append('|');
				}
				builder.Append(LatencyTracker.GetShortName(pendingLatencyRecord.ComponentId));
				flag = false;
			}
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00043BD0 File Offset: 0x00041DD0
		private void AppendComponentLatencyString(StringBuilder builder, int lastFoldingPoint, bool enableHeaderFolding, bool useTreeFormat)
		{
			if (useTreeFormat)
			{
				return;
			}
			bool flag = false;
			foreach (LatencyRecord record in this.GetCompletedRecords())
			{
				if (flag)
				{
					builder.Append('|');
					lastFoldingPoint = LatencyFormatter.AddFolding(builder, lastFoldingPoint, enableHeaderFolding);
				}
				LatencyFormatter.AppendLatencyRecord(builder, record, null);
				flag = true;
			}
		}

		// Token: 0x040008F6 RID: 2294
		public const int MaxRecordCount = 1000;

		// Token: 0x040008F7 RID: 2295
		public const ushort MaxAgentCount = 1000;

		// Token: 0x040008F8 RID: 2296
		public const string TotalShortName = "TOTAL";

		// Token: 0x040008F9 RID: 2297
		public const string TooManyAgentsShortName = "__TOO_MANY__";

		// Token: 0x040008FA RID: 2298
		public static Func<DateTime> TimeProvider = new Func<DateTime>(LatencyTracker.DefaultTimeProvider);

		// Token: 0x040008FB RID: 2299
		public static Func<long> StopwatchProvider = new Func<long>(LatencyTracker.DefaultStopWatchProvider);

		// Token: 0x040008FC RID: 2300
		private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
		{
			{
				ProcessTransportRole.Edge,
				"MSExchangeTransport"
			},
			{
				ProcessTransportRole.Hub,
				"MSExchangeTransport"
			},
			{
				ProcessTransportRole.FrontEnd,
				"MSExchangeFrontEndTransport"
			},
			{
				ProcessTransportRole.MailboxDelivery,
				"MSExchange Delivery"
			},
			{
				ProcessTransportRole.MailboxSubmission,
				"MSExchange Submission"
			}
		};

		// Token: 0x040008FD RID: 2301
		private static readonly TimeSpan PeriodicUpdateInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x040008FE RID: 2302
		private static readonly Regex EscapedUnicodeRegex = new Regex("\\\\u(?<Value>[a-fA-F0-9]{4})");

		// Token: 0x040008FF RID: 2303
		private static readonly LatencyTracker.ComponentDefinition[] componentsArray = new LatencyTracker.ComponentDefinition[]
		{
			new LatencyTracker.ComponentDefinition(LatencyComponent.None, "NONE", Strings.LatencyComponentNone, LatencyCounterType.None, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerOnResolvedMessage, "CATRS", Strings.LatencyComponentCategorizerOnResolvedMessage, LatencyAgentGroup.Categorizer, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerOnRoutedMessage, "CATRT", Strings.LatencyComponentCategorizerOnRoutedMessage, LatencyAgentGroup.Categorizer, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerOnSubmittedMessage, "CATSM", Strings.LatencyComponentCategorizerOnSubmittedMessage, LatencyAgentGroup.Categorizer, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerOnCategorizedMessage, "CATCM", Strings.LatencyComponentCategorizerOnCategorizedMessage, LatencyAgentGroup.Categorizer, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryAgentOnOpenConnection, "DAOC", Strings.LatencyComponentDeliveryAgentOnOpenConnection, LatencyAgentGroup.Delivery, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryAgentOnDeliverMailItem, "DADM", Strings.LatencyComponentDeliveryAgentOnDeliverMailItem, LatencyAgentGroup.Delivery, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveOnDataCommand, "SMRDC", Strings.LatencyComponentSmtpReceiveOnDataCommand, LatencyAgentGroup.SmtpReceive, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveOnEndOfData, "SMRED", Strings.LatencyComponentSmtpReceiveOnEndOfData, LatencyAgentGroup.SmtpReceive, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveOnEndOfHeaders, "SMREH", Strings.LatencyComponentSmtpReceiveOnEndOfHeaders, LatencyAgentGroup.SmtpReceive, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveOnRcptCommand, "SMRRC", Strings.LatencyComponentSmtpReceiveOnRcptCommand, LatencyAgentGroup.SmtpReceive, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveOnRcpt2Command, "SMRR2C", Strings.LatencyComponentSmtpReceiveOnRcpt2Command, LatencyAgentGroup.SmtpReceive, LatencyCounterType.None, ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveOnProxyInboundMessage, "SMRPI", Strings.LatencyComponentSmtpReceiveOnProxyInboundMessage, LatencyAgentGroup.SmtpReceive, LatencyCounterType.None, ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverOnCreatedMessage, "SDDCM", Strings.LatencyComponentStoreDriverOnCreatedMessage, LatencyAgentGroup.StoreDriver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverOnInitializedMessage, "SDDIM", Strings.LatencyComponentStoreDriverOnInitializedMessage, LatencyAgentGroup.StoreDriver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverOnPromotedMessage, "SDDPM", Strings.LatencyComponentStoreDriverOnPromotedMessage, LatencyAgentGroup.StoreDriver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverOnDeliveredMessage, "SDDDLV", Strings.LatencyComponentStoreDriverOnDeliveredMessage, LatencyAgentGroup.StoreDriver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverOnDemotedMessage, "SDSDM", Strings.LatencyComponentStoreDriverOnDemotedMessage, LatencyAgentGroup.StoreDriver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage, "MTSSDSDM", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage, LatencyAgentGroup.MailboxTransportSubmissionStoreDriverSubmission, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverOnCompletedMessage, "SDDCPM", Strings.LatencyComponentStoreDriverOnCompletedMessage, LatencyAgentGroup.StoreDriver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Agent, "AGNT", Strings.LatencyComponentAgent, LatencyAgentGroup.UnassignedAgentGroup, LatencyCounterType.None, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Categorizer, "CAT", Strings.LatencyComponentCategorizer, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerBifurcation, "CBIF", Strings.LatencyComponentCategorizerBifurcation, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerContentConversion, "CCC", Strings.LatencyComponentCategorizerContentConversion, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerFinal, "CFIN", Strings.LatencyComponentCategorizerFinal, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerLocking, "CL", Strings.LatencyComponentCategorizerLocking, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerResolver, "CRSL", Strings.LatencyComponentCategorizerResolver, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.CategorizerRouting, "CRT", Strings.LatencyComponentCategorizerRouting, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ContentAggregation, "CA", Strings.LatencyComponentContentAggregation, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ContentAggregationMailItemCommit, "CAMIC", Strings.LatencyComponentContentAggregationMailItemCommit, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Deferral, "DFR", Strings.LatencyComponentDeferral, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Delivery, "D", Strings.LatencyComponentDelivery, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryAgent, "DAD", Strings.LatencyComponentDeliveryAgent, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueInternal, "QDI", Strings.LatencyComponentDeliveryQueueInternal, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueExternal, "QDE", Strings.LatencyComponentDeliveryQueueExternal, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailbox, "QDM", Strings.LatencyComponentDeliveryQueueMailbox, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxDeliverAgentTransientFailure, "QDMDATF", Strings.LatencyComponentDeliveryQueueMailboxDeliverAgentTransientFailure, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded, "QDMDMDTLE", Strings.LatencyComponentDeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxInsufficientResources, "QDMIR", Strings.LatencyComponentDeliveryQueueMailboxInsufficientResources, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxMailboxDatabaseOffline, "QDMMDO", Strings.LatencyComponentDeliveryQueueMailboxMailboxDatabaseOffline, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxMailboxServerOffline, "QDMMSO", Strings.LatencyComponentDeliveryQueueMailboxMailboxServerOffline, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxMapiExceptionLockViolation, "QDMMELV", Strings.LatencyComponentDeliveryQueueMailboxMapiExceptionLockViolation, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxMapiExceptionTimeout, "QDMMET", Strings.LatencyComponentDeliveryQueueMailboxMapiExceptionTimeout, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded, "QDMMCMSLE", Strings.LatencyComponentDeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueMailboxRecipientThreadLimitExceeded, "QDMRTLE", Strings.LatencyComponentDeliveryQueueMailboxRecipientThreadLimitExceeded, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DeliveryQueueLocking, "QDL", Strings.LatencyComponentDeliveryQueueLocking, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.DsnGenerator, "DSN", Strings.LatencyComponentDsnGenerator, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Dumpster, "DMP", Strings.LatencyComponentDumpster, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ExternalServers, "ES", Strings.LatencyComponentExternalServers, LatencyCounterType.LongRangePercentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.SkipEndToEnd),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ExternalPartnerServers, "EPS", Strings.LatencyComponentExternalPartnerServers, LatencyCounterType.LongRangePercentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.SkipEndToEnd),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Heartbeat, "HB", Strings.LatencyComponentHeartbeat, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxMove, "MM", Strings.LatencyComponentMailboxMove, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxRules, "MR", Strings.LatencyComponentMailboxRules, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionService, "MTSS", Strings.LatencyComponentMailboxTransportSubmissionService, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SubmissionAssistant, "SA", Strings.LatencyComponentSubmissionAssistant, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SubmissionAssistantThrottling, "SAT", Strings.LatencyComponentSubmissionAssistantThrottling, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmission, "MTSSD", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmission, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionAD, "MTSSDA", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionAD, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionContentConversion, "MTSSDC", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionContentConversion, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionHubSelector, "MTSSDH", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionHubSelector, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap, "MTSSDPL", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtp, "MTSSDM", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtp, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtpOut, "MTSSDMO", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtpOut, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession, "MTSSDSOS", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession, "MTSSDSDS", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession, LatencyCounterType.Percentile, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreStats, "MTSSDSS", Strings.LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreStats, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailSubmissionService, "MSS", Strings.LatencyComponentMailSubmissionService, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailSubmissionServiceFailedAttempt, "MSSFA", Strings.LatencyComponentMailSubmissionServiceFailedAttempt, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailSubmissionServiceNotify, "MSSN", Strings.LatencyComponentMailSubmissionServiceNotify, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailSubmissionServiceNotifyRetrySchedule, "MSSNRS", Strings.LatencyComponentMailSubmissionServiceNotifyRetrySchedule, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailSubmissionServiceShadowResubmitDecision, "MSSSRD", Strings.LatencyComponentMailSubmissionServiceShadowResubmitDecision, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MailSubmissionServiceThrottling, "MSST", Strings.LatencyComponentMailSubmissionServiceThrottling, LatencyCounterType.None, ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.MexRuntimeThreadpoolQueue, "MEXRTPQ", Strings.LatencyComponentMexRuntimeThreadpoolQueue, LatencyCounterType.None, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.NonSmtpGateway, "NSGW", Strings.LatencyComponentNonSmtpGateway, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.OriginalMailDsn, "OMDSN", Strings.LatencyComponentOriginalMailDsn, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Pickup, "PCK", Strings.LatencyComponentPickup, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.PoisonQueue, "QP", Strings.LatencyComponentPoisonQueue, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ProcessingScheduler, "PSC", Strings.LatencyComponentProcessingScheduler, LatencyCounterType.PrioritizedPercentile, ProcessTransportRoleFlags.Hub, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ProcessingSchedulerScoped, "PSCSQ", Strings.LatencyComponentProcessingSchedulerScoped, LatencyCounterType.PrioritizedPercentile, ProcessTransportRoleFlags.Hub, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.QuarantineReleaseOrReport, "QUAR", Strings.LatencyComponentQuarantineReleaseOrReport, LatencyCounterType.None, ProcessTransportRoleFlags.FrontEnd, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Replay, "RPL", Strings.LatencyComponentReplay, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireB2BRac, "RMABR", Strings.LatencyComponentRmsAcquireB2BRac, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireB2BLicense, "RMABL", Strings.LatencyComponentRmsAcquireB2BLicense, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireCertificationMexData, "RMACM", Strings.LatencyComponentRmsAcquireCertificationMexData, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireClc, "RMAC", Strings.LatencyComponentRmsAcquireClc, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireLicense, "RMAL", Strings.LatencyComponentRmsAcquireLicense, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireServerLicensingMexData, "RMASLM", Strings.LatencyComponentRmsAcquireServerLicensingMexData, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquirePrelicense, "RMAPL", Strings.LatencyComponentRmsAcquirePreLicense, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireServerBoxRac, "RMASR", Strings.LatencyComponentRmsAcquireServerBoxRac, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireTemplateInfo, "RMATI", Strings.LatencyComponentRmsAcquireTemplateInfo, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsAcquireTemplates, "RMAT", Strings.LatencyComponentRmsAcquireTemplates, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsFindServiceLocation, "RMFSL", Strings.LatencyComponentRmsFindServiceLocation, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.RmsRequestDelegationToken, "RMRDT", Strings.LatencyComponentRmsRequestDelegationToken, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ServiceRestart, "RST", Strings.LatencyComponentServiceRestart, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.ShadowQueue, "QSH", Strings.LatencyComponentShadowQueue, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.SkipEndToEnd),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceive, "SMR", Strings.LatencyComponentSmtpReceive, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveCommit, "SMRC", Strings.LatencyComponentSmtpReceiveCommit, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveCommitLocal, "SMRCL", Strings.LatencyComponentSmtpReceiveCommitLocal, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveCommitRemote, "SMRCR", Strings.LatencyComponentSmtpReceiveCommitRemote, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveDataInternal, "SMRDI", Strings.LatencyComponentSmtpReceiveDataInternal, LatencyCounterType.Percentile, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpReceiveDataExternal, "SMRDE", Strings.LatencyComponentSmtpReceiveDataExternal, LatencyCounterType.Percentile, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpSend, "SMS", Strings.LatencyComponentSmtpSend, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpSendConnect, "SMSC", Strings.LatencyComponentSmtpSendConnect, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SmtpSendMailboxDelivery, "SMSMBXD", Strings.LatencyComponentSmtpSendMailboxDelivery, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverDelivery, "SDD", Strings.LatencyComponentStoreDriverDelivery, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverDeliveryAD, "SDDAD", Strings.LatencyComponentStoreDriverDeliveryAD, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverDeliveryContentConversion, "SDDCC", Strings.LatencyComponentStoreDriverDeliveryContentConversion, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverDeliveryMessageConcurrency, "SDDMC", Strings.LatencyComponentStoreDriverDeliveryMessageConcurrency, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverDeliveryRpc, "SDDR", Strings.LatencyComponentStoreDriverDeliveryRpc, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverDeliveryStore, "SDDS", Strings.LatencyComponentStoreDriverDeliveryStore, LatencyCounterType.None, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxDelivery, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverSubmissionAD, "SDSAD", Strings.LatencyComponentStoreDriverSubmissionAD, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverSubmissionRpc, "SDSR", Strings.LatencyComponentStoreDriverSubmissionRpc, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverSubmissionStore, "SDSS", Strings.LatencyComponentStoreDriverSubmissionStore, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.StoreDriverSubmit, "SDS", Strings.LatencyComponentStoreDriverSubmit, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.MailboxSubmission, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.SubmissionQueue, "QS", Strings.LatencyComponentSubmissionQueue, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.UnderThreshold, "UTH", Strings.LatencyComponentUnderThreshold, LatencyCounterType.None, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Unknown, "UNK", Strings.LatencyComponentUnknown, LatencyCounterType.None, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.UnreachableQueue, "QU", Strings.LatencyComponentUnreachableQueue, LatencyCounterType.Percentile, ProcessTransportRoleFlags.Hub | ProcessTransportRoleFlags.Edge, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Total, "TOTAL", Strings.LatencyComponentTotal, LatencyCounterType.PrioritizedPercentile, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.Process, "P", Strings.LatencyComponentProcess, LatencyCounterType.PrioritizedPercentile, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal),
			new LatencyTracker.ComponentDefinition(LatencyComponent.TooManyComponents, "MANY", Strings.LatencyComponentTooManyComponents, LatencyCounterType.None, ProcessTransportRoleFlags.All, LatencyComponentAction.Normal)
		};

		// Token: 0x04000900 RID: 2304
		private static readonly Dictionary<int, LatencyTracker.ComponentDefinition> components = LatencyTracker.componentsArray.ToDictionary((LatencyTracker.ComponentDefinition cd) => (int)cd.ComponentId);

		// Token: 0x04000901 RID: 2305
		private static readonly Dictionary<string, LatencyTracker.ComponentDefinition> ShortNameToDefinitionMap = LatencyTracker.componentsArray.ToDictionary((LatencyTracker.ComponentDefinition cd) => cd.ShortName);

		// Token: 0x04000902 RID: 2306
		private static readonly AutoReadThroughCache<string, ILatencyPerformanceCounter> agentPerformanceCounters = new AutoReadThroughCache<string, ILatencyPerformanceCounter>((string name) => LatencyPerformanceCounter.CreateInstance(name, LatencyTracker.Configuration.PercentileLatencyExpiryInterval, (long)((ulong)LatencyTracker.Configuration.PercentileLatencyInfinitySeconds)));

		// Token: 0x04000903 RID: 2307
		private static ILatencyPerformanceCounter endToEndPerformanceCounter;

		// Token: 0x04000904 RID: 2308
		private static TransportAppConfig.LatencyTrackerConfig config;

		// Token: 0x04000905 RID: 2309
		private static string[][] agentNames = new string[6][];

		// Token: 0x04000906 RID: 2310
		private static GuardedTimer timer;

		// Token: 0x04000907 RID: 2311
		private List<LatencyRecord> latencies;

		// Token: 0x04000908 RID: 2312
		private List<PendingLatencyRecord> pendingComponents;

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x060010D4 RID: 4308
		public delegate ILatencyPerformanceCounter CreateLatencyPerformanceCounterDelegate(string instanceName);

		// Token: 0x02000182 RID: 386
		internal class ComponentDefinition
		{
			// Token: 0x060010D7 RID: 4311 RVA: 0x0004491E File Offset: 0x00042B1E
			public ComponentDefinition(LatencyComponent componentId, string shortName, LocalizedString fullName, LatencyCounterType latencyCounterType, ProcessTransportRoleFlags transportRoles, LatencyComponentAction componentAction = LatencyComponentAction.Normal) : this(componentId, shortName, fullName, LatencyAgentGroup.UnassignedAgentGroup, latencyCounterType, transportRoles, componentAction)
			{
			}

			// Token: 0x060010D8 RID: 4312 RVA: 0x00044934 File Offset: 0x00042B34
			public ComponentDefinition(LatencyComponent componentId, string shortName, LocalizedString fullName, LatencyAgentGroup agentGroup, LatencyCounterType latencyCounterType, ProcessTransportRoleFlags transportRoles, LatencyComponentAction componentAction = LatencyComponentAction.Normal)
			{
				this.ComponentId = componentId;
				this.AgentGroup = agentGroup;
				this.ShortName = shortName;
				this.FullName = fullName;
				this.latencyCounterType = latencyCounterType;
				this.transportRoles = transportRoles;
				this.latencyComponentAction = componentAction;
			}

			// Token: 0x060010D9 RID: 4313 RVA: 0x00044971 File Offset: 0x00042B71
			public static void SetLatencyPerformanceCounterTestStubCreator(LatencyTracker.CreateLatencyPerformanceCounterDelegate createDelegate)
			{
				LatencyTracker.ComponentDefinition.latencyPerformanceCounterTestStubCreator = createDelegate;
			}

			// Token: 0x060010DA RID: 4314 RVA: 0x0004497C File Offset: 0x00042B7C
			public static ProcessTransportRoleFlags MapTransportRoleToFlag(ProcessTransportRole transportRole)
			{
				switch (transportRole)
				{
				case ProcessTransportRole.Hub:
					return ProcessTransportRoleFlags.Hub;
				case ProcessTransportRole.Edge:
					return ProcessTransportRoleFlags.Edge;
				case ProcessTransportRole.FrontEnd:
					return ProcessTransportRoleFlags.FrontEnd;
				case ProcessTransportRole.MailboxSubmission:
					return ProcessTransportRoleFlags.MailboxSubmission;
				case ProcessTransportRole.MailboxDelivery:
					return ProcessTransportRoleFlags.MailboxDelivery;
				default:
					return ProcessTransportRoleFlags.None;
				}
			}

			// Token: 0x060010DB RID: 4315 RVA: 0x000449B4 File Offset: 0x00042BB4
			public void Initialize(ProcessTransportRole transportRole)
			{
				if (LatencyTracker.ComponentDefinition.latencyPerformanceCounterTestStubCreator != null)
				{
					this.perfCounter = LatencyTracker.ComponentDefinition.latencyPerformanceCounterTestStubCreator(this.FullName.ToString());
					return;
				}
				ProcessTransportRoleFlags processTransportRoleFlags = LatencyTracker.ComponentDefinition.MapTransportRoleToFlag(transportRole);
				if ((processTransportRoleFlags & this.transportRoles) != processTransportRoleFlags)
				{
					return;
				}
				switch (this.latencyCounterType)
				{
				case LatencyCounterType.None:
					break;
				case LatencyCounterType.Percentile:
					this.perfCounter = LatencyPerformanceCounter.CreateInstance(this.FullName.ToString(), LatencyTracker.Configuration.PercentileLatencyExpiryInterval, (long)((ulong)LatencyTracker.Configuration.PercentileLatencyInfinitySeconds));
					return;
				case LatencyCounterType.PrioritizedPercentile:
					if (transportRole == ProcessTransportRole.Hub || transportRole == ProcessTransportRole.Edge || transportRole == ProcessTransportRole.MailboxDelivery)
					{
						this.perfCounter = PrioritizedLatencyPerformanceCounter.CreateInstance(this.FullName.ToString(), LatencyTracker.Configuration.PercentileLatencyExpiryInterval, (long)((ulong)LatencyTracker.Configuration.PercentileLatencyInfinitySeconds));
						return;
					}
					this.perfCounter = LatencyPerformanceCounter.CreateInstance(this.FullName.ToString(), LatencyTracker.Configuration.PercentileLatencyExpiryInterval, (long)((ulong)LatencyTracker.Configuration.PercentileLatencyInfinitySeconds));
					return;
				case LatencyCounterType.LongRangePercentile:
					this.perfCounter = LatencyPerformanceCounter.CreateInstance(this.FullName.ToString(), LatencyTracker.Configuration.PercentileLatencyExpiryInterval, (long)((ulong)LatencyTracker.Configuration.PercentileLatencyInfinitySeconds), true);
					break;
				default:
					return;
				}
			}

			// Token: 0x060010DC RID: 4316 RVA: 0x00044B00 File Offset: 0x00042D00
			public void UpdatePerformanceCounter(long latencySeconds)
			{
				if (this.perfCounter != null)
				{
					if (this.latencyComponentAction == LatencyComponentAction.SkipEndToEnd && this.perfCounter.CounterType == LatencyPerformanceCounterType.EndToEnd)
					{
						return;
					}
					if (this.latencyComponentAction == LatencyComponentAction.ResetEndToEnd && this.perfCounter.CounterType == LatencyPerformanceCounterType.EndToEnd)
					{
						this.perfCounter.Reset();
						return;
					}
					this.perfCounter.AddValue(latencySeconds);
				}
			}

			// Token: 0x060010DD RID: 4317 RVA: 0x00044B5C File Offset: 0x00042D5C
			public void UpdatePerformanceCounter(long latencySeconds, DeliveryPriority priority)
			{
				if (this.perfCounter != null)
				{
					if (this.latencyComponentAction == LatencyComponentAction.SkipEndToEnd && this.perfCounter.CounterType == LatencyPerformanceCounterType.EndToEnd)
					{
						return;
					}
					if (this.latencyComponentAction == LatencyComponentAction.ResetEndToEnd && this.perfCounter.CounterType == LatencyPerformanceCounterType.EndToEnd)
					{
						this.perfCounter.Reset();
						return;
					}
					this.perfCounter.AddValue(latencySeconds, priority);
				}
			}

			// Token: 0x060010DE RID: 4318 RVA: 0x00044BB9 File Offset: 0x00042DB9
			public void UpdatePerformanceCounter()
			{
				if (this.perfCounter != null)
				{
					this.perfCounter.Update();
				}
			}

			// Token: 0x04000910 RID: 2320
			public readonly LatencyComponent ComponentId;

			// Token: 0x04000911 RID: 2321
			public readonly string ShortName;

			// Token: 0x04000912 RID: 2322
			public readonly LocalizedString FullName;

			// Token: 0x04000913 RID: 2323
			public readonly LatencyAgentGroup AgentGroup;

			// Token: 0x04000914 RID: 2324
			private static LatencyTracker.CreateLatencyPerformanceCounterDelegate latencyPerformanceCounterTestStubCreator;

			// Token: 0x04000915 RID: 2325
			private readonly ProcessTransportRoleFlags transportRoles;

			// Token: 0x04000916 RID: 2326
			private readonly LatencyCounterType latencyCounterType;

			// Token: 0x04000917 RID: 2327
			private ILatencyPerformanceCounter perfCounter;

			// Token: 0x04000918 RID: 2328
			private LatencyComponentAction latencyComponentAction;
		}
	}
}

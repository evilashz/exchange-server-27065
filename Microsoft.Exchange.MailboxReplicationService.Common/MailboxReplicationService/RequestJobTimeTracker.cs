using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	public sealed class RequestJobTimeTracker : XMLSerializableBase
	{
		// Token: 0x06001B8E RID: 7054 RVA: 0x0003A79C File Offset: 0x0003899C
		public RequestJobTimeTracker()
		{
			this.durations = new Dictionary<RequestState, RequestJobDurationData>();
			this.timestamps = new Dictionary<RequestJobTimestamp, DateTime>();
			this.curState = RequestState.None;
			this.stateChangeTimestamp = (DateTime)ExDateTime.UtcNow;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0003A7D1 File Offset: 0x000389D1
		private RequestJobTimeTracker(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0003A7D9 File Offset: 0x000389D9
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0003A7E1 File Offset: 0x000389E1
		[XmlIgnore]
		public RequestState CurrentState
		{
			get
			{
				return this.curState;
			}
			set
			{
				this.SetState(value);
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0003A7EC File Offset: 0x000389EC
		internal void SetState(RequestState value)
		{
			if (this.curState == value)
			{
				return;
			}
			DateTime utcNow = TimeProvider.UtcNow;
			TimeSpan duration = utcNow.Subtract(this.stateChangeTimestamp);
			this.RefreshDurations();
			if (RequestJobTimeTracker.SupportDurationTracking(this.curState))
			{
				RequestJobDurationData requestJobDurationData;
				if (!this.durations.TryGetValue(this.curState, out requestJobDurationData))
				{
					requestJobDurationData = new RequestJobDurationData(this.curState);
				}
				requestJobDurationData.AddTime(duration);
				this.durations[this.curState] = requestJobDurationData;
			}
			this.stateChangeTimestamp = utcNow;
			this.curState = value;
			if (RequestJobTimeTracker.SupportDurationTracking(this.curState))
			{
				this.UpdateActiveCounts(this.curState, 1L);
				this.IncRateCounts(this.curState);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0003A899 File Offset: 0x00038A99
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0003A8A1 File Offset: 0x00038AA1
		[XmlElement("CurrentState")]
		public int CurrentStateInt
		{
			get
			{
				return (int)this.curState;
			}
			set
			{
				this.curState = (RequestState)value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0003A8AA File Offset: 0x00038AAA
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x0003A8B7 File Offset: 0x00038AB7
		[XmlElement("StateChangeTimestamp")]
		public long StateChangeTimestampInt
		{
			get
			{
				return this.stateChangeTimestamp.Ticks;
			}
			set
			{
				this.stateChangeTimestamp = new DateTime(value);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0003A8C8 File Offset: 0x00038AC8
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x0003A938 File Offset: 0x00038B38
		[XmlArrayItem("D")]
		[XmlArray("Durations")]
		public RequestJobDurationData[] Durations
		{
			get
			{
				RequestJobDurationData[] array = new RequestJobDurationData[this.durations.Count];
				int num = 0;
				foreach (KeyValuePair<RequestState, RequestJobDurationData> keyValuePair in this.durations)
				{
					array[num++] = keyValuePair.Value;
				}
				return array;
			}
			set
			{
				this.durations.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						RequestJobDurationData requestJobDurationData = value[i];
						this.durations[requestJobDurationData.State] = requestJobDurationData;
					}
				}
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0003A97C File Offset: 0x00038B7C
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x0003A9F8 File Offset: 0x00038BF8
		[XmlArrayItem("TS")]
		[XmlArray("Timestamps")]
		public RequestJobTimestampData[] Timestamps
		{
			get
			{
				RequestJobTimestampData[] array = new RequestJobTimestampData[this.timestamps.Count];
				int num = 0;
				foreach (KeyValuePair<RequestJobTimestamp, DateTime> keyValuePair in this.timestamps)
				{
					array[num++] = new RequestJobTimestampData(keyValuePair.Key, keyValuePair.Value);
				}
				return array;
			}
			set
			{
				this.timestamps.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						RequestJobTimestampData requestJobTimestampData = value[i];
						this.timestamps[requestJobTimestampData.Id] = requestJobTimestampData.Timestamp;
					}
				}
			}
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0003AA40 File Offset: 0x00038C40
		public static bool SupportDurationTracking(RequestState requestState)
		{
			return requestState != RequestState.None && requestState != RequestState.InitialSeedingComplete && requestState != RequestState.Removed;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0003AA64 File Offset: 0x00038C64
		public static bool SupportTimestampTracking(RequestJobTimestamp timestamp)
		{
			if (timestamp != RequestJobTimestamp.None)
			{
				switch (timestamp)
				{
				case RequestJobTimestamp.DomainControllerUpdate:
				case RequestJobTimestamp.RequestCanceled:
				case RequestJobTimestamp.LastSuccessfulSourceConnection:
				case RequestJobTimestamp.LastSuccessfulTargetConnection:
				case RequestJobTimestamp.SourceConnectionFailure:
				case RequestJobTimestamp.TargetConnectionFailure:
				case RequestJobTimestamp.IsIntegStarted:
				case RequestJobTimestamp.LastServerBusyBackoff:
				case RequestJobTimestamp.ServerBusyBackoffUntil:
				case RequestJobTimestamp.MaxTimestamp:
					break;
				default:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		public static void MergeTimestamps(RequestJobTimeTracker mrsTimeTracker, RequestJobTimeTracker other)
		{
			foreach (RequestJobTimestampData requestJobTimestampData in other.Timestamps)
			{
				if (!RequestJobTimeTracker.mrsMasteredTimestamps.Contains(requestJobTimestampData.Id))
				{
					if (RequestJobTimeTracker.nonMrsMasteredTimestamps.Contains(requestJobTimestampData.Id))
					{
						mrsTimeTracker.SetTimestamp(requestJobTimestampData.Id, new DateTime?(requestJobTimestampData.Timestamp));
					}
					else
					{
						DateTime? timestamp = mrsTimeTracker.GetTimestamp(requestJobTimestampData.Id);
						if (timestamp == null || timestamp.Value < requestJobTimestampData.Timestamp)
						{
							mrsTimeTracker.SetTimestamp(requestJobTimestampData.Id, new DateTime?(requestJobTimestampData.Timestamp));
						}
					}
				}
			}
			foreach (RequestJobTimestamp ts in RequestJobTimeTracker.nonMrsMasteredTimestamps)
			{
				if (other.GetTimestamp(ts) == null)
				{
					mrsTimeTracker.SetTimestamp(ts, null);
				}
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0003ABC4 File Offset: 0x00038DC4
		public void AddDurationToState(TimeSpan duration, RequestState requestState)
		{
			RequestJobDurationData requestJobDurationData;
			if (!this.durations.TryGetValue(requestState, out requestJobDurationData))
			{
				requestJobDurationData = new RequestJobDurationData(requestState);
			}
			requestJobDurationData.AddTime(duration);
			this.durations[requestState] = requestJobDurationData;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0003ABFC File Offset: 0x00038DFC
		public TimeSpan GetCurrentDurationChunk()
		{
			return DateTime.UtcNow - this.stateChangeTimestamp;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0003AC10 File Offset: 0x00038E10
		public RequestJobDurationData GetDuration(RequestState state)
		{
			RequestJobDurationData requestJobDurationData = null;
			RequestJobDurationData requestJobDurationData2;
			if (this.durations.TryGetValue(state, out requestJobDurationData2))
			{
				requestJobDurationData = requestJobDurationData2;
			}
			if (state == this.curState)
			{
				DateTime utcNow = TimeProvider.UtcNow;
				TimeSpan duration = utcNow.Subtract(this.stateChangeTimestamp);
				if (requestJobDurationData == null)
				{
					requestJobDurationData = new RequestJobDurationData(state);
					requestJobDurationData.AddTime(duration);
					this.durations[this.curState] = requestJobDurationData;
				}
				else
				{
					requestJobDurationData.AddTime(duration);
				}
				this.stateChangeTimestamp = utcNow;
			}
			RequestJobStateNode state2 = RequestJobStateNode.GetState(state);
			if (state2 != null)
			{
				foreach (RequestJobStateNode requestJobStateNode in state2.Children)
				{
					RequestJobDurationData duration2 = this.GetDuration(requestJobStateNode.MRState);
					if (duration2 != null)
					{
						requestJobDurationData += duration2;
					}
				}
			}
			return requestJobDurationData;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0003ACEC File Offset: 0x00038EEC
		public RequestJobDurationData GetDisplayDuration(RequestState state)
		{
			RequestJobDurationData duration = this.GetDuration(state);
			if (duration == null)
			{
				return new RequestJobDurationData(state);
			}
			return duration;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0003AD0C File Offset: 0x00038F0C
		public EnhancedTimeSpan? GetDisplayDuration(RequestState[] states)
		{
			EnhancedTimeSpan? result = null;
			foreach (RequestState state in states)
			{
				RequestJobDurationData displayDuration = this.GetDisplayDuration(state);
				if (displayDuration != null)
				{
					result = new EnhancedTimeSpan?(displayDuration.Duration + ((result != null) ? result.Value : EnhancedTimeSpan.Zero));
				}
			}
			return result;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0003AD6E File Offset: 0x00038F6E
		public void SetTimestamp(RequestJobTimestamp ts, DateTime? value)
		{
			if (value == null)
			{
				if (this.timestamps.ContainsKey(ts))
				{
					this.timestamps.Remove(ts);
					return;
				}
			}
			else
			{
				this.timestamps[ts] = value.Value;
			}
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		public DateTime? GetTimestamp(RequestJobTimestamp ts)
		{
			DateTime value;
			if (this.timestamps.TryGetValue(ts, out value))
			{
				return new DateTime?(value);
			}
			return null;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0003ADD8 File Offset: 0x00038FD8
		public void GetThrottledDurations(out ThrottleDurations sourceDurations, out ThrottleDurations targetDurations)
		{
			sourceDurations = new ThrottleDurations(this.GetNonEmptyDuration(RequestState.StalledDueToReadThrottle), this.GetNonEmptyDuration(RequestState.StalledDueToReadCpu), EnhancedTimeSpan.Zero, EnhancedTimeSpan.Zero, this.GetNonEmptyDuration(RequestState.StalledDueToReadUnknown));
			targetDurations = new ThrottleDurations(this.GetNonEmptyDuration(RequestState.StalledDueToWriteThrottle), this.GetNonEmptyDuration(RequestState.StalledDueToWriteCpu), this.GetNonEmptyDuration(RequestState.StalledDueToHA), this.GetNonEmptyDuration(RequestState.StalledDueToCI), this.GetNonEmptyDuration(RequestState.StalledDueToWriteUnknown));
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0003AE48 File Offset: 0x00039048
		public DateTime? GetDisplayTimestamp(RequestJobTimestamp ts)
		{
			DateTime? timestamp = this.GetTimestamp(ts);
			if (timestamp == null)
			{
				return null;
			}
			return new DateTime?(timestamp.Value.ToLocalTime());
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0003AE84 File Offset: 0x00039084
		public override string ToString()
		{
			this.RefreshDurations();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Current state: {0}\n", this.CurrentState);
			stringBuilder.AppendFormat("Last state change: {0}\n", this.stateChangeTimestamp);
			stringBuilder.AppendLine("Durations (per-slot data is in milliseconds):");
			foreach (RequestJobStateNode treeRoot in RequestJobStateNode.RootStates)
			{
				this.DumpDurationTree(stringBuilder, treeRoot, string.Empty);
			}
			stringBuilder.AppendLine("Timestamps:");
			for (RequestJobTimestamp requestJobTimestamp = RequestJobTimestamp.None; requestJobTimestamp < RequestJobTimestamp.MaxTimestamp; requestJobTimestamp++)
			{
				stringBuilder.AppendFormat("{0} {1}\n", requestJobTimestamp, this.GetDisplayTimestamp(requestJobTimestamp));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0003AF60 File Offset: 0x00039160
		internal RequestJobTimeTrackerXML GetDiagnosticInfo(RequestStatisticsDiagnosticArgument arguments)
		{
			bool showTimeSlots = arguments.HasArgument("showtimeslots");
			this.RefreshDurations();
			RequestJobTimeTrackerXML requestJobTimeTrackerXML = new RequestJobTimeTrackerXML();
			requestJobTimeTrackerXML.CurrentState = this.CurrentState.ToString();
			requestJobTimeTrackerXML.LastStateChangeTimeStamp = this.stateChangeTimestamp.ToString("O");
			for (RequestJobTimestamp requestJobTimestamp = RequestJobTimestamp.None; requestJobTimestamp < RequestJobTimestamp.MaxTimestamp; requestJobTimestamp++)
			{
				DateTime? timestamp = this.GetTimestamp(requestJobTimestamp);
				if (timestamp != null)
				{
					requestJobTimeTrackerXML.AddTimestamp(requestJobTimestamp, timestamp.Value);
				}
			}
			foreach (RequestJobStateNode treeRoot in RequestJobStateNode.RootStates)
			{
				RequestJobTimeTrackerXML.DurationRec durationRec = this.DumpDurationTreeForDiagnostics(treeRoot, showTimeSlots);
				if (durationRec != null && durationRec.Duration != TimeSpan.Zero.ToString())
				{
					if (requestJobTimeTrackerXML.Durations == null)
					{
						requestJobTimeTrackerXML.Durations = new List<RequestJobTimeTrackerXML.DurationRec>();
					}
					requestJobTimeTrackerXML.Durations.Add(durationRec);
				}
			}
			return requestJobTimeTrackerXML;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0003B070 File Offset: 0x00039270
		internal void AttachPerfCounters(MDBPerfCounterHelper pcHelper)
		{
			this.pcHelper = pcHelper;
			this.UpdateActiveCounts(this.CurrentState, 1L);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0003B088 File Offset: 0x00039288
		private void RefreshDurations()
		{
			DateTime utcNow = TimeProvider.UtcNow;
			if (utcNow <= this.stateChangeTimestamp)
			{
				return;
			}
			foreach (KeyValuePair<RequestState, RequestJobDurationData> keyValuePair in this.durations)
			{
				keyValuePair.Value.Refresh(this.stateChangeTimestamp);
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0003B0FC File Offset: 0x000392FC
		private TimeSpan GetNonEmptyDuration(RequestState state)
		{
			RequestJobDurationData duration = this.GetDuration(state);
			if (duration != null)
			{
				return duration.Duration;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0003B120 File Offset: 0x00039320
		private void UpdateActiveCounts(RequestState rjState, long delta)
		{
			if (this.pcHelper == null)
			{
				return;
			}
			for (RequestJobStateNode requestJobStateNode = RequestJobStateNode.GetState(rjState); requestJobStateNode != null; requestJobStateNode = requestJobStateNode.Parent)
			{
				if (requestJobStateNode.GetActivePerfCounter != null)
				{
					requestJobStateNode.GetActivePerfCounter(this.pcHelper.PerfCounter).IncrementBy(delta);
				}
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0003B170 File Offset: 0x00039370
		private void IncRateCounts(RequestState rjState)
		{
			if (this.pcHelper == null)
			{
				return;
			}
			for (RequestJobStateNode requestJobStateNode = RequestJobStateNode.GetState(rjState); requestJobStateNode != null; requestJobStateNode = requestJobStateNode.Parent)
			{
				if (requestJobStateNode.GetCountRatePerfCounter != null)
				{
					requestJobStateNode.GetCountRatePerfCounter(this.pcHelper).IncrementBy(1L);
				}
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0003B1BC File Offset: 0x000393BC
		private void DumpDurationTree(StringBuilder strBuilder, RequestJobStateNode treeRoot, string indent)
		{
			strBuilder.AppendFormat("{0}{1}: {2}\n", indent, treeRoot.MRState.ToString(), this.GetDisplayDuration(treeRoot.MRState));
			indent += "  ";
			foreach (RequestJobStateNode treeRoot2 in treeRoot.Children)
			{
				this.DumpDurationTree(strBuilder, treeRoot2, indent);
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0003B248 File Offset: 0x00039448
		private RequestJobTimeTrackerXML.DurationRec DumpDurationTreeForDiagnostics(RequestJobStateNode treeRoot, bool showTimeSlots = false)
		{
			RequestJobDurationData displayDuration = this.GetDisplayDuration(treeRoot.MRState);
			if (displayDuration == null)
			{
				return null;
			}
			RequestJobTimeTrackerXML.DurationRec durationRec = displayDuration.GetDurationRec(treeRoot.MRState, showTimeSlots);
			foreach (RequestJobStateNode treeRoot2 in treeRoot.Children)
			{
				RequestJobTimeTrackerXML.DurationRec durationRec2 = this.DumpDurationTreeForDiagnostics(treeRoot2, showTimeSlots);
				if (durationRec2 != null && durationRec2.Duration != TimeSpan.Zero.ToString())
				{
					if (durationRec.ChildNodes == null)
					{
						durationRec.ChildNodes = new List<RequestJobTimeTrackerXML.DurationRec>();
					}
					durationRec.ChildNodes.Add(durationRec2);
				}
			}
			return durationRec;
		}

		// Token: 0x04000C13 RID: 3091
		private static readonly List<RequestJobTimestamp> mrsMasteredTimestamps = new List<RequestJobTimestamp>(1)
		{
			RequestJobTimestamp.MailboxLocked
		};

		// Token: 0x04000C14 RID: 3092
		private static readonly List<RequestJobTimestamp> nonMrsMasteredTimestamps = new List<RequestJobTimestamp>(2)
		{
			RequestJobTimestamp.CompleteAfter,
			RequestJobTimestamp.StartAfter
		};

		// Token: 0x04000C15 RID: 3093
		private Dictionary<RequestState, RequestJobDurationData> durations;

		// Token: 0x04000C16 RID: 3094
		private Dictionary<RequestJobTimestamp, DateTime> timestamps;

		// Token: 0x04000C17 RID: 3095
		private RequestState curState;

		// Token: 0x04000C18 RID: 3096
		private DateTime stateChangeTimestamp;

		// Token: 0x04000C19 RID: 3097
		[NonSerialized]
		private MDBPerfCounterHelper pcHelper;
	}
}

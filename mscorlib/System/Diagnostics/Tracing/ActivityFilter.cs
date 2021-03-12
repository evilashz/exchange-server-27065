using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003FF RID: 1023
	internal sealed class ActivityFilter : IDisposable
	{
		// Token: 0x0600342B RID: 13355 RVA: 0x000C9CD0 File Offset: 0x000C7ED0
		public static void DisableFilter(ref ActivityFilter filterList, EventSource source)
		{
			if (filterList == null)
			{
				return;
			}
			ActivityFilter activityFilter = filterList;
			ActivityFilter next = activityFilter.m_next;
			while (next != null)
			{
				if (next.m_providerGuid == source.Guid)
				{
					if (next.m_eventId >= 0 && next.m_eventId < source.m_eventData.Length)
					{
						EventSource.EventMetadata[] eventData = source.m_eventData;
						int eventId = next.m_eventId;
						eventData[eventId].TriggersActivityTracking = eventData[eventId].TriggersActivityTracking - 1;
					}
					activityFilter.m_next = next.m_next;
					next.Dispose();
					next = activityFilter.m_next;
				}
				else
				{
					activityFilter = next;
					next = activityFilter.m_next;
				}
			}
			if (filterList.m_providerGuid == source.Guid)
			{
				if (filterList.m_eventId >= 0 && filterList.m_eventId < source.m_eventData.Length)
				{
					EventSource.EventMetadata[] eventData2 = source.m_eventData;
					int eventId2 = filterList.m_eventId;
					eventData2[eventId2].TriggersActivityTracking = eventData2[eventId2].TriggersActivityTracking - 1;
				}
				ActivityFilter activityFilter2 = filterList;
				filterList = activityFilter2.m_next;
				activityFilter2.Dispose();
			}
			if (filterList != null)
			{
				ActivityFilter.EnsureActivityCleanupDelegate(filterList);
			}
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000C9DD0 File Offset: 0x000C7FD0
		public static void UpdateFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, string startEvents)
		{
			ActivityFilter.DisableFilter(ref filterList, source);
			if (!string.IsNullOrEmpty(startEvents))
			{
				foreach (string text in startEvents.Split(new char[]
				{
					' '
				}))
				{
					int samplingFreq = 1;
					int num = -1;
					int num2 = text.IndexOf(':');
					if (num2 < 0)
					{
						source.ReportOutOfBandMessage("ERROR: Invalid ActivitySamplingStartEvent specification: " + text, false);
					}
					else
					{
						string text2 = text.Substring(num2 + 1);
						if (!int.TryParse(text2, out samplingFreq))
						{
							source.ReportOutOfBandMessage("ERROR: Invalid sampling frequency specification: " + text2, false);
						}
						else
						{
							text = text.Substring(0, num2);
							if (!int.TryParse(text, out num))
							{
								num = -1;
								for (int j = 0; j < source.m_eventData.Length; j++)
								{
									EventSource.EventMetadata[] eventData = source.m_eventData;
									if (eventData[j].Name != null && eventData[j].Name.Length == text.Length && string.Compare(eventData[j].Name, text, StringComparison.OrdinalIgnoreCase) == 0)
									{
										num = eventData[j].Descriptor.EventId;
										break;
									}
								}
							}
							if (num < 0 || num >= source.m_eventData.Length)
							{
								source.ReportOutOfBandMessage("ERROR: Invalid eventId specification: " + text, false);
							}
							else
							{
								ActivityFilter.EnableFilter(ref filterList, source, perEventSourceSessionId, num, samplingFreq);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000C9F3C File Offset: 0x000C813C
		public static ActivityFilter GetFilter(ActivityFilter filterList, EventSource source)
		{
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_providerGuid == source.Guid && activityFilter.m_samplingFreq != -1)
				{
					return activityFilter;
				}
			}
			return null;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000C9F78 File Offset: 0x000C8178
		[SecurityCritical]
		public unsafe static bool PassesActivityFilter(ActivityFilter filterList, Guid* childActivityID, bool triggeringEvent, EventSource source, int eventId)
		{
			bool flag = false;
			if (triggeringEvent)
			{
				ActivityFilter activityFilter = filterList;
				while (activityFilter != null)
				{
					if (eventId == activityFilter.m_eventId && source.Guid == activityFilter.m_providerGuid)
					{
						int curSampleCount;
						int value;
						do
						{
							curSampleCount = activityFilter.m_curSampleCount;
							if (curSampleCount <= 1)
							{
								value = activityFilter.m_samplingFreq;
							}
							else
							{
								value = curSampleCount - 1;
							}
						}
						while (Interlocked.CompareExchange(ref activityFilter.m_curSampleCount, value, curSampleCount) != curSampleCount);
						if (curSampleCount <= 1)
						{
							Guid internalCurrentThreadActivityId = EventSource.InternalCurrentThreadActivityId;
							Tuple<Guid, int> tuple;
							if (!activityFilter.m_rootActiveActivities.TryGetValue(internalCurrentThreadActivityId, out tuple))
							{
								flag = true;
								activityFilter.m_activeActivities[internalCurrentThreadActivityId] = Environment.TickCount;
								activityFilter.m_rootActiveActivities[internalCurrentThreadActivityId] = Tuple.Create<Guid, int>(source.Guid, eventId);
								break;
							}
							break;
						}
						else
						{
							Guid internalCurrentThreadActivityId2 = EventSource.InternalCurrentThreadActivityId;
							Tuple<Guid, int> tuple2;
							if (activityFilter.m_rootActiveActivities.TryGetValue(internalCurrentThreadActivityId2, out tuple2) && tuple2.Item1 == source.Guid && tuple2.Item2 == eventId)
							{
								int num;
								activityFilter.m_activeActivities.TryRemove(internalCurrentThreadActivityId2, out num);
								break;
							}
							break;
						}
					}
					else
					{
						activityFilter = activityFilter.m_next;
					}
				}
			}
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			if (activeActivities != null)
			{
				if (!flag)
				{
					flag = (!activeActivities.IsEmpty && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId));
				}
				if (flag && childActivityID != null && source.m_eventData[eventId].Descriptor.Opcode == 9)
				{
					ActivityFilter.FlowActivityIfNeeded(filterList, null, childActivityID);
				}
			}
			return flag;
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000CA0DC File Offset: 0x000C82DC
		[SecuritySafeCritical]
		public static bool IsCurrentActivityActive(ActivityFilter filterList)
		{
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			return activeActivities != null && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000CA104 File Offset: 0x000C8304
		[SecurityCritical]
		public unsafe static void FlowActivityIfNeeded(ActivityFilter filterList, Guid* currentActivityId, Guid* childActivityID)
		{
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			if (currentActivityId != null && !activeActivities.ContainsKey(*currentActivityId))
			{
				return;
			}
			if (activeActivities.Count > 100000)
			{
				ActivityFilter.TrimActiveActivityStore(activeActivities);
				activeActivities[EventSource.InternalCurrentThreadActivityId] = Environment.TickCount;
			}
			activeActivities[*childActivityID] = Environment.TickCount;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000CA160 File Offset: 0x000C8360
		public static void UpdateKwdTriggers(ActivityFilter activityFilter, Guid sourceGuid, EventSource source, EventKeywords sessKeywords)
		{
			for (ActivityFilter activityFilter2 = activityFilter; activityFilter2 != null; activityFilter2 = activityFilter2.m_next)
			{
				if (sourceGuid == activityFilter2.m_providerGuid && (source.m_eventData[activityFilter2.m_eventId].TriggersActivityTracking > 0 || source.m_eventData[activityFilter2.m_eventId].Descriptor.Opcode == 9))
				{
					source.m_keywordTriggers |= (source.m_eventData[activityFilter2.m_eventId].Descriptor.Keywords & (long)sessKeywords);
				}
			}
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000CA1F1 File Offset: 0x000C83F1
		public IEnumerable<Tuple<int, int>> GetFilterAsTuple(Guid sourceGuid)
		{
			ActivityFilter af;
			for (af = this; af != null; af = af.m_next)
			{
				if (af.m_providerGuid == sourceGuid)
				{
					yield return Tuple.Create<int, int>(af.m_eventId, af.m_samplingFreq);
				}
			}
			af = null;
			yield break;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000CA208 File Offset: 0x000C8408
		public void Dispose()
		{
			if (this.m_myActivityDelegate != null)
			{
				EventSource.s_activityDying = (Action<Guid>)Delegate.Remove(EventSource.s_activityDying, this.m_myActivityDelegate);
				this.m_myActivityDelegate = null;
			}
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000CA234 File Offset: 0x000C8434
		private ActivityFilter(EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq, ActivityFilter existingFilter = null)
		{
			this.m_providerGuid = source.Guid;
			this.m_perEventSourceSessionId = perEventSourceSessionId;
			this.m_eventId = eventId;
			this.m_samplingFreq = samplingFreq;
			this.m_next = existingFilter;
			ConcurrentDictionary<Guid, int> activeActivities;
			if (existingFilter == null || (activeActivities = ActivityFilter.GetActiveActivities(existingFilter)) == null)
			{
				this.m_activeActivities = new ConcurrentDictionary<Guid, int>();
				this.m_rootActiveActivities = new ConcurrentDictionary<Guid, Tuple<Guid, int>>();
				this.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(this);
				EventSource.s_activityDying = (Action<Guid>)Delegate.Combine(EventSource.s_activityDying, this.m_myActivityDelegate);
				return;
			}
			this.m_activeActivities = activeActivities;
			this.m_rootActiveActivities = existingFilter.m_rootActiveActivities;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000CA2D4 File Offset: 0x000C84D4
		private static void EnsureActivityCleanupDelegate(ActivityFilter filterList)
		{
			if (filterList == null)
			{
				return;
			}
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_myActivityDelegate != null)
				{
					return;
				}
			}
			filterList.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(filterList);
			EventSource.s_activityDying = (Action<Guid>)Delegate.Combine(EventSource.s_activityDying, filterList.m_myActivityDelegate);
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000CA324 File Offset: 0x000C8524
		private static Action<Guid> GetActivityDyingDelegate(ActivityFilter filterList)
		{
			return delegate(Guid oldActivity)
			{
				int num;
				filterList.m_activeActivities.TryRemove(oldActivity, out num);
				Tuple<Guid, int> tuple;
				filterList.m_rootActiveActivities.TryRemove(oldActivity, out tuple);
			};
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000CA34A File Offset: 0x000C854A
		private static bool EnableFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq)
		{
			filterList = new ActivityFilter(source, perEventSourceSessionId, eventId, samplingFreq, filterList);
			if (0 <= eventId && eventId < source.m_eventData.Length)
			{
				EventSource.EventMetadata[] eventData = source.m_eventData;
				eventData[eventId].TriggersActivityTracking = eventData[eventId].TriggersActivityTracking + 1;
			}
			return true;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000CA388 File Offset: 0x000C8588
		private static void TrimActiveActivityStore(ConcurrentDictionary<Guid, int> activities)
		{
			if (activities.Count > 100000)
			{
				KeyValuePair<Guid, int>[] array = activities.ToArray();
				int tickNow = Environment.TickCount;
				Array.Sort<KeyValuePair<Guid, int>>(array, (KeyValuePair<Guid, int> x, KeyValuePair<Guid, int> y) => (int.MaxValue & tickNow - y.Value) - (int.MaxValue & tickNow - x.Value));
				for (int i = 0; i < array.Length / 2; i++)
				{
					int num;
					activities.TryRemove(array[i].Key, out num);
				}
			}
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000CA3F4 File Offset: 0x000C85F4
		private static ConcurrentDictionary<Guid, int> GetActiveActivities(ActivityFilter filterList)
		{
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_activeActivities != null)
				{
					return activityFilter.m_activeActivities;
				}
			}
			return null;
		}

		// Token: 0x040016F5 RID: 5877
		private ConcurrentDictionary<Guid, int> m_activeActivities;

		// Token: 0x040016F6 RID: 5878
		private ConcurrentDictionary<Guid, Tuple<Guid, int>> m_rootActiveActivities;

		// Token: 0x040016F7 RID: 5879
		private Guid m_providerGuid;

		// Token: 0x040016F8 RID: 5880
		private int m_eventId;

		// Token: 0x040016F9 RID: 5881
		private int m_samplingFreq;

		// Token: 0x040016FA RID: 5882
		private int m_curSampleCount;

		// Token: 0x040016FB RID: 5883
		private int m_perEventSourceSessionId;

		// Token: 0x040016FC RID: 5884
		private const int MaxActivityTrackCount = 100000;

		// Token: 0x040016FD RID: 5885
		private ActivityFilter m_next;

		// Token: 0x040016FE RID: 5886
		private Action<Guid> m_myActivityDelegate;
	}
}

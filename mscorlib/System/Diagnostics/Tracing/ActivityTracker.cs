using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003EE RID: 1006
	internal class ActivityTracker
	{
		// Token: 0x06003319 RID: 13081 RVA: 0x000C22B0 File Offset: 0x000C04B0
		public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options)
		{
			if (this.m_current == null)
			{
				if (this.m_checkedForEnable)
				{
					return;
				}
				this.m_checkedForEnable = true;
				if (TplEtwProvider.Log.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
				{
					this.Enable();
				}
				if (this.m_current == null)
				{
					return;
				}
			}
			ActivityTracker.ActivityInfo value = this.m_current.Value;
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartEnter", text);
				log.DebugFacilityMessage("OnStartEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(value));
			}
			if (value != null)
			{
				if (value.m_level >= 100)
				{
					activityId = Guid.Empty;
					relatedActivityId = Guid.Empty;
					if (log.Debug)
					{
						log.DebugFacilityMessage("OnStartRET", "Fail");
					}
					return;
				}
				if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None)
				{
					ActivityTracker.ActivityInfo activityInfo = this.FindActiveActivity(text, value);
					if (activityInfo != null)
					{
						this.OnStop(providerName, activityName, task, ref activityId);
						value = this.m_current.Value;
					}
				}
			}
			long uniqueId;
			if (value == null)
			{
				uniqueId = Interlocked.Increment(ref ActivityTracker.m_nextId);
			}
			else
			{
				uniqueId = Interlocked.Increment(ref value.m_lastChildID);
			}
			relatedActivityId = EventSource.CurrentThreadActivityId;
			ActivityTracker.ActivityInfo activityInfo2 = new ActivityTracker.ActivityInfo(text, uniqueId, value, relatedActivityId, options);
			this.m_current.Value = activityInfo2;
			activityId = activityInfo2.ActivityId;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo2));
				log.DebugFacilityMessage1("OnStartRet", activityId.ToString(), relatedActivityId.ToString());
			}
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000C2438 File Offset: 0x000C0638
		public void OnStop(string providerName, string activityName, int task, ref Guid activityId)
		{
			if (this.m_current == null)
			{
				return;
			}
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopEnter", text);
				log.DebugFacilityMessage("OnStopEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value));
			}
			ActivityTracker.ActivityInfo activityInfo;
			for (;;)
			{
				ActivityTracker.ActivityInfo value = this.m_current.Value;
				activityInfo = null;
				ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, value);
				if (activityInfo2 == null)
				{
					break;
				}
				activityId = activityInfo2.ActivityId;
				ActivityTracker.ActivityInfo activityInfo3 = value;
				while (activityInfo3 != activityInfo2 && activityInfo3 != null)
				{
					if (activityInfo3.m_stopped != 0)
					{
						activityInfo3 = activityInfo3.m_creator;
					}
					else
					{
						if (activityInfo3.CanBeOrphan())
						{
							if (activityInfo == null)
							{
								activityInfo = activityInfo3;
							}
						}
						else
						{
							activityInfo3.m_stopped = 1;
						}
						activityInfo3 = activityInfo3.m_creator;
					}
				}
				if (Interlocked.CompareExchange(ref activityInfo2.m_stopped, 1, 0) == 0)
				{
					goto Block_9;
				}
			}
			activityId = Guid.Empty;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRET", "Fail");
			}
			return;
			Block_9:
			if (activityInfo == null)
			{
				ActivityTracker.ActivityInfo activityInfo2;
				activityInfo = activityInfo2.m_creator;
			}
			this.m_current.Value = activityInfo;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
				log.DebugFacilityMessage("OnStopRet", activityId.ToString());
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000C257C File Offset: 0x000C077C
		[SecuritySafeCritical]
		public void Enable()
		{
			if (this.m_current == null)
			{
				this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000C259D File Offset: 0x000C079D
		public static ActivityTracker Instance
		{
			get
			{
				return ActivityTracker.s_activityTrackerInstance;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000C25A4 File Offset: 0x000C07A4
		private Guid CurrentActivityId
		{
			get
			{
				return this.m_current.Value.ActivityId;
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000C25B8 File Offset: 0x000C07B8
		private ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
		{
			for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
			{
				if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
				{
					return activityInfo;
				}
			}
			return null;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000C25EC File Offset: 0x000C07EC
		private string NormalizeActivityName(string providerName, string activityName, int task)
		{
			if (activityName.EndsWith("Start"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Start".Length);
			}
			else if (activityName.EndsWith("Stop"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Stop".Length);
			}
			else if (task != 0)
			{
				activityName = "task" + task.ToString();
			}
			return providerName + activityName;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000C2668 File Offset: 0x000C0868
		private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
		{
			ActivityTracker.ActivityInfo activityInfo = args.CurrentValue;
			ActivityTracker.ActivityInfo previousValue = args.PreviousValue;
			if (previousValue != null && previousValue.m_creator == activityInfo && (activityInfo == null || previousValue.m_activityIdToRestore != activityInfo.ActivityId))
			{
				EventSource.SetCurrentThreadActivityId(previousValue.m_activityIdToRestore);
				return;
			}
			while (activityInfo != null)
			{
				if (activityInfo.m_stopped == 0)
				{
					EventSource.SetCurrentThreadActivityId(activityInfo.ActivityId);
					return;
				}
				activityInfo = activityInfo.m_creator;
			}
		}

		// Token: 0x0400166C RID: 5740
		private AsyncLocal<ActivityTracker.ActivityInfo> m_current;

		// Token: 0x0400166D RID: 5741
		private bool m_checkedForEnable;

		// Token: 0x0400166E RID: 5742
		private static ActivityTracker s_activityTrackerInstance = new ActivityTracker();

		// Token: 0x0400166F RID: 5743
		private static long m_nextId = 0L;

		// Token: 0x04001670 RID: 5744
		private const ushort MAX_ACTIVITY_DEPTH = 100;

		// Token: 0x02000B4D RID: 2893
		private class ActivityInfo
		{
			// Token: 0x06006B33 RID: 27443 RVA: 0x00172800 File Offset: 0x00170A00
			public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, Guid activityIDToRestore, EventActivityOptions options)
			{
				this.m_name = name;
				this.m_eventOptions = options;
				this.m_creator = creator;
				this.m_uniqueId = uniqueId;
				this.m_level = ((creator != null) ? (creator.m_level + 1) : 0);
				this.m_activityIdToRestore = activityIDToRestore;
				this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
			}

			// Token: 0x17001240 RID: 4672
			// (get) Token: 0x06006B34 RID: 27444 RVA: 0x0017285E File Offset: 0x00170A5E
			public Guid ActivityId
			{
				get
				{
					return this.m_guid;
				}
			}

			// Token: 0x06006B35 RID: 27445 RVA: 0x00172866 File Offset: 0x00170A66
			public static string Path(ActivityTracker.ActivityInfo activityInfo)
			{
				if (activityInfo == null)
				{
					return "";
				}
				return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + activityInfo.m_uniqueId;
			}

			// Token: 0x06006B36 RID: 27446 RVA: 0x00172894 File Offset: 0x00170A94
			public override string ToString()
			{
				string text = "";
				if (this.m_stopped != 0)
				{
					text = ",DEAD";
				}
				return string.Concat(new string[]
				{
					this.m_name,
					"(",
					ActivityTracker.ActivityInfo.Path(this),
					text,
					")"
				});
			}

			// Token: 0x06006B37 RID: 27447 RVA: 0x001728E6 File Offset: 0x00170AE6
			public static string LiveActivities(ActivityTracker.ActivityInfo list)
			{
				if (list == null)
				{
					return "";
				}
				return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
			}

			// Token: 0x06006B38 RID: 27448 RVA: 0x0017290C File Offset: 0x00170B0C
			public bool CanBeOrphan()
			{
				return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
			}

			// Token: 0x06006B39 RID: 27449 RVA: 0x0017291C File Offset: 0x00170B1C
			[SecuritySafeCritical]
			private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
			{
				fixed (Guid* ptr = &idRet)
				{
					int whereToAddId = 0;
					if (this.m_creator != null)
					{
						whereToAddId = this.m_creator.m_activityPathGuidOffset;
						idRet = this.m_creator.m_guid;
					}
					else
					{
						int domainID = Thread.GetDomainID();
						whereToAddId = ActivityTracker.ActivityInfo.AddIdToGuid(ptr, whereToAddId, (uint)domainID, false);
					}
					activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(ptr, whereToAddId, (uint)this.m_uniqueId, false);
					if (12 < activityPathGuidOffset)
					{
						this.CreateOverflowGuid(ptr);
					}
				}
			}

			// Token: 0x06006B3A RID: 27450 RVA: 0x0017298C File Offset: 0x00170B8C
			[SecurityCritical]
			private unsafe void CreateOverflowGuid(Guid* outPtr)
			{
				for (ActivityTracker.ActivityInfo creator = this.m_creator; creator != null; creator = creator.m_creator)
				{
					if (creator.m_activityPathGuidOffset <= 10)
					{
						uint id = (uint)Interlocked.Increment(ref creator.m_lastChildID);
						*outPtr = creator.m_guid;
						int num = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, creator.m_activityPathGuidOffset, id, true);
						if (num <= 12)
						{
							break;
						}
					}
				}
			}

			// Token: 0x06006B3B RID: 27451 RVA: 0x001729E4 File Offset: 0x00170BE4
			[SecurityCritical]
			private unsafe static int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
			{
				byte* ptr = (byte*)outPtr;
				byte* ptr2 = ptr + 12;
				ptr += whereToAddId;
				if (ptr2 == ptr)
				{
					return 13;
				}
				if (0U < id && id <= 10U && !overflow)
				{
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, id);
				}
				else
				{
					uint num = 4U;
					if (id <= 255U)
					{
						num = 1U;
					}
					else if (id <= 65535U)
					{
						num = 2U;
					}
					else if (id <= 16777215U)
					{
						num = 3U;
					}
					if (overflow)
					{
						if (ptr2 == ptr + 2)
						{
							return 13;
						}
						ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 11U);
					}
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 12U + (num - 1U));
					if (ptr < ptr2 && *ptr != 0)
					{
						if (id < 4096U)
						{
							*ptr = (byte)(192U + (id >> 8));
							id &= 255U;
						}
						ptr++;
					}
					while (0U < num)
					{
						if (ptr2 == ptr)
						{
							ptr++;
							break;
						}
						*(ptr++) = (byte)id;
						id >>= 8;
						num -= 1U;
					}
				}
				*(int*)(outPtr + (IntPtr)3 * 4 / (IntPtr)sizeof(Guid)) = (int)(*(uint*)outPtr + *(uint*)(outPtr + 4 / sizeof(Guid)) + *(uint*)(outPtr + (IntPtr)2 * 4 / (IntPtr)sizeof(Guid)) + 1503500717U);
				return (int)((long)((byte*)ptr - (byte*)outPtr));
			}

			// Token: 0x06006B3C RID: 27452 RVA: 0x00172AD4 File Offset: 0x00170CD4
			[SecurityCritical]
			private unsafe static void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
			{
				if (*ptr != 0)
				{
					byte* ptr2 = ptr;
					ptr = ptr2 + 1;
					byte* ptr3 = ptr2;
					*ptr3 |= (byte)value;
					return;
				}
				*ptr = (byte)(value << 4);
			}

			// Token: 0x040033F6 RID: 13302
			internal readonly string m_name;

			// Token: 0x040033F7 RID: 13303
			private readonly long m_uniqueId;

			// Token: 0x040033F8 RID: 13304
			internal readonly Guid m_guid;

			// Token: 0x040033F9 RID: 13305
			internal readonly int m_activityPathGuidOffset;

			// Token: 0x040033FA RID: 13306
			internal readonly int m_level;

			// Token: 0x040033FB RID: 13307
			internal readonly EventActivityOptions m_eventOptions;

			// Token: 0x040033FC RID: 13308
			internal long m_lastChildID;

			// Token: 0x040033FD RID: 13309
			internal int m_stopped;

			// Token: 0x040033FE RID: 13310
			internal readonly ActivityTracker.ActivityInfo m_creator;

			// Token: 0x040033FF RID: 13311
			internal readonly Guid m_activityIdToRestore;

			// Token: 0x02000CCB RID: 3275
			private enum NumberListCodes : byte
			{
				// Token: 0x0400384D RID: 14413
				End,
				// Token: 0x0400384E RID: 14414
				LastImmediateValue = 10,
				// Token: 0x0400384F RID: 14415
				PrefixCode,
				// Token: 0x04003850 RID: 14416
				MultiByte1
			}
		}
	}
}

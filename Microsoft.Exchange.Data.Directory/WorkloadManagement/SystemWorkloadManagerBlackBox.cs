using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A09 RID: 2569
	internal class SystemWorkloadManagerBlackBox
	{
		// Token: 0x060076D0 RID: 30416 RVA: 0x001873A4 File Offset: 0x001855A4
		static SystemWorkloadManagerBlackBox()
		{
			string name = "WorkloadManager.MaxHistoryDepth";
			IntAppSettingsEntry intAppSettingsEntry = new IntAppSettingsEntry(name, SystemWorkloadManagerBlackBox.maxHistoryDepth, ExTraceGlobals.PoliciesTracer);
			SystemWorkloadManagerBlackBox.maxHistoryDepth = intAppSettingsEntry.Value;
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x001873F8 File Offset: 0x001855F8
		public static void AddActiveClassification(WorkloadClassification classification)
		{
			lock (SystemWorkloadManagerBlackBox.activeLock)
			{
				if (SystemWorkloadManagerBlackBox.active == null || !SystemWorkloadManagerBlackBox.active.Contains(classification))
				{
					HashSet<WorkloadClassification> hashSet;
					if (SystemWorkloadManagerBlackBox.active == null)
					{
						hashSet = new HashSet<WorkloadClassification>();
					}
					else
					{
						hashSet = new HashSet<WorkloadClassification>(SystemWorkloadManagerBlackBox.active);
					}
					hashSet.Add(classification);
					SystemWorkloadManagerBlackBox.active = hashSet;
				}
			}
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x00187470 File Offset: 0x00185670
		public static void RecordMonitorUpdate(ref SystemWorkloadManagerLogEntry lastEntry, ResourceKey resource, WorkloadClassification classification, ResourceLoad load)
		{
			SystemWorkloadManagerBlackBox.Record(ref lastEntry, SystemWorkloadManagerLogEntryType.Monitor, resource, classification, load, -1, false);
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x0018747E File Offset: 0x0018567E
		public static void RecordAdmissionUpdate(ref SystemWorkloadManagerLogEntry lastEntry, ResourceKey resource, WorkloadClassification classification, ResourceLoad load, int slots, bool delayed)
		{
			SystemWorkloadManagerBlackBox.Record(ref lastEntry, SystemWorkloadManagerLogEntryType.Admission, resource, classification, load, slots, delayed);
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x00187490 File Offset: 0x00185690
		public static SystemWorkloadManagerLogEntry[] GetRecords(bool clear = false)
		{
			if (clear)
			{
				Queue<SystemWorkloadManagerLogEntry> queue = null;
				lock (SystemWorkloadManagerBlackBox.history)
				{
					queue = SystemWorkloadManagerBlackBox.history;
					SystemWorkloadManagerBlackBox.history = new Queue<SystemWorkloadManagerLogEntry>();
				}
				return queue.ToArray();
			}
			SystemWorkloadManagerLogEntry[] result;
			lock (SystemWorkloadManagerBlackBox.history)
			{
				result = SystemWorkloadManagerBlackBox.history.ToArray();
			}
			return result;
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x00187520 File Offset: 0x00185720
		private static void Record(ref SystemWorkloadManagerLogEntry lastEntry, SystemWorkloadManagerLogEntryType type, ResourceKey resource, WorkloadClassification classification, ResourceLoad load, int slots, bool delayed)
		{
			if ((SystemWorkloadManagerBlackBox.active == null || SystemWorkloadManagerBlackBox.active.Contains(classification)) && (lastEntry == null || lastEntry.CurrentEvent.Load.State != load.State || lastEntry.CurrentEvent.Slots != slots || lastEntry.CurrentEvent.Delayed != delayed))
			{
				SystemWorkloadManagerEvent currentEvent = new SystemWorkloadManagerEvent(load, slots, delayed);
				lock (SystemWorkloadManagerBlackBox.history)
				{
					while (SystemWorkloadManagerBlackBox.history.Count >= SystemWorkloadManagerBlackBox.maxHistoryDepth)
					{
						SystemWorkloadManagerBlackBox.history.Dequeue();
					}
					if (lastEntry == null)
					{
						lastEntry = new SystemWorkloadManagerLogEntry(type, resource, classification, currentEvent, null);
						SystemWorkloadManagerBlackBox.history.Enqueue(lastEntry);
					}
					else
					{
						lastEntry = new SystemWorkloadManagerLogEntry(type, resource, classification, currentEvent, lastEntry.CurrentEvent);
						SystemWorkloadManagerBlackBox.history.Enqueue(lastEntry);
					}
				}
			}
		}

		// Token: 0x04004C11 RID: 19473
		private static int maxHistoryDepth = 1000;

		// Token: 0x04004C12 RID: 19474
		private static Queue<SystemWorkloadManagerLogEntry> history = new Queue<SystemWorkloadManagerLogEntry>();

		// Token: 0x04004C13 RID: 19475
		private static HashSet<WorkloadClassification> active = null;

		// Token: 0x04004C14 RID: 19476
		private static object activeLock = new object();
	}
}

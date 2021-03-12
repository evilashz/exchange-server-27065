using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Win32;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactsUploaderPerformanceTracker : IContactsUploaderPerformanceTracker, ILogEvent
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000064B8 File Offset: 0x000046B8
		public ContactsUploaderPerformanceTracker()
		{
			this.stopwatch = new Stopwatch();
			this.internalState = ContactsUploaderPerformanceTracker.InternalState.Stopped;
			this.OperationResult = "Succeded";
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000064DD File Offset: 0x000046DD
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000064E5 File Offset: 0x000046E5
		public int ReceivedContactsCount { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000064EE File Offset: 0x000046EE
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000064F6 File Offset: 0x000046F6
		public double ExportedDataSize { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000064FF File Offset: 0x000046FF
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00006507 File Offset: 0x00004707
		public string OperationResult { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00006510 File Offset: 0x00004710
		public string EventId
		{
			get
			{
				return "PerformanceData";
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006518 File Offset: 0x00004718
		public void Start()
		{
			this.EnforceInternalState(ContactsUploaderPerformanceTracker.InternalState.Stopped, "Start");
			this.elapsedTimeBookmarks = new List<ContactsUploaderPerformanceTracker.ElapsedTimeBookmark>();
			this.stopwatch.Start();
			this.startThreadTimes = ThreadTimes.GetFromCurrentThread();
			this.startStorePerformanceData = RpcDataProvider.Instance.TakeSnapshot(true);
			this.internalState = ContactsUploaderPerformanceTracker.InternalState.Started;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000656A File Offset: 0x0000476A
		public void IncrementContactsRead()
		{
			this.contactsRead++;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000657A File Offset: 0x0000477A
		public void IncrementContactsExported()
		{
			this.contactsExported++;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000658C File Offset: 0x0000478C
		public void AddTimeBookmark(ContactsUploaderPerformanceTrackerBookmarks activity)
		{
			this.EnforceInternalState(ContactsUploaderPerformanceTracker.InternalState.Started, "AddTimeBookmark");
			this.elapsedTimeBookmarks.Add(new ContactsUploaderPerformanceTracker.ElapsedTimeBookmark
			{
				Activity = activity,
				ElapsedTime = this.stopwatch.Elapsed
			});
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000065D4 File Offset: 0x000047D4
		public void Stop()
		{
			this.EnforceInternalState(ContactsUploaderPerformanceTracker.InternalState.Started, "Stop");
			this.stopwatch.Stop();
			ThreadTimes fromCurrentThread = ThreadTimes.GetFromCurrentThread();
			PerformanceData pd = RpcDataProvider.Instance.TakeSnapshot(false);
			this.internalState = ContactsUploaderPerformanceTracker.InternalState.Stopped;
			this.cpuTime += fromCurrentThread.Kernel - this.startThreadTimes.Kernel + (fromCurrentThread.User - this.startThreadTimes.User);
			PerformanceData performanceData = pd - this.startStorePerformanceData;
			this.storeRpcLatency += performanceData.Latency;
			this.storeRpcCount += (int)performanceData.Count;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000668C File Offset: 0x0000488C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			this.EnforceInternalState(ContactsUploaderPerformanceTracker.InternalState.Stopped, "GetEventData");
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>
			{
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.ContactsRead, this.contactsRead),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.ContactsExported, this.contactsExported),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.ContactsReceived, this.ReceivedContactsCount),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.DataSize, this.ExportedDataSize),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.RpcCount, this.storeRpcCount),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.RpcLatency, this.storeRpcLatency.TotalMilliseconds),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.CpuTime, this.cpuTime.TotalMilliseconds),
				ContactsUploaderPerformanceTracker.CreateEventData(ContactsUploaderPerformanceTrackerSchema.Result, this.OperationResult)
			};
			this.AddBookmarkData(list);
			return list;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006772 File Offset: 0x00004972
		private static KeyValuePair<string, object> CreateEventData(ContactsUploaderPerformanceTrackerSchema field, object value)
		{
			return new KeyValuePair<string, object>(DisplayNameAttribute.GetEnumName(field), value);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006788 File Offset: 0x00004988
		private void AddBookmarkData(List<KeyValuePair<string, object>> eventData)
		{
			ArgumentValidator.ThrowIfNull("eventData", eventData);
			TimeSpan t = default(TimeSpan);
			foreach (ContactsUploaderPerformanceTracker.ElapsedTimeBookmark elapsedTimeBookmark in this.elapsedTimeBookmarks)
			{
				double totalMilliseconds = (elapsedTimeBookmark.ElapsedTime - t).TotalMilliseconds;
				eventData.Add(new KeyValuePair<string, object>(DisplayNameAttribute.GetEnumName(elapsedTimeBookmark.Activity), totalMilliseconds));
				t = elapsedTimeBookmark.ElapsedTime;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006828 File Offset: 0x00004A28
		private void EnforceInternalState(ContactsUploaderPerformanceTracker.InternalState expectedState, string action)
		{
			if (this.internalState != expectedState)
			{
				string message = string.Format("{0} can only be performed when state is {1}. Present state is {2}", action, expectedState, this.internalState);
				ExWatson.SendReport(new InvalidOperationException(message), ReportOptions.None, null);
			}
		}

		// Token: 0x04000044 RID: 68
		private const string DefaultOperationResult = "Succeded";

		// Token: 0x04000045 RID: 69
		private readonly Stopwatch stopwatch;

		// Token: 0x04000046 RID: 70
		private ThreadTimes startThreadTimes;

		// Token: 0x04000047 RID: 71
		private PerformanceData startStorePerformanceData;

		// Token: 0x04000048 RID: 72
		private int contactsRead;

		// Token: 0x04000049 RID: 73
		private int contactsExported;

		// Token: 0x0400004A RID: 74
		private List<ContactsUploaderPerformanceTracker.ElapsedTimeBookmark> elapsedTimeBookmarks;

		// Token: 0x0400004B RID: 75
		private TimeSpan cpuTime;

		// Token: 0x0400004C RID: 76
		private TimeSpan storeRpcLatency;

		// Token: 0x0400004D RID: 77
		private int storeRpcCount;

		// Token: 0x0400004E RID: 78
		private ContactsUploaderPerformanceTracker.InternalState internalState;

		// Token: 0x02000015 RID: 21
		private enum InternalState
		{
			// Token: 0x04000053 RID: 83
			Stopped,
			// Token: 0x04000054 RID: 84
			Started
		}

		// Token: 0x02000016 RID: 22
		private struct ElapsedTimeBookmark
		{
			// Token: 0x04000055 RID: 85
			public ContactsUploaderPerformanceTrackerBookmarks Activity;

			// Token: 0x04000056 RID: 86
			public TimeSpan ElapsedTime;
		}
	}
}

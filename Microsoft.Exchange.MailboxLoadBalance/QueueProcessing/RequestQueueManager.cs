using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000E5 RID: 229
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RequestQueueManager : IRequestQueueManager
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x00013F14 File Offset: 0x00012114
		public RequestQueueManager()
		{
			this.injectionQueues = new ConcurrentDictionary<Guid, RequestQueue>();
			this.processingQueues = new ConcurrentDictionary<Guid, RequestQueue>();
			this.queueMonitor = new Timer(new TimerCallback(this.VerifyQueues), null, TimeSpan.Zero, TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00013F68 File Offset: 0x00012168
		public IRequestQueue MainProcessingQueue
		{
			get
			{
				return this.GetProcessingQueue(Guid.Empty, "Main");
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00013F7C File Offset: 0x0001217C
		public IRequestQueue GetInjectionQueue(DirectoryDatabase database)
		{
			RequestQueue requestQueue;
			if (!this.injectionQueues.TryGetValue(database.Guid, out requestQueue))
			{
				InjectionQueueCounters counters = new InjectionQueueCounters(database.Name);
				RequestQueue value = new RequestQueue(database.Guid, counters);
				this.injectionQueues.TryAdd(database.Guid, value);
			}
			return this.injectionQueues[database.Guid];
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00013FDB File Offset: 0x000121DB
		public IRequestQueue GetProcessingQueue(LoadEntity loadObject)
		{
			return this.GetProcessingQueue(loadObject.Guid, loadObject.Name);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00013FEF File Offset: 0x000121EF
		public IRequestQueue GetProcessingQueue(DirectoryObject directoryObject)
		{
			return this.GetProcessingQueue(directoryObject.Guid, directoryObject.Name);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00014040 File Offset: 0x00012240
		public QueueManagerDiagnosticData GetDiagnosticData(bool includeRequestDetails, bool includeRequestVerboseData)
		{
			return new QueueManagerDiagnosticData
			{
				InjectionQueues = (from iq in this.injectionQueues
				select iq.Value.GetDiagnosticData(includeRequestDetails, includeRequestVerboseData)).ToList<QueueDiagnosticData>(),
				ProcessingQueues = (from pq in this.processingQueues
				select pq.Value.GetDiagnosticData(includeRequestDetails, includeRequestVerboseData)).ToList<QueueDiagnosticData>()
			};
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000140AC File Offset: 0x000122AC
		public void Clean()
		{
			foreach (IRequestQueue requestQueue in this.injectionQueues.Values)
			{
				requestQueue.Clean();
			}
			foreach (IRequestQueue requestQueue2 in this.processingQueues.Values)
			{
				requestQueue2.Clean();
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00014140 File Offset: 0x00012340
		private IRequestQueue GetProcessingQueue(Guid queueId, string queueName)
		{
			RequestQueue requestQueue;
			if (!this.processingQueues.TryGetValue(queueId, out requestQueue))
			{
				RequestQueue value = new RequestQueue(queueId, new ProcessingQueueCounters(queueName));
				this.processingQueues.TryAdd(queueId, value);
			}
			return this.processingQueues[queueId];
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00014184 File Offset: 0x00012384
		private void VerifyQueues(object state)
		{
			foreach (RequestQueue requestQueue in this.injectionQueues.Values)
			{
				requestQueue.EnsureThreadIsActiveIfNeeded();
			}
			foreach (RequestQueue requestQueue2 in this.processingQueues.Values)
			{
				requestQueue2.EnsureThreadIsActiveIfNeeded();
			}
		}

		// Token: 0x040002B3 RID: 691
		private readonly Timer queueMonitor;

		// Token: 0x040002B4 RID: 692
		private readonly ConcurrentDictionary<Guid, RequestQueue> injectionQueues;

		// Token: 0x040002B5 RID: 693
		private readonly ConcurrentDictionary<Guid, RequestQueue> processingQueues;
	}
}

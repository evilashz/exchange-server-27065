using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncQueueEntry<T> : IComparable<SyncQueueEntry<T>>
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x00018C78 File Offset: 0x00016E78
		public SyncQueueEntry(T item, ExDateTime nextPollingTime)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.item = item;
			this.nextPollingTime = nextPollingTime;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00018CA1 File Offset: 0x00016EA1
		public T Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00018CA9 File Offset: 0x00016EA9
		public ExDateTime NextPollingTime
		{
			get
			{
				return this.nextPollingTime;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00018CB4 File Offset: 0x00016EB4
		public int CompareTo(SyncQueueEntry<T> syncQueueEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("syncQueueEntry", syncQueueEntry);
			return this.NextPollingTime.UtcTicks.CompareTo(syncQueueEntry.NextPollingTime.UtcTicks);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00018CF4 File Offset: 0x00016EF4
		internal void AddDiagnosticInfoTo(XElement parentElement, string itemName)
		{
			XName name = itemName;
			T t = this.item;
			parentElement.Add(new XElement(name, t.ToString()));
			parentElement.Add(new XElement("nextPollingTime", this.nextPollingTime.ToString("o")));
		}

		// Token: 0x04000232 RID: 562
		private readonly T item;

		// Token: 0x04000233 RID: 563
		private readonly ExDateTime nextPollingTime;
	}
}

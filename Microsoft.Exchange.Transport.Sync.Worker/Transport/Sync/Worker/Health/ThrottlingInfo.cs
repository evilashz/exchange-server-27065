using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Health
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThrottlingInfo
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		internal ThrottlingInfo()
		{
			this.Cache = new Dictionary<SyncResourceMonitorType, Dictionary<ResourceLoadState, TimeSpan>>();
			foreach (object obj in Enum.GetValues(typeof(SyncResourceMonitorType)))
			{
				SyncResourceMonitorType key = (SyncResourceMonitorType)obj;
				Dictionary<ResourceLoadState, TimeSpan> dictionary = new Dictionary<ResourceLoadState, TimeSpan>();
				foreach (object obj2 in Enum.GetValues(typeof(ResourceLoadState)))
				{
					ResourceLoadState key2 = (ResourceLoadState)obj2;
					dictionary.Add(key2, TimeSpan.Zero);
				}
				this.Cache.Add(key, dictionary);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		internal Dictionary<SyncResourceMonitorType, Dictionary<ResourceLoadState, TimeSpan>> Cache { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000ACB1 File Offset: 0x00008EB1
		internal TimeSpan BackOffTime
		{
			get
			{
				return this.GetBackoffTime();
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000ACBC File Offset: 0x00008EBC
		internal void Add(SyncResourceMonitorType monitor, ResourceLoadState health, int backoff)
		{
			Dictionary<ResourceLoadState, TimeSpan> dictionary;
			TimeSpan timeSpan;
			if (this.Cache.TryGetValue(monitor, out dictionary) && dictionary.TryGetValue(health, out timeSpan))
			{
				timeSpan = dictionary[health];
				timeSpan += TimeSpan.FromMilliseconds((double)backoff);
				this.Cache[monitor][health] = timeSpan;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AD10 File Offset: 0x00008F10
		private TimeSpan GetBackoffTime()
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			if (this.Cache != null && this.Cache.Count != 0)
			{
				foreach (KeyValuePair<SyncResourceMonitorType, Dictionary<ResourceLoadState, TimeSpan>> keyValuePair in this.Cache)
				{
					foreach (KeyValuePair<ResourceLoadState, TimeSpan> keyValuePair2 in keyValuePair.Value)
					{
						timeSpan += keyValuePair2.Value;
					}
				}
			}
			return timeSpan;
		}
	}
}

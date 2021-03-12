using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x0200067D RID: 1661
	internal sealed class OnRemovedEventArgs<K, V> : EventArgs
	{
		// Token: 0x06001E18 RID: 7704 RVA: 0x000373F3 File Offset: 0x000355F3
		public OnRemovedEventArgs(K key, V value, CacheItemRemovedReason removalReason)
		{
			this.Key = key;
			this.Value = value;
			this.RemovalReason = removalReason;
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00037410 File Offset: 0x00035610
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x00037418 File Offset: 0x00035618
		public K Key { get; private set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00037421 File Offset: 0x00035621
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x00037429 File Offset: 0x00035629
		public V Value { get; private set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x00037432 File Offset: 0x00035632
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0003743A File Offset: 0x0003563A
		public CacheItemRemovedReason RemovalReason { get; private set; }
	}
}

using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x0200069D RID: 1693
	internal abstract class CacheEntryBase<K, T>
	{
		// Token: 0x06001F1D RID: 7965 RVA: 0x0003ABAE File Offset: 0x00038DAE
		protected CacheEntryBase(K key, T value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x0003ABC4 File Offset: 0x00038DC4
		internal T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0003ABCC File Offset: 0x00038DCC
		internal K Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0003ABDC File Offset: 0x00038DDC
		internal bool InShouldRemoveCycle { get; set; }

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0003ABE5 File Offset: 0x00038DE5
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x0003ABED File Offset: 0x00038DED
		internal CacheEntryBase<K, T> Next { get; set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0003ABF6 File Offset: 0x00038DF6
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x0003ABFE File Offset: 0x00038DFE
		internal CacheEntryBase<K, T> Previous { get; set; }

		// Token: 0x06001F26 RID: 7974
		internal abstract void OnTouch();

		// Token: 0x06001F27 RID: 7975
		internal abstract bool OnBeforeExpire();

		// Token: 0x06001F28 RID: 7976
		internal abstract void OnForceExtend();

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001F29 RID: 7977
		internal abstract DateTime ExpirationUtc { get; }

		// Token: 0x04001E8A RID: 7818
		private readonly K key;

		// Token: 0x04001E8B RID: 7819
		private readonly T value;
	}
}

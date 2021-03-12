using System;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x020004AE RID: 1198
	[__DynamicallyInvokable]
	[Serializable]
	public struct KeyValuePair<TKey, TValue>
	{
		// Token: 0x060039C8 RID: 14792 RVA: 0x000DB070 File Offset: 0x000D9270
		[__DynamicallyInvokable]
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x000DB080 File Offset: 0x000D9280
		[__DynamicallyInvokable]
		public TKey Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this.key;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x000DB088 File Offset: 0x000D9288
		[__DynamicallyInvokable]
		public TValue Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000DB090 File Offset: 0x000D9290
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (this.Key != null)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				TKey tkey = this.Key;
				stringBuilder2.Append(tkey.ToString());
			}
			stringBuilder.Append(", ");
			if (this.Value != null)
			{
				StringBuilder stringBuilder3 = stringBuilder;
				TValue tvalue = this.Value;
				stringBuilder3.Append(tvalue.ToString());
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x0400189D RID: 6301
		private TKey key;

		// Token: 0x0400189E RID: 6302
		private TValue value;
	}
}

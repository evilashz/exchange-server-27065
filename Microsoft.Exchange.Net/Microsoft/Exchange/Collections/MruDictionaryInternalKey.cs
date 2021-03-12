using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MruDictionaryInternalKey<T> : IComparer<MruDictionaryInternalKey<T>>, IComparable<MruDictionaryInternalKey<T>>
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000A25B File Offset: 0x0000845B
		public MruDictionaryInternalKey(T originalKey, IComparer<T> originalKeyComparer, DateTime lastAccessedTime)
		{
			this.LastAccessedTime = lastAccessedTime;
			this.OriginalKey = originalKey;
			this.OriginalKeyComparer = originalKeyComparer;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A278 File Offset: 0x00008478
		public int Compare(MruDictionaryInternalKey<T> x, MruDictionaryInternalKey<T> y)
		{
			int num = Comparer<DateTime>.Default.Compare(x.LastAccessedTime, y.LastAccessedTime);
			if (num == 0)
			{
				num = this.OriginalKeyComparer.Compare(x.OriginalKey, y.OriginalKey);
			}
			return num;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public int CompareTo(MruDictionaryInternalKey<T> other)
		{
			return this.Compare(this, other);
		}

		// Token: 0x04000159 RID: 345
		public readonly T OriginalKey;

		// Token: 0x0400015A RID: 346
		public readonly IComparer<T> OriginalKeyComparer;

		// Token: 0x0400015B RID: 347
		public readonly DateTime LastAccessedTime;
	}
}

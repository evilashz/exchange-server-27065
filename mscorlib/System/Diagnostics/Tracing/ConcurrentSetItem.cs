using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200040F RID: 1039
	internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
	{
		// Token: 0x060034BF RID: 13503
		public abstract int Compare(ItemType other);

		// Token: 0x060034C0 RID: 13504
		public abstract int Compare(KeyType key);
	}
}

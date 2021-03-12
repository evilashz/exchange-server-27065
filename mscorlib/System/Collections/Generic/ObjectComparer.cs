using System;

namespace System.Collections.Generic
{
	// Token: 0x02000490 RID: 1168
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06003914 RID: 14612 RVA: 0x000D946A File Offset: 0x000D766A
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000D9484 File Offset: 0x000D7684
		public override bool Equals(object obj)
		{
			ObjectComparer<T> objectComparer = obj as ObjectComparer<T>;
			return objectComparer != null;
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000D949C File Offset: 0x000D769C
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

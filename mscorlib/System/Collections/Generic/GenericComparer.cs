using System;

namespace System.Collections.Generic
{
	// Token: 0x0200048E RID: 1166
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x0600390C RID: 14604 RVA: 0x000D9397 File Offset: 0x000D7597
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000D93C8 File Offset: 0x000D75C8
		public override bool Equals(object obj)
		{
			GenericComparer<T> genericComparer = obj as GenericComparer<T>;
			return genericComparer != null;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000D93E0 File Offset: 0x000D75E0
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

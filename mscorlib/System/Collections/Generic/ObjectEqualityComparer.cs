using System;

namespace System.Collections.Generic
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06003968 RID: 14696 RVA: 0x000DA906 File Offset: 0x000D8B06
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000DA939 File Offset: 0x000D8B39
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000DA954 File Offset: 0x000D8B54
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000DA9C8 File Offset: 0x000D8BC8
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000DAA3C File Offset: 0x000D8C3C
		public override bool Equals(object obj)
		{
			ObjectEqualityComparer<T> objectEqualityComparer = obj as ObjectEqualityComparer<T>;
			return objectEqualityComparer != null;
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000DAA54 File Offset: 0x000D8C54
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

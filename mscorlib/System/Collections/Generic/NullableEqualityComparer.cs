using System;

namespace System.Collections.Generic
{
	// Token: 0x02000495 RID: 1173
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x06003961 RID: 14689 RVA: 0x000DA79A File Offset: 0x000D899A
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000DA7D5 File Offset: 0x000D89D5
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000DA7E4 File Offset: 0x000D89E4
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000DA85C File Offset: 0x000D8A5C
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000DA8D4 File Offset: 0x000D8AD4
		public override bool Equals(object obj)
		{
			NullableEqualityComparer<T> nullableEqualityComparer = obj as NullableEqualityComparer<T>;
			return nullableEqualityComparer != null;
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000DA8EC File Offset: 0x000D8AEC
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

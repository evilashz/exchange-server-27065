using System;

namespace System.Collections.Generic
{
	// Token: 0x02000494 RID: 1172
	[Serializable]
	internal class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x0600395A RID: 14682 RVA: 0x000DA644 File Offset: 0x000D8844
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000DA672 File Offset: 0x000D8872
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000DA68C File Offset: 0x000D888C
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

		// Token: 0x0600395D RID: 14685 RVA: 0x000DA6F8 File Offset: 0x000D88F8
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

		// Token: 0x0600395E RID: 14686 RVA: 0x000DA768 File Offset: 0x000D8968
		public override bool Equals(object obj)
		{
			GenericEqualityComparer<T> genericEqualityComparer = obj as GenericEqualityComparer<T>;
			return genericEqualityComparer != null;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000DA780 File Offset: 0x000D8980
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

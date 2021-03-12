using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000391 RID: 913
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Util
	{
		// Token: 0x06001626 RID: 5670 RVA: 0x00039303 File Offset: 0x00037503
		public static IEqualityComparer<TValue> CreateSelectorEqualityComparer<TValue, TKey>(Func<TValue, TKey> keySelector)
		{
			return Util.CreateSelectorEqualityComparer<TValue, TKey>(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00039310 File Offset: 0x00037510
		public static IEqualityComparer<TValue> CreateSelectorEqualityComparer<TValue, TKey>(Func<TValue, TKey> keySelector, IEqualityComparer<TKey> keyEqualityComparer)
		{
			return new Util.SelectorEqualityComparer<TValue, TKey>(keySelector, keyEqualityComparer);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00039319 File Offset: 0x00037519
		public static void DisposeIfPresent(IDisposable disposable)
		{
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00039324 File Offset: 0x00037524
		public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
		{
			ICollection<T> collection = enumerable as ICollection<T>;
			if (collection == null)
			{
				using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
				{
					return !enumerator.MoveNext();
				}
			}
			return collection.Count == 0;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00039374 File Offset: 0x00037574
		public static T[] MergeArrays<T>(params ICollection<T>[] collections)
		{
			int num = 0;
			foreach (ICollection<T> collection in collections)
			{
				if (collection != null)
				{
					num += collection.Count;
				}
			}
			T[] array = new T[num];
			int num2 = 0;
			foreach (ICollection<T> collection2 in collections)
			{
				if (collection2 != null)
				{
					collection2.CopyTo(array, num2);
					num2 += collection2.Count;
				}
			}
			return array;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000393EC File Offset: 0x000375EC
		public static void AppendToString<T>(StringBuilder stringBuilder, IEnumerable<T> collection)
		{
			if (collection != null)
			{
				bool flag = true;
				foreach (T t in collection)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append((t != null) ? t.ToString() : "null");
					flag = false;
				}
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00039468 File Offset: 0x00037668
		public static void AppendToString(StringBuilder stringBuilder, IEnumerable collection)
		{
			if (collection != null)
			{
				bool flag = true;
				foreach (object obj in collection)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append((obj != null) ? obj.ToString() : "null");
					flag = false;
				}
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000394E0 File Offset: 0x000376E0
		public static void AppendToString(StringBuilder stringBuilder, byte[] buffer)
		{
			if (buffer != null)
			{
				Util.AppendToString(stringBuilder, buffer, 0, buffer.Length);
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000394F0 File Offset: 0x000376F0
		public static void AppendToString(StringBuilder stringBuilder, byte[] buffer, int offset, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (i != 0 && i % 4 == 0)
				{
					stringBuilder.Append(" ");
				}
				byte b = buffer[offset + i];
				stringBuilder.Append("0123456789ABCDEF"[(b & 240) >> 4]);
				stringBuilder.Append("0123456789ABCDEF"[(int)(b & 15)]);
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00039554 File Offset: 0x00037754
		public static void AppendToString(StringBuilder stringBuilder, object value)
		{
			if (value == null)
			{
				stringBuilder.Append("null");
				return;
			}
			if (value is string)
			{
				stringBuilder.Append((string)value);
				return;
			}
			if (value is byte[])
			{
				Util.AppendToString(stringBuilder, (byte[])value);
				return;
			}
			if (value is IEnumerable)
			{
				Util.AppendToString(stringBuilder, (IEnumerable)value);
				return;
			}
			stringBuilder.Append(value);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000395B9 File Offset: 0x000377B9
		public static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000395C5 File Offset: 0x000377C5
		public static void ThrowOnNullOrEmptyArgument(string argument, string argumentName)
		{
			if (string.IsNullOrEmpty(argument))
			{
				throw new ArgumentNullException(argumentName, "Cannot be null or empty.");
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000395DB File Offset: 0x000377DB
		public static void ThrowOnIntPtrZero(IntPtr intPtr, string intPtrName)
		{
			if (object.Equals(intPtr, IntPtr.Zero))
			{
				throw new ArgumentException("Cannot be IntPtr.Zero", intPtrName);
			}
		}

		// Token: 0x04000B78 RID: 2936
		private const string HexDigits = "0123456789ABCDEF";

		// Token: 0x02000392 RID: 914
		private sealed class SelectorEqualityComparer<TValue, TKey> : IEqualityComparer<TValue>
		{
			// Token: 0x06001633 RID: 5683 RVA: 0x00039600 File Offset: 0x00037800
			public SelectorEqualityComparer(Func<TValue, TKey> keySelector, IEqualityComparer<TKey> keyEqualityComparer)
			{
				this.keySelector = keySelector;
				this.keyEqualityComparer = keyEqualityComparer;
			}

			// Token: 0x06001634 RID: 5684 RVA: 0x00039616 File Offset: 0x00037816
			public bool Equals(TValue x, TValue y)
			{
				return this.keyEqualityComparer.Equals(this.keySelector(x), this.keySelector(y));
			}

			// Token: 0x06001635 RID: 5685 RVA: 0x0003963B File Offset: 0x0003783B
			public int GetHashCode(TValue obj)
			{
				return this.keyEqualityComparer.GetHashCode(this.keySelector(obj));
			}

			// Token: 0x04000B79 RID: 2937
			private readonly Func<TValue, TKey> keySelector;

			// Token: 0x04000B7A RID: 2938
			private readonly IEqualityComparer<TKey> keyEqualityComparer;
		}
	}
}

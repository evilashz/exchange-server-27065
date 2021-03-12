using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009AB RID: 2475
	internal sealed class MapToCollectionAdapter
	{
		// Token: 0x0600630C RID: 25356 RVA: 0x00150CCB File Offset: 0x0014EECB
		private MapToCollectionAdapter()
		{
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x00150CD4 File Offset: 0x0014EED4
		[SecurityCritical]
		internal int Count<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMap<K, V> map = obj as IMap<K, V>;
			if (map != null)
			{
				uint size = map.Size;
				if (2147483647U < size)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				}
				return (int)size;
			}
			else
			{
				IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
				uint size2 = vector.Size;
				if (2147483647U < size2)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				return (int)size2;
			}
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x00150D3D File Offset: 0x0014EF3D
		[SecurityCritical]
		internal bool IsReadOnly<K, V>()
		{
			return false;
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x00150D40 File Offset: 0x0014EF40
		[SecurityCritical]
		internal void Add<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				dictionary.Add(item.Key, item.Value);
				return;
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			vector.Append(item);
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x00150D84 File Offset: 0x0014EF84
		[SecurityCritical]
		internal void Clear<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMap<K, V> map = obj as IMap<K, V>;
			if (map != null)
			{
				map.Clear();
				return;
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			vector.Clear();
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x00150DB8 File Offset: 0x0014EFB8
		[SecurityCritical]
		internal bool Contains<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				V x;
				return dictionary.TryGetValue(item.Key, out x) && EqualityComparer<V>.Default.Equals(x, item.Value);
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			uint num;
			return vector.IndexOf(item, out num);
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x00150E10 File Offset: 0x0014F010
		[SecurityCritical]
		internal void CopyTo<K, V>(KeyValuePair<K, V>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length <= arrayIndex && this.Count<K, V>() > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			if (array.Length - arrayIndex < this.Count<K, V>())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			IIterable<KeyValuePair<K, V>> iterable = JitHelpers.UnsafeCast<IIterable<KeyValuePair<K, V>>>(this);
			foreach (KeyValuePair<K, V> keyValuePair in iterable)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x00150EC0 File Offset: 0x0014F0C0
		[SecurityCritical]
		internal bool Remove<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				return dictionary.Remove(item.Key);
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return false;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			VectorToListAdapter.RemoveAtHelper<KeyValuePair<K, V>>(vector, num);
			return true;
		}
	}
}

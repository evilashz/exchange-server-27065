using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009AE RID: 2478
	internal sealed class DictionaryToMapAdapter
	{
		// Token: 0x06006325 RID: 25381 RVA: 0x001512EB File Offset: 0x0014F4EB
		private DictionaryToMapAdapter()
		{
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x001512F4 File Offset: 0x0014F4F4
		[SecurityCritical]
		internal V Lookup<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			V result;
			if (!dictionary.TryGetValue(key, out result))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return result;
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x00151334 File Offset: 0x0014F534
		[SecurityCritical]
		internal uint Size<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			return (uint)dictionary.Count;
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x00151350 File Offset: 0x0014F550
		[SecurityCritical]
		internal bool HasKey<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			return dictionary.ContainsKey(key);
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x0015136C File Offset: 0x0014F56C
		[SecurityCritical]
		internal IReadOnlyDictionary<K, V> GetView<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = dictionary as IReadOnlyDictionary<K, V>;
			if (readOnlyDictionary == null)
			{
				readOnlyDictionary = new ReadOnlyDictionary<K, V>(dictionary);
			}
			return readOnlyDictionary;
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x00151394 File Offset: 0x0014F594
		[SecurityCritical]
		internal bool Insert<K, V>(K key, V value)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			bool result = dictionary.ContainsKey(key);
			dictionary[key] = value;
			return result;
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001513BC File Offset: 0x0014F5BC
		[SecurityCritical]
		internal void Remove<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			if (!dictionary.Remove(key))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x001513F8 File Offset: 0x0014F5F8
		[SecurityCritical]
		internal void Clear<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			dictionary.Clear();
		}
	}
}

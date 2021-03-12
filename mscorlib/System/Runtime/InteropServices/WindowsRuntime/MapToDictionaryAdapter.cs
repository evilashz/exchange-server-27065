using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009AA RID: 2474
	internal sealed class MapToDictionaryAdapter
	{
		// Token: 0x06006301 RID: 25345 RVA: 0x00150A74 File Offset: 0x0014EC74
		private MapToDictionaryAdapter()
		{
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x00150A7C File Offset: 0x0014EC7C
		[SecurityCritical]
		internal V Indexer_Get<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> this2 = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			return MapToDictionaryAdapter.Lookup<K, V>(this2, key);
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x00150AAC File Offset: 0x0014ECAC
		[SecurityCritical]
		internal void Indexer_Set<K, V>(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> this2 = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			MapToDictionaryAdapter.Insert<K, V>(this2, key, value);
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x00150ADC File Offset: 0x0014ECDC
		[SecurityCritical]
		internal ICollection<K> Keys<K, V>()
		{
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			IDictionary<K, V> dictionary = (IDictionary<K, V>)map;
			return new DictionaryKeyCollection<K, V>(dictionary);
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x00150B00 File Offset: 0x0014ED00
		[SecurityCritical]
		internal ICollection<V> Values<K, V>()
		{
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			IDictionary<K, V> dictionary = (IDictionary<K, V>)map;
			return new DictionaryValueCollection<K, V>(dictionary);
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x00150B24 File Offset: 0x0014ED24
		[SecurityCritical]
		internal bool ContainsKey<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			return map.HasKey(key);
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x00150B54 File Offset: 0x0014ED54
		[SecurityCritical]
		internal void Add<K, V>(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.ContainsKey<K, V>(key))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate"));
			}
			IMap<K, V> this2 = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			MapToDictionaryAdapter.Insert<K, V>(this2, key, value);
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x00150BA0 File Offset: 0x0014EDA0
		[SecurityCritical]
		internal bool Remove<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			if (!map.HasKey(key))
			{
				return false;
			}
			bool result;
			try
			{
				map.Remove(key);
				result = true;
			}
			catch (Exception ex)
			{
				if (-2147483637 != ex._HResult)
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00150C04 File Offset: 0x0014EE04
		[SecurityCritical]
		internal bool TryGetValue<K, V>(K key, out V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			if (!map.HasKey(key))
			{
				value = default(V);
				return false;
			}
			bool result;
			try
			{
				value = MapToDictionaryAdapter.Lookup<K, V>(map, key);
				result = true;
			}
			catch (KeyNotFoundException)
			{
				value = default(V);
				result = false;
			}
			return result;
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00150C6C File Offset: 0x0014EE6C
		private static V Lookup<K, V>(IMap<K, V> _this, K key)
		{
			V result;
			try
			{
				result = _this.Lookup(key);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x00150CB4 File Offset: 0x0014EEB4
		private static bool Insert<K, V>(IMap<K, V> _this, K key, V value)
		{
			return _this.Insert(key, value);
		}
	}
}

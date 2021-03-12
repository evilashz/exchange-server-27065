using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B7 RID: 2487
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class IMapViewToIReadOnlyDictionaryAdapter
	{
		// Token: 0x06006368 RID: 25448 RVA: 0x00151FA2 File Offset: 0x001501A2
		private IMapViewToIReadOnlyDictionaryAdapter()
		{
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x00151FAC File Offset: 0x001501AC
		[SecurityCritical]
		internal V Indexer_Get<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> this2 = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			return IMapViewToIReadOnlyDictionaryAdapter.Lookup<K, V>(this2, key);
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x00151FDC File Offset: 0x001501DC
		[SecurityCritical]
		internal IEnumerable<K> Keys<K, V>()
		{
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			IReadOnlyDictionary<K, V> dictionary = (IReadOnlyDictionary<K, V>)mapView;
			return new ReadOnlyDictionaryKeyCollection<K, V>(dictionary);
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x00152000 File Offset: 0x00150200
		[SecurityCritical]
		internal IEnumerable<V> Values<K, V>()
		{
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			IReadOnlyDictionary<K, V> dictionary = (IReadOnlyDictionary<K, V>)mapView;
			return new ReadOnlyDictionaryValueCollection<K, V>(dictionary);
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x00152024 File Offset: 0x00150224
		[SecurityCritical]
		internal bool ContainsKey<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			return mapView.HasKey(key);
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x00152054 File Offset: 0x00150254
		[SecurityCritical]
		internal bool TryGetValue<K, V>(K key, out V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			if (!mapView.HasKey(key))
			{
				value = default(V);
				return false;
			}
			bool result;
			try
			{
				value = mapView.Lookup(key);
				result = true;
			}
			catch (Exception ex)
			{
				if (-2147483637 != ex._HResult)
				{
					throw;
				}
				value = default(V);
				result = false;
			}
			return result;
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x001520CC File Offset: 0x001502CC
		private static V Lookup<K, V>(IMapView<K, V> _this, K key)
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
	}
}

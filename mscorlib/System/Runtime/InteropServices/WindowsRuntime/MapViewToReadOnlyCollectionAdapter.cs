using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009AC RID: 2476
	internal sealed class MapViewToReadOnlyCollectionAdapter
	{
		// Token: 0x06006314 RID: 25364 RVA: 0x00150F24 File Offset: 0x0014F124
		private MapViewToReadOnlyCollectionAdapter()
		{
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x00150F2C File Offset: 0x0014F12C
		[SecurityCritical]
		internal int Count<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMapView<K, V> mapView = obj as IMapView<K, V>;
			if (mapView != null)
			{
				uint size = mapView.Size;
				if (2147483647U < size)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				}
				return (int)size;
			}
			else
			{
				IVectorView<KeyValuePair<K, V>> vectorView = JitHelpers.UnsafeCast<IVectorView<KeyValuePair<K, V>>>(this);
				uint size2 = vectorView.Size;
				if (2147483647U < size2)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				return (int)size2;
			}
		}
	}
}

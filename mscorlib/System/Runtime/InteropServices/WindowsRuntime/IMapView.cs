using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F2 RID: 2546
	[Guid("e480ce40-a338-4ada-adcf-272272e48cb9")]
	[ComImport]
	internal interface IMapView<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x060064DE RID: 25822
		V Lookup(K key);

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060064DF RID: 25823
		uint Size { get; }

		// Token: 0x060064E0 RID: 25824
		bool HasKey(K key);

		// Token: 0x060064E1 RID: 25825
		void Split(out IMapView<K, V> first, out IMapView<K, V> second);
	}
}

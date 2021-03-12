using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F1 RID: 2545
	[Guid("3c2925fe-8519-45c1-aa79-197b6718c1c1")]
	[ComImport]
	internal interface IMap<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x060064D7 RID: 25815
		V Lookup(K key);

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060064D8 RID: 25816
		uint Size { get; }

		// Token: 0x060064D9 RID: 25817
		bool HasKey(K key);

		// Token: 0x060064DA RID: 25818
		IReadOnlyDictionary<K, V> GetView();

		// Token: 0x060064DB RID: 25819
		bool Insert(K key, V value);

		// Token: 0x060064DC RID: 25820
		void Remove(K key);

		// Token: 0x060064DD RID: 25821
		void Clear();
	}
}

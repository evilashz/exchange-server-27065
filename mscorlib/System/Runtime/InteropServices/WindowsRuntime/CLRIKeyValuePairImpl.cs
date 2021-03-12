using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F4 RID: 2548
	internal sealed class CLRIKeyValuePairImpl<K, V> : IKeyValuePair<K, V>
	{
		// Token: 0x060064E4 RID: 25828 RVA: 0x00155100 File Offset: 0x00153300
		public CLRIKeyValuePairImpl([In] ref KeyValuePair<K, V> pair)
		{
			this._pair = pair;
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x00155114 File Offset: 0x00153314
		public K Key
		{
			get
			{
				return this._pair.Key;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x00155130 File Offset: 0x00153330
		public V Value
		{
			get
			{
				return this._pair.Value;
			}
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x0015514C File Offset: 0x0015334C
		internal static object BoxHelper(object pair)
		{
			KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>)pair;
			return new CLRIKeyValuePairImpl<K, V>(ref keyValuePair);
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x00155168 File Offset: 0x00153368
		internal static object UnboxHelper(object wrapper)
		{
			CLRIKeyValuePairImpl<K, V> clrikeyValuePairImpl = (CLRIKeyValuePairImpl<K, V>)wrapper;
			return clrikeyValuePairImpl._pair;
		}

		// Token: 0x060064E9 RID: 25833 RVA: 0x00155188 File Offset: 0x00153388
		public override string ToString()
		{
			return this._pair.ToString();
		}

		// Token: 0x04002C89 RID: 11401
		private readonly KeyValuePair<K, V> _pair;
	}
}

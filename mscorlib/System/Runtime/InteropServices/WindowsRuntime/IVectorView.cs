using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EE RID: 2542
	[Guid("bbe1fa4c-b0e3-4583-baef-1f1b2e483e56")]
	[ComImport]
	internal interface IVectorView<T> : IIterable<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060064C6 RID: 25798
		T GetAt(uint index);

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060064C7 RID: 25799
		uint Size { get; }

		// Token: 0x060064C8 RID: 25800
		bool IndexOf(T value, out uint index);

		// Token: 0x060064C9 RID: 25801
		uint GetMany(uint startIndex, [Out] T[] items);
	}
}

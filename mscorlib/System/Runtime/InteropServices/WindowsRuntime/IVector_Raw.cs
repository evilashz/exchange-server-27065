using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009ED RID: 2541
	[Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
	[ComImport]
	internal interface IVector_Raw<T> : IIterable<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060064BA RID: 25786
		T GetAt(uint index);

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060064BB RID: 25787
		uint Size { get; }

		// Token: 0x060064BC RID: 25788
		IVectorView<T> GetView();

		// Token: 0x060064BD RID: 25789
		bool IndexOf(T value, out uint index);

		// Token: 0x060064BE RID: 25790
		void SetAt(uint index, T value);

		// Token: 0x060064BF RID: 25791
		void InsertAt(uint index, T value);

		// Token: 0x060064C0 RID: 25792
		void RemoveAt(uint index);

		// Token: 0x060064C1 RID: 25793
		void Append(T value);

		// Token: 0x060064C2 RID: 25794
		void RemoveAtEnd();

		// Token: 0x060064C3 RID: 25795
		void Clear();

		// Token: 0x060064C4 RID: 25796
		uint GetMany(uint startIndex, [Out] T[] items);

		// Token: 0x060064C5 RID: 25797
		void ReplaceAll(T[] items);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EC RID: 2540
	[Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
	[ComImport]
	internal interface IVector<T> : IIterable<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060064AE RID: 25774
		T GetAt(uint index);

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060064AF RID: 25775
		uint Size { get; }

		// Token: 0x060064B0 RID: 25776
		IReadOnlyList<T> GetView();

		// Token: 0x060064B1 RID: 25777
		bool IndexOf(T value, out uint index);

		// Token: 0x060064B2 RID: 25778
		void SetAt(uint index, T value);

		// Token: 0x060064B3 RID: 25779
		void InsertAt(uint index, T value);

		// Token: 0x060064B4 RID: 25780
		void RemoveAt(uint index);

		// Token: 0x060064B5 RID: 25781
		void Append(T value);

		// Token: 0x060064B6 RID: 25782
		void RemoveAtEnd();

		// Token: 0x060064B7 RID: 25783
		void Clear();

		// Token: 0x060064B8 RID: 25784
		uint GetMany(uint startIndex, [Out] T[] items);

		// Token: 0x060064B9 RID: 25785
		void ReplaceAll(T[] items);
	}
}

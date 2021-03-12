using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EA RID: 2538
	[Guid("6a79e863-4300-459a-9966-cbb660963ee1")]
	[ComImport]
	internal interface IIterator<T>
	{
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060064A7 RID: 25767
		T Current { get; }

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060064A8 RID: 25768
		bool HasCurrent { get; }

		// Token: 0x060064A9 RID: 25769
		bool MoveNext();

		// Token: 0x060064AA RID: 25770
		int GetMany([Out] T[] items);
	}
}

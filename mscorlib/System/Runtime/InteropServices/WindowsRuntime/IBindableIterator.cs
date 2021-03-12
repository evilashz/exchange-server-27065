using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EB RID: 2539
	[Guid("6a1d6c07-076d-49f2-8314-f52c9c9a8331")]
	[ComImport]
	internal interface IBindableIterator
	{
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060064AB RID: 25771
		object Current { get; }

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060064AC RID: 25772
		bool HasCurrent { get; }

		// Token: 0x060064AD RID: 25773
		bool MoveNext();
	}
}

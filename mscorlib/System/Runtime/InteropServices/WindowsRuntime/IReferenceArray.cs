using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DC RID: 2524
	[Guid("61c17707-2d65-11e0-9ae8-d48564015472")]
	[ComImport]
	internal interface IReferenceArray<T> : IPropertyValue
	{
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x0600645F RID: 25695
		T[] Value { get; }
	}
}

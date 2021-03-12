using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DB RID: 2523
	[Guid("61c17706-2d65-11e0-9ae8-d48564015472")]
	[ComImport]
	internal interface IReference<T> : IPropertyValue
	{
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x0600645E RID: 25694
		T Value { get; }
	}
}

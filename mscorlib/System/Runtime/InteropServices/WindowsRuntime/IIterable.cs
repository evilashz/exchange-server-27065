using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E8 RID: 2536
	[Guid("faa585ea-6214-4217-afda-7f46de5869b3")]
	[ComImport]
	internal interface IIterable<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060064A5 RID: 25765
		IIterator<T> First();
	}
}

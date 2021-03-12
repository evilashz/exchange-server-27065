using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000471 RID: 1137
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IEnumerable
	{
		// Token: 0x0600379B RID: 14235
		[DispId(-4)]
		[__DynamicallyInvokable]
		IEnumerator GetEnumerator();
	}
}

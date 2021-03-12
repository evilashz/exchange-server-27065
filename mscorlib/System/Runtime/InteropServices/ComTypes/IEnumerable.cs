using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009FA RID: 2554
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x060064FF RID: 25855
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}

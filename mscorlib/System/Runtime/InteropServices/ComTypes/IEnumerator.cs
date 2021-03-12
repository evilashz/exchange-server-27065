using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009FB RID: 2555
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x06006500 RID: 25856
		bool MoveNext();

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06006501 RID: 25857
		object Current { get; }

		// Token: 0x06006502 RID: 25858
		void Reset();
	}
}

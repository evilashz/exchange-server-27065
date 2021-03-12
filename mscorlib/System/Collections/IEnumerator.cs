using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000472 RID: 1138
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IEnumerator
	{
		// Token: 0x0600379C RID: 14236
		[__DynamicallyInvokable]
		bool MoveNext();

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600379D RID: 14237
		[__DynamicallyInvokable]
		object Current { [__DynamicallyInvokable] get; }

		// Token: 0x0600379E RID: 14238
		[__DynamicallyInvokable]
		void Reset();
	}
}

using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F3 RID: 2547
	[Guid("02b51929-c1c4-4a7e-8940-0312b5c18500")]
	[ComImport]
	internal interface IKeyValuePair<K, V>
	{
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060064E2 RID: 25826
		K Key { get; }

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060064E3 RID: 25827
		V Value { get; }
	}
}

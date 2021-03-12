using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F0 RID: 2544
	[Guid("346dd6e7-976e-4bc3-815d-ece243bc0f33")]
	[ComImport]
	internal interface IBindableVectorView : IBindableIterable
	{
		// Token: 0x060064D4 RID: 25812
		object GetAt(uint index);

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060064D5 RID: 25813
		uint Size { get; }

		// Token: 0x060064D6 RID: 25814
		bool IndexOf(object value, out uint index);
	}
}

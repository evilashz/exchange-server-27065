using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Misc
{
	// Token: 0x0200001E RID: 30
	[Guid("00000100-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IEnumUnknown
	{
		// Token: 0x060000EC RID: 236
		void RemoteNext([In] int celt, [MarshalAs(UnmanagedType.IUnknown)] out object rgelt, out int pceltFetched);

		// Token: 0x060000ED RID: 237
		void Skip([In] int celt);

		// Token: 0x060000EE RID: 238
		void Reset();

		// Token: 0x060000EF RID: 239
		void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppenum);
	}
}

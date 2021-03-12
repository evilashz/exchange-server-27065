using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Misc
{
	// Token: 0x0200001D RID: 29
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000101-0000-0000-C000-000000000046")]
	internal interface IEnumString
	{
		// Token: 0x060000E8 RID: 232
		[PreserveSig]
		int RemoteNext([In] int celt, [MarshalAs(UnmanagedType.LPWStr)] out string rgelt, out int pceltFetched);

		// Token: 0x060000E9 RID: 233
		[PreserveSig]
		int Skip([In] int celt);

		// Token: 0x060000EA RID: 234
		void Reset();

		// Token: 0x060000EB RID: 235
		void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumString ppenum);
	}
}

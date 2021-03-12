using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000035 RID: 53
	[Guid("3C613A02-34B2-44EA-9A7C-45AEA9C6FD6D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICColorContext
	{
		// Token: 0x0600018F RID: 399
		void InitializeFromFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string wzFilename);

		// Token: 0x06000190 RID: 400
		void InitializeFromMemory([In] ref byte pbBuffer, [In] int cbBufferSize);

		// Token: 0x06000191 RID: 401
		void InitializeFromExifColorSpace([In] int value);

		// Token: 0x06000192 RID: 402
		void GetType(out WICColorContextType pType);

		// Token: 0x06000193 RID: 403
		void GetProfileBytes([In] int cbBuffer, [In] [Out] ref byte pbBuffer, out int pcbActual);

		// Token: 0x06000194 RID: 404
		void GetExifColorSpace(out int pValue);
	}
}

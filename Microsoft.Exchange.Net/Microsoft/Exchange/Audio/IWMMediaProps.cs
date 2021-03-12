using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200062D RID: 1581
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("96406BCE-2B2B-11d3-B36B-00C04F6108FF")]
	[ComImport]
	internal interface IWMMediaProps
	{
		// Token: 0x06001CB6 RID: 7350
		void GetType(out Guid pguidType);

		// Token: 0x06001CB7 RID: 7351
		void GetMediaType(IntPtr pType, [In] [Out] ref uint pcbType);

		// Token: 0x06001CB8 RID: 7352
		void SetMediaType([In] ref WindowsMediaNativeMethods.WM_MEDIA_TYPE pType);
	}
}

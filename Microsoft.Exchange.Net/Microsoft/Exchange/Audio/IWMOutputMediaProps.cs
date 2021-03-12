using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200062E RID: 1582
	[Guid("96406BD7-2B2B-11d3-B36B-00C04F6108FF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IWMOutputMediaProps : IWMMediaProps
	{
		// Token: 0x06001CB9 RID: 7353
		void GetType(out Guid pguidType);

		// Token: 0x06001CBA RID: 7354
		void GetMediaType(IntPtr pType, [In] [Out] ref uint pcbType);

		// Token: 0x06001CBB RID: 7355
		void SetMediaType([In] ref WindowsMediaNativeMethods.WM_MEDIA_TYPE pType);

		// Token: 0x06001CBC RID: 7356
		void GetStreamGroupName();

		// Token: 0x06001CBD RID: 7357
		void GetConnectionName();
	}
}

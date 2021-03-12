using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200062F RID: 1583
	[Guid("96406BD5-2B2B-11d3-B36B-00C04F6108FF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IWMInputMediaProps : IWMMediaProps
	{
		// Token: 0x06001CBE RID: 7358
		void GetType(out Guid pguidType);

		// Token: 0x06001CBF RID: 7359
		void GetMediaType(IntPtr pType, [In] [Out] ref uint pcbType);

		// Token: 0x06001CC0 RID: 7360
		void SetMediaType([In] ref WindowsMediaNativeMethods.WM_MEDIA_TYPE pType);

		// Token: 0x06001CC1 RID: 7361
		void GetConnectionName();

		// Token: 0x06001CC2 RID: 7362
		void GetGroupName();
	}
}

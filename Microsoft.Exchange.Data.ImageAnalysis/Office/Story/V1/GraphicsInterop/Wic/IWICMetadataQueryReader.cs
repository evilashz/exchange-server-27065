using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200003A RID: 58
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("30989668-E1C9-4597-B395-458EEDB808DF")]
	internal interface IWICMetadataQueryReader
	{
		// Token: 0x060001BD RID: 445
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x060001BE RID: 446
		void GetLocation([In] int cchMaxLength, [In] [Out] ref ushort wzNamespace, out int pcchActualLength);

		// Token: 0x060001BF RID: 447
		[PreserveSig]
		int GetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, out PROPVARIANT pvarValue);

		// Token: 0x060001C0 RID: 448
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IEnumString ppIEnumString);
	}
}

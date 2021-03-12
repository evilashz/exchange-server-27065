using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200003B RID: 59
	[Guid("A721791A-0DEF-4D06-BD91-2118BF1DB10B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICMetadataQueryWriter : IWICMetadataQueryReader
	{
		// Token: 0x060001C1 RID: 449
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x060001C2 RID: 450
		void GetLocation([In] int cchMaxLength, [In] [Out] ref ushort wzNamespace, out int pcchActualLength);

		// Token: 0x060001C3 RID: 451
		void GetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, out PROPVARIANT pvarValue);

		// Token: 0x060001C4 RID: 452
		void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out IEnumString ppIEnumString);

		// Token: 0x060001C5 RID: 453
		void SetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, [In] ref PROPVARIANT pvarValue);

		// Token: 0x060001C6 RID: 454
		void RemoveMetadataByName([MarshalAs(UnmanagedType.LPWStr)] [In] string wzName);
	}
}

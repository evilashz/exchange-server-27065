using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020009AB RID: 2475
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("175EF0A4-8EB8-49ac-9049-F40EC69EC0A7")]
	[ComImport]
	internal interface I_IrmPolicyInfoRMS
	{
		// Token: 0x060035AD RID: 13741
		[PreserveSig]
		int HrGetICrypt([MarshalAs(UnmanagedType.Interface)] out object piic);

		// Token: 0x060035AE RID: 13742
		[PreserveSig]
		int HrGetSignedIL([MarshalAs(UnmanagedType.BStr)] out string pbstrIL);

		// Token: 0x060035AF RID: 13743
		[PreserveSig]
		int HrGetServerId([MarshalAs(UnmanagedType.BStr)] out string pbstrServerId);

		// Token: 0x060035B0 RID: 13744
		[PreserveSig]
		int HrGetEULs([In] IntPtr rgbstrEUL, [In] IntPtr rgbstrId, [MarshalAs(UnmanagedType.U4)] out uint pcbEULs);

		// Token: 0x060035B1 RID: 13745
		[PreserveSig]
		int HrSetSignedIL([MarshalAs(UnmanagedType.BStr)] [In] string bstrIL);

		// Token: 0x060035B2 RID: 13746
		[PreserveSig]
		int HrSetServerEUL([MarshalAs(UnmanagedType.BStr)] [In] string bstrEUL);

		// Token: 0x060035B3 RID: 13747
		[PreserveSig]
		int HrGetRightsTemplate([MarshalAs(UnmanagedType.BStr)] out string pbstrRightsTemplate);

		// Token: 0x060035B4 RID: 13748
		[PreserveSig]
		int HrGetListGuid([MarshalAs(UnmanagedType.BStr)] out string pbstrListGuid);
	}
}

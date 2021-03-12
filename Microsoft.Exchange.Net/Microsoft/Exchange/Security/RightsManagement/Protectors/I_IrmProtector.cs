using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020009AF RID: 2479
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("FCFBC0AC-672B-452D-80E5-40652503D96E")]
	[ComImport]
	internal interface I_IrmProtector
	{
		// Token: 0x060035C4 RID: 13764
		[PreserveSig]
		int HrInit([MarshalAs(UnmanagedType.BStr)] out string pbstrProduct, [MarshalAs(UnmanagedType.U4)] out int pdwVersion, [MarshalAs(UnmanagedType.BStr)] out string pbstrExtentions, [MarshalAs(UnmanagedType.Bool)] out bool pfUseRMS);

		// Token: 0x060035C5 RID: 13765
		[PreserveSig]
		int HrIsProtected([MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbInput, [MarshalAs(UnmanagedType.I4)] out MsoIpiResult pdwResult);

		// Token: 0x060035C6 RID: 13766
		[PreserveSig]
		int HrSetLangId([MarshalAs(UnmanagedType.U2)] [In] ushort langid);

		// Token: 0x060035C7 RID: 13767
		[PreserveSig]
		int HrProtectRMS([MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbInput, [MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbOutput, [MarshalAs(UnmanagedType.Interface)] [In] I_IrmPolicyInfoRMS piid, [MarshalAs(UnmanagedType.I4)] out MsoIpiStatus pdwStatus);

		// Token: 0x060035C8 RID: 13768
		[PreserveSig]
		int HrUnprotectRMS([MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbInput, [MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbOutput, [MarshalAs(UnmanagedType.Interface)] [In] I_IrmPolicyInfoRMS piid, [MarshalAs(UnmanagedType.I4)] out MsoIpiStatus pdwStatus);

		// Token: 0x060035C9 RID: 13769
		[PreserveSig]
		int HrProtect([MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbInput, [MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbOutput, [MarshalAs(UnmanagedType.Interface)] [In] object piid, [MarshalAs(UnmanagedType.I4)] out MsoIpiStatus pdwStatus);

		// Token: 0x060035CA RID: 13770
		[PreserveSig]
		int HrUnprotect([MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbInput, [MarshalAs(UnmanagedType.Interface)] [In] ILockBytes pilbOutput, [MarshalAs(UnmanagedType.Interface)] [In] object piid, [MarshalAs(UnmanagedType.I4)] out MsoIpiStatus pdwStatus);
	}
}

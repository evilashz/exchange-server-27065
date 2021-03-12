using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200026B RID: 619
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("2DC76CDD-1AA6-4157-808F-E68D2AD29FE8")]
	[ComImport]
	internal interface IExchangeExportHierManifestEx
	{
		// Token: 0x06000AB2 RID: 2738
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AB3 RID: 2739
		[PreserveSig]
		unsafe int Config([MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, SyncConfigFlags flags, [In] IExchangeHierManifestCallback pCallback, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps);

		// Token: 0x06000AB4 RID: 2740
		[PreserveSig]
		int Synchronize(int ulFlags);

		// Token: 0x06000AB5 RID: 2741
		[PreserveSig]
		int GetState(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen);

		// Token: 0x06000AB6 RID: 2742
		[PreserveSig]
		int Checkpoint([MarshalAs(UnmanagedType.LPArray)] byte[] pbCheckpointIdsetGiven, int cbCheckpointIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCheckpointCnsetSeen, int cbCheckpointCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeFids, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeCns, [MarshalAs(UnmanagedType.LPArray)] [In] long[] deleteMids, out SafeExMemoryHandle pbIdsetGiven, out int cbIdsetGiven, out SafeExMemoryHandle pbCnsetSeen, out int cbCnsetSeen);
	}
}

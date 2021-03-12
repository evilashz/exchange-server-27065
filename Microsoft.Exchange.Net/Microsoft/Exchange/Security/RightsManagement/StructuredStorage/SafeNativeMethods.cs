using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A1E RID: 2590
	internal static class SafeNativeMethods
	{
		// Token: 0x0600388F RID: 14479
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern ZLib.ErrorCode rms_deflate([In] [Out] ref ZLib.ZStream stream, [In] int flush);

		// Token: 0x06003890 RID: 14480
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern ZLib.ErrorCode rms_deflate_init([In] [Out] ref ZLib.ZStream stream, [In] int level, [MarshalAs(UnmanagedType.LPStr)] [In] string version, [In] int zStreamStructSize);

		// Token: 0x06003891 RID: 14481
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern ZLib.ErrorCode rms_inflate([In] [Out] ref ZLib.ZStream stream, [In] int flush);

		// Token: 0x06003892 RID: 14482
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern ZLib.ErrorCode rms_inflate_init([In] [Out] ref ZLib.ZStream stream, [MarshalAs(UnmanagedType.LPStr)] [In] string version, [In] int zStreamStructSize);

		// Token: 0x06003893 RID: 14483
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern ZLib.ErrorCode rms_inflateEnd([In] [Out] ref ZLib.ZStream stream);

		// Token: 0x06003894 RID: 14484
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern ZLib.ErrorCode rms_deflateEnd([In] [Out] ref ZLib.ZStream stream);

		// Token: 0x06003895 RID: 14485
		[DllImport("user32.dll")]
		public static extern SafeIconHandle LoadIcon(IntPtr hInstance, IntPtr lpIconName);

		// Token: 0x06003896 RID: 14486
		[DllImport("ole32.dll")]
		public static extern SafeWin32HGlobalHandle OleMetafilePictFromIconAndLabel(SafeIconHandle iconHandle, [MarshalAs(UnmanagedType.LPWStr)] string lpszLabel, [MarshalAs(UnmanagedType.LPWStr)] string lpszSourceFile, uint iIconIndex);

		// Token: 0x06003897 RID: 14487
		[DllImport("ole32.dll")]
		public static extern SafeWin32HGlobalHandle OleGetIconOfFile([MarshalAs(UnmanagedType.LPWStr)] string lpszLabel, [MarshalAs(UnmanagedType.Bool)] bool fUseFileAsLabel);

		// Token: 0x06003898 RID: 14488
		[DllImport("gdi32.dll")]
		public static extern uint GetMetaFileBitsEx(IntPtr hmf, uint nSize, [Out] byte[] lpvData);

		// Token: 0x06003899 RID: 14489
		[DllImport("kernel32.dll")]
		public static extern IntPtr GlobalLock(SafeWin32HGlobalHandle globalHandle);

		// Token: 0x0600389A RID: 14490
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GlobalUnlock(SafeWin32HGlobalHandle globalHandle);

		// Token: 0x0600389B RID: 14491
		[DllImport("ole32.dll")]
		public static extern int StgCreateDocfileOnILockBytes(ILockBytes plkbyt, uint grfMode, uint reserved, out IStorage ppstgOpen);

		// Token: 0x0600389C RID: 14492
		[DllImport("ole32.dll")]
		public static extern int StgIsStorageILockBytes(ILockBytes plkbyt);

		// Token: 0x0600389D RID: 14493
		[DllImport("ole32.dll")]
		public static extern int StgOpenStorageOnILockBytes(ILockBytes plkbyt, IStorage pStgPriority, uint grfMode, IntPtr snbEnclude, uint reserved, out IStorage ppstgOpen);

		// Token: 0x0600389E RID: 14494
		[DllImport("RightsManagementWrapper.dll")]
		public static extern int WrapEncryptedStorage([MarshalAs(UnmanagedType.Interface)] [In] IStream stream, [In] SafeRightsManagementHandle encryptorHandle, [In] SafeRightsManagementHandle decryptorHandle, [MarshalAs(UnmanagedType.Bool)] [In] bool create, [MarshalAs(UnmanagedType.Interface)] out IStorage encryptedStorage);

		// Token: 0x0600389F RID: 14495
		[DllImport("RightsManagementWrapper.dll")]
		public static extern int WrapStreamWithEncryptingStream([MarshalAs(UnmanagedType.Interface)] [In] IStream stream, [In] SafeRightsManagementHandle encryptorHandle, [In] SafeRightsManagementHandle decryptorHandle, [MarshalAs(UnmanagedType.Interface)] out IStream encryptedStream);

		// Token: 0x060038A0 RID: 14496
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern int EnsureDRMEnvironmentInitialized([MarshalAs(UnmanagedType.U4)] [In] uint eSecurityProviderType, [MarshalAs(UnmanagedType.U4)] [In] uint eSpecification, [MarshalAs(UnmanagedType.LPWStr)] [In] string securityProvider, [MarshalAs(UnmanagedType.LPWStr)] [In] string manifestCredentials, [MarshalAs(UnmanagedType.LPWStr)] [In] string machineCredentials, out SafeRightsManagementEnvironmentHandle environmentHandle, out SafeRightsManagementHandle defaultLibrary);

		// Token: 0x060038A1 RID: 14497
		[DllImport("RightsManagementWrapper.dll")]
		internal static extern int HmfpSetRenderBits([MarshalAs(UnmanagedType.U4)] [In] uint count, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] bytes, out SafeIconHandle iconHandle);
	}
}

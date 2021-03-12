using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B5 RID: 693
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExExportManifestExHandle : SafeExInterfaceHandle
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003425F File Offset: 0x0003245F
		protected SafeExExportManifestExHandle()
		{
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00034267 File Offset: 0x00032467
		internal SafeExExportManifestExHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00034270 File Offset: 0x00032470
		internal SafeExExportManifestExHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00034279 File Offset: 0x00032479
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExExportManifestExHandle>(this);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00034284 File Offset: 0x00032484
		internal unsafe int Config(byte[] pbIdsetGiven, int cbIdsetGiven, byte[] pbCnsetSeen, int cbCnsetSeen, byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, byte[] pbCnsetRead, int cbCnsetRead, SyncConfigFlags flags, IExchangeManifestExCallback iExchangeManifestExCallback, SRestriction* lpRestriction, PropTag[] lpIncludeProps)
		{
			return SafeExExportManifestExHandle.IExchangeExportManifestEx_Config(this.handle, pbIdsetGiven, cbIdsetGiven, pbCnsetSeen, cbCnsetSeen, pbCnsetSeenFAI, cbCnsetSeenFAI, pbCnsetRead, cbCnsetRead, flags, iExchangeManifestExCallback, lpRestriction, lpIncludeProps);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000342B1 File Offset: 0x000324B1
		internal int Synchronize(int ulFlags)
		{
			return SafeExExportManifestExHandle.IExchangeExportManifestEx_Synchronize(this.handle, ulFlags);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000342C0 File Offset: 0x000324C0
		internal int GetState(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead)
		{
			return SafeExExportManifestExHandle.IExchangeExportManifestEx_GetState(this.handle, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen, out pbCnsetSeenFAI, out cbCnsetSeenFAI, out pbCnsetRead, out cbCnsetRead);
		}

		// Token: 0x06000CBB RID: 3259
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeExportManifestEx_Config(IntPtr iExchangeExportManifestEx, [MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetRead, int cbCnsetRead, SyncConfigFlags flags, [In] IExchangeManifestExCallback iExchangeManifestExCallback, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps);

		// Token: 0x06000CBC RID: 3260
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportManifestEx_Synchronize(IntPtr iExchangeExportManifestEx, int ulFlags);

		// Token: 0x06000CBD RID: 3261
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportManifestEx_GetState(IntPtr iExchangeExportManifestEx, out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead);
	}
}

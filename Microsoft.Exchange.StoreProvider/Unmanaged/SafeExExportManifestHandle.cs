using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B6 RID: 694
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExExportManifestHandle : SafeExInterfaceHandle, IExExportManifest, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x000342E5 File Offset: 0x000324E5
		protected SafeExExportManifestHandle()
		{
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000342ED File Offset: 0x000324ED
		internal SafeExExportManifestHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000342F6 File Offset: 0x000324F6
		internal SafeExExportManifestHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000342FF File Offset: 0x000324FF
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExExportManifestHandle>(this);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00034307 File Offset: 0x00032507
		public unsafe int Config(IStream iStream, SyncConfigFlags flags, IExchangeManifestCallback pCallback, SRestriction* lpRestriction, PropTag[] lpIncludeProps)
		{
			return SafeExExportManifestHandle.IExchangeExportManifest_Config(this.handle, iStream, flags, pCallback, lpRestriction, lpIncludeProps);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003431B File Offset: 0x0003251B
		public int Synchronize(int ulFlags)
		{
			return SafeExExportManifestHandle.IExchangeExportManifest_Synchronize(this.handle, ulFlags);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00034329 File Offset: 0x00032529
		public int Checkpoint(IStream iStream, bool clearCnsets, long[] changeMids, long[] changeCns, long[] changeAssociatedCns, long[] deleteMids, long[] readCns)
		{
			return SafeExExportManifestHandle.IExchangeExportManifest_Checkpoint(this.handle, iStream, clearCnsets, changeMids, changeCns, changeAssociatedCns, deleteMids, readCns);
		}

		// Token: 0x06000CC5 RID: 3269
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeExportManifest_Config(IntPtr iExchangeExportManifest, IStream iStream, SyncConfigFlags flags, [In] IExchangeManifestCallback pCallback, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps);

		// Token: 0x06000CC6 RID: 3270
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportManifest_Synchronize(IntPtr iExchangeExportManifest, int ulFlags);

		// Token: 0x06000CC7 RID: 3271
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportManifest_Checkpoint(IntPtr iExchangeExportManifest, IStream iStream, [MarshalAs(UnmanagedType.Bool)] [In] bool clearCnsets, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeMids, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeCns, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeAssociatedCns, [MarshalAs(UnmanagedType.LPArray)] [In] long[] deleteMids, [MarshalAs(UnmanagedType.LPArray)] [In] long[] readCns);
	}
}

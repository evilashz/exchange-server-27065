using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B4 RID: 692
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExExportHierManifestExHandle : SafeExInterfaceHandle
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x000341C8 File Offset: 0x000323C8
		protected SafeExExportHierManifestExHandle()
		{
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000341D0 File Offset: 0x000323D0
		internal SafeExExportHierManifestExHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000341D9 File Offset: 0x000323D9
		internal SafeExExportHierManifestExHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000341E2 File Offset: 0x000323E2
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExExportHierManifestExHandle>(this);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000341EC File Offset: 0x000323EC
		internal unsafe int Config(byte[] pbIdsetGiven, int cbIdsetGiven, byte[] pbCnsetSeen, int cbCnsetSeen, SyncConfigFlags flags, IExchangeHierManifestCallback iExchangeHierManifestCallback, SRestriction* lpRestriction, PropTag[] lpIncludeProps, PropTag[] lpExcludeProps)
		{
			return SafeExExportHierManifestExHandle.IExchangeExportHierManifestEx_Config(this.handle, pbIdsetGiven, cbIdsetGiven, pbCnsetSeen, cbCnsetSeen, flags, iExchangeHierManifestCallback, lpRestriction, lpIncludeProps, lpExcludeProps);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00034213 File Offset: 0x00032413
		internal int Synchronize(int ulFlags)
		{
			return SafeExExportHierManifestExHandle.IExchangeExportHierManifestEx_Synchronize(this.handle, ulFlags);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00034221 File Offset: 0x00032421
		internal int GetState(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen)
		{
			return SafeExExportHierManifestExHandle.IExchangeExportHierManifestEx_GetState(this.handle, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00034234 File Offset: 0x00032434
		internal int Checkpoint(byte[] pbCheckpointIdsetGiven, int cbCheckpointIdsetGiven, byte[] pbCheckpointCnsetSeen, int cbCheckpointCnsetSeen, long[] changeFids, long[] changeCns, long[] deleteMids, out SafeExMemoryHandle pbIdsetGiven, out int cbIdsetGiven, out SafeExMemoryHandle pbCnsetSeen, out int cbCnsetSeen)
		{
			return SafeExExportHierManifestExHandle.IExchangeExportHierManifestEx_Checkpoint(this.handle, pbCheckpointIdsetGiven, cbCheckpointIdsetGiven, pbCheckpointCnsetSeen, cbCheckpointCnsetSeen, changeFids, changeCns, deleteMids, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen);
		}

		// Token: 0x06000CB0 RID: 3248
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeExportHierManifestEx_Config(IntPtr iExchangeExportHierManifestEx, [MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, SyncConfigFlags flags, [In] IExchangeHierManifestCallback iExchangeHierManifestCallback, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps);

		// Token: 0x06000CB1 RID: 3249
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportHierManifestEx_Synchronize(IntPtr iExchangeExportHierManifestEx, int ulFlags);

		// Token: 0x06000CB2 RID: 3250
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportHierManifestEx_GetState(IntPtr iExchangeExportHierManifestEx, out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen);

		// Token: 0x06000CB3 RID: 3251
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportHierManifestEx_Checkpoint(IntPtr iExchangeExportHierManifestEx, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCheckpointIdsetGiven, int cbCheckpointIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCheckpointCnsetSeen, int cbCheckpointCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeFids, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeCns, [MarshalAs(UnmanagedType.LPArray)] [In] long[] deleteMids, out SafeExMemoryHandle pbIdsetGiven, out int cbIdsetGiven, out SafeExMemoryHandle pbCnsetSeen, out int cbCnsetSeen);
	}
}

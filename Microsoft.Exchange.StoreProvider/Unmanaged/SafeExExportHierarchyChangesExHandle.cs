using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B3 RID: 691
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExExportHierarchyChangesExHandle : SafeExInterfaceHandle
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x00034165 File Offset: 0x00032365
		protected SafeExExportHierarchyChangesExHandle()
		{
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003416D File Offset: 0x0003236D
		internal SafeExExportHierarchyChangesExHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00034176 File Offset: 0x00032376
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExExportHierarchyChangesExHandle>(this);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00034180 File Offset: 0x00032380
		internal unsafe int Config(byte[] pbIdsetGiven, int cbIdsetGiven, byte[] pbCnsetSeen, int cbCnsetSeen, SyncConfigFlags flags, SRestriction* lpRestriction, PropTag[] lpIncludeProps, PropTag[] lpExcludeProps, int ulBufferSize)
		{
			return SafeExExportHierarchyChangesExHandle.IExchangeExportHierarchyChangesEx_Config(this.handle, pbIdsetGiven, cbIdsetGiven, pbCnsetSeen, cbCnsetSeen, flags, lpRestriction, lpIncludeProps, lpExcludeProps, ulBufferSize);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000341A7 File Offset: 0x000323A7
		internal int GetBuffers(out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks)
		{
			return SafeExExportHierarchyChangesExHandle.IExchangeExportHierarchyChangesEx_GetBuffers(this.handle, out ppBlocks, out cBlocks);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000341B6 File Offset: 0x000323B6
		internal int GetState(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen)
		{
			return SafeExExportHierarchyChangesExHandle.IExchangeExportHierarchyChangesEx_GetState(this.handle, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen);
		}

		// Token: 0x06000CA5 RID: 3237
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeExportHierarchyChangesEx_Config(IntPtr iExchangeExportChangesEx, [MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, SyncConfigFlags flags, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, int ulBufferSize);

		// Token: 0x06000CA6 RID: 3238
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportHierarchyChangesEx_GetBuffers(IntPtr iExchangeExportChangesEx, out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks);

		// Token: 0x06000CA7 RID: 3239
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportHierarchyChangesEx_GetState(IntPtr iExchangeExportChangesEx, out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen);
	}
}

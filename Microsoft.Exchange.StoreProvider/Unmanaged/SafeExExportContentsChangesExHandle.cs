using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B2 RID: 690
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExExportContentsChangesExHandle : SafeExInterfaceHandle
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x000340E6 File Offset: 0x000322E6
		protected SafeExExportContentsChangesExHandle()
		{
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000340EE File Offset: 0x000322EE
		internal SafeExExportContentsChangesExHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000340F7 File Offset: 0x000322F7
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExExportContentsChangesExHandle>(this);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00034100 File Offset: 0x00032300
		internal unsafe int Config(byte[] pbIdsetGiven, int cbIdsetGiven, byte[] pbCnsetSeen, int cbCnsetSeen, byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, byte[] pbCnsetRead, int cbCnsetRead, SyncConfigFlags flags, SRestriction* lpRestriction, PropTag[] lpIncludeProps, PropTag[] lpExcludeProps, int ulBufferSize)
		{
			return SafeExExportContentsChangesExHandle.IExchangeExportContentsChangesEx_Config(this.handle, pbIdsetGiven, cbIdsetGiven, pbCnsetSeen, cbCnsetSeen, pbCnsetSeenFAI, cbCnsetSeenFAI, pbCnsetRead, cbCnsetRead, flags, lpRestriction, lpIncludeProps, lpExcludeProps, ulBufferSize);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003412F File Offset: 0x0003232F
		internal int GetBuffers(out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks)
		{
			return SafeExExportContentsChangesExHandle.IExchangeExportContentsChangesEx_GetBuffers(this.handle, out ppBlocks, out cBlocks);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00034140 File Offset: 0x00032340
		internal int GetState(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead)
		{
			return SafeExExportContentsChangesExHandle.IExchangeExportContentsChangesEx_GetState(this.handle, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen, out pbCnsetSeenFAI, out cbCnsetSeenFAI, out pbCnsetRead, out cbCnsetRead);
		}

		// Token: 0x06000C9C RID: 3228
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeExportContentsChangesEx_Config(IntPtr iExchangeExportChangesEx, [MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetRead, int cbCnsetRead, SyncConfigFlags flags, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, int ulBufferSize);

		// Token: 0x06000C9D RID: 3229
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportContentsChangesEx_GetBuffers(IntPtr iExchangeExportChangesEx, out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks);

		// Token: 0x06000C9E RID: 3230
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportContentsChangesEx_GetState(IntPtr iExchangeExportChangesEx, out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead);
	}
}

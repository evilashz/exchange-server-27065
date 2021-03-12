using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002BB RID: 699
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExImportHierarchyChangesHandle : SafeExInterfaceHandle, IExImportHierarchyChanges, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x000345E5 File Offset: 0x000327E5
		protected SafeExImportHierarchyChangesHandle()
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000345ED File Offset: 0x000327ED
		internal SafeExImportHierarchyChangesHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000345F6 File Offset: 0x000327F6
		internal SafeExImportHierarchyChangesHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000345FF File Offset: 0x000327FF
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExImportHierarchyChangesHandle>(this);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00034607 File Offset: 0x00032807
		public int Config(IStream iStream, int ulFlags)
		{
			return SafeExImportHierarchyChangesHandle.IExchangeImportHierarchyChanges_Config(this.handle, iStream, ulFlags);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00034616 File Offset: 0x00032816
		public int UpdateState(IStream iStream)
		{
			return SafeExImportHierarchyChangesHandle.IExchangeImportHierarchyChanges_UpdateState(this.handle, iStream);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00034624 File Offset: 0x00032824
		public unsafe int ImportFolderChange(int cpvalChanges, SPropValue* ppvalChanges)
		{
			return SafeExImportHierarchyChangesHandle.IExchangeImportHierarchyChanges_ImportFolderChange(this.handle, cpvalChanges, ppvalChanges);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00034633 File Offset: 0x00032833
		public unsafe int ImportFolderDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList)
		{
			return SafeExImportHierarchyChangesHandle.IExchangeImportHierarchyChanges_ImportFolderDeletion(this.handle, ulFlags, lpSrcEntryList);
		}

		// Token: 0x06000D15 RID: 3349
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportHierarchyChanges_Config(IntPtr iExchangeImportHierarchyChanges, IStream iStream, int ulFlags);

		// Token: 0x06000D16 RID: 3350
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportHierarchyChanges_UpdateState(IntPtr iExchangeImportHierarchyChanges, IStream iStream);

		// Token: 0x06000D17 RID: 3351
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeImportHierarchyChanges_ImportFolderChange(IntPtr iExchangeImportHierarchyChanges, int cpvalChanges, SPropValue* ppvalChanges);

		// Token: 0x06000D18 RID: 3352
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeImportHierarchyChanges_ImportFolderDeletion(IntPtr iExchangeImportHierarchyChanges, int ulFlags, _SBinaryArray* lpSrcEntryList);
	}
}

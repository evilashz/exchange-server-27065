using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B1 RID: 689
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExExportChangesHandle : SafeExInterfaceHandle, IExExportChanges, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0003408F File Offset: 0x0003228F
		protected SafeExExportChangesHandle()
		{
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00034097 File Offset: 0x00032297
		internal SafeExExportChangesHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000340A0 File Offset: 0x000322A0
		internal SafeExExportChangesHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000340A9 File Offset: 0x000322A9
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExExportChangesHandle>(this);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000340B1 File Offset: 0x000322B1
		public unsafe int Config(IStream iStream, int ulFlags, IntPtr iCollector, SRestriction* lpRestriction, PropTag[] lpIncludeProps, PropTag[] lpExcludeProps, int ulBufferSize)
		{
			return SafeExExportChangesHandle.IExchangeExportChanges_Config(this.handle, iStream, ulFlags, iCollector, lpRestriction, lpIncludeProps, lpExcludeProps, ulBufferSize);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000340C9 File Offset: 0x000322C9
		public int Synchronize(out int lpulSteps, out int lpulProgress)
		{
			return SafeExExportChangesHandle.IExchangeExportChanges_Synchronize(this.handle, out lpulSteps, out lpulProgress);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000340D8 File Offset: 0x000322D8
		public int UpdateState(IStream iStream)
		{
			return SafeExExportChangesHandle.IExchangeExportChanges_UpdateState(this.handle, iStream);
		}

		// Token: 0x06000C93 RID: 3219
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeExportChanges_Config(IntPtr iExchangeExportChanges, IStream iStream, int ulFlags, IntPtr iCollector, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, int ulBufferSize);

		// Token: 0x06000C94 RID: 3220
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportChanges_Synchronize(IntPtr iExchangeExportChanges, out int lpulSteps, out int lpulProgress);

		// Token: 0x06000C95 RID: 3221
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeExportChanges_UpdateState(IntPtr iExchangeExportChanges, IStream iStream);
	}
}

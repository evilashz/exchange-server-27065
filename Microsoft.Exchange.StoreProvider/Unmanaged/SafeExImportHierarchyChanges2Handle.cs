using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002BC RID: 700
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExImportHierarchyChanges2Handle : SafeExImportHierarchyChangesHandle
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x00034642 File Offset: 0x00032842
		protected SafeExImportHierarchyChanges2Handle()
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003464A File Offset: 0x0003284A
		internal SafeExImportHierarchyChanges2Handle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00034653 File Offset: 0x00032853
		internal SafeExImportHierarchyChanges2Handle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003465C File Offset: 0x0003285C
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExImportHierarchyChanges2Handle>(this);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00034664 File Offset: 0x00032864
		internal int ConfigEx(byte[] pbIdsetGiven, int cbIdsetGiven, byte[] pbCnsetSeen, int cbCnsetSeen, int ulFlags)
		{
			return SafeExImportHierarchyChanges2Handle.IExchangeImportHierarchyChanges2_ConfigEx(this.handle, pbIdsetGiven, cbIdsetGiven, pbCnsetSeen, cbCnsetSeen, ulFlags);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00034678 File Offset: 0x00032878
		internal int UpdateStateEx(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen)
		{
			return SafeExImportHierarchyChanges2Handle.IExchangeImportHierarchyChanges2_UpdateStateEx(this.handle, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen);
		}

		// Token: 0x06000D1F RID: 3359
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportHierarchyChanges2_ConfigEx(IntPtr iExchangeImportHierarchyChanges2, [MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, int ulFlags);

		// Token: 0x06000D20 RID: 3360
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportHierarchyChanges2_UpdateStateEx(IntPtr iExchangeImportHierarchyChanges2, out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen);
	}
}

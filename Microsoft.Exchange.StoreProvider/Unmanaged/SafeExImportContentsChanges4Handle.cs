using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002BA RID: 698
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExImportContentsChanges4Handle : SafeExImportContentsChangesHandle
	{
		// Token: 0x06000D05 RID: 3333 RVA: 0x00034575 File Offset: 0x00032775
		protected SafeExImportContentsChanges4Handle()
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003457D File Offset: 0x0003277D
		internal SafeExImportContentsChanges4Handle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00034586 File Offset: 0x00032786
		internal SafeExImportContentsChanges4Handle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003458F File Offset: 0x0003278F
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExImportContentsChanges4Handle>(this);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00034598 File Offset: 0x00032798
		internal int ConfigEx(byte[] pbIdsetGiven, int cbIdsetGiven, byte[] pbCnsetSeen, int cbCnsetSeen, byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, byte[] pbCnsetRead, int cbCnsetRead, int ulFlags)
		{
			return SafeExImportContentsChanges4Handle.IExchangeImportContentsChanges4_ConfigEx(this.handle, pbIdsetGiven, cbIdsetGiven, pbCnsetSeen, cbCnsetSeen, pbCnsetSeenFAI, cbCnsetSeenFAI, pbCnsetRead, cbCnsetRead, ulFlags);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000345C0 File Offset: 0x000327C0
		internal int UpdateStateEx(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead)
		{
			return SafeExImportContentsChanges4Handle.IExchangeImportContentsChanges4_UpdateStateEx(this.handle, out pbIdsetGiven, out cbIdsetGiven, out pbCnsetSeen, out cbCnsetSeen, out pbCnsetSeenFAI, out cbCnsetSeenFAI, out pbCnsetRead, out cbCnsetRead);
		}

		// Token: 0x06000D0B RID: 3339
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportContentsChanges4_ConfigEx(IntPtr iExchangeImportContentsChanges4, [MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetRead, int cbCnsetRead, int ulFlags);

		// Token: 0x06000D0C RID: 3340
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeImportContentsChanges4_UpdateStateEx(IntPtr iExchangeImportContentsChanges4, out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead);
	}
}

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000654 RID: 1620
	[Guid("97EA99C7-0186-4AD4-8DF9-C5B4E0ED6B22")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IBackgroundCopyCallback
	{
		// Token: 0x06001D6C RID: 7532
		void JobTransferred([MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyJob pJob);

		// Token: 0x06001D6D RID: 7533
		void JobError([MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyJob pJob, [MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyError pError);

		// Token: 0x06001D6E RID: 7534
		void JobModification([MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyJob pJob, [In] uint dwReserved);
	}
}

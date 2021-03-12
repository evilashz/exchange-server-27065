using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000653 RID: 1619
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("5CE34C0D-0DC9-4C1F-897C-DAA1B78CEE7C")]
	[ComImport]
	internal interface IBackgroundCopyManager
	{
		// Token: 0x06001D68 RID: 7528
		void CreateJob([MarshalAs(UnmanagedType.LPWStr)] string DisplayName, BG_JOB_TYPE Type, out Guid pJobId, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyJob ppJob);

		// Token: 0x06001D69 RID: 7529
		void GetJob(ref Guid jobID, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyJob ppJob);

		// Token: 0x06001D6A RID: 7530
		void EnumJobs(uint dwFlags, [MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyJobs ppenum);

		// Token: 0x06001D6B RID: 7531
		void GetErrorDescription([MarshalAs(UnmanagedType.Error)] int hResult, uint LanguageId, [MarshalAs(UnmanagedType.LPWStr)] out string pErrorDescription);
	}
}

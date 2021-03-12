using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000659 RID: 1625
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1AF4F612-3B71-466F-8F58-7B6F73AC57AD")]
	[ComImport]
	internal interface IEnumBackgroundCopyJobs
	{
		// Token: 0x06001DC4 RID: 7620
		void Next(uint celt, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyJob rgelt, out uint pceltFetched);

		// Token: 0x06001DC5 RID: 7621
		void Skip(uint celt);

		// Token: 0x06001DC6 RID: 7622
		void Reset();

		// Token: 0x06001DC7 RID: 7623
		void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyJobs ppenum);

		// Token: 0x06001DC8 RID: 7624
		void GetCount(out uint puCount);
	}
}

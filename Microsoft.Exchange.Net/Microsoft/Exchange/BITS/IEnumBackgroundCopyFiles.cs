using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x0200065A RID: 1626
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CA51E165-C365-424C-8D41-24AAA4FF3C40")]
	[ComImport]
	internal interface IEnumBackgroundCopyFiles
	{
		// Token: 0x06001DC9 RID: 7625
		void Next(uint celt, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyFile rgelt, out uint pceltFetched);

		// Token: 0x06001DCA RID: 7626
		void Skip(uint celt);

		// Token: 0x06001DCB RID: 7627
		void Reset();

		// Token: 0x06001DCC RID: 7628
		void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyFiles ppenum);

		// Token: 0x06001DCD RID: 7629
		void GetCount(out uint puCount);
	}
}

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x0200065B RID: 1627
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("01B7BD23-FB88-4A77-8490-5891D3E4653A")]
	[ComImport]
	internal interface IBackgroundCopyFile
	{
		// Token: 0x06001DCE RID: 7630
		void GetRemoteName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001DCF RID: 7631
		void GetLocalName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001DD0 RID: 7632
		void GetProgress(out _BG_FILE_PROGRESS pVal);
	}
}

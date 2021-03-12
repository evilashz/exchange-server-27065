using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200062A RID: 1578
	[Guid("d16679f2-6ca0-472d-8d31-2f5d55aee155")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IWMProfileManager
	{
		// Token: 0x06001CA0 RID: 7328
		void CreateEmptyProfile();

		// Token: 0x06001CA1 RID: 7329
		void LoadProfileByID();

		// Token: 0x06001CA2 RID: 7330
		void LoadProfileByData([MarshalAs(UnmanagedType.LPWStr)] [In] string pwszProfile, [MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);

		// Token: 0x06001CA3 RID: 7331
		void SaveProfile();

		// Token: 0x06001CA4 RID: 7332
		void GetSystemProfileCount();

		// Token: 0x06001CA5 RID: 7333
		void LoadSystemProfile();
	}
}

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Forefront.ActiveDirectoryConnector
{
	// Token: 0x02000003 RID: 3
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FOleAutomation)]
	[Guid("2307AD21-F105-4DA4-B699-355E4F3C9D4A")]
	[ComImport]
	public interface IADFilteringSettingsWatcher
	{
		// Token: 0x06000001 RID: 1
		void Start();

		// Token: 0x06000002 RID: 2
		void Stop();

		// Token: 0x06000003 RID: 3
		int GetProcessId();
	}
}

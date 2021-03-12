using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095C RID: 2396
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IPersistFile instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIPersistFile
	{
		// Token: 0x0600619B RID: 24987
		void GetClassID(out Guid pClassID);

		// Token: 0x0600619C RID: 24988
		[PreserveSig]
		int IsDirty();

		// Token: 0x0600619D RID: 24989
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x0600619E RID: 24990
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x0600619F RID: 24991
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x060061A0 RID: 24992
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}

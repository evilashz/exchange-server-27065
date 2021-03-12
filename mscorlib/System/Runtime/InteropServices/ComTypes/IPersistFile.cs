using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A05 RID: 2565
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IPersistFile
	{
		// Token: 0x0600652F RID: 25903
		[__DynamicallyInvokable]
		void GetClassID(out Guid pClassID);

		// Token: 0x06006530 RID: 25904
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsDirty();

		// Token: 0x06006531 RID: 25905
		[__DynamicallyInvokable]
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x06006532 RID: 25906
		[__DynamicallyInvokable]
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x06006533 RID: 25907
		[__DynamicallyInvokable]
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x06006534 RID: 25908
		[__DynamicallyInvokable]
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}

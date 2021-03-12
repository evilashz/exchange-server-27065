using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000945 RID: 2373
	[Guid("FA1F3615-ACB9-486d-9EAC-1BEF87E36B09")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibExporterNameProvider
	{
		// Token: 0x06006120 RID: 24864
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		string[] GetNames();
	}
}

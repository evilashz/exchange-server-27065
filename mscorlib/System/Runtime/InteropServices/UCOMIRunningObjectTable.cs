using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095E RID: 2398
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IRunningObjectTable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00000010-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIRunningObjectTable
	{
		// Token: 0x060061AD RID: 25005
		void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, UCOMIMoniker pmkObjectName, out int pdwRegister);

		// Token: 0x060061AE RID: 25006
		void Revoke(int dwRegister);

		// Token: 0x060061AF RID: 25007
		void IsRunning(UCOMIMoniker pmkObjectName);

		// Token: 0x060061B0 RID: 25008
		void GetObject(UCOMIMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x060061B1 RID: 25009
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x060061B2 RID: 25010
		void GetTimeOfLastChange(UCOMIMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x060061B3 RID: 25011
		void EnumRunning(out UCOMIEnumMoniker ppenumMoniker);
	}
}

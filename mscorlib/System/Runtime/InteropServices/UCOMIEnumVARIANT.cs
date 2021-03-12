using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000958 RID: 2392
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumVARIANT instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumVARIANT
	{
		// Token: 0x0600617F RID: 24959
		[PreserveSig]
		int Next(int celt, int rgvar, int pceltFetched);

		// Token: 0x06006180 RID: 24960
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006181 RID: 24961
		[PreserveSig]
		int Reset();

		// Token: 0x06006182 RID: 24962
		void Clone(int ppenum);
	}
}

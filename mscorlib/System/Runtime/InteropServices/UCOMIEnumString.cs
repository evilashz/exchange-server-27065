using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000957 RID: 2391
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumString instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00000101-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumString
	{
		// Token: 0x0600617B RID: 24955
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, out int pceltFetched);

		// Token: 0x0600617C RID: 24956
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600617D RID: 24957
		[PreserveSig]
		int Reset();

		// Token: 0x0600617E RID: 24958
		void Clone(out UCOMIEnumString ppenum);
	}
}

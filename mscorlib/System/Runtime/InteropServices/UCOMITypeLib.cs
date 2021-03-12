using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097B RID: 2427
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeLib instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeLib
	{
		// Token: 0x060061D4 RID: 25044
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x060061D5 RID: 25045
		void GetTypeInfo(int index, out UCOMITypeInfo ppTI);

		// Token: 0x060061D6 RID: 25046
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x060061D7 RID: 25047
		void GetTypeInfoOfGuid(ref Guid guid, out UCOMITypeInfo ppTInfo);

		// Token: 0x060061D8 RID: 25048
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x060061D9 RID: 25049
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x060061DA RID: 25050
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060061DB RID: 25051
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x060061DC RID: 25052
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] UCOMITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x060061DD RID: 25053
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}

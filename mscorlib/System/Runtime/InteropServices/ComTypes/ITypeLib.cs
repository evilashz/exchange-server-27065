using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A25 RID: 2597
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeLib
	{
		// Token: 0x06006568 RID: 25960
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x06006569 RID: 25961
		[__DynamicallyInvokable]
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x0600656A RID: 25962
		[__DynamicallyInvokable]
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x0600656B RID: 25963
		[__DynamicallyInvokable]
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x0600656C RID: 25964
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x0600656D RID: 25965
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x0600656E RID: 25966
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600656F RID: 25967
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x06006570 RID: 25968
		[__DynamicallyInvokable]
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x06006571 RID: 25969
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}

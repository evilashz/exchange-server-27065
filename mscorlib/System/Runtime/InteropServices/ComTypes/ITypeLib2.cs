using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A26 RID: 2598
	[Guid("00020411-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeLib2 : ITypeLib
	{
		// Token: 0x06006572 RID: 25970
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x06006573 RID: 25971
		[__DynamicallyInvokable]
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x06006574 RID: 25972
		[__DynamicallyInvokable]
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x06006575 RID: 25973
		[__DynamicallyInvokable]
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x06006576 RID: 25974
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x06006577 RID: 25975
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06006578 RID: 25976
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06006579 RID: 25977
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x0600657A RID: 25978
		[__DynamicallyInvokable]
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x0600657B RID: 25979
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);

		// Token: 0x0600657C RID: 25980
		[__DynamicallyInvokable]
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x0600657D RID: 25981
		[LCIDConversion(1)]
		[__DynamicallyInvokable]
		void GetDocumentation2(int index, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x0600657E RID: 25982
		void GetLibStatistics(IntPtr pcUniqueNames, out int pcchUniqueNames);

		// Token: 0x0600657F RID: 25983
		void GetAllCustData(IntPtr pCustData);
	}
}

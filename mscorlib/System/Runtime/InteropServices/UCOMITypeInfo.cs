using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000977 RID: 2423
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeInfo instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeInfo
	{
		// Token: 0x060061C1 RID: 25025
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x060061C2 RID: 25026
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x060061C3 RID: 25027
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x060061C4 RID: 25028
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x060061C5 RID: 25029
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x060061C6 RID: 25030
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x060061C7 RID: 25031
		void GetImplTypeFlags(int index, out int pImplTypeFlags);

		// Token: 0x060061C8 RID: 25032
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x060061C9 RID: 25033
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, out EXCEPINFO pExcepInfo, out int puArgErr);

		// Token: 0x060061CA RID: 25034
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060061CB RID: 25035
		void GetDllEntry(int memid, INVOKEKIND invKind, out string pBstrDllName, out string pBstrName, out short pwOrdinal);

		// Token: 0x060061CC RID: 25036
		void GetRefTypeInfo(int hRef, out UCOMITypeInfo ppTI);

		// Token: 0x060061CD RID: 25037
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x060061CE RID: 25038
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x060061CF RID: 25039
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x060061D0 RID: 25040
		void GetContainingTypeLib(out UCOMITypeLib ppTLB, out int pIndex);

		// Token: 0x060061D1 RID: 25041
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x060061D2 RID: 25042
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x060061D3 RID: 25043
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}

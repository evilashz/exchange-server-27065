using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A27 RID: 2599
	[Guid("00020412-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeInfo2 : ITypeInfo
	{
		// Token: 0x06006580 RID: 25984
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06006581 RID: 25985
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06006582 RID: 25986
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06006583 RID: 25987
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x06006584 RID: 25988
		[__DynamicallyInvokable]
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x06006585 RID: 25989
		[__DynamicallyInvokable]
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x06006586 RID: 25990
		[__DynamicallyInvokable]
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x06006587 RID: 25991
		[__DynamicallyInvokable]
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x06006588 RID: 25992
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x06006589 RID: 25993
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600658A RID: 25994
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x0600658B RID: 25995
		[__DynamicallyInvokable]
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x0600658C RID: 25996
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x0600658D RID: 25997
		[__DynamicallyInvokable]
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x0600658E RID: 25998
		[__DynamicallyInvokable]
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x0600658F RID: 25999
		[__DynamicallyInvokable]
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06006590 RID: 26000
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06006591 RID: 26001
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06006592 RID: 26002
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);

		// Token: 0x06006593 RID: 26003
		[__DynamicallyInvokable]
		void GetTypeKind(out TYPEKIND pTypeKind);

		// Token: 0x06006594 RID: 26004
		[__DynamicallyInvokable]
		void GetTypeFlags(out int pTypeFlags);

		// Token: 0x06006595 RID: 26005
		[__DynamicallyInvokable]
		void GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, out int pFuncIndex);

		// Token: 0x06006596 RID: 26006
		[__DynamicallyInvokable]
		void GetVarIndexOfMemId(int memid, out int pVarIndex);

		// Token: 0x06006597 RID: 26007
		[__DynamicallyInvokable]
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x06006598 RID: 26008
		[__DynamicallyInvokable]
		void GetFuncCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x06006599 RID: 26009
		[__DynamicallyInvokable]
		void GetParamCustData(int indexFunc, int indexParam, ref Guid guid, out object pVarVal);

		// Token: 0x0600659A RID: 26010
		[__DynamicallyInvokable]
		void GetVarCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600659B RID: 26011
		[__DynamicallyInvokable]
		void GetImplTypeCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600659C RID: 26012
		[LCIDConversion(1)]
		[__DynamicallyInvokable]
		void GetDocumentation2(int memid, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x0600659D RID: 26013
		void GetAllCustData(IntPtr pCustData);

		// Token: 0x0600659E RID: 26014
		void GetAllFuncCustData(int index, IntPtr pCustData);

		// Token: 0x0600659F RID: 26015
		void GetAllParamCustData(int indexFunc, int indexParam, IntPtr pCustData);

		// Token: 0x060065A0 RID: 26016
		void GetAllVarCustData(int index, IntPtr pCustData);

		// Token: 0x060065A1 RID: 26017
		void GetAllImplTypeCustData(int index, IntPtr pCustData);
	}
}

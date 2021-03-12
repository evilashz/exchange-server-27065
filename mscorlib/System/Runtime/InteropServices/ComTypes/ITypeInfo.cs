using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A21 RID: 2593
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeInfo
	{
		// Token: 0x06006555 RID: 25941
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06006556 RID: 25942
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06006557 RID: 25943
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06006558 RID: 25944
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x06006559 RID: 25945
		[__DynamicallyInvokable]
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x0600655A RID: 25946
		[__DynamicallyInvokable]
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x0600655B RID: 25947
		[__DynamicallyInvokable]
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x0600655C RID: 25948
		[__DynamicallyInvokable]
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x0600655D RID: 25949
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x0600655E RID: 25950
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600655F RID: 25951
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x06006560 RID: 25952
		[__DynamicallyInvokable]
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x06006561 RID: 25953
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x06006562 RID: 25954
		[__DynamicallyInvokable]
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x06006563 RID: 25955
		[__DynamicallyInvokable]
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x06006564 RID: 25956
		[__DynamicallyInvokable]
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06006565 RID: 25957
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06006566 RID: 25958
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06006567 RID: 25959
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}

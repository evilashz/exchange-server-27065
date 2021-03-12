using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000983 RID: 2435
	internal static class NativeMethods
	{
		// Token: 0x06006217 RID: 25111
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("oleaut32.dll", PreserveSig = false)]
		internal static extern void VariantClear(IntPtr variant);

		// Token: 0x02000C68 RID: 3176
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("00020400-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IDispatch
		{
			// Token: 0x06006FF9 RID: 28665
			[SecurityCritical]
			void GetTypeInfoCount(out uint pctinfo);

			// Token: 0x06006FFA RID: 28666
			[SecurityCritical]
			void GetTypeInfo(uint iTInfo, int lcid, out IntPtr info);

			// Token: 0x06006FFB RID: 28667
			[SecurityCritical]
			void GetIDsOfNames(ref Guid iid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] string[] names, uint cNames, int lcid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] [Out] int[] rgDispId);

			// Token: 0x06006FFC RID: 28668
			[SecurityCritical]
			void Invoke(int dispIdMember, ref Guid riid, int lcid, INVOKEKIND wFlags, ref DISPPARAMS pDispParams, IntPtr pvarResult, IntPtr pExcepInfo, IntPtr puArgErr);
		}
	}
}

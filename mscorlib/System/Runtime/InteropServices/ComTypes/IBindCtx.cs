using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009F7 RID: 2551
	[Guid("0000000e-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IBindCtx
	{
		// Token: 0x060064EE RID: 25838
		[__DynamicallyInvokable]
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x060064EF RID: 25839
		[__DynamicallyInvokable]
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x060064F0 RID: 25840
		[__DynamicallyInvokable]
		void ReleaseBoundObjects();

		// Token: 0x060064F1 RID: 25841
		[__DynamicallyInvokable]
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x060064F2 RID: 25842
		[__DynamicallyInvokable]
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x060064F3 RID: 25843
		[__DynamicallyInvokable]
		void GetRunningObjectTable(out IRunningObjectTable pprot);

		// Token: 0x060064F4 RID: 25844
		[__DynamicallyInvokable]
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x060064F5 RID: 25845
		[__DynamicallyInvokable]
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x060064F6 RID: 25846
		[__DynamicallyInvokable]
		void EnumObjectParam(out IEnumString ppenum);

		// Token: 0x060064F7 RID: 25847
		[__DynamicallyInvokable]
		[PreserveSig]
		int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}

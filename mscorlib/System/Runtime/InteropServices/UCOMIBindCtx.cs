using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094E RID: 2382
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IBindCtx instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000e-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIBindCtx
	{
		// Token: 0x0600615A RID: 24922
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x0600615B RID: 24923
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x0600615C RID: 24924
		void ReleaseBoundObjects();

		// Token: 0x0600615D RID: 24925
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x0600615E RID: 24926
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x0600615F RID: 24927
		void GetRunningObjectTable(out UCOMIRunningObjectTable pprot);

		// Token: 0x06006160 RID: 24928
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06006161 RID: 24929
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x06006162 RID: 24930
		void EnumObjectParam(out UCOMIEnumString ppenum);

		// Token: 0x06006163 RID: 24931
		void RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}

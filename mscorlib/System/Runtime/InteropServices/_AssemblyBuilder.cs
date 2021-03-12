using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000985 RID: 2437
	[Guid("BEBB2505-8B54-3443-AEAD-142A16DD9CC7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(AssemblyBuilder))]
	[ComVisible(true)]
	public interface _AssemblyBuilder
	{
		// Token: 0x0600624A RID: 25162
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600624B RID: 25163
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600624C RID: 25164
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600624D RID: 25165
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}

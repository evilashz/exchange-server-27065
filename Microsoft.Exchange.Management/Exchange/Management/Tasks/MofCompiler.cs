using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000FA RID: 250
	[Guid("6daf9757-2e37-11d2-aec9-00c04fb68820")]
	[ComImport]
	internal class MofCompiler : IMofCompiler
	{
		// Token: 0x0600075E RID: 1886
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		public extern int CompileFile([MarshalAs(UnmanagedType.LPWStr)] string FileName, [MarshalAs(UnmanagedType.LPWStr)] string ServerAndNamespace, [MarshalAs(UnmanagedType.LPWStr)] string User, [MarshalAs(UnmanagedType.LPWStr)] string Authority, [MarshalAs(UnmanagedType.LPWStr)] string Password, int lOptionFlags, int lClassFlags, int lInstanceFlags, [In] [Out] ref WbemCompileStatusInfo pInfo);

		// Token: 0x0600075F RID: 1887
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		public extern int CompileBuffer(int BuffSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] byte[] pBuffer, [MarshalAs(UnmanagedType.LPWStr)] string ServerAndNamespace, [MarshalAs(UnmanagedType.LPWStr)] string User, [MarshalAs(UnmanagedType.LPWStr)] string Authority, [MarshalAs(UnmanagedType.LPWStr)] string Password, int lOptionFlags, int lClassFlags, int lInstanceFlags, [In] [Out] ref WbemCompileStatusInfo pInfo);

		// Token: 0x06000760 RID: 1888
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		public extern int CreateBMOF([MarshalAs(UnmanagedType.LPWStr)] string TextFileName, [MarshalAs(UnmanagedType.LPWStr)] string BMOFFileName, [MarshalAs(UnmanagedType.LPWStr)] string ServerAndNamespace, int lOptionFlags, int lClassFlags, int lInstanceFlags, [In] [Out] ref WbemCompileStatusInfo pInfo);

		// Token: 0x06000761 RID: 1889
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MofCompiler();

		// Token: 0x0400037C RID: 892
		internal const int WbemSNoError = 0;

		// Token: 0x0400037D RID: 893
		internal const int WbemSAlreadyExists = 262145;
	}
}

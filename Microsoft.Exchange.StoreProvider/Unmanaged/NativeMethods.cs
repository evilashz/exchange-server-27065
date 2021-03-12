using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200029F RID: 671
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class NativeMethods
	{
		// Token: 0x06000C73 RID: 3187
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		internal static extern int EcInitProvider(out IntPtr pPerfData);

		// Token: 0x06000C74 RID: 3188
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		internal static extern int EcGetExRpcManage(out SafeExRpcManageHandle iExRpcManage);

		// Token: 0x06000C75 RID: 3189
		[DllImport("exrpc32.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void DiagnosticCtxGetContext(out THREAD_DIAG_CONTEXT ctx);

		// Token: 0x06000C76 RID: 3190
		[DllImport("exrpc32.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void DiagnosticCtxReleaseContext();

		// Token: 0x06000C77 RID: 3191
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		internal static extern void DiagnosticCtxLogLocation(int lid);

		// Token: 0x06000C78 RID: 3192
		[DllImport("exrpc32.dll", CharSet = CharSet.Ansi, EntryPoint = "DiagnosticCtxLogInfoEx1", ExactSpelling = true)]
		internal static extern void DiagnosticCtxLogInfo(int lid, uint value, string message);

		// Token: 0x06000C79 RID: 3193
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetTLSPerformanceContext(out PerformanceContext ctx);

		// Token: 0x06000C7A RID: 3194
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		internal static extern void SetForceMapiRpc([MarshalAs(UnmanagedType.Bool)] bool forceMapiRpc);

		// Token: 0x06000C7B RID: 3195
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		internal static extern void AbandonNotificationsDuringShutdown([MarshalAs(UnmanagedType.Bool)] bool abandon);

		// Token: 0x06000C7C RID: 3196
		[DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int StgOpenStorage(string pwcsName, IntPtr pstgPriority, NativeMethods.StorageMode mode, IntPtr snbExclude, int reserved, out IStorage ppstgOpen);

		// Token: 0x06000C7D RID: 3197
		[DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int StgCreateStorageEx(string pwcsName, NativeMethods.StorageMode mode, NativeMethods.StorageFormat format, int grfAttrs, IntPtr pStgOptions, int reserved, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppstgOpen);

		// Token: 0x04001136 RID: 4406
		internal const string EXRPC32 = "exrpc32.dll";

		// Token: 0x04001137 RID: 4407
		internal const uint DIAG_CTX_RECORD_LEN_MASK = 4026531840U;

		// Token: 0x04001138 RID: 4408
		internal const uint DIAG_CTX_RECORD_LEN_SHIFT = 28U;

		// Token: 0x04001139 RID: 4409
		internal const uint DIAG_CTX_RECORD_LAYOUT_MASK = 267386880U;

		// Token: 0x0400113A RID: 4410
		internal const uint DIAG_CTX_RECORD_LAYOUT_SHIFT = 20U;

		// Token: 0x0400113B RID: 4411
		internal const uint DIAG_CTX_RECORD_LID_MASK = 1048575U;

		// Token: 0x0400113C RID: 4412
		internal const uint THREAD_DIAG_CTX_BUFF_SIZE = 512U;

		// Token: 0x020002A0 RID: 672
		[Flags]
		internal enum StorageMode : uint
		{
			// Token: 0x0400113E RID: 4414
			Direct = 0U,
			// Token: 0x0400113F RID: 4415
			Transacted = 65536U,
			// Token: 0x04001140 RID: 4416
			Simple = 134217728U,
			// Token: 0x04001141 RID: 4417
			Read = 0U,
			// Token: 0x04001142 RID: 4418
			Write = 1U,
			// Token: 0x04001143 RID: 4419
			ReadWrite = 2U,
			// Token: 0x04001144 RID: 4420
			ShareDenyNone = 64U,
			// Token: 0x04001145 RID: 4421
			ShareDenyRead = 48U,
			// Token: 0x04001146 RID: 4422
			ShareDenyWrite = 32U,
			// Token: 0x04001147 RID: 4423
			ShareExclusive = 16U,
			// Token: 0x04001148 RID: 4424
			Priority = 262144U,
			// Token: 0x04001149 RID: 4425
			DeleteOnRelease = 67108864U,
			// Token: 0x0400114A RID: 4426
			NoScratch = 1048576U,
			// Token: 0x0400114B RID: 4427
			Create = 4096U,
			// Token: 0x0400114C RID: 4428
			Convert = 131072U,
			// Token: 0x0400114D RID: 4429
			FailIfThere = 0U,
			// Token: 0x0400114E RID: 4430
			NoSnapshot = 2097152U,
			// Token: 0x0400114F RID: 4431
			DirectSWMR = 4194304U
		}

		// Token: 0x020002A1 RID: 673
		internal enum StorageFormat
		{
			// Token: 0x04001151 RID: 4433
			Storage,
			// Token: 0x04001152 RID: 4434
			File = 3,
			// Token: 0x04001153 RID: 4435
			Any,
			// Token: 0x04001154 RID: 4436
			DocumentFile
		}
	}
}

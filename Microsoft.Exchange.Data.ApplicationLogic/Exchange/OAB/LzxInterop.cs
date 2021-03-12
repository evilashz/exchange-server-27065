using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200014C RID: 332
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class LzxInterop
	{
		// Token: 0x06000D6F RID: 3439
		[DllImport("mspatchLInterop.dll", EntryPoint = "InteropRawLzxCompressBuffer", SetLastError = true)]
		public static extern uint RawLzxCompressBuffer(IntPtr uncompressedBuffer, int uncompressedSize, IntPtr compressedBuffer, int compressedBufferSize, out int compressedSize);

		// Token: 0x06000D70 RID: 3440
		[DllImport("mspatchLInterop.dll", EntryPoint = "InteropCreateRawLzxPatchDataFromBuffers", SetLastError = true)]
		public static extern uint CreateRawLzxPatchDataFromBuffers(IntPtr oldDataBuffer, int oldDataSize, IntPtr newDataBuffer, int newDataSize, IntPtr patchDataBuffer, int patchDataSize, out int actualPatchDataSize);

		// Token: 0x06000D71 RID: 3441
		[DllImport("mspatchLInterop.dll", EntryPoint = "InteropApplyRawLzxPatchToBuffer", SetLastError = true)]
		public static extern uint ApplyRawLzxPatchToBuffer(IntPtr oldDataBuffer, int oldDataSize, IntPtr patchDataBuffer, int patchDataSize, IntPtr newDataBuffer, int newDataSize);

		// Token: 0x04000724 RID: 1828
		public const uint Win32ErrorInsufficientBuffer = 122U;
	}
}

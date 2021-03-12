using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001EB RID: 491
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct FastTransferBlock
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x00021584 File Offset: 0x0001F784
		internal unsafe FastTransferBlock(FxBlock* pBlock)
		{
			this.Buffer = Array<byte>.New(pBlock->bufferSize);
			if (pBlock->bufferSize > 0)
			{
				Marshal.Copy(pBlock->buffer, this.Buffer, 0, pBlock->bufferSize);
			}
			this.Steps = pBlock->steps;
			this.Progress = pBlock->progress;
			this.State = (FastTransferState)pBlock->state;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000215E7 File Offset: 0x0001F7E7
		private FastTransferBlock(FastTransferState state)
		{
			this.Buffer = Array<byte>.Empty;
			this.Steps = 0U;
			this.Progress = 0U;
			this.State = state;
		}

		// Token: 0x040006A9 RID: 1705
		public static readonly FastTransferBlock Done = new FastTransferBlock(FastTransferState.Done);

		// Token: 0x040006AA RID: 1706
		public static readonly FastTransferBlock Error = new FastTransferBlock(FastTransferState.Error);

		// Token: 0x040006AB RID: 1707
		public static readonly FastTransferBlock Partial = new FastTransferBlock(FastTransferState.Partial);

		// Token: 0x040006AC RID: 1708
		public readonly byte[] Buffer;

		// Token: 0x040006AD RID: 1709
		public readonly uint Steps;

		// Token: 0x040006AE RID: 1710
		public readonly uint Progress;

		// Token: 0x040006AF RID: 1711
		public readonly FastTransferState State;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000245 RID: 581
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060020B8 RID: 8376
		int InputBlockSize { get; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060020B9 RID: 8377
		int OutputBlockSize { get; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060020BA RID: 8378
		bool CanTransformMultipleBlocks { get; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060020BB RID: 8379
		bool CanReuseTransform { get; }

		// Token: 0x060020BC RID: 8380
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x060020BD RID: 8381
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}

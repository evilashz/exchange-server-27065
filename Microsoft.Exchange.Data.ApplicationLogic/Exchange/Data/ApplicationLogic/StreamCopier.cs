using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200013E RID: 318
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class StreamCopier
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x00036924 File Offset: 0x00034B24
		public StreamCopier(int bufferSize)
		{
			this.bufferSize = bufferSize;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00036935 File Offset: 0x00034B35
		public CopyStreamResult CopyStream(Stream input, Stream output)
		{
			return this.CopyStream(input, output, null, delegate()
			{
			});
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00036960 File Offset: 0x00034B60
		public CopyStreamResult CopyStream(Stream input, Stream output, HashAlgorithm hashAlgorithm, Action abortFileOperation)
		{
			Stopwatch stopwatch = new Stopwatch();
			Stopwatch stopwatch2 = new Stopwatch();
			long num = 0L;
			byte[] array = new byte[this.bufferSize];
			for (;;)
			{
				abortFileOperation();
				stopwatch.Start();
				int num2 = input.Read(array, 0, array.Length);
				stopwatch.Stop();
				if (num2 == 0)
				{
					break;
				}
				if (hashAlgorithm != null)
				{
					hashAlgorithm.TransformBlock(array, 0, num2, array, 0);
				}
				stopwatch2.Start();
				output.Write(array, 0, num2);
				stopwatch2.Stop();
				num += (long)num2;
			}
			if (hashAlgorithm != null)
			{
				hashAlgorithm.TransformFinalBlock(array, 0, 0);
			}
			return new CopyStreamResult(stopwatch.Elapsed, stopwatch2.Elapsed, num);
		}

		// Token: 0x040006F4 RID: 1780
		private readonly int bufferSize;
	}
}

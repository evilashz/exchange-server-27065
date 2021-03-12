using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000175 RID: 373
	internal interface IByteSource
	{
		// Token: 0x06000FFF RID: 4095
		bool GetOutputChunk(out byte[] chunkBuffer, out int chunkOffset, out int chunkLength);

		// Token: 0x06001000 RID: 4096
		void ReportOutput(int readCount);
	}
}

using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001AB RID: 427
	public interface ICompressAndObfuscate
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600087D RID: 2173
		int MaxCompressionSize { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600087E RID: 2174
		int MinCompressionSize { get; }

		// Token: 0x0600087F RID: 2175
		bool TryCompress(ArraySegment<byte> sources, ArraySegment<byte> destination, out int compressedSize);

		// Token: 0x06000880 RID: 2176
		bool TryExpand(ArraySegment<byte> sources, ArraySegment<byte> destination);

		// Token: 0x06000881 RID: 2177
		void Obfuscate(ArraySegment<byte> buffer);
	}
}

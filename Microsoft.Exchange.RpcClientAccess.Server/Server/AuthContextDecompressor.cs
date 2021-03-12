using System;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000002 RID: 2
	internal sealed class AuthContextDecompressor : IAuthenticationContextCompression
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool TryDecompress(ArraySegment<byte> source, ArraySegment<byte> destination)
		{
			return CompressAndObfuscate.Instance.TryExpand(source, destination);
		}
	}
}

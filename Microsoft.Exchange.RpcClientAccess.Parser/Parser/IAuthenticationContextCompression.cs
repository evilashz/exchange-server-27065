using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F7 RID: 759
	internal interface IAuthenticationContextCompression
	{
		// Token: 0x06001191 RID: 4497
		bool TryDecompress(ArraySegment<byte> source, ArraySegment<byte> destination);
	}
}

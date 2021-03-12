using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RpcServiceException : Exception
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00009172 File Offset: 0x00007372
		protected RpcServiceException(string message, int status, Exception innerException) : base(message, innerException)
		{
			this.RpcStatus = status;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009183 File Offset: 0x00007383
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000918B File Offset: 0x0000738B
		public int RpcStatus { get; private set; }

		// Token: 0x06000299 RID: 665 RVA: 0x00009194 File Offset: 0x00007394
		public static uint GetHResultFromStatusCode(int statusCode)
		{
			return (uint)(statusCode + -2147024896);
		}

		// Token: 0x04000268 RID: 616
		public const int RPC_S_OUT_OF_MEMORY = 14;

		// Token: 0x04000269 RID: 617
		public const int RPC_S_INVALID_ARG = 87;

		// Token: 0x0400026A RID: 618
		public const int RPC_S_INVALID_BINDING = 1702;

		// Token: 0x0400026B RID: 619
		public const int RPC_S_SERVER_UNAVAILABLE = 1722;

		// Token: 0x0400026C RID: 620
		public const int RPC_S_SERVER_TOO_BUSY = 1723;

		// Token: 0x0400026D RID: 621
		public const int RPC_S_CALL_FAILED = 1726;

		// Token: 0x0400026E RID: 622
		public const int RPC_S_CALL_FAILED_DNE = 1727;
	}
}

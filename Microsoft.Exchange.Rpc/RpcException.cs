using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	internal class RpcException : Exception
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00001010 File Offset: 0x00000410
		public int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00001070 File Offset: 0x00000470
		public RpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00001054 File Offset: 0x00000454
		public RpcException(string message, int hr, Exception innerException) : base(message, innerException)
		{
			base.HResult = hr;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00001038 File Offset: 0x00000438
		public RpcException(string message, int hr) : base(message)
		{
			base.HResult = hr;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00001024 File Offset: 0x00000424
		public RpcException(string message) : base(message)
		{
		}
	}
}

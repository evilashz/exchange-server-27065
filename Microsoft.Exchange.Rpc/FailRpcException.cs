using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class FailRpcException : Exception
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000011D4 File Offset: 0x000005D4
		public int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00001234 File Offset: 0x00000634
		public FailRpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00001218 File Offset: 0x00000618
		public FailRpcException(string message, int hr, Exception innerException) : base(message, innerException)
		{
			base.HResult = hr;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000011FC File Offset: 0x000005FC
		public FailRpcException(string message, int hr) : base(message)
		{
			base.HResult = hr;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000011E8 File Offset: 0x000005E8
		public FailRpcException(string message) : base(message)
		{
		}
	}
}

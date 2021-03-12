using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class CallCancelledException : RpcException
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x00001150 File Offset: 0x00000550
		public CallCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00001130 File Offset: 0x00000530
		public CallCancelledException(string message) : base(message)
		{
			base.HResult = 1818;
		}
	}
}

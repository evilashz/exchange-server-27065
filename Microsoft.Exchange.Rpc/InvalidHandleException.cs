using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class InvalidHandleException : RpcException
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x000011BC File Offset: 0x000005BC
		public InvalidHandleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000011A0 File Offset: 0x000005A0
		public InvalidHandleException(string message) : base(message)
		{
			base.HResult = 6;
		}
	}
}

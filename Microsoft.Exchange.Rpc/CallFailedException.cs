using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class CallFailedException : RpcException
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x00001188 File Offset: 0x00000588
		public CallFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00001168 File Offset: 0x00000568
		public CallFailedException(string message) : base(message)
		{
			base.HResult = 1726;
		}
	}
}

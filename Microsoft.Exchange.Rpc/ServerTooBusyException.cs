using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal class ServerTooBusyException : RpcException
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00001118 File Offset: 0x00000518
		public ServerTooBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000010F8 File Offset: 0x000004F8
		public ServerTooBusyException(string message) : base(message)
		{
			base.HResult = 1723;
		}
	}
}

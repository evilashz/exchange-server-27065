using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002BA RID: 698
	[Serializable]
	internal class SyncMigrationRpcTransientException : RpcException
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00030798 File Offset: 0x0002FB98
		public SyncMigrationRpcTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00030780 File Offset: 0x0002FB80
		public SyncMigrationRpcTransientException(string message, int hr) : base(message, hr)
		{
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003076C File Offset: 0x0002FB6C
		public SyncMigrationRpcTransientException(string message) : base(message)
		{
		}
	}
}

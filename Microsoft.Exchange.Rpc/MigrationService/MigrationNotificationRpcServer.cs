using System;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x0200029C RID: 668
	internal abstract class MigrationNotificationRpcServer : RpcServerBase
	{
		// Token: 0x06000C59 RID: 3161
		public abstract byte[] UpdateMigrationRequest(int version, byte[] pInBytes);

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002CE28 File Offset: 0x0002C228
		public MigrationNotificationRpcServer()
		{
		}

		// Token: 0x04000D55 RID: 3413
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.MigrationNotificationService_v1_0_s_ifspec;
	}
}

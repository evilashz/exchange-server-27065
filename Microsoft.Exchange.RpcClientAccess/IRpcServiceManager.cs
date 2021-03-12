using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200001C RID: 28
	internal interface IRpcServiceManager
	{
		// Token: 0x060000C4 RID: 196
		void StopService();

		// Token: 0x060000C5 RID: 197
		void AddTcpPort(string port);

		// Token: 0x060000C6 RID: 198
		void AddHttpPort(string port);

		// Token: 0x060000C7 RID: 199
		void EnableLrpc();

		// Token: 0x060000C8 RID: 200
		void AddServer(Action starter, Action stopper);
	}
}

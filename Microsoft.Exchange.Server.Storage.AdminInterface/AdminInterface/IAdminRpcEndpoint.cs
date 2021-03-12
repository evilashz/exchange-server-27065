using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000004 RID: 4
	public interface IAdminRpcEndpoint
	{
		// Token: 0x0600000E RID: 14
		bool StartInterface(Guid? instanceGuid, bool isLocalOnly);

		// Token: 0x0600000F RID: 15
		void StopInterface();
	}
}

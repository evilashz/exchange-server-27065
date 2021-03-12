using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200002C RID: 44
	internal interface IServerPicker<ManagedObjectType, ContextObjectType> where ManagedObjectType : class
	{
		// Token: 0x06000280 RID: 640
		ManagedObjectType PickNextServer(ContextObjectType context);

		// Token: 0x06000281 RID: 641
		ManagedObjectType PickNextServer(ContextObjectType context, out int totalServers);

		// Token: 0x06000282 RID: 642
		ManagedObjectType PickNextServer(ContextObjectType context, Guid tenantGuid, out int totalServers);

		// Token: 0x06000283 RID: 643
		void ServerUnavailable(ManagedObjectType server);
	}
}

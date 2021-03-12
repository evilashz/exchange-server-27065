using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000707 RID: 1799
	internal class ExchangeServerRoleSchema : ADPresentationSchema
	{
		// Token: 0x060054C9 RID: 21705 RVA: 0x00132708 File Offset: 0x00130908
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ServerSchema>();
		}

		// Token: 0x040038E8 RID: 14568
		public static readonly ADPropertyDefinition IsHubTransportServer = ServerSchema.IsHubTransportServer;

		// Token: 0x040038E9 RID: 14569
		public static readonly ADPropertyDefinition IsClientAccessServer = ServerSchema.IsClientAccessServer;

		// Token: 0x040038EA RID: 14570
		public static readonly ADPropertyDefinition IsExchange2007OrLater = ServerSchema.IsExchange2007OrLater;

		// Token: 0x040038EB RID: 14571
		public static readonly ADPropertyDefinition IsEdgeServer = ServerSchema.IsEdgeServer;

		// Token: 0x040038EC RID: 14572
		public static readonly ADPropertyDefinition IsMailboxServer = ServerSchema.IsMailboxServer;

		// Token: 0x040038ED RID: 14573
		public static readonly ADPropertyDefinition IsProvisionedServer = ServerSchema.IsProvisionedServer;

		// Token: 0x040038EE RID: 14574
		public static readonly ADPropertyDefinition IsUnifiedMessagingServer = ServerSchema.IsUnifiedMessagingServer;

		// Token: 0x040038EF RID: 14575
		public static readonly ADPropertyDefinition IsCafeServer = ServerSchema.IsCafeServer;

		// Token: 0x040038F0 RID: 14576
		public static readonly ADPropertyDefinition IsFrontendTransportServer = ServerSchema.IsFrontendTransportServer;
	}
}

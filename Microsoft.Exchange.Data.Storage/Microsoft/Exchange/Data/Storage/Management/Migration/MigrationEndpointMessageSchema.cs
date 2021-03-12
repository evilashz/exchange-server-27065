using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management.Migration
{
	// Token: 0x02000A27 RID: 2599
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationEndpointMessageSchema
	{
		// Token: 0x04003602 RID: 13826
		public const string MigrationEndpointMessageClass = "IPM.MS-Exchange.MigrationEndpoint";

		// Token: 0x04003603 RID: 13827
		public static readonly StorePropertyDefinition MigrationEndpointType = InternalSchema.MigrationEndpointType;

		// Token: 0x04003604 RID: 13828
		public static readonly StorePropertyDefinition MigrationEndpointName = InternalSchema.MigrationEndpointName;

		// Token: 0x04003605 RID: 13829
		public static readonly StorePropertyDefinition RemoteHostName = InternalSchema.MigrationEndpointRemoteHostName;

		// Token: 0x04003606 RID: 13830
		public static readonly StorePropertyDefinition ExchangeServer = InternalSchema.MigrationEndpointExchangeServer;

		// Token: 0x04003607 RID: 13831
		public static readonly StorePropertyDefinition LastModifiedTime = InternalSchema.MigrationSubscriptionSettingsLastModifiedTime;

		// Token: 0x04003608 RID: 13832
		public static readonly StorePropertyDefinition MigrationEndpointGuid = InternalSchema.MigrationEndpointGuid;
	}
}

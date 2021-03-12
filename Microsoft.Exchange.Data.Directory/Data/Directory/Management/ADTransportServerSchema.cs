using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000778 RID: 1912
	internal class ADTransportServerSchema : TransportServerSchema
	{
		// Token: 0x06005E06 RID: 24070 RVA: 0x00143CF7 File Offset: 0x00141EF7
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ActiveDirectoryServerSchema>();
		}

		// Token: 0x04003FF2 RID: 16370
		public static readonly ADPropertyDefinition MaxConcurrentMailboxSubmissions = ActiveDirectoryServerSchema.MaxConcurrentMailboxSubmissions;

		// Token: 0x04003FF3 RID: 16371
		public static readonly ADPropertyDefinition UseDowngradedExchangeServerAuth = ActiveDirectoryServerSchema.UseDowngradedExchangeServerAuth;

		// Token: 0x04003FF4 RID: 16372
		public static readonly ADPropertyDefinition TransportSyncAccountsSuccessivePoisonItemThreshold = ActiveDirectoryServerSchema.TransportSyncAccountsSuccessivePoisonItemThreshold;

		// Token: 0x04003FF5 RID: 16373
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogEnabled = ActiveDirectoryServerSchema.TransportSyncHubHealthLogEnabled;

		// Token: 0x04003FF6 RID: 16374
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogFilePath = ActiveDirectoryServerSchema.TransportSyncHubHealthLogFilePath;

		// Token: 0x04003FF7 RID: 16375
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogMaxAge = ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxAge;

		// Token: 0x04003FF8 RID: 16376
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogMaxDirectorySize = ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxDirectorySize;

		// Token: 0x04003FF9 RID: 16377
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogMaxFileSize = ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxFileSize;
	}
}

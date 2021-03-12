using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000507 RID: 1287
	internal sealed class MsoTenantCookieContainerSchema : OrganizationSchema
	{
		// Token: 0x040026F8 RID: 9976
		public static readonly ADPropertyDefinition MsoForwardSyncRecipientCookie = MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie;

		// Token: 0x040026F9 RID: 9977
		public static readonly ADPropertyDefinition MsoForwardSyncNonRecipientCookie = MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie;

		// Token: 0x040026FA RID: 9978
		public static readonly ADPropertyDefinition ExternalDirectoryOrganizationId = ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId;

		// Token: 0x040026FB RID: 9979
		public static readonly ADPropertyDefinition DirSyncServiceInstance = ExchangeConfigurationUnitSchema.DirSyncServiceInstance;
	}
}

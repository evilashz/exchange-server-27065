using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E2 RID: 1250
	internal class MiniEmailTransportSchema : ADObjectSchema
	{
		// Token: 0x040025C9 RID: 9673
		public static readonly ADPropertyDefinition Server = ADEmailTransportSchema.Server;

		// Token: 0x040025CA RID: 9674
		public static readonly ADPropertyDefinition InternalConnectionSettings = PopImapAdConfigurationSchema.InternalConnectionSettings;

		// Token: 0x040025CB RID: 9675
		public static readonly ADPropertyDefinition ExternalConnectionSettings = PopImapAdConfigurationSchema.ExternalConnectionSettings;

		// Token: 0x040025CC RID: 9676
		public static readonly ADPropertyDefinition UnencryptedOrTLSBindings = PopImapAdConfigurationSchema.UnencryptedOrTLSBindings;

		// Token: 0x040025CD RID: 9677
		public static readonly ADPropertyDefinition SSLBindings = PopImapAdConfigurationSchema.SSLBindings;

		// Token: 0x040025CE RID: 9678
		public static readonly ADPropertyDefinition LoginType = PopImapAdConfigurationSchema.LoginType;
	}
}

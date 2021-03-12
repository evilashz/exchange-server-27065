using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000319 RID: 793
	internal sealed class ActiveSyncMailboxPolicySchema : MobileMailboxPolicySchema
	{
		// Token: 0x040016C1 RID: 5825
		public static readonly ADPropertyDefinition MaxDevicePasswordFailedAttempts = MobileMailboxPolicySchema.MaxPasswordFailedAttempts;

		// Token: 0x040016C2 RID: 5826
		public static readonly ADPropertyDefinition MaxInactivityTimeDeviceLock = MobileMailboxPolicySchema.MaxInactivityTimeLock;

		// Token: 0x040016C3 RID: 5827
		public static readonly ADPropertyDefinition DevicePasswordExpiration = MobileMailboxPolicySchema.PasswordExpiration;

		// Token: 0x040016C4 RID: 5828
		public static readonly ADPropertyDefinition DevicePasswordHistory = MobileMailboxPolicySchema.PasswordHistory;

		// Token: 0x040016C5 RID: 5829
		public static readonly ADPropertyDefinition MinDevicePasswordLength = MobileMailboxPolicySchema.MinPasswordLength;

		// Token: 0x040016C6 RID: 5830
		public static readonly ADPropertyDefinition MinDevicePasswordComplexCharacters = MobileMailboxPolicySchema.MinPasswordComplexCharacters;

		// Token: 0x040016C7 RID: 5831
		public static readonly ADPropertyDefinition AlphanumericDevicePasswordRequired = MobileMailboxPolicySchema.AlphanumericPasswordRequired;

		// Token: 0x040016C8 RID: 5832
		public static readonly ADPropertyDefinition DevicePasswordEnabled = MobileMailboxPolicySchema.PasswordEnabled;

		// Token: 0x040016C9 RID: 5833
		public static readonly ADPropertyDefinition AllowSimpleDevicePassword = MobileMailboxPolicySchema.AllowSimplePassword;

		// Token: 0x040016CA RID: 5834
		public static readonly ADPropertyDefinition IsDefaultPolicy = MobileMailboxPolicySchema.IsDefault;
	}
}

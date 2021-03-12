using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200021D RID: 541
	[Flags]
	public enum UserAccountControlFlags
	{
		// Token: 0x04000C5A RID: 3162
		None = 0,
		// Token: 0x04000C5B RID: 3163
		Script = 1,
		// Token: 0x04000C5C RID: 3164
		AccountDisabled = 2,
		// Token: 0x04000C5D RID: 3165
		HomeDirectoryRequired = 8,
		// Token: 0x04000C5E RID: 3166
		Lockout = 16,
		// Token: 0x04000C5F RID: 3167
		PasswordNotRequired = 32,
		// Token: 0x04000C60 RID: 3168
		CannotChangePassowrd = 64,
		// Token: 0x04000C61 RID: 3169
		EncryptedTextPasswordAllowed = 128,
		// Token: 0x04000C62 RID: 3170
		TemporaryDuplicateAccount = 256,
		// Token: 0x04000C63 RID: 3171
		NormalAccount = 512,
		// Token: 0x04000C64 RID: 3172
		InterDomainTrustAccount = 2048,
		// Token: 0x04000C65 RID: 3173
		WorkstationTrustAccount = 4096,
		// Token: 0x04000C66 RID: 3174
		ServerTrustAccount = 8192,
		// Token: 0x04000C67 RID: 3175
		DoNotExpirePassword = 65536,
		// Token: 0x04000C68 RID: 3176
		MnsLogonAccount = 131072,
		// Token: 0x04000C69 RID: 3177
		SmartCardRequired = 262144,
		// Token: 0x04000C6A RID: 3178
		TrustedForDelegation = 524288,
		// Token: 0x04000C6B RID: 3179
		NotDelegated = 1048576,
		// Token: 0x04000C6C RID: 3180
		UseDesKeyOnly = 2097152,
		// Token: 0x04000C6D RID: 3181
		DoNotRequirePreauthentication = 4194304,
		// Token: 0x04000C6E RID: 3182
		PasswordExpired = 8388608,
		// Token: 0x04000C6F RID: 3183
		TrustedToAuthenticateForDelegation = 16777216
	}
}

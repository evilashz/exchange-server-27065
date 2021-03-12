using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C79 RID: 3193
	internal enum SecurityStatus
	{
		// Token: 0x04003B47 RID: 15175
		OK,
		// Token: 0x04003B48 RID: 15176
		ContinueNeeded = 590610,
		// Token: 0x04003B49 RID: 15177
		CompleteNeeded,
		// Token: 0x04003B4A RID: 15178
		CompAndContinue,
		// Token: 0x04003B4B RID: 15179
		ContextExpired = 590615,
		// Token: 0x04003B4C RID: 15180
		CredentialsNeeded = 590624,
		// Token: 0x04003B4D RID: 15181
		Renegotiate,
		// Token: 0x04003B4E RID: 15182
		OutOfMemory = -2146893056,
		// Token: 0x04003B4F RID: 15183
		InvalidHandle,
		// Token: 0x04003B50 RID: 15184
		Unsupported,
		// Token: 0x04003B51 RID: 15185
		TargetUnknown,
		// Token: 0x04003B52 RID: 15186
		InternalError,
		// Token: 0x04003B53 RID: 15187
		PackageNotFound,
		// Token: 0x04003B54 RID: 15188
		NotOwner,
		// Token: 0x04003B55 RID: 15189
		CannotInstall,
		// Token: 0x04003B56 RID: 15190
		InvalidToken,
		// Token: 0x04003B57 RID: 15191
		CannotPack,
		// Token: 0x04003B58 RID: 15192
		QopNotSupported,
		// Token: 0x04003B59 RID: 15193
		NoImpersonation,
		// Token: 0x04003B5A RID: 15194
		LogonDenied,
		// Token: 0x04003B5B RID: 15195
		UnknownCredentials,
		// Token: 0x04003B5C RID: 15196
		NoCredentials,
		// Token: 0x04003B5D RID: 15197
		MessageAltered,
		// Token: 0x04003B5E RID: 15198
		OutOfSequence,
		// Token: 0x04003B5F RID: 15199
		NoAuthenticatingAuthority,
		// Token: 0x04003B60 RID: 15200
		IncompleteMessage = -2146893032,
		// Token: 0x04003B61 RID: 15201
		IncompleteCredentials = -2146893024,
		// Token: 0x04003B62 RID: 15202
		BufferNotEnough,
		// Token: 0x04003B63 RID: 15203
		WrongPrincipal,
		// Token: 0x04003B64 RID: 15204
		TimeSkew = -2146893020,
		// Token: 0x04003B65 RID: 15205
		UntrustedRoot,
		// Token: 0x04003B66 RID: 15206
		IllegalMessage,
		// Token: 0x04003B67 RID: 15207
		CertUnknown,
		// Token: 0x04003B68 RID: 15208
		CertExpired,
		// Token: 0x04003B69 RID: 15209
		EncryptFailure,
		// Token: 0x04003B6A RID: 15210
		DecryptFailure = -2146893008,
		// Token: 0x04003B6B RID: 15211
		AlgorithmMismatch,
		// Token: 0x04003B6C RID: 15212
		SecurityQosFailed,
		// Token: 0x04003B6D RID: 15213
		SmartcardLogonRequired = -2146892994,
		// Token: 0x04003B6E RID: 15214
		UnsupportedPreauth = -2146892989,
		// Token: 0x04003B6F RID: 15215
		BadBindings = -2146892986,
		// Token: 0x04003B70 RID: 15216
		ErrorMask = -2147483648,
		// Token: 0x04003B71 RID: 15217
		TLS1_0NotSupported,
		// Token: 0x04003B72 RID: 15218
		UnexpectedExchangeAuthBlob = 1,
		// Token: 0x04003B73 RID: 15219
		ExtendedProtectionMissingSpn,
		// Token: 0x04003B74 RID: 15220
		ExtendedProtectionWrongSpn,
		// Token: 0x04003B75 RID: 15221
		ExtendedProtectionOSNotPatched
	}
}

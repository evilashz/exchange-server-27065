using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.com.IPC.WSService
{
	// Token: 0x020009FF RID: 2559
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "RmsErrors", Namespace = "http://microsoft.com/IPC/WSService")]
	public enum RmsErrors
	{
		// Token: 0x04002F43 RID: 12099
		[EnumMember]
		RmsInternalFailure,
		// Token: 0x04002F44 RID: 12100
		[EnumMember]
		InvalidArgument,
		// Token: 0x04002F45 RID: 12101
		[EnumMember]
		UnsupportedDataVersion,
		// Token: 0x04002F46 RID: 12102
		[EnumMember]
		ClusterDecommissioned,
		// Token: 0x04002F47 RID: 12103
		[EnumMember]
		RequestedFeatureIsDisabled,
		// Token: 0x04002F48 RID: 12104
		[EnumMember]
		UnauthorizedAccess,
		// Token: 0x04002F49 RID: 12105
		[EnumMember]
		VerifyMachineCertificateChainFailed,
		// Token: 0x04002F4A RID: 12106
		[EnumMember]
		EmailAddressVerificationFailure,
		// Token: 0x04002F4B RID: 12107
		[EnumMember]
		UserNotFoundInActiveDirectory,
		// Token: 0x04002F4C RID: 12108
		[EnumMember]
		GroupIdentityCredentialHasInvalidSignature,
		// Token: 0x04002F4D RID: 12109
		[EnumMember]
		GroupIdentityCredentialHasInvalidTimeRange,
		// Token: 0x04002F4E RID: 12110
		[EnumMember]
		UntrustedGroupIdentityCredential,
		// Token: 0x04002F4F RID: 12111
		[EnumMember]
		ExcludedGroupIdentityCredential,
		// Token: 0x04002F50 RID: 12112
		[EnumMember]
		UnexpectedGroupIdentityCredential,
		// Token: 0x04002F51 RID: 12113
		[EnumMember]
		UnauthorizedEmailAddress,
		// Token: 0x04002F52 RID: 12114
		[EnumMember]
		IssuanceLicenseHasInvalidSignature,
		// Token: 0x04002F53 RID: 12115
		[EnumMember]
		IssuanceLicenseHasInvalidTimeRange,
		// Token: 0x04002F54 RID: 12116
		[EnumMember]
		UntrustedIssuanceLicense,
		// Token: 0x04002F55 RID: 12117
		[EnumMember]
		UnknownRightsTemplate,
		// Token: 0x04002F56 RID: 12118
		[EnumMember]
		UnexpectedIssuanceLicense,
		// Token: 0x04002F57 RID: 12119
		[EnumMember]
		NoRightsForRequestedPrincipal
	}
}

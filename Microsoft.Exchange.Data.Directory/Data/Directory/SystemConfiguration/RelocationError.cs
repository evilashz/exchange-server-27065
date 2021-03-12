using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D7 RID: 1495
	public enum RelocationError : byte
	{
		// Token: 0x04002F4D RID: 12109
		None,
		// Token: 0x04002F4E RID: 12110
		ADTransientError,
		// Token: 0x04002F4F RID: 12111
		GLSTransientError = 3,
		// Token: 0x04002F50 RID: 12112
		HighADReplicationLatency,
		// Token: 0x04002F51 RID: 12113
		RequestCanceled,
		// Token: 0x04002F52 RID: 12114
		UserExperienceCreateMailboxFailure,
		// Token: 0x04002F53 RID: 12115
		UserExperienceSetPasswordFailure,
		// Token: 0x04002F54 RID: 12116
		UserExperienceRemoveMailboxFailure,
		// Token: 0x04002F55 RID: 12117
		UserExperienceVerificationFailure,
		// Token: 0x04002F56 RID: 12118
		SyncFailureDueToSourceObjectBeingModified,
		// Token: 0x04002F57 RID: 12119
		ValidationTransientFailureEncountered,
		// Token: 0x04002F58 RID: 12120
		AddSidHistorySourcePdcTransferred,
		// Token: 0x04002F59 RID: 12121
		LastTransientError = 127,
		// Token: 0x04002F5A RID: 12122
		ADPermanentErrorSourceForest,
		// Token: 0x04002F5B RID: 12123
		ADPermanentErrorDestinationForest,
		// Token: 0x04002F5C RID: 12124
		GLSPermanentErrorTryGetTenantForestsByOrgGuid,
		// Token: 0x04002F5D RID: 12125
		GLSTenantNotFoundError,
		// Token: 0x04002F5E RID: 12126
		GLSPermanentError,
		// Token: 0x04002F5F RID: 12127
		ADUnkownSchemaAttribute,
		// Token: 0x04002F60 RID: 12128
		TooManyTransitions,
		// Token: 0x04002F61 RID: 12129
		RPCPermanentError,
		// Token: 0x04002F62 RID: 12130
		DestinationAuditingDisabled,
		// Token: 0x04002F63 RID: 12131
		MemberInAliasPermanentError,
		// Token: 0x04002F64 RID: 12132
		ObjectsWithCnfNameFoundInSource,
		// Token: 0x04002F65 RID: 12133
		UserExperiencePermanentFailure,
		// Token: 0x04002F66 RID: 12134
		ADDriverValidationFailed,
		// Token: 0x04002F67 RID: 12135
		StaleRelocation,
		// Token: 0x04002F68 RID: 12136
		DataValidationFailed,
		// Token: 0x04002F69 RID: 12137
		BlockingConstraintsDetected,
		// Token: 0x04002F6A RID: 12138
		DuplicateSamAccountNameInSource,
		// Token: 0x04002F6B RID: 12139
		MissingGlsDomainRecord,
		// Token: 0x04002F6C RID: 12140
		MissingMServDomainRecord,
		// Token: 0x04002F6D RID: 12141
		InvalidMXRecord,
		// Token: 0x04002F6E RID: 12142
		UserExperienceCreateMailboxPermanentFailure,
		// Token: 0x04002F6F RID: 12143
		UnclassifiedPermanentError = 255
	}
}

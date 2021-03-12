using System;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200002E RID: 46
	internal enum ReportingErrorCode
	{
		// Token: 0x0400006A RID: 106
		ErrorMissingTenantDomainInRequest,
		// Token: 0x0400006B RID: 107
		ErrorTenantNotInOrgScope,
		// Token: 0x0400006C RID: 108
		ErrorTenantNotResolved,
		// Token: 0x0400006D RID: 109
		InvalidFormatQuery,
		// Token: 0x0400006E RID: 110
		ErrorSchemaInitializationFail,
		// Token: 0x0400006F RID: 111
		ErrorOAuthPartnerApplicationNotLinkToServiceAccount,
		// Token: 0x04000070 RID: 112
		ErrorOAuthAuthorizationNoAccount,
		// Token: 0x04000071 RID: 113
		ErrorVersionAmbiguous,
		// Token: 0x04000072 RID: 114
		ErrorInvalidVersion,
		// Token: 0x04000073 RID: 115
		ErrorOverBudget,
		// Token: 0x04000074 RID: 116
		ADTransientError,
		// Token: 0x04000075 RID: 117
		ADOperationError,
		// Token: 0x04000076 RID: 118
		CreateRunspaceConfigTimeoutError,
		// Token: 0x04000077 RID: 119
		ConnectionFailedException,
		// Token: 0x04000078 RID: 120
		InvalidQueryException,
		// Token: 0x04000079 RID: 121
		DataMartTimeoutException,
		// Token: 0x0400007A RID: 122
		UnknownError
	}
}

using System;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000218 RID: 536
	internal enum PswsErrorCode
	{
		// Token: 0x04000498 RID: 1176
		AuthZUserError = 830001,
		// Token: 0x04000499 RID: 1177
		MemberShipIdError,
		// Token: 0x0400049A RID: 1178
		OverBudgetException,
		// Token: 0x0400049B RID: 1179
		GetISSError,
		// Token: 0x0400049C RID: 1180
		ClientAccessRuleBlock,
		// Token: 0x0400049D RID: 1181
		CmdletExecutionFailure = 840001,
		// Token: 0x0400049E RID: 1182
		CmdletShouldContinue,
		// Token: 0x0400049F RID: 1183
		RetriableTransientException = 841101,
		// Token: 0x040004A0 RID: 1184
		ParameterValidationFailed = 842001,
		// Token: 0x040004A1 RID: 1185
		DuplicateObjectCreation,
		// Token: 0x040004A2 RID: 1186
		ScopePermissionDenied = 843001,
		// Token: 0x040004A3 RID: 1187
		UnkownFailure = 849999
	}
}

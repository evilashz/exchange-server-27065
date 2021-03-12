using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000053 RID: 83
	internal enum SetAggregatedAccountMetadata
	{
		// Token: 0x040004BC RID: 1212
		[DisplayName("SAA", "TT")]
		TotalTime,
		// Token: 0x040004BD RID: 1213
		[DisplayName("SAA", "VET")]
		VerifyEnvironmentTime,
		// Token: 0x040004BE RID: 1214
		[DisplayName("SAA", "VUIT")]
		VerifyUserIdentityTypeTime,
		// Token: 0x040004BF RID: 1215
		[DisplayName("SAA", "SSCT")]
		SetSyncRequestCmdletTime,
		// Token: 0x040004C0 RID: 1216
		[DisplayName("SAA", "GSCT")]
		GetSyncRequestStatisticsCmdletTime,
		// Token: 0x040004C1 RID: 1217
		[DisplayName("SAA", "TUCL")]
		TestUserCanLogonTime,
		// Token: 0x040004C2 RID: 1218
		[DisplayName("SAA", "CVSE")]
		CacheValidatedSettings,
		// Token: 0x040004C3 RID: 1219
		[DisplayName("SAA", "CNVS")]
		CacheNotValidatedSettings,
		// Token: 0x040004C4 RID: 1220
		[DisplayName("SAA", "GSNE")]
		GetSyncRequestStatisticsCmdletNonAggregatedAccountError,
		// Token: 0x040004C5 RID: 1221
		[DisplayName("SAA", "GUST")]
		GetUpdatedConnectionSettingsTime,
		// Token: 0x040004C6 RID: 1222
		[DisplayName("SAA", "TLST")]
		TestLogonWithCurrentSettingsTime
	}
}

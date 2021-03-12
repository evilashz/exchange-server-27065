using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200004F RID: 79
	internal enum SuiteStorage
	{
		// Token: 0x0400048D RID: 1165
		[DisplayName("SS", "SUS")]
		SetUserSettings,
		// Token: 0x0400048E RID: 1166
		[DisplayName("SS", "SOS")]
		SetOrgSettings,
		// Token: 0x0400048F RID: 1167
		[DisplayName("SS", "SOSExMbxP")]
		SetOrgSettingsExMbxPrincipal,
		// Token: 0x04000490 RID: 1168
		[DisplayName("SS", "LUS")]
		LoadUserSettings,
		// Token: 0x04000491 RID: 1169
		[DisplayName("SS", "LOS")]
		LoadOrgSettings,
		// Token: 0x04000492 RID: 1170
		[DisplayName("SS", "LOSExMbxP")]
		LoadOrgSettingsExMbxPrincipal,
		// Token: 0x04000493 RID: 1171
		[DisplayName("SS", "USUS")]
		UpdateStorageUpdateSetting,
		// Token: 0x04000494 RID: 1172
		[DisplayName("SS", "RSUC")]
		ReadStorageUserConfiguration,
		// Token: 0x04000495 RID: 1173
		[DisplayName("SS", "RSS")]
		ReadStorageSetting,
		// Token: 0x04000496 RID: 1174
		[DisplayName("SS", "RSSNF")]
		ReadStorageSettingNotFound,
		// Token: 0x04000497 RID: 1175
		[DisplayName("SS", "GOMbxDD")]
		GetOrgMailboxIsDcDomain,
		// Token: 0x04000498 RID: 1176
		[DisplayName("SS", "GOMbxSS")]
		GetOrgMailboxSessionSettings,
		// Token: 0x04000499 RID: 1177
		[DisplayName("SS", "GOMCnt")]
		GetOrgMailboxCount,
		// Token: 0x0400049A RID: 1178
		[DisplayName("SS", "GOMbx")]
		GetOrgMailbox,
		// Token: 0x0400049B RID: 1179
		[DisplayName("SS", "ONFEx")]
		ObjectNotFoundException,
		// Token: 0x0400049C RID: 1180
		[DisplayName("SS", "SPEx")]
		StoragePermanentException,
		// Token: 0x0400049D RID: 1181
		[DisplayName("SS", "STEx")]
		StorageTransientException,
		// Token: 0x0400049E RID: 1182
		[DisplayName("SS", "QEEx")]
		QuotaExceededException,
		// Token: 0x0400049F RID: 1183
		[DisplayName("SS", "SCEx")]
		SaveConflictException,
		// Token: 0x040004A0 RID: 1184
		[DisplayName("SS", "RTNEx")]
		CannotResolveTenantNameException,
		// Token: 0x040004A1 RID: 1185
		[DisplayName("SS", "AZAdmin")]
		AuthZUserNotInAdminRole,
		// Token: 0x040004A2 RID: 1186
		[DisplayName("SS", "RBACEx")]
		MessageUnableToLoadRBACSettingsException,
		// Token: 0x040004A3 RID: 1187
		[DisplayName("SS", "UpdScope")]
		UpdateStorageMailboxScope,
		// Token: 0x040004A4 RID: 1188
		[DisplayName("SS", "UpdTotals")]
		UpdateStorageUpdateSettingTotal,
		// Token: 0x040004A5 RID: 1189
		[DisplayName("SS", "UpdBytes")]
		UpdateStorageTotalBytes,
		// Token: 0x040004A6 RID: 1190
		[DisplayName("SS", "ReadScope")]
		ReadStorageMailboxScope,
		// Token: 0x040004A7 RID: 1191
		[DisplayName("SS", "ReadTotals")]
		ReadStorageSettingTotal,
		// Token: 0x040004A8 RID: 1192
		[DisplayName("SS", "ReadBytes")]
		ReadStorageTotalBytes
	}
}

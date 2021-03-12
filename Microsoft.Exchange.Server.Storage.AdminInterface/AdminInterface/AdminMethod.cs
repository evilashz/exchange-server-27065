using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000002 RID: 2
	internal enum AdminMethod : byte
	{
		// Token: 0x04000002 RID: 2
		None,
		// Token: 0x04000003 RID: 3
		ForTestPurposes,
		// Token: 0x04000004 RID: 4
		AdminGetIFVersion = 11,
		// Token: 0x04000005 RID: 5
		EcListAllMdbStatus50,
		// Token: 0x04000006 RID: 6
		EcListMdbStatus50,
		// Token: 0x04000007 RID: 7
		EcAdminGetCnctTable50,
		// Token: 0x04000008 RID: 8
		EcGetLastBackupTimes50,
		// Token: 0x04000009 RID: 9
		EcClearAbsentInDsFlagOnMailbox50,
		// Token: 0x0400000A RID: 10
		EcPurgeCachedMailboxObject50,
		// Token: 0x0400000B RID: 11
		EcSyncMailboxesWithDS50,
		// Token: 0x0400000C RID: 12
		EcAdminDeletePrivateMailbox50,
		// Token: 0x0400000D RID: 13
		EcSetMailboxSecurityDescriptor50,
		// Token: 0x0400000E RID: 14
		EcGetMailboxSecurityDescriptor50,
		// Token: 0x0400000F RID: 15
		EcAdminGetLogonTable50,
		// Token: 0x04000010 RID: 16
		EcMountDatabase50,
		// Token: 0x04000011 RID: 17
		EcUnmountDatabase50,
		// Token: 0x04000012 RID: 18
		EcAdminGetMailboxBasicInfo50,
		// Token: 0x04000013 RID: 19
		EcAdminSetMailboxBasicInfo50,
		// Token: 0x04000014 RID: 20
		EcPurgeCachedMdbObject50,
		// Token: 0x04000015 RID: 21
		EcAdminGetMailboxTable50,
		// Token: 0x04000016 RID: 22
		EcAdminNotifyOnDSChange50,
		// Token: 0x04000017 RID: 23
		EcReadMdbEvents50,
		// Token: 0x04000018 RID: 24
		EcSyncMailboxWithDS50,
		// Token: 0x04000019 RID: 25
		EcDeleteMdbWatermarksForConsumer50,
		// Token: 0x0400001A RID: 26
		EcDeleteMdbWatermarksForMailbox50,
		// Token: 0x0400001B RID: 27
		EcSaveMdbWatermarks50,
		// Token: 0x0400001C RID: 28
		EcGetMdbWatermarksForConsumer50,
		// Token: 0x0400001D RID: 29
		EcWriteMdbEvents50,
		// Token: 0x0400001E RID: 30
		EcGetMdbWatermarksForMailbox50,
		// Token: 0x0400001F RID: 31
		EcDoMaintenanceTask50,
		// Token: 0x04000020 RID: 32
		EcGetLastBackupInfo50,
		// Token: 0x04000021 RID: 33
		EcAdminGetMailboxTableEntry50,
		// Token: 0x04000022 RID: 34
		EcAdminGetMailboxTableEntryFlags50,
		// Token: 0x04000023 RID: 35
		EcLogReplayRequest2,
		// Token: 0x04000024 RID: 36
		EcAdminGetViewsTableEx50,
		// Token: 0x04000025 RID: 37
		EcAdminExecuteTask50,
		// Token: 0x04000026 RID: 38
		EcAdminGetFeatureVersion50,
		// Token: 0x04000027 RID: 39
		EcAdminGetMailboxSignature50,
		// Token: 0x04000028 RID: 40
		EcAdminStartBlockModeReplicationToPassive50,
		// Token: 0x04000029 RID: 41
		EcGetDatabaseSizeEx50,
		// Token: 0x0400002A RID: 42
		EcAdminISIntegCheck50,
		// Token: 0x0400002B RID: 43
		EcMultiMailboxSearch,
		// Token: 0x0400002C RID: 44
		EcGetMultiMailboxSearchKeywordStats,
		// Token: 0x0400002D RID: 45
		EcAdminGetResourceMonitorDigest50,
		// Token: 0x0400002E RID: 46
		EcAdminGetDatabaseProcessInfo50,
		// Token: 0x0400002F RID: 47
		EcAdminProcessSnapshotOperation50,
		// Token: 0x04000030 RID: 48
		EcAdminGetPhysicalDatabaseInformation50,
		// Token: 0x04000031 RID: 49
		EcAdminPrePopulateCacheEx50,
		// Token: 0x04000032 RID: 50
		EcForceNewLog50,
		// Token: 0x04000033 RID: 51
		EcAdminIntegrityCheckEx50,
		// Token: 0x04000034 RID: 52
		EcCreateUserInfo50,
		// Token: 0x04000035 RID: 53
		EcReadUserInfo50,
		// Token: 0x04000036 RID: 54
		EcUpdateUserInfo50,
		// Token: 0x04000037 RID: 55
		EcDeleteUserInfo50,
		// Token: 0x04000038 RID: 56
		EcAdminGetRestrictionTableEx50
	}
}

using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Rpc.AdminRpc
{
	// Token: 0x02000145 RID: 325
	public interface IAdminRpcServer
	{
		// Token: 0x060008A7 RID: 2215
		void AdminGetIFVersion(ClientSecurityContext callerSecurityContext, out ushort majorVersion, out ushort minorVersion);

		// Token: 0x060008A8 RID: 2216
		int EcListAllMdbStatus50(ClientSecurityContext callerSecurityContext, [MarshalAs(UnmanagedType.U1)] bool basicInformation, out uint countMdbs, out byte[] mdbStatus, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008A9 RID: 2217
		int EcListMdbStatus50(ClientSecurityContext callerSecurityContext, Guid[] mdbGuids, out uint[] mdbStatusFlags, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008AA RID: 2218
		int EcGetDatabaseSizeEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out uint dbTotalPages, out uint dbAvailablePages, out uint pageSize, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008AB RID: 2219
		int EcAdminGetCnctTable50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008AC RID: 2220
		int EcGetLastBackupTimes50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out long lastCompleteBackupTime, out long lastIncrementalBackupTime, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008AD RID: 2221
		int EcClearAbsentInDsFlagOnMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008AE RID: 2222
		int EcPurgeCachedMailboxObject50(ClientSecurityContext callerSecurityContext, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008AF RID: 2223
		int EcSyncMailboxesWithDS50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B0 RID: 2224
		int EcAdminDeletePrivateMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B1 RID: 2225
		int EcSetMailboxSecurityDescriptor50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] ntsd, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B2 RID: 2226
		int EcGetMailboxSecurityDescriptor50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, out byte[] ntsd, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B3 RID: 2227
		int EcAdminGetLogonTable50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B4 RID: 2228
		int EcMountDatabase50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B5 RID: 2229
		int EcUnmountDatabase50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B6 RID: 2230
		int EcStartBlockModeReplicationToPassive50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, string passiveName, uint firstGenToSend, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B7 RID: 2231
		int EcAdminSetMailboxBasicInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] mailboxInfo, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B8 RID: 2232
		int EcPurgeCachedMdbObject50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008B9 RID: 2233
		int EcAdminGetMailboxTable50(ClientSecurityContext callerSecurityContext, Guid? mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008BA RID: 2234
		int EcAdminNotifyOnDSChange50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint obj, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008BB RID: 2235
		int EcReadMdbEvents50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008BC RID: 2236
		int EcSyncMailboxWithDS50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008BD RID: 2237
		int EcDeleteMdbWatermarksForConsumer50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, out uint delCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008BE RID: 2238
		int EcDeleteMdbWatermarksForMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid mailboxDsGuid, out uint delCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008BF RID: 2239
		int EcSaveMdbWatermarks50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C0 RID: 2240
		int EcGetMdbWatermarksForConsumer50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, out MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C1 RID: 2241
		int EcWriteMdbEvents50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C2 RID: 2242
		int EcGetMdbWatermarksForMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid mailboxDsGuid, out MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C3 RID: 2243
		int EcDoMaintenanceTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint task, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C4 RID: 2244
		int EcGetLastBackupInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out long lastCompleteBackupTime, out long lastIncrementalBackupTime, out long lastDifferentialBackupTime, out long lastCopyBackupTime, out int snapFull, out int snapIncremental, out int snapDifferential, out int snapCopy, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C5 RID: 2245
		int EcAdminGetMailboxTableEntry50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint[] propTags, uint cpid, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C6 RID: 2246
		int EcAdminGetMailboxTableEntryFlags50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] propTags, uint cpid, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C7 RID: 2247
		int EcLogReplayRequest2(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint logReplayMax, uint logReplayFlags, out uint logReplayNext, out byte[] databaseInfo, out uint patchPageNumber, out byte[] patchToken, out byte[] patchData, out uint[] corruptPages, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C8 RID: 2248
		int EcAdminGetViewsTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008C9 RID: 2249
		int EcAdminGetRestrictionTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008CA RID: 2250
		int EcAdminExecuteTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid taskClass, int taskId, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008CB RID: 2251
		int EcAdminGetFeatureVersion50(ClientSecurityContext callerSecurityContext, uint feature, out uint version, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008CC RID: 2252
		int EcAdminGetMailboxSignature50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, out byte[] mailboxSignature, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008CD RID: 2253
		int EcAdminISIntegCheck50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] taskIds, out string requestId, [Out] byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008CE RID: 2254
		int EcMultiMailboxSearch(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequest, out byte[] searchResultsOut, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008CF RID: 2255
		int EcGetMultiMailboxSearchKeywordStats(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] keywordStatRequest, out byte[] keywordStatsResultsOut, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D0 RID: 2256
		int EcAdminGetResourceMonitorDigest50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propertyTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D1 RID: 2257
		int EcAdminGetDatabaseProcessInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D2 RID: 2258
		int EcAdminProcessSnapshotOperation50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint opCode, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D3 RID: 2259
		int EcAdminGetPhysicalDatabaseInformation50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out byte[] databaseInfo, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D4 RID: 2260
		int EcAdminPrePopulateCacheEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] partitionHint, string dcName, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D5 RID: 2261
		int EcForceNewLog50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D6 RID: 2262
		int EcAdminIntegrityCheckEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint operation, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut);

		// Token: 0x060008D7 RID: 2263
		int EcCreateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, byte[] auxIn, out byte[] auxOut);

		// Token: 0x060008D8 RID: 2264
		int EcReadUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, uint[] propertyTags, out ArraySegment<byte> result, byte[] auxIn, out byte[] auxOut);

		// Token: 0x060008D9 RID: 2265
		int EcUpdateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, uint[] deletePropertyTags, byte[] auxIn, out byte[] auxOut);

		// Token: 0x060008DA RID: 2266
		int EcDeleteUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] auxIn, out byte[] auxOut);
	}
}

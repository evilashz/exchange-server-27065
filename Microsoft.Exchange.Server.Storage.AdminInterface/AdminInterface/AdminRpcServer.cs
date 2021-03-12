using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.AdminInterface;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.Rpc.MultiMailboxSearch;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.HA;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.MapiDisp;
using Microsoft.Exchange.Server.Storage.MultiMailboxSearch;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;
using Microsoft.Exchange.Server.Storage.StoreIntegrityCheck;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000014 RID: 20
	public class AdminRpcServer : IAdminRpcServer
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000342A File Offset: 0x0000162A
		public void AdminGetIFVersion(ClientSecurityContext callerSecurityContext, out ushort majorVersion, out ushort minorVersion)
		{
			majorVersion = 7;
			minorVersion = 17;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003433 File Offset: 0x00001633
		public int EcAdminGetCnctTable50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			result = null;
			rowCount = 0U;
			auxiliaryOut = Array<byte>.Empty;
			return -2147221246;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000344A File Offset: 0x0000164A
		public int EcAdminGetLogonTable50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			result = null;
			rowCount = 0U;
			auxiliaryOut = Array<byte>.Empty;
			return -2147221246;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003464 File Offset: 0x00001664
		public int EcAdminGetFeatureVersion50(ClientSecurityContext callerSecurityContext, uint feature, out uint version, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			switch (feature)
			{
			case 1U:
				version = 104U;
				break;
			case 2U:
				version = 1U;
				break;
			case 3U:
				version = 1U;
				break;
			case 4U:
				version = 4U;
				break;
			default:
				version = 0U;
				errorCode = ErrorCode.CreateInvalidParameter((LID)37752U);
				break;
			}
			auxiliaryOut = Array<byte>.Empty;
			return (int)errorCode;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000034CC File Offset: 0x000016CC
		public int EcListAllMdbStatus50(ClientSecurityContext callerSecurityContext, bool basicInformation, out uint countMdbs, out byte[] mdbStatus, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			countMdbs = 0U;
			mdbStatus = null;
			AdminRpcServer.AdminRpcListAllMdbStatus adminRpcListAllMdbStatus = new AdminRpcServer.AdminRpcListAllMdbStatus(callerSecurityContext, basicInformation, auxiliaryIn);
			ErrorCode errorCode = adminRpcListAllMdbStatus.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				countMdbs = adminRpcListAllMdbStatus.MdbCount;
				mdbStatus = adminRpcListAllMdbStatus.MdbStatusRaw;
			}
			auxiliaryOut = adminRpcListAllMdbStatus.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003520 File Offset: 0x00001720
		public int EcListMdbStatus50(ClientSecurityContext callerSecurityContext, Guid[] mdbGuids, out uint[] mdbStatusFlags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			mdbStatusFlags = null;
			AdminRpcListMdbStatus adminRpcListMdbStatus = new AdminRpcListMdbStatus(callerSecurityContext, mdbGuids, auxiliaryIn);
			ErrorCode errorCode = adminRpcListMdbStatus.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				mdbStatusFlags = adminRpcListMdbStatus.MdbStatus;
			}
			auxiliaryOut = adminRpcListMdbStatus.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003568 File Offset: 0x00001768
		public int EcGetDatabaseSizeEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out uint totalPages, out uint availablePages, out uint pageSize, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcGetDatabaseSize adminRpcGetDatabaseSize = new AdminRpcServer.AdminRpcGetDatabaseSize(callerSecurityContext, mdbGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetDatabaseSize.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				totalPages = adminRpcGetDatabaseSize.TotalPages;
				availablePages = adminRpcGetDatabaseSize.AvailablePages;
				pageSize = adminRpcGetDatabaseSize.PageSize;
			}
			else
			{
				totalPages = 0U;
				availablePages = 0U;
				pageSize = 0U;
			}
			auxiliaryOut = adminRpcGetDatabaseSize.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000035CC File Offset: 0x000017CC
		public int EcMountDatabase50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcMountDatabase adminRpcMountDatabase = new AdminRpcMountDatabase(callerSecurityContext, mdbGuid, flags, auxiliaryIn);
			ErrorCode errorCode = adminRpcMountDatabase.EcExecute();
			auxiliaryOut = adminRpcMountDatabase.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000035FC File Offset: 0x000017FC
		public int EcUnmountDatabase50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcUnmountDatabase adminRpcUnmountDatabase = new AdminRpcUnmountDatabase(callerSecurityContext, mdbGuid, flags, auxiliaryIn);
			ErrorCode errorCode = adminRpcUnmountDatabase.EcExecute();
			auxiliaryOut = adminRpcUnmountDatabase.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000362C File Offset: 0x0000182C
		public int EcStartBlockModeReplicationToPassive50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, string passiveName, uint firstGenToSend, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcStartBlockModeReplicationToPassive adminRpcStartBlockModeReplicationToPassive = new AdminRpcServer.AdminRpcStartBlockModeReplicationToPassive(callerSecurityContext, mdbGuid, passiveName, firstGenToSend, auxiliaryIn);
			ErrorCode errorCode = adminRpcStartBlockModeReplicationToPassive.EcExecute();
			auxiliaryOut = adminRpcStartBlockModeReplicationToPassive.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000365C File Offset: 0x0000185C
		public int EcPurgeCachedMdbObject50(ClientSecurityContext callerSecurityContext, Guid databaseGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcPurgeCachedMdbObject adminRpcPurgeCachedMdbObject = new AdminRpcServer.AdminRpcPurgeCachedMdbObject(callerSecurityContext, databaseGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcPurgeCachedMdbObject.EcExecute();
			auxiliaryOut = adminRpcPurgeCachedMdbObject.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003688 File Offset: 0x00001888
		public int EcDoMaintenanceTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint task, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminDoMaintenanceTask adminDoMaintenanceTask = new AdminRpcServer.AdminDoMaintenanceTask(callerSecurityContext, mdbGuid, task, auxiliaryIn);
			ErrorCode errorCode = adminDoMaintenanceTask.EcExecute();
			auxiliaryOut = adminDoMaintenanceTask.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036B8 File Offset: 0x000018B8
		public int EcGetLastBackupInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out long lastFullBackupTime, out long lastIncrementalBackupTime, out long lastDifferentialBackupTime, out long lastCopyBackupTime, out int snapFull, out int snapIncremental, out int snapDifferential, out int snapCopy, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			lastFullBackupTime = 0L;
			lastIncrementalBackupTime = 0L;
			lastDifferentialBackupTime = 0L;
			lastCopyBackupTime = 0L;
			snapFull = 0;
			snapIncremental = 0;
			snapDifferential = 0;
			snapCopy = 0;
			AdminRpcServer.AdminRpcGetLastBackupInfo50 adminRpcGetLastBackupInfo = new AdminRpcServer.AdminRpcGetLastBackupInfo50(callerSecurityContext, mdbGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetLastBackupInfo.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				if (adminRpcGetLastBackupInfo.LastFullBackupTime > ParseSerialize.MinFileTimeDateTime)
				{
					lastFullBackupTime = adminRpcGetLastBackupInfo.LastFullBackupTime.ToFileTimeUtc();
				}
				if (adminRpcGetLastBackupInfo.LastIncrementalBackupTime > ParseSerialize.MinFileTimeDateTime)
				{
					lastIncrementalBackupTime = adminRpcGetLastBackupInfo.LastIncrementalBackupTime.ToFileTimeUtc();
				}
				if (adminRpcGetLastBackupInfo.LastDifferentialBackupTime > ParseSerialize.MinFileTimeDateTime)
				{
					lastDifferentialBackupTime = adminRpcGetLastBackupInfo.LastDifferentialBackupTime.ToFileTimeUtc();
				}
				if (adminRpcGetLastBackupInfo.LastCopyBackupTime > ParseSerialize.MinFileTimeDateTime)
				{
					lastCopyBackupTime = adminRpcGetLastBackupInfo.LastCopyBackupTime.ToFileTimeUtc();
				}
				snapFull = adminRpcGetLastBackupInfo.SnapshotFullBackup;
				snapIncremental = adminRpcGetLastBackupInfo.SnapshotIncrementalBackup;
				snapDifferential = adminRpcGetLastBackupInfo.SnapshotDifferentialBackup;
				snapCopy = adminRpcGetLastBackupInfo.SnapshotCopyBackup;
			}
			auxiliaryOut = adminRpcGetLastBackupInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000037CC File Offset: 0x000019CC
		public int EcGetLastBackupTimes50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out long lastFullBackupTime, out long lastIncrementalBackupTime, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			lastFullBackupTime = 0L;
			lastIncrementalBackupTime = 0L;
			AdminRpcServer.AdminRpcGetLastBackupInfo50 adminRpcGetLastBackupInfo = new AdminRpcServer.AdminRpcGetLastBackupInfo50(callerSecurityContext, mdbGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetLastBackupInfo.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				if (adminRpcGetLastBackupInfo.LastFullBackupTime > ParseSerialize.MinFileTimeDateTime)
				{
					lastFullBackupTime = adminRpcGetLastBackupInfo.LastFullBackupTime.ToFileTimeUtc();
				}
				if (adminRpcGetLastBackupInfo.LastIncrementalBackupTime > ParseSerialize.MinFileTimeDateTime)
				{
					lastIncrementalBackupTime = adminRpcGetLastBackupInfo.LastIncrementalBackupTime.ToFileTimeUtc();
				}
			}
			auxiliaryOut = adminRpcGetLastBackupInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003854 File Offset: 0x00001A54
		public int EcLogReplayRequest2(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint logReplayMax, uint logReplayFlags, out uint logReplayNext, out byte[] databaseInfo, out uint patchPageNumber, out byte[] patchToken, out byte[] patchData, out uint[] corruptPages, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcLogReplayRequest2 adminRpcLogReplayRequest = new AdminRpcServer.AdminRpcLogReplayRequest2(callerSecurityContext, mdbGuid, logReplayMax, logReplayFlags, auxiliaryIn);
			ErrorCode errorCode = adminRpcLogReplayRequest.EcExecute();
			logReplayNext = adminRpcLogReplayRequest.LogReplayNext;
			databaseInfo = adminRpcLogReplayRequest.DatabaseHeaderInfo;
			patchPageNumber = adminRpcLogReplayRequest.PatchPageNumber;
			patchToken = adminRpcLogReplayRequest.PatchToken;
			patchData = adminRpcLogReplayRequest.PatchData;
			corruptPages = adminRpcLogReplayRequest.CorruptPages;
			auxiliaryOut = adminRpcLogReplayRequest.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000038BC File Offset: 0x00001ABC
		public int EcAdminGetResourceMonitorDigest50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propertyTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcGetResourceMonitorDigest adminRpcGetResourceMonitorDigest = new AdminRpcServer.AdminRpcGetResourceMonitorDigest(callerSecurityContext, mdbGuid, propertyTags, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetResourceMonitorDigest.EcExecute();
			result = adminRpcGetResourceMonitorDigest.Result;
			rowCount = adminRpcGetResourceMonitorDigest.RowCount;
			auxiliaryOut = adminRpcGetResourceMonitorDigest.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000038FC File Offset: 0x00001AFC
		public int EcAdminGetDatabaseProcessInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcGetDatabaseProcessInfo adminRpcGetDatabaseProcessInfo = new AdminRpcServer.AdminRpcGetDatabaseProcessInfo(callerSecurityContext, mdbGuid, propTags, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetDatabaseProcessInfo.EcExecute();
			result = adminRpcGetDatabaseProcessInfo.Result;
			rowCount = adminRpcGetDatabaseProcessInfo.RowCount;
			auxiliaryOut = adminRpcGetDatabaseProcessInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000393C File Offset: 0x00001B3C
		public int EcAdminProcessSnapshotOperation50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint opCode, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.EcAdminProcessSnapshotOperation ecAdminProcessSnapshotOperation = new AdminRpcServer.EcAdminProcessSnapshotOperation(callerSecurityContext, mdbGuid, opCode, flags, auxiliaryIn);
			ErrorCode errorCode = ecAdminProcessSnapshotOperation.EcExecute();
			auxiliaryOut = ecAdminProcessSnapshotOperation.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000396C File Offset: 0x00001B6C
		public int EcAdminGetPhysicalDatabaseInformation50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out byte[] databaseInfo, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.EcAdminGetPhysicalDatabaseInformation ecAdminGetPhysicalDatabaseInformation = new AdminRpcServer.EcAdminGetPhysicalDatabaseInformation(callerSecurityContext, mdbGuid, auxiliaryIn);
			ErrorCode errorCode = ecAdminGetPhysicalDatabaseInformation.EcExecute();
			databaseInfo = ecAdminGetPhysicalDatabaseInformation.DatabaseHeaderInfo;
			auxiliaryOut = ecAdminGetPhysicalDatabaseInformation.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000039A4 File Offset: 0x00001BA4
		public int EcForceNewLog50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.EcAdminForceNewLog ecAdminForceNewLog = new AdminRpcServer.EcAdminForceNewLog(callerSecurityContext, mdbGuid, auxiliaryIn);
			ErrorCode errorCode = ecAdminForceNewLog.EcExecute();
			auxiliaryOut = ecAdminForceNewLog.AuxiliaryOut;
			errorCode != ErrorCode.NoError;
			return (int)errorCode;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039DC File Offset: 0x00001BDC
		public int EcReadMdbEvents50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			response = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcReadMdbEvents adminRpcReadMdbEvents = new AdminRpcServer.AdminRpcReadMdbEvents(callerSecurityContext, mdbGuid, mdbVersionGuid, request, auxiliaryIn);
			ErrorCode errorCode = adminRpcReadMdbEvents.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				response = adminRpcReadMdbEvents.Response;
				mdbVersionGuid = adminRpcReadMdbEvents.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcReadMdbEvents.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A3C File Offset: 0x00001C3C
		public int EcWriteMdbEvents50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			response = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcWriteMdbEvents adminRpcWriteMdbEvents = new AdminRpcServer.AdminRpcWriteMdbEvents(callerSecurityContext, mdbGuid, mdbVersionGuid, request, auxiliaryIn);
			ErrorCode errorCode = adminRpcWriteMdbEvents.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				response = adminRpcWriteMdbEvents.Response;
				mdbVersionGuid = adminRpcWriteMdbEvents.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcWriteMdbEvents.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A9C File Offset: 0x00001C9C
		public int EcDeleteMdbWatermarksForConsumer50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, out uint delCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			delCount = 0U;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcDeleteMdbWatermarksForConsumer adminRpcDeleteMdbWatermarksForConsumer = new AdminRpcServer.AdminRpcDeleteMdbWatermarksForConsumer(callerSecurityContext, mdbGuid, mdbVersionGuid, mailboxDsGuid, consumerGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcDeleteMdbWatermarksForConsumer.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				delCount = adminRpcDeleteMdbWatermarksForConsumer.DeletedCount;
				mdbVersionGuid = adminRpcDeleteMdbWatermarksForConsumer.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcDeleteMdbWatermarksForConsumer.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003B00 File Offset: 0x00001D00
		public int EcDeleteMdbWatermarksForMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid mailboxDsGuid, out uint delCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			delCount = 0U;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcDeleteMdbWatermarksForMailbox adminRpcDeleteMdbWatermarksForMailbox = new AdminRpcServer.AdminRpcDeleteMdbWatermarksForMailbox(callerSecurityContext, mdbGuid, mdbVersionGuid, mailboxDsGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcDeleteMdbWatermarksForMailbox.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				delCount = adminRpcDeleteMdbWatermarksForMailbox.DeletedCount;
				mdbVersionGuid = adminRpcDeleteMdbWatermarksForMailbox.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcDeleteMdbWatermarksForMailbox.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003B60 File Offset: 0x00001D60
		public int EcSaveMdbWatermarks50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcSaveMdbWatermarks adminRpcSaveMdbWatermarks = new AdminRpcServer.AdminRpcSaveMdbWatermarks(callerSecurityContext, mdbGuid, mdbVersionGuid, wms, auxiliaryIn);
			ErrorCode errorCode = adminRpcSaveMdbWatermarks.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				mdbVersionGuid = adminRpcSaveMdbWatermarks.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcSaveMdbWatermarks.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public int EcGetMdbWatermarksForConsumer50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, out MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			wms = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcGetMdbWatermarksForConsumer adminRpcGetMdbWatermarksForConsumer = new AdminRpcServer.AdminRpcGetMdbWatermarksForConsumer(callerSecurityContext, mdbGuid, mdbVersionGuid, mailboxDsGuid, consumerGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetMdbWatermarksForConsumer.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				wms = adminRpcGetMdbWatermarksForConsumer.Wms;
				mdbVersionGuid = adminRpcGetMdbWatermarksForConsumer.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcGetMdbWatermarksForConsumer.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003C18 File Offset: 0x00001E18
		public int EcGetMdbWatermarksForMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid mailboxDsGuid, out MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			wms = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcGetMdbWatermarksForMailbox adminRpcGetMdbWatermarksForMailbox = new AdminRpcServer.AdminRpcGetMdbWatermarksForMailbox(callerSecurityContext, mdbGuid, mdbVersionGuid, mailboxDsGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetMdbWatermarksForMailbox.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				wms = adminRpcGetMdbWatermarksForMailbox.Wms;
				mdbVersionGuid = adminRpcGetMdbWatermarksForMailbox.MdbVersionGuid;
			}
			auxiliaryOut = adminRpcGetMdbWatermarksForMailbox.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C78 File Offset: 0x00001E78
		public int EcClearAbsentInDsFlagOnMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcClearAbsentInDsOnMailbox adminRpcClearAbsentInDsOnMailbox = new AdminRpcServer.AdminRpcClearAbsentInDsOnMailbox(callerSecurityContext, mdbGuid, mailboxGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcClearAbsentInDsOnMailbox.EcExecute();
			auxiliaryOut = adminRpcClearAbsentInDsOnMailbox.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public int EcPurgeCachedMailboxObject50(ClientSecurityContext callerSecurityContext, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcPurgeCachedMailboxObject adminRpcPurgeCachedMailboxObject = new AdminRpcServer.AdminRpcPurgeCachedMailboxObject(callerSecurityContext, mailboxGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcPurgeCachedMailboxObject.EcExecute();
			auxiliaryOut = adminRpcPurgeCachedMailboxObject.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public int EcSyncMailboxesWithDS50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			auxiliaryOut = Array<byte>.Empty;
			return (int)ErrorCode.CreateNotSupported((LID)45031U);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public int EcAdminDeletePrivateMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcDeletePrivateMailbox adminRpcDeletePrivateMailbox = new AdminRpcServer.AdminRpcDeletePrivateMailbox(callerSecurityContext, mdbGuid, mailboxGuid, (DeleteMailboxFlags)flags, auxiliaryIn);
			ErrorCode errorCode = adminRpcDeletePrivateMailbox.EcExecute();
			auxiliaryOut = adminRpcDeletePrivateMailbox.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003D24 File Offset: 0x00001F24
		public int EcSetMailboxSecurityDescriptor50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] ntsd, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			auxiliaryOut = Array<byte>.Empty;
			return (int)ErrorCode.CreateNotSupported((LID)45376U);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003D44 File Offset: 0x00001F44
		public int EcGetMailboxSecurityDescriptor50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, out byte[] ntsd, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcGetMailboxSecurityDescriptor adminRpcGetMailboxSecurityDescriptor = new AdminRpcServer.AdminRpcGetMailboxSecurityDescriptor(callerSecurityContext, mdbGuid, mailboxGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetMailboxSecurityDescriptor.EcExecute();
			ntsd = adminRpcGetMailboxSecurityDescriptor.NTSecurityDescriptor;
			auxiliaryOut = adminRpcGetMailboxSecurityDescriptor.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003D7C File Offset: 0x00001F7C
		public int EcAdminGetMailboxSignature50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, out byte[] mailboxSignature, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminGetMailboxSignature adminGetMailboxSignature = new AdminRpcServer.AdminGetMailboxSignature(callerSecurityContext, mdbGuid, mailboxGuid, (MailboxSignatureFlags)flags, auxiliaryIn);
			ErrorCode errorCode = adminGetMailboxSignature.EcExecute();
			mailboxSignature = adminGetMailboxSignature.Result;
			auxiliaryOut = adminGetMailboxSignature.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public int EcAdminSetMailboxBasicInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] mailboxBasicInfo, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcSetMailboxBasicInfo adminRpcSetMailboxBasicInfo = new AdminRpcServer.AdminRpcSetMailboxBasicInfo(callerSecurityContext, mdbGuid, mailboxGuid, mailboxBasicInfo, auxiliaryIn);
			ErrorCode errorCode = adminRpcSetMailboxBasicInfo.EcExecute();
			auxiliaryOut = adminRpcSetMailboxBasicInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public int EcAdminGetMailboxTable50(ClientSecurityContext callerSecurityContext, Guid? mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			result = null;
			rowCount = 0U;
			AdminRpcServer.AdminRpcGetMailboxTable adminRpcGetMailboxTable = (mdbGuid != null) ? new AdminRpcServer.AdminRpcGetMailboxTable(callerSecurityContext, mdbGuid, lparam, propTags, cpid, auxiliaryIn) : new AdminRpcServer.AdminRpcGetMailboxTable(callerSecurityContext, lparam, propTags, cpid, auxiliaryIn);
			ErrorCode errorCode = adminRpcGetMailboxTable.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				result = adminRpcGetMailboxTable.Result;
				rowCount = adminRpcGetMailboxTable.RowCount;
			}
			auxiliaryOut = adminRpcGetMailboxTable.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003E59 File Offset: 0x00002059
		public int EcAdminNotifyOnDSChange50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint obj, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			auxiliaryOut = Array<byte>.Empty;
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003E70 File Offset: 0x00002070
		public int EcSyncMailboxWithDS50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminRpcSyncMailboxWithDS adminRpcSyncMailboxWithDS = new AdminRpcServer.AdminRpcSyncMailboxWithDS(callerSecurityContext, mdbGuid, mailboxGuid, auxiliaryIn);
			ErrorCode errorCode = adminRpcSyncMailboxWithDS.EcExecute();
			auxiliaryOut = adminRpcSyncMailboxWithDS.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003EA0 File Offset: 0x000020A0
		public int EcAdminGetMailboxTableEntry50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint[] propTags, uint cpid, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminGetMailboxTableEntry50 adminGetMailboxTableEntry = new AdminRpcServer.AdminGetMailboxTableEntry50(callerSecurityContext, mdbGuid, mailboxGuid, GetMailboxInfoTableFlags.None, propTags, auxiliaryIn);
			ErrorCode errorCode = adminGetMailboxTableEntry.EcExecute();
			result = adminGetMailboxTableEntry.Result;
			rowCount = ((errorCode == ErrorCode.NoError) ? 1U : 0U);
			auxiliaryOut = adminGetMailboxTableEntry.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003EF0 File Offset: 0x000020F0
		public int EcAdminGetMailboxTableEntryFlags50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] propTags, uint cpid, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminGetMailboxTableEntry50 adminGetMailboxTableEntry = new AdminRpcServer.AdminGetMailboxTableEntry50(callerSecurityContext, mdbGuid, mailboxGuid, (GetMailboxInfoTableFlags)flags, propTags, auxiliaryIn);
			ErrorCode errorCode = adminGetMailboxTableEntry.EcExecute();
			result = adminGetMailboxTableEntry.Result;
			rowCount = ((errorCode == ErrorCode.NoError) ? 1U : 0U);
			auxiliaryOut = adminGetMailboxTableEntry.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F40 File Offset: 0x00002140
		public int EcAdminGetViewsTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminGetViewsTableEx50 adminGetViewsTableEx = new AdminRpcServer.AdminGetViewsTableEx50(callerSecurityContext, mdbGuid, mailboxGuid, folderLTID, propTags, auxiliaryIn);
			ErrorCode errorCode = adminGetViewsTableEx.EcExecute();
			result = adminGetViewsTableEx.Result;
			rowCount = adminGetViewsTableEx.ResultRowCount;
			auxiliaryOut = adminGetViewsTableEx.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003F84 File Offset: 0x00002184
		public int EcAdminGetRestrictionTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminGetRestrictionTableEx50 adminGetRestrictionTableEx = new AdminRpcServer.AdminGetRestrictionTableEx50(callerSecurityContext, mdbGuid, mailboxGuid, folderLTID, propTags, auxiliaryIn);
			ErrorCode errorCode = adminGetRestrictionTableEx.EcExecute();
			result = adminGetRestrictionTableEx.Result;
			rowCount = adminGetRestrictionTableEx.ResultRowCount;
			auxiliaryOut = adminGetRestrictionTableEx.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003FC8 File Offset: 0x000021C8
		public int EcAdminExecuteTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid taskClass, int taskId, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminExecuteTask50 adminExecuteTask = new AdminRpcServer.AdminExecuteTask50(callerSecurityContext, mdbGuid, taskClass, taskId, auxiliaryIn);
			ErrorCode errorCode = adminExecuteTask.EcExecute();
			auxiliaryOut = adminExecuteTask.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003FF8 File Offset: 0x000021F8
		public int EcAdminPrePopulateCacheEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] partitionHint, string domainController, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminPrePopulateCacheEx50 adminPrePopulateCacheEx = new AdminRpcServer.AdminPrePopulateCacheEx50(callerSecurityContext, mdbGuid, mailboxGuid, partitionHint, domainController, auxiliaryIn);
			ErrorCode errorCode = adminPrePopulateCacheEx.EcExecute();
			auxiliaryOut = adminPrePopulateCacheEx.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000402C File Offset: 0x0000222C
		public int EcMultiMailboxSearch(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequest, out byte[] searchResultsOut, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.TraceFunction("Entering AdminRpcServer.EcMultiMailboxSearch");
			int result = this.ExecuteEcMultiMailboxSearch(null, callerSecurityContext, mdbGuid, searchRequest, out searchResultsOut, auxiliaryIn, out auxiliaryOut);
			AdminRpcServer.TraceFunction("Exiting AdminRpcServer.EcMultiMailboxSearch");
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004060 File Offset: 0x00002260
		public int EcGetMultiMailboxSearchKeywordStats(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] keywordStatRequest, out byte[] keywordStatsResultsOut, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.TraceFunction("Entering AdminRpcServer.EcGetMultiMailboxSearchKeywordStats");
			int result = this.ExecuteEcGetMultiMailboxSearchKeywordStats(null, callerSecurityContext, mdbGuid, keywordStatRequest, out keywordStatsResultsOut, auxiliaryIn, out auxiliaryOut);
			AdminRpcServer.TraceFunction("Exiting AdminRpcServer.EcGetMultiMailboxSearchKeywordStats");
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004094 File Offset: 0x00002294
		internal int ExecuteEcMultiMailboxSearch(IMultiMailboxSearch searcher, ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequestByteArray, out byte[] searchResponseByteArray, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.TraceFunction("Entering AdminRpcServer.ExecuteEcMultiMailboxSearch");
			searchResponseByteArray = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcMultiMailboxSearch adminRpcMultiMailboxSearch = new AdminRpcServer.AdminRpcMultiMailboxSearch(callerSecurityContext, mdbGuid, searchRequestByteArray, auxiliaryIn);
			IDisposable disposable = null;
			if (searcher != null)
			{
				disposable = adminRpcMultiMailboxSearch.SetMultiMailboxSearchTestHook(searcher);
			}
			ErrorCode errorCode = ErrorCode.NoError;
			using (disposable)
			{
				errorCode = adminRpcMultiMailboxSearch.EcExecute();
				if (errorCode == ErrorCode.NoError)
				{
					searchResponseByteArray = adminRpcMultiMailboxSearch.ResponseAsByteArray;
				}
			}
			auxiliaryOut = adminRpcMultiMailboxSearch.AuxiliaryOut;
			AdminRpcServer.TraceFunction("Exiting AdminRpcServer.ExecuteEcMultiMailboxSearch");
			return (int)errorCode;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004128 File Offset: 0x00002328
		internal int ExecuteEcGetMultiMailboxSearchKeywordStats(IMultiMailboxSearch searcher, ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequestByteArray, out byte[] searchResponseByteArray, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.TraceFunction("Entering AdminRpcServer.ExecuteEcGetMultiMailboxSearchKeywordStats");
			searchResponseByteArray = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcMultiMailboxSearchKeywordStats adminRpcMultiMailboxSearchKeywordStats = new AdminRpcServer.AdminRpcMultiMailboxSearchKeywordStats(callerSecurityContext, mdbGuid, searchRequestByteArray, auxiliaryIn);
			IDisposable disposable = null;
			if (searcher != null)
			{
				disposable = adminRpcMultiMailboxSearchKeywordStats.SetMultiMailboxSearchTestHook(searcher);
			}
			ErrorCode errorCode = ErrorCode.NoError;
			using (disposable)
			{
				errorCode = adminRpcMultiMailboxSearchKeywordStats.EcExecute();
				if (errorCode == ErrorCode.NoError)
				{
					searchResponseByteArray = adminRpcMultiMailboxSearchKeywordStats.ResponseAsByteArray;
				}
			}
			auxiliaryOut = adminRpcMultiMailboxSearchKeywordStats.AuxiliaryOut;
			AdminRpcServer.TraceFunction("Exiting AdminRpcServer.ExecuteEcGetMultiMailboxSearchKeywordStats");
			return (int)errorCode;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000041BC File Offset: 0x000023BC
		private static void TraceFunction(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.MultiMailboxSearchTracer.TraceFunction(36368, 0L, message);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000041E8 File Offset: 0x000023E8
		public int EcCreateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminCreateUserInfo50 adminCreateUserInfo = new AdminRpcServer.AdminCreateUserInfo50(callerSecurityContext, mdbGuid, userInfoGuid, flags, properties, auxiliaryIn);
			ErrorCode errorCode = adminCreateUserInfo.EcExecute();
			auxiliaryOut = adminCreateUserInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000421C File Offset: 0x0000241C
		public int EcReadUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, uint[] propertyTags, out ArraySegment<byte> result, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminReadUserInfo50 adminReadUserInfo = new AdminRpcServer.AdminReadUserInfo50(callerSecurityContext, mdbGuid, userInfoGuid, flags, propertyTags, auxiliaryIn);
			ErrorCode errorCode = adminReadUserInfo.EcExecute();
			result = adminReadUserInfo.Result;
			auxiliaryOut = adminReadUserInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000425C File Offset: 0x0000245C
		public int EcUpdateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, uint[] deletePropertyTags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			AdminRpcServer.AdminUpdateUserInfo50 adminUpdateUserInfo = new AdminRpcServer.AdminUpdateUserInfo50(callerSecurityContext, mdbGuid, userInfoGuid, flags, properties, deletePropertyTags, auxiliaryIn);
			ErrorCode errorCode = adminUpdateUserInfo.EcExecute();
			auxiliaryOut = adminUpdateUserInfo.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004290 File Offset: 0x00002490
		public int EcDeleteUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			auxiliaryOut = null;
			return -2147221246;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000429C File Offset: 0x0000249C
		private static Properties ParseProperties(byte[] propertiesBuffer, uint[] deletePropertyTags)
		{
			Properties result = new Properties(20);
			if (propertiesBuffer != null && propertiesBuffer.Length != 0)
			{
				using (Reader reader = new BufferReader(new ArraySegment<byte>(propertiesBuffer)))
				{
					PropertyValue[] array = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
					foreach (PropertyValue propertyValue in array)
					{
						object value = propertyValue.Value;
						RcaTypeHelpers.MassageIncomingPropertyValue(propertyValue.PropertyTag, ref value);
						result.Add(LegacyHelper.ConvertFromLegacyPropTag(propertyValue.PropertyTag, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.UserInfo, null, false), value);
					}
				}
			}
			if (deletePropertyTags != null && deletePropertyTags.Length != 0)
			{
				StorePropTag[] array3 = LegacyHelper.ConvertFromLegacyPropTags(deletePropertyTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.UserInfo, null, false);
				foreach (StorePropTag storePropTag in array3)
				{
					if (result.Contains(storePropTag))
					{
						throw new StoreException((LID)54716U, ErrorCodeValue.InvalidParameter);
					}
					result.Add(storePropTag, null);
				}
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000043AC File Offset: 0x000025AC
		public int EcAdminISIntegCheck50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] taskIds, out string requestId, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			requestId = null;
			AdminRpcServer.AdminRpcStoreIntegrityCheck adminRpcStoreIntegrityCheck = new AdminRpcServer.AdminRpcStoreIntegrityCheck(callerSecurityContext, mdbGuid, mailboxGuid, flags, taskIds, auxiliaryIn);
			ErrorCode errorCode = adminRpcStoreIntegrityCheck.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				requestId = adminRpcStoreIntegrityCheck.RequestGuid.ToString();
			}
			auxiliaryOut = adminRpcStoreIntegrityCheck.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004408 File Offset: 0x00002608
		public int EcAdminIntegrityCheckEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint operation, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			response = null;
			auxiliaryOut = null;
			AdminRpcServer.AdminRpcStoreIntegrityCheckEx adminRpcStoreIntegrityCheckEx = new AdminRpcServer.AdminRpcStoreIntegrityCheckEx(callerSecurityContext, mdbGuid, mailboxGuid, operation, request, auxiliaryIn);
			ErrorCode errorCode = adminRpcStoreIntegrityCheckEx.EcExecute();
			if (errorCode == ErrorCode.NoError)
			{
				response = adminRpcStoreIntegrityCheckEx.Response;
			}
			auxiliaryOut = adminRpcStoreIntegrityCheckEx.AuxiliaryOut;
			return (int)errorCode;
		}

		// Token: 0x0400005C RID: 92
		public const uint MailboxSignatureVersion = 104U;

		// Token: 0x0400005D RID: 93
		public const uint DeleteMailboxVersion = 1U;

		// Token: 0x0400005E RID: 94
		public const uint InTransitStatusVersion = 1U;

		// Token: 0x0400005F RID: 95
		public const uint MailboxShapeVersion = 4U;

		// Token: 0x04000060 RID: 96
		public const ushort MajorInterfaceVersion = 7;

		// Token: 0x04000061 RID: 97
		public const ushort MinorInterfaceVersion = 17;

		// Token: 0x02000015 RID: 21
		public enum VersionedFeature
		{
			// Token: 0x04000063 RID: 99
			None,
			// Token: 0x04000064 RID: 100
			MailboxSignatureServerVersion,
			// Token: 0x04000065 RID: 101
			DeleteMailboxServerVersion,
			// Token: 0x04000066 RID: 102
			IntransitStatusServerVersion,
			// Token: 0x04000067 RID: 103
			MailboxShapeServerVersion
		}

		// Token: 0x02000016 RID: 22
		internal class AdminRpcListAllMdbStatus : AdminRpc
		{
			// Token: 0x06000086 RID: 134 RVA: 0x00004460 File Offset: 0x00002660
			public AdminRpcListAllMdbStatus(ClientSecurityContext callerSecurityContext, bool basicInformation, byte[] auxiliaryIn) : base(AdminMethod.EcListAllMdbStatus50, callerSecurityContext, auxiliaryIn)
			{
				this.basicInformation = basicInformation;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000087 RID: 135 RVA: 0x00004473 File Offset: 0x00002673
			public uint MdbCount
			{
				get
				{
					return this.mdbCount;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000088 RID: 136 RVA: 0x0000447B File Offset: 0x0000267B
			public byte[] MdbStatusRaw
			{
				get
				{
					return this.mdbStatusRaw;
				}
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00004484 File Offset: 0x00002684
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ICollection<StoreDatabase> databaseListSnapshot = Storage.GetDatabaseListSnapshot();
				this.mdbCount = (uint)databaseListSnapshot.Count;
				List<AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus> list = new List<AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus>((int)this.MdbCount);
				foreach (StoreDatabase storeDatabase in databaseListSnapshot)
				{
					storeDatabase.GetSharedLock(context.Diagnostics);
					try
					{
						list.Add(new AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus(storeDatabase.MdbGuid, (ulong)storeDatabase.ExternalMdbStatus, storeDatabase.MdbName, storeDatabase.VServerName, storeDatabase.LegacyDN));
					}
					finally
					{
						storeDatabase.ReleaseSharedLock();
					}
				}
				AdminRpcParseFormat.SerializeMdbStatus(list, this.basicInformation, out this.mdbStatusRaw);
				return ErrorCode.NoError;
			}

			// Token: 0x04000068 RID: 104
			private bool basicInformation;

			// Token: 0x04000069 RID: 105
			private uint mdbCount;

			// Token: 0x0400006A RID: 106
			private byte[] mdbStatusRaw;

			// Token: 0x02000017 RID: 23
			public struct MdbStatus
			{
				// Token: 0x0600008A RID: 138 RVA: 0x00004548 File Offset: 0x00002748
				public MdbStatus(Guid guidMdb, ulong ulStatus, string mdbName, string vServerName, string legacyDN)
				{
					this.guidMdb = guidMdb;
					this.ulStatus = ulStatus;
					this.mdbName = mdbName;
					this.vServerName = vServerName;
					this.legacyDN = legacyDN;
				}

				// Token: 0x17000012 RID: 18
				// (get) Token: 0x0600008B RID: 139 RVA: 0x0000456F File Offset: 0x0000276F
				public Guid GuidMdb
				{
					get
					{
						return this.guidMdb;
					}
				}

				// Token: 0x17000013 RID: 19
				// (get) Token: 0x0600008C RID: 140 RVA: 0x00004577 File Offset: 0x00002777
				public ulong UlStatus
				{
					get
					{
						return this.ulStatus;
					}
				}

				// Token: 0x17000014 RID: 20
				// (get) Token: 0x0600008D RID: 141 RVA: 0x0000457F File Offset: 0x0000277F
				public string MdbName
				{
					get
					{
						return this.mdbName;
					}
				}

				// Token: 0x17000015 RID: 21
				// (get) Token: 0x0600008E RID: 142 RVA: 0x00004587 File Offset: 0x00002787
				public string VServerName
				{
					get
					{
						return this.vServerName;
					}
				}

				// Token: 0x17000016 RID: 22
				// (get) Token: 0x0600008F RID: 143 RVA: 0x0000458F File Offset: 0x0000278F
				public string LegacyDN
				{
					get
					{
						return this.legacyDN;
					}
				}

				// Token: 0x0400006B RID: 107
				private Guid guidMdb;

				// Token: 0x0400006C RID: 108
				private ulong ulStatus;

				// Token: 0x0400006D RID: 109
				private string mdbName;

				// Token: 0x0400006E RID: 110
				private string vServerName;

				// Token: 0x0400006F RID: 111
				private string legacyDN;
			}
		}

		// Token: 0x02000018 RID: 24
		internal class AdminRpcGetDatabaseSize : AdminRpc
		{
			// Token: 0x06000090 RID: 144 RVA: 0x00004597 File Offset: 0x00002797
			public AdminRpcGetDatabaseSize(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn) : base(AdminMethod.EcGetDatabaseSizeEx50, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000091 RID: 145 RVA: 0x000045A9 File Offset: 0x000027A9
			public uint TotalPages
			{
				get
				{
					return this.totalPages;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000092 RID: 146 RVA: 0x000045B1 File Offset: 0x000027B1
			public uint AvailablePages
			{
				get
				{
					return this.availablePages;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000093 RID: 147 RVA: 0x000045B9 File Offset: 0x000027B9
			public uint PageSize
			{
				get
				{
					return this.pageSize;
				}
			}

			// Token: 0x06000094 RID: 148 RVA: 0x000045C1 File Offset: 0x000027C1
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				base.Database.PhysicalDatabase.GetDatabaseSize(context, out this.totalPages, out this.availablePages, out this.pageSize);
				return ErrorCode.NoError;
			}

			// Token: 0x04000070 RID: 112
			private uint totalPages;

			// Token: 0x04000071 RID: 113
			private uint availablePages;

			// Token: 0x04000072 RID: 114
			private uint pageSize;
		}

		// Token: 0x02000019 RID: 25
		internal class AdminRpcStartBlockModeReplicationToPassive : AdminRpc
		{
			// Token: 0x06000095 RID: 149 RVA: 0x000045EB File Offset: 0x000027EB
			public AdminRpcStartBlockModeReplicationToPassive(ClientSecurityContext callerSecurityContext, Guid mdbGuid, string passiveName, uint firstGenToSend, byte[] auxiliaryIn) : base(AdminMethod.EcAdminStartBlockModeReplicationToPassive50, callerSecurityContext, auxiliaryIn)
			{
				base.MdbGuid = new Guid?(mdbGuid);
				this.passiveName = passiveName;
				this.firstGenToSend = firstGenToSend;
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00004613 File Offset: 0x00002813
			protected override ErrorCode EcCheckPermissions(MapiContext context)
			{
				return AdminRpcPermissionChecks.EcCheckConstrainedDelegationRights(context, base.DatabaseInfo);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00004624 File Offset: 0x00002824
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				StoreDatabase storeDatabase = Storage.FindDatabase(base.MdbGuid.Value);
				if (storeDatabase == null)
				{
					errorCode = ErrorCode.CreateNotFound((LID)49352U);
				}
				else
				{
					storeDatabase.GetSharedLock(context.Diagnostics);
					try
					{
						JetHADatabase jetHADatabase = storeDatabase.PhysicalDatabase as JetHADatabase;
						if (jetHADatabase == null)
						{
							errorCode = ErrorCode.CreateBlockModeInitFailed((LID)41928U);
						}
						else
						{
							errorCode = jetHADatabase.StartBlockModeReplicationToPassive(context.Diagnostics, this.passiveName, this.firstGenToSend);
						}
					}
					finally
					{
						storeDatabase.ReleaseSharedLock();
					}
				}
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)48840U);
				}
				return errorCode;
			}

			// Token: 0x04000073 RID: 115
			private string passiveName;

			// Token: 0x04000074 RID: 116
			private uint firstGenToSend;
		}

		// Token: 0x0200001A RID: 26
		internal class AdminRpcPurgeCachedMdbObject : AdminRpc
		{
			// Token: 0x06000098 RID: 152 RVA: 0x000046E0 File Offset: 0x000028E0
			internal AdminRpcPurgeCachedMdbObject(ClientSecurityContext callerSecurityContext, Guid databaseGuid, byte[] auxiliaryIn) : base(AdminMethod.EcPurgeCachedMdbObject50, callerSecurityContext, auxiliaryIn)
			{
				this.databaseGuid = databaseGuid;
			}

			// Token: 0x06000099 RID: 153 RVA: 0x000046F4 File Offset: 0x000028F4
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshDatabaseInfo(context, this.databaseGuid);
				ServerInfo serverInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(context);
				if (this.databaseGuid.Equals(serverInfo.Guid))
				{
					Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshServerInfo(context);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x04000075 RID: 117
			private readonly Guid databaseGuid;
		}

		// Token: 0x0200001B RID: 27
		internal class AdminDoMaintenanceTask : AdminRpc
		{
			// Token: 0x0600009A RID: 154 RVA: 0x00004744 File Offset: 0x00002944
			public AdminDoMaintenanceTask(ClientSecurityContext callerSecurityContext, Guid databaseGuid, uint task, byte[] auxiliaryIn) : base(AdminMethod.EcDoMaintenanceTask50, callerSecurityContext, auxiliaryIn)
			{
				this.task = (AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask)task;
				base.MdbGuid = new Guid?(databaseGuid);
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00004764 File Offset: 0x00002964
			internal override int OperationDetail
			{
				get
				{
					return (int)(this.task + 2000U);
				}
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00004774 File Offset: 0x00002974
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode result = ErrorCode.NoError;
				AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask maintenanceTask = this.task;
				if (maintenanceTask != AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask.ReReadMDBQuotas)
				{
					switch (maintenanceTask)
					{
					case AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask.PurgeDatabaseCache:
						result = Storage.PurgeDatabaseCache(context, base.MdbGuid.Value).Propagate((LID)38152U);
						break;
					case AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask.ExtendDatabase:
						result = Storage.ExtendDatabase(context, base.MdbGuid.Value).Propagate((LID)44648U);
						break;
					case AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask.ShrinkDatabase:
						result = Storage.ShrinkDatabase(context, base.MdbGuid.Value).Propagate((LID)64264U);
						break;
					case AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask.CleanupVersionStore:
						if (!DefaultSettings.Get.VersionStoreCleanupMaintenanceTaskSupported)
						{
							result = ErrorCode.CreateWithLid((LID)44028U, ErrorCodeValue.NotSupported);
						}
						else
						{
							result = Storage.VersionStoreCleanup(context, base.MdbGuid.Value).Propagate((LID)60412U);
						}
						break;
					default:
						result = ErrorCode.CreateWithLid((LID)57016U, ErrorCodeValue.NotSupported);
						break;
					}
				}
				else
				{
					Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshDatabaseInfo(context, base.MdbGuid.Value);
				}
				return result;
			}

			// Token: 0x04000076 RID: 118
			private readonly AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask task;

			// Token: 0x0200001C RID: 28
			internal enum MaintenanceTask : uint
			{
				// Token: 0x04000078 RID: 120
				FolderTombstones = 1U,
				// Token: 0x04000079 RID: 121
				FolderConflictAging,
				// Token: 0x0400007A RID: 122
				SiteFolderCheck,
				// Token: 0x0400007B RID: 123
				EventHistoryCleanup,
				// Token: 0x0400007C RID: 124
				TombstonesMaintenance,
				// Token: 0x0400007D RID: 125
				PurgeIndices,
				// Token: 0x0400007E RID: 126
				PFExpiry,
				// Token: 0x0400007F RID: 127
				UpdateServerVersions,
				// Token: 0x04000080 RID: 128
				HardDeletes,
				// Token: 0x04000081 RID: 129
				DeletedMailboxCleanup,
				// Token: 0x04000082 RID: 130
				ReReadMDBQuotas,
				// Token: 0x04000083 RID: 131
				ReReadAuditInfo,
				// Token: 0x04000084 RID: 132
				InvCachedDsInfo,
				// Token: 0x04000085 RID: 133
				DbSizeCheck,
				// Token: 0x04000086 RID: 134
				DeliveredToCleanup,
				// Token: 0x04000087 RID: 135
				FolderCleanup,
				// Token: 0x04000088 RID: 136
				AgeOutAllFolders,
				// Token: 0x04000089 RID: 137
				AgeOutAllViews,
				// Token: 0x0400008A RID: 138
				AgeOutAllDVUEntries,
				// Token: 0x0400008B RID: 139
				MdbHealthCheck,
				// Token: 0x0400008C RID: 140
				QuarantinedMailboxCleanup,
				// Token: 0x0400008D RID: 141
				RequestTimeoutTest,
				// Token: 0x0400008E RID: 142
				DeletedCiFailedItemCleanup,
				// Token: 0x0400008F RID: 143
				ISINTEGProvisionedFolders,
				// Token: 0x04000090 RID: 144
				PurgeDatabaseCache = 100U,
				// Token: 0x04000091 RID: 145
				ExtendDatabase,
				// Token: 0x04000092 RID: 146
				ShrinkDatabase,
				// Token: 0x04000093 RID: 147
				CleanupVersionStore
			}
		}

		// Token: 0x0200001D RID: 29
		internal class AdminRpcGetLastBackupInfo50 : AdminRpc
		{
			// Token: 0x0600009D RID: 157 RVA: 0x000048B9 File Offset: 0x00002AB9
			public AdminRpcGetLastBackupInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn) : base(AdminMethod.EcGetLastBackupTimes50, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600009E RID: 158 RVA: 0x000048CB File Offset: 0x00002ACB
			public DateTime LastFullBackupTime
			{
				get
				{
					return this.lastFullBackupTime;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600009F RID: 159 RVA: 0x000048D3 File Offset: 0x00002AD3
			public DateTime LastIncrementalBackupTime
			{
				get
				{
					return this.lastIncrementalBackupTime;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x000048DB File Offset: 0x00002ADB
			public DateTime LastDifferentialBackupTime
			{
				get
				{
					return this.lastDifferentialBackupTime;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x000048E3 File Offset: 0x00002AE3
			public DateTime LastCopyBackupTime
			{
				get
				{
					return this.lastCopyBackupTime;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x000048EB File Offset: 0x00002AEB
			public int SnapshotFullBackup
			{
				get
				{
					return this.snapFull;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x000048F3 File Offset: 0x00002AF3
			public int SnapshotIncrementalBackup
			{
				get
				{
					return this.snapIncremental;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x000048FB File Offset: 0x00002AFB
			public int SnapshotDifferentialBackup
			{
				get
				{
					return this.snapDifferential;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004903 File Offset: 0x00002B03
			public int SnapshotCopyBackup
			{
				get
				{
					return this.snapCopy;
				}
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x0000490B File Offset: 0x00002B0B
			protected override ErrorCode EcCheckPermissions(MapiContext context)
			{
				return AdminRpcPermissionChecks.EcDefaultCheck(context, base.DatabaseInfo);
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x0000491C File Offset: 0x00002B1C
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				base.Database.PhysicalDatabase.GetLastBackupInformation(context, out this.lastFullBackupTime, out this.lastIncrementalBackupTime, out this.lastDifferentialBackupTime, out this.lastCopyBackupTime, out this.snapFull, out this.snapIncremental, out this.snapDifferential, out this.snapCopy);
				return ErrorCode.NoError;
			}

			// Token: 0x04000094 RID: 148
			private DateTime lastFullBackupTime;

			// Token: 0x04000095 RID: 149
			private DateTime lastIncrementalBackupTime;

			// Token: 0x04000096 RID: 150
			private DateTime lastDifferentialBackupTime;

			// Token: 0x04000097 RID: 151
			private DateTime lastCopyBackupTime;

			// Token: 0x04000098 RID: 152
			private int snapFull;

			// Token: 0x04000099 RID: 153
			private int snapIncremental;

			// Token: 0x0400009A RID: 154
			private int snapDifferential;

			// Token: 0x0400009B RID: 155
			private int snapCopy;
		}

		// Token: 0x0200001E RID: 30
		internal sealed class AdminRpcLogReplayRequest2 : AdminRpc
		{
			// Token: 0x060000A8 RID: 168 RVA: 0x0000496F File Offset: 0x00002B6F
			public AdminRpcLogReplayRequest2(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint logReplayMax, uint logReplayFlags, byte[] auxiliaryIn) : base(AdminMethod.EcLogReplayRequest2, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.AnyOnlineState, auxiliaryIn)
			{
				this.logReplayMax = logReplayMax;
				this.logReplayFlags = logReplayFlags;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004992 File Offset: 0x00002B92
			// (set) Token: 0x060000AA RID: 170 RVA: 0x0000499A File Offset: 0x00002B9A
			public uint LogReplayNext { get; private set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000AB RID: 171 RVA: 0x000049A3 File Offset: 0x00002BA3
			// (set) Token: 0x060000AC RID: 172 RVA: 0x000049AB File Offset: 0x00002BAB
			public byte[] DatabaseHeaderInfo { get; private set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000AD RID: 173 RVA: 0x000049B4 File Offset: 0x00002BB4
			// (set) Token: 0x060000AE RID: 174 RVA: 0x000049BC File Offset: 0x00002BBC
			public uint PatchPageNumber { get; private set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000AF RID: 175 RVA: 0x000049C5 File Offset: 0x00002BC5
			// (set) Token: 0x060000B0 RID: 176 RVA: 0x000049CD File Offset: 0x00002BCD
			public byte[] PatchToken { get; private set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x000049D6 File Offset: 0x00002BD6
			// (set) Token: 0x060000B2 RID: 178 RVA: 0x000049DE File Offset: 0x00002BDE
			public byte[] PatchData { get; private set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x000049E7 File Offset: 0x00002BE7
			// (set) Token: 0x060000B4 RID: 180 RVA: 0x000049EF File Offset: 0x00002BEF
			public uint[] CorruptPages { get; private set; }

			// Token: 0x060000B5 RID: 181 RVA: 0x000049F8 File Offset: 0x00002BF8
			protected override ErrorCode EcCheckPermissions(MapiContext context)
			{
				return AdminRpcPermissionChecks.EcCheckConstrainedDelegationRights(context, base.DatabaseInfo);
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00004A08 File Offset: 0x00002C08
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				if (!base.Database.IsOnlinePassive)
				{
					base.Database.TraceState((LID)54268U);
					return ErrorCode.CreateNotFound((LID)43672U);
				}
				base.Database.PhysicalDatabase.LogReplayStatus.SetMaxLogGenerationToReplay(this.logReplayMax, this.logReplayFlags);
				uint logReplayNext;
				byte[] databaseHeaderInfo;
				uint patchPageNumber;
				byte[] patchToken;
				byte[] patchData;
				uint[] corruptPages;
				base.Database.PhysicalDatabase.LogReplayStatus.GetReplayStatus(out logReplayNext, out databaseHeaderInfo, out patchPageNumber, out patchToken, out patchData, out corruptPages);
				this.LogReplayNext = logReplayNext;
				this.DatabaseHeaderInfo = databaseHeaderInfo;
				this.PatchPageNumber = patchPageNumber;
				this.PatchToken = patchToken;
				this.PatchData = patchData;
				this.CorruptPages = corruptPages;
				return ErrorCode.NoError;
			}

			// Token: 0x0400009C RID: 156
			private readonly uint logReplayMax;

			// Token: 0x0400009D RID: 157
			private readonly uint logReplayFlags;
		}

		// Token: 0x0200001F RID: 31
		internal sealed class AdminRpcGetResourceMonitorDigest : AdminRpc
		{
			// Token: 0x060000B7 RID: 183 RVA: 0x00004ABA File Offset: 0x00002CBA
			public AdminRpcGetResourceMonitorDigest(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propertyTags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetResourceMonitorDigest50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.AnyAttachedState, auxiliaryIn)
			{
				this.propertyTags = propertyTags;
				this.RowCount = 0U;
				this.Result = null;
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004AE3 File Offset: 0x00002CE3
			// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004AEB File Offset: 0x00002CEB
			public byte[] Result { get; private set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000BA RID: 186 RVA: 0x00004AF4 File Offset: 0x00002CF4
			// (set) Token: 0x060000BB RID: 187 RVA: 0x00004AFC File Offset: 0x00002CFC
			public uint RowCount { get; private set; }

			// Token: 0x060000BC RID: 188 RVA: 0x00004B08 File Offset: 0x00002D08
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (!(ErrorCode.NoError != errorCode) && (this.propertyTags == null || this.propertyTags.Length == 0))
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)50984U);
				}
				return errorCode;
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00004B50 File Offset: 0x00002D50
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				StorePropTag[] storePropTags = LegacyHelper.ConvertFromLegacyPropTags(this.propertyTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Mailbox, null, true);
				List<Properties> digestRows = this.GetDigestRows(context, context.Database, storePropTags);
				if (digestRows != null && digestRows.Count > 0)
				{
					int num = 0;
					int num2 = 0;
					for (int i = 0; i < digestRows.Count; i++)
					{
						LegacyHelper.MassageOutgoingProperties(digestRows[i]);
						num2 += AdminRpcParseFormat.SetValues(null, ref num, 0, digestRows[i]);
					}
					byte[] array = new byte[num2];
					num = 0;
					for (int j = 0; j < digestRows.Count; j++)
					{
						AdminRpcParseFormat.SetValues(array, ref num, array.Length, digestRows[j]);
					}
					this.RowCount = (uint)digestRows.Count;
					this.Result = array;
				}
				return ErrorCode.NoError;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00004C18 File Offset: 0x00002E18
			private List<Properties> GetDigestRows(MapiContext context, StoreDatabase database, StorePropTag[] storePropTags)
			{
				ResourceMonitorDigestSnapshot digestHistory = database.ResourceDigest.GetDigestHistory();
				int num = ((digestHistory.TimeInServerDigest != null) ? digestHistory.TimeInServerDigest.Length : 0) + ((digestHistory.LogRecordBytesDigest != null) ? digestHistory.LogRecordBytesDigest.Length : 0);
				if (num == 0)
				{
					return null;
				}
				List<Properties> list = new List<Properties>(num * 25 * 2);
				if (digestHistory.TimeInServerDigest != null && digestHistory.TimeInServerDigest.Length > 0)
				{
					for (int i = 0; i < digestHistory.TimeInServerDigest.Length; i++)
					{
						ResourceDigestStats[] array = digestHistory.TimeInServerDigest[i];
						if (array != null && array.Length > 0)
						{
							for (int j = 0; j < array.Length; j++)
							{
								list.Add(this.GetRowFromSample(context, i + 1, "TimeInServer", array[j], storePropTags));
							}
						}
					}
				}
				if (digestHistory.LogRecordBytesDigest != null && digestHistory.LogRecordBytesDigest.Length > 0)
				{
					for (int k = 0; k < digestHistory.LogRecordBytesDigest.Length; k++)
					{
						ResourceDigestStats[] array2 = digestHistory.LogRecordBytesDigest[k];
						if (array2 != null && array2.Length > 0)
						{
							for (int l = 0; l < array2.Length; l++)
							{
								list.Add(this.GetRowFromSample(context, k + 1, "LogBytes", array2[l], storePropTags));
							}
						}
					}
				}
				return list;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00004D64 File Offset: 0x00002F64
			private Properties GetRowFromSample(MapiContext context, int sampleId, string digestCategory, ResourceDigestStats sample, StorePropTag[] storePropTags)
			{
				string text;
				bool flag;
				this.GetMailboxDetails(context, sample.MailboxNumber, out text, out flag);
				Properties result = new Properties(storePropTags.Length);
				for (int i = 0; i < storePropTags.Length; i++)
				{
					object obj = null;
					ushort propId = storePropTags[i].PropId;
					if (propId <= 26375)
					{
						if (propId != 12289)
						{
							if (propId == 26375)
							{
								obj = sample.MailboxGuid.ToByteArray();
							}
						}
						else
						{
							obj = text;
						}
					}
					else
					{
						switch (propId)
						{
						case 26413:
							obj = (int)sample.TimeInServer.TotalMilliseconds;
							break;
						case 26414:
							obj = (int)sample.TimeInCPU.TotalMilliseconds;
							break;
						case 26415:
							obj = sample.ROPCount;
							break;
						case 26416:
							obj = sample.PageRead;
							break;
						case 26417:
							obj = sample.PagePreread;
							break;
						case 26418:
							obj = sample.LogRecordCount;
							break;
						case 26419:
							obj = sample.LogRecordBytes;
							break;
						case 26420:
							obj = sample.LdapReads;
							break;
						case 26421:
							obj = sample.LdapSearches;
							break;
						case 26422:
							obj = digestCategory;
							break;
						case 26423:
							obj = sampleId;
							break;
						case 26424:
							obj = sample.SampleTime;
							break;
						default:
							if (propId == 26650)
							{
								obj = flag;
							}
							break;
						}
					}
					if (obj != null)
					{
						result.Add(storePropTags[i], obj);
					}
					else
					{
						result.Add(storePropTags[i].ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
					}
				}
				return result;
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00004F44 File Offset: 0x00003144
			private void GetMailboxDetails(MapiContext context, int mailboxNumber, out string displayName, out bool quarantined)
			{
				displayName = null;
				quarantined = false;
				if (this.mailboxInfo == null)
				{
					this.mailboxInfo = new Dictionary<int, AdminRpcServer.AdminRpcGetResourceMonitorDigest.MailboxDigestInfo>();
				}
				AdminRpcServer.AdminRpcGetResourceMonitorDigest.MailboxDigestInfo value;
				if (this.mailboxInfo.TryGetValue(mailboxNumber, out value))
				{
					displayName = value.DisplayName;
					quarantined = value.Quarantined;
					return;
				}
				TimeSpan lockTimeout = TimeSpan.FromSeconds(5.0);
				context.InitializeMailboxExclusiveOperation(mailboxNumber, ExecutionDiagnostics.OperationSource.AdminRpc, lockTimeout);
				try
				{
					ErrorCode first = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
					if (first != ErrorCode.NoError)
					{
						return;
					}
					quarantined = context.LockedMailboxState.Quarantined;
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						if (mailbox != null)
						{
							displayName = mailbox.GetDisplayName(context);
						}
					}
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(false);
					}
				}
				value.DisplayName = displayName;
				value.Quarantined = quarantined;
				this.mailboxInfo[mailboxNumber] = value;
			}

			// Token: 0x040000A4 RID: 164
			private readonly uint[] propertyTags;

			// Token: 0x040000A5 RID: 165
			private Dictionary<int, AdminRpcServer.AdminRpcGetResourceMonitorDigest.MailboxDigestInfo> mailboxInfo;

			// Token: 0x02000020 RID: 32
			private struct MailboxDigestInfo
			{
				// Token: 0x040000A8 RID: 168
				public string DisplayName;

				// Token: 0x040000A9 RID: 169
				public bool Quarantined;
			}
		}

		// Token: 0x02000021 RID: 33
		internal sealed class AdminRpcGetDatabaseProcessInfo : AdminRpc
		{
			// Token: 0x060000C1 RID: 193 RVA: 0x00005048 File Offset: 0x00003248
			public AdminRpcGetDatabaseProcessInfo(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propTags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetDatabaseProcessInfo50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.AnyOnlineState, auxiliaryIn)
			{
				this.propTags = propTags;
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005063 File Offset: 0x00003263
			public byte[] Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000506B File Offset: 0x0000326B
			public uint RowCount
			{
				get
				{
					return this.rowCount;
				}
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00005074 File Offset: 0x00003274
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				StorePropTag[] array = LegacyHelper.ConvertFromLegacyPropTags(this.propTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.ProcessInfo, null, true);
				Properties properties = new Properties(array.Length);
				if (context.Database != null)
				{
					int i = 0;
					while (i < array.Length)
					{
						object obj = null;
						switch (array[i].PropId)
						{
						case 26263:
							using (Process currentProcess = Process.GetCurrentProcess())
							{
								obj = currentProcess.Id;
								break;
							}
							goto IL_87;
						case 26264:
							goto IL_87;
						case 26265:
							obj = StoreDatabase.GetMaximumSchemaVersion().Value;
							break;
						case 26266:
							if (context.Database.IsOnlineActive)
							{
								obj = context.Database.GetCurrentSchemaVersion(context).Value;
							}
							break;
						case 26267:
							if (context.Database.IsOnlineActive)
							{
								obj = context.Database.GetRequestedSchemaVersion(context, context.Database.GetCurrentSchemaVersion(context), StoreDatabase.GetMaximumSchemaVersion()).Value;
							}
							break;
						}
						IL_116:
						if (obj != null)
						{
							properties.Add(array[i], obj);
						}
						else
						{
							properties.Add(array[i].ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
						}
						i++;
						continue;
						IL_87:
						obj = StoreDatabase.GetMinimumSchemaVersion().Value;
						goto IL_116;
					}
					if (properties.Count > 0)
					{
						LegacyHelper.MassageOutgoingProperties(properties);
						int num = 0;
						int num2 = 0;
						int num3 = AdminRpcParseFormat.SetValues(null, ref num, 0, properties);
						byte[] array2 = new byte[num3];
						AdminRpcParseFormat.SetValues(array2, ref num2, array2.Length, properties);
						this.result = array2;
					}
					this.rowCount = 1U;
					return ErrorCode.NoError;
				}
				return ErrorCode.CreateDatabaseError((LID)44712U);
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00005244 File Offset: 0x00003444
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				if (this.propTags == null || this.propTags.Length == 0)
				{
					return ErrorCode.CreateInvalidParameter((LID)61096U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x040000AA RID: 170
			private uint[] propTags;

			// Token: 0x040000AB RID: 171
			private byte[] result;

			// Token: 0x040000AC RID: 172
			private uint rowCount;
		}

		// Token: 0x02000022 RID: 34
		internal sealed class EcAdminProcessSnapshotOperation : AdminRpc
		{
			// Token: 0x060000C6 RID: 198 RVA: 0x0000526D File Offset: 0x0000346D
			public EcAdminProcessSnapshotOperation(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint opCode, uint flags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminProcessSnapshotOperation50, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
				this.operationCode = (SnapshotOperationCode)opCode;
				this.flags = flags;
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00005290 File Offset: 0x00003490
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				if (base.Database != null && base.Database.IsOnlineActive)
				{
					switch (this.operationCode)
					{
					case SnapshotOperationCode.Prepare:
						if (base.Database.IsBackupInProgress)
						{
							return ErrorCode.CreateBackupInProgress((LID)33832U);
						}
						base.Database.SetBackupInProgress();
						base.Database.PhysicalDatabase.SnapshotPrepare(this.flags);
						break;
					case SnapshotOperationCode.Freeze:
						if (!base.Database.IsBackupInProgress)
						{
							return ErrorCode.CreateInvalidBackupSequence((LID)50216U);
						}
						base.Database.PhysicalDatabase.SnapshotFreeze(this.flags);
						break;
					case SnapshotOperationCode.Thaw:
						if (!base.Database.IsBackupInProgress)
						{
							return ErrorCode.CreateInvalidBackupSequence((LID)47144U);
						}
						base.Database.PhysicalDatabase.SnapshotThaw(this.flags);
						break;
					case SnapshotOperationCode.Truncate:
						if (!base.Database.IsBackupInProgress)
						{
							return ErrorCode.CreateInvalidBackupSequence((LID)63528U);
						}
						base.Database.PhysicalDatabase.SnapshotTruncateLogInstance(this.flags);
						break;
					case SnapshotOperationCode.Stop:
						if (!base.Database.IsBackupInProgress)
						{
							return ErrorCode.CreateInvalidBackupSequence((LID)38952U);
						}
						base.Database.PhysicalDatabase.SnapshotStop(this.flags);
						base.Database.ResetBackupInProgress();
						break;
					default:
						return ErrorCode.CreateInvalidParameter((LID)46928U);
					}
					return ErrorCode.NoError;
				}
				return ErrorCode.CreateDatabaseError((LID)38736U);
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x0000542D File Offset: 0x0000362D
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				if (this.operationCode <= SnapshotOperationCode.None || this.operationCode >= SnapshotOperationCode.Last)
				{
					return ErrorCode.CreateInvalidParameter((LID)55120U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x040000AD RID: 173
			private readonly SnapshotOperationCode operationCode;

			// Token: 0x040000AE RID: 174
			private readonly uint flags;
		}

		// Token: 0x02000023 RID: 35
		internal class EcAdminGetPhysicalDatabaseInformation : AdminRpc
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x00005456 File Offset: 0x00003656
			public EcAdminGetPhysicalDatabaseInformation(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetPhysicalDatabaseInformation50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.AnyOnlineState, auxiliaryIn)
			{
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000CA RID: 202 RVA: 0x00005469 File Offset: 0x00003669
			// (set) Token: 0x060000CB RID: 203 RVA: 0x00005471 File Offset: 0x00003671
			public byte[] DatabaseHeaderInfo { get; private set; }

			// Token: 0x060000CC RID: 204 RVA: 0x0000547A File Offset: 0x0000367A
			protected override ErrorCode EcCheckPermissions(MapiContext context)
			{
				return AdminRpcPermissionChecks.EcCheckConstrainedDelegationRights(context, base.DatabaseInfo);
			}

			// Token: 0x060000CD RID: 205 RVA: 0x00005488 File Offset: 0x00003688
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				byte[] databaseHeaderInfo = null;
				if (base.Database.IsOnlineActive)
				{
					base.Database.PhysicalDatabase.GetSerializedDatabaseInformation(context, out databaseHeaderInfo);
				}
				else if (base.Database.IsOnlinePassive)
				{
					base.Database.PhysicalDatabase.LogReplayStatus.GetDatabaseInformation(out databaseHeaderInfo);
				}
				this.DatabaseHeaderInfo = databaseHeaderInfo;
				return ErrorCode.NoError;
			}
		}

		// Token: 0x02000024 RID: 36
		internal class EcAdminForceNewLog : AdminRpc
		{
			// Token: 0x060000CE RID: 206 RVA: 0x000054E9 File Offset: 0x000036E9
			public EcAdminForceNewLog(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn) : base(AdminMethod.EcForceNewLog50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.OnlineActive, auxiliaryIn)
			{
			}

			// Token: 0x060000CF RID: 207 RVA: 0x000054FC File Offset: 0x000036FC
			protected override ErrorCode EcCheckPermissions(MapiContext context)
			{
				return AdminRpcPermissionChecks.EcDefaultCheck(context, base.DatabaseInfo);
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x0000550A File Offset: 0x0000370A
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				base.Database.PhysicalDatabase.ForceNewLog(context);
				return ErrorCode.NoError;
			}
		}

		// Token: 0x02000025 RID: 37
		internal abstract class AdminRpcEventsBase : AdminRpc
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x00005522 File Offset: 0x00003722
			public AdminRpcEventsBase(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, byte[] auxiliaryIn) : base(methodId, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
				this.mdbVersionGuid = mdbVersionGuid;
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x0000553C File Offset: 0x0000373C
			public AdminRpcEventsBase(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid mdbGuid, AdminRpc.ExpectedDatabaseState expectedDatabaseState, Guid mdbVersionGuid, byte[] auxiliaryIn) : base(methodId, callerSecurityContext, new Guid?(mdbGuid), expectedDatabaseState, auxiliaryIn)
			{
				this.mdbVersionGuid = mdbVersionGuid;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005558 File Offset: 0x00003758
			public Guid MdbVersionGuid
			{
				get
				{
					return this.mdbVersionGuid;
				}
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00005560 File Offset: 0x00003760
			protected ErrorCode CheckMdbVersion()
			{
				EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
				if (this.mdbVersionGuid == Guid.Empty)
				{
					this.mdbVersionGuid = eventHistory.MdbVersionGuid;
				}
				else if (this.mdbVersionGuid != eventHistory.MdbVersionGuid)
				{
					return ErrorCode.CreateVersionMismatch((LID)42044U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x040000B0 RID: 176
			protected Guid mdbVersionGuid;
		}

		// Token: 0x02000026 RID: 38
		internal class AdminRpcReadMdbEvents : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x000055C1 File Offset: 0x000037C1
			public AdminRpcReadMdbEvents(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, byte[] request, byte[] auxiliaryIn) : base(AdminMethod.EcReadMdbEvents50, callerSecurityContext, mdbGuid, AdminRpc.ExpectedDatabaseState.AnyAttachedState, mdbVersionGuid, auxiliaryIn)
			{
				this.request = request;
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x000055D9 File Offset: 0x000037D9
			public byte[] Response
			{
				get
				{
					return this.response;
				}
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x000055E4 File Offset: 0x000037E4
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)54781U);
				}
				else
				{
					EventHistory.ReadEventsFlags readEventsFlags;
					long num;
					uint num2;
					uint num3;
					Restriction restriction;
					errorCode = AdminRpcParseFormat.ParseReadEventsRequest(context, this.request, out readEventsFlags, out num, out num2, out num3, out restriction);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)42493U);
					}
					else
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(100);
							stringBuilder.Append("INPUT  ReadMdbEvents:");
							stringBuilder.Append(" callerSecurityContext:[");
							stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
							stringBuilder.Append("] mdbGuid:[");
							stringBuilder.Append(base.MdbGuid.Value);
							stringBuilder.Append("] mdbVersionGuid:[");
							stringBuilder.Append(base.MdbVersionGuid);
							stringBuilder.Append("] readFlags:[");
							stringBuilder.Append(readEventsFlags);
							stringBuilder.Append("] startCounter:[");
							stringBuilder.Append(num);
							stringBuilder.Append("] eventsWant:[");
							stringBuilder.Append(num2);
							stringBuilder.Append("] eventsToCheck:[");
							stringBuilder.Append(num3);
							if (restriction != null)
							{
								stringBuilder.Append("] restriction:[");
								stringBuilder.Append(restriction);
							}
							stringBuilder.Append("]");
							ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
						List<EventEntry> list = null;
						long num4 = 0L;
						if (-1L == num)
						{
							EventEntry item;
							errorCode = eventHistory.ReadLastEvent(context, out item);
							if (errorCode == ErrorCode.NoError)
							{
								list = new List<EventEntry>(1);
								list.Add(item);
							}
						}
						else
						{
							if (num < 0L)
							{
								return ErrorCode.CreateRpcFormat((LID)64888U);
							}
							errorCode = eventHistory.ReadEvents(context, num, num2, num3, restriction, readEventsFlags, out list, out num4);
						}
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)58877U);
						}
						else
						{
							if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder2 = new StringBuilder(100);
								stringBuilder2.Append("OUTPUT  ReadMdbEvents:");
								stringBuilder2.Append(" mdbVersionGuid:[");
								stringBuilder2.Append(base.MdbVersionGuid);
								stringBuilder2.Append("] events.Count:[");
								stringBuilder2.Append((list == null) ? 0 : list.Count);
								stringBuilder2.Append("] endCounter:[");
								stringBuilder2.Append(num4);
								stringBuilder2.Append("]");
								ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder2.ToString());
							}
							errorCode = AdminRpcParseFormat.FormatReadEventsResponse(0, num4, list, out this.response);
							if (errorCode != ErrorCode.NoError)
							{
								errorCode = errorCode.Propagate((LID)34301U);
							}
						}
					}
				}
				return errorCode;
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x000058F8 File Offset: 0x00003AF8
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (!(errorCode != ErrorCode.NoError) && (this.request == null || this.request.Length < 4))
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)38397U);
				}
				return errorCode;
			}

			// Token: 0x040000B1 RID: 177
			private byte[] request;

			// Token: 0x040000B2 RID: 178
			private byte[] response;
		}

		// Token: 0x02000027 RID: 39
		internal class AdminRpcWriteMdbEvents : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x0000593E File Offset: 0x00003B3E
			public AdminRpcWriteMdbEvents(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, byte[] request, byte[] auxiliaryIn) : base(AdminMethod.EcWriteMdbEvents50, callerSecurityContext, mdbGuid, mdbVersionGuid, auxiliaryIn)
			{
				this.request = request;
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00005955 File Offset: 0x00003B55
			public byte[] Response
			{
				get
				{
					return this.response;
				}
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00005960 File Offset: 0x00003B60
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)50685U);
				}
				else
				{
					int value;
					List<EventEntry> list;
					errorCode = AdminRpcParseFormat.ParseWriteEventsRequest(this.request, out value, out list);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)45397U);
					}
					else
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(100);
							stringBuilder.Append("INPUT  WriteMdbEvents:");
							stringBuilder.Append(" callerSecurityContext:[");
							stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
							stringBuilder.Append("] mdbGuid:[");
							stringBuilder.Append(base.MdbGuid.Value);
							stringBuilder.Append("] mdbVersionGuid:[");
							stringBuilder.Append(base.MdbVersionGuid);
							stringBuilder.Append("] writeFlags:[");
							stringBuilder.Append(value);
							stringBuilder.Append("] events.Count:[");
							stringBuilder.Append((list == null) ? 0 : list.Count);
							stringBuilder.Append("]");
							ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
						List<long> list2;
						errorCode = eventHistory.WriteEvents(context, list, out list2);
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)61781U);
						}
						else
						{
							if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder2 = new StringBuilder(100);
								stringBuilder2.Append("OUTPUT  WriteMdbEvents:");
								stringBuilder2.Append(" mdbVersionGuid:[");
								stringBuilder2.Append(base.MdbVersionGuid);
								stringBuilder2.Append("] eventCounters.Count:[");
								stringBuilder2.Append((list2 == null) ? 0 : list2.Count);
								stringBuilder2.Append("]");
								ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder2.ToString());
							}
							errorCode = AdminRpcParseFormat.FormatWriteEventsResponse(list2, out this.response);
							if (errorCode != ErrorCode.NoError)
							{
								errorCode = errorCode.Propagate((LID)37205U);
							}
						}
					}
				}
				return errorCode;
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00005BA4 File Offset: 0x00003DA4
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (!(errorCode != ErrorCode.NoError) && (this.request == null || this.request.Length < 4))
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)41301U);
				}
				return errorCode;
			}

			// Token: 0x040000B3 RID: 179
			private byte[] request;

			// Token: 0x040000B4 RID: 180
			private byte[] response;
		}

		// Token: 0x02000028 RID: 40
		internal class AdminRpcDeleteMdbWatermarksForConsumer : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000DD RID: 221 RVA: 0x00005BEA File Offset: 0x00003DEA
			public AdminRpcDeleteMdbWatermarksForConsumer(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, byte[] auxiliaryIn) : base(AdminMethod.EcDeleteMdbWatermarksForConsumer50, callerSecurityContext, mdbGuid, mdbVersionGuid, auxiliaryIn)
			{
				this.mailboxDsGuid = mailboxDsGuid;
				this.consumerGuid = consumerGuid;
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00005C09 File Offset: 0x00003E09
			public uint DeletedCount
			{
				get
				{
					return this.deletedCount;
				}
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00005C14 File Offset: 0x00003E14
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)57685U);
				}
				else
				{
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("INPUT  DeleteMdbWatermarksForConsumer:");
						stringBuilder.Append(" callerSecurityContext:[");
						stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
						stringBuilder.Append("] mdbGuid:[");
						stringBuilder.Append(base.MdbGuid.Value);
						stringBuilder.Append("] mdbVersionGuid:[");
						stringBuilder.Append(base.MdbVersionGuid);
						stringBuilder.Append("] consumerGuid:[");
						stringBuilder.Append(this.consumerGuid);
						if (this.mailboxDsGuid != null)
						{
							stringBuilder.Append("] mailboxGuid:[");
							stringBuilder.Append(this.mailboxDsGuid);
						}
						stringBuilder.Append("]");
						ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
					errorCode = eventHistory.DeleteWatermarksForConsumer(context, this.consumerGuid, this.mailboxDsGuid, out this.deletedCount);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)33109U);
					}
					else if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder2 = new StringBuilder(100);
						stringBuilder2.Append("OUTPUT  DeleteMdbWatermarksForConsumer:");
						stringBuilder2.Append(" mdbVersionGuid:[");
						stringBuilder2.Append(base.MdbVersionGuid);
						stringBuilder2.Append("] deletedCount:[");
						stringBuilder2.Append(this.deletedCount);
						stringBuilder2.Append("]");
						ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder2.ToString());
					}
				}
				return errorCode;
			}

			// Token: 0x040000B5 RID: 181
			private Guid? mailboxDsGuid;

			// Token: 0x040000B6 RID: 182
			private Guid consumerGuid;

			// Token: 0x040000B7 RID: 183
			private uint deletedCount;
		}

		// Token: 0x02000029 RID: 41
		internal class AdminRpcDeleteMdbWatermarksForMailbox : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000E0 RID: 224 RVA: 0x00005E07 File Offset: 0x00004007
			public AdminRpcDeleteMdbWatermarksForMailbox(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, Guid mailboxDsGuid, byte[] auxiliaryIn) : base(AdminMethod.EcDeleteMdbWatermarksForMailbox50, callerSecurityContext, mdbGuid, mdbVersionGuid, auxiliaryIn)
			{
				this.mailboxDsGuid = mailboxDsGuid;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005E1E File Offset: 0x0000401E
			public uint DeletedCount
			{
				get
				{
					return this.deletedCount;
				}
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00005E28 File Offset: 0x00004028
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)48981U);
				}
				else
				{
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("INPUT  DeleteMdbWatermarksForMailbox:");
						stringBuilder.Append(" callerSecurityContext:[");
						stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
						stringBuilder.Append("] mdbGuid:[");
						stringBuilder.Append(base.MdbGuid.Value);
						stringBuilder.Append("] mdbVersionGuid:[");
						stringBuilder.Append(base.MdbVersionGuid);
						stringBuilder.Append("] mailboxGuid:[");
						stringBuilder.Append(this.mailboxDsGuid);
						stringBuilder.Append("]");
						ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
					errorCode = eventHistory.DeleteWatermarksForMailbox(context, this.mailboxDsGuid, out this.deletedCount);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)65365U);
					}
					else if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder2 = new StringBuilder(100);
						stringBuilder2.Append("OUTPUT  DeleteMdbWatermarksForMailbox:");
						stringBuilder2.Append(" mdbVersionGuid:[");
						stringBuilder2.Append(base.MdbVersionGuid);
						stringBuilder2.Append("] deletedCount:[");
						stringBuilder2.Append(this.deletedCount);
						stringBuilder2.Append("]");
						ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder2.ToString());
					}
				}
				return errorCode;
			}

			// Token: 0x040000B8 RID: 184
			private Guid mailboxDsGuid;

			// Token: 0x040000B9 RID: 185
			private uint deletedCount;
		}

		// Token: 0x0200002A RID: 42
		internal class AdminRpcSaveMdbWatermarks : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000E3 RID: 227 RVA: 0x00005FEA File Offset: 0x000041EA
			public AdminRpcSaveMdbWatermarks(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, MDBEVENTWM[] wms, byte[] auxiliaryIn) : base(AdminMethod.EcSaveMdbWatermarks50, callerSecurityContext, mdbGuid, mdbVersionGuid, auxiliaryIn)
			{
				this.wms = wms;
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00006004 File Offset: 0x00004204
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)57173U);
				}
				else
				{
					List<EventWatermark> list;
					errorCode = AdminRpcParseFormat.ParseMDBEVENTWMs(this.wms, out list);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)44885U);
					}
					else
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(100);
							stringBuilder.Append("INPUT  SaveMdbWatermarks:");
							stringBuilder.Append(" callerSecurityContext:[");
							stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
							stringBuilder.Append("] mdbGuid:[");
							stringBuilder.Append(base.MdbGuid.Value);
							stringBuilder.Append("] mdbVersionGuid:[");
							stringBuilder.Append(base.MdbVersionGuid);
							stringBuilder.Append("] watermarks.Count:[");
							stringBuilder.Append((list == null) ? 0 : list.Count);
							stringBuilder.Append("]");
							ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
						errorCode = eventHistory.SaveWatermarks(context, list);
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)61269U);
						}
					}
				}
				return errorCode;
			}

			// Token: 0x040000BA RID: 186
			private MDBEVENTWM[] wms;
		}

		// Token: 0x0200002B RID: 43
		internal class AdminRpcGetMdbWatermarksForConsumer : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000E5 RID: 229 RVA: 0x00006176 File Offset: 0x00004376
			public AdminRpcGetMdbWatermarksForConsumer(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, byte[] auxiliaryIn) : base(AdminMethod.EcGetMdbWatermarksForConsumer50, callerSecurityContext, mdbGuid, mdbVersionGuid, auxiliaryIn)
			{
				this.mailboxDsGuid = mailboxDsGuid;
				this.consumerGuid = consumerGuid;
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006195 File Offset: 0x00004395
			public MDBEVENTWM[] Wms
			{
				get
				{
					return this.wms;
				}
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x000061A0 File Offset: 0x000043A0
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)36693U);
				}
				else
				{
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("INPUT  GetMdbWatermarksForConsumer:");
						stringBuilder.Append(" callerSecurityContext:[");
						stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
						stringBuilder.Append("] mdbGuid:[");
						stringBuilder.Append(base.MdbGuid.Value);
						stringBuilder.Append("] mdbVersionGuid:[");
						stringBuilder.Append(base.MdbVersionGuid);
						stringBuilder.Append("] consumerGuid:[");
						stringBuilder.Append(this.consumerGuid);
						if (this.mailboxDsGuid != null)
						{
							stringBuilder.Append("] mailboxGuid:[");
							stringBuilder.Append(this.mailboxDsGuid);
						}
						stringBuilder.Append("]");
						ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
					List<EventWatermark> list;
					errorCode = eventHistory.ReadWatermarksForConsumer(context, this.consumerGuid, this.mailboxDsGuid, out list);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)53077U);
					}
					else
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder2 = new StringBuilder(100);
							stringBuilder2.Append("OUTPUT  GetMdbWatermarksForConsumer:");
							stringBuilder2.Append(" mdbVersionGuid:[");
							stringBuilder2.Append(base.MdbVersionGuid);
							stringBuilder2.Append("] watermarks.Count:[");
							stringBuilder2.Append((list == null) ? 0 : list.Count);
							stringBuilder2.Append("]");
							ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder2.ToString());
						}
						errorCode = AdminRpcParseFormat.FormatMDBEVENTWMs(list, out this.wms);
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)46933U);
						}
					}
				}
				return errorCode;
			}

			// Token: 0x040000BB RID: 187
			private MDBEVENTWM[] wms;

			// Token: 0x040000BC RID: 188
			private Guid? mailboxDsGuid;

			// Token: 0x040000BD RID: 189
			private Guid consumerGuid;
		}

		// Token: 0x0200002C RID: 44
		internal class AdminRpcGetMdbWatermarksForMailbox : AdminRpcServer.AdminRpcEventsBase
		{
			// Token: 0x060000E8 RID: 232 RVA: 0x000063CC File Offset: 0x000045CC
			public AdminRpcGetMdbWatermarksForMailbox(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mdbVersionGuid, Guid mailboxDsGuid, byte[] auxiliaryIn) : base(AdminMethod.EcGetMdbWatermarksForMailbox50, callerSecurityContext, mdbGuid, mdbVersionGuid, auxiliaryIn)
			{
				this.mailboxDsGuid = mailboxDsGuid;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x000063E3 File Offset: 0x000045E3
			public MDBEVENTWM[] Wms
			{
				get
				{
					return this.wms;
				}
			}

			// Token: 0x060000EA RID: 234 RVA: 0x000063EC File Offset: 0x000045EC
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = base.CheckMdbVersion();
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)63317U);
				}
				else
				{
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("INPUT  GetMdbWatermarksForMailbox:");
						stringBuilder.Append(" callerSecurityContext:[");
						stringBuilder.Append((base.ClientSecurityContext == null) ? "null" : base.ClientSecurityContext.ToString());
						stringBuilder.Append("] mdbGuid:[");
						stringBuilder.Append(base.MdbGuid.Value);
						stringBuilder.Append("] mdbVersionGuid:[");
						stringBuilder.Append(base.MdbVersionGuid);
						stringBuilder.Append("] mailboxGuid:[");
						stringBuilder.Append(this.mailboxDsGuid);
						stringBuilder.Append("]");
						ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					EventHistory eventHistory = EventHistory.GetEventHistory(base.Database);
					List<EventWatermark> list;
					errorCode = eventHistory.ReadWatermarksForMailbox(context, this.mailboxDsGuid, out list);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)38741U);
					}
					else
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder2 = new StringBuilder(100);
							stringBuilder2.Append("OUTPUT  GetMdbWatermarksForMailbox:");
							stringBuilder2.Append(" mdbVersionGuid:[");
							stringBuilder2.Append(base.MdbVersionGuid);
							stringBuilder2.Append("] watermarks.Count:[");
							stringBuilder2.Append((list == null) ? 0 : list.Count);
							stringBuilder2.Append("]");
							ExTraceGlobals.AdminRpcTracer.TraceDebug(0L, stringBuilder2.ToString());
						}
						errorCode = AdminRpcParseFormat.FormatMDBEVENTWMs(list, out this.wms);
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)55125U);
						}
					}
				}
				return errorCode;
			}

			// Token: 0x040000BE RID: 190
			private MDBEVENTWM[] wms;

			// Token: 0x040000BF RID: 191
			private Guid mailboxDsGuid;
		}

		// Token: 0x0200002D RID: 45
		internal abstract class MailboxAdminRpc : AdminRpc
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000EB RID: 235 RVA: 0x000065E7 File Offset: 0x000047E7
			internal Guid MailboxGuid
			{
				get
				{
					return this.mailboxGuid;
				}
			}

			// Token: 0x060000EC RID: 236 RVA: 0x000065EF File Offset: 0x000047EF
			internal MailboxAdminRpc(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, bool includeSoftDeleted, byte[] auxiliaryIn) : base(methodId, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
				this.mailboxGuid = mailboxGuid;
				this.includeSoftDeleted = includeSoftDeleted;
			}

			// Token: 0x060000ED RID: 237 RVA: 0x00006614 File Offset: 0x00004814
			protected override ErrorCode EcInitializeResources(MapiContext context)
			{
				ErrorCode first = base.EcInitializeResources(context);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)17512U);
				}
				((AdminExecutionDiagnostics)context.Diagnostics).AdminExMonLogger.SetMailboxGuid(this.MailboxGuid);
				return ErrorCode.NoError;
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00006668 File Offset: 0x00004868
			protected override void CleanupResources(MapiContext context)
			{
				base.CleanupResources(context);
			}

			// Token: 0x060000EF RID: 239 RVA: 0x00006674 File Offset: 0x00004874
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				bool commit = false;
				try
				{
					context.InitializeMailboxExclusiveOperation(this.MailboxGuid, ExecutionDiagnostics.OperationSource.AdminRpc, MapiContext.MailboxLockTimeout);
					ErrorCode errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
					if (errorCode != ErrorCode.NoError)
					{
						if (errorCode == ErrorCodeValue.NotFound)
						{
							return ErrorCode.CreateUnknownMailbox((LID)63960U);
						}
						return errorCode.Propagate((LID)35288U);
					}
					else
					{
						if (!this.includeSoftDeleted && context.LockedMailboxState.IsSoftDeleted)
						{
							return ErrorCode.CreateUnknownMailbox((LID)17520U);
						}
						errorCode = this.EcExecuteMailboxRpcOperation(context);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)39164U);
						}
						commit = true;
					}
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
				return ErrorCode.NoError;
			}

			// Token: 0x060000F0 RID: 240
			protected abstract ErrorCode EcExecuteMailboxRpcOperation(MapiContext context);

			// Token: 0x040000C0 RID: 192
			private readonly Guid mailboxGuid;

			// Token: 0x040000C1 RID: 193
			private readonly bool includeSoftDeleted;
		}

		// Token: 0x0200002E RID: 46
		internal class AdminRpcClearAbsentInDsOnMailbox : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x00006760 File Offset: 0x00004960
			internal AdminRpcClearAbsentInDsOnMailbox(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn) : base(AdminMethod.EcClearAbsentInDsFlagOnMailbox50, callerSecurityContext, mdbGuid, mailboxGuid, true, auxiliaryIn)
			{
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x000067A4 File Offset: 0x000049A4
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				if (base.Database.IsRecovery)
				{
					return ErrorCode.CreateInvalidParameter((LID)39624U);
				}
				if (context.LockedMailboxState.IsSoftDeleted)
				{
					return ErrorCode.CreateInvalidParameter((LID)54136U);
				}
				MailboxInfo directoryMailboxInfo = null;
				TenantHint tenantHint = context.LockedMailboxState.TenantHint;
				ErrorCode first = context.PulseMailboxOperation(delegate()
				{
					directoryMailboxInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetMailboxInfo(context, tenantHint, this.MailboxGuid, GetMailboxInfoFlags.None);
				});
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)55548U);
				}
				if (directoryMailboxInfo == null)
				{
					return ErrorCode.CreateNotFound((LID)43260U);
				}
				using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
				{
					Microsoft.Exchange.Server.Storage.MapiDisp.MailboxCleanup.ReconnectMailboxToADObject(context, mailbox, directoryMailboxInfo);
				}
				return ErrorCode.NoError;
			}
		}

		// Token: 0x0200002F RID: 47
		internal class AdminRpcPurgeCachedMailboxObject : AdminRpc
		{
			// Token: 0x060000F3 RID: 243 RVA: 0x000068BC File Offset: 0x00004ABC
			internal AdminRpcPurgeCachedMailboxObject(ClientSecurityContext callerSecurityContext, Guid mailboxGuid, byte[] auxiliaryIn) : base(AdminMethod.EcPurgeCachedMailboxObject50, callerSecurityContext, auxiliaryIn)
			{
				this.mailboxGuid = mailboxGuid;
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x000068CF File Offset: 0x00004ACF
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshMailboxInfo(context, this.mailboxGuid);
				return ErrorCode.NoError;
			}

			// Token: 0x040000C2 RID: 194
			private readonly Guid mailboxGuid;
		}

		// Token: 0x02000030 RID: 48
		internal class AdminRpcDeletePrivateMailbox : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x000068E7 File Offset: 0x00004AE7
			private bool IsMailboxMoved
			{
				get
				{
					return (this.flags & DeleteMailboxFlags.MailboxMoved) == DeleteMailboxFlags.MailboxMoved;
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x000068F4 File Offset: 0x00004AF4
			private bool IsHardDelete
			{
				get
				{
					return (this.flags & DeleteMailboxFlags.HardDelete) == DeleteMailboxFlags.HardDelete;
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006901 File Offset: 0x00004B01
			private bool IsMoveFailed
			{
				get
				{
					return (this.flags & DeleteMailboxFlags.MoveFailed) == DeleteMailboxFlags.MoveFailed;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000690E File Offset: 0x00004B0E
			private bool IsSoftDelete
			{
				get
				{
					return (this.flags & DeleteMailboxFlags.SoftDelete) == DeleteMailboxFlags.SoftDelete;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000691B File Offset: 0x00004B1B
			private bool IsRemoveSoftDeleted
			{
				get
				{
					return (this.flags & DeleteMailboxFlags.RemoveSoftDeleted) == DeleteMailboxFlags.RemoveSoftDeleted;
				}
			}

			// Token: 0x060000FA RID: 250 RVA: 0x0000692A File Offset: 0x00004B2A
			internal AdminRpcDeletePrivateMailbox(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, DeleteMailboxFlags flags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminDeletePrivateMailbox50, callerSecurityContext, mdbGuid, mailboxGuid, true, auxiliaryIn)
			{
				this.flags = flags;
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00006944 File Offset: 0x00004B44
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				if ((this.flags & ~(DeleteMailboxFlags.MailboxMoved | DeleteMailboxFlags.HardDelete | DeleteMailboxFlags.MoveFailed | DeleteMailboxFlags.SoftDelete | DeleteMailboxFlags.RemoveSoftDeleted)) != DeleteMailboxFlags.None)
				{
					return ErrorCode.CreateInvalidParameter((LID)41848U);
				}
				if ((this.IsHardDelete && this.IsSoftDelete) || (!this.IsHardDelete && !this.IsSoftDelete))
				{
					return ErrorCode.CreateInvalidParameter((LID)60056U);
				}
				if (this.IsSoftDelete && this.IsRemoveSoftDeleted)
				{
					return ErrorCode.CreateInvalidParameter((LID)17584U);
				}
				if (!this.IsMailboxMoved && this.IsMoveFailed)
				{
					return ErrorCode.CreateInvalidParameter((LID)33656U);
				}
				if (this.IsSoftDelete && this.IsMoveFailed)
				{
					return ErrorCode.CreateInvalidParameter((LID)58232U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00006A04 File Offset: 0x00004C04
			protected override ErrorCode EcInitializeResources(MapiContext context)
			{
				if (base.MdbGuid == null)
				{
					return ErrorCode.CreateInvalidParameter((LID)49744U);
				}
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.DeregisterAllSessionssOfMailbox(context, base.MdbGuid.Value, base.MailboxGuid);
				ErrorCode errorCode = base.EcInitializeResources(context);
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)17600U);
				}
				return errorCode;
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00006A74 File Offset: 0x00004C74
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				if (DefaultSettings.Get.UserInformationIsEnabled && UserInfoUpgrader.IsReady(context, context.Database) && context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql && this.IsSoftDelete)
				{
					UserInformation.TryMarkAsSoftDeleted(context, base.MailboxGuid);
				}
				return base.EcExecuteRpc(context);
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00006ACC File Offset: 0x00004CCC
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				ErrorCode result = ErrorCode.NoError;
				MailboxNotificationEvent mailboxNotificationEvent = null;
				if (!context.LockedMailboxState.IsAccessible)
				{
					result = ErrorCode.CreateUnexpectedMailboxState((LID)50040U);
				}
				else if (!context.LockedMailboxState.IsSoftDeleted && this.IsRemoveSoftDeleted)
				{
					result = ErrorCode.CreateUnexpectedMailboxState((LID)48248U);
				}
				else if (context.LockedMailboxState.IsSoftDeleted && !this.IsRemoveSoftDeleted)
				{
					result = ErrorCode.CreateUnexpectedMailboxState((LID)64632U);
				}
				else if (context.LockedMailboxState.IsSoftDeleted && this.IsSoftDelete)
				{
					result = ErrorCode.CreateUnexpectedMailboxState((LID)40056U);
				}
				else
				{
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						if (this.IsMailboxMoved)
						{
							if (this.IsMoveFailed)
							{
								mailboxNotificationEvent = NotificationEvents.CreateMailboxMoveFailedNotificationEvent(context, mailbox, false);
							}
							else
							{
								MailboxQuarantineProvider.Instance.UnquarantineMailbox(base.MdbGuid.Value, base.MailboxGuid);
								mailboxNotificationEvent = NotificationEvents.CreateMailboxMoveSucceededNotificationEvent(context, mailbox, true);
							}
						}
						else if (this.IsHardDelete)
						{
							mailboxNotificationEvent = NotificationEvents.CreateMailboxDeletedNotificationEvent(context, mailbox);
						}
						if (this.IsSoftDelete)
						{
							mailbox.SoftDelete(context);
						}
						else if (this.IsHardDelete)
						{
							mailbox.HardDelete(context);
						}
						if (mailboxNotificationEvent != null)
						{
							context.RiseNotificationEvent(mailboxNotificationEvent);
						}
					}
				}
				return result;
			}

			// Token: 0x040000C3 RID: 195
			private DeleteMailboxFlags flags;
		}

		// Token: 0x02000031 RID: 49
		internal class AdminRpcGetMailboxSecurityDescriptor : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x060000FF RID: 255 RVA: 0x00006C2C File Offset: 0x00004E2C
			internal AdminRpcGetMailboxSecurityDescriptor(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn) : base(AdminMethod.EcGetMailboxSecurityDescriptor50, callerSecurityContext, mdbGuid, mailboxGuid, false, auxiliaryIn)
			{
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000100 RID: 256 RVA: 0x00006C3C File Offset: 0x00004E3C
			public byte[] NTSecurityDescriptor
			{
				get
				{
					return this.ntSecurityDescriptor;
				}
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00006D74 File Offset: 0x00004F74
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				MailboxInfo mailboxInfo = null;
				SecurityDescriptor databaseOrServerADSecurityDescriptor = null;
				TenantHint tenantHint = context.LockedMailboxState.TenantHint;
				errorCode = context.PulseMailboxOperation(delegate()
				{
					Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshMailboxInfo(context, this.MailboxGuid);
					mailboxInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetMailboxInfo(context, tenantHint, this.MailboxGuid, GetMailboxInfoFlags.None);
					if (mailboxInfo == null)
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.AdminRpcTracer.TraceError<Guid>(0L, "mailbox info for mailbox {0} was not found", this.MailboxGuid);
						}
						throw new StoreException((LID)41624U, ErrorCodeValue.NotFound);
					}
					if (mailboxInfo.IsSystemAttendantRecipient)
					{
						databaseOrServerADSecurityDescriptor = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(context).NTSecurityDescriptor;
						return;
					}
					DatabaseInfo databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(context, mailboxInfo.MdbGuid);
					if (databaseInfo == null)
					{
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.AdminRpcTracer.TraceError<Guid>(0L, "database info for database {0} was not found", mailboxInfo.MdbGuid);
						}
						throw new StoreException((LID)58008U, ErrorCodeValue.NotFound);
					}
					databaseOrServerADSecurityDescriptor = databaseInfo.NTSecurityDescriptor;
				});
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.AdminRpcTracer.TraceError<Guid, ErrorCode>(0L, "PulseMailboxOperation for mailbox {0} failed with {1}", base.MailboxGuid, errorCode);
					}
					return errorCode.Propagate((LID)59644U);
				}
				SecurityDescriptor exchangeSecurityDescriptor = mailboxInfo.ExchangeSecurityDescriptor;
				this.ntSecurityDescriptor = Mailbox.CreateMailboxSecurityDescriptorBlob(databaseOrServerADSecurityDescriptor, exchangeSecurityDescriptor);
				if (this.ntSecurityDescriptor == null)
				{
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.AdminRpcTracer.TraceError(0L, "computing mailbox SD has failed");
					}
					return ErrorCode.CreateCallFailed((LID)33432U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x040000C4 RID: 196
			private byte[] ntSecurityDescriptor;
		}

		// Token: 0x02000032 RID: 50
		internal class AdminGetMailboxSignature : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x06000102 RID: 258 RVA: 0x00006E74 File Offset: 0x00005074
			internal AdminGetMailboxSignature(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, MailboxSignatureFlags flags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetMailboxSignature50, callerSecurityContext, mdbGuid, mailboxGuid, false, auxiliaryIn)
			{
				this.flags = flags;
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00006E8C File Offset: 0x0000508C
			public byte[] Result
			{
				get
				{
					return this.mailboxSignature;
				}
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00006E94 File Offset: 0x00005094
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				MailboxSignatureSectionType mailboxSignatureSectionType = MailboxSignatureSectionType.None;
				MailboxSignatureFlags mailboxSignatureFlags = this.flags;
				if ((mailboxSignatureFlags & MailboxSignatureFlags.GetMailboxShape) == MailboxSignatureFlags.GetMailboxShape)
				{
					mailboxSignatureSectionType |= MailboxSignatureSectionType.MailboxShape;
					mailboxSignatureFlags &= ~MailboxSignatureFlags.GetMailboxShape;
				}
				bool flag = false;
				if (MailboxSignatureFlags.GetLegacy == mailboxSignatureFlags)
				{
					mailboxSignatureSectionType |= (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.TenantHint);
					flag = true;
				}
				else if (MailboxSignatureFlags.GetMailboxSignature == mailboxSignatureFlags)
				{
					mailboxSignatureSectionType |= (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata | MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping | MailboxSignatureSectionType.TenantHint);
					flag = true;
				}
				else if ((MailboxSignatureFlags.GetNamedPropertyMapping | MailboxSignatureFlags.GetReplidGuidMapping) == mailboxSignatureFlags)
				{
					mailboxSignatureSectionType |= (MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping);
					flag = true;
				}
				else if (MailboxSignatureFlags.GetMappingMetadata == mailboxSignatureFlags)
				{
					mailboxSignatureSectionType |= MailboxSignatureSectionType.MappingMetadata;
				}
				uint num;
				bool assertCondition;
				if (flag && Mailbox.GetMailboxTypeVersion(context, context.LockedMailboxState.MailboxType, context.LockedMailboxState.MailboxTypeDetail, out num, out assertCondition))
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Mailbox exists and isAllowed==false?");
					mailboxSignatureSectionType |= MailboxSignatureSectionType.MailboxTypeVersion;
				}
				if (UserInfoUpgrader.IsReady(context, context.Database) && DefaultSettings.Get.UserInformationIsEnabled && context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
				{
					mailboxSignatureSectionType |= MailboxSignatureSectionType.UserInformation;
				}
				using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
				{
					MailboxSignature.Serialize(context, mailbox, mailboxSignatureSectionType, out this.mailboxSignature);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00006FA4 File Offset: 0x000051A4
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode first = base.EcValidateArguments(context);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)36444U);
				}
				MailboxSignatureFlags mailboxSignatureFlags = this.flags;
				if ((mailboxSignatureFlags & MailboxSignatureFlags.GetMailboxShape) == MailboxSignatureFlags.GetMailboxShape)
				{
					mailboxSignatureFlags &= ~MailboxSignatureFlags.GetMailboxShape;
					if (mailboxSignatureFlags != MailboxSignatureFlags.GetLegacy && mailboxSignatureFlags != MailboxSignatureFlags.GetMailboxSignature)
					{
						return ErrorCode.CreateInvalidParameter((LID)57436U);
					}
				}
				if (mailboxSignatureFlags != MailboxSignatureFlags.GetLegacy && mailboxSignatureFlags != MailboxSignatureFlags.GetMailboxSignature && mailboxSignatureFlags != MailboxSignatureFlags.GetMappingMetadata && mailboxSignatureFlags != (MailboxSignatureFlags.GetNamedPropertyMapping | MailboxSignatureFlags.GetReplidGuidMapping))
				{
					return ErrorCode.CreateInvalidParameter((LID)64584U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00007030 File Offset: 0x00005230
			protected override ErrorCode EcInitializeResources(MapiContext context)
			{
				ErrorCode errorCode = base.EcInitializeResources(context);
				if (errorCode == ErrorCodeValue.UnknownMailbox)
				{
					errorCode = ErrorCode.CreateNotFound((LID)48200U);
				}
				return errorCode;
			}

			// Token: 0x040000C5 RID: 197
			private MailboxSignatureFlags flags;

			// Token: 0x040000C6 RID: 198
			private byte[] mailboxSignature;
		}

		// Token: 0x02000033 RID: 51
		internal class AdminRpcSetMailboxBasicInfo : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x06000107 RID: 263 RVA: 0x00007063 File Offset: 0x00005263
			internal AdminRpcSetMailboxBasicInfo(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] mailboxSignature, byte[] auxiliaryIn) : base(AdminMethod.EcAdminSetMailboxBasicInfo50, callerSecurityContext, mdbGuid, mailboxGuid, true, auxiliaryIn)
			{
				this.mailboxSignature = mailboxSignature;
			}

			// Token: 0x06000108 RID: 264 RVA: 0x0000707B File Offset: 0x0000527B
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				return ErrorCode.NoError;
			}

			// Token: 0x06000109 RID: 265 RVA: 0x00007084 File Offset: 0x00005284
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				Guid mailboxInstanceGuid;
				Guid defaultFoldersReplGuid;
				ExchangeId[] fidList;
				Guid mappingSignatureGuid;
				Guid localIdGuid;
				ulong nextIdCounter;
				uint? reservedIdCounterRange;
				ulong nextCnCounter;
				uint? reservedCnCounterRange;
				TenantHint tenantHint;
				Dictionary<ushort, StoreNamedPropInfo> numberToNameMap;
				Dictionary<ushort, Guid> replidGuidMap;
				PropertyBlob.BlobReader mailboxShapePropertyBlobReader;
				Mailbox.MailboxTypeVersion mailboxTypeVersion;
				PartitionInformation partitionInformation;
				PropertyBlob.BlobReader userInformationPropertyBlobReader;
				MailboxSignatureSectionType mailboxSignatureSectionType;
				MailboxSignature.Parse(this.mailboxSignature, base.MdbGuid.Value, base.MailboxGuid, out mailboxInstanceGuid, out defaultFoldersReplGuid, out fidList, out mappingSignatureGuid, out localIdGuid, out nextIdCounter, out reservedIdCounterRange, out nextCnCounter, out reservedCnCounterRange, out tenantHint, out numberToNameMap, out replidGuidMap, out mailboxShapePropertyBlobReader, out mailboxTypeVersion, out partitionInformation, out userInformationPropertyBlobReader, out mailboxSignatureSectionType);
				if ((short)(mailboxSignatureSectionType & MailboxSignatureSectionType.UserInformation) == 256)
				{
					errorCode = this.ProcessUserInformation(context, userInformationPropertyBlobReader);
					if (errorCode != ErrorCode.NoError)
					{
						return errorCode.Propagate((LID)49996U);
					}
				}
				mailboxSignatureSectionType &= ~(MailboxSignatureSectionType.MailboxTypeVersion | MailboxSignatureSectionType.PartitionInformation | MailboxSignatureSectionType.UserInformation);
				context.InitializeMailboxExclusiveOperation(base.MailboxGuid, ExecutionDiagnostics.OperationSource.AdminRpc, MapiContext.MailboxLockTimeout);
				errorCode = AdminRpcPermissionChecks.EcDefaultCheck(context, base.DatabaseInfo);
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)50023U);
				}
				if ((short)(mailboxSignatureSectionType & (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.TenantHint)) == 17 || (short)(mailboxSignatureSectionType & (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata | MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping | MailboxSignatureSectionType.TenantHint)) == 31)
				{
					errorCode = this.CreateDestinationMailbox(context, fidList, tenantHint, mailboxInstanceGuid, localIdGuid, mappingSignatureGuid, nextIdCounter, reservedIdCounterRange, nextCnCounter, reservedCnCounterRange, numberToNameMap, replidGuidMap, defaultFoldersReplGuid, (short)(mailboxSignatureSectionType & (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata | MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping | MailboxSignatureSectionType.TenantHint)) == 31, mailboxTypeVersion, partitionInformation);
					if (errorCode != ErrorCode.NoError)
					{
						return errorCode.Propagate((LID)32860U);
					}
					errorCode = this.CheckMailboxVersionAndUpgrade(context);
					if (errorCode != ErrorCode.NoError)
					{
						return errorCode.Propagate((LID)54876U);
					}
					if ((short)(mailboxSignatureSectionType & MailboxSignatureSectionType.MailboxShape) == 32 && mailboxShapePropertyBlobReader.PropertyCount > 0)
					{
						errorCode = this.ProcessMailboxShape(context, mailboxShapePropertyBlobReader);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)49244U);
						}
					}
				}
				else if ((short)(mailboxSignatureSectionType & (MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping)) == 12)
				{
					errorCode = this.ProcessMailboxSignatureMappings(context, numberToNameMap, replidGuidMap, mailboxTypeVersion);
					if (errorCode != ErrorCode.NoError)
					{
						return errorCode.Propagate((LID)48732U);
					}
				}
				else if ((short)(mailboxSignatureSectionType & MailboxSignatureSectionType.MappingMetadata) == 2)
				{
					errorCode = this.ProcessMailboxSignatureMappingMetadata(context, localIdGuid, mappingSignatureGuid, nextIdCounter, reservedIdCounterRange.Value, nextCnCounter, reservedCnCounterRange.Value);
					if (errorCode != ErrorCode.NoError)
					{
						return errorCode.Propagate((LID)65116U);
					}
				}
				return errorCode;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x000072BC File Offset: 0x000054BC
			private ErrorCode CreateDestinationMailbox(MapiContext context, ExchangeId[] fidList, TenantHint tenantHint, Guid mailboxInstanceGuid, Guid localIdGuid, Guid mappingSignatureGuid, ulong nextIdCounter, uint? reservedIdCounterRange, ulong nextCnCounter, uint? reservedCnCounterRange, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap, Dictionary<ushort, Guid> replidGuidMap, Guid defaultFoldersReplGuid, bool preserveMailboxSignature, Mailbox.MailboxTypeVersion mailboxTypeVersion, PartitionInformation partitionInformation)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				MailboxInfo mailboxInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetMailboxInfo(context, tenantHint, base.MailboxGuid, GetMailboxInfoFlags.IgnoreHomeMdb);
				AddressInfo addressInfoByMailboxGuid = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByMailboxGuid(context, tenantHint, base.MailboxGuid, GetAddressInfoFlags.None);
				DatabaseInfo databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(context, base.MdbGuid.Value);
				if (!preserveMailboxSignature)
				{
					localIdGuid = Guid.NewGuid();
					mappingSignatureGuid = Guid.NewGuid();
					nextIdCounter = Mailbox.GetFirstAvailableIdGlobcount(mailboxInfo);
					nextCnCounter = 1UL;
					reservedIdCounterRange = null;
					reservedCnCounterRange = null;
					numberToNameMap = null;
					replidGuidMap = null;
				}
				bool flag = false;
				MailboxCreation mailboxCreation;
				if (partitionInformation != null)
				{
					flag = ((partitionInformation.Flags & PartitionInformation.ControlFlags.CreateNewPartition) == PartitionInformation.ControlFlags.CreateNewPartition);
					mailboxCreation = MailboxCreation.Allow(new Guid?(partitionInformation.PartitionGuid), flag);
				}
				else
				{
					mailboxCreation = MailboxCreation.Allow(null);
				}
				errorCode = context.StartMailboxOperation(mailboxCreation, true, false);
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)58428U);
				}
				bool commit = false;
				try
				{
					Mailbox.ValidateMailboxTypeVersion(context, mailboxInfo.Type, mailboxInfo.TypeDetail, mailboxTypeVersion);
					if (flag)
					{
						while (!context.LockedMailboxState.IsNewMailboxPartition)
						{
							Guid newUnifiedMailboxGuid = Guid.NewGuid();
							try
							{
								context.LockedMailboxState.AddReference();
								Mailbox.MakeRoomForNewPartition(context, context.LockedMailboxState.UnifiedState.UnifiedMailboxGuid, newUnifiedMailboxGuid);
							}
							finally
							{
								context.LockedMailboxState.ReleaseReference();
							}
							context.Commit();
							MailboxState mailboxState = MailboxStateCache.Get(context, context.LockedMailboxState.MailboxNumber);
							MailboxStateCache.MakeRoomForNewPartition(context, mailboxState, newUnifiedMailboxGuid);
							errorCode = context.PulseMailboxOperation(mailboxCreation, true, null);
							if (errorCode != ErrorCode.NoError)
							{
								return errorCode.Propagate((LID)54524U);
							}
						}
					}
					while (!context.LockedMailboxState.IsNew)
					{
						using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
						{
							mailbox.MakeRoomForNewMailbox(context);
							context.Commit();
							MailboxState mailboxState2 = MailboxStateCache.Get(context, context.LockedMailboxState.MailboxNumber);
							MailboxStateCache.MakeRoomForNewMailbox(mailboxState2);
						}
						errorCode = context.PulseMailboxOperation(mailboxCreation, true, null);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)59864U);
						}
					}
					context.LockedMailboxState.MailboxInstanceGuid = mailboxInstanceGuid;
					context.LockedMailboxState.TenantHint = tenantHint;
					if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MailboxSignatureTracer.TraceDebug(58440L, "Database {0} : Mailbox {1} : {2} : Create the destination mailbox preserving mailbox signature: {3}.", new object[]
						{
							context.Database.MdbName,
							context.LockedMailboxState.MailboxNumber,
							mailboxInfo.OwnerDisplayName,
							preserveMailboxSignature
						});
					}
					CrucialFolderId fidc = MapiMailbox.GetFIDC(context, fidList, mailboxInfo.Type != MailboxInfo.MailboxType.Private);
					using (context.GrantMailboxFullRights())
					{
						MapiMailbox.CreateMailbox(context, context.LockedMailboxState, addressInfoByMailboxGuid, mailboxInfo, databaseInfo, tenantHint, mailboxInstanceGuid, localIdGuid, mappingSignatureGuid, nextIdCounter, reservedIdCounterRange, nextCnCounter, reservedCnCounterRange, fidc, numberToNameMap, replidGuidMap, defaultFoldersReplGuid, true);
					}
					context.LockedMailboxState.AddReference();
					try
					{
						InTransitInfo.SetInTransitState(context.LockedMailboxState, InTransitStatus.DestinationOfMove | InTransitStatus.OnlineMove, null);
						LogicalIndexCache.DiscardCacheForMailbox(context, context.LockedMailboxState);
					}
					finally
					{
						context.LockedMailboxState.ReleaseReference();
					}
					commit = true;
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						MailboxState capturedMailboxState = context.LockedMailboxState;
						context.RegisterStateAction(null, delegate(Context ctx)
						{
							MailboxStateCache.DeleteMailboxState(ctx, capturedMailboxState);
						});
						context.EndMailboxOperation(commit);
					}
				}
				return errorCode;
			}

			// Token: 0x0600010B RID: 267 RVA: 0x000076B8 File Offset: 0x000058B8
			private ErrorCode CheckMailboxVersionAndUpgrade(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				errorCode = context.StartMailboxOperation();
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)42588U);
				}
				bool commit = false;
				try
				{
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						mailbox.CheckMailboxVersionAndUpgrade(context);
						mailbox.Save(context);
					}
					commit = true;
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
				return errorCode;
			}

			// Token: 0x0600010C RID: 268 RVA: 0x0000774C File Offset: 0x0000594C
			private ErrorCode ProcessMailboxShape(MapiContext context, PropertyBlob.BlobReader mailboxShapePropertyBlobReader)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				errorCode = context.StartMailboxOperation();
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)35900U);
				}
				bool commit = false;
				try
				{
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						errorCode = mailbox.SetMailboxShape(context, mailboxShapePropertyBlobReader);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)40540U);
						}
						mailbox.Save(context);
					}
					commit = true;
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
				return errorCode;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00007804 File Offset: 0x00005A04
			private ErrorCode ProcessMailboxSignatureMappings(MapiContext context, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap, Dictionary<ushort, Guid> replidGuidMap, Mailbox.MailboxTypeVersion mailboxTypeVersion)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				errorCode = context.StartMailboxOperation();
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)35528U);
				}
				bool commit = false;
				try
				{
					Mailbox.ValidateMailboxTypeVersion(context, context.LockedMailboxState.MailboxType, context.LockedMailboxState.MailboxTypeDetail, mailboxTypeVersion);
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						if (!mailbox.GetMRSPreservingMailboxSignature(context))
						{
							return ErrorCode.CreateCorruptData((LID)46240U);
						}
						if (!mailbox.GetPreservingMailboxSignature(context))
						{
							return ErrorCode.CreateCorruptData((LID)54332U);
						}
						mailbox.NamedPropertyMap.Process(context, numberToNameMap);
						mailbox.ReplidGuidMap.Process(context, replidGuidMap);
					}
					commit = true;
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
				return errorCode;
			}

			// Token: 0x0600010E RID: 270 RVA: 0x000078FC File Offset: 0x00005AFC
			private ErrorCode ProcessMailboxSignatureMappingMetadata(MapiContext context, Guid localIdGuid, Guid mappingSignatureGuid, ulong nextIdCounter, uint reservedIdCounterRange, ulong nextCnCounter, uint reservedCnCounterRange)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				errorCode = context.StartMailboxOperation();
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)59616U);
				}
				bool commit = false;
				checked
				{
					try
					{
						using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
						{
							if (!mailbox.GetMRSPreservingMailboxSignature(context))
							{
								if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.ErrorTrace))
								{
									ExTraceGlobals.MailboxSignatureTracer.TraceError<string, Guid, string>(10L, "The mailbox {0} with Guid {1}, on database {2} is not in MRS mailbox signature preserving mode", mailbox.GetDisplayName(context), mailbox.MailboxGuid, context.Database.MdbName);
								}
								return ErrorCode.CreateCorruptData((LID)52284U);
							}
							if (mailbox.GetPreservingMailboxSignature(context))
							{
								if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.ErrorTrace))
								{
									ExTraceGlobals.MailboxSignatureTracer.TraceError<string, Guid, string>(35040L, "The mailbox {0} with Guid {1}, on database {2} is still in mailbox signature preserving mode", mailbox.GetDisplayName(context), mailbox.MailboxGuid, context.Database.MdbName);
								}
								return ErrorCode.CreateCorruptData((LID)51424U);
							}
							if (!mappingSignatureGuid.Equals(mailbox.GetMappingSignatureGuid(context)))
							{
								if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.ErrorTrace))
								{
									ExTraceGlobals.MailboxSignatureTracer.TraceError(45280L, "The mailbox {0} with Guid {1}, on database {2} has mapping signature Guid {3} != {4}.", new object[]
									{
										mailbox.GetDisplayName(context),
										mailbox.MailboxGuid,
										context.Database.MdbName,
										mailbox.GetMappingSignatureGuid(context),
										mappingSignatureGuid
									});
								}
								return ErrorCode.CreateCorruptData((LID)61664U);
							}
							if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ulong num;
								ulong num2;
								mailbox.GetGlobalCounters(context, out num, out num2);
								ExTraceGlobals.MailboxSignatureTracer.TraceDebug(53472L, "The mailbox {0} with Guid {1}, has current Id: {2}, Cn: {3}, next Id: {4}, Cn: {5}.", new object[]
								{
									mailbox.GetDisplayName(context),
									mailbox.MailboxGuid,
									num,
									num2,
									nextIdCounter,
									nextCnCounter
								});
							}
							mailbox.VerifyAndUpdateGlobalCounters(context, localIdGuid, nextIdCounter, nextCnCounter);
							ulong num3 = nextIdCounter + unchecked((ulong)reservedIdCounterRange);
							mailbox.SetProperty(context, PropTag.Mailbox.ReservedIdCounterRangeUpperLimit, (long)num3);
							ulong num4 = nextCnCounter + unchecked((ulong)reservedCnCounterRange);
							mailbox.SetProperty(context, PropTag.Mailbox.ReservedCnCounterRangeUpperLimit, (long)num4);
							if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.MailboxSignatureTracer.TraceDebug(41184L, "The mailbox {0} with Guid {1}, reserver counter upper limit Id: {2}, Cn: {3}", new object[]
								{
									mailbox.GetDisplayName(context),
									mailbox.MailboxGuid,
									num3,
									num4
								});
							}
							mailbox.SetPreservingMailboxSignature(context, true);
							mailbox.Save(context);
						}
						commit = true;
					}
					finally
					{
						if (context.IsMailboxOperationStarted)
						{
							context.EndMailboxOperation(commit);
						}
					}
					return errorCode;
				}
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00007C10 File Offset: 0x00005E10
			private ErrorCode ProcessUserInformation(MapiContext context, PropertyBlob.BlobReader userInformationPropertyBlobReader)
			{
				if (!DefaultSettings.Get.UserInformationIsEnabled)
				{
					return ErrorCode.CreateNotSupported((LID)51788U);
				}
				if (!UserInfoUpgrader.IsReady(context, context.Database))
				{
					return ErrorCode.CreateNotSupported((LID)62540U);
				}
				if (userInformationPropertyBlobReader.PropertyCount == 0)
				{
					return ErrorCode.NoError;
				}
				List<uint> list = new List<uint>(userInformationPropertyBlobReader.PropertyCount);
				List<object> list2 = new List<object>(userInformationPropertyBlobReader.PropertyCount);
				for (int i = 0; i < userInformationPropertyBlobReader.PropertyCount; i++)
				{
					list.Add(userInformationPropertyBlobReader.GetPropertyTag(i));
					list2.Add(userInformationPropertyBlobReader.GetPropertyValue(i));
				}
				Guid? guid = null;
				Properties initialProperties = new Properties(list.Count);
				for (int j = 0; j < list.Count; j++)
				{
					StorePropTag storePropTag = LegacyHelper.ConvertFromLegacyPropTag(list[j], Microsoft.Exchange.Server.Storage.PropTags.ObjectType.UserInfo, null, false);
					if (storePropTag == PropTag.UserInfo.UserInformationGuid)
					{
						guid = (Guid?)list2[j];
					}
					else if (storePropTag.PropInfo == null || !storePropTag.PropInfo.IsCategory(3) || storePropTag.PropInfo.IsCategory(4))
					{
						initialProperties.Add(storePropTag, list2[j]);
					}
				}
				if (guid == null)
				{
					return ErrorCode.CreateCorruptData((LID)43084U);
				}
				UserInformation.Create(context, guid.Value, initialProperties, true);
				return ErrorCode.NoError;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00007D70 File Offset: 0x00005F70
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode first = base.EcValidateArguments(context);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)52828U);
				}
				MailboxSignatureSectionType mailboxSignatureSectionType;
				MailboxSignature.Parse(this.mailboxSignature, out mailboxSignatureSectionType);
				mailboxSignatureSectionType &= ~(MailboxSignatureSectionType.MailboxTypeVersion | MailboxSignatureSectionType.PartitionInformation | MailboxSignatureSectionType.UserInformation);
				if ((short)(mailboxSignatureSectionType & MailboxSignatureSectionType.MailboxShape) == 32)
				{
					mailboxSignatureSectionType &= ~MailboxSignatureSectionType.MailboxShape;
					if (mailboxSignatureSectionType != (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata | MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping | MailboxSignatureSectionType.TenantHint) && mailboxSignatureSectionType != (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.TenantHint))
					{
						return ErrorCode.CreateCorruptData((LID)56924U);
					}
				}
				if (mailboxSignatureSectionType != (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata | MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping | MailboxSignatureSectionType.TenantHint) && mailboxSignatureSectionType != (MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.TenantHint) && mailboxSignatureSectionType != (MailboxSignatureSectionType.NamedPropertyMapping | MailboxSignatureSectionType.ReplidGuidMapping) && mailboxSignatureSectionType != MailboxSignatureSectionType.MappingMetadata)
				{
					return ErrorCode.CreateCorruptData((LID)17428U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x040000C7 RID: 199
			private readonly byte[] mailboxSignature;
		}

		// Token: 0x02000034 RID: 52
		internal class AdminRpcGetMailboxTable : AdminRpc
		{
			// Token: 0x06000111 RID: 273 RVA: 0x00007E0F File Offset: 0x0000600F
			internal AdminRpcGetMailboxTable(ClientSecurityContext callerSecurityContext, Guid? mdbGuid, int lparam, uint[] propTags, uint cpid, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetMailboxTable50, callerSecurityContext, mdbGuid, AdminRpc.ExpectedDatabaseState.AnyAttachedState, auxiliaryIn)
			{
				this.lparam = lparam;
				this.propTags = propTags;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00007E2D File Offset: 0x0000602D
			internal AdminRpcGetMailboxTable(ClientSecurityContext callerSecurityContext, int lparam, uint[] propTags, uint cpid, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetMailboxTable50, callerSecurityContext, auxiliaryIn)
			{
				this.lparam = lparam;
				this.propTags = propTags;
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000113 RID: 275 RVA: 0x00007E48 File Offset: 0x00006048
			public byte[] Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000114 RID: 276 RVA: 0x00007E50 File Offset: 0x00006050
			public uint RowCount
			{
				get
				{
					return this.rowCount;
				}
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00007E58 File Offset: 0x00006058
			private static RequiredMaintenanceResourceType RequiredMaintenanceResourceTypeFromMailboxInfoTableFlags(GetMailboxInfoTableFlags mailboxInfoTableFlags)
			{
				switch (mailboxInfoTableFlags)
				{
				case GetMailboxInfoTableFlags.MaintenanceItems:
					return RequiredMaintenanceResourceType.Store;
				case GetMailboxInfoTableFlags.MaintenanceItemsWithDS:
					return RequiredMaintenanceResourceType.DirectoryServiceAndStore;
				case GetMailboxInfoTableFlags.UrgentMaintenanceItems:
					return RequiredMaintenanceResourceType.StoreUrgent;
				default:
					return RequiredMaintenanceResourceType.Store;
				}
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00007E84 File Offset: 0x00006084
			private void GetMailboxTableInfoForDatabase(MapiContext context, StoreDatabase database)
			{
				int num = 0;
				List<Properties> list;
				try
				{
					IL_02:
					GetMailboxInfoTableFlags getMailboxInfoTableFlags = (GetMailboxInfoTableFlags)this.lparam;
					if (GetMailboxInfoTableFlags.MaintenanceItems == getMailboxInfoTableFlags || GetMailboxInfoTableFlags.MaintenanceItemsWithDS == getMailboxInfoTableFlags || GetMailboxInfoTableFlags.UrgentMaintenanceItems == getMailboxInfoTableFlags)
					{
						list = new List<Properties>();
						List<MaintenanceHandler.MaintenanceToSchedule> scheduledMaintenances = MaintenanceHandler.GetScheduledMaintenances(context, AdminRpcServer.AdminRpcGetMailboxTable.RequiredMaintenanceResourceTypeFromMailboxInfoTableFlags(getMailboxInfoTableFlags));
						using (List<MaintenanceHandler.MaintenanceToSchedule>.Enumerator enumerator = scheduledMaintenances.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MaintenanceHandler.MaintenanceToSchedule maintenanceToSchedule = enumerator.Current;
								Properties item = new Properties(2);
								foreach (StorePropTag tag in this.storePropTags)
								{
									object value = Property.NotFoundError(tag);
									uint propTag = tag.PropTag;
									if (propTag != 1035665480U)
									{
										if (propTag != 1728512072U)
										{
											if (propTag == 1746862083U)
											{
												value = maintenanceToSchedule.MailboxNumber;
											}
										}
										else
										{
											value = maintenanceToSchedule.MailboxGuid;
										}
									}
									else
									{
										value = maintenanceToSchedule.MaintenanceId;
									}
									item.Add(tag, value);
								}
								list.Add(item);
							}
							goto IL_118;
						}
					}
					list = MapiMailbox.GetMailboxInfoTable(context, database, getMailboxInfoTableFlags, this.storePropTags);
					IL_118:;
				}
				catch (SqlException ex)
				{
					context.OnExceptionCatch(ex);
					if (ex.Number != 601)
					{
						throw;
					}
					if (++num >= 10)
					{
						throw new StoreException((LID)39352U, ErrorCodeValue.Busy, "Server is busy", ex);
					}
					if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.AdminRpcTracer.TraceError<SqlException, int>(47957, 0L, "Encountered exception {0}, retry: {1}", ex, num);
					}
					goto IL_02;
				}
				int num2 = 0;
				for (int j = 0; j < list.Count; j++)
				{
					LegacyHelper.MassageOutgoingProperties(list[j]);
					this.outputBufferSize += AdminRpcParseFormat.SetValues(null, ref num2, 0, list[j]);
				}
				if (this.tableRows == null)
				{
					this.tableRows = list;
					return;
				}
				this.tableRows.AddRange(list);
			}

			// Token: 0x06000117 RID: 279 RVA: 0x000080B8 File Offset: 0x000062B8
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode noError = ErrorCode.NoError;
				this.storePropTags = LegacyHelper.ConvertFromLegacyPropTags(this.propTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Mailbox, null, true);
				if (base.MdbGuid == null)
				{
					Storage.ForEachDatabase(context, delegate(Context executionContext, StoreDatabase database, Func<bool> shouldCallbackContinue)
					{
						this.GetMailboxTableInfoForDatabase((MapiContext)executionContext, database);
					});
				}
				else
				{
					if (base.Database == null)
					{
						return ErrorCode.CreateNotInitialized((LID)49789U);
					}
					this.GetMailboxTableInfoForDatabase(context, base.Database);
				}
				byte[] array = new byte[this.outputBufferSize];
				if (this.tableRows != null)
				{
					int num = 0;
					for (int i = 0; i < this.tableRows.Count; i++)
					{
						AdminRpcParseFormat.SetValues(array, ref num, array.Length, this.tableRows[i]);
					}
					this.rowCount = (uint)this.tableRows.Count;
				}
				else
				{
					this.rowCount = 0U;
				}
				this.result = array;
				return noError;
			}

			// Token: 0x06000118 RID: 280 RVA: 0x000081A0 File Offset: 0x000063A0
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				if (this.propTags == null || this.propTags.Length == 0)
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)60925U);
				}
				else if (!EnumValidator.IsValidValue<GetMailboxInfoTableFlags>((GetMailboxInfoTableFlags)this.lparam))
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)63416U);
				}
				return errorCode;
			}

			// Token: 0x040000C8 RID: 200
			private const int AverageNumberOfMailboxes = 100;

			// Token: 0x040000C9 RID: 201
			private byte[] result;

			// Token: 0x040000CA RID: 202
			private uint[] propTags;

			// Token: 0x040000CB RID: 203
			private uint rowCount;

			// Token: 0x040000CC RID: 204
			private int lparam;

			// Token: 0x040000CD RID: 205
			private StorePropTag[] storePropTags;

			// Token: 0x040000CE RID: 206
			private int outputBufferSize;

			// Token: 0x040000CF RID: 207
			private List<Properties> tableRows;
		}

		// Token: 0x02000035 RID: 53
		internal class AdminRpcSyncMailboxWithDS : AdminRpc
		{
			// Token: 0x0600011A RID: 282 RVA: 0x000081F5 File Offset: 0x000063F5
			internal AdminRpcSyncMailboxWithDS(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn) : base(AdminMethod.EcSyncMailboxWithDS50, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
				this.mailboxGuid = mailboxGuid;
			}

			// Token: 0x0600011B RID: 283 RVA: 0x0000820F File Offset: 0x0000640F
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				if (this.mailboxGuid == Guid.Empty)
				{
					return ErrorCode.CreateInvalidParameter((LID)57319U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00008238 File Offset: 0x00006438
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode result = ErrorCode.NoError;
				if (base.Database.IsRecovery)
				{
					result = ErrorCode.CreateInvalidParameter((LID)56008U);
				}
				else
				{
					Microsoft.Exchange.Server.Storage.MapiDisp.MailboxCleanup.SynchronizeDirectoryAndMailboxTable(context, this.mailboxGuid, true);
				}
				return result;
			}

			// Token: 0x040000D0 RID: 208
			private Guid mailboxGuid;
		}

		// Token: 0x02000036 RID: 54
		internal class AdminGetMailboxTableEntry50 : AdminRpc
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600011D RID: 285 RVA: 0x00008278 File Offset: 0x00006478
			// (set) Token: 0x0600011E RID: 286 RVA: 0x00008280 File Offset: 0x00006480
			private Guid MailboxGuid
			{
				get
				{
					return this.mailboxGuid;
				}
				set
				{
					this.mailboxGuid = value;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600011F RID: 287 RVA: 0x00008289 File Offset: 0x00006489
			// (set) Token: 0x06000120 RID: 288 RVA: 0x00008291 File Offset: 0x00006491
			private GetMailboxInfoTableFlags Flags
			{
				get
				{
					return this.flags;
				}
				set
				{
					this.flags = value;
				}
			}

			// Token: 0x06000121 RID: 289 RVA: 0x0000829A File Offset: 0x0000649A
			internal AdminGetMailboxTableEntry50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, GetMailboxInfoTableFlags flags, uint[] propTags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetMailboxTableEntry50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.AnyAttachedState, auxiliaryIn)
			{
				this.propTags = propTags;
				this.MailboxGuid = mailboxGuid;
				this.Flags = flags;
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000122 RID: 290 RVA: 0x000082C5 File Offset: 0x000064C5
			public byte[] Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x06000123 RID: 291 RVA: 0x000082D0 File Offset: 0x000064D0
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				StorePropTag[] requestedPropTags = LegacyHelper.ConvertFromLegacyPropTags(this.propTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Mailbox, null, true);
				this.TryUpdateTableSizeStatistics(context, requestedPropTags);
				Properties mailboxInfoTableEntry = MapiMailbox.GetMailboxInfoTableEntry(context, base.Database, this.MailboxGuid, this.Flags, requestedPropTags);
				int num = 0;
				int num2 = AdminRpcParseFormat.SetValues(null, ref num, 0, mailboxInfoTableEntry);
				num = 0;
				byte[] array = new byte[num2];
				AdminRpcParseFormat.SetValues(array, ref num, array.Length, mailboxInfoTableEntry);
				this.result = array;
				return ErrorCode.NoError;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00008344 File Offset: 0x00006544
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode first = base.EcValidateArguments(context);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)58240U);
				}
				if (this.propTags == null || this.propTags.Length == 0)
				{
					return ErrorCode.CreateInvalidParameter((LID)57519U);
				}
				return first;
			}

			// Token: 0x06000125 RID: 293 RVA: 0x0000839C File Offset: 0x0000659C
			protected override ErrorCode EcInitializeResources(MapiContext context)
			{
				ErrorCode errorCode = base.EcInitializeResources(context);
				if (errorCode == ErrorCodeValue.UnknownMailbox)
				{
					return ErrorCode.CreateNotFound((LID)48504U);
				}
				return errorCode;
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00008404 File Offset: 0x00006604
			private void TryUpdateTableSizeStatistics(MapiContext context, StorePropTag[] requestedPropTags)
			{
				bool flag = false;
				IList<Mailbox.TableSizeStatistics> tableSizeStatisticsDefinitions = Mailbox.TableSizeStatisticsDefinitions;
				for (int i = 0; i < requestedPropTags.Length; i++)
				{
					StorePropTag requestedPropTag = requestedPropTags[i];
					if (tableSizeStatisticsDefinitions.Any((Mailbox.TableSizeStatistics s) => s.TotalPagesProperty == requestedPropTag || s.AvailablePagesProperty == requestedPropTag))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
				bool value = ConfigurationSchema.AggresiveUpdateMailboxTableSizeStatistics.Value;
				TimeSpan lockTimeout = value ? TimeSpan.FromMinutes(10.0) : TimeSpan.FromSeconds(5.0);
				context.InitializeMailboxExclusiveOperation(this.MailboxGuid, ExecutionDiagnostics.OperationSource.AdminRpc, lockTimeout);
				bool commit = false;
				try
				{
					ErrorCode errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
					if (errorCode != ErrorCode.NoError)
					{
						if (value)
						{
							throw new StoreException((LID)49100U, errorCode);
						}
					}
					else if (context.LockedMailboxState.IsAccessible)
					{
						using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
						{
							mailbox.UpdateTableSizeStatistics(context);
							mailbox.Save(context);
						}
						commit = true;
					}
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
			}

			// Token: 0x040000D1 RID: 209
			private Guid mailboxGuid;

			// Token: 0x040000D2 RID: 210
			private GetMailboxInfoTableFlags flags;

			// Token: 0x040000D3 RID: 211
			private byte[] result;

			// Token: 0x040000D4 RID: 212
			private uint[] propTags;
		}

		// Token: 0x02000037 RID: 55
		internal class AdminGetViewsTableEx50 : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x06000127 RID: 295 RVA: 0x00008558 File Offset: 0x00006758
			internal AdminGetViewsTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetViewsTableEx50, callerSecurityContext, mdbGuid, mailboxGuid, false, auxiliaryIn)
			{
				this.propTags = propTags;
				this.folderLTID = folderLTID;
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000128 RID: 296 RVA: 0x00008578 File Offset: 0x00006778
			public byte[] Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000129 RID: 297 RVA: 0x00008580 File Offset: 0x00006780
			public uint ResultRowCount
			{
				get
				{
					return this.resultRowCount;
				}
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00008588 File Offset: 0x00006788
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				IList<Properties> viewsProperties;
				using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
				{
					StorePropTag[] storePropTags = LegacyHelper.ConvertFromLegacyPropTags(this.propTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.ViewDefinition, null, true);
					ExchangeId folderId = ExchangeId.Create(context, mailbox.ReplidGuidMap, this.folderLTID.guid, this.folderLTID.globCount);
					viewsProperties = Folder.GetViewsProperties(context, mailbox, folderId, storePropTags);
				}
				int num = 0;
				int num2 = 0;
				foreach (Properties properties in viewsProperties)
				{
					LegacyHelper.MassageOutgoingProperties(properties);
					num2 += AdminRpcParseFormat.SetValues(null, ref num, 0, properties);
				}
				byte[] buff = new byte[num2];
				num = 0;
				foreach (Properties properties2 in viewsProperties)
				{
					num2 += AdminRpcParseFormat.SetValues(buff, ref num, num2, properties2);
				}
				this.result = buff;
				this.resultRowCount = (uint)viewsProperties.Count;
				return ErrorCode.NoError;
			}

			// Token: 0x0600012B RID: 299 RVA: 0x000086BC File Offset: 0x000068BC
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode first = base.EcValidateArguments(context);
				if (!(first != ErrorCode.NoError))
				{
					if (this.propTags == null || this.propTags.Length == 0)
					{
						first = ErrorCode.CreateInvalidParameter((LID)42407U);
					}
					else if (this.folderLTID.guid == Guid.Empty || this.folderLTID.globCount == 0UL)
					{
						first = ErrorCode.CreateInvalidParameter((LID)58791U);
					}
				}
				return first;
			}

			// Token: 0x040000D5 RID: 213
			private byte[] result;

			// Token: 0x040000D6 RID: 214
			private uint[] propTags;

			// Token: 0x040000D7 RID: 215
			private LTID folderLTID;

			// Token: 0x040000D8 RID: 216
			private uint resultRowCount;
		}

		// Token: 0x02000038 RID: 56
		internal class AdminGetRestrictionTableEx50 : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x0600012C RID: 300 RVA: 0x00008739 File Offset: 0x00006939
			internal AdminGetRestrictionTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, byte[] auxiliaryIn) : base(AdminMethod.EcAdminGetRestrictionTableEx50, callerSecurityContext, mdbGuid, mailboxGuid, false, auxiliaryIn)
			{
				this.propTags = propTags;
				this.folderLTID = folderLTID;
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600012D RID: 301 RVA: 0x00008759 File Offset: 0x00006959
			public byte[] Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x0600012E RID: 302 RVA: 0x00008761 File Offset: 0x00006961
			public uint ResultRowCount
			{
				get
				{
					return this.resultRowCount;
				}
			}

			// Token: 0x0600012F RID: 303 RVA: 0x0000876C File Offset: 0x0000696C
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				IList<Properties> restrictionsProperties;
				using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
				{
					StorePropTag[] storePropTags = LegacyHelper.ConvertFromLegacyPropTags(this.propTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.RestrictionView, null, true);
					ExchangeId exchangeId = ExchangeId.Create(context, mailbox.ReplidGuidMap, this.folderLTID.guid, this.folderLTID.globCount);
					ExchangeId materializedRestrictionRootForFolder = ((LogicalMailbox)mailbox).GetMaterializedRestrictionRootForFolder(context, exchangeId);
					restrictionsProperties = Folder.GetRestrictionsProperties(context, mailbox, exchangeId, materializedRestrictionRootForFolder, storePropTags);
				}
				int num = 0;
				int num2 = 0;
				foreach (Properties properties in restrictionsProperties)
				{
					LegacyHelper.MassageOutgoingProperties(properties);
					num2 += AdminRpcParseFormat.SetValues(null, ref num, 0, properties);
				}
				byte[] buff = new byte[num2];
				num = 0;
				foreach (Properties properties2 in restrictionsProperties)
				{
					num2 += AdminRpcParseFormat.SetValues(buff, ref num, num2, properties2);
				}
				this.result = buff;
				this.resultRowCount = (uint)restrictionsProperties.Count;
				return ErrorCode.NoError;
			}

			// Token: 0x06000130 RID: 304 RVA: 0x000088B0 File Offset: 0x00006AB0
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode first = base.EcValidateArguments(context);
				if (!(first != ErrorCode.NoError))
				{
					if (this.propTags == null || this.propTags.Length == 0)
					{
						first = ErrorCode.CreateInvalidParameter((LID)65228U);
					}
					else if (this.folderLTID.guid == Guid.Empty || this.folderLTID.globCount == 0UL)
					{
						first = ErrorCode.CreateInvalidParameter((LID)63180U);
					}
				}
				return first;
			}

			// Token: 0x040000D9 RID: 217
			private byte[] result;

			// Token: 0x040000DA RID: 218
			private uint[] propTags;

			// Token: 0x040000DB RID: 219
			private LTID folderLTID;

			// Token: 0x040000DC RID: 220
			private uint resultRowCount;
		}

		// Token: 0x02000039 RID: 57
		internal class AdminExecuteTask50 : AdminRpc
		{
			// Token: 0x06000131 RID: 305 RVA: 0x00008930 File Offset: 0x00006B30
			internal AdminExecuteTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid taskClass, int taskId, byte[] auxiliaryIn) : base(AdminMethod.EcAdminExecuteTask50, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
				this.taskClass = taskClass;
				this.taskId = taskId;
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000132 RID: 306 RVA: 0x00008A7C File Offset: 0x00006C7C
			internal override int OperationDetail
			{
				get
				{
					if (this.taskMapping.ContainsKey(this.taskClass))
					{
						return (int)(this.taskMapping[this.taskClass] + 4000U);
					}
					return base.OperationDetail;
				}
			}

			// Token: 0x06000133 RID: 307 RVA: 0x00008AB0 File Offset: 0x00006CB0
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode result = ErrorCode.NoError;
				if (base.Database.IsRecovery)
				{
					result = ErrorCode.CreateInvalidParameter((LID)43720U);
				}
				else if (this.taskId == 0)
				{
					MaintenanceHandler.DoDatabaseMaintenance(context, base.DatabaseInfo, this.taskClass);
				}
				else
				{
					MaintenanceHandler.DoMailboxMaintenance(context, this.taskClass, this.taskId);
				}
				return result;
			}

			// Token: 0x040000DD RID: 221
			private int taskId;

			// Token: 0x040000DE RID: 222
			private Guid taskClass;

			// Token: 0x040000DF RID: 223
			public static readonly Guid TestPurposesTaskId = Guid.Empty;

			// Token: 0x040000E0 RID: 224
			private Dictionary<Guid, AdminRpcServer.AdminExecuteTask50.MaintenanceTask> taskMapping = new Dictionary<Guid, AdminRpcServer.AdminExecuteTask50.MaintenanceTask>
			{
				{
					new Guid("{8dda68d9-e1c4-4b97-a884-bf0ab208cf5c}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.MarkLogicalIndicesForCleanup
				},
				{
					new Guid("{f6f50b68-76c8-4b41-865f-e984022602ac}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.DeliveredToCleanupMaintenance
				},
				{
					new Guid("{9a0932ca-268a-4a60-b90e-fa9335a2f139}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.EventHistoryCleanupMaintenance
				},
				{
					new Guid("{128e9fa8-7013-42d8-a957-9bda9f288649}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.MarkHardDeletedMailboxesForCleanupMaintenance
				},
				{
					new Guid("{81650b69-0c92-488f-9de7-d2e41fca7efa}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.CleanupAndRemoveTombstoneMailboxes
				},
				{
					new Guid("{db82d4f5-00a5-4d65-96bc-45b81285f12f}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.MarkMailboxesForSearchFolderAgeOut
				},
				{
					new Guid("{94196d5c-e792-466d-8f8d-e72ae0dd780f}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.MaintenanceCleanupTombstoneTable
				},
				{
					new Guid("{ecb20c7e-2942-40bc-92b2-acdf8948ab1a}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.UrgentTombstoneTableCleanup
				},
				{
					new Guid("{285abee5-b82c-4849-ac49-beaf265f3b46}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.MarkExpiredDisabledMailboxesForSynchronizationWithDS
				},
				{
					new Guid("{82d947ed-87da-4389-b4fa-af51d947f16e}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.MarkIdleUserAccessibleMailboxesForSynchronizationWithDS
				},
				{
					new Guid("{818429a5-c7c8-4546-8cad-c71efaf3c219}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.CleanupLogicalIndexes
				},
				{
					new Guid("{f4946920-3356-4f2d-bfb0-e72f14af6f56}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.ApplyMaintenanceTable
				},
				{
					new Guid("{c9490642-e68b-4157-954e-540d81e0ed87}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.AgeOutMailboxSearchFolders
				},
				{
					new Guid("{05ad2280-b95c-4e3f-bc1c-baaa8fb97e55}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.CleanupAndRemoveHardDeletedMailbox
				},
				{
					new Guid("{8b5c9cf4-109d-46b2-a050-d509f4c58e03}"),
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.SynchronizeDirectoryAndMailboxTable
				},
				{
					AdminRpcServer.AdminExecuteTask50.TestPurposesTaskId,
					AdminRpcServer.AdminExecuteTask50.MaintenanceTask.ForTestPurposes
				}
			};

			// Token: 0x0200003A RID: 58
			internal enum MaintenanceTask : uint
			{
				// Token: 0x040000E2 RID: 226
				ForTestPurposes,
				// Token: 0x040000E3 RID: 227
				MarkLogicalIndicesForCleanup,
				// Token: 0x040000E4 RID: 228
				DeliveredToCleanupMaintenance,
				// Token: 0x040000E5 RID: 229
				EventHistoryCleanupMaintenance,
				// Token: 0x040000E6 RID: 230
				MarkHardDeletedMailboxesForCleanupMaintenance,
				// Token: 0x040000E7 RID: 231
				CleanupAndRemoveTombstoneMailboxes,
				// Token: 0x040000E8 RID: 232
				MarkMailboxesForSearchFolderAgeOut,
				// Token: 0x040000E9 RID: 233
				MaintenanceCleanupTombstoneTable,
				// Token: 0x040000EA RID: 234
				UrgentTombstoneTableCleanup,
				// Token: 0x040000EB RID: 235
				MarkExpiredDisabledMailboxesForSynchronizationWithDS,
				// Token: 0x040000EC RID: 236
				MarkIdleUserAccessibleMailboxesForSynchronizationWithDS,
				// Token: 0x040000ED RID: 237
				CleanupLogicalIndexes,
				// Token: 0x040000EE RID: 238
				ApplyMaintenanceTable,
				// Token: 0x040000EF RID: 239
				AgeOutMailboxSearchFolders,
				// Token: 0x040000F0 RID: 240
				CleanupAndRemoveHardDeletedMailbox,
				// Token: 0x040000F1 RID: 241
				SynchronizeDirectoryAndMailboxTable
			}
		}

		// Token: 0x0200003B RID: 59
		internal class AdminPrePopulateCacheEx50 : AdminRpcServer.MailboxAdminRpc
		{
			// Token: 0x06000135 RID: 309 RVA: 0x00008B1D File Offset: 0x00006D1D
			internal AdminPrePopulateCacheEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] partitionHint, string domainController, byte[] auxiliaryIn) : base(AdminMethod.EcAdminPrePopulateCacheEx50, callerSecurityContext, mdbGuid, mailboxGuid, false, auxiliaryIn)
			{
				this.domainController = domainController;
				this.partitionHint = partitionHint;
			}

			// Token: 0x06000136 RID: 310 RVA: 0x00008B3D File Offset: 0x00006D3D
			protected override ErrorCode EcExecuteMailboxRpcOperation(MapiContext context)
			{
				return ErrorCode.NoError;
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00008B44 File Offset: 0x00006D44
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				TenantHint tenantHint = TenantHint.RootOrg;
				if (this.partitionHint != null)
				{
					tenantHint = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.ResolveTenantHint(context, this.partitionHint);
				}
				Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.PrePopulateCachesForMailbox(context, tenantHint, base.MailboxGuid, this.domainController);
				return ErrorCode.NoError;
			}

			// Token: 0x06000138 RID: 312 RVA: 0x00008B90 File Offset: 0x00006D90
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (!(errorCode != ErrorCode.NoError) && (this.domainController == null || this.domainController.Length == 0))
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)42800U);
				}
				return errorCode;
			}

			// Token: 0x06000139 RID: 313 RVA: 0x00008BD8 File Offset: 0x00006DD8
			protected override ErrorCode EcInitializeResources(MapiContext context)
			{
				((AdminExecutionDiagnostics)context.Diagnostics).AdminExMonLogger.SetMdbGuid(base.MdbGuid.Value);
				((AdminExecutionDiagnostics)context.Diagnostics).AdminExMonLogger.SetMailboxGuid(base.MailboxGuid);
				return ErrorCode.NoError;
			}

			// Token: 0x0600013A RID: 314 RVA: 0x00008C28 File Offset: 0x00006E28
			protected override void CleanupResources(MapiContext context)
			{
			}

			// Token: 0x040000F2 RID: 242
			private readonly string domainController;

			// Token: 0x040000F3 RID: 243
			private readonly byte[] partitionHint;
		}

		// Token: 0x0200003C RID: 60
		internal abstract class AdminRpcMultiMailboxSearchBase : AdminRpc
		{
			// Token: 0x0600013B RID: 315 RVA: 0x00008C2C File Offset: 0x00006E2C
			protected AdminRpcMultiMailboxSearchBase(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn) : base(methodId, callerSecurityContext, new Guid?(mdbGuid), auxiliaryIn)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.ctor");
				this.multiMailboxSearcher = Hookable<IMultiMailboxSearch>.Create(true, new MultiMailboxSearch(mdbGuid, AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.SearchTimeOut));
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.ctor");
			}

			// Token: 0x0600013C RID: 316 RVA: 0x00008D08 File Offset: 0x00006F08
			protected static ErrorCode InspectAndFixSearchCriteria(uint errorLID, Guid mdbGuid, ref SearchCriteria searchCriteria, CompareInfo compareInfo)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.InspectAndFixSearchCriteria");
				ErrorCode errorCode = ErrorCode.NoError;
				StorePropTag nonFullTextPropTag = StorePropTag.Invalid;
				if (searchCriteria != null)
				{
					searchCriteria = searchCriteria.InspectAndFix(delegate(SearchCriteria criteriaToInspect, CompareInfo localCompareInfo)
					{
						ErrorCode errorCode = ErrorCode.NoError;
						SearchCriteriaText searchCriteriaText = criteriaToInspect as SearchCriteriaText;
						SearchCriteriaCompare searchCriteriaCompare = (searchCriteriaText == null) ? (criteriaToInspect as SearchCriteriaCompare) : null;
						ExtendedPropertyColumn extendedColumn = null;
						if (searchCriteriaText != null)
						{
							extendedColumn = (searchCriteriaText.Lhs as ExtendedPropertyColumn);
						}
						else if (searchCriteriaCompare != null)
						{
							extendedColumn = (searchCriteriaCompare.Lhs as ExtendedPropertyColumn);
						}
						errorCode = AdminRpcServer.AdminRpcMultiMailboxSearchBase.IsPropertyInFullTextIndex(errorLID, mdbGuid, extendedColumn, ref nonFullTextPropTag);
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode;
							return Factory.CreateSearchCriteriaFalse();
						}
						return criteriaToInspect;
					}, compareInfo, true);
				}
				if (errorCode != ErrorCode.NoError && nonFullTextPropTag != StorePropTag.Invalid && ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(39056, 0L, "eDiscovery search on request validation failed due to query restriction/searchCriteria:{0} containing non full text property:{1}.", searchCriteria.ToString(), string.IsNullOrEmpty(nonFullTextPropTag.DescriptiveName) ? string.Empty : nonFullTextPropTag.DescriptiveName);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.InspectAndFixSearchCriteria");
				return errorCode;
			}

			// Token: 0x0600013D RID: 317 RVA: 0x00008DED File Offset: 0x00006FED
			protected static void TraceFunction(string message)
			{
				if (string.IsNullOrEmpty(message))
				{
					return;
				}
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceFunction(40080, 0L, message);
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600013E RID: 318 RVA: 0x00008E17 File Offset: 0x00007017
			protected IMultiMailboxSearch MultiMailboxSearcher
			{
				get
				{
					return this.multiMailboxSearcher.Value;
				}
			}

			// Token: 0x0600013F RID: 319 RVA: 0x00008E24 File Offset: 0x00007024
			protected static string GetFastPropertyNameOfColumn(StorePropTag storePropTag, Guid mdbGuid)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.GetFastPropertyNameOfColumn");
				string text = string.Empty;
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(58768, 0L, "Fast Property name requested for {0} Store Property.", storePropTag.DescriptiveName);
				}
				FullTextIndexSchema.FullTextIndexInfo fullTextIndexInfo = null;
				if (FullTextIndexSchema.Current.IsPropertyInFullTextIndex(storePropTag.PropInfo.PropName, mdbGuid, out fullTextIndexInfo))
				{
					text = fullTextIndexInfo.FastPropertyName;
					ExAssert.RetailAssert(!string.IsNullOrEmpty(fullTextIndexInfo.FastPropertyName), "Fast Property Name " + storePropTag.DescriptiveName + " cannot be null or empty.");
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(34192, 0L, "Fast Property name requested for {0} Store Property and the Fast Property name is {1}", storePropTag.DescriptiveName, text);
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.GetFastPropertyNameOfColumn");
				return text;
			}

			// Token: 0x06000140 RID: 320
			protected abstract ErrorCode ExecuteRpc(MapiContext context);

			// Token: 0x06000141 RID: 321 RVA: 0x00008EF0 File Offset: 0x000070F0
			internal IDisposable SetMultiMailboxSearchTestHook(IMultiMailboxSearch search)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.SetMultiMailboxSearchTestHook");
				IDisposable result = this.multiMailboxSearcher.SetTestHook(search);
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.SetMultiMailboxSearchTestHook");
				return result;
			}

			// Token: 0x06000142 RID: 322
			protected abstract void UpdatePerfCounters(StorePerDatabasePerformanceCountersInstance perfInstance, long timeTaken, bool isFailed);

			// Token: 0x06000143 RID: 323 RVA: 0x00008F20 File Offset: 0x00007120
			protected ErrorCode ValidateSearchCriteria(MapiContext context, SearchCriteria searchCriteria, uint errorLID)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.ValidateSearchCriteria");
				ErrorCode errorCode = AdminRpcServer.AdminRpcMultiMailboxSearchBase.InspectAndFixSearchCriteria(errorLID, context.DatabaseGuid, ref searchCriteria, (context.Culture == null) ? null : context.Culture.CompareInfo);
				if (errorCode != ErrorCode.NoError)
				{
					errorCode = errorCode.Propagate((LID)50752U);
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(63216, 0L, "eDiscovery search on database {0} request validation failed due to query:{1} containing non full text property.", (base.Database != null) ? base.Database.MdbName : string.Empty, searchCriteria.ToString());
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.ValidateSearchCriteria");
				return errorCode;
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00008FCC File Offset: 0x000071CC
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.EcExcuteRpc");
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(50576, 0L, "eDiscovery search initiated on database {0}", base.Database.MdbName);
				}
				if (AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.IsMaxSearchCountReached())
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(47504, 0L, "eDiscovery Max allowed searches limit hit for database {0}", base.Database.MdbName);
					}
					ErrorCode result = ErrorCode.CreateMaxMultiMailboxSearchExceeded((LID)55560U);
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.EcExcuteRpc");
					return result;
				}
				ErrorCode errorCode = ErrorCode.NoError;
				StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(base.Database);
				StopwatchStamp stamp = StopwatchStamp.GetStamp();
				try
				{
					AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.IncrementSearchCount();
					errorCode = this.ExecuteRpc(context);
				}
				finally
				{
					AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.DecrementSearchCount();
					long num = (long)stamp.ElapsedTime.TotalMilliseconds;
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, long>(63888, 0L, "eDiscovery search initiated on database {0} took {1} ms", base.Database.MdbName, num);
					}
					if (databaseInstance != null)
					{
						this.UpdatePerfCounters(databaseInstance, num, errorCode != ErrorCode.NoError);
						databaseInstance.AverageMultiMailboxSearchTimeSpentInStore.IncrementBy(num);
						databaseInstance.AverageMultiMailboxSearchTimeSpentInStoreBase.Increment();
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.EcExcuteRpc");
				return errorCode;
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00009140 File Offset: 0x00007340
			protected ErrorCode RequestToSearchCriteria(MapiContext context, MultiMailboxRequestBase request, byte[] serializedQuery, out SearchCriteria searchCriteria)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcServerMultiMailboxSearchBase.RequestToSearchCriteria");
				ErrorCode result = ErrorCode.NoError;
				searchCriteria = null;
				try
				{
					Restriction restriction = Restriction.Deserialize(context, serializedQuery, null, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
					searchCriteria = restriction.ToSearchCriteria(base.Database, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
				}
				catch (StoreException ex)
				{
					context.OnExceptionCatch(ex);
					result = ErrorCode.CreateMultiMailboxSearchInvalidRestriction((LID)43104U);
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceError<int, string>(59488, 0L, "Invalid query specified in the search request. The de-serialize of the query restriction failed with error code {0}, reason: {1}.", (int)ex.Error, string.IsNullOrEmpty(ex.Message) ? string.Empty : ex.Message);
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcServerMultiMailboxSearchBase.RequestToSearchCriteria");
				return result;
			}

			// Token: 0x06000146 RID: 326 RVA: 0x000091F8 File Offset: 0x000073F8
			protected SearchCriteria CreateFolderRestrictionCriteria(MapiContext context, IReplidGuidMap replidGuidMap, MultiMailboxSearchMailboxInfo searchMailboxInfo)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Enterting AdminRpcServerMultiMailboxSearchBase.CreateMailboxSearchCriteria");
				SearchCriteria result = null;
				MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Database);
				if (searchMailboxInfo.FolderRestriction == null || searchMailboxInfo.FolderRestriction.Length == 0)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<Guid, string>(39312, 0L, "No Folder Restriction specified for the mailbox {0} on database {1}", searchMailboxInfo.MailboxGuid, base.Database.MdbName);
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcServerMultiMailboxSearchBase.CreateMailboxSearchCriteria");
					return result;
				}
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<Guid, string>(55696, 0L, "Folder Restriction specified for the mailbox {0} on database {1}, creating the folder restriction.", searchMailboxInfo.MailboxGuid, base.Database.MdbName);
				}
				if (searchMailboxInfo.FolderRestriction != null && searchMailboxInfo.FolderRestriction.Length > 0)
				{
					List<SearchCriteria> list = new List<SearchCriteria>(6);
					Restriction restriction = Restriction.Deserialize(context, searchMailboxInfo.FolderRestriction, null, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
					SearchCriteria searchCriteria = restriction.ToSearchCriteria(base.Database, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
					SearchCriteriaAnd searchCriteriaAnd = searchCriteria as SearchCriteriaAnd;
					if (searchCriteriaAnd != null && searchCriteriaAnd.NestedCriteria != null && searchCriteriaAnd.NestedCriteria.Length > 0)
					{
						foreach (SearchCriteria searchCriteria2 in searchCriteriaAnd.NestedCriteria)
						{
							SearchCriteriaCompare searchCriteriaCompare = searchCriteria2 as SearchCriteriaCompare;
							if (searchCriteriaCompare == null)
							{
								if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.WarningTrace))
								{
									ExTraceGlobals.MultiMailboxSearchTracer.TraceWarning<string, Guid, string>(43584, 0L, "Invalid SearchCriteria type found for request:{0} on mailbox:{1} in database:{2}, expected SearchCriteriaCompare, ignoring the current Criteria", searchMailboxInfo.CorrelationId.ToString(), searchMailboxInfo.MailboxGuid, base.Database.MdbName);
								}
							}
							else
							{
								ConstantColumn constantColumn = searchCriteriaCompare.Rhs as ConstantColumn;
								if (constantColumn == null)
								{
									if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.WarningTrace))
									{
										ExTraceGlobals.MultiMailboxSearchTracer.TraceWarning<string, Guid, string>(59968, 0L, "FolderRestriction SearchCriteria has incorrect RHS, RHS must be a ConstantColumn on request:{0} on mailbox:{1} in database:{2}, ignoring the current Criteria", searchMailboxInfo.CorrelationId.ToString(), searchMailboxInfo.MailboxGuid, base.Database.MdbName);
									}
								}
								else
								{
									byte[] array = constantColumn.Value as byte[];
									if (array == null || array.Length != 22)
									{
										if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.WarningTrace))
										{
											ExTraceGlobals.MultiMailboxSearchTracer.TraceWarning<string, Guid, string>(35392, 0L, "FolderRestriction SearchCriteria has invalid folderId value for request:{0} on mailbox:{1} in database:{2}.Expected a FID represented as ByteArray[22] ignoring the current Criteria", searchMailboxInfo.CorrelationId.ToString(), searchMailboxInfo.MailboxGuid, base.Database.MdbName);
										}
									}
									else
									{
										ExchangeId exchangeId = ExchangeId.CreateFrom22ByteArray(context, replidGuidMap, array);
										list.Add(Factory.CreateSearchCriteriaCompare(messageTable.FolderId, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(exchangeId.To26ByteArray())));
									}
								}
							}
						}
					}
					if (list.Count == 1)
					{
						result = list[0];
					}
					if (list.Count > 1)
					{
						result = Factory.CreateSearchCriteriaAnd(list.ToArray());
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcServerMultiMailboxSearchBase.CreateMailboxSearchCriteria");
				return result;
			}

			// Token: 0x06000147 RID: 327 RVA: 0x000094C4 File Offset: 0x000076C4
			protected override void CleanupResources(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.CleanupResources");
				if (this.MultiMailboxSearcher != null && this.MultiMailboxSearcher is IDisposable)
				{
					((IDisposable)this.MultiMailboxSearcher).Dispose();
					this.multiMailboxSearcher = null;
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.CleanupResources");
			}

			// Token: 0x06000148 RID: 328 RVA: 0x00009511 File Offset: 0x00007711
			protected void SetResponseByteArray(byte[] responseAsBytes)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.SetResponseByteArray");
				this.responseByteArray = responseAsBytes;
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.SetResponseByteArray");
			}

			// Token: 0x06000149 RID: 329 RVA: 0x00009530 File Offset: 0x00007730
			private static ErrorCode IsPropertyInFullTextIndex(uint errorLID, Guid mdbGuid, ExtendedPropertyColumn extendedColumn, ref StorePropTag nonFullTextPropTag)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchBase.IsPropertyInFullTextIndex");
				ErrorCode result = ErrorCode.NoError;
				FullTextIndexSchema.FullTextIndexInfo fullTextIndexInfo = null;
				if (extendedColumn != null && !FullTextIndexSchema.Current.IsPropertyInFullTextIndex(extendedColumn.StorePropTag.PropInfo.PropName, mdbGuid, out fullTextIndexInfo))
				{
					nonFullTextPropTag = extendedColumn.StorePropTag;
					result = ErrorCode.CreateMultiMailboxSearchNonFullTextSearch((LID)errorLID, nonFullTextPropTag.PropTag);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchBase.IsPropertyInFullTextIndex");
				return result;
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600014A RID: 330 RVA: 0x000095A3 File Offset: 0x000077A3
			internal byte[] ResponseAsByteArray
			{
				get
				{
					return this.responseByteArray;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600014B RID: 331 RVA: 0x000095AB File Offset: 0x000077AB
			// (set) Token: 0x0600014C RID: 332 RVA: 0x000095B3 File Offset: 0x000077B3
			protected MultiMailboxResponseBase Response
			{
				get
				{
					return this.searchResponse;
				}
				set
				{
					this.searchResponse = value;
				}
			}

			// Token: 0x040000F4 RID: 244
			private const int ExpectedFolderIdByteArrayLength = 22;

			// Token: 0x040000F5 RID: 245
			private Hookable<IMultiMailboxSearch> multiMailboxSearcher;

			// Token: 0x040000F6 RID: 246
			private MultiMailboxResponseBase searchResponse;

			// Token: 0x040000F7 RID: 247
			private byte[] responseByteArray;
		}

		// Token: 0x0200003D RID: 61
		internal class AdminRpcMultiMailboxSearch : AdminRpcServer.AdminRpcMultiMailboxSearchBase
		{
			// Token: 0x0600014D RID: 333 RVA: 0x000095BC File Offset: 0x000077BC
			internal AdminRpcMultiMailboxSearch(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequestByteArray, byte[] auxiliaryIn) : base(AdminMethod.EcMultiMailboxSearch, callerSecurityContext, mdbGuid, auxiliaryIn)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.ctor");
				this.searchRequest = MultiMailboxSearchRequest.DeSerialize(searchRequestByteArray);
				this.resultSet = new List<MultiMailboxSearchResult>(1);
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.ctor");
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600014E RID: 334 RVA: 0x0000962D File Offset: 0x0000782D
			internal static List<string> RequiredRefinersList
			{
				get
				{
					return AdminRpcServer.AdminRpcMultiMailboxSearch.requiredRefinerList;
				}
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00009634 File Offset: 0x00007834
			private static List<string> InitializeRefinerList()
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminMultiMailboxSearch.InitializeRefinerList");
				List<string> list = new List<string>(1);
				string fastPropertyNameOfColumn = AdminRpcServer.AdminRpcMultiMailboxSearchBase.GetFastPropertyNameOfColumn(PropTag.Message.MessageSize, Guid.Empty);
				ExAssert.RetailAssert(!string.IsNullOrEmpty(fastPropertyNameOfColumn), "MessageSize FastProperty Name cannot be empty.");
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(43408, 0L, "Selecting the following refiners {0}(FastPropertyName:{1})", PropTag.Message.MessageSize.DescriptiveName, fastPropertyNameOfColumn);
				}
				list.Add(fastPropertyNameOfColumn);
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminMultiMailboxSearch.InitializeRefinerList");
				return list;
			}

			// Token: 0x06000150 RID: 336 RVA: 0x000096B8 File Offset: 0x000078B8
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.EcValidateArguments");
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(59792, 0L, "eDiscovery search on database {0} base request validation failed.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.EcValidateArguments");
					return errorCode.Propagate((LID)49248U);
				}
				if (this.SearchRequest == null)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(36080, 0L, "eDiscovery search on database {0} request validation failed.Invalid or empty search request.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)46140U);
				}
				if (errorCode == ErrorCode.NoError && (this.SearchRequest.Restriction == null || this.SearchRequest.Restriction.Length == 0))
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(60656, 0L, "eDiscovery search on database {0} request validation failed.Invalid or empty restriction.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)37872U);
				}
				if (errorCode == ErrorCode.NoError && (this.SearchRequest.MailboxInfos == null || this.SearchRequest.MailboxInfos.Length == 0))
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(35216, 0L, "eDiscovery search on database {0} request validation failed.Invalid or empty Mailbox.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)54256U);
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.MailboxInfos.Length > 1)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(44272, 0L, "eDiscovery search on database {0} request validation failed. Request has multiple mailboxes to search for.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)41968U);
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.Paging == null)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(56560, 0L, "eDiscovery search on database {0} request validation failed. Invalid paging info.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)58352U);
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.Paging.PageSize <= 0)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(40176, 0L, "eDiscovery search on database {0} request validation failed. Invalid Page size specified.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)33776U);
				}
				if (errorCode == ErrorCode.NoError && string.IsNullOrEmpty(this.SearchRequest.Query))
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(64752, 0L, "eDiscovery search on database {0} request validation failed.Invalid or empty query in the request.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)62524U);
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.RefinersEnabled && this.SearchRequest.RefinerResultsTrimCount <= 0)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(48368, 0L, "eDiscovery search on database {0} request validation failed.Refiner validation failed.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxSearchRequest((LID)50160U);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.EcValidateArguments");
				return errorCode;
			}

			// Token: 0x06000151 RID: 337 RVA: 0x00009AA8 File Offset: 0x00007CA8
			protected override void UpdatePerfCounters(StorePerDatabasePerformanceCountersInstance perfInstance, long timeTaken, bool isFailed)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.UpdatePerfCounters");
				if (perfInstance != null)
				{
					perfInstance.TotalMultiMailboxPreviewSearches.Increment();
					perfInstance.AverageMultiMailboxPreviewSearchLatency.IncrementBy(timeTaken);
					perfInstance.AverageMultiMailboxPreviewSearchLatencyBase.Increment();
					if (isFailed)
					{
						perfInstance.MultiMailboxPreviewSearchesFailed.Increment();
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.UpdatePerfCounters");
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00009B00 File Offset: 0x00007D00
			protected override ErrorCode ExecuteRpc(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.ExecuteRpc");
				ErrorCode errorCode = this.InitializeAndValidateSearchRequest(context);
				if (errorCode != ErrorCode.NoError)
				{
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.ExecuteRpc");
					return errorCode.Propagate((LID)38464U);
				}
				string text = this.GenerateClauseforPagination(context);
				char c = (this.searchRequest.SortingOrder == Sorting.Ascending) ? '+' : '-';
				string text2 = string.Format("{0}{1} {0}[docid]", c, this.sortByPropertyName);
				SearchCriteria searchCriteria = null;
				context.InitializeMailboxExclusiveOperation(this.SearchRequest.MailboxInfos[0].MailboxGuid, ExecutionDiagnostics.OperationSource.AdminRpc, MapiContext.MailboxLockTimeout);
				bool commit = false;
				int mailboxNumber;
				try
				{
					errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceError<Guid, string>(55872L, "eDiscovery preview search on mailbox:{0} in database {1} failed. Mailbox state for this mailbox is invalid.", this.SearchRequest.MailboxInfos[0].MailboxGuid, base.Database.MdbName);
						}
						return ErrorCode.CreateMultiMailboxSearchMailboxNotFound((LID)47680U);
					}
					mailboxNumber = context.LockedMailboxState.MailboxNumber;
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						searchCriteria = base.CreateFolderRestrictionCriteria(context, mailbox.ReplidGuidMap, this.SearchRequest.MailboxInfos[0]);
					}
					commit = true;
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
				if (searchCriteria != null)
				{
					this.searchCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
					{
						this.searchCriteria,
						searchCriteria
					});
				}
				MultiMailboxSearchCriteria multiMailboxSearchCriteria = new MultiMailboxSearchCriteria(this.SearchRequest.CorrelationId, this.searchCriteria, this.SearchRequest.MailboxInfos[0].MailboxGuid, mailboxNumber, this.SearchRequest.Query, (this.SearchRequest.Paging != null) ? this.SearchRequest.Paging.PageSize : this.DefaultPageSize, text2, text);
				string text3 = multiMailboxSearchCriteria.SearchCriteria.ToString();
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation(51600, 0L, "eDiscovery search on database {0} for {1} mailboxes using the following paginationClause {2} and sortSpecification {3}", new object[]
					{
						base.Database.MdbName,
						this.SearchRequest.MailboxInfos.Length,
						text,
						text2
					});
				}
				base.MultiMailboxSearcher.RefinersList = AdminRpcServer.AdminRpcMultiMailboxSearch.RequiredRefinersList;
				IList<FullTextIndexRow> list = null;
				Dictionary<string, List<RefinersResultRow>> dictionary = null;
				KeywordStatsResultRow keywordStatsResultRow = null;
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(45456, 0L, "Executing eDiscovery search on database {0} for the following query {1}", base.Database.MdbName, text3);
				}
				errorCode = base.MultiMailboxSearcher.Search(context, multiMailboxSearchCriteria, out list, out keywordStatsResultRow, out dictionary);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, string>(61840, 0L, "Executing eDiscovery search on database {0} for the following query {1} failed with the error code {2}.", base.Database.MdbName, text3, errorCode.ToString());
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.ExecuteRpc");
					return errorCode.Propagate((LID)63040U);
				}
				if (list != null && list.Count > 0 && this.SearchRequest.RefinersEnabled && dictionary == null && ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.WarningTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceWarning<int, string>(64656, 0L, "MultiMailbox Search Query returned results(count={0}), but refiners result were not returned for query = {1}", list.Count, this.SearchRequest.Query);
				}
				long num = 0L;
				long num2 = 0L;
				if (keywordStatsResultRow != null)
				{
					num = keywordStatsResultRow.Count;
					num2 = (long)keywordStatsResultRow.Size;
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation(37264, 0L, "Executing eDiscovery search on database {0} for the following query {1} returned {2} hits accounting to {3} bytes result size.", new object[]
						{
							base.Database.MdbName,
							text3,
							num,
							num2
						});
					}
				}
				if (this.SearchRequest.RefinersEnabled && dictionary != null && dictionary.Count > 0)
				{
					Dictionary<string, List<MultiMailboxSearchRefinersResult>> refinersOutput = this.CreateMultiMailboxSearchRefinerResults(dictionary);
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(53648, 0L, "Creating Response with refiners data for eDiscovery search on database {0} for the following query {1}", base.Database.MdbName, text3);
					}
					base.Response = new MultiMailboxSearchResponse(refinersOutput, num, num2);
				}
				else
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(41360, 0L, "Creating Response with no refiners data for eDiscovery search on database {0} for the following query {1}", base.Database.MdbName, text3);
					}
					base.Response = new MultiMailboxSearchResponse(null, num, num2);
				}
				foreach (FullTextIndexRow fullTextIndexRow in list)
				{
					this.resultSet.Add(new MultiMailboxSearchResult(fullTextIndexRow.MailboxGuid, fullTextIndexRow.DocumentId, fullTextIndexRow.FastDocumentId));
				}
				base.Response.Results = this.resultSet.ToArray();
				if (this.resultSet.Count > 0)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(57744, 0L, "Setting the response with paging reference for eDiscovery search on database {0} for the following query {1}", base.Database.MdbName, text3);
					}
					((MultiMailboxSearchResponse)base.Response).PagingReference = new PageReference
					{
						PreviousPageReference = this.resultSet[0],
						NextPageReference = this.resultSet[this.resultSet.Count - 1]
					};
				}
				base.SetResponseByteArray(MultiMailboxSearchResponse.Serialize((MultiMailboxSearchResponse)base.Response));
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<int, string, string>(33168, 0L, "Serializing the response(size:{0}) for the eDiscovery search query on database {1} for the following query {2}", base.ResponseAsByteArray.Length, base.Database.MdbName, text3);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.ExecuteRpc");
				return errorCode;
			}

			// Token: 0x06000153 RID: 339 RVA: 0x0000A108 File Offset: 0x00008308
			private ErrorCode InitializeAndValidateSearchRequest(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.InitializeAndValidateSearchRequest");
				ErrorCode errorCode = ErrorCode.NoError;
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(38640, 0L, "eDiscovery search on database {0} request validation, Generating the SearchCriteria from the SearchRequest.", (base.Database != null) ? base.Database.MdbName : string.Empty);
				}
				errorCode = base.RequestToSearchCriteria(context, this.SearchRequest, this.SearchRequest.Restriction, out this.searchCriteria);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(51296, 0L, "eDiscovery search on database {0} request validation failed.Invalid/Incorrect query:\"{1}\".", (base.Database != null) ? base.Database.MdbName : string.Empty, this.searchRequest.Query);
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.InitializeAndValidateSearchRequest");
					return errorCode.Propagate((LID)34912U);
				}
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(55024, 0L, "eDiscovery search on database {0} request validation, validating the SearchCriteria for non full text index property.", (base.Database != null) ? base.Database.MdbName : string.Empty);
				}
				errorCode = base.ValidateSearchCriteria(context, this.searchCriteria, 43152U);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(48368, 0L, "eDiscovery search on database {0} request validation failed.Query has non full text index property.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.InitializeAndValidateSearchRequest");
					return errorCode.Propagate((LID)51952U);
				}
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(42736, 0L, "eDiscovery search on database {0} request validation, validating the sortBy Criteria.", (base.Database != null) ? base.Database.MdbName : string.Empty);
				}
				if (errorCode == ErrorCode.NoError && (this.SearchRequest.SortCriteria == null || this.SearchRequest.SortCriteria.Length == 0))
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(58608, 0L, "eDiscovery search on database {0} request validation failed.No SortCriteria specified.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateMultiMailboxSearchInvalidSortBy((LID)34032U);
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.InitializeAndValidateSearchRequest");
					return errorCode;
				}
				if (errorCode == ErrorCode.NoError)
				{
					errorCode = this.GetSortByPropertyFromRequest(context, out this.sortByPropertyName);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(48368, 0L, "eDiscovery search on database {0} request validation failed.Invalid SortBy property:{1} specified in the request", (base.Database != null) ? base.Database.MdbName : string.Empty, this.sortByPropertyName);
						}
						AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.InitializeAndValidateSearchRequest");
						return errorCode.Propagate((LID)62192U);
					}
				}
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(59120, 0L, "eDiscovery search on database {0} request validation, validating the pagination Criteria.", (base.Database != null) ? base.Database.MdbName : string.Empty);
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.Paging.Direction != PagingDirection.None)
				{
					if (this.SearchRequest.Paging.ReferenceItem == null)
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, string>(35568, 0L, "eDiscovery search query on database {0} for the following query {1} with paging direction:{2}, but reference item is null", (base.Database != null) ? base.Database.MdbName : string.Empty, this.SearchRequest.Query, (this.SearchRequest.Paging.Direction == PagingDirection.None) ? "none" : ((this.SearchRequest.Paging.Direction == PagingDirection.Next) ? "next" : "previous"));
						}
						errorCode = ErrorCode.CreateMultiMailboxSearchInvalidPagination((LID)56048U);
					}
					if (errorCode == ErrorCode.NoError && (this.SearchRequest.Paging.ReferenceItem.EqualsRestriction == null || this.SearchRequest.Paging.ReferenceItem.EqualsRestriction.Length == 0))
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(60144, 0L, "eDiscovery search on database {0} request validation failed.Invalid or empty PaginationRestriction", (base.Database != null) ? base.Database.MdbName : string.Empty);
						}
						errorCode = ErrorCode.CreateMultiMailboxSearchInvalidPagination((LID)43760U);
					}
					if (errorCode == ErrorCode.NoError && (this.SearchRequest.Paging.ReferenceItem.ComparisionRestriction == null || this.SearchRequest.Paging.ReferenceItem.ComparisionRestriction.Length == 0))
					{
						errorCode = ErrorCode.CreateMultiMailboxSearchInvalidPagination((LID)63728U);
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(61168, 0L, "eDiscovery search on database {0} request validation failed.Invalid or empty PaginationRestriction", (base.Database != null) ? base.Database.MdbName : string.Empty);
						}
					}
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.Paging.Direction != PagingDirection.None && this.SearchRequest.Paging.ReferenceItem != null)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(47344, 0L, "eDiscovery search on database {0} request validation, validating the pagination filter for non fulltext property", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = this.CreatePagingCriteriaFromRequest(context, out this.paginationEqualsSearchCriteria, out this.paginationCriteria);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)41712U);
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.InitializeAndValidateSearchRequest");
				return errorCode;
			}

			// Token: 0x06000154 RID: 340 RVA: 0x0000A6BC File Offset: 0x000088BC
			private ErrorCode GetSortByPropertyFromRequest(MapiContext context, out string sortByPropertyName)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.GetSortByPropertyFromRequest");
				sortByPropertyName = string.Empty;
				RestrictionExists restrictionExists = Restriction.Deserialize(context, this.SearchRequest.SortCriteria, null, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message) as RestrictionExists;
				ErrorCode result;
				if (restrictionExists != null)
				{
					sortByPropertyName = AdminRpcServer.AdminRpcMultiMailboxSearchBase.GetFastPropertyNameOfColumn(restrictionExists.PropertyTag, context.DatabaseGuid);
					if (!string.IsNullOrEmpty(sortByPropertyName))
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation(52976, 0L, "eDiscovery search query on database {0} for the following query {1} using {2} sort on sortBy property:{3}", new object[]
							{
								base.Database.MdbName,
								this.SearchRequest.Query,
								(this.SearchRequest.SortingOrder == Sorting.Ascending) ? "ascending" : "descending",
								sortByPropertyName
							});
						}
						result = ErrorCode.NoError;
					}
					else
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation(46832, 0L, "eDiscovery search query on database {0} for the following query {1} using {2} sort on non full text index property:{3}", new object[]
							{
								base.Database.MdbName,
								this.SearchRequest.Query,
								(this.SearchRequest.SortingOrder == Sorting.Ascending) ? "ascending" : "descending",
								restrictionExists.PropertyTag.DescriptiveName
							});
						}
						result = ErrorCode.CreateMultiMailboxSearchNonFullTextSortBy((LID)36592U, restrictionExists.PropertyTag.PropTag);
					}
				}
				else
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, string>(34544, 0L, "eDiscovery search query on database {0} for the following query {1} using {2} sort, has invalid sortBy restriction", base.Database.MdbName, this.SearchRequest.Query, (this.SearchRequest.SortingOrder == Sorting.Ascending) ? "ascending" : "descending");
					}
					result = ErrorCode.CreateMultiMailboxSearchInvalidSortBy((LID)50416U);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.GetSortByPropertyFromRequest");
				return result;
			}

			// Token: 0x06000155 RID: 341 RVA: 0x0000A88C File Offset: 0x00008A8C
			private ErrorCode CreatePagingCriteriaFromRequest(MapiContext context, out SearchCriteria paginationReferenceItemEqualsSearchCritieria, out SearchCriteria paginationSearchCriteria)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.CreatePagingCriteriaFromRequest");
				ErrorCode errorCode = ErrorCode.NoError;
				paginationReferenceItemEqualsSearchCritieria = null;
				paginationSearchCriteria = null;
				if (this.SearchRequest.Paging.Direction != PagingDirection.None && this.SearchRequest.Paging.ReferenceDocumentId > 0L)
				{
					errorCode = this.CreatePaginationSearchCriteriaFromReferenceItemRestriction(context, this.SearchRequest.Paging.ReferenceItem.EqualsRestriction, out paginationReferenceItemEqualsSearchCritieria);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)32832U);
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, ErrorCode>(47856, 0L, "eDiscovery search on database {0} for query:{1} coverting pagination reference item's equals filter restriction to search criteria failed with errorcode: {2}", (base.Database != null) ? base.Database.MdbName : string.Empty, this.SearchRequest.Query, errorCode);
						}
					}
					if (errorCode == ErrorCode.NoError)
					{
						errorCode = this.CreatePaginationSearchCriteriaFromReferenceItemRestriction(context, this.SearchRequest.Paging.ReferenceItem.ComparisionRestriction, out paginationSearchCriteria);
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)49216U);
							if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
							{
								ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, ErrorCode>(64240, 0L, "eDiscovery search on database {0} for query:{1} coverting pagination reference item's comparision filter restriction to search criteria failed with errorcode: {2}", (base.Database != null) ? base.Database.MdbName : string.Empty, this.SearchRequest.Query, errorCode);
							}
						}
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.CreatePagingCriteriaFromRequest");
				return errorCode;
			}

			// Token: 0x06000156 RID: 342 RVA: 0x0000AA04 File Offset: 0x00008C04
			private ErrorCode CreatePaginationSearchCriteriaFromReferenceItemRestriction(MapiContext context, byte[] serializedRestriction, out SearchCriteria paginationSearchCriteria)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.CreatePaginationSearchCriteriaFromReferenceItemRestriction");
				ErrorCode errorCode = ErrorCode.NoError;
				paginationSearchCriteria = null;
				if (serializedRestriction == null || serializedRestriction.Length == 0)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(50928, 0L, "eDiscovery search on database {0} for query:{1} pagination reference item's filter restriction is null or empty length.", (base.Database != null) ? base.Database.MdbName : string.Empty, this.SearchRequest.Query);
					}
					errorCode = ErrorCode.CreateMultiMailboxSearchInvalidPagination((LID)39664U);
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.CreatePaginationSearchCriteriaFromReferenceItemRestriction");
					return errorCode;
				}
				Restriction restriction = Restriction.Deserialize(context, serializedRestriction, null, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
				if (restriction != null)
				{
					SearchCriteria searchCriteria = restriction.ToSearchCriteria(base.Database, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
					errorCode = AdminRpcServer.AdminRpcMultiMailboxSearchBase.InspectAndFixSearchCriteria(42224U, context.DatabaseGuid, ref searchCriteria, (context.Culture == null) ? null : context.Culture.CompareInfo);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode = errorCode.Propagate((LID)48704U);
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, string>(54512, 0L, "eDiscovery search on database {0} for query:{1} has non FullText Index property in the pagination reference item equals filter:{2}.", (base.Database != null) ? base.Database.MdbName : string.Empty, this.SearchRequest.Query, (searchCriteria != null) ? searchCriteria.ToString() : string.Empty);
						}
						errorCode = ErrorCode.CreateMultiMailboxSearchNonFullTextPropertyInPagination((LID)38128U);
						AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.CreatePaginationSearchCriteriaFromReferenceItemRestriction");
						return errorCode;
					}
					paginationSearchCriteria = searchCriteria;
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, string>(59536, 0L, "eDiscovery search query on database {0} for the following query {1} using pagination criteria:{2}", base.Database.MdbName, this.SearchRequest.Query, paginationSearchCriteria.ToFqlString(FqlQueryGenerator.Options.LooseCheck, context.Culture));
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.CreatePaginationSearchCriteriaFromReferenceItemRestriction");
				return errorCode;
			}

			// Token: 0x06000157 RID: 343 RVA: 0x0000ABC4 File Offset: 0x00008DC4
			private Dictionary<string, List<MultiMailboxSearchRefinersResult>> CreateMultiMailboxSearchRefinerResults(Dictionary<string, List<RefinersResultRow>> refinersResults)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.CreateMultiMailboxSearchRefinerResults");
				Dictionary<string, List<MultiMailboxSearchRefinersResult>> dictionary = new Dictionary<string, List<MultiMailboxSearchRefinersResult>>(refinersResults.Count);
				foreach (string key in refinersResults.Keys)
				{
					List<RefinersResultRow> list = refinersResults[key];
					if (list != null && list.Count > 0)
					{
						if (list.Count > this.SearchRequest.RefinerResultsTrimCount)
						{
							list = list.Take(this.SearchRequest.RefinerResultsTrimCount).ToList<RefinersResultRow>();
						}
						List<MultiMailboxSearchRefinersResult> list2 = new List<MultiMailboxSearchRefinersResult>(list.Count);
						foreach (RefinersResultRow refinersResultRow in list)
						{
							list2.Add(new MultiMailboxSearchRefinersResult(refinersResultRow.EntryName, refinersResultRow.EntryCount));
						}
						dictionary.Add(key, list2);
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.CreateMultiMailboxSearchRefinerResults");
				return dictionary;
			}

			// Token: 0x06000158 RID: 344 RVA: 0x0000ACE4 File Offset: 0x00008EE4
			private string GenerateClauseforPagination(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearch.GenerateClauseforPagination");
				string text = string.Empty;
				if (this.SearchRequest.Paging != null && this.SearchRequest.Paging.Direction != PagingDirection.None && this.SearchRequest.Paging.ReferenceDocumentId > 0L && this.SearchRequest.Paging.ReferenceItem != null && this.paginationCriteria != null && this.paginationEqualsSearchCriteria != null)
				{
					string text2 = string.Empty;
					string text3 = string.Empty;
					if (this.SearchRequest.SortingOrder == Sorting.Ascending)
					{
						text2 = this.SearchRequest.Paging.ReferenceDocumentId.ToString();
						text3 = "max";
					}
					else
					{
						text2 = "min";
						text3 = this.SearchRequest.Paging.ReferenceDocumentId.ToString();
					}
					text = string.Format("and({0},or(and(documentid:range({1},{2},from=gt,to=lt),{3}),{4}))", new string[]
					{
						"{0}",
						text2,
						text3,
						this.paginationEqualsSearchCriteria.ToFqlString(FqlQueryGenerator.Options.LooseCheck, context.Culture),
						this.paginationCriteria.ToFqlString(FqlQueryGenerator.Options.LooseCheck, context.Culture)
					});
				}
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string, string>(49552, 0L, "eDiscovery search query on database {0} for the following query {1} following pagination clause specified {2}", base.Database.MdbName, this.SearchRequest.Query, text);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearch.GenerateClauseforPagination");
				return text;
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000159 RID: 345 RVA: 0x0000AE5B File Offset: 0x0000905B
			protected MultiMailboxSearchRequest SearchRequest
			{
				get
				{
					return this.searchRequest;
				}
			}

			// Token: 0x040000F8 RID: 248
			private const string PaginationClauseFormat = "and({0},or(and(documentid:range({1},{2},from=gt,to=lt),{3}),{4}))";

			// Token: 0x040000F9 RID: 249
			private const string SortSpecificationFormat = "{0}{1} {0}[docid]";

			// Token: 0x040000FA RID: 250
			private const char FastQueryAscendingSortOrder = '+';

			// Token: 0x040000FB RID: 251
			private const char FastQueryDescendingSortOrder = '-';

			// Token: 0x040000FC RID: 252
			private readonly int DefaultPageSize = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "MultiMailboxSearch.DefaultPageSize", 25);

			// Token: 0x040000FD RID: 253
			private readonly MultiMailboxSearchRequest searchRequest;

			// Token: 0x040000FE RID: 254
			private static List<string> requiredRefinerList = AdminRpcServer.AdminRpcMultiMailboxSearch.InitializeRefinerList();

			// Token: 0x040000FF RID: 255
			private List<MultiMailboxSearchResult> resultSet;

			// Token: 0x04000100 RID: 256
			private SearchCriteria searchCriteria;

			// Token: 0x04000101 RID: 257
			private string sortByPropertyName = string.Empty;

			// Token: 0x04000102 RID: 258
			private SearchCriteria paginationEqualsSearchCriteria;

			// Token: 0x04000103 RID: 259
			private SearchCriteria paginationCriteria;
		}

		// Token: 0x0200003E RID: 62
		internal class AdminRpcMultiMailboxSearchKeywordStats : AdminRpcServer.AdminRpcMultiMailboxSearchBase
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600015B RID: 347 RVA: 0x0000AE6F File Offset: 0x0000906F
			internal static List<string> RequiredRefinersList
			{
				get
				{
					return AdminRpcServer.AdminRpcMultiMailboxSearchKeywordStats.requiredRefinerList;
				}
			}

			// Token: 0x0600015C RID: 348 RVA: 0x0000AE76 File Offset: 0x00009076
			internal AdminRpcMultiMailboxSearchKeywordStats(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequestByteArray, byte[] auxiliaryIn) : base(AdminMethod.EcGetMultiMailboxSearchKeywordStats, callerSecurityContext, mdbGuid, auxiliaryIn)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchKeywordStats.ctor");
				this.searchRequest = MultiMailboxKeywordStatsRequest.DeSerialize(searchRequestByteArray);
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.ctor");
			}

			// Token: 0x0600015D RID: 349 RVA: 0x0000AEA4 File Offset: 0x000090A4
			private static List<string> InitializeRefinerList()
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchKeywordStats.InitializeRefinerList");
				List<string> list = new List<string>(1);
				string fastPropertyNameOfColumn = AdminRpcServer.AdminRpcMultiMailboxSearchBase.GetFastPropertyNameOfColumn(PropTag.Message.MessageSize, Guid.Empty);
				ExAssert.RetailAssert(!string.IsNullOrEmpty(fastPropertyNameOfColumn), "MessageSize FastProperty Name cannot be empty.");
				list.Add(fastPropertyNameOfColumn);
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.InitializeRefinerList");
				return list;
			}

			// Token: 0x0600015E RID: 350 RVA: 0x0000AEF8 File Offset: 0x000090F8
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchKeywordStats.EcValidateArguments");
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(49040, 0L, "eDiscovery keyword stats query on database {0} base request validation failed.", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.EcValidateArguments");
					return errorCode.Propagate((LID)53344U);
				}
				if (this.SearchRequest == null)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(65424, 0L, "eDiscovery keyword stats query on database {0} request validation failed.Search request is null", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxKeywordStatsRequest((LID)65084U);
				}
				if (errorCode == ErrorCode.NoError && (this.SearchRequest.MailboxInfos == null || this.SearchRequest.MailboxInfos.Length == 0))
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(52464, 0L, "eDiscovery keyword stats query on database {0} request validation failed.Search request is null", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxKeywordStatsRequest((LID)34960U);
				}
				if (errorCode == ErrorCode.NoError && this.SearchRequest.MailboxInfos.Length > 1)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(46320, 0L, "eDiscovery keyword stats query on database {0} request validation failed.Search request is null", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxKeywordStatsRequest((LID)51344U);
				}
				if (errorCode == ErrorCode.NoError && (this.SearchRequest.Keywords == null || this.SearchRequest.Keywords.Count == 0))
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string>(62704, 0L, "eDiscovery keyword stats query on database {0} request validation failed for Keywords property.Invalid or empty Keywords", (base.Database != null) ? base.Database.MdbName : string.Empty);
					}
					errorCode = ErrorCode.CreateInvalidMultiMailboxKeywordStatsRequest((LID)62448U);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchKeywordStats.EcValidateArguments");
				return errorCode;
			}

			// Token: 0x0600015F RID: 351 RVA: 0x0000B138 File Offset: 0x00009338
			protected override void UpdatePerfCounters(StorePerDatabasePerformanceCountersInstance perfInstance, long timeTaken, bool isFailed)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchKeywordStats.UpdatePerfCounters");
				if (perfInstance != null)
				{
					perfInstance.TotalMultiMailboxKeywordStatsSearches.Increment();
					perfInstance.AverageMultiMailboxKeywordStatsSearchLatency.IncrementBy(timeTaken);
					perfInstance.AverageMultiMailboxKeywordStatsSearchLatencyBase.Increment();
					if (isFailed)
					{
						perfInstance.MultiMailboxKeywordStatsSearchesFailed.Increment();
					}
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.UpdatePerfCounters");
			}

			// Token: 0x06000160 RID: 352 RVA: 0x0000B190 File Offset: 0x00009390
			protected override ErrorCode ExecuteRpc(MapiContext context)
			{
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Entering AdminRpcMultiMailboxSearchKeywordStats.ExecuteRpc");
				ErrorCode errorCode = ErrorCode.NoError;
				int num = Math.Min(this.SearchRequest.Keywords.Count, AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.MaxAllowedKeywordStatsQueryCount);
				List<MultiMailboxKeywordStatsResult> list = new List<MultiMailboxKeywordStatsResult>(num);
				IList<KeywordStatsResultRow> list2 = new List<KeywordStatsResultRow>(num);
				base.MultiMailboxSearcher.RefinersList = AdminRpcServer.AdminRpcMultiMailboxSearchKeywordStats.RequiredRefinersList;
				List<MultiMailboxSearchCriteria> list3 = new List<MultiMailboxSearchCriteria>(num);
				SearchCriteria searchCriteria = null;
				context.InitializeMailboxExclusiveOperation(this.SearchRequest.MailboxInfos[0].MailboxGuid, ExecutionDiagnostics.OperationSource.AdminRpc, MapiContext.MailboxLockTimeout);
				bool commit = false;
				int mailboxNumber;
				try
				{
					errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceError<Guid, string>(64064L, "eDiscovery preview search on mailbox:{0} in database {1} failed. Mailbox state for this mailbox is invalid.", this.SearchRequest.MailboxInfos[0].MailboxGuid, base.Database.MdbName);
						}
						return ErrorCode.CreateMultiMailboxSearchMailboxNotFound((LID)39488U);
					}
					mailboxNumber = context.LockedMailboxState.MailboxNumber;
					using (Mailbox mailbox = Mailbox.OpenMailbox(context, context.LockedMailboxState))
					{
						searchCriteria = base.CreateFolderRestrictionCriteria(context, mailbox.ReplidGuidMap, this.SearchRequest.MailboxInfos[0]);
					}
					commit = true;
				}
				finally
				{
					if (context.IsMailboxOperationStarted)
					{
						context.EndMailboxOperation(commit);
					}
				}
				for (int i = 0; i < num; i++)
				{
					KeyValuePair<string, byte[]> keyValuePair = this.SearchRequest.Keywords[i];
					if (string.IsNullOrEmpty(keyValuePair.Key) || keyValuePair.Value == null || keyValuePair.Value.Length == 0)
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, int, int>(40848, 0L, "eDiscovery Keyword stats search on database {0} for {1} mailboxes, found invalid request at row ", base.Database.MdbName, this.SearchRequest.MailboxInfos.Length, num + 1);
						}
						AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.ExecuteRpc");
						errorCode = ErrorCode.CreateInvalidMultiMailboxKeywordStatsRequest((LID)65040U);
						return errorCode;
					}
					SearchCriteria searchCriteria2 = null;
					errorCode = base.RequestToSearchCriteria(context, this.SearchRequest, keyValuePair.Value, out searchCriteria2);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, string>(61536, 0L, "eDiscovery search on database {0} request validation failed.Invalid/Incorrect query:\"{1}\".", (base.Database != null) ? base.Database.MdbName : string.Empty, keyValuePair.Key);
						}
						AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.ExecuteRpc");
						return errorCode.Propagate((LID)45152U);
					}
					errorCode = base.ValidateSearchCriteria(context, searchCriteria2, 55440U);
					if (errorCode != ErrorCode.NoError)
					{
						AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.ExecuteRpc");
						return errorCode.Propagate((LID)57440U);
					}
					if (searchCriteria != null)
					{
						searchCriteria2 = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
						{
							searchCriteria2,
							searchCriteria
						});
					}
					MultiMailboxSearchCriteria item = new MultiMailboxSearchCriteria(this.SearchRequest.CorrelationId, searchCriteria2, this.SearchRequest.MailboxInfos[0].MailboxGuid, mailboxNumber, keyValuePair.Key);
					list3.Add(item);
				}
				errorCode = base.MultiMailboxSearcher.GetKeywordStatistics(context, list3.ToArray(), out list2);
				if (errorCode != ErrorCode.NoError)
				{
					if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<string, int, string>(57232, 0L, "eDiscovery Keyword stats search on database {0} for {1} mailboxes failed with the error code {2}", base.Database.MdbName, this.SearchRequest.MailboxInfos.Length, errorCode.ToString());
					}
					AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.ExecuteRpc");
					return errorCode.Propagate((LID)32864U);
				}
				base.Response = new MultiMailboxKeywordStatsResponse();
				foreach (KeywordStatsResultRow keywordStatsResultRow in list2)
				{
					list.Add(new MultiMailboxKeywordStatsResult(keywordStatsResultRow.Keyword, keywordStatsResultRow.Count, (long)keywordStatsResultRow.Size));
				}
				base.Response.Results = list.ToArray();
				base.SetResponseByteArray(MultiMailboxKeywordStatsResponse.Serialize((MultiMailboxKeywordStatsResponse)base.Response));
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<int, string, int>(44944, 0L, "Serializing the response(size:{0}) for the eDiscovery keyword stats query on database {1} for the keyword count {2}", base.ResponseAsByteArray.Length, base.Database.MdbName, this.SearchRequest.Keywords.Count);
				}
				AdminRpcServer.AdminRpcMultiMailboxSearchBase.TraceFunction("Exiting AdminRpcMultiMailboxSearchKeywordStats.ExecuteRpc");
				return errorCode;
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000161 RID: 353 RVA: 0x0000B628 File Offset: 0x00009828
			protected MultiMailboxKeywordStatsRequest SearchRequest
			{
				get
				{
					return this.searchRequest;
				}
			}

			// Token: 0x04000104 RID: 260
			private readonly MultiMailboxKeywordStatsRequest searchRequest;

			// Token: 0x04000105 RID: 261
			private static List<string> requiredRefinerList = AdminRpcServer.AdminRpcMultiMailboxSearchKeywordStats.InitializeRefinerList();
		}

		// Token: 0x0200003F RID: 63
		internal class MultiMailboxSearchFactory
		{
			// Token: 0x06000163 RID: 355 RVA: 0x0000B63C File Offset: 0x0000983C
			protected MultiMailboxSearchFactory()
			{
				AdminRpcServer.TraceFunction("Entering MultiMailboxSearchFactory.ctor");
				this.maxSearchAllowed = (long)RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "eDiscoveryMaxSearches", 100);
				this.searchTimeOutInSeconds = (long)RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "eDiscoverySearchTimeout", 120);
				this.maxAllowedKeywordStatsQueryPerCall = (long)RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "eDiscoverySearchMaxQueryCount", 25);
				if (ExTraceGlobals.MultiMailboxSearchTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MultiMailboxSearchTracer.TraceInformation<long, long, long>(61328, 0L, "eDiscovery MultiMailboxSearch Factory Intialized with values MaxSearchAllowed: {0}, SearchTimeOutInSecond: {1}, KeywordStatsMaBatchSize: {2}", this.maxSearchAllowed, this.searchTimeOutInSeconds, this.maxAllowedKeywordStatsQueryPerCall);
				}
				AdminRpcServer.TraceFunction("Exiting MultiMailboxSearchFactory.ctor");
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000164 RID: 356 RVA: 0x0000B700 File Offset: 0x00009900
			internal static Hookable<AdminRpcServer.MultiMailboxSearchFactory> Instance
			{
				get
				{
					if (AdminRpcServer.MultiMailboxSearchFactory.instance == null)
					{
						AdminRpcServer.MultiMailboxSearchFactory.syncRoot = new object();
						using (LockManager.Lock(AdminRpcServer.MultiMailboxSearchFactory.syncRoot))
						{
							if (AdminRpcServer.MultiMailboxSearchFactory.instance == null)
							{
								AdminRpcServer.MultiMailboxSearchFactory.instance = Hookable<AdminRpcServer.MultiMailboxSearchFactory>.Create(true, new AdminRpcServer.MultiMailboxSearchFactory());
							}
						}
					}
					return AdminRpcServer.MultiMailboxSearchFactory.instance;
				}
			}

			// Token: 0x06000165 RID: 357 RVA: 0x0000B768 File Offset: 0x00009968
			internal IDisposable SetMultiMailboxSearchFactoryTestHook(AdminRpcServer.MultiMailboxSearchFactory factory)
			{
				AdminRpcServer.TraceFunction("Entering MultiMailboxSearchFactory.SetMultiMailboxSearchFactoryTestHook");
				IDisposable result = AdminRpcServer.MultiMailboxSearchFactory.Instance.SetTestHook(factory);
				AdminRpcServer.TraceFunction("Exiting MultiMailboxSearchFactory.SetMultiMailboxSearchFactoryTestHook");
				return result;
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000166 RID: 358 RVA: 0x0000B796 File Offset: 0x00009996
			internal TimeSpan SearchTimeOut
			{
				get
				{
					return TimeSpan.FromSeconds((double)AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.searchTimeOutInSeconds);
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000167 RID: 359 RVA: 0x0000B7AD File Offset: 0x000099AD
			// (set) Token: 0x06000168 RID: 360 RVA: 0x0000B7BE File Offset: 0x000099BE
			internal long SearchTimeOutInSeconds
			{
				get
				{
					return AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.searchTimeOutInSeconds;
				}
				set
				{
					Interlocked.Exchange(ref AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.searchTimeOutInSeconds, value);
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000169 RID: 361 RVA: 0x0000B7D6 File Offset: 0x000099D6
			internal long CurrentSearchCount
			{
				get
				{
					return AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.currentSearchCount;
				}
			}

			// Token: 0x0600016A RID: 362 RVA: 0x0000B7E7 File Offset: 0x000099E7
			internal void IncrementSearchCount()
			{
				Interlocked.Increment(ref AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.currentSearchCount);
			}

			// Token: 0x0600016B RID: 363 RVA: 0x0000B7FE File Offset: 0x000099FE
			internal void DecrementSearchCount()
			{
				Interlocked.Decrement(ref AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.currentSearchCount);
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600016C RID: 364 RVA: 0x0000B815 File Offset: 0x00009A15
			// (set) Token: 0x0600016D RID: 365 RVA: 0x0000B827 File Offset: 0x00009A27
			internal int MaxSearchesAllowed
			{
				get
				{
					return (int)AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.maxSearchAllowed;
				}
				set
				{
					Interlocked.Exchange(ref AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.maxSearchAllowed, (long)value);
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600016E RID: 366 RVA: 0x0000B840 File Offset: 0x00009A40
			// (set) Token: 0x0600016F RID: 367 RVA: 0x0000B852 File Offset: 0x00009A52
			internal int MaxAllowedKeywordStatsQueryCount
			{
				get
				{
					return (int)AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.maxAllowedKeywordStatsQueryPerCall;
				}
				set
				{
					Interlocked.Exchange(ref AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.maxAllowedKeywordStatsQueryPerCall, (long)value);
				}
			}

			// Token: 0x06000170 RID: 368 RVA: 0x0000B86C File Offset: 0x00009A6C
			internal bool IsMaxSearchCountReached()
			{
				long num = AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.CurrentSearchCount;
				Interlocked.Increment(ref num);
				return num >= (long)AdminRpcServer.MultiMailboxSearchFactory.Instance.Value.MaxSearchesAllowed;
			}

			// Token: 0x04000106 RID: 262
			private static object syncRoot = new object();

			// Token: 0x04000107 RID: 263
			private static Hookable<AdminRpcServer.MultiMailboxSearchFactory> instance;

			// Token: 0x04000108 RID: 264
			private long currentSearchCount;

			// Token: 0x04000109 RID: 265
			private long maxSearchAllowed;

			// Token: 0x0400010A RID: 266
			private long searchTimeOutInSeconds;

			// Token: 0x0400010B RID: 267
			private long maxAllowedKeywordStatsQueryPerCall;
		}

		// Token: 0x02000040 RID: 64
		internal abstract class AdminUserInfoBase : AdminRpc
		{
			// Token: 0x06000172 RID: 370 RVA: 0x0000B8B3 File Offset: 0x00009AB3
			protected AdminUserInfoBase(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid mdbGuid, AdminRpc.ExpectedDatabaseState expectedDatabaseState, byte[] auxiliaryIn) : base(methodId, callerSecurityContext, new Guid?(mdbGuid), expectedDatabaseState, auxiliaryIn)
			{
			}

			// Token: 0x06000173 RID: 371 RVA: 0x0000B8C8 File Offset: 0x00009AC8
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				if (!DefaultSettings.Get.UserInformationIsEnabled)
				{
					return ErrorCode.CreateNotSupported((LID)57932U);
				}
				ErrorCode errorCode = base.EcValidateArguments(context);
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode.Propagate((LID)48572U);
				}
				if (context.ClientType != ClientType.ADDriver)
				{
					return ErrorCode.CreateNoAccess((LID)37964U);
				}
				return errorCode;
			}
		}

		// Token: 0x02000041 RID: 65
		internal class AdminCreateUserInfo50 : AdminRpcServer.AdminUserInfoBase
		{
			// Token: 0x06000174 RID: 372 RVA: 0x0000B933 File Offset: 0x00009B33
			internal AdminCreateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, byte[] auxiliaryIn) : base(AdminMethod.EcCreateUserInfo50, callerSecurityContext, mdbGuid, AdminRpc.ExpectedDatabaseState.OnlineActive, auxiliaryIn)
			{
				this.userInfoGuid = userInfoGuid;
				this.properties = properties;
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000B954 File Offset: 0x00009B54
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				Properties initialProperties = AdminRpcServer.ParseProperties(this.properties, null);
				UserInformation.Create(context, this.userInfoGuid, initialProperties);
				return ErrorCode.NoError;
			}

			// Token: 0x0400010C RID: 268
			private readonly Guid userInfoGuid;

			// Token: 0x0400010D RID: 269
			private readonly byte[] properties;
		}

		// Token: 0x02000042 RID: 66
		internal class AdminReadUserInfo50 : AdminRpcServer.AdminUserInfoBase
		{
			// Token: 0x06000176 RID: 374 RVA: 0x0000B980 File Offset: 0x00009B80
			internal AdminReadUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, uint[] propertyTags, byte[] auxiliaryIn) : base(AdminMethod.EcReadUserInfo50, callerSecurityContext, mdbGuid, AdminRpc.ExpectedDatabaseState.AnyAttachedState, auxiliaryIn)
			{
				this.userInfoGuid = userInfoGuid;
				this.propertyTags = propertyTags;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B99E File Offset: 0x00009B9E
			public ArraySegment<byte> Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x06000178 RID: 376 RVA: 0x0000B9A6 File Offset: 0x00009BA6
			protected override ErrorCode EcCheckPermissions(MapiContext context)
			{
				if (context.SecurityContext.IsAuthenticated && context.SecurityContext.UserSid.IsWellKnown(WellKnownSidType.NetworkServiceSid))
				{
					return ErrorCode.NoError;
				}
				return base.EcCheckPermissions(context);
			}

			// Token: 0x06000179 RID: 377 RVA: 0x0000B9D8 File Offset: 0x00009BD8
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				Properties properties = UserInformation.Read(context, this.userInfoGuid, LegacyHelper.ConvertFromLegacyPropTags(this.propertyTags, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.UserInfo, null, false));
				int num = 0;
				int num2 = AdminRpcParseFormat.SetValues(null, ref num, 0, properties);
				BufferPoolCollection.BufferSize bufferSize;
				byte[] array;
				if (BufferPoolCollection.AutoCleanupCollection.TryMatchBufferSize(num2, out bufferSize))
				{
					BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize);
					array = bufferPool.Acquire();
				}
				else
				{
					array = new byte[num2];
				}
				num = 0;
				AdminRpcParseFormat.SetValues(array, ref num, num2, properties);
				this.result = new ArraySegment<byte>(array, 0, num2);
				return ErrorCode.NoError;
			}

			// Token: 0x0600017A RID: 378 RVA: 0x0000BA5C File Offset: 0x00009C5C
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode first = base.EcValidateArguments(context);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)48572U);
				}
				if (this.propertyTags == null || this.propertyTags.Length == 0)
				{
					return ErrorCode.CreateInvalidParameter((LID)42428U);
				}
				return first;
			}

			// Token: 0x0400010E RID: 270
			private readonly Guid userInfoGuid;

			// Token: 0x0400010F RID: 271
			private readonly uint[] propertyTags;

			// Token: 0x04000110 RID: 272
			private ArraySegment<byte> result;
		}

		// Token: 0x02000043 RID: 67
		internal class AdminUpdateUserInfo50 : AdminRpcServer.AdminUserInfoBase
		{
			// Token: 0x0600017B RID: 379 RVA: 0x0000BAB3 File Offset: 0x00009CB3
			internal AdminUpdateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, uint[] deletePropertyTags, byte[] auxiliaryIn) : base(AdminMethod.EcUpdateUserInfo50, callerSecurityContext, mdbGuid, AdminRpc.ExpectedDatabaseState.OnlineActive, auxiliaryIn)
			{
				this.userInfoGuid = userInfoGuid;
				this.properties = properties;
				this.deletePropertyTags = deletePropertyTags;
			}

			// Token: 0x0600017C RID: 380 RVA: 0x0000BADC File Offset: 0x00009CDC
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				Properties properties = AdminRpcServer.ParseProperties(this.properties, this.deletePropertyTags);
				UserInformation.Update(context, this.userInfoGuid, properties);
				return ErrorCode.NoError;
			}

			// Token: 0x04000111 RID: 273
			private readonly Guid userInfoGuid;

			// Token: 0x04000112 RID: 274
			private readonly byte[] properties;

			// Token: 0x04000113 RID: 275
			private readonly uint[] deletePropertyTags;
		}

		// Token: 0x02000044 RID: 68
		internal class AdminDeleteUserInfo50 : AdminRpcServer.AdminUserInfoBase
		{
			// Token: 0x0600017D RID: 381 RVA: 0x0000BB0D File Offset: 0x00009D0D
			internal AdminDeleteUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] auxiliaryIn) : base(AdminMethod.EcDeleteUserInfo50, callerSecurityContext, mdbGuid, AdminRpc.ExpectedDatabaseState.OnlineActive, auxiliaryIn)
			{
				this.userInfoGuid = userInfoGuid;
			}

			// Token: 0x0600017E RID: 382 RVA: 0x0000BB23 File Offset: 0x00009D23
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				UserInformation.Delete(context, this.userInfoGuid);
				return ErrorCode.NoError;
			}

			// Token: 0x04000114 RID: 276
			private readonly Guid userInfoGuid;
		}

		// Token: 0x02000045 RID: 69
		internal class AdminRpcStoreIntegrityCheck : AdminRpc
		{
			// Token: 0x0600017F RID: 383 RVA: 0x0000BB3C File Offset: 0x00009D3C
			internal AdminRpcStoreIntegrityCheck(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] taskIds, byte[] auxiliaryIn) : base(AdminMethod.EcAdminISIntegCheck50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.OnlineActive, auxiliaryIn)
			{
				this.mailboxGuid = mailboxGuid;
				this.flags = (IntegrityCheckRequestFlags)flags;
				this.taskIds = (from x in taskIds
				select (TaskId)x).ToArray<TaskId>();
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000180 RID: 384 RVA: 0x0000BB99 File Offset: 0x00009D99
			public Guid RequestGuid
			{
				get
				{
					return this.requestGuid;
				}
			}

			// Token: 0x06000181 RID: 385 RVA: 0x0000BBA4 File Offset: 0x00009DA4
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				errorCode = base.EcValidateArguments(context);
				if (errorCode != ErrorCode.NoError)
				{
					errorCode.Propagate((LID)41576U);
				}
				return errorCode;
			}

			// Token: 0x06000182 RID: 386 RVA: 0x0000BBE0 File Offset: 0x00009DE0
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				Properties[] array = null;
				Guid a = JobBuilder.BuildAndSchedule(context, this.mailboxGuid, this.flags, this.taskIds, null, ref array);
				if (a == Guid.Empty)
				{
					return ErrorCode.CreateInvalidParameter((LID)62556U);
				}
				this.requestGuid = a;
				return ErrorCode.NoError;
			}

			// Token: 0x04000115 RID: 277
			private readonly Guid mailboxGuid;

			// Token: 0x04000116 RID: 278
			private readonly IntegrityCheckRequestFlags flags;

			// Token: 0x04000117 RID: 279
			private readonly TaskId[] taskIds;

			// Token: 0x04000118 RID: 280
			private Guid requestGuid;
		}

		// Token: 0x02000046 RID: 70
		internal class AdminRpcStoreIntegrityCheckEx : AdminRpc
		{
			// Token: 0x06000184 RID: 388 RVA: 0x0000BC34 File Offset: 0x00009E34
			internal AdminRpcStoreIntegrityCheckEx(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint operation, byte[] request, byte[] auxiliaryIn) : base(AdminMethod.EcAdminIntegrityCheckEx50, callerSecurityContext, new Guid?(mdbGuid), AdminRpc.ExpectedDatabaseState.OnlineActive, auxiliaryIn)
			{
				this.mailboxGuid = mailboxGuid;
				this.operation = (Operation)operation;
				this.request = request;
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000185 RID: 389 RVA: 0x0000BC5F File Offset: 0x00009E5F
			public byte[] Response
			{
				get
				{
					return this.response;
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000186 RID: 390 RVA: 0x0000BC67 File Offset: 0x00009E67
			internal override int OperationDetail
			{
				get
				{
					return (int)(this.operation + 3000U);
				}
			}

			// Token: 0x06000187 RID: 391 RVA: 0x0000BC78 File Offset: 0x00009E78
			protected override ErrorCode EcValidateArguments(MapiContext context)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				errorCode = base.EcValidateArguments(context);
				if (errorCode != ErrorCode.NoError)
				{
					errorCode.Propagate((LID)40284U);
				}
				else
				{
					errorCode = AdminRpcParseFormat.ParseIntegrityCheckRequest(this.request, out this.flags, out this.requestGuid, out this.taskIds, out this.propTags);
					if (errorCode != ErrorCode.NoError)
					{
						errorCode.Propagate((LID)56668U);
					}
					else if (!Enum.IsDefined(typeof(Operation), this.operation))
					{
						errorCode = ErrorCode.CreateInvalidParameter((LID)60764U);
					}
					else if (this.operation == Operation.CreateJob && this.requestGuid != Guid.Empty)
					{
						errorCode = ErrorCode.CreateInvalidParameter((LID)36188U);
					}
				}
				return errorCode;
			}

			// Token: 0x06000188 RID: 392 RVA: 0x0000BD58 File Offset: 0x00009F58
			protected override ErrorCode EcExecuteRpc(MapiContext context)
			{
				ErrorCode noError = ErrorCode.NoError;
				this.response = null;
				switch (this.operation)
				{
				case Operation.CreateJob:
					return this.CreateIntegrityCheckJob(context);
				case Operation.QueryJob:
					return this.QueryIntegrityCheckJob(context);
				case Operation.RemoveJob:
					return this.RemoveIntegrityCheckJob(context);
				case Operation.ExecuteJob:
					return this.ExecuteIntegrityCheckJob(context);
				}
				return ErrorCode.CreateInvalidParameter((LID)46428U);
			}

			// Token: 0x06000189 RID: 393 RVA: 0x0000BDD4 File Offset: 0x00009FD4
			private ErrorCode CreateIntegrityCheckJob(MapiContext context)
			{
				Properties[] rows = null;
				Guid a = JobBuilder.BuildAndSchedule(context, this.mailboxGuid, (IntegrityCheckRequestFlags)this.flags, this.taskIds, this.propTags, ref rows);
				if (a == Guid.Empty)
				{
					return ErrorCode.CreateInvalidParameter((LID)37980U);
				}
				int num = 0;
				int num2 = AdminRpcParseFormat.SerializeIntegrityCheckResponse(null, ref num, 0, rows);
				this.response = new byte[num2];
				num = 0;
				AdminRpcParseFormat.SerializeIntegrityCheckResponse(this.response, ref num, num2, rows);
				return ErrorCode.NoError;
			}

			// Token: 0x0600018A RID: 394 RVA: 0x0000BE54 File Offset: 0x0000A054
			private ErrorCode QueryIntegrityCheckJob(MapiContext context)
			{
				List<Properties> list = new List<Properties>();
				IEnumerable<IntegrityCheckJob> enumerable = null;
				if (this.flags == 0)
				{
					if (this.requestGuid != Guid.Empty)
					{
						enumerable = InMemoryJobStorage.Instance(context.Database).GetJobsByRequestGuid(this.requestGuid);
					}
					else if (this.mailboxGuid != Guid.Empty)
					{
						enumerable = InMemoryJobStorage.Instance(context.Database).GetJobsByMailboxGuid(this.mailboxGuid);
					}
					else
					{
						enumerable = InMemoryJobStorage.Instance(context.Database).GetAllJobs();
					}
				}
				else if (this.flags == 1 || this.flags == 2)
				{
					JobPriority priority = JobPriority.Normal;
					RequiredMaintenanceResourceType requiredMaintenanceResourceType = RequiredMaintenanceResourceType.StoreOnlineIntegrityCheck;
					if (this.flags == 2)
					{
						priority = JobPriority.Low;
						requiredMaintenanceResourceType = RequiredMaintenanceResourceType.StoreScheduledIntegrityCheck;
					}
					AssistantActivityMonitor.Instance(context.Database).UpdateAssistantActivityState(requiredMaintenanceResourceType, true);
					enumerable = JobScheduler.Instance(context.Database).GetReadyJobs(priority);
				}
				else if (this.flags == 4)
				{
					IntegrityCheckJob job = InMemoryJobStorage.Instance(context.Database).GetJob(this.requestGuid);
					if (job != null)
					{
						enumerable = new IntegrityCheckJob[]
						{
							job
						};
					}
				}
				if (enumerable != null)
				{
					foreach (IntegrityCheckJob integrityCheckJob in enumerable)
					{
						list.Add(integrityCheckJob.GetProperties(this.propTags));
					}
				}
				if (list.Count > 0)
				{
					int num = 0;
					int num2 = AdminRpcParseFormat.SerializeIntegrityCheckResponse(null, ref num, 0, list);
					this.response = new byte[num2];
					num = 0;
					AdminRpcParseFormat.SerializeIntegrityCheckResponse(this.response, ref num, num2, list);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x0600018B RID: 395 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
			private ErrorCode RemoveIntegrityCheckJob(MapiContext context)
			{
				IntegrityCheckJob job = InMemoryJobStorage.Instance(context.Database).GetJob(this.requestGuid);
				if (job != null)
				{
					JobScheduler.Instance(context.Database).RemoveJob(job);
					InMemoryJobStorage.Instance(context.Database).RemoveJob(this.requestGuid);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x0600018C RID: 396 RVA: 0x0000C044 File Offset: 0x0000A244
			private ErrorCode ExecuteIntegrityCheckJob(MapiContext context)
			{
				List<Properties> list = new List<Properties>();
				if ((this.flags & 1) != 0 || (this.flags & 2) != 0)
				{
					RequiredMaintenanceResourceType requiredMaintenanceResourceType = RequiredMaintenanceResourceType.StoreOnlineIntegrityCheck;
					if ((this.flags & 2) != 0)
					{
						requiredMaintenanceResourceType = RequiredMaintenanceResourceType.StoreScheduledIntegrityCheck;
					}
					AssistantActivityMonitor.Instance(context.Database).UpdateAssistantActivityState(requiredMaintenanceResourceType, false);
				}
				IntegrityCheckJob job = InMemoryJobStorage.Instance(context.Database).GetJob(this.requestGuid);
				if (job != null)
				{
					JobScheduler.Instance(context.Database).ExecuteJob(context, job);
					list.Add(job.GetProperties(this.propTags));
				}
				if (list.Count > 0)
				{
					int num = 0;
					int num2 = AdminRpcParseFormat.SerializeIntegrityCheckResponse(null, ref num, 0, list);
					this.response = new byte[num2];
					num = 0;
					AdminRpcParseFormat.SerializeIntegrityCheckResponse(this.response, ref num, num2, list);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x0400011A RID: 282
			private readonly Guid mailboxGuid;

			// Token: 0x0400011B RID: 283
			private readonly Operation operation;

			// Token: 0x0400011C RID: 284
			private byte[] request;

			// Token: 0x0400011D RID: 285
			private byte[] response;

			// Token: 0x0400011E RID: 286
			private int flags;

			// Token: 0x0400011F RID: 287
			private Guid requestGuid;

			// Token: 0x04000120 RID: 288
			private TaskId[] taskIds;

			// Token: 0x04000121 RID: 289
			private StorePropTag[] propTags;
		}
	}
}

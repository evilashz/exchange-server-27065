using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000006 RID: 6
	public struct ErrorCode : IEquatable<ErrorCode>
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public static ErrorCode CreateStoreTestFailure(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.StoreTestFailure);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public static ErrorCode CreateStoreTestFailure(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.StoreTestFailure, propTag);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public static ErrorCode CreateUnknownUser(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnknownUser);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002AD1 File Offset: 0x00000CD1
		public static ErrorCode CreateUnknownUser(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnknownUser, propTag);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002ADF File Offset: 0x00000CDF
		public static ErrorCode CreateDatabaseRolledBack(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DatabaseRolledBack);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002AEC File Offset: 0x00000CEC
		public static ErrorCode CreateDatabaseRolledBack(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DatabaseRolledBack, propTag);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002AFA File Offset: 0x00000CFA
		public static ErrorCode CreateDatabaseBadVersion(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DatabaseBadVersion);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002B07 File Offset: 0x00000D07
		public static ErrorCode CreateDatabaseBadVersion(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DatabaseBadVersion, propTag);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002B15 File Offset: 0x00000D15
		public static ErrorCode CreateDatabaseError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DatabaseError);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002B22 File Offset: 0x00000D22
		public static ErrorCode CreateDatabaseError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DatabaseError, propTag);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002B30 File Offset: 0x00000D30
		public static ErrorCode CreateInvalidCollapseState(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidCollapseState);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002B3D File Offset: 0x00000D3D
		public static ErrorCode CreateInvalidCollapseState(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidCollapseState, propTag);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002B4B File Offset: 0x00000D4B
		public static ErrorCode CreateNoDeleteSubmitMessage(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoDeleteSubmitMessage);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002B58 File Offset: 0x00000D58
		public static ErrorCode CreateNoDeleteSubmitMessage(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoDeleteSubmitMessage, propTag);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002B66 File Offset: 0x00000D66
		public static ErrorCode CreateRecoveryMDBMismatch(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RecoveryMDBMismatch);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B73 File Offset: 0x00000D73
		public static ErrorCode CreateRecoveryMDBMismatch(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RecoveryMDBMismatch, propTag);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B81 File Offset: 0x00000D81
		public static ErrorCode CreateSearchFolderScopeViolation(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SearchFolderScopeViolation);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B8E File Offset: 0x00000D8E
		public static ErrorCode CreateSearchFolderScopeViolation(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SearchFolderScopeViolation, propTag);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B9C File Offset: 0x00000D9C
		public static ErrorCode CreateSearchEvaluationInProgress(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SearchEvaluationInProgress);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public static ErrorCode CreateSearchEvaluationInProgress(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SearchEvaluationInProgress, propTag);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002BB7 File Offset: 0x00000DB7
		public static ErrorCode CreateNestedSearchChainTooDeep(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NestedSearchChainTooDeep);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static ErrorCode CreateNestedSearchChainTooDeep(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NestedSearchChainTooDeep, propTag);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public static ErrorCode CreateCorruptSearchScope(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CorruptSearchScope);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BDF File Offset: 0x00000DDF
		public static ErrorCode CreateCorruptSearchScope(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CorruptSearchScope, propTag);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002BED File Offset: 0x00000DED
		public static ErrorCode CreateCorruptSearchBacklink(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CorruptSearchBacklink);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002BFA File Offset: 0x00000DFA
		public static ErrorCode CreateCorruptSearchBacklink(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CorruptSearchBacklink, propTag);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002C08 File Offset: 0x00000E08
		public static ErrorCode CreateGlobalCounterRangeExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.GlobalCounterRangeExceeded);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C15 File Offset: 0x00000E15
		public static ErrorCode CreateGlobalCounterRangeExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.GlobalCounterRangeExceeded, propTag);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C23 File Offset: 0x00000E23
		public static ErrorCode CreateCorruptMidsetDeleted(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CorruptMidsetDeleted);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C30 File Offset: 0x00000E30
		public static ErrorCode CreateCorruptMidsetDeleted(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CorruptMidsetDeleted, propTag);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C3E File Offset: 0x00000E3E
		public static ErrorCode CreateRpcFormat(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RpcFormat);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C4B File Offset: 0x00000E4B
		public static ErrorCode CreateRpcFormat(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RpcFormat, propTag);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C59 File Offset: 0x00000E59
		public static ErrorCode CreateQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.QuotaExceeded);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C66 File Offset: 0x00000E66
		public static ErrorCode CreateQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.QuotaExceeded, propTag);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C74 File Offset: 0x00000E74
		public static ErrorCode CreateMaxSubmissionExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MaxSubmissionExceeded);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C81 File Offset: 0x00000E81
		public static ErrorCode CreateMaxSubmissionExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MaxSubmissionExceeded, propTag);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C8F File Offset: 0x00000E8F
		public static ErrorCode CreateMaxAttachmentExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MaxAttachmentExceeded);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C9C File Offset: 0x00000E9C
		public static ErrorCode CreateMaxAttachmentExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MaxAttachmentExceeded, propTag);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CAA File Offset: 0x00000EAA
		public static ErrorCode CreateShutoffQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ShutoffQuotaExceeded);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public static ErrorCode CreateShutoffQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ShutoffQuotaExceeded, propTag);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public static ErrorCode CreateMaxObjectsExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MaxObjectsExceeded);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public static ErrorCode CreateMaxObjectsExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MaxObjectsExceeded, propTag);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static ErrorCode CreateMessagePerFolderCountReceiveQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MessagePerFolderCountReceiveQuotaExceeded);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002CED File Offset: 0x00000EED
		public static ErrorCode CreateMessagePerFolderCountReceiveQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MessagePerFolderCountReceiveQuotaExceeded, propTag);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002CFB File Offset: 0x00000EFB
		public static ErrorCode CreateFolderHierarchyChildrenCountReceiveQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FolderHierarchyChildrenCountReceiveQuotaExceeded);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D08 File Offset: 0x00000F08
		public static ErrorCode CreateFolderHierarchyChildrenCountReceiveQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FolderHierarchyChildrenCountReceiveQuotaExceeded, propTag);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D16 File Offset: 0x00000F16
		public static ErrorCode CreateFolderHierarchyDepthReceiveQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FolderHierarchyDepthReceiveQuotaExceeded);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D23 File Offset: 0x00000F23
		public static ErrorCode CreateFolderHierarchyDepthReceiveQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FolderHierarchyDepthReceiveQuotaExceeded, propTag);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D31 File Offset: 0x00000F31
		public static ErrorCode CreateDynamicSearchFoldersPerScopeCountReceiveQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D3E File Offset: 0x00000F3E
		public static ErrorCode CreateDynamicSearchFoldersPerScopeCountReceiveQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded, propTag);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static ErrorCode CreateFolderHierarchySizeReceiveQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FolderHierarchySizeReceiveQuotaExceeded);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D59 File Offset: 0x00000F59
		public static ErrorCode CreateFolderHierarchySizeReceiveQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FolderHierarchySizeReceiveQuotaExceeded, propTag);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D67 File Offset: 0x00000F67
		public static ErrorCode CreateNotVisible(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotVisible);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D74 File Offset: 0x00000F74
		public static ErrorCode CreateNotVisible(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotVisible, propTag);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D82 File Offset: 0x00000F82
		public static ErrorCode CreateNotExpanded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotExpanded);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D8F File Offset: 0x00000F8F
		public static ErrorCode CreateNotExpanded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotExpanded, propTag);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D9D File Offset: 0x00000F9D
		public static ErrorCode CreateNotCollapsed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotCollapsed);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DAA File Offset: 0x00000FAA
		public static ErrorCode CreateNotCollapsed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotCollapsed, propTag);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public static ErrorCode CreateLeaf(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Leaf);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DC5 File Offset: 0x00000FC5
		public static ErrorCode CreateLeaf(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Leaf, propTag);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public static ErrorCode CreateMessageCycle(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MessageCycle);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public static ErrorCode CreateMessageCycle(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MessageCycle, propTag);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DEE File Offset: 0x00000FEE
		public static ErrorCode CreateRejected(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Rejected);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DFB File Offset: 0x00000FFB
		public static ErrorCode CreateRejected(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Rejected, propTag);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E09 File Offset: 0x00001009
		public static ErrorCode CreateUnknownMailbox(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnknownMailbox);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E16 File Offset: 0x00001016
		public static ErrorCode CreateUnknownMailbox(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnknownMailbox, propTag);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E24 File Offset: 0x00001024
		public static ErrorCode CreateDisabledMailbox(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DisabledMailbox);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E31 File Offset: 0x00001031
		public static ErrorCode CreateDisabledMailbox(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DisabledMailbox, propTag);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E3F File Offset: 0x0000103F
		public static ErrorCode CreateAdUnavailable(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.AdUnavailable);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E4C File Offset: 0x0000104C
		public static ErrorCode CreateAdUnavailable(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.AdUnavailable, propTag);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E5A File Offset: 0x0000105A
		public static ErrorCode CreateADPropertyError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ADPropertyError);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E67 File Offset: 0x00001067
		public static ErrorCode CreateADPropertyError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ADPropertyError, propTag);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E75 File Offset: 0x00001075
		public static ErrorCode CreateRpcServerTooBusy(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RpcServerTooBusy);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E82 File Offset: 0x00001082
		public static ErrorCode CreateRpcServerTooBusy(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RpcServerTooBusy, propTag);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E90 File Offset: 0x00001090
		public static ErrorCode CreateRpcServerUnavailable(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RpcServerUnavailable);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E9D File Offset: 0x0000109D
		public static ErrorCode CreateRpcServerUnavailable(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RpcServerUnavailable, propTag);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002EAB File Offset: 0x000010AB
		public static ErrorCode CreateEventsDeleted(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.EventsDeleted);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EB8 File Offset: 0x000010B8
		public static ErrorCode CreateEventsDeleted(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.EventsDeleted, propTag);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EC6 File Offset: 0x000010C6
		public static ErrorCode CreateMaxPoolExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MaxPoolExceeded);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002ED3 File Offset: 0x000010D3
		public static ErrorCode CreateMaxPoolExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MaxPoolExceeded, propTag);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EE1 File Offset: 0x000010E1
		public static ErrorCode CreateEventNotFound(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.EventNotFound);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EEE File Offset: 0x000010EE
		public static ErrorCode CreateEventNotFound(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.EventNotFound, propTag);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EFC File Offset: 0x000010FC
		public static ErrorCode CreateInvalidPool(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidPool);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F09 File Offset: 0x00001109
		public static ErrorCode CreateInvalidPool(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidPool, propTag);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F17 File Offset: 0x00001117
		public static ErrorCode CreateBlockModeInitFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BlockModeInitFailed);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F24 File Offset: 0x00001124
		public static ErrorCode CreateBlockModeInitFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BlockModeInitFailed, propTag);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F32 File Offset: 0x00001132
		public static ErrorCode CreateUnexpectedMailboxState(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnexpectedMailboxState);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F3F File Offset: 0x0000113F
		public static ErrorCode CreateUnexpectedMailboxState(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnexpectedMailboxState, propTag);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F4D File Offset: 0x0000114D
		public static ErrorCode CreateSoftDeletedMailbox(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SoftDeletedMailbox);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F5A File Offset: 0x0000115A
		public static ErrorCode CreateSoftDeletedMailbox(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SoftDeletedMailbox, propTag);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F68 File Offset: 0x00001168
		public static ErrorCode CreateDatabaseStateConflict(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DatabaseStateConflict);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F75 File Offset: 0x00001175
		public static ErrorCode CreateDatabaseStateConflict(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DatabaseStateConflict, propTag);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F83 File Offset: 0x00001183
		public static ErrorCode CreateRpcInvalidSession(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RpcInvalidSession);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F90 File Offset: 0x00001190
		public static ErrorCode CreateRpcInvalidSession(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RpcInvalidSession, propTag);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F9E File Offset: 0x0000119E
		public static ErrorCode CreatePublicFolderDumpstersLimitExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PublicFolderDumpstersLimitExceeded);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FAB File Offset: 0x000011AB
		public static ErrorCode CreatePublicFolderDumpstersLimitExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PublicFolderDumpstersLimitExceeded, propTag);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FB9 File Offset: 0x000011B9
		public static ErrorCode CreateInvalidMultiMailboxSearchRequest(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidMultiMailboxSearchRequest);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FC6 File Offset: 0x000011C6
		public static ErrorCode CreateInvalidMultiMailboxSearchRequest(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidMultiMailboxSearchRequest, propTag);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FD4 File Offset: 0x000011D4
		public static ErrorCode CreateInvalidMultiMailboxKeywordStatsRequest(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidMultiMailboxKeywordStatsRequest);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002FE1 File Offset: 0x000011E1
		public static ErrorCode CreateInvalidMultiMailboxKeywordStatsRequest(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidMultiMailboxKeywordStatsRequest, propTag);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FEF File Offset: 0x000011EF
		public static ErrorCode CreateMultiMailboxSearchFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchFailed);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FFC File Offset: 0x000011FC
		public static ErrorCode CreateMultiMailboxSearchFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchFailed, propTag);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000300A File Offset: 0x0000120A
		public static ErrorCode CreateMaxMultiMailboxSearchExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MaxMultiMailboxSearchExceeded);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003017 File Offset: 0x00001217
		public static ErrorCode CreateMaxMultiMailboxSearchExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MaxMultiMailboxSearchExceeded, propTag);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003025 File Offset: 0x00001225
		public static ErrorCode CreateMultiMailboxSearchOperationFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchOperationFailed);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003032 File Offset: 0x00001232
		public static ErrorCode CreateMultiMailboxSearchOperationFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchOperationFailed, propTag);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003040 File Offset: 0x00001240
		public static ErrorCode CreateMultiMailboxSearchNonFullTextSearch(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchNonFullTextSearch);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000304D File Offset: 0x0000124D
		public static ErrorCode CreateMultiMailboxSearchNonFullTextSearch(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchNonFullTextSearch, propTag);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000305B File Offset: 0x0000125B
		public static ErrorCode CreateMultiMailboxSearchTimeOut(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchTimeOut);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003068 File Offset: 0x00001268
		public static ErrorCode CreateMultiMailboxSearchTimeOut(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchTimeOut, propTag);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003076 File Offset: 0x00001276
		public static ErrorCode CreateMultiMailboxKeywordStatsTimeOut(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxKeywordStatsTimeOut);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003083 File Offset: 0x00001283
		public static ErrorCode CreateMultiMailboxKeywordStatsTimeOut(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxKeywordStatsTimeOut, propTag);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003091 File Offset: 0x00001291
		public static ErrorCode CreateMultiMailboxSearchInvalidSortBy(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchInvalidSortBy);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000309E File Offset: 0x0000129E
		public static ErrorCode CreateMultiMailboxSearchInvalidSortBy(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchInvalidSortBy, propTag);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030AC File Offset: 0x000012AC
		public static ErrorCode CreateMultiMailboxSearchNonFullTextSortBy(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchNonFullTextSortBy);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030B9 File Offset: 0x000012B9
		public static ErrorCode CreateMultiMailboxSearchNonFullTextSortBy(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchNonFullTextSortBy, propTag);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030C7 File Offset: 0x000012C7
		public static ErrorCode CreateMultiMailboxSearchInvalidPagination(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchInvalidPagination);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030D4 File Offset: 0x000012D4
		public static ErrorCode CreateMultiMailboxSearchInvalidPagination(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchInvalidPagination, propTag);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030E2 File Offset: 0x000012E2
		public static ErrorCode CreateMultiMailboxSearchNonFullTextPropertyInPagination(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchNonFullTextPropertyInPagination);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030EF File Offset: 0x000012EF
		public static ErrorCode CreateMultiMailboxSearchNonFullTextPropertyInPagination(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchNonFullTextPropertyInPagination, propTag);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030FD File Offset: 0x000012FD
		public static ErrorCode CreateMultiMailboxSearchMailboxNotFound(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchMailboxNotFound);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000310A File Offset: 0x0000130A
		public static ErrorCode CreateMultiMailboxSearchMailboxNotFound(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchMailboxNotFound, propTag);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003118 File Offset: 0x00001318
		public static ErrorCode CreateMultiMailboxSearchInvalidRestriction(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MultiMailboxSearchInvalidRestriction);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003125 File Offset: 0x00001325
		public static ErrorCode CreateMultiMailboxSearchInvalidRestriction(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MultiMailboxSearchInvalidRestriction, propTag);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003133 File Offset: 0x00001333
		public static ErrorCode CreateFullTextIndexCallFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FullTextIndexCallFailed);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003140 File Offset: 0x00001340
		public static ErrorCode CreateFullTextIndexCallFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FullTextIndexCallFailed, propTag);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000314E File Offset: 0x0000134E
		public static ErrorCode CreateUserInformationAlreadyExists(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserInformationAlreadyExists);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000315B File Offset: 0x0000135B
		public static ErrorCode CreateUserInformationAlreadyExists(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserInformationAlreadyExists, propTag);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003169 File Offset: 0x00001369
		public static ErrorCode CreateUserInformationLockTimeout(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserInformationLockTimeout);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003176 File Offset: 0x00001376
		public static ErrorCode CreateUserInformationLockTimeout(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserInformationLockTimeout, propTag);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003184 File Offset: 0x00001384
		public static ErrorCode CreateUserInformationNotFound(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserInformationNotFound);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003191 File Offset: 0x00001391
		public static ErrorCode CreateUserInformationNotFound(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserInformationNotFound, propTag);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000319F File Offset: 0x0000139F
		public static ErrorCode CreateUserInformationNoAccess(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserInformationNoAccess);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000031AC File Offset: 0x000013AC
		public static ErrorCode CreateUserInformationNoAccess(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserInformationNoAccess, propTag);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000031BA File Offset: 0x000013BA
		public static ErrorCode CreateUserInformationPropertyError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserInformationPropertyError);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000031C7 File Offset: 0x000013C7
		public static ErrorCode CreateUserInformationPropertyError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserInformationPropertyError, propTag);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000031D5 File Offset: 0x000013D5
		public static ErrorCode CreateUserInformationSoftDeleted(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserInformationSoftDeleted);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031E2 File Offset: 0x000013E2
		public static ErrorCode CreateUserInformationSoftDeleted(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserInformationSoftDeleted, propTag);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000031F0 File Offset: 0x000013F0
		public static ErrorCode CreateInterfaceNotSupported(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InterfaceNotSupported);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000031FD File Offset: 0x000013FD
		public static ErrorCode CreateInterfaceNotSupported(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InterfaceNotSupported, propTag);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000320B File Offset: 0x0000140B
		public static ErrorCode CreateCallFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CallFailed);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003218 File Offset: 0x00001418
		public static ErrorCode CreateCallFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CallFailed, propTag);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003226 File Offset: 0x00001426
		public static ErrorCode CreateStreamAccessDenied(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.StreamAccessDenied);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003233 File Offset: 0x00001433
		public static ErrorCode CreateStreamAccessDenied(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.StreamAccessDenied, propTag);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003241 File Offset: 0x00001441
		public static ErrorCode CreateStgInsufficientMemory(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.StgInsufficientMemory);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000324E File Offset: 0x0000144E
		public static ErrorCode CreateStgInsufficientMemory(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.StgInsufficientMemory, propTag);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000325C File Offset: 0x0000145C
		public static ErrorCode CreateStreamSeekError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.StreamSeekError);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003269 File Offset: 0x00001469
		public static ErrorCode CreateStreamSeekError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.StreamSeekError, propTag);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003277 File Offset: 0x00001477
		public static ErrorCode CreateLockViolation(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.LockViolation);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003284 File Offset: 0x00001484
		public static ErrorCode CreateLockViolation(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.LockViolation, propTag);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003292 File Offset: 0x00001492
		public static ErrorCode CreateStreamInvalidParam(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.StreamInvalidParam);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000329F File Offset: 0x0000149F
		public static ErrorCode CreateStreamInvalidParam(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.StreamInvalidParam, propTag);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000032AD File Offset: 0x000014AD
		public static ErrorCode CreateNotSupported(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotSupported);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000032BA File Offset: 0x000014BA
		public static ErrorCode CreateNotSupported(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotSupported, propTag);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000032C8 File Offset: 0x000014C8
		public static ErrorCode CreateBadCharWidth(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BadCharWidth);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000032D5 File Offset: 0x000014D5
		public static ErrorCode CreateBadCharWidth(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BadCharWidth, propTag);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000032E3 File Offset: 0x000014E3
		public static ErrorCode CreateStringTooLong(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.StringTooLong);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000032F0 File Offset: 0x000014F0
		public static ErrorCode CreateStringTooLong(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.StringTooLong, propTag);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000032FE File Offset: 0x000014FE
		public static ErrorCode CreateUnknownFlags(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnknownFlags);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000330B File Offset: 0x0000150B
		public static ErrorCode CreateUnknownFlags(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnknownFlags, propTag);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003319 File Offset: 0x00001519
		public static ErrorCode CreateInvalidEntryId(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidEntryId);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003326 File Offset: 0x00001526
		public static ErrorCode CreateInvalidEntryId(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidEntryId, propTag);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003334 File Offset: 0x00001534
		public static ErrorCode CreateInvalidObject(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidObject);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003341 File Offset: 0x00001541
		public static ErrorCode CreateInvalidObject(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidObject, propTag);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000334F File Offset: 0x0000154F
		public static ErrorCode CreateObjectChanged(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ObjectChanged);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000335C File Offset: 0x0000155C
		public static ErrorCode CreateObjectChanged(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ObjectChanged, propTag);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000336A File Offset: 0x0000156A
		public static ErrorCode CreateObjectDeleted(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ObjectDeleted);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003377 File Offset: 0x00001577
		public static ErrorCode CreateObjectDeleted(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ObjectDeleted, propTag);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003385 File Offset: 0x00001585
		public static ErrorCode CreateBusy(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Busy);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003392 File Offset: 0x00001592
		public static ErrorCode CreateBusy(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Busy, propTag);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000033A0 File Offset: 0x000015A0
		public static ErrorCode CreateNotEnoughDisk(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotEnoughDisk);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000033AD File Offset: 0x000015AD
		public static ErrorCode CreateNotEnoughDisk(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotEnoughDisk, propTag);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000033BB File Offset: 0x000015BB
		public static ErrorCode CreateNotEnoughResources(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotEnoughResources);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000033C8 File Offset: 0x000015C8
		public static ErrorCode CreateNotEnoughResources(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotEnoughResources, propTag);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033D6 File Offset: 0x000015D6
		public static ErrorCode CreateNotFound(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotFound);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000033E3 File Offset: 0x000015E3
		public static ErrorCode CreateNotFound(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotFound, propTag);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000033F1 File Offset: 0x000015F1
		public static ErrorCode CreateVersionMismatch(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.VersionMismatch);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000033FE File Offset: 0x000015FE
		public static ErrorCode CreateVersionMismatch(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.VersionMismatch, propTag);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000340C File Offset: 0x0000160C
		public static ErrorCode CreateLogonFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.LogonFailed);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003419 File Offset: 0x00001619
		public static ErrorCode CreateLogonFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.LogonFailed, propTag);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003427 File Offset: 0x00001627
		public static ErrorCode CreateSessionLimit(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SessionLimit);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003434 File Offset: 0x00001634
		public static ErrorCode CreateSessionLimit(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SessionLimit, propTag);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003442 File Offset: 0x00001642
		public static ErrorCode CreateUserCancel(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UserCancel);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000344F File Offset: 0x0000164F
		public static ErrorCode CreateUserCancel(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UserCancel, propTag);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000345D File Offset: 0x0000165D
		public static ErrorCode CreateUnableToAbort(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnableToAbort);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000346A File Offset: 0x0000166A
		public static ErrorCode CreateUnableToAbort(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnableToAbort, propTag);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003478 File Offset: 0x00001678
		public static ErrorCode CreateNetworkError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NetworkError);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003485 File Offset: 0x00001685
		public static ErrorCode CreateNetworkError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NetworkError, propTag);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003493 File Offset: 0x00001693
		public static ErrorCode CreateDiskError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DiskError);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000034A0 File Offset: 0x000016A0
		public static ErrorCode CreateDiskError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DiskError, propTag);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000034AE File Offset: 0x000016AE
		public static ErrorCode CreateTooComplex(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TooComplex);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000034BB File Offset: 0x000016BB
		public static ErrorCode CreateTooComplex(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TooComplex, propTag);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000034C9 File Offset: 0x000016C9
		public static ErrorCode CreateConditionViolation(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ConditionViolation);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000034D6 File Offset: 0x000016D6
		public static ErrorCode CreateConditionViolation(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ConditionViolation, propTag);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000034E4 File Offset: 0x000016E4
		public static ErrorCode CreateBadColumn(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BadColumn);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000034F1 File Offset: 0x000016F1
		public static ErrorCode CreateBadColumn(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BadColumn, propTag);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000034FF File Offset: 0x000016FF
		public static ErrorCode CreateExtendedError(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ExtendedError);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000350C File Offset: 0x0000170C
		public static ErrorCode CreateExtendedError(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ExtendedError, propTag);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000351A File Offset: 0x0000171A
		public static ErrorCode CreateComputed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Computed);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003527 File Offset: 0x00001727
		public static ErrorCode CreateComputed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Computed, propTag);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003535 File Offset: 0x00001735
		public static ErrorCode CreateCorruptData(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CorruptData);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003542 File Offset: 0x00001742
		public static ErrorCode CreateCorruptData(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CorruptData, propTag);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003550 File Offset: 0x00001750
		public static ErrorCode CreateUnconfigured(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Unconfigured);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000355D File Offset: 0x0000175D
		public static ErrorCode CreateUnconfigured(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Unconfigured, propTag);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000356B File Offset: 0x0000176B
		public static ErrorCode CreateFailOneProvider(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FailOneProvider);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003578 File Offset: 0x00001778
		public static ErrorCode CreateFailOneProvider(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FailOneProvider, propTag);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003586 File Offset: 0x00001786
		public static ErrorCode CreateUnknownCPID(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnknownCPID);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003593 File Offset: 0x00001793
		public static ErrorCode CreateUnknownCPID(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnknownCPID, propTag);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000035A1 File Offset: 0x000017A1
		public static ErrorCode CreateUnknownLCID(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnknownLCID);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000035AE File Offset: 0x000017AE
		public static ErrorCode CreateUnknownLCID(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnknownLCID, propTag);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000035BC File Offset: 0x000017BC
		public static ErrorCode CreatePasswordChangeRequired(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PasswordChangeRequired);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000035C9 File Offset: 0x000017C9
		public static ErrorCode CreatePasswordChangeRequired(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PasswordChangeRequired, propTag);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000035D7 File Offset: 0x000017D7
		public static ErrorCode CreatePasswordExpired(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PasswordExpired);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000035E4 File Offset: 0x000017E4
		public static ErrorCode CreatePasswordExpired(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PasswordExpired, propTag);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000035F2 File Offset: 0x000017F2
		public static ErrorCode CreateInvalidWorkstationAccount(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidWorkstationAccount);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000035FF File Offset: 0x000017FF
		public static ErrorCode CreateInvalidWorkstationAccount(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidWorkstationAccount, propTag);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000360D File Offset: 0x0000180D
		public static ErrorCode CreateInvalidAccessTime(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidAccessTime);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000361A File Offset: 0x0000181A
		public static ErrorCode CreateInvalidAccessTime(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidAccessTime, propTag);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003628 File Offset: 0x00001828
		public static ErrorCode CreateAccountDisabled(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.AccountDisabled);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003635 File Offset: 0x00001835
		public static ErrorCode CreateAccountDisabled(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.AccountDisabled, propTag);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003643 File Offset: 0x00001843
		public static ErrorCode CreateEndOfSession(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.EndOfSession);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003650 File Offset: 0x00001850
		public static ErrorCode CreateEndOfSession(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.EndOfSession, propTag);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000365E File Offset: 0x0000185E
		public static ErrorCode CreateUnknownEntryId(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnknownEntryId);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000366B File Offset: 0x0000186B
		public static ErrorCode CreateUnknownEntryId(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnknownEntryId, propTag);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003679 File Offset: 0x00001879
		public static ErrorCode CreateMissingRequiredColumn(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MissingRequiredColumn);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003686 File Offset: 0x00001886
		public static ErrorCode CreateMissingRequiredColumn(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MissingRequiredColumn, propTag);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003694 File Offset: 0x00001894
		public static ErrorCode CreateFailCallback(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FailCallback);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000036A1 File Offset: 0x000018A1
		public static ErrorCode CreateFailCallback(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FailCallback, propTag);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000036AF File Offset: 0x000018AF
		public static ErrorCode CreateBadValue(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BadValue);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000036BC File Offset: 0x000018BC
		public static ErrorCode CreateBadValue(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BadValue, propTag);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000036CA File Offset: 0x000018CA
		public static ErrorCode CreateInvalidType(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidType);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000036D7 File Offset: 0x000018D7
		public static ErrorCode CreateInvalidType(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidType, propTag);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000036E5 File Offset: 0x000018E5
		public static ErrorCode CreateTypeNoSupport(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TypeNoSupport);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000036F2 File Offset: 0x000018F2
		public static ErrorCode CreateTypeNoSupport(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TypeNoSupport, propTag);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003700 File Offset: 0x00001900
		public static ErrorCode CreateUnexpectedType(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnexpectedType);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000370D File Offset: 0x0000190D
		public static ErrorCode CreateUnexpectedType(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnexpectedType, propTag);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000371B File Offset: 0x0000191B
		public static ErrorCode CreateTooBig(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TooBig);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003728 File Offset: 0x00001928
		public static ErrorCode CreateTooBig(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TooBig, propTag);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003736 File Offset: 0x00001936
		public static ErrorCode CreateDeclineCopy(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DeclineCopy);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003743 File Offset: 0x00001943
		public static ErrorCode CreateDeclineCopy(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DeclineCopy, propTag);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003751 File Offset: 0x00001951
		public static ErrorCode CreateUnexpectedId(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnexpectedId);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000375E File Offset: 0x0000195E
		public static ErrorCode CreateUnexpectedId(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnexpectedId, propTag);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000376C File Offset: 0x0000196C
		public static ErrorCode CreateUnableToComplete(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnableToComplete);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003779 File Offset: 0x00001979
		public static ErrorCode CreateUnableToComplete(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnableToComplete, propTag);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003787 File Offset: 0x00001987
		public static ErrorCode CreateTimeout(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Timeout);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003794 File Offset: 0x00001994
		public static ErrorCode CreateTimeout(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Timeout, propTag);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000037A2 File Offset: 0x000019A2
		public static ErrorCode CreateTableEmpty(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TableEmpty);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000037AF File Offset: 0x000019AF
		public static ErrorCode CreateTableEmpty(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TableEmpty, propTag);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000037BD File Offset: 0x000019BD
		public static ErrorCode CreateTableTooBig(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TableTooBig);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000037CA File Offset: 0x000019CA
		public static ErrorCode CreateTableTooBig(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TableTooBig, propTag);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000037D8 File Offset: 0x000019D8
		public static ErrorCode CreateInvalidBookmark(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidBookmark);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000037E5 File Offset: 0x000019E5
		public static ErrorCode CreateInvalidBookmark(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidBookmark, propTag);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000037F3 File Offset: 0x000019F3
		public static ErrorCode CreateDataLoss(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DataLoss);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003800 File Offset: 0x00001A00
		public static ErrorCode CreateDataLoss(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DataLoss, propTag);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000380E File Offset: 0x00001A0E
		public static ErrorCode CreateWait(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Wait);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000381B File Offset: 0x00001A1B
		public static ErrorCode CreateWait(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Wait, propTag);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003829 File Offset: 0x00001A29
		public static ErrorCode CreateCancel(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Cancel);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003836 File Offset: 0x00001A36
		public static ErrorCode CreateCancel(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Cancel, propTag);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003844 File Offset: 0x00001A44
		public static ErrorCode CreateNotMe(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotMe);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003851 File Offset: 0x00001A51
		public static ErrorCode CreateNotMe(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotMe, propTag);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000385F File Offset: 0x00001A5F
		public static ErrorCode CreateCorruptStore(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CorruptStore);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000386C File Offset: 0x00001A6C
		public static ErrorCode CreateCorruptStore(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CorruptStore, propTag);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000387A File Offset: 0x00001A7A
		public static ErrorCode CreateNotInQueue(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotInQueue);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003887 File Offset: 0x00001A87
		public static ErrorCode CreateNotInQueue(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotInQueue, propTag);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003895 File Offset: 0x00001A95
		public static ErrorCode CreateNoSuppress(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoSuppress);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000038A2 File Offset: 0x00001AA2
		public static ErrorCode CreateNoSuppress(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoSuppress, propTag);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000038B0 File Offset: 0x00001AB0
		public static ErrorCode CreateCollision(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Collision);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000038BD File Offset: 0x00001ABD
		public static ErrorCode CreateCollision(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Collision, propTag);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000038CB File Offset: 0x00001ACB
		public static ErrorCode CreateNotInitialized(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotInitialized);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000038D8 File Offset: 0x00001AD8
		public static ErrorCode CreateNotInitialized(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotInitialized, propTag);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000038E6 File Offset: 0x00001AE6
		public static ErrorCode CreateNonStandard(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NonStandard);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000038F3 File Offset: 0x00001AF3
		public static ErrorCode CreateNonStandard(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NonStandard, propTag);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003901 File Offset: 0x00001B01
		public static ErrorCode CreateNoRecipients(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoRecipients);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000390E File Offset: 0x00001B0E
		public static ErrorCode CreateNoRecipients(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoRecipients, propTag);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000391C File Offset: 0x00001B1C
		public static ErrorCode CreateSubmitted(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Submitted);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003929 File Offset: 0x00001B29
		public static ErrorCode CreateSubmitted(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Submitted, propTag);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003937 File Offset: 0x00001B37
		public static ErrorCode CreateHasFolders(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.HasFolders);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003944 File Offset: 0x00001B44
		public static ErrorCode CreateHasFolders(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.HasFolders, propTag);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003952 File Offset: 0x00001B52
		public static ErrorCode CreateHasMessages(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.HasMessages);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000395F File Offset: 0x00001B5F
		public static ErrorCode CreateHasMessages(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.HasMessages, propTag);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000396D File Offset: 0x00001B6D
		public static ErrorCode CreateFolderCycle(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FolderCycle);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000397A File Offset: 0x00001B7A
		public static ErrorCode CreateFolderCycle(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FolderCycle, propTag);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003988 File Offset: 0x00001B88
		public static ErrorCode CreateRootFolder(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FolderCycle);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003995 File Offset: 0x00001B95
		public static ErrorCode CreateRootFolder(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FolderCycle, propTag);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000039A3 File Offset: 0x00001BA3
		public static ErrorCode CreateRecursionLimit(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RecursionLimit);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000039B0 File Offset: 0x00001BB0
		public static ErrorCode CreateRecursionLimit(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RecursionLimit, propTag);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000039BE File Offset: 0x00001BBE
		public static ErrorCode CreateLockIdLimit(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.LockIdLimit);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000039CB File Offset: 0x00001BCB
		public static ErrorCode CreateLockIdLimit(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.LockIdLimit, propTag);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000039D9 File Offset: 0x00001BD9
		public static ErrorCode CreateTooManyMountedDatabases(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TooManyMountedDatabases);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000039E6 File Offset: 0x00001BE6
		public static ErrorCode CreateTooManyMountedDatabases(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TooManyMountedDatabases, propTag);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000039F4 File Offset: 0x00001BF4
		public static ErrorCode CreatePartialItem(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PartialItem);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003A01 File Offset: 0x00001C01
		public static ErrorCode CreatePartialItem(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PartialItem, propTag);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003A0F File Offset: 0x00001C0F
		public static ErrorCode CreateAmbiguousRecip(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.AmbiguousRecip);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003A1C File Offset: 0x00001C1C
		public static ErrorCode CreateAmbiguousRecip(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.AmbiguousRecip, propTag);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003A2A File Offset: 0x00001C2A
		public static ErrorCode CreateSyncObjectDeleted(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncObjectDeleted);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003A37 File Offset: 0x00001C37
		public static ErrorCode CreateSyncObjectDeleted(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncObjectDeleted, propTag);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003A45 File Offset: 0x00001C45
		public static ErrorCode CreateSyncIgnore(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncIgnore);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003A52 File Offset: 0x00001C52
		public static ErrorCode CreateSyncIgnore(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncIgnore, propTag);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003A60 File Offset: 0x00001C60
		public static ErrorCode CreateSyncConflict(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncConflict);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003A6D File Offset: 0x00001C6D
		public static ErrorCode CreateSyncConflict(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncConflict, propTag);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003A7B File Offset: 0x00001C7B
		public static ErrorCode CreateSyncNoParent(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncNoParent);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003A88 File Offset: 0x00001C88
		public static ErrorCode CreateSyncNoParent(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncNoParent, propTag);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003A96 File Offset: 0x00001C96
		public static ErrorCode CreateSyncIncest(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncIncest);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003AA3 File Offset: 0x00001CA3
		public static ErrorCode CreateSyncIncest(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncIncest, propTag);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003AB1 File Offset: 0x00001CB1
		public static ErrorCode CreateErrorPathNotFound(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ErrorPathNotFound);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003ABE File Offset: 0x00001CBE
		public static ErrorCode CreateErrorPathNotFound(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ErrorPathNotFound, propTag);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003ACC File Offset: 0x00001CCC
		public static ErrorCode CreateNoAccess(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoAccess);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003AD9 File Offset: 0x00001CD9
		public static ErrorCode CreateNoAccess(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoAccess, propTag);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003AE7 File Offset: 0x00001CE7
		public static ErrorCode CreateNotEnoughMemory(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotEnoughMemory);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public static ErrorCode CreateNotEnoughMemory(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotEnoughMemory, propTag);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003B02 File Offset: 0x00001D02
		public static ErrorCode CreateInvalidParameter(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidParameter);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003B0F File Offset: 0x00001D0F
		public static ErrorCode CreateInvalidParameter(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidParameter, propTag);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003B1D File Offset: 0x00001D1D
		public static ErrorCode CreateErrorCanNotComplete(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ErrorCanNotComplete);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00003B2A File Offset: 0x00001D2A
		public static ErrorCode CreateErrorCanNotComplete(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ErrorCanNotComplete, propTag);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003B38 File Offset: 0x00001D38
		public static ErrorCode CreateNamedPropQuotaExceeded(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NamedPropQuotaExceeded);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003B45 File Offset: 0x00001D45
		public static ErrorCode CreateNamedPropQuotaExceeded(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NamedPropQuotaExceeded, propTag);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003B53 File Offset: 0x00001D53
		public static ErrorCode CreateTooManyRecips(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TooManyRecips);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003B60 File Offset: 0x00001D60
		public static ErrorCode CreateTooManyRecips(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TooManyRecips, propTag);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003B6E File Offset: 0x00001D6E
		public static ErrorCode CreateTooManyProps(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TooManyProps);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003B7B File Offset: 0x00001D7B
		public static ErrorCode CreateTooManyProps(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TooManyProps, propTag);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003B89 File Offset: 0x00001D89
		public static ErrorCode CreateParameterOverflow(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ParameterOverflow);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003B96 File Offset: 0x00001D96
		public static ErrorCode CreateParameterOverflow(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ParameterOverflow, propTag);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public static ErrorCode CreateBadFolderName(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BadFolderName);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003BB1 File Offset: 0x00001DB1
		public static ErrorCode CreateBadFolderName(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BadFolderName, propTag);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003BBF File Offset: 0x00001DBF
		public static ErrorCode CreateSearchFolder(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SearchFolder);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00003BCC File Offset: 0x00001DCC
		public static ErrorCode CreateSearchFolder(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SearchFolder, propTag);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003BDA File Offset: 0x00001DDA
		public static ErrorCode CreateNotSearchFolder(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NotSearchFolder);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public static ErrorCode CreateNotSearchFolder(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NotSearchFolder, propTag);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public static ErrorCode CreateFolderSetReceive(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.FolderSetReceive);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003C02 File Offset: 0x00001E02
		public static ErrorCode CreateFolderSetReceive(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.FolderSetReceive, propTag);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003C10 File Offset: 0x00001E10
		public static ErrorCode CreateNoReceiveFolder(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoReceiveFolder);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003C1D File Offset: 0x00001E1D
		public static ErrorCode CreateNoReceiveFolder(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoReceiveFolder, propTag);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003C2B File Offset: 0x00001E2B
		public static ErrorCode CreateInvalidRecipients(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidRecipients);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003C38 File Offset: 0x00001E38
		public static ErrorCode CreateInvalidRecipients(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidRecipients, propTag);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003C46 File Offset: 0x00001E46
		public static ErrorCode CreateBufferTooSmall(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BufferTooSmall);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003C53 File Offset: 0x00001E53
		public static ErrorCode CreateBufferTooSmall(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BufferTooSmall, propTag);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003C61 File Offset: 0x00001E61
		public static ErrorCode CreateRequiresRefResolve(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.RequiresRefResolve);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003C6E File Offset: 0x00001E6E
		public static ErrorCode CreateRequiresRefResolve(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.RequiresRefResolve, propTag);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003C7C File Offset: 0x00001E7C
		public static ErrorCode CreateNullObject(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NullObject);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003C89 File Offset: 0x00001E89
		public static ErrorCode CreateNullObject(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NullObject, propTag);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003C97 File Offset: 0x00001E97
		public static ErrorCode CreateSendAsDenied(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SendAsDenied);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public static ErrorCode CreateSendAsDenied(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SendAsDenied, propTag);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public static ErrorCode CreateDestinationNullObject(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DestinationNullObject);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003CBF File Offset: 0x00001EBF
		public static ErrorCode CreateDestinationNullObject(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DestinationNullObject, propTag);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003CCD File Offset: 0x00001ECD
		public static ErrorCode CreateNoService(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoService);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003CDA File Offset: 0x00001EDA
		public static ErrorCode CreateNoService(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoService, propTag);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public static ErrorCode CreateErrorsReturned(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ErrorsReturned);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public static ErrorCode CreateErrorsReturned(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ErrorsReturned, propTag);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003D03 File Offset: 0x00001F03
		public static ErrorCode CreatePositionChanged(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PositionChanged);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003D10 File Offset: 0x00001F10
		public static ErrorCode CreatePositionChanged(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PositionChanged, propTag);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003D1E File Offset: 0x00001F1E
		public static ErrorCode CreateApproxCount(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ApproxCount);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003D2B File Offset: 0x00001F2B
		public static ErrorCode CreateApproxCount(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ApproxCount, propTag);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003D39 File Offset: 0x00001F39
		public static ErrorCode CreateCancelMessage(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CancelMessage);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003D46 File Offset: 0x00001F46
		public static ErrorCode CreateCancelMessage(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CancelMessage, propTag);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003D54 File Offset: 0x00001F54
		public static ErrorCode CreatePartialCompletion(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PartialCompletion);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003D61 File Offset: 0x00001F61
		public static ErrorCode CreatePartialCompletion(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PartialCompletion, propTag);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003D6F File Offset: 0x00001F6F
		public static ErrorCode CreateSecurityRequiredLow(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SecurityRequiredLow);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003D7C File Offset: 0x00001F7C
		public static ErrorCode CreateSecurityRequiredLow(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SecurityRequiredLow, propTag);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003D8A File Offset: 0x00001F8A
		public static ErrorCode CreateSecuirtyRequiredMedium(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SecuirtyRequiredMedium);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003D97 File Offset: 0x00001F97
		public static ErrorCode CreateSecuirtyRequiredMedium(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SecuirtyRequiredMedium, propTag);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public static ErrorCode CreatePartialItems(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.PartialItems);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003DB2 File Offset: 0x00001FB2
		public static ErrorCode CreatePartialItems(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.PartialItems, propTag);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public static ErrorCode CreateSyncProgress(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncProgress);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003DCD File Offset: 0x00001FCD
		public static ErrorCode CreateSyncProgress(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncProgress, propTag);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003DDB File Offset: 0x00001FDB
		public static ErrorCode CreateSyncClientChangeNewer(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SyncClientChangeNewer);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public static ErrorCode CreateSyncClientChangeNewer(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SyncClientChangeNewer, propTag);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static ErrorCode CreateExiting(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.Exiting);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003E03 File Offset: 0x00002003
		public static ErrorCode CreateExiting(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.Exiting, propTag);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003E11 File Offset: 0x00002011
		public static ErrorCode CreateMdbNotInitialized(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MdbNotInitialized);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003E1E File Offset: 0x0000201E
		public static ErrorCode CreateMdbNotInitialized(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MdbNotInitialized, propTag);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003E2C File Offset: 0x0000202C
		public static ErrorCode CreateServerOutOfMemory(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ServerOutOfMemory);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003E39 File Offset: 0x00002039
		public static ErrorCode CreateServerOutOfMemory(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ServerOutOfMemory, propTag);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003E47 File Offset: 0x00002047
		public static ErrorCode CreateMailboxInTransit(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MailboxInTransit);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003E54 File Offset: 0x00002054
		public static ErrorCode CreateMailboxInTransit(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MailboxInTransit, propTag);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003E62 File Offset: 0x00002062
		public static ErrorCode CreateBackupInProgress(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.BackupInProgress);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003E6F File Offset: 0x0000206F
		public static ErrorCode CreateBackupInProgress(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.BackupInProgress, propTag);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00003E7D File Offset: 0x0000207D
		public static ErrorCode CreateInvalidBackupSequence(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.InvalidBackupSequence);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003E8A File Offset: 0x0000208A
		public static ErrorCode CreateInvalidBackupSequence(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.InvalidBackupSequence, propTag);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003E98 File Offset: 0x00002098
		public static ErrorCode CreateWrongServer(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.WrongServer);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003EA5 File Offset: 0x000020A5
		public static ErrorCode CreateWrongServer(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.WrongServer, propTag);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003EB3 File Offset: 0x000020B3
		public static ErrorCode CreateMailboxQuarantined(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MailboxQuarantined);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003EC0 File Offset: 0x000020C0
		public static ErrorCode CreateMailboxQuarantined(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MailboxQuarantined, propTag);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003ECE File Offset: 0x000020CE
		public static ErrorCode CreateMountInProgress(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.MountInProgress);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003EDB File Offset: 0x000020DB
		public static ErrorCode CreateMountInProgress(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.MountInProgress, propTag);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00003EE9 File Offset: 0x000020E9
		public static ErrorCode CreateDismountInProgress(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DismountInProgress);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003EF6 File Offset: 0x000020F6
		public static ErrorCode CreateDismountInProgress(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DismountInProgress, propTag);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003F04 File Offset: 0x00002104
		public static ErrorCode CreateCannotRegisterNewReplidGuidMapping(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CannotRegisterNewReplidGuidMapping);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003F11 File Offset: 0x00002111
		public static ErrorCode CreateCannotRegisterNewReplidGuidMapping(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CannotRegisterNewReplidGuidMapping, propTag);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003F1F File Offset: 0x0000211F
		public static ErrorCode CreateCannotRegisterNewNamedPropertyMapping(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CannotRegisterNewNamedPropertyMapping);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003F2C File Offset: 0x0000212C
		public static ErrorCode CreateCannotRegisterNewNamedPropertyMapping(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CannotRegisterNewNamedPropertyMapping, propTag);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003F3A File Offset: 0x0000213A
		public static ErrorCode CreateCannotPreserveMailboxSignature(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.CannotPreserveMailboxSignature);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003F47 File Offset: 0x00002147
		public static ErrorCode CreateCannotPreserveMailboxSignature(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.CannotPreserveMailboxSignature, propTag);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003F55 File Offset: 0x00002155
		public static ErrorCode CreateExceptionThrown(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.ExceptionThrown);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003F62 File Offset: 0x00002162
		public static ErrorCode CreateExceptionThrown(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.ExceptionThrown, propTag);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003F70 File Offset: 0x00002170
		public static ErrorCode CreateSessionLocked(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.SessionLocked);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003F7D File Offset: 0x0000217D
		public static ErrorCode CreateSessionLocked(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.SessionLocked, propTag);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003F8B File Offset: 0x0000218B
		public static ErrorCode CreateDuplicateObject(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DuplicateObject);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003F98 File Offset: 0x00002198
		public static ErrorCode CreateDuplicateObject(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DuplicateObject, propTag);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003FA6 File Offset: 0x000021A6
		public static ErrorCode CreateDuplicateDelivery(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.DuplicateDelivery);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003FB3 File Offset: 0x000021B3
		public static ErrorCode CreateDuplicateDelivery(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.DuplicateDelivery, propTag);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003FC1 File Offset: 0x000021C1
		public static ErrorCode CreateUnregisteredNamedProp(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.UnregisteredNamedProp);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003FCE File Offset: 0x000021CE
		public static ErrorCode CreateUnregisteredNamedProp(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.UnregisteredNamedProp, propTag);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003FDC File Offset: 0x000021DC
		public static ErrorCode CreateTaskRequestFailed(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.TaskRequestFailed);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003FE9 File Offset: 0x000021E9
		public static ErrorCode CreateTaskRequestFailed(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.TaskRequestFailed, propTag);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003FF7 File Offset: 0x000021F7
		public static ErrorCode CreateNoReplicaHere(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoReplicaHere);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004004 File Offset: 0x00002204
		public static ErrorCode CreateNoReplicaHere(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoReplicaHere, propTag);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004012 File Offset: 0x00002212
		public static ErrorCode CreateNoReplicaAvailable(LID lid)
		{
			return ErrorCode.CreateWithLid(lid, ErrorCodeValue.NoReplicaAvailable);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000401F File Offset: 0x0000221F
		public static ErrorCode CreateNoReplicaAvailable(LID lid, uint propTag)
		{
			return ErrorCode.CreateWithLidAndPropTag(lid, ErrorCodeValue.NoReplicaAvailable, propTag);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000402D File Offset: 0x0000222D
		private ErrorCode(ErrorCodeValue errorCodeValue)
		{
			this.errorCodeValue = errorCodeValue;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004036 File Offset: 0x00002236
		public static ErrorCode CreateWithLid(LID lid, ErrorCodeValue errorCodeValue)
		{
			if (errorCodeValue != ErrorCodeValue.NoError)
			{
				DiagnosticContext.TraceStoreError(lid, (uint)errorCodeValue);
			}
			return new ErrorCode(errorCodeValue);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004048 File Offset: 0x00002248
		public static ErrorCode CreateWithLidAndPropTag(LID lid, ErrorCodeValue errorCodeValue, uint propTag)
		{
			if (errorCodeValue != ErrorCodeValue.NoError)
			{
				DiagnosticContext.TracePropTagError(lid, (uint)errorCodeValue, propTag);
			}
			return new ErrorCode(errorCodeValue);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000405B File Offset: 0x0000225B
		public static implicit operator ErrorCodeValue(ErrorCode errorCode)
		{
			return errorCode.errorCodeValue;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004064 File Offset: 0x00002264
		public static explicit operator int(ErrorCode errorCode)
		{
			return (int)errorCode.errorCodeValue;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000406D File Offset: 0x0000226D
		public static bool operator ==(ErrorCode first, ErrorCode second)
		{
			return first.errorCodeValue == second.errorCodeValue;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000407F File Offset: 0x0000227F
		public static bool operator !=(ErrorCode first, ErrorCode second)
		{
			return first.errorCodeValue != second.errorCodeValue;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004094 File Offset: 0x00002294
		public override string ToString()
		{
			return this.errorCodeValue.ToString();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000040A6 File Offset: 0x000022A6
		public override bool Equals(object obj)
		{
			return (obj is ErrorCode && this.Equals((ErrorCode)obj)) || (obj is ErrorCodeValue && this.errorCodeValue == (ErrorCodeValue)obj);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000040D8 File Offset: 0x000022D8
		public override int GetHashCode()
		{
			return (int)this.errorCodeValue;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000040E0 File Offset: 0x000022E0
		public bool Equals(ErrorCode other)
		{
			return this.errorCodeValue == other.errorCodeValue;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000040F1 File Offset: 0x000022F1
		public ErrorCode Propagate(LID lid)
		{
			if (this.errorCodeValue != ErrorCodeValue.NoError)
			{
				DiagnosticContext.TraceStoreError(lid, (uint)this.errorCodeValue);
			}
			return this;
		}

		// Token: 0x040001E1 RID: 481
		public const ErrorCodeValue StoreTestFailure = ErrorCodeValue.StoreTestFailure;

		// Token: 0x040001E2 RID: 482
		public const ErrorCodeValue UnknownUser = ErrorCodeValue.UnknownUser;

		// Token: 0x040001E3 RID: 483
		public const ErrorCodeValue DatabaseRolledBack = ErrorCodeValue.DatabaseRolledBack;

		// Token: 0x040001E4 RID: 484
		public const ErrorCodeValue DatabaseBadVersion = ErrorCodeValue.DatabaseBadVersion;

		// Token: 0x040001E5 RID: 485
		public const ErrorCodeValue DatabaseError = ErrorCodeValue.DatabaseError;

		// Token: 0x040001E6 RID: 486
		public const ErrorCodeValue InvalidCollapseState = ErrorCodeValue.InvalidCollapseState;

		// Token: 0x040001E7 RID: 487
		public const ErrorCodeValue NoDeleteSubmitMessage = ErrorCodeValue.NoDeleteSubmitMessage;

		// Token: 0x040001E8 RID: 488
		public const ErrorCodeValue RecoveryMDBMismatch = ErrorCodeValue.RecoveryMDBMismatch;

		// Token: 0x040001E9 RID: 489
		public const ErrorCodeValue SearchFolderScopeViolation = ErrorCodeValue.SearchFolderScopeViolation;

		// Token: 0x040001EA RID: 490
		public const ErrorCodeValue SearchEvaluationInProgress = ErrorCodeValue.SearchEvaluationInProgress;

		// Token: 0x040001EB RID: 491
		public const ErrorCodeValue NestedSearchChainTooDeep = ErrorCodeValue.NestedSearchChainTooDeep;

		// Token: 0x040001EC RID: 492
		public const ErrorCodeValue CorruptSearchScope = ErrorCodeValue.CorruptSearchScope;

		// Token: 0x040001ED RID: 493
		public const ErrorCodeValue CorruptSearchBacklink = ErrorCodeValue.CorruptSearchBacklink;

		// Token: 0x040001EE RID: 494
		public const ErrorCodeValue GlobalCounterRangeExceeded = ErrorCodeValue.GlobalCounterRangeExceeded;

		// Token: 0x040001EF RID: 495
		public const ErrorCodeValue CorruptMidsetDeleted = ErrorCodeValue.CorruptMidsetDeleted;

		// Token: 0x040001F0 RID: 496
		public const ErrorCodeValue RpcFormat = ErrorCodeValue.RpcFormat;

		// Token: 0x040001F1 RID: 497
		public const ErrorCodeValue QuotaExceeded = ErrorCodeValue.QuotaExceeded;

		// Token: 0x040001F2 RID: 498
		public const ErrorCodeValue MaxSubmissionExceeded = ErrorCodeValue.MaxSubmissionExceeded;

		// Token: 0x040001F3 RID: 499
		public const ErrorCodeValue MaxAttachmentExceeded = ErrorCodeValue.MaxAttachmentExceeded;

		// Token: 0x040001F4 RID: 500
		public const ErrorCodeValue ShutoffQuotaExceeded = ErrorCodeValue.ShutoffQuotaExceeded;

		// Token: 0x040001F5 RID: 501
		public const ErrorCodeValue MaxObjectsExceeded = ErrorCodeValue.MaxObjectsExceeded;

		// Token: 0x040001F6 RID: 502
		public const ErrorCodeValue MessagePerFolderCountReceiveQuotaExceeded = ErrorCodeValue.MessagePerFolderCountReceiveQuotaExceeded;

		// Token: 0x040001F7 RID: 503
		public const ErrorCodeValue FolderHierarchyChildrenCountReceiveQuotaExceeded = ErrorCodeValue.FolderHierarchyChildrenCountReceiveQuotaExceeded;

		// Token: 0x040001F8 RID: 504
		public const ErrorCodeValue FolderHierarchyDepthReceiveQuotaExceeded = ErrorCodeValue.FolderHierarchyDepthReceiveQuotaExceeded;

		// Token: 0x040001F9 RID: 505
		public const ErrorCodeValue DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded = ErrorCodeValue.DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded;

		// Token: 0x040001FA RID: 506
		public const ErrorCodeValue FolderHierarchySizeReceiveQuotaExceeded = ErrorCodeValue.FolderHierarchySizeReceiveQuotaExceeded;

		// Token: 0x040001FB RID: 507
		public const ErrorCodeValue NotVisible = ErrorCodeValue.NotVisible;

		// Token: 0x040001FC RID: 508
		public const ErrorCodeValue NotExpanded = ErrorCodeValue.NotExpanded;

		// Token: 0x040001FD RID: 509
		public const ErrorCodeValue NotCollapsed = ErrorCodeValue.NotCollapsed;

		// Token: 0x040001FE RID: 510
		public const ErrorCodeValue Leaf = ErrorCodeValue.Leaf;

		// Token: 0x040001FF RID: 511
		public const ErrorCodeValue MessageCycle = ErrorCodeValue.MessageCycle;

		// Token: 0x04000200 RID: 512
		public const ErrorCodeValue Rejected = ErrorCodeValue.Rejected;

		// Token: 0x04000201 RID: 513
		public const ErrorCodeValue UnknownMailbox = ErrorCodeValue.UnknownMailbox;

		// Token: 0x04000202 RID: 514
		public const ErrorCodeValue DisabledMailbox = ErrorCodeValue.DisabledMailbox;

		// Token: 0x04000203 RID: 515
		public const ErrorCodeValue AdUnavailable = ErrorCodeValue.AdUnavailable;

		// Token: 0x04000204 RID: 516
		public const ErrorCodeValue ADPropertyError = ErrorCodeValue.ADPropertyError;

		// Token: 0x04000205 RID: 517
		public const ErrorCodeValue RpcServerTooBusy = ErrorCodeValue.RpcServerTooBusy;

		// Token: 0x04000206 RID: 518
		public const ErrorCodeValue RpcServerUnavailable = ErrorCodeValue.RpcServerUnavailable;

		// Token: 0x04000207 RID: 519
		public const ErrorCodeValue EventsDeleted = ErrorCodeValue.EventsDeleted;

		// Token: 0x04000208 RID: 520
		public const ErrorCodeValue MaxPoolExceeded = ErrorCodeValue.MaxPoolExceeded;

		// Token: 0x04000209 RID: 521
		public const ErrorCodeValue EventNotFound = ErrorCodeValue.EventNotFound;

		// Token: 0x0400020A RID: 522
		public const ErrorCodeValue InvalidPool = ErrorCodeValue.InvalidPool;

		// Token: 0x0400020B RID: 523
		public const ErrorCodeValue BlockModeInitFailed = ErrorCodeValue.BlockModeInitFailed;

		// Token: 0x0400020C RID: 524
		public const ErrorCodeValue UnexpectedMailboxState = ErrorCodeValue.UnexpectedMailboxState;

		// Token: 0x0400020D RID: 525
		public const ErrorCodeValue SoftDeletedMailbox = ErrorCodeValue.SoftDeletedMailbox;

		// Token: 0x0400020E RID: 526
		public const ErrorCodeValue DatabaseStateConflict = ErrorCodeValue.DatabaseStateConflict;

		// Token: 0x0400020F RID: 527
		public const ErrorCodeValue RpcInvalidSession = ErrorCodeValue.RpcInvalidSession;

		// Token: 0x04000210 RID: 528
		public const ErrorCodeValue PublicFolderDumpstersLimitExceeded = ErrorCodeValue.PublicFolderDumpstersLimitExceeded;

		// Token: 0x04000211 RID: 529
		public const ErrorCodeValue InvalidMultiMailboxSearchRequest = ErrorCodeValue.InvalidMultiMailboxSearchRequest;

		// Token: 0x04000212 RID: 530
		public const ErrorCodeValue InvalidMultiMailboxKeywordStatsRequest = ErrorCodeValue.InvalidMultiMailboxKeywordStatsRequest;

		// Token: 0x04000213 RID: 531
		public const ErrorCodeValue MultiMailboxSearchFailed = ErrorCodeValue.MultiMailboxSearchFailed;

		// Token: 0x04000214 RID: 532
		public const ErrorCodeValue MaxMultiMailboxSearchExceeded = ErrorCodeValue.MaxMultiMailboxSearchExceeded;

		// Token: 0x04000215 RID: 533
		public const ErrorCodeValue MultiMailboxSearchOperationFailed = ErrorCodeValue.MultiMailboxSearchOperationFailed;

		// Token: 0x04000216 RID: 534
		public const ErrorCodeValue MultiMailboxSearchNonFullTextSearch = ErrorCodeValue.MultiMailboxSearchNonFullTextSearch;

		// Token: 0x04000217 RID: 535
		public const ErrorCodeValue MultiMailboxSearchTimeOut = ErrorCodeValue.MultiMailboxSearchTimeOut;

		// Token: 0x04000218 RID: 536
		public const ErrorCodeValue MultiMailboxKeywordStatsTimeOut = ErrorCodeValue.MultiMailboxKeywordStatsTimeOut;

		// Token: 0x04000219 RID: 537
		public const ErrorCodeValue MultiMailboxSearchInvalidSortBy = ErrorCodeValue.MultiMailboxSearchInvalidSortBy;

		// Token: 0x0400021A RID: 538
		public const ErrorCodeValue MultiMailboxSearchNonFullTextSortBy = ErrorCodeValue.MultiMailboxSearchNonFullTextSortBy;

		// Token: 0x0400021B RID: 539
		public const ErrorCodeValue MultiMailboxSearchInvalidPagination = ErrorCodeValue.MultiMailboxSearchInvalidPagination;

		// Token: 0x0400021C RID: 540
		public const ErrorCodeValue MultiMailboxSearchNonFullTextPropertyInPagination = ErrorCodeValue.MultiMailboxSearchNonFullTextPropertyInPagination;

		// Token: 0x0400021D RID: 541
		public const ErrorCodeValue MultiMailboxSearchMailboxNotFound = ErrorCodeValue.MultiMailboxSearchMailboxNotFound;

		// Token: 0x0400021E RID: 542
		public const ErrorCodeValue MultiMailboxSearchInvalidRestriction = ErrorCodeValue.MultiMailboxSearchInvalidRestriction;

		// Token: 0x0400021F RID: 543
		public const ErrorCodeValue FullTextIndexCallFailed = ErrorCodeValue.FullTextIndexCallFailed;

		// Token: 0x04000220 RID: 544
		public const ErrorCodeValue UserInformationAlreadyExists = ErrorCodeValue.UserInformationAlreadyExists;

		// Token: 0x04000221 RID: 545
		public const ErrorCodeValue UserInformationLockTimeout = ErrorCodeValue.UserInformationLockTimeout;

		// Token: 0x04000222 RID: 546
		public const ErrorCodeValue UserInformationNotFound = ErrorCodeValue.UserInformationNotFound;

		// Token: 0x04000223 RID: 547
		public const ErrorCodeValue UserInformationNoAccess = ErrorCodeValue.UserInformationNoAccess;

		// Token: 0x04000224 RID: 548
		public const ErrorCodeValue UserInformationPropertyError = ErrorCodeValue.UserInformationPropertyError;

		// Token: 0x04000225 RID: 549
		public const ErrorCodeValue UserInformationSoftDeleted = ErrorCodeValue.UserInformationSoftDeleted;

		// Token: 0x04000226 RID: 550
		public const ErrorCodeValue InterfaceNotSupported = ErrorCodeValue.InterfaceNotSupported;

		// Token: 0x04000227 RID: 551
		public const ErrorCodeValue CallFailed = ErrorCodeValue.CallFailed;

		// Token: 0x04000228 RID: 552
		public const ErrorCodeValue StreamAccessDenied = ErrorCodeValue.StreamAccessDenied;

		// Token: 0x04000229 RID: 553
		public const ErrorCodeValue StgInsufficientMemory = ErrorCodeValue.StgInsufficientMemory;

		// Token: 0x0400022A RID: 554
		public const ErrorCodeValue StreamSeekError = ErrorCodeValue.StreamSeekError;

		// Token: 0x0400022B RID: 555
		public const ErrorCodeValue LockViolation = ErrorCodeValue.LockViolation;

		// Token: 0x0400022C RID: 556
		public const ErrorCodeValue StreamInvalidParam = ErrorCodeValue.StreamInvalidParam;

		// Token: 0x0400022D RID: 557
		public const ErrorCodeValue NotSupported = ErrorCodeValue.NotSupported;

		// Token: 0x0400022E RID: 558
		public const ErrorCodeValue BadCharWidth = ErrorCodeValue.BadCharWidth;

		// Token: 0x0400022F RID: 559
		public const ErrorCodeValue StringTooLong = ErrorCodeValue.StringTooLong;

		// Token: 0x04000230 RID: 560
		public const ErrorCodeValue UnknownFlags = ErrorCodeValue.UnknownFlags;

		// Token: 0x04000231 RID: 561
		public const ErrorCodeValue InvalidEntryId = ErrorCodeValue.InvalidEntryId;

		// Token: 0x04000232 RID: 562
		public const ErrorCodeValue InvalidObject = ErrorCodeValue.InvalidObject;

		// Token: 0x04000233 RID: 563
		public const ErrorCodeValue ObjectChanged = ErrorCodeValue.ObjectChanged;

		// Token: 0x04000234 RID: 564
		public const ErrorCodeValue ObjectDeleted = ErrorCodeValue.ObjectDeleted;

		// Token: 0x04000235 RID: 565
		public const ErrorCodeValue Busy = ErrorCodeValue.Busy;

		// Token: 0x04000236 RID: 566
		public const ErrorCodeValue NotEnoughDisk = ErrorCodeValue.NotEnoughDisk;

		// Token: 0x04000237 RID: 567
		public const ErrorCodeValue NotEnoughResources = ErrorCodeValue.NotEnoughResources;

		// Token: 0x04000238 RID: 568
		public const ErrorCodeValue NotFound = ErrorCodeValue.NotFound;

		// Token: 0x04000239 RID: 569
		public const ErrorCodeValue VersionMismatch = ErrorCodeValue.VersionMismatch;

		// Token: 0x0400023A RID: 570
		public const ErrorCodeValue LogonFailed = ErrorCodeValue.LogonFailed;

		// Token: 0x0400023B RID: 571
		public const ErrorCodeValue SessionLimit = ErrorCodeValue.SessionLimit;

		// Token: 0x0400023C RID: 572
		public const ErrorCodeValue UserCancel = ErrorCodeValue.UserCancel;

		// Token: 0x0400023D RID: 573
		public const ErrorCodeValue UnableToAbort = ErrorCodeValue.UnableToAbort;

		// Token: 0x0400023E RID: 574
		public const ErrorCodeValue NetworkError = ErrorCodeValue.NetworkError;

		// Token: 0x0400023F RID: 575
		public const ErrorCodeValue DiskError = ErrorCodeValue.DiskError;

		// Token: 0x04000240 RID: 576
		public const ErrorCodeValue TooComplex = ErrorCodeValue.TooComplex;

		// Token: 0x04000241 RID: 577
		public const ErrorCodeValue ConditionViolation = ErrorCodeValue.ConditionViolation;

		// Token: 0x04000242 RID: 578
		public const ErrorCodeValue BadColumn = ErrorCodeValue.BadColumn;

		// Token: 0x04000243 RID: 579
		public const ErrorCodeValue ExtendedError = ErrorCodeValue.ExtendedError;

		// Token: 0x04000244 RID: 580
		public const ErrorCodeValue Computed = ErrorCodeValue.Computed;

		// Token: 0x04000245 RID: 581
		public const ErrorCodeValue CorruptData = ErrorCodeValue.CorruptData;

		// Token: 0x04000246 RID: 582
		public const ErrorCodeValue Unconfigured = ErrorCodeValue.Unconfigured;

		// Token: 0x04000247 RID: 583
		public const ErrorCodeValue FailOneProvider = ErrorCodeValue.FailOneProvider;

		// Token: 0x04000248 RID: 584
		public const ErrorCodeValue UnknownCPID = ErrorCodeValue.UnknownCPID;

		// Token: 0x04000249 RID: 585
		public const ErrorCodeValue UnknownLCID = ErrorCodeValue.UnknownLCID;

		// Token: 0x0400024A RID: 586
		public const ErrorCodeValue PasswordChangeRequired = ErrorCodeValue.PasswordChangeRequired;

		// Token: 0x0400024B RID: 587
		public const ErrorCodeValue PasswordExpired = ErrorCodeValue.PasswordExpired;

		// Token: 0x0400024C RID: 588
		public const ErrorCodeValue InvalidWorkstationAccount = ErrorCodeValue.InvalidWorkstationAccount;

		// Token: 0x0400024D RID: 589
		public const ErrorCodeValue InvalidAccessTime = ErrorCodeValue.InvalidAccessTime;

		// Token: 0x0400024E RID: 590
		public const ErrorCodeValue AccountDisabled = ErrorCodeValue.AccountDisabled;

		// Token: 0x0400024F RID: 591
		public const ErrorCodeValue EndOfSession = ErrorCodeValue.EndOfSession;

		// Token: 0x04000250 RID: 592
		public const ErrorCodeValue UnknownEntryId = ErrorCodeValue.UnknownEntryId;

		// Token: 0x04000251 RID: 593
		public const ErrorCodeValue MissingRequiredColumn = ErrorCodeValue.MissingRequiredColumn;

		// Token: 0x04000252 RID: 594
		public const ErrorCodeValue FailCallback = ErrorCodeValue.FailCallback;

		// Token: 0x04000253 RID: 595
		public const ErrorCodeValue BadValue = ErrorCodeValue.BadValue;

		// Token: 0x04000254 RID: 596
		public const ErrorCodeValue InvalidType = ErrorCodeValue.InvalidType;

		// Token: 0x04000255 RID: 597
		public const ErrorCodeValue TypeNoSupport = ErrorCodeValue.TypeNoSupport;

		// Token: 0x04000256 RID: 598
		public const ErrorCodeValue UnexpectedType = ErrorCodeValue.UnexpectedType;

		// Token: 0x04000257 RID: 599
		public const ErrorCodeValue TooBig = ErrorCodeValue.TooBig;

		// Token: 0x04000258 RID: 600
		public const ErrorCodeValue DeclineCopy = ErrorCodeValue.DeclineCopy;

		// Token: 0x04000259 RID: 601
		public const ErrorCodeValue UnexpectedId = ErrorCodeValue.UnexpectedId;

		// Token: 0x0400025A RID: 602
		public const ErrorCodeValue UnableToComplete = ErrorCodeValue.UnableToComplete;

		// Token: 0x0400025B RID: 603
		public const ErrorCodeValue Timeout = ErrorCodeValue.Timeout;

		// Token: 0x0400025C RID: 604
		public const ErrorCodeValue TableEmpty = ErrorCodeValue.TableEmpty;

		// Token: 0x0400025D RID: 605
		public const ErrorCodeValue TableTooBig = ErrorCodeValue.TableTooBig;

		// Token: 0x0400025E RID: 606
		public const ErrorCodeValue InvalidBookmark = ErrorCodeValue.InvalidBookmark;

		// Token: 0x0400025F RID: 607
		public const ErrorCodeValue DataLoss = ErrorCodeValue.DataLoss;

		// Token: 0x04000260 RID: 608
		public const ErrorCodeValue Wait = ErrorCodeValue.Wait;

		// Token: 0x04000261 RID: 609
		public const ErrorCodeValue Cancel = ErrorCodeValue.Cancel;

		// Token: 0x04000262 RID: 610
		public const ErrorCodeValue NotMe = ErrorCodeValue.NotMe;

		// Token: 0x04000263 RID: 611
		public const ErrorCodeValue CorruptStore = ErrorCodeValue.CorruptStore;

		// Token: 0x04000264 RID: 612
		public const ErrorCodeValue NotInQueue = ErrorCodeValue.NotInQueue;

		// Token: 0x04000265 RID: 613
		public const ErrorCodeValue NoSuppress = ErrorCodeValue.NoSuppress;

		// Token: 0x04000266 RID: 614
		public const ErrorCodeValue Collision = ErrorCodeValue.Collision;

		// Token: 0x04000267 RID: 615
		public const ErrorCodeValue NotInitialized = ErrorCodeValue.NotInitialized;

		// Token: 0x04000268 RID: 616
		public const ErrorCodeValue NonStandard = ErrorCodeValue.NonStandard;

		// Token: 0x04000269 RID: 617
		public const ErrorCodeValue NoRecipients = ErrorCodeValue.NoRecipients;

		// Token: 0x0400026A RID: 618
		public const ErrorCodeValue Submitted = ErrorCodeValue.Submitted;

		// Token: 0x0400026B RID: 619
		public const ErrorCodeValue HasFolders = ErrorCodeValue.HasFolders;

		// Token: 0x0400026C RID: 620
		public const ErrorCodeValue HasMessages = ErrorCodeValue.HasMessages;

		// Token: 0x0400026D RID: 621
		public const ErrorCodeValue FolderCycle = ErrorCodeValue.FolderCycle;

		// Token: 0x0400026E RID: 622
		public const ErrorCodeValue RootFolder = ErrorCodeValue.FolderCycle;

		// Token: 0x0400026F RID: 623
		public const ErrorCodeValue RecursionLimit = ErrorCodeValue.RecursionLimit;

		// Token: 0x04000270 RID: 624
		public const ErrorCodeValue LockIdLimit = ErrorCodeValue.LockIdLimit;

		// Token: 0x04000271 RID: 625
		public const ErrorCodeValue TooManyMountedDatabases = ErrorCodeValue.TooManyMountedDatabases;

		// Token: 0x04000272 RID: 626
		public const ErrorCodeValue PartialItem = ErrorCodeValue.PartialItem;

		// Token: 0x04000273 RID: 627
		public const ErrorCodeValue AmbiguousRecip = ErrorCodeValue.AmbiguousRecip;

		// Token: 0x04000274 RID: 628
		public const ErrorCodeValue SyncObjectDeleted = ErrorCodeValue.SyncObjectDeleted;

		// Token: 0x04000275 RID: 629
		public const ErrorCodeValue SyncIgnore = ErrorCodeValue.SyncIgnore;

		// Token: 0x04000276 RID: 630
		public const ErrorCodeValue SyncConflict = ErrorCodeValue.SyncConflict;

		// Token: 0x04000277 RID: 631
		public const ErrorCodeValue SyncNoParent = ErrorCodeValue.SyncNoParent;

		// Token: 0x04000278 RID: 632
		public const ErrorCodeValue SyncIncest = ErrorCodeValue.SyncIncest;

		// Token: 0x04000279 RID: 633
		public const ErrorCodeValue ErrorPathNotFound = ErrorCodeValue.ErrorPathNotFound;

		// Token: 0x0400027A RID: 634
		public const ErrorCodeValue NoAccess = ErrorCodeValue.NoAccess;

		// Token: 0x0400027B RID: 635
		public const ErrorCodeValue NotEnoughMemory = ErrorCodeValue.NotEnoughMemory;

		// Token: 0x0400027C RID: 636
		public const ErrorCodeValue InvalidParameter = ErrorCodeValue.InvalidParameter;

		// Token: 0x0400027D RID: 637
		public const ErrorCodeValue ErrorCanNotComplete = ErrorCodeValue.ErrorCanNotComplete;

		// Token: 0x0400027E RID: 638
		public const ErrorCodeValue NamedPropQuotaExceeded = ErrorCodeValue.NamedPropQuotaExceeded;

		// Token: 0x0400027F RID: 639
		public const ErrorCodeValue TooManyRecips = ErrorCodeValue.TooManyRecips;

		// Token: 0x04000280 RID: 640
		public const ErrorCodeValue TooManyProps = ErrorCodeValue.TooManyProps;

		// Token: 0x04000281 RID: 641
		public const ErrorCodeValue ParameterOverflow = ErrorCodeValue.ParameterOverflow;

		// Token: 0x04000282 RID: 642
		public const ErrorCodeValue BadFolderName = ErrorCodeValue.BadFolderName;

		// Token: 0x04000283 RID: 643
		public const ErrorCodeValue SearchFolder = ErrorCodeValue.SearchFolder;

		// Token: 0x04000284 RID: 644
		public const ErrorCodeValue NotSearchFolder = ErrorCodeValue.NotSearchFolder;

		// Token: 0x04000285 RID: 645
		public const ErrorCodeValue FolderSetReceive = ErrorCodeValue.FolderSetReceive;

		// Token: 0x04000286 RID: 646
		public const ErrorCodeValue NoReceiveFolder = ErrorCodeValue.NoReceiveFolder;

		// Token: 0x04000287 RID: 647
		public const ErrorCodeValue InvalidRecipients = ErrorCodeValue.InvalidRecipients;

		// Token: 0x04000288 RID: 648
		public const ErrorCodeValue BufferTooSmall = ErrorCodeValue.BufferTooSmall;

		// Token: 0x04000289 RID: 649
		public const ErrorCodeValue RequiresRefResolve = ErrorCodeValue.RequiresRefResolve;

		// Token: 0x0400028A RID: 650
		public const ErrorCodeValue NullObject = ErrorCodeValue.NullObject;

		// Token: 0x0400028B RID: 651
		public const ErrorCodeValue SendAsDenied = ErrorCodeValue.SendAsDenied;

		// Token: 0x0400028C RID: 652
		public const ErrorCodeValue DestinationNullObject = ErrorCodeValue.DestinationNullObject;

		// Token: 0x0400028D RID: 653
		public const ErrorCodeValue NoService = ErrorCodeValue.NoService;

		// Token: 0x0400028E RID: 654
		public const ErrorCodeValue ErrorsReturned = ErrorCodeValue.ErrorsReturned;

		// Token: 0x0400028F RID: 655
		public const ErrorCodeValue PositionChanged = ErrorCodeValue.PositionChanged;

		// Token: 0x04000290 RID: 656
		public const ErrorCodeValue ApproxCount = ErrorCodeValue.ApproxCount;

		// Token: 0x04000291 RID: 657
		public const ErrorCodeValue CancelMessage = ErrorCodeValue.CancelMessage;

		// Token: 0x04000292 RID: 658
		public const ErrorCodeValue PartialCompletion = ErrorCodeValue.PartialCompletion;

		// Token: 0x04000293 RID: 659
		public const ErrorCodeValue SecurityRequiredLow = ErrorCodeValue.SecurityRequiredLow;

		// Token: 0x04000294 RID: 660
		public const ErrorCodeValue SecuirtyRequiredMedium = ErrorCodeValue.SecuirtyRequiredMedium;

		// Token: 0x04000295 RID: 661
		public const ErrorCodeValue PartialItems = ErrorCodeValue.PartialItems;

		// Token: 0x04000296 RID: 662
		public const ErrorCodeValue SyncProgress = ErrorCodeValue.SyncProgress;

		// Token: 0x04000297 RID: 663
		public const ErrorCodeValue SyncClientChangeNewer = ErrorCodeValue.SyncClientChangeNewer;

		// Token: 0x04000298 RID: 664
		public const ErrorCodeValue Exiting = ErrorCodeValue.Exiting;

		// Token: 0x04000299 RID: 665
		public const ErrorCodeValue MdbNotInitialized = ErrorCodeValue.MdbNotInitialized;

		// Token: 0x0400029A RID: 666
		public const ErrorCodeValue ServerOutOfMemory = ErrorCodeValue.ServerOutOfMemory;

		// Token: 0x0400029B RID: 667
		public const ErrorCodeValue MailboxInTransit = ErrorCodeValue.MailboxInTransit;

		// Token: 0x0400029C RID: 668
		public const ErrorCodeValue BackupInProgress = ErrorCodeValue.BackupInProgress;

		// Token: 0x0400029D RID: 669
		public const ErrorCodeValue InvalidBackupSequence = ErrorCodeValue.InvalidBackupSequence;

		// Token: 0x0400029E RID: 670
		public const ErrorCodeValue WrongServer = ErrorCodeValue.WrongServer;

		// Token: 0x0400029F RID: 671
		public const ErrorCodeValue MailboxQuarantined = ErrorCodeValue.MailboxQuarantined;

		// Token: 0x040002A0 RID: 672
		public const ErrorCodeValue MountInProgress = ErrorCodeValue.MountInProgress;

		// Token: 0x040002A1 RID: 673
		public const ErrorCodeValue DismountInProgress = ErrorCodeValue.DismountInProgress;

		// Token: 0x040002A2 RID: 674
		public const ErrorCodeValue CannotRegisterNewReplidGuidMapping = ErrorCodeValue.CannotRegisterNewReplidGuidMapping;

		// Token: 0x040002A3 RID: 675
		public const ErrorCodeValue CannotRegisterNewNamedPropertyMapping = ErrorCodeValue.CannotRegisterNewNamedPropertyMapping;

		// Token: 0x040002A4 RID: 676
		public const ErrorCodeValue CannotPreserveMailboxSignature = ErrorCodeValue.CannotPreserveMailboxSignature;

		// Token: 0x040002A5 RID: 677
		public const ErrorCodeValue ExceptionThrown = ErrorCodeValue.ExceptionThrown;

		// Token: 0x040002A6 RID: 678
		public const ErrorCodeValue SessionLocked = ErrorCodeValue.SessionLocked;

		// Token: 0x040002A7 RID: 679
		public const ErrorCodeValue DuplicateObject = ErrorCodeValue.DuplicateObject;

		// Token: 0x040002A8 RID: 680
		public const ErrorCodeValue DuplicateDelivery = ErrorCodeValue.DuplicateDelivery;

		// Token: 0x040002A9 RID: 681
		public const ErrorCodeValue UnregisteredNamedProp = ErrorCodeValue.UnregisteredNamedProp;

		// Token: 0x040002AA RID: 682
		public const ErrorCodeValue TaskRequestFailed = ErrorCodeValue.TaskRequestFailed;

		// Token: 0x040002AB RID: 683
		public const ErrorCodeValue NoReplicaHere = ErrorCodeValue.NoReplicaHere;

		// Token: 0x040002AC RID: 684
		public const ErrorCodeValue NoReplicaAvailable = ErrorCodeValue.NoReplicaAvailable;

		// Token: 0x040002AD RID: 685
		public static readonly ErrorCode NoError = new ErrorCode(ErrorCodeValue.NoError);

		// Token: 0x040002AE RID: 686
		private readonly ErrorCodeValue errorCodeValue;
	}
}

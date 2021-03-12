using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000277 RID: 631
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3E783D77-6B93-4270-ABE2-0EBAF1905DF4")]
	[ComImport]
	internal interface IExRpcAdmin
	{
		// Token: 0x06000AE0 RID: 2784
		[PreserveSig]
		int HrGetServerVersion(out short pwMajor, out short pwMinor);

		// Token: 0x06000AE1 RID: 2785
		[PreserveSig]
		int HrGetMailboxInfoSize(out int cbInfo);

		// Token: 0x06000AE2 RID: 2786
		[PreserveSig]
		int HrDeletePrivateMailbox([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int ulFlags);

		// Token: 0x06000AE3 RID: 2787
		[PreserveSig]
		int HrGetMailboxBasicInfo([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] rgbInfo, int cbInfo);

		// Token: 0x06000AE4 RID: 2788
		[PreserveSig]
		int HrSetMailboxBasicInfo([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] rgbInfo, int cbInfo);

		// Token: 0x06000AE5 RID: 2789
		[PreserveSig]
		int HrNotifyOnDSChange([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int ulObject);

		// Token: 0x06000AE6 RID: 2790
		[PreserveSig]
		int HrListMdbStatus(int cMdb, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgguidMdb, [MarshalAs(UnmanagedType.LPArray)] [Out] uint[] rgulMdbStatus);

		// Token: 0x06000AE7 RID: 2791
		[PreserveSig]
		unsafe int HrReadMapiEvents([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, long llStartEvent, int cEventWanted, int cEventsToCheck, [In] SRestriction* pFilter, int ulFlags, out int pcEventActual, out SafeExLinkedMemoryHandle pEvents, out long pllEndCounter);

		// Token: 0x06000AE8 RID: 2792
		[PreserveSig]
		int HrReadLastMapiEvent([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, out SafeExLinkedMemoryHandle pEvent);

		// Token: 0x06000AE9 RID: 2793
		[PreserveSig]
		int HrSaveWatermarks([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, int cWM, IntPtr pWMs);

		// Token: 0x06000AEA RID: 2794
		[PreserveSig]
		int HrGetWatermarks([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidConsumer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, out int pcWM, out SafeExMemoryHandle pWMs);

		// Token: 0x06000AEB RID: 2795
		[PreserveSig]
		int HrGetWatermarksForMailbox([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailboxDS, out int pcWMs, out IntPtr ppWMs);

		// Token: 0x06000AEC RID: 2796
		[PreserveSig]
		int HrDeleteWatermarksForMailbox([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailboxDS, out int pcDel);

		// Token: 0x06000AED RID: 2797
		[PreserveSig]
		int HrDeleteWatermarksForConsumer([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailboxDS, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidConsumer, out int pcDel);

		// Token: 0x06000AEE RID: 2798
		[PreserveSig]
		int HrGetMailboxTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [In] uint flags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000AEF RID: 2799
		[PreserveSig]
		int HrGetLogonTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000AF0 RID: 2800
		[PreserveSig]
		int HrGetMailboxSecurityDescriptor([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, out SafeExMemoryHandle ppntsd, out int pcntsd);

		// Token: 0x06000AF1 RID: 2801
		[PreserveSig]
		int HrSetMailboxSecurityDescriptor([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pntsd, int cntsd);

		// Token: 0x06000AF2 RID: 2802
		[PreserveSig]
		int HrMountDatabase([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidStorageGroup, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int ulFlags);

		// Token: 0x06000AF3 RID: 2803
		[PreserveSig]
		int HrUnmountDatabase([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidStorageGroup, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int grfFlags);

		// Token: 0x06000AF4 RID: 2804
		[PreserveSig]
		int HrFlushCache(out int pcMDBs, out SafeExMemoryHandle pCheckpointStatus);

		// Token: 0x06000AF5 RID: 2805
		[PreserveSig]
		int HrGetLastBackupTimes([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out long ftLastCompleteBackupTime, out long ftLastIncrementalBackupTime);

		// Token: 0x06000AF6 RID: 2806
		[PreserveSig]
		int HrGetLastBackupInfo([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out long ftLastCompleteBackupTime, out long ftLastIncrementalBackupTime, out long ftLastDifferentialBackup, out long ftLastCopyBackup, out int SnapFull, out int SnapIncremental, out int SnapDifferential, out int SnapCopy);

		// Token: 0x06000AF7 RID: 2807
		[PreserveSig]
		int HrPurgeCachedMailboxObject([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000AF8 RID: 2808
		[PreserveSig]
		int HrPurgeCachedMdbObject([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000AF9 RID: 2809
		[PreserveSig]
		int HrClearAbsentInDsFlagOnMailbox([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000AFA RID: 2810
		[PreserveSig]
		int HrSyncMailboxesWithDS([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000AFB RID: 2811
		[PreserveSig]
		int HrHasLocalReplicas([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.Bool)] out bool fHasReplicas);

		// Token: 0x06000AFC RID: 2812
		[PreserveSig]
		int HrListAllMdbStatus([MarshalAs(UnmanagedType.Bool)] bool fBasicInformation, out int pcMdbs, out SafeExMemoryHandle pMdbStatus);

		// Token: 0x06000AFD RID: 2813
		[PreserveSig]
		int HrGetReplicaInformationTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000AFE RID: 2814
		[PreserveSig]
		int HrSyncMailboxWithDS([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000AFF RID: 2815
		[PreserveSig]
		int HrCiCreatePropertyStore([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance);

		// Token: 0x06000B00 RID: 2816
		[PreserveSig]
		int HrCiDeletePropertyStore([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance);

		// Token: 0x06000B01 RID: 2817
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		unsafe int HrCiAddMappings([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cEntryIds, _SBinaryArray* pEntryIdList, [MarshalAs(UnmanagedType.LPArray)] long[] pReceiveTimes, uint ulBatchID, out uint* rgulDocumentIds);

		// Token: 0x06000B02 RID: 2818
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		int HrCiDeleteMapping([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId);

		// Token: 0x06000B03 RID: 2819
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		int HrCiDeleteFolderMapping([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId);

		// Token: 0x06000B04 RID: 2820
		[PreserveSig]
		int HrCiDeleteMailboxMapping([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000B05 RID: 2821
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		unsafe int HrCiUpdateStatesByDocumentIdsOnBatchCompletion([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, int cDocumentIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulDocumentIds, _SBinaryArray* pEntryIdList, [MarshalAs(UnmanagedType.LPArray)] int[] rgHresults, uint ulBatchId);

		// Token: 0x06000B06 RID: 2822
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		unsafe int HrCiUpdateStatesByDocumentIdsOnBatchStart([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, int cDocumentIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulDocumentIds, _SBinaryArray* pEntryIdList, [MarshalAs(UnmanagedType.Bool)] bool fRetryStatesExpected, uint ulBatchId);

		// Token: 0x06000B07 RID: 2823
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		unsafe int HrCiGetDataFromDocumentIds([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, int cDocumentIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulDocumentIds, _SBinaryArray* pEntryIdList, out short* rgsStates, out uint* rgulBatchIds);

		// Token: 0x06000B08 RID: 2824
		[Obsolete("use HrCiEnumerateRetryTable", true)]
		[PreserveSig]
		unsafe int HrCiEnumerateRetryList([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, out int cDocumentIds, out uint* rgulDocumentIds, out _SBinaryArray* pEntryIdList);

		// Token: 0x06000B09 RID: 2825
		[PreserveSig]
		int HrCiMoveDocument([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryIdSource, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryIdSource, int cbEntryIdTarget, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryIdTarget);

		// Token: 0x06000B0A RID: 2826
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		unsafe int HrCiGetEntryIdFromDocumentId([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, uint ulDocumentId, out int cbEntryId, out byte* pEntryId, out Guid pguidMailbox);

		// Token: 0x06000B0B RID: 2827
		[PreserveSig]
		int HrCiGetWaterMark([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.Bool)] bool fIsHighWatermark, out ulong ullWatermark, out System.Runtime.InteropServices.ComTypes.FILETIME lastAccessTime);

		// Token: 0x06000B0C RID: 2828
		[PreserveSig]
		int HrCiSetWaterMark([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.Bool)] bool fIsHighWatermark, ulong ullWatermark);

		// Token: 0x06000B0D RID: 2829
		[PreserveSig]
		int HrCiGetMailboxState([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, out System.Runtime.InteropServices.ComTypes.FILETIME ftStart, out uint ulState, out ulong ullEventCounter);

		// Token: 0x06000B0E RID: 2830
		[PreserveSig]
		int HrCiSetMailboxStateAndStartDate([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME pftStart, uint ulState);

		// Token: 0x06000B0F RID: 2831
		[PreserveSig]
		int HrCiSetMailboxState([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, uint ulState);

		// Token: 0x06000B10 RID: 2832
		[PreserveSig]
		int HrCiSetMailboxEventCounter([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, ulong ullEventCounter);

		// Token: 0x06000B11 RID: 2833
		[PreserveSig]
		int HrCiEnumerateMailboxesByState([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, uint ulState, out int cMailboxes, out SafeExMemoryHandle rgGuidMailboxes);

		// Token: 0x06000B12 RID: 2834
		[PreserveSig]
		int HrCiPurgeMailboxes([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, uint ulState);

		// Token: 0x06000B13 RID: 2835
		[PreserveSig]
		int HrCiSetCiEnabled([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.Bool)] bool fIsEnabled);

		// Token: 0x06000B14 RID: 2836
		[PreserveSig]
		int HrCiSetIndexedPtags([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int cptags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] rgptags);

		// Token: 0x06000B15 RID: 2837
		[PreserveSig]
		int HrCiGetDocumentIdFromEntryId([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryID, out uint ulDocumentId);

		// Token: 0x06000B16 RID: 2838
		[PreserveSig]
		int HrDoMaintenanceTask([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int ulTask);

		// Token: 0x06000B17 RID: 2839
		[PreserveSig]
		int HrExecuteTask([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidTaskClass, int taskId);

		// Token: 0x06000B18 RID: 2840
		[PreserveSig]
		unsafe int EcReadMdbEvents([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdbVer, [In] int cbRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbRequest, out int cbResponse, _SBinaryArray* pbResponse);

		// Token: 0x06000B19 RID: 2841
		[PreserveSig]
		unsafe int EcWriteMdbEvents([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdbVer, [In] int cbRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte* pbRequest, out int cbResponse, _SBinaryArray* pbResponse);

		// Token: 0x06000B1A RID: 2842
		[PreserveSig]
		int HrTriggerPFSync([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] int cbEntryId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] pEntryId, int ulFlags);

		// Token: 0x06000B1B RID: 2843
		[PreserveSig]
		int HrSetPFReplicas([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] rgszDN, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] rgulAgeLimit, int ulSize);

		// Token: 0x06000B1C RID: 2844
		[PreserveSig]
		int HrCiGetCatalogState([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, out short catalogState, out ulong propertyBlobSize, out SafeExMemoryHandle propertyBlob);

		// Token: 0x06000B1D RID: 2845
		[PreserveSig]
		int HrCiSetCatalogState([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, short catalogState, [In] uint cbPropertyBlob, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] propertyBlob);

		// Token: 0x06000B1E RID: 2846
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		int HrCiGetFailedItems([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] ulong itemNumber, [PointerType("SRowSet *")] out SafeExLinkedMemoryHandle lpSRowset);

		// Token: 0x06000B1F RID: 2847
		[Obsolete("This feature is gone.", true)]
		[PreserveSig]
		unsafe int HrCiGetDocumentStatesFromEntryIDs([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgGuidMailboxList, _SBinaryArray* pEntryIdList, out short* rgsStates);

		// Token: 0x06000B20 RID: 2848
		[PreserveSig]
		int HrIntegCheck([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] ref IntegrityTestResult pTestParam);

		// Token: 0x06000B21 RID: 2849
		[PreserveSig]
		int HrIntegGetProgress([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, long iTest, out long pcTotalTest, out long piCurrentTest, out long pcCurrentPercent, out IntegrityTestResult pTestResult);

		// Token: 0x06000B22 RID: 2850
		[PreserveSig]
		int HrIntegGetCancel([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out long pcTotalTest, out long piCurrentTest, out long pcCurrentPercent, out IntegrityTestResult pTestResult);

		// Token: 0x06000B23 RID: 2851
		[PreserveSig]
		int HrIntegGetDone([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000B24 RID: 2852
		[PreserveSig]
		int Slot1();

		// Token: 0x06000B25 RID: 2853
		[PreserveSig]
		int HrCiUpdateRetryTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, int cDocumentIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulDocumentIds, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgMailboxGuids, [MarshalAs(UnmanagedType.LPArray)] int[] rgHresults, [MarshalAs(UnmanagedType.LPArray)] short[] initialStates);

		// Token: 0x06000B26 RID: 2854
		[PreserveSig]
		int HrCiEnumerateRetryTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, out int cDocumentIds, [PointerType("uint*")] out SafeExMemoryHandle rgulDocumentIds, [PointerType("Guid*")] out SafeExMemoryHandle rgMailboxGuids, [PointerType("short*")] out SafeExMemoryHandle rgStates);

		// Token: 0x06000B27 RID: 2855
		[PreserveSig]
		int HrCiEntryIdFromDocumentId([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, uint ulDocumentId, out int cbEntryId, [PointerType("byte*")] out SafeExLinkedMemoryHandle pEntryId);

		// Token: 0x06000B28 RID: 2856
		[PreserveSig]
		int HrGetPublicFolderDN(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId, [MarshalAs(UnmanagedType.LPWStr)] string folderName, out SafeExMemoryHandle lppszDN);

		// Token: 0x06000B29 RID: 2857
		[PreserveSig]
		int HrCiSeedPropertyStore([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidSourceInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidDestinationInstance);

		// Token: 0x06000B2A RID: 2858
		[PreserveSig]
		int HrLogReplayRequest([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] uint ulgenLogReplayMax, out uint ulgenLogReplayNext, out uint pCbOut, out SafeExMemoryHandle pDbinfo);

		// Token: 0x06000B2B RID: 2859
		[PreserveSig]
		int HrStartBlockModeReplicationToPassive([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPWStr)] [In] string passiveName, [In] uint ulFirstGenToSend);

		// Token: 0x06000B2C RID: 2860
		[PreserveSig]
		int HrGetRestrictionTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000B2D RID: 2861
		[PreserveSig]
		int HrGetViewsTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000B2E RID: 2862
		[PreserveSig]
		int HrGetDatabaseSize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out ulong dbTotalPages, out ulong dbAvailablePages, out ulong dbPageSize);

		// Token: 0x06000B2F RID: 2863
		[PreserveSig]
		int HrCiUpdateFailedItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint documentId, [In] uint errorCode, [In] uint flags);

		// Token: 0x06000B30 RID: 2864
		[PreserveSig]
		int HrCiEnumerateFailedItems([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] uint lastMaxDocId, [PointerType("SRowSet *")] out SafeExLinkedMemoryHandle lpSRowset);

		// Token: 0x06000B31 RID: 2865
		[PreserveSig]
		int HrPrePopulateCache([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStr)] [In] string legacyDN, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] int cbPartitionHint, [MarshalAs(UnmanagedType.LPArray)] byte[] pbPartitionHint, [MarshalAs(UnmanagedType.LPStr)] [In] string dcName);

		// Token: 0x06000B32 RID: 2866
		[PreserveSig]
		int HrGetMailboxTableEntry([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000B33 RID: 2867
		[PreserveSig]
		int HrGetMailboxTableEntryFlags([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint flags, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000B34 RID: 2868
		[PreserveSig]
		int HrCiEnumerateFailedItemsByMaillbox([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint lastMaxDocId, [PointerType("SRowSet *")] out SafeExLinkedMemoryHandle lpSRowset);

		// Token: 0x06000B35 RID: 2869
		[PreserveSig]
		int HrCiUpdateFailedItemAndRetryTableByErrorCode([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] uint errorCode, [In] uint lastMaxDocId, out uint curMaxDocId, out uint itemNumber);

		// Token: 0x06000B36 RID: 2870
		[PreserveSig]
		int HrGetPublicFolderGlobalsTable([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000B37 RID: 2871
		[PreserveSig]
		int HrCiGetTableSize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] short tableId, [In] ulong ulFlags, out ulong pulSize);

		// Token: 0x06000B38 RID: 2872
		[PreserveSig]
		int HrMultiMailboxSearch([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, ulong cbSearchRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pSearchRequest, out ulong cbSearchResponse, out SafeExMemoryHandle pSearchResponse);

		// Token: 0x06000B39 RID: 2873
		[PreserveSig]
		int HrGetMultiMailboxSearchKeywordStats([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, ulong cbSearchRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pSearchRequest, out ulong cbSearchResponse, out SafeExMemoryHandle pSearchResponse);

		// Token: 0x06000B3A RID: 2874
		[PreserveSig]
		int HrGetDatabaseProcessInfo([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowSet);

		// Token: 0x06000B3B RID: 2875
		[PreserveSig]
		int HrProcessSnapshotOperation([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pGuidMdb, [In] uint opCode, [In] uint flags);

		// Token: 0x06000B3C RID: 2876
		[PreserveSig]
		int HrGetPhysicalDatabaseInformation([MarshalAs(UnmanagedType.LPStruct)] [In] Guid pGuidMdb, out uint pCbOut, out SafeExMemoryHandle pDbinfo);

		// Token: 0x06000B3D RID: 2877
		[PreserveSig]
		unsafe int HrFormatSearchRestriction([In] SRestriction* pRestriction, out long cbFormatted, out SafeExMemoryHandle pbFormatted);
	}
}

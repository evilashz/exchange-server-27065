using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002CB RID: 715
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExRpcAdminHandle : SafeExInterfaceHandle
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x0003729F File Offset: 0x0003549F
		protected SafeExRpcAdminHandle()
		{
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000372A7 File Offset: 0x000354A7
		internal SafeExRpcAdminHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000372B0 File Offset: 0x000354B0
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExRpcAdminHandle>(this);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000372B8 File Offset: 0x000354B8
		internal int HrGetServerVersion(out short pwMajor, out short pwMinor)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetServerVersion(this.handle, out pwMajor, out pwMinor);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000372C7 File Offset: 0x000354C7
		internal int HrGetMailboxInfoSize(out int cbInfo)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxInfoSize(this.handle, out cbInfo);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000372D5 File Offset: 0x000354D5
		internal int HrDeletePrivateMailbox(Guid pguidMdb, Guid pguidMailbox, int ulFlags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrDeletePrivateMailbox(this.handle, pguidMdb, pguidMailbox, ulFlags);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000372E5 File Offset: 0x000354E5
		internal int HrGetMailboxBasicInfo(Guid pguidMdb, Guid pguidMailbox, byte[] rgbInfo, int cbInfo)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxBasicInfo(this.handle, pguidMdb, pguidMailbox, rgbInfo, cbInfo);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000372F7 File Offset: 0x000354F7
		internal int HrSetMailboxBasicInfo(Guid pguidMdb, Guid pguidMailbox, byte[] rgbInfo, int cbInfo)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrSetMailboxBasicInfo(this.handle, pguidMdb, pguidMailbox, rgbInfo, cbInfo);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00037309 File Offset: 0x00035509
		internal int HrNotifyOnDSChange(Guid pguidMdb, Guid pguidMailbox, int ulObject)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrNotifyOnDSChange(this.handle, pguidMdb, pguidMailbox, ulObject);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00037319 File Offset: 0x00035519
		internal int HrListMdbStatus(int cMdb, Guid[] rgguidMdb, uint[] rgulMdbStatus)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrListMdbStatus(this.handle, cMdb, rgguidMdb, rgulMdbStatus);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003732C File Offset: 0x0003552C
		internal unsafe int HrReadMapiEvents(Guid pguidMdb, ref Guid pguidMdbVer, long llStartEvent, int cEventWanted, int cEventsToCheck, SRestriction* pFilter, int ulFlags, out int pcEventActual, out SafeExLinkedMemoryHandle pEvents, out long pllEndCounter)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrReadMapiEvents(this.handle, pguidMdb, ref pguidMdbVer, llStartEvent, cEventWanted, cEventsToCheck, pFilter, ulFlags, out pcEventActual, out pEvents, out pllEndCounter);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00037355 File Offset: 0x00035555
		internal int HrReadLastMapiEvent(Guid pguidMdb, ref Guid pguidMdbVer, out SafeExLinkedMemoryHandle pEvent)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrReadLastMapiEvent(this.handle, pguidMdb, ref pguidMdbVer, out pEvent);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00037365 File Offset: 0x00035565
		internal int HrSaveWatermarks(Guid pguidMdb, ref Guid pguidMdbVer, int cWM, IntPtr pWMs)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrSaveWatermarks(this.handle, pguidMdb, ref pguidMdbVer, cWM, pWMs);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00037377 File Offset: 0x00035577
		internal int HrGetWatermarks(Guid pguidMdb, ref Guid pguidMdbVer, Guid pguidConsumer, Guid pguidMailbox, out int pcWM, out SafeExMemoryHandle pWMs)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetWatermarks(this.handle, pguidMdb, ref pguidMdbVer, pguidConsumer, pguidMailbox, out pcWM, out pWMs);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003738D File Offset: 0x0003558D
		internal int HrGetWatermarksForMailbox(Guid pguidMdb, ref Guid pguidMdbVer, Guid pguidMailboxDS, out int pcWMs, out SafeExMemoryHandle pWMs)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetWatermarksForMailbox(this.handle, pguidMdb, ref pguidMdbVer, pguidMailboxDS, out pcWMs, out pWMs);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000373A1 File Offset: 0x000355A1
		internal int HrDeleteWatermarksForMailbox(Guid pguidMdb, ref Guid pguidMdbVer, Guid pguidMailboxDS, out int pcDel)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrDeleteWatermarksForMailbox(this.handle, pguidMdb, ref pguidMdbVer, pguidMailboxDS, out pcDel);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000373B3 File Offset: 0x000355B3
		internal int HrDeleteWatermarksForConsumer(Guid pguidMdb, ref Guid pguidMdbVer, Guid pguidMailboxDS, Guid pguidConsumer, out int pcDel)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrDeleteWatermarksForConsumer(this.handle, pguidMdb, ref pguidMdbVer, pguidMailboxDS, pguidConsumer, out pcDel);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000373C7 File Offset: 0x000355C7
		internal int HrGetMailboxTable(Guid pguidMdb, uint flags, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxTable(this.handle, pguidMdb, flags, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000373D9 File Offset: 0x000355D9
		internal int HrGetLogonTable(Guid pguidMdb, PropTag[] lpPropTagArray, int ulFlags, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetLogonTable(this.handle, pguidMdb, lpPropTagArray, ulFlags, out lpSRowset);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000373EB File Offset: 0x000355EB
		internal int HrGetMailboxSecurityDescriptor(Guid pguidMdb, Guid pguidMailbox, out SafeExMemoryHandle ppntsd, out int pcntsd)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxSecurityDescriptor(this.handle, pguidMdb, pguidMailbox, out ppntsd, out pcntsd);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000373FD File Offset: 0x000355FD
		internal int HrSetMailboxSecurityDescriptor(Guid pguidMdb, Guid pguidMailbox, byte[] pntsd, int cntsd)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrSetMailboxSecurityDescriptor(this.handle, pguidMdb, pguidMailbox, pntsd, cntsd);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003740F File Offset: 0x0003560F
		internal int HrMountDatabase(Guid pguidStorageGroup, Guid pguidMdb, int ulFlags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrMountDatabase(this.handle, pguidStorageGroup, pguidMdb, ulFlags);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003741F File Offset: 0x0003561F
		internal int HrUnmountDatabase(Guid pguidStorageGroup, Guid pguidMdb, int grfFlags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrUnmountDatabase(this.handle, pguidStorageGroup, pguidMdb, grfFlags);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003742F File Offset: 0x0003562F
		internal int HrFlushCache(out int pcMDBs, out SafeExMemoryHandle pCheckpointStatus)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrFlushCache(this.handle, out pcMDBs, out pCheckpointStatus);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003743E File Offset: 0x0003563E
		internal int HrGetLastBackupTimes(Guid pguidMdb, out long ftLastCompleteBackupTime, out long ftLastIncrementalBackupTime)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetLastBackupTimes(this.handle, pguidMdb, out ftLastCompleteBackupTime, out ftLastIncrementalBackupTime);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00037450 File Offset: 0x00035650
		internal int HrGetLastBackupInfo(Guid pguidMdb, out long ftLastCompleteBackupTime, out long ftLastIncrementalBackupTime, out long ftLastDifferentialBackup, out long ftLastCopyBackup, out int SnapFull, out int SnapIncremental, out int SnapDifferential, out int SnapCopy)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetLastBackupInfo(this.handle, pguidMdb, out ftLastCompleteBackupTime, out ftLastIncrementalBackupTime, out ftLastDifferentialBackup, out ftLastCopyBackup, out SnapFull, out SnapIncremental, out SnapDifferential, out SnapCopy);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00037477 File Offset: 0x00035677
		internal int HrPurgeCachedMailboxObject(Guid pguidMailbox)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrPurgeCachedMailboxObject(this.handle, pguidMailbox);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00037485 File Offset: 0x00035685
		internal int HrPurgeCachedMdbObject(Guid pguidMdb)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrPurgeCachedMdbObject(this.handle, pguidMdb);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00037493 File Offset: 0x00035693
		internal int HrClearAbsentInDsFlagOnMailbox(Guid pguidMdb, Guid pguidMailbox)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrClearAbsentInDsFlagOnMailbox(this.handle, pguidMdb, pguidMailbox);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000374A2 File Offset: 0x000356A2
		internal int HrSyncMailboxesWithDS(Guid pguidMdb)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrSyncMailboxesWithDS(this.handle, pguidMdb);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000374B0 File Offset: 0x000356B0
		internal int HrHasLocalReplicas(Guid pguidMdb, out bool fHasReplicas)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrHasLocalReplicas(this.handle, pguidMdb, out fHasReplicas);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000374BF File Offset: 0x000356BF
		internal int HrListAllMdbStatus(bool fBasicInformation, out int pcMdbs, out SafeExMemoryHandle pMdbStatus)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrListAllMdbStatus(this.handle, fBasicInformation, out pcMdbs, out pMdbStatus);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000374CF File Offset: 0x000356CF
		internal int HrGetReplicaInformationTable(Guid pguidMdb, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetReplicaInformationTable(this.handle, pguidMdb, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x000374DF File Offset: 0x000356DF
		internal int HrSyncMailboxWithDS(Guid pguidMdb, Guid pguidMailbox)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrSyncMailboxWithDS(this.handle, pguidMdb, pguidMailbox);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000374EE File Offset: 0x000356EE
		internal int HrCiCreatePropertyStore(Guid pguidMdb, Guid pguidInstance)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiCreatePropertyStore(this.handle, pguidMdb, pguidInstance);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000374FD File Offset: 0x000356FD
		internal int HrCiDeletePropertyStore(Guid pguidMdb, Guid pguidInstance)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiDeletePropertyStore(this.handle, pguidMdb, pguidInstance);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003750C File Offset: 0x0003570C
		internal int HrCiDeleteMailboxMapping(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiDeleteMailboxMapping(this.handle, pguidMdb, pguidInstance, pguidMailbox);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003751C File Offset: 0x0003571C
		internal int HrCiMoveDocument(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, int cbEntryIdSource, byte[] pEntryIdSource, int cbEntryIdTarget, byte[] pEntryIdTarget)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiMoveDocument(this.handle, pguidMdb, pguidInstance, pguidMailbox, cbEntryIdSource, pEntryIdSource, cbEntryIdTarget, pEntryIdTarget);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00037534 File Offset: 0x00035734
		internal int HrCiGetWaterMark(Guid pguidMdb, Guid pguidInstance, bool fIsHighWatermark, out ulong ullWatermark, out System.Runtime.InteropServices.ComTypes.FILETIME lastAccessTime)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiGetWaterMark(this.handle, pguidMdb, pguidInstance, fIsHighWatermark, out ullWatermark, out lastAccessTime);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00037548 File Offset: 0x00035748
		internal int HrCiSetWaterMark(Guid pguidMdb, Guid pguidInstance, bool fIsHighWatermark, ulong ullWatermark)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetWaterMark(this.handle, pguidMdb, pguidInstance, fIsHighWatermark, ullWatermark);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003755A File Offset: 0x0003575A
		internal int HrCiGetMailboxState(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, out System.Runtime.InteropServices.ComTypes.FILETIME ftStart, out uint ulState, out ulong ullEventCounter)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiGetMailboxState(this.handle, pguidMdb, pguidInstance, pguidMailbox, out ftStart, out ulState, out ullEventCounter);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00037570 File Offset: 0x00035770
		internal int HrCiSetMailboxStateAndStartDate(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, ref System.Runtime.InteropServices.ComTypes.FILETIME pftStart, uint ulState)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetMailboxStateAndStartDate(this.handle, pguidMdb, pguidInstance, pguidMailbox, ref pftStart, ulState);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00037584 File Offset: 0x00035784
		internal int HrCiSetMailboxState(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, uint ulState)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetMailboxState(this.handle, pguidMdb, pguidInstance, pguidMailbox, ulState);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00037596 File Offset: 0x00035796
		internal int HrCiSetMailboxEventCounter(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, ulong ullEventCounter)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetMailboxEventCounter(this.handle, pguidMdb, pguidInstance, pguidMailbox, ullEventCounter);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x000375A8 File Offset: 0x000357A8
		internal int HrCiEnumerateMailboxesByState(Guid pguidMdb, Guid pguidInstance, uint ulState, out int cMailboxes, out SafeExMemoryHandle rgGuidMailboxes)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiEnumerateMailboxesByState(this.handle, pguidMdb, pguidInstance, ulState, out cMailboxes, out rgGuidMailboxes);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000375BC File Offset: 0x000357BC
		internal int HrCiPurgeMailboxes(Guid pguidMdb, Guid pguidInstance, uint ulState)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiPurgeMailboxes(this.handle, pguidMdb, pguidInstance, ulState);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000375CC File Offset: 0x000357CC
		internal int HrCiSetCiEnabled(Guid pguidMdb, bool fIsEnabled)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetCiEnabled(this.handle, pguidMdb, fIsEnabled);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000375DB File Offset: 0x000357DB
		internal int HrCiSetIndexedPtags(Guid pguidMdb, int cptags, int[] rgptags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetIndexedPtags(this.handle, pguidMdb, cptags, rgptags);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000375EB File Offset: 0x000357EB
		internal int HrCiGetDocumentIdFromEntryId(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, int cbEntryId, byte[] pEntryID, out uint ulDocumentId)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiGetDocumentIdFromEntryId(this.handle, pguidMdb, pguidInstance, pguidMailbox, cbEntryId, pEntryID, out ulDocumentId);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00037601 File Offset: 0x00035801
		internal int HrDoMaintenanceTask(Guid pguidMdb, int ulTask)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrDoMaintenanceTask(this.handle, pguidMdb, ulTask);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00037610 File Offset: 0x00035810
		internal int HrExecuteTask(Guid pguidMdb, Guid pguidTaskClass, int taskId)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrExecuteTask(this.handle, pguidMdb, pguidTaskClass, taskId);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00037620 File Offset: 0x00035820
		internal unsafe int EcReadMdbEvents(Guid pguidMdb, Guid pguidMdbVer, int cbRequest, byte[] pbRequest, out int cbResponse, _SBinaryArray* pbResponse)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_EcReadMdbEvents(this.handle, pguidMdb, pguidMdbVer, cbRequest, pbRequest, out cbResponse, pbResponse);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00037636 File Offset: 0x00035836
		internal unsafe int EcWriteMdbEvents(Guid pguidMdb, Guid pguidMdbVer, int cbRequest, byte[] pbRequest, out int cbResponse, _SBinaryArray* pbResponse)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_EcWriteMdbEvents(this.handle, pguidMdb, pguidMdbVer, cbRequest, pbRequest, out cbResponse, pbResponse);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003764C File Offset: 0x0003584C
		internal int HrTriggerPFSync(Guid pguidMdb, int cbEntryId, byte[] pEntryId, int ulFlags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrTriggerPFSync(this.handle, pguidMdb, cbEntryId, pEntryId, ulFlags);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003765E File Offset: 0x0003585E
		internal int HrSetPFReplicas(Guid pguidMdb, string[] rgszDN, int[] rgulAgeLimit, int ulSize)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrSetPFReplicas(this.handle, pguidMdb, rgszDN, rgulAgeLimit, ulSize);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00037670 File Offset: 0x00035870
		internal int HrCiGetCatalogState(Guid pguidMdb, Guid pguidInstance, out short catalogState, out ulong propertyBlobSize, out SafeExMemoryHandle propertyBlob)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiGetCatalogState(this.handle, pguidMdb, pguidInstance, out catalogState, out propertyBlobSize, out propertyBlob);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00037684 File Offset: 0x00035884
		internal int HrCiSetCatalogState(Guid pguidMdb, Guid pguidInstance, short catalogState, uint cbPropertyBlob, byte[] propertyBlob)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSetCatalogState(this.handle, pguidMdb, pguidInstance, catalogState, cbPropertyBlob, propertyBlob);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00037698 File Offset: 0x00035898
		internal int HrIntegCheck(Guid pguidMdb, ref IntegrityTestResult pTestParam)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrIntegCheck(this.handle, pguidMdb, ref pTestParam);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000376A7 File Offset: 0x000358A7
		internal int HrIntegGetProgress(Guid pguidMdb, long iTest, out long pcTotalTest, out long piCurrentTest, out long pcCurrentPercent, out IntegrityTestResult pTestResult)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrIntegGetProgress(this.handle, pguidMdb, iTest, out pcTotalTest, out piCurrentTest, out pcCurrentPercent, out pTestResult);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000376BD File Offset: 0x000358BD
		internal int HrIntegGetCancel(Guid pguidMdb, out long pcTotalTest, out long piCurrentTest, out long pcCurrentPercent, out IntegrityTestResult pTestResult)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrIntegGetCancel(this.handle, pguidMdb, out pcTotalTest, out piCurrentTest, out pcCurrentPercent, out pTestResult);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000376D1 File Offset: 0x000358D1
		internal int HrIntegGetDone(Guid pguidMdb)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrIntegGetDone(this.handle, pguidMdb);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000376DF File Offset: 0x000358DF
		internal int HrCiUpdateRetryTable(Guid pguidMdb, Guid pguidInstance, int cDocumentIds, uint[] rgulDocumentIds, Guid[] rgMailboxGuids, int[] rgHresults, short[] initialStates)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiUpdateRetryTable(this.handle, pguidMdb, pguidInstance, cDocumentIds, rgulDocumentIds, rgMailboxGuids, rgHresults, initialStates);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000376F7 File Offset: 0x000358F7
		internal int HrCiEnumerateRetryTable(Guid pguidMdb, Guid pguidInstance, out int cDocumentIds, out SafeExMemoryHandle rgulDocumentIds, out SafeExMemoryHandle rgMailboxGuids, out SafeExMemoryHandle rgStates)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiEnumerateRetryTable(this.handle, pguidMdb, pguidInstance, out cDocumentIds, out rgulDocumentIds, out rgMailboxGuids, out rgStates);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003770D File Offset: 0x0003590D
		internal int HrCiEntryIdFromDocumentId(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, uint ulDocumentId, out int cbEntryId, out SafeExLinkedMemoryHandle pEntryId)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiEntryIdFromDocumentId(this.handle, pguidMdb, pguidInstance, pguidMailbox, ulDocumentId, out cbEntryId, out pEntryId);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00037723 File Offset: 0x00035923
		internal int HrGetPublicFolderDN(int cbEntryId, byte[] pEntryId, string folderName, out SafeExMemoryHandle lppszDN)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetPublicFolderDN(this.handle, cbEntryId, pEntryId, folderName, out lppszDN);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00037735 File Offset: 0x00035935
		internal int HrCiSeedPropertyStore(Guid pguidMdb, Guid pguidSourceInstance, Guid pguidDestinationInstance)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiSeedPropertyStore(this.handle, pguidMdb, pguidSourceInstance, pguidDestinationInstance);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00037745 File Offset: 0x00035945
		internal int HrCiGetTableSize(Guid pguidMdb, Guid pguidInstance, short tableId, ulong flags, out ulong size)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiGetTableSize(this.handle, pguidMdb, pguidInstance, tableId, flags, out size);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003775C File Offset: 0x0003595C
		internal int HrLogReplayRequest(Guid pguidMdb, uint ulgenLogReplayMax, uint ulLogReplayFlags, out uint ulgenLogReplayNext, out uint pCbOut, out SafeExMemoryHandle pDbinfo, out uint patchPageNumber, out uint cbPatchToken, out SafeExMemoryHandle ppvPatchToken, out uint cbPatchData, out SafeExMemoryHandle ppvPatchData, out uint cpgnoCorrupt, out SafeExMemoryHandle ppgnoCorrupt)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrLogReplayRequest(this.handle, pguidMdb, ulgenLogReplayMax, ulLogReplayFlags, out ulgenLogReplayNext, out pCbOut, out pDbinfo, out patchPageNumber, out cbPatchToken, out ppvPatchToken, out cbPatchData, out ppvPatchData, out cpgnoCorrupt, out ppgnoCorrupt);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003778B File Offset: 0x0003598B
		internal int HrStartBlockModeReplicationToPassive(Guid pguidMdb, string passiveName, uint ulFirstGenToSend)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrStartBlockModeReplicationToPassive(this.handle, pguidMdb, passiveName, ulFirstGenToSend);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003779B File Offset: 0x0003599B
		internal int HrStartSendingGranularLogData(Guid pguidMdb, string pipeName, uint flags, uint maxIoDepth, uint maxIoLatency)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrStartSendingGranularLogData(this.handle, pguidMdb, pipeName, flags, maxIoDepth, maxIoLatency);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000377AF File Offset: 0x000359AF
		internal int HrStopSendingGranularLogData(Guid pguidMdb, uint flags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrStopSendingGranularLogData(this.handle, pguidMdb, flags);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000377BE File Offset: 0x000359BE
		internal int HrGetRestrictionTable(Guid pguidMdb, Guid pguidMailbox, int cbEntryId, byte[] pEntryId, PropTag[] lpPropTagArray, int ulFlags, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetRestrictionTable(this.handle, pguidMdb, pguidMailbox, cbEntryId, pEntryId, lpPropTagArray, ulFlags, out lpSRowset);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000377D6 File Offset: 0x000359D6
		internal int HrGetViewsTable(Guid pguidMdb, Guid pguidMailbox, int cbEntryId, byte[] pEntryId, PropTag[] lpPropTagArray, int ulFlags, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetViewsTable(this.handle, pguidMdb, pguidMailbox, cbEntryId, pEntryId, lpPropTagArray, ulFlags, out lpSRowset);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000377EE File Offset: 0x000359EE
		internal int HrGetDatabaseSize(Guid pguidMdb, out ulong dbTotalPages, out ulong dbAvailablePages, out ulong dbPageSize)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetDatabaseSize(this.handle, pguidMdb, out dbTotalPages, out dbAvailablePages, out dbPageSize);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00037800 File Offset: 0x00035A00
		internal int HrCiUpdateFailedItem(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, uint documentId, uint errorCode, uint flags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiUpdateFailedItem(this.handle, pguidMdb, pguidInstance, pguidMailbox, documentId, errorCode, flags);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00037816 File Offset: 0x00035A16
		internal int HrCiEnumerateFailedItems(Guid pguidMdb, Guid pguidInstance, uint lastMaxDocId, out SafeExLinkedMemoryHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiEnumerateFailedItems(this.handle, pguidMdb, pguidInstance, lastMaxDocId, out lpSRowset);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00037828 File Offset: 0x00035A28
		internal int HrPrePopulateCache(Guid pguidMdb, string legacyDN, Guid pguidMailbox, byte[] partitionHint, string dcName)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrPrePopulateCache(this.handle, pguidMdb, legacyDN, pguidMailbox, (partitionHint != null) ? partitionHint.Length : 0, partitionHint, dcName);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00037847 File Offset: 0x00035A47
		internal int HrGetMailboxTableEntry(Guid pguidMdb, Guid pguidMailbox, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxTableEntry(this.handle, pguidMdb, pguidMailbox, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00037859 File Offset: 0x00035A59
		internal int HrGetMailboxTableEntryFlags(Guid pguidMdb, Guid pguidMailbox, uint flags, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxTableEntryFlags(this.handle, pguidMdb, pguidMailbox, flags, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003786D File Offset: 0x00035A6D
		internal int HrCiEnumerateFailedItemsByMailbox(Guid pguidMdb, Guid pguidInstance, Guid pguidMailbox, uint lastMaxDocId, out SafeExLinkedMemoryHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiEnumerateFailedItemsByMailbox(this.handle, pguidMdb, pguidInstance, pguidMailbox, lastMaxDocId, out lpSRowset);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00037881 File Offset: 0x00035A81
		internal int HrCiUpdateFailedItemAndRetryTableByErrorCode(Guid pguidMdb, Guid pguidInstance, uint errorCode, uint lastMaxDocId, out uint curMaxDocId, out uint itemNumber)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrCiUpdateFailedItemAndRetryTableByErrorCode(this.handle, pguidMdb, pguidInstance, errorCode, lastMaxDocId, out curMaxDocId, out itemNumber);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00037897 File Offset: 0x00035A97
		internal int HrGetResourceMonitorDigest(Guid pguidMdb, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetResourceMonitorDigest(this.handle, pguidMdb, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000378A7 File Offset: 0x00035AA7
		internal int HrGetMailboxSignatureBasicInfo(Guid pguidMdb, Guid pguidMailbox, uint ulFlags, out int cbMailboxBasicInfo, out SafeExMemoryHandle ppbMailboxBasicInfo)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMailboxSignatureBasicInfo(this.handle, pguidMdb, pguidMailbox, ulFlags, out cbMailboxBasicInfo, out ppbMailboxBasicInfo);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000378BB File Offset: 0x00035ABB
		internal int HrGetFeatureVersion(uint versionedFeature, out uint featureVersion)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetFeatureVersion(this.handle, versionedFeature, out featureVersion);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000378CA File Offset: 0x00035ACA
		internal int HrISIntegCheck(Guid pguidMdb, Guid pguidMailbox, uint ulFlags, int dbtasks, uint[] dbtaskids, out SafeExMemoryHandle taskId)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrISIntegCheck(this.handle, pguidMdb, pguidMailbox, ulFlags, dbtasks, dbtaskids, out taskId);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000378E0 File Offset: 0x00035AE0
		internal int HrForceNewLog(Guid pguidMdb)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrForceNewLog(this.handle, pguidMdb);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000378EE File Offset: 0x00035AEE
		internal int HrGetPublicFolderGlobalsTable(Guid pguidMdb, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetPublicFolderGlobalsTable(this.handle, pguidMdb, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000378FE File Offset: 0x00035AFE
		internal int HrMultiMailboxSearch(Guid pguidMdb, ulong cbRequest, byte[] pbRequest, out ulong cbResponse, out SafeExMemoryHandle pSearchResponse)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrMultiMailboxSearch(this.handle, pguidMdb, cbRequest, pbRequest, out cbResponse, out pSearchResponse);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00037912 File Offset: 0x00035B12
		internal int HrGetMultiMailboxSearchKeywordStats(Guid pguidMdb, ulong cbRequest, byte[] pbRequest, out ulong cbResponse, out SafeExMemoryHandle pSearchResponse)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetMultiMailboxSearchKeywordStats(this.handle, pguidMdb, cbRequest, pbRequest, out cbResponse, out pSearchResponse);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00037926 File Offset: 0x00035B26
		internal unsafe int HrFormatSearchRestriction(SRestriction* restriction, out ulong size, out SafeExMemoryHandle lpRestriction)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrFormatSearchRestriction(this.handle, restriction, out size, out lpRestriction);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00037936 File Offset: 0x00035B36
		internal uint GetStorePerRPCStats(out PerRpcStats pPerRpcPerformanceStatistics)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_GetStorePerRPCStats(this.handle, out pPerRpcPerformanceStatistics);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00037944 File Offset: 0x00035B44
		internal int HrGetDatabaseProcessInfo(Guid pMdbGuid, PropTag[] lpPropTags, out SafeExProwsHandle lpSRowSet)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetDatabaseProcessInfo(this.handle, pMdbGuid, lpPropTags, out lpSRowSet);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00037954 File Offset: 0x00035B54
		internal int HrProcessSnapshotOperation(Guid pGuidMdb, uint opCode, uint flags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrProcessSnapshotOperation(this.handle, pGuidMdb, opCode, flags);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00037964 File Offset: 0x00035B64
		internal int HrGetPhysicalDatabaseInformation(Guid pGuidMdb, out uint pCbOut, out SafeExMemoryHandle pDbinfo)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrGetPhysicalDatabaseInformation(this.handle, pGuidMdb, out pCbOut, out pDbinfo);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00037974 File Offset: 0x00035B74
		internal int HrStoreIntegrityCheckEx(Guid pguidMdb, Guid pguidMailbox, Guid pguidRequest, uint ulOperation, uint ulFlags, uint[] dbtaskIds, PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrStoreIntegrityCheckEx(this.handle, pguidMdb, pguidMailbox, pguidRequest, ulOperation, ulFlags, (uint)((dbtaskIds == null) ? 0 : dbtaskIds.Length), dbtaskIds, lpPropTagArray, out lpSRowset);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000379A4 File Offset: 0x00035BA4
		internal unsafe int HrCreateUserInfo(Guid pguidMdb, Guid pguidUserInfo, uint ulFlags, PropValue[] properties)
		{
			int num = 0;
			foreach (PropValue propValue in properties)
			{
				num += propValue.GetBytesToMarshal();
			}
			fixed (byte* ptr = new byte[num])
			{
				PropValue.MarshalToNative(properties, ptr);
				return SafeExRpcAdminHandle.IExRpcAdmin_HrCreateUserInfo(this.handle, pguidMdb, pguidUserInfo, ulFlags, properties.Length, (SPropValue*)ptr);
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00037A23 File Offset: 0x00035C23
		internal int HrReadUserInfo(Guid pguidMdb, Guid pguidUserInfo, uint ulFlags, PropTag[] lpPropTags, out SafeExProwsHandle lpSRowset)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrReadUserInfo(this.handle, pguidMdb, pguidUserInfo, ulFlags, lpPropTags, out lpSRowset);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00037A38 File Offset: 0x00035C38
		internal unsafe int HrUpdateUserInfo(Guid pguidMdb, Guid pguidUserInfo, uint ulFlags, PropValue[] properties, PropTag[] lpDeletePropTags)
		{
			int num = 0;
			foreach (PropValue propValue in properties)
			{
				num += propValue.GetBytesToMarshal();
			}
			fixed (byte* ptr = new byte[num])
			{
				PropValue.MarshalToNative(properties, ptr);
				return SafeExRpcAdminHandle.IExRpcAdmin_HrUpdateUserInfo(this.handle, pguidMdb, pguidUserInfo, ulFlags, properties.Length, (SPropValue*)ptr, lpDeletePropTags);
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00037AB9 File Offset: 0x00035CB9
		internal int HrDeleteUserInfo(Guid pguidMdb, Guid pguidUserInfo, uint ulFlags)
		{
			return SafeExRpcAdminHandle.IExRpcAdmin_HrDeleteUserInfo(this.handle, pguidMdb, pguidUserInfo, ulFlags);
		}

		// Token: 0x06000EEC RID: 3820
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetServerVersion(IntPtr iExRpcAdmin, out short pwMajor, out short pwMinor);

		// Token: 0x06000EED RID: 3821
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxInfoSize(IntPtr iExRpcAdmin, out int cbInfo);

		// Token: 0x06000EEE RID: 3822
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrDeletePrivateMailbox(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int ulFlags);

		// Token: 0x06000EEF RID: 3823
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxBasicInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] rgbInfo, int cbInfo);

		// Token: 0x06000EF0 RID: 3824
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrSetMailboxBasicInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] rgbInfo, int cbInfo);

		// Token: 0x06000EF1 RID: 3825
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrNotifyOnDSChange(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int ulObject);

		// Token: 0x06000EF2 RID: 3826
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrListMdbStatus(IntPtr iExRpcAdmin, int cMdb, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgguidMdb, [MarshalAs(UnmanagedType.LPArray)] [Out] uint[] rgulMdbStatus);

		// Token: 0x06000EF3 RID: 3827
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcAdmin_HrReadMapiEvents(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, long llStartEvent, int cEventWanted, int cEventsToCheck, [In] SRestriction* pFilter, int ulFlags, out int pcEventActual, out SafeExLinkedMemoryHandle pEvents, out long pllEndCounter);

		// Token: 0x06000EF4 RID: 3828
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrReadLastMapiEvent(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, out SafeExLinkedMemoryHandle pEvent);

		// Token: 0x06000EF5 RID: 3829
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrSaveWatermarks(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, int cWM, IntPtr pWMs);

		// Token: 0x06000EF6 RID: 3830
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetWatermarks(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidConsumer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, out int pcWM, out SafeExMemoryHandle pWMs);

		// Token: 0x06000EF7 RID: 3831
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetWatermarksForMailbox(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailboxDS, out int pcWMs, out SafeExMemoryHandle pWMs);

		// Token: 0x06000EF8 RID: 3832
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrDeleteWatermarksForMailbox(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailboxDS, out int pcDel);

		// Token: 0x06000EF9 RID: 3833
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrDeleteWatermarksForConsumer(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] [Out] ref Guid pguidMdbVer, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailboxDS, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidConsumer, out int pcDel);

		// Token: 0x06000EFA RID: 3834
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] uint flags, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000EFB RID: 3835
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetLogonTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000EFC RID: 3836
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxSecurityDescriptor(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, out SafeExMemoryHandle ppntsd, out int pcntsd);

		// Token: 0x06000EFD RID: 3837
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrSetMailboxSecurityDescriptor(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pntsd, int cntsd);

		// Token: 0x06000EFE RID: 3838
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrMountDatabase(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidStorageGroup, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int ulFlags);

		// Token: 0x06000EFF RID: 3839
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrUnmountDatabase(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidStorageGroup, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int grfFlags);

		// Token: 0x06000F00 RID: 3840
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrFlushCache(IntPtr iExRpcAdmin, out int pcMDBs, out SafeExMemoryHandle pCheckpointStatus);

		// Token: 0x06000F01 RID: 3841
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetLastBackupTimes(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out long ftLastCompleteBackupTime, out long ftLastIncrementalBackupTime);

		// Token: 0x06000F02 RID: 3842
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetLastBackupInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out long ftLastCompleteBackupTime, out long ftLastIncrementalBackupTime, out long ftLastDifferentialBackup, out long ftLastCopyBackup, out int SnapFull, out int SnapIncremental, out int SnapDifferential, out int SnapCopy);

		// Token: 0x06000F03 RID: 3843
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrPurgeCachedMailboxObject(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000F04 RID: 3844
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrPurgeCachedMdbObject(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000F05 RID: 3845
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrClearAbsentInDsFlagOnMailbox(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000F06 RID: 3846
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrSyncMailboxesWithDS(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000F07 RID: 3847
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrHasLocalReplicas(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.Bool)] out bool fHasReplicas);

		// Token: 0x06000F08 RID: 3848
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrListAllMdbStatus(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.Bool)] bool fBasicInformation, out int pcMdbs, out SafeExMemoryHandle pMdbStatus);

		// Token: 0x06000F09 RID: 3849
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetReplicaInformationTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F0A RID: 3850
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrSyncMailboxWithDS(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000F0B RID: 3851
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiCreatePropertyStore(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance);

		// Token: 0x06000F0C RID: 3852
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiDeletePropertyStore(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance);

		// Token: 0x06000F0D RID: 3853
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiDeleteMailboxMapping(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox);

		// Token: 0x06000F0E RID: 3854
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiMoveDocument(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryIdSource, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryIdSource, int cbEntryIdTarget, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryIdTarget);

		// Token: 0x06000F0F RID: 3855
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiGetWaterMark(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.Bool)] bool fIsHighWatermark, out ulong ullWatermark, out System.Runtime.InteropServices.ComTypes.FILETIME lastAccessTime);

		// Token: 0x06000F10 RID: 3856
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetWaterMark(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.Bool)] bool fIsHighWatermark, ulong ullWatermark);

		// Token: 0x06000F11 RID: 3857
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiGetMailboxState(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, out System.Runtime.InteropServices.ComTypes.FILETIME ftStart, out uint ulState, out ulong ullEventCounter);

		// Token: 0x06000F12 RID: 3858
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetMailboxStateAndStartDate(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME pftStart, uint ulState);

		// Token: 0x06000F13 RID: 3859
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetMailboxState(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, uint ulState);

		// Token: 0x06000F14 RID: 3860
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetMailboxEventCounter(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, ulong ullEventCounter);

		// Token: 0x06000F15 RID: 3861
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiEnumerateMailboxesByState(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, uint ulState, out int cMailboxes, out SafeExMemoryHandle rgGuidMailboxes);

		// Token: 0x06000F16 RID: 3862
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiPurgeMailboxes(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, uint ulState);

		// Token: 0x06000F17 RID: 3863
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetCiEnabled(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.Bool)] bool fIsEnabled);

		// Token: 0x06000F18 RID: 3864
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetIndexedPtags(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int cptags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] rgptags);

		// Token: 0x06000F19 RID: 3865
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiGetDocumentIdFromEntryId(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryID, out uint ulDocumentId);

		// Token: 0x06000F1A RID: 3866
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrDoMaintenanceTask(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, int ulTask);

		// Token: 0x06000F1B RID: 3867
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrExecuteTask(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidTaskClass, int ulTaskId);

		// Token: 0x06000F1C RID: 3868
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcAdmin_EcReadMdbEvents(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdbVer, [In] int cbRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbRequest, out int cbResponse, _SBinaryArray* pbResponse);

		// Token: 0x06000F1D RID: 3869
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcAdmin_EcWriteMdbEvents(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdbVer, [In] int cbRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbRequest, out int cbResponse, _SBinaryArray* pbResponse);

		// Token: 0x06000F1E RID: 3870
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrTriggerPFSync(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] int cbEntryId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] pEntryId, int ulFlags);

		// Token: 0x06000F1F RID: 3871
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrSetPFReplicas(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] rgszDN, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] rgulAgeLimit, int ulSize);

		// Token: 0x06000F20 RID: 3872
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiGetCatalogState(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, out short catalogState, out ulong propertyBlobSize, out SafeExMemoryHandle propertyBlob);

		// Token: 0x06000F21 RID: 3873
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSetCatalogState(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, short catalogState, [In] uint cbPropertyBlob, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] propertyBlob);

		// Token: 0x06000F22 RID: 3874
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrIntegCheck(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] ref IntegrityTestResult pTestParam);

		// Token: 0x06000F23 RID: 3875
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrIntegGetProgress(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, long iTest, out long pcTotalTest, out long piCurrentTest, out long pcCurrentPercent, out IntegrityTestResult pTestResult);

		// Token: 0x06000F24 RID: 3876
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrIntegGetCancel(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out long pcTotalTest, out long piCurrentTest, out long pcCurrentPercent, out IntegrityTestResult pTestResult);

		// Token: 0x06000F25 RID: 3877
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrIntegGetDone(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000F26 RID: 3878
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiUpdateRetryTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, int cDocumentIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulDocumentIds, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgMailboxGuids, [MarshalAs(UnmanagedType.LPArray)] int[] rgHresults, [MarshalAs(UnmanagedType.LPArray)] short[] initialStates);

		// Token: 0x06000F27 RID: 3879
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiEnumerateRetryTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, out int cDocumentIds, out SafeExMemoryHandle rgulDocumentIds, out SafeExMemoryHandle rgMailboxGuids, out SafeExMemoryHandle rgStates);

		// Token: 0x06000F28 RID: 3880
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiEntryIdFromDocumentId(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, uint ulDocumentId, out int cbEntryId, out SafeExLinkedMemoryHandle pEntryId);

		// Token: 0x06000F29 RID: 3881
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetPublicFolderDN(IntPtr iExRpcAdmin, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId, [MarshalAs(UnmanagedType.LPWStr)] string folderName, out SafeExMemoryHandle lppszDN);

		// Token: 0x06000F2A RID: 3882
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiSeedPropertyStore(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidSourceInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidDestinationInstance);

		// Token: 0x06000F2B RID: 3883
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrLogReplayRequest(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] uint ulgenLogReplayMax, [In] uint ulLogReplayFlags, out uint ulgenLogReplayNext, out uint pCbOut, out SafeExMemoryHandle pDbinfo, out uint ppatchPageNumber, out uint pcbPatchToken, out SafeExMemoryHandle ppvPatchToken, out uint pcbPatchData, out SafeExMemoryHandle ppvPatchData, out uint pcpgnoCorrupt, out SafeExMemoryHandle ppgnoCorrupt);

		// Token: 0x06000F2C RID: 3884
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrStartBlockModeReplicationToPassive(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPWStr)] [In] string passiveName, [In] uint ulFirstGenToSend);

		// Token: 0x06000F2D RID: 3885
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrStartSendingGranularLogData(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPWStr)] [In] string pipeName, [In] uint flags, [In] uint maxIoDepth, [In] uint maxIoLatency);

		// Token: 0x06000F2E RID: 3886
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrStopSendingGranularLogData(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] uint flags);

		// Token: 0x06000F2F RID: 3887
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetRestrictionTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F30 RID: 3888
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetViewsTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] byte[] pEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F31 RID: 3889
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetDatabaseSize(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, out ulong dbTotalPages, out ulong dbAvailablePages, out ulong dbPageSize);

		// Token: 0x06000F32 RID: 3890
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiUpdateFailedItem(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint documentId, [In] uint errorCode, [In] uint flags);

		// Token: 0x06000F33 RID: 3891
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiEnumerateFailedItems(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] uint lastMaxDocId, out SafeExLinkedMemoryHandle lpSRowset);

		// Token: 0x06000F34 RID: 3892
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrPrePopulateCache(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStr)] [In] string legacyDN, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] int cbPartitionHint, [MarshalAs(UnmanagedType.LPArray)] byte[] pbPartitionHint, [MarshalAs(UnmanagedType.LPStr)] [In] string dcName);

		// Token: 0x06000F35 RID: 3893
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxTableEntry(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F36 RID: 3894
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxTableEntryFlags(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint flags, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F37 RID: 3895
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiEnumerateFailedItemsByMailbox(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint lastMaxDocId, out SafeExLinkedMemoryHandle lpSRowset);

		// Token: 0x06000F38 RID: 3896
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiUpdateFailedItemAndRetryTableByErrorCode(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] uint errorCode, [In] uint lastMaxDocId, out uint curMaxDocId, out uint itemNumber);

		// Token: 0x06000F39 RID: 3897
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetResourceMonitorDigest(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F3A RID: 3898
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMailboxSignatureBasicInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint ulFlags, out int cbMailboxBasicInfo, out SafeExMemoryHandle ppbMailboxBasicInfo);

		// Token: 0x06000F3B RID: 3899
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetFeatureVersion(IntPtr iExRpcAdmin, [In] uint versionedFeature, out uint featureVersion);

		// Token: 0x06000F3C RID: 3900
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrISIntegCheck(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMailbox, [In] uint Flags, [In] int cTaskIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulTaskIds, out SafeExMemoryHandle szTaskId);

		// Token: 0x06000F3D RID: 3901
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrForceNewLog(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb);

		// Token: 0x06000F3E RID: 3902
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetPublicFolderGlobalsTable(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F3F RID: 3903
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrCiGetTableSize(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidInstance, [In] short tableId, [In] ulong ulFlags, out ulong ulSize);

		// Token: 0x06000F40 RID: 3904
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrMultiMailboxSearch(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] ulong cbRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbRequest, out ulong cbResponse, out SafeExMemoryHandle ppvResponse);

		// Token: 0x06000F41 RID: 3905
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetMultiMailboxSearchKeywordStats(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pguidMdb, [In] ulong cbRequest, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbRequest, out ulong cbResponse, out SafeExMemoryHandle ppvResponse);

		// Token: 0x06000F42 RID: 3906
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcAdmin_HrFormatSearchRestriction(IntPtr iExRpcAdmin, [In] SRestriction* restricition, out ulong size, out SafeExMemoryHandle lpFormattedRestriction);

		// Token: 0x06000F43 RID: 3907
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern uint IExRpcAdmin_GetStorePerRPCStats(IntPtr iExRpcAdmin, out PerRpcStats pPerRpcPerformanceStatistics);

		// Token: 0x06000F44 RID: 3908
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetDatabaseProcessInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTags, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F45 RID: 3909
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrProcessSnapshotOperation(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, uint opCode, uint flags);

		// Token: 0x06000F46 RID: 3910
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrGetPhysicalDatabaseInformation(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, out uint pCbOut, out SafeExMemoryHandle pDbinfo);

		// Token: 0x06000F47 RID: 3911
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrStoreIntegrityCheckEx(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMailboxGuid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pRequestGuid, [In] uint ulOperation, [In] uint ulFlags, [In] uint cTaskIds, [MarshalAs(UnmanagedType.LPArray)] uint[] rgulTaskIds, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F48 RID: 3912
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcAdmin_HrCreateUserInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pUserInfoGuid, [In] uint ulFlags, int cValues, SPropValue* lpPropArray);

		// Token: 0x06000F49 RID: 3913
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrReadUserInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pUserInfoGuid, [In] uint ulFlags, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTags, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000F4A RID: 3914
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcAdmin_HrUpdateUserInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pUserInfoGuid, [In] uint ulFlags, int cValues, SPropValue* lpPropArray, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpDeletePropTags);

		// Token: 0x06000F4B RID: 3915
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcAdmin_HrDeleteUserInfo(IntPtr iExRpcAdmin, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pMdbGuid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid pUserInfoGuid, [In] uint ulFlags);
	}
}

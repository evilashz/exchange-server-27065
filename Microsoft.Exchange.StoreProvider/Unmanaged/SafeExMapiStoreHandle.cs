using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C6 RID: 710
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExMapiStoreHandle : SafeExMapiPropHandle, IExMapiStore, IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x0003632A File Offset: 0x0003452A
		protected SafeExMapiStoreHandle()
		{
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00036332 File Offset: 0x00034532
		internal SafeExMapiStoreHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003633B File Offset: 0x0003453B
		internal SafeExMapiStoreHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00036344 File Offset: 0x00034544
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiStoreHandle>(this);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003634C File Offset: 0x0003454C
		public int AdviseEx(byte[] lpEntryId, AdviseFlags ulEventMask, IntPtr iOnNotifyDelegate, ulong callbackId, out IntPtr piConnection)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_AdviseEx(this.handle, (lpEntryId != null) ? lpEntryId.Length : 0, lpEntryId, ulEventMask, iOnNotifyDelegate, callbackId, out piConnection);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00036369 File Offset: 0x00034569
		public int Unadvise(IntPtr iConnection)
		{
			return SafeExMapiStoreHandle.IMsgStore_Unadvise(this.handle, iConnection);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00036378 File Offset: 0x00034578
		public int OpenEntry(byte[] lpEntryID, Guid lpInterface, int ulFlags, out int lpulObjType, out IExInterface iObj)
		{
			SafeExInterfaceHandle safeExInterfaceHandle;
			int result = SafeExMapiStoreHandle.IMsgStore_OpenEntry(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, lpInterface, ulFlags, out lpulObjType, out safeExInterfaceHandle);
			iObj = safeExInterfaceHandle;
			return result;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000363A8 File Offset: 0x000345A8
		public int GetPerUser(byte[] ltid, bool fSendOnlyIfChanged, int ibStream, int cbDataLimit, out byte[] data, out bool fLast)
		{
			data = null;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int num = 0;
			int result;
			try
			{
				int num2 = SafeExMapiStoreHandle.IExRpcMsgStore_GetPerUser(this.handle, ltid, fSendOnlyIfChanged, ibStream, cbDataLimit, out safeExMemoryHandle, out num, out fLast);
				if (num2 == 0)
				{
					if (num > 0 && safeExMemoryHandle != null)
					{
						data = new byte[num];
						Marshal.Copy(safeExMemoryHandle.DangerousGetHandle(), data, 0, num);
					}
					else
					{
						data = Array<byte>.Empty;
					}
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00036420 File Offset: 0x00034620
		public int SetPerUser(byte[] ltid, Guid? guidReplica, int lib, byte[] pb, int cb, bool fLast)
		{
			return this.InternalSetPerUser(ltid, guidReplica, lib, pb, cb, fLast);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00036434 File Offset: 0x00034634
		private unsafe int InternalSetPerUser(byte[] ltid, Guid? guidReplica, int lib, byte[] pb, int cb, bool fLast)
		{
			Guid valueOrDefault = guidReplica.GetValueOrDefault();
			Guid* guidReplica2 = (guidReplica != null) ? (&valueOrDefault) : null;
			return SafeExMapiStoreHandle.IExRpcMsgStore_SetPerUser(this.handle, ltid, guidReplica2, lib, pb, cb, fLast);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003646E File Offset: 0x0003466E
		public int SetReceiveFolder(string lpwszMessageClass, int ulFlags, byte[] lpEntryID)
		{
			return SafeExMapiStoreHandle.IMsgStore_SetReceiveFolder(this.handle, lpwszMessageClass, ulFlags, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00036488 File Offset: 0x00034688
		public int GetReceiveFolder(string lpwszMessageClass, int ulFlags, out byte[] lppEntryId, out string lppszExplicitClass)
		{
			lppEntryId = null;
			lppszExplicitClass = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int num = 0;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle2 = null;
			int result;
			try
			{
				int num2 = SafeExMapiStoreHandle.IMsgStore_GetReceiveFolder(this.handle, lpwszMessageClass, ulFlags, out num, out safeExLinkedMemoryHandle, out safeExLinkedMemoryHandle2);
				if (num2 == 0)
				{
					byte[] array = new byte[num];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num);
					lppEntryId = array;
					if (!safeExLinkedMemoryHandle2.IsInvalid)
					{
						lppszExplicitClass = Marshal.PtrToStringUni(safeExLinkedMemoryHandle2.DangerousGetHandle());
					}
				}
				result = num2;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
				if (safeExLinkedMemoryHandle2 != null)
				{
					safeExLinkedMemoryHandle2.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00036510 File Offset: 0x00034710
		public int GetReceiveFolderInfo(out PropValue[][] lpSRowSet)
		{
			lpSRowSet = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_GetReceiveFolderInfo(this.handle, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					lpSRowSet = SRowSet.Unmarshal(safeExLinkedMemoryHandle);
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003655C File Offset: 0x0003475C
		public int StoreLogoff(ref int ulFlags)
		{
			return SafeExMapiStoreHandle.IMsgStore_StoreLogoff(this.handle, ref ulFlags);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003656A File Offset: 0x0003476A
		public int AbortSubmit(byte[] lpEntryID, int ulFlags)
		{
			return SafeExMapiStoreHandle.IMsgStore_AbortSubmit(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, ulFlags);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00036584 File Offset: 0x00034784
		public int CreateEntryId(long fid, long mid, bool fMessage, bool fLongTerm, out byte[] lppEntryId)
		{
			lppEntryId = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_CreateEntryId(this.handle, fid, mid, fMessage, fLongTerm, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[num2];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num2);
					lppEntryId = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000365E4 File Offset: 0x000347E4
		public int GetShortTermIdsFromLongTermEntryId(byte[] lpEntryID, out bool pfMessage, out long pFid, out long pMid)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetShortTermIdsFromLongTermEntryId(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, out pfMessage, out pFid, out pMid);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00036600 File Offset: 0x00034800
		public int CreateEntryIdFromLegacyDN(string lpszLegacyDN, out byte[] lppEntryId)
		{
			lppEntryId = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_CreateEntryIdFromLegacyDN(this.handle, lpszLegacyDN, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[num2];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num2);
					lppEntryId = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003665C File Offset: 0x0003485C
		public int GetParentEntryId(byte[] lpEntryID, out byte[] lppParentEntryId)
		{
			lppParentEntryId = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_GetParentEntryId(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[num2];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num2);
					lppParentEntryId = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000366C0 File Offset: 0x000348C0
		public int GetAddressTypes(out string[] lppszAddressTypes)
		{
			lppszAddressTypes = null;
			IntPtr zero = IntPtr.Zero;
			int result;
			try
			{
				int num = 0;
				int num2 = SafeExMapiStoreHandle.IExRpcMsgStore_GetAddressTypes(this.handle, out num, out zero);
				if (num2 == 0)
				{
					string[] array = new string[num];
					for (int i = 0; i < num; i++)
					{
						IntPtr ptr = Marshal.ReadIntPtr(zero, i * IntPtr.Size);
						array[i] = Marshal.PtrToStringAnsi(ptr);
					}
					lppszAddressTypes = array;
				}
				result = num2;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SafeExMemoryHandle.FreePvFnEx(zero);
				}
			}
			return result;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003674C File Offset: 0x0003494C
		public int BackoffNow(out int iNow)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_BackoffNow(this.handle, out iNow);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003675C File Offset: 0x0003495C
		public int CompressEntryId(byte[] lpEntryID, out byte[] lppCompressedEntryId)
		{
			lppCompressedEntryId = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_CompressEntryId(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[num2];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num2);
					lppCompressedEntryId = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000367C0 File Offset: 0x000349C0
		public int ExpandEntryId(byte[] lpCompressedEntryID, out byte[] lppEntryId)
		{
			lppEntryId = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_ExpandEntryId(this.handle, (lpCompressedEntryID != null) ? lpCompressedEntryID.Length : 0, lpCompressedEntryID, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[num2];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num2);
					lppEntryId = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00036824 File Offset: 0x00034A24
		public int CreateGlobalIdFromId(long id, out byte[] lppGid)
		{
			lppGid = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_CreateGlobalIdFromId(this.handle, id, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[22];
					safeExLinkedMemoryHandle.CopyTo(array, 0, array.Length);
					lppGid = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00036880 File Offset: 0x00034A80
		public int CreateIdFromGlobalId(byte[] gid, out long id)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_CreateIdFromGlobalId(this.handle, gid, out id);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003688F File Offset: 0x00034A8F
		public int MapActionsToMDBActions(RuleAction[] actions, out byte[] mdbActions)
		{
			return this.InternalMapActionsToMDBActions(actions, out mdbActions);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003689C File Offset: 0x00034A9C
		private unsafe int InternalMapActionsToMDBActions(RuleAction[] actions, out byte[] mdbActions)
		{
			mdbActions = null;
			uint bytesToMarshal = (uint)RuleActions.GetBytesToMarshal(actions);
			byte[] array = new byte[bytesToMarshal];
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int num;
				try
				{
					fixed (byte* ptr = &array[0])
					{
						byte* ptr2 = ptr;
						RuleActions.MarshalToNative(ref ptr2, actions);
						num = SafeExMapiStoreHandle.IExRpcMsgStore_MapActionsToMDBActions(this.handle, (_Actions*)ptr, out bytesToMarshal, out safeExMemoryHandle);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (num == 0)
				{
					byte[] array2 = new byte[bytesToMarshal];
					safeExMemoryHandle.CopyTo(array2, 0, (int)bytesToMarshal);
					mdbActions = array2;
				}
				result = num;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00036934 File Offset: 0x00034B34
		public int GetMailboxInstanceGuid(out Guid guidMailboxInstanceGuid)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetMailboxInstanceGuid(this.handle, out guidMailboxInstanceGuid);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00036942 File Offset: 0x00034B42
		public int GetMdbIdMapping(out ushort replidServer, out Guid guidServer)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetMdbIdMapping(this.handle, out replidServer, out guidServer);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00036951 File Offset: 0x00034B51
		public int GetSpoolerQueueFid(out long fidSpoolerQ)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetSpoolerQueueFid(this.handle, out fidSpoolerQ);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003695F File Offset: 0x00034B5F
		public int GetLocalRepIds(uint cid, out MapiLtidNative ltid)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetLocalRepIds(this.handle, cid, out ltid);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00036970 File Offset: 0x00034B70
		public int CreatePublicEntryId(long fid, long mid, bool fMessage, out byte[] lppEntryId)
		{
			lppEntryId = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiStoreHandle.IExRpcMsgStore_CreatePublicEntryId(this.handle, fid, mid, fMessage, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = new byte[num2];
					safeExLinkedMemoryHandle.CopyTo(array, 0, num2);
					lppEntryId = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000369D0 File Offset: 0x00034BD0
		public int GetTransportQueueFolderId(out long fidTransportQueue)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetTransportQueueFolderId(this.handle, out fidTransportQueue);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000369DE File Offset: 0x00034BDE
		public int GetIsRulesInterfaceAvailable(out bool rulesInterfaceAvailable)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetIsRulesInterfaceAvailable(this.handle, out rulesInterfaceAvailable);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000369EC File Offset: 0x00034BEC
		public int SetSpooler()
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_SetSpooler(this.handle);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000369F9 File Offset: 0x00034BF9
		public int SpoolerSetMessageLockState(byte[] lpEntryID, int ulLockState)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_SpoolerSetMessageLockState(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, ulLockState);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00036A11 File Offset: 0x00034C11
		public int SpoolerNotifyMessageNewMail(byte[] lpEntryID, string lpszMsgClass, int ulMessageFlags)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_SpoolerNotifyMessageNewMail(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, lpszMsgClass, ulMessageFlags);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00036A2A File Offset: 0x00034C2A
		public int GetPerUserGuid(MapiLtidNative ltid, out Guid guid)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetPerUserGuid(this.handle, ltid, out guid);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00036A3C File Offset: 0x00034C3C
		public int GetPerUserLtids(Guid guid, out MapiLtidNative[] ltids)
		{
			SafeExMemoryHandle safeExMemoryHandle = null;
			int num = 0;
			ltids = Array<MapiLtidNative>.Empty;
			int result;
			try
			{
				int num2 = SafeExMapiStoreHandle.IExRpcMsgStore_GetPerUserLtids(this.handle, guid, out num, out safeExMemoryHandle);
				if (num != 0)
				{
					ltids = safeExMemoryHandle.ReadMapiLtidNativeArray(num);
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00036A90 File Offset: 0x00034C90
		internal int GetAllPerUserLtids(byte[] ltidStart, out MapiPUDNative[] perUserData, out bool isFinished)
		{
			SafeExMemoryHandle safeExMemoryHandle = null;
			int num = 0;
			perUserData = Array<MapiPUDNative>.Empty;
			int result;
			try
			{
				int num2 = SafeExMapiStoreHandle.IExRpcMsgStore_GetAllPerUserLtids(this.handle, ltidStart, out num, out safeExMemoryHandle, out isFinished);
				if (num != 0)
				{
					perUserData = safeExMemoryHandle.ReadMapiPudNativeArray(num);
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00036AE8 File Offset: 0x00034CE8
		public int GetEffectiveRights(byte[] lpAddressBookEntryId, byte[] lpEntryId, out uint rights)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetEffectiveRights(this.handle, (lpAddressBookEntryId != null) ? lpAddressBookEntryId.Length : 0, lpAddressBookEntryId, (lpEntryId != null) ? lpEntryId.Length : 0, lpEntryId, out rights);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00036B0A File Offset: 0x00034D0A
		public int PrereadMessages(byte[][] entryIds)
		{
			return this.InternalPrereadMessages(entryIds);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00036B14 File Offset: 0x00034D14
		private unsafe int InternalPrereadMessages(byte[][] entryIds)
		{
			SBinary[] array = new SBinary[entryIds.GetLength(0)];
			for (int i = 0; i < entryIds.GetLength(0); i++)
			{
				array[i] = new SBinary(entryIds[i]);
			}
			int bytesToMarshal = SBinaryArray.GetBytesToMarshal(array);
			fixed (byte* ptr = new byte[bytesToMarshal])
			{
				SBinaryArray.MarshalToNative(ptr, array);
				return SafeExMapiStoreHandle.IExRpcMsgStore_PrereadMessages(this.handle, (_SBinaryArray*)ptr);
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00036B8C File Offset: 0x00034D8C
		internal int SetCurrentActivityInfo(Guid activityId, string component, string protocol, string action)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_SetCurrentActivityInfo(this.handle, activityId, component, protocol, action);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00036B9E File Offset: 0x00034D9E
		public int GetInTransitStatus(out uint inTransitStatus)
		{
			return SafeExMapiStoreHandle.IExRpcMsgStore_GetInTransitStatus(this.handle, out inTransitStatus);
		}

		// Token: 0x06000E0A RID: 3594
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_Advise(IntPtr iMsgStore, int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection);

		// Token: 0x06000E0B RID: 3595
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_Unadvise(IntPtr iMsgStore, IntPtr iConnection);

		// Token: 0x06000E0C RID: 3596
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_OpenEntry(IntPtr iMsgStore, int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int lpulObjType, out SafeExInterfaceHandle iObj);

		// Token: 0x06000E0D RID: 3597
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetPerUser(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpLtid, bool fSendOnlyIfChanged, int lib, int cbDataLimit, out SafeExMemoryHandle pb, out int cb, out bool fLast);

		// Token: 0x06000E0E RID: 3598
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcMsgStore_SetPerUser(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpLtid, Guid* guidReplica, int lib, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pb, int cb, bool fLast);

		// Token: 0x06000E0F RID: 3599
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_SetReceiveFolder(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszMessageClass, int ulFlags, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID);

		// Token: 0x06000E10 RID: 3600
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_GetReceiveFolder(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszMessageClass, int ulFlags, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId, out SafeExLinkedMemoryHandle lppszExplicitClass);

		// Token: 0x06000E11 RID: 3601
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_StoreLogoff(IntPtr iMsgStore, ref int ulFlags);

		// Token: 0x06000E12 RID: 3602
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMsgStore_AbortSubmit(IntPtr iMsgStore, int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, int ulFlags);

		// Token: 0x06000E13 RID: 3603
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_CreateEntryId(IntPtr iMsgStore, long fid, long mid, [MarshalAs(UnmanagedType.Bool)] bool fMessage, [MarshalAs(UnmanagedType.Bool)] bool fLongTerm, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000E14 RID: 3604
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetReceiveFolderInfo(IntPtr iMsgStore, out SafeExLinkedMemoryHandle lpSRowSet);

		// Token: 0x06000E15 RID: 3605
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetShortTermIdsFromLongTermEntryId(IntPtr iMsgStore, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.Bool)] out bool pfMessage, out long pFid, out long pMid);

		// Token: 0x06000E16 RID: 3606
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_CreateEntryIdFromLegacyDN(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPStr)] string lpszLegacyDN, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000E17 RID: 3607
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetParentEntryId(IntPtr iMsgStore, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int lpcbParentEntryId, out SafeExLinkedMemoryHandle lppParentEntryId);

		// Token: 0x06000E18 RID: 3608
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetAddressTypes(IntPtr iMsgStore, out int cAddressTypes, out IntPtr lppszAddressTypes);

		// Token: 0x06000E19 RID: 3609
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_BackoffNow(IntPtr iMsgStore, out int iNow);

		// Token: 0x06000E1A RID: 3610
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_CompressEntryId(IntPtr iMsgStore, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int lpcbCompressedEntryId, out SafeExLinkedMemoryHandle lppCompressedEntryId);

		// Token: 0x06000E1B RID: 3611
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_ExpandEntryId(IntPtr iMsgStore, int cbCompressedEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpCompressedEntryID, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000E1C RID: 3612
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_CreateGlobalIdFromId(IntPtr iMsgStore, long id, out SafeExLinkedMemoryHandle lppGid);

		// Token: 0x06000E1D RID: 3613
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_CreateIdFromGlobalId(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] gid, out long id);

		// Token: 0x06000E1E RID: 3614
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcMsgStore_MapActionsToMDBActions(IntPtr iMsgStore, _Actions* pactions, out uint cbBuf, out SafeExMemoryHandle pbBuf);

		// Token: 0x06000E1F RID: 3615
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetMailboxInstanceGuid(IntPtr iMsgStore, out Guid guidMailboxInstanceGuid);

		// Token: 0x06000E20 RID: 3616
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetMdbIdMapping(IntPtr iMsgStore, out ushort replidServer, out Guid guidServer);

		// Token: 0x06000E21 RID: 3617
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetSpoolerQueueFid(IntPtr iMsgStore, out long fidSpoolerQ);

		// Token: 0x06000E22 RID: 3618
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetLocalRepIds(IntPtr iMsgStore, uint cid, out MapiLtidNative ltid);

		// Token: 0x06000E23 RID: 3619
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_CreatePublicEntryId(IntPtr iMsgStore, long fid, long mid, [MarshalAs(UnmanagedType.Bool)] bool fMessage, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000E24 RID: 3620
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetTransportQueueFolderId(IntPtr iMsgStore, out long fidTransportQueue);

		// Token: 0x06000E25 RID: 3621
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetIsRulesInterfaceAvailable(IntPtr iMsgStore, [MarshalAs(UnmanagedType.Bool)] out bool rulesInterfaceAvailable);

		// Token: 0x06000E26 RID: 3622
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_SetSpooler(IntPtr iMsgStore);

		// Token: 0x06000E27 RID: 3623
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_SpoolerSetMessageLockState(IntPtr iMsgStore, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, int ulLockState);

		// Token: 0x06000E28 RID: 3624
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_SpoolerNotifyMessageNewMail(IntPtr iMsgStore, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStr)] string lpszMsgClass, int ulMessageFlags);

		// Token: 0x06000E29 RID: 3625
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetPerUserGuid(IntPtr iMsgStore, MapiLtidNative ltid, out Guid guid);

		// Token: 0x06000E2A RID: 3626
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetPerUserLtids(IntPtr iMsgStore, [In] Guid guid, out int lpcLtids, out SafeExMemoryHandle lppLtids);

		// Token: 0x06000E2B RID: 3627
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetAllPerUserLtids(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] startLtid, out int lpcpud, out SafeExMemoryHandle lppPud, out bool isFinished);

		// Token: 0x06000E2C RID: 3628
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_UpdateDeferredActionMessages(IntPtr iMsgStore, int cbServerEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpServerEntryId, int cbClientEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpClientEntryId);

		// Token: 0x06000E2D RID: 3629
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetEffectiveRights(IntPtr iMsgStore, int cbAddressBookEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpAddressBookEntryId, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, out uint rights);

		// Token: 0x06000E2E RID: 3630
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcMsgStore_PrereadMessages(IntPtr iMsgStore, _SBinaryArray* sbaEntryIDs);

		// Token: 0x06000E2F RID: 3631
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_AdviseEx(IntPtr iMsgStore, int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, AdviseFlags ulEventMask, IntPtr iOnNotifyDelegate, ulong callbackId, out IntPtr piConnection);

		// Token: 0x06000E30 RID: 3632
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_SetCurrentActivityInfo(IntPtr iMsgStore, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpGuidActivityId, [MarshalAs(UnmanagedType.LPStr)] [In] string lpszComponent, [MarshalAs(UnmanagedType.LPStr)] [In] string lpszProtocol, [MarshalAs(UnmanagedType.LPStr)] [In] string lpszAction);

		// Token: 0x06000E31 RID: 3633
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcMsgStore_GetInTransitStatus(IntPtr iMsgStore, out uint ulInTransitStatus);
	}
}

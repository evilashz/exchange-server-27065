using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C4 RID: 708
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExMapiFolderHandle : SafeExMapiContainerHandle, IExMapiFolder, IExMapiContainer, IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x000354B4 File Offset: 0x000336B4
		protected SafeExMapiFolderHandle()
		{
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000354BC File Offset: 0x000336BC
		internal SafeExMapiFolderHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000354C5 File Offset: 0x000336C5
		internal SafeExMapiFolderHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000354CE File Offset: 0x000336CE
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiFolderHandle>(this);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000354D8 File Offset: 0x000336D8
		public int CreateMessage(int ulFlags, out IExMapiMessage iMessage)
		{
			SafeExMapiMessageHandle safeExMapiMessageHandle = null;
			int result = SafeExMapiFolderHandle.IMAPIFolder_CreateMessage(this.handle, IntPtr.Zero, ulFlags, out safeExMapiMessageHandle);
			iMessage = safeExMapiMessageHandle;
			return result;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000354FF File Offset: 0x000336FF
		public int CopyMessages(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags)
		{
			return this.InternalCopyMessages(sbinArray, IntPtr.Zero, ((SafeExMapiFolderHandle)iMAPIFolderDest).DangerousGetHandle(), IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00035524 File Offset: 0x00033724
		private unsafe int InternalCopyMessages(SBinary[] sbinArray, IntPtr lpInterface, IntPtr iMAPIFolderDest, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			int bytesToMarshal = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[bytesToMarshal])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				return SafeExMapiFolderHandle.IMAPIFolder_CopyMessages(this.handle, (_SBinaryArray*)ptr, lpInterface, iMAPIFolderDest, ulUIParam, lpProgress, ulFlags);
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00035575 File Offset: 0x00033775
		public int CopyMessages_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags)
		{
			return this.InternalCopyMessages_External(sbinArray, IntPtr.Zero, iMAPIFolderDest, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00035590 File Offset: 0x00033790
		private unsafe int InternalCopyMessages_External(SBinary[] sbinArray, IntPtr lpInterface, IMAPIFolder iMAPIFolderDest, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			int bytesToMarshal = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[bytesToMarshal])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				return SafeExMapiFolderHandle.IMAPIFolder_CopyMessages_External(this.handle, (_SBinaryArray*)ptr, lpInterface, iMAPIFolderDest, ulUIParam, lpProgress, ulFlags);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000355E1 File Offset: 0x000337E1
		public int DeleteMessages(SBinary[] sbinArray, int ulFlags)
		{
			return this.InternalDeleteMessages(sbinArray, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000355F8 File Offset: 0x000337F8
		private unsafe int InternalDeleteMessages(SBinary[] sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			int bytesToMarshal = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[bytesToMarshal])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				return SafeExMapiFolderHandle.IMAPIFolder_DeleteMessages(this.handle, (_SBinaryArray*)ptr, ulUIParam, lpProgress, ulFlags);
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00035648 File Offset: 0x00033848
		public int CreateFolder(int ulFolderType, string lpwszFolderName, string lpwszFolderComment, int ulFlags, out IExMapiFolder iMAPIFolderNew)
		{
			SafeExMapiFolderHandle safeExMapiFolderHandle = null;
			int result = SafeExMapiFolderHandle.IMAPIFolder_CreateFolder(this.handle, ulFolderType, lpwszFolderName, lpwszFolderComment, IntPtr.Zero, ulFlags, out safeExMapiFolderHandle);
			iMAPIFolderNew = safeExMapiFolderHandle;
			return result;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00035674 File Offset: 0x00033874
		public int CopyFolder(int cbEntryId, byte[] lpEntryId, IExMapiFolder iMAPIFolderDest, string lpwszNewFolderName, int ulFlags)
		{
			return SafeExMapiFolderHandle.IMAPIFolder_CopyFolder(this.handle, cbEntryId, lpEntryId, IntPtr.Zero, ((SafeExMapiFolderHandle)iMAPIFolderDest).DangerousGetHandle(), lpwszNewFolderName, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000356AC File Offset: 0x000338AC
		public int CopyFolder_External(int cbEntryId, byte[] lpEntryId, IMAPIFolder iMAPIFolderDest, string lpwszNewFolderName, int ulFlags)
		{
			return SafeExMapiFolderHandle.IMAPIFolder_CopyFolder_External(this.handle, cbEntryId, lpEntryId, IntPtr.Zero, iMAPIFolderDest, lpwszNewFolderName, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000356DA File Offset: 0x000338DA
		public int DeleteFolder(byte[] lpEntryId, int ulFlags)
		{
			return SafeExMapiFolderHandle.IMAPIFolder_DeleteFolder(this.handle, (lpEntryId != null) ? lpEntryId.Length : 0, lpEntryId, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000356FC File Offset: 0x000338FC
		public int SetReadFlags(SBinary[] sbinArray, int ulFlags)
		{
			return this.InternalSetReadFlags(sbinArray, ulFlags);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00035708 File Offset: 0x00033908
		public unsafe int InternalSetReadFlags(SBinary[] sbinArray, int ulFlags)
		{
			if (sbinArray != null && sbinArray.Length > 0)
			{
				int bytesToMarshal = SBinaryArray.GetBytesToMarshal(sbinArray);
				fixed (byte* ptr = new byte[bytesToMarshal])
				{
					SBinaryArray.MarshalToNative(ptr, sbinArray);
					return SafeExMapiFolderHandle.IMAPIFolder_SetReadFlags(this.handle, (_SBinaryArray*)ptr, IntPtr.Zero, IntPtr.Zero, ulFlags);
				}
			}
			return SafeExMapiFolderHandle.IMAPIFolder_SetReadFlags(this.handle, null, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0003577E File Offset: 0x0003397E
		public int GetMessageStatus(byte[] lpEntryId, int ulFlags, out MessageStatus pulMessageStatus)
		{
			return SafeExMapiFolderHandle.IMAPIFolder_GetMessageStatus(this.handle, (lpEntryId != null) ? lpEntryId.Length : 0, lpEntryId, ulFlags, out pulMessageStatus);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00035797 File Offset: 0x00033997
		public int SetMessageStatus(byte[] lpEntryId, MessageStatus ulNewStatus, MessageStatus ulNewStatusMask, out MessageStatus pulOldStatus)
		{
			return SafeExMapiFolderHandle.IMAPIFolder_SetMessageStatus(this.handle, (lpEntryId != null) ? lpEntryId.Length : 0, lpEntryId, ulNewStatus, ulNewStatusMask, out pulOldStatus);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000357B2 File Offset: 0x000339B2
		public int EmptyFolder(int ulFlags)
		{
			return SafeExMapiFolderHandle.IMAPIFolder_EmptyFolder(this.handle, IntPtr.Zero, IntPtr.Zero, ulFlags);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000357CA File Offset: 0x000339CA
		public int IsContentAvailable(out bool isContentAvailable)
		{
			return SafeExMapiFolderHandle.IExRpcFolder_IsContentAvailable(this.handle, out isContentAvailable);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000357D8 File Offset: 0x000339D8
		public int GetReplicaServers(out string[] servers, out uint numberOfCheapServers)
		{
			servers = null;
			numberOfCheapServers = 0U;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				uint num = 0U;
				uint num3;
				int num2 = SafeExMapiFolderHandle.IExRpcFolder_GetReplicaServers(this.handle, out num3, out safeExLinkedMemoryHandle, out num);
				if (num2 == 0)
				{
					numberOfCheapServers = (uint)((ushort)num);
					if (0U < num3 && !safeExLinkedMemoryHandle.IsInvalid)
					{
						string[] array = new string[num3];
						IntPtr intPtr = safeExLinkedMemoryHandle.DangerousGetHandle();
						int num4 = Marshal.SizeOf(typeof(IntPtr));
						int num5 = 0;
						while ((long)num5 < (long)((ulong)num3))
						{
							IntPtr ptr = Marshal.ReadIntPtr(intPtr);
							array[num5] = Marshal.PtrToStringAnsi(ptr);
							intPtr = (IntPtr)((long)intPtr + (long)num4);
							num5++;
						}
						servers = array;
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
			}
			return result;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00035894 File Offset: 0x00033A94
		public int SetMessageFlags(int cbEntryId, byte[] lpEntryId, uint ulStatus, uint ulMask)
		{
			return SafeExMapiFolderHandle.IExRpcFolder_SetMessageFlags(this.handle, cbEntryId, lpEntryId, ulStatus, ulMask);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000358A6 File Offset: 0x00033AA6
		public int CopyMessagesEx(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags, PropValue[] pva)
		{
			return this.InternalCopyMessagesEx(sbinArray, iMAPIFolderDest, ulFlags, pva);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000358B4 File Offset: 0x00033AB4
		private unsafe int InternalCopyMessagesEx(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags, PropValue[] pva)
		{
			int num = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[num])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				num = 0;
				for (int i = 0; i < pva.Length; i++)
				{
					num += pva[i].GetBytesToMarshal();
				}
				fixed (byte* ptr2 = new byte[num])
				{
					PropValue.MarshalToNative(pva, ptr2);
					return SafeExMapiFolderHandle.IExRpcFolder_CopyMessagesEx(this.handle, (_SBinaryArray*)ptr, ((SafeExMapiFolderHandle)iMAPIFolderDest).DangerousGetHandle(), ulFlags, pva.Length, (SPropValue*)ptr2);
				}
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003595D File Offset: 0x00033B5D
		public int CopyMessagesEx_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, PropValue[] pva)
		{
			return this.InternalCopyMessagesEx_External(sbinArray, iMAPIFolderDest, ulFlags, pva);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003596C File Offset: 0x00033B6C
		private unsafe int InternalCopyMessagesEx_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, PropValue[] pva)
		{
			int num = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[num])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				num = 0;
				for (int i = 0; i < pva.Length; i++)
				{
					num += pva[i].GetBytesToMarshal();
				}
				fixed (byte* ptr2 = new byte[num])
				{
					PropValue.MarshalToNative(pva, ptr2);
					return SafeExMapiFolderHandle.IExRpcFolder_CopyMessagesEx_External(this.handle, (_SBinaryArray*)ptr, iMAPIFolderDest, ulFlags, pva.Length, (SPropValue*)ptr2);
				}
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00035A0B File Offset: 0x00033C0B
		public int SetPropsConditional(Restriction lpRes, PropValue[] lpPropArray, out PropProblem[] lppProblems)
		{
			return this.InternalSetPropsConditional(lpRes, lpPropArray, out lppProblems);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00035A18 File Offset: 0x00033C18
		private unsafe int InternalSetPropsConditional(Restriction lpRes, PropValue[] lpPropArray, out PropProblem[] lppProblems)
		{
			lppProblems = null;
			int num = lpRes.GetBytesToMarshal();
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SRestriction* ptr2 = (SRestriction*)ptr;
			ptr += (SRestriction.SizeOf + 7 & -8);
			lpRes.MarshalToNative(ptr2, ref ptr);
			for (int i = 0; i < lpPropArray.Length; i++)
			{
				num += lpPropArray[i].GetBytesToMarshal();
			}
			fixed (byte* ptr3 = new byte[num])
			{
				PropValue.MarshalToNative(lpPropArray, ptr3);
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
				int result;
				try
				{
					int num2 = SafeExMapiFolderHandle.IExRpcFolder_SetPropsConditional(this.handle, ptr2, lpPropArray.Length, (SPropValue*)ptr3, out safeExLinkedMemoryHandle);
					if (num2 == 0 && !safeExLinkedMemoryHandle.IsInvalid)
					{
						lppProblems = safeExLinkedMemoryHandle.ReadPropProblemArray();
					}
					result = num2;
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
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00035AE8 File Offset: 0x00033CE8
		public int CopyMessagesEID(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags, PropValue[] lpPropArray, out byte[][] lppEntryIds, out byte[][] lppChangeNumbers)
		{
			return this.InternalCopyMessagesEID(sbinArray, iMAPIFolderDest, ulFlags, lpPropArray, out lppEntryIds, out lppChangeNumbers);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00035AFC File Offset: 0x00033CFC
		private unsafe int InternalCopyMessagesEID(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags, PropValue[] lpPropArray, out byte[][] lppEntryIds, out byte[][] lppChangeNumbers)
		{
			lppEntryIds = null;
			lppChangeNumbers = null;
			int num = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[num])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				int num2 = 0;
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle2 = null;
				int result;
				try
				{
					if (lpPropArray != null && lpPropArray.Length > 0)
					{
						num = 0;
						for (int i = 0; i < lpPropArray.Length; i++)
						{
							num += lpPropArray[i].GetBytesToMarshal();
						}
						try
						{
							fixed (byte* ptr2 = new byte[num])
							{
								PropValue.MarshalToNative(lpPropArray, ptr2);
								num2 = SafeExMapiFolderHandle.IExRpcFolder_CopyMessagesEID(this.handle, (_SBinaryArray*)ptr, ((SafeExMapiFolderHandle)iMAPIFolderDest).DangerousGetHandle(), ulFlags, lpPropArray.Length, (SPropValue*)ptr2, out safeExLinkedMemoryHandle, out safeExLinkedMemoryHandle2);
								goto IL_E9;
							}
						}
						finally
						{
							byte* ptr2 = null;
						}
					}
					num2 = SafeExMapiFolderHandle.IExRpcFolder_CopyMessagesEID(this.handle, (_SBinaryArray*)ptr, ((SafeExMapiFolderHandle)iMAPIFolderDest).DangerousGetHandle(), ulFlags, 0, null, out safeExLinkedMemoryHandle, out safeExLinkedMemoryHandle2);
					IL_E9:
					if (!safeExLinkedMemoryHandle.IsInvalid)
					{
						lppEntryIds = _SBinaryArray.Unmarshal(safeExLinkedMemoryHandle);
					}
					if (!safeExLinkedMemoryHandle2.IsInvalid)
					{
						lppChangeNumbers = _SBinaryArray.Unmarshal(safeExLinkedMemoryHandle2);
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
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00035C50 File Offset: 0x00033E50
		public int CopyMessagesEID_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, PropValue[] lpPropArray, out byte[][] lppEntryIds, out byte[][] lppChangeNumbers)
		{
			return this.InternalCopyMessagesEID_External(sbinArray, iMAPIFolderDest, ulFlags, lpPropArray, out lppEntryIds, out lppChangeNumbers);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00035C64 File Offset: 0x00033E64
		private unsafe int InternalCopyMessagesEID_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, PropValue[] lpPropArray, out byte[][] lppEntryIds, out byte[][] lppChangeNumbers)
		{
			lppEntryIds = null;
			lppChangeNumbers = null;
			int num = SBinaryArray.GetBytesToMarshal(sbinArray);
			fixed (byte* ptr = new byte[num])
			{
				SBinaryArray.MarshalToNative(ptr, sbinArray);
				int num2 = 0;
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle2 = null;
				int result;
				try
				{
					if (lpPropArray != null && lpPropArray.Length > 0)
					{
						num = 0;
						for (int i = 0; i < lpPropArray.Length; i++)
						{
							num += lpPropArray[i].GetBytesToMarshal();
						}
						try
						{
							fixed (byte* ptr2 = new byte[num])
							{
								PropValue.MarshalToNative(lpPropArray, ptr2);
								num2 = SafeExMapiFolderHandle.IExRpcFolder_CopyMessagesEID_External(this.handle, (_SBinaryArray*)ptr, iMAPIFolderDest, ulFlags, lpPropArray.Length, (SPropValue*)ptr2, out safeExLinkedMemoryHandle, out safeExLinkedMemoryHandle2);
								goto IL_D2;
							}
						}
						finally
						{
							byte* ptr2 = null;
						}
					}
					num2 = SafeExMapiFolderHandle.IExRpcFolder_CopyMessagesEID_External(this.handle, (_SBinaryArray*)ptr, iMAPIFolderDest, ulFlags, 0, null, out safeExLinkedMemoryHandle, out safeExLinkedMemoryHandle2);
					IL_D2:
					if (!safeExLinkedMemoryHandle.IsInvalid)
					{
						lppEntryIds = _SBinaryArray.Unmarshal(safeExLinkedMemoryHandle);
					}
					if (!safeExLinkedMemoryHandle2.IsInvalid)
					{
						lppChangeNumbers = _SBinaryArray.Unmarshal(safeExLinkedMemoryHandle2);
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
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00035DA0 File Offset: 0x00033FA0
		public int CreateFolderEx(int ulFolderType, string lpwszFolderName, string lpwszFolderComment, byte[] lpEntryId, int ulFlags, out IExMapiFolder iMAPIFolderNew)
		{
			SafeExMapiFolderHandle safeExMapiFolderHandle = null;
			int result = SafeExMapiFolderHandle.IExRpcFolder_CreateFolderEx(this.handle, ulFolderType, lpwszFolderName, lpwszFolderComment, (lpEntryId != null) ? lpEntryId.Length : 0, lpEntryId, IntPtr.Zero, ulFlags, out safeExMapiFolderHandle);
			iMAPIFolderNew = safeExMapiFolderHandle;
			return result;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00035DD9 File Offset: 0x00033FD9
		public int HrSerializeSRestrictionEx(Restriction prest, out byte[] pbRest)
		{
			return this.InternalHrSerializeSRestrictionEx(prest, out pbRest);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00035DE4 File Offset: 0x00033FE4
		private unsafe int InternalHrSerializeSRestrictionEx(Restriction prest, out byte[] pbRest)
		{
			pbRest = null;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int bytesToMarshal = prest.GetBytesToMarshal();
				byte[] array = new byte[bytesToMarshal];
				try
				{
					fixed (byte* ptr = array)
					{
						SRestriction* ptr2 = (SRestriction*)ptr;
						byte* ptr3 = ptr;
						ptr3 += (SRestriction.SizeOf + 7 & -8);
						prest.MarshalToNative(ptr2, ref ptr3);
						uint num2;
						int num = SafeExMapiFolderHandle.HrSerializeSRestrictionEx(this.handle, ptr2, out safeExMemoryHandle, out num2);
						if (num == 0)
						{
							array = new byte[num2];
							safeExMemoryHandle.CopyTo(array, 0, (int)num2);
							pbRest = array;
						}
						result = num;
					}
				}
				finally
				{
					byte* ptr = null;
				}
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

		// Token: 0x06000D9C RID: 3484 RVA: 0x00035EA4 File Offset: 0x000340A4
		public int HrDeserializeSRestrictionEx(byte[] pbRest, out Restriction prest)
		{
			prest = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExMapiFolderHandle.HrDeserializeSRestrictionEx(this.handle, pbRest, (uint)pbRest.Length, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					prest = Restriction.Unmarshal(safeExLinkedMemoryHandle);
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

		// Token: 0x06000D9D RID: 3485 RVA: 0x00035EF4 File Offset: 0x000340F4
		public unsafe int HrSerializeActionsEx(RuleAction[] pActions, out byte[] pbActions)
		{
			pbActions = null;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int bytesToMarshal = RuleActions.GetBytesToMarshal(pActions);
			byte[] array = new byte[bytesToMarshal];
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				RuleActions.MarshalToNative(ref ptr2, pActions);
			}
			int result;
			try
			{
				try
				{
					fixed (byte* ptr3 = &array[0])
					{
						_Actions* pActions2 = (_Actions*)ptr3;
						uint num2;
						int num = SafeExMapiFolderHandle.HrSerializeActionsEx(this.handle, pActions2, out safeExMemoryHandle, out num2);
						if (num == 0)
						{
							pbActions = new byte[num2];
							safeExMemoryHandle.CopyTo(pbActions, 0, (int)num2);
						}
						result = num;
					}
				}
				finally
				{
					byte* ptr3 = null;
				}
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

		// Token: 0x06000D9E RID: 3486 RVA: 0x00035F9C File Offset: 0x0003419C
		public int HrDeserializeActionsEx(byte[] pbActions, out RuleAction[] pActions)
		{
			pActions = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExMapiFolderHandle.HrDeserializeActionsEx(this.handle, pbActions, (uint)pbActions.Length, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					pActions = RuleActions.Unmarshal(safeExLinkedMemoryHandle.DangerousGetHandle());
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

		// Token: 0x06000D9F RID: 3487 RVA: 0x00035FF0 File Offset: 0x000341F0
		public int SetPropsEx(bool trackChanges, ICollection<PropValue> lpPropArray, out PropProblem[] lppProblems)
		{
			return this.InternalSetPropsEx(trackChanges, lpPropArray, out lppProblems);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00035FFC File Offset: 0x000341FC
		private unsafe int InternalSetPropsEx(bool trackChanges, ICollection<PropValue> lpPropArray, out PropProblem[] lppProblems)
		{
			lppProblems = null;
			int num = 0;
			foreach (PropValue propValue in lpPropArray)
			{
				num += propValue.GetBytesToMarshal();
			}
			fixed (byte* ptr = new byte[num])
			{
				PropValue.MarshalToNative(lpPropArray, ptr);
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
				int result;
				try
				{
					int num2 = SafeExMapiFolderHandle.IExRpcFolder_SetPropsEx(this.handle, trackChanges, lpPropArray.Count, (SPropValue*)ptr, out safeExLinkedMemoryHandle);
					if (!safeExLinkedMemoryHandle.IsInvalid)
					{
						lppProblems = safeExLinkedMemoryHandle.ReadPropProblemArray();
					}
					result = num2;
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
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000360C0 File Offset: 0x000342C0
		public int DeletePropsEx(bool trackChanges, ICollection<PropTag> lpPropTags, out PropProblem[] lppProblems)
		{
			lppProblems = null;
			PropTag[] lpPropTagArray = PropTagHelper.SPropTagArray(lpPropTags);
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExMapiFolderHandle.IExRpcFolder_DeletePropsEx(this.handle, trackChanges, lpPropTagArray, out safeExLinkedMemoryHandle);
				if (num == 0 && safeExLinkedMemoryHandle != null)
				{
					lppProblems = safeExLinkedMemoryHandle.ReadPropProblemArray();
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

		// Token: 0x06000DA2 RID: 3490
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_CreateMessage(IntPtr iMAPIFolder, IntPtr lpInterface, int ulFlags, out SafeExMapiMessageHandle iMessage);

		// Token: 0x06000DA3 RID: 3491
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPIFolder_CopyMessages(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IntPtr lpInterface, IntPtr iMAPIFolderDest, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DA4 RID: 3492
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPIFolder_CopyMessages_External(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IntPtr lpInterface, IMAPIFolder iMAPIFolderDest, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DA5 RID: 3493
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPIFolder_DeleteMessages(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DA6 RID: 3494
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_CreateFolder(IntPtr iMAPIFolder, int ulFolderType, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderName, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderComment, IntPtr lpInterface, int ulFlags, out SafeExMapiFolderHandle iMAPIFolderNew);

		// Token: 0x06000DA7 RID: 3495
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_CopyFolder(IntPtr iMAPIFolder, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, IntPtr lpInterface, IntPtr iMAPIFolderDest, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszNewFolderName, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DA8 RID: 3496
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_CopyFolder_External(IntPtr iMAPIFolder, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, IntPtr lpInterface, IMAPIFolder iMAPIFolderDest, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszNewFolderName, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DA9 RID: 3497
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_DeleteFolder(IntPtr iMAPIFolder, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DAA RID: 3498
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPIFolder_SetReadFlags(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DAB RID: 3499
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_GetMessageStatus(IntPtr iMAPIFolder, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, int ulFlags, out MessageStatus pulMessageStatus);

		// Token: 0x06000DAC RID: 3500
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_SetMessageStatus(IntPtr iMAPIFolder, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, MessageStatus ulNewStatus, MessageStatus ulNewStatusMask, out MessageStatus pulOldStatus);

		// Token: 0x06000DAD RID: 3501
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIFolder_EmptyFolder(IntPtr iMAPIFolder, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000DAE RID: 3502
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcFolder_IsContentAvailable(IntPtr iMAPIFolder, [MarshalAs(UnmanagedType.Bool)] out bool isContentAvailable);

		// Token: 0x06000DAF RID: 3503
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcFolder_GetReplicaServers(IntPtr iMAPIFolder, out uint numberOfServers, out SafeExLinkedMemoryHandle servers, out uint numberOfCheapServers);

		// Token: 0x06000DB0 RID: 3504
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcFolder_SetMessageFlags(IntPtr iMAPIFolder, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryId, uint ulStatus, uint ulMask);

		// Token: 0x06000DB1 RID: 3505
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcFolder_CopyMessagesEx(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IntPtr iMAPIFolderDest, int ulFlags, int cValues, SPropValue* lpPropArray);

		// Token: 0x06000DB2 RID: 3506
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcFolder_CopyMessagesEx_External(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, int cValues, SPropValue* lpPropArray);

		// Token: 0x06000DB3 RID: 3507
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcFolder_SetPropsConditional(IntPtr iMAPIFolder, [In] SRestriction* lpRes, int cValues, SPropValue* lpPropArray, out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000DB4 RID: 3508
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcFolder_CopyMessagesEID(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IntPtr iMAPIFolderDest, int ulFlags, int cValues, SPropValue* lpPropArray, out SafeExLinkedMemoryHandle lppEntryIds, out SafeExLinkedMemoryHandle lppChangeNumbers);

		// Token: 0x06000DB5 RID: 3509
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcFolder_CopyMessagesEID_External(IntPtr iMAPIFolder, _SBinaryArray* sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, int cValues, SPropValue* lpPropArray, out SafeExLinkedMemoryHandle lppEntryIds, out SafeExLinkedMemoryHandle lppChangeNumbers);

		// Token: 0x06000DB6 RID: 3510
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcFolder_CreateFolderEx(IntPtr iMAPIFolder, int ulFolderType, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderName, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpwszFolderComment, int cbEntryId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] lpEntryId, IntPtr lpInterface, int ulFlags, out SafeExMapiFolderHandle iMAPIFolderNew);

		// Token: 0x06000DB7 RID: 3511
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExRpcFolder_SetPropsEx(IntPtr iMAPIFolder, bool trackChanges, int cValues, SPropValue* lpPropArray, out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000DB8 RID: 3512
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcFolder_DeletePropsEx(IntPtr iMAPIFolder, bool trackChanges, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, out SafeExLinkedMemoryHandle lppProblems);

		// Token: 0x06000DB9 RID: 3513
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int HrSerializeSRestrictionEx(IntPtr iMAPIProp, SRestriction* prest, out SafeExMemoryHandle pbRest, out uint cbRest);

		// Token: 0x06000DBA RID: 3514
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int HrDeserializeSRestrictionEx(IntPtr iMAPIProp, [MarshalAs(UnmanagedType.LPArray)] byte[] pbRest, uint cbRest, out SafeExLinkedMemoryHandle prest);

		// Token: 0x06000DBB RID: 3515
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int HrSerializeActionsEx(IntPtr iMAPIProp, _Actions* pActions, out SafeExMemoryHandle pbActions, out uint cbActions);

		// Token: 0x06000DBC RID: 3516
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int HrDeserializeActionsEx(IntPtr iMAPIProp, [MarshalAs(UnmanagedType.LPArray)] byte[] pbActions, uint cbActions, out SafeExLinkedMemoryHandle pActions);
	}
}

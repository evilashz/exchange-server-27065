using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000280 RID: 640
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("37FB08C3-F6C8-4de8-B8DA-AB7E41D01ECE")]
	[ComImport]
	internal interface IExRpcMsgStore
	{
		// Token: 0x06000B62 RID: 2914
		[PreserveSig]
		int CreateEntryId(long fid, long mid, [MarshalAs(UnmanagedType.Bool)] bool fMessage, [MarshalAs(UnmanagedType.Bool)] bool fLongTerm, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000B63 RID: 2915
		[PreserveSig]
		int GetShortTermIdsFromLongTermEntryId(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.Bool)] out bool pfMessage, out long pFid, out long pMid);

		// Token: 0x06000B64 RID: 2916
		[PreserveSig]
		int CreateEntryIdFromLegacyDN([MarshalAs(UnmanagedType.LPStr)] string lpszLegacyDN, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000B65 RID: 2917
		[PreserveSig]
		int GetParentEntryId(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int lpcbParentEntryId, out SafeExLinkedMemoryHandle lppParentEntryId);

		// Token: 0x06000B66 RID: 2918
		[PreserveSig]
		int GetAddressTypes(out int cAddressTypes, out IntPtr lppszAddressTypes);

		// Token: 0x06000B67 RID: 2919
		[PreserveSig]
		int BackoffNow(out int iNow);

		// Token: 0x06000B68 RID: 2920
		[PreserveSig]
		int CompressEntryId(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int lpcbCompressedEntryId, out SafeExLinkedMemoryHandle lppCompressedEntryId);

		// Token: 0x06000B69 RID: 2921
		[PreserveSig]
		int ExpandEntryId(int cbCompressedEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpCompressedEntryID, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000B6A RID: 2922
		[PreserveSig]
		int CreateGlobalIdFromId(long id, out SafeExLinkedMemoryHandle lppGid);

		// Token: 0x06000B6B RID: 2923
		[PreserveSig]
		int CreateIdFromGlobalId([MarshalAs(UnmanagedType.LPArray)] [In] byte[] gid, out long id);

		// Token: 0x06000B6C RID: 2924
		[PreserveSig]
		unsafe int MapActionsToMDBActions(_Actions* pactions, out uint cbBuf, out SafeExMemoryHandle pbBuf);

		// Token: 0x06000B6D RID: 2925
		[PreserveSig]
		int GetMailboxInstanceGuid(out Guid guidMailboxInstanceGuid);

		// Token: 0x06000B6E RID: 2926
		[PreserveSig]
		int GetMdbIdMapping(out ushort replidServer, out Guid guidServer);

		// Token: 0x06000B6F RID: 2927
		[PreserveSig]
		int GetReceiveFolderInfo([PointerType("SRowSet*")] out SafeExLinkedMemoryHandle lpSRowSet);

		// Token: 0x06000B70 RID: 2928
		[PreserveSig]
		int GetSpoolerQueueFid(out long fidSpoolerQ);

		// Token: 0x06000B71 RID: 2929
		[PreserveSig]
		int GetLocalRepIds(uint cid, out MapiLtidNative ltid);

		// Token: 0x06000B72 RID: 2930
		[PreserveSig]
		int CreatePublicEntryId(long fid, long mid, [MarshalAs(UnmanagedType.Bool)] bool fMessage, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId);

		// Token: 0x06000B73 RID: 2931
		[PreserveSig]
		int GetTransportQueueFolderId(out long fidTransportQueue);

		// Token: 0x06000B74 RID: 2932
		[PreserveSig]
		int GetIsRulesInterfaceAvailable([MarshalAs(UnmanagedType.Bool)] out bool rulesInterfaceAvailable);

		// Token: 0x06000B75 RID: 2933
		[PreserveSig]
		int SetSpooler();

		// Token: 0x06000B76 RID: 2934
		[PreserveSig]
		int SpoolerSetMessageLockState(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, int ulLockState);

		// Token: 0x06000B77 RID: 2935
		[PreserveSig]
		int SpoolerNotifyMessageNewMail(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStr)] string lpszMsgClass, int ulMessageFlags);

		// Token: 0x06000B78 RID: 2936
		[PreserveSig]
		unsafe int PrereadMessages(_SBinaryArray* sbaEntryIDs);
	}
}

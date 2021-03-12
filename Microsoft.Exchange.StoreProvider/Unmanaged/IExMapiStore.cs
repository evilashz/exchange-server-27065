using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000291 RID: 657
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiStore : IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000BE0 RID: 3040
		int AdviseEx(byte[] lpEntryId, AdviseFlags ulEventMask, IntPtr iOnNotifyDelegate, ulong callbackId, out IntPtr piConnection);

		// Token: 0x06000BE1 RID: 3041
		int Unadvise(IntPtr iConnection);

		// Token: 0x06000BE2 RID: 3042
		int OpenEntry(byte[] lpEntryID, Guid lpInterface, int ulFlags, out int lpulObjType, out IExInterface iObj);

		// Token: 0x06000BE3 RID: 3043
		int GetPerUser(byte[] ltid, bool fSendOnlyIfChanged, int ibStream, int cbDataLimit, out byte[] pb, out bool fLast);

		// Token: 0x06000BE4 RID: 3044
		int SetPerUser(byte[] ltid, Guid? guidReplica, int lib, byte[] pb, int cb, bool fLast);

		// Token: 0x06000BE5 RID: 3045
		int SetReceiveFolder(string lpwszMessageClass, int ulFlags, byte[] lpEntryID);

		// Token: 0x06000BE6 RID: 3046
		int GetReceiveFolder(string lpwszMessageClass, int ulFlags, out byte[] lppEntryId, out string lppszExplicitClass);

		// Token: 0x06000BE7 RID: 3047
		int GetReceiveFolderInfo(out PropValue[][] lpSRowSet);

		// Token: 0x06000BE8 RID: 3048
		int StoreLogoff(ref int ulFlags);

		// Token: 0x06000BE9 RID: 3049
		int AbortSubmit(byte[] lpEntryID, int ulFlags);

		// Token: 0x06000BEA RID: 3050
		int CreateEntryId(long fid, long mid, bool fMessage, bool fLongTerm, out byte[] lppEntryId);

		// Token: 0x06000BEB RID: 3051
		int GetShortTermIdsFromLongTermEntryId(byte[] lpEntryID, out bool pfMessage, out long pFid, out long pMid);

		// Token: 0x06000BEC RID: 3052
		int CreateEntryIdFromLegacyDN(string lpszLegacyDN, out byte[] lppEntryId);

		// Token: 0x06000BED RID: 3053
		int GetParentEntryId(byte[] lpEntryID, out byte[] lppParentEntryId);

		// Token: 0x06000BEE RID: 3054
		int GetAddressTypes(out string[] lppszAddressTypes);

		// Token: 0x06000BEF RID: 3055
		int BackoffNow(out int iNow);

		// Token: 0x06000BF0 RID: 3056
		int CompressEntryId(byte[] lpEntryID, out byte[] lppCompressedEntryId);

		// Token: 0x06000BF1 RID: 3057
		int ExpandEntryId(byte[] lpCompressedEntryID, out byte[] lppEntryId);

		// Token: 0x06000BF2 RID: 3058
		int CreateGlobalIdFromId(long id, out byte[] lppGid);

		// Token: 0x06000BF3 RID: 3059
		int CreateIdFromGlobalId(byte[] gid, out long id);

		// Token: 0x06000BF4 RID: 3060
		int MapActionsToMDBActions(RuleAction[] actions, out byte[] mdbActions);

		// Token: 0x06000BF5 RID: 3061
		int GetMailboxInstanceGuid(out Guid guidMailboxInstanceGuid);

		// Token: 0x06000BF6 RID: 3062
		int GetMdbIdMapping(out ushort replidServer, out Guid guidServer);

		// Token: 0x06000BF7 RID: 3063
		int GetSpoolerQueueFid(out long fidSpoolerQ);

		// Token: 0x06000BF8 RID: 3064
		int GetLocalRepIds(uint cid, out MapiLtidNative ltid);

		// Token: 0x06000BF9 RID: 3065
		int CreatePublicEntryId(long fid, long mid, bool fMessage, out byte[] lppEntryId);

		// Token: 0x06000BFA RID: 3066
		int GetTransportQueueFolderId(out long fidTransportQueue);

		// Token: 0x06000BFB RID: 3067
		int GetIsRulesInterfaceAvailable(out bool rulesInterfaceAvailable);

		// Token: 0x06000BFC RID: 3068
		int SetSpooler();

		// Token: 0x06000BFD RID: 3069
		int SpoolerSetMessageLockState(byte[] lpEntryID, int ulLockState);

		// Token: 0x06000BFE RID: 3070
		int SpoolerNotifyMessageNewMail(byte[] lpEntryID, string lpszMsgClass, int ulMessageFlags);

		// Token: 0x06000BFF RID: 3071
		int GetPerUserGuid(MapiLtidNative ltid, out Guid guid);

		// Token: 0x06000C00 RID: 3072
		int GetPerUserLtids(Guid guid, out MapiLtidNative[] ltids);

		// Token: 0x06000C01 RID: 3073
		int GetEffectiveRights(byte[] lpAddressBookEntryId, byte[] lpEntryId, out uint rights);

		// Token: 0x06000C02 RID: 3074
		int PrereadMessages(byte[][] entryIds);

		// Token: 0x06000C03 RID: 3075
		int GetInTransitStatus(out uint inTransitStatus);
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000297 RID: 663
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiFolder : IExMapiContainer, IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C35 RID: 3125
		int CreateMessage(int ulFlags, out IExMapiMessage iMessage);

		// Token: 0x06000C36 RID: 3126
		int CopyMessages(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags);

		// Token: 0x06000C37 RID: 3127
		int CopyMessages_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags);

		// Token: 0x06000C38 RID: 3128
		int DeleteMessages(SBinary[] sbinArray, int ulFlags);

		// Token: 0x06000C39 RID: 3129
		int CreateFolder(int ulFolderType, string lpwszFolderName, string lpwszFolderComment, int ulFlags, out IExMapiFolder iMAPIFolderNew);

		// Token: 0x06000C3A RID: 3130
		int CopyFolder(int cbEntryId, byte[] lpEntryId, IExMapiFolder iMAPIFolderDest, string lpwszNewFolderName, int ulFlags);

		// Token: 0x06000C3B RID: 3131
		int CopyFolder_External(int cbEntryId, byte[] lpEntryId, IMAPIFolder iMAPIFolderDest, string lpwszNewFolderName, int ulFlags);

		// Token: 0x06000C3C RID: 3132
		int DeleteFolder(byte[] lpEntryId, int ulFlags);

		// Token: 0x06000C3D RID: 3133
		int SetReadFlags(SBinary[] sbinArray, int ulFlags);

		// Token: 0x06000C3E RID: 3134
		int GetMessageStatus(byte[] lpEntryId, int ulFlags, out MessageStatus pulMessageStatus);

		// Token: 0x06000C3F RID: 3135
		int SetMessageStatus(byte[] lpEntryId, MessageStatus ulNewStatus, MessageStatus ulNewStatusMask, out MessageStatus pulOldStatus);

		// Token: 0x06000C40 RID: 3136
		int EmptyFolder(int ulFlags);

		// Token: 0x06000C41 RID: 3137
		int IsContentAvailable(out bool isContentAvailable);

		// Token: 0x06000C42 RID: 3138
		int GetReplicaServers(out string[] servers, out uint numberOfCheapServers);

		// Token: 0x06000C43 RID: 3139
		int SetMessageFlags(int cbEntryId, byte[] lpEntryId, uint ulStatus, uint ulMask);

		// Token: 0x06000C44 RID: 3140
		int CopyMessagesEx(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags, PropValue[] pva);

		// Token: 0x06000C45 RID: 3141
		int CopyMessagesEx_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, PropValue[] pva);

		// Token: 0x06000C46 RID: 3142
		int SetPropsConditional(Restriction lpRes, PropValue[] lpPropArray, out PropProblem[] lppProblems);

		// Token: 0x06000C47 RID: 3143
		int CopyMessagesEID(SBinary[] sbinArray, IExMapiFolder iMAPIFolderDest, int ulFlags, PropValue[] lpPropArray, out byte[][] lppEntryIds, out byte[][] lppChangeNumbers);

		// Token: 0x06000C48 RID: 3144
		int CopyMessagesEID_External(SBinary[] sbinArray, IMAPIFolder iMAPIFolderDest, int ulFlags, PropValue[] lpPropArray, out byte[][] lppEntryIds, out byte[][] lppChangeNumbers);

		// Token: 0x06000C49 RID: 3145
		int CreateFolderEx(int ulFolderType, string lpwszFolderName, string lpwszFolderComment, byte[] lpEntryId, int ulFlags, out IExMapiFolder iMAPIFolderNew);

		// Token: 0x06000C4A RID: 3146
		int HrSerializeSRestrictionEx(Restriction prest, out byte[] pbRest);

		// Token: 0x06000C4B RID: 3147
		int HrDeserializeSRestrictionEx(byte[] pbRest, out Restriction prest);

		// Token: 0x06000C4C RID: 3148
		int HrSerializeActionsEx(RuleAction[] pActions, out byte[] pbActions);

		// Token: 0x06000C4D RID: 3149
		int HrDeserializeActionsEx(byte[] pbActions, out RuleAction[] pActions);

		// Token: 0x06000C4E RID: 3150
		int SetPropsEx(bool trackChanges, ICollection<PropValue> lpPropArray, out PropProblem[] lppProblems);

		// Token: 0x06000C4F RID: 3151
		int DeletePropsEx(bool trackChanges, ICollection<PropTag> lpPropTags, out PropProblem[] lppProblems);
	}
}

using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FxProxyFolderWrapper : FxProxyWrapper, IMAPIFolder, IMAPIContainer, IMAPIProp
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		internal FxProxyFolderWrapper(IMapiFxCollector iFxCollector) : base(iFxCollector)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000ABDE File Offset: 0x00008DDE
		public int GetContentsTable(int ulFlags, out IMAPITable iMAPITable)
		{
			iMAPITable = null;
			return -2147221246;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public int GetHierarchyTable(int ulFlags, out IMAPITable iMAPITable)
		{
			iMAPITable = null;
			return -2147221246;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		public int OpenEntry(int cbEntryID, byte[] lpEntryID, Guid lpInterface, int ulFlags, out int lpulObjType, out object obj)
		{
			lpulObjType = 0;
			obj = null;
			return -2147221246;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000AC01 File Offset: 0x00008E01
		public unsafe int SetSearchCriteria(SRestriction* lpRestriction, _SBinaryArray* lpContainerList, int ulSearchFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000AC08 File Offset: 0x00008E08
		public int GetSearchCriteria(int ulFlags, out SafeExLinkedMemoryHandle lpRestriction, out SafeExLinkedMemoryHandle lpContainerList, out int ulSearchState)
		{
			lpRestriction = null;
			lpContainerList = null;
			ulSearchState = 0;
			return -2147221246;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000AC19 File Offset: 0x00008E19
		public int CreateMessage(IntPtr lpInterface, int ulFlags, out IMessage iMessage)
		{
			iMessage = null;
			return -2147221246;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000AC23 File Offset: 0x00008E23
		public unsafe int CopyMessages(_SBinaryArray* sbinArray, IntPtr lpInterface, IMAPIFolder destFolder, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000AC2A File Offset: 0x00008E2A
		public unsafe int DeleteMessages(_SBinaryArray* sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000AC31 File Offset: 0x00008E31
		public int CreateFolder(int ulFolderType, string lpwszFolderName, string lpwszFolderComment, IntPtr lpInterface, int ulFlags, out IMAPIFolder iMAPIFolder)
		{
			iMAPIFolder = null;
			return -2147221246;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public int CopyFolder(int cbEntryId, byte[] lpEntryId, IntPtr lpInterface, IMAPIFolder destFolder, string lpwszNewFolderName, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000AC43 File Offset: 0x00008E43
		public int DeleteFolder(int cbEntryId, byte[] lpEntryId, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000AC4A File Offset: 0x00008E4A
		public unsafe int SetReadFlags(_SBinaryArray* sbinArray, IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000AC51 File Offset: 0x00008E51
		public int GetMessageStatus(int cbEntryId, byte[] lpEntryId, int ulFlags, out MessageStatus pulMessageStatus)
		{
			pulMessageStatus = MessageStatus.None;
			return -2147221246;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000AC5C File Offset: 0x00008E5C
		public int SetMessageStatus(int cbEntryId, byte[] lpEntryId, MessageStatus ulNewStatus, MessageStatus ulNewStatusMask, out MessageStatus pulOldStatus)
		{
			pulOldStatus = MessageStatus.None;
			return -2147221246;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AC67 File Offset: 0x00008E67
		public int Slot1c()
		{
			return -2147221246;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AC6E File Offset: 0x00008E6E
		public int EmptyFolder(IntPtr ulUIParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}
	}
}

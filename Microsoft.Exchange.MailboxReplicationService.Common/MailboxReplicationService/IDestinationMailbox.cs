using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000141 RID: 321
	internal interface IDestinationMailbox : IMailbox, IDisposable
	{
		// Token: 0x06000AB6 RID: 2742
		bool MailboxExists();

		// Token: 0x06000AB7 RID: 2743
		CreateMailboxResult CreateMailbox(byte[] mailboxData, MailboxSignatureFlags sourceSignatureFlags);

		// Token: 0x06000AB8 RID: 2744
		void ProcessMailboxSignature(byte[] mailboxData);

		// Token: 0x06000AB9 RID: 2745
		IDestinationFolder GetFolder(byte[] entryId);

		// Token: 0x06000ABA RID: 2746
		IFxProxy GetFxProxy();

		// Token: 0x06000ABB RID: 2747
		PropProblemData[] SetProps(PropValueData[] pva);

		// Token: 0x06000ABC RID: 2748
		IFxProxyPool GetFxProxyPool(ICollection<byte[]> folderIds);

		// Token: 0x06000ABD RID: 2749
		void CreateFolder(FolderRec sourceFolder, CreateFolderFlags createFolderFlags, out byte[] newFolderId);

		// Token: 0x06000ABE RID: 2750
		void MoveFolder(byte[] folderId, byte[] oldParentId, byte[] newParentId);

		// Token: 0x06000ABF RID: 2751
		void DeleteFolder(FolderRec folderRec);

		// Token: 0x06000AC0 RID: 2752
		void SetMailboxSecurityDescriptor(RawSecurityDescriptor sd);

		// Token: 0x06000AC1 RID: 2753
		void SetUserSecurityDescriptor(RawSecurityDescriptor sd);

		// Token: 0x06000AC2 RID: 2754
		void PreFinalSyncDataProcessing(int? sourceMailboxVersion);

		// Token: 0x06000AC3 RID: 2755
		ConstraintCheckResultType CheckDataGuarantee(DateTime commitTimestamp, out LocalizedString failureReason);

		// Token: 0x06000AC4 RID: 2756
		void ForceLogRoll();

		// Token: 0x06000AC5 RID: 2757
		List<ReplayAction> GetActions(string replaySyncState, int maxNumberOfActions);

		// Token: 0x06000AC6 RID: 2758
		void SetMailboxSettings(ItemPropertiesBase item);
	}
}

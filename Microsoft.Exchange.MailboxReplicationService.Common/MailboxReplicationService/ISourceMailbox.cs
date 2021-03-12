using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000140 RID: 320
	internal interface ISourceMailbox : IMailbox, IDisposable
	{
		// Token: 0x06000AAC RID: 2732
		byte[] GetMailboxBasicInfo(MailboxSignatureFlags flags);

		// Token: 0x06000AAD RID: 2733
		ISourceFolder GetFolder(byte[] entryId);

		// Token: 0x06000AAE RID: 2734
		void CopyTo(IFxProxy destMailbox, PropTag[] excludeProps);

		// Token: 0x06000AAF RID: 2735
		void SetMailboxSyncState(string syncStateStr);

		// Token: 0x06000AB0 RID: 2736
		string GetMailboxSyncState();

		// Token: 0x06000AB1 RID: 2737
		MailboxChangesManifest EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges);

		// Token: 0x06000AB2 RID: 2738
		void ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps);

		// Token: 0x06000AB3 RID: 2739
		void ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags);

		// Token: 0x06000AB4 RID: 2740
		List<ReplayActionResult> ReplayActions(List<ReplayAction> actions);

		// Token: 0x06000AB5 RID: 2741
		List<ItemPropertiesBase> GetMailboxSettings(GetMailboxSettingsFlags flags);
	}
}

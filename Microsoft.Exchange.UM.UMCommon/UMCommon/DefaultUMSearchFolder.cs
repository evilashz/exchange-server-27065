using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200005D RID: 93
	internal abstract class DefaultUMSearchFolder : UMSearchFolder
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000D003 File Offset: 0x0000B203
		internal DefaultUMSearchFolder(MailboxSession itemStore) : base(itemStore)
		{
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600038A RID: 906
		protected abstract DefaultFolderType DefaultFolderType { get; }

		// Token: 0x0600038B RID: 907 RVA: 0x0000D00C File Offset: 0x0000B20C
		protected override void InternalCreateSearchFolder()
		{
			StoreObjectId folderId = base.ItemStore.CreateDefaultFolder(this.DefaultFolderType);
			base.SearchFolder = SearchFolder.Bind(base.ItemStore, folderId);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D03D File Offset: 0x0000B23D
		protected override void InternalDeleteSearchFolder()
		{
			base.ItemStore.DeleteDefaultFolder(this.DefaultFolderType, DeleteItemFlags.SoftDelete);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D054 File Offset: 0x0000B254
		protected override bool InternalTryRepairSearchFolder()
		{
			StoreObjectId folderId = null;
			if (base.ItemStore.TryFixDefaultFolderId(this.DefaultFolderType, out folderId))
			{
				base.SearchFolder = SearchFolder.Bind(base.ItemStore, folderId);
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Successfully repaired UM search folder.");
				return true;
			}
			ExTraceGlobals.UtilTracer.TraceError(0L, "Failed to repair UM search folder.");
			return false;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		protected override StoreObjectId GetSearchFolderId()
		{
			return base.ItemStore.GetDefaultFolderId(this.DefaultFolderType);
		}
	}
}

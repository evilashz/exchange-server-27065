using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000029 RID: 41
	public sealed class CrucialFolderId
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0003768C File Offset: 0x0003588C
		public void AdjustPrivateMailboxReplids(Context context, IReplidGuidMap replidGuidMap)
		{
			this.FidRoot = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidRoot);
			this.FidIPMsubtree = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidIPMsubtree);
			this.FidDAF = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidDAF);
			this.FidSpoolerQ = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidSpoolerQ);
			this.FidInbox = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidInbox);
			this.FidOutbox = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidOutbox);
			this.FidSentmail = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidSentmail);
			this.FidWastebasket = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidWastebasket);
			this.FidFinder = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidFinder);
			this.FidViews = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidViews);
			this.FidCommonViews = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidCommonViews);
			this.FidSchedule = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidSchedule);
			this.FidShortcuts = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidShortcuts);
			this.FidConversations = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidConversations);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000377A4 File Offset: 0x000359A4
		public void AdjustPublicFoldersMailboxReplids(Context context, IReplidGuidMap replidGuidMap)
		{
			this.FidRoot = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidRoot);
			this.FidPublicFolderIpmSubTree = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderIpmSubTree);
			this.FidPublicFolderNonIpmSubTree = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderNonIpmSubTree);
			this.FidPublicFolderEFormsRegistry = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderEFormsRegistry);
			this.FidPublicFolderDumpsterRoot = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderDumpsterRoot);
			this.FidPublicFolderTombstonesRoot = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderTombstonesRoot);
			this.FidPublicFolderAsyncDeleteState = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderAsyncDeleteState);
			this.FidPublicFolderInternalSubmission = CrucialFolderId.AdjustReplid(context, replidGuidMap, this.FidPublicFolderInternalSubmission);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0003784C File Offset: 0x00035A4C
		private static ExchangeId AdjustReplid(Context context, IReplidGuidMap replidGuidMap, ExchangeId id)
		{
			ushort replidFromGuid = replidGuidMap.GetReplidFromGuid(context, id.Guid);
			if (!id.IsReplidKnown || id.Replid != replidFromGuid)
			{
				if (!ExchangeId.IsGlobCntValid(id.Counter))
				{
					throw new CorruptDataException((LID)62032U, string.Format("Corrupt Global Counter {0:X}.", id.Counter));
				}
				if (!ReplidGuidMap.IsReplidValid(replidFromGuid))
				{
					throw new CorruptDataException((LID)51008U, string.Format("Corrupt Replid {0:X}.", replidFromGuid));
				}
				if (Guid.Empty == id.Guid)
				{
					throw new CorruptDataException((LID)34624U, "Corrupt Guid (zeros).");
				}
				id = ExchangeId.Create(id.Guid, id.Counter, replidFromGuid);
			}
			return id;
		}

		// Token: 0x04000289 RID: 649
		public ExchangeId FidRoot = ExchangeId.Zero;

		// Token: 0x0400028A RID: 650
		public ExchangeId FidIPMsubtree = ExchangeId.Zero;

		// Token: 0x0400028B RID: 651
		public ExchangeId FidDAF = ExchangeId.Zero;

		// Token: 0x0400028C RID: 652
		public ExchangeId FidSpoolerQ = ExchangeId.Zero;

		// Token: 0x0400028D RID: 653
		public ExchangeId FidInbox = ExchangeId.Zero;

		// Token: 0x0400028E RID: 654
		public ExchangeId FidOutbox = ExchangeId.Zero;

		// Token: 0x0400028F RID: 655
		public ExchangeId FidSentmail = ExchangeId.Zero;

		// Token: 0x04000290 RID: 656
		public ExchangeId FidWastebasket = ExchangeId.Zero;

		// Token: 0x04000291 RID: 657
		public ExchangeId FidFinder = ExchangeId.Zero;

		// Token: 0x04000292 RID: 658
		public ExchangeId FidViews = ExchangeId.Zero;

		// Token: 0x04000293 RID: 659
		public ExchangeId FidCommonViews = ExchangeId.Zero;

		// Token: 0x04000294 RID: 660
		public ExchangeId FidSchedule = ExchangeId.Zero;

		// Token: 0x04000295 RID: 661
		public ExchangeId FidShortcuts = ExchangeId.Zero;

		// Token: 0x04000296 RID: 662
		public ExchangeId FidConversations = ExchangeId.Zero;

		// Token: 0x04000297 RID: 663
		public ExchangeId FidPublicFolderIpmSubTree = ExchangeId.Zero;

		// Token: 0x04000298 RID: 664
		public ExchangeId FidPublicFolderNonIpmSubTree = ExchangeId.Zero;

		// Token: 0x04000299 RID: 665
		public ExchangeId FidPublicFolderEFormsRegistry = ExchangeId.Zero;

		// Token: 0x0400029A RID: 666
		public ExchangeId FidPublicFolderDumpsterRoot = ExchangeId.Zero;

		// Token: 0x0400029B RID: 667
		public ExchangeId FidPublicFolderTombstonesRoot = ExchangeId.Zero;

		// Token: 0x0400029C RID: 668
		public ExchangeId FidPublicFolderAsyncDeleteState = ExchangeId.Zero;

		// Token: 0x0400029D RID: 669
		public ExchangeId FidPublicFolderInternalSubmission = ExchangeId.Zero;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008E2 RID: 2274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OptimizationInfo : IConversationStatistics
	{
		// Token: 0x06005531 RID: 21809 RVA: 0x00161B3C File Offset: 0x0015FD3C
		public OptimizationInfo(ConversationId conversationId, int totalNodeCount)
		{
			this.ConversationId = conversationId;
			this.TotalNodeCount = totalNodeCount;
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06005532 RID: 21810 RVA: 0x00161B5D File Offset: 0x0015FD5D
		// (set) Token: 0x06005533 RID: 21811 RVA: 0x00161B65 File Offset: 0x0015FD65
		public ConversationId ConversationId { get; private set; }

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06005534 RID: 21812 RVA: 0x00161B6E File Offset: 0x0015FD6E
		// (set) Token: 0x06005535 RID: 21813 RVA: 0x00161B76 File Offset: 0x0015FD76
		public int TotalNodeCount { get; private set; }

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x00161B7F File Offset: 0x0015FD7F
		// (set) Token: 0x06005537 RID: 21815 RVA: 0x00161B87 File Offset: 0x0015FD87
		public int BodyTagMatchingAttemptsCount { get; private set; }

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06005538 RID: 21816 RVA: 0x00161B90 File Offset: 0x0015FD90
		// (set) Token: 0x06005539 RID: 21817 RVA: 0x00161B98 File Offset: 0x0015FD98
		public int BodyTagMatchingIssuesCount { get; private set; }

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x00161BA1 File Offset: 0x0015FDA1
		public int LeafNodeCount
		{
			get
			{
				return this.leafNodes;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x00161BA9 File Offset: 0x0015FDA9
		public int ItemsExtracted
		{
			get
			{
				return this.itemsExtracted;
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x00161BB1 File Offset: 0x0015FDB1
		public int ItemsOpened
		{
			get
			{
				return this.itemsOpened;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x0600553D RID: 21821 RVA: 0x00161BB9 File Offset: 0x0015FDB9
		public int SummariesConstructed
		{
			get
			{
				return this.summaryConstructed;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x00161BC1 File Offset: 0x0015FDC1
		public int BodyTagNotPresentCount
		{
			get
			{
				return this.bodyTagNotPresentCount;
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x0600553F RID: 21823 RVA: 0x00161BC9 File Offset: 0x0015FDC9
		public int BodyTagMismatchedCount
		{
			get
			{
				return this.bodyTagMismatchedCount;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x00161BD1 File Offset: 0x0015FDD1
		public int BodyFormatMismatchedCount
		{
			get
			{
				return this.bodyFormatMismatchedCount;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x00161BD9 File Offset: 0x0015FDD9
		public int NonMSHeaderCount
		{
			get
			{
				return this.nonMSHeaderCount;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x00161BE1 File Offset: 0x0015FDE1
		public int ExtraPropertiesNeededCount
		{
			get
			{
				return this.extraPropertiesNeededCount;
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x00161BE9 File Offset: 0x0015FDE9
		public int ParticipantNotFoundCount
		{
			get
			{
				return this.participantNotFoundCount;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06005544 RID: 21828 RVA: 0x00161BF1 File Offset: 0x0015FDF1
		public int AttachmentPresentCount
		{
			get
			{
				return this.attachmentPresentCount;
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06005545 RID: 21829 RVA: 0x00161BF9 File Offset: 0x0015FDF9
		public int MapiAttachmentPresentCount
		{
			get
			{
				return this.mapiAttachmentPresentCount;
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06005546 RID: 21830 RVA: 0x00161C01 File Offset: 0x0015FE01
		public int PossibleInlinesCount
		{
			get
			{
				return this.possibleInlinesCount;
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06005547 RID: 21831 RVA: 0x00161C09 File Offset: 0x0015FE09
		public int IrmProtectedCount
		{
			get
			{
				return this.irmProtectedCount;
			}
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00161C14 File Offset: 0x0015FE14
		internal void UpdateItemIsLeafNode(StoreObjectId storeId)
		{
			ItemOptimizationStatus itemOptimizationStatus = this.GetOptimizationInfo(storeId);
			if ((itemOptimizationStatus & ItemOptimizationStatus.LeafNode) == ItemOptimizationStatus.LeafNode)
			{
				return;
			}
			itemOptimizationStatus |= ItemOptimizationStatus.LeafNode;
			this.optimizationStatus[storeId] = itemOptimizationStatus;
			this.leafNodes++;
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x00161C50 File Offset: 0x0015FE50
		internal void UpdateItemExtracted(StoreObjectId storeId)
		{
			ItemOptimizationStatus itemOptimizationStatus = this.GetOptimizationInfo(storeId);
			if ((itemOptimizationStatus & ItemOptimizationStatus.Opened) != ItemOptimizationStatus.None)
			{
				throw new InvalidOperationException("Can't extract already opened item");
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.IrmProtected) == ItemOptimizationStatus.IrmProtected)
			{
				throw new InvalidOperationException("Can't extract from an IRM message");
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.BodyTagNotPresent) == ItemOptimizationStatus.BodyTagNotPresent)
			{
				itemOptimizationStatus &= ~ItemOptimizationStatus.BodyTagNotPresent;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.AttachmentPresnet) == ItemOptimizationStatus.AttachmentPresnet)
			{
				throw new InvalidOperationException("Can't extract item that has Attachments");
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.MapiAttachmentPresent) == ItemOptimizationStatus.MapiAttachmentPresent)
			{
				throw new InvalidOperationException("Can't extract item that has MAPI Attachments");
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.PossibleInlines) == ItemOptimizationStatus.PossibleInlines)
			{
				throw new InvalidOperationException("Can't extract item that has inlines");
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.NonMsHeader) == ItemOptimizationStatus.NonMsHeader)
			{
				this.nonMSHeaderCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.NonMsHeader;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.BodyTagMismatched) == ItemOptimizationStatus.BodyTagMismatched)
			{
				this.bodyTagMismatchedCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.BodyTagMismatched;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.BodyFormatMismatched) == ItemOptimizationStatus.BodyFormatMismatched)
			{
				this.bodyFormatMismatchedCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.BodyFormatMismatched;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.ParticipantNotFound) == ItemOptimizationStatus.ParticipantNotFound)
			{
				this.participantNotFoundCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.ParticipantNotFound;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.ExtraPropertiesNeeded) == ItemOptimizationStatus.ExtraPropertiesNeeded)
			{
				this.extraPropertiesNeededCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.ExtraPropertiesNeeded;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				return;
			}
			this.itemsExtracted++;
			this.optimizationStatus[storeId] = (itemOptimizationStatus | ItemOptimizationStatus.Extracted);
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00161DAC File Offset: 0x0015FFAC
		internal void UpdateItemOpened(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			ItemOptimizationStatus itemOptimizationStatus = optimizationInfo & ItemOptimizationStatus.Extracted;
			if ((optimizationInfo & ItemOptimizationStatus.Opened) == ItemOptimizationStatus.Opened)
			{
				return;
			}
			this.itemsOpened++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.Opened);
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x00161DEC File Offset: 0x0015FFEC
		internal void UpdateItemSummaryConstructed(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.SummaryConstructed) == ItemOptimizationStatus.SummaryConstructed)
			{
				return;
			}
			this.summaryConstructed++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.SummaryConstructed);
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00161E28 File Offset: 0x00160028
		internal void UpdateItemBodyTagNotPresent(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				throw new InvalidOperationException("Item already loaded");
			}
			if ((optimizationInfo & ItemOptimizationStatus.BodyTagNotPresent) == ItemOptimizationStatus.BodyTagNotPresent)
			{
				return;
			}
			this.bodyTagNotPresentCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.BodyTagNotPresent);
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x00161E78 File Offset: 0x00160078
		internal void UpdateItemMayHaveInline(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				throw new InvalidOperationException("Extrated message may not have parent and inlines");
			}
			if ((optimizationInfo & ItemOptimizationStatus.PossibleInlines) == ItemOptimizationStatus.PossibleInlines)
			{
				return;
			}
			this.possibleInlinesCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.PossibleInlines);
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x00161ED0 File Offset: 0x001600D0
		internal void UpdateItemBodyTagMismatched(StoreObjectId storeId)
		{
			ItemOptimizationStatus itemOptimizationStatus = this.GetOptimizationInfo(storeId);
			if ((itemOptimizationStatus & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				return;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.BodyTagMismatched) == ItemOptimizationStatus.BodyTagMismatched)
			{
				return;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.NonMsHeader) == ItemOptimizationStatus.NonMsHeader)
			{
				this.nonMSHeaderCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.NonMsHeader;
			}
			if ((itemOptimizationStatus & ItemOptimizationStatus.BodyFormatMismatched) == ItemOptimizationStatus.BodyFormatMismatched)
			{
				this.bodyFormatMismatchedCount--;
				itemOptimizationStatus &= ~ItemOptimizationStatus.BodyFormatMismatched;
			}
			this.bodyTagMismatchedCount++;
			this.optimizationStatus[storeId] = (itemOptimizationStatus | ItemOptimizationStatus.BodyTagMismatched);
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x00161F54 File Offset: 0x00160154
		internal void UpdateItemBodyFormatMismatched(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				return;
			}
			if ((optimizationInfo & ItemOptimizationStatus.BodyFormatMismatched) == ItemOptimizationStatus.BodyFormatMismatched)
			{
				return;
			}
			if ((optimizationInfo & ItemOptimizationStatus.BodyTagMismatched) == ItemOptimizationStatus.BodyTagMismatched)
			{
				return;
			}
			this.bodyFormatMismatchedCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.BodyFormatMismatched);
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x00161FA0 File Offset: 0x001601A0
		internal void UpdateItemExtraPropertiesNeeded(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				throw new InvalidOperationException("Item already loaded");
			}
			if ((optimizationInfo & ItemOptimizationStatus.ExtraPropertiesNeeded) == ItemOptimizationStatus.ExtraPropertiesNeeded)
			{
				return;
			}
			this.extraPropertiesNeededCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.ExtraPropertiesNeeded);
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x00161FF8 File Offset: 0x001601F8
		internal void UpdateItemParticipantNotFound(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.ParticipantNotFound) == ItemOptimizationStatus.ParticipantNotFound)
			{
				return;
			}
			this.participantNotFoundCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.ParticipantNotFound);
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x00162040 File Offset: 0x00160240
		internal void UpdateItemAttachmentPresent(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				throw new InvalidOperationException("Item already loaded");
			}
			if ((optimizationInfo & ItemOptimizationStatus.AttachmentPresnet) == ItemOptimizationStatus.AttachmentPresnet)
			{
				return;
			}
			this.attachmentPresentCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.AttachmentPresnet);
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x00162098 File Offset: 0x00160298
		internal void UpdateItemMapiAttachmentPresent(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				throw new InvalidOperationException("Item already loaded");
			}
			if ((optimizationInfo & ItemOptimizationStatus.MapiAttachmentPresent) == ItemOptimizationStatus.MapiAttachmentPresent)
			{
				return;
			}
			this.mapiAttachmentPresentCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.MapiAttachmentPresent);
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x001620F0 File Offset: 0x001602F0
		internal void UpdateItemIrmProtected(StoreObjectId storeId)
		{
			ItemOptimizationStatus optimizationInfo = this.GetOptimizationInfo(storeId);
			if ((optimizationInfo & ItemOptimizationStatus.Extracted) == ItemOptimizationStatus.Extracted)
			{
				throw new InvalidOperationException("Item already loaded");
			}
			if ((optimizationInfo & ItemOptimizationStatus.IrmProtected) == ItemOptimizationStatus.IrmProtected)
			{
				return;
			}
			this.irmProtectedCount++;
			this.optimizationStatus[storeId] = (optimizationInfo | ItemOptimizationStatus.IrmProtected);
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x00162148 File Offset: 0x00160348
		internal ItemOptimizationStatus GetOptimizationInfo(StoreObjectId storeId)
		{
			ItemOptimizationStatus result;
			if (this.optimizationStatus.TryGetValue(storeId, out result))
			{
				return result;
			}
			return ItemOptimizationStatus.None;
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x00162168 File Offset: 0x00160368
		internal void IncrementBodyTagMatchingAttempts()
		{
			this.BodyTagMatchingAttemptsCount++;
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x00162178 File Offset: 0x00160378
		internal void IncrementBodyTagMatchingIssues()
		{
			this.BodyTagMatchingIssuesCount++;
		}

		// Token: 0x04002DC6 RID: 11718
		private int leafNodes;

		// Token: 0x04002DC7 RID: 11719
		private int itemsExtracted;

		// Token: 0x04002DC8 RID: 11720
		private int itemsOpened;

		// Token: 0x04002DC9 RID: 11721
		private int summaryConstructed;

		// Token: 0x04002DCA RID: 11722
		private int bodyTagNotPresentCount;

		// Token: 0x04002DCB RID: 11723
		private int bodyTagMismatchedCount;

		// Token: 0x04002DCC RID: 11724
		private int bodyFormatMismatchedCount;

		// Token: 0x04002DCD RID: 11725
		private int nonMSHeaderCount;

		// Token: 0x04002DCE RID: 11726
		private int extraPropertiesNeededCount;

		// Token: 0x04002DCF RID: 11727
		private int participantNotFoundCount;

		// Token: 0x04002DD0 RID: 11728
		private int attachmentPresentCount;

		// Token: 0x04002DD1 RID: 11729
		private int irmProtectedCount;

		// Token: 0x04002DD2 RID: 11730
		private int possibleInlinesCount;

		// Token: 0x04002DD3 RID: 11731
		private int mapiAttachmentPresentCount;

		// Token: 0x04002DD4 RID: 11732
		private Dictionary<StoreObjectId, ItemOptimizationStatus> optimizationStatus = new Dictionary<StoreObjectId, ItemOptimizationStatus>();
	}
}

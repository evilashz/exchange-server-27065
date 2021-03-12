using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200005A RID: 90
	internal class DeleteMessagesOperation : MessageListBulkOperation
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0001095F File Offset: 0x0000EB5F
		public DeleteMessagesOperation(MapiFolder folder, IList<ExchangeId> messageIds, bool sendNRN, int chunkSize) : base(folder, messageIds, chunkSize)
		{
			this.sendNRN = sendNRN;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00010972 File Offset: 0x0000EB72
		public DeleteMessagesOperation(MapiFolder folder, IList<ExchangeId> messageIds, bool sendNRN) : this(folder, messageIds, sendNRN, 100)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0001097F File Offset: 0x0000EB7F
		protected override bool ProcessStart(MapiContext context, out int progressCount, ref ErrorCode error)
		{
			progressCount = 0;
			if (base.MessageIds == null || base.MessageIds.Count == 0)
			{
				base.Folder.StoreFolder.InvalidateIndexes(context, true, false);
			}
			return true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000109AD File Offset: 0x0000EBAD
		protected override void ProcessEnd(MapiContext context, bool incomplete, ErrorCode error)
		{
			if (this.CheckSourceFolder(context))
			{
				BulkOperation.InvalidateFolderIndicesIfNeeded(context, base.Folder.StoreFolder);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000109CC File Offset: 0x0000EBCC
		protected override bool ProcessMessages(MapiContext context, MapiFolder folder, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			return BulkOperation.DeleteMessages(context, folder, this.sendNRN, false, midsToProcess, BulkErrorAction.Incomplete, BulkErrorAction.Error, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000109F0 File Offset: 0x0000EBF0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeleteMessagesOperation>(this);
		}

		// Token: 0x04000192 RID: 402
		private bool sendNRN;
	}
}

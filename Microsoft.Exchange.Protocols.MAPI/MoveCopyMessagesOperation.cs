using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200005B RID: 91
	internal class MoveCopyMessagesOperation : MessageListBulkOperation
	{
		// Token: 0x0600024B RID: 587 RVA: 0x000109F8 File Offset: 0x0000EBF8
		public MoveCopyMessagesOperation(bool copy, MapiFolder sourceFolder, MapiFolder destinationFolder, IList<ExchangeId> messageIds, Properties propsToSet, IList<ExchangeId> outputMids, IList<ExchangeId> outputCns, int chunkSize) : base(sourceFolder, messageIds, chunkSize)
		{
			this.copy = copy;
			this.destinationFolder = destinationFolder;
			this.propsToSet = propsToSet;
			this.outputMids = outputMids;
			this.outputCns = outputCns;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00010A2C File Offset: 0x0000EC2C
		public MoveCopyMessagesOperation(bool copy, MapiFolder sourceFolder, MapiFolder destinationFolder, IList<ExchangeId> messageIds, Properties propsToSet, IList<ExchangeId> outputMids, IList<ExchangeId> outputCns) : this(copy, sourceFolder, destinationFolder, messageIds, propsToSet, outputMids, outputCns, 100)
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00010A4C File Offset: 0x0000EC4C
		internal static IDisposable SetIdleIndexTimeForEmptyFolderOperation(TimeSpan idleTime)
		{
			return MoveCopyMessagesOperation.idleIndexTimeForEmptyFolderOperation.SetTestHook(idleTime);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00010A59 File Offset: 0x0000EC59
		protected bool CheckDestinationFolder(MapiContext context)
		{
			return this.destinationFolder.CheckAlive(context);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00010A68 File Offset: 0x0000EC68
		protected override bool ProcessStart(MapiContext context, out int progressCount, ref ErrorCode error)
		{
			progressCount = 0;
			if (!this.CheckDestinationFolder(context))
			{
				error = ErrorCode.CreateObjectDeleted((LID)53784U);
				return false;
			}
			if (this.destinationFolder.IsSearchFolder())
			{
				progressCount = 0;
				error = ErrorCode.CreateSearchFolder((LID)41496U);
				return false;
			}
			if (!this.copy)
			{
				if (base.MessageIds == null || base.MessageIds.Count == 0)
				{
					base.Folder.StoreFolder.InvalidateIndexes(context, true, false);
				}
				else
				{
					bool flag = context.Diagnostics.ClientActionString != null && context.Diagnostics.ClientActionString.Contains("EmptyFolder");
					if (flag)
					{
						DateTime lastReferenceDateThreshold = base.Folder.Logon.StoreMailbox.UtcNow - MoveCopyMessagesOperation.idleIndexTimeForEmptyFolderOperation.Value;
						base.Folder.StoreFolder.InvalidateIndexes(context, true, false, lastReferenceDateThreshold);
					}
				}
			}
			return true;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00010B57 File Offset: 0x0000ED57
		protected override void ProcessEnd(MapiContext context, bool incomplete, ErrorCode error)
		{
			if (!this.copy && this.CheckSourceFolder(context))
			{
				BulkOperation.InvalidateFolderIndicesIfNeeded(context, base.Folder.StoreFolder);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00010B7C File Offset: 0x0000ED7C
		protected override bool ProcessMessages(MapiContext context, MapiFolder folder, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			if (!this.CheckDestinationFolder(context))
			{
				progressCount = 0;
				error = ErrorCode.CreateObjectDeleted((LID)57880U);
				return false;
			}
			BulkErrorAction softErrorAction = (midsToProcess.Count == 1) ? BulkErrorAction.Error : BulkErrorAction.Incomplete;
			if (this.copy)
			{
				return BulkOperation.CopyMessages(context, folder, this.destinationFolder, midsToProcess, this.propsToSet, BulkErrorAction.Incomplete, softErrorAction, this.outputMids, this.outputCns, out progressCount, ref incomplete, ref error);
			}
			return BulkOperation.MoveMessages(context, folder, this.destinationFolder, midsToProcess, this.propsToSet, BulkErrorAction.Incomplete, softErrorAction, this.outputMids, this.outputCns, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00010C15 File Offset: 0x0000EE15
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MoveCopyMessagesOperation>(this);
		}

		// Token: 0x04000193 RID: 403
		private static Hookable<TimeSpan> idleIndexTimeForEmptyFolderOperation = Hookable<TimeSpan>.Create(false, DefaultSettings.Get.IdleIndexTimeForEmptyFolderOperation);

		// Token: 0x04000194 RID: 404
		private bool copy;

		// Token: 0x04000195 RID: 405
		private MapiFolder destinationFolder;

		// Token: 0x04000196 RID: 406
		private Properties propsToSet;

		// Token: 0x04000197 RID: 407
		private IList<ExchangeId> outputMids;

		// Token: 0x04000198 RID: 408
		private IList<ExchangeId> outputCns;
	}
}

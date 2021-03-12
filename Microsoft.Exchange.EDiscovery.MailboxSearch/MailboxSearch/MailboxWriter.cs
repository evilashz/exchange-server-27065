using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000014 RID: 20
	internal class MailboxWriter : IContextualBatchDataWriter<List<ItemInformation>>, IBatchDataWriter<List<ItemInformation>>, IDisposable
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000876C File Offset: 0x0000696C
		public MailboxWriter(IExportContext exportContext, ITargetMailbox targetMailbox, IProgressController progressController) : this(exportContext, targetMailbox, progressController, null)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008778 File Offset: 0x00006978
		public MailboxWriter(IExportContext exportContext, ITargetMailbox targetMailbox, IProgressController progressController, TargetFolderProvider<string, BaseFolderType, ITargetMailbox> targetFolderProvider)
		{
			Util.ThrowIfNull(exportContext, "exportContext");
			Util.ThrowIfNull(targetMailbox, "targetMailbox");
			Util.ThrowIfNull(progressController, "progressController");
			this.includeDuplicates = exportContext.ExportMetadata.IncludeDuplicates;
			this.targetMailbox = targetMailbox;
			this.progressController = progressController;
			this.timer = new Stopwatch();
			this.targetFolderProvider = targetFolderProvider;
			if (this.targetFolderProvider == null)
			{
				this.targetFolderProvider = new MailboxTargetFolderProvider(exportContext, this.targetMailbox);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000087F8 File Offset: 0x000069F8
		public void EnterDataContext(DataContext dataContext)
		{
			this.dataContext = dataContext;
			this.timer.Restart();
			this.targetFolderProvider.Reset(this.dataContext);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000881D File Offset: 0x00006A1D
		public void ExitDataContext(bool errorHappened)
		{
			if (this.dataContext != null)
			{
				this.dataContext = null;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000882E File Offset: 0x00006A2E
		public void ExitPFDataContext(bool errorHappened)
		{
			if (this.dataContext != null)
			{
				this.dataContext = null;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008840 File Offset: 0x00006A40
		public void WriteDataBatch(List<ItemInformation> dataBatch)
		{
			List<ItemInformation> list = new List<ItemInformation>(Math.Min(dataBatch.Count, Constants.ReadWriteBatchSize));
			string parentFolderId = null;
			string text = string.Empty;
			ProgressRecord progressRecord = new ProgressRecord(this.dataContext);
			try
			{
				foreach (ItemInformation itemInformation in dataBatch)
				{
					if (this.progressController.IsStopRequested)
					{
						break;
					}
					BaseFolderType parentFolder = this.targetFolderProvider.GetParentFolder(this.targetMailbox, itemInformation.Id.ParentFolder, this.includeDuplicates);
					if (parentFolder == null)
					{
						progressRecord.ReportItemError(itemInformation.Id, null, ExportErrorType.ParentFolderNotFound, string.Format("EDiscoveryError:E007:: Parent folder for this item is not found.", new object[0]));
					}
					else
					{
						parentFolderId = parentFolder.FolderId.Id;
						if (text == string.Empty)
						{
							text = parentFolder.FolderId.Id;
						}
						if (itemInformation.Error == null)
						{
							if (itemInformation.Id.IsDuplicate)
							{
								progressRecord.ReportItemExported(itemInformation.Id, null, null);
							}
							else
							{
								if (text != parentFolder.FolderId.Id || list.Count == Constants.ReadWriteBatchSize)
								{
									this.CopyItemsToTargetMailbox(text, list, progressRecord);
									text = parentFolder.FolderId.Id;
									list.Clear();
								}
								list.Add(itemInformation);
							}
						}
						else
						{
							progressRecord.ReportItemError(itemInformation.Id, null, itemInformation.Error.ErrorType, itemInformation.Error.Message);
						}
					}
				}
				if (list.Count > 0)
				{
					this.CopyItemsToTargetMailbox(parentFolderId, list, progressRecord);
					list.Clear();
				}
			}
			finally
			{
				this.timer.Stop();
				if (progressRecord != null)
				{
					progressRecord.ReportDuration(this.timer.Elapsed);
					this.progressController.ReportProgress(progressRecord);
				}
				this.timer.Restart();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008A4C File Offset: 0x00006C4C
		public void Dispose()
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008A50 File Offset: 0x00006C50
		private void CopyItemsToTargetMailbox(string parentFolderId, IList<ItemInformation> items, ProgressRecord progressRecord)
		{
			List<ItemInformation> list = this.targetMailbox.CopyItems(parentFolderId, items);
			if (list != null && list.Count > 0)
			{
				foreach (ItemInformation itemInformation in list)
				{
					if (itemInformation.Error != null)
					{
						progressRecord.ReportItemError(itemInformation.Id, null, itemInformation.Error.ErrorType, itemInformation.Error.Message);
					}
					else
					{
						progressRecord.ReportItemExported(itemInformation.Id, itemInformation.Id.Id, null);
					}
				}
			}
		}

		// Token: 0x04000087 RID: 135
		private readonly Stopwatch timer;

		// Token: 0x04000088 RID: 136
		private readonly TargetFolderProvider<string, BaseFolderType, ITargetMailbox> targetFolderProvider;

		// Token: 0x04000089 RID: 137
		private readonly ITargetMailbox targetMailbox;

		// Token: 0x0400008A RID: 138
		private readonly IProgressController progressController;

		// Token: 0x0400008B RID: 139
		private readonly bool includeDuplicates;

		// Token: 0x0400008C RID: 140
		private DataContext dataContext;
	}
}

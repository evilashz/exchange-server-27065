using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000033 RID: 51
	internal class DataRetriever : IBatchDataReader<List<ItemInformation>>
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00005F2C File Offset: 0x0000412C
		public DataRetriever(DataContext dataContext, IProgressController progressController)
		{
			this.dataContext = dataContext;
			this.processedItemCount = this.dataContext.ProcessedItemCount;
			this.currentBatch = new List<ItemId>(ConstantProvider.ExportBatchItemCountLimit);
			this.currentBatchSize = 0U;
			this.currentBatchForDuplicates = new List<ItemId>(ConstantProvider.ExportBatchItemCountLimit);
			this.progressController = progressController;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600019F RID: 415 RVA: 0x00005F88 File Offset: 0x00004188
		// (remove) Token: 0x060001A0 RID: 416 RVA: 0x00005FC0 File Offset: 0x000041C0
		public event EventHandler<DataBatchEventArgs<List<ItemInformation>>> DataBatchRead;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001A1 RID: 417 RVA: 0x00005FF8 File Offset: 0x000041F8
		// (remove) Token: 0x060001A2 RID: 418 RVA: 0x00006030 File Offset: 0x00004230
		public event EventHandler AbortingOnError;

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006065 File Offset: 0x00004265
		public DataContext DataContext
		{
			get
			{
				return this.dataContext;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000606D File Offset: 0x0000426D
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00006075 File Offset: 0x00004275
		public IContextualBatchDataWriter<List<ItemInformation>> DataWriter { get; set; }

		// Token: 0x060001A6 RID: 422 RVA: 0x00006080 File Offset: 0x00004280
		public void StartReading()
		{
			try
			{
				int num = 0;
				int num2 = this.dataContext.ProcessedItemCount;
				foreach (ItemId itemId in this.dataContext.ItemIdList.ReadItemIds())
				{
					if (this.progressController.IsStopRequested)
					{
						Tracer.TraceInformation("DataRetriever.StartReading: Stop requested", new object[0]);
						return;
					}
					if (num < num2)
					{
						num++;
					}
					else
					{
						this.ProcessItemIdLegacy(itemId);
					}
				}
				this.ProcessBatchDataLegacy();
			}
			catch (ExportException ex)
			{
				Tracer.TraceError("DataRetriever.StartReading: Exception happend: {0}", new object[]
				{
					ex
				});
				this.StopOnError(new ErrorRecord
				{
					ErrorType = ex.ErrorType,
					DiagnosticMessage = ex.Message,
					Time = DateTime.UtcNow,
					Item = null,
					SourceId = this.dataContext.SourceId
				});
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006198 File Offset: 0x00004398
		public void ProcessItems(ref List<ItemId> itemList)
		{
			try
			{
				int num = 0;
				int num2 = 0;
				foreach (ItemId itemId in itemList)
				{
					if (this.progressController.IsStopRequested)
					{
						Tracer.TraceInformation("DataRetriever.ProcessItems: Stop requested", new object[0]);
						return;
					}
					if (num < this.processedItemCount)
					{
						num++;
					}
					else
					{
						this.ProcessItemId(itemId);
					}
					num2++;
					if ((ulong)this.currentBatchSize >= (ulong)((long)ConstantProvider.ExportBatchSizeLimit) || this.currentBatch.Count + this.currentBatchForDuplicates.Count >= ConstantProvider.ExportBatchItemCountLimit)
					{
						break;
					}
				}
				if (num2 == itemList.Count)
				{
					itemList.Clear();
				}
				else if (itemList.Count > 0 && itemList.Count > num2)
				{
					itemList.RemoveRange(0, num2);
				}
				else
				{
					Tracer.TraceError("DataRetriever.ProcessItems: error: tempCount > itemList.Count, this shouldn't happen tempCount: {0}; itemList.Count: {1}", new object[]
					{
						num2,
						itemList.Count
					});
				}
			}
			catch (ExportException ex)
			{
				Tracer.TraceError("DataRetriever.ProcessItems: Exception happend: {0}", new object[]
				{
					ex
				});
				this.StopOnError(new ErrorRecord
				{
					ErrorType = ex.ErrorType,
					DiagnosticMessage = ex.Message,
					Time = DateTime.UtcNow,
					Item = null,
					SourceId = this.dataContext.SourceId
				});
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006344 File Offset: 0x00004544
		public List<ItemInformation> ProcessBatchData()
		{
			Tracer.TraceInformation("DataRetriever.ProcessBatchData: Processing batch data. currentBatch.Count={0}, currentBatchForDuplicates.Count={1}", new object[]
			{
				this.currentBatch.Count,
				this.currentBatchForDuplicates.Count
			});
			List<ItemInformation> list = null;
			if (this.currentBatch.Count > 0)
			{
				list = this.dataContext.ServiceClient.ExportItems(this.dataContext.IsPublicFolder ? this.dataContext.SourceLegacyExchangeDN : this.dataContext.SourceId, this.currentBatch, this.progressController.IsDocumentIdHintFlightingEnabled);
				this.currentBatch.Clear();
				this.currentBatchSize = 0U;
			}
			Tracer.TraceInformation("DataRetriever.ProcessBatchData: batch data exported.", new object[0]);
			if (this.currentBatchForDuplicates.Count > 0)
			{
				if (list == null)
				{
					list = new List<ItemInformation>(this.currentBatchForDuplicates.Count);
				}
				list.AddRange(from itemId in this.currentBatchForDuplicates
				select new ItemInformation
				{
					Id = itemId
				});
				this.currentBatchForDuplicates.Clear();
			}
			return list;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006460 File Offset: 0x00004660
		public void OnDataBatchRead(List<ItemInformation> items)
		{
			EventHandler<DataBatchEventArgs<List<ItemInformation>>> dataBatchRead = this.DataBatchRead;
			if (dataBatchRead != null)
			{
				dataBatchRead(this, new DataBatchEventArgs<List<ItemInformation>>
				{
					DataBatch = items
				});
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000648C File Offset: 0x0000468C
		private void ProcessItemIdLegacy(ItemId itemId)
		{
			if (itemId.IsDuplicate)
			{
				this.currentBatchForDuplicates.Add(itemId);
			}
			else
			{
				if (this.currentBatch.Count > 0 && !string.Equals(this.dataContext.ServiceClient.GetPhysicalPartitionIdentifier(this.currentBatch[0]), this.dataContext.ServiceClient.GetPhysicalPartitionIdentifier(itemId), StringComparison.OrdinalIgnoreCase))
				{
					this.ProcessBatchDataLegacy();
				}
				this.currentBatch.Add(itemId);
				this.currentBatchSize += itemId.Size;
			}
			if ((ulong)this.currentBatchSize >= (ulong)((long)ConstantProvider.ExportBatchSizeLimit) || this.currentBatch.Count + this.currentBatchForDuplicates.Count >= ConstantProvider.ExportBatchItemCountLimit)
			{
				this.ProcessBatchDataLegacy();
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000654A File Offset: 0x0000474A
		private void ProcessItemId(ItemId itemId)
		{
			if (itemId.IsDuplicate)
			{
				this.currentBatchForDuplicates.Add(itemId);
				return;
			}
			this.currentBatch.Add(itemId);
			this.currentBatchSize += itemId.Size;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000659C File Offset: 0x0000479C
		private void ProcessBatchDataLegacy()
		{
			Tracer.TraceInformation("DataRetriever.ProcessBatchData2: Processing batch data. currentBatch.Count={0}, currentBatchForDuplicates.Count={1}", new object[]
			{
				this.currentBatch.Count,
				this.currentBatchForDuplicates.Count
			});
			List<ItemInformation> list = null;
			if (this.currentBatch.Count > 0)
			{
				list = this.dataContext.ServiceClient.ExportItems(this.dataContext.IsPublicFolder ? this.dataContext.SourceLegacyExchangeDN : this.dataContext.SourceId, this.currentBatch, this.progressController.IsDocumentIdHintFlightingEnabled);
				this.currentBatch.Clear();
				this.currentBatchSize = 0U;
			}
			Tracer.TraceInformation("DataRetriever.ProcessBatchData: batch data exported.", new object[0]);
			if (this.currentBatchForDuplicates.Count > 0)
			{
				if (list == null)
				{
					list = new List<ItemInformation>(this.currentBatchForDuplicates.Count);
				}
				list.AddRange(from itemId in this.currentBatchForDuplicates
				select new ItemInformation
				{
					Id = itemId
				});
				this.currentBatchForDuplicates.Clear();
			}
			if (list != null && list.Count > 0)
			{
				this.OnDataBatchRead(list);
				Tracer.TraceInformation("DataRetriever.ProcessBatchData: OnDataBatchRead triggered.", new object[0]);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000066D8 File Offset: 0x000048D8
		private void StopOnError(ErrorRecord error)
		{
			EventHandler abortingOnError = this.AbortingOnError;
			if (abortingOnError != null)
			{
				abortingOnError(this, EventArgs.Empty);
			}
			this.currentBatch.Clear();
			this.currentBatchForDuplicates.Clear();
			ProgressRecord progressRecord = new ProgressRecord();
			progressRecord.ReportSourceError(error);
			this.progressController.ReportProgress(progressRecord);
		}

		// Token: 0x040000A8 RID: 168
		private readonly DataContext dataContext;

		// Token: 0x040000A9 RID: 169
		private readonly List<ItemId> currentBatch;

		// Token: 0x040000AA RID: 170
		private readonly List<ItemId> currentBatchForDuplicates;

		// Token: 0x040000AB RID: 171
		private readonly IProgressController progressController;

		// Token: 0x040000AC RID: 172
		private readonly int processedItemCount;

		// Token: 0x040000AD RID: 173
		private uint currentBatchSize;
	}
}

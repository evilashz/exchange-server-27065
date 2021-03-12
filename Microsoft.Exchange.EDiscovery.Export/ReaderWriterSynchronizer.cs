using System;
using System.Threading.Tasks;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200001A RID: 26
	internal class ReaderWriterSynchronizer<T>
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000040C0 File Offset: 0x000022C0
		public ReaderWriterSynchronizer(IBatchDataReader<T> batchDataReader, IBatchDataWriter<T> batchDataWriter)
		{
			this.writingTask = null;
			this.batchDataReader = batchDataReader;
			this.batchDataWriter = batchDataWriter;
			this.batchDataReader.DataBatchRead += this.WriteDataBatch;
			this.batchDataReader.AbortingOnError += this.ReaderAborting;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004118 File Offset: 0x00002318
		public void Execute()
		{
			ExportException ex = null;
			try
			{
				this.batchDataReader.StartReading();
			}
			finally
			{
				ex = AsynchronousTaskHandler.WaitForAsynchronousTask(this.writingTask);
				this.writingTask = null;
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000041B4 File Offset: 0x000023B4
		private void WriteDataBatch(object sender, DataBatchEventArgs<T> args)
		{
			ScenarioData scenarioData = ScenarioData.Current;
			ExportException ex = AsynchronousTaskHandler.WaitForAsynchronousTask(this.writingTask);
			this.writingTask = ((ex != null || args.DataBatch == null) ? null : Task.Factory.StartNew(delegate(object dataBatch)
			{
				ScenarioData scenarioData;
				using (new ScenarioData(scenarioData))
				{
					this.batchDataWriter.WriteDataBatch((T)((object)dataBatch));
				}
			}, args.DataBatch));
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004224 File Offset: 0x00002424
		private void ReaderAborting(object sender, EventArgs args)
		{
			AsynchronousTaskHandler.WaitForAsynchronousTask(this.writingTask);
			this.writingTask = null;
		}

		// Token: 0x04000065 RID: 101
		private readonly IBatchDataReader<T> batchDataReader;

		// Token: 0x04000066 RID: 102
		private readonly IBatchDataWriter<T> batchDataWriter;

		// Token: 0x04000067 RID: 103
		private Task writingTask;
	}
}

using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000177 RID: 375
	internal abstract class ConverterInput : IDisposable
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0007603C File Offset: 0x0007423C
		public bool EndOfFile
		{
			get
			{
				return this.endOfFile;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00076044 File Offset: 0x00074244
		public int MaxTokenSize
		{
			get
			{
				return this.maxTokenSize;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0007604C File Offset: 0x0007424C
		protected ConverterInput(IProgressMonitor progressMonitor)
		{
			this.progressMonitor = progressMonitor;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0007605B File Offset: 0x0007425B
		public virtual void SetRestartConsumer(IRestartable restartConsumer)
		{
		}

		// Token: 0x06001007 RID: 4103
		public abstract bool ReadMore(ref char[] buffer, ref int start, ref int current, ref int end);

		// Token: 0x06001008 RID: 4104
		public abstract void ReportProcessed(int processedSize);

		// Token: 0x06001009 RID: 4105
		public abstract int RemoveGap(int gapBegin, int gapEnd);

		// Token: 0x0600100A RID: 4106 RVA: 0x0007605D File Offset: 0x0007425D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0007606C File Offset: 0x0007426C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040010C6 RID: 4294
		protected bool endOfFile;

		// Token: 0x040010C7 RID: 4295
		protected int maxTokenSize;

		// Token: 0x040010C8 RID: 4296
		protected IProgressMonitor progressMonitor;
	}
}

using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001AA RID: 426
	public class LazyWriter : DisposeTrackableBase
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x00062440 File Offset: 0x00060640
		public LazyWriter(string path)
		{
			this.path = path;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000624C8 File Offset: 0x000606C8
		public void WriteLine(string line)
		{
			if (this.writer == null)
			{
				this.CreateStreamWriter();
			}
			GroupMetricsUtility.TryDiskOperation(delegate
			{
				this.writer.WriteLine(line);
			}, delegate(Exception exception)
			{
				GroupMetricsUtility.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToWriteFile, exception.GetType().FullName, new object[]
				{
					this.path,
					exception.GetType().FullName,
					exception.Message
				});
				throw new LazyWriterException(exception);
			});
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00062515 File Offset: 0x00060715
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LazyWriter>(this);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0006251D File Offset: 0x0006071D
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.writer != null)
			{
				this.writer.Dispose();
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000625F8 File Offset: 0x000607F8
		private void CreateStreamWriter()
		{
			GroupMetricsUtility.TryDiskOperation(delegate
			{
				string directoryName = Path.GetDirectoryName(this.path);
				Directory.CreateDirectory(directoryName);
				this.writer = new StreamWriter(this.path, false);
				GroupMetricsUtility.Tracer.TraceDebug<string>(0L, "LazyWriter.CreateStreamWriter succeeded: {0}", this.path);
			}, delegate(Exception exception)
			{
				GroupMetricsUtility.Tracer.TraceError<string>(0L, "LazyWriter.CreateStreamWriter failed: {0}", this.path);
				this.writer = null;
				GroupMetricsUtility.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToOpenFile, exception.GetType().FullName, new object[]
				{
					this.path,
					exception.GetType().FullName,
					exception.Message
				});
				throw new LazyWriterException(exception);
			});
		}

		// Token: 0x04000A8C RID: 2700
		private readonly string path;

		// Token: 0x04000A8D RID: 2701
		private StreamWriter writer;
	}
}

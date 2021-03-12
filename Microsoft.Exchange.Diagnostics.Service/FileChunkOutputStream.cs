using System;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000008 RID: 8
	public class FileChunkOutputStream : OutputStream, IMultipleOutputStream
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000039B8 File Offset: 0x00001BB8
		public FileChunkOutputStream(string name, string outputDirectory) : this(name, outputDirectory, FileChunkOutputStream.DefaultFields)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000039C8 File Offset: 0x00001BC8
		public FileChunkOutputStream(string name, string outputDirectory, string[] fields) : base(name)
		{
			this.logSchema = new LogSchema("Microsoft Exchange Server", "15.0.0.0", base.Name, fields);
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.logSchema, LogHeaderCsvOption.CsvStrict);
			this.exlog = new Log(base.Name, headerFormatter, base.Name);
			int configInt = Configuration.GetConfigInt("FileChunkOutputStreamMaxBufferSize", 0, 10485760, 2097152);
			TimeSpan configTimeSpan = Configuration.GetConfigTimeSpan("FileChunkOutputStreamFlushInterval", TimeSpan.FromSeconds(10.0), TimeSpan.FromMinutes(60.0), TimeSpan.FromMinutes(1.0));
			this.exlog.Configure(outputDirectory, TimeSpan.Zero, 5368709120L, 10485760L, configInt, configTimeSpan, true);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003A8C File Offset: 0x00001C8C
		public void WriteLine(string analyzerName, string outputFormat, string format, params object[] args)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[1] = analyzerName;
			logRowFormatter[2] = outputFormat;
			if (args != null && args.Length > 0)
			{
				logRowFormatter[3] = string.Format(format, args);
			}
			else
			{
				logRowFormatter[3] = format;
			}
			this.exlog.Append(logRowFormatter, 0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public void WriteRawLine(string format, params object[] args)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			if (args != null && args.Length > 0)
			{
				logRowFormatter[1] = string.Format(format, args);
			}
			else
			{
				string[] array = format.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					logRowFormatter[i + 1] = array[i];
				}
			}
			this.exlog.Append(logRowFormatter, 0);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003B55 File Offset: 0x00001D55
		public virtual OutputStream OpenOutputStream(string analyzerName, string outputFormatName, string streamName)
		{
			return new FileChunkOutputStream.WrappedFileChunkOutputStream(this, analyzerName, outputFormatName);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003B5F File Offset: 0x00001D5F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.exlog != null)
			{
				this.exlog.Close();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003B77 File Offset: 0x00001D77
		protected override void InternalWriteHeaderLine(string format, params object[] args)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003B79 File Offset: 0x00001D79
		protected override void InternalWriteLine(string format, params object[] args)
		{
			this.WriteLine("UnknownAnalyzer", "UnknownFormat", format, args);
		}

		// Token: 0x04000020 RID: 32
		private const string ComponentName = "File Chunk Output Streamer";

		// Token: 0x04000021 RID: 33
		private static readonly string[] DefaultFields = new string[]
		{
			"date-time",
			"analyzer-name",
			"output-format",
			"data"
		};

		// Token: 0x04000022 RID: 34
		private readonly Log exlog;

		// Token: 0x04000023 RID: 35
		private readonly LogSchema logSchema;

		// Token: 0x02000009 RID: 9
		private class WrappedFileChunkOutputStream : OutputStream
		{
			// Token: 0x0600002B RID: 43 RVA: 0x00003BCA File Offset: 0x00001DCA
			public WrappedFileChunkOutputStream(FileChunkOutputStream stream, string analyzerName, string outputFormat) : base(analyzerName)
			{
				this.stream = stream;
				this.analyzerName = analyzerName;
				this.outputFormat = outputFormat;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00003BE8 File Offset: 0x00001DE8
			protected override void InternalDispose(bool disposing)
			{
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00003BEA File Offset: 0x00001DEA
			protected override void InternalWriteHeaderLine(string format, params object[] args)
			{
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00003BEC File Offset: 0x00001DEC
			protected override void InternalWriteLine(string format, params object[] args)
			{
				this.stream.WriteLine(this.analyzerName, this.outputFormat, format, args);
			}

			// Token: 0x04000024 RID: 36
			private readonly FileChunkOutputStream stream;

			// Token: 0x04000025 RID: 37
			private readonly string analyzerName;

			// Token: 0x04000026 RID: 38
			private readonly string outputFormat;
		}
	}
}

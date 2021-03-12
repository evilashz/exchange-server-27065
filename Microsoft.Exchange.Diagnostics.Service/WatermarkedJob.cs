using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000020 RID: 32
	public abstract class WatermarkedJob : Job, IDisposable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00007840 File Offset: 0x00005A40
		public WatermarkedJob(string jobName, LogSource logSource, string extensionName, Watermark watermark, string watermarkDirectory) : base(jobName, logSource, extensionName)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			this.outputFormats = new Dictionary<string, string>();
			this.watermark = watermark;
			this.lastWatermarkTimestamp = this.watermark.Timestamp;
			this.watermarkFileStream = new FileStream(Path.Combine(watermarkDirectory, string.Format("{0}_Watermark.xml", base.Name)), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
			this.watermark.Save(this.watermarkFileStream);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000078BF File Offset: 0x00005ABF
		public Watermark Watermark
		{
			get
			{
				return this.watermark;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000078C7 File Offset: 0x00005AC7
		public Dictionary<string, string> OutputFormats
		{
			get
			{
				return this.outputFormats;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000078CF File Offset: 0x00005ACF
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000078D7 File Offset: 0x00005AD7
		internal WaitHandle WaitHandle
		{
			get
			{
				return this.waitHandle;
			}
			set
			{
				this.waitHandle = value;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000078E0 File Offset: 0x00005AE0
		public override void ProcessLines()
		{
			Dictionary<string, string> overrides = Configuration.Overrides;
			TimeSpan configTimeSpan = Configuration.GetConfigTimeSpan(base.Name + "_JobRefreshInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(60.0), TimeSpan.FromMilliseconds(-1.0));
			for (;;)
			{
				Dictionary<string, string> overrides2 = Configuration.Overrides;
				if (!object.ReferenceEquals(overrides, overrides2))
				{
					base.GetLogAnalyzers<LogAnalyzer>();
					Log.LogInformationMessage("Configuration change for job '{0}', reconfiguring analyzers.", new object[]
					{
						base.Name
					});
					base.ExecutionContext.RaiseOnConfigurationUpdate();
					overrides = Configuration.Overrides;
				}
				base.ProcessLines();
				if (this.waitHandle == null)
				{
					break;
				}
				this.waitHandle.WaitOne(configTimeSpan);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007992 File Offset: 0x00005B92
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Dispose(true);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000079AA File Offset: 0x00005BAA
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.watermarkFileStream != null)
			{
				new Watermark(this.lastWatermarkTimestamp).Save(this.watermarkFileStream);
				this.watermarkFileStream.Dispose();
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000079D8 File Offset: 0x00005BD8
		protected override void LinePostProcessEvent(LogSourceLine line)
		{
			if (line == null)
			{
				return;
			}
			DateTime? timestamp = line.Timestamp;
			if (timestamp != null)
			{
				this.lastWatermarkTimestamp = timestamp.Value;
				if (Math.Abs((timestamp.Value - this.watermark.Timestamp).TotalSeconds) >= 1.0)
				{
					this.watermark = new Watermark(timestamp.Value);
					this.watermark.Save(this.watermarkFileStream);
				}
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly FileStream watermarkFileStream;

		// Token: 0x04000070 RID: 112
		private readonly Dictionary<string, string> outputFormats;

		// Token: 0x04000071 RID: 113
		private WaitHandle waitHandle;

		// Token: 0x04000072 RID: 114
		private Watermark watermark;

		// Token: 0x04000073 RID: 115
		private DateTime lastWatermarkTimestamp;

		// Token: 0x04000074 RID: 116
		private bool disposed;
	}
}

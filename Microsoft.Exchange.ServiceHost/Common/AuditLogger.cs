using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ServiceHost.Common
{
	// Token: 0x02000005 RID: 5
	internal abstract class AuditLogger : DisposeTrackableBase
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002DA8 File Offset: 0x00000FA8
		protected AuditLogger(string[] fields, int logFileMaxAge = 30, int maxDirectorySize = 100, int maxFileSize = 100, int maxCacheSize = 10, int logFlushInterval = 10)
		{
			this.logFileMaxAge = TimeSpan.FromDays((double)logFileMaxAge);
			this.maxDirectorySize = (long)(maxDirectorySize * 1024 * 1024);
			this.maxFileSize = (long)(maxFileSize * 1024 * 1024);
			this.maxCacheSize = maxCacheSize * 1024 * 1024;
			this.logFlushInterval = TimeSpan.FromSeconds((double)logFlushInterval);
			this.logSchema = new LogSchema("Microsoft Exchange", Assembly.GetExecutingAssembly().GetName().Version.ToString(), this.LogTypeName, fields);
			this.log = new Log(this.FileNamePrefixName, new LogHeaderFormatter(this.logSchema, true), this.LogComponentName);
			this.Configure();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23
		internal abstract string LogPath { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24
		internal abstract string LogTypeName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25
		internal abstract string FileNamePrefixName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26
		internal abstract string LogComponentName { get; }

		// Token: 0x0600001B RID: 27 RVA: 0x00002E74 File Offset: 0x00001074
		public void Configure()
		{
			if (!base.IsDisposed)
			{
				lock (this.logLock)
				{
					this.log.Configure(this.LogPath, this.logFileMaxAge, this.maxDirectorySize, this.maxFileSize, this.maxCacheSize, this.logFlushInterval, true);
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EE8 File Offset: 0x000010E8
		public void Close()
		{
			if (!base.IsDisposed && this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F0C File Offset: 0x0000110C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002F17 File Offset: 0x00001117
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AuditLogger>(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002F1F File Offset: 0x0000111F
		protected LogRowFormatter NewLogRow()
		{
			return new LogRowFormatter(this.logSchema);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002F2C File Offset: 0x0000112C
		protected void AppendLogRow(LogRowFormatter row)
		{
			if (!base.IsDisposed)
			{
				this.log.Append(row, 0);
			}
		}

		// Token: 0x0400000D RID: 13
		private const string SoftwareName = "Microsoft Exchange";

		// Token: 0x0400000E RID: 14
		private readonly LogSchema logSchema;

		// Token: 0x0400000F RID: 15
		private readonly object logLock = new object();

		// Token: 0x04000010 RID: 16
		private readonly TimeSpan logFileMaxAge;

		// Token: 0x04000011 RID: 17
		private readonly long maxDirectorySize;

		// Token: 0x04000012 RID: 18
		private readonly long maxFileSize;

		// Token: 0x04000013 RID: 19
		private readonly int maxCacheSize;

		// Token: 0x04000014 RID: 20
		private readonly TimeSpan logFlushInterval;

		// Token: 0x04000015 RID: 21
		private Log log;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000004 RID: 4
	public class DataLogger : DataLoggerBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002120 File Offset: 0x00000320
		public DataLogger(ILogConfig logConfig, IList<string> columnNames, IList<Type> columnTypes) : base(new List<string>
		{
			"LogSessionId",
			"LogSequenceNumber"
		}.Concat(columnNames).ToList<string>(), new List<Type>
		{
			typeof(string),
			typeof(string)
		}.Concat(columnTypes).ToList<Type>())
		{
			this.Initialize(logConfig);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002194 File Offset: 0x00000394
		public override string[] RecentlyLoggedRows
		{
			get
			{
				return this.FileLog.RecentlyLoggedRows;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021A1 File Offset: 0x000003A1
		public ILogConfig LogConfig
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021A9 File Offset: 0x000003A9
		public Guid CurrentLogSessionId
		{
			get
			{
				return this.logSessionId;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021B1 File Offset: 0x000003B1
		protected LogWrapper FileLog
		{
			get
			{
				if (this.FileLogInstance == null)
				{
					this.FileLogInstance = new LogWrapper(this.LogConfig, base.ColumnNames.ToArray<string>());
				}
				return this.FileLogInstance;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000021E5 File Offset: 0x000003E5
		protected LogWrapper FileLogInstance { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x000021F0 File Offset: 0x000003F0
		public override void Log(IList<object> values)
		{
			if (!this.LogConfig.IsLoggingEnabled)
			{
				return;
			}
			if (this.logSequenceNumber == this.LogConfig.LogSessionLineCount - 1)
			{
				lock (this.logSessionResetLockObject)
				{
					if (this.logSequenceNumber == this.LogConfig.LogSessionLineCount - 1)
					{
						this.ResetLogSession();
					}
				}
			}
			List<object> first = new List<object>
			{
				this.logSessionId,
				Interlocked.Increment(ref this.logSequenceNumber)
			};
			this.FileLog.Append(first.Concat(values).ToList<object>());
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B0 File Offset: 0x000004B0
		public override void Flush()
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022B2 File Offset: 0x000004B2
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022C1 File Offset: 0x000004C1
		public string[] GetRecentlyLoggedRows()
		{
			return this.FileLog.RecentlyLoggedRows;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022CE File Offset: 0x000004CE
		internal void Initialize(ILogConfig logConfig)
		{
			ArgumentValidator.ThrowIfNull("logConfig", logConfig);
			this.config = logConfig;
			this.logSessionResetLockObject = new object();
			this.ResetLogSession();
			this.ReleaseLogInstance();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022F9 File Offset: 0x000004F9
		protected void ReleaseLogInstance()
		{
			if (this.FileLogInstance != null)
			{
				this.FileLogInstance.Dispose();
				this.FileLogInstance = null;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002315 File Offset: 0x00000515
		private void ResetLogSession()
		{
			this.logSessionId = Guid.NewGuid();
			this.logSequenceNumber = -1;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002329 File Offset: 0x00000529
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					this.ReleaseLogInstance();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000003 RID: 3
		private ILogConfig config;

		// Token: 0x04000004 RID: 4
		private bool isDisposed;

		// Token: 0x04000005 RID: 5
		private Guid logSessionId;

		// Token: 0x04000006 RID: 6
		private int logSequenceNumber;

		// Token: 0x04000007 RID: 7
		private object logSessionResetLockObject;
	}
}

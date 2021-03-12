using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x020007F2 RID: 2034
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class LogConfigurationBase : ILogConfiguration
	{
		// Token: 0x06004C29 RID: 19497 RVA: 0x0013C104 File Offset: 0x0013A304
		public LogConfigurationBase()
		{
			this.enabled = new BoolAppSettingsEntry(this.Component + "Enabled", true, this.Tracer);
			StringAppSettingsEntry stringAppSettingsEntry = new StringAppSettingsEntry(this.Component + "Path", null, this.Tracer);
			this.logPath = (stringAppSettingsEntry.Value ?? Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\" + this.Component + "\\"));
			this.prefix = this.Component + "_" + ApplicationName.Current.UniqueId + "_";
			this.maxDirectorySize = new ByteQuantifiedSizeAppSettingsEntry(this.Component + "MaxDirectorySize", ByteQuantifiedSize.FromMB(250UL), this.Tracer);
			this.maxFileSize = new ByteQuantifiedSizeAppSettingsEntry(this.Component + "MaxFileSize", ByteQuantifiedSize.FromMB(10UL), this.Tracer);
			this.maxAge = new TimeSpanAppSettingsEntry(this.Component + "MaxAge", TimeSpanUnit.Minutes, TimeSpan.FromDays(30.0), this.Tracer);
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x0013C22F File Offset: 0x0013A42F
		public bool IsLoggingEnabled
		{
			get
			{
				return this.enabled.Value;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0013C23C File Offset: 0x0013A43C
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06004C2C RID: 19500 RVA: 0x0013C23F File Offset: 0x0013A43F
		public string LogPath
		{
			get
			{
				return this.logPath;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x0013C247 File Offset: 0x0013A447
		public string LogPrefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x0013C24F File Offset: 0x0013A44F
		public string LogComponent
		{
			get
			{
				return this.Component;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0013C257 File Offset: 0x0013A457
		public string LogType
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x0013C260 File Offset: 0x0013A460
		public long MaxLogDirectorySizeInBytes
		{
			get
			{
				return (long)this.maxDirectorySize.Value.ToBytes();
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x0013C280 File Offset: 0x0013A480
		public long MaxLogFileSizeInBytes
		{
			get
			{
				return (long)this.maxFileSize.Value.ToBytes();
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x0013C2A0 File Offset: 0x0013A4A0
		public TimeSpan MaxLogAge
		{
			get
			{
				return this.maxAge.Value;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06004C33 RID: 19507
		protected abstract string Component { get; }

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06004C34 RID: 19508
		protected abstract string Type { get; }

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06004C35 RID: 19509
		protected abstract Trace Tracer { get; }

		// Token: 0x0400296B RID: 10603
		private readonly BoolAppSettingsEntry enabled;

		// Token: 0x0400296C RID: 10604
		private readonly TimeSpanAppSettingsEntry maxAge;

		// Token: 0x0400296D RID: 10605
		private readonly ByteQuantifiedSizeAppSettingsEntry maxDirectorySize;

		// Token: 0x0400296E RID: 10606
		private readonly ByteQuantifiedSizeAppSettingsEntry maxFileSize;

		// Token: 0x0400296F RID: 10607
		private readonly string prefix;

		// Token: 0x04002970 RID: 10608
		private readonly string logPath;
	}
}

using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200002A RID: 42
	internal abstract class ActivityContextLogFileSettings
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006BEF File Offset: 0x00004DEF
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00006BF7 File Offset: 0x00004DF7
		internal bool Enabled { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006C00 File Offset: 0x00004E00
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00006C08 File Offset: 0x00004E08
		internal string DirectoryPath { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006C11 File Offset: 0x00004E11
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006C19 File Offset: 0x00004E19
		internal TimeSpan MaxAge { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006C22 File Offset: 0x00004E22
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00006C2A File Offset: 0x00004E2A
		internal ByteQuantifiedSize MaxDirectorySize { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006C33 File Offset: 0x00004E33
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00006C3B File Offset: 0x00004E3B
		internal ByteQuantifiedSize MaxFileSize { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006C44 File Offset: 0x00004E44
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00006C4C File Offset: 0x00004E4C
		internal ByteQuantifiedSize CacheSize { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006C55 File Offset: 0x00004E55
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00006C5D File Offset: 0x00004E5D
		internal TimeSpan FlushInterval { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006C66 File Offset: 0x00004E66
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00006C6E File Offset: 0x00004E6E
		internal bool FlushToDisk { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000194 RID: 404
		protected abstract Trace Tracer { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000195 RID: 405
		protected abstract string LogTypeName { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000196 RID: 406
		protected abstract string LogSubFolderName { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006C77 File Offset: 0x00004E77
		protected virtual bool DefaultEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006C7A File Offset: 0x00004E7A
		protected ActivityContextLogFileSettings()
		{
			this.LoadSettings();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006C88 File Offset: 0x00004E88
		protected virtual void LoadCustomSettings()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006C8C File Offset: 0x00004E8C
		protected virtual void LoadSettings()
		{
			this.Tracer.TraceDebug<string>(0L, "Start loading {0} settings.", this.LogTypeName);
			BoolAppSettingsEntry boolAppSettingsEntry = new BoolAppSettingsEntry("LogEnabled", this.DefaultEnabled, this.Tracer);
			this.Enabled = boolAppSettingsEntry.Value;
			StringAppSettingsEntry stringAppSettingsEntry = new StringAppSettingsEntry("LogDirectoryPath", Path.Combine(ExchangeSetupContext.LoggingPath, this.LogSubFolderName), this.Tracer);
			this.DirectoryPath = stringAppSettingsEntry.Value;
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry = new TimeSpanAppSettingsEntry("LogFileAgeInDays", TimeSpanUnit.Days, TimeSpan.FromDays(30.0), this.Tracer);
			this.MaxAge = timeSpanAppSettingsEntry.Value;
			ByteQuantifiedSizeAppSettingsEntry byteQuantifiedSizeAppSettingsEntry = new ByteQuantifiedSizeAppSettingsEntry("LogDirectorySizeLimit", ByteQuantifiedSize.Parse("100MB"), this.Tracer);
			this.MaxDirectorySize = byteQuantifiedSizeAppSettingsEntry.Value;
			ByteQuantifiedSizeAppSettingsEntry byteQuantifiedSizeAppSettingsEntry2 = new ByteQuantifiedSizeAppSettingsEntry("LogFileSizeLimit", ByteQuantifiedSize.Parse("10MB"), this.Tracer);
			this.MaxFileSize = byteQuantifiedSizeAppSettingsEntry2.Value;
			ByteQuantifiedSizeAppSettingsEntry byteQuantifiedSizeAppSettingsEntry3 = new ByteQuantifiedSizeAppSettingsEntry("LogCacheSizeLimit", ByteQuantifiedSize.Parse("2MB"), this.Tracer);
			this.CacheSize = byteQuantifiedSizeAppSettingsEntry3.Value;
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry2 = new TimeSpanAppSettingsEntry("LogFlushIntervalInSeconds", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(60.0), this.Tracer);
			this.FlushInterval = timeSpanAppSettingsEntry2.Value;
			this.FlushToDisk = true;
			this.LoadCustomSettings();
			this.Tracer.TraceDebug<string>(0L, "{0} settings are loaded successfully.", this.LogTypeName);
		}
	}
}

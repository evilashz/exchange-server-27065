using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F3B RID: 3899
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditingOpticsLoggerSettings
	{
		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x06008604 RID: 34308 RVA: 0x0024B6AD File Offset: 0x002498AD
		// (set) Token: 0x06008605 RID: 34309 RVA: 0x0024B6B5 File Offset: 0x002498B5
		internal bool Enabled { get; private set; }

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x06008606 RID: 34310 RVA: 0x0024B6BE File Offset: 0x002498BE
		// (set) Token: 0x06008607 RID: 34311 RVA: 0x0024B6C6 File Offset: 0x002498C6
		internal string DirectoryPath { get; private set; }

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06008608 RID: 34312 RVA: 0x0024B6CF File Offset: 0x002498CF
		// (set) Token: 0x06008609 RID: 34313 RVA: 0x0024B6D7 File Offset: 0x002498D7
		internal TimeSpan MaxAge { get; private set; }

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x0600860A RID: 34314 RVA: 0x0024B6E0 File Offset: 0x002498E0
		// (set) Token: 0x0600860B RID: 34315 RVA: 0x0024B6E8 File Offset: 0x002498E8
		internal ByteQuantifiedSize MaxDirectorySize { get; private set; }

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x0600860C RID: 34316 RVA: 0x0024B6F1 File Offset: 0x002498F1
		// (set) Token: 0x0600860D RID: 34317 RVA: 0x0024B6F9 File Offset: 0x002498F9
		internal ByteQuantifiedSize MaxFileSize { get; private set; }

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x0600860E RID: 34318 RVA: 0x0024B702 File Offset: 0x00249902
		// (set) Token: 0x0600860F RID: 34319 RVA: 0x0024B70A File Offset: 0x0024990A
		internal ByteQuantifiedSize CacheSize { get; private set; }

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x06008610 RID: 34320 RVA: 0x0024B713 File Offset: 0x00249913
		// (set) Token: 0x06008611 RID: 34321 RVA: 0x0024B71B File Offset: 0x0024991B
		internal TimeSpan FlushInterval { get; private set; }

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x06008612 RID: 34322 RVA: 0x0024B724 File Offset: 0x00249924
		// (set) Token: 0x06008613 RID: 34323 RVA: 0x0024B72C File Offset: 0x0024992C
		internal bool FlushToDisk { get; private set; }

		// Token: 0x06008614 RID: 34324 RVA: 0x0024B738 File Offset: 0x00249938
		internal static AuditingOpticsLoggerSettings Load()
		{
			AuditingOpticsLoggerSettings.Tracer.TraceDebug(0L, "Start loading Auditing Optics log settings.");
			AuditingOpticsLoggerSettings auditingOpticsLoggerSettings = new AuditingOpticsLoggerSettings();
			BoolAppSettingsEntry boolAppSettingsEntry = new BoolAppSettingsEntry("LogEnabled", true, AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.Enabled = boolAppSettingsEntry.Value;
			StringAppSettingsEntry stringAppSettingsEntry = new StringAppSettingsEntry("LogDirectoryPath", Path.Combine(ExchangeSetupContext.LoggingPath, AuditingOpticsConstants.LoggerComponentName), AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.DirectoryPath = stringAppSettingsEntry.Value;
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry = new TimeSpanAppSettingsEntry("LogFileAgeInDays", TimeSpanUnit.Days, TimeSpan.FromDays(30.0), AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.MaxAge = timeSpanAppSettingsEntry.Value;
			ByteQuantifiedSizeAppSettingsEntry byteQuantifiedSizeAppSettingsEntry = new ByteQuantifiedSizeAppSettingsEntry("LogDirectorySizeLimit", ByteQuantifiedSize.Parse("100MB"), AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.MaxDirectorySize = byteQuantifiedSizeAppSettingsEntry.Value;
			ByteQuantifiedSizeAppSettingsEntry byteQuantifiedSizeAppSettingsEntry2 = new ByteQuantifiedSizeAppSettingsEntry("LogFileSizeLimit", ByteQuantifiedSize.Parse("10MB"), AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.MaxFileSize = byteQuantifiedSizeAppSettingsEntry2.Value;
			ByteQuantifiedSizeAppSettingsEntry byteQuantifiedSizeAppSettingsEntry3 = new ByteQuantifiedSizeAppSettingsEntry("LogCacheSizeLimit", ByteQuantifiedSize.Parse("256KB"), AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.CacheSize = byteQuantifiedSizeAppSettingsEntry3.Value;
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry2 = new TimeSpanAppSettingsEntry("LogFlushIntervalInSeconds", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(60.0), AuditingOpticsLoggerSettings.Tracer);
			auditingOpticsLoggerSettings.FlushInterval = timeSpanAppSettingsEntry2.Value;
			auditingOpticsLoggerSettings.FlushToDisk = true;
			AuditingOpticsLoggerSettings.Tracer.TraceDebug(0L, "The Auditing Optics log settings are loaded successfully.");
			return auditingOpticsLoggerSettings;
		}

		// Token: 0x040059BF RID: 22975
		private static readonly Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;
	}
}

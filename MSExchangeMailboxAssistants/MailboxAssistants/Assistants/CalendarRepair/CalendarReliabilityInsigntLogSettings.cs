using System;
using System.IO;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000158 RID: 344
	internal sealed class CalendarReliabilityInsigntLogSettings : ActivityContextLogFileSettings
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00054B64 File Offset: 0x00052D64
		protected override string LogSubFolderName
		{
			get
			{
				return "CRAInsightLog";
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00054B6B File Offset: 0x00052D6B
		protected override bool DefaultEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00054B6E File Offset: 0x00052D6E
		protected override string LogTypeName
		{
			get
			{
				return "CRAInsightLog";
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00054B75 File Offset: 0x00052D75
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.CalendarRepairAssistantTracer;
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00054B7C File Offset: 0x00052D7C
		public CalendarReliabilityInsigntLogSettings()
		{
			this.LoadSettings();
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00054B8A File Offset: 0x00052D8A
		private void SafeTraceDebug(long id, string message, params object[] args)
		{
			if (this.Tracer != null)
			{
				this.Tracer.TraceDebug(id, message, args);
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00054BA4 File Offset: 0x00052DA4
		protected override void LoadSettings()
		{
			this.SafeTraceDebug(0L, "Start loading {0} settings.", new object[]
			{
				this.LogTypeName
			});
			ICalendarRepairLoggerSettings calendarRepairAssistantLogging = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MailboxAssistants.CalendarRepairAssistantLogging;
			base.Enabled = calendarRepairAssistantLogging.InsightLogEnabled;
			base.DirectoryPath = Path.Combine(ExchangeSetupContext.LoggingPath, calendarRepairAssistantLogging.InsightLogDirectoryName);
			base.MaxAge = calendarRepairAssistantLogging.InsightLogFileAgeInDays;
			base.MaxDirectorySize = new ByteQuantifiedSize(calendarRepairAssistantLogging.InsightLogDirectorySizeLimit);
			base.MaxFileSize = new ByteQuantifiedSize(calendarRepairAssistantLogging.InsightLogFileSize);
			base.CacheSize = new ByteQuantifiedSize(calendarRepairAssistantLogging.InsightLogCacheSize);
			base.FlushInterval = calendarRepairAssistantLogging.InsightLogFlushIntervalInSeconds;
			base.FlushToDisk = true;
			this.LoadCustomSettings();
			this.SafeTraceDebug(0L, "{0} settings are loaded successfully.", new object[]
			{
				this.LogTypeName
			});
		}

		// Token: 0x04000913 RID: 2323
		internal const string LogName = "CRAInsightLog";
	}
}

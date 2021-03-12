using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;

namespace Microsoft.Exchange.Assistants.Logging
{
	// Token: 0x020000BD RID: 189
	internal class MailboxAssistantsSlaReportLogFileSettings : ActivityContextLogFileSettings
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001B8B1 File Offset: 0x00019AB1
		protected override string LogSubFolderName
		{
			get
			{
				return "MailboxAssistantsSlaReportLog";
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		protected override string LogTypeName
		{
			get
			{
				return "MailboxAssistantsSlaReportLog";
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001B8BF File Offset: 0x00019ABF
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AssistantBaseTracer;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001B8C6 File Offset: 0x00019AC6
		internal static IDisposable SetAssistantsLogFileSettingsEnabledTestHook(bool assistantsLogFileSettingsEnabled)
		{
			return MailboxAssistantsSlaReportLogFileSettings.logTestHook.SetTestHook(assistantsLogFileSettingsEnabled);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		protected override void LoadSettings()
		{
			if (MailboxAssistantsSlaReportLogFileSettings.logTestHook.Value)
			{
				base.LoadSettings();
				base.DirectoryPath = Path.GetFullPath(Path.Combine(base.DirectoryPath, "..\\" + this.LogSubFolderName));
				return;
			}
			base.Enabled = false;
		}

		// Token: 0x04000362 RID: 866
		internal const string MailboxSlaReportLogName = "MailboxAssistantsSlaReportLog";

		// Token: 0x04000363 RID: 867
		private static readonly Hookable<bool> logTestHook = Hookable<bool>.Create(true, true);
	}
}

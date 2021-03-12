using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200001C RID: 28
	internal class AssistantsLogFileSettings : ActivityContextLogFileSettings
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00005334 File Offset: 0x00003534
		private AssistantsLogFileSettings()
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000533C File Offset: 0x0000353C
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005344 File Offset: 0x00003544
		internal string[] LogDisabledAssistants { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000534D File Offset: 0x0000354D
		protected override string LogTypeName
		{
			get
			{
				return "Mailbox Assistants Log";
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005354 File Offset: 0x00003554
		protected override string LogSubFolderName
		{
			get
			{
				return "MailboxAssistantsLog";
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000535B File Offset: 0x0000355B
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AssistantBaseTracer;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005362 File Offset: 0x00003562
		internal static AssistantsLogFileSettings Load()
		{
			return new AssistantsLogFileSettings();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005369 File Offset: 0x00003569
		internal static IDisposable SetAssistantsLogFileSettingsEnabledTestHook(bool assistantsLogFileSettingsEnabled)
		{
			return AssistantsLogFileSettings.hookableAssistantsLogFileSettingsEnabled.SetTestHook(assistantsLogFileSettingsEnabled);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005376 File Offset: 0x00003576
		protected override void LoadSettings()
		{
			if (AssistantsLogFileSettings.hookableAssistantsLogFileSettingsEnabled.Value)
			{
				base.LoadSettings();
				return;
			}
			base.Enabled = false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005394 File Offset: 0x00003594
		protected override void LoadCustomSettings()
		{
			base.LoadCustomSettings();
			StringArrayAppSettingsEntry stringArrayAppSettingsEntry = new StringArrayAppSettingsEntry("LogDisabledAssistants", new string[0], this.Tracer);
			this.LogDisabledAssistants = stringArrayAppSettingsEntry.Value;
		}

		// Token: 0x040000E7 RID: 231
		internal const string AssistantsLogSubFolderName = "MailboxAssistantsLog";

		// Token: 0x040000E8 RID: 232
		private static readonly Hookable<bool> hookableAssistantsLogFileSettingsEnabled = Hookable<bool>.Create(true, true);
	}
}

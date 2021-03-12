using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000249 RID: 585
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ReminderLogConfiguration : ILogConfiguration
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x0007B558 File Offset: 0x00079758
		public ReminderLogConfiguration()
		{
			this.IsLoggingEnabled = true;
			this.LogPath = ReminderLogConfiguration.DefaultLogPath;
			this.MaxLogAge = TimeSpan.FromDays(30.0);
			this.MaxLogDirectorySizeInBytes = 21474836480L;
			this.MaxLogFileSizeInBytes = 10485760L;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0007B5AC File Offset: 0x000797AC
		public static string DefaultLogPath
		{
			get
			{
				return ReminderLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\Reminder\\Assistants");
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0007B5C3 File Offset: 0x000797C3
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x0007B5CB File Offset: 0x000797CB
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0007B5D4 File Offset: 0x000797D4
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0007B5D7 File Offset: 0x000797D7
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x0007B5DF File Offset: 0x000797DF
		public string LogPath { get; private set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0007B5E8 File Offset: 0x000797E8
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x0007B5F0 File Offset: 0x000797F0
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0007B5F9 File Offset: 0x000797F9
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0007B601 File Offset: 0x00079801
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0007B60A File Offset: 0x0007980A
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0007B612 File Offset: 0x00079812
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0007B61B File Offset: 0x0007981B
		public string LogComponent
		{
			get
			{
				return "ReminderLog";
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0007B622 File Offset: 0x00079822
		public string LogPrefix
		{
			get
			{
				return ReminderLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0007B629 File Offset: 0x00079829
		public string LogType
		{
			get
			{
				return "Reminder Log";
			}
		}

		// Token: 0x04000CDB RID: 3291
		private const string LogTypeValue = "Reminder Log";

		// Token: 0x04000CDC RID: 3292
		private const string LogComponentValue = "ReminderLog";

		// Token: 0x04000CDD RID: 3293
		private const string DefaultRelativeFilePath = "Logging\\Reminder\\Assistants";

		// Token: 0x04000CDE RID: 3294
		private const int MaxLogAgeInDay = 30;

		// Token: 0x04000CDF RID: 3295
		private const int MaxLogDirectorySizeInGB = 20;

		// Token: 0x04000CE0 RID: 3296
		private const int MaxLogFileSizeInMB = 10;

		// Token: 0x04000CE1 RID: 3297
		public static readonly string LogPrefixValue = "Reminder";

		// Token: 0x04000CE2 RID: 3298
		private static string defaultLogPath;
	}
}

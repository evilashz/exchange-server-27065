using System;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000040 RID: 64
	internal sealed class AppConfig : IAppConfiguration
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000D0CE File Offset: 0x0000B2CE
		internal AppConfig()
		{
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000D0D6 File Offset: 0x0000B2D6
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000D0DE File Offset: 0x0000B2DE
		public int MaxConcurrentSubmissions { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000D0EF File Offset: 0x0000B2EF
		public bool IsWriteToPickupFolderEnabled { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000D100 File Offset: 0x0000B300
		public TimeSpan HangDetectionInterval { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000D109 File Offset: 0x0000B309
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000D111 File Offset: 0x0000B311
		public TimeSpan SmtpOutWaitTimeOut { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000D11A File Offset: 0x0000B31A
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000D122 File Offset: 0x0000B322
		public bool ShouldLogTemporaryFailures { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D12B File Offset: 0x0000B32B
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000D133 File Offset: 0x0000B333
		public bool ShouldLogNotifyEvents { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000D13C File Offset: 0x0000B33C
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000D144 File Offset: 0x0000B344
		public bool UseLocalHubOnly { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000D14D File Offset: 0x0000B34D
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000D155 File Offset: 0x0000B355
		public bool EnableCalendarHeaderCreation { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000D15E File Offset: 0x0000B35E
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000D166 File Offset: 0x0000B366
		public bool EnableSeriesMessageProcessing { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000D16F File Offset: 0x0000B36F
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000D177 File Offset: 0x0000B377
		public bool EnableUnparkedMessageRestoring { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000D180 File Offset: 0x0000B380
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000D188 File Offset: 0x0000B388
		public bool EnableMailboxQuarantine { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000D191 File Offset: 0x0000B391
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000D199 File Offset: 0x0000B399
		public int MailboxQuarantineCrashCountThreshold { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000D1A2 File Offset: 0x0000B3A2
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000D1AA File Offset: 0x0000B3AA
		public TimeSpan MailboxQuarantineCrashCountWindow { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000D1B3 File Offset: 0x0000B3B3
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000D1BB File Offset: 0x0000B3BB
		public TimeSpan MailboxQuarantineSpan { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public string PoisonRegistryEntryLocation { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000D1DD File Offset: 0x0000B3DD
		public TimeSpan PoisonRegistryEntryExpiryWindow { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000D1E6 File Offset: 0x0000B3E6
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		public int PoisonRegistryEntryMaxCount { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000D1FF File Offset: 0x0000B3FF
		public bool SenderRateDeprioritizationEnabled { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000D208 File Offset: 0x0000B408
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000D210 File Offset: 0x0000B410
		public int SenderRateDeprioritizationThreshold { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000D219 File Offset: 0x0000B419
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000D221 File Offset: 0x0000B421
		public bool SenderRateThrottlingEnabled { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000D22A File Offset: 0x0000B42A
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000D232 File Offset: 0x0000B432
		public int SenderRateThrottlingThreshold { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000D23B File Offset: 0x0000B43B
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000D243 File Offset: 0x0000B443
		public TimeSpan SenderRateThrottlingRetryInterval { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000D24C File Offset: 0x0000B44C
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000D254 File Offset: 0x0000B454
		public bool EnableSendNdrForPoisonMessage { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000D25D File Offset: 0x0000B45D
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000D265 File Offset: 0x0000B465
		public TimeSpan ServiceHeartbeatPeriodicLoggingInterval { get; private set; }

		// Token: 0x06000291 RID: 657 RVA: 0x0000D270 File Offset: 0x0000B470
		public static IAppConfiguration Load()
		{
			return new AppConfig
			{
				MaxConcurrentSubmissions = AppConfig.GetConfigInt(AppConfig.maxConcurrentSubmissions, 1, int.MaxValue, 5),
				IsWriteToPickupFolderEnabled = AppConfig.GetConfigBool(AppConfig.writeToPickupFolderEnabled, false),
				HangDetectionInterval = AppConfig.GetConfigTimeSpan(AppConfig.hangDetectionInterval, TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0)),
				SmtpOutWaitTimeOut = AppConfig.GetConfigTimeSpan(AppConfig.smtpOutWaitTimeOut, TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(15.0)),
				ShouldLogTemporaryFailures = AppConfig.GetConfigBool(AppConfig.logTemporaryFailures, true),
				ShouldLogNotifyEvents = AppConfig.GetConfigBool(AppConfig.logNotifyEvents, true),
				UseLocalHubOnly = AppConfig.GetConfigBool(AppConfig.useLocalHubOnly, false),
				EnableCalendarHeaderCreation = AppConfig.GetConfigBool(AppConfig.enableCalendarHeaderCreation, true),
				EnableSeriesMessageProcessing = AppConfig.GetConfigBool(AppConfig.enableSeriesMessageProcessing, true),
				EnableUnparkedMessageRestoring = AppConfig.GetConfigBool(AppConfig.enableUnparkedMessageRestoring, true),
				EnableMailboxQuarantine = AppConfig.GetConfigBool(AppConfig.enableMailboxQuarantine, false),
				MailboxQuarantineCrashCountThreshold = AppConfig.GetConfigInt(AppConfig.mailboxQuarantineCrashCountThreshold, 1, int.MaxValue, 8),
				MailboxQuarantineCrashCountWindow = AppConfig.GetConfigTimeSpan(AppConfig.mailboxQuarantineCrashCountWindow, TimeSpan.MinValue, TimeSpan.MaxValue, TimeSpan.FromHours(4.0)),
				MailboxQuarantineSpan = AppConfig.GetConfigTimeSpan(AppConfig.mailboxQuarantineSpan, TimeSpan.MinValue, TimeSpan.MaxValue, TimeSpan.FromHours(24.0)),
				PoisonRegistryEntryLocation = AppConfig.GetConfigString(AppConfig.poisonRegistryEntryLocation, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\PoisonMessage\\StoreDriver\\Submission"),
				PoisonRegistryEntryExpiryWindow = AppConfig.GetConfigTimeSpan(AppConfig.poisonRegistryEntryExpiryWindow, TimeSpan.MinValue, TimeSpan.MaxValue, TimeSpan.FromDays(7.0)),
				PoisonRegistryEntryMaxCount = AppConfig.GetConfigInt(AppConfig.poisonRegistryEntryMaxCount, 1, int.MaxValue, 100),
				SenderRateDeprioritizationEnabled = AppConfig.GetConfigBool("SenderRateDeprioritizationEnabled", true),
				SenderRateDeprioritizationThreshold = AppConfig.GetConfigInt("SenderRateDeprioritizationThreshold", 2, 100000, 30),
				SenderRateThrottlingEnabled = AppConfig.GetConfigBool("SenderRateThrottlingEnabled", true),
				SenderRateThrottlingThreshold = AppConfig.GetConfigInt("SenderRateThrottlingThreshold", 2, 100000, 30),
				SenderRateThrottlingRetryInterval = AppConfig.GetConfigTimeSpan("SenderRateThrottlingRetryInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(12.0), TimeSpan.FromSeconds(30.0)),
				EnableSendNdrForPoisonMessage = AppConfig.GetConfigBool(AppConfig.enableSendNdrForPoisonMessage, true),
				ServiceHeartbeatPeriodicLoggingInterval = AppConfig.GetConfigTimeSpan(AppConfig.serviceHeartbeatPeriodicLoggingInterval, TimeSpan.MinValue, TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0))
			};
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		public void AddDiagnosticInfo(XElement parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			parent.Add(new object[]
			{
				new XElement(AppConfig.hangDetectionInterval, this.HangDetectionInterval),
				new XElement(AppConfig.writeToPickupFolderEnabled, this.IsWriteToPickupFolderEnabled),
				new XElement(AppConfig.logNotifyEvents, this.ShouldLogNotifyEvents),
				new XElement(AppConfig.logTemporaryFailures, this.ShouldLogTemporaryFailures),
				new XElement(AppConfig.maxConcurrentSubmissions, this.MaxConcurrentSubmissions),
				new XElement(AppConfig.useLocalHubOnly, this.UseLocalHubOnly),
				new XElement(AppConfig.smtpOutWaitTimeOut, this.SmtpOutWaitTimeOut),
				new XElement(AppConfig.enableCalendarHeaderCreation, this.EnableCalendarHeaderCreation),
				new XElement(AppConfig.enableMailboxQuarantine, this.EnableMailboxQuarantine),
				new XElement(AppConfig.poisonRegistryEntryExpiryWindow, this.PoisonRegistryEntryExpiryWindow),
				new XElement(AppConfig.poisonRegistryEntryMaxCount, this.PoisonRegistryEntryMaxCount),
				new XElement(AppConfig.senderRateDeprioritizationEnabled, this.SenderRateDeprioritizationEnabled),
				new XElement(AppConfig.senderRateDeprioritizationThreshold, this.SenderRateDeprioritizationThreshold),
				new XElement(AppConfig.senderRateThrottlingEnabled, this.SenderRateThrottlingEnabled),
				new XElement(AppConfig.senderRateThrottlingThreshold, this.SenderRateThrottlingThreshold),
				new XElement(AppConfig.senderRateThrottlingRetryInterval, this.SenderRateThrottlingRetryInterval),
				new XElement(AppConfig.serviceHeartbeatPeriodicLoggingInterval, this.ServiceHeartbeatPeriodicLoggingInterval),
				new XElement(AppConfig.poisonRegistryEntryLocation, this.PoisonRegistryEntryLocation),
				new XElement(AppConfig.enableSendNdrForPoisonMessage, this.EnableSendNdrForPoisonMessage),
				new XElement(AppConfig.mailboxQuarantineCrashCountThreshold, this.MailboxQuarantineCrashCountThreshold),
				new XElement(AppConfig.mailboxQuarantineCrashCountWindow, this.MailboxQuarantineCrashCountWindow),
				new XElement(AppConfig.mailboxQuarantineSpan, this.MailboxQuarantineSpan)
			});
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		private static bool GetConfigBool(string label, bool defaultValue)
		{
			bool result;
			try
			{
				result = TransportAppConfig.GetConfigBool(label, defaultValue);
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		private static int GetConfigInt(string label, int minimumValue, int maximumValue, int defaultValue)
		{
			int result;
			try
			{
				result = TransportAppConfig.GetConfigInt(label, minimumValue, maximumValue, defaultValue);
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000D804 File Offset: 0x0000BA04
		private static string GetConfigString(string label, string defaultValue)
		{
			string result;
			try
			{
				result = TransportAppConfig.GetConfigString(label, defaultValue);
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000D834 File Offset: 0x0000BA34
		private static TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			TimeSpan result;
			try
			{
				result = TransportAppConfig.GetConfigValue<TimeSpan>(label, min, max, defaultValue, new TransportAppConfig.TryParse<TimeSpan>(TimeSpan.TryParse));
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x04000139 RID: 313
		private static string writeToPickupFolderEnabled = "WriteToPickupFolderEnabled";

		// Token: 0x0400013A RID: 314
		private static string hangDetectionInterval = "HangDetectionInterval";

		// Token: 0x0400013B RID: 315
		private static string smtpOutWaitTimeOut = "SmtpOutWaitTimeOut";

		// Token: 0x0400013C RID: 316
		private static string logTemporaryFailures = "LogTemporaryFailures";

		// Token: 0x0400013D RID: 317
		private static string logNotifyEvents = "LogNotifyEvents";

		// Token: 0x0400013E RID: 318
		private static string maxConcurrentSubmissions = "MaxConcurrentSubmissions";

		// Token: 0x0400013F RID: 319
		private static string useLocalHubOnly = "UseLocalHubOnly";

		// Token: 0x04000140 RID: 320
		private static string enableCalendarHeaderCreation = "EnableCalendarHeaderCreation";

		// Token: 0x04000141 RID: 321
		private static string enableSeriesMessageProcessing = "EnableSeriesMessageProcessing";

		// Token: 0x04000142 RID: 322
		private static string enableUnparkedMessageRestoring = "EnableUnparkedMessageRestoring";

		// Token: 0x04000143 RID: 323
		private static string enableMailboxQuarantine = "EnableMailboxQuarantine";

		// Token: 0x04000144 RID: 324
		private static string mailboxQuarantineCrashCountThreshold = "MailboxQuarantineCrashCountThreshold";

		// Token: 0x04000145 RID: 325
		private static string mailboxQuarantineCrashCountWindow = "MailboxQuarantineCrashCountWindow";

		// Token: 0x04000146 RID: 326
		private static string mailboxQuarantineSpan = "MailboxQuarantineSpan";

		// Token: 0x04000147 RID: 327
		private static string poisonRegistryEntryLocation = "PoisonRegistryEntryLocation";

		// Token: 0x04000148 RID: 328
		private static string poisonRegistryEntryExpiryWindow = "PoisonRegistryEntryExpiryWindow";

		// Token: 0x04000149 RID: 329
		private static string poisonRegistryEntryMaxCount = "PoisonRegistryEntryMaxCount";

		// Token: 0x0400014A RID: 330
		private static string senderRateDeprioritizationEnabled = "SenderRateDeprioritizationEnabled";

		// Token: 0x0400014B RID: 331
		private static string senderRateDeprioritizationThreshold = "SenderRateDeprioritizationThreshold";

		// Token: 0x0400014C RID: 332
		private static string senderRateThrottlingEnabled = "SenderRateThrottlingEnabled";

		// Token: 0x0400014D RID: 333
		private static string senderRateThrottlingThreshold = "SenderRateThrottlingThreshold";

		// Token: 0x0400014E RID: 334
		private static string senderRateThrottlingRetryInterval = "SenderRateThrottlingRetryInterval";

		// Token: 0x0400014F RID: 335
		private static string enableSendNdrForPoisonMessage = "EnableSendNdrForPoisonMessage";

		// Token: 0x04000150 RID: 336
		private static string serviceHeartbeatPeriodicLoggingInterval = "ServiceHeartbeatPeriodicLoggingInterval";
	}
}

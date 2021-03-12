using System;
using System.Configuration;
using System.IO;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000202 RID: 514
	public class OwaSettingsLoader : OwaSettingsLoaderBase
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x000679F0 File Offset: 0x00065BF0
		internal OwaSettingsLoader()
		{
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00067A14 File Offset: 0x00065C14
		internal override void Load()
		{
			base.Load();
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.OWA);
			this.CheckIncompatibleTimeoutValues();
			this.ReadInstantMessageSettings();
			this.ReadNotificationSettings();
			this.InitializeMaxBytesInConversationReadingPane();
			MessagePrefetchConfiguration.InitializeSettings();
			this.ReadAndInitializeIMPerfCounterSettings();
			this.ReadAndInitializeExceptionPerfCounterSettings();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00067A4C File Offset: 0x00065C4C
		private void CheckIncompatibleTimeoutValues()
		{
			int sessionTimeout = OwaConfigurationManager.Configuration.SessionTimeout;
			if ((double)(sessionTimeout * 60) < Math.Ceiling(1.25 * (double)this.MaxPendingRequestLifeInSeconds))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_IncompatibleTimeoutSetting, string.Empty, new object[]
				{
					sessionTimeout,
					this.MaxPendingRequestLifeInSeconds,
					300
				});
				throw new OwaInvalidInputException("The timeout setting values for \"UserContextTimeout\" and \"MaxPendingRequestLife\" chosen by the admin are conflicting", null, 0);
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00067AD0 File Offset: 0x00065CD0
		internal void InitializeMaxBytesInConversationReadingPane()
		{
			long maxBytesInConversationReadingPane = Globals.MaxBytesInConversationReadingPane;
			if (maxBytesInConversationReadingPane != -9223372036854775808L)
			{
				Conversation.MaxBytesForConversation = maxBytesInConversationReadingPane;
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00067AF8 File Offset: 0x00065CF8
		private void ReadNotificationSettings()
		{
			string text = ConfigurationManager.AppSettings["PushNotificationsEnabled"];
			string text2 = ConfigurationManager.AppSettings["PullNotificationsEnabled"];
			string text3 = ConfigurationManager.AppSettings["FolderContentNotificationsEnabled"];
			int num;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num))
			{
				if (num > 0)
				{
					this.isPushNotificationsEnabled = true;
				}
				else
				{
					this.isPushNotificationsEnabled = false;
				}
			}
			int num2;
			if (!string.IsNullOrEmpty(text2) && int.TryParse(text2, out num2))
			{
				if (num2 > 0)
				{
					this.isPullNotificationsEnabled = true;
				}
				else
				{
					this.isPullNotificationsEnabled = false;
				}
			}
			int num3;
			if (!string.IsNullOrEmpty(text3) && int.TryParse(text3, out num3))
			{
				if (num3 > 0)
				{
					this.isFolderContentNotificationsEnabled = true;
					return;
				}
				this.isFolderContentNotificationsEnabled = false;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00067BA8 File Offset: 0x00065DA8
		private void ReadInstantMessageSettings()
		{
			this.activityBasedPresenceDuration = 300000;
			string text = ConfigurationManager.AppSettings["ActivityBasedPresenceDuration"];
			int num;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && 0 < num)
			{
				this.activityBasedPresenceDuration = num * 60 * 1000;
			}
			this.ReadOcsServerSettings();
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00067BFC File Offset: 0x00065DFC
		private void ReadOcsServerSettings()
		{
			string value = ConfigurationManager.AppSettings["EnableIMForOwaPremium"];
			bool flag = false;
			bool flag2;
			if (!string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out flag2))
			{
				flag = flag2;
			}
			if (!flag)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Globals.ReadOcsServerSettings. OWA Premium Instant Messaging integration is disabled.");
				return;
			}
			int mtlsPortNumber = -1;
			bool flag3 = true;
			bool flag4 = true;
			string text = string.Empty;
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Globals.ReadOcsServerSettings");
			if (OwaConfigurationManager.Configuration != null)
			{
				this.ocsServerName = OwaConfigurationManager.Configuration.InstantMessagingServerName;
				if (string.IsNullOrEmpty(this.ocsServerName))
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Instant Messaging Server name is set to null or empty on the OWA Virtual Directory object.");
					flag3 = false;
				}
				text = OwaConfigurationManager.Configuration.InstantMessagingCertificateThumbprint;
				if (string.IsNullOrEmpty(text))
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Instant Messaging Certificate Thumbprint is null or empty on the OWA Virtual Directory object.");
					flag4 = false;
				}
			}
			if (!flag3 || !flag4)
			{
				if (OwaConfigurationManager.Configuration != null && OwaConfigurationManager.Configuration.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
				{
					if (!flag3)
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMServerNameInvalid);
					}
					if (!flag4)
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorIMCertificateThumbprintInvalid);
					}
				}
				return;
			}
			InstantMessageCertUtils.GetIMCertInfo(text, out this.certificateIssuer, out this.certificateSerial);
			if (string.IsNullOrEmpty(this.certificateIssuer))
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate Issuer is null.");
				return;
			}
			if (this.certificateSerial == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "IM Certificate Serial is null.");
				return;
			}
			string text2 = ConfigurationManager.AppSettings["IMPortNumber"];
			int num;
			if (!string.IsNullOrEmpty(text2) && int.TryParse(text2, out num) && num >= 0)
			{
				mtlsPortNumber = num;
			}
			InstantMessageOCSProvider.InitializeEndpointManager(this.certificateIssuer, this.certificateSerial, mtlsPortNumber);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00067D94 File Offset: 0x00065F94
		private void ReadAndInitializeExceptionPerfCounterSettings()
		{
			string text = ConfigurationManager.AppSettings["MailboxOfflineExQueueSize"];
			string text2 = ConfigurationManager.AppSettings["ConnectionFailedTransientExQueueSize"];
			string text3 = ConfigurationManager.AppSettings["StorageTransientExQueueSize"];
			string text4 = ConfigurationManager.AppSettings["StoragePermanentExQueueSize"];
			int num;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && num > 0)
			{
				PerformanceCounterManager.MailboxOfflineExResultsQueueSize = num;
			}
			int num2;
			if (!string.IsNullOrEmpty(text2) && int.TryParse(text2, out num2) && num2 > 0)
			{
				PerformanceCounterManager.ConnectionFailedTransientExResultsQueueSize = num2;
			}
			int num3;
			if (!string.IsNullOrEmpty(text4) && int.TryParse(text4, out num3) && num3 > 0)
			{
				PerformanceCounterManager.StoragePermanentExResultsQueueSize = num3;
			}
			int num4;
			if (!string.IsNullOrEmpty(text3) && int.TryParse(text3, out num4) && num4 > 0)
			{
				PerformanceCounterManager.StorageTransientExResultsQueueSize = num4;
			}
			PerformanceCounterManager.InitializeExPerfCountersQueueSizes();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00067E60 File Offset: 0x00066060
		private void ReadAndInitializeIMPerfCounterSettings()
		{
			int signInFailureQueueSize = 100;
			int sentMessageFailureQueueSize = 100;
			string text = ConfigurationManager.AppSettings["SignInFailureQueueSize"];
			string text2 = ConfigurationManager.AppSettings["SentMessageFailureQueueSize"];
			int num;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && num > 0)
			{
				signInFailureQueueSize = num;
			}
			int num2;
			if (!string.IsNullOrEmpty(text2) && int.TryParse(text2, out num2) && num2 > 0)
			{
				sentMessageFailureQueueSize = num2;
			}
			PerformanceCounterManager.InitializeIMQueueSizes(signInFailureQueueSize, sentMessageFailureQueueSize);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00067ED0 File Offset: 0x000660D0
		private void IncomingTlsNegotiationFailedHandler(object sender, ErrorEventArgs args)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Globals.IncomingTlsNegotiationFailedHandler");
			Exception ex = null;
			if (args != null)
			{
				ex = args.GetException();
			}
			OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_FailedToEstablishMTLSConnection, string.Empty, new object[]
			{
				(ex != null && ex.Message != null) ? ex.Message : string.Empty
			});
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00067F30 File Offset: 0x00066130
		private int GetIntSettingFromConfig(string settingName, int defaultValue)
		{
			string text = ConfigurationManager.AppSettings[settingName];
			int result = defaultValue;
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string, int>(0L, "{0} not set in web.config, using default value of {1}.", settingName, defaultValue);
			}
			else if (!int.TryParse(text, out result))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string, string, int>(0L, "Invalid {0} set in web.config (value: {1}); using default value of {2}.", settingName, text, defaultValue);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00067F8C File Offset: 0x0006618C
		public override bool IsPreCheckinApp
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["IsPreCheckinApp"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00067FB8 File Offset: 0x000661B8
		public override bool IsPushNotificationsEnabled
		{
			get
			{
				return this.isPushNotificationsEnabled;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00067FC0 File Offset: 0x000661C0
		public override bool IsPullNotificationsEnabled
		{
			get
			{
				return this.isPullNotificationsEnabled;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00067FC8 File Offset: 0x000661C8
		public override bool IsFolderContentNotificationsEnabled
		{
			get
			{
				return this.isFolderContentNotificationsEnabled;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00067FD0 File Offset: 0x000661D0
		public override int ConnectionCacheSize
		{
			get
			{
				string text = ConfigurationManager.AppSettings["ConnectionCacheSize"];
				if (string.IsNullOrEmpty(text))
				{
					ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "ConnectionCacheSize not set in web.config; using default value of {0}.", 100);
					return 100;
				}
				int num;
				if (int.TryParse(text, out num) && 0 < num)
				{
					return num;
				}
				ExTraceGlobals.CoreTracer.TraceDebug<string, int>(0L, "Invalid ConnectionCacheSize set in web.config (value: {0}); using default value of {1}.", text, 100);
				return 100;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00068034 File Offset: 0x00066234
		public override bool ListenAdNotifications
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["ListenAdNotifications"], out flag);
				return !flag2 || flag;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00068060 File Offset: 0x00066260
		public override bool RenderBreadcrumbsInAboutPage
		{
			get
			{
				bool flag = true;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["RenderBreadcrumbsInAboutPage"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0006808C File Offset: 0x0006628C
		public override int MaximumTemporaryFilteredViewPerUser
		{
			get
			{
				string text = ConfigurationManager.AppSettings["MaximumTemporaryFilteredViewPerUser"];
				int num;
				if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num < 1)
				{
					return 60;
				}
				return num;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x000680C4 File Offset: 0x000662C4
		public override int MaximumFilteredViewInFavoritesPerUser
		{
			get
			{
				string text = ConfigurationManager.AppSettings["MaximumFilteredViewInFavoritesPerUser"];
				int num;
				if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num < 1)
				{
					return 25;
				}
				return num;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000680FC File Offset: 0x000662FC
		public override bool DisableBreadcrumbs
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["DisableBreadcrumbs"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00068128 File Offset: 0x00066328
		public override int MaxBreadcrumbs
		{
			get
			{
				string text = ConfigurationManager.AppSettings["MaxBreadcrumbs"];
				if (string.IsNullOrEmpty(text))
				{
					ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "MaxBreadcrumbs not set in web.config; using default value of {0}.", 20);
					return 20;
				}
				int num;
				if (int.TryParse(text, out num) && 0 < num)
				{
					return num;
				}
				ExTraceGlobals.CoreTracer.TraceDebug<string, int>(0L, "Invalid defaultMaxBreadcrumbs set in web.config (value: {0}); using default value of {1}.", text, 20);
				return 20;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0006818C File Offset: 0x0006638C
		public override bool StoreTransientExceptionEventLogEnabled
		{
			get
			{
				string value = ConfigurationManager.AppSettings["StoreTransientExceptionEventLogEnabled"];
				bool flag;
				return !string.IsNullOrEmpty(value) && bool.TryParse(value, out flag) && flag;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x000681C0 File Offset: 0x000663C0
		public override int StoreTransientExceptionEventLogThreshold
		{
			get
			{
				string text = ConfigurationManager.AppSettings["StoreTransientExceptionEventLogThreshold"];
				if (string.IsNullOrEmpty(text))
				{
					return 50;
				}
				int num;
				if (!int.TryParse(text, out num))
				{
					return 50;
				}
				if (num <= 0)
				{
					return 50;
				}
				return num;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00068200 File Offset: 0x00066400
		public override int StoreTransientExceptionEventLogFrequencyInSeconds
		{
			get
			{
				string text = ConfigurationManager.AppSettings["StoreTransientExceptionEventLogFrequencyInSeconds"];
				if (string.IsNullOrEmpty(text))
				{
					return 1800;
				}
				int num;
				if (!int.TryParse(text, out num))
				{
					return 1800;
				}
				if (num <= 0)
				{
					return 1800;
				}
				return num;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00068248 File Offset: 0x00066448
		public override int MaxPendingRequestLifeInSeconds
		{
			get
			{
				string text = ConfigurationManager.AppSettings["MaxPendingRequestLife"];
				if (string.IsNullOrEmpty(text))
				{
					return 300;
				}
				int num = -1;
				int.TryParse(text, out num);
				if (30 > num || num > 1800)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string, int>(0L, "Invalid max pending request life set in web.config (value: {0}); using default value of {1}.", text, 300);
					return 300;
				}
				return num;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000682AC File Offset: 0x000664AC
		public override int MaxItemsInConversationExpansion
		{
			get
			{
				int result;
				bool flag = int.TryParse(ConfigurationManager.AppSettings["MaxItemsInConversationExpansion"], out result);
				if (flag)
				{
					return result;
				}
				return 499;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x000682DC File Offset: 0x000664DC
		public override int MaxItemsInConversationReadingPane
		{
			get
			{
				int result;
				bool flag = int.TryParse(ConfigurationManager.AppSettings["MaxItemsInConversationReadingPane"], out result);
				if (flag)
				{
					return result;
				}
				return 100;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00068308 File Offset: 0x00066508
		public override long MaxBytesInConversationReadingPane
		{
			get
			{
				string text = ConfigurationManager.AppSettings["MaxBytesInConversationReadingPane"];
				if (!string.IsNullOrEmpty(text))
				{
					long result;
					bool flag = long.TryParse(text, out result);
					if (flag)
					{
						return result;
					}
				}
				return long.MinValue;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00068344 File Offset: 0x00066544
		public override bool HideDeletedItems
		{
			get
			{
				bool flag2;
				bool flag = bool.TryParse(ConfigurationManager.AppSettings["HideDeletedItems"], out flag2);
				return flag && flag2;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0006836E File Offset: 0x0006656E
		public override string OCSServerName
		{
			get
			{
				return this.ocsServerName;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00068376 File Offset: 0x00066576
		public override int ActivityBasedPresenceDuration
		{
			get
			{
				return this.activityBasedPresenceDuration;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0006837E File Offset: 0x0006657E
		public override int MailTipsMaxClientCacheSize
		{
			get
			{
				return this.GetIntSettingFromConfig("MailTipsMaxClientCacheSize", 300);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00068390 File Offset: 0x00066590
		public override int MailTipsMaxMailboxSourcedRecipientSize
		{
			get
			{
				return this.GetIntSettingFromConfig("MailTipsMaxMailboxSourcedRecipientSize", 300);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000683A2 File Offset: 0x000665A2
		public override int MailTipsClientCacheEntryExpiryInHours
		{
			get
			{
				return this.GetIntSettingFromConfig("MailTipsClientCacheEntryExpiryInHours", 2);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000683B0 File Offset: 0x000665B0
		internal override PhishingLevel MinimumSuspiciousPhishingLevel
		{
			get
			{
				return this.minimumSuspiciousPhishingLevel;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000683B8 File Offset: 0x000665B8
		internal override int UserContextLockTimeout
		{
			get
			{
				if (this.userContextLockTimeout <= 0)
				{
					this.userContextLockTimeout = Math.Min(this.GetIntSettingFromConfig("UserContextTimeout", 3000), 30000);
					if (this.userContextLockTimeout <= 0)
					{
						this.userContextLockTimeout = 3000;
					}
				}
				return this.userContextLockTimeout;
			}
		}

		// Token: 0x04000B7E RID: 2942
		private const string ConnCacheSize = "ConnectionCacheSize";

		// Token: 0x04000B7F RID: 2943
		private const int DefaultConnectionCacheSize = 100;

		// Token: 0x04000B80 RID: 2944
		private const double MinUserTimeoutMaxPendingLifeRatio = 1.25;

		// Token: 0x04000B81 RID: 2945
		private const int DefaultStoreTransientExceptionEventLogThreshold = 50;

		// Token: 0x04000B82 RID: 2946
		private const int DefaultStoreTransientExceptionEventLogFrequencyInSeconds = 1800;

		// Token: 0x04000B83 RID: 2947
		private const int DefaultMaxPendingRequestLifeInSeconds = 300;

		// Token: 0x04000B84 RID: 2948
		private const int DefaultMaxBreadcrumbs = 20;

		// Token: 0x04000B85 RID: 2949
		private const bool DefaultDisableBreadcrumbs = false;

		// Token: 0x04000B86 RID: 2950
		private const bool DefaultBypassOwaXmlAttachmentFiltering = false;

		// Token: 0x04000B87 RID: 2951
		private const int DefaultMaximumTemporaryFilteredViewPerUser = 60;

		// Token: 0x04000B88 RID: 2952
		private const int DefaultMaximumFilteredViewInFavoritesPerUser = 25;

		// Token: 0x04000B89 RID: 2953
		internal const int DefaultUserContextLockTimeout = 3000;

		// Token: 0x04000B8A RID: 2954
		internal const int MaxUserContextLockTimeout = 30000;

		// Token: 0x04000B8B RID: 2955
		internal const string UserContextLockTimeoutStr = "UserContextTimeout";

		// Token: 0x04000B8C RID: 2956
		private bool isPushNotificationsEnabled = true;

		// Token: 0x04000B8D RID: 2957
		private bool isPullNotificationsEnabled;

		// Token: 0x04000B8E RID: 2958
		private bool isFolderContentNotificationsEnabled = true;

		// Token: 0x04000B8F RID: 2959
		private string ocsServerName;

		// Token: 0x04000B90 RID: 2960
		private string certificateIssuer;

		// Token: 0x04000B91 RID: 2961
		private byte[] certificateSerial;

		// Token: 0x04000B92 RID: 2962
		private int activityBasedPresenceDuration;

		// Token: 0x04000B93 RID: 2963
		private PhishingLevel minimumSuspiciousPhishingLevel = PhishingLevel.Suspicious1;

		// Token: 0x04000B94 RID: 2964
		private int userContextLockTimeout = -1;
	}
}

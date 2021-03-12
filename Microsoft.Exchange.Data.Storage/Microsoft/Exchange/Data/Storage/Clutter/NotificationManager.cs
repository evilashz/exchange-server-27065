using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Clutter.Notifications;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x02000441 RID: 1089
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NotificationManager : IDisposable
	{
		// Token: 0x0600308D RID: 12429 RVA: 0x000C767C File Offset: 0x000C587C
		public NotificationManager(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator) : this(session, snapshot, frontEndLocator, null)
		{
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000C7688 File Offset: 0x000C5888
		public NotificationManager(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator, UserConfiguration inferenceSettings)
		{
			this.session = session;
			this.snapshot = snapshot;
			this.frontEndLocator = frontEndLocator;
			if (inferenceSettings == null || inferenceSettings.GetDictionary() == null)
			{
				this.inferenceSettings = ClutterUtilities.GetInferenceSettingsConfiguration(session);
				this.ownsInferenceSettings = true;
			}
			else
			{
				this.inferenceSettings = inferenceSettings;
				this.ownsInferenceSettings = false;
			}
			this.localTimeZone = DateTimeHelper.GetUserTimeZoneOrUtc(this.session);
			this.MigrateNotificationType("ClutterReady", ClutterNotificationType.Invitation);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000C76FE File Offset: 0x000C58FE
		public static void SendNotification(ClutterNotificationType notificationType, DefaultFolderType folder, MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator)
		{
			NotificationManager.SendNotification(notificationType, folder, session, snapshot, frontEndLocator, null);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000C770C File Offset: 0x000C590C
		public static void SendNotification(ClutterNotificationType notificationType, DefaultFolderType folder, MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator, UserConfiguration inferenceSettings)
		{
			using (NotificationManager notificationManager = new NotificationManager(session, snapshot, frontEndLocator, inferenceSettings))
			{
				notificationManager.SendNotification(notificationType, folder);
				notificationManager.Save();
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000C7750 File Offset: 0x000C5950
		public void SendNotification(ClutterNotificationType notificationType, DefaultFolderType folder)
		{
			this.ThrowIfNone(notificationType);
			ClutterNotification notification = this.GetNotification(notificationType);
			using (MessageItem messageItem = notification.Compose(folder))
			{
				messageItem.Load(new PropertyDefinition[]
				{
					MessageItemSchema.InferenceMessageIdentifier
				});
				Guid? valueAsNullable = messageItem.GetValueAsNullable<Guid>(MessageItemSchema.InferenceMessageIdentifier);
				this.SetUserConfigurationProperty(this.GetScheduledTimePropertyKey(notificationType), null);
				this.SetUserConfigurationProperty(this.GetSentTimePropertyKey(notificationType), ExDateTime.UtcNow);
				this.SetUserConfigurationProperty(this.GetInternetMessageIdPropertyKey(notificationType), messageItem.InternetMessageId);
				this.SetUserConfigurationProperty(this.GetMessageGuidPropertyKey(notificationType), (valueAsNullable != null) ? valueAsNullable.Value.ToString() : null);
				this.SetUserConfigurationProperty(this.GetServerVersionPropertyKey(notificationType), "15.00.1497.010");
				NotificationManager.LogNotificationSentActivity(this.session, notificationType, messageItem, valueAsNullable, folder);
			}
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000C783C File Offset: 0x000C5A3C
		public void ScheduleNotification(ClutterNotificationType notificationType, int afterMinimumDays, DayOfWeek onDayOfWeek)
		{
			DayOfWeek onDayOfWeek2 = (onDayOfWeek - DayOfWeek.Monday < 0) ? (onDayOfWeek - 1 + 7) : (onDayOfWeek - 1);
			this.ScheduleNotification(notificationType, afterMinimumDays, onDayOfWeek2, TimeSpan.FromHours(12.0));
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000C7871 File Offset: 0x000C5A71
		public void ScheduleNotification(ClutterNotificationType notificationType, int afterMinimumDays, DayOfWeek onDayOfWeek, TimeSpan atTimeOfDay)
		{
			this.ScheduleNotification(notificationType, DateTimeHelper.GetFutureTimestamp(ExDateTime.UtcNow, afterMinimumDays, onDayOfWeek, atTimeOfDay, this.localTimeZone));
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000C788E File Offset: 0x000C5A8E
		public void ScheduleNotification(ClutterNotificationType notificationType, ExDateTime scheduledTime)
		{
			this.ThrowIfNone(notificationType);
			this.SetUserConfigurationProperty(this.GetScheduledTimePropertyKey(notificationType), scheduledTime);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000C78AA File Offset: 0x000C5AAA
		public void CancelScheduledNotification(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			this.SetUserConfigurationProperty(this.GetScheduledTimePropertyKey(notificationType), null);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000C78C4 File Offset: 0x000C5AC4
		public void CancelScheduledNotifications()
		{
			foreach (ClutterNotificationType clutterNotificationType in (ClutterNotificationType[])Enum.GetValues(typeof(ClutterNotificationType)))
			{
				if (clutterNotificationType != ClutterNotificationType.None)
				{
					this.CancelScheduledNotification(clutterNotificationType);
				}
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000C7904 File Offset: 0x000C5B04
		public ClutterNotificationType GetNextScheduledNotification(out ExDateTime scheduledTime)
		{
			ClutterNotificationType result = ClutterNotificationType.None;
			scheduledTime = ExDateTime.MaxValue;
			foreach (ClutterNotificationType clutterNotificationType in (ClutterNotificationType[])Enum.GetValues(typeof(ClutterNotificationType)))
			{
				if (clutterNotificationType != ClutterNotificationType.None)
				{
					ExDateTime? notificationScheduledTime = this.GetNotificationScheduledTime(clutterNotificationType);
					if (notificationScheduledTime != null && notificationScheduledTime.Value < scheduledTime)
					{
						result = clutterNotificationType;
						scheduledTime = notificationScheduledTime.Value;
					}
				}
			}
			return result;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C7984 File Offset: 0x000C5B84
		public ExDateTime? GetNotificationScheduledTime(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			return this.GetUserConfigurationProperty<ExDateTime?>(this.GetScheduledTimePropertyKey(notificationType), null);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000C79B0 File Offset: 0x000C5BB0
		public bool IsNotificationSent(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			return this.GetNotificationSentTime(notificationType) != null;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000C79D4 File Offset: 0x000C5BD4
		public ExDateTime? GetNotificationSentTime(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			return this.GetUserConfigurationProperty<ExDateTime?>(this.GetSentTimePropertyKey(notificationType), null);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000C79FE File Offset: 0x000C5BFE
		public string GetNotificationInternetMessageId(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			return this.GetUserConfigurationProperty<string>(this.GetInternetMessageIdPropertyKey(notificationType), null);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000C7A15 File Offset: 0x000C5C15
		public string GetNotificationMessageGuid(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			return this.GetUserConfigurationProperty<string>(this.GetMessageGuidPropertyKey(notificationType), null);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000C7A2C File Offset: 0x000C5C2C
		public string GetNotificationServerVersion(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			return this.GetUserConfigurationProperty<string>(this.GetServerVersionPropertyKey(notificationType), null);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000C7A43 File Offset: 0x000C5C43
		public void Save()
		{
			this.inferenceSettings.Save();
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000C7A50 File Offset: 0x000C5C50
		public void Dispose()
		{
			if (this.ownsInferenceSettings && this.inferenceSettings != null)
			{
				this.inferenceSettings.Dispose();
			}
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000C7A70 File Offset: 0x000C5C70
		private ClutterNotification GetNotification(ClutterNotificationType notificationType)
		{
			this.ThrowIfNone(notificationType);
			switch (notificationType)
			{
			case ClutterNotificationType.Invitation:
				return new InvitationNotification(this.session, this.snapshot, this.frontEndLocator);
			case ClutterNotificationType.OptedIn:
				return new OptInNotification(this.session, this.snapshot, this.frontEndLocator);
			case ClutterNotificationType.FirstReminder:
			case ClutterNotificationType.SecondReminder:
			case ClutterNotificationType.ThirdReminder:
				return new PeriodicNotification(this.session, this.snapshot, this.frontEndLocator);
			case ClutterNotificationType.AutoEnablementNotice:
				return new AutoEnablementNotice(this.session, this.snapshot, this.frontEndLocator);
			default:
				throw new ArgumentException(string.Format("Unknown clutter notification type: {0}", notificationType), "notificationType");
			}
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000C7B24 File Offset: 0x000C5D24
		private static void LogNotificationSentActivity(MailboxSession session, ClutterNotificationType notificationType, MessageItem message, Guid? messageGuid, DefaultFolderType folder)
		{
			if (session.ActivitySession != null)
			{
				Dictionary<string, string> messageProperties = new Dictionary<string, string>
				{
					{
						"NotificationType",
						notificationType.ToString()
					},
					{
						"InternetMessageId",
						message.InternetMessageId
					},
					{
						"MessageGuid",
						(messageGuid != null) ? messageGuid.Value.ToString() : string.Empty
					},
					{
						"CreationFolder",
						folder.ToString()
					}
				};
				session.ActivitySession.CaptureClutterNotificationSent(message.InternalObjectId, messageProperties);
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000C7BC4 File Offset: 0x000C5DC4
		private string GetScheduledTimePropertyKey(ClutterNotificationType notificationType)
		{
			return this.GetPropertyKey(notificationType, "ScheduledTime");
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C7BD2 File Offset: 0x000C5DD2
		private string GetSentTimePropertyKey(ClutterNotificationType notificationType)
		{
			return this.GetPropertyKey(notificationType, "SentTime");
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C7BE0 File Offset: 0x000C5DE0
		private string GetInternetMessageIdPropertyKey(ClutterNotificationType notificationType)
		{
			return this.GetPropertyKey(notificationType, "InternetMessageId");
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000C7BEE File Offset: 0x000C5DEE
		private string GetMessageGuidPropertyKey(ClutterNotificationType notificationType)
		{
			return this.GetPropertyKey(notificationType, "MessageGuid");
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000C7BFC File Offset: 0x000C5DFC
		private string GetServerVersionPropertyKey(ClutterNotificationType notificationType)
		{
			return this.GetPropertyKey(notificationType, "ServerVersion");
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000C7C0A File Offset: 0x000C5E0A
		private string GetPropertyKey(ClutterNotificationType notificationType, string propertySuffix)
		{
			this.ThrowIfNone(notificationType);
			return string.Format("{0}.{1}", notificationType.ToString(), propertySuffix);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C7C2C File Offset: 0x000C5E2C
		private void MigrateNotificationType(string oldNotificationName, ClutterNotificationType notificationType)
		{
			this.MigrateProperty(oldNotificationName, notificationType.ToString(), "SentTime");
			this.MigrateProperty(oldNotificationName, notificationType.ToString(), "InternetMessageId");
			this.MigrateProperty(oldNotificationName, notificationType.ToString(), "MessageGuid");
			this.MigrateProperty(oldNotificationName, notificationType.ToString(), "ServerVersion");
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000C7C98 File Offset: 0x000C5E98
		private void MigrateProperty(string oldPrefix, string newPrefix, string suffix)
		{
			string property = string.Format("{0}.{1}", oldPrefix, suffix);
			string property2 = string.Format("{0}.{1}", newPrefix, suffix);
			object userConfigurationProperty = this.GetUserConfigurationProperty(property);
			object userConfigurationProperty2 = this.GetUserConfigurationProperty(property2);
			if (userConfigurationProperty != null && userConfigurationProperty2 == null)
			{
				this.SetUserConfigurationProperty(property, null);
				this.SetUserConfigurationProperty(property2, userConfigurationProperty);
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000C7CE8 File Offset: 0x000C5EE8
		private T GetUserConfigurationProperty<T>(string property, T defaultValue)
		{
			object userConfigurationProperty = this.GetUserConfigurationProperty(property);
			if (userConfigurationProperty != null && userConfigurationProperty is T)
			{
				return (T)((object)userConfigurationProperty);
			}
			return defaultValue;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000C7D10 File Offset: 0x000C5F10
		private object GetUserConfigurationProperty(string property)
		{
			if (!this.inferenceSettings.GetDictionary().Contains(property))
			{
				return null;
			}
			return this.inferenceSettings.GetDictionary()[property];
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000C7D38 File Offset: 0x000C5F38
		private void SetUserConfigurationProperty(string property, object value)
		{
			if (value == null)
			{
				this.inferenceSettings.GetDictionary().Remove(property);
				return;
			}
			this.inferenceSettings.GetDictionary()[property] = value;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000C7D61 File Offset: 0x000C5F61
		private void ThrowIfNone(ClutterNotificationType notificationType)
		{
			if (notificationType == ClutterNotificationType.None)
			{
				throw new ArgumentException("ClutterNotificationType.None cannot have properties", "notificationType");
			}
		}

		// Token: 0x04001A6B RID: 6763
		public const string ScheduledTimeSuffix = "ScheduledTime";

		// Token: 0x04001A6C RID: 6764
		public const string SentTimeSuffix = "SentTime";

		// Token: 0x04001A6D RID: 6765
		public const string InternetMessageIdSuffix = "InternetMessageId";

		// Token: 0x04001A6E RID: 6766
		public const string MessageGuidSuffix = "MessageGuid";

		// Token: 0x04001A6F RID: 6767
		public const string ServerVersionSuffix = "ServerVersion";

		// Token: 0x04001A70 RID: 6768
		private readonly MailboxSession session;

		// Token: 0x04001A71 RID: 6769
		private readonly VariantConfigurationSnapshot snapshot;

		// Token: 0x04001A72 RID: 6770
		private readonly IFrontEndLocator frontEndLocator;

		// Token: 0x04001A73 RID: 6771
		private readonly ExTimeZone localTimeZone;

		// Token: 0x04001A74 RID: 6772
		private readonly UserConfiguration inferenceSettings;

		// Token: 0x04001A75 RID: 6773
		private readonly bool ownsInferenceSettings;
	}
}

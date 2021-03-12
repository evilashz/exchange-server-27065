using System;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B07 RID: 2823
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PushNotificationSubscriptionTableEntry
	{
		// Token: 0x06006693 RID: 26259 RVA: 0x001B32EC File Offset: 0x001B14EC
		public static bool IsSubscriptionDisabled(byte[] subscriptionOption)
		{
			return subscriptionOption == null || subscriptionOption.Length == 0 || PushNotificationSubscriptionTableEntry.IsSubscriptionDisabled((PushNotificationSubscriptionOption)subscriptionOption[0]);
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x001B3300 File Offset: 0x001B1500
		public static bool IsSubscriptionDisabled(PushNotificationSubscriptionOption subscriptionOption)
		{
			return subscriptionOption == PushNotificationSubscriptionOption.NoSubscription;
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x001B3306 File Offset: 0x001B1506
		public static bool IsEmailSubscriptionEnabled(PushNotificationSubscriptionOption subscriptionOption)
		{
			return (subscriptionOption & PushNotificationSubscriptionOption.Email) == PushNotificationSubscriptionOption.Email;
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x001B330E File Offset: 0x001B150E
		public static bool IsSuppressNotificationsWhenOofEnabled(PushNotificationSubscriptionOption subscriptionOption)
		{
			return (subscriptionOption & PushNotificationSubscriptionOption.SuppressNotificationsWhenOof) == PushNotificationSubscriptionOption.SuppressNotificationsWhenOof;
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x001B3318 File Offset: 0x001B1518
		public static bool IsBackgroundSyncEnabled(PushNotificationSubscriptionOption subscriptionOption)
		{
			return (subscriptionOption & PushNotificationSubscriptionOption.BackgroundSync) == PushNotificationSubscriptionOption.BackgroundSync;
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x001B3322 File Offset: 0x001B1522
		public void DisableSubscriptionOnMailboxTable(IMailboxSession session)
		{
			this.UpdateSubscriptionOnMailboxTable(session, PushNotificationSubscriptionOption.NoSubscription);
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x001B332C File Offset: 0x001B152C
		public void UpdateSubscriptionOnMailboxTable(IMailboxSession session, PushNotificationSubscription subscription)
		{
			this.UpdateSubscriptionOnMailboxTable(session, subscription.GetSubscriptionOption());
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x001B333C File Offset: 0x001B153C
		public virtual PushNotificationSubscriptionOption ReadSubscriptionOnMailboxTable(IMailboxSession session)
		{
			session.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.PushNotificationSubscriptionType
			});
			object obj = session.Mailbox.TryGetProperty(MailboxSchema.PushNotificationSubscriptionType);
			byte[] array = obj as byte[];
			if (!(obj is PropertyError) && !PushNotificationSubscriptionTableEntry.IsSubscriptionDisabled(array))
			{
				return (PushNotificationSubscriptionOption)array[0];
			}
			return PushNotificationSubscriptionOption.NoSubscription;
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x001B3394 File Offset: 0x001B1594
		public virtual void UpdateSubscriptionOnMailboxTable(IMailboxSession session, PushNotificationSubscriptionOption subscriptionOption)
		{
			session.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.PushNotificationSubscriptionType
			});
			session.Mailbox[MailboxSchema.PushNotificationSubscriptionType] = new byte[]
			{
				(byte)subscriptionOption
			};
			session.Mailbox.Save();
			session.Mailbox.Load();
		}

		// Token: 0x04003A2E RID: 14894
		public const int SubscriptionTableEntryArrayLength = 1;
	}
}

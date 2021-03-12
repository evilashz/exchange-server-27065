using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B6 RID: 950
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CalendarUpgrade
	{
		// Token: 0x06002B6E RID: 11118 RVA: 0x000AD200 File Offset: 0x000AB400
		public static CalendarUpgrade.CalendarUpgradeStatus MarkMailboxForUpgrade(IMailboxSession session, IXSOFactory xsoFactory)
		{
			ExAssert.RetailAssert(session != null, "session is null");
			ExAssert.RetailAssert(xsoFactory != null, "xsoFactory is null");
			VariantConfigurationSnapshot configuration = session.MailboxOwner.GetConfiguration();
			if (!configuration.DataStorage.CalendarUpgrade.Enabled)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Not flighted user", session.MailboxGuid);
				return CalendarUpgrade.CalendarUpgradeStatus.NotFlightedUser;
			}
			if (!CalendarUpgrade.IsInterestingMailboxType(session.MailboxOwner.RecipientTypeDetails))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Not user mailbox", session.MailboxGuid);
				return CalendarUpgrade.CalendarUpgradeStatus.NotUserMailbox;
			}
			IXSOMailbox mailbox = session.Mailbox;
			if (!CalendarUpgrade.IsMailboxActive(mailbox))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Inactive mailbox", session.MailboxGuid);
				return CalendarUpgrade.CalendarUpgradeStatus.InactiveMailbox;
			}
			if (mailbox.GetValueOrDefault<int?>(MailboxSchema.ItemsPendingUpgrade, null) != null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Already marked for upgrade", session.MailboxGuid);
				return CalendarUpgrade.CalendarUpgradeStatus.AlreadyMarkedForUpgrade;
			}
			int? num = null;
			int? num2 = null;
			CalendarUpgrade.GetCalendarFolderProperties(session, xsoFactory, out num, out num2);
			if (num != null && num == 1)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Upgrade complete", session.MailboxGuid);
				return CalendarUpgrade.CalendarUpgradeStatus.UpgradeComplete;
			}
			int minCalendarItemsForUpgrade = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).DataStorage.CalendarUpgradeSettings.MinCalendarItemsForUpgrade;
			if (num2 == null || num2 < minCalendarItemsForUpgrade)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Not enough calendar items", session.MailboxGuid);
				return CalendarUpgrade.CalendarUpgradeStatus.NotEnoughCalendarItems;
			}
			session.Mailbox.SetOrDeleteProperty(MailboxSchema.ItemsPendingUpgrade, num2);
			session.Mailbox.Save();
			ExTraceGlobals.StorageTracer.TraceDebug<Guid>(0L, "Mailbox {0}: Marked for upgrade", session.MailboxGuid);
			return CalendarUpgrade.CalendarUpgradeStatus.MarkedForUpgrade;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000AD3E8 File Offset: 0x000AB5E8
		public static bool IsMailboxActive(IXSOMailbox mbx)
		{
			ExDateTime? valueOrDefault = mbx.GetValueOrDefault<ExDateTime?>(MailboxSchema.LastLogonTime, null);
			return CalendarUpgrade.IsMailboxActive(valueOrDefault);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000AD410 File Offset: 0x000AB610
		public static bool IsMailboxActive(ExDateTime? lastLogonTime)
		{
			return lastLogonTime != null && !(lastLogonTime < ExDateTime.UtcNow - CalendarUpgrade.AllowedInactivity);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000AD458 File Offset: 0x000AB658
		private static bool IsInterestingMailboxType(RecipientTypeDetails recipientTypeDetails)
		{
			return recipientTypeDetails == RecipientTypeDetails.UserMailbox || recipientTypeDetails == RecipientTypeDetails.LinkedMailbox || recipientTypeDetails == RecipientTypeDetails.GroupMailbox;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000AD474 File Offset: 0x000AB674
		private static void GetCalendarFolderProperties(IMailboxSession session, IXSOFactory xsoFactory, out int? calendarFolderVersion, out int? calendarItemCount)
		{
			calendarFolderVersion = null;
			calendarItemCount = null;
			using (IFolder folder = xsoFactory.BindToFolder(session, DefaultFolderType.Calendar))
			{
				calendarFolderVersion = folder.GetValueOrDefault<int?>(FolderSchema.CalendarFolderVersion, null);
				calendarItemCount = folder.GetValueOrDefault<int?>(FolderSchema.ItemCount, null);
			}
		}

		// Token: 0x04001846 RID: 6214
		private static readonly TimeSpan AllowedInactivity = TimeSpan.FromDays(30.0);

		// Token: 0x020003B7 RID: 951
		public enum CalendarUpgradeStatus
		{
			// Token: 0x04001848 RID: 6216
			NotFlightedUser,
			// Token: 0x04001849 RID: 6217
			NotUserMailbox,
			// Token: 0x0400184A RID: 6218
			InactiveMailbox,
			// Token: 0x0400184B RID: 6219
			AlreadyMarkedForUpgrade,
			// Token: 0x0400184C RID: 6220
			UpgradeComplete,
			// Token: 0x0400184D RID: 6221
			NotEnoughCalendarItems,
			// Token: 0x0400184E RID: 6222
			MarkedForUpgrade
		}
	}
}

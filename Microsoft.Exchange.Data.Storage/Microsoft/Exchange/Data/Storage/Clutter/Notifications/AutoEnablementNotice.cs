using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter.Notifications
{
	// Token: 0x02000446 RID: 1094
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutoEnablementNotice : ClutterNotification
	{
		// Token: 0x060030EA RID: 12522 RVA: 0x000C8B30 File Offset: 0x000C6D30
		static AutoEnablementNotice()
		{
			ExTimeZone pacificStandardTimeZone = null;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName("Pacific Standard Time", out pacificStandardTimeZone))
			{
				pacificStandardTimeZone = ExTimeZone.UtcTimeZone;
			}
			AutoEnablementNotice.PacificStandardTimeZone = pacificStandardTimeZone;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000C8B68 File Offset: 0x000C6D68
		public AutoEnablementNotice(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator) : base(session, snapshot, frontEndLocator)
		{
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000C8B73 File Offset: 0x000C6D73
		protected override LocalizedString GetSubject()
		{
			return ClientStrings.ClutterNotificationAutoEnablementNoticeSubject;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000C8B7A File Offset: 0x000C6D7A
		protected override Importance GetImportance()
		{
			return Importance.High;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000C8B80 File Offset: 0x000C6D80
		protected override void WriteMessageProperties(MessageItem message)
		{
			ExTimeZone userTimeZoneOrDefault = DateTimeHelper.GetUserTimeZoneOrDefault(base.Session, AutoEnablementNotice.PacificStandardTimeZone);
			ExDateTime futureTimestamp = DateTimeHelper.GetFutureTimestamp(ExDateTime.UtcNow, 0, DayOfWeek.Tuesday, TimeSpan.FromHours(12.0), userTimeZoneOrDefault);
			message.SetFlag(ClientStrings.RequestedActionFollowUp.ToString(base.Culture), new ExDateTime?(ExDateTime.UtcNow), new ExDateTime?(futureTimestamp));
			message.Reminder.DueBy = new ExDateTime?(futureTimestamp);
			message.Reminder.IsSet = true;
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000C8C00 File Offset: 0x000C6E00
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			base.WriteHeader(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeHeader);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeIntro, 30U);
			base.WriteSubHeader(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeWeCallIt);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeHowItWorks);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeYoullBeEnabed(AutoEnablementNotice.OptOutUrl));
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeItsAutomatic, 20U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365Closing, 0U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365DisplayName);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationAutoEnablementNoticeLearnMore(ClutterNotification.AnnouncementUrl, ClutterNotification.LearnMoreUrl));
		}

		// Token: 0x04001A93 RID: 6803
		public static readonly string OptOutUrl = "http://aka.ms/ItsCrazyButDontEnableMeForClutter";

		// Token: 0x04001A94 RID: 6804
		private static readonly ExTimeZone PacificStandardTimeZone;
	}
}

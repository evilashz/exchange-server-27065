using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter.Notifications
{
	// Token: 0x02000447 RID: 1095
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvitationNotification : ClutterNotification
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x000C8C8D File Offset: 0x000C6E8D
		public InvitationNotification(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator) : base(session, snapshot, frontEndLocator)
		{
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000C8C98 File Offset: 0x000C6E98
		protected override LocalizedString GetSubject()
		{
			return ClientStrings.ClutterNotificationInvitationSubject;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000C8CA0 File Offset: 0x000C6EA0
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			base.WriteHeader(streamWriter, ClientStrings.ClutterNotificationInvitationHeader);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationInvitationIntro, 30U);
			base.WriteSubHeader(streamWriter, ClientStrings.ClutterNotificationInvitationWeCallIt);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationInvitationHowItWorks);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationInvitationItsAutomatic);
			base.WriteTurnOnInstructions(streamWriter);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationInvitationIfYouDontLikeIt, 0U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationInvitationLearnMore(ClutterNotification.LearnMoreUrl));
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationInvitationO365Helps, 20U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365Closing, 0U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365DisplayName);
		}
	}
}

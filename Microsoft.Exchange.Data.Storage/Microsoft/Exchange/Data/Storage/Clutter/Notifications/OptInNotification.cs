using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter.Notifications
{
	// Token: 0x02000448 RID: 1096
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OptInNotification : ClutterNotification
	{
		// Token: 0x060030F3 RID: 12531 RVA: 0x000C8D37 File Offset: 0x000C6F37
		public OptInNotification(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator) : base(session, snapshot, frontEndLocator)
		{
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000C8D42 File Offset: 0x000C6F42
		protected override LocalizedString GetSubject()
		{
			return ClientStrings.ClutterNotificationOptInSubject;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000C8D4C File Offset: 0x000C6F4C
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			string clutterFolderName = ClientStrings.ClutterFolderName.ToString(base.Culture);
			base.WriteHeader(streamWriter, ClientStrings.ClutterNotificationOptInHeader);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationOptInIntro(clutterFolderName));
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationOptInHowToTrain(clutterFolderName));
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationOptInFeedback(ClutterNotification.FeedbackMailtoUrl));
			base.WriteTurnOffInstructions(streamWriter);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationOptInPrivacy, 0U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationOptInLearnMore(ClutterNotification.LearnMoreUrl), 20U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365Closing, 0U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365DisplayName);
		}
	}
}

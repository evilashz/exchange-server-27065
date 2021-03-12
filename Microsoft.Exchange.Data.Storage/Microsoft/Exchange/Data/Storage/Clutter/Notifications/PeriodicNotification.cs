using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter.Notifications
{
	// Token: 0x02000449 RID: 1097
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PeriodicNotification : ClutterNotification
	{
		// Token: 0x060030F6 RID: 12534 RVA: 0x000C8DE4 File Offset: 0x000C6FE4
		public PeriodicNotification(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator) : base(session, snapshot, frontEndLocator)
		{
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000C8DEF File Offset: 0x000C6FEF
		protected override LocalizedString GetSubject()
		{
			return ClientStrings.ClutterNotificationPeriodicSubject;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000C8DF8 File Offset: 0x000C6FF8
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			string clutterFolderName = ClientStrings.ClutterFolderName.ToString(base.Culture);
			base.WriteHeader(streamWriter, ClientStrings.ClutterNotificationPeriodicHeader);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationPeriodicIntro(clutterFolderName));
			base.WriteSurveyInstructions(streamWriter);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationPeriodicCheckBack, 20U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationO365DisplayName, 0U);
			base.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationPeriodicLearnMore(ClutterNotification.LearnMoreUrl));
		}
	}
}

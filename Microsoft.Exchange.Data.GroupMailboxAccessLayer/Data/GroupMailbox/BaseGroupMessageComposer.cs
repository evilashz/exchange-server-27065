using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BaseGroupMessageComposer : IMessageComposer
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A8 RID: 424
		protected abstract ADUser[] Recipients { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A9 RID: 425
		protected abstract Participant FromParticipant { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AA RID: 426
		protected abstract string Subject { get; }

		// Token: 0x060001AB RID: 427 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public virtual void WriteToMessage(MessageItem message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			foreach (ADUser aduser in this.Recipients)
			{
				BaseGroupMessageComposer.Tracer.TraceDebug<string, string, SmtpAddress>((long)this.GetHashCode(), "BaseGroupMessageComposer.WriteMessage: Adding recipient with ExternalId:{0}. Legacy DN:{1}, PrimarySmtpAddress: {2}", aduser.ExternalDirectoryObjectId, aduser.LegacyExchangeDN, aduser.PrimarySmtpAddress);
				Participant participant = new Participant(aduser.DisplayName, aduser.PrimarySmtpAddress.ToString(), "SMTP");
				message.Recipients.Add(participant, RecipientItemType.To);
			}
			message.AutoResponseSuppress = AutoResponseSuppress.All;
			message.From = this.FromParticipant;
			message.Subject = this.Subject;
			this.SetAdditionalMessageProperties(message);
			using (Stream stream = message.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.Unicode)))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode))
				{
					this.WriteMessageBody(streamWriter);
				}
			}
			this.AddAttachments(message);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C428 File Offset: 0x0000A628
		protected static CultureInfo GetPreferredCulture(params ADUser[] users)
		{
			foreach (ADUser aduser in users)
			{
				CultureInfo cultureInfo = aduser.Languages.FirstOrDefault((CultureInfo language) => BaseGroupMessageComposer.SupportedClientLanguages.Contains(language));
				if (cultureInfo != null)
				{
					BaseGroupMessageComposer.Tracer.TraceDebug<CultureInfo, string>(0L, "BaseGroupMessageComposer.GetPreferredCulture: language {0} is supported by {1}.", cultureInfo, aduser.Guid.ToString());
					return cultureInfo;
				}
			}
			if (BaseGroupMessageComposer.SupportedClientLanguages.Contains(CultureInfo.CurrentCulture))
			{
				BaseGroupMessageComposer.Tracer.TraceDebug<CultureInfo>(0L, "BaseGroupMessageComposer.GetPreferredCulture: no language of the provided users is supported - returning current culture: {0}.", CultureInfo.CurrentCulture);
				return CultureInfo.CurrentCulture;
			}
			BaseGroupMessageComposer.Tracer.TraceDebug<CultureInfo>(0L, "BaseGroupMessageComposer.GetPreferredCulture: Couldn't find a supported language, returning default culture: {0}.", BaseGroupMessageComposer.ProductDefaultCulture);
			return BaseGroupMessageComposer.ProductDefaultCulture;
		}

		// Token: 0x060001AD RID: 429
		protected abstract void WriteMessageBody(StreamWriter streamWriter);

		// Token: 0x060001AE RID: 430
		protected abstract void SetAdditionalMessageProperties(MessageItem message);

		// Token: 0x060001AF RID: 431 RVA: 0x0000C4EF File Offset: 0x0000A6EF
		protected virtual void AddAttachments(MessageItem message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			WelcomeMessageBodyBuilder.CalendarIcon.AddImageAsAttachment(message);
			WelcomeMessageBodyBuilder.ConversationIcon.AddImageAsAttachment(message);
			WelcomeMessageBodyBuilder.DocumentIcon.AddImageAsAttachment(message);
		}

		// Token: 0x040000E0 RID: 224
		protected static readonly Trace Tracer = ExTraceGlobals.GroupEmailNotificationHandlerTracer;

		// Token: 0x040000E1 RID: 225
		private static readonly CultureInfo ProductDefaultCulture = new CultureInfo("en-US");

		// Token: 0x040000E2 RID: 226
		private static readonly HashSet<CultureInfo> SupportedClientLanguages = new HashSet<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter.Notifications
{
	// Token: 0x02000445 RID: 1093
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ClutterNotification
	{
		// Token: 0x060030CD RID: 12493 RVA: 0x000C83C0 File Offset: 0x000C65C0
		protected ClutterNotification(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("snapshot", snapshot);
			if (frontEndLocator == null)
			{
				InferenceDiagnosticsLog.Log("ClutterNotification.ctor", "FrontEndLocator was not provided (it must be dependency injected). Using default OWA path.");
			}
			this.Session = session;
			this.Snapshot = snapshot;
			this.FrontEndLocator = frontEndLocator;
			this.Culture = ClutterNotification.GetPreferredCulture(this.Session);
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000C8421 File Offset: 0x000C6621
		// (set) Token: 0x060030CF RID: 12495 RVA: 0x000C8429 File Offset: 0x000C6629
		private protected MailboxSession Session { protected get; private set; }

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000C8432 File Offset: 0x000C6632
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x000C843A File Offset: 0x000C663A
		private protected VariantConfigurationSnapshot Snapshot { protected get; private set; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000C8443 File Offset: 0x000C6643
		// (set) Token: 0x060030D3 RID: 12499 RVA: 0x000C844B File Offset: 0x000C664B
		private protected IFrontEndLocator FrontEndLocator { protected get; private set; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000C8454 File Offset: 0x000C6654
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x000C845C File Offset: 0x000C665C
		private protected CultureInfo Culture { protected get; private set; }

		// Token: 0x060030D6 RID: 12502 RVA: 0x000C8468 File Offset: 0x000C6668
		public MessageItem Compose(DefaultFolderType folder)
		{
			bool flag = false;
			MessageItem messageItem = null;
			MessageItem result;
			try
			{
				messageItem = MessageItem.Create(this.Session, this.Session.GetDefaultFolderId(folder));
				Participant participant = new Participant(this.Session.MailboxOwner);
				messageItem.Recipients.Add(participant, RecipientItemType.To);
				messageItem.AutoResponseSuppress = AutoResponseSuppress.All;
				messageItem.From = this.GetFrom();
				messageItem.Subject = this.GetSubject().ToString(this.Culture);
				messageItem.IsDraft = false;
				messageItem.IsRead = false;
				messageItem.Importance = this.GetImportance();
				messageItem[MessageItemSchema.InferenceMessageIdentifier] = Guid.NewGuid();
				this.WriteMessageProperties(messageItem);
				using (Stream stream = messageItem.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.Unicode)))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode))
					{
						streamWriter.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
						streamWriter.Write("<html>");
						streamWriter.Write("<head>");
						streamWriter.Write("<meta name='ProgId' content='Word.Document'>");
						streamWriter.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=us-ascii\">");
						streamWriter.Write("<meta content=\"text/html; charset=US-ASCII\">");
						streamWriter.Write("</head>");
						streamWriter.Write("<body>");
						this.WriteMessageBody(streamWriter);
						streamWriter.Write("</body>");
						streamWriter.Write("</html>");
					}
				}
				messageItem.Save(SaveMode.NoConflictResolutionForceSave);
				messageItem.Load();
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x060030D7 RID: 12503
		protected abstract LocalizedString GetSubject();

		// Token: 0x060030D8 RID: 12504 RVA: 0x000C8644 File Offset: 0x000C6844
		protected virtual Importance GetImportance()
		{
			return Importance.Normal;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000C8647 File Offset: 0x000C6847
		protected virtual void WriteMessageProperties(MessageItem message)
		{
		}

		// Token: 0x060030DA RID: 12506
		protected abstract void WriteMessageBody(StreamWriter streamWriter);

		// Token: 0x060030DB RID: 12507 RVA: 0x000C864C File Offset: 0x000C684C
		protected void WriteHeader(StreamWriter streamWriter, LocalizedString text)
		{
			streamWriter.Write("<div style='font-family: ");
			streamWriter.Write(ClientStrings.ClutterNotificationHeaderFont.ToString(this.Culture));
			streamWriter.Write("; font-size: 42px; color: #0072C6; line-height: normal; margin-top: 0; margin-bottom: 20px;'>");
			streamWriter.Write(text.ToString(this.Culture));
			streamWriter.Write("</div>");
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000C86A8 File Offset: 0x000C68A8
		protected void WriteSubHeader(StreamWriter streamWriter, LocalizedString text)
		{
			streamWriter.Write("<div style='font-family: ");
			streamWriter.Write(ClientStrings.ClutterNotificationBodyFont.ToString(this.Culture));
			streamWriter.Write("; font-size: 21px; color: #323232; line-height: normal; margin-top: 0; margin-bottom: 10px;'>");
			streamWriter.Write(text.ToString(this.Culture));
			streamWriter.Write("</div>");
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000C8702 File Offset: 0x000C6902
		protected void WriteParagraph(StreamWriter streamWriter, LocalizedString text)
		{
			this.WriteParagraph(streamWriter, text, 10U);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000C8710 File Offset: 0x000C6910
		protected void WriteParagraph(StreamWriter streamWriter, LocalizedString text, uint marginBottom)
		{
			streamWriter.Write("<div style='font-family: ");
			streamWriter.Write(ClientStrings.ClutterNotificationBodyFont.ToString(this.Culture));
			streamWriter.Write("; font-size: 14px; color: #323232; line-height: 20px; margin-top: 0; margin-bottom: ");
			streamWriter.Write(marginBottom.ToString());
			streamWriter.Write("px;'>");
			streamWriter.Write(text.ToString(this.Culture));
			streamWriter.Write("</div>");
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000C8782 File Offset: 0x000C6982
		protected void WriteTurnOnInstructions(StreamWriter streamWriter)
		{
			this.WriteEnablementInstructions(streamWriter, true);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000C878C File Offset: 0x000C698C
		protected void WriteTurnOffInstructions(StreamWriter streamWriter)
		{
			this.WriteEnablementInstructions(streamWriter, false);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000C8796 File Offset: 0x000C6996
		protected void WriteSurveyInstructions(StreamWriter streamWriter)
		{
			this.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationTakeSurveyDeepLink(this.GetOptionsDeepLink()));
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000C87AC File Offset: 0x000C69AC
		protected void WriteSteps(StreamWriter streamWriter, params LocalizedString[] steps)
		{
			streamWriter.Write("<table style='font-family: ");
			streamWriter.Write(ClientStrings.ClutterNotificationBodyFont.ToString(this.Culture));
			streamWriter.Write("; font-size: 14px; margin-top: 0; margin-bottom: 10px; margin-left: 20px;' cellpadding='0' cellspacing='0' border='0'>");
			for (int i = 0; i < steps.Length; i++)
			{
				this.WriteStep(streamWriter, i + 1, steps[i], (i != steps.Length - 1) ? 0U : 10U);
			}
			streamWriter.Write("</table>");
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000C8824 File Offset: 0x000C6A24
		protected Uri GetOwaUrl()
		{
			if (this.FrontEndLocator != null)
			{
				return this.FrontEndLocator.GetOwaUrl(this.Session.MailboxOwner);
			}
			return new Uri(ClutterNotification.Office365OwaUrl);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000C8850 File Offset: 0x000C6A50
		protected string GetOptionsDeepLink()
		{
			UriBuilder uriBuilder = new UriBuilder(this.GetOwaUrl());
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
			nameValueCollection[ClutterNotification.OwaOptionsDeepLinkParam] = ClutterNotification.OwaClutterOptionsDeepLinkPath;
			if (!this.Snapshot.OwaClient.Options.Enabled)
			{
				nameValueCollection[ClutterNotification.OwaLayoutParam] = ClutterNotification.OwaLayoutMouseValue;
			}
			uriBuilder.Query = nameValueCollection.ToString();
			return uriBuilder.Uri.AbsoluteUri;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000C88C6 File Offset: 0x000C6AC6
		private void WriteEnablementInstructions(StreamWriter streamWriter, bool turnOn)
		{
			if (turnOn)
			{
				this.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationEnableDeepLink(this.GetOptionsDeepLink()));
				return;
			}
			this.WriteParagraph(streamWriter, ClientStrings.ClutterNotificationDisableDeepLink(this.GetOptionsDeepLink()));
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000C88F0 File Offset: 0x000C6AF0
		private void WriteStep(StreamWriter streamWriter, int step, LocalizedString content, uint marginBottom)
		{
			streamWriter.Write("<tr><td style='font-family: ");
			streamWriter.Write(ClientStrings.ClutterNotificationBodyFont.ToString(this.Culture));
			streamWriter.Write("; font-size: 14px; color: #323232; line-height: 20px; margin-top: 0; margin-bottom: ");
			streamWriter.Write(marginBottom.ToString());
			streamWriter.Write("px; width: 35px; vertical-align: top;'>");
			streamWriter.Write(step.ToString(this.Culture));
			streamWriter.Write(".&nbsp;</td><td style='font-family: ");
			streamWriter.Write(ClientStrings.ClutterNotificationBodyFont.ToString(this.Culture));
			streamWriter.Write("; font-size: 14px; color: #323232; line-height: 20px; margin-top: 0; margin-bottom: ");
			streamWriter.Write(marginBottom.ToString());
			streamWriter.Write("px;'>");
			streamWriter.Write(content.ToString(this.Culture));
			streamWriter.Write("</td></tr>");
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000C89BC File Offset: 0x000C6BBC
		private Participant GetFrom()
		{
			return new Participant(ClientStrings.ClutterNotificationO365DisplayName.ToString(this.Culture), ClutterNotification.EmailFromSmtp, "SMTP");
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000C89EC File Offset: 0x000C6BEC
		private static CultureInfo GetPreferredCulture(MailboxSession session)
		{
			foreach (CultureInfo cultureInfo in session.MailboxOwner.PreferredCultures.Concat(new CultureInfo[]
			{
				session.InternalPreferedCulture
			}))
			{
				if (ClutterNotification.SupportedClientLanguages.Contains(cultureInfo))
				{
					return cultureInfo;
				}
			}
			if (ClutterNotification.SupportedClientLanguages.Contains(CultureInfo.CurrentCulture))
			{
				return CultureInfo.CurrentCulture;
			}
			InferenceDiagnosticsLog.Log("ClutterNotification.GetPreferredCulture", string.Format("No supported culture could be found for mailbox '{0}'. Falling back to default {1}.", session.MailboxGuid, ClutterNotification.ProductDefaultCulture.Name));
			return ClutterNotification.ProductDefaultCulture;
		}

		// Token: 0x04001A84 RID: 6788
		public static readonly string Office365OwaUrl = "http://outlook.office365.com/owa/";

		// Token: 0x04001A85 RID: 6789
		public static readonly string OwaOptionsDeepLinkParam = "path";

		// Token: 0x04001A86 RID: 6790
		public static readonly string AnnouncementUrl = "http://blogs.office.com/2014/03/31/the-evolution-of-email/";

		// Token: 0x04001A87 RID: 6791
		public static readonly string OwaClutterOptionsDeepLinkPath = "/options/clutter";

		// Token: 0x04001A88 RID: 6792
		public static readonly string OwaLayoutParam = "layout";

		// Token: 0x04001A89 RID: 6793
		public static readonly string OwaLayoutMouseValue = "mouse";

		// Token: 0x04001A8A RID: 6794
		public static readonly string LearnMoreUrl = "http://go.microsoft.com/fwlink/?LinkId=506974";

		// Token: 0x04001A8B RID: 6795
		public static readonly string FeedbackMailtoUrl = "mailto:ExClutterFeedback@microsoft.com?subject=Clutter%20feedback";

		// Token: 0x04001A8C RID: 6796
		private static readonly string EmailFromSmtp = "no-reply@office365.com";

		// Token: 0x04001A8D RID: 6797
		private static readonly CultureInfo ProductDefaultCulture = new CultureInfo("en-US");

		// Token: 0x04001A8E RID: 6798
		private static readonly HashSet<CultureInfo> SupportedClientLanguages = new HashSet<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));
	}
}

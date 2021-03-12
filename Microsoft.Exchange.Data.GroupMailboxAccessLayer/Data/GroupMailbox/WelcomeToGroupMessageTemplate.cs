using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WelcomeToGroupMessageTemplate : IMessageComposerBuilder
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000E57C File Offset: 0x0000C77C
		public WelcomeToGroupMessageTemplate(ADUser groupMailbox, IExchangePrincipal groupPrincipal, ADRecipient executingUser)
		{
			ArgumentValidator.ThrowIfNull("groupMailbox", groupMailbox);
			ArgumentValidator.ThrowIfNull("groupPrincipal", groupPrincipal);
			this.groupMailbox = groupMailbox;
			this.encodedGroupDescription = WelcomeToGroupMessageTemplate.GetGroupDescription(groupMailbox);
			this.encodedGroupDisplayName = AntiXssEncoder.HtmlEncode(groupMailbox.DisplayName, false);
			this.groupPrincipal = groupPrincipal;
			this.executingUser = executingUser;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		public string EncodedGroupDisplayName
		{
			get
			{
				return this.encodedGroupDisplayName;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public string EncodedGroupDescription
		{
			get
			{
				return this.encodedGroupDescription;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		public string GroupInboxUrl
		{
			get
			{
				return this.groupInboxUrl;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public string GroupCalendarUrl
		{
			get
			{
				return this.groupCalendarUrl;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		public string GroupSharePointUrl
		{
			get
			{
				return this.groupSharePointUrl;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000E600 File Offset: 0x0000C800
		public string SubscribeUrl
		{
			get
			{
				return this.subscribeUrl;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000E608 File Offset: 0x0000C808
		public string UnsubscribeUrl
		{
			get
			{
				return this.unsubscribeUrl;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000E610 File Offset: 0x0000C810
		public bool GroupIsAutoSubscribe
		{
			get
			{
				return this.Group.AutoSubscribeNewGroupMembers;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000E61D File Offset: 0x0000C81D
		public ADUser Group
		{
			get
			{
				return this.groupMailbox;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000E625 File Offset: 0x0000C825
		public ImageAttachment GroupPhoto
		{
			get
			{
				return this.groupPhoto;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000E62D File Offset: 0x0000C82D
		public ImageAttachment ExecutingUserPhoto
		{
			get
			{
				return this.executingUserPhoto;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000E635 File Offset: 0x0000C835
		public bool GroupHasPhoto
		{
			get
			{
				return this.GroupPhoto != null;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000E643 File Offset: 0x0000C843
		public bool ExecutingUserHasPhoto
		{
			get
			{
				return this.ExecutingUserPhoto != null;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000E651 File Offset: 0x0000C851
		public Participant EmailFrom
		{
			get
			{
				return this.emailFrom;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000E659 File Offset: 0x0000C859
		public Participant EmailSender
		{
			get
			{
				return this.emailSender;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000E661 File Offset: 0x0000C861
		public ADRecipient ExecutingUser
		{
			get
			{
				return this.executingUser;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000E669 File Offset: 0x0000C869
		public string EncodedExecutingUserDisplayName
		{
			get
			{
				return this.encodedExecutingUserDisplayName;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000E671 File Offset: 0x0000C871
		public IMessageComposer Build(ADUser recipient)
		{
			ArgumentValidator.ThrowIfNull("recipient", recipient);
			this.Initialize();
			return new WelcomeToGroupMessageComposer(this, recipient, this.groupMailbox);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000E694 File Offset: 0x0000C894
		private static ImageAttachment GetADThumbnailPhoto(ADRecipient adUser, string imageId, string imageName)
		{
			ImageAttachment result = null;
			if (adUser != null && adUser.ThumbnailPhoto != null && adUser.ThumbnailPhoto.Length > 0)
			{
				WelcomeToGroupMessageTemplate.Tracer.TraceDebug<ADRecipient>(0L, "WelcomeToGroupMessageTemplate.GetADThumbnailPhoto: Found thumbnail photo for {0}", adUser);
				result = new ImageAttachment(imageName, imageId, "image/jpeg", adUser.ThumbnailPhoto);
			}
			else
			{
				WelcomeToGroupMessageTemplate.Tracer.TraceDebug<ADRecipient>(0L, "WelcomeToGroupMessageTemplate.GetADThumbnailPhoto: No thumbnail photo found for {0}.", adUser);
			}
			return result;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		private static string GetGroupDescription(ADUser groupMailbox)
		{
			string text = string.Empty;
			if (groupMailbox.Description != null && groupMailbox.Description.Count > 0)
			{
				string input = groupMailbox.Description[0];
				text = AntiXssEncoder.HtmlEncode(input, false);
				WelcomeToGroupMessageTemplate.Tracer.TraceDebug<string, string>(0L, "WelcomeToGroupMessageTemplate.GetGroupDescription: Found description for Group: {0}. Description {1}.", groupMailbox.ExternalDirectoryObjectId, text);
			}
			else
			{
				WelcomeToGroupMessageTemplate.Tracer.TraceDebug<string>(0L, "WelcomeToGroupMessageTemplate.GetGroupDescription: Couldn't find description for Group: {0}.", text);
			}
			return text;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000E760 File Offset: 0x0000C960
		private static string GetCalendarUrlForGroupMailbox(string calendarUrl)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
			dictionary["src"] = "Mail";
			dictionary["to"] = "cal";
			dictionary["type"] = "MG";
			dictionary["exsvurl"] = "1";
			string text = WelcomeToGroupMessageTemplate.AddQueryParams(calendarUrl, dictionary);
			WelcomeToGroupMessageTemplate.Tracer.TraceDebug<string, string>(0L, "WelcomeToGroupMessageTemplate.GetCalendarUrlForGroupMailbox: Base URL: '{0}'. Calculated URL: '{1}'.", calendarUrl, text);
			return text;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		private static string GetInboxUrlForGroupMailbox(string inboxUrl)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
			dictionary["src"] = "Mail";
			dictionary["to"] = "conv";
			dictionary["type"] = "MG";
			dictionary["exsvurl"] = "1";
			string text = WelcomeToGroupMessageTemplate.AddQueryParams(inboxUrl, dictionary);
			WelcomeToGroupMessageTemplate.Tracer.TraceDebug<string, string>(0L, "WelcomeToGroupMessageTemplate.GetInboxUrlForGroupMailbox: Base URL: '{0}'. Calculated URL: '{1}'.", inboxUrl, text);
			return text;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000E840 File Offset: 0x0000CA40
		private static string AddQueryParams(string originalUrl, Dictionary<string, string> queryParams)
		{
			Uri uri = new Uri(originalUrl);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
			foreach (KeyValuePair<string, string> keyValuePair in queryParams)
			{
				nameValueCollection[keyValuePair.Key] = keyValuePair.Value;
			}
			return new UriBuilder(originalUrl)
			{
				Query = nameValueCollection.ToString()
			}.Uri.AbsoluteUri;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		private void Initialize()
		{
			MailboxUrls owaMailboxUrls = MailboxUrls.GetOwaMailboxUrls(this.groupPrincipal);
			EscalationLinkBuilder escalationLinkBuilder = new EscalationLinkBuilder(this.groupPrincipal, owaMailboxUrls);
			this.groupInboxUrl = WelcomeToGroupMessageTemplate.GetInboxUrlForGroupMailbox(owaMailboxUrls.InboxUrl);
			this.groupCalendarUrl = WelcomeToGroupMessageTemplate.GetCalendarUrlForGroupMailbox(owaMailboxUrls.CalendarUrl);
			this.subscribeUrl = escalationLinkBuilder.GetEscalationLink(EscalationLinkType.Subscribe);
			this.unsubscribeUrl = escalationLinkBuilder.GetEscalationLink(EscalationLinkType.Unsubscribe);
			this.groupSharePointUrl = (new SharePointUrlResolver(this.groupMailbox).GetDocumentsUrl() ?? string.Empty);
			this.groupPhoto = WelcomeToGroupMessageTemplate.GetADThumbnailPhoto(this.groupMailbox, WelcomeMessageBodyData.GroupPhotoImageId, WelcomeMessageBodyData.GroupPhotoImageId + ".jpg");
			this.executingUserPhoto = WelcomeToGroupMessageTemplate.GetADThumbnailPhoto(this.executingUser, WelcomeMessageBodyData.UserPhotoImageId, WelcomeMessageBodyData.UserPhotoImageId + ".jpg");
			if (this.executingUser != null)
			{
				WelcomeToGroupMessageTemplate.Tracer.TraceDebug((long)this.GetHashCode(), "WelcomeToGroupMessageTemplate.WelcomeToGroupMessageTemplate: Executing user is known. Setting Sender=Group, From-executingUser.");
				this.emailSender = new Participant(this.groupMailbox);
				this.emailFrom = new Participant(this.executingUser);
				this.encodedExecutingUserDisplayName = AntiXssEncoder.HtmlEncode(this.emailFrom.DisplayName, false);
				return;
			}
			WelcomeToGroupMessageTemplate.Tracer.TraceDebug((long)this.GetHashCode(), "WelcomeToGroupMessageTemplate.WelcomeToGroupMessageTemplate: Executing user is unknown. From-executingUser, sender won't be set.");
			this.emailFrom = new Participant(this.groupMailbox);
		}

		// Token: 0x04000128 RID: 296
		private const string SaveUrlOnLogoffParameter = "exsvurl";

		// Token: 0x04000129 RID: 297
		private const string SourceParameter = "src";

		// Token: 0x0400012A RID: 298
		private const string ToParameter = "to";

		// Token: 0x0400012B RID: 299
		private const string TypeParameter = "type";

		// Token: 0x0400012C RID: 300
		private const string Mail = "Mail";

		// Token: 0x0400012D RID: 301
		private const string SharePoint = "sp";

		// Token: 0x0400012E RID: 302
		private const string Calendar = "cal";

		// Token: 0x0400012F RID: 303
		private const string Conversations = "conv";

		// Token: 0x04000130 RID: 304
		private const string ModernGroup = "MG";

		// Token: 0x04000131 RID: 305
		private static readonly Trace Tracer = ExTraceGlobals.GroupEmailNotificationHandlerTracer;

		// Token: 0x04000132 RID: 306
		private readonly string encodedGroupDisplayName;

		// Token: 0x04000133 RID: 307
		private readonly string encodedGroupDescription;

		// Token: 0x04000134 RID: 308
		private readonly ADUser groupMailbox;

		// Token: 0x04000135 RID: 309
		private readonly ADRecipient executingUser;

		// Token: 0x04000136 RID: 310
		private string groupInboxUrl;

		// Token: 0x04000137 RID: 311
		private string groupCalendarUrl;

		// Token: 0x04000138 RID: 312
		private string groupSharePointUrl;

		// Token: 0x04000139 RID: 313
		private string subscribeUrl;

		// Token: 0x0400013A RID: 314
		private string unsubscribeUrl;

		// Token: 0x0400013B RID: 315
		private ImageAttachment groupPhoto;

		// Token: 0x0400013C RID: 316
		private ImageAttachment executingUserPhoto;

		// Token: 0x0400013D RID: 317
		private Participant emailFrom;

		// Token: 0x0400013E RID: 318
		private Participant emailSender;

		// Token: 0x0400013F RID: 319
		private string encodedExecutingUserDisplayName;

		// Token: 0x04000140 RID: 320
		private IExchangePrincipal groupPrincipal;
	}
}

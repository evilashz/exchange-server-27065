using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003B1 RID: 945
	internal sealed class MeetingResponseWriter : MeetingPageWriter
	{
		// Token: 0x06002389 RID: 9097 RVA: 0x000CC658 File Offset: 0x000CA858
		public MeetingResponseWriter(MeetingResponse meetingResponse, UserContext userContext, bool isPreviewForm, bool isInDeletedItems, bool isEmbeddedItem, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled) : base(meetingResponse, userContext, isPreviewForm, isInDeletedItems, isEmbeddedItem, isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled)
		{
			this.meetingResponse = meetingResponse;
			this.isEmbeddedItem = isEmbeddedItem;
			object obj = meetingResponse.TryGetProperty(MessageItemSchema.IsDraft);
			this.isDraft = (obj is bool && (bool)obj);
			this.isDelegated = meetingResponse.IsDelegated();
			if (!Utilities.IsPublic(meetingResponse) && !this.isDraft && !isEmbeddedItem)
			{
				this.isOrganizer = base.ProcessMeetingMessage(meetingResponse, Utilities.IsItemInDefaultFolder(meetingResponse, DefaultFolderType.Inbox));
				if (this.isOrganizer)
				{
					this.AttendeeResponseWell = new CalendarItemAttendeeResponseRecipientWell(this.CalendarItemBase);
				}
			}
			this.recipientWell = new MessageRecipientWell(meetingResponse);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000CC703 File Offset: 0x000CA903
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingResponseWriter>(this);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000CC70C File Offset: 0x000CA90C
		protected internal override void BuildInfobar()
		{
			InfobarMessageBuilder.AddFlag(this.FormInfobar, this.meetingResponse, this.UserContext);
			if (this.isDraft)
			{
				this.FormInfobar.AddMessage(-1981719796, InfobarMessageType.Informational);
				string format = string.Empty;
				switch (this.meetingResponse.ResponseType)
				{
				case ResponseType.Tentative:
					format = LocalizedStrings.GetHtmlEncoded(-588720585);
					break;
				case ResponseType.Accept:
					format = LocalizedStrings.GetHtmlEncoded(-14610226);
					break;
				case ResponseType.Decline:
					format = LocalizedStrings.GetHtmlEncoded(-1615218790);
					break;
				}
				SanitizedHtmlString messageHtml;
				if (this.meetingResponse.From != null && string.CompareOrdinal(this.UserContext.ExchangePrincipal.LegacyDn, this.meetingResponse.From.EmailAddress) != 0)
				{
					ADSessionSettings adSettings = Utilities.CreateScopedADSessionSettings(this.UserContext.LogonIdentity.DomainName);
					string displayName = ExchangePrincipal.FromLegacyDN(adSettings, this.meetingResponse.From.EmailAddress).MailboxInfo.DisplayName;
					messageHtml = SanitizedHtmlString.Format(format, new object[]
					{
						displayName
					});
				}
				else
				{
					messageHtml = SanitizedHtmlString.Format(format, new object[]
					{
						LocalizedStrings.GetNonEncoded(372029413)
					});
				}
				this.FormInfobar.AddMessage(messageHtml, InfobarMessageType.Informational);
				return;
			}
			string s = string.Empty;
			string arg = string.Empty;
			if (this.OriginalSender == null || string.IsNullOrEmpty(this.OriginalSender.DisplayName))
			{
				arg = LocalizedStrings.GetNonEncoded(-342979842);
			}
			else
			{
				arg = this.OriginalSender.DisplayName;
			}
			switch (this.meetingResponse.ResponseType)
			{
			case ResponseType.Tentative:
				s = string.Format(Strings.InfoAttendeeTentative, arg);
				break;
			case ResponseType.Accept:
				s = string.Format(Strings.InfoAttendeeAccepted, arg);
				break;
			case ResponseType.Decline:
				s = string.Format(Strings.InfoAttendeeDecline, arg);
				break;
			}
			this.FormInfobar.AddMessage(Utilities.SanitizeHtmlEncode(s), InfobarMessageType.Informational);
			InfobarMessageBuilder.AddImportance(this.FormInfobar, this.meetingResponse);
			InfobarMessageBuilder.AddSensitivity(this.FormInfobar, this.meetingResponse);
			if (this.isDelegated)
			{
				this.FormInfobar.AddMessage(Utilities.SanitizeHtmlEncode(string.Format(LocalizedStrings.GetNonEncoded(-1205864060), MeetingUtilities.GetReceivedOnBehalfOfDisplayName(this.meetingResponse))), InfobarMessageType.Informational);
			}
			if (!this.isEmbeddedItem && !Utilities.IsPublic(this.meetingResponse))
			{
				InfobarMessageBuilder.AddReadReceiptNotice(this.UserContext, this.FormInfobar, this.meetingResponse);
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000CC980 File Offset: 0x000CAB80
		protected override void RenderMeetingInfoHeader(TextWriter writer)
		{
			writer.Write("<div id=\"divMtgInfoHeader\">");
			MeetingInfoHeaderToolbar meetingInfoHeaderToolbar = new MeetingInfoHeaderToolbar(this.meetingResponse.ResponseType);
			meetingInfoHeaderToolbar.Render(writer);
			string value = string.Empty;
			string value2 = string.Empty;
			switch (this.meetingResponse.ResponseType)
			{
			case ResponseType.Tentative:
				value = "respTentative";
				value2 = LocalizedStrings.GetNonEncoded(1798747159);
				break;
			case ResponseType.Accept:
				value = "respAccept";
				value2 = LocalizedStrings.GetNonEncoded(988533680);
				break;
			case ResponseType.Decline:
				value = "respDecline";
				value2 = LocalizedStrings.GetNonEncoded(884780479);
				break;
			}
			writer.Write("<div id=\"divMtgHeaderTxt\" class=\"");
			writer.Write(value);
			writer.Write("\">");
			writer.Write(value2);
			writer.Write("</div>");
			writer.Write("</div>");
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000CCA4F File Offset: 0x000CAC4F
		protected override void RenderOpenCalendarToolbar(TextWriter writer)
		{
			writer.Write("<div id=\"divWhenL\" class=\"roWellLabel pvwLabel\">");
			writer.Write(SanitizedHtmlString.FromStringId(-524211323));
			writer.Write("</div>");
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000CCA77 File Offset: 0x000CAC77
		public override void RenderTitle(TextWriter writer)
		{
			RenderingUtilities.RenderSubject(writer, this.meetingResponse, Strings.UntitledMeeting);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000CCA8A File Offset: 0x000CAC8A
		protected override string GetMeetingInfoClass()
		{
			return "mtgResponse";
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000CCA91 File Offset: 0x000CAC91
		public override void RenderSubject(TextWriter writer, bool disableEdit)
		{
			writer.Write(disableEdit ? "<div id=\"divSubj\">" : "<div id=\"divSubj\" tabindex=0 _editable=1>");
			RenderingUtilities.RenderSubject(writer, this.meetingResponse);
			writer.Write("</div>");
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000CCABF File Offset: 0x000CACBF
		public override void RenderSendOnBehalf(TextWriter writer)
		{
			RenderingUtilities.RenderSendOnBehalf(writer, this.UserContext, this.meetingResponse.From);
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x000CCAD8 File Offset: 0x000CACD8
		public override int StoreObjectType
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x000CCADC File Offset: 0x000CACDC
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x000CCAE4 File Offset: 0x000CACE4
		public override bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return this.isOrganizer && this.CalendarItemBase != null;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x000CCAFC File Offset: 0x000CACFC
		public override bool ShouldRenderSentField
		{
			get
			{
				return !this.isDraft;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000CCB07 File Offset: 0x000CAD07
		protected internal override string DescriptionTag
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000CCB0A File Offset: 0x000CAD0A
		internal override Participant ActualSender
		{
			get
			{
				return this.meetingResponse.Sender;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x000CCB17 File Offset: 0x000CAD17
		internal override Participant OriginalSender
		{
			get
			{
				return this.meetingResponse.From;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000CCB24 File Offset: 0x000CAD24
		public override bool ShouldRenderReminder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040018C0 RID: 6336
		private MeetingResponse meetingResponse;

		// Token: 0x040018C1 RID: 6337
		private MessageRecipientWell recipientWell;

		// Token: 0x040018C2 RID: 6338
		private bool isDraft;

		// Token: 0x040018C3 RID: 6339
		private bool isOrganizer;

		// Token: 0x040018C4 RID: 6340
		private bool isDelegated;

		// Token: 0x040018C5 RID: 6341
		private bool isEmbeddedItem;
	}
}

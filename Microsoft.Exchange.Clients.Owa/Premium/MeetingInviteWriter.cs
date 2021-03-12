using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003AF RID: 943
	internal sealed class MeetingInviteWriter : MeetingPageWriter
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x000CBCE0 File Offset: 0x000C9EE0
		public MeetingInviteWriter(MeetingRequest meetingRequest, UserContext userContext, string toolbarId, bool isPreviewForm, bool isInDeletedItems, bool isEmbeddedItem, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled) : base(meetingRequest, userContext, isPreviewForm, isInDeletedItems, isEmbeddedItem, isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled)
		{
			this.meetingRequest = meetingRequest;
			this.isDelegated = meetingRequest.IsDelegated();
			if (toolbarId == null)
			{
				toolbarId = "mpToolbar";
			}
			if (!Utilities.IsPublic(meetingRequest) && !this.IsDraft && !isEmbeddedItem)
			{
				this.isOrganizer = base.ProcessMeetingMessage(meetingRequest, Utilities.IsItemInDefaultFolder(meetingRequest, DefaultFolderType.Inbox));
				if (this.isOrganizer)
				{
					this.AttendeeResponseWell = new CalendarItemAttendeeResponseRecipientWell(this.CalendarItemBase);
				}
			}
			this.meetingMessageType = meetingRequest.MeetingRequestType;
			if (!this.IsDraft)
			{
				if (this.meetingMessageType == MeetingMessageType.Outdated)
				{
					this.infobarResponseString = new Strings.IDs?(1771878760);
					this.meetingMessageType = MeetingMessageType.Outdated;
				}
				else if (this.isOrganizer)
				{
					this.infobarResponseString = new Strings.IDs?(247559721);
					this.meetingMessageType = MeetingMessageType.InformationalUpdate;
				}
				else if (this.meetingMessageType == MeetingMessageType.InformationalUpdate)
				{
					if (this.CalendarItemBase != null && this.CalendarItemBase.ResponseType != ResponseType.NotResponded)
					{
						this.infobarResponseString = new Strings.IDs?(689325109);
					}
					else
					{
						this.meetingMessageType = MeetingMessageType.FullUpdate;
					}
				}
			}
			InfobarMessageBuilder.AddFlag(this.FormInfobar, meetingRequest, userContext);
			this.meetingRequestRecipientWell = new MeetingRequestRecipientWell(meetingRequest);
			this.toolbar = new EditMeetingInviteToolbar(toolbarId, meetingRequest.IsResponseRequested, Utilities.IsInArchiveMailbox(meetingRequest), this.meetingMessageType);
			this.toolbar.ToolbarType = ToolbarType.Preview;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000CBE48 File Offset: 0x000CA048
		protected internal override void BuildInfobar()
		{
			this.BuildInfobar(this.FormInfobar);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000CBE58 File Offset: 0x000CA058
		internal void BuildInfobar(Infobar infobar)
		{
			if (this.IsPhishingItemWithEnabledLinks())
			{
				string s = "<a id=\"aIbBlk\" href=\"#\" " + string.Format(CultureInfo.InvariantCulture, ">{0}</a> {1} ", new object[]
				{
					LocalizedStrings.GetHtmlEncoded(-672110188),
					LocalizedStrings.GetHtmlEncoded(-1020475744)
				});
				string s2 = string.Format(CultureInfo.InvariantCulture, "<a href=\"#\" " + Utilities.GetScriptHandler("onclick", "opnHlp(\"" + Utilities.JavascriptEncode(Utilities.BuildEhcHref(HelpIdsLight.EmailSafetyLight.ToString())) + "\");") + ">{0}</a>", new object[]
				{
					LocalizedStrings.GetHtmlEncoded(338562664)
				});
				infobar.AddMessage(SanitizedHtmlString.Format("{0}{1}{2}", new object[]
				{
					LocalizedStrings.GetNonEncoded(1581910613),
					SanitizedHtmlString.GetSanitizedStringWithoutEncoding(s),
					SanitizedHtmlString.GetSanitizedStringWithoutEncoding(s2)
				}), InfobarMessageType.Phishing);
			}
			if (this.isDelegated)
			{
				infobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1205864060), new object[]
				{
					MeetingUtilities.GetReceivedOnBehalfOfDisplayName(this.meetingRequest)
				}), InfobarMessageType.Informational);
			}
			if (this.meetingRequest.MeetingRequestType == MeetingMessageType.PrincipalWantsCopy)
			{
				infobar.AddMessage(SanitizedHtmlString.FromStringId(-332743944), InfobarMessageType.Informational);
			}
			if (!this.IsDraft && this.meetingRequest.MeetingRequestType != MeetingMessageType.Outdated && this.CalendarItemBase != null)
			{
				CalendarUtilities.AddCalendarInfobarMessages(infobar, this.CalendarItemBase, this.meetingRequest, this.UserContext);
			}
			if (this.infobarResponseString != null)
			{
				infobar.AddMessage(this.infobarResponseString.Value, InfobarMessageType.Informational);
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000CBFFA File Offset: 0x000CA1FA
		private bool IsPhishingItemWithEnabledLinks()
		{
			return base.IsSuspectedPhishingItem && !JunkEmailUtilities.IsItemLinkEnabled(this.meetingRequest);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000CC014 File Offset: 0x000CA214
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingInviteWriter>(this);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000CC01C File Offset: 0x000CA21C
		public override void RenderTitle(TextWriter writer)
		{
			RenderingUtilities.RenderSubject(writer, this.meetingRequest, LocalizedStrings.GetNonEncoded(-1500721828));
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000CC034 File Offset: 0x000CA234
		public override void RenderSubject(TextWriter writer, bool disableEdit)
		{
			writer.Write("<div id=\"divSubj\"");
			if ((this.ChangeHighlight & 16) != 0)
			{
				writer.Write(" class=\"updNew\"");
			}
			if (disableEdit)
			{
				writer.Write(">");
			}
			else
			{
				writer.Write(" tabindex=0 _editable=1>");
			}
			RenderingUtilities.RenderSubject(writer, this.meetingRequest);
			writer.Write("</div>");
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000CC094 File Offset: 0x000CA294
		public override void RenderWhen(TextWriter writer)
		{
			bool flag = 0 != (this.ChangeHighlight & 7);
			writer.Write("<div id=\"divMtgTbWhen\"><div");
			if (flag)
			{
				writer.Write(" class=\"updNew\"");
			}
			writer.Write(">");
			if (!string.IsNullOrEmpty(base.When.ToString()))
			{
				writer.Write(base.When);
			}
			writer.Write("</div>");
			if (flag && this.OldWhen.Length > 0)
			{
				writer.Write("<div class=\"updOld\">");
				writer.Write(this.MeetingPageUserContext.DirectionMark);
				writer.Write("(");
				writer.Write(this.OldWhen);
				writer.Write(")");
				writer.Write(this.MeetingPageUserContext.DirectionMark);
				writer.Write("</div>");
			}
			writer.Write("</div>");
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000CC174 File Offset: 0x000CA374
		public override void RenderLocation(TextWriter writer)
		{
			bool flag = (this.ChangeHighlight & 8) != 0;
			writer.Write("<div id=\"divMtgTbWhere\"><div id=\"divLocationL\" class=\"roWellLabel pvwLabel");
			if (flag)
			{
				writer.Write(" updNew");
			}
			writer.Write("\">");
			writer.Write(LocalizedStrings.GetNonEncoded(1666265192));
			writer.Write("</div><div id=\"divLoc\">");
			writer.Write("<div");
			if (flag)
			{
				writer.Write(" class=\"updNew\"");
			}
			writer.Write(">");
			writer.Write(this.Location);
			writer.Write("</div>");
			if (flag && this.OldLocation.Length > 0)
			{
				writer.Write("<div class=\"updOld\">");
				writer.Write(this.MeetingPageUserContext.DirectionMark);
				writer.Write("(");
				writer.Write(this.OldLocation);
				writer.Write(")");
				writer.Write(this.MeetingPageUserContext.DirectionMark);
				writer.Write("</div>");
			}
			writer.Write("</div>");
			writer.Write("</div>");
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000CC28B File Offset: 0x000CA48B
		protected override string GetMeetingInfoClass()
		{
			if (this.meetingRequest.MeetingRequestType == MeetingMessageType.Outdated)
			{
				return "mtgOutdated";
			}
			return string.Empty;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000CC2AA File Offset: 0x000CA4AA
		protected override string GetMeetingToolbarClass()
		{
			if (this.meetingMessageType == MeetingMessageType.NewMeetingRequest || this.meetingMessageType == MeetingMessageType.FullUpdate)
			{
				return "threeBtnTb";
			}
			return "oneBtnTb";
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000CC2D0 File Offset: 0x000CA4D0
		public override void RenderDescription(TextWriter writer)
		{
			writer.Write("<div class=\"dscrp ");
			if ((this.ChangeHighlight & 128) != 0)
			{
				writer.Write("updNew");
			}
			writer.Write("\">");
			Utilities.HtmlEncode(this.DescriptionTag, writer);
			writer.Write("</div>");
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000CC323 File Offset: 0x000CA523
		public override int StoreObjectType
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000CC327 File Offset: 0x000CA527
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.meetingRequestRecipientWell;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000CC32F File Offset: 0x000CA52F
		public override bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return this.isOrganizer && this.CalendarItemBase != null;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000CC347 File Offset: 0x000CA547
		public override bool ShouldRenderSentField
		{
			get
			{
				return !this.IsDraft;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000CC354 File Offset: 0x000CA554
		private bool IsDraft
		{
			get
			{
				object obj = this.meetingRequest.TryGetProperty(MessageItemSchema.IsDraft);
				return !(obj is bool) || (bool)obj;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000CC382 File Offset: 0x000CA582
		private int ChangeHighlight
		{
			get
			{
				return (int)this.meetingRequest.ChangeHighlight;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000CC390 File Offset: 0x000CA590
		protected internal override string OldLocation
		{
			get
			{
				string oldLocation = this.meetingRequest.OldLocation;
				if (oldLocation != null)
				{
					return Utilities.HtmlEncode(oldLocation);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000CC3B8 File Offset: 0x000CA5B8
		protected internal override string OldWhen
		{
			get
			{
				string text = this.meetingRequest.GenerateOldWhen();
				if (text != null)
				{
					return Utilities.HtmlEncode(text);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000CC3E0 File Offset: 0x000CA5E0
		internal override Participant OriginalSender
		{
			get
			{
				return this.meetingRequest.From;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000CC3ED File Offset: 0x000CA5ED
		internal override Participant ActualSender
		{
			get
			{
				return this.meetingRequest.Sender;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000CC3FA File Offset: 0x000CA5FA
		internal override Toolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000CC402 File Offset: 0x000CA602
		public override bool ShouldRenderReminder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040018B8 RID: 6328
		private MeetingRequest meetingRequest;

		// Token: 0x040018B9 RID: 6329
		private EditMeetingInviteToolbar toolbar;

		// Token: 0x040018BA RID: 6330
		private MeetingRequestRecipientWell meetingRequestRecipientWell;

		// Token: 0x040018BB RID: 6331
		private bool isOrganizer;

		// Token: 0x040018BC RID: 6332
		private Strings.IDs? infobarResponseString;

		// Token: 0x040018BD RID: 6333
		private bool isDelegated;

		// Token: 0x040018BE RID: 6334
		private MeetingMessageType meetingMessageType;
	}
}

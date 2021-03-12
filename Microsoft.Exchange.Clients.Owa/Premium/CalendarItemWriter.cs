using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200032D RID: 813
	internal sealed class CalendarItemWriter : MeetingPageWriter
	{
		// Token: 0x06001EDE RID: 7902 RVA: 0x000B1750 File Offset: 0x000AF950
		internal CalendarItemWriter(CalendarItemBase calendarItemBase, UserContext userContext, bool isPreviewForm, bool isInDeletedItems, bool isEmbeddedItem, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled) : base(calendarItemBase, userContext, isPreviewForm, isInDeletedItems, isEmbeddedItem, isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled)
		{
			this.CalendarItemBase = calendarItemBase;
			this.isOrganizer = this.CalendarItemBase.IsOrganizer();
			this.isMeeting = this.CalendarItemBase.IsMeeting;
			bool flag = Utilities.IsPublic(calendarItemBase);
			this.calendarRecipientWell = new CalendarItemRecipientWell(this.CalendarItemBase);
			if (flag)
			{
				if (this.isMeeting)
				{
					this.AttendeeResponseWell = new CalendarItemAttendeeResponseRecipientWell(this.CalendarItemBase);
					return;
				}
			}
			else
			{
				bool isInArchiveMailbox = Utilities.IsInArchiveMailbox(this.CalendarItemBase);
				if (this.isMeeting)
				{
					if (this.isOrganizer)
					{
						this.AttendeeResponseWell = new CalendarItemAttendeeResponseRecipientWell(this.CalendarItemBase);
					}
					else if (!this.CalendarItemBase.IsCancelled)
					{
						this.toolbar = new EditMeetingInviteToolbar("mpToolbar", this.IsResponseRequested, isInArchiveMailbox);
					}
				}
				else if (!this.isOrganizer)
				{
					this.toolbar = new EditMeetingInviteToolbar("mpToolbar", isInArchiveMailbox);
				}
				if (this.toolbar != null)
				{
					this.toolbar.ToolbarType = ToolbarType.Preview;
				}
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000B1853 File Offset: 0x000AFA53
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarItemWriter>(this);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000B185B File Offset: 0x000AFA5B
		protected internal override void BuildInfobar()
		{
			CalendarUtilities.AddCalendarInfobarMessages(this.FormInfobar, this.CalendarItemBase, null, this.UserContext);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000B1875 File Offset: 0x000AFA75
		public override void RenderTitle(TextWriter writer)
		{
			RenderingUtilities.RenderSubject(writer, this.CalendarItemBase, LocalizedStrings.GetNonEncoded(-1500721828));
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000B188D File Offset: 0x000AFA8D
		public override void RenderSubject(TextWriter writer, bool disableEdit)
		{
			writer.Write(disableEdit ? "<div id=\"divSubj\">" : "<div id=\"divSubj\" tabindex=0 _editable=1>");
			RenderingUtilities.RenderSubject(writer, this.CalendarItemBase);
			writer.Write("</div>");
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000B18BB File Offset: 0x000AFABB
		protected override void RenderOpenCalendarToolbar(TextWriter writer)
		{
			if (!this.CalendarItemBase.IsCancelled)
			{
				base.RenderOpenCalendarToolbar(writer);
				return;
			}
			writer.Write("<div id=\"divWhenL\" class=\"roWellLabel pvwLabel\">");
			writer.Write(SanitizedHtmlString.FromStringId(-524211323));
			writer.Write("</div>");
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000B18F8 File Offset: 0x000AFAF8
		protected override void RenderMeetingInfoHeader(TextWriter writer)
		{
			if (!this.CalendarItemBase.IsCancelled)
			{
				return;
			}
			MeetingCancelWriter.RenderCancelledMeetingHeader(writer, this.isOrganizer, base.IsInDeletedItems);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000B191A File Offset: 0x000AFB1A
		protected override string GetMeetingInfoClass()
		{
			if (this.CalendarItemBase.IsCancelled)
			{
				return "mtgCancel";
			}
			return base.GetMeetingInfoClass();
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000B1935 File Offset: 0x000AFB35
		protected override string GetMeetingToolbarClass()
		{
			if (this.toolbar == null)
			{
				return string.Empty;
			}
			return "threeBtnTb";
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000B194A File Offset: 0x000AFB4A
		public override bool ShouldRenderRecipientWell(RecipientWellType recipientWellType)
		{
			return this.CalendarItemBase.IsMeeting && base.ShouldRenderRecipientWell(recipientWellType);
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000B1962 File Offset: 0x000AFB62
		public override int StoreObjectType
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000B1966 File Offset: 0x000AFB66
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.calendarRecipientWell;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000B196E File Offset: 0x000AFB6E
		public override bool ShouldRenderSentField
		{
			get
			{
				return (this.isMeeting && !this.isOrganizer) || this.CalendarItemBase.MeetingRequestWasSent;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000B198D File Offset: 0x000AFB8D
		public override bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return this.isMeeting && this.isOrganizer && this.CalendarItemBase.MeetingRequestWasSent;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x000B19AC File Offset: 0x000AFBAC
		protected internal override string DescriptionTag
		{
			get
			{
				if (this.isMeeting && !this.isOrganizer)
				{
					return base.DescriptionTag;
				}
				return null;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x000B19C8 File Offset: 0x000AFBC8
		private bool IsResponseRequested
		{
			get
			{
				object obj = this.CalendarItemBase.TryGetProperty(ItemSchema.IsResponseRequested);
				return obj is bool && (bool)obj;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x000B19F6 File Offset: 0x000AFBF6
		private bool IsDelegateForwardedMeetingRequest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x000B19F9 File Offset: 0x000AFBF9
		internal override Participant OriginalSender
		{
			get
			{
				return this.CalendarItemBase.Organizer;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x000B1A06 File Offset: 0x000AFC06
		internal override Participant ActualSender
		{
			get
			{
				return this.CalendarItemBase.Organizer;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x000B1A13 File Offset: 0x000AFC13
		internal override Toolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x000B1A1B File Offset: 0x000AFC1B
		public override bool ShouldRenderReminder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400169F RID: 5791
		private Toolbar toolbar;

		// Token: 0x040016A0 RID: 5792
		private CalendarItemRecipientWell calendarRecipientWell;

		// Token: 0x040016A1 RID: 5793
		private bool isOrganizer;

		// Token: 0x040016A2 RID: 5794
		private bool isMeeting;
	}
}

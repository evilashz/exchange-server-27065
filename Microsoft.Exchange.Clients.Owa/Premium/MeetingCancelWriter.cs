using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003AD RID: 941
	internal sealed class MeetingCancelWriter : MeetingPageWriter
	{
		// Token: 0x0600235A RID: 9050 RVA: 0x000CB970 File Offset: 0x000C9B70
		public MeetingCancelWriter(MeetingCancellation meetingCancellation, UserContext userContext, string toolbarId, bool isPreviewForm, bool isInDeletedItems, bool isEmbeddedItem, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled) : base(meetingCancellation, userContext, isPreviewForm, isInDeletedItems, isEmbeddedItem, isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled)
		{
			this.meetingCancellation = meetingCancellation;
			this.isDelegated = meetingCancellation.IsDelegated();
			if (toolbarId == null)
			{
				toolbarId = "mpToolbar";
			}
			this.isOrganizer = true;
			if (!Utilities.IsPublic(meetingCancellation) && !this.IsDraft && !isEmbeddedItem)
			{
				this.isOrganizer = base.ProcessMeetingMessage(meetingCancellation, Utilities.IsItemInDefaultFolder(meetingCancellation, DefaultFolderType.Inbox));
				this.isOutOfDate = MeetingUtilities.MeetingCancellationIsOutOfDate(meetingCancellation);
			}
			this.recipientWell = new MessageRecipientWell(meetingCancellation);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000CB9F5 File Offset: 0x000C9BF5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingCancelWriter>(this);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000CBA00 File Offset: 0x000C9C00
		protected internal override void BuildInfobar()
		{
			if (this.meetingCancellation.Importance == Importance.High)
			{
				this.FormInfobar.AddMessage(-788473393, InfobarMessageType.Informational);
			}
			else if (this.meetingCancellation.Importance == Importance.Low)
			{
				this.FormInfobar.AddMessage(-1193056027, InfobarMessageType.Informational);
			}
			if (this.isDelegated)
			{
				this.FormInfobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-1205864060), new object[]
				{
					MeetingUtilities.GetReceivedOnBehalfOfDisplayName(this.meetingCancellation)
				}), InfobarMessageType.Informational);
			}
			if (!this.isOutOfDate)
			{
				this.FormInfobar.AddMessage(-161808760, InfobarMessageType.Informational);
				return;
			}
			this.FormInfobar.AddMessage(21101307, InfobarMessageType.Informational);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000CBAB0 File Offset: 0x000C9CB0
		protected override void RenderOpenCalendarToolbar(TextWriter writer)
		{
			writer.Write("<div id=\"divWhenL\" class=\"roWellLabel pvwLabel\">");
			writer.Write(SanitizedHtmlString.FromStringId(-524211323));
			writer.Write("</div>");
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000CBAD8 File Offset: 0x000C9CD8
		public override void RenderTitle(TextWriter writer)
		{
			RenderingUtilities.RenderSubject(writer, this.meetingCancellation, LocalizedStrings.GetNonEncoded(-1500721828));
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000CBAF0 File Offset: 0x000C9CF0
		protected override string GetMeetingInfoClass()
		{
			return "mtgCancel";
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000CBAF7 File Offset: 0x000C9CF7
		public override void RenderSubject(TextWriter writer, bool disableEdit)
		{
			writer.Write(disableEdit ? "<div id=\"divSubj\">" : "<div id=\"divSubj\" tabindex=0 _editable=1>");
			RenderingUtilities.RenderSubject(writer, this.meetingCancellation);
			writer.Write("</div>");
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000CBB25 File Offset: 0x000C9D25
		protected override void RenderMeetingInfoHeader(TextWriter writer)
		{
			MeetingCancelWriter.RenderCancelledMeetingHeader(writer, this.isOrganizer, base.IsInDeletedItems);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000CBB3C File Offset: 0x000C9D3C
		public static void RenderCancelledMeetingHeader(TextWriter writer, bool isOrganizer, bool isDeleted)
		{
			writer.Write("<div id=\"divMtgInfoHeader\">");
			MeetingInfoHeaderToolbar meetingInfoHeaderToolbar = new MeetingInfoHeaderToolbar();
			meetingInfoHeaderToolbar.Render(writer);
			writer.Write("<div id=\"divMtgHeaderTxt\" class=\"mtgCanceled\">");
			writer.Write(SanitizedHtmlString.FromStringId(-383210701));
			writer.Write("</div>");
			if (!isOrganizer && !isDeleted)
			{
				writer.Write("<div id=\"divMtgCancelLink\">");
				writer.Write(SanitizedHtmlString.FromStringId(-2115983576));
				writer.Write("</div>");
			}
			writer.Write("</div>");
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000CBBBE File Offset: 0x000C9DBE
		public override int StoreObjectType
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x000CBBC2 File Offset: 0x000C9DC2
		public override bool ShouldRenderSentField
		{
			get
			{
				return !this.IsDraft;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x000CBBD0 File Offset: 0x000C9DD0
		private bool IsDraft
		{
			get
			{
				object obj = this.meetingCancellation.TryGetProperty(MessageItemSchema.IsDraft);
				return !(obj is bool) || (bool)obj;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000CBBFE File Offset: 0x000C9DFE
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x000CBC06 File Offset: 0x000C9E06
		internal override Participant OriginalSender
		{
			get
			{
				return this.meetingCancellation.From;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000CBC13 File Offset: 0x000C9E13
		internal override Participant ActualSender
		{
			get
			{
				return this.meetingCancellation.Sender;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000CBC20 File Offset: 0x000C9E20
		public override bool ShouldRenderReminder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040018B2 RID: 6322
		private MeetingCancellation meetingCancellation;

		// Token: 0x040018B3 RID: 6323
		private MessageRecipientWell recipientWell;

		// Token: 0x040018B4 RID: 6324
		private bool isDelegated;

		// Token: 0x040018B5 RID: 6325
		private bool isOutOfDate;

		// Token: 0x040018B6 RID: 6326
		private bool isOrganizer;
	}
}

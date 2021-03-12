using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200005F RID: 95
	internal sealed class MeetingInviteWriter : MeetingPageWriter
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00017830 File Offset: 0x00015A30
		public MeetingInviteWriter(MeetingRequest meetingRequest, UserContext userContext, bool isEmbeddedItem) : base(meetingRequest, userContext)
		{
			this.meetingRequest = meetingRequest;
			if (!isEmbeddedItem && !meetingRequest.IsDelegated())
			{
				this.isOrganizer = base.ProcessMeetingMessage(meetingRequest, Utilities.IsItemInDefaultFolder(meetingRequest, DefaultFolderType.Inbox));
				if (this.isOrganizer)
				{
					this.AttendeeResponseWell = new CalendarItemAttendeeResponseRecipientWell(userContext, base.CalendarItemBase);
				}
			}
			this.meetingMessageType = meetingRequest.MeetingRequestType;
			if (this.meetingMessageType != MeetingMessageType.Outdated)
			{
				if (this.isOrganizer)
				{
					this.meetingMessageType = MeetingMessageType.InformationalUpdate;
				}
				else if (this.meetingMessageType == MeetingMessageType.InformationalUpdate && base.CalendarItemBase != null && base.CalendarItemBase.ResponseType == ResponseType.NotResponded)
				{
					this.meetingMessageType = MeetingMessageType.FullUpdate;
				}
			}
			this.meetingRequestRecipientWell = new MeetingRequestRecipientWell(userContext, meetingRequest);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000178F0 File Offset: 0x00015AF0
		protected override void InternalDispose(bool isDisposing)
		{
			try
			{
				if (isDisposing && base.CalendarItemBase != null)
				{
					base.CalendarItemBase.Dispose();
					base.CalendarItemBase = null;
				}
			}
			finally
			{
				base.InternalDispose(isDisposing);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00017934 File Offset: 0x00015B34
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingInviteWriter>(this);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001793C File Offset: 0x00015B3C
		public override void RenderWhen(TextWriter writer)
		{
			bool flag = 0 != (this.ChangeHighlight & 7);
			writer.Write("<td class=\"hdmsb");
			if (flag)
			{
				writer.Write(" updnw");
			}
			writer.Write("\">");
			writer.Write(SanitizedHtmlString.FromStringId(-524211323));
			writer.Write("</td><td class=\"hdmtxt\">");
			writer.Write("<span");
			if (flag)
			{
				writer.Write(" class=\"updnw\"");
			}
			writer.Write(">");
			if (!SanitizedStringBase<OwaHtml>.IsNullOrEmpty(base.When))
			{
				writer.Write(base.When);
			}
			writer.Write("</span>");
			if (flag && this.OldWhen.Length > 0)
			{
				writer.Write("<div class=\"updold\">");
				writer.Write("(");
				writer.Write(this.OldWhen);
				writer.Write(")");
				writer.Write("</div>");
			}
			writer.Write("</td>");
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00017A34 File Offset: 0x00015C34
		public override void RenderLocation(TextWriter writer)
		{
			bool flag = (this.ChangeHighlight & 8) != 0;
			writer.Write("<td class=\"hdmsb");
			if (flag)
			{
				writer.Write(" updnw");
			}
			writer.Write("\">");
			writer.Write(SanitizedHtmlString.FromStringId(-1134349396));
			writer.Write("</td><td class=\"hdmtxt\">");
			writer.Write("<span");
			if (flag)
			{
				writer.Write(" class=\"updnw\"");
			}
			writer.Write(">");
			writer.Write(this.Location);
			writer.Write("</span>");
			if (flag && this.OldLocation.Length > 0)
			{
				writer.Write("<div class=\"updold\">");
				writer.Write("(");
				writer.Write(this.OldLocation);
				writer.Write(")");
				writer.Write("</div>");
			}
			writer.Write("</td>");
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00017B20 File Offset: 0x00015D20
		public override void RenderToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, true);
			MeetingMessageType meetingMessageType = this.meetingMessageType;
			if (meetingMessageType > MeetingMessageType.FullUpdate)
			{
				if (meetingMessageType != MeetingMessageType.InformationalUpdate)
				{
					if (meetingMessageType == MeetingMessageType.Outdated)
					{
						toolbar.RenderStartForSubToolbar();
						toolbar.RenderButton(ToolbarButtons.MeetingOutOfDate);
						toolbar.RenderDivider();
						toolbar.RenderButton(ToolbarButtons.ShowCalendar);
						toolbar.RenderFill();
						toolbar.RenderEndForSubToolbar();
						return;
					}
					if (meetingMessageType != MeetingMessageType.PrincipalWantsCopy)
					{
						return;
					}
				}
				toolbar.RenderStartForSubToolbar();
				toolbar.RenderButton(ToolbarButtons.MeetingNoResponseRequired);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.ShowCalendar);
				toolbar.RenderFill();
				toolbar.RenderEndForSubToolbar();
				return;
			}
			if (meetingMessageType != MeetingMessageType.NewMeetingRequest && meetingMessageType != MeetingMessageType.FullUpdate)
			{
				return;
			}
			toolbar.RenderStartForSubToolbar();
			toolbar.RenderButton(ToolbarButtons.MeetingAccept);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.MeetingTentative);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.MeetingDecline);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.ShowCalendar);
			toolbar.RenderFill();
			toolbar.RenderEndForSubToolbar();
			if (this.meetingRequest.IsResponseRequested)
			{
				MeetingPageWriter.RenderResponseEditTypeSelectToolbar(writer);
				return;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00017C2E File Offset: 0x00015E2E
		public override int StoreObjectType
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00017C32 File Offset: 0x00015E32
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.meetingRequestRecipientWell;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00017C3A File Offset: 0x00015E3A
		public override bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return this.isOrganizer && base.CalendarItemBase != null;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00017C52 File Offset: 0x00015E52
		public override bool HasToolbar
		{
			get
			{
				return !this.isOrganizer;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00017C5D File Offset: 0x00015E5D
		private int ChangeHighlight
		{
			get
			{
				return (int)this.meetingRequest.ChangeHighlight;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00017C6C File Offset: 0x00015E6C
		private string OldLocation
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00017C94 File Offset: 0x00015E94
		private string OldWhen
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00017CBC File Offset: 0x00015EBC
		internal override Participant OriginalSender
		{
			get
			{
				return this.meetingRequest.From;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00017CC9 File Offset: 0x00015EC9
		internal override Participant ActualSender
		{
			get
			{
				return this.meetingRequest.Sender;
			}
		}

		// Token: 0x040001E9 RID: 489
		internal static readonly PropertyDefinition[] PrefetchProperties = new PropertyDefinition[]
		{
			MessageItemSchema.IsRead,
			MessageItemSchema.IsDraft,
			MeetingMessageSchema.CalendarProcessed,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			MessageItemSchema.ReplyTime,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet
		};

		// Token: 0x040001EA RID: 490
		private MeetingRequest meetingRequest;

		// Token: 0x040001EB RID: 491
		private MeetingRequestRecipientWell meetingRequestRecipientWell;

		// Token: 0x040001EC RID: 492
		private bool isOrganizer;

		// Token: 0x040001ED RID: 493
		private MeetingMessageType meetingMessageType;
	}
}

using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200001E RID: 30
	internal sealed class CalendarItemWriter : MeetingPageWriter
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00007A20 File Offset: 0x00005C20
		public CalendarItemWriter(CalendarItemBase calendarItemBase, UserContext userContext) : base(calendarItemBase, userContext)
		{
			base.CalendarItemBase = calendarItemBase;
			this.messageRecipientWell = new CalendarItemRecipientWell(userContext, base.CalendarItemBase);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007A4C File Offset: 0x00005C4C
		public override void RenderToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, true);
			toolbar.RenderStartForSubToolbar();
			if (base.CalendarItemBase.IsCancelled)
			{
				toolbar.RenderButton(ToolbarButtons.RemoveFromCalendar);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.ShowCalendar);
				toolbar.RenderFill();
				toolbar.RenderEndForSubToolbar();
				return;
			}
			MeetingMessageType meetingMessageType = this.meetingMessageType;
			if (meetingMessageType != MeetingMessageType.NewMeetingRequest && meetingMessageType != MeetingMessageType.FullUpdate)
			{
				if (meetingMessageType != MeetingMessageType.Outdated)
				{
					return;
				}
				toolbar.RenderButton(ToolbarButtons.MeetingOutOfDate);
				toolbar.RenderFill();
				toolbar.RenderEndForSubToolbar();
			}
			else
			{
				toolbar.RenderButton(ToolbarButtons.MeetingAccept);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.MeetingTentative);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.MeetingDecline);
				toolbar.RenderDivider();
				toolbar.RenderFill();
				toolbar.RenderEndForSubToolbar();
				if (this.IsResponseRequested)
				{
					MeetingPageWriter.RenderResponseEditTypeSelectToolbar(writer);
					return;
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00007B1E File Offset: 0x00005D1E
		public override int StoreObjectType
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007B22 File Offset: 0x00005D22
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.messageRecipientWell;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00007B2A File Offset: 0x00005D2A
		private bool IsResponseRequested
		{
			get
			{
				return ItemUtility.GetProperty<bool>(base.CalendarItemBase, ItemSchema.IsResponseRequested, false);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007B3D File Offset: 0x00005D3D
		internal override Participant OriginalSender
		{
			get
			{
				return base.CalendarItemBase.Organizer;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007B4A File Offset: 0x00005D4A
		internal override Participant ActualSender
		{
			get
			{
				return base.CalendarItemBase.Organizer;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007B57 File Offset: 0x00005D57
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarItemWriter>(this);
		}

		// Token: 0x0400008D RID: 141
		private CalendarItemRecipientWell messageRecipientWell;

		// Token: 0x0400008E RID: 142
		private MeetingMessageType meetingMessageType = MeetingMessageType.NewMeetingRequest;
	}
}

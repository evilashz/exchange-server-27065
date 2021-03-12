using System;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000063 RID: 99
	internal sealed class MeetingResponseWriter : MeetingPageWriter
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x000186F8 File Offset: 0x000168F8
		public MeetingResponseWriter(MeetingResponse meetingResponse, UserContext userContext, bool isEmbeddedItem) : base(meetingResponse, userContext)
		{
			this.meetingResponse = meetingResponse;
			if (!isEmbeddedItem && !meetingResponse.IsDelegated())
			{
				this.isOrganizer = base.ProcessMeetingMessage(meetingResponse, Utilities.IsItemInDefaultFolder(meetingResponse, DefaultFolderType.Inbox));
				if (this.isOrganizer)
				{
					this.AttendeeResponseWell = new CalendarItemAttendeeResponseRecipientWell(userContext, base.CalendarItemBase);
				}
			}
			this.recipientWell = new MessageRecipientWell(userContext, meetingResponse);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001875C File Offset: 0x0001695C
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

		// Token: 0x060002C6 RID: 710 RVA: 0x000187A0 File Offset: 0x000169A0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingResponseWriter>(this);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000187A8 File Offset: 0x000169A8
		public override int StoreObjectType
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000187AC File Offset: 0x000169AC
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000187B4 File Offset: 0x000169B4
		public override bool HasToolbar
		{
			get
			{
				return !this.isOrganizer;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000187BF File Offset: 0x000169BF
		public override bool ShouldRenderAttendeeResponseWells
		{
			get
			{
				return this.isOrganizer && base.CalendarItemBase != null;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000187D7 File Offset: 0x000169D7
		internal override Participant ActualSender
		{
			get
			{
				return this.meetingResponse.Sender;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000187E4 File Offset: 0x000169E4
		internal override Participant OriginalSender
		{
			get
			{
				return this.meetingResponse.From;
			}
		}

		// Token: 0x0400020C RID: 524
		internal static PropertyDefinition[] PrefetchProperties = new PropertyDefinition[]
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

		// Token: 0x0400020D RID: 525
		private MeetingResponse meetingResponse;

		// Token: 0x0400020E RID: 526
		private MessageRecipientWell recipientWell;

		// Token: 0x0400020F RID: 527
		private bool isOrganizer;
	}
}

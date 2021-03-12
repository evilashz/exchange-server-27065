using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200005E RID: 94
	internal sealed class MeetingCancelWriter : MeetingPageWriter
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000176A8 File Offset: 0x000158A8
		public MeetingCancelWriter(MeetingCancellation meetingCancellation, UserContext userContext, bool isEmbeddedItem) : base(meetingCancellation, userContext)
		{
			this.meetingCancellation = meetingCancellation;
			if (!isEmbeddedItem && !meetingCancellation.IsDelegated())
			{
				this.isOrganizer = base.ProcessMeetingMessage(meetingCancellation, Utilities.IsItemInDefaultFolder(meetingCancellation, DefaultFolderType.Inbox));
			}
			this.messageRecipientWell = new MessageRecipientWell(userContext, meetingCancellation);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000176F8 File Offset: 0x000158F8
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

		// Token: 0x060002A0 RID: 672 RVA: 0x0001773C File Offset: 0x0001593C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingCancelWriter>(this);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00017744 File Offset: 0x00015944
		public override void RenderToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, true);
			toolbar.RenderStartForSubToolbar();
			if (!this.isOrganizer)
			{
				toolbar.RenderButton(ToolbarButtons.RemoveFromCalendar);
			}
			else
			{
				toolbar.RenderButton(ToolbarButtons.MeetingNoResponseRequired);
			}
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.ShowCalendar);
			toolbar.RenderFill();
			toolbar.RenderEndForSubToolbar();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001779C File Offset: 0x0001599C
		public override int StoreObjectType
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000177A0 File Offset: 0x000159A0
		public override RecipientWell RecipientWell
		{
			get
			{
				return this.messageRecipientWell;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000177A8 File Offset: 0x000159A8
		internal override Participant OriginalSender
		{
			get
			{
				return this.meetingCancellation.From;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000177B5 File Offset: 0x000159B5
		internal override Participant ActualSender
		{
			get
			{
				return this.meetingCancellation.Sender;
			}
		}

		// Token: 0x040001E5 RID: 485
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

		// Token: 0x040001E6 RID: 486
		private MeetingCancellation meetingCancellation;

		// Token: 0x040001E7 RID: 487
		private MessageRecipientWell messageRecipientWell;

		// Token: 0x040001E8 RID: 488
		private bool isOrganizer = true;
	}
}

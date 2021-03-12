using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000369 RID: 873
	public class EditMeetingInviteToolbar : Toolbar
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x000BD9BF File Offset: 0x000BBBBF
		internal EditMeetingInviteToolbar(string id, bool isInArchiveMailbox) : base(id, ToolbarType.Form)
		{
			this.isOrganizer = true;
			this.isInArchiveMailbox = isInArchiveMailbox;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000BD9E5 File Offset: 0x000BBBE5
		internal EditMeetingInviteToolbar(string id, bool isResponseRequested, bool isInArchiveMailbox) : base(id, ToolbarType.Form)
		{
			this.isResponseRequested = isResponseRequested;
			this.isInArchiveMailbox = isInArchiveMailbox;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000BDA0B File Offset: 0x000BBC0B
		internal EditMeetingInviteToolbar(string id, bool isResponseRequested, bool isInArchiveMailbox, MeetingMessageType meetingMessageType) : base(id, ToolbarType.Form)
		{
			this.isResponseRequested = isResponseRequested;
			this.isInArchiveMailbox = isInArchiveMailbox;
			this.meetingMessageType = meetingMessageType;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x000BDA39 File Offset: 0x000BBC39
		public override bool HasBigButton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000BDA3C File Offset: 0x000BBC3C
		protected override void RenderButtons()
		{
			if (!this.isOrganizer)
			{
				MeetingMessageType meetingMessageType = this.meetingMessageType;
				if (meetingMessageType > MeetingMessageType.FullUpdate)
				{
					if (meetingMessageType != MeetingMessageType.InformationalUpdate)
					{
						if (meetingMessageType == MeetingMessageType.Outdated)
						{
							base.RenderButton(ToolbarButtons.MeetingOutOfDate);
							return;
						}
						if (meetingMessageType != MeetingMessageType.PrincipalWantsCopy)
						{
							return;
						}
					}
					base.RenderButton(ToolbarButtons.MeetingNoResponseRequired);
					return;
				}
				if (meetingMessageType != MeetingMessageType.NewMeetingRequest && meetingMessageType != MeetingMessageType.FullUpdate)
				{
					return;
				}
				ToolbarButtonFlags toolbarButtonFlags = ToolbarButtonFlags.None;
				if (this.isInArchiveMailbox)
				{
					toolbarButtonFlags |= ToolbarButtonFlags.Disabled;
				}
				if (!this.isResponseRequested)
				{
					base.RenderButton(ToolbarButtons.MeetingAccept, toolbarButtonFlags);
					base.RenderButton(ToolbarButtons.MeetingTentative, toolbarButtonFlags);
					base.RenderButton(ToolbarButtons.MeetingDecline, toolbarButtonFlags);
					return;
				}
				base.RenderButton(ToolbarButtons.MeetingAcceptMenu, toolbarButtonFlags, new Toolbar.RenderMenuItems(this.RenderMeetingResponseMenuItems));
				base.RenderButton(ToolbarButtons.MeetingTentativeMenu, toolbarButtonFlags, new Toolbar.RenderMenuItems(this.RenderMeetingResponseMenuItems));
				base.RenderButton(ToolbarButtons.MeetingDeclineMenu, toolbarButtonFlags, new Toolbar.RenderMenuItems(this.RenderMeetingResponseMenuItems));
				return;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000BDB31 File Offset: 0x000BBD31
		private void RenderMeetingResponseMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.MeetingEditResponse);
			base.RenderMenuItem(ToolbarButtons.MeetingSendResponseNow);
			base.RenderMenuItem(ToolbarButtons.MeetingNoResponse);
		}

		// Token: 0x04001789 RID: 6025
		private bool isResponseRequested = true;

		// Token: 0x0400178A RID: 6026
		private MeetingMessageType meetingMessageType = MeetingMessageType.NewMeetingRequest;

		// Token: 0x0400178B RID: 6027
		private bool isOrganizer;

		// Token: 0x0400178C RID: 6028
		private bool isInArchiveMailbox;
	}
}

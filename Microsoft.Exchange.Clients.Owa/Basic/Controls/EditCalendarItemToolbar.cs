using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200003D RID: 61
	internal class EditCalendarItemToolbar : Toolbar
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000EE64 File Offset: 0x0000D064
		public EditCalendarItemToolbar(bool isNew, bool isMeeting, bool meetingRequestWasSent, Importance importance, CalendarItemType calendarItemType, TextWriter writer, bool isHeader, bool isBeingCanceled) : base(writer, isHeader)
		{
			this.isNew = isNew;
			this.isMeeting = isMeeting;
			this.meetingRequestWasSent = meetingRequestWasSent;
			this.importance = importance;
			this.calendarItemType = calendarItemType;
			this.isBeingCanceled = isBeingCanceled;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public void RenderButtons()
		{
			bool flag = !this.isNew && !this.isBeingCanceled;
			if (this.isMeeting)
			{
				if (this.isNew || !this.meetingRequestWasSent)
				{
					if (this.isBeingCanceled)
					{
						base.RenderButton(ToolbarButtons.SaveAndClose);
					}
					else
					{
						base.RenderButton(ToolbarButtons.Send);
						base.RenderButton(ToolbarButtons.SaveImageOnly);
					}
				}
				else
				{
					if (this.isBeingCanceled)
					{
						base.RenderButton(ToolbarButtons.SendCancellation);
					}
					else
					{
						base.RenderButton(ToolbarButtons.SendUpdate);
						base.RenderButton(ToolbarButtons.SaveImageOnly);
					}
					base.RenderDivider();
					base.RenderButton(ToolbarButtons.ReplyAll);
				}
			}
			else
			{
				base.RenderButton(ToolbarButtons.SaveAndClose);
			}
			base.RenderDivider();
			base.RenderButton(ToolbarButtons.Cancel);
			base.RenderDivider();
			base.RenderButton(ToolbarButtons.AttachFile);
			base.RenderDivider();
			if (CalendarItemType.Occurrence == this.calendarItemType || CalendarItemType.Exception == this.calendarItemType)
			{
				base.RenderButton(ToolbarButtons.EditSeries);
			}
			else
			{
				base.RenderButton(ToolbarButtons.Recurrence);
			}
			base.RenderDivider();
			if (!this.isMeeting)
			{
				base.RenderButton(ToolbarButtons.InviteAttendees);
				if (flag)
				{
					base.RenderDivider("divInv", true);
				}
			}
			else
			{
				base.RenderButton(ToolbarButtons.CheckNamesImageOnly);
				base.RenderDivider("divInv", true);
			}
			if (this.isMeeting)
			{
				if (this.importance == Importance.High)
				{
					base.RenderButton(ToolbarButtons.ImportanceHigh, ToolbarButtonFlags.Selected);
					base.RenderSpace();
					base.RenderButton(ToolbarButtons.ImportanceLow);
				}
				else if (this.importance == Importance.Low)
				{
					base.RenderButton(ToolbarButtons.ImportanceHigh);
					base.RenderSpace();
					base.RenderButton(ToolbarButtons.ImportanceLow, ToolbarButtonFlags.Selected);
				}
				else
				{
					base.RenderButton(ToolbarButtons.ImportanceHigh);
					base.RenderSpace();
					base.RenderButton(ToolbarButtons.ImportanceLow);
				}
				if (flag)
				{
					base.RenderDivider();
				}
			}
			if (flag)
			{
				base.RenderButton(ToolbarButtons.DeleteImage);
			}
			base.RenderFill();
		}

		// Token: 0x04000136 RID: 310
		private bool isNew = true;

		// Token: 0x04000137 RID: 311
		private bool isMeeting;

		// Token: 0x04000138 RID: 312
		private bool meetingRequestWasSent;

		// Token: 0x04000139 RID: 313
		private bool isBeingCanceled;

		// Token: 0x0400013A RID: 314
		private Importance importance = Importance.Normal;

		// Token: 0x0400013B RID: 315
		private CalendarItemType calendarItemType;
	}
}

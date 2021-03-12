using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000364 RID: 868
	public class EditCalendarItemToolbar : Toolbar
	{
		// Token: 0x0600209C RID: 8348 RVA: 0x000BCB70 File Offset: 0x000BAD70
		internal EditCalendarItemToolbar(CalendarItemBase calendarItemBase, bool isMeeting, Markup currentMarkup, bool isPublicItem)
		{
			this.isNew = (calendarItemBase == null);
			this.isMeeting = isMeeting;
			this.meetingRequestWasSent = (!this.isNew && calendarItemBase.MeetingRequestWasSent);
			this.initialMarkup = currentMarkup;
			this.importance = (this.isNew ? Importance.Normal : calendarItemBase.Importance);
			this.calendarItemType = (this.isNew ? CalendarItemType.Single : calendarItemBase.CalendarItemType);
			this.isPublicItem = isPublicItem;
			this.canEdit = (this.isNew || ItemUtility.UserCanEditItem(calendarItemBase));
			if (this.isNew || CalendarUtilities.UserCanDeleteCalendarItem(calendarItemBase) || (calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster && this.canEdit))
			{
				this.canDelete = true;
				return;
			}
			this.canDelete = false;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000BCC40 File Offset: 0x000BAE40
		protected override void RenderButtons()
		{
			base.RenderHelpButton(HelpIdsLight.CalendarLight.ToString(), string.Empty);
			if (this.isMeeting)
			{
				if (this.isNew || !this.meetingRequestWasSent)
				{
					if (this.isPublicItem)
					{
						base.RenderButton(ToolbarButtons.SaveAndClose);
					}
					else
					{
						base.RenderButton(ToolbarButtons.Send);
						base.RenderButton(ToolbarButtons.SaveImageOnly);
						base.RenderButton(ToolbarButtons.SaveAndClose, ToolbarButtonFlags.Hidden);
					}
				}
				else
				{
					if (this.isPublicItem)
					{
						base.RenderButton(ToolbarButtons.SaveAndClose);
					}
					else
					{
						base.RenderButton(ToolbarButtons.SendUpdate);
						base.RenderButton(ToolbarButtons.SendCancelation, ToolbarButtonFlags.Hidden);
						base.RenderButton(ToolbarButtons.SaveAndCloseImageOnly);
					}
					if (this.isPublicItem)
					{
						base.RenderButton(ToolbarButtons.ReplyImageOnly, ToolbarButtonFlags.Disabled);
						base.RenderButton(ToolbarButtons.ReplyAllImageOnly, ToolbarButtonFlags.Disabled);
					}
					else
					{
						base.RenderButton(ToolbarButtons.ReplyAllImageOnly);
					}
				}
			}
			else
			{
				base.RenderButton(ToolbarButtons.SaveAndClose);
				base.RenderButton(ToolbarButtons.Send, ToolbarButtonFlags.Hidden);
				base.RenderButton(ToolbarButtons.SaveImageOnly, ToolbarButtonFlags.Hidden);
			}
			base.RenderButton(ToolbarButtons.AttachFile);
			base.RenderButton(ToolbarButtons.InsertImage);
			ToolbarButtonFlags flags = ToolbarButtonFlags.None;
			if (CalendarItemType.Occurrence == this.calendarItemType || CalendarItemType.Exception == this.calendarItemType)
			{
				flags = ToolbarButtonFlags.Disabled;
			}
			base.RenderButton(ToolbarButtons.RecurrenceImageOnly, flags);
			if (!this.isPublicItem)
			{
				base.RenderButton(ToolbarButtons.CheckNames, this.isMeeting ? ToolbarButtonFlags.None : ToolbarButtonFlags.Hidden);
			}
			if (base.UserContext.BrowserType == BrowserType.IE)
			{
				base.RenderButton(ToolbarButtons.SpellCheck, base.UserContext.IsFeatureEnabled(Feature.SpellChecker) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled, new Toolbar.RenderMenuItems(base.RenderSpellCheckLanguageDialog));
			}
			if (!this.isPublicItem && (!this.isMeeting || (this.isMeeting && !this.meetingRequestWasSent)))
			{
				base.RenderButton(ToolbarButtons.CancelInvitation, this.isMeeting ? ToolbarButtonFlags.None : ToolbarButtonFlags.Hidden);
				base.RenderButton(ToolbarButtons.InviteAttendees, this.isMeeting ? ToolbarButtonFlags.Hidden : ToolbarButtonFlags.None);
			}
			base.RenderButton(ToolbarButtons.ImportanceHigh, (this.importance == Importance.High) ? ToolbarButtonFlags.Pressed : ToolbarButtonFlags.None);
			base.RenderButton(ToolbarButtons.ImportanceLow, (this.importance == Importance.Low) ? ToolbarButtonFlags.Pressed : ToolbarButtonFlags.None);
			if (CalendarItemType.Occurrence != this.calendarItemType && CalendarItemType.Exception != this.calendarItemType)
			{
				base.RenderButton(ToolbarButtons.Categories);
			}
			this.RenderDeleteButton();
			base.RenderButton(ToolbarButtons.Print);
			if (this.initialMarkup == Markup.Html)
			{
				base.RenderHtmlTextToggle("0");
			}
			else
			{
				base.RenderHtmlTextToggle("1");
			}
			base.RenderButton(ToolbarButtons.MailTips);
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000BCEE0 File Offset: 0x000BB0E0
		private void RenderDeleteButton()
		{
			ToolbarButtonFlags flags = this.canDelete ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			if (this.isMeeting && this.meetingRequestWasSent)
			{
				base.RenderButton(ToolbarButtons.CancelMeeting, flags);
				return;
			}
			if (!this.isNew)
			{
				base.RenderButton(ToolbarButtons.Delete, flags);
			}
		}

		// Token: 0x04001775 RID: 6005
		private bool isNew = true;

		// Token: 0x04001776 RID: 6006
		private bool isMeeting;

		// Token: 0x04001777 RID: 6007
		private bool meetingRequestWasSent;

		// Token: 0x04001778 RID: 6008
		private Markup initialMarkup;

		// Token: 0x04001779 RID: 6009
		private Importance importance = Importance.Normal;

		// Token: 0x0400177A RID: 6010
		private CalendarItemType calendarItemType;

		// Token: 0x0400177B RID: 6011
		private bool isPublicItem;

		// Token: 0x0400177C RID: 6012
		private bool canEdit;

		// Token: 0x0400177D RID: 6013
		private bool canDelete;
	}
}

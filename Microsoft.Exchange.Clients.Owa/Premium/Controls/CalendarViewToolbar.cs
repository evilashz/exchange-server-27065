using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000331 RID: 817
	internal sealed class CalendarViewToolbar : Toolbar
	{
		// Token: 0x06001F12 RID: 7954 RVA: 0x000B2974 File Offset: 0x000B0B74
		public CalendarViewToolbar(CalendarViewType viewType, bool isPublicFolder, bool userCanCreateItem, bool userHasRightToLoad, bool isWebPartRequest, ReadingPanePosition readingPanePosition, SanitizedHtmlString folderInfo) : base("divTBL")
		{
			this.viewType = viewType;
			this.folderInfo = folderInfo;
			this.isPublicFolder = isPublicFolder;
			this.flagsForNewButton = (userCanCreateItem ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled);
			this.flagsForNonNewButton = (userHasRightToLoad ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled);
			this.readingPanePosition = readingPanePosition;
			this.isWebPartRequest = isWebPartRequest;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000B29D8 File Offset: 0x000B0BD8
		protected override void RenderButtons()
		{
			if (base.UserContext.IsWebPartRequest && this.folderInfo != null)
			{
				base.RenderButton(ToolbarButtons.CalendarTitle, this.folderInfo);
			}
			if (this.isPublicFolder)
			{
				base.RenderButton(ToolbarButtons.NewWithAppointmentIcon, this.flagsForNewButton);
			}
			else
			{
				base.RenderButton(ToolbarButtons.NewAppointmentCombo, this.flagsForNewButton, new Toolbar.RenderMenuItems(this.RenderNewMenuItems));
			}
			this.RenderNonNewButton(ToolbarButtons.DeleteTextOnly);
			base.RenderFloatedSpacer(3);
			this.RenderNonNewButton(ToolbarButtons.Today);
			base.RenderFloatedSpacer(3);
			if (this.viewType == CalendarViewType.Min)
			{
				this.RenderNonNewButton(ToolbarButtons.DayView, ToolbarButtonFlags.Pressed);
			}
			else
			{
				this.RenderNonNewButton(ToolbarButtons.DayView);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Organization))
			{
				if (this.viewType == CalendarViewType.WorkWeek)
				{
					this.RenderNonNewButton(ToolbarButtons.WorkWeekView, ToolbarButtonFlags.Pressed);
				}
				else
				{
					this.RenderNonNewButton(ToolbarButtons.WorkWeekView);
				}
			}
			if (this.viewType == CalendarViewType.Weekly)
			{
				this.RenderNonNewButton(ToolbarButtons.WeekView, ToolbarButtonFlags.Pressed);
			}
			else
			{
				this.RenderNonNewButton(ToolbarButtons.WeekView);
			}
			if (this.viewType == CalendarViewType.Monthly)
			{
				this.RenderNonNewButton(ToolbarButtons.MonthView, ToolbarButtonFlags.Pressed);
			}
			else
			{
				this.RenderNonNewButton(ToolbarButtons.MonthView);
			}
			if (!this.isPublicFolder && !this.isWebPartRequest)
			{
				base.RenderFloatedSpacer(3);
				base.RenderButton(ToolbarButtons.ShareCalendar, ToolbarButtonFlags.None);
			}
			base.RenderButton(ToolbarButtons.ChangeView, this.flagsForNonNewButton, new Toolbar.RenderMenuItems(this.RenderReadingPaneAndListViewMenuItems));
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.PrintCalendar, ToolbarButtonFlags.None);
			base.RenderFloatedSpacer(1, "divMeasure");
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000B2B65 File Offset: 0x000B0D65
		private void RenderNonNewButton(ToolbarButton button)
		{
			base.RenderButton(button, this.flagsForNonNewButton);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000B2B74 File Offset: 0x000B0D74
		private void RenderNonNewButton(ToolbarButton button, ToolbarButtonFlags flags)
		{
			base.RenderButton(button, flags | this.flagsForNonNewButton);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000B2B85 File Offset: 0x000B0D85
		private void RenderNewMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.NewAppointment);
			base.RenderMenuItem(ToolbarButtons.NewMeetingRequest);
			base.RenderMenuItem(ToolbarButtons.NewMessage);
			base.RenderCustomNewMenuItems();
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000B2BB0 File Offset: 0x000B0DB0
		private void RenderReadingPaneAndListViewMenuItems()
		{
			ViewDropDownMenu viewDropDownMenu = new ViewDropDownMenu(base.UserContext, this.readingPanePosition, false, false);
			viewDropDownMenu.Render(base.Writer);
		}

		// Token: 0x040016A7 RID: 5799
		private readonly CalendarViewType viewType;

		// Token: 0x040016A8 RID: 5800
		private readonly SanitizedHtmlString folderInfo;

		// Token: 0x040016A9 RID: 5801
		private readonly bool isPublicFolder;

		// Token: 0x040016AA RID: 5802
		private readonly ToolbarButtonFlags flagsForNonNewButton;

		// Token: 0x040016AB RID: 5803
		private readonly ToolbarButtonFlags flagsForNewButton;

		// Token: 0x040016AC RID: 5804
		private readonly ReadingPanePosition readingPanePosition;

		// Token: 0x040016AD RID: 5805
		private readonly bool isWebPartRequest;
	}
}

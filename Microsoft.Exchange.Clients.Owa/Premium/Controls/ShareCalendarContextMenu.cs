using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000412 RID: 1042
	internal sealed class ShareCalendarContextMenu : ContextMenu
	{
		// Token: 0x0600258D RID: 9613 RVA: 0x000D9333 File Offset: 0x000D7533
		public ShareCalendarContextMenu(UserContext userContext) : base("divShareCalCm", userContext)
		{
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000D9344 File Offset: 0x000D7544
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, -1037074462, ThemeFileId.CalendarSharedTo, "divOpenShareCalendar", "opnshcal");
			ContextMenu.RenderMenuDivider(output, "divS1");
			base.RenderMenuItem(output, 14861680, ThemeFileId.CalendarSharedOut, "divShareACalendar", "shcurcal");
			base.RenderMenuItem(output, 1443081199, ThemeFileId.ChangePermission, "divChangeSharingPermission", "chgperm");
			ContextMenu.RenderMenuDivider(output, "divS2");
			base.RenderMenuItem(output, -1068180164, ThemeFileId.WebCalendar, "divPublishACalendar", "pubcal");
			base.RenderMenuItem(output, -2142303303, ThemeFileId.WebCalendar, "divSendPublishLink", "sndLnk");
			base.RenderMenuItem(output, -517719709, ThemeFileId.ChangePermission, "divChangePublishingPermission", "chgpbperm");
			ContextMenu.RenderMenuDivider(output, "divS3");
			base.RenderMenuItem(output, 124032253, ThemeFileId.None, "divShowUrl", "showurl");
		}
	}
}

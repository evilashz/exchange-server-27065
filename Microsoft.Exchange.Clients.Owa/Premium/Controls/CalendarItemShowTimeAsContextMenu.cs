using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200032A RID: 810
	internal sealed class CalendarItemShowTimeAsContextMenu : ContextMenu
	{
		// Token: 0x06001EA5 RID: 7845 RVA: 0x000B0D58 File Offset: 0x000AEF58
		public CalendarItemShowTimeAsContextMenu(UserContext userContext) : base("divCalShwTmAsCm", userContext)
		{
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000B0D68 File Offset: 0x000AEF68
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, -971703552, ThemeFileId.Clear, "divShowTimeFree", "free");
			base.RenderMenuItem(output, 1797669216, ThemeFileId.Clear, "divShowTimeTentative", "tentative");
			base.RenderMenuItem(output, 2052801377, ThemeFileId.Clear, "divShowTimeBusy", "busy");
			base.RenderMenuItem(output, 2047193656, ThemeFileId.Clear, "divShowTimeOOF", "unavailable");
		}
	}
}

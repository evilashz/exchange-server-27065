using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004EB RID: 1259
	public static class ActiveChatsRenderingUtilities
	{
		// Token: 0x06002FCC RID: 12236 RVA: 0x00116370 File Offset: 0x00114570
		public static void RenderActiveChatsMenu(UserContext userContext, TextWriter output)
		{
			NotificationRenderingUtilities.RenderNotificationMenu(userContext, output, "divActChts", null, -1947596443, -1018465893, null, 1414246128, null, 1414246128, "divBtnOpen", 197744374, null);
		}
	}
}

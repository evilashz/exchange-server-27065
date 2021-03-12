using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004EA RID: 1258
	public static class BuddyRequestRenderingUtilities
	{
		// Token: 0x06002FCB RID: 12235 RVA: 0x00116304 File Offset: 0x00114504
		public static void RenderBuddyRequestMenu(UserContext userContext, TextWriter output)
		{
			Strings.IDs buttonAllText = 1592123169;
			Strings.IDs button1Text = -2119870632;
			Strings.IDs button2Text = -475579318;
			Strings.IDs description = -1018465893;
			if (userContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
			{
				buttonAllText = -1320039448;
				button1Text = -1359586561;
				button2Text = 292745765;
				description = 1419100302;
			}
			NotificationRenderingUtilities.RenderNotificationMenu(userContext, output, "divBddy", null, -533779368, description, "divBtnAcceptall", buttonAllText, "divBtnDecline", button1Text, "divBtnAccept", button2Text, null);
		}
	}
}

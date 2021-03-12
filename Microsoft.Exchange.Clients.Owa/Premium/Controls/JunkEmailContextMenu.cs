using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003A7 RID: 935
	public sealed class JunkEmailContextMenu : ContextMenu
	{
		// Token: 0x0600234B RID: 9035 RVA: 0x000CB504 File Offset: 0x000C9704
		public static JunkEmailContextMenu Create(UserContext userContext, JunkEmailContextMenuType junkEmailContextMenuType)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return new JunkEmailContextMenu(userContext, junkEmailContextMenuType);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000CB51B File Offset: 0x000C971B
		public JunkEmailContextMenu(UserContext userContext, JunkEmailContextMenuType junkEmailContextMenuType) : base((junkEmailContextMenuType == JunkEmailContextMenuType.Item) ? "divJnkmItm" : ((junkEmailContextMenuType == JunkEmailContextMenuType.Sender) ? "divJnkmSnd" : "divJnkmRcp"), userContext)
		{
			this.type = junkEmailContextMenuType;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000CB548 File Offset: 0x000C9748
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			switch (this.type)
			{
			case JunkEmailContextMenuType.Item:
				base.RenderMenuItem(output, 460585, "sndbsl");
				base.RenderMenuItem(output, 1616411850, "sndssl");
				base.RenderMenuItem(output, 930405866, "snddmssl");
				ContextMenu.RenderMenuDivider(output, "divS11");
				base.RenderMenuItem(output, 278436626, ThemeFileId.Inbox, "divNtJnk", "ntjnk");
				return;
			case JunkEmailContextMenuType.Sender:
				base.RenderMenuItem(output, 1707311266, "tobsl");
				base.RenderMenuItem(output, -1334943953, "tossl");
				base.RenderMenuItem(output, 527346223, "dmtossl");
				return;
			case JunkEmailContextMenuType.Recipient:
				base.RenderMenuItem(output, 1679197603, "rcvsrl");
				return;
			default:
				throw new OwaInvalidRequestException("Invalid junk mail context menu type");
			}
		}

		// Token: 0x040018A6 RID: 6310
		private JunkEmailContextMenuType type;
	}
}

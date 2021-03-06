using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000360 RID: 864
	public sealed class DumpsterViewArrangeByMenu : ArrangeByMenu
	{
		// Token: 0x06002089 RID: 8329 RVA: 0x000BC904 File Offset: 0x000BAB04
		internal DumpsterViewArrangeByMenu(Folder folder)
		{
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000BC90C File Offset: 0x000BAB0C
		protected override void RenderMenuItems(TextWriter output, UserContext userContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ArrangeByMenu.RenderMenuItem(output, 1932142663, null, ColumnId.DeletedOnTime);
			ArrangeByMenu.RenderMenuItem(output, 1309845117, null, ColumnId.From);
			ArrangeByMenu.RenderMenuItem(output, 262509582, null, ColumnId.To);
			ArrangeByMenu.RenderMenuItem(output, 1128018090, null, ColumnId.Size);
			ArrangeByMenu.RenderMenuItem(output, 2014646167, "divSbj", ColumnId.Subject);
			ArrangeByMenu.RenderMenuItem(output, 785343019, null, ColumnId.MailIcon);
			ArrangeByMenu.RenderMenuItem(output, 1072079569, null, ColumnId.HasAttachment);
		}
	}
}

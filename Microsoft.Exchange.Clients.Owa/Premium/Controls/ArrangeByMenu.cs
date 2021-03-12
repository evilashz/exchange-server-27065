using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000318 RID: 792
	public abstract class ArrangeByMenu
	{
		// Token: 0x06001E0F RID: 7695 RVA: 0x000AE214 File Offset: 0x000AC414
		protected static void RenderMenuItem(TextWriter output, Strings.IDs displayString, string id, ColumnId columnId)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			Column column = ListViewColumns.GetColumn(columnId);
			output.Write("<div class=cmLnk");
			if (id != null)
			{
				output.Write(" id=");
				output.Write(id);
			}
			output.Write(" _cid=");
			output.Write((int)columnId);
			output.Write(" _so=");
			output.Write(((int)column.DefaultSortOrder).ToString(CultureInfo.InvariantCulture));
			output.Write(" _lnk=1");
			output.Write(" _tD=");
			output.Write(column.IsTypeDownCapable ? "1" : "0");
			output.Write(">");
			output.Write("<span id=spnT>");
			output.Write(LocalizedStrings.GetHtmlEncoded(displayString));
			output.Write("</span></div>");
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000AE2EC File Offset: 0x000AC4EC
		public void Render(TextWriter output, UserContext userContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			output.Write("<div id=divAbm class=ctxMnu style=display:none>");
			this.RenderMenuItems(output, userContext);
			RenderingUtilities.RenderDropShadows(output, userContext);
			output.Write("</div>");
		}

		// Token: 0x06001E11 RID: 7697
		protected abstract void RenderMenuItems(TextWriter output, UserContext userContext);
	}
}

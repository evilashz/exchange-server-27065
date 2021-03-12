using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200040D RID: 1037
	internal abstract class SecondaryNavigationList
	{
		// Token: 0x06002572 RID: 9586 RVA: 0x000D8D7D File Offset: 0x000D6F7D
		protected SecondaryNavigationList(string elementId)
		{
			this.elementId = elementId;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002573 RID: 9587
		protected abstract int Count { get; }

		// Token: 0x06002574 RID: 9588 RVA: 0x000D8D8C File Offset: 0x000D6F8C
		protected virtual void RenderListAttributes(TextWriter output)
		{
		}

		// Token: 0x06002575 RID: 9589
		protected abstract void RenderEntryOnClickHandler(TextWriter output, int entryIndex);

		// Token: 0x06002576 RID: 9590 RVA: 0x000D8D8E File Offset: 0x000D6F8E
		protected virtual void RenderEntryOnContextMenuHandler(TextWriter output)
		{
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000D8D90 File Offset: 0x000D6F90
		protected virtual void RenderEntryAttributes(TextWriter output, int entryIndex)
		{
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000D8D92 File Offset: 0x000D6F92
		protected virtual void RenderEntryIcon(TextWriter output, int entryIndex)
		{
		}

		// Token: 0x06002579 RID: 9593
		protected abstract string GetEntryText(int entryIndex);

		// Token: 0x0600257A RID: 9594 RVA: 0x000D8D94 File Offset: 0x000D6F94
		protected virtual void RenderFooter(TextWriter output)
		{
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000D8D98 File Offset: 0x000D6F98
		public void Render(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=\"");
			output.Write(this.elementId);
			output.Write("\" class=\"secNvLst\" fSNL=\"1\" ");
			this.RenderListAttributes(output);
			output.Write(">");
			output.Write("<div id=\"divDefEnts\">");
			this.RenderEntries(output);
			output.Write("</div>");
			this.RenderFooter(output);
			output.Write("</div>");
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000D8E18 File Offset: 0x000D7018
		public void RenderEntries(TextWriter output)
		{
			for (int i = 0; i < this.Count; i++)
			{
				output.Write("<div class=\"snlEntW\"><div id=\"divEnt\" class=\"snlEnt snlDef\" _onclick=\"");
				this.RenderEntryOnClickHandler(output, i);
				output.Write("\" ");
				this.RenderEntryOnContextMenuHandler(output);
				this.RenderEntryAttributes(output, i);
				output.Write(">");
				this.RenderEntryIcon(output, i);
				output.Write("<span class=\"snlEntTxt\">");
				Utilities.HtmlEncode(this.GetEntryText(i), output);
				output.Write("</span></div></div>");
			}
		}

		// Token: 0x040019EC RID: 6636
		private string elementId;
	}
}

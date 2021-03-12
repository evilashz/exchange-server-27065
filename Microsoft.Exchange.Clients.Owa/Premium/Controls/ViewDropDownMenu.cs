using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000432 RID: 1074
	public class ViewDropDownMenu
	{
		// Token: 0x060026E3 RID: 9955 RVA: 0x000DE21E File Offset: 0x000DC41E
		public ViewDropDownMenu(UserContext userContext, ReadingPanePosition readingPanePosition, bool showConversationOptions, bool allowReadingPaneBottom)
		{
			this.userContext = userContext;
			this.readingPanePosition = readingPanePosition;
			this.showConversationOptions = showConversationOptions;
			this.allowReadingPaneBottom = allowReadingPaneBottom;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000DE254 File Offset: 0x000DC454
		public void Render(TextWriter writer)
		{
			writer.Write("<div id=\"divRPddm\">");
			if (this.showConversationOptions)
			{
				this.RenderTitleMenuItem(writer, 2107827829, new Strings.IDs?(-1637335381));
				this.RenderCheckboxMenuItem(writer, ToolbarButtons.UseConversations, true, false);
				if (!this.userContext.IsWebPartRequest)
				{
					this.RenderLinkMenuItem(writer, ToolbarButtons.ConversationOptions);
				}
			}
			this.RenderTitleMenuItem(writer, 549375552, null);
			this.RenderCheckboxMenuItem(writer, ToolbarButtons.ReadingPaneRight, this.readingPanePosition == ReadingPanePosition.Right, true);
			if (this.allowReadingPaneBottom)
			{
				this.RenderCheckboxMenuItem(writer, ToolbarButtons.ReadingPaneBottom, this.readingPanePosition == ReadingPanePosition.Bottom, true);
			}
			this.RenderCheckboxMenuItem(writer, ToolbarButtons.ReadingPaneOff, this.readingPanePosition == ReadingPanePosition.Off, true);
			writer.Write("</div>");
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000DE31C File Offset: 0x000DC51C
		private void RenderTitleMenuItem(TextWriter writer, Strings.IDs title, Strings.IDs? caption)
		{
			writer.Write("<div class=\"vwMnTtl\">");
			writer.Write(SanitizedHtmlString.FromStringId(title));
			writer.Write("</div>");
			if (caption != null)
			{
				writer.Write("<div class=\"vwMnCap\">");
				writer.Write(SanitizedHtmlString.FromStringId(caption.Value));
				writer.Write("</div>");
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000DE37C File Offset: 0x000DC57C
		private void RenderCheckboxMenuItem(TextWriter writer, ToolbarButton button, bool selected, bool radioButton)
		{
			writer.Write("<div id=\"divMnuItm\" ");
			writer.Write(radioButton ? "fRdo=\"1\"" : "fChk=\"1\"");
			if (selected)
			{
				writer.Write(" fSel=\"1\"");
			}
			writer.Write(" cmd=\"");
			writer.Write(button.Command);
			writer.Write("\"><a id=\"");
			writer.Write(button.Command);
			writer.Write("\" class=\"vwMnItm\" href=\"#\"><div class=\"vwMnChk\">");
			OwaContext.Current.SessionContext.RenderThemeImage(writer, ThemeFileId.Checkmark, "tbLh", new object[]
			{
				"id=\"imgChk\"",
				selected ? null : "style=\"display:none\""
			});
			writer.Write("</div><span class=\"tbLh tbBtwn\">");
			Utilities.SanitizeHtmlEncode(LocalizedStrings.GetNonEncoded(button.TextId), writer);
			writer.Write("</span></a></div>");
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000DE450 File Offset: 0x000DC650
		private void RenderLinkMenuItem(TextWriter writer, ToolbarButton button)
		{
			writer.Write("<div id=\"divMnuItm\" cmd=\"");
			writer.Write(button.Command);
			writer.Write("\"><a id=\"");
			writer.Write(button.Command);
			writer.Write("\" class=\"vwMnItm\" href=\"#\"><div class=\"vwMnChk\"></div><span class=\"tbLh tbBtwn\">");
			Utilities.SanitizeHtmlEncode(LocalizedStrings.GetNonEncoded(button.TextId), writer);
			writer.Write("</span></a></div>");
		}

		// Token: 0x04001B33 RID: 6963
		private UserContext userContext;

		// Token: 0x04001B34 RID: 6964
		private ReadingPanePosition readingPanePosition = ReadingPanePosition.Right;

		// Token: 0x04001B35 RID: 6965
		private bool showConversationOptions;

		// Token: 0x04001B36 RID: 6966
		private bool allowReadingPaneBottom = true;
	}
}

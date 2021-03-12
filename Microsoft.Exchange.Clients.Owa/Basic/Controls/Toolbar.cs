using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200003C RID: 60
	internal class Toolbar
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000E749 File Offset: 0x0000C949
		public Toolbar(TextWriter writer, bool isHeader)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.isHeader = isHeader;
			this.userContext = UserContextManager.GetUserContext();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000E77F File Offset: 0x0000C97F
		public Toolbar()
		{
			this.userContext = UserContextManager.GetUserContext();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000E79C File Offset: 0x0000C99C
		public void RenderStart()
		{
			if (this.isHeader)
			{
				this.writer.Write("<table class=\"tbhd\" cellpadding=0 cellspacing=0><caption>");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1587641320));
				this.writer.Write("</caption><tr>");
				return;
			}
			this.writer.Write("<table class=\"tbft\" cellpadding=0 cellspacing=0><caption>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-997034062));
			this.writer.Write("</caption><tr>");
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public void RenderStartForSubToolbar()
		{
			this.writer.Write("<table class=\"stb\" cellpadding=0 cellspacing=0><tr>");
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000E82E File Offset: 0x0000CA2E
		public void RenderEndForSubToolbar()
		{
			this.writer.Write("</tr></table>");
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000E840 File Offset: 0x0000CA40
		public void RenderEnd()
		{
			if (this.isHeader)
			{
				this.writer.Write("<td align=\"right\" class=\"crvTp\"><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.CornerTopRight);
				this.writer.Write("\" alt=\"\"></td>");
			}
			else
			{
				this.writer.Write("<td align=\"right\" class=\"crvBtm\"><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.CornerBottomRight);
				this.writer.Write("\" alt=\"\"></td>");
			}
			this.writer.Write("</tr></table>");
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000E8CD File Offset: 0x0000CACD
		public void RenderButton(ToolbarButton button)
		{
			this.RenderButton(button, ToolbarButtonFlags.None);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		public void RenderButton(ToolbarButton button, ToolbarButtonFlags flags)
		{
			flags |= button.Flags;
			bool flag = ToolbarButtonFlags.Tab == (flags & ToolbarButtonFlags.Tab);
			bool flag2 = ToolbarButtonFlags.NoAction == (flags & ToolbarButtonFlags.NoAction);
			if (flag2)
			{
				this.writer.Write("<td nowrap><div class=\"divNoRR\">");
				if ((flags & ToolbarButtonFlags.Image) != (ToolbarButtonFlags)0U)
				{
					this.writer.Write("<img src=\"");
					this.userContext.RenderThemeFileUrl(this.writer, button.Image);
					this.writer.Write("\"");
					if ((flags & ToolbarButtonFlags.Text) != (ToolbarButtonFlags)0U)
					{
						this.writer.Write(" alt=\"\">");
						this.writer.Write(' ');
					}
					else
					{
						if (button.TextId != -1018465893)
						{
							this.writer.Write(" alt=\"");
							if (button.ToolTip == null)
							{
								this.writer.Write(LocalizedStrings.GetHtmlEncoded(button.TextId));
							}
							else
							{
								this.writer.Write(button.ToolTip);
							}
							this.writer.Write("\"");
						}
						this.writer.Write(">");
					}
				}
				else
				{
					this.writer.Write("<img class=\"noSrc\" src=\"");
					this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
					this.writer.Write("\" alt=\"\">");
				}
				if ((flags & ToolbarButtonFlags.Text) != (ToolbarButtonFlags)0U)
				{
					this.writer.Write(LocalizedStrings.GetHtmlEncoded(button.TextId));
				}
				this.writer.Write("</div></td>");
				return;
			}
			if (flag)
			{
				this.writer.Write("<td class=\"tabhk\"><img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear1x1);
				this.writer.Write("\"></td>");
			}
			this.writer.Write("<td");
			if ((flags & ToolbarButtonFlags.Sticky) != (ToolbarButtonFlags)0U)
			{
				this.writer.Write(" id=\"{0}\"", button.Command);
				if ((flags & ToolbarButtonFlags.Selected) != (ToolbarButtonFlags)0U)
				{
					this.writer.Write(" class=\"sl\"");
				}
			}
			if (flag)
			{
				this.writer.Write(" class=\"tab\"");
			}
			this.writer.Write(" nowrap>");
			if (flag)
			{
				this.writer.Write("<div class=\"tabbrd\">");
			}
			string arg = string.Empty;
			if (flag)
			{
				this.writer.Write("<a class=");
				arg = "tab";
			}
			else
			{
				this.writer.Write("<a href=\"#\" onClick=\"return onClkTb('");
				this.writer.Write(button.Command);
				this.writer.Write("');\" class=");
			}
			if ((flags & ToolbarButtonFlags.NoHover) == (ToolbarButtonFlags)0U)
			{
				this.writer.Write("\"btn{0}\"", arg);
			}
			else
			{
				this.writer.Write("\"noHv\"");
			}
			if (button.TextId != -1018465893)
			{
				this.writer.Write(" title=\"");
				if (button.ToolTip == null)
				{
					this.writer.Write(LocalizedStrings.GetHtmlEncoded(button.TextId));
				}
				else
				{
					this.writer.Write(button.ToolTip);
				}
				this.writer.Write("\"");
			}
			this.writer.Write(" id=\"");
			if (this.isHeader)
			{
				this.writer.Write("lnkHdr");
			}
			else
			{
				this.writer.Write("lnkFtr");
			}
			this.writer.Write(button.Command);
			this.writer.Write("\">");
			if ((flags & ToolbarButtonFlags.Image) != (ToolbarButtonFlags)0U)
			{
				this.writer.Write("<img src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, button.Image);
				this.writer.Write("\"");
				if ((flags & ToolbarButtonFlags.Text) != (ToolbarButtonFlags)0U)
				{
					this.writer.Write(" alt=\"\">");
					this.writer.Write(' ');
				}
				else
				{
					if (button.TextId != -1018465893)
					{
						this.writer.Write(" alt=\"");
						if (button.ToolTip == null)
						{
							this.writer.Write(LocalizedStrings.GetHtmlEncoded(button.TextId));
						}
						else
						{
							this.writer.Write(button.ToolTip);
						}
						this.writer.Write("\"");
					}
					this.writer.Write(">");
				}
			}
			else
			{
				this.writer.Write("<img class=\"noSrc\" src=\"");
				this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.Clear);
				this.writer.Write("\" alt=\"\">");
			}
			if ((flags & ToolbarButtonFlags.Text) != (ToolbarButtonFlags)0U)
			{
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(button.TextId));
			}
			if (flag2)
			{
				this.writer.Write("</div>");
			}
			else
			{
				this.writer.Write("</a>");
			}
			if (flag)
			{
				this.writer.Write("</div>");
			}
			this.writer.Write("</td>");
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public void RenderDivider(string id, bool displayed)
		{
			this.writer.Write("<td class=\"dv\"");
			if (id != null)
			{
				this.writer.Write(" id=\"");
				this.writer.Write(id);
				this.writer.Write("\"");
			}
			if (!displayed)
			{
				this.writer.Write(" style=\"display:none\"");
			}
			this.writer.Write("><img src=\"");
			this.userContext.RenderThemeFileUrl(this.writer, ThemeFileId.ToolbarDivider);
			this.writer.Write("\" alt=\"\"></td>");
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000EE35 File Offset: 0x0000D035
		public void RenderDivider()
		{
			this.RenderDivider(null, true);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000EE3F File Offset: 0x0000D03F
		public void RenderFill()
		{
			this.writer.Write("<td class=\"w100\">&nbsp;</td>");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000EE51 File Offset: 0x0000D051
		public void RenderSpace()
		{
			this.writer.Write("<td>&nbsp;</td>");
		}

		// Token: 0x04000133 RID: 307
		private TextWriter writer;

		// Token: 0x04000134 RID: 308
		private bool isHeader = true;

		// Token: 0x04000135 RID: 309
		private UserContext userContext;
	}
}

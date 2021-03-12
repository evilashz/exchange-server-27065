using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200004C RID: 76
	public class Infobar
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00012FB4 File Offset: 0x000111B4
		public Infobar()
		{
			this.userContext = UserContextManager.GetUserContext();
			OwaContext owaContext = OwaContext.Current;
			if (owaContext[OwaContextProperty.InfobarMessage] != null)
			{
				this.messages.Add((InfobarMessage)owaContext[OwaContextProperty.InfobarMessage]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00013005 File Offset: 0x00011205
		public int MessageCount
		{
			get
			{
				return this.messages.Count;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00013012 File Offset: 0x00011212
		internal void AddMessage(InfobarMessage message)
		{
			this.messages.Add(message);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00013021 File Offset: 0x00011221
		internal void AddMessageText(string messageText, InfobarMessageType type)
		{
			this.messages.Add(InfobarMessage.CreateText(messageText, type));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00013036 File Offset: 0x00011236
		internal void AddMessageHtml(SanitizedHtmlString messageHtml, InfobarMessageType type)
		{
			this.messages.Add(InfobarMessage.CreateHtml(messageHtml, type));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001304B File Offset: 0x0001124B
		internal void AddMessageLocalized(Strings.IDs stringId, InfobarMessageType type)
		{
			this.messages.Add(InfobarMessage.CreateLocalized(stringId, type));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00013060 File Offset: 0x00011260
		public void Render(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (this.messages.Count == 0)
			{
				return;
			}
			Infobar.InfobarMessageComparer comparer = new Infobar.InfobarMessageComparer();
			this.messages.Sort(comparer);
			output.Write("<div id=\"divIB\">");
			int num;
			for (int i = 0; i < this.messages.Count; i = num)
			{
				num = i + 1;
				while (num < this.messages.Count && ((InfobarMessage)this.messages[num]).Type == ((InfobarMessage)this.messages[i]).Type)
				{
					num++;
				}
				InfobarMessage infobarMessage = (InfobarMessage)this.messages[i];
				output.Write("<div id=");
				switch (infobarMessage.Type)
				{
				case InfobarMessageType.Expanding:
				case InfobarMessageType.Informational:
				case InfobarMessageType.Prompt:
				case InfobarMessageType.Warning:
					output.Write("dvInf");
					break;
				case InfobarMessageType.Error:
					output.Write("dvErr");
					break;
				case InfobarMessageType.JunkEmail:
					output.Write("dvJnkMl");
					break;
				case InfobarMessageType.Phishing:
					output.Write("dvPhsh");
					break;
				}
				output.Write(">");
				bool flag = false;
				if (infobarMessage.Type == InfobarMessageType.Error || infobarMessage.Type == InfobarMessageType.JunkEmail || infobarMessage.Type == InfobarMessageType.Phishing || infobarMessage.Type == InfobarMessageType.Prompt || infobarMessage.Type == InfobarMessageType.Warning)
				{
					flag = true;
					output.Write("<table cellpadding=\"0\" cellspacing=\"0\">");
					output.Write("<tr><td valign=\"top\" rowspan=\"");
					output.Write(num - i);
					output.Write("\"><img src=\"");
					switch (infobarMessage.Type)
					{
					case InfobarMessageType.Prompt:
						this.userContext.RenderThemeFileUrl(output, ThemeFileId.Exclaim);
						break;
					case InfobarMessageType.Error:
						this.userContext.RenderThemeFileUrl(output, ThemeFileId.Error);
						break;
					case InfobarMessageType.JunkEmail:
					case InfobarMessageType.Phishing:
						this.userContext.RenderThemeFileUrl(output, ThemeFileId.YellowShield);
						break;
					case InfobarMessageType.Warning:
						this.userContext.RenderThemeFileUrl(output, ThemeFileId.Warning);
						break;
					}
					output.Write("\" alt=\"\"></td>");
					output.Write("<td class=\"w100\">");
					if (infobarMessage.Type == InfobarMessageType.Prompt)
					{
						Infobar.RenderPromptMessage(output, infobarMessage);
					}
					else
					{
						Infobar.RenderInfoMessage(output, infobarMessage);
					}
					output.Write("</td></tr>");
					i++;
				}
				if (flag && i < num)
				{
					output.Write("<tr><td class=\"w100\">");
				}
				for (int j = i; j < num; j++)
				{
					infobarMessage = (InfobarMessage)this.messages[j];
					if (infobarMessage.Type == InfobarMessageType.Expanding)
					{
						Infobar.RenderExpandingMessage(output, infobarMessage, j > 0);
					}
					else if (infobarMessage.Type == InfobarMessageType.Prompt)
					{
						Infobar.RenderPromptMessage(output, infobarMessage);
					}
					else
					{
						Infobar.RenderInfoMessage(output, infobarMessage);
					}
				}
				if (flag && i < num)
				{
					output.Write("</td></tr>");
				}
				if (flag)
				{
					output.Write("</table>");
				}
				output.Write("</div>");
			}
			output.Write("</div>");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00013334 File Offset: 0x00011534
		public static void RenderInfoMessage(TextWriter output, InfobarMessage infobarMessage)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (infobarMessage == null)
			{
				throw new ArgumentNullException("infobarMessage");
			}
			output.Write("<div class=\"w100\"");
			if (!string.IsNullOrEmpty(infobarMessage.TagId))
			{
				output.Write(" id=\"");
				Utilities.SanitizeHtmlEncode(infobarMessage.TagId, output);
				output.Write("\"");
			}
			output.Write(">");
			if (infobarMessage.Type == InfobarMessageType.Error || infobarMessage.IsActionResult)
			{
				output.Write("<h1>");
			}
			infobarMessage.RenderMessageString(output);
			if (infobarMessage.Type == InfobarMessageType.Error || infobarMessage.IsActionResult)
			{
				output.Write("</h1>");
			}
			output.Write("</div>");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000133EC File Offset: 0x000115EC
		private static void RenderPromptMessage(TextWriter output, InfobarMessage infobarMessage)
		{
			output.Write("<table cellpadding=\"0\" cellspacing=\"0\" class=\"w100\"><tr><td>");
			output.Write("<div id=\"divIbPm\"><span class=\"vam\"><h1 tabindex=0>&nbsp;");
			infobarMessage.RenderMessageString(output);
			output.Write("</h1></span></div>");
			if (!SanitizedStringBase<OwaHtml>.IsNullOrEmpty(infobarMessage.BodyHtml))
			{
				output.Write("<table cellpadding=\"4\" cellspacing=\"4\" class=\"w100\"><tr><td class=\"ibsubbox\">");
				output.Write(infobarMessage.BodyHtml);
				output.Write("</td></tr>");
				output.Write("</table>");
			}
			if (!SanitizedStringBase<OwaHtml>.IsNullOrEmpty(infobarMessage.FooterHtml))
			{
				output.Write("<tr><td><div id=\"divIbPb\" tabindex=0>");
				output.Write(infobarMessage.FooterHtml);
				output.Write("</div></td></tr>");
			}
			output.Write("</td></tr></table>");
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00013498 File Offset: 0x00011698
		private static void RenderExpandingMessage(TextWriter output, InfobarMessage infobarMessage, bool isVerticalSpaceRequired)
		{
			output.Write("<div class=\"iem");
			if (isVerticalSpaceRequired)
			{
				output.Write(" vsp");
			}
			output.Write("\">");
			infobarMessage.RenderMessageString(output);
			output.Write("</div>");
			output.Write("<div id=divIbE{0}>", infobarMessage.IsExpanding ? string.Empty : " style=\"display:none\"");
			output.Write(infobarMessage.ExpandSectionHtml);
			output.Write("</div>");
		}

		// Token: 0x04000184 RID: 388
		private ArrayList messages = new ArrayList(2);

		// Token: 0x04000185 RID: 389
		private UserContext userContext;

		// Token: 0x0200004D RID: 77
		private class InfobarMessageComparer : IComparer
		{
			// Token: 0x060001FF RID: 511 RVA: 0x00013514 File Offset: 0x00011714
			public int Compare(object objectX, object objectY)
			{
				if (objectX == null)
				{
					throw new ArgumentNullException("objectX");
				}
				if (objectY == null)
				{
					throw new ArgumentNullException("objectY");
				}
				InfobarMessage infobarMessage = objectX as InfobarMessage;
				InfobarMessage infobarMessage2 = objectY as InfobarMessage;
				return infobarMessage.Type - infobarMessage2.Type;
			}
		}
	}
}

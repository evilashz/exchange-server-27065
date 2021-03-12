using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000390 RID: 912
	public class Infobar
	{
		// Token: 0x06002295 RID: 8853 RVA: 0x000C6131 File Offset: 0x000C4331
		public Infobar()
		{
			this.sessionContext = OwaContext.Current.SessionContext;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000C616B File Offset: 0x000C436B
		public Infobar(string divErrorId, string barClass) : this()
		{
			this.divErrorId = divErrorId;
			this.barClass = barClass;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000C6181 File Offset: 0x000C4381
		public int MessageCount
		{
			get
			{
				return this.messages.Count;
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000C618E File Offset: 0x000C438E
		public void AddMessage(InfobarMessage infobarMessage)
		{
			this.messages.Add(infobarMessage);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000C619D File Offset: 0x000C439D
		public void AddMessage(Strings.IDs messageString, InfobarMessageType type)
		{
			this.AddMessage(SanitizedHtmlString.FromStringId(messageString), type);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000C61AC File Offset: 0x000C43AC
		public void AddMessage(SanitizedHtmlString messageHtml, InfobarMessageType type)
		{
			this.messages.Add(new InfobarMessage(messageHtml, type));
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000C61C1 File Offset: 0x000C43C1
		public void AddMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, string tagId)
		{
			this.messages.Add(new InfobarMessage(messageHtml, type, tagId));
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000C61D7 File Offset: 0x000C43D7
		public void AddMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, string tagId, bool hideMessage)
		{
			this.messages.Add(new InfobarMessage(messageHtml, type, tagId, hideMessage));
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000C61EF File Offset: 0x000C43EF
		public void AddMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, SanitizedHtmlString linkText, SanitizedHtmlString expandSection)
		{
			this.messages.Add(new InfobarMessage(messageHtml, type, null, linkText, expandSection));
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000C6208 File Offset: 0x000C4408
		public void AddMessage(SanitizedHtmlString messageHtml, InfobarMessageType type, string tagId, SanitizedHtmlString linkText, SanitizedHtmlString expandSection)
		{
			this.messages.Add(new InfobarMessage(messageHtml, type, tagId, linkText, expandSection));
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000C6222 File Offset: 0x000C4422
		public static void RenderMessage(TextWriter output, InfobarMessage infobarMessage, ISessionContext sessionContext)
		{
			if (infobarMessage == null)
			{
				throw new ArgumentNullException("infobarMessage");
			}
			Infobar.RenderMessage(output, infobarMessage.Type, infobarMessage.Message, infobarMessage.TagId, false, sessionContext);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000C624C File Offset: 0x000C444C
		public static void RenderMessage(TextWriter output, InfobarMessageType messageType, SanitizedHtmlString messageHtml, ISessionContext sessionContext)
		{
			Infobar.RenderMessage(output, messageType, messageHtml, null, false, sessionContext);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000C625C File Offset: 0x000C445C
		public static void RenderMessage(TextWriter output, InfobarMessageType messageType, SanitizedHtmlString messageHtml, string messageId, bool hideMessage, ISessionContext sessionContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (messageHtml == null)
			{
				throw new ArgumentNullException("messageHtml");
			}
			Infobar.RenderMessageIdAndClass(output, messageType, messageId, sessionContext);
			output.Write("\"");
			if (hideMessage)
			{
				output.Write(" style=\"display:none\" ");
				output.Write("isVisible");
				output.Write("=0");
			}
			else
			{
				output.Write(" ");
				output.Write("isVisible");
				output.Write("=1");
			}
			output.Write(">");
			output.Write(sessionContext.IsRtl ? "<div class=\"fltRight\">" : "<div class=\"fltLeft\">");
			sessionContext.RenderThemeImage(output, ThemeFileId.Dash, sessionContext.IsRtl ? "rtl dashImg" : "dashImg", new object[]
			{
				"id=imgDash"
			});
			output.Write("</div>");
			output.Write(messageHtml);
			output.Write("</div>");
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000C6357 File Offset: 0x000C4557
		public void Render(TextWriter output)
		{
			this.Render(output, true);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000C6361 File Offset: 0x000C4561
		public void Render(TextWriter output, bool isEditable)
		{
			this.Render(output, isEditable, false);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000C636C File Offset: 0x000C456C
		public void Render(TextWriter output, bool isEditable, bool renderHidden)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=\"divInfobar\"");
			output.Write(" class=\"");
			output.Write(this.barClass);
			if (this.sessionContext.IsRtl)
			{
				output.Write(" rtl");
			}
			output.Write("\"");
			if (this.ShouldHideInfobar(renderHidden))
			{
				output.Write(" style=\"display:none\"");
			}
			output.Write(">");
			output.Write("<div id=\"divInfobarColor\"");
			output.Write(" class=\"");
			if (Infobar.HasHighSeverityMessages(this.messages))
			{
				output.Write("highSeverity");
			}
			else if (0 < this.messages.Count)
			{
				output.Write("lowSeverity");
			}
			output.Write("\"></div>");
			output.Write("<div id=\"divIB\">");
			RenderingUtilities.RenderErrorInfobar(this.sessionContext, output, this.divErrorId);
			Infobar.InfobarMessageComparer comparer = new Infobar.InfobarMessageComparer();
			this.messages.Sort(comparer);
			InfobarMessageType infobarMessageType = InfobarMessageType.Maximum;
			foreach (object obj in this.messages)
			{
				InfobarMessage infobarMessage = (InfobarMessage)obj;
				InfobarMessageType type = infobarMessage.Type;
				if (type == InfobarMessageType.Expanding || type == InfobarMessageType.ExpandingError)
				{
					Infobar.RenderExpandingMessage(output, infobarMessage.Type, infobarMessage.Message, infobarMessage.TagId, infobarMessage.LinkText, infobarMessage.ExpandSection, this.sessionContext, infobarMessageType == infobarMessage.Type);
				}
				else
				{
					Infobar.RenderMessage(output, infobarMessage.Type, infobarMessage.Message, infobarMessage.TagId, infobarMessage.HideMessage, this.sessionContext);
				}
				infobarMessageType = infobarMessage.Type;
			}
			output.Write("</div></div>");
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000C653C File Offset: 0x000C473C
		public static void RenderExpandingMessage(TextWriter output, InfobarMessageType messageType, SanitizedHtmlString message, string messageId, SanitizedHtmlString linkText, SanitizedHtmlString expandSection, ISessionContext sessionContext)
		{
			Infobar.RenderExpandingMessage(output, messageType, message, messageId, linkText, expandSection, sessionContext, false);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000C6550 File Offset: 0x000C4750
		public static void RenderExpandingMessage(TextWriter output, InfobarMessageType messageType, SanitizedHtmlString message, string messageId, SanitizedHtmlString linkText, SanitizedHtmlString expandSection, ISessionContext sessionContext, bool isVerticalSpaceRequired)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (linkText == null)
			{
				throw new ArgumentNullException("linkText");
			}
			if (expandSection == null)
			{
				throw new ArgumentNullException("expandSection");
			}
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			Infobar.RenderMessageIdAndClass(output, messageType, messageId, sessionContext);
			if (isVerticalSpaceRequired)
			{
				output.Write(" vsp");
			}
			output.Write("\">");
			string styleClass = sessionContext.IsRtl ? "rtl dashImg" : "dashImg";
			sessionContext.RenderThemeImage(output, ThemeFileId.Dash, styleClass, new object[]
			{
				"id=imgDash"
			});
			if (messageType == InfobarMessageType.ExpandingError)
			{
				output.Write("<span class=\"ibM\">");
			}
			output.Write(message);
			if (messageType == InfobarMessageType.ExpandingError)
			{
				output.Write("</span>");
			}
			output.Write("<span id=spnIbL ");
			Utilities.RenderScriptHandler(output, "onclick", "tglInfo(_this);");
			output.Write(">");
			output.Write(linkText);
			sessionContext.RenderThemeImage(output, ThemeFileId.Expand, null, new object[]
			{
				"id=imgExp"
			});
			output.Write("</span>");
			output.Write("<div id=divIbE ");
			Utilities.RenderScriptHandler(output, "onclick", "canEvt(event);");
			output.Write(" style=\"display:none\">");
			output.Write(expandSection);
			output.Write("</div></div>");
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000C66B0 File Offset: 0x000C48B0
		public void SetInfobarClass(string barClass)
		{
			this.barClass = barClass;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000C66B9 File Offset: 0x000C48B9
		public void SetShouldHonorHideByDefault(bool shouldHonorHideByDefault)
		{
			this.shouldHonorHideByDefault = shouldHonorHideByDefault;
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000C66C2 File Offset: 0x000C48C2
		public string InfobarClass
		{
			get
			{
				return this.barClass;
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000C66CC File Offset: 0x000C48CC
		private static bool HasHighSeverityMessages(ArrayList messages)
		{
			foreach (object obj in messages)
			{
				InfobarMessage infobarMessage = (InfobarMessage)obj;
				if (InfobarMessageType.Informational4 < infobarMessage.Type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000C672C File Offset: 0x000C492C
		private bool ShouldHideInfobar(bool renderHidden)
		{
			if (renderHidden || (this.shouldHonorHideByDefault && this.sessionContext.HideMailTipsByDefault))
			{
				return true;
			}
			foreach (object obj in this.messages)
			{
				InfobarMessage infobarMessage = (InfobarMessage)obj;
				if (!infobarMessage.HideMessage)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000C67A8 File Offset: 0x000C49A8
		private static void RenderMessageIdAndClass(TextWriter output, InfobarMessageType messageType, string messageId, ISessionContext sessionContext)
		{
			output.Write("<div ");
			if (string.IsNullOrEmpty(messageId))
			{
				messageId = "divInfobarMessage";
			}
			output.Write(SanitizedHtmlString.Format(" id=\"{0}\"", new object[]
			{
				messageId
			}));
			output.Write(" iType=");
			output.Write((int)messageType);
			output.Write(" class=\"");
			output.Write("infobarMessageItem");
			if (sessionContext.IsRtl)
			{
				output.Write(" rtl");
			}
			switch (messageType)
			{
			case InfobarMessageType.ExpandingError:
			case InfobarMessageType.Error:
				output.Write(" error");
				return;
			case InfobarMessageType.JunkEmail:
				output.Write(" junk");
				return;
			case InfobarMessageType.Phishing:
				output.Write(" phishing");
				return;
			case InfobarMessageType.Warning:
				output.Write(" warning");
				return;
			default:
				return;
			}
		}

		// Token: 0x0400185F RID: 6239
		private const string InfobarsVisibleAttribute = "isVisible";

		// Token: 0x04001860 RID: 6240
		private const string InfobarMessageItemClass = "infobarMessageItem";

		// Token: 0x04001861 RID: 6241
		private const string DefaultInfobarMessageDivId = "divInfobarMessage";

		// Token: 0x04001862 RID: 6242
		private ArrayList messages = new ArrayList(2);

		// Token: 0x04001863 RID: 6243
		private ISessionContext sessionContext;

		// Token: 0x04001864 RID: 6244
		private string divErrorId = "divErr";

		// Token: 0x04001865 RID: 6245
		private string barClass = "infobar";

		// Token: 0x04001866 RID: 6246
		private bool shouldHonorHideByDefault;

		// Token: 0x02000391 RID: 913
		private class InfobarMessageComparer : IComparer
		{
			// Token: 0x060022AD RID: 8877 RVA: 0x000C6874 File Offset: 0x000C4A74
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
				if (infobarMessage.Type > infobarMessage2.Type)
				{
					return -1;
				}
				if (infobarMessage.Type < infobarMessage2.Type)
				{
					return 1;
				}
				return infobarMessage.Message.CompareTo(infobarMessage2.Message);
			}
		}
	}
}

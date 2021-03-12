using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000002 RID: 2
	public abstract class OptionsBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public OptionsBase(OwaContext owaContext, TextWriter writer)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.request = owaContext.HttpContext.Request;
			this.userContext = owaContext.UserContext;
			this.writer = writer;
			this.command = Utilities.GetFormParameter(this.request, "hidcmdpst", false);
		}

		// Token: 0x06000002 RID: 2
		public abstract void Render();

		// Token: 0x06000003 RID: 3
		public abstract void RenderScript();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000214C File Offset: 0x0000034C
		public bool ShowInfoBar
		{
			get
			{
				return !string.IsNullOrEmpty(this.commitStatus);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		public InfobarMessageType InfobarMessageType
		{
			get
			{
				return this.infobarMessageType;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002164 File Offset: 0x00000364
		protected string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
		protected void SetSavedSuccessfully(bool savedSuccessfully)
		{
			if (savedSuccessfully)
			{
				this.SetInfobarMessage(LocalizedStrings.GetNonEncoded(191284072), InfobarMessageType.Informational);
				return;
			}
			this.SetInfobarMessage(LocalizedStrings.GetNonEncoded(-1203841103), InfobarMessageType.Error);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
		protected void SetInfobarMessage(string message, InfobarMessageType type)
		{
			this.commitStatus = message;
			this.infobarMessageType = type;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A4 File Offset: 0x000003A4
		protected void RenderJSVariable(string varName, string value)
		{
			if (string.IsNullOrEmpty(varName))
			{
				throw new ArgumentException("varName can not be null or empty string");
			}
			this.writer.Write("var ");
			this.writer.Write(varName);
			this.writer.Write(" = ");
			this.writer.Write(value);
			this.writer.Write(";");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220C File Offset: 0x0000040C
		protected void RenderJSVariable(string varName, bool value)
		{
			if (string.IsNullOrEmpty(varName))
			{
				throw new ArgumentException("varName can not be null or empty string");
			}
			this.writer.Write("var ");
			this.writer.Write(varName);
			this.writer.Write(" = ");
			this.writer.Write(value ? "true" : "false");
			this.writer.Write(";");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002284 File Offset: 0x00000484
		protected void RenderJSVariableWithQuotes(string varName, string value)
		{
			if (string.IsNullOrEmpty(varName))
			{
				throw new ArgumentException("varName can not be null or empty string");
			}
			this.writer.Write("var ");
			this.writer.Write(varName);
			this.writer.Write(" = \"");
			this.writer.Write(Utilities.JavascriptEncode(value));
			this.writer.Write("\";");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F1 File Offset: 0x000004F1
		protected void RenderHeaderRow(ThemeFileId themeFileId, Strings.IDs headerStringId)
		{
			this.RenderHeaderRow(themeFileId, headerStringId, 1);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022FC File Offset: 0x000004FC
		protected void RenderHeaderRow(ThemeFileId themeFileId, Strings.IDs headerStringId, int colspan)
		{
			this.writer.Write("<tr><td");
			if (colspan > 1)
			{
				this.writer.Write(" colspan=");
				this.writer.Write(colspan);
			}
			this.writer.Write(" class=\"hdr\"><img src=\"");
			this.userContext.RenderThemeFileUrl(this.writer, themeFileId);
			this.writer.Write("\" alt=\"\"><h1 class=\"bld\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(headerStringId));
			this.writer.Write("</h1></td></tr>");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000238C File Offset: 0x0000058C
		public void RenderInfobar()
		{
			if (this.ShowInfoBar)
			{
				InfobarMessage infobarMessage = InfobarMessage.CreateText(this.commitStatus, this.infobarMessageType);
				infobarMessage.IsActionResult = true;
				this.infobar.AddMessage(infobarMessage);
				this.infobar.Render(this.writer);
			}
		}

		// Token: 0x04000001 RID: 1
		private const string CommandParameter = "hidcmdpst";

		// Token: 0x04000002 RID: 2
		private string commitStatus;

		// Token: 0x04000003 RID: 3
		protected UserContext userContext;

		// Token: 0x04000004 RID: 4
		protected TextWriter writer;

		// Token: 0x04000005 RID: 5
		protected HttpRequest request;

		// Token: 0x04000006 RID: 6
		protected Infobar infobar = new Infobar();

		// Token: 0x04000007 RID: 7
		private InfobarMessageType infobarMessageType = InfobarMessageType.Informational;

		// Token: 0x04000008 RID: 8
		private string command;
	}
}

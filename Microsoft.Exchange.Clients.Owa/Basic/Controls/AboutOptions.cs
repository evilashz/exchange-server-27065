using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000003 RID: 3
	public class AboutOptions : OptionsBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000023D7 File Offset: 0x000005D7
		public AboutOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.Load(owaContext);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023E8 File Offset: 0x000005E8
		private void Load(OwaContext owaContext)
		{
			this.about = new AboutDetails(owaContext);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023F8 File Offset: 0x000005F8
		public override void Render()
		{
			string s = null;
			bool flag = false;
			base.RenderHeaderRow(ThemeFileId.AboutOwa, 282998996, 2);
			this.writer.Write("<tr><td colspan=2 class=\"optAbSpt\">");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(2138124634));
			this.writer.Write("</td></tr>");
			this.writer.Write("<tr><td colspan=2 class=\"optAbSpc\"></td></tr>");
			for (int i = 0; i < this.about.Count; i++)
			{
				Strings.IDs localizedID;
				this.about.GetDetails(i, out localizedID, out s, out flag);
				this.writer.Write("<tr><td class=\"optAb\">");
				if (flag)
				{
					this.writer.Write("&nbsp; &nbsp; &nbsp;");
				}
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(localizedID));
				this.writer.Write(":</td><td class=\"optAbVal\">");
				this.writer.Write(Utilities.HtmlEncode(s));
				this.writer.Write("</td></tr>");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024EC File Offset: 0x000006EC
		public override void RenderScript()
		{
		}

		// Token: 0x04000009 RID: 9
		private AboutDetails about;
	}
}

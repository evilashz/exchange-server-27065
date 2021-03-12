using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003C9 RID: 969
	internal abstract class NavigationBarItemBase
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000D0033 File Offset: 0x000CE233
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000D003B File Offset: 0x000CE23B
		protected virtual bool IsCurrentModule(NavigationModule module)
		{
			return false;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000D003E File Offset: 0x000CE23E
		protected NavigationBarItemBase(UserContext userContext, string text, string idSuffix)
		{
			this.userContext = userContext;
			this.text = text;
			this.idSuffix = idSuffix;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000D005C File Offset: 0x000CE25C
		public void Render(TextWriter writer, NavigationModule currentModule, int wunderBarWidth, bool useSmallIcon)
		{
			bool flag = wunderBarWidth != 0;
			writer.Write("<a href=\"#\" class=\"nbMnuItm");
			if (flag)
			{
				writer.Write(useSmallIcon ? " nbMnuItmWS" : " nbMnuItmWB");
			}
			else
			{
				writer.Write(" nbMnuItmN");
			}
			writer.Write(this.IsCurrentModule(currentModule) ? " nbHiLt" : " nbNoHiLt");
			writer.Write("\"");
			if (flag)
			{
				writer.Write(" style=\"width:");
				writer.Write(wunderBarWidth);
				writer.Write("%\"");
			}
			if (this.idSuffix != null)
			{
				writer.Write(" id=\"");
				writer.Write((flag ? "lnkQl" : "lnk") + this.idSuffix);
				writer.Write("\" ");
			}
			this.RenderOnClickHandler(writer, currentModule);
			if (flag)
			{
				writer.Write(" title=\"");
				Utilities.HtmlEncode(this.text, writer);
				writer.Write("\"");
			}
			writer.Write(">");
			this.RenderImageTag(writer, useSmallIcon, flag);
			if (!flag)
			{
				writer.Write("<span class=\"nbMainTxt\">");
				Utilities.HtmlEncode(this.text, writer);
				writer.Write("</span>");
			}
			writer.Write("</a>");
		}

		// Token: 0x0600240F RID: 9231
		protected abstract void RenderImageTag(TextWriter writer, bool useSmallIcons, bool isWunderBar);

		// Token: 0x06002410 RID: 9232
		protected abstract void RenderOnClickHandler(TextWriter writer, NavigationModule currentModule);

		// Token: 0x040018FE RID: 6398
		private const string LnkIdPrefix = "lnk";

		// Token: 0x040018FF RID: 6399
		private const string LnkIdQlPrefix = "lnkQl";

		// Token: 0x04001900 RID: 6400
		private readonly UserContext userContext;

		// Token: 0x04001901 RID: 6401
		private readonly string text;

		// Token: 0x04001902 RID: 6402
		private readonly string idSuffix;
	}
}

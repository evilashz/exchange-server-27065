using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003AC RID: 940
	public class ManageCategoriesDialog
	{
		// Token: 0x06002356 RID: 9046 RVA: 0x000CB776 File Offset: 0x000C9976
		public ManageCategoriesDialog(UserContext userContext)
		{
			this.userContext = userContext;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000CB788 File Offset: 0x000C9988
		public void Render(TextWriter writer)
		{
			ManageCategoriesDialog.RenderStrings(writer);
			writer.Write("<div id=divCtgLst tabindex=0>");
			MasterCategoryList masterCategoryList = this.userContext.GetMasterCategoryList();
			foreach (Category category in masterCategoryList)
			{
				ManageCategoriesDialog.RenderCategory(writer, category);
			}
			writer.Write("</div>");
			writer.Write("<div class=mngCtgLnk><a href=# id=\"lnkCrtCat\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1641878163));
			writer.Write("</a></div><div class=mngCtgLnk><a href=# id=\"lnkChgCatClr\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(782364430));
			writer.Write("</a></div><div class=mngCtgLnk><a href=# id=\"lnkDelCat\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(1478174110));
			writer.Write("</a></div>");
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000CB854 File Offset: 0x000C9A54
		internal static void RenderCategory(TextWriter writer, Category category)
		{
			string text = category.Name;
			string text2 = null;
			if (50 < text.Length)
			{
				text = text.Substring(0, 50) + "...";
				text2 = category.Name;
			}
			writer.Write("<div nowrap");
			if (text2 != null)
			{
				writer.Write(" title=\"");
				Utilities.HtmlEncode(text2, writer);
				writer.Write("\"");
			}
			writer.Write(">");
			CategorySwatch.RenderSwatch(writer, category);
			writer.Write("&nbsp;");
			Utilities.HtmlEncode(text, writer, true);
			writer.Write("</div>");
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000CB8EC File Offset: 0x000C9AEC
		private static void RenderStrings(TextWriter writer)
		{
			writer.Write("<div id=divStr style=display:none L_MngCtgs=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1754071941));
			writer.Write("\" L_DltCtg=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1566500907));
			writer.Write("\" L_CnfDlt=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(367329051));
			writer.Write("\" L_ExpDlt=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(1365940928));
			writer.Write("\"></div>");
		}

		// Token: 0x040018B0 RID: 6320
		private const int MaximumCharactersToDisplayCategoryName = 50;

		// Token: 0x040018B1 RID: 6321
		private UserContext userContext;
	}
}

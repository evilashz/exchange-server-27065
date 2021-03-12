using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000332 RID: 818
	public abstract class CategoryMenuBase : ContextMenu
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x000B2BDD File Offset: 0x000B0DDD
		public CategoryMenuBase(string id, UserContext userContext) : base(id, userContext)
		{
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000B2BE8 File Offset: 0x000B0DE8
		internal void RenderCategoryMenuItem(TextWriter output, Category category, string id)
		{
			this.currentCategory = category;
			base.RenderMenuItem(output, null, ThemeFileId.CheckUnchecked, id, Utilities.HtmlEncode("cat:" + category.Name), false, null, null, null, new ContextMenu.RenderMenuItemHtml(this.RenderCategoryMenuItemHtml));
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000B2C30 File Offset: 0x000B0E30
		private void RenderCategoryMenuItemHtml(TextWriter output)
		{
			string text = this.currentCategory.Name;
			bool flag = false;
			if (35 < text.Length)
			{
				text = text.Substring(0, 35) + "...";
				flag = true;
			}
			if (flag)
			{
				output.Write("<span title=\"");
				Utilities.HtmlEncode(this.currentCategory.Name, output);
				output.Write("\">");
			}
			CategorySwatch.RenderSwatch(output, this.currentCategory);
			output.Write(" ");
			Utilities.HtmlEncode(text, output);
			if (flag)
			{
				output.Write("</span>");
			}
		}

		// Token: 0x040016AE RID: 5806
		private const int MaximumCharactersToDisplayCategoryName = 35;

		// Token: 0x040016AF RID: 5807
		protected const string CategoryPrefix = "cat:";

		// Token: 0x040016B0 RID: 5808
		private Category currentCategory;

		// Token: 0x02000333 RID: 819
		public enum CategoryState
		{
			// Token: 0x040016B2 RID: 5810
			UnChecked,
			// Token: 0x040016B3 RID: 5811
			PartialCheck,
			// Token: 0x040016B4 RID: 5812
			Checked
		}
	}
}

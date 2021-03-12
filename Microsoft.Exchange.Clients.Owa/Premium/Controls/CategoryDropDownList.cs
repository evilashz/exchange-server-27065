using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000335 RID: 821
	internal sealed class CategoryDropDownList : DropDownList
	{
		// Token: 0x06001F22 RID: 7970 RVA: 0x000B2F3C File Offset: 0x000B113C
		private CategoryDropDownList(OwaStoreObjectId folderId) : base("divCDd", null, null)
		{
			UserContext userContext = UserContextManager.GetUserContext();
			MasterCategoryList masterCategoryList = userContext.GetMasterCategoryList(folderId);
			if (masterCategoryList != null)
			{
				this.categories = masterCategoryList.ToArray();
				Array.Sort<Category>(this.categories, new MostRecentlyUsedCategories.CategoryNameComparer());
				if (0 < this.categories.Length)
				{
					this.selectedCategory = this.categories[0];
					base.SelectedValue = this.selectedCategory.Name;
					return;
				}
			}
			else
			{
				this.categories = new Category[0];
			}
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000B2FBC File Offset: 0x000B11BC
		public static void RenderCategoryDropDownList(TextWriter writer, OwaStoreObjectId folderId)
		{
			CategoryDropDownList categoryDropDownList = new CategoryDropDownList(folderId);
			categoryDropDownList.Render(writer);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000B2FD7 File Offset: 0x000B11D7
		protected override void RenderSelectedValue(TextWriter writer)
		{
			if (this.selectedCategory != null)
			{
				writer.Write(CategoryDropDownList.GetCategoryHtml(this.selectedCategory));
			}
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000B2FF4 File Offset: 0x000B11F4
		private static string GetCategoryHtml(Category category)
		{
			string text = category.Name;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			if (35 < text.Length)
			{
				text = text.Substring(0, 35) + "...";
				flag = true;
			}
			stringWriter.Write("<span class=\"listItm\"");
			if (flag)
			{
				stringWriter.Write(" title=\"");
				Utilities.HtmlEncode(category.Name, stringWriter);
				stringWriter.Write("\" ");
			}
			stringWriter.Write(">");
			CategorySwatch.RenderSwatch(stringWriter, category);
			stringWriter.Write("&nbsp;");
			Utilities.HtmlEncode(text, stringWriter);
			stringWriter.Write("</span>");
			stringWriter.Close();
			return stringBuilder.ToString();
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000B30A4 File Offset: 0x000B12A4
		protected override DropDownListItem[] CreateListItems()
		{
			DropDownListItem[] array = new DropDownListItem[this.categories.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DropDownListItem(this.categories[i].Name, CategoryDropDownList.GetCategoryHtml(this.categories[i]), true);
			}
			return array;
		}

		// Token: 0x040016B8 RID: 5816
		private const int MaximumCharactersToDisplayCategoryName = 35;

		// Token: 0x040016B9 RID: 5817
		private Category[] categories;

		// Token: 0x040016BA RID: 5818
		private Category selectedCategory;
	}
}

using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003DA RID: 986
	public sealed class OtherCategoriesMenu : CategoryMenuBase
	{
		// Token: 0x0600245B RID: 9307 RVA: 0x000D382F File Offset: 0x000D1A2F
		private OtherCategoriesMenu(UserContext userContext, string menuId) : base(menuId, userContext)
		{
			this.shouldScroll = true;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000D3840 File Offset: 0x000D1A40
		internal static OtherCategoriesMenu Create(UserContext userContext, Category[] categories, ContextMenu parentMenu)
		{
			return new OtherCategoriesMenu(userContext, parentMenu.Id + "O")
			{
				categories = categories
			};
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000D386C File Offset: 0x000D1A6C
		protected override void RenderMenuItems(TextWriter output)
		{
			for (int i = 0; i < this.categories.Length; i++)
			{
				base.RenderCategoryMenuItem(output, this.categories[i], string.Format(CultureInfo.InvariantCulture, "divCatOth{0}", new object[]
				{
					i
				}));
			}
		}

		// Token: 0x0400193E RID: 6462
		private Category[] categories;
	}
}

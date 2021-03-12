using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000334 RID: 820
	public sealed class CategoryContextMenu : CategoryMenuBase
	{
		// Token: 0x06001F1B RID: 7963 RVA: 0x000B2CC1 File Offset: 0x000B0EC1
		private CategoryContextMenu(UserContext userContext, OwaStoreObjectId folderId, string menuId) : base(menuId, userContext)
		{
			this.folderId = folderId;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000B2CD4 File Offset: 0x000B0ED4
		internal static void Render(UserContext userContext, TextWriter output, OutlookModule outlookModule, OwaStoreObjectId folderId)
		{
			new CategoryContextMenu(userContext, folderId, "divCatM")
			{
				outlookModule = outlookModule,
				renderAdditionalMenuItem = true
			}.Render(output);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000B2D04 File Offset: 0x000B0F04
		internal static CategoryContextMenu Create(UserContext userContext, OutlookModule outlookModule, string menuId, bool renderAdditionalMenuItem)
		{
			return new CategoryContextMenu(userContext, null, menuId)
			{
				renderAdditionalMenuItem = renderAdditionalMenuItem,
				outlookModule = outlookModule
			};
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000B2D29 File Offset: 0x000B0F29
		protected override void RenderExpandoData(TextWriter output)
		{
			output.Write(" sPfx=");
			output.Write("cat:");
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000B2D44 File Offset: 0x000B0F44
		protected override void RenderMenuItems(TextWriter output)
		{
			MasterCategoryList masterCategoryList = this.userContext.GetMasterCategoryList(this.folderId);
			if (masterCategoryList != null)
			{
				MostRecentlyUsedCategories mostRecentlyUsedCategories = MostRecentlyUsedCategories.Create(masterCategoryList, this.outlookModule);
				for (int i = 0; i < mostRecentlyUsedCategories.MostRecentCategories.Length; i++)
				{
					base.RenderCategoryMenuItem(output, mostRecentlyUsedCategories.MostRecentCategories[i], string.Format(CultureInfo.InvariantCulture, "divCatRct{0}", new object[]
					{
						i
					}));
				}
				if (mostRecentlyUsedCategories.OtherCategories != null && 0 < mostRecentlyUsedCategories.OtherCategories.Length)
				{
					base.RenderMenuItem(output, 1005271872, ThemeFileId.None, null, null, false, null, null, OtherCategoriesMenu.Create(this.userContext, mostRecentlyUsedCategories.OtherCategories, this));
				}
			}
			if (this.renderAdditionalMenuItem)
			{
				if (this.folderId == null || !this.folderId.IsOtherMailbox)
				{
					base.RenderMenuItem(output, -1639820326, "mng", masterCategoryList == null);
				}
				ContextMenu.RenderMenuDivider(output, null);
				base.RenderMenuItem(output, -108608469, "clr");
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000B2E3C File Offset: 0x000B103C
		internal static void ModifyCategories(Item item, string[] addCategories, string[] removeCategories)
		{
			if (removeCategories != null)
			{
				for (int i = 0; i < removeCategories.Length; i++)
				{
					item.Categories.Remove(removeCategories[i]);
				}
			}
			if (addCategories != null)
			{
				for (int j = 0; j < addCategories.Length; j++)
				{
					if (!item.Categories.Contains(addCategories[j]))
					{
						item.Categories.Add(addCategories[j]);
					}
				}
			}
			if (item.Categories.Count == 0)
			{
				CategoryContextMenu.ClearCategories(item);
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000B2EAC File Offset: 0x000B10AC
		internal static void ClearCategories(Item item)
		{
			item.Categories.Clear();
			item.DeleteProperties(new PropertyDefinition[]
			{
				CalendarItemBaseSchema.AppointmentColor
			});
			FlagStatus property = ItemUtility.GetProperty<FlagStatus>(item, ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			if (property != FlagStatus.Complete)
			{
				bool property2 = ItemUtility.GetProperty<bool>(item, ItemSchema.IsToDoItem, false);
				if (property2)
				{
					item[ItemSchema.ItemColor] = ItemColor.Red;
					return;
				}
				item.DeleteProperties(new PropertyDefinition[]
				{
					ItemSchema.ItemColor
				});
				if (!(item is CalendarItemBase) && !(item is Task))
				{
					item.ClearFlag();
				}
			}
		}

		// Token: 0x040016B5 RID: 5813
		private OutlookModule outlookModule;

		// Token: 0x040016B6 RID: 5814
		private OwaStoreObjectId folderId;

		// Token: 0x040016B7 RID: 5815
		private bool renderAdditionalMenuItem;
	}
}

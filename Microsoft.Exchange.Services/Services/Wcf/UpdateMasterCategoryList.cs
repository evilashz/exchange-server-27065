using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200093F RID: 2367
	internal class UpdateMasterCategoryList : ServiceCommand<UpdateMasterCategoryListResponse>
	{
		// Token: 0x06004480 RID: 17536 RVA: 0x000EBD26 File Offset: 0x000E9F26
		public UpdateMasterCategoryList(CallContext context, UpdateMasterCategoryListRequest request) : base(context)
		{
			this.request = request;
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000EBD38 File Offset: 0x000E9F38
		protected override UpdateMasterCategoryListResponse InternalExecute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.MailboxIdentityMailboxSession;
			bool flag = false;
			this.masterCategoryList = MasterCategoryListHelper.GetMasterCategoryList(mailboxIdentityMailboxSession, base.CallContext.OwaCulture);
			if (this.request.RemoveCategoryList != null)
			{
				bool flag2 = this.RemoveCategories();
				if (flag2)
				{
					flag = true;
				}
			}
			if (this.request.AddCategoryList != null)
			{
				bool flag2 = this.AddCategories();
				if (flag2)
				{
					flag = true;
				}
			}
			if (this.request.ChangeCategoryColorList != null)
			{
				bool flag2 = this.ChangeCategoriesColor();
				if (flag2)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.masterCategoryList.Save();
			}
			return new UpdateMasterCategoryListResponse(MasterCategoryListHelper.GetMasterList(this.masterCategoryList));
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x000EBDD0 File Offset: 0x000E9FD0
		private bool AddCategories()
		{
			bool result = false;
			int num = this.request.AddCategoryList.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = this.AddCategory(this.request.AddCategoryList[i]);
				if (flag)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000EBE14 File Offset: 0x000EA014
		private bool RemoveCategories()
		{
			bool result = false;
			int num = this.request.RemoveCategoryList.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = this.DeleteCategory(this.request.RemoveCategoryList[i]);
				if (flag)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x000EBE58 File Offset: 0x000EA058
		private bool ChangeCategoriesColor()
		{
			bool result = false;
			int num = this.request.ChangeCategoryColorList.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = this.ChangeCategoryColor(this.request.ChangeCategoryColorList[i]);
				if (flag)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000EBE9C File Offset: 0x000EA09C
		private bool AddCategory(CategoryType categoryType)
		{
			string text = categoryType.Name;
			text = text.Trim();
			int color = categoryType.Color;
			if (!text.Contains(",") && !text.Contains(";") && !text.Contains("؛") && !text.Contains("﹔"))
			{
				text.Contains("；");
			}
			bool result = false;
			if (!this.masterCategoryList.Contains(text))
			{
				Category item = Category.Create(text, color, false);
				this.masterCategoryList.Add(item);
				result = true;
			}
			return result;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x000EBF28 File Offset: 0x000EA128
		private bool ChangeCategoryColor(CategoryType categoryType)
		{
			string name = categoryType.Name;
			int color = categoryType.Color;
			bool result = false;
			if (this.masterCategoryList.Contains(name) && this.masterCategoryList[name].Color != color)
			{
				this.masterCategoryList[name].Color = color;
				result = true;
			}
			return result;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x000EBF7C File Offset: 0x000EA17C
		private bool DeleteCategory(string categoryName)
		{
			bool result = false;
			if (this.masterCategoryList.Contains(categoryName))
			{
				this.masterCategoryList.Remove(categoryName);
				result = true;
			}
			return result;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000EBFA9 File Offset: 0x000EA1A9
		private bool IsValidCategoryColor(int color)
		{
			return -1 <= color && color <= 24;
		}

		// Token: 0x040027EF RID: 10223
		private const string GuidPattern = "^(\\{{0,1}([0-9A-F]){8}-([0-9A-F]){4}-([0-9A-F]){4}-([0-9A-F]){4}-([0-9A-F]){12}\\}{0,1})$";

		// Token: 0x040027F0 RID: 10224
		private static readonly Regex guidRegEx = new Regex("^(\\{{0,1}([0-9A-F]){8}-([0-9A-F]){4}-([0-9A-F]){4}-([0-9A-F]){4}-([0-9A-F]){12}\\}{0,1})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040027F1 RID: 10225
		private UpdateMasterCategoryListRequest request;

		// Token: 0x040027F2 RID: 10226
		private MasterCategoryList masterCategoryList;
	}
}

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000483 RID: 1155
	[OwaEventNamespace("Cat")]
	internal sealed class CategoryEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002C7F RID: 11391 RVA: 0x000F8DDC File Offset: 0x000F6FDC
		[OwaEvent("gtCM")]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("typ", typeof(StoreObjectType))]
		public void GetCategoryMenu()
		{
			this.ThrowIfWebPartsCannotActAsOwner();
			StoreObjectType storeObjectType = (StoreObjectType)base.GetParameter("typ");
			OwaStoreObjectId folderId = null;
			if (base.IsParameterSet("fId"))
			{
				folderId = (OwaStoreObjectId)base.GetParameter("fId");
			}
			StoreObjectType storeObjectType2 = storeObjectType;
			OutlookModule outlookModule;
			switch (storeObjectType2)
			{
			case StoreObjectType.ContactsFolder:
				break;
			case StoreObjectType.TasksFolder:
				goto IL_6C;
			default:
				switch (storeObjectType2)
				{
				case StoreObjectType.CalendarItem:
				case StoreObjectType.CalendarItemOccurrence:
					outlookModule = OutlookModule.Contacts;
					goto IL_76;
				case StoreObjectType.Contact:
				case StoreObjectType.DistributionList:
					break;
				case StoreObjectType.Task:
					goto IL_6C;
				default:
					outlookModule = OutlookModule.Mail;
					goto IL_76;
				}
				break;
			}
			outlookModule = OutlookModule.Contacts;
			goto IL_76;
			IL_6C:
			outlookModule = OutlookModule.Contacts;
			IL_76:
			CategoryContextMenu.Render(base.UserContext, this.Writer, outlookModule, folderId);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000F8E74 File Offset: 0x000F7074
		[OwaEventParameter("id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("ck", typeof(string), false, true)]
		[OwaEventParameter("typ", typeof(StoreObjectType), false, true)]
		[OwaEvent("mdfy")]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("catAdd", typeof(string), true, true)]
		[OwaEventParameter("catRem", typeof(string), true, true)]
		public void ModifyCategories()
		{
			this.ThrowIfWebPartsCannotActAsOwner();
			string[] addCategories = (string[])base.GetParameter("catAdd");
			string[] removeCategories = (string[])base.GetParameter("catRem");
			using (Item item = this.GetItem())
			{
				CategoryContextMenu.ModifyCategories(item, addCategories, removeCategories);
				MeetingMessage meetingMessage = item as MeetingMessage;
				if (meetingMessage != null)
				{
					CalendarItemBase calendarItemBase = MeetingUtilities.TryGetCorrelatedItem(meetingMessage);
					if (calendarItemBase != null)
					{
						CategoryContextMenu.ModifyCategories(calendarItemBase, addCategories, removeCategories);
						Utilities.SaveItem(calendarItemBase);
					}
				}
				Utilities.SaveItem(item, true, SaveMode.FailOnAnyConflict);
				item.Load();
				this.Writer.Write("var sCats = \"");
				StringBuilder stringBuilder = new StringBuilder();
				StringWriter stringWriter = new StringWriter(stringBuilder);
				CategorySwatch.RenderCategories(base.OwaContext, stringWriter, item);
				stringWriter.Close();
				Utilities.JavascriptEncode(stringBuilder.ToString(), this.Writer);
				this.Writer.Write("\";");
				this.Writer.Write("a_rgCats = ");
				CategorySwatch.RenderCategoriesJavascriptArray(this.SanitizingWriter, item);
				this.Writer.Write(";");
				this.Writer.Write("a_sId = \"");
				Utilities.JavascriptEncode(Utilities.GetIdAsString(item), this.Writer);
				this.Writer.Write("\";");
				this.Writer.Write("a_sCK = \"");
				Utilities.JavascriptEncode(item.Id.ChangeKeyAsBase64String(), this.Writer);
				this.Writer.Write("\";");
			}
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000F9000 File Offset: 0x000F7200
		[OwaEventParameter("ck", typeof(string))]
		[OwaEvent("clr")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		public void ClearCategories()
		{
			base.ThrowIfCannotActAsOwner();
			using (Item item = this.GetItem())
			{
				item.OpenAsReadWrite();
				CategoryContextMenu.ClearCategories(item);
				item.Save(SaveMode.ResolveConflicts);
				item.Load();
				this.Writer.Write("a_sCK = \"");
				Utilities.JavascriptEncode(item.Id.ChangeKeyAsBase64String(), this.Writer);
				this.Writer.Write("\";");
			}
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000F9088 File Offset: 0x000F7288
		[OwaEvent("MngCtgs")]
		public void GetManageCategoriesDialog()
		{
			base.ThrowIfCannotActAsOwner();
			ManageCategoriesDialog manageCategoriesDialog = new ManageCategoriesDialog(base.UserContext);
			manageCategoriesDialog.Render(this.Writer);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000F90B4 File Offset: 0x000F72B4
		[OwaEventParameter("clr", typeof(int))]
		[OwaEventParameter("nm", typeof(string))]
		[OwaEvent("CrtCtg")]
		public void CreateCategory()
		{
			base.ThrowIfCannotActAsOwner();
			string text = (string)base.GetParameter("nm");
			if (text.Length > 255)
			{
				throw new OwaInvalidRequestException("Category name cannot be longer than 255 characters.");
			}
			int color = (int)base.GetParameter("clr");
			if (!CategorySwatch.IsValidCategoryColor(color))
			{
				throw new OwaInvalidRequestException("Category color must be in the range [-1, 24].");
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				RenderingUtilities.RenderError(base.UserContext, this.Writer, 1243373352);
				return;
			}
			if (text.Contains(",") || text.Contains(";") || text.Contains("؛") || text.Contains("﹔") || text.Contains("；"))
			{
				RenderingUtilities.RenderError(base.UserContext, this.Writer, 1243373352);
				return;
			}
			if (CategoryEventHandler.guidRegEx.IsMatch(text))
			{
				RenderingUtilities.RenderError(base.UserContext, this.Writer, 1243373352);
				return;
			}
			MasterCategoryList masterCategoryList = base.UserContext.GetMasterCategoryList(true);
			if (masterCategoryList.Contains(text))
			{
				RenderingUtilities.RenderError(base.UserContext, this.Writer, -210070156);
				return;
			}
			Category category = Category.Create(text, color, false);
			masterCategoryList.Add(category);
			ManageCategoriesDialog.RenderCategory(this.Writer, category);
			masterCategoryList.Save();
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000F9208 File Offset: 0x000F7408
		[OwaEventParameter("nm", typeof(string))]
		[OwaEventParameter("clr", typeof(int))]
		[OwaEvent("ChgCtg")]
		public void ChangeCategoryColor()
		{
			base.ThrowIfCannotActAsOwner();
			string text = (string)base.GetParameter("nm");
			if (string.IsNullOrEmpty(text))
			{
				throw new OwaInvalidRequestException("Category name cannot be null or empty.");
			}
			int color = (int)base.GetParameter("clr");
			if (!CategorySwatch.IsValidCategoryColor(color))
			{
				throw new OwaInvalidRequestException("Category color must be in the range [-1, 24].");
			}
			MasterCategoryList masterCategoryList = base.UserContext.GetMasterCategoryList(true);
			if (!masterCategoryList.Contains(text))
			{
				throw new OwaInvalidRequestException("Category does not exist");
			}
			masterCategoryList[text].Color = color;
			masterCategoryList.Save();
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000F9298 File Offset: 0x000F7498
		[OwaEvent("DltCtg")]
		[OwaEventParameter("nm", typeof(string))]
		public void DeleteCategory()
		{
			base.ThrowIfCannotActAsOwner();
			string categoryName = (string)base.GetParameter("nm");
			MasterCategoryList masterCategoryList = base.UserContext.GetMasterCategoryList(true);
			if (!masterCategoryList.Contains(categoryName))
			{
				return;
			}
			masterCategoryList.Remove(categoryName);
			masterCategoryList.Save();
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000F92E4 File Offset: 0x000F74E4
		private Item GetItem()
		{
			OwaStoreObjectId owaStoreObjectId = base.GetParameter("id") as OwaStoreObjectId;
			string text = base.GetParameter("ck") as string;
			Item result;
			if (owaStoreObjectId == null)
			{
				if (!base.IsParameterSet("typ"))
				{
					throw new OwaInvalidRequestException();
				}
				StoreObjectType itemType = (StoreObjectType)base.GetParameter("typ");
				result = Utilities.CreateImplicitDraftItem(itemType, base.GetParameter("fId") as OwaStoreObjectId);
			}
			else
			{
				if (text == null)
				{
					throw new OwaInvalidRequestException();
				}
				result = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, text, new PropertyDefinition[0]);
			}
			return result;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000F9373 File Offset: 0x000F7573
		private void ThrowIfWebPartsCannotActAsOwner()
		{
			if (base.UserContext.IsWebPartRequest && !base.UserContext.CanActAsOwner)
			{
				throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(1622692336), true);
			}
		}

		// Token: 0x04001D6F RID: 7535
		public const string EventNamespace = "Cat";

		// Token: 0x04001D70 RID: 7536
		public const string MethodGetCategoryMenu = "gtCM";

		// Token: 0x04001D71 RID: 7537
		public const string MethodClearCategories = "clr";

		// Token: 0x04001D72 RID: 7538
		public const string MethodModifyCategories = "mdfy";

		// Token: 0x04001D73 RID: 7539
		public const string MethodGetManageCategoriesDialog = "MngCtgs";

		// Token: 0x04001D74 RID: 7540
		public const string MethodCreateCategory = "CrtCtg";

		// Token: 0x04001D75 RID: 7541
		public const string MethodChangeCategoryColor = "ChgCtg";

		// Token: 0x04001D76 RID: 7542
		public const string MethodDeleteCategory = "DltCtg";

		// Token: 0x04001D77 RID: 7543
		public const string CategoryName = "nm";

		// Token: 0x04001D78 RID: 7544
		public const string CategoryColor = "clr";

		// Token: 0x04001D79 RID: 7545
		public const string Id = "id";

		// Token: 0x04001D7A RID: 7546
		public const string ChangeKey = "ck";

		// Token: 0x04001D7B RID: 7547
		public const string FolderId = "fId";

		// Token: 0x04001D7C RID: 7548
		public const string ItemType = "typ";

		// Token: 0x04001D7D RID: 7549
		public const string AddCategories = "catAdd";

		// Token: 0x04001D7E RID: 7550
		public const string RemoveCategories = "catRem";

		// Token: 0x04001D7F RID: 7551
		private const string HexDigit = "[0123456789ABCDEF]";

		// Token: 0x04001D80 RID: 7552
		private const string GuidExpression = "^\\{[0123456789ABCDEF]{8}-[0123456789ABCDEF]{4}-[0123456789ABCDEF]{4}-[0123456789ABCDEF]{4}-[0123456789ABCDEF]{12}\\}$";

		// Token: 0x04001D81 RID: 7553
		private static readonly Regex guidRegEx = new Regex("^\\{[0123456789ABCDEF]{8}-[0123456789ABCDEF]{4}-[0123456789ABCDEF]{4}-[0123456789ABCDEF]{4}-[0123456789ABCDEF]{12}\\}$", RegexOptions.Compiled);
	}
}

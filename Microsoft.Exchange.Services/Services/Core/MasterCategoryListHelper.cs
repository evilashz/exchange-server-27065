using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020005E9 RID: 1513
	internal static class MasterCategoryListHelper
	{
		// Token: 0x06002DB1 RID: 11697 RVA: 0x000B2514 File Offset: 0x000B0714
		internal static MasterCategoryListType GetMasterCategoryListType(MailboxSession mailboxSession, CultureInfo culture)
		{
			MasterCategoryListType masterCategoryListType = new MasterCategoryListType();
			Category[] defaultCategories = MasterCategoryListHelper.CreateDefaultCategoriesList(culture);
			masterCategoryListType.DefaultList = MasterCategoryListHelper.CreateDefaultList(defaultCategories);
			masterCategoryListType.MasterList = MasterCategoryListHelper.InternalGetMasterList(mailboxSession, defaultCategories);
			return masterCategoryListType;
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000B2548 File Offset: 0x000B0748
		internal static MasterCategoryList GetMasterCategoryList(MailboxSession mailboxSession, CultureInfo culture = null)
		{
			MasterCategoryList masterCategoryList = MasterCategoryListHelper.InternalGetMasterCategoryList(mailboxSession);
			if (masterCategoryList.Count == 0)
			{
				MasterCategoryListHelper.AddDefaultCategoriesToMasterCategoryList(masterCategoryList, culture);
				masterCategoryList.Save();
			}
			return masterCategoryList;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000B2574 File Offset: 0x000B0774
		internal static CategoryType[] GetMasterList(MasterCategoryList mcl)
		{
			if (mcl == null)
			{
				throw new ArgumentNullException("mcl");
			}
			List<CategoryType> list = new List<CategoryType>();
			foreach (Category category in mcl)
			{
				list.Add(new CategoryType(category.Name, category.Color));
			}
			return list.ToArray();
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B25E8 File Offset: 0x000B07E8
		internal static CategoryType[] GetMasterList(MasterCategoryListType mcl)
		{
			if (mcl == null)
			{
				throw new ArgumentNullException("mcl");
			}
			List<CategoryType> list = new List<CategoryType>();
			if (mcl.MasterList.Length == 0)
			{
				foreach (CategoryType item in mcl.DefaultList)
				{
					list.Add(item);
				}
			}
			else
			{
				foreach (CategoryType item2 in mcl.MasterList)
				{
					list.Add(item2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000B2668 File Offset: 0x000B0868
		private static CategoryType[] InternalGetMasterList(MailboxSession mailboxSession, Category[] defaultCategories)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			MasterCategoryList masterCategoryList = MasterCategoryListHelper.InternalGetMasterCategoryList(mailboxSession);
			if (masterCategoryList.Count == 0)
			{
				MasterCategoryListHelper.AddDefaultCategoriesToMasterCategoryList(masterCategoryList, defaultCategories);
				masterCategoryList.Save();
			}
			return MasterCategoryListHelper.GetMasterList(masterCategoryList);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000B26A8 File Offset: 0x000B08A8
		private static MasterCategoryList InternalGetMasterCategoryList(MailboxSession mailboxSession)
		{
			MasterCategoryList result = null;
			bool flag = false;
			bool flag2 = false;
			try
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug(0L, "MasterCategoryListHelper::GetMasterCategoryList - force reload of the master category list.");
				result = mailboxSession.GetMasterCategoryList(true);
			}
			catch (CorruptDataException ex)
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceError<string, string>(0L, "MasterCategoryListHelper.GetMasterCategoryList: Corrupt MCL. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
				flag2 = true;
			}
			catch (ObjectExistedException ex2)
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceError<string, string>(0L, "MasterCategoryListHelper.GetMasterCategoryList: ObjectExistedException. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
				flag = true;
			}
			if (flag || flag2)
			{
				result = MasterCategoryListHelper.RefetchMcl(flag2, mailboxSession);
			}
			return result;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000B2748 File Offset: 0x000B0948
		private static MasterCategoryList RefetchMcl(bool deleteBeforeRefetch, MailboxSession mailboxSession)
		{
			if (deleteBeforeRefetch)
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug(0L, "MasterCategoryListHelper::RefetchMcl - delete master category list before reload of the master category list.");
				mailboxSession.DeleteMasterCategoryList();
			}
			ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug(0L, "MasterCategoryListHelper::RefetchMcl - force reload of the master category list.");
			return mailboxSession.GetMasterCategoryList(true);
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000B277C File Offset: 0x000B097C
		private static void AddDefaultCategoriesToMasterCategoryList(MasterCategoryList masterCategoryList, CultureInfo culture)
		{
			Category[] defaultCategories = MasterCategoryListHelper.CreateDefaultCategoriesList(culture);
			MasterCategoryListHelper.AddDefaultCategoriesToMasterCategoryList(masterCategoryList, defaultCategories);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B2798 File Offset: 0x000B0998
		private static void AddDefaultCategoriesToMasterCategoryList(MasterCategoryList masterCategoryList, Category[] defaultCategories)
		{
			int num = defaultCategories.Length;
			for (int i = 0; i < num; i++)
			{
				masterCategoryList.Add(defaultCategories[i]);
			}
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000B27C0 File Offset: 0x000B09C0
		private static Category[] CreateDefaultCategoriesList(CultureInfo culture)
		{
			return new Category[]
			{
				Category.Create(ForwardReplyUtilities.ClientsResourceManager.GetString("Red", culture), 0, true),
				Category.Create(ForwardReplyUtilities.ClientsResourceManager.GetString("Orange", culture), 1, true),
				Category.Create(ForwardReplyUtilities.ClientsResourceManager.GetString("Yellow", culture), 3, true),
				Category.Create(ForwardReplyUtilities.ClientsResourceManager.GetString("Green", culture), 4, true),
				Category.Create(ForwardReplyUtilities.ClientsResourceManager.GetString("Blue", culture), 7, true),
				Category.Create(ForwardReplyUtilities.ClientsResourceManager.GetString("Purple", culture), 8, true)
			};
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000B2874 File Offset: 0x000B0A74
		private static CategoryType[] CreateDefaultList(Category[] defaultCategories)
		{
			int num = defaultCategories.Length;
			CategoryType[] array = new CategoryType[num];
			for (int i = 0; i < num; i++)
			{
				Category category = defaultCategories[i];
				array[i] = new CategoryType(category.Name, category.Color);
			}
			return array;
		}

		// Token: 0x04001B4E RID: 6990
		private const string RedCategory = "Red";

		// Token: 0x04001B4F RID: 6991
		private const string OrangeCategory = "Orange";

		// Token: 0x04001B50 RID: 6992
		private const string YellowCategory = "Yellow";

		// Token: 0x04001B51 RID: 6993
		private const string GreenCategory = "Green";

		// Token: 0x04001B52 RID: 6994
		private const string BlueCategory = "Blue";

		// Token: 0x04001B53 RID: 6995
		private const string PurpleCategory = "Purple";

		// Token: 0x020005EA RID: 1514
		private enum CategoryColors
		{
			// Token: 0x04001B55 RID: 6997
			Red,
			// Token: 0x04001B56 RID: 6998
			Orange,
			// Token: 0x04001B57 RID: 6999
			Yellow = 3,
			// Token: 0x04001B58 RID: 7000
			Green,
			// Token: 0x04001B59 RID: 7001
			Blue = 7,
			// Token: 0x04001B5A RID: 7002
			Purple
		}
	}
}

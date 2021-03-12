using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000243 RID: 579
	internal class SearchFilterGenerator
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x0007C588 File Offset: 0x0007A788
		public QueryFilter Execute(string searchString, bool contentIndexingEnabled, Dictionary<string, bool> folderClass, SearchScope searchScope, CultureInfo cultureInfo)
		{
			if (searchString.Trim().Length == 0)
			{
				return null;
			}
			this.queryFilter = AqsParser.ParseAndBuildQuery(searchString, AqsParser.ParseOption.SuppressError | (contentIndexingEnabled ? AqsParser.ParseOption.None : AqsParser.ParseOption.ContentIndexingDisabled), cultureInfo, RescopedAll.Default, null, null);
			if (this.queryFilter == null)
			{
				return null;
			}
			this.AddItemTypeFilter(searchScope, folderClass);
			return this.queryFilter;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0007C5DC File Offset: 0x0007A7DC
		private void AddItemTypeFilter(SearchScope searchScope, Dictionary<string, bool> folderClassDict)
		{
			if (searchScope == SearchScope.SelectedFolder)
			{
				return;
			}
			List<string> list = new List<string>();
			foreach (string text in folderClassDict.Keys)
			{
				if (ObjectClass.IsContactsFolder(text))
				{
					foreach (string item in SearchFilterGenerator.contactModuleIncludeList)
					{
						list.Add(item);
					}
				}
				else if (ObjectClass.IsTaskRequest(text))
				{
					foreach (string item2 in SearchFilterGenerator.taskModuleIncludeList)
					{
						list.Add(item2);
					}
				}
				else if (ObjectClass.IsCalendarFolder(text))
				{
					foreach (string item3 in SearchFilterGenerator.calendarModuleIncludeList)
					{
						list.Add(item3);
					}
				}
				else
				{
					if (ObjectClass.IsMessageFolder(text))
					{
						return;
					}
					if (ObjectClass.IsNotesFolder(text))
					{
						foreach (string item4 in SearchFilterGenerator.noteModuleIncludeList)
						{
							list.Add(item4);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				TextFilter[] array5 = new TextFilter[list.Count];
				for (int m = 0; m < list.Count; m++)
				{
					array5[m] = new TextFilter(StoreObjectSchema.ItemClass, list[m], MatchOptions.PrefixOnWords, MatchFlags.IgnoreCase);
				}
				if (array5.Length == 1)
				{
					QueryFilter[] filters = new QueryFilter[]
					{
						this.queryFilter,
						array5[0]
					};
					this.queryFilter = new AndFilter(filters);
					return;
				}
				this.queryFilter = new AndFilter(new QueryFilter[]
				{
					this.queryFilter,
					new OrFilter(array5)
				});
			}
		}

		// Token: 0x04000C92 RID: 3218
		private static string[] contactModuleIncludeList = new string[]
		{
			"IPM.Contact"
		};

		// Token: 0x04000C93 RID: 3219
		private static string[] taskModuleIncludeList = new string[]
		{
			"IPM.Task"
		};

		// Token: 0x04000C94 RID: 3220
		private static string[] calendarModuleIncludeList = new string[]
		{
			"IPM.Appointment"
		};

		// Token: 0x04000C95 RID: 3221
		private static string[] noteModuleIncludeList = new string[]
		{
			"IPM.StickyNote"
		};

		// Token: 0x04000C96 RID: 3222
		private QueryFilter queryFilter;
	}
}

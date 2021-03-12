using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200023F RID: 575
	internal class SearchFilterGenerator : IFilterGenerator
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x000777DE File Offset: 0x000759DE
		private SearchFilterGenerator(QueryFilter advancedQueryFilter, CultureInfo userCultureInfo, IPolicyTagProvider policyTagProvider)
		{
			this.advancedQueryFilter = advancedQueryFilter;
			this.userCultureInfo = userCultureInfo;
			this.policyTagProvider = policyTagProvider;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00077818 File Offset: 0x00075A18
		public static AqsParser.ParseOption GetAqsParseOption(Folder folder, bool isContentIndexingEnabled)
		{
			AqsParser.ParseOption parseOption = AqsParser.ParseOption.SuppressError;
			if (!isContentIndexingEnabled)
			{
				parseOption |= AqsParser.ParseOption.ContentIndexingDisabled;
			}
			bool flag = Array.Exists<string>(SearchFilterGenerator.prefixAllowedFolderList, (string item) => string.Equals(item, folder.ClassName, StringComparison.OrdinalIgnoreCase));
			flag |= (folder is SearchFolder || folder.Session is PublicFolderSession);
			if (Globals.DisablePrefixSearch && !flag)
			{
				parseOption |= AqsParser.ParseOption.DisablePrefixMatch;
			}
			return parseOption;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0007788C File Offset: 0x00075A8C
		public static QueryFilter Execute(string searchString, bool isContentIndexingEnabled, CultureInfo userCultureInfo, IPolicyTagProvider policyTagProvider, Folder folder, SearchScope searchScope, QueryFilter advancedQueryFilter)
		{
			SearchFilterGenerator searchFilterGenerator = new SearchFilterGenerator(advancedQueryFilter, userCultureInfo, policyTagProvider);
			return searchFilterGenerator.Execute(searchString, isContentIndexingEnabled, folder, searchScope);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000778B0 File Offset: 0x00075AB0
		public QueryFilter Execute(string searchString, bool isContentIndexingEnabled, Folder folder, SearchScope searchScope)
		{
			this.searchScope = searchScope;
			this.folderClass = folder.ClassName;
			if (searchString != null)
			{
				this.queryFilter = AqsParser.ParseAndBuildQuery(searchString, SearchFilterGenerator.GetAqsParseOption(folder, isContentIndexingEnabled), this.userCultureInfo, RescopedAll.Default, null, this.policyTagProvider);
			}
			if (this.advancedQueryFilter != null)
			{
				if (this.queryFilter == null)
				{
					this.queryFilter = this.advancedQueryFilter;
				}
				else
				{
					this.queryFilter = new AndFilter(new QueryFilter[]
					{
						this.queryFilter,
						this.advancedQueryFilter
					});
				}
			}
			if (this.queryFilter == null)
			{
				return null;
			}
			this.AddItemTypeFilter();
			return this.queryFilter;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00077950 File Offset: 0x00075B50
		private void AddItemTypeFilter()
		{
			if (this.searchScope == SearchScope.SelectedFolder)
			{
				return;
			}
			string[] array = null;
			if (ObjectClass.IsContactsFolder(this.folderClass))
			{
				array = SearchFilterGenerator.contactModuleIncludeList;
			}
			else if (ObjectClass.IsTaskFolder(this.folderClass))
			{
				array = SearchFilterGenerator.taskModuleIncludeList;
			}
			if (array != null)
			{
				QueryFilter[] array2 = new QueryFilter[array.Length * 2];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i * 2] = new TextFilter(StoreObjectSchema.ItemClass, array[i], MatchOptions.FullString, MatchFlags.IgnoreCase);
					array2[i * 2 + 1] = new TextFilter(StoreObjectSchema.ItemClass, array[i] + ".", MatchOptions.Prefix, MatchFlags.IgnoreCase);
				}
				this.queryFilter = new AndFilter(new QueryFilter[]
				{
					this.queryFilter,
					new OrFilter(array2)
				});
			}
		}

		// Token: 0x04000D44 RID: 3396
		private static string[] contactModuleIncludeList = new string[]
		{
			"IPM.Contact",
			"IPM.DistList"
		};

		// Token: 0x04000D45 RID: 3397
		private static string[] taskModuleIncludeList = new string[]
		{
			"IPM.Task",
			"IPM.TaskRequest"
		};

		// Token: 0x04000D46 RID: 3398
		private static string[] prefixAllowedFolderList = new string[]
		{
			"IPF.Contact"
		};

		// Token: 0x04000D47 RID: 3399
		private SearchScope searchScope;

		// Token: 0x04000D48 RID: 3400
		private string folderClass;

		// Token: 0x04000D49 RID: 3401
		private QueryFilter advancedQueryFilter;

		// Token: 0x04000D4A RID: 3402
		private CultureInfo userCultureInfo;

		// Token: 0x04000D4B RID: 3403
		private QueryFilter queryFilter;

		// Token: 0x04000D4C RID: 3404
		private IPolicyTagProvider policyTagProvider;
	}
}

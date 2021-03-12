using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000222 RID: 546
	internal static class ExtensionHelper
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00042DAD File Offset: 0x00040FAD
		public static TSource AggregateOfDefault<TSource>(this IEnumerable<TSource> sources, Func<TSource, TSource, TSource> func)
		{
			return sources.DefaultIfEmpty<TSource>().Aggregate(func);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00042DBC File Offset: 0x00040FBC
		public static void ForEach<TSource>(this IEnumerable<TSource> sources, Action<TSource> func)
		{
			if (sources != null)
			{
				foreach (TSource obj in sources)
				{
					func(obj);
				}
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00042E08 File Offset: 0x00041008
		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00042E10 File Offset: 0x00041010
		public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> sources)
		{
			return sources == null || sources.Count<TSource>() == 0;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00042E20 File Offset: 0x00041020
		public static string ValueOrDefault(this string str, string defVal)
		{
			if (!str.IsNullOrEmpty())
			{
				return str;
			}
			return defVal;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00042E2D File Offset: 0x0004102D
		public static string DomainUserName(this ADObjectId adId)
		{
			return adId.DomainId.Name + "\\" + adId.Name;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00042E4A File Offset: 0x0004104A
		public static string ToLabelTag(this Globals.LogFields logField)
		{
			return "{Label" + logField.ToString() + "}";
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00042E66 File Offset: 0x00041066
		public static string ToValueTag(this Globals.LogFields logField)
		{
			return "{" + logField.ToString() + "}";
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00042E84 File Offset: 0x00041084
		public static StoreId GetSubFolderIdByName(this Folder parentFolder, string folderName)
		{
			StoreId result = null;
			using (QueryResult queryResult = parentFolder.FolderQuery(FolderQueryFlags.None, new TextFilter(FolderSchema.DisplayName, folderName, MatchOptions.ExactPhrase, MatchFlags.IgnoreCase), null, new PropertyDefinition[]
			{
				FolderSchema.Id,
				FolderSchema.DisplayName
			}))
			{
				foreach (Pair<StoreId, string> pair in queryResult.Enumerator<StoreId, string>())
				{
					if (pair.First != null && folderName.Equals(pair.Second, StringComparison.OrdinalIgnoreCase))
					{
						result = pair.First;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00042F44 File Offset: 0x00041144
		public static List<Pair<StoreId, string>> GetSubFoldersWithIdAndName(this Folder parentFolder)
		{
			List<Pair<StoreId, string>> result;
			using (QueryResult queryResult = parentFolder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
			{
				FolderSchema.Id,
				FolderSchema.DisplayName
			}))
			{
				result = (from x in queryResult.Enumerator<StoreId, string>()
				where x != null
				select x).ToList<Pair<StoreId, string>>();
			}
			return result;
		}
	}
}

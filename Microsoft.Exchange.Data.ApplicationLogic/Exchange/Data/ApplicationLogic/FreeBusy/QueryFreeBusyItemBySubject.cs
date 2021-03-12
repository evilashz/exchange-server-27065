using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.FreeBusy
{
	// Token: 0x02000137 RID: 311
	internal static class QueryFreeBusyItemBySubject
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00034A60 File Offset: 0x00032C60
		public static object[][] Query(Folder folder, string subject)
		{
			QueryFreeBusyItemBySubject.Tracer.TraceDebug<string, string>(0L, "Searching for item with subject '{0}' in folder '{1}'", subject, folder.DisplayName);
			QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, subject);
			List<object[]> list = new List<object[]>(1);
			using (QueryResult queryResult = QueryFreeBusyItemBySubject.CreateQueryResult(folder))
			{
				DateTime minValue = DateTime.MinValue;
				queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
				object[][] rows = queryResult.GetRows(10);
				if (rows != null)
				{
					foreach (object[] array2 in rows)
					{
						if (!(array2[0] is VersionedId))
						{
							QueryFreeBusyItemBySubject.Tracer.TraceDebug(0L, "Query returned row without id.");
						}
						else
						{
							string y = array2[2] as string;
							if (string.IsNullOrEmpty(subject))
							{
								QueryFreeBusyItemBySubject.Tracer.TraceDebug(0L, "Query returned row without subject.");
							}
							else
							{
								if (!StringComparer.InvariantCultureIgnoreCase.Equals(subject, y))
								{
									break;
								}
								list.Add(array2);
							}
						}
					}
				}
			}
			QueryFreeBusyItemBySubject.Tracer.TraceDebug<int>(0L, "Search resulted in {0} items.", list.Count);
			return list.ToArray();
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00034B70 File Offset: 0x00032D70
		private static QueryResult CreateQueryResult(Folder folder)
		{
			return folder.ItemQuery(ItemQueryType.None, null, QueryFreeBusyItemBySubject.sortBy, QueryFreeBusyItemBySubject.properties);
		}

		// Token: 0x040006BC RID: 1724
		internal const int IdIndex = 0;

		// Token: 0x040006BD RID: 1725
		internal const int LastModifiedTimeIndex = 1;

		// Token: 0x040006BE RID: 1726
		internal const int SubjectIndex = 2;

		// Token: 0x040006BF RID: 1727
		private const int RowBatch = 10;

		// Token: 0x040006C0 RID: 1728
		private static readonly PropertyDefinition[] properties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.Subject
		};

		// Token: 0x040006C1 RID: 1729
		private static readonly SortBy[] sortBy = new SortBy[]
		{
			new SortBy(ItemSchema.Subject, SortOrder.Ascending)
		};

		// Token: 0x040006C2 RID: 1730
		private static readonly Trace Tracer = ExTraceGlobals.FreeBusyTracer;
	}
}

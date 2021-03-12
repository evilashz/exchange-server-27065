using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000014 RID: 20
	internal class DomainTable
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00004170 File Offset: 0x00002370
		internal void Add(string internalDomain, string externalDomain, MultiValuedProperty<string> exceptionList, bool checkSubDomains)
		{
			DomainTable.DomainMapping item = default(DomainTable.DomainMapping);
			item.Domain = new string[2];
			item.Domain[0] = DomainTable.ReverseString(internalDomain);
			item.Domain[1] = DomainTable.ReverseString(externalDomain);
			item.RemapSubdomain = checkSubDomains;
			if (exceptionList != null)
			{
				item.ExceptionList = CollectionsUtil.CreateCaseInsensitiveHashtable();
				foreach (string text in exceptionList)
				{
					item.ExceptionList.Add(text, text);
				}
			}
			this.domainMapList.Add(item);
			int index = this.domainMapList.Count - 1;
			DomainTable.IndexEntry item2 = default(DomainTable.IndexEntry);
			item2.Index = index;
			item2.Value = item.Domain[0];
			this.internalIndex.Add(item2);
			item2 = default(DomainTable.IndexEntry);
			item2.Index = index;
			item2.Value = item.Domain[1];
			this.externalIndex.Add(item2);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004284 File Offset: 0x00002484
		internal void Sort()
		{
			this.internalIndex.Sort(DomainTable.comparer);
			this.externalIndex.Sort(DomainTable.comparer);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000042A8 File Offset: 0x000024A8
		internal string Remap(string domain, MapTable.MapEntryType direction)
		{
			MapTable.MapEntryType mapEntryType;
			List<DomainTable.IndexEntry> list;
			if (direction == MapTable.MapEntryType.Internal)
			{
				mapEntryType = MapTable.MapEntryType.External;
				list = this.internalIndex;
			}
			else
			{
				mapEntryType = MapTable.MapEntryType.Internal;
				list = this.externalIndex;
			}
			DomainTable.IndexEntry item = new DomainTable.IndexEntry
			{
				Value = DomainTable.ReverseString(domain)
			};
			int num = list.BinarySearch(item, DomainTable.comparer);
			int index;
			if (num >= 0)
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Found exact match in domain-table");
				index = list[num].Index;
				return DomainTable.ReverseString(this.domainMapList[index].Domain[(int)mapEntryType]);
			}
			if (direction == MapTable.MapEntryType.External)
			{
				return null;
			}
			num = ~num;
			if (num == 0)
			{
				return null;
			}
			num--;
			ExTraceGlobals.AddressRewritingTracer.TraceDebug<int>((long)this.GetHashCode(), "Exact match not found in domain-table, checking subdomain match (index of next lower item: {0}", num);
			DomainTable.IndexEntry indexEntry = list[num];
			index = indexEntry.Index;
			if (!this.domainMapList[index].RemapSubdomain)
			{
				return null;
			}
			if (!item.Value.StartsWith(indexEntry.Value, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			if (item.Value[indexEntry.Value.Length] != '.')
			{
				return null;
			}
			ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Subdomain match found");
			if (this.domainMapList[index].ExceptionList.Contains(domain))
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug<string>((long)this.GetHashCode(), "This domain is on the exception list for the entry: {0}", this.domainMapList[index].Domain[(int)direction]);
				return null;
			}
			return DomainTable.ReverseString(this.domainMapList[index].Domain[(int)mapEntryType]);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000443C File Offset: 0x0000263C
		private static string ReverseString(string input)
		{
			char[] array = input.ToCharArray();
			Array.Reverse(array);
			return new string(array);
		}

		// Token: 0x0400004F RID: 79
		private static IComparer<DomainTable.IndexEntry> comparer = new DomainTable.IndexEntryComparer();

		// Token: 0x04000050 RID: 80
		private List<DomainTable.DomainMapping> domainMapList = new List<DomainTable.DomainMapping>();

		// Token: 0x04000051 RID: 81
		private List<DomainTable.IndexEntry> internalIndex = new List<DomainTable.IndexEntry>();

		// Token: 0x04000052 RID: 82
		private List<DomainTable.IndexEntry> externalIndex = new List<DomainTable.IndexEntry>();

		// Token: 0x02000015 RID: 21
		public struct IndexEntry
		{
			// Token: 0x04000053 RID: 83
			internal int Index;

			// Token: 0x04000054 RID: 84
			internal string Value;
		}

		// Token: 0x02000016 RID: 22
		private struct DomainMapping
		{
			// Token: 0x04000055 RID: 85
			internal string[] Domain;

			// Token: 0x04000056 RID: 86
			internal bool RemapSubdomain;

			// Token: 0x04000057 RID: 87
			internal Hashtable ExceptionList;
		}

		// Token: 0x02000017 RID: 23
		public class IndexEntryComparer : IComparer<DomainTable.IndexEntry>
		{
			// Token: 0x0600005F RID: 95 RVA: 0x00004491 File Offset: 0x00002691
			public int Compare(DomainTable.IndexEntry x, DomainTable.IndexEntry y)
			{
				return string.Compare(x.Value, y.Value, StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol.EDiscovery
{
	// Token: 0x02000048 RID: 72
	internal class SearchResult : ResultBase
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000A600 File Offset: 0x00008800
		static SearchResult()
		{
			SearchResult.description.ComplianceStructureId = 99;
			SearchResult.description.RegisterIntegerPropertyGetterAndSetter(0, (SearchResult item) => item.PageSize, delegate(SearchResult item, int value)
			{
				item.PageSize = value;
			});
			SearchResult.description.RegisterLongPropertyGetterAndSetter(0, (SearchResult item) => item.TotalCount, delegate(SearchResult item, long value)
			{
				item.totalCount = value;
			});
			SearchResult.description.RegisterLongPropertyGetterAndSetter(1, (SearchResult item) => item.TotalSize, delegate(SearchResult item, long value)
			{
				item.totalSize = value;
			});
			SearchResult.description.RegisterComplexCollectionAccessor<SearchResult.TargetSearchResult>(0, (SearchResult item) => item.Results.Count, (SearchResult item, int index) => item.Results.ToList<SearchResult.TargetSearchResult>()[index], delegate(SearchResult item, SearchResult.TargetSearchResult value, int index)
			{
				item.Results.Add(value);
			}, SearchResult.TargetSearchResult.Description);
			SearchResult.description.RegisterComplexCollectionAccessor<FaultRecord>(1, (SearchResult item) => item.Faults.Count, (SearchResult item, int index) => item.Faults.ToList<FaultRecord>()[index], delegate(SearchResult item, FaultRecord value, int index)
			{
				item.Faults.TryAdd(value);
			}, FaultRecord.Description);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000A7C0 File Offset: 0x000089C0
		public static ComplianceSerializationDescription<SearchResult> Description
		{
			get
			{
				return SearchResult.description;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A7C7 File Offset: 0x000089C7
		public override int SerializationVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000A7CA File Offset: 0x000089CA
		public long TotalSize
		{
			get
			{
				return this.totalSize;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A7D2 File Offset: 0x000089D2
		public long TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000A7DA File Offset: 0x000089DA
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000A7E2 File Offset: 0x000089E2
		public int PageSize { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000A7EB File Offset: 0x000089EB
		public ConcurrentBag<SearchResult.TargetSearchResult> Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A7F3 File Offset: 0x000089F3
		public override byte[] GetSerializedResult()
		{
			return ComplianceSerializer.Serialize<SearchResult>(SearchResult.Description, this);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A800 File Offset: 0x00008A00
		public void UpdateTotalSize(long size)
		{
			long num;
			long value;
			do
			{
				num = this.totalSize;
				value = num + size;
			}
			while (num != Interlocked.CompareExchange(ref this.totalSize, value, num));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A828 File Offset: 0x00008A28
		public void UpdateTotalCount(long count)
		{
			long num;
			long value;
			do
			{
				num = this.totalCount;
				value = num + count;
			}
			while (num != Interlocked.CompareExchange(ref this.totalCount, value, num));
		}

		// Token: 0x04000138 RID: 312
		private static ComplianceSerializationDescription<SearchResult> description = new ComplianceSerializationDescription<SearchResult>();

		// Token: 0x04000139 RID: 313
		private ConcurrentBag<SearchResult.TargetSearchResult> results = new ConcurrentBag<SearchResult.TargetSearchResult>();

		// Token: 0x0400013A RID: 314
		private long totalCount;

		// Token: 0x0400013B RID: 315
		private long totalSize;

		// Token: 0x02000049 RID: 73
		public class TargetSearchResult
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x0000A898 File Offset: 0x00008A98
			static TargetSearchResult()
			{
				SearchResult.TargetSearchResult.description.ComplianceStructureId = 99;
				SearchResult.TargetSearchResult.description.RegisterLongPropertyGetterAndSetter(0, (SearchResult.TargetSearchResult item) => item.Count, delegate(SearchResult.TargetSearchResult item, long value)
				{
					item.Count = value;
				});
				SearchResult.TargetSearchResult.description.RegisterLongPropertyGetterAndSetter(1, (SearchResult.TargetSearchResult item) => item.Size, delegate(SearchResult.TargetSearchResult item, long value)
				{
					item.Size = value;
				});
				SearchResult.TargetSearchResult.description.RegisterComplexPropertyAsBlobGetterAndSetter<Target>(0, (SearchResult.TargetSearchResult item) => item.Target, delegate(SearchResult.TargetSearchResult item, Target value)
				{
					item.Target = value;
				}, Target.Description);
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A98F File Offset: 0x00008B8F
			public static ComplianceSerializationDescription<SearchResult.TargetSearchResult> Description
			{
				get
				{
					return SearchResult.TargetSearchResult.description;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A996 File Offset: 0x00008B96
			// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000A99E File Offset: 0x00008B9E
			public long Size { get; set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A9A7 File Offset: 0x00008BA7
			// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A9AF File Offset: 0x00008BAF
			public long Count { get; set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A9B8 File Offset: 0x00008BB8
			// (set) Token: 0x060001CC RID: 460 RVA: 0x0000A9C0 File Offset: 0x00008BC0
			public Target Target { get; set; }

			// Token: 0x060001CD RID: 461 RVA: 0x0000A9C9 File Offset: 0x00008BC9
			public override string ToString()
			{
				return string.Format("Binding: {0}, Item count: {1}, Total size: {2}", this.Target.Identifier, this.Count, this.Size);
			}

			// Token: 0x04000149 RID: 329
			private static ComplianceSerializationDescription<SearchResult.TargetSearchResult> description = new ComplianceSerializationDescription<SearchResult.TargetSearchResult>();
		}
	}
}

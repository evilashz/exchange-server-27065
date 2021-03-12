using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000024 RID: 36
	internal class WorkItemResultIndex<TWorkItemResult> where TWorkItemResult : WorkItemResult
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A68F File Offset: 0x0000888F
		internal static IIndexDescriptor<TWorkItemResult, int> WorkItemIdAndExecutionEndTime(int workItemId, DateTime minExecutionEndTime)
		{
			return new WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexDescriptorForWorkItemIdAndExecutionEndTime<TWorkItemResult>(workItemId, minExecutionEndTime);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A698 File Offset: 0x00008898
		internal static IIndexDescriptor<TWorkItemResult, string> ResultNameAndExecutionEndTime(string resultName, DateTime minExecutionEndTime)
		{
			return new WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexDescriptorForResultNameAndExecutionEndTime<TWorkItemResult>(resultName, minExecutionEndTime);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A6A1 File Offset: 0x000088A1
		internal static IIndexDescriptor<TWorkItemResult, string> AllResultsAndExecutionEndTime(DateTime minExecutionEndTime)
		{
			return new WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexDescriptorForALLResultsAndExecutionEndTime<TWorkItemResult>(null, minExecutionEndTime);
		}

		// Token: 0x02000025 RID: 37
		internal abstract class WorkItemResultIndexBase<TResult, TKey> : IIndexDescriptor<TResult, TKey>, IIndexDescriptor where TResult : WorkItemResult
		{
			// Token: 0x060002C6 RID: 710 RVA: 0x0000A6B2 File Offset: 0x000088B2
			protected WorkItemResultIndexBase(TKey key, DateTime minExecutionEndTime)
			{
				this.key = key;
				this.MinExecutionEndTime = minExecutionEndTime;
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A6C8 File Offset: 0x000088C8
			// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000A6D0 File Offset: 0x000088D0
			public DateTime MinExecutionEndTime { get; set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000A6D9 File Offset: 0x000088D9
			public TKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x060002CA RID: 714
			public abstract IEnumerable<TKey> GetKeyValues(TResult item);

			// Token: 0x060002CB RID: 715
			public abstract IDataAccessQuery<TResult> ApplyIndexRestriction(IDataAccessQuery<TResult> query);

			// Token: 0x040000F5 RID: 245
			private TKey key;
		}

		// Token: 0x02000026 RID: 38
		private class WorkItemResultIndexDescriptorForWorkItemIdAndExecutionEndTime<TResult> : WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexBase<TResult, int> where TResult : WorkItemResult
		{
			// Token: 0x060002CC RID: 716 RVA: 0x0000A6E1 File Offset: 0x000088E1
			internal WorkItemResultIndexDescriptorForWorkItemIdAndExecutionEndTime(int key, DateTime minExecutionEndTime) : base(key, minExecutionEndTime)
			{
			}

			// Token: 0x060002CD RID: 717 RVA: 0x0000A7D0 File Offset: 0x000089D0
			public override IEnumerable<int> GetKeyValues(TResult item)
			{
				yield return item.WorkItemId;
				yield break;
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000A828 File Offset: 0x00008A28
			public override IDataAccessQuery<TResult> ApplyIndexRestriction(IDataAccessQuery<TResult> query)
			{
				IEnumerable<TResult> query2 = from r in query
				where r.WorkItemId == base.Key && r.ExecutionEndTime > base.MinExecutionEndTime
				select r;
				return query.AsDataAccessQuery<TResult>(query2);
			}
		}

		// Token: 0x02000027 RID: 39
		private class WorkItemResultIndexDescriptorForResultNameAndExecutionEndTime<TResult> : WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexBase<TResult, string> where TResult : WorkItemResult
		{
			// Token: 0x060002D0 RID: 720 RVA: 0x0000A84F File Offset: 0x00008A4F
			internal WorkItemResultIndexDescriptorForResultNameAndExecutionEndTime(string key, DateTime minExecutionEndTime) : base(key, minExecutionEndTime)
			{
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000A98C File Offset: 0x00008B8C
			public override IEnumerable<string> GetKeyValues(TResult item)
			{
				string name = item.ResultName;
				while (!string.IsNullOrEmpty(name))
				{
					yield return name;
					int slashIndex = name.LastIndexOf('/');
					if (slashIndex <= 0)
					{
						break;
					}
					name = name.Substring(0, slashIndex);
				}
				yield break;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0000AA20 File Offset: 0x00008C20
			public override IDataAccessQuery<TResult> ApplyIndexRestriction(IDataAccessQuery<TResult> query)
			{
				IEnumerable<TResult> enumerable = from r in query
				select r;
				if (IndexCapabilities.SupportsCaseInsensitiveStringComparison)
				{
					enumerable = from r in enumerable
					where r.ResultName.StartsWith(base.Key, StringComparison.OrdinalIgnoreCase) && r.ExecutionEndTime > base.MinExecutionEndTime
					select r;
				}
				else
				{
					enumerable = from r in enumerable
					where r.ResultName.StartsWith(base.Key) && r.ExecutionEndTime > base.MinExecutionEndTime
					select r;
				}
				return query.AsDataAccessQuery<TResult>(enumerable);
			}
		}

		// Token: 0x02000028 RID: 40
		private class WorkItemResultIndexDescriptorForALLResultsAndExecutionEndTime<TResult> : WorkItemResultIndex<TWorkItemResult>.WorkItemResultIndexBase<TResult, string> where TResult : WorkItemResult
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x0000AA95 File Offset: 0x00008C95
			internal WorkItemResultIndexDescriptorForALLResultsAndExecutionEndTime(string key, DateTime minExecutionEndTime) : base(key, minExecutionEndTime)
			{
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000AA9F File Offset: 0x00008C9F
			public override IEnumerable<string> GetKeyValues(TResult item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000AAC0 File Offset: 0x00008CC0
			public override IDataAccessQuery<TResult> ApplyIndexRestriction(IDataAccessQuery<TResult> query)
			{
				IEnumerable<TResult> query2 = from r in query
				where r.ExecutionEndTime > base.MinExecutionEndTime
				select r;
				return query.AsDataAccessQuery<TResult>(query2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000FB RID: 251
	internal abstract class MigrationHierarchyProcessorBase<TParent, TChild, TChildId, TResponse> : MigrationProcessorBase<TParent, TResponse> where TResponse : MigrationProcessorResponse
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x00036AB4 File Offset: 0x00034CB4
		protected MigrationHierarchyProcessorBase(TParent migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D4F RID: 3407
		protected abstract MigrationProcessorResponse DefaultCorruptedChildResponse { get; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D50 RID: 3408
		protected abstract Func<int?, IEnumerable<TChildId>>[] ProcessableChildObjectQueries { get; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D51 RID: 3409
		protected abstract int? MaxChildObjectsToProcessCount { get; }

		// Token: 0x06000D52 RID: 3410 RVA: 0x00036AC0 File Offset: 0x00034CC0
		protected override TResponse InternalProcess()
		{
			TResponse result = this.ProcessObject();
			int num = 0;
			int? maxCount = this.MaxChildObjectsToProcessCount;
			if (maxCount != null && maxCount.Value <= 0)
			{
				maxCount = null;
			}
			foreach (TChildId childId in this.GetChildObjectIds(this.ProcessableChildObjectQueries, maxCount).Distinct<TChildId>().ToArray<TChildId>())
			{
				TChild child;
				MigrationProcessorResponse childResponse;
				if (this.TryLoad(childId, out child))
				{
					childResponse = this.ProcessChild(child);
				}
				else
				{
					childResponse = this.DefaultCorruptedChildResponse;
				}
				result.Aggregate(childResponse);
				num++;
			}
			if (maxCount != null && maxCount.Value == num)
			{
				result.Aggregate(MigrationProcessorResponse.Create(MigrationProcessorResult.Working, null, null));
			}
			return result;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00036E48 File Offset: 0x00035048
		protected virtual IEnumerable<TChildId> GetChildObjectIds(Func<int?, IEnumerable<TChildId>>[] queries, int? maxCount = null)
		{
			int totalEncountered = 0;
			int? maxCountRemaining = maxCount;
			foreach (Func<int?, IEnumerable<TChildId>> query in queries)
			{
				if (maxCountRemaining != null && totalEncountered > 0)
				{
					maxCountRemaining = new int?(maxCountRemaining.Value - totalEncountered);
				}
				foreach (TChildId messageId in query(maxCountRemaining))
				{
					totalEncountered++;
					yield return messageId;
					if (maxCount != null && totalEncountered >= maxCount.Value)
					{
						yield break;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000D54 RID: 3412
		protected abstract bool TryLoad(TChildId childId, out TChild child);

		// Token: 0x06000D55 RID: 3413
		protected abstract MigrationProcessorResponse ProcessChild(TChild child);

		// Token: 0x06000D56 RID: 3414
		protected abstract TResponse ProcessObject();
	}
}

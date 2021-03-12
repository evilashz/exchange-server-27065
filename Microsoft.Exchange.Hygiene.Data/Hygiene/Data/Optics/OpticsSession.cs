using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common.Reputation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.Optics
{
	// Token: 0x020001B4 RID: 436
	internal class OpticsSession : IOpticsSession
	{
		// Token: 0x06001241 RID: 4673 RVA: 0x00037BE1 File Offset: 0x00035DE1
		public OpticsSession(string callerId = "Unknown")
		{
			this.callerId = callerId;
			this.DataProvider = ConfigDataProviderFactory.CreateDataProvider(DatabaseType.Optics);
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00037C13 File Offset: 0x00035E13
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00037C1B File Offset: 0x00035E1B
		public IConfigDataProvider DataProvider { get; protected set; }

		// Token: 0x06001244 RID: 4676 RVA: 0x00037C58 File Offset: 0x00035E58
		public IEnumerable<ReputationQueryResult> Query(IEnumerable<ReputationQueryInput> queryInputs)
		{
			if (queryInputs == null || !queryInputs.Any<ReputationQueryInput>())
			{
				return Enumerable.Empty<ReputationQueryResult>();
			}
			if (!this.reputationQueryCache.Initialized)
			{
				this.reputationQueryCache.InitializeCache();
			}
			List<ReputationQueryResult> list = new List<ReputationQueryResult>();
			foreach (ReputationQueryInput reputationQueryInput in queryInputs)
			{
				long value = 0L;
				if (!this.reputationQueryCache.TryGetValue((ReputationEntityType)reputationQueryInput.EntityType, reputationQueryInput.DataPointType, reputationQueryInput.EntityKey, out value))
				{
					break;
				}
				list.Add(new ReputationQueryResult
				{
					EntityType = reputationQueryInput.EntityType,
					DataPointType = reputationQueryInput.DataPointType,
					EntityKey = reputationQueryInput.EntityKey,
					Value = value
				});
			}
			if (queryInputs.Count<ReputationQueryInput>() != list.Count)
			{
				List<QueryFilter> list2 = new List<QueryFilter>();
				list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ReputationQueryResult.QueryInputCountProperty, queryInputs.Count<ReputationQueryInput>()));
				foreach (ReputationQueryInput reputationQueryInput2 in queryInputs)
				{
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ReputationQueryResult.EntityTypeProperty, reputationQueryInput2.EntityType));
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ReputationQueryResult.EntityKeyProperty, reputationQueryInput2.EntityKey));
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ReputationQueryResult.DataPointTypeProperty, reputationQueryInput2.DataPointType));
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ReputationQueryResult.FlagsProperty, reputationQueryInput2.Flags));
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ReputationQueryResult.UdpTimeoutProperty, reputationQueryInput2.UdpTimeout));
				}
				QueryFilter filter = QueryFilter.AndTogether(list2.ToArray());
				IConfigurable[] array = this.DataProvider.Find<ReputationQueryResult>(filter, null, false, null);
				list = ((array != null) ? array.Cast<ReputationQueryResult>().ToList<ReputationQueryResult>() : new List<ReputationQueryResult>());
				using (List<ReputationQueryResult>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						ReputationQueryResult result = enumerator3.Current;
						if (result.Value != -9223372036854775808L)
						{
							int ttl = queryInputs.FirstOrDefault((ReputationQueryInput input) => input.EntityType == result.EntityType && input.DataPointType == result.DataPointType).Ttl;
							this.reputationQueryCache.TryAddValue((ReputationEntityType)result.EntityType, result.DataPointType, result.EntityKey, result.Value, ttl);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00037F28 File Offset: 0x00036128
		internal void Save(OpticsLogBatch batch)
		{
			this.DataProvider.Save(batch);
		}

		// Token: 0x040008BC RID: 2236
		protected const string DefaultCallerId = "Unknown";

		// Token: 0x040008BD RID: 2237
		private ReputationQueryCache reputationQueryCache = new ReputationQueryCache();

		// Token: 0x040008BE RID: 2238
		protected string callerId = "Unknown";
	}
}

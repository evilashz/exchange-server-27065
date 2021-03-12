using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A4 RID: 2468
	internal abstract class BudgetCache<T> : ThrottlingCacheBase<BudgetKey, T> where T : Budget
	{
		// Token: 0x060071EC RID: 29164 RVA: 0x00179A12 File Offset: 0x00177C12
		internal BudgetCache() : base(100000, true, BudgetCache<T>.FiveMinutes, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x060071ED RID: 29165 RVA: 0x00179A28 File Offset: 0x00177C28
		internal int CacheEfficiency
		{
			get
			{
				long num = Interlocked.Read(ref this.cacheHits);
				long num2 = Interlocked.Read(ref this.cacheMisses);
				int result;
				try
				{
					double num3 = (double)(num + num2);
					result = ((num3 == 0.0) ? 100 : ((int)((double)num / num3 * 100.0)));
				}
				catch (OverflowException)
				{
					this.cacheHits = 0L;
					this.cacheMisses = 0L;
					result = 100;
				}
				return result;
			}
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x00179A9C File Offset: 0x00177C9C
		internal static QueryFilter ParseWhereClause(string whereClause)
		{
			QueryParser queryParser = new QueryParser(whereClause, BudgetCache<T>.filterSchema, QueryParser.Capabilities.All, null, new QueryParser.ConvertValueFromStringDelegate(BudgetCache<T>.ConvertValueFromString));
			return queryParser.ParseTree;
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x00179AD0 File Offset: 0x00177CD0
		internal BudgetCacheHandlerMetadata GetMetadata(string filter)
		{
			QueryFilter queryFilter = null;
			if (!string.IsNullOrEmpty(filter))
			{
				queryFilter = BudgetCache<T>.ParseWhereClause(filter);
			}
			List<T> values = base.Values;
			BudgetCacheHandlerMetadata budgetCacheHandlerMetadata = new BudgetCacheHandlerMetadata();
			budgetCacheHandlerMetadata.TotalCount = base.Count;
			budgetCacheHandlerMetadata.Efficiency = this.CacheEfficiency;
			budgetCacheHandlerMetadata.Budgets = new List<BudgetHandlerMetadata>();
			foreach (T t in values)
			{
				Budget budget = t;
				if (queryFilter != null)
				{
					IReadOnlyPropertyBag readOnlyPropertyBag = budget;
					if (readOnlyPropertyBag != null && !OpathFilterEvaluator.FilterMatches(queryFilter, readOnlyPropertyBag))
					{
						continue;
					}
				}
				if (budget.Owner.IsServiceAccountBudget)
				{
					budgetCacheHandlerMetadata.ServiceAccountBudgets++;
				}
				BudgetHandlerMetadata budgetHandlerMetadata = new BudgetHandlerMetadata();
				budgetHandlerMetadata.Locked = false;
				budgetHandlerMetadata.LockedAt = null;
				budgetHandlerMetadata.LockedUntil = null;
				float balance = budget.CasTokenBucket.GetBalance();
				if (balance >= 0f)
				{
					budgetCacheHandlerMetadata.NotThrottled++;
				}
				else if (budget.CasTokenBucket.Locked)
				{
					budgetHandlerMetadata.Locked = true;
					budgetHandlerMetadata.LockedAt = budget.CasTokenBucket.LockedAt.ToString();
					budgetHandlerMetadata.LockedUntil = budget.CasTokenBucket.LockedUntilUtc.ToString();
					budgetCacheHandlerMetadata.InCutoff++;
				}
				else
				{
					budgetCacheHandlerMetadata.InMicroDelay++;
				}
				budgetHandlerMetadata.Key = budget.Owner.ToString();
				budgetHandlerMetadata.OutstandingActions = budget.OutstandingActionsCount;
				budgetHandlerMetadata.Snapshot = budget.ToString();
				budgetCacheHandlerMetadata.Budgets.Add(budgetHandlerMetadata);
			}
			budgetCacheHandlerMetadata.MatchingCount = budgetCacheHandlerMetadata.Budgets.Count;
			return budgetCacheHandlerMetadata;
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x00179CA8 File Offset: 0x00177EA8
		protected override bool HandleShouldRemove(BudgetKey key, T value)
		{
			return value.CanExpire;
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x00179CB8 File Offset: 0x00177EB8
		protected override T CreateOnCacheMiss(BudgetKey key, ref bool shouldAdd)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IThrottlingPolicy throttlingPolicy = null;
			LookupBudgetKey lookupBudgetKey = key as LookupBudgetKey;
			if (lookupBudgetKey != null)
			{
				throttlingPolicy = lookupBudgetKey.Lookup();
			}
			if (throttlingPolicy == null)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[BudgetCache.CreateOnCacheMiss] Using global policy for account: {0}", key.ToString());
				throttlingPolicy = ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
			}
			T result = this.CreateBudget(key, throttlingPolicy);
			ThrottlingPerfCounterWrapper.IncrementBudgetCount();
			Interlocked.Increment(ref this.cacheMisses);
			return result;
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x00179D37 File Offset: 0x00177F37
		protected override void HandleRemove(BudgetKey key, T value, RemoveReason reason)
		{
			ThrottlingPerfCounterWrapper.DecrementBudgetCount();
			base.HandleRemove(key, value, reason);
			value.Expire();
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x00179D54 File Offset: 0x00177F54
		protected override void AfterCacheHit(BudgetKey key, T value)
		{
			Interlocked.Increment(ref this.cacheHits);
			value.AfterCacheHit();
			base.AfterCacheHit(key, value);
		}

		// Token: 0x060071F4 RID: 29172
		protected abstract T CreateBudget(BudgetKey key, IThrottlingPolicy policy);

		// Token: 0x060071F5 RID: 29173 RVA: 0x00179D78 File Offset: 0x00177F78
		private static object ConvertValueFromString(object valueToConvert, Type resultType)
		{
			string text = valueToConvert as string;
			bool flag;
			if (resultType == typeof(bool) && bool.TryParse(text, out flag))
			{
				return flag;
			}
			object result;
			if (resultType.IsEnum && EnumValidator.TryParse(resultType, text, EnumParseOptions.Default, out result))
			{
				return result;
			}
			if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				bool flag2 = text == null || "null".Equals(text, StringComparison.OrdinalIgnoreCase) || "$null".Equals(text, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					return null;
				}
			}
			return LanguagePrimitives.ConvertTo(text, resultType);
		}

		// Token: 0x040049C6 RID: 18886
		private static readonly ObjectSchema filterSchema = ObjectSchema.GetInstance<BudgetMetadataSchema>();

		// Token: 0x040049C7 RID: 18887
		private static readonly TimeSpan FiveMinutes = TimeSpan.FromMinutes(5.0);

		// Token: 0x040049C8 RID: 18888
		private long cacheHits;

		// Token: 0x040049C9 RID: 18889
		private long cacheMisses;
	}
}

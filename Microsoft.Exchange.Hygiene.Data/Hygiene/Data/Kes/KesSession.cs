using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;
using Microsoft.Exchange.Hygiene.Data.Spam;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x020001DA RID: 474
	internal class KesSession : KesSpamCommon
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x0003FE8C File Offset: 0x0003E08C
		public KesSession(string callerId = "Unknown")
		{
			this.callerId = callerId;
			base.DataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Kes);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0003FEAD File Offset: 0x0003E0AD
		internal ReputationList[] FindReputationLists()
		{
			return base.DataProvider.Find<ReputationList>(base.BuildVersionParam, null, false, null).Cast<ReputationList>().ToArray<ReputationList>();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0003FECD File Offset: 0x0003E0CD
		internal ReputationList FindReputationListByID(byte reputationListID)
		{
			return base.DataProvider.Find<ReputationList>(new ComparisonFilter(ComparisonOperator.Equal, ReputationList.ReputationListIDProperty, reputationListID), null, false, null).Cast<ReputationList>().FirstOrDefault<ReputationList>();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
		internal ReputationListType FindReputationListTypeByID(byte reputationListTypeID)
		{
			return base.DataProvider.Find<ReputationListType>(new ComparisonFilter(ComparisonOperator.Equal, ReputationListType.ReputationListTypeIDProperty, reputationListTypeID), null, false, null).Cast<ReputationListType>().FirstOrDefault<ReputationListType>();
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003FF23 File Offset: 0x0003E123
		internal ReputationListSettings[] FindReputationListSettings(byte reputationListID)
		{
			return base.DataProvider.Find<ReputationListSettings>(new ComparisonFilter(ComparisonOperator.Equal, ReputationListSettings.ReputationListIDProperty, reputationListID), null, false, null).Cast<ReputationListSettings>().ToArray<ReputationListSettings>();
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0003FF50 File Offset: 0x0003E150
		internal IEnumerable<SpamExclusionData> FindSpamExclusionData(SpamExclusionDataID dataID, byte dataTypeID, Guid? spamExclusionDataID = null, string exclusionDataTag = null)
		{
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.DataIDProperty, dataID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.DataTypeIDProperty, dataTypeID)
			});
			if (spamExclusionDataID == null && exclusionDataTag == null)
			{
				throw new ArgumentNullException("SpamExclusionDataID or ExclusionDataTag must be set. Use FindPaged function if you need to read all data.");
			}
			if (spamExclusionDataID != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.SpamExclusionDataIDProperty, spamExclusionDataID)
				});
			}
			if (exclusionDataTag != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.ExclusionDataTagProperty, exclusionDataTag)
				});
			}
			return base.DataProvider.Find<SpamExclusionData>(queryFilter, null, true, null).Cast<SpamExclusionData>();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00040014 File Offset: 0x0003E214
		internal IPagedReader<SpamExclusionData> FindPagedSpamExclusionData(SpamExclusionDataID dataID, byte dataTypeID, bool? isPersistent = null, bool? isListed = null, string createdBy = null, DateTime? expirationDate = null, string comment = null, int pageSize = 1000)
		{
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.DataIDProperty, dataID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.DataTypeIDProperty, dataTypeID)
			});
			if (isPersistent != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.IsPersistentProperty, isPersistent)
				});
			}
			if (isListed != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.IsListedProperty, isListed)
				});
			}
			if (createdBy != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.CreatedByProperty, createdBy)
				});
			}
			if (expirationDate != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.ExpirationDateProperty, expirationDate)
				});
			}
			if (comment != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamExclusionData.CommentProperty, comment)
				});
			}
			return new ConfigDataProviderPagedReader<SpamExclusionData>(base.DataProvider, null, queryFilter, null, pageSize);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004015C File Offset: 0x0003E35C
		internal SyncWatermark[] FindLastSyncWatermark(IEnumerable<string> syncContext)
		{
			if (syncContext != null && syncContext.Any<string>())
			{
				string[] source = syncContext.Distinct<string>().ToArray<string>();
				QueryFilter filter = QueryFilter.AndTogether((from item in source
				select new ComparisonFilter(ComparisonOperator.Equal, SyncWatermark.SyncContextProperty, item)).ToArray<ComparisonFilter>());
				return base.DataProvider.Find<SyncWatermark>(filter, null, false, null).Cast<SyncWatermark>().ToArray<SyncWatermark>();
			}
			return new SyncWatermark[0];
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00040244 File Offset: 0x0003E444
		internal SpamRulePackageData FindSpamRules(RuleScopeType scopeIDs, RuleStatusType ruleStatus, out SpamRulePackageData newRules, out SpamRulePackageData deletedRules, ref DateTime? changedDate, bool activeOnly = true)
		{
			newRules = null;
			deletedRules = null;
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 0),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.ScopeIDProperty, (byte)scopeIDs),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, activeOnly),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.StateProperty, (byte)ruleStatus),
				base.BuildVersionParam
			});
			if (changedDate != null && changedDate != DateTime.MinValue)
			{
				DateTime dateTime = changedDate.Value.Subtract(TimeSpan.FromSeconds(30.0));
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, RuleBase.ChangedDatetimeProperty, dateTime)
				});
			}
			List<SpamRule> list = base.DataProvider.Find<SpamRule>(queryFilter, null, true, null).Cast<SpamRule>().ToList<SpamRule>();
			if (list != null && list.Count > 0)
			{
				List<SpamRule> list2 = (from item in list
				where item.IsActive.Value
				select item).ToList<SpamRule>();
				if (list2 != null && list2.Any<SpamRule>())
				{
					newRules = base.GenerateSpamRulePackageData(list2);
				}
				List<SpamRule> list3 = (from item in list
				where !item.IsActive.Value
				select item).ToList<SpamRule>();
				if (list3 != null && list3.Count > 0)
				{
					deletedRules = base.GenerateSpamRulePackageData(list3);
				}
				changedDate = new DateTime?((from item in list
				where item.ChangeDatetime != null
				select item.ChangeDatetime.Value).Max<DateTime>());
				return base.GenerateSpamRulePackageData(list);
			}
			return null;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000404C4 File Offset: 0x0003E6C4
		internal void FindURIRules(RuleScopeType scopeIDs, RuleStatusType ruleStatus, out URIRulePackageData newRules, out URIRulePackageData deletedRules, ref DateTime? changedDate, bool activeOnly = true)
		{
			newRules = null;
			deletedRules = null;
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 1),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.ScopeIDProperty, (byte)scopeIDs),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, activeOnly),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.StateProperty, (byte)ruleStatus),
				base.BuildVersionParam
			});
			if (changedDate != null && changedDate.Value != DateTime.MinValue)
			{
				DateTime dateTime = changedDate.Value.Subtract(TimeSpan.FromSeconds(30.0));
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, RuleBase.ChangedDatetimeProperty, dateTime)
				});
			}
			List<URIRule> list = base.DataProvider.Find<URIRule>(queryFilter, null, true, null).Cast<URIRule>().ToList<URIRule>();
			if (list != null && list.Count > 0)
			{
				List<URIRule> list2 = (from item in list
				where item.IsActive.Value
				select item).ToList<URIRule>();
				if (list2 != null && list2.Any<URIRule>())
				{
					newRules = base.GenerateURIRulePackageData(list2);
				}
				List<URIRule> list3 = (from item in list
				where !item.IsActive.Value
				select item).ToList<URIRule>();
				if (list3 != null && list3.Count > 0)
				{
					deletedRules = base.GenerateURIRulePackageData(list3);
				}
				changedDate = new DateTime?((from item in list
				where item.ChangeDatetime != null
				select item.ChangeDatetime.Value).Max<DateTime>());
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x000406EC File Offset: 0x0003E8EC
		internal SpamRulePackageData FindSpamRules(RuleScopeType scopeIDs, RuleStatusType ruleStatus, out SpamRulePackageData newRules, out SpamRulePackageData deletedRules, ref string pageCookie, bool activeOnly = true)
		{
			newRules = null;
			deletedRules = null;
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 0),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.ScopeIDProperty, (byte)scopeIDs),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, activeOnly),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.StateProperty, (byte)ruleStatus),
				base.BuildVersionParam
			});
			List<SpamRule> list = base.FindPagedRules<SpamRule>(queryFilter, ref pageCookie, 1000);
			if (list != null && list.Count > 0)
			{
				List<SpamRule> list2 = (from item in list
				where item.IsActive.Value
				select item).ToList<SpamRule>();
				if (list2 != null && list2.Any<SpamRule>())
				{
					newRules = base.GenerateSpamRulePackageData(list2);
				}
				List<SpamRule> list3 = (from item in list
				where !item.IsActive.Value
				select item).ToList<SpamRule>();
				if (list3 != null && list3.Count > 0)
				{
					deletedRules = base.GenerateSpamRulePackageData(list3);
				}
				return base.GenerateSpamRulePackageData(list);
			}
			return null;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00040854 File Offset: 0x0003EA54
		internal void FindURIRules(RuleScopeType scopeIDs, RuleStatusType ruleStatus, out URIRulePackageData newRules, out URIRulePackageData deletedRules, ref string pageCookie, bool activeOnly = true)
		{
			newRules = null;
			deletedRules = null;
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 1),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.ScopeIDProperty, (byte)scopeIDs),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, activeOnly),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.StateProperty, (byte)ruleStatus),
				base.BuildVersionParam
			});
			List<URIRule> list = base.FindPagedRules<URIRule>(queryFilter, ref pageCookie, 1000);
			if (list != null && list.Count > 0)
			{
				List<URIRule> list2 = (from item in list
				where item.IsActive.Value
				select item).ToList<URIRule>();
				if (list2 != null && list2.Any<URIRule>())
				{
					newRules = base.GenerateURIRulePackageData(list2);
				}
				List<URIRule> list3 = (from item in list
				where !item.IsActive.Value
				select item).ToList<URIRule>();
				if (list3 != null && list3.Count > 0)
				{
					deletedRules = base.GenerateURIRulePackageData(list3);
				}
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00040974 File Offset: 0x0003EB74
		internal void Save(IEnumerable<MessageRuleMap> items)
		{
			if (items != null && items.Any<MessageRuleMap>())
			{
				foreach (IEnumerable<MessageRuleMap> enumerable in items.BatchSplit(1000))
				{
					foreach (MessageRuleMap configurable in enumerable)
					{
						base.Save(configurable);
					}
				}
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00040A04 File Offset: 0x0003EC04
		internal MessageRuleMap[] FindMessageRuleMaps(byte? processingGroup = null)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (processingGroup != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, MessageRuleMap.ProcessingGroupProperty, processingGroup.Value));
			}
			return base.DataProvider.Find<MessageRuleMap>(new AndFilter(list.ToArray()), null, false, null).Cast<MessageRuleMap>().ToArray<MessageRuleMap>();
		}
	}
}

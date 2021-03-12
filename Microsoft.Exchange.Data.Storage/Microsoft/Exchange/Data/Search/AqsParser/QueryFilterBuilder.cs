using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Search.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.StructuredQuery;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000D02 RID: 3330
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class QueryFilterBuilder
	{
		// Token: 0x06007294 RID: 29332 RVA: 0x001FB1E8 File Offset: 0x001F93E8
		private QueryFilter BuildTextFilter(PropertyDefinition[] properties, string propertyValue, MatchOptions matchOption, MatchFlags matchFlags)
		{
			if (string.IsNullOrEmpty(propertyValue))
			{
				return null;
			}
			if (properties.Length == 1)
			{
				return new TextFilter(properties[0], propertyValue, matchOption, matchFlags);
			}
			if (properties.Length > 1)
			{
				QueryFilter[] array = new QueryFilter[properties.Length];
				for (int i = 0; i < properties.Length; i++)
				{
					array[i] = new TextFilter(properties[i], propertyValue, matchOption, matchFlags);
				}
				return new OrFilter(array);
			}
			return null;
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x001FB248 File Offset: 0x001F9448
		private QueryFilter BuildTextFilter(PropertyDefinition[] properties, string propertyValue, ConditionOperation opt)
		{
			if (string.IsNullOrEmpty(propertyValue))
			{
				return null;
			}
			MatchOptions matchOption;
			switch (opt)
			{
			case 1:
			case 12:
				matchOption = MatchOptions.ExactPhrase;
				goto IL_69;
			case 7:
			case 13:
				matchOption = (this.IsContentIndexingEnabled ? MatchOptions.PrefixOnWords : MatchOptions.SubString);
				goto IL_69;
			case 8:
			case 9:
				matchOption = MatchOptions.SubString;
				goto IL_69;
			case 11:
				matchOption = MatchOptions.WildcardString;
				goto IL_69;
			}
			return null;
			IL_69:
			int num;
			if (!this.ShortWildcardsAllowed && (propertyValue.Length <= 1 || (propertyValue.Length <= 4 && int.TryParse(propertyValue, NumberStyles.Integer, this.culture, out num))))
			{
				matchOption = MatchOptions.ExactPhrase;
			}
			return this.BuildTextFilter(properties, propertyValue, matchOption, MatchFlags.Loose);
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x001FB2F8 File Offset: 0x001F94F8
		private QueryFilter BuildComparisonFilter(ComparisonOperator comparisionOpt, PropertyDefinition[] properties, object propertyValue)
		{
			if (this.IsQueryConverting && propertyValue != null && propertyValue is ExDateTime)
			{
				propertyValue = new ExDateTime(ExTimeZone.CurrentTimeZone, ((ExDateTime)propertyValue).UniversalTime);
			}
			if (properties.Length == 1)
			{
				return new ComparisonFilter(comparisionOpt, properties[0], propertyValue);
			}
			if (properties.Length > 1)
			{
				QueryFilter[] array = new QueryFilter[properties.Length];
				for (int i = 0; i < properties.Length; i++)
				{
					array[i] = new ComparisonFilter(comparisionOpt, properties[i], propertyValue);
				}
				return new OrFilter(array);
			}
			return null;
		}

		// Token: 0x06007297 RID: 29335 RVA: 0x001FB37C File Offset: 0x001F957C
		private QueryFilter BuildExistsFilter(PropertyDefinition[] properties)
		{
			if (properties.Length == 1)
			{
				return new ExistsFilter(properties[0]);
			}
			if (properties.Length > 1)
			{
				QueryFilter[] array = new QueryFilter[properties.Length];
				for (int i = 0; i < properties.Length; i++)
				{
					array[i] = new ExistsFilter(properties[i]);
				}
				return new OrFilter(array);
			}
			return null;
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x001FB3C8 File Offset: 0x001F95C8
		private QueryFilter BuildComparisonFilter(ConditionOperation opt, PropertyDefinition[] properties, object propertyValue)
		{
			ComparisonOperator comparisionOpt;
			switch (opt)
			{
			case 1:
				comparisionOpt = ComparisonOperator.Equal;
				goto IL_3C;
			case 3:
				comparisionOpt = ComparisonOperator.LessThan;
				goto IL_3C;
			case 4:
				comparisionOpt = ComparisonOperator.GreaterThan;
				goto IL_3C;
			case 5:
				comparisionOpt = ComparisonOperator.LessThanOrEqual;
				goto IL_3C;
			case 6:
				comparisionOpt = ComparisonOperator.GreaterThanOrEqual;
				goto IL_3C;
			}
			return null;
			IL_3C:
			return this.BuildComparisonFilter(comparisionOpt, properties, propertyValue);
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x001FB41C File Offset: 0x001F961C
		private QueryFilter BuildRecipientFilter(PropertyDefinition[] propertyMapping, ConditionOperation opt, object propertyValue)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			QueryFilter queryFilter = this.BuildTextFilter(propertyMapping, (string)propertyValue, opt);
			if (queryFilter != null)
			{
				list.Add(queryFilter);
			}
			if (this.RecipientResolver != null)
			{
				string[] array = this.RecipientResolver.Resolve(propertyValue as string);
				if (array != null)
				{
					foreach (string text in array)
					{
						if (!text.Equals((string)propertyValue, StringComparison.OrdinalIgnoreCase))
						{
							queryFilter = this.BuildTextFilter(propertyMapping, text, opt);
							if (queryFilter != null)
							{
								list.Add(queryFilter);
							}
						}
					}
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			if (list.Count <= 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x001FB4CC File Offset: 0x001F96CC
		private QueryFilter BuildFromFilter(ConditionOperation opt, object value)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			QueryFilter queryFilter = this.BuildTextFilter(this.fromMapping, (string)value, opt);
			if (queryFilter != null)
			{
				list.Add(queryFilter);
			}
			if (this.RecipientResolver != null)
			{
				string[] array = this.RecipientResolver.Resolve(value as string);
				if (array != null)
				{
					foreach (string text in array)
					{
						if (!text.Equals((string)value, StringComparison.OrdinalIgnoreCase))
						{
							queryFilter = this.BuildTextFilter(this.fromMapping, text, 1);
							if (queryFilter != null)
							{
								list.Add(queryFilter);
							}
						}
					}
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			if (list.Count <= 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x001FB585 File Offset: 0x001F9785
		private QueryFilter BuildToFilter(ConditionOperation opt, object value)
		{
			return this.BuildRecipientFilter(this.toMapping, opt, value);
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x001FB595 File Offset: 0x001F9795
		private QueryFilter BuildBccFilter(ConditionOperation opt, object value)
		{
			return this.BuildRecipientFilter(this.bccMapping, opt, value);
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x001FB5A5 File Offset: 0x001F97A5
		private QueryFilter BuildCcFilter(ConditionOperation opt, object value)
		{
			return this.BuildRecipientFilter(this.ccMapping, opt, value);
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x001FB5B8 File Offset: 0x001F97B8
		private QueryFilter BuildParticipantsFilter(ConditionOperation opt, object value)
		{
			return this.BuildRecipientFilter(this.IsQueryConverting ? new PropertyDefinition[]
			{
				this.participantsMapping[0]
			} : this.participantsMapping, opt, value);
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x001FB5F0 File Offset: 0x001F97F0
		private QueryFilter BuildSubjectFilter(ConditionOperation opt, object value)
		{
			return this.BuildTextFilter(this.subjectMapping, (string)value, opt);
		}

		// Token: 0x060072A0 RID: 29344 RVA: 0x001FB605 File Offset: 0x001F9805
		private QueryFilter BuildBodyFilter(ConditionOperation opt, object value)
		{
			return this.BuildTextFilter(this.bodyMapping, (string)value, opt);
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x001FB61C File Offset: 0x001F981C
		private QueryFilter BuildAttachmentFilter(ConditionOperation opt, object value)
		{
			QueryFilter queryFilter = this.BuildTextFilter(this.attachmentMapping, (string)value, opt);
			if (queryFilter != null && !this.IsContentIndexingEnabled)
			{
				QueryFilter queryFilter2 = this.BuildTextFilter(this.attachmentSubMapping, (string)value, opt);
				if (queryFilter2 != null)
				{
					return new OrFilter(new QueryFilter[]
					{
						queryFilter,
						new SubFilter(SubFilterProperties.Attachments, queryFilter2)
					});
				}
			}
			return queryFilter;
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x001FB67C File Offset: 0x001F987C
		private QueryFilter BuildAttachmentNamesFilter(ConditionOperation opt, object value)
		{
			return this.BuildTextFilter(this.attachmentNamesMapping, (string)value, opt);
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x001FB694 File Offset: 0x001F9894
		private QueryFilter BuildSentFilter(ConditionOperation opt, object value)
		{
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, ((DateTime)value).ToUniversalTime());
			return this.BuildComparisonFilter(opt, this.sentMapping, exDateTime);
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x001FB6D0 File Offset: 0x001F98D0
		private QueryFilter BuildReceivedFilter(ConditionOperation opt, object value)
		{
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, ((DateTime)value).ToUniversalTime());
			return this.BuildComparisonFilter(opt, this.receivedMapping, exDateTime);
		}

		// Token: 0x060072A5 RID: 29349 RVA: 0x001FB70C File Offset: 0x001F990C
		private QueryFilter BuildKindFilter(ConditionOperation opt, object value)
		{
			if (AqsParser.KindKeywordMap.ContainsKey((string)value))
			{
				KindKeyword kindKeyword = AqsParser.KindKeywordMap[(string)value];
				if (this.IsQueryConverting)
				{
					return this.BuildTextFilter(this.kindMapping, kindKeyword.ToString(), 1);
				}
				List<QueryFilter> list = new List<QueryFilter>();
				string[] array = Globals.KindKeywordToClassMap[kindKeyword];
				foreach (string propertyValue in array)
				{
					QueryFilter queryFilter = this.BuildTextFilter(this.kindMapping, propertyValue, 13);
					if (queryFilter != null)
					{
						list.Add(queryFilter);
					}
				}
				if (list.Count > 0)
				{
					if (list.Count <= 1)
					{
						return list[0];
					}
					return new OrFilter(list.ToArray());
				}
			}
			return null;
		}

		// Token: 0x060072A6 RID: 29350 RVA: 0x001FB7D2 File Offset: 0x001F99D2
		private QueryFilter BuildHasAttachmentFilter(ConditionOperation opt, object value)
		{
			if (value is bool)
			{
				return this.BuildComparisonFilter(1, this.hasAttachmentMapping, (bool)value);
			}
			return null;
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x001FB7F8 File Offset: 0x001F99F8
		private QueryFilter BuildIsFlaggedFilter(ConditionOperation opt, object value)
		{
			if (!(value is bool))
			{
				return null;
			}
			if ((bool)value)
			{
				return this.BuildComparisonFilter(1, this.isFlaggedMapping, FlagStatus.Flagged);
			}
			return new OrFilter(new QueryFilter[]
			{
				this.BuildComparisonFilter(1, this.isFlaggedMapping, FlagStatus.NotFlagged),
				new NotFilter(this.BuildExistsFilter(this.isFlaggedMapping))
			});
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x001FB862 File Offset: 0x001F9A62
		private QueryFilter BuildIsReadFilter(ConditionOperation opt, object value)
		{
			if (value is bool)
			{
				return this.BuildComparisonFilter(1, this.isReadMapping, (bool)value);
			}
			return null;
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x001FB886 File Offset: 0x001F9A86
		private QueryFilter BuildCategoryFilter(ConditionOperation opt, object value)
		{
			return this.BuildTextFilter(this.categoryMapping, (string)value, opt);
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x001FB89C File Offset: 0x001F9A9C
		private QueryFilter BuildImportanceFilter(ConditionOperation opt, object value)
		{
			int? num = null;
			if (value is long)
			{
				num = new int?((int)((long)value));
			}
			else if (value is int)
			{
				num = new int?((int)value);
			}
			if (num != null)
			{
				QueryFilter queryFilter = this.BuildComparisonFilter(opt, this.importanceMapping, num.Value / 2);
				if (this.IsQueryConverting && queryFilter.Equals(QueryFilterBuilder.importanceHighFilter))
				{
					queryFilter = this.BuildTextFilter(this.importanceMapping, Importance.High.ToString(), 1);
				}
				return queryFilter;
			}
			return null;
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x001FB934 File Offset: 0x001F9B34
		private QueryFilter BuildSizeFilter(ConditionOperation opt, object value)
		{
			int? num = null;
			if (value is long)
			{
				num = new int?((int)((long)value));
			}
			else if (value is int)
			{
				num = new int?((int)value);
			}
			if (num != null)
			{
				return this.BuildComparisonFilter(opt, this.sizeMapping, num.Value);
			}
			return null;
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x001FB9D8 File Offset: 0x001F9BD8
		private QueryFilter BuildPolicyTagFilter(ConditionOperation opt, object value)
		{
			string tagName = value as string;
			if (string.IsNullOrEmpty(tagName))
			{
				return null;
			}
			List<QueryFilter> list = new List<QueryFilter>();
			if (QueryFilterBuilder.guidRegex.Match(tagName).Success)
			{
				Guid guid = Guid.Empty;
				try
				{
					guid = new Guid(tagName);
					list.Add(this.BuildComparisonFilter(ComparisonOperator.Equal, this.policyTagMapping, guid.ToByteArray()));
				}
				catch (FormatException)
				{
				}
			}
			IEnumerable<PolicyTag> enumerable = null;
			if (this.PolicyTagProvider != null)
			{
				PolicyTag[] policyTags = this.PolicyTagProvider.PolicyTags;
				if (policyTags != null)
				{
					switch (opt)
					{
					case 1:
					case 12:
						enumerable = from x in policyTags
						where x.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase)
						select x;
						break;
					case 7:
					case 8:
					case 9:
					case 11:
					case 13:
					{
						Regex regex = new Regex(tagName, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
						enumerable = from x in policyTags
						where regex.Match(x.Name).Success
						select x;
						break;
					}
					}
				}
			}
			if (enumerable != null)
			{
				foreach (PolicyTag policyTag in enumerable)
				{
					list.Add(this.BuildComparisonFilter(ComparisonOperator.Equal, this.policyTagMapping, policyTag.PolicyGuid.ToByteArray()));
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			if (list.Count <= 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x001FBBAC File Offset: 0x001F9DAC
		private QueryFilter BuildExpiresFilter(ConditionOperation opt, object value)
		{
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, ((DateTime)value).ToUniversalTime());
			return this.BuildComparisonFilter(opt, this.expiresMapping, exDateTime);
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x001FBBE8 File Offset: 0x001F9DE8
		internal QueryFilter BuildAllFilter(ConditionOperation opt, object value)
		{
			PropertyKeyword[] array2;
			switch (this.RescopedAll)
			{
			case RescopedAll.Default:
				return this.BuildTextFilter(this.allMapping, (string)value, opt);
			case RescopedAll.Subject:
			{
				PropertyKeyword[] array = new PropertyKeyword[1];
				array2 = array;
				break;
			}
			case RescopedAll.Body:
				array2 = new PropertyKeyword[]
				{
					PropertyKeyword.Body
				};
				break;
			case RescopedAll.BodyAndSubject:
				array2 = new PropertyKeyword[]
				{
					PropertyKeyword.Subject,
					PropertyKeyword.Body
				};
				break;
			case RescopedAll.From:
				array2 = new PropertyKeyword[]
				{
					PropertyKeyword.From
				};
				break;
			case RescopedAll.Participants:
				array2 = new PropertyKeyword[]
				{
					PropertyKeyword.Participants
				};
				break;
			default:
				throw new ArgumentException("RescopedAll");
			}
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (PropertyKeyword key in array2)
			{
				QueryFilter queryFilter = this.filterBuilderMap[key](opt, value);
				if (queryFilter != null)
				{
					list.Add(queryFilter);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			if (list.Count <= 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x001FBCFC File Offset: 0x001F9EFC
		private QueryFilter PredicateToQueryFilter(LeafCondition leafCond)
		{
			if (!AqsParser.PropertyKeywordMap.ContainsKey(leafCond.PropertyName))
			{
				return null;
			}
			PropertyKeyword? propertyKeyword = new PropertyKeyword?(AqsParser.PropertyKeywordMap[leafCond.PropertyName]);
			if (propertyKeyword == null)
			{
				return null;
			}
			if (!this.AllowedKeywords.Contains(propertyKeyword.Value))
			{
				return null;
			}
			if (leafCond.Operation == 2)
			{
				QueryFilter queryFilter = this.filterBuilderMap[propertyKeyword.Value](1, leafCond.Value);
				if (queryFilter != null)
				{
					return new NotFilter(queryFilter);
				}
			}
			else
			{
				if (leafCond.Operation != 10)
				{
					return this.filterBuilderMap[propertyKeyword.Value](leafCond.Operation, leafCond.Value);
				}
				QueryFilter queryFilter2 = this.filterBuilderMap[propertyKeyword.Value](9, leafCond.Value);
				if (queryFilter2 != null)
				{
					return new NotFilter(queryFilter2);
				}
			}
			return null;
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x001FBDE0 File Offset: 0x001F9FE0
		private QueryFilter ConditionToQueryFilter(Condition condition)
		{
			if (condition.Type == null)
			{
				HashSet<QueryFilter> hashSet = new HashSet<QueryFilter>();
				foreach (Condition condition2 in ((CompoundCondition)condition).Children)
				{
					QueryFilter queryFilter = this.ConditionToQueryFilter(condition2);
					if (queryFilter == null)
					{
						return null;
					}
					hashSet.Add(queryFilter);
				}
				if (hashSet.Count > 0)
				{
					QueryFilter queryFilter2 = (hashSet.Count > 1) ? new AndFilter(hashSet.ToArray<QueryFilter>()) : hashSet.First<QueryFilter>();
					if (this.IsQueryConverting)
					{
						if (queryFilter2.Equals(QueryFilterBuilder.importanceNormalFilter))
						{
							queryFilter2 = this.BuildTextFilter(this.importanceMapping, Importance.Normal.ToString(), 1);
						}
						else if (queryFilter2.Equals(QueryFilterBuilder.importanceLowFilter))
						{
							queryFilter2 = this.BuildTextFilter(this.importanceMapping, Importance.Low.ToString(), 1);
						}
					}
					return queryFilter2;
				}
			}
			else if (condition.Type == 1)
			{
				HashSet<QueryFilter> hashSet2 = new HashSet<QueryFilter>();
				foreach (Condition condition3 in ((CompoundCondition)condition).Children)
				{
					QueryFilter queryFilter3 = this.ConditionToQueryFilter(condition3);
					if (queryFilter3 == null)
					{
						return null;
					}
					hashSet2.Add(queryFilter3);
				}
				if (hashSet2.Count > 0)
				{
					if (hashSet2.Count <= 1)
					{
						return hashSet2.First<QueryFilter>();
					}
					return new OrFilter(hashSet2.ToArray<QueryFilter>());
				}
			}
			else if (condition.Type == 2)
			{
				QueryFilter queryFilter4 = this.ConditionToQueryFilter(((NegationCondition)condition).Child);
				if (queryFilter4 != null)
				{
					return new NotFilter(queryFilter4);
				}
			}
			else
			{
				if (condition.Type == 3)
				{
					return this.PredicateToQueryFilter((LeafCondition)condition);
				}
				throw new ArgumentException("No condition node other than NOT, AND, OR and Leaf is allowed.");
			}
			return null;
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x060072B1 RID: 29361 RVA: 0x001FBFCC File Offset: 0x001FA1CC
		// (set) Token: 0x060072B2 RID: 29362 RVA: 0x001FBFD4 File Offset: 0x001FA1D4
		public IRecipientResolver RecipientResolver { get; set; }

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x060072B3 RID: 29363 RVA: 0x001FBFDD File Offset: 0x001FA1DD
		// (set) Token: 0x060072B4 RID: 29364 RVA: 0x001FBFE5 File Offset: 0x001FA1E5
		public IPolicyTagProvider PolicyTagProvider { get; set; }

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x060072B5 RID: 29365 RVA: 0x001FBFEE File Offset: 0x001FA1EE
		public bool IsContentIndexingEnabled
		{
			get
			{
				return (this.options & AqsParser.ParseOption.ContentIndexingDisabled) == AqsParser.ParseOption.None;
			}
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x060072B6 RID: 29366 RVA: 0x001FBFFF File Offset: 0x001FA1FF
		public bool ShortWildcardsAllowed
		{
			get
			{
				return (this.options & AqsParser.ParseOption.AllowShortWildcards) != AqsParser.ParseOption.None;
			}
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x060072B7 RID: 29367 RVA: 0x001FC013 File Offset: 0x001FA213
		// (set) Token: 0x060072B8 RID: 29368 RVA: 0x001FC01B File Offset: 0x001FA21B
		public RescopedAll RescopedAll
		{
			get
			{
				return this.rescopedAll;
			}
			set
			{
				this.rescopedAll = value;
			}
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x060072B9 RID: 29369 RVA: 0x001FC024 File Offset: 0x001FA224
		public bool IsQueryConverting
		{
			get
			{
				return (this.options & AqsParser.ParseOption.QueryConverting) != AqsParser.ParseOption.None;
			}
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x060072BA RID: 29370 RVA: 0x001FC038 File Offset: 0x001FA238
		// (set) Token: 0x060072BB RID: 29371 RVA: 0x001FC040 File Offset: 0x001FA240
		public HashSet<PropertyKeyword> AllowedKeywords { get; set; }

		// Token: 0x060072BC RID: 29372 RVA: 0x001FC04C File Offset: 0x001FA24C
		public QueryFilterBuilder(CultureInfo culture, AqsParser.ParseOption options)
		{
			this.culture = culture;
			this.options = options;
			this.AllowedKeywords = PropertyKeywordHelper.AllPropertyKeywords;
			this.filterBuilderMap = new Dictionary<PropertyKeyword, QueryFilterBuilder.FilterBuildDelegate>();
			this.filterBuilderMap.Add(PropertyKeyword.From, new QueryFilterBuilder.FilterBuildDelegate(this.BuildFromFilter));
			this.filterBuilderMap.Add(PropertyKeyword.To, new QueryFilterBuilder.FilterBuildDelegate(this.BuildToFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Bcc, new QueryFilterBuilder.FilterBuildDelegate(this.BuildBccFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Cc, new QueryFilterBuilder.FilterBuildDelegate(this.BuildCcFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Participants, new QueryFilterBuilder.FilterBuildDelegate(this.BuildParticipantsFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Subject, new QueryFilterBuilder.FilterBuildDelegate(this.BuildSubjectFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Body, new QueryFilterBuilder.FilterBuildDelegate(this.BuildBodyFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Sent, new QueryFilterBuilder.FilterBuildDelegate(this.BuildSentFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Received, new QueryFilterBuilder.FilterBuildDelegate(this.BuildReceivedFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Attachment, new QueryFilterBuilder.FilterBuildDelegate(this.BuildAttachmentFilter));
			this.filterBuilderMap.Add(PropertyKeyword.AttachmentNames, new QueryFilterBuilder.FilterBuildDelegate(this.BuildAttachmentNamesFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Kind, new QueryFilterBuilder.FilterBuildDelegate(this.BuildKindFilter));
			this.filterBuilderMap.Add(PropertyKeyword.PolicyTag, new QueryFilterBuilder.FilterBuildDelegate(this.BuildPolicyTagFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Expires, new QueryFilterBuilder.FilterBuildDelegate(this.BuildExpiresFilter));
			this.filterBuilderMap.Add(PropertyKeyword.IsFlagged, new QueryFilterBuilder.FilterBuildDelegate(this.BuildIsFlaggedFilter));
			this.filterBuilderMap.Add(PropertyKeyword.IsRead, new QueryFilterBuilder.FilterBuildDelegate(this.BuildIsReadFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Category, new QueryFilterBuilder.FilterBuildDelegate(this.BuildCategoryFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Importance, new QueryFilterBuilder.FilterBuildDelegate(this.BuildImportanceFilter));
			this.filterBuilderMap.Add(PropertyKeyword.Size, new QueryFilterBuilder.FilterBuildDelegate(this.BuildSizeFilter));
			this.filterBuilderMap.Add(PropertyKeyword.HasAttachment, new QueryFilterBuilder.FilterBuildDelegate(this.BuildHasAttachmentFilter));
			this.filterBuilderMap.Add(PropertyKeyword.All, new QueryFilterBuilder.FilterBuildDelegate(this.BuildAllFilter));
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x001FC4C8 File Offset: 0x001FA6C8
		public QueryFilter Build(Condition condRoot)
		{
			if (condRoot == null)
			{
				throw new ArgumentNullException("condRoot");
			}
			QueryFilter queryFilter = this.ConditionToQueryFilter(condRoot);
			QueryFilterBuilder.Tracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "QueryFilterBuilder.Build creates a filter of {0}", queryFilter);
			return queryFilter;
		}

		// Token: 0x04005004 RID: 20484
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04005005 RID: 20485
		private readonly CultureInfo culture;

		// Token: 0x04005006 RID: 20486
		private Dictionary<PropertyKeyword, QueryFilterBuilder.FilterBuildDelegate> filterBuilderMap;

		// Token: 0x04005007 RID: 20487
		private RescopedAll rescopedAll;

		// Token: 0x04005008 RID: 20488
		private static Regex guidRegex = new Regex("^(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}$", RegexOptions.Compiled);

		// Token: 0x04005009 RID: 20489
		private readonly AqsParser.ParseOption options;

		// Token: 0x0400500A RID: 20490
		private static readonly QueryFilter importanceHighFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.Importance, 2);

		// Token: 0x0400500B RID: 20491
		private static readonly QueryFilter importanceNormalFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.Importance, 1),
			new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.Importance, 2)
		});

		// Token: 0x0400500C RID: 20492
		private static readonly QueryFilter importanceLowFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.Importance, 0),
			new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.Importance, 1)
		});

		// Token: 0x0400500D RID: 20493
		private PropertyDefinition[] fromMapping = new PropertyDefinition[]
		{
			ItemSchema.SearchSender
		};

		// Token: 0x0400500E RID: 20494
		private PropertyDefinition[] toMapping = new PropertyDefinition[]
		{
			ItemSchema.SearchRecipientsTo
		};

		// Token: 0x0400500F RID: 20495
		private PropertyDefinition[] ccMapping = new PropertyDefinition[]
		{
			ItemSchema.SearchRecipientsCc
		};

		// Token: 0x04005010 RID: 20496
		private PropertyDefinition[] bccMapping = new PropertyDefinition[]
		{
			ItemSchema.SearchRecipientsBcc
		};

		// Token: 0x04005011 RID: 20497
		private PropertyDefinition[] participantsMapping = new PropertyDefinition[]
		{
			ItemSchema.SearchRecipients,
			ItemSchema.SearchSender
		};

		// Token: 0x04005012 RID: 20498
		private PropertyDefinition[] bodyMapping = new PropertyDefinition[]
		{
			ItemSchema.TextBody
		};

		// Token: 0x04005013 RID: 20499
		private PropertyDefinition[] subjectMapping = new PropertyDefinition[]
		{
			ItemSchema.Subject
		};

		// Token: 0x04005014 RID: 20500
		private PropertyDefinition[] attachmentMapping = new PropertyDefinition[]
		{
			ItemSchema.AttachmentContent
		};

		// Token: 0x04005015 RID: 20501
		private PropertyDefinition[] attachmentSubMapping = new PropertyDefinition[]
		{
			AttachmentSchema.AttachFileName,
			AttachmentSchema.AttachLongFileName,
			AttachmentSchema.AttachExtension,
			AttachmentSchema.DisplayName
		};

		// Token: 0x04005016 RID: 20502
		private PropertyDefinition[] attachmentNamesMapping = new PropertyDefinition[]
		{
			AttachmentSchema.AttachLongFileName
		};

		// Token: 0x04005017 RID: 20503
		private PropertyDefinition[] sentMapping = new PropertyDefinition[]
		{
			ItemSchema.SentTime
		};

		// Token: 0x04005018 RID: 20504
		private PropertyDefinition[] receivedMapping = new PropertyDefinition[]
		{
			ItemSchema.ReceivedTime
		};

		// Token: 0x04005019 RID: 20505
		private PropertyDefinition[] kindMapping = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		};

		// Token: 0x0400501A RID: 20506
		private PropertyDefinition[] policyTagMapping = new PropertyDefinition[]
		{
			StoreObjectSchema.PolicyTag
		};

		// Token: 0x0400501B RID: 20507
		private PropertyDefinition[] expiresMapping = new PropertyDefinition[]
		{
			ItemSchema.RetentionDate
		};

		// Token: 0x0400501C RID: 20508
		private PropertyDefinition[] hasAttachmentMapping = new PropertyDefinition[]
		{
			ItemSchema.HasAttachment
		};

		// Token: 0x0400501D RID: 20509
		private PropertyDefinition[] isFlaggedMapping = new PropertyDefinition[]
		{
			ItemSchema.FlagStatus
		};

		// Token: 0x0400501E RID: 20510
		private PropertyDefinition[] isReadMapping = new PropertyDefinition[]
		{
			MessageItemSchema.IsRead
		};

		// Token: 0x0400501F RID: 20511
		private PropertyDefinition[] categoryMapping = new PropertyDefinition[]
		{
			ItemSchema.Categories
		};

		// Token: 0x04005020 RID: 20512
		private PropertyDefinition[] importanceMapping = new PropertyDefinition[]
		{
			ItemSchema.Importance
		};

		// Token: 0x04005021 RID: 20513
		private PropertyDefinition[] sizeMapping = new PropertyDefinition[]
		{
			ItemSchema.Size
		};

		// Token: 0x04005022 RID: 20514
		private PropertyDefinition[] allMapping = new PropertyDefinition[]
		{
			ItemSchema.SearchAllIndexedProps
		};

		// Token: 0x02000D03 RID: 3331
		// (Invoke) Token: 0x060072C0 RID: 29376
		private delegate QueryFilter FilterBuildDelegate(ConditionOperation opt, object value);
	}
}

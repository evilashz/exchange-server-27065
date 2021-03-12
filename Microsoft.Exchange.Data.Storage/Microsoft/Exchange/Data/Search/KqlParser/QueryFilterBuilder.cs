using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Search.KqlParser
{
	// Token: 0x02000D0B RID: 3339
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class QueryFilterBuilder : QueryTreeNodeVisitor
	{
		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x060072E4 RID: 29412 RVA: 0x001FD80D File Offset: 0x001FBA0D
		// (set) Token: 0x060072E5 RID: 29413 RVA: 0x001FD815 File Offset: 0x001FBA15
		internal HashSet<PropertyKeyword> AllowedKeywords { get; set; }

		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x060072E6 RID: 29414 RVA: 0x001FD820 File Offset: 0x001FBA20
		internal List<QueryFilter> CurrentLevelFilters
		{
			get
			{
				List<QueryFilter> result;
				if (!this.queryFilters.TryGetValue(this.currentLevel, out result))
				{
					result = (this.queryFilters[this.currentLevel] = new List<QueryFilter>());
				}
				return result;
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x060072E7 RID: 29415 RVA: 0x001FD85C File Offset: 0x001FBA5C
		internal List<QueryFilter> ParentLevelFilters
		{
			get
			{
				List<QueryFilter> result;
				if (!this.queryFilters.TryGetValue(this.currentLevel - 1, out result))
				{
					result = (this.queryFilters[this.currentLevel - 1] = new List<QueryFilter>());
				}
				return result;
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x001FDA18 File Offset: 0x001FBC18
		public QueryFilterBuilder(LocalizedKeywordMapping keywordMapping, KqlParser.ParseOption parseOptions, RescopedAll rescopedAll, IRecipientResolver recipientResolver, IPolicyTagProvider policyTagProvider, CultureInfo culture)
		{
			this.culture = culture;
			this.parseOptions = parseOptions;
			this.AllowedKeywords = PropertyKeywordHelper.AllPropertyKeywords;
			this.keywordMapping = keywordMapping;
			this.recipientResolver = recipientResolver;
			this.policyTagProvider = policyTagProvider;
			this.rescopedAll = rescopedAll;
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x001FDA6E File Offset: 0x001FBC6E
		public QueryFilter Build(TreeNode treeNode)
		{
			if (treeNode != null)
			{
				this.isQueryFilterValid = true;
				treeNode.Accept(this);
				if (this.isQueryFilterValid && this.CurrentLevelFilters.Count > 0)
				{
					return this.CurrentLevelFilters[0];
				}
			}
			return null;
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x001FDAA5 File Offset: 0x001FBCA5
		public QueryFilter BuildAllFilter(string query)
		{
			return QueryFilterBuilder.stringNodeBuilderMap[PropertyKeyword.All](this, Globals.PropertyKeywordToDefinitionMap[PropertyKeyword.All], query, MatchOptions.FullString);
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x001FDAC7 File Offset: 0x001FBCC7
		public override void Visit(AndNode node)
		{
			this.VisitGroupNode();
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x001FDADC File Offset: 0x001FBCDC
		public override void Exit(AndNode node)
		{
			this.ExitGroupNode((List<QueryFilter> currentFilters) => new AndFilter(currentFilters.ToArray()));
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x001FDB01 File Offset: 0x001FBD01
		public override void Visit(OrNode node)
		{
			this.VisitGroupNode();
		}

		// Token: 0x060072EF RID: 29423 RVA: 0x001FDB16 File Offset: 0x001FBD16
		public override void Exit(OrNode node)
		{
			this.ExitGroupNode((List<QueryFilter> currentFilters) => new OrFilter(currentFilters.ToArray()));
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x001FDB3B File Offset: 0x001FBD3B
		public override void Visit(WordsNode node)
		{
			this.VisitGroupNode();
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x001FDB50 File Offset: 0x001FBD50
		public override void Exit(WordsNode node)
		{
			this.ExitGroupNode((List<QueryFilter> currentFilters) => new OrFilter(currentFilters.ToArray()));
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x001FDB75 File Offset: 0x001FBD75
		public override void Visit(NotNode node)
		{
			this.VisitGroupNode();
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x001FDB96 File Offset: 0x001FBD96
		public override void Exit(NotNode node)
		{
			this.ExitGroupNode(delegate(List<QueryFilter> currentFilters)
			{
				if (currentFilters.Count == 1)
				{
					return new NotFilter(currentFilters[0]);
				}
				return null;
			});
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x001FDBBB File Offset: 0x001FBDBB
		public override void Visit(NearNode node)
		{
			this.VisitGroupNode();
		}

		// Token: 0x060072F5 RID: 29429 RVA: 0x001FDBEC File Offset: 0x001FBDEC
		public override void Exit(NearNode node)
		{
			this.ExitGroupNode((List<QueryFilter> currentFilters) => new NearFilter((uint)node.ExtraTermsAllowed, false, new AndFilter(currentFilters.ToArray())));
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x001FDC18 File Offset: 0x001FBE18
		public override void Visit(OnearNode node)
		{
			this.VisitGroupNode();
		}

		// Token: 0x060072F7 RID: 29431 RVA: 0x001FDC48 File Offset: 0x001FBE48
		public override void Exit(OnearNode node)
		{
			this.ExitGroupNode((List<QueryFilter> currentFilters) => new NearFilter((uint)node.ExtraTermsAllowed, true, new AndFilter(currentFilters.ToArray())));
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x001FDC74 File Offset: 0x001FBE74
		public override void Visit(ScopeNode node)
		{
			this.currentScope = node.Scope;
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x001FDC82 File Offset: 0x001FBE82
		public override void Exit(ScopeNode node)
		{
			this.currentScope = null;
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x001FDC8C File Offset: 0x001FBE8C
		public override void Visit(StringNode node)
		{
			PropertyKeyword key;
			if (!this.TryGetCurrentPropertyKeyword(out key))
			{
				this.isQueryFilterValid = false;
				return;
			}
			string text = node.Text;
			MatchOptions options = MatchOptions.FullString;
			if (node.Mode == null)
			{
				options = MatchOptions.ExactPhrase;
			}
			else if (node.Wildcard || (this.parseOptions & KqlParser.ParseOption.DisablePrefixMatch) == KqlParser.ParseOption.None)
			{
				text = text.TrimEnd(new char[]
				{
					'*'
				});
				int num;
				if ((this.parseOptions & KqlParser.ParseOption.AllowShortWildcards) != KqlParser.ParseOption.None || (text.Length > 1 && (text.Length > 4 || !int.TryParse(text, NumberStyles.Integer, this.culture, out num))))
				{
					options = (((this.parseOptions & KqlParser.ParseOption.ContentIndexingDisabled) == KqlParser.ParseOption.None) ? MatchOptions.PrefixOnWords : MatchOptions.SubString);
				}
			}
			QueryFilter queryFilter = QueryFilterBuilder.stringNodeBuilderMap[key](this, Globals.PropertyKeywordToDefinitionMap[key], text, options);
			if (queryFilter != null)
			{
				this.CurrentLevelFilters.Add(queryFilter);
				return;
			}
			this.isQueryFilterValid = false;
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x001FDD68 File Offset: 0x001FBF68
		public override void Visit<T>(RangeNode<T> node)
		{
			PropertyKeyword key;
			if (!this.TryGetCurrentPropertyKeyword(out key))
			{
				this.isQueryFilterValid = false;
				return;
			}
			ComparisonOperator comparisonOperator = ComparisonOperator.Equal;
			bool flag = true;
			if (node.StartInclusive != null)
			{
				comparisonOperator = (node.StartInclusive.Value ? ComparisonOperator.GreaterThanOrEqual : ComparisonOperator.GreaterThan);
				flag = false;
				if (comparisonOperator == ComparisonOperator.GreaterThanOrEqual)
				{
					if (typeof(T) == typeof(DateTime))
					{
						flag = (node.Start as DateTime? == DateTime.MinValue);
					}
					else if (typeof(T) == typeof(int))
					{
						flag = (node.Start as int? == int.MinValue);
					}
					else if (typeof(T) == typeof(float))
					{
						float? num = node.Start as float?;
						flag = (num.GetValueOrDefault() == float.MinValue && num != null);
					}
					else
					{
						this.isQueryFilterValid = false;
					}
				}
			}
			ComparisonOperator comparisonOperator2 = ComparisonOperator.Equal;
			bool flag2 = true;
			if (node.EndInclusive != null)
			{
				comparisonOperator2 = (node.EndInclusive.Value ? ComparisonOperator.LessThanOrEqual : ComparisonOperator.LessThan);
				flag2 = false;
				if (comparisonOperator2 == ComparisonOperator.LessThanOrEqual)
				{
					if (typeof(T) == typeof(DateTime))
					{
						flag2 = (node.End as DateTime? == DateTime.MaxValue);
					}
					else if (typeof(T) == typeof(int))
					{
						flag2 = (node.End as int? == int.MaxValue);
					}
					else if (typeof(T) == typeof(float))
					{
						float? num2 = node.End as float?;
						flag2 = (num2.GetValueOrDefault() == float.MaxValue && num2 != null);
					}
					else
					{
						this.isQueryFilterValid = false;
					}
				}
			}
			PropertyDefinition[] properties = Globals.PropertyKeywordToDefinitionMap[key];
			if (!flag2 && !flag)
			{
				this.CurrentLevelFilters.Add(new AndFilter(new QueryFilter[]
				{
					QueryFilterBuilder.BuildComparisonFilter(properties, comparisonOperator, node.Start),
					QueryFilterBuilder.BuildComparisonFilter(properties, comparisonOperator2, node.End)
				}));
				return;
			}
			if (!flag)
			{
				this.CurrentLevelFilters.Add(QueryFilterBuilder.BuildComparisonFilter(properties, comparisonOperator, node.Start));
				return;
			}
			if (!flag2)
			{
				this.CurrentLevelFilters.Add(QueryFilterBuilder.BuildComparisonFilter(properties, comparisonOperator2, node.End));
				return;
			}
			this.isQueryFilterValid = false;
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x001FE09C File Offset: 0x001FC29C
		public override void Visit<T>(NumericNode<T> node)
		{
			PropertyKeyword key;
			if (this.TryGetCurrentPropertyKeyword(out key))
			{
				this.CurrentLevelFilters.Add(QueryFilterBuilder.BuildComparisonFilter(Globals.PropertyKeywordToDefinitionMap[key], ComparisonOperator.Equal, node.Value));
				return;
			}
			this.isQueryFilterValid = false;
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x001FE0E2 File Offset: 0x001FC2E2
		private void VisitGroupNode()
		{
			this.currentLevel++;
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x001FE0F4 File Offset: 0x001FC2F4
		private void ExitGroupNode(QueryFilterBuilder.CreateGroupQueryFilterDelegate callback)
		{
			List<QueryFilter> currentLevelFilters = this.CurrentLevelFilters;
			if (currentLevelFilters.Count > 0)
			{
				if (callback(currentLevelFilters) == null)
				{
					this.isQueryFilterValid = false;
				}
				else
				{
					this.ParentLevelFilters.Add(callback(currentLevelFilters));
				}
				currentLevelFilters.Clear();
			}
			else
			{
				this.isQueryFilterValid = false;
			}
			this.currentLevel--;
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x001FE154 File Offset: 0x001FC354
		private static QueryFilter BuildStringFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (PropertyDefinition property in properties)
			{
				list.Add(new TextFilter(property, value, options, MatchFlags.Loose));
			}
			if (list.Count != 1)
			{
				return new OrFilter(list.ToArray());
			}
			return list[0];
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x001FE1C8 File Offset: 0x001FC3C8
		private static QueryFilter BuildComparisonFilter(IEnumerable<PropertyDefinition> properties, ComparisonOperator comparisonOperator, object value)
		{
			object obj = value;
			if (obj is DateTime)
			{
				obj = new ExDateTime(ExTimeZone.UtcTimeZone, ((DateTime)value).ToUniversalTime());
			}
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (PropertyDefinition property in properties)
			{
				list.Add(new ComparisonFilter(comparisonOperator, property, obj));
			}
			if (list.Count != 1)
			{
				return new OrFilter(list.ToArray());
			}
			return list[0];
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x001FE268 File Offset: 0x001FC468
		private QueryFilter BuildExistsFilter(IEnumerable<PropertyDefinition> properties)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (PropertyDefinition property in properties)
			{
				list.Add(new ExistsFilter(property));
			}
			if (list.Count != 1)
			{
				return new OrFilter(list.ToArray());
			}
			return list[0];
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x001FE2D8 File Offset: 0x001FC4D8
		private static QueryFilter BuildRecipientFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			IEnumerable<PropertyDefinition> modifiedRecipientPropertiesForEDiscovery = QueryFilterBuilder.GetModifiedRecipientPropertiesForEDiscovery(builder.parseOptions, properties);
			list.Add(QueryFilterBuilder.BuildStringFilter(builder, modifiedRecipientPropertiesForEDiscovery, value, options));
			if ((builder.parseOptions & KqlParser.ParseOption.QueryPreserving) == KqlParser.ParseOption.None && builder.recipientResolver != null)
			{
				foreach (string text in builder.recipientResolver.Resolve(value))
				{
					if (!text.Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						list.Add(QueryFilterBuilder.BuildStringFilter(builder, modifiedRecipientPropertiesForEDiscovery, text, options));
					}
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x06007303 RID: 29443 RVA: 0x001FE374 File Offset: 0x001FC574
		private static IEnumerable<PropertyDefinition> GetModifiedRecipientPropertiesForEDiscovery(KqlParser.ParseOption parseOption, IEnumerable<PropertyDefinition> properties)
		{
			if ((parseOption & KqlParser.ParseOption.EDiscoveryMode) == KqlParser.ParseOption.None)
			{
				return properties;
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			List<PropertyDefinition> list2 = new List<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				list.Add(propertyDefinition);
				if (propertyDefinition == ItemSchema.SearchRecipientsTo)
				{
					list2.Add(MessageItemSchema.ToGroupExpansionRecipients);
				}
				else if (propertyDefinition == ItemSchema.SearchRecipientsCc)
				{
					list2.Add(MessageItemSchema.CcGroupExpansionRecipients);
				}
				else if (propertyDefinition == ItemSchema.SearchRecipientsBcc)
				{
					list2.Add(MessageItemSchema.BccGroupExpansionRecipients);
				}
				else if (propertyDefinition == ItemSchema.SearchRecipients)
				{
					list2.Add(MessageItemSchema.ToGroupExpansionRecipients);
					list2.Add(MessageItemSchema.CcGroupExpansionRecipients);
					list2.Add(MessageItemSchema.BccGroupExpansionRecipients);
				}
			}
			if (list2.Count > 0)
			{
				list.AddRange(list2);
			}
			return list;
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x001FE44C File Offset: 0x001FC64C
		private static QueryFilter BuildAttachmentFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			QueryFilter queryFilter = QueryFilterBuilder.BuildStringFilter(builder, properties, value, options);
			if ((builder.parseOptions & KqlParser.ParseOption.ContentIndexingDisabled) != KqlParser.ParseOption.None)
			{
				return new OrFilter(new QueryFilter[]
				{
					queryFilter,
					new SubFilter(SubFilterProperties.Attachments, QueryFilterBuilder.BuildStringFilter(builder, Globals.AttachmentSubMapping, value, options))
				});
			}
			return queryFilter;
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x001FE498 File Offset: 0x001FC698
		private static QueryFilter BuildKindFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			KindKeyword kindKeyword;
			if (!builder.keywordMapping.TryGetKindKeyword(value, out kindKeyword))
			{
				return null;
			}
			if ((builder.parseOptions & KqlParser.ParseOption.QueryPreserving) != KqlParser.ParseOption.None)
			{
				return QueryFilterBuilder.BuildStringFilter(builder, properties, kindKeyword.ToString(), MatchOptions.FullString);
			}
			List<QueryFilter> list = new List<QueryFilter>();
			string[] array = Globals.KindKeywordToClassMap[kindKeyword];
			foreach (string value2 in array)
			{
				list.Add(QueryFilterBuilder.BuildStringFilter(builder, properties, value2, ((builder.parseOptions & KqlParser.ParseOption.ContentIndexingDisabled) == KqlParser.ParseOption.None) ? MatchOptions.PrefixOnWords : MatchOptions.SubString));
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x001FE540 File Offset: 0x001FC740
		private static QueryFilter BuildImportanceFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			Importance importance;
			if (!builder.keywordMapping.TryGetImportance(value, out importance))
			{
				return null;
			}
			if ((builder.parseOptions & KqlParser.ParseOption.QueryPreserving) != KqlParser.ParseOption.None)
			{
				return QueryFilterBuilder.BuildStringFilter(builder, properties, value, MatchOptions.FullString);
			}
			return QueryFilterBuilder.BuildComparisonFilter(properties, ComparisonOperator.Equal, (int)importance);
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x001FE584 File Offset: 0x001FC784
		private static QueryFilter BuildBooleanFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			int num;
			if (int.TryParse(value, out num) && (num == 0 || num == 1))
			{
				return QueryFilterBuilder.BuildComparisonFilter(properties, ComparisonOperator.Equal, num);
			}
			return null;
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x001FE5B4 File Offset: 0x001FC7B4
		private static QueryFilter BuildIsFlaggedFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			int num;
			if (!int.TryParse(value, out num) || (num != 0 && num != 1))
			{
				return null;
			}
			if (num == 1)
			{
				return QueryFilterBuilder.BuildComparisonFilter(properties, ComparisonOperator.Equal, FlagStatus.Flagged);
			}
			return new OrFilter(new QueryFilter[]
			{
				QueryFilterBuilder.BuildComparisonFilter(properties, ComparisonOperator.Equal, FlagStatus.NotFlagged),
				new NotFilter(builder.BuildExistsFilter(properties))
			});
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x001FE648 File Offset: 0x001FC848
		private static QueryFilter BuildPolicyTagFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			Guid guid;
			if (Guid.TryParse(value, out guid))
			{
				list.Add(QueryFilterBuilder.BuildComparisonFilter(properties, ComparisonOperator.Equal, guid.ToByteArray()));
			}
			if (builder.policyTagProvider != null)
			{
				IEnumerable<PolicyTag> enumerable = null;
				PolicyTag[] policyTags = builder.policyTagProvider.PolicyTags;
				if (policyTags != null)
				{
					if (options == MatchOptions.PrefixOnWords || options == MatchOptions.SubString)
					{
						enumerable = from x in policyTags
						where x.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0
						select x;
					}
					else
					{
						enumerable = from x in policyTags
						where x.Name.Equals(value, StringComparison.OrdinalIgnoreCase)
						select x;
					}
				}
				foreach (PolicyTag policyTag in enumerable)
				{
					list.Add(QueryFilterBuilder.BuildComparisonFilter(properties, ComparisonOperator.Equal, policyTag.PolicyGuid.ToByteArray()));
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x001FE770 File Offset: 0x001FC970
		private static QueryFilter BuildAllFilter(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options)
		{
			List<PropertyKeyword> list = new List<PropertyKeyword>();
			switch (builder.rescopedAll)
			{
			case RescopedAll.Default:
				return QueryFilterBuilder.BuildStringFilter(builder, properties, value, options);
			case RescopedAll.Subject:
				list.Add(PropertyKeyword.Subject);
				break;
			case RescopedAll.Body:
				list.Add(PropertyKeyword.Body);
				break;
			case RescopedAll.BodyAndSubject:
				list.Add(PropertyKeyword.Subject);
				list.Add(PropertyKeyword.Body);
				break;
			case RescopedAll.From:
				list.Add(PropertyKeyword.From);
				break;
			case RescopedAll.Participants:
				list.Add(PropertyKeyword.Participants);
				break;
			default:
				throw new ArgumentException("RescopedAll");
			}
			List<QueryFilter> list2 = new List<QueryFilter>();
			foreach (PropertyKeyword key in list)
			{
				QueryFilter queryFilter = QueryFilterBuilder.stringNodeBuilderMap[key](builder, Globals.PropertyKeywordToDefinitionMap[key], value, options);
				if (queryFilter == null)
				{
					builder.isQueryFilterValid = false;
					return null;
				}
				list2.Add(queryFilter);
			}
			if (list2.Count == 1)
			{
				return list2[0];
			}
			return new OrFilter(list2.ToArray());
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x001FE88C File Offset: 0x001FCA8C
		private bool TryGetCurrentPropertyKeyword(out PropertyKeyword propertyKeyword)
		{
			if (string.IsNullOrEmpty(this.currentScope))
			{
				propertyKeyword = PropertyKeyword.All;
				return true;
			}
			if (!this.keywordMapping.TryGetPropertyKeyword(this.currentScope, out propertyKeyword))
			{
				return false;
			}
			if (!this.AllowedKeywords.Contains(propertyKeyword))
			{
				propertyKeyword = PropertyKeyword.All;
				return false;
			}
			return true;
		}

		// Token: 0x04005048 RID: 20552
		private static readonly Dictionary<PropertyKeyword, QueryFilterBuilder.CreateTextQueryFilterDelegate> stringNodeBuilderMap = new Dictionary<PropertyKeyword, QueryFilterBuilder.CreateTextQueryFilterDelegate>
		{
			{
				PropertyKeyword.From,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildRecipientFilter)
			},
			{
				PropertyKeyword.To,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildRecipientFilter)
			},
			{
				PropertyKeyword.Cc,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildRecipientFilter)
			},
			{
				PropertyKeyword.Bcc,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildRecipientFilter)
			},
			{
				PropertyKeyword.Participants,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildRecipientFilter)
			},
			{
				PropertyKeyword.Recipients,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildRecipientFilter)
			},
			{
				PropertyKeyword.Attachment,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildAttachmentFilter)
			},
			{
				PropertyKeyword.AttachmentNames,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildStringFilter)
			},
			{
				PropertyKeyword.Kind,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildKindFilter)
			},
			{
				PropertyKeyword.Importance,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildImportanceFilter)
			},
			{
				PropertyKeyword.IsFlagged,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildIsFlaggedFilter)
			},
			{
				PropertyKeyword.PolicyTag,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildPolicyTagFilter)
			},
			{
				PropertyKeyword.All,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildAllFilter)
			},
			{
				PropertyKeyword.HasAttachment,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildBooleanFilter)
			},
			{
				PropertyKeyword.IsRead,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildBooleanFilter)
			},
			{
				PropertyKeyword.Subject,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildStringFilter)
			},
			{
				PropertyKeyword.Body,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildStringFilter)
			},
			{
				PropertyKeyword.Category,
				new QueryFilterBuilder.CreateTextQueryFilterDelegate(QueryFilterBuilder.BuildStringFilter)
			}
		};

		// Token: 0x04005049 RID: 20553
		private readonly IRecipientResolver recipientResolver;

		// Token: 0x0400504A RID: 20554
		private readonly IPolicyTagProvider policyTagProvider;

		// Token: 0x0400504B RID: 20555
		private readonly KqlParser.ParseOption parseOptions;

		// Token: 0x0400504C RID: 20556
		private readonly RescopedAll rescopedAll;

		// Token: 0x0400504D RID: 20557
		private readonly Dictionary<int, List<QueryFilter>> queryFilters = new Dictionary<int, List<QueryFilter>>();

		// Token: 0x0400504E RID: 20558
		private readonly LocalizedKeywordMapping keywordMapping;

		// Token: 0x0400504F RID: 20559
		private readonly CultureInfo culture;

		// Token: 0x04005050 RID: 20560
		private int currentLevel;

		// Token: 0x04005051 RID: 20561
		private string currentScope;

		// Token: 0x04005052 RID: 20562
		private bool isQueryFilterValid;

		// Token: 0x02000D0C RID: 3340
		// (Invoke) Token: 0x06007311 RID: 29457
		private delegate QueryFilter CreateGroupQueryFilterDelegate(List<QueryFilter> currentFilters);

		// Token: 0x02000D0D RID: 3341
		// (Invoke) Token: 0x06007315 RID: 29461
		private delegate QueryFilter CreateTextQueryFilterDelegate(QueryFilterBuilder builder, IEnumerable<PropertyDefinition> properties, string value, MatchOptions options);
	}
}

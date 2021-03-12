using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.Kes;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001D9 RID: 473
	internal class KesSpamCommon : HygieneSession
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0003B59C File Offset: 0x0003979C
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0003B5A4 File Offset: 0x000397A4
		public IConfigDataProvider DataProvider { get; protected set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0003B5AD File Offset: 0x000397AD
		internal Guid NewID
		{
			get
			{
				return CombGuidGenerator.NewGuid();
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0003B5B4 File Offset: 0x000397B4
		internal ComparisonFilter BuildVersionParam
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, DalHelper.BuildVersionProp, "15.00.1497.010");
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003B610 File Offset: 0x00039810
		internal void Save(IEnumerable<ReputationListSettings> items)
		{
			if (items != null && items.Any<ReputationListSettings>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<ReputationListSettings>>().ForAll(delegate(IEnumerable<ReputationListSettings> batch)
				{
					foreach (ReputationListSettings configurable in batch)
					{
						this.Save(configurable);
					}
				});
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003B6BC File Offset: 0x000398BC
		public void Save(IEnumerable<SpamDataBlob> items, bool saveToPrimaryOnly = false)
		{
			if (items != null && items.Any<SpamDataBlob>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<SpamDataBlob>>().ForAll(delegate(IEnumerable<SpamDataBlob> batch)
				{
					foreach (SpamDataBlob spamDataBlob in batch)
					{
						spamDataBlob[DataBlobCommonSchema.PrimaryOnlyProperty] = saveToPrimaryOnly;
						this.Save(spamDataBlob);
					}
				});
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003B758 File Offset: 0x00039958
		internal void Save(IEnumerable<SpamExclusionData> items)
		{
			if (items != null && items.Any<SpamExclusionData>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<SpamExclusionData>>().ForAll(delegate(IEnumerable<SpamExclusionData> batch)
				{
					foreach (SpamExclusionData configurable in batch)
					{
						this.Save(configurable);
					}
				});
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003B7A8 File Offset: 0x000399A8
		internal void Save(IEnumerable<SyncWatermark> items)
		{
			if (items != null && items.Any<SyncWatermark>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<SyncWatermark>>().ForAll(delegate(IEnumerable<SyncWatermark> batch)
				{
					this.Save(new SyncWatermarkBatch(batch));
				});
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0003B7F8 File Offset: 0x000399F8
		internal void Save(IEnumerable<RuleBase> items)
		{
			if (items != null && items.Any<RuleBase>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<RuleBase>>().ForAll(delegate(IEnumerable<RuleBase> batch)
				{
					this.Save(new RuleBaseBatch(batch));
				});
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0003B848 File Offset: 0x00039A48
		internal void Save(IEnumerable<RuleUpdate> items)
		{
			if (items != null && items.Any<RuleUpdate>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<RuleUpdate>>().ForAll(delegate(IEnumerable<RuleUpdate> batch)
				{
					this.Save(new RuleUpdateBatch(batch));
				});
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0003B898 File Offset: 0x00039A98
		internal void Save(IEnumerable<Processor> items)
		{
			if (items != null && items.Any<Processor>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<Processor>>().ForAll(delegate(IEnumerable<Processor> batch)
				{
					this.Save(new ProcessorBatch(batch));
				});
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0003B8E8 File Offset: 0x00039AE8
		internal void Save(IEnumerable<RulePredicate> items)
		{
			if (items != null && items.Any<RulePredicate>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<RulePredicate>>().ForAll(delegate(IEnumerable<RulePredicate> batch)
				{
					this.Save(new RulePredicateBatch(batch));
				});
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0003B938 File Offset: 0x00039B38
		internal void Save(IEnumerable<PredicateExtendedProperty> items)
		{
			if (items != null && items.Any<PredicateExtendedProperty>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<PredicateExtendedProperty>>().ForAll(delegate(IEnumerable<PredicateExtendedProperty> batch)
				{
					this.Save(new PredicateExtendedPropertyBatch(batch));
				});
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003B988 File Offset: 0x00039B88
		internal void Save(IEnumerable<RuleExtendedProperty> items)
		{
			if (items != null && items.Any<RuleExtendedProperty>())
			{
				items.BatchSplit(1000).AsParallel<IEnumerable<RuleExtendedProperty>>().ForAll(delegate(IEnumerable<RuleExtendedProperty> batch)
				{
					this.Save(new RuleExtendedPropertyBatch(batch));
				});
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0003B9C8 File Offset: 0x00039BC8
		internal void Save(IConfigurable configurable)
		{
			this.ApplyAuditProperties(configurable);
			this.DataProvider.Save(configurable);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0003B9DD File Offset: 0x00039BDD
		internal void Delete(IConfigurable configurable)
		{
			this.ApplyAuditProperties(configurable);
			this.DataProvider.Delete(configurable);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0003B9F2 File Offset: 0x00039BF2
		internal Guid SaveSpamRule(SpamRule rule, ResultData result)
		{
			if (rule.ID == Guid.Empty)
			{
				rule.ID = this.NewID;
			}
			this.Save(rule);
			this.Save(this.GenerateRuleExtendedPropertiesFromResultData(rule.ID, result));
			return rule.ID;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0003BA32 File Offset: 0x00039C32
		internal Guid SaveRulePredicate(RulePredicate rulePredicate)
		{
			if (rulePredicate.PredicateID == Guid.Empty)
			{
				rulePredicate.PredicateID = this.NewID;
			}
			this.Save(rulePredicate);
			return rulePredicate.PredicateID;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
		internal IEnumerable<RuleExtendedProperty> GenerateRuleExtendedPropertiesFromResultData(Guid id, ResultData result)
		{
			switch (result.ResultType)
			{
			case ResultType.AddScore:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "AddScore",
					IntValue = result.Score
				};
				break;
			case ResultType.MarkAsSpam:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "MarkAsSpam",
					BoolValue = new bool?(true)
				};
				break;
			case ResultType.SkipFiltering:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "SkipFiltering",
					BoolValue = new bool?(true)
				};
				if (result.Score != null)
				{
					yield return new RuleExtendedProperty
					{
						ID = id,
						PropertyName = "SkipFilteringPercentage",
						IntValue = new int?(result.Score.Value)
					};
				}
				break;
			case ResultType.AddFeatureScore:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "AddFeatureScore",
					IntValue = result.Score
				};
				break;
			case ResultType.Reject:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "Reject",
					StringValue = result.ResponseString
				};
				break;
			case ResultType.MarkAsHighRisk:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "MarkAsHighRisk",
					BoolValue = new bool?(true)
				};
				break;
			case ResultType.SetPartition:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "SetPartition",
					IntValue = result.Score
				};
				break;
			case ResultType.SilentDrop:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "SilentDrop",
					BoolValue = new bool?(true)
				};
				break;
			case ResultType.MarkAsBulk:
				yield return new RuleExtendedProperty
				{
					ID = id,
					PropertyName = "MarkAsBulk",
					BoolValue = new bool?(false)
				};
				break;
			default:
				throw new ArgumentException("Invalid result type");
			}
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "ResultData",
				BoolValue = new bool?(true)
			};
			yield break;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003BFFC File Offset: 0x0003A1FC
		internal ResultData GenerateResultDataFromRuleExtendedProperties(ILookup<string, RuleExtendedProperty> properties)
		{
			if (properties.Contains("ResultData"))
			{
				if (properties.Contains("AddScore"))
				{
					return new ResultData
					{
						ResultType = ResultType.AddScore,
						Score = properties["AddScore"].First<RuleExtendedProperty>().IntValue
					};
				}
				if (properties.Contains("AddFeatureScore"))
				{
					return new ResultData
					{
						ResultType = ResultType.AddFeatureScore,
						Score = properties["AddFeatureScore"].First<RuleExtendedProperty>().IntValue
					};
				}
				if (properties.Contains("MarkAsHighRisk"))
				{
					return new ResultData
					{
						ResultType = ResultType.MarkAsHighRisk
					};
				}
				if (properties.Contains("MarkAsSpam"))
				{
					return new ResultData
					{
						ResultType = ResultType.MarkAsSpam
					};
				}
				if (properties.Contains("SkipFiltering"))
				{
					int? score = null;
					if (properties.Contains("SkipFilteringPercentage"))
					{
						score = properties["SkipFilteringPercentage"].First<RuleExtendedProperty>().IntValue;
					}
					return new ResultData
					{
						ResultType = ResultType.SkipFiltering,
						Score = score
					};
				}
				if (properties.Contains("Reject"))
				{
					return new ResultData
					{
						ResultType = ResultType.Reject,
						ResponseString = properties["Reject"].First<RuleExtendedProperty>().StringValue
					};
				}
				if (properties.Contains("SetPartition"))
				{
					return new ResultData
					{
						ResultType = ResultType.SetPartition,
						Score = properties["SetPartition"].First<RuleExtendedProperty>().IntValue
					};
				}
				if (properties.Contains("SilentDrop"))
				{
					return new ResultData
					{
						ResultType = ResultType.SilentDrop
					};
				}
				if (properties.Contains("MarkAsBulk"))
				{
					return new ResultData
					{
						ResultType = ResultType.MarkAsBulk
					};
				}
			}
			return null;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003C5B0 File Offset: 0x0003A7B0
		internal IEnumerable<RuleExtendedProperty> GenerateRuleExtendedPropertiesFromAuthoringData(Guid id, AuthoringData authoringData)
		{
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringData",
				BoolValue = new bool?(true)
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataGroupId",
				LongValue = new long?(authoringData.GroupID)
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataRuleTarget",
				IntValue = new int?((int)authoringData.RuleTarget)
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataRegex",
				StringValue = authoringData.Regex
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataLanguageId",
				IntValue = new int?((int)authoringData.LanguageID)
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataCategory",
				IntValue = new int?((int)authoringData.Category)
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataFlags",
				StringValue = authoringData.Flags
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				PropertyName = "AuthoringDataActionId",
				IntValue = new int?((int)authoringData.ActionID)
			};
			yield break;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003C5DC File Offset: 0x0003A7DC
		internal AuthoringData GenerateAuthoringDataFromRuleExtendedProperties(ILookup<string, RuleExtendedProperty> properties)
		{
			if (properties.Contains("AuthoringData"))
			{
				AuthoringData authoringData = new AuthoringData();
				if (properties.Contains("AuthoringDataGroupId"))
				{
					authoringData.GroupID = properties["AuthoringDataGroupId"].First<RuleExtendedProperty>().LongValue.Value;
				}
				if (properties.Contains("AuthoringDataRuleTarget"))
				{
					authoringData.RuleTarget = (byte)properties["AuthoringDataRuleTarget"].First<RuleExtendedProperty>().IntValue.Value;
				}
				if (properties.Contains("AuthoringDataRegex"))
				{
					authoringData.Regex = properties["AuthoringDataRegex"].First<RuleExtendedProperty>().StringValue;
				}
				if (properties.Contains("AuthoringDataLanguageId"))
				{
					authoringData.LanguageID = (byte)properties["AuthoringDataLanguageId"].First<RuleExtendedProperty>().IntValue.Value;
				}
				if (properties.Contains("AuthoringDataCategory"))
				{
					authoringData.Category = (byte)properties["AuthoringDataCategory"].First<RuleExtendedProperty>().IntValue.Value;
				}
				if (properties.Contains("AuthoringDataFlags"))
				{
					authoringData.Flags = properties["AuthoringDataFlags"].First<RuleExtendedProperty>().StringValue;
				}
				if (properties.Contains("AuthoringDataActionId"))
				{
					authoringData.ActionID = (byte)properties["AuthoringDataActionId"].First<RuleExtendedProperty>().IntValue.Value;
				}
				return authoringData;
			}
			return null;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0003C748 File Offset: 0x0003A948
		internal RuleBase GenerateRuleBaseFromRuleData(Guid id, RuleType ruleType, RuleDataBase rule)
		{
			return new RuleBase
			{
				ID = id,
				RuleType = new byte?((byte)ruleType),
				RuleID = new long?(rule.RuleID),
				ScopeID = new byte?((byte)rule.ScopeID),
				Sequence = new decimal?(rule.Sequence),
				IsActive = new bool?(rule.IsActive),
				IsPersistent = new bool?(rule.IsPersistent),
				GroupID = new long?(rule.GroupID),
				State = new byte?((byte)(rule.RuleStatus ?? RuleStatusType.PreApproved)),
				AddedVersion = rule.AddedVersion,
				RemovedVersion = rule.RemovedVersion
			};
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		internal IEnumerable<RuleExtendedProperty> GenerateRuleExtendedPropertiesFromRuleData(Guid id, RuleDataBase rule)
		{
			if (!string.IsNullOrEmpty(rule.Comment))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.Comment,
					PropertyName = "Comment"
				};
			}
			if (!string.IsNullOrEmpty(rule.ApprovedBy))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.ApprovedBy,
					PropertyName = "ApprovedBy"
				};
			}
			if (!string.IsNullOrEmpty(rule.ModifiedBy))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.ModifiedBy,
					PropertyName = "ModifiedBy"
				};
			}
			if (!string.IsNullOrEmpty(rule.DeletedBy))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.DeletedBy,
					PropertyName = "DeletedBy"
				};
			}
			if (rule.ApprovalStatusID != null)
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					IntValue = new int?(rule.ApprovalStatusID.Value),
					PropertyName = "ApprovalStatusId"
				};
			}
			if (rule.ApprovedDate != null)
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					DatetimeValue = new DateTime?(rule.ApprovedDate.Value),
					PropertyName = "ApprovedDate"
				};
			}
			yield break;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0003CE0C File Offset: 0x0003B00C
		internal IEnumerable<RuleExtendedProperty> GenerateRuleExtendedPropertiesFromSpamRuleData(Guid id, SpamRuleData rule)
		{
			if (rule.AsfID != null)
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					IntValue = new int?(rule.AsfID.Value),
					PropertyName = "AsfId"
				};
			}
			if (!string.IsNullOrEmpty(rule.ConditionMatchPhrase))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.ConditionMatchPhrase,
					PropertyName = "ConditionMatchPhrase"
				};
			}
			if (!string.IsNullOrEmpty(rule.ConditionNotMatchPhrase))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.ConditionNotMatchPhrase,
					PropertyName = "ConditionNotMatchPhrase"
				};
			}
			yield break;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0003D0A4 File Offset: 0x0003B2A4
		internal IEnumerable<RuleExtendedProperty> GenerateRuleExtendedPropertiesFromURIRuleData(Guid id, URIRuleData rule)
		{
			if (!string.IsNullOrEmpty(rule.Uri))
			{
				yield return new RuleExtendedProperty
				{
					ID = id,
					StringValue = rule.Uri,
					PropertyName = "Uri"
				};
			}
			yield return new RuleExtendedProperty
			{
				ID = id,
				IntValue = new int?(rule.UriType),
				PropertyName = "UriTypeId"
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				IntValue = new int?(rule.Score),
				PropertyName = "Score"
			};
			yield return new RuleExtendedProperty
			{
				ID = id,
				BoolValue = new bool?(rule.Overridable),
				PropertyName = "Overridable"
			};
			yield break;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003D0D0 File Offset: 0x0003B2D0
		internal void GenerateRuleDataFromRuleExtendedProperties(RuleDataBase ruleData, ILookup<string, RuleExtendedProperty> properties)
		{
			if (properties.Contains("Comment"))
			{
				ruleData.Comment = properties["Comment"].First<RuleExtendedProperty>().StringValue;
			}
			if (properties.Contains("ApprovedBy"))
			{
				ruleData.ApprovedBy = properties["ApprovedBy"].First<RuleExtendedProperty>().StringValue;
			}
			if (properties.Contains("ModifiedBy"))
			{
				ruleData.ModifiedBy = properties["ModifiedBy"].First<RuleExtendedProperty>().StringValue;
			}
			if (properties.Contains("DeletedBy"))
			{
				ruleData.DeletedBy = properties["DeletedBy"].First<RuleExtendedProperty>().StringValue;
			}
			if (properties.Contains("ApprovalStatusId"))
			{
				ruleData.ApprovalStatusID = properties["ApprovalStatusId"].First<RuleExtendedProperty>().IntValue;
			}
			if (properties.Contains("ApprovedDate"))
			{
				ruleData.ApprovedDate = properties["ApprovedDate"].First<RuleExtendedProperty>().DatetimeValue;
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		internal void GenerateSpamRuleDataFromRuleExtendedProperties(SpamRuleData ruleData, ILookup<string, RuleExtendedProperty> properties)
		{
			if (properties.Contains("AsfId"))
			{
				ruleData.AsfID = properties["AsfId"].First<RuleExtendedProperty>().IntValue;
			}
			if (properties.Contains("ConditionMatchPhrase"))
			{
				ruleData.ConditionMatchPhrase = properties["ConditionMatchPhrase"].First<RuleExtendedProperty>().StringValue;
			}
			if (properties.Contains("ConditionNotMatchPhrase"))
			{
				ruleData.ConditionNotMatchPhrase = properties["ConditionNotMatchPhrase"].First<RuleExtendedProperty>().StringValue;
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0003D258 File Offset: 0x0003B458
		internal void GenerateURIRuleDataFromRuleExtendedProperties(URIRuleData ruleData, ILookup<string, RuleExtendedProperty> properties)
		{
			if (properties.Contains("Uri"))
			{
				ruleData.Uri = properties["Uri"].First<RuleExtendedProperty>().StringValue;
			}
			if (properties.Contains("UriTypeId"))
			{
				ruleData.UriType = properties["UriTypeId"].First<RuleExtendedProperty>().IntValue.Value;
			}
			if (properties.Contains("Score"))
			{
				ruleData.Score = properties["Score"].First<RuleExtendedProperty>().IntValue.Value;
			}
			if (properties.Contains("Overridable"))
			{
				ruleData.Overridable = properties["Overridable"].First<RuleExtendedProperty>().BoolValue.Value;
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0003D320 File Offset: 0x0003B520
		internal void GenerateRuleDataFromRuleBase(RuleDataBase ruleData, RuleBase rule)
		{
			ruleData.RuleID = rule.RuleID.Value;
			ruleData.ScopeID = (RuleScopeType)rule.ScopeID.Value;
			ruleData.Sequence = rule.Sequence.Value;
			ruleData.IsActive = rule.IsActive.Value;
			ruleData.IsPersistent = rule.IsPersistent.Value;
			ruleData.GroupID = rule.GroupID.Value;
			byte? state = rule.State;
			ruleData.RuleStatus = ((state != null) ? new RuleStatusType?((RuleStatusType)state.GetValueOrDefault()) : null);
			ruleData.AddedVersion = rule.AddedVersion;
			ruleData.RemovedVersion = rule.RemovedVersion;
			ruleData.CreatedDatetime = rule.CreatedDatetime;
			ruleData.ChangeDatetime = rule.ChangeDatetime;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0003DB90 File Offset: 0x0003BD90
		internal IEnumerable<Processor> GenerateProcessorsFromProcessorData(ProcessorData processor)
		{
			switch (processor.ProcessorType)
			{
			case ProcessorType.SpfCheck:
			case ProcessorType.SmartScreen:
			case ProcessorType.SenderIDCheck:
			case ProcessorType.MXLookup:
			case ProcessorType.ALookup:
			case ProcessorType.DkimKeyLookup:
			case ProcessorType.PtrLookup:
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "ExpectedResult",
					IntValue = processor.ExpectedResult
				};
				break;
			case ProcessorType.Keywords:
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Keywords",
					StringValue = string.Join("\n", processor.Keywords)
				};
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "WordBoundary",
					StringValue = processor.WordBoundary
				};
				break;
			case ProcessorType.RegEx:
			{
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Value",
					StringValue = processor.Value
				};
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "PreconditionPredicateId",
					LongValue = processor.Precondition
				};
				Processor processor2 = new Processor();
				processor2.ProcessorID = new long?(processor.ProcessorID);
				processor2.PropertyName = "CaseSensitivityMode";
				ConfigurablePropertyTable configurablePropertyTable = processor2;
				byte? caseSensitivityType = processor.CaseSensitivityType;
				configurablePropertyTable.IntValue = ((caseSensitivityType != null) ? new int?((int)caseSensitivityType.GetValueOrDefault()) : null);
				yield return processor2;
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "CachingEnabled",
					BoolValue = processor.CachingEnabled
				};
				break;
			}
			case ProcessorType.BackscatterCheck:
			case ProcessorType.UriScan:
			case ProcessorType.DirectoryBasedCheck:
			case ProcessorType.AsyncProcessor:
			case ProcessorType.CountryCheck:
			case ProcessorType.LanguageCheck:
			case ProcessorType.DkimVerifier:
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Value",
					BoolValue = new bool?(true)
				};
				break;
			case ProcessorType.SimilarityFingerprint:
			case ProcessorType.ContainmentFingerprint:
			{
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Value",
					StringValue = processor.Value
				};
				Processor processor3 = new Processor();
				processor3.ProcessorID = new long?(processor.ProcessorID);
				processor3.PropertyName = "Coefficient";
				ConfigurablePropertyTable configurablePropertyTable2 = processor3;
				double? coefficient = processor.Coefficient;
				configurablePropertyTable2.DecimalValue = ((coefficient != null) ? new decimal?((decimal)coefficient.GetValueOrDefault()) : null);
				yield return processor3;
				break;
			}
			case ProcessorType.RegexTextTarget:
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Value",
					StringValue = processor.Value
				};
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Target",
					StringValue = processor.Target
				};
				break;
			case ProcessorType.ConcatTextTarget:
				yield return new Processor
				{
					ProcessorID = new long?(processor.ProcessorID),
					PropertyName = "Target",
					StringValue = processor.Target
				};
				break;
			default:
				throw new ArgumentException("Invalid processor type");
			}
			yield return new Processor
			{
				ProcessorID = new long?(processor.ProcessorID),
				PropertyName = "ProcessorType",
				IntValue = new int?((int)processor.ProcessorType)
			};
			yield break;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		internal ProcessorData GenerateProcessorDataFromProcessors(ILookup<string, Processor> processors)
		{
			ProcessorData processorData = new ProcessorData
			{
				ProcessorID = processors["ProcessorType"].First<Processor>().ProcessorID.Value,
				ProcessorType = (ProcessorType)processors["ProcessorType"].First<Processor>().IntValue.Value
			};
			switch (processorData.ProcessorType)
			{
			case ProcessorType.SpfCheck:
			case ProcessorType.SmartScreen:
			case ProcessorType.SenderIDCheck:
			case ProcessorType.MXLookup:
			case ProcessorType.ALookup:
			case ProcessorType.DkimKeyLookup:
			case ProcessorType.PtrLookup:
				if (processors.Contains("ExpectedResult"))
				{
					processorData.ExpectedResult = processors["ExpectedResult"].First<Processor>().IntValue;
				}
				break;
			case ProcessorType.Keywords:
				if (processors.Contains("Keywords"))
				{
					processorData.Keywords = processors["Keywords"].First<Processor>().StringValue.Replace("\r", string.Empty).Split(new char[]
					{
						'\n'
					}, StringSplitOptions.RemoveEmptyEntries);
				}
				if (processors.Contains("WordBoundary"))
				{
					processorData.WordBoundary = processors["WordBoundary"].First<Processor>().StringValue;
				}
				break;
			case ProcessorType.RegEx:
				if (processors.Contains("Value"))
				{
					processorData.Value = processors["Value"].First<Processor>().StringValue;
				}
				if (processors.Contains("PreconditionPredicateId"))
				{
					processorData.Precondition = processors["PreconditionPredicateId"].First<Processor>().LongValue;
				}
				if (processors.Contains("CaseSensitivityMode"))
				{
					ProcessorData processorData2 = processorData;
					int? intValue = processors["CaseSensitivityMode"].First<Processor>().IntValue;
					processorData2.CaseSensitivityType = ((intValue != null) ? new byte?((byte)intValue.GetValueOrDefault()) : null);
				}
				if (processors.Contains("CachingEnabled"))
				{
					processorData.CachingEnabled = processors["CachingEnabled"].First<Processor>().BoolValue;
				}
				break;
			case ProcessorType.BackscatterCheck:
			case ProcessorType.UriScan:
			case ProcessorType.DirectoryBasedCheck:
			case ProcessorType.AsyncProcessor:
			case ProcessorType.CountryCheck:
			case ProcessorType.LanguageCheck:
			case ProcessorType.DkimVerifier:
				break;
			case ProcessorType.SimilarityFingerprint:
			case ProcessorType.ContainmentFingerprint:
				if (processors.Contains("Value"))
				{
					processorData.Value = processors["Value"].First<Processor>().StringValue;
				}
				if (processors.Contains("Coefficient"))
				{
					ProcessorData processorData3 = processorData;
					decimal? decimalValue = processors["Coefficient"].First<Processor>().DecimalValue;
					processorData3.Coefficient = ((decimalValue != null) ? new double?((double)decimalValue.GetValueOrDefault()) : null);
				}
				break;
			case ProcessorType.RegexTextTarget:
				if (processors.Contains("Value"))
				{
					processorData.Value = processors["Value"].First<Processor>().StringValue;
				}
				if (processors.Contains("Target"))
				{
					processorData.Target = processors["Target"].First<Processor>().StringValue;
				}
				break;
			case ProcessorType.ConcatTextTarget:
				if (processors.Contains("Target"))
				{
					processorData.Target = processors["Target"].First<Processor>().StringValue;
				}
				break;
			default:
				throw new ArgumentException("Invalid processor type");
			}
			return processorData;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0003DEF8 File Offset: 0x0003C0F8
		internal void GeneratePredicatesFromPredicateData(Guid id, PredicateData predicate, Guid? parentId, List<RulePredicate> predicates, List<PredicateExtendedProperty> predicateExtendedProperties)
		{
			Guid newID = this.NewID;
			RulePredicate rulePredicate = new RulePredicate();
			rulePredicate.ID = id;
			rulePredicate.PredicateID = newID;
			rulePredicate.ParentID = parentId;
			rulePredicate.PredicateType = new byte?((byte)predicate.PredicateType);
			RulePredicate rulePredicate2 = rulePredicate;
			int? sequence = predicate.Sequence;
			rulePredicate2.Sequence = ((sequence != null) ? new decimal?(sequence.GetValueOrDefault()) : null);
			rulePredicate.ProcessorID = predicate.ProcessorID;
			predicates.Add(rulePredicate);
			switch (predicate.PredicateType)
			{
			case PredicateType.Any:
				predicateExtendedProperties.Add(new PredicateExtendedProperty
				{
					ID = id,
					PredicateID = newID,
					IntValue = predicate.MinOccurs,
					PropertyName = "MinOccurs"
				});
				predicateExtendedProperties.Add(new PredicateExtendedProperty
				{
					ID = id,
					PredicateID = newID,
					IntValue = predicate.MaxOccurs,
					PropertyName = "MaxOccurs"
				});
				if (predicate.ChildPredicate == null)
				{
					return;
				}
				using (List<PredicateData>.Enumerator enumerator = predicate.ChildPredicate.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PredicateData predicate2 = enumerator.Current;
						this.GeneratePredicatesFromPredicateData(id, predicate2, new Guid?(newID), predicates, predicateExtendedProperties);
					}
					return;
				}
				break;
			case PredicateType.Match:
			case PredicateType.Exists:
				predicateExtendedProperties.Add(new PredicateExtendedProperty
				{
					ID = id,
					PredicateID = newID,
					StringValue = predicate.Target,
					PropertyName = "Target"
				});
				return;
			case PredicateType.FeatureMatch:
				return;
			case PredicateType.NumericMatch:
			{
				predicateExtendedProperties.Add(new PredicateExtendedProperty
				{
					ID = id,
					PredicateID = newID,
					StringValue = predicate.Target,
					PropertyName = "Target"
				});
				predicateExtendedProperties.Add(new PredicateExtendedProperty
				{
					ID = id,
					PredicateID = newID,
					LongValue = predicate.Value,
					PropertyName = "Value"
				});
				PredicateExtendedProperty predicateExtendedProperty = new PredicateExtendedProperty();
				predicateExtendedProperty.ID = id;
				predicateExtendedProperty.PredicateID = newID;
				ConfigurablePropertyTable configurablePropertyTable = predicateExtendedProperty;
				NumericOperationType? operation = predicate.Operation;
				configurablePropertyTable.IntValue = ((operation != null) ? new int?((int)operation.GetValueOrDefault()) : null);
				predicateExtendedProperty.PropertyName = "Operation";
				predicateExtendedProperties.Add(predicateExtendedProperty);
				return;
			}
			}
			throw new ArgumentException("Invalid predicate type");
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0003E62C File Offset: 0x0003C82C
		internal IEnumerable<PredicateData> GeneratePredicateDataFromPredicates(long ruleID, Guid parentId, ILookup<Guid, RulePredicate> predicates, ILookup<Guid, PredicateExtendedProperty> properties)
		{
			foreach (RulePredicate predicate in predicates[parentId])
			{
				PredicateData predicateData2 = new PredicateData();
				predicateData2.RuleID = new long?(ruleID);
				predicateData2.PredicateType = (PredicateType)predicate.PredicateType.Value;
				predicateData2.ProcessorID = predicate.ProcessorID;
				PredicateData predicateData3 = predicateData2;
				decimal? sequence = predicate.Sequence;
				predicateData3.Sequence = ((sequence != null) ? new int?((int)sequence.GetValueOrDefault()) : null);
				PredicateData predicateData = predicateData2;
				ILookup<string, PredicateExtendedProperty> predicateProperties = properties[predicate.PredicateID].ToLookup((PredicateExtendedProperty item) => item.PropertyName);
				switch (predicateData.PredicateType)
				{
				case PredicateType.Any:
					if (predicateProperties.Contains("MaxOccurs"))
					{
						predicateData.MaxOccurs = predicateProperties["MaxOccurs"].First<PredicateExtendedProperty>().IntValue;
					}
					if (predicateProperties.Contains("MinOccurs"))
					{
						predicateData.MinOccurs = predicateProperties["MinOccurs"].First<PredicateExtendedProperty>().IntValue;
					}
					predicateData.ChildPredicate = this.GeneratePredicateDataFromPredicates(ruleID, predicate.PredicateID, predicates, properties).ToList<PredicateData>();
					break;
				case PredicateType.Match:
				case PredicateType.Exists:
					if (predicateProperties.Contains("Target"))
					{
						predicateData.Target = predicateProperties["Target"].First<PredicateExtendedProperty>().StringValue;
					}
					break;
				case PredicateType.FeatureMatch:
					break;
				case PredicateType.NumericMatch:
					if (predicateProperties.Contains("Target"))
					{
						predicateData.Target = predicateProperties["Target"].First<PredicateExtendedProperty>().StringValue;
					}
					if (predicateProperties.Contains("Value"))
					{
						predicateData.Value = predicateProperties["Value"].First<PredicateExtendedProperty>().LongValue;
					}
					if (predicateProperties.Contains("Operation"))
					{
						PredicateData predicateData4 = predicateData;
						int? intValue = predicateProperties["Operation"].First<PredicateExtendedProperty>().IntValue;
						predicateData4.Operation = ((intValue != null) ? new NumericOperationType?((NumericOperationType)intValue.GetValueOrDefault()) : null);
					}
					break;
				default:
					throw new ArgumentException("Invalid predicate type");
				}
				yield return predicateData;
			}
			yield break;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0003E920 File Offset: 0x0003CB20
		internal SpamRulePackageData GenerateSpamRulePackageData(IEnumerable<SpamRule> rules)
		{
			SpamRulePackageData result;
			try
			{
				IEnumerable<SpamRuleData> source = rules.BatchSplit(1000).AsParallel<IEnumerable<SpamRule>>().SelectMany(delegate(IEnumerable<SpamRule> batch)
				{
					ILookup<Guid, RuleExtendedProperty> allExtendedProperties = this.ReadRuleDataByRulesIds<RuleExtendedProperty>(batch).ToLookup((RuleExtendedProperty key) => key.ID);
					ILookup<Guid, RulePredicate> allRulePredicates = this.ReadRuleDataByRulesIds<RulePredicate>(batch).ToLookup((RulePredicate key) => key.ID);
					ILookup<Guid, PredicateExtendedProperty> allPredicateExtendedProperties = this.ReadRuleDataByRulesIds<PredicateExtendedProperty>(batch).ToLookup((PredicateExtendedProperty key) => key.PredicateID);
					return batch.Select(delegate(SpamRule rule)
					{
						ILookup<string, RuleExtendedProperty> properties = allExtendedProperties[rule.ID].ToLookup((RuleExtendedProperty item) => item.PropertyName);
						SpamRuleData spamRuleData = new SpamRuleData();
						this.GenerateRuleDataFromRuleBase(spamRuleData, rule);
						this.GenerateRuleDataFromRuleExtendedProperties(spamRuleData, properties);
						this.GenerateSpamRuleDataFromRuleExtendedProperties(spamRuleData, properties);
						spamRuleData.Result = this.GenerateResultDataFromRuleExtendedProperties(properties);
						spamRuleData.AuthoringProperties = this.GenerateAuthoringDataFromRuleExtendedProperties(properties);
						ILookup<Guid, RulePredicate> predicates = allRulePredicates[rule.ID].ToLookup(delegate(RulePredicate key)
						{
							Guid? parentID = key.ParentID;
							if (parentID == null)
							{
								return Guid.Empty;
							}
							return parentID.GetValueOrDefault();
						});
						spamRuleData.Predicate = this.GeneratePredicateDataFromPredicates(rule.RuleID.Value, Guid.Empty, predicates, allPredicateExtendedProperties).FirstOrDefault<PredicateData>();
						return spamRuleData;
					});
				});
				IEnumerable<ProcessorData> source2 = source.SelectMany((SpamRuleData rule) => this.GetProcessorIdsFromPredicateData(rule.Predicate)).BatchSplit(1000).AsParallel<IEnumerable<long>>().SelectMany(delegate(IEnumerable<long> batch)
				{
					ILookup<long, Processor> source3 = this.ReadProcessorsByIds(batch).ToLookup((Processor key) => key.ProcessorID.Value);
					return from @group in source3
					select this.GenerateProcessorDataFromProcessors(@group.ToLookup((Processor key) => key.PropertyName));
				});
				result = new SpamRulePackageData
				{
					Rules = source.ToArray<SpamRuleData>(),
					Processors = source2.ToArray<ProcessorData>()
				};
			}
			catch (AggregateException ex)
			{
				AggregateException ex2;
				if (ex2.InnerExceptions.All((Exception ex) => ex is TransientDALException))
				{
					throw new TransientDALException(new LocalizedString("Transient exceptions thrown. See InnerExceptions for details"), ex2);
				}
				throw new PermanentDALException(new LocalizedString("At least one non retriable exception thrown. See InnerExceptions for details"), ex2);
			}
			return result;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0003EB18 File Offset: 0x0003CD18
		internal URIRulePackageData GenerateURIRulePackageData(IEnumerable<URIRule> rules)
		{
			URIRulePackageData result;
			try
			{
				IEnumerable<URIRuleData> source = rules.BatchSplit(1000).AsParallel<IEnumerable<URIRule>>().SelectMany(delegate(IEnumerable<URIRule> batch)
				{
					ILookup<Guid, RuleExtendedProperty> allExtendedProperties = this.ReadRuleDataByRulesIds<RuleExtendedProperty>(batch).ToLookup((RuleExtendedProperty key) => key.ID);
					return batch.Select(delegate(URIRule rule)
					{
						ILookup<string, RuleExtendedProperty> properties = allExtendedProperties[rule.ID].ToLookup((RuleExtendedProperty item) => item.PropertyName);
						URIRuleData uriruleData = new URIRuleData();
						this.GenerateRuleDataFromRuleBase(uriruleData, rule);
						this.GenerateRuleDataFromRuleExtendedProperties(uriruleData, properties);
						this.GenerateURIRuleDataFromRuleExtendedProperties(uriruleData, properties);
						return uriruleData;
					});
				});
				result = new URIRulePackageData
				{
					Rules = source.ToArray<URIRuleData>()
				};
			}
			catch (AggregateException ex)
			{
				AggregateException ex2;
				if (ex2.InnerExceptions.All((Exception ex) => ex is TransientDALException))
				{
					throw new TransientDALException(new LocalizedString("Transient exceptions thrown. See InnerExceptions for details"), ex2);
				}
				throw new PermanentDALException(new LocalizedString("At least one non retriable exception thrown. See InnerExceptions for details"), ex2);
			}
			return result;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0003EBC4 File Offset: 0x0003CDC4
		internal IEnumerable<SpamDataBlob> FindSpamDataBlob(SpamDataBlobDataID dataID, byte dataTypeID, int majorVersion, int minorVersion, int? chunkID = null)
		{
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.DataIDProperty, dataID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.DataTypeIDProperty, dataTypeID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.MajorVersionProperty, majorVersion),
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.MinorVersionProperty, minorVersion)
			});
			if (chunkID != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.ChunkIDProperty, chunkID)
				});
			}
			return this.DataProvider.Find<SpamDataBlob>(queryFilter, null, true, null).Cast<SpamDataBlob>();
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0003EC74 File Offset: 0x0003CE74
		internal IEnumerable<SpamDataBlobVersion> FindSpamDataBlobVersion(SpamDataBlobDataID? dataID = null, byte? dataTypeID = null, bool queryPrimaryOnly = false)
		{
			QueryFilter queryFilter = this.BuildVersionParam;
			if (dataID != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlobVersion.DataIDProperty, dataID)
				});
			}
			byte? b = dataTypeID;
			int? num = (b != null) ? new int?((int)b.GetValueOrDefault()) : null;
			if (num != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlobVersion.DataTypeIDProperty, dataTypeID)
				});
			}
			queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				queryFilter,
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.PrimaryOnlyProperty, queryPrimaryOnly)
			});
			return this.DataProvider.Find<SpamDataBlobVersion>(queryFilter, null, true, null).Cast<SpamDataBlobVersion>();
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0003F1D4 File Offset: 0x0003D3D4
		internal IEnumerable<SpamDataBlob> FindSpamDataBlobUpdates(SpamDataBlobDataID dataID, byte dataTypeID, int lastMajorVersion, int lastMinorVersion)
		{
			HygienePropertyDefinition lastMajorVersionProperty = new HygienePropertyDefinition("i_LastMajorVersion", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
			HygienePropertyDefinition lastMinorVersionProperty = new HygienePropertyDefinition("i_LastMinorVersion", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
			QueryFilter findMajorSpamDataBlobFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.DataIDProperty, dataID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.DataTypeIDProperty, dataTypeID),
				new ComparisonFilter(ComparisonOperator.Equal, lastMajorVersionProperty, lastMajorVersion)
			});
			foreach (SpamDataBlob block in this.DataProvider.Find<SpamDataBlob>(findMajorSpamDataBlobFilter, null, true, null).Cast<SpamDataBlob>())
			{
				if (block.MajorVersion > lastMajorVersion || (block.MajorVersion == lastMajorVersion && block.MinorVersion > lastMinorVersion))
				{
					lastMajorVersion = block.MajorVersion;
					lastMinorVersion = block.MinorVersion;
				}
				yield return block;
			}
			QueryFilter findMinorSpamDataFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.DataIDProperty, dataID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.DataTypeIDProperty, dataTypeID),
				new ComparisonFilter(ComparisonOperator.Equal, SpamDataBlob.MajorVersionProperty, lastMajorVersion),
				new ComparisonFilter(ComparisonOperator.Equal, lastMinorVersionProperty, lastMinorVersion)
			});
			foreach (SpamDataBlob block2 in this.DataProvider.Find<SpamDataBlob>(findMinorSpamDataFilter, null, true, null).Cast<SpamDataBlob>())
			{
				if (block2.MajorVersion > lastMajorVersion || (block2.MajorVersion == lastMajorVersion && block2.MinorVersion > lastMinorVersion))
				{
					lastMajorVersion = block2.MajorVersion;
					lastMinorVersion = block2.MinorVersion;
				}
				yield return block2;
			}
			yield break;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0003F210 File Offset: 0x0003D410
		internal void Save(SpamRulePackageData newRules, SpamRulePackageData deletedRules, IEnumerable<SyncWatermark> syncWatermarks)
		{
			List<RuleBase> list = new List<RuleBase>();
			List<RulePredicate> list2 = new List<RulePredicate>();
			List<PredicateExtendedProperty> list3 = new List<PredicateExtendedProperty>();
			List<RuleExtendedProperty> list4 = new List<RuleExtendedProperty>();
			List<Processor> list5 = new List<Processor>();
			List<RuleUpdate> list6 = new List<RuleUpdate>();
			if (newRules != null && newRules.Rules.Any<SpamRuleData>())
			{
				foreach (SpamRuleData spamRuleData in newRules.Rules)
				{
					Guid newID = this.NewID;
					list.Add(this.GenerateRuleBaseFromRuleData(newID, RuleType.Spam, spamRuleData));
					list4.AddRange(this.GenerateRuleExtendedPropertiesFromRuleData(newID, spamRuleData));
					list4.AddRange(this.GenerateRuleExtendedPropertiesFromSpamRuleData(newID, spamRuleData));
					if (spamRuleData.Result != null)
					{
						list4.AddRange(this.GenerateRuleExtendedPropertiesFromResultData(newID, spamRuleData.Result));
					}
					if (spamRuleData.AuthoringProperties != null)
					{
						list4.AddRange(this.GenerateRuleExtendedPropertiesFromAuthoringData(newID, spamRuleData.AuthoringProperties));
					}
					this.GeneratePredicatesFromPredicateData(newID, spamRuleData.Predicate, null, list2, list3);
				}
			}
			if (newRules != null && newRules.Processors.Any<ProcessorData>())
			{
				Dictionary<long, ProcessorData> dictionary = new Dictionary<long, ProcessorData>(newRules.Processors.Length);
				foreach (ProcessorData processorData in newRules.Processors)
				{
					dictionary[processorData.ProcessorID] = processorData;
				}
				foreach (long key in dictionary.Keys)
				{
					list5.AddRange(this.GenerateProcessorsFromProcessorData(dictionary[key]));
				}
			}
			if (deletedRules != null && deletedRules.Rules.Any<SpamRuleData>())
			{
				foreach (SpamRuleData spamRuleData2 in deletedRules.Rules)
				{
					list6.Add(new RuleUpdate
					{
						RuleID = new long?(spamRuleData2.RuleID),
						RuleType = new byte?(0),
						IsActive = new bool?(false),
						State = new byte?(1)
					});
				}
			}
			this.Save(list5);
			this.Save(list3);
			this.Save(list2);
			this.Save(list4);
			this.Save(list);
			this.Save(list6);
			this.Save(syncWatermarks);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0003F470 File Offset: 0x0003D670
		internal void Save(URIRulePackageData newRules, URIRulePackageData deletedRules, IEnumerable<SyncWatermark> syncWatermarks)
		{
			List<RuleBase> list = new List<RuleBase>();
			List<RuleExtendedProperty> list2 = new List<RuleExtendedProperty>();
			List<RuleUpdate> list3 = new List<RuleUpdate>();
			if (newRules != null && newRules.Rules.Any<URIRuleData>())
			{
				foreach (URIRuleData rule in newRules.Rules)
				{
					Guid newID = this.NewID;
					list.Add(this.GenerateRuleBaseFromRuleData(newID, RuleType.URI, rule));
					list2.AddRange(this.GenerateRuleExtendedPropertiesFromRuleData(newID, rule));
					list2.AddRange(this.GenerateRuleExtendedPropertiesFromURIRuleData(newID, rule));
				}
			}
			if (deletedRules != null && deletedRules.Rules.Any<URIRuleData>())
			{
				foreach (URIRuleData uriruleData in deletedRules.Rules)
				{
					list3.Add(new RuleUpdate
					{
						RuleID = new long?(uriruleData.RuleID),
						RuleType = new byte?(1),
						IsActive = new bool?(false),
						State = new byte?(1)
					});
				}
			}
			this.Save(list2);
			this.Save(list);
			this.Save(list3);
			this.Save(syncWatermarks);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0003F58D File Offset: 0x0003D78D
		internal void Save(SpamRulePackageData newRules, SpamRulePackageData deletedRules)
		{
			this.Save(newRules, deletedRules, new List<SyncWatermark>());
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0003F59C File Offset: 0x0003D79C
		internal void Save(URIRulePackageData newRules, URIRulePackageData deletedRules)
		{
			this.Save(newRules, deletedRules, new List<SyncWatermark>());
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0003F5AC File Offset: 0x0003D7AC
		internal SpamRulePackageData FindSpamRulesByGroupID(long groupID, bool activeOnly = false)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.GroupIDProperty, groupID),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 0),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, activeOnly),
				this.BuildVersionParam
			});
			List<SpamRule> list = this.DataProvider.Find<SpamRule>(filter, null, true, null).Cast<SpamRule>().ToList<SpamRule>();
			if (list != null && list.Any<SpamRule>())
			{
				return this.GenerateSpamRulePackageData(list);
			}
			return null;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0003F63C File Offset: 0x0003D83C
		internal URIRulePackageData FindURIRulesByGroupID(long groupID, bool activeOnly = false)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.GroupIDProperty, groupID),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 1),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, activeOnly),
				this.BuildVersionParam
			});
			List<URIRule> list = this.DataProvider.Find<URIRule>(filter, null, true, null).Cast<URIRule>().ToList<URIRule>();
			if (list != null && list.Any<URIRule>())
			{
				return this.GenerateURIRulePackageData(list);
			}
			return null;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0003F6CC File Offset: 0x0003D8CC
		internal SpamRulePackageData FindSpamRuleByRuleID(long ruleID)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleIDProperty, ruleID),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 0),
				this.BuildVersionParam
			});
			List<SpamRule> list = this.DataProvider.Find<SpamRule>(filter, null, true, null).Cast<SpamRule>().ToList<SpamRule>();
			if (list != null && list.Any<SpamRule>())
			{
				return this.GenerateSpamRulePackageData(list);
			}
			return null;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0003F748 File Offset: 0x0003D948
		internal URIRulePackageData FindURIRuleByRuleID(long ruleID)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleIDProperty, ruleID),
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 1),
				this.BuildVersionParam
			});
			List<URIRule> list = this.DataProvider.Find<URIRule>(filter, null, true, null).Cast<URIRule>().ToList<URIRule>();
			if (list != null && list.Any<URIRule>())
			{
				return this.GenerateURIRulePackageData(list);
			}
			return null;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
		internal URIRulePackageData FindURIRules()
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RuleBase.RuleTypeProperty, 1),
				new ComparisonFilter(ComparisonOperator.Equal, KesSpamSchema.ActiveOnlyProperty, true),
				this.BuildVersionParam
			});
			List<URIRule> list = this.DataProvider.Find<URIRule>(filter, null, true, null).Cast<URIRule>().ToList<URIRule>();
			if (list != null && list.Any<URIRule>())
			{
				return this.GenerateURIRulePackageData(list);
			}
			return null;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0003F83E File Offset: 0x0003DA3E
		internal void UpdateRule(IEnumerable<RuleUpdate> items)
		{
			this.Save(items);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0003F870 File Offset: 0x0003DA70
		internal long FindMaxRuleID(RuleType ruleType)
		{
			List<MaxRuleID> list = this.DataProvider.Find<MaxRuleID>(new ComparisonFilter(ComparisonOperator.Equal, MaxRuleID.RuleTypeProperty, (byte)ruleType), null, true, null).Cast<MaxRuleID>().ToList<MaxRuleID>();
			if (list == null || !list.Any<MaxRuleID>())
			{
				return 0L;
			}
			return list.Select(delegate(MaxRuleID item)
			{
				long? ruleID = item.RuleID;
				if (ruleID == null)
				{
					return 0L;
				}
				return ruleID.GetValueOrDefault();
			}).Max();
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0003F908 File Offset: 0x0003DB08
		internal long FindMaxProcessorID()
		{
			List<MaxProcessorID> list = this.DataProvider.Find<MaxProcessorID>(this.BuildVersionParam, null, true, null).Cast<MaxProcessorID>().ToList<MaxProcessorID>();
			if (list == null || !list.Any<MaxProcessorID>())
			{
				return 0L;
			}
			return list.Select(delegate(MaxProcessorID item)
			{
				long? processorID = item.ProcessorID;
				if (processorID == null)
				{
					return 0L;
				}
				return processorID.GetValueOrDefault();
			}).Max();
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0003F9C2 File Offset: 0x0003DBC2
		internal IEnumerable<T> ReadRuleDataByRulesIds<T>(IEnumerable<RuleBase> rules) where T : IConfigurable, new()
		{
			return rules.BatchSplit(1000).AsParallel<IEnumerable<RuleBase>>().SelectMany(delegate(IEnumerable<RuleBase> batch)
			{
				QueryFilter filter = QueryFilter.AndTogether((from item in batch
				select new ComparisonFilter(ComparisonOperator.Equal, RuleBase.IDProperty, item.ID)).ToArray<ComparisonFilter>());
				return this.DataProvider.Find<T>(filter, null, true, null).Cast<T>();
			});
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0003FA47 File Offset: 0x0003DC47
		internal IEnumerable<Processor> ReadProcessorsByIds(IEnumerable<long> ids)
		{
			return ids.BatchSplit(1000).AsParallel<IEnumerable<long>>().SelectMany(delegate(IEnumerable<long> batch)
			{
				QueryFilter filter = QueryFilter.AndTogether((from item in batch
				select new ComparisonFilter(ComparisonOperator.Equal, Processor.ProcessorIDProperty, item)).ToArray<ComparisonFilter>());
				return this.DataProvider.Find<Processor>(filter, null, true, null).Cast<Processor>();
			});
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0003FD08 File Offset: 0x0003DF08
		internal IEnumerable<long> GetProcessorIdsFromPredicateData(PredicateData predicate)
		{
			if (predicate != null)
			{
				if (predicate.ProcessorID != null)
				{
					yield return predicate.ProcessorID.Value;
				}
				long targetValue;
				if (!string.IsNullOrEmpty(predicate.Target) && long.TryParse(predicate.Target, out targetValue) && targetValue != 0L)
				{
					yield return targetValue;
				}
				if (predicate.ChildPredicate != null)
				{
					foreach (long value in predicate.ChildPredicate.SelectMany((PredicateData child) => this.GetProcessorIdsFromPredicateData(child)))
					{
						yield return value;
					}
				}
			}
			yield break;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0003FD2C File Offset: 0x0003DF2C
		internal List<T> FindPagedRules<T>(QueryFilter queryFilter, ref string pageCookie, int pageSize) where T : IConfigurable, new()
		{
			List<T> list = new List<T>();
			bool flag = false;
			while (!flag)
			{
				QueryFilter pagingQueryFilter = PagingHelper.GetPagingQueryFilter(queryFilter, pageCookie);
				IEnumerable<T> collection = this.DataProvider.FindPaged<T>(pagingQueryFilter, null, true, null, pageSize);
				list.AddRange(collection);
				pageCookie = PagingHelper.GetProcessedCookie(pagingQueryFilter, out flag);
			}
			return list;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0003FD74 File Offset: 0x0003DF74
		internal static void GetPageCookieDateTime(string pageCookie, out DateTime minDate, out DateTime maxDate)
		{
			IEnumerable<DateTime> dateTimes = PageCookieTvp.GetDateTimes(pageCookie);
			minDate = DateTime.MaxValue;
			maxDate = DateTime.MinValue;
			foreach (DateTime dateTime in dateTimes)
			{
				if (minDate > dateTime)
				{
					minDate = dateTime;
				}
				if (maxDate < dateTime)
				{
					maxDate = dateTime;
				}
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0003FDFC File Offset: 0x0003DFFC
		internal string CreatePageCookieFromDateTime(DateTime dateTime)
		{
			dateTime = dateTime.AddSeconds(-30.0);
			IPartitionedDataProvider partitionedDataProvider = (IPartitionedDataProvider)this.DataProvider;
			int numberOfPersistentCopiesPerPartition = partitionedDataProvider.GetNumberOfPersistentCopiesPerPartition(0);
			string[] array = new string[numberOfPersistentCopiesPerPartition];
			for (int i = 0; i < numberOfPersistentCopiesPerPartition; i++)
			{
				array[i] = partitionedDataProvider.GetPartitionedDatabaseCopyName(0, i);
			}
			return PageCookieTvp.CreatePageCookie(array, dateTime);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0003FE54 File Offset: 0x0003E054
		internal void ApplyAuditProperties(IConfigurable configurable)
		{
			IPropertyBag propertyBag = configurable as IPropertyBag;
			AuditHelper.ApplyAuditProperties(propertyBag, CombGuidGenerator.NewGuid(), this.callerId);
		}

		// Token: 0x04000998 RID: 2456
		public const int DefaultBatchSize = 1000;

		// Token: 0x04000999 RID: 2457
		protected const string DefaultCallerId = "Unknown";

		// Token: 0x0400099A RID: 2458
		protected string callerId = "Unknown";
	}
}

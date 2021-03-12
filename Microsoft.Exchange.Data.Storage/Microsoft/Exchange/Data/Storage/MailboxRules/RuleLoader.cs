using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BFE RID: 3070
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RuleLoader
	{
		// Token: 0x06006D80 RID: 28032 RVA: 0x001D50CC File Offset: 0x001D32CC
		protected RuleLoader(IRuleEvaluationContext context)
		{
			this.context = context;
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x001D50DB File Offset: 0x001D32DB
		public static bool IsJunkEmailRule(Rule rule)
		{
			return string.Equals(rule.Provider, "JunkEmailRule", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x001D50EE File Offset: 0x001D32EE
		public static bool IsNeverClutterOverrideRule(Rule rule)
		{
			return string.Equals(rule.Provider, "NeverClutterOverrideRule", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006D83 RID: 28035 RVA: 0x001D5104 File Offset: 0x001D3304
		public static List<Rule> LoadRules(IRuleEvaluationContext context)
		{
			RuleLoader ruleLoader = new RuleLoader(context);
			return ruleLoader.Load();
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x001D5120 File Offset: 0x001D3320
		public ulong GetRulesQuota()
		{
			ADRawEntry data = this.context.RecipientCache.FindAndCacheRecipient(this.context.Recipient).Data;
			if (data != null)
			{
				return ((ByteQuantifiedSize)data[IADMailStorageSchema.RulesQuota]).ToBytes();
			}
			this.context.TraceError("Unable to get recipient rules quota. Use default limit.");
			return 65536UL;
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x001D5184 File Offset: 0x001D3384
		internal static bool HasOofRule(List<Rule> rules)
		{
			if (rules == null)
			{
				return false;
			}
			foreach (Rule rule in rules)
			{
				if (RuleLoader.IsOofRule(rule))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x001D51E0 File Offset: 0x001D33E0
		internal static bool IsOofRule(Rule rule)
		{
			return (rule.StateFlags & RuleStateFlags.OnlyWhenOOFEx) != (RuleStateFlags)0 || ((rule.StateFlags & RuleStateFlags.OnlyWhenOOF) != (RuleStateFlags)0 && (rule.StateFlags & RuleStateFlags.LegacyOofRule) == (RuleStateFlags)0);
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x001D520D File Offset: 0x001D340D
		internal static bool IsDisabled(Rule rule)
		{
			return (rule.StateFlags & RuleStateFlags.Enabled) == (RuleStateFlags)0;
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x001D5238 File Offset: 0x001D3438
		internal List<Rule> Load()
		{
			List<Rule> rules = (List<Rule>)RuleUtil.RunMapiCode(ServerStrings.LoadRulesFromStore, new ActionWithReturnValue(this.LoadRulesFromStore));
			if (rules == null || rules.Count == 0)
			{
				return rules;
			}
			RuleUtil.RunMapiCode(ServerStrings.EnforceRulesQuota, delegate()
			{
				this.EnforceRulesQuota(rules);
				return null;
			});
			rules.RemoveAll(new Predicate<Rule>(this.CanSkipRule));
			this.FixRules(rules);
			return rules;
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x001D52D4 File Offset: 0x001D34D4
		protected virtual List<Rule> LoadRulesFromStore()
		{
			Folder currentFolder = this.context.CurrentFolder;
			MapiFolder mapiFolder = currentFolder.MapiFolder;
			RulesRetrievalInfo rulesRetrievalInfo;
			Rule[] rulesForTransport = mapiFolder.GetRulesForTransport(out rulesRetrievalInfo, new PropTag[0]);
			switch (rulesRetrievalInfo)
			{
			case RulesRetrievalInfo.CacheMiss:
				this.UpdateExtraTrackingData("RuleCacheMisses", currentFolder);
				break;
			case RulesRetrievalInfo.CacheCorruption:
				this.UpdateExtraTrackingData("RuleCacheCorruptions", currentFolder);
				break;
			}
			return new List<Rule>(rulesForTransport);
		}

		// Token: 0x06006D8A RID: 28042 RVA: 0x001D533C File Offset: 0x001D353C
		protected virtual ulong GetFolderRulesSize(Folder folder)
		{
			ulong result = 0UL;
			PropValue prop = folder.MapiFolder.GetProp(PropTag.RulesSize);
			if (!prop.IsError() && prop.Value != null)
			{
				result = (ulong)((long)prop.GetInt());
			}
			return result;
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x001D537C File Offset: 0x001D357C
		private bool CanSkipRule(Rule rule)
		{
			if (rule.Actions == null)
			{
				this.context.TraceDebug<string>("Skipping rule with no actions: \"{0}.\"", rule.Name);
				return true;
			}
			if (!this.context.IsOof && RuleLoader.IsOofRule(rule))
			{
				this.context.TraceDebug<string>("Skipping OOF rule: \"{0}.\"", rule.Name);
				return true;
			}
			if ((rule.StateFlags & RuleStateFlags.TempDisabled) != (RuleStateFlags)0)
			{
				if (!this.context.ShouldExecuteDisabledAndInErrorRules)
				{
					this.context.TraceDebug<string>("Skipping temporarily disabled rule: \"{0}.\"", rule.Name);
					return true;
				}
				this.context.TraceDebug<string>("Including temporarily disabled rule: \"{0}\" for test message", rule.Name);
			}
			if ((rule.StateFlags & RuleStateFlags.LegacyOofRule) != (RuleStateFlags)0)
			{
				this.context.TraceDebug<string>("Skipping legacy OOF rule \"{0}.\"", rule.Name);
				return true;
			}
			if ((rule.StateFlags & RuleStateFlags.Enabled) == (RuleStateFlags)0 && (rule.StateFlags & RuleStateFlags.OnlyWhenOOFEx) == (RuleStateFlags)0)
			{
				if (!this.context.ShouldExecuteDisabledAndInErrorRules)
				{
					this.context.TraceDebug<string>("Skipping disabled rule \"{0}.\"", rule.Name);
					return true;
				}
				this.context.TraceDebug<string>("Including disabled rule: \"{0}\" for test message", rule.Name);
			}
			if ((rule.StateFlags & RuleStateFlags.Error) != (RuleStateFlags)0)
			{
				if (!this.context.ShouldExecuteDisabledAndInErrorRules)
				{
					this.context.TraceDebug<string>("Skipping rule \"{0},\" it is in error.", rule.Name);
					return true;
				}
				this.context.TraceDebug<string>("Including in error rule: \"{0}\" for test message", rule.Name);
			}
			if (this.IsSafeMessage() && RuleLoader.IsJunkEmailRule(rule))
			{
				this.context.TraceDebug<string>("Skipping junk email rule \"{0}\" for this trusted message.", rule.Name);
				return true;
			}
			return false;
		}

		// Token: 0x06006D8C RID: 28044 RVA: 0x001D550C File Offset: 0x001D370C
		private void EnforceRulesQuota(List<Rule> rules)
		{
			int num = -1;
			Folder currentFolder = this.context.CurrentFolder;
			bool flag = false;
			ulong rulesQuota = this.GetRulesQuota();
			object obj = currentFolder.TryGetProperty(FolderSchema.FolderRulesSize);
			ulong num2;
			if (obj is PropertyError)
			{
				num2 = this.GetFolderRulesSize(currentFolder);
			}
			else
			{
				num2 = (ulong)((long)((int)obj));
			}
			for (int i = rules.Count - 1; i >= 0; i--)
			{
				if (num2 < rulesQuota)
				{
					return;
				}
				Rule rule = rules[i];
				if (!rule.IsExtended && !RuleLoader.IsDisabled(rule))
				{
					if (!flag && RuleLoader.IsOofRule(rule))
					{
						if (num == -1)
						{
							num = i;
						}
					}
					else
					{
						this.context.TraceDebug<string>("EnforceRulesQuota. Disabling and mark in error rule {0}", rule.Name);
						this.context.DisableAndMarkRuleInError(rule, rule.Actions[0].ActionType, 0, DeferredError.RuleError.FolderOverQuota);
					}
					num2 = this.GetFolderRulesSize(currentFolder);
				}
				if (i == 0 && !flag && num2 >= rulesQuota && num != -1)
				{
					flag = true;
					i = num + 1;
				}
			}
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x001D5604 File Offset: 0x001D3804
		private PropValue FixProperty(PropValue property)
		{
			if (property.PropTag != PropTag.SpamConfidenceLevel || !(property.Value is int) || (int)property.Value != -1)
			{
				return property;
			}
			ADRawEntry data = this.context.RecipientCache.FindAndCacheRecipient(this.context.Recipient).Data;
			if (data == null || data[ADRecipientSchema.SCLJunkEnabled] == null || data[ADRecipientSchema.SCLJunkThreshold] == null || !(bool)data[ADRecipientSchema.SCLJunkEnabled])
			{
				return new PropValue(property.PropTag, this.context.RuleConfig.SCLJunkThreshold);
			}
			return new PropValue(property.PropTag, (int)data[ADRecipientSchema.SCLJunkThreshold]);
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x001D56D8 File Offset: 0x001D38D8
		private Restriction FixRestriction(Restriction restriction)
		{
			if (restriction == null)
			{
				return null;
			}
			switch (restriction.Type)
			{
			case Restriction.ResType.And:
			{
				Restriction.AndRestriction andRestriction = (Restriction.AndRestriction)restriction;
				andRestriction.Restrictions = this.FixRestrictions(andRestriction.Restrictions);
				break;
			}
			case Restriction.ResType.Or:
			{
				Restriction.OrRestriction orRestriction = (Restriction.OrRestriction)restriction;
				orRestriction.Restrictions = this.FixRestrictions(orRestriction.Restrictions);
				break;
			}
			case Restriction.ResType.Not:
			{
				Restriction.NotRestriction notRestriction = (Restriction.NotRestriction)restriction;
				notRestriction.Restriction = this.FixRestriction(notRestriction.Restriction);
				break;
			}
			case Restriction.ResType.Property:
			{
				Restriction.PropertyRestriction propertyRestriction = (Restriction.PropertyRestriction)restriction;
				propertyRestriction.PropValue = this.FixProperty(propertyRestriction.PropValue);
				break;
			}
			case Restriction.ResType.SubRestriction:
			{
				Restriction.SubRestriction subRestriction = (Restriction.SubRestriction)restriction;
				subRestriction.Restriction = this.FixRestriction(subRestriction.Restriction);
				break;
			}
			case Restriction.ResType.Comment:
			{
				Restriction.CommentRestriction commentRestriction = (Restriction.CommentRestriction)restriction;
				commentRestriction.Restriction = this.FixRestriction(commentRestriction.Restriction);
				break;
			}
			case Restriction.ResType.Count:
			{
				Restriction.CountRestriction countRestriction = (Restriction.CountRestriction)restriction;
				countRestriction.Restriction = this.FixRestriction(countRestriction.Restriction);
				break;
			}
			}
			return restriction;
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x001D57FC File Offset: 0x001D39FC
		private Restriction[] FixRestrictions(Restriction[] conditions)
		{
			if (conditions != null)
			{
				for (int i = 0; i < conditions.Length; i++)
				{
					conditions[i] = this.FixRestriction(conditions[i]);
				}
			}
			return conditions;
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x001D5828 File Offset: 0x001D3A28
		private void FixRules(List<Rule> rules)
		{
			this.context.TraceDebug<object>("Fix Rules. The SCL Junk Threshold is {0}", this.context.RuleConfig.SCLJunkThreshold);
			foreach (Rule rule in rules)
			{
				if (rule.Name == null)
				{
					rule.Name = string.Empty;
				}
				if (rule.IsExtended)
				{
					rule.Condition = this.FixRestriction(rule.Condition);
				}
			}
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x001D58BC File Offset: 0x001D3ABC
		private bool IsSafeMessage()
		{
			object obj = this.context.Message.TryGetProperty(ItemSchema.SpamConfidenceLevel);
			if (obj is int)
			{
				int num = (int)obj;
				return num == -1;
			}
			return false;
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x001D58F4 File Offset: 0x001D3AF4
		private void UpdateExtraTrackingData(string propertyName, Folder folder)
		{
			string text = null;
			for (int i = 0; i < this.context.ExtraTrackingEventData.Count; i++)
			{
				KeyValuePair<string, string> item = this.context.ExtraTrackingEventData[i];
				if ("RuleCacheCorruptions".Equals(item.Key, StringComparison.OrdinalIgnoreCase))
				{
					text = item.Value;
					this.context.ExtraTrackingEventData.Remove(item);
					break;
				}
			}
			this.context.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("RuleCacheCorruptions", string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}:{3}", new object[]
			{
				text,
				string.IsNullOrEmpty(text) ? string.Empty : "|",
				this.context.Recipient.ToString(),
				folder.DisplayName
			})));
		}

		// Token: 0x04003E43 RID: 15939
		private const ulong DefaultRulesQuota = 65536UL;

		// Token: 0x04003E44 RID: 15940
		private const string JunkEmailRule = "JunkEmailRule";

		// Token: 0x04003E45 RID: 15941
		private const string NeverClutterOverrideRule = "NeverClutterOverrideRule";

		// Token: 0x04003E46 RID: 15942
		private const int InvalidRuleIndex = -1;

		// Token: 0x04003E47 RID: 15943
		private const string RuleCacheCorruptionsPropertyName = "RuleCacheCorruptions";

		// Token: 0x04003E48 RID: 15944
		private const string RuleCacheMissesPropertyName = "RuleCacheMisses";

		// Token: 0x04003E49 RID: 15945
		private IRuleEvaluationContext context;
	}
}

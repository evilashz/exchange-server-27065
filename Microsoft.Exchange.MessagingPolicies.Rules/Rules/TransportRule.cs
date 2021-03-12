using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000032 RID: 50
	internal class TransportRule : Rule
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00008E6A File Offset: 0x0000706A
		public TransportRule(string name) : this(name, null)
		{
			this.SenderAddressLocation = SenderAddressLocation.Header;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008E7B File Offset: 0x0000707B
		public TransportRule(string name, Condition condition) : base(name, condition)
		{
			this.fork = new List<RuleBifurcationInfo>();
			this.SenderAddressLocation = SenderAddressLocation.Header;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008E97 File Offset: 0x00007097
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00008E9F File Offset: 0x0000709F
		public List<RuleBifurcationInfo> Fork
		{
			get
			{
				return this.fork;
			}
			set
			{
				this.fork = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008EA8 File Offset: 0x000070A8
		public int RegexCharacterCount
		{
			get
			{
				int num = 0;
				foreach (RuleBifurcationInfo ruleBifurcationInfo in this.Fork)
				{
					foreach (string text in ruleBifurcationInfo.Patterns)
					{
						num += text.Length;
					}
				}
				num += this.CountRegexCharsForCondition(base.Condition);
				return num;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008F4C File Offset: 0x0000714C
		public List<string> RecipientsAddedByActions
		{
			get
			{
				List<string> list = new List<string>();
				foreach (Action action in base.Actions)
				{
					if (string.Equals(action.Name, "AddToRecipientSmtpOnly", StringComparison.InvariantCultureIgnoreCase) || string.Equals(action.Name, "AddCcRecipientSmtpOnly", StringComparison.InvariantCultureIgnoreCase) || string.Equals(action.Name, "AddEnvelopeRecipient", StringComparison.InvariantCultureIgnoreCase) || string.Equals(action.Name, "RedirectMessage", StringComparison.InvariantCultureIgnoreCase) || string.Equals(action.Name, "GenerateIncidentReport", StringComparison.InvariantCultureIgnoreCase) || string.Equals(action.Name, "GenerateNotification", StringComparison.InvariantCultureIgnoreCase))
					{
						Value value = action.Arguments[0] as Value;
						if (value != null)
						{
							list.Add((string)value.ParsedValue);
						}
					}
				}
				return list;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009040 File Offset: 0x00007240
		public override Version MinimumVersion
		{
			get
			{
				return this.ComputeMinVersion();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009048 File Offset: 0x00007248
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009050 File Offset: 0x00007250
		public SenderAddressLocation SenderAddressLocation { get; set; }

		// Token: 0x060001B1 RID: 433 RVA: 0x0000905C File Offset: 0x0000725C
		public override int GetEstimatedSize()
		{
			int num = 0;
			foreach (RuleBifurcationInfo ruleBifurcationInfo in this.Fork)
			{
				num += ruleBifurcationInfo.GetEstimatedSize();
			}
			num += 4;
			return num + base.GetEstimatedSize();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000090C0 File Offset: 0x000072C0
		public TransportActionType MostSpecificActionType
		{
			get
			{
				TransportActionType transportActionType = TransportActionType.NonRecipientRelated;
				foreach (Action action in base.Actions)
				{
					TransportAction transportAction = (TransportAction)action;
					if (transportAction.Type == TransportActionType.RecipientRelated && transportActionType == TransportActionType.NonRecipientRelated)
					{
						transportActionType = transportAction.Type;
					}
					else if (transportAction.Type == TransportActionType.BifurcationNeeded && transportActionType != TransportActionType.BifurcationNeeded)
					{
						transportActionType = transportAction.Type;
					}
				}
				return transportActionType;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009140 File Offset: 0x00007340
		public void IncrementMessagesEvaluated()
		{
			if (this.counter != null)
			{
				this.counter.MessagesEvaluated.Increment();
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000915B File Offset: 0x0000735B
		public void IncrementMessagesProcessed()
		{
			if (this.counter != null)
			{
				this.counter.MessagesProcessed.Increment();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009176 File Offset: 0x00007376
		public void CreatePerformanceCounter(string collectionName)
		{
			this.counter = RulesCounters.GetInstance(collectionName + "," + base.Name);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009194 File Offset: 0x00007394
		public void SetDlpPolicy(Guid dlpPolicyId, string dlpPolicyName)
		{
			if (Guid.Empty == dlpPolicyId)
			{
				throw new ArgumentException("dlpId");
			}
			if (string.IsNullOrEmpty(dlpPolicyName))
			{
				throw new ArgumentException("dlpPolicyName");
			}
			base.RemoveAllTags("CP");
			base.RemoveAllTags("CPN");
			base.AddTag(new RuleTag(dlpPolicyId.ToString("D"), "CP"));
			base.AddTag(new RuleTag(dlpPolicyName, "CPN"));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009210 File Offset: 0x00007410
		public bool TryGetDlpPolicy(out Tuple<Guid, string> dlpPolicy)
		{
			dlpPolicy = new Tuple<Guid, string>(Guid.Empty, string.Empty);
			Guid item;
			if (!this.TryGetDlpPolicyId(out item))
			{
				return false;
			}
			IEnumerable<RuleTag> tags = base.GetTags("CPN");
			string empty = string.Empty;
			int num = tags.Count<RuleTag>();
			if (num == 1)
			{
				dlpPolicy = new Tuple<Guid, string>(item, tags.First<RuleTag>().Name);
				return true;
			}
			if (num > 1)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceWarning<int>(0L, "More than one DLP name found in rule [count = {0}]", num);
				return false;
			}
			dlpPolicy = new Tuple<Guid, string>(item, string.Empty);
			return true;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009294 File Offset: 0x00007494
		public bool TryGetDlpPolicyId(out Guid dlpId)
		{
			IEnumerable<RuleTag> tags = base.GetTags("CP");
			int num = tags.Count<RuleTag>();
			if (num == 1)
			{
				return Guid.TryParse(tags.First<RuleTag>().Name, out dlpId);
			}
			if (num > 1)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceWarning<int>(0L, "More than one DLP found in rule [count = {0}]", num);
			}
			dlpId = Guid.Empty;
			return false;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000092EC File Offset: 0x000074EC
		private static Version MaxVersion(Version a, Version b)
		{
			if (!(a < b))
			{
				return a;
			}
			return b;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000092FC File Offset: 0x000074FC
		private Version ComputeMinVersion()
		{
			Version version = TransportRuleConstants.VersionedContainerBaseVersion;
			version = TransportRule.MaxVersion(version, base.MinimumVersion);
			foreach (Action action in base.Actions)
			{
				version = TransportRule.MaxVersion(version, action.MinimumVersion);
			}
			version = TransportRule.MaxVersion(version, base.Condition.MinimumVersion);
			foreach (RuleBifurcationInfo ruleBifurcationInfo in this.Fork)
			{
				version = TransportRule.MaxVersion(version, ruleBifurcationInfo.MinimumVersion);
			}
			return version;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000093E4 File Offset: 0x000075E4
		private int CountRegexCharsForCondition(Condition condition)
		{
			int num = 0;
			if (condition is AttachmentMatchesRegexPredicate)
			{
				AttachmentMatchesRegexPredicate attachmentMatchesRegexPredicate = condition as AttachmentMatchesRegexPredicate;
				num += attachmentMatchesRegexPredicate.Value.RawValues.Sum((string pattern) => pattern.Length);
			}
			else if (condition is SenderAttributeMatchesRegexPredicate)
			{
				SenderAttributeMatchesRegexPredicate senderAttributeMatchesRegexPredicate = condition as SenderAttributeMatchesRegexPredicate;
				num += senderAttributeMatchesRegexPredicate.Value.RawValues.Sum((string pattern) => pattern.Length);
			}
			else if (condition is LegacyMatchesPredicate)
			{
				LegacyMatchesPredicate legacyMatchesPredicate = (LegacyMatchesPredicate)condition;
				num += legacyMatchesPredicate.Patterns.Sum((string pattern) => pattern.Length);
			}
			else if (condition is TextMatchingPredicate)
			{
				TextMatchingPredicate textMatchingPredicate = (TextMatchingPredicate)condition;
				num += textMatchingPredicate.Patterns.Sum((string pattern) => pattern.Length);
			}
			else
			{
				if (condition is OrCondition)
				{
					OrCondition orCondition = (OrCondition)condition;
					using (List<Condition>.Enumerator enumerator = orCondition.SubConditions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Condition condition2 = enumerator.Current;
							num += this.CountRegexCharsForCondition(condition2);
						}
						return num;
					}
				}
				if (condition is AndCondition)
				{
					AndCondition andCondition = (AndCondition)condition;
					using (List<Condition>.Enumerator enumerator2 = andCondition.SubConditions.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Condition condition3 = enumerator2.Current;
							num += this.CountRegexCharsForCondition(condition3);
						}
						return num;
					}
				}
				if (condition is NotCondition)
				{
					NotCondition notCondition = (NotCondition)condition;
					num += this.CountRegexCharsForCondition(notCondition.SubCondition);
				}
			}
			return num;
		}

		// Token: 0x0400015B RID: 347
		public static readonly Version SenderAddressLocationVersion = new Version("15.00.0005.004");

		// Token: 0x0400015C RID: 348
		private RulesCountersInstance counter;

		// Token: 0x0400015D RID: 349
		private List<RuleBifurcationInfo> fork;
	}
}

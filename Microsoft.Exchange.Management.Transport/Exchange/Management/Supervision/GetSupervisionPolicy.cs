using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x0200008B RID: 139
	[Cmdlet("Get", "SupervisionPolicy")]
	public sealed class GetSupervisionPolicy : GetMultitenancySingletonSystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001295A File Offset: 0x00010B5A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001295D File Offset: 0x00010B5D
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00012983 File Offset: 0x00010B83
		[Parameter(Mandatory = false)]
		public SwitchParameter DisplayDetails
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisplayDetails"] ?? false);
			}
			set
			{
				base.Fields["DisplayDetails"] = value;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001299C File Offset: 0x00010B9C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			Dictionary<string, string> ruleNames = GetSupervisionPolicy.GetRuleNames();
			ADRuleStorageManager adruleStorageManager = null;
			try
			{
				adruleStorageManager = new ADRuleStorageManager("TransportVersioned", base.DataSession);
			}
			catch (RuleCollectionNotInAdException exception)
			{
				base.WriteError(exception, (ErrorCategory)1003, null);
			}
			QueryFilter queryFilter = GetSupervisionPolicy.GetQueryFilter(ruleNames.Keys);
			adruleStorageManager.LoadRuleCollectionWithoutParsing(queryFilter);
			try
			{
				adruleStorageManager.ParseRuleCollection();
			}
			catch (ParserException exception2)
			{
				base.WriteError(exception2, (ErrorCategory)1003, null);
			}
			SupervisionPolicy presentationObject = GetSupervisionPolicy.GetPresentationObject(adruleStorageManager, ref ruleNames, this.DisplayDetails.ToBool());
			this.WriteResult(presentationObject);
			string missingPolicies = GetSupervisionPolicy.GetMissingPolicies(ruleNames);
			if (!string.IsNullOrEmpty(missingPolicies))
			{
				this.WriteWarning(Strings.SupervisionPoliciesNotFound(missingPolicies));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00012A68 File Offset: 0x00010C68
		private static SupervisionPolicy GetPresentationObject(ADRuleStorageManager storageManager, ref Dictionary<string, string> rules, bool displayDetails)
		{
			SupervisionPolicy supervisionPolicy = new SupervisionPolicy("SupervisionPolicy" + storageManager.RuleCollectionId.GetHashCode().ToString());
			foreach (Microsoft.Exchange.MessagingPolicies.Rules.Rule rule in storageManager.GetRuleCollection())
			{
				TransportRule transportRule = (TransportRule)rule;
				if (!transportRule.IsTooAdvancedToParse)
				{
					Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule2 = Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule.CreateFromInternalRule(TransportRulePredicate.BridgeheadMappings, TransportRuleAction.BridgeheadMappings, transportRule, 0, null);
					if (transportRule.Name.Equals(GetSupervisionPolicy.ClosedCampusInboundRuleName))
					{
						supervisionPolicy.ClosedCampusInboundPolicyEnabled = (transportRule.Enabled == RuleState.Enabled);
						if (displayDetails)
						{
							supervisionPolicy.ClosedCampusInboundPolicyGroupExceptions = GetSupervisionPolicy.ConvertToSmtpAddressMVP(rule2.ExceptIfSentToMemberOf);
							supervisionPolicy.ClosedCampusInboundPolicyDomainExceptions = GetSupervisionPolicy.ConvertToSmtpDomains(rule2.ExceptIfFromAddressMatchesPatterns);
						}
						rules.Remove(GetSupervisionPolicy.ClosedCampusInboundRuleName);
					}
					else if (transportRule.Name.Equals(GetSupervisionPolicy.ClosedCampusOutboundRuleName))
					{
						supervisionPolicy.ClosedCampusOutboundPolicyEnabled = (transportRule.Enabled == RuleState.Enabled);
						if (displayDetails)
						{
							supervisionPolicy.ClosedCampusOutboundPolicyGroupExceptions = GetSupervisionPolicy.ConvertToSmtpAddressMVP(rule2.ExceptIfFromMemberOf);
							supervisionPolicy.ClosedCampusOutboundPolicyDomainExceptions = GetSupervisionPolicy.ConvertToSmtpDomains(rule2.ExceptIfRecipientAddressMatchesPatterns);
						}
						rules.Remove(GetSupervisionPolicy.ClosedCampusOutboundRuleName);
					}
					else if (transportRule.Name.Equals(GetSupervisionPolicy.BadWordsRuleName))
					{
						supervisionPolicy.BadWordsPolicyEnabled = (transportRule.Enabled == RuleState.Enabled);
						if (displayDetails)
						{
							supervisionPolicy.BadWordsList = GetSupervisionPolicy.ConvertToCommaSeparatedString(rule2.SubjectOrBodyContainsWords);
						}
						rules.Remove(GetSupervisionPolicy.BadWordsRuleName);
					}
					else if (transportRule.Name.Equals(GetSupervisionPolicy.AntiBullyingRuleName))
					{
						supervisionPolicy.AntiBullyingPolicyEnabled = (transportRule.Enabled == RuleState.Enabled);
						rules.Remove(GetSupervisionPolicy.AntiBullyingRuleName);
					}
				}
			}
			return supervisionPolicy;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00012C44 File Offset: 0x00010E44
		private static QueryFilter GetQueryFilter(IEnumerable<string> ruleNames)
		{
			QueryFilter queryFilter = null;
			foreach (string propertyValue in ruleNames)
			{
				QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, propertyValue);
				if (queryFilter != null)
				{
					queryFilter = new OrFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					});
				}
				else
				{
					queryFilter = queryFilter2;
				}
			}
			return queryFilter;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00012CB8 File Offset: 0x00010EB8
		internal static Dictionary<string, string> GetRuleNames()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
			dictionary[GetSupervisionPolicy.ClosedCampusInboundRuleName] = "Closed campus inbound";
			dictionary[GetSupervisionPolicy.ClosedCampusOutboundRuleName] = "Closed campus outbound";
			dictionary[GetSupervisionPolicy.BadWordsRuleName] = "Bad words";
			dictionary[GetSupervisionPolicy.AntiBullyingRuleName] = "Anti bullying";
			return dictionary;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00012D10 File Offset: 0x00010F10
		private static string GetMissingPolicies(Dictionary<string, string> missingRules)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in missingRules)
			{
				if (!string.IsNullOrEmpty(stringBuilder.ToString()))
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(keyValuePair.Value);
				}
				else
				{
					stringBuilder.Append(keyValuePair.Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012D9C File Offset: 0x00010F9C
		private static SmtpDomain[] ConvertToSmtpDomains(Pattern[] patterns)
		{
			if (patterns == null)
			{
				return null;
			}
			SmtpDomain[] array = new SmtpDomain[patterns.Length];
			int num = 0;
			foreach (Pattern pattern in patterns)
			{
				string text = pattern.ToString();
				array[num++] = new SmtpDomain(text.Substring(1, text.Length - 1));
			}
			return array;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012E08 File Offset: 0x00011008
		private static MultiValuedProperty<SmtpAddress> ConvertToSmtpAddressMVP(RecipientIdParameter[] idParameters)
		{
			MultiValuedProperty<SmtpAddress> multiValuedProperty = new MultiValuedProperty<SmtpAddress>();
			if (idParameters == null)
			{
				return multiValuedProperty;
			}
			foreach (RecipientIdParameter recipientIdParameter in idParameters)
			{
				multiValuedProperty.Add(SmtpAddress.Parse(recipientIdParameter.RawIdentity));
			}
			return multiValuedProperty;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012E48 File Offset: 0x00011048
		private static string ConvertToCommaSeparatedString(Word[] words)
		{
			if (words == null || words.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(words[0].Value);
			for (int i = 1; i < words.Length; i++)
			{
				stringBuilder.Append(SupervisionPolicy.BadWordsSeparator);
				stringBuilder.Append(" ");
				stringBuilder.Append(words[i].Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000198 RID: 408
		internal const string RuleCollectionName = "TransportVersioned";

		// Token: 0x04000199 RID: 409
		internal const string RuleNamePrefix = "__";

		// Token: 0x0400019A RID: 410
		internal const string ClosedCampusInboundPresentationName = "Closed campus inbound";

		// Token: 0x0400019B RID: 411
		internal const string ClosedCampusOutboundPresentationName = "Closed campus outbound";

		// Token: 0x0400019C RID: 412
		internal const string BadWordsPresentationName = "Bad words";

		// Token: 0x0400019D RID: 413
		internal const string AntiBullyingPresentationName = "Anti bullying";

		// Token: 0x0400019E RID: 414
		private const int supervisionRuleCount = 4;

		// Token: 0x0400019F RID: 415
		public const string ClosedCampusInboundRejectReasonText = "You can't send e-mail to people in this organization.";

		// Token: 0x040001A0 RID: 416
		public const string ClosedCampusOutboundRejectReasonText = "You can't send e-mail to people outside this organization.";

		// Token: 0x040001A1 RID: 417
		public const string BadWordsRejectReasonText = "This message contains inappropriate language that's not permitted by the organization.";

		// Token: 0x040001A2 RID: 418
		public const string AntiBullyingRejectReasonText = "You're not allowed to send e-mail to this person";

		// Token: 0x040001A3 RID: 419
		public static readonly string ClosedCampusInboundRuleName = "__" + "closedcampusinbound";

		// Token: 0x040001A4 RID: 420
		public static readonly string ClosedCampusOutboundRuleName = "__" + "closedcampusoutbound";

		// Token: 0x040001A5 RID: 421
		public static readonly string BadWordsRuleName = "__" + "badwords";

		// Token: 0x040001A6 RID: 422
		public static readonly string AntiBullyingRuleName = "__" + "antibullying";
	}
}

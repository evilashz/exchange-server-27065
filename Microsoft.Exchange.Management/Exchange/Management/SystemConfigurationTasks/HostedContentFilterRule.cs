using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A4D RID: 2637
	public class HostedContentFilterRule : HygieneFilterRule
	{
		// Token: 0x06005E64 RID: 24164 RVA: 0x0018BA42 File Offset: 0x00189C42
		public HostedContentFilterRule()
		{
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x0018BA4C File Offset: 0x00189C4C
		internal HostedContentFilterRule(TransportRule transportRule, string name, int priority, RuleState state, string comments, TransportRulePredicate[] conditions, TransportRulePredicate[] exceptions, HostedContentFilterPolicyIdParameter policyId) : base(transportRule, name, priority, state, comments, conditions, exceptions, policyId)
		{
			if (base.Conditions != null)
			{
				foreach (TransportRulePredicate predicate in base.Conditions)
				{
					base.SetParametersFromPredicate(predicate, false);
				}
			}
			if (base.Exceptions != null)
			{
				foreach (TransportRulePredicate predicate2 in base.Exceptions)
				{
					base.SetParametersFromPredicate(predicate2, true);
				}
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x06005E66 RID: 24166 RVA: 0x0018BAC6 File Offset: 0x00189CC6
		internal ObjectSchema Schema
		{
			get
			{
				return HostedContentFilterRule.schema;
			}
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x06005E67 RID: 24167 RVA: 0x0018BAD8 File Offset: 0x00189CD8
		internal bool IsEsnCompatible
		{
			get
			{
				if (base.Exceptions != null && base.Exceptions.Length > 0)
				{
					return false;
				}
				return base.Conditions.All((TransportRulePredicate condition) => condition is RecipientDomainIsPredicate);
			}
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x0018BB17 File Offset: 0x00189D17
		// (set) Token: 0x06005E69 RID: 24169 RVA: 0x0018BB24 File Offset: 0x00189D24
		public HostedContentFilterPolicyIdParameter HostedContentFilterPolicy
		{
			get
			{
				return base.PolicyId as HostedContentFilterPolicyIdParameter;
			}
			set
			{
				base.PolicyId = value;
			}
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0018BB30 File Offset: 0x00189D30
		internal override IEnumerable<Microsoft.Exchange.MessagingPolicies.Rules.Action> CreateActions()
		{
			List<Microsoft.Exchange.MessagingPolicies.Rules.Action> list = new List<Microsoft.Exchange.MessagingPolicies.Rules.Action>();
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("X-MS-Exchange-Organization-HostedContentFilterPolicy"),
				new Value(base.PolicyId.ToString())
			};
			list.Add(TransportRuleParser.Instance.CreateAction("SetHeader", arguments, null));
			list.Add(TransportRuleParser.Instance.CreateAction("Halt", new ShortList<Argument>(), null));
			return list;
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0018BBA4 File Offset: 0x00189DA4
		internal override string BuildActionDescription()
		{
			return Strings.HostedContentFilterActionDescription((base.PolicyId == null) ? string.Empty : base.PolicyId.ToString());
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x0018BBCA File Offset: 0x00189DCA
		internal override void SuppressPiiData(PiiMap piiMap)
		{
			base.SuppressPiiData(piiMap);
			if (base.PolicyId != null)
			{
				base.PolicyId = SuppressingPiiProperty.TryRedactValue<ADIdParameter>(HostedContentFilterRuleSchema.HostedContentFilterPolicy, base.PolicyId);
			}
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x0018BBF4 File Offset: 0x00189DF4
		internal static HostedContentFilterRule CreateFromInternalRule(TransportRule rule, int priority, TransportRule transportRule)
		{
			IConfigDataProvider session = null;
			if (transportRule != null)
			{
				session = transportRule.Session;
			}
			TransportRulePredicate[] conditions;
			TransportRulePredicate[] exceptions;
			TransportRuleAction[] array;
			Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule.TryConvert(HygieneFilterRule.SupportedPredicates, HygieneFilterRule.SupportedActions, rule, out conditions, out exceptions, out array, session);
			if (array == null || array.Length != 2 || !(array[0] is SetHeaderAction) || !(array[1] is StopRuleProcessingAction))
			{
				throw new CorruptFilterRuleException(Strings.CorruptRule(rule.Name, Strings.ErrorCorruptRuleAction));
			}
			string identity = ((SetHeaderAction)array[0]).HeaderValue.ToString();
			return new HostedContentFilterRule(transportRule, rule.Name, priority, rule.Enabled, rule.Comments, conditions, exceptions, new HostedContentFilterPolicyIdParameter(identity));
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x0018BCA0 File Offset: 0x00189EA0
		internal static HostedContentFilterRule CreateCorruptRule(int priority, TransportRule transportRule, LocalizedString errorText)
		{
			return new HostedContentFilterRule(transportRule, transportRule.Name, priority, RuleState.Disabled, null, null, null, null)
			{
				isValid = false,
				errorText = errorText
			};
		}

		// Token: 0x040034E7 RID: 13543
		private static readonly HostedContentFilterRuleSchema schema = ObjectSchema.GetInstance<HostedContentFilterRuleSchema>();
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A49 RID: 2633
	[Cmdlet("Get", "HostedContentFilterRule", DefaultParameterSetName = "Identity")]
	public sealed class GetHostedContentFilterRule : GetHygieneFilterRuleTaskBase
	{
		// Token: 0x06005E38 RID: 24120 RVA: 0x0018B1F4 File Offset: 0x001893F4
		public GetHostedContentFilterRule() : base("HostedContentFilterVersioned")
		{
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0018B201 File Offset: 0x00189401
		internal override IConfigurable CreateCorruptTaskRule(int priority, TransportRule transportRule, string errorMessage)
		{
			return HostedContentFilterRule.CreateCorruptRule(priority, transportRule, Strings.CorruptRule(transportRule.Name, errorMessage));
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0018B218 File Offset: 0x00189418
		internal override IConfigurable CreateTaskRuleFromInternalRule(TransportRule rule, int priority, TransportRule transportRule)
		{
			HostedContentFilterRule result;
			try
			{
				result = HostedContentFilterRule.CreateFromInternalRule(rule, priority, transportRule);
			}
			catch (CorruptFilterRuleException ex)
			{
				result = HostedContentFilterRule.CreateCorruptRule(priority, transportRule, ex.LocalizedString);
			}
			return result;
		}
	}
}

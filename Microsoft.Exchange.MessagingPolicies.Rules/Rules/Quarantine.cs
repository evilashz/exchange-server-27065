using System;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200007A RID: 122
	internal class Quarantine : TransportAction
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00014903 File Offset: 0x00012B03
		public Quarantine(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001490C File Offset: 0x00012B0C
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00014910 File Offset: 0x00012B10
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			if (transportRulesEvaluationContext.EventType == EventType.EndOfData)
			{
				transportRulesEvaluationContext.EndOfDataSource.Quarantine(null, "Quarantined by rule agent");
				transportRulesEvaluationContext.MessageQuarantined = true;
				return ExecutionControl.SkipAll;
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.CompliancePolicy.QuarantineAction.Enabled)
			{
				CommonUtils.StampPutInQuarantineHeader(transportRulesEvaluationContext.MailItem.Message, QuarantineFlavor.Policy, 0, false);
				transportRulesEvaluationContext.MessageQuarantined = true;
				return ExecutionControl.SkipAll;
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001497C File Offset: 0x00012B7C
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00014983 File Offset: 0x00012B83
		public override string Name
		{
			get
			{
				return "Quarantine";
			}
		}
	}
}

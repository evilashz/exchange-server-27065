using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200096A RID: 2410
	internal class SetDlpPolicyImpl : CmdletImplementation
	{
		// Token: 0x06005625 RID: 22053 RVA: 0x00162264 File Offset: 0x00160464
		public SetDlpPolicyImpl(SetDlpPolicy taskObject)
		{
			this.taskObject = taskObject;
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00162274 File Offset: 0x00160474
		public override void Validate()
		{
			base.Validate();
			DlpPolicyMetaData dlpPolicyMetaData = DlpPolicyParser.ParseDlpPolicyInstance(this.taskObject.TargetItem.TransportRulesXml);
			if (this.taskObject.Fields.IsModified("State"))
			{
				dlpPolicyMetaData.State = this.taskObject.State;
			}
			if (this.taskObject.Fields.IsModified("Mode"))
			{
				dlpPolicyMetaData.Mode = this.taskObject.Mode;
			}
			if (this.taskObject.TargetItem.IsModified(ADObjectSchema.Name))
			{
				dlpPolicyMetaData.Name = this.taskObject.TargetItem.Name;
			}
			if (this.taskObject.Fields.IsModified("Description"))
			{
				dlpPolicyMetaData.Description = this.taskObject.Description;
			}
			ADComplianceProgram adcomplianceProgram = dlpPolicyMetaData.ToAdObject();
			this.taskObject.TargetItem.State = adcomplianceProgram.State;
			this.taskObject.TargetItem.Name = adcomplianceProgram.Name;
			this.taskObject.TargetItem.Description = adcomplianceProgram.Description;
			this.taskObject.TargetItem.TransportRulesXml = adcomplianceProgram.TransportRulesXml;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x001623A4 File Offset: 0x001605A4
		public override void ProcessRecord()
		{
			Tuple<RuleState, RuleMode> tuple = DlpUtils.DlpStateToRuleState(this.taskObject.TargetItem.State);
			this.UpdateRules(tuple.Item1, tuple.Item2);
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x001623E4 File Offset: 0x001605E4
		protected void UpdateRules(RuleState state, RuleMode mode)
		{
			bool flag = this.taskObject.TargetItem.IsModified(ADObjectSchema.Name);
			ADRuleStorageManager adruleStorageManager;
			IEnumerable<TransportRuleHandle> transportRuleHandles = DlpUtils.GetTransportRuleHandles(base.DataSession, out adruleStorageManager);
			foreach (TransportRule transportRule in (from handle in transportRuleHandles
			select handle.Rule).Where(new Func<TransportRule, bool>(this.RuleDlpPolicyIdMatches)))
			{
				transportRule.Enabled = state;
				transportRule.Mode = mode;
				if (flag)
				{
					transportRule.SetDlpPolicy(this.taskObject.TargetItem.ImmutableId, this.taskObject.TargetItem.Name);
				}
			}
			adruleStorageManager.UpdateRuleHandles(transportRuleHandles);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x001624C0 File Offset: 0x001606C0
		protected bool RuleDlpPolicyIdMatches(TransportRule rule)
		{
			Guid guid;
			return rule.TryGetDlpPolicyId(out guid) && guid.Equals(this.taskObject.TargetItem.ImmutableId);
		}

		// Token: 0x040031D5 RID: 12757
		public static readonly string Identity = "Identity";

		// Token: 0x040031D6 RID: 12758
		private SetDlpPolicy taskObject;
	}
}

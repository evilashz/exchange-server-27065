using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000963 RID: 2403
	internal class GetDlpPolicyTemplateImpl : CmdletImplementation
	{
		// Token: 0x060055E3 RID: 21987 RVA: 0x0016139E File Offset: 0x0015F59E
		public GetDlpPolicyTemplateImpl(GetDlpPolicyTemplate taskObject)
		{
			this.taskObject = taskObject;
			this.taskObject.Fields.ResetChangeTracking();
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x001613BD File Offset: 0x0015F5BD
		public override void Validate()
		{
			if (this.taskObject.IdentityData != null)
			{
				this.taskObject.IdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn(DlpUtils.OutOfBoxDlpPoliciesCollectionName);
			}
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x001613E8 File Offset: 0x0015F5E8
		public void WriteResult(IEnumerable<ADComplianceProgram> tenantDlpPolicyTemplates, GetDlpPolicy.WriteDelegate writeDelegate)
		{
			IEnumerable<ADComplianceProgram> outOfBoxDlpTemplates;
			if (this.taskObject.Identity == null)
			{
				outOfBoxDlpTemplates = DlpUtils.GetOutOfBoxDlpTemplates(base.DataSession);
			}
			else
			{
				outOfBoxDlpTemplates = DlpUtils.GetOutOfBoxDlpTemplates(base.DataSession, this.taskObject.Identity.ToString());
			}
			foreach (ADComplianceProgram dlpPolicy in outOfBoxDlpTemplates)
			{
				writeDelegate(new DlpPolicyTemplate(dlpPolicy, this.taskObject.CommandRuntime.Host.CurrentCulture));
			}
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00161484 File Offset: 0x0015F684
		public ObjectId GetRootId()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			if (configurationSession == null)
			{
				return null;
			}
			return configurationSession.GetOrgContainerId().GetChildId("Transport Settings").GetChildId("Rules").GetChildId(DlpUtils.OutOfBoxDlpPoliciesCollectionName);
		}

		// Token: 0x040031C8 RID: 12744
		private GetDlpPolicyTemplate taskObject;
	}
}

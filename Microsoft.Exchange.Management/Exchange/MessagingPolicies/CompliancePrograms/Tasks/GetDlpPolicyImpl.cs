using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000960 RID: 2400
	internal class GetDlpPolicyImpl : CmdletImplementation
	{
		// Token: 0x060055D4 RID: 21972 RVA: 0x00161186 File Offset: 0x0015F386
		public GetDlpPolicyImpl(GetDlpPolicy taskObject)
		{
			this.taskObject = taskObject;
			this.taskObject.Fields.ResetChangeTracking();
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x001611A5 File Offset: 0x0015F3A5
		public override void Validate()
		{
			if (this.taskObject.IdentityData != null)
			{
				this.taskObject.IdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn(DlpUtils.TenantDlpPoliciesCollectionName);
			}
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x001611D0 File Offset: 0x0015F3D0
		public void WriteResult(IEnumerable<ADComplianceProgram> tenantDlpPolicies, GetDlpPolicy.WriteDelegate writeDelegate)
		{
			if (this.taskObject.NeedSuppressingPiiData && this.taskObject.ExchangeRunspaceConfig != null)
			{
				this.taskObject.ExchangeRunspaceConfig.EnablePiiMap = true;
			}
			foreach (ADComplianceProgram adDlpPolicy in tenantDlpPolicies)
			{
				DlpPolicy dlpPolicy = this.TryGetDlpPolicy(adDlpPolicy);
				if (this.taskObject.NeedSuppressingPiiData)
				{
					dlpPolicy.SuppressPiiData(Utils.GetSessionPiiMap(this.taskObject.ExchangeRunspaceConfig));
				}
				writeDelegate(dlpPolicy);
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00161270 File Offset: 0x0015F470
		public ObjectId GetRootId()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			if (configurationSession == null)
			{
				return null;
			}
			return configurationSession.GetOrgContainerId().GetChildId("Transport Settings").GetChildId("Rules").GetChildId(DlpUtils.TenantDlpPoliciesCollectionName);
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x001612B4 File Offset: 0x0015F4B4
		private DlpPolicy TryGetDlpPolicy(ADComplianceProgram adDlpPolicy)
		{
			DlpPolicy result;
			try
			{
				result = new DlpPolicy(adDlpPolicy);
			}
			catch (DlpPolicyParsingException)
			{
				DlpPolicy dlpPolicy = new DlpPolicy(null);
				dlpPolicy.SetAdDlpPolicyWithNoDlpXml(adDlpPolicy);
				this.taskObject.WriteWarning(Strings.DlpPolicyXmlInvalid);
				result = dlpPolicy;
			}
			return result;
		}

		// Token: 0x040031C6 RID: 12742
		private readonly GetDlpPolicy taskObject;
	}
}

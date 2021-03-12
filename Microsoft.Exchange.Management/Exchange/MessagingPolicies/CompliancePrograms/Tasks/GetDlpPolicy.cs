using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200095E RID: 2398
	[Cmdlet("Get", "DlpPolicy", DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(DlpPolicy)
	})]
	public sealed class GetDlpPolicy : GetMultitenancySystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x060055CA RID: 21962 RVA: 0x001610E5 File Offset: 0x0015F2E5
		public OptionalIdentityData IdentityData
		{
			get
			{
				return base.OptionalIdentityData;
			}
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x001610ED File Offset: 0x0015F2ED
		public GetDlpPolicy()
		{
			this.impl = new GetDlpPolicyImpl(this);
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x060055CC RID: 21964 RVA: 0x00161101 File Offset: 0x0015F301
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity != null)
				{
					return null;
				}
				return RuleIdParameter.GetRuleCollectionId(base.DataSession, DlpUtils.TenantDlpPoliciesCollectionName);
			}
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x0016111D File Offset: 0x0015F31D
		protected override void InternalValidate()
		{
			this.SetupImpl();
			this.impl.Validate();
			base.InternalValidate();
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x00161136 File Offset: 0x0015F336
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			this.SetupImpl();
			this.impl.WriteResult((IEnumerable<ADComplianceProgram>)dataObjects, new GetDlpPolicy.WriteDelegate(this.WriteResult));
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0016115C File Offset: 0x0015F35C
		private void SetupImpl()
		{
			this.impl.DataSession = base.DataSession;
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031C5 RID: 12741
		private readonly GetDlpPolicyImpl impl;

		// Token: 0x0200095F RID: 2399
		// (Invoke) Token: 0x060055D1 RID: 21969
		public delegate void WriteDelegate(IConfigurable obj);
	}
}

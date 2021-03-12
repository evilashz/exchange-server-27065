using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000961 RID: 2401
	[OutputType(new Type[]
	{
		typeof(DlpPolicyTemplate)
	})]
	[Cmdlet("Get", "DlpPolicyTemplate", DefaultParameterSetName = "Identity")]
	public sealed class GetDlpPolicyTemplate : GetSystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x00161300 File Offset: 0x0015F500
		public OptionalIdentityData IdentityData
		{
			get
			{
				return base.OptionalIdentityData;
			}
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x00161308 File Offset: 0x0015F508
		public GetDlpPolicyTemplate()
		{
			this.impl = new GetDlpPolicyTemplateImpl(this);
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x0016131C File Offset: 0x0015F51C
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 73, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\DlpPolicy\\GetDlpPolicyTemplate.cs");
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x0016133B File Offset: 0x0015F53B
		protected override void InternalValidate()
		{
			this.SetupImpl();
			this.impl.Validate();
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x0016134E File Offset: 0x0015F54E
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			this.SetupImpl();
			this.impl.WriteResult((IEnumerable<ADComplianceProgram>)dataObjects, new GetDlpPolicy.WriteDelegate(this.WriteResult));
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x00161374 File Offset: 0x0015F574
		private void SetupImpl()
		{
			this.impl.DataSession = base.DataSession;
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031C7 RID: 12743
		private GetDlpPolicyTemplateImpl impl;

		// Token: 0x02000962 RID: 2402
		// (Invoke) Token: 0x060055E0 RID: 21984
		public delegate void WriteDelegate(IConfigurable obj);
	}
}

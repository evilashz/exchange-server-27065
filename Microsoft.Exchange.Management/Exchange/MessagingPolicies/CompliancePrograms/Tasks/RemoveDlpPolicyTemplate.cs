using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200096D RID: 2413
	[Cmdlet("Remove", "DlpPolicyTemplate", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDlpPolicyTemplate : GetSystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x06005636 RID: 22070 RVA: 0x00162684 File Offset: 0x00160884
		public RemoveDlpPolicyTemplate()
		{
			this.impl = new RemoveDlpPolicyTemplateImpl(this);
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x00162698 File Offset: 0x00160898
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 53, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\DlpPolicy\\RemoveDlpPolicyTemplate.cs");
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06005638 RID: 22072 RVA: 0x001626B7 File Offset: 0x001608B7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUninstallDlpPolicyTemplate(this.Identity.RawIdentity);
			}
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x001626C9 File Offset: 0x001608C9
		protected override void InternalProcessRecord()
		{
			this.SetupImpl();
			this.impl.ProcessRecord();
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x001626DC File Offset: 0x001608DC
		protected override void InternalValidate()
		{
			this.SetupImpl();
			this.impl.Validate();
			base.InternalValidate();
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x001626F5 File Offset: 0x001608F5
		private void SetupImpl()
		{
			this.impl.DataSession = base.DataSession;
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031DB RID: 12763
		private RemoveDlpPolicyTemplateImpl impl;
	}
}

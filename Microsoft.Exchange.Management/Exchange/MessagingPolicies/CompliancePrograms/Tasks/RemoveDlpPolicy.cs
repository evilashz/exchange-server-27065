using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200096B RID: 2411
	[Cmdlet("Remove", "DlpPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDlpPolicy : RemoveSystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x0600562C RID: 22060 RVA: 0x001624FC File Offset: 0x001606FC
		internal ADComplianceProgram GetDataObject()
		{
			return base.DataObject;
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x00162504 File Offset: 0x00160704
		public RemoveDlpPolicy()
		{
			this.impl = new RemoveDlpPolicyImpl(this);
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x0600562E RID: 22062 RVA: 0x00162518 File Offset: 0x00160718
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUninstallDlpPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0016252A File Offset: 0x0016072A
		protected override void InternalProcessRecord()
		{
			this.SetupImpl();
			this.impl.ProcessRecord();
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x0016253D File Offset: 0x0016073D
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = DlpPolicyIdParameter.GetDlpPolicyCollectionRdn();
			}
			base.InternalValidate();
			this.SetupImpl();
			this.impl.Validate();
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0016256E File Offset: 0x0016076E
		private void SetupImpl()
		{
			this.impl.DataSession = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031D8 RID: 12760
		private RemoveDlpPolicyImpl impl;
	}
}

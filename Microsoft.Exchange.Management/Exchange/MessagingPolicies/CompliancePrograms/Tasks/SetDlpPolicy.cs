using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000969 RID: 2409
	[Cmdlet("Set", "DlpPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDlpPolicy : SetSystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x06005618 RID: 22040 RVA: 0x00162124 File Offset: 0x00160324
		public SetDlpPolicy()
		{
			this.impl = new SetDlpPolicyImpl(this);
		}

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x06005619 RID: 22041 RVA: 0x00162138 File Offset: 0x00160338
		// (set) Token: 0x0600561A RID: 22042 RVA: 0x0016214F File Offset: 0x0016034F
		[Parameter(Mandatory = false)]
		public RuleState State
		{
			get
			{
				return (RuleState)base.Fields["State"];
			}
			set
			{
				base.Fields["State"] = value;
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x0600561B RID: 22043 RVA: 0x00162167 File Offset: 0x00160367
		// (set) Token: 0x0600561C RID: 22044 RVA: 0x0016217E File Offset: 0x0016037E
		[Parameter(Mandatory = false)]
		public RuleMode Mode
		{
			get
			{
				return (RuleMode)base.Fields["Mode"];
			}
			set
			{
				base.Fields["Mode"] = value;
			}
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x00162196 File Offset: 0x00160396
		// (set) Token: 0x0600561E RID: 22046 RVA: 0x001621AD File Offset: 0x001603AD
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x001621C0 File Offset: 0x001603C0
		// (set) Token: 0x06005620 RID: 22048 RVA: 0x001621C8 File Offset: 0x001603C8
		internal ADComplianceProgram TargetItem { get; set; }

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06005621 RID: 22049 RVA: 0x001621D1 File Offset: 0x001603D1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDlpPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x001621E3 File Offset: 0x001603E3
		protected override void InternalProcessRecord()
		{
			this.SetupImpl();
			this.impl.ProcessRecord();
			base.InternalProcessRecord();
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x001621FC File Offset: 0x001603FC
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = DlpPolicyIdParameter.GetDlpPolicyCollectionRdn();
			}
			base.InternalValidate();
			this.TargetItem = this.DataObject;
			this.impl.Validate();
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x00162233 File Offset: 0x00160433
		private void SetupImpl()
		{
			this.impl.DataSession = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031D3 RID: 12755
		private SetDlpPolicyImpl impl;
	}
}

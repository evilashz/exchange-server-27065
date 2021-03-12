using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005CB RID: 1483
	public class GetDlpPolicyTemplateCommand : SyntheticCommandWithPipelineInput<DlpPolicyTemplate, DlpPolicyTemplate>
	{
		// Token: 0x06004D05 RID: 19717 RVA: 0x0007B2B8 File Offset: 0x000794B8
		private GetDlpPolicyTemplateCommand() : base("Get-DlpPolicyTemplate")
		{
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0007B2C5 File Offset: 0x000794C5
		public GetDlpPolicyTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0007B2D4 File Offset: 0x000794D4
		public virtual GetDlpPolicyTemplateCommand SetParameters(GetDlpPolicyTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0007B2DE File Offset: 0x000794DE
		public virtual GetDlpPolicyTemplateCommand SetParameters(GetDlpPolicyTemplateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005CC RID: 1484
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D28 RID: 11560
			// (set) Token: 0x06004D09 RID: 19721 RVA: 0x0007B2E8 File Offset: 0x000794E8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D29 RID: 11561
			// (set) Token: 0x06004D0A RID: 19722 RVA: 0x0007B2FB File Offset: 0x000794FB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D2A RID: 11562
			// (set) Token: 0x06004D0B RID: 19723 RVA: 0x0007B313 File Offset: 0x00079513
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D2B RID: 11563
			// (set) Token: 0x06004D0C RID: 19724 RVA: 0x0007B32B File Offset: 0x0007952B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D2C RID: 11564
			// (set) Token: 0x06004D0D RID: 19725 RVA: 0x0007B343 File Offset: 0x00079543
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005CD RID: 1485
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D2D RID: 11565
			// (set) Token: 0x06004D0F RID: 19727 RVA: 0x0007B363 File Offset: 0x00079563
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D2E RID: 11566
			// (set) Token: 0x06004D10 RID: 19728 RVA: 0x0007B381 File Offset: 0x00079581
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D2F RID: 11567
			// (set) Token: 0x06004D11 RID: 19729 RVA: 0x0007B394 File Offset: 0x00079594
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D30 RID: 11568
			// (set) Token: 0x06004D12 RID: 19730 RVA: 0x0007B3AC File Offset: 0x000795AC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D31 RID: 11569
			// (set) Token: 0x06004D13 RID: 19731 RVA: 0x0007B3C4 File Offset: 0x000795C4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D32 RID: 11570
			// (set) Token: 0x06004D14 RID: 19732 RVA: 0x0007B3DC File Offset: 0x000795DC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}

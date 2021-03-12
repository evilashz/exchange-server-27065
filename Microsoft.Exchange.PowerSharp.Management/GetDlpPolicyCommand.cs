using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005C8 RID: 1480
	public class GetDlpPolicyCommand : SyntheticCommandWithPipelineInput<DlpPolicy, DlpPolicy>
	{
		// Token: 0x06004CF2 RID: 19698 RVA: 0x0007B138 File Offset: 0x00079338
		private GetDlpPolicyCommand() : base("Get-DlpPolicy")
		{
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0007B145 File Offset: 0x00079345
		public GetDlpPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0007B154 File Offset: 0x00079354
		public virtual GetDlpPolicyCommand SetParameters(GetDlpPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0007B15E File Offset: 0x0007935E
		public virtual GetDlpPolicyCommand SetParameters(GetDlpPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005C9 RID: 1481
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D1B RID: 11547
			// (set) Token: 0x06004CF6 RID: 19702 RVA: 0x0007B168 File Offset: 0x00079368
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D1C RID: 11548
			// (set) Token: 0x06004CF7 RID: 19703 RVA: 0x0007B186 File Offset: 0x00079386
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D1D RID: 11549
			// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x0007B199 File Offset: 0x00079399
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D1E RID: 11550
			// (set) Token: 0x06004CF9 RID: 19705 RVA: 0x0007B1B1 File Offset: 0x000793B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D1F RID: 11551
			// (set) Token: 0x06004CFA RID: 19706 RVA: 0x0007B1C9 File Offset: 0x000793C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D20 RID: 11552
			// (set) Token: 0x06004CFB RID: 19707 RVA: 0x0007B1E1 File Offset: 0x000793E1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005CA RID: 1482
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D21 RID: 11553
			// (set) Token: 0x06004CFD RID: 19709 RVA: 0x0007B201 File Offset: 0x00079401
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D22 RID: 11554
			// (set) Token: 0x06004CFE RID: 19710 RVA: 0x0007B21F File Offset: 0x0007941F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D23 RID: 11555
			// (set) Token: 0x06004CFF RID: 19711 RVA: 0x0007B23D File Offset: 0x0007943D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D24 RID: 11556
			// (set) Token: 0x06004D00 RID: 19712 RVA: 0x0007B250 File Offset: 0x00079450
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D25 RID: 11557
			// (set) Token: 0x06004D01 RID: 19713 RVA: 0x0007B268 File Offset: 0x00079468
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D26 RID: 11558
			// (set) Token: 0x06004D02 RID: 19714 RVA: 0x0007B280 File Offset: 0x00079480
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D27 RID: 11559
			// (set) Token: 0x06004D03 RID: 19715 RVA: 0x0007B298 File Offset: 0x00079498
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

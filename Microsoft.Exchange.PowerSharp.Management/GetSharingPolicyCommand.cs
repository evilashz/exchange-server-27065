using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000686 RID: 1670
	public class GetSharingPolicyCommand : SyntheticCommandWithPipelineInput<SharingPolicy, SharingPolicy>
	{
		// Token: 0x060058D7 RID: 22743 RVA: 0x0008B0F3 File Offset: 0x000892F3
		private GetSharingPolicyCommand() : base("Get-SharingPolicy")
		{
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x0008B100 File Offset: 0x00089300
		public GetSharingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0008B10F File Offset: 0x0008930F
		public virtual GetSharingPolicyCommand SetParameters(GetSharingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0008B119 File Offset: 0x00089319
		public virtual GetSharingPolicyCommand SetParameters(GetSharingPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000687 RID: 1671
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003784 RID: 14212
			// (set) Token: 0x060058DB RID: 22747 RVA: 0x0008B123 File Offset: 0x00089323
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003785 RID: 14213
			// (set) Token: 0x060058DC RID: 22748 RVA: 0x0008B141 File Offset: 0x00089341
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003786 RID: 14214
			// (set) Token: 0x060058DD RID: 22749 RVA: 0x0008B154 File Offset: 0x00089354
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003787 RID: 14215
			// (set) Token: 0x060058DE RID: 22750 RVA: 0x0008B16C File Offset: 0x0008936C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003788 RID: 14216
			// (set) Token: 0x060058DF RID: 22751 RVA: 0x0008B184 File Offset: 0x00089384
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003789 RID: 14217
			// (set) Token: 0x060058E0 RID: 22752 RVA: 0x0008B19C File Offset: 0x0008939C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000688 RID: 1672
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700378A RID: 14218
			// (set) Token: 0x060058E2 RID: 22754 RVA: 0x0008B1BC File Offset: 0x000893BC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700378B RID: 14219
			// (set) Token: 0x060058E3 RID: 22755 RVA: 0x0008B1DA File Offset: 0x000893DA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700378C RID: 14220
			// (set) Token: 0x060058E4 RID: 22756 RVA: 0x0008B1F8 File Offset: 0x000893F8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700378D RID: 14221
			// (set) Token: 0x060058E5 RID: 22757 RVA: 0x0008B20B File Offset: 0x0008940B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700378E RID: 14222
			// (set) Token: 0x060058E6 RID: 22758 RVA: 0x0008B223 File Offset: 0x00089423
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700378F RID: 14223
			// (set) Token: 0x060058E7 RID: 22759 RVA: 0x0008B23B File Offset: 0x0008943B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003790 RID: 14224
			// (set) Token: 0x060058E8 RID: 22760 RVA: 0x0008B253 File Offset: 0x00089453
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

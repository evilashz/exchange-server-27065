using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006D4 RID: 1748
	public class GetIntraOrganizationConnectorCommand : SyntheticCommandWithPipelineInput<IntraOrganizationConnector, IntraOrganizationConnector>
	{
		// Token: 0x06005B68 RID: 23400 RVA: 0x0008E45B File Offset: 0x0008C65B
		private GetIntraOrganizationConnectorCommand() : base("Get-IntraOrganizationConnector")
		{
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0008E468 File Offset: 0x0008C668
		public GetIntraOrganizationConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0008E477 File Offset: 0x0008C677
		public virtual GetIntraOrganizationConnectorCommand SetParameters(GetIntraOrganizationConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0008E481 File Offset: 0x0008C681
		public virtual GetIntraOrganizationConnectorCommand SetParameters(GetIntraOrganizationConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006D5 RID: 1749
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003979 RID: 14713
			// (set) Token: 0x06005B6C RID: 23404 RVA: 0x0008E48B File Offset: 0x0008C68B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700397A RID: 14714
			// (set) Token: 0x06005B6D RID: 23405 RVA: 0x0008E4A9 File Offset: 0x0008C6A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700397B RID: 14715
			// (set) Token: 0x06005B6E RID: 23406 RVA: 0x0008E4BC File Offset: 0x0008C6BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700397C RID: 14716
			// (set) Token: 0x06005B6F RID: 23407 RVA: 0x0008E4D4 File Offset: 0x0008C6D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700397D RID: 14717
			// (set) Token: 0x06005B70 RID: 23408 RVA: 0x0008E4EC File Offset: 0x0008C6EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700397E RID: 14718
			// (set) Token: 0x06005B71 RID: 23409 RVA: 0x0008E504 File Offset: 0x0008C704
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020006D6 RID: 1750
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700397F RID: 14719
			// (set) Token: 0x06005B73 RID: 23411 RVA: 0x0008E524 File Offset: 0x0008C724
			public virtual IntraOrganizationConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003980 RID: 14720
			// (set) Token: 0x06005B74 RID: 23412 RVA: 0x0008E537 File Offset: 0x0008C737
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003981 RID: 14721
			// (set) Token: 0x06005B75 RID: 23413 RVA: 0x0008E555 File Offset: 0x0008C755
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003982 RID: 14722
			// (set) Token: 0x06005B76 RID: 23414 RVA: 0x0008E568 File Offset: 0x0008C768
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003983 RID: 14723
			// (set) Token: 0x06005B77 RID: 23415 RVA: 0x0008E580 File Offset: 0x0008C780
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003984 RID: 14724
			// (set) Token: 0x06005B78 RID: 23416 RVA: 0x0008E598 File Offset: 0x0008C798
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003985 RID: 14725
			// (set) Token: 0x06005B79 RID: 23417 RVA: 0x0008E5B0 File Offset: 0x0008C7B0
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

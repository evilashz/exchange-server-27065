using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006A8 RID: 1704
	public class SetPendingFederatedDomainCommand : SyntheticCommandWithPipelineInputNoOutput<FederatedOrganizationId>
	{
		// Token: 0x06005A18 RID: 23064 RVA: 0x0008CA5F File Offset: 0x0008AC5F
		private SetPendingFederatedDomainCommand() : base("Set-PendingFederatedDomain")
		{
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x0008CA6C File Offset: 0x0008AC6C
		public SetPendingFederatedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x0008CA7B File Offset: 0x0008AC7B
		public virtual SetPendingFederatedDomainCommand SetParameters(SetPendingFederatedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005A1B RID: 23067 RVA: 0x0008CA85 File Offset: 0x0008AC85
		public virtual SetPendingFederatedDomainCommand SetParameters(SetPendingFederatedDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006A9 RID: 1705
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003881 RID: 14465
			// (set) Token: 0x06005A1C RID: 23068 RVA: 0x0008CA8F File Offset: 0x0008AC8F
			public virtual SmtpDomain PendingAccountNamespace
			{
				set
				{
					base.PowerSharpParameters["PendingAccountNamespace"] = value;
				}
			}

			// Token: 0x17003882 RID: 14466
			// (set) Token: 0x06005A1D RID: 23069 RVA: 0x0008CAA2 File Offset: 0x0008ACA2
			public virtual SmtpDomain PendingDomains
			{
				set
				{
					base.PowerSharpParameters["PendingDomains"] = value;
				}
			}

			// Token: 0x17003883 RID: 14467
			// (set) Token: 0x06005A1E RID: 23070 RVA: 0x0008CAB5 File Offset: 0x0008ACB5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003884 RID: 14468
			// (set) Token: 0x06005A1F RID: 23071 RVA: 0x0008CAC8 File Offset: 0x0008ACC8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003885 RID: 14469
			// (set) Token: 0x06005A20 RID: 23072 RVA: 0x0008CAE0 File Offset: 0x0008ACE0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003886 RID: 14470
			// (set) Token: 0x06005A21 RID: 23073 RVA: 0x0008CAF8 File Offset: 0x0008ACF8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003887 RID: 14471
			// (set) Token: 0x06005A22 RID: 23074 RVA: 0x0008CB10 File Offset: 0x0008AD10
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003888 RID: 14472
			// (set) Token: 0x06005A23 RID: 23075 RVA: 0x0008CB28 File Offset: 0x0008AD28
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006AA RID: 1706
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003889 RID: 14473
			// (set) Token: 0x06005A25 RID: 23077 RVA: 0x0008CB48 File Offset: 0x0008AD48
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700388A RID: 14474
			// (set) Token: 0x06005A26 RID: 23078 RVA: 0x0008CB66 File Offset: 0x0008AD66
			public virtual SmtpDomain PendingAccountNamespace
			{
				set
				{
					base.PowerSharpParameters["PendingAccountNamespace"] = value;
				}
			}

			// Token: 0x1700388B RID: 14475
			// (set) Token: 0x06005A27 RID: 23079 RVA: 0x0008CB79 File Offset: 0x0008AD79
			public virtual SmtpDomain PendingDomains
			{
				set
				{
					base.PowerSharpParameters["PendingDomains"] = value;
				}
			}

			// Token: 0x1700388C RID: 14476
			// (set) Token: 0x06005A28 RID: 23080 RVA: 0x0008CB8C File Offset: 0x0008AD8C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700388D RID: 14477
			// (set) Token: 0x06005A29 RID: 23081 RVA: 0x0008CB9F File Offset: 0x0008AD9F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700388E RID: 14478
			// (set) Token: 0x06005A2A RID: 23082 RVA: 0x0008CBB7 File Offset: 0x0008ADB7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700388F RID: 14479
			// (set) Token: 0x06005A2B RID: 23083 RVA: 0x0008CBCF File Offset: 0x0008ADCF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003890 RID: 14480
			// (set) Token: 0x06005A2C RID: 23084 RVA: 0x0008CBE7 File Offset: 0x0008ADE7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003891 RID: 14481
			// (set) Token: 0x06005A2D RID: 23085 RVA: 0x0008CBFF File Offset: 0x0008ADFF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}

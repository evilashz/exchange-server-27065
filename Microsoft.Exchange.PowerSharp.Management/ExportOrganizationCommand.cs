using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BB1 RID: 2993
	public class ExportOrganizationCommand : SyntheticCommandWithPipelineInput<ExchangeConfigurationUnit, ExchangeConfigurationUnit>
	{
		// Token: 0x060090F7 RID: 37111 RVA: 0x000D3E6E File Offset: 0x000D206E
		private ExportOrganizationCommand() : base("Export-Organization")
		{
		}

		// Token: 0x060090F8 RID: 37112 RVA: 0x000D3E7B File Offset: 0x000D207B
		public ExportOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060090F9 RID: 37113 RVA: 0x000D3E8A File Offset: 0x000D208A
		public virtual ExportOrganizationCommand SetParameters(ExportOrganizationCommand.CustomCredentialsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060090FA RID: 37114 RVA: 0x000D3E94 File Offset: 0x000D2094
		public virtual ExportOrganizationCommand SetParameters(ExportOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BB2 RID: 2994
		public class CustomCredentialsParameters : ParametersBase
		{
			// Token: 0x1700654E RID: 25934
			// (set) Token: 0x060090FB RID: 37115 RVA: 0x000D3E9E File Offset: 0x000D209E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700654F RID: 25935
			// (set) Token: 0x060090FC RID: 37116 RVA: 0x000D3EBC File Offset: 0x000D20BC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006550 RID: 25936
			// (set) Token: 0x060090FD RID: 37117 RVA: 0x000D3ECF File Offset: 0x000D20CF
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006551 RID: 25937
			// (set) Token: 0x060090FE RID: 37118 RVA: 0x000D3EE2 File Offset: 0x000D20E2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006552 RID: 25938
			// (set) Token: 0x060090FF RID: 37119 RVA: 0x000D3EFA File Offset: 0x000D20FA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006553 RID: 25939
			// (set) Token: 0x06009100 RID: 37120 RVA: 0x000D3F12 File Offset: 0x000D2112
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006554 RID: 25940
			// (set) Token: 0x06009101 RID: 37121 RVA: 0x000D3F2A File Offset: 0x000D212A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BB3 RID: 2995
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006555 RID: 25941
			// (set) Token: 0x06009103 RID: 37123 RVA: 0x000D3F4A File Offset: 0x000D214A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006556 RID: 25942
			// (set) Token: 0x06009104 RID: 37124 RVA: 0x000D3F68 File Offset: 0x000D2168
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006557 RID: 25943
			// (set) Token: 0x06009105 RID: 37125 RVA: 0x000D3F80 File Offset: 0x000D2180
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006558 RID: 25944
			// (set) Token: 0x06009106 RID: 37126 RVA: 0x000D3F98 File Offset: 0x000D2198
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006559 RID: 25945
			// (set) Token: 0x06009107 RID: 37127 RVA: 0x000D3FB0 File Offset: 0x000D21B0
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006D7 RID: 1751
	public class NewIntraOrganizationConnectorCommand : SyntheticCommandWithPipelineInput<IntraOrganizationConnector, IntraOrganizationConnector>
	{
		// Token: 0x06005B7B RID: 23419 RVA: 0x0008E5D0 File Offset: 0x0008C7D0
		private NewIntraOrganizationConnectorCommand() : base("New-IntraOrganizationConnector")
		{
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0008E5DD File Offset: 0x0008C7DD
		public NewIntraOrganizationConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x0008E5EC File Offset: 0x0008C7EC
		public virtual NewIntraOrganizationConnectorCommand SetParameters(NewIntraOrganizationConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006D8 RID: 1752
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003986 RID: 14726
			// (set) Token: 0x06005B7E RID: 23422 RVA: 0x0008E5F6 File Offset: 0x0008C7F6
			public virtual MultiValuedProperty<SmtpDomain> TargetAddressDomains
			{
				set
				{
					base.PowerSharpParameters["TargetAddressDomains"] = value;
				}
			}

			// Token: 0x17003987 RID: 14727
			// (set) Token: 0x06005B7F RID: 23423 RVA: 0x0008E609 File Offset: 0x0008C809
			public virtual Uri DiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["DiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x17003988 RID: 14728
			// (set) Token: 0x06005B80 RID: 23424 RVA: 0x0008E61C File Offset: 0x0008C81C
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003989 RID: 14729
			// (set) Token: 0x06005B81 RID: 23425 RVA: 0x0008E634 File Offset: 0x0008C834
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700398A RID: 14730
			// (set) Token: 0x06005B82 RID: 23426 RVA: 0x0008E652 File Offset: 0x0008C852
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700398B RID: 14731
			// (set) Token: 0x06005B83 RID: 23427 RVA: 0x0008E665 File Offset: 0x0008C865
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700398C RID: 14732
			// (set) Token: 0x06005B84 RID: 23428 RVA: 0x0008E678 File Offset: 0x0008C878
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700398D RID: 14733
			// (set) Token: 0x06005B85 RID: 23429 RVA: 0x0008E690 File Offset: 0x0008C890
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700398E RID: 14734
			// (set) Token: 0x06005B86 RID: 23430 RVA: 0x0008E6A8 File Offset: 0x0008C8A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700398F RID: 14735
			// (set) Token: 0x06005B87 RID: 23431 RVA: 0x0008E6C0 File Offset: 0x0008C8C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003990 RID: 14736
			// (set) Token: 0x06005B88 RID: 23432 RVA: 0x0008E6D8 File Offset: 0x0008C8D8
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

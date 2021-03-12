using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200067D RID: 1661
	public class GetFederationInformationCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x060058A5 RID: 22693 RVA: 0x0008AD45 File Offset: 0x00088F45
		private GetFederationInformationCommand() : base("Get-FederationInformation")
		{
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x0008AD52 File Offset: 0x00088F52
		public GetFederationInformationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x0008AD61 File Offset: 0x00088F61
		public virtual GetFederationInformationCommand SetParameters(GetFederationInformationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200067E RID: 1662
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003764 RID: 14180
			// (set) Token: 0x060058A8 RID: 22696 RVA: 0x0008AD6B File Offset: 0x00088F6B
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003765 RID: 14181
			// (set) Token: 0x060058A9 RID: 22697 RVA: 0x0008AD7E File Offset: 0x00088F7E
			public virtual MultiValuedProperty<string> TrustedHostnames
			{
				set
				{
					base.PowerSharpParameters["TrustedHostnames"] = value;
				}
			}

			// Token: 0x17003766 RID: 14182
			// (set) Token: 0x060058AA RID: 22698 RVA: 0x0008AD91 File Offset: 0x00088F91
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003767 RID: 14183
			// (set) Token: 0x060058AB RID: 22699 RVA: 0x0008ADA9 File Offset: 0x00088FA9
			public virtual SwitchParameter BypassAdditionalDomainValidation
			{
				set
				{
					base.PowerSharpParameters["BypassAdditionalDomainValidation"] = value;
				}
			}

			// Token: 0x17003768 RID: 14184
			// (set) Token: 0x060058AC RID: 22700 RVA: 0x0008ADC1 File Offset: 0x00088FC1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003769 RID: 14185
			// (set) Token: 0x060058AD RID: 22701 RVA: 0x0008ADD9 File Offset: 0x00088FD9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700376A RID: 14186
			// (set) Token: 0x060058AE RID: 22702 RVA: 0x0008ADF1 File Offset: 0x00088FF1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700376B RID: 14187
			// (set) Token: 0x060058AF RID: 22703 RVA: 0x0008AE09 File Offset: 0x00089009
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

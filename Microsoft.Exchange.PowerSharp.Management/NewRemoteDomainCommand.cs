using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007D9 RID: 2009
	public class NewRemoteDomainCommand : SyntheticCommandWithPipelineInput<DomainContentConfig, DomainContentConfig>
	{
		// Token: 0x06006432 RID: 25650 RVA: 0x00099645 File Offset: 0x00097845
		private NewRemoteDomainCommand() : base("New-RemoteDomain")
		{
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x00099652 File Offset: 0x00097852
		public NewRemoteDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x00099661 File Offset: 0x00097861
		public virtual NewRemoteDomainCommand SetParameters(NewRemoteDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007DA RID: 2010
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004039 RID: 16441
			// (set) Token: 0x06006435 RID: 25653 RVA: 0x0009966B File Offset: 0x0009786B
			public virtual SmtpDomainWithSubdomains DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700403A RID: 16442
			// (set) Token: 0x06006436 RID: 25654 RVA: 0x0009967E File Offset: 0x0009787E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700403B RID: 16443
			// (set) Token: 0x06006437 RID: 25655 RVA: 0x0009969C File Offset: 0x0009789C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700403C RID: 16444
			// (set) Token: 0x06006438 RID: 25656 RVA: 0x000996AF File Offset: 0x000978AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700403D RID: 16445
			// (set) Token: 0x06006439 RID: 25657 RVA: 0x000996C2 File Offset: 0x000978C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700403E RID: 16446
			// (set) Token: 0x0600643A RID: 25658 RVA: 0x000996DA File Offset: 0x000978DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700403F RID: 16447
			// (set) Token: 0x0600643B RID: 25659 RVA: 0x000996F2 File Offset: 0x000978F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004040 RID: 16448
			// (set) Token: 0x0600643C RID: 25660 RVA: 0x0009970A File Offset: 0x0009790A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004041 RID: 16449
			// (set) Token: 0x0600643D RID: 25661 RVA: 0x00099722 File Offset: 0x00097922
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

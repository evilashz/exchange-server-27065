using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000648 RID: 1608
	public class GetTrustCommand : SyntheticCommandWithPipelineInput<ADDomainTrustInfo, ADDomainTrustInfo>
	{
		// Token: 0x0600511E RID: 20766 RVA: 0x000804BB File Offset: 0x0007E6BB
		private GetTrustCommand() : base("Get-Trust")
		{
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x000804C8 File Offset: 0x0007E6C8
		public GetTrustCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x000804D7 File Offset: 0x0007E6D7
		public virtual GetTrustCommand SetParameters(GetTrustCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000649 RID: 1609
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003047 RID: 12359
			// (set) Token: 0x06005121 RID: 20769 RVA: 0x000804E1 File Offset: 0x0007E6E1
			public virtual Fqdn DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003048 RID: 12360
			// (set) Token: 0x06005122 RID: 20770 RVA: 0x000804F4 File Offset: 0x0007E6F4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003049 RID: 12361
			// (set) Token: 0x06005123 RID: 20771 RVA: 0x0008050C File Offset: 0x0007E70C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700304A RID: 12362
			// (set) Token: 0x06005124 RID: 20772 RVA: 0x00080524 File Offset: 0x0007E724
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700304B RID: 12363
			// (set) Token: 0x06005125 RID: 20773 RVA: 0x0008053C File Offset: 0x0007E73C
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007DB RID: 2011
	public class RemoveRemoteDomainCommand : SyntheticCommandWithPipelineInput<DomainContentConfig, DomainContentConfig>
	{
		// Token: 0x0600643F RID: 25663 RVA: 0x00099742 File Offset: 0x00097942
		private RemoveRemoteDomainCommand() : base("Remove-RemoteDomain")
		{
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x0009974F File Offset: 0x0009794F
		public RemoveRemoteDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x0009975E File Offset: 0x0009795E
		public virtual RemoveRemoteDomainCommand SetParameters(RemoveRemoteDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x00099768 File Offset: 0x00097968
		public virtual RemoveRemoteDomainCommand SetParameters(RemoveRemoteDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007DC RID: 2012
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004042 RID: 16450
			// (set) Token: 0x06006443 RID: 25667 RVA: 0x00099772 File Offset: 0x00097972
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004043 RID: 16451
			// (set) Token: 0x06006444 RID: 25668 RVA: 0x00099785 File Offset: 0x00097985
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004044 RID: 16452
			// (set) Token: 0x06006445 RID: 25669 RVA: 0x0009979D File Offset: 0x0009799D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004045 RID: 16453
			// (set) Token: 0x06006446 RID: 25670 RVA: 0x000997B5 File Offset: 0x000979B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004046 RID: 16454
			// (set) Token: 0x06006447 RID: 25671 RVA: 0x000997CD File Offset: 0x000979CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004047 RID: 16455
			// (set) Token: 0x06006448 RID: 25672 RVA: 0x000997E5 File Offset: 0x000979E5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004048 RID: 16456
			// (set) Token: 0x06006449 RID: 25673 RVA: 0x000997FD File Offset: 0x000979FD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007DD RID: 2013
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004049 RID: 16457
			// (set) Token: 0x0600644B RID: 25675 RVA: 0x0009981D File Offset: 0x00097A1D
			public virtual RemoteDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700404A RID: 16458
			// (set) Token: 0x0600644C RID: 25676 RVA: 0x00099830 File Offset: 0x00097A30
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700404B RID: 16459
			// (set) Token: 0x0600644D RID: 25677 RVA: 0x00099843 File Offset: 0x00097A43
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700404C RID: 16460
			// (set) Token: 0x0600644E RID: 25678 RVA: 0x0009985B File Offset: 0x00097A5B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700404D RID: 16461
			// (set) Token: 0x0600644F RID: 25679 RVA: 0x00099873 File Offset: 0x00097A73
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700404E RID: 16462
			// (set) Token: 0x06006450 RID: 25680 RVA: 0x0009988B File Offset: 0x00097A8B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700404F RID: 16463
			// (set) Token: 0x06006451 RID: 25681 RVA: 0x000998A3 File Offset: 0x00097AA3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004050 RID: 16464
			// (set) Token: 0x06006452 RID: 25682 RVA: 0x000998BB File Offset: 0x00097ABB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}

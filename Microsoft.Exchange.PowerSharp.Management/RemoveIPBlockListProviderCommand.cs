using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200076A RID: 1898
	public class RemoveIPBlockListProviderCommand : SyntheticCommandWithPipelineInput<IPBlockListProvider, IPBlockListProvider>
	{
		// Token: 0x0600604F RID: 24655 RVA: 0x000947B1 File Offset: 0x000929B1
		private RemoveIPBlockListProviderCommand() : base("Remove-IPBlockListProvider")
		{
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x000947BE File Offset: 0x000929BE
		public RemoveIPBlockListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x000947CD File Offset: 0x000929CD
		public virtual RemoveIPBlockListProviderCommand SetParameters(RemoveIPBlockListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x000947D7 File Offset: 0x000929D7
		public virtual RemoveIPBlockListProviderCommand SetParameters(RemoveIPBlockListProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200076B RID: 1899
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D34 RID: 15668
			// (set) Token: 0x06006053 RID: 24659 RVA: 0x000947E1 File Offset: 0x000929E1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D35 RID: 15669
			// (set) Token: 0x06006054 RID: 24660 RVA: 0x000947F4 File Offset: 0x000929F4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D36 RID: 15670
			// (set) Token: 0x06006055 RID: 24661 RVA: 0x0009480C File Offset: 0x00092A0C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D37 RID: 15671
			// (set) Token: 0x06006056 RID: 24662 RVA: 0x00094824 File Offset: 0x00092A24
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D38 RID: 15672
			// (set) Token: 0x06006057 RID: 24663 RVA: 0x0009483C File Offset: 0x00092A3C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D39 RID: 15673
			// (set) Token: 0x06006058 RID: 24664 RVA: 0x00094854 File Offset: 0x00092A54
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003D3A RID: 15674
			// (set) Token: 0x06006059 RID: 24665 RVA: 0x0009486C File Offset: 0x00092A6C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200076C RID: 1900
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003D3B RID: 15675
			// (set) Token: 0x0600605B RID: 24667 RVA: 0x0009488C File Offset: 0x00092A8C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPBlockListProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17003D3C RID: 15676
			// (set) Token: 0x0600605C RID: 24668 RVA: 0x000948AA File Offset: 0x00092AAA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D3D RID: 15677
			// (set) Token: 0x0600605D RID: 24669 RVA: 0x000948BD File Offset: 0x00092ABD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D3E RID: 15678
			// (set) Token: 0x0600605E RID: 24670 RVA: 0x000948D5 File Offset: 0x00092AD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D3F RID: 15679
			// (set) Token: 0x0600605F RID: 24671 RVA: 0x000948ED File Offset: 0x00092AED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D40 RID: 15680
			// (set) Token: 0x06006060 RID: 24672 RVA: 0x00094905 File Offset: 0x00092B05
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D41 RID: 15681
			// (set) Token: 0x06006061 RID: 24673 RVA: 0x0009491D File Offset: 0x00092B1D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003D42 RID: 15682
			// (set) Token: 0x06006062 RID: 24674 RVA: 0x00094935 File Offset: 0x00092B35
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

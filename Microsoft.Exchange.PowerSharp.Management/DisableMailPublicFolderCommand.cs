using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200022D RID: 557
	public class DisableMailPublicFolderCommand : SyntheticCommandWithPipelineInput<ADPublicFolder, ADPublicFolder>
	{
		// Token: 0x06002AA9 RID: 10921 RVA: 0x0004F200 File Offset: 0x0004D400
		private DisableMailPublicFolderCommand() : base("Disable-MailPublicFolder")
		{
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0004F20D File Offset: 0x0004D40D
		public DisableMailPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0004F21C File Offset: 0x0004D41C
		public virtual DisableMailPublicFolderCommand SetParameters(DisableMailPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0004F226 File Offset: 0x0004D426
		public virtual DisableMailPublicFolderCommand SetParameters(DisableMailPublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200022E RID: 558
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001208 RID: 4616
			// (set) Token: 0x06002AAD RID: 10925 RVA: 0x0004F230 File Offset: 0x0004D430
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001209 RID: 4617
			// (set) Token: 0x06002AAE RID: 10926 RVA: 0x0004F243 File Offset: 0x0004D443
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700120A RID: 4618
			// (set) Token: 0x06002AAF RID: 10927 RVA: 0x0004F25B File Offset: 0x0004D45B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700120B RID: 4619
			// (set) Token: 0x06002AB0 RID: 10928 RVA: 0x0004F273 File Offset: 0x0004D473
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700120C RID: 4620
			// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x0004F28B File Offset: 0x0004D48B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700120D RID: 4621
			// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x0004F2A3 File Offset: 0x0004D4A3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700120E RID: 4622
			// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x0004F2BB File Offset: 0x0004D4BB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200022F RID: 559
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700120F RID: 4623
			// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x0004F2DB File Offset: 0x0004D4DB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailPublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001210 RID: 4624
			// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x0004F2F9 File Offset: 0x0004D4F9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001211 RID: 4625
			// (set) Token: 0x06002AB7 RID: 10935 RVA: 0x0004F30C File Offset: 0x0004D50C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001212 RID: 4626
			// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x0004F324 File Offset: 0x0004D524
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001213 RID: 4627
			// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x0004F33C File Offset: 0x0004D53C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001214 RID: 4628
			// (set) Token: 0x06002ABA RID: 10938 RVA: 0x0004F354 File Offset: 0x0004D554
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001215 RID: 4629
			// (set) Token: 0x06002ABB RID: 10939 RVA: 0x0004F36C File Offset: 0x0004D56C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001216 RID: 4630
			// (set) Token: 0x06002ABC RID: 10940 RVA: 0x0004F384 File Offset: 0x0004D584
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000230 RID: 560
	public class EnableMailPublicFolderCommand : SyntheticCommandWithPipelineInput<ADPublicFolder, ADPublicFolder>
	{
		// Token: 0x06002ABE RID: 10942 RVA: 0x0004F3A4 File Offset: 0x0004D5A4
		private EnableMailPublicFolderCommand() : base("Enable-MailPublicFolder")
		{
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0004F3B1 File Offset: 0x0004D5B1
		public EnableMailPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0004F3C0 File Offset: 0x0004D5C0
		public virtual EnableMailPublicFolderCommand SetParameters(EnableMailPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0004F3CA File Offset: 0x0004D5CA
		public virtual EnableMailPublicFolderCommand SetParameters(EnableMailPublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000231 RID: 561
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001217 RID: 4631
			// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17001218 RID: 4632
			// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x0004F3EC File Offset: 0x0004D5EC
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17001219 RID: 4633
			// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x0004F404 File Offset: 0x0004D604
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700121A RID: 4634
			// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x0004F417 File Offset: 0x0004D617
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700121B RID: 4635
			// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x0004F42F File Offset: 0x0004D62F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700121C RID: 4636
			// (set) Token: 0x06002AC7 RID: 10951 RVA: 0x0004F447 File Offset: 0x0004D647
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700121D RID: 4637
			// (set) Token: 0x06002AC8 RID: 10952 RVA: 0x0004F45F File Offset: 0x0004D65F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700121E RID: 4638
			// (set) Token: 0x06002AC9 RID: 10953 RVA: 0x0004F477 File Offset: 0x0004D677
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000232 RID: 562
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700121F RID: 4639
			// (set) Token: 0x06002ACB RID: 10955 RVA: 0x0004F497 File Offset: 0x0004D697
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001220 RID: 4640
			// (set) Token: 0x06002ACC RID: 10956 RVA: 0x0004F4B5 File Offset: 0x0004D6B5
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17001221 RID: 4641
			// (set) Token: 0x06002ACD RID: 10957 RVA: 0x0004F4CD File Offset: 0x0004D6CD
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17001222 RID: 4642
			// (set) Token: 0x06002ACE RID: 10958 RVA: 0x0004F4E5 File Offset: 0x0004D6E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001223 RID: 4643
			// (set) Token: 0x06002ACF RID: 10959 RVA: 0x0004F4F8 File Offset: 0x0004D6F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001224 RID: 4644
			// (set) Token: 0x06002AD0 RID: 10960 RVA: 0x0004F510 File Offset: 0x0004D710
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001225 RID: 4645
			// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x0004F528 File Offset: 0x0004D728
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001226 RID: 4646
			// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x0004F540 File Offset: 0x0004D740
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001227 RID: 4647
			// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x0004F558 File Offset: 0x0004D758
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

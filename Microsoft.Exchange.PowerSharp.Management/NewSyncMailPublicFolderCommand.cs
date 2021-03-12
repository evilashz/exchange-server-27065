using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000247 RID: 583
	public class NewSyncMailPublicFolderCommand : SyntheticCommandWithPipelineInput<ADPublicFolder, ADPublicFolder>
	{
		// Token: 0x06002B87 RID: 11143 RVA: 0x0005041D File Offset: 0x0004E61D
		private NewSyncMailPublicFolderCommand() : base("New-SyncMailPublicFolder")
		{
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0005042A File Offset: 0x0004E62A
		public NewSyncMailPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00050439 File Offset: 0x0004E639
		public virtual NewSyncMailPublicFolderCommand SetParameters(NewSyncMailPublicFolderCommand.SyncMailPublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00050443 File Offset: 0x0004E643
		public virtual NewSyncMailPublicFolderCommand SetParameters(NewSyncMailPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000248 RID: 584
		public class SyncMailPublicFolderParameters : ParametersBase
		{
			// Token: 0x170012B2 RID: 4786
			// (set) Token: 0x06002B8B RID: 11147 RVA: 0x0005044D File Offset: 0x0004E64D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170012B3 RID: 4787
			// (set) Token: 0x06002B8C RID: 11148 RVA: 0x00050460 File Offset: 0x0004E660
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170012B4 RID: 4788
			// (set) Token: 0x06002B8D RID: 11149 RVA: 0x00050473 File Offset: 0x0004E673
			public virtual SwitchParameter HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170012B5 RID: 4789
			// (set) Token: 0x06002B8E RID: 11150 RVA: 0x0005048B File Offset: 0x0004E68B
			public virtual string EntryId
			{
				set
				{
					base.PowerSharpParameters["EntryId"] = value;
				}
			}

			// Token: 0x170012B6 RID: 4790
			// (set) Token: 0x06002B8F RID: 11151 RVA: 0x0005049E File Offset: 0x0004E69E
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170012B7 RID: 4791
			// (set) Token: 0x06002B90 RID: 11152 RVA: 0x000504B6 File Offset: 0x0004E6B6
			public virtual SmtpAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170012B8 RID: 4792
			// (set) Token: 0x06002B91 RID: 11153 RVA: 0x000504CE File Offset: 0x0004E6CE
			public virtual ProxyAddress EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170012B9 RID: 4793
			// (set) Token: 0x06002B92 RID: 11154 RVA: 0x000504E1 File Offset: 0x0004E6E1
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170012BA RID: 4794
			// (set) Token: 0x06002B93 RID: 11155 RVA: 0x000504F9 File Offset: 0x0004E6F9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170012BB RID: 4795
			// (set) Token: 0x06002B94 RID: 11156 RVA: 0x00050517 File Offset: 0x0004E717
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012BC RID: 4796
			// (set) Token: 0x06002B95 RID: 11157 RVA: 0x0005052A File Offset: 0x0004E72A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170012BD RID: 4797
			// (set) Token: 0x06002B96 RID: 11158 RVA: 0x00050542 File Offset: 0x0004E742
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170012BE RID: 4798
			// (set) Token: 0x06002B97 RID: 11159 RVA: 0x0005055A File Offset: 0x0004E75A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170012BF RID: 4799
			// (set) Token: 0x06002B98 RID: 11160 RVA: 0x00050572 File Offset: 0x0004E772
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170012C0 RID: 4800
			// (set) Token: 0x06002B99 RID: 11161 RVA: 0x0005058A File Offset: 0x0004E78A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000249 RID: 585
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170012C1 RID: 4801
			// (set) Token: 0x06002B9B RID: 11163 RVA: 0x000505AA File Offset: 0x0004E7AA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170012C2 RID: 4802
			// (set) Token: 0x06002B9C RID: 11164 RVA: 0x000505C2 File Offset: 0x0004E7C2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170012C3 RID: 4803
			// (set) Token: 0x06002B9D RID: 11165 RVA: 0x000505E0 File Offset: 0x0004E7E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012C4 RID: 4804
			// (set) Token: 0x06002B9E RID: 11166 RVA: 0x000505F3 File Offset: 0x0004E7F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170012C5 RID: 4805
			// (set) Token: 0x06002B9F RID: 11167 RVA: 0x0005060B File Offset: 0x0004E80B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170012C6 RID: 4806
			// (set) Token: 0x06002BA0 RID: 11168 RVA: 0x00050623 File Offset: 0x0004E823
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x0005063B File Offset: 0x0004E83B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170012C8 RID: 4808
			// (set) Token: 0x06002BA2 RID: 11170 RVA: 0x00050653 File Offset: 0x0004E853
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

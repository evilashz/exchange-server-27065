using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007F8 RID: 2040
	public class NewOfflineAddressBookCommand : SyntheticCommandWithPipelineInputNoOutput<AddressBookBaseIdParameter>
	{
		// Token: 0x0600653F RID: 25919 RVA: 0x0009ABA8 File Offset: 0x00098DA8
		private NewOfflineAddressBookCommand() : base("New-OfflineAddressBook")
		{
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x0009ABB5 File Offset: 0x00098DB5
		public NewOfflineAddressBookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x0009ABC4 File Offset: 0x00098DC4
		public virtual NewOfflineAddressBookCommand SetParameters(NewOfflineAddressBookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007F9 RID: 2041
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004108 RID: 16648
			// (set) Token: 0x06006542 RID: 25922 RVA: 0x0009ABCE File Offset: 0x00098DCE
			public virtual AddressBookBaseIdParameter AddressLists
			{
				set
				{
					base.PowerSharpParameters["AddressLists"] = value;
				}
			}

			// Token: 0x17004109 RID: 16649
			// (set) Token: 0x06006543 RID: 25923 RVA: 0x0009ABE1 File Offset: 0x00098DE1
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x1700410A RID: 16650
			// (set) Token: 0x06006544 RID: 25924 RVA: 0x0009ABF9 File Offset: 0x00098DF9
			public virtual string GeneratingMailbox
			{
				set
				{
					base.PowerSharpParameters["GeneratingMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700410B RID: 16651
			// (set) Token: 0x06006545 RID: 25925 RVA: 0x0009AC17 File Offset: 0x00098E17
			public virtual bool GlobalWebDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalWebDistributionEnabled"] = value;
				}
			}

			// Token: 0x1700410C RID: 16652
			// (set) Token: 0x06006546 RID: 25926 RVA: 0x0009AC2F File Offset: 0x00098E2F
			public virtual bool ShadowMailboxDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["ShadowMailboxDistributionEnabled"] = value;
				}
			}

			// Token: 0x1700410D RID: 16653
			// (set) Token: 0x06006547 RID: 25927 RVA: 0x0009AC47 File Offset: 0x00098E47
			public virtual Unlimited<int>? DiffRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["DiffRetentionPeriod"] = value;
				}
			}

			// Token: 0x1700410E RID: 16654
			// (set) Token: 0x06006548 RID: 25928 RVA: 0x0009AC5F File Offset: 0x00098E5F
			public virtual VirtualDirectoryIdParameter VirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["VirtualDirectories"] = value;
				}
			}

			// Token: 0x1700410F RID: 16655
			// (set) Token: 0x06006549 RID: 25929 RVA: 0x0009AC72 File Offset: 0x00098E72
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004110 RID: 16656
			// (set) Token: 0x0600654A RID: 25930 RVA: 0x0009AC90 File Offset: 0x00098E90
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004111 RID: 16657
			// (set) Token: 0x0600654B RID: 25931 RVA: 0x0009ACA3 File Offset: 0x00098EA3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004112 RID: 16658
			// (set) Token: 0x0600654C RID: 25932 RVA: 0x0009ACB6 File Offset: 0x00098EB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004113 RID: 16659
			// (set) Token: 0x0600654D RID: 25933 RVA: 0x0009ACCE File Offset: 0x00098ECE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004114 RID: 16660
			// (set) Token: 0x0600654E RID: 25934 RVA: 0x0009ACE6 File Offset: 0x00098EE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004115 RID: 16661
			// (set) Token: 0x0600654F RID: 25935 RVA: 0x0009ACFE File Offset: 0x00098EFE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004116 RID: 16662
			// (set) Token: 0x06006550 RID: 25936 RVA: 0x0009AD16 File Offset: 0x00098F16
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B02 RID: 2818
	public class SetRecipientEnforcementProvisioningPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x06008A63 RID: 35427 RVA: 0x000CB662 File Offset: 0x000C9862
		private SetRecipientEnforcementProvisioningPolicyCommand() : base("Set-RecipientEnforcementProvisioningPolicy")
		{
		}

		// Token: 0x06008A64 RID: 35428 RVA: 0x000CB66F File Offset: 0x000C986F
		public SetRecipientEnforcementProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x000CB67E File Offset: 0x000C987E
		public virtual SetRecipientEnforcementProvisioningPolicyCommand SetParameters(SetRecipientEnforcementProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x000CB688 File Offset: 0x000C9888
		public virtual SetRecipientEnforcementProvisioningPolicyCommand SetParameters(SetRecipientEnforcementProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B03 RID: 2819
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006018 RID: 24600
			// (set) Token: 0x06008A67 RID: 35431 RVA: 0x000CB692 File Offset: 0x000C9892
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17006019 RID: 24601
			// (set) Token: 0x06008A68 RID: 35432 RVA: 0x000CB6AA File Offset: 0x000C98AA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700601A RID: 24602
			// (set) Token: 0x06008A69 RID: 35433 RVA: 0x000CB6BD File Offset: 0x000C98BD
			public virtual Unlimited<int> DistributionListCountQuota
			{
				set
				{
					base.PowerSharpParameters["DistributionListCountQuota"] = value;
				}
			}

			// Token: 0x1700601B RID: 24603
			// (set) Token: 0x06008A6A RID: 35434 RVA: 0x000CB6D5 File Offset: 0x000C98D5
			public virtual Unlimited<int> MailboxCountQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxCountQuota"] = value;
				}
			}

			// Token: 0x1700601C RID: 24604
			// (set) Token: 0x06008A6B RID: 35435 RVA: 0x000CB6ED File Offset: 0x000C98ED
			public virtual Unlimited<int> MailUserCountQuota
			{
				set
				{
					base.PowerSharpParameters["MailUserCountQuota"] = value;
				}
			}

			// Token: 0x1700601D RID: 24605
			// (set) Token: 0x06008A6C RID: 35436 RVA: 0x000CB705 File Offset: 0x000C9905
			public virtual Unlimited<int> ContactCountQuota
			{
				set
				{
					base.PowerSharpParameters["ContactCountQuota"] = value;
				}
			}

			// Token: 0x1700601E RID: 24606
			// (set) Token: 0x06008A6D RID: 35437 RVA: 0x000CB71D File Offset: 0x000C991D
			public virtual Unlimited<int> TeamMailboxCountQuota
			{
				set
				{
					base.PowerSharpParameters["TeamMailboxCountQuota"] = value;
				}
			}

			// Token: 0x1700601F RID: 24607
			// (set) Token: 0x06008A6E RID: 35438 RVA: 0x000CB735 File Offset: 0x000C9935
			public virtual Unlimited<int> PublicFolderMailboxCountQuota
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxCountQuota"] = value;
				}
			}

			// Token: 0x17006020 RID: 24608
			// (set) Token: 0x06008A6F RID: 35439 RVA: 0x000CB74D File Offset: 0x000C994D
			public virtual Unlimited<int> MailPublicFolderCountQuota
			{
				set
				{
					base.PowerSharpParameters["MailPublicFolderCountQuota"] = value;
				}
			}

			// Token: 0x17006021 RID: 24609
			// (set) Token: 0x06008A70 RID: 35440 RVA: 0x000CB765 File Offset: 0x000C9965
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006022 RID: 24610
			// (set) Token: 0x06008A71 RID: 35441 RVA: 0x000CB77D File Offset: 0x000C997D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006023 RID: 24611
			// (set) Token: 0x06008A72 RID: 35442 RVA: 0x000CB795 File Offset: 0x000C9995
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006024 RID: 24612
			// (set) Token: 0x06008A73 RID: 35443 RVA: 0x000CB7AD File Offset: 0x000C99AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006025 RID: 24613
			// (set) Token: 0x06008A74 RID: 35444 RVA: 0x000CB7C5 File Offset: 0x000C99C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B04 RID: 2820
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006026 RID: 24614
			// (set) Token: 0x06008A76 RID: 35446 RVA: 0x000CB7E5 File Offset: 0x000C99E5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientEnforcementProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006027 RID: 24615
			// (set) Token: 0x06008A77 RID: 35447 RVA: 0x000CB803 File Offset: 0x000C9A03
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17006028 RID: 24616
			// (set) Token: 0x06008A78 RID: 35448 RVA: 0x000CB81B File Offset: 0x000C9A1B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006029 RID: 24617
			// (set) Token: 0x06008A79 RID: 35449 RVA: 0x000CB82E File Offset: 0x000C9A2E
			public virtual Unlimited<int> DistributionListCountQuota
			{
				set
				{
					base.PowerSharpParameters["DistributionListCountQuota"] = value;
				}
			}

			// Token: 0x1700602A RID: 24618
			// (set) Token: 0x06008A7A RID: 35450 RVA: 0x000CB846 File Offset: 0x000C9A46
			public virtual Unlimited<int> MailboxCountQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxCountQuota"] = value;
				}
			}

			// Token: 0x1700602B RID: 24619
			// (set) Token: 0x06008A7B RID: 35451 RVA: 0x000CB85E File Offset: 0x000C9A5E
			public virtual Unlimited<int> MailUserCountQuota
			{
				set
				{
					base.PowerSharpParameters["MailUserCountQuota"] = value;
				}
			}

			// Token: 0x1700602C RID: 24620
			// (set) Token: 0x06008A7C RID: 35452 RVA: 0x000CB876 File Offset: 0x000C9A76
			public virtual Unlimited<int> ContactCountQuota
			{
				set
				{
					base.PowerSharpParameters["ContactCountQuota"] = value;
				}
			}

			// Token: 0x1700602D RID: 24621
			// (set) Token: 0x06008A7D RID: 35453 RVA: 0x000CB88E File Offset: 0x000C9A8E
			public virtual Unlimited<int> TeamMailboxCountQuota
			{
				set
				{
					base.PowerSharpParameters["TeamMailboxCountQuota"] = value;
				}
			}

			// Token: 0x1700602E RID: 24622
			// (set) Token: 0x06008A7E RID: 35454 RVA: 0x000CB8A6 File Offset: 0x000C9AA6
			public virtual Unlimited<int> PublicFolderMailboxCountQuota
			{
				set
				{
					base.PowerSharpParameters["PublicFolderMailboxCountQuota"] = value;
				}
			}

			// Token: 0x1700602F RID: 24623
			// (set) Token: 0x06008A7F RID: 35455 RVA: 0x000CB8BE File Offset: 0x000C9ABE
			public virtual Unlimited<int> MailPublicFolderCountQuota
			{
				set
				{
					base.PowerSharpParameters["MailPublicFolderCountQuota"] = value;
				}
			}

			// Token: 0x17006030 RID: 24624
			// (set) Token: 0x06008A80 RID: 35456 RVA: 0x000CB8D6 File Offset: 0x000C9AD6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006031 RID: 24625
			// (set) Token: 0x06008A81 RID: 35457 RVA: 0x000CB8EE File Offset: 0x000C9AEE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006032 RID: 24626
			// (set) Token: 0x06008A82 RID: 35458 RVA: 0x000CB906 File Offset: 0x000C9B06
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006033 RID: 24627
			// (set) Token: 0x06008A83 RID: 35459 RVA: 0x000CB91E File Offset: 0x000C9B1E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006034 RID: 24628
			// (set) Token: 0x06008A84 RID: 35460 RVA: 0x000CB936 File Offset: 0x000C9B36
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

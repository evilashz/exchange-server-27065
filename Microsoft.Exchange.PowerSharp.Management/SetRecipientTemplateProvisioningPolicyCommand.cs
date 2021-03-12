using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B05 RID: 2821
	public class SetRecipientTemplateProvisioningPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x06008A86 RID: 35462 RVA: 0x000CB956 File Offset: 0x000C9B56
		private SetRecipientTemplateProvisioningPolicyCommand() : base("Set-RecipientTemplateProvisioningPolicy")
		{
		}

		// Token: 0x06008A87 RID: 35463 RVA: 0x000CB963 File Offset: 0x000C9B63
		public SetRecipientTemplateProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A88 RID: 35464 RVA: 0x000CB972 File Offset: 0x000C9B72
		public virtual SetRecipientTemplateProvisioningPolicyCommand SetParameters(SetRecipientTemplateProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x000CB97C File Offset: 0x000C9B7C
		public virtual SetRecipientTemplateProvisioningPolicyCommand SetParameters(SetRecipientTemplateProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B06 RID: 2822
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006035 RID: 24629
			// (set) Token: 0x06008A8A RID: 35466 RVA: 0x000CB986 File Offset: 0x000C9B86
			public virtual string DefaultDistributionListOU
			{
				set
				{
					base.PowerSharpParameters["DefaultDistributionListOU"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006036 RID: 24630
			// (set) Token: 0x06008A8B RID: 35467 RVA: 0x000CB9A4 File Offset: 0x000C9BA4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006037 RID: 24631
			// (set) Token: 0x06008A8C RID: 35468 RVA: 0x000CB9B7 File Offset: 0x000C9BB7
			public virtual Unlimited<ByteQuantifiedSize> DefaultMaxSendSize
			{
				set
				{
					base.PowerSharpParameters["DefaultMaxSendSize"] = value;
				}
			}

			// Token: 0x17006038 RID: 24632
			// (set) Token: 0x06008A8D RID: 35469 RVA: 0x000CB9CF File Offset: 0x000C9BCF
			public virtual Unlimited<ByteQuantifiedSize> DefaultMaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["DefaultMaxReceiveSize"] = value;
				}
			}

			// Token: 0x17006039 RID: 24633
			// (set) Token: 0x06008A8E RID: 35470 RVA: 0x000CB9E7 File Offset: 0x000C9BE7
			public virtual Unlimited<ByteQuantifiedSize> DefaultProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultProhibitSendQuota"] = value;
				}
			}

			// Token: 0x1700603A RID: 24634
			// (set) Token: 0x06008A8F RID: 35471 RVA: 0x000CB9FF File Offset: 0x000C9BFF
			public virtual Unlimited<ByteQuantifiedSize> DefaultProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x1700603B RID: 24635
			// (set) Token: 0x06008A90 RID: 35472 RVA: 0x000CBA17 File Offset: 0x000C9C17
			public virtual Unlimited<ByteQuantifiedSize> DefaultIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultIssueWarningQuota"] = value;
				}
			}

			// Token: 0x1700603C RID: 24636
			// (set) Token: 0x06008A91 RID: 35473 RVA: 0x000CBA2F File Offset: 0x000C9C2F
			public virtual ByteQuantifiedSize? DefaultRulesQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultRulesQuota"] = value;
				}
			}

			// Token: 0x1700603D RID: 24637
			// (set) Token: 0x06008A92 RID: 35474 RVA: 0x000CBA47 File Offset: 0x000C9C47
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700603E RID: 24638
			// (set) Token: 0x06008A93 RID: 35475 RVA: 0x000CBA5F File Offset: 0x000C9C5F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700603F RID: 24639
			// (set) Token: 0x06008A94 RID: 35476 RVA: 0x000CBA77 File Offset: 0x000C9C77
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006040 RID: 24640
			// (set) Token: 0x06008A95 RID: 35477 RVA: 0x000CBA8F File Offset: 0x000C9C8F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006041 RID: 24641
			// (set) Token: 0x06008A96 RID: 35478 RVA: 0x000CBAA7 File Offset: 0x000C9CA7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B07 RID: 2823
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006042 RID: 24642
			// (set) Token: 0x06008A98 RID: 35480 RVA: 0x000CBAC7 File Offset: 0x000C9CC7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006043 RID: 24643
			// (set) Token: 0x06008A99 RID: 35481 RVA: 0x000CBAE5 File Offset: 0x000C9CE5
			public virtual string DefaultDistributionListOU
			{
				set
				{
					base.PowerSharpParameters["DefaultDistributionListOU"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006044 RID: 24644
			// (set) Token: 0x06008A9A RID: 35482 RVA: 0x000CBB03 File Offset: 0x000C9D03
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006045 RID: 24645
			// (set) Token: 0x06008A9B RID: 35483 RVA: 0x000CBB16 File Offset: 0x000C9D16
			public virtual Unlimited<ByteQuantifiedSize> DefaultMaxSendSize
			{
				set
				{
					base.PowerSharpParameters["DefaultMaxSendSize"] = value;
				}
			}

			// Token: 0x17006046 RID: 24646
			// (set) Token: 0x06008A9C RID: 35484 RVA: 0x000CBB2E File Offset: 0x000C9D2E
			public virtual Unlimited<ByteQuantifiedSize> DefaultMaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["DefaultMaxReceiveSize"] = value;
				}
			}

			// Token: 0x17006047 RID: 24647
			// (set) Token: 0x06008A9D RID: 35485 RVA: 0x000CBB46 File Offset: 0x000C9D46
			public virtual Unlimited<ByteQuantifiedSize> DefaultProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultProhibitSendQuota"] = value;
				}
			}

			// Token: 0x17006048 RID: 24648
			// (set) Token: 0x06008A9E RID: 35486 RVA: 0x000CBB5E File Offset: 0x000C9D5E
			public virtual Unlimited<ByteQuantifiedSize> DefaultProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17006049 RID: 24649
			// (set) Token: 0x06008A9F RID: 35487 RVA: 0x000CBB76 File Offset: 0x000C9D76
			public virtual Unlimited<ByteQuantifiedSize> DefaultIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultIssueWarningQuota"] = value;
				}
			}

			// Token: 0x1700604A RID: 24650
			// (set) Token: 0x06008AA0 RID: 35488 RVA: 0x000CBB8E File Offset: 0x000C9D8E
			public virtual ByteQuantifiedSize? DefaultRulesQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultRulesQuota"] = value;
				}
			}

			// Token: 0x1700604B RID: 24651
			// (set) Token: 0x06008AA1 RID: 35489 RVA: 0x000CBBA6 File Offset: 0x000C9DA6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700604C RID: 24652
			// (set) Token: 0x06008AA2 RID: 35490 RVA: 0x000CBBBE File Offset: 0x000C9DBE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700604D RID: 24653
			// (set) Token: 0x06008AA3 RID: 35491 RVA: 0x000CBBD6 File Offset: 0x000C9DD6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700604E RID: 24654
			// (set) Token: 0x06008AA4 RID: 35492 RVA: 0x000CBBEE File Offset: 0x000C9DEE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700604F RID: 24655
			// (set) Token: 0x06008AA5 RID: 35493 RVA: 0x000CBC06 File Offset: 0x000C9E06
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

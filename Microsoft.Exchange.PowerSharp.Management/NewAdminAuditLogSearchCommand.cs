using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000069 RID: 105
	public class NewAdminAuditLogSearchCommand : SyntheticCommandWithPipelineInput<AdminAuditLogSearch, AdminAuditLogSearch>
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x0003682D File Offset: 0x00034A2D
		private NewAdminAuditLogSearchCommand() : base("New-AdminAuditLogSearch")
		{
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0003683A File Offset: 0x00034A3A
		public NewAdminAuditLogSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00036849 File Offset: 0x00034A49
		public virtual NewAdminAuditLogSearchCommand SetParameters(NewAdminAuditLogSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00036853 File Offset: 0x00034A53
		public virtual NewAdminAuditLogSearchCommand SetParameters(NewAdminAuditLogSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200006A RID: 106
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170002A5 RID: 677
			// (set) Token: 0x060017C2 RID: 6082 RVA: 0x0003685D File Offset: 0x00034A5D
			public virtual MultiValuedProperty<string> Cmdlets
			{
				set
				{
					base.PowerSharpParameters["Cmdlets"] = value;
				}
			}

			// Token: 0x170002A6 RID: 678
			// (set) Token: 0x060017C3 RID: 6083 RVA: 0x00036870 File Offset: 0x00034A70
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x170002A7 RID: 679
			// (set) Token: 0x060017C4 RID: 6084 RVA: 0x00036883 File Offset: 0x00034A83
			public virtual MultiValuedProperty<string> ObjectIds
			{
				set
				{
					base.PowerSharpParameters["ObjectIds"] = value;
				}
			}

			// Token: 0x170002A8 RID: 680
			// (set) Token: 0x060017C5 RID: 6085 RVA: 0x00036896 File Offset: 0x00034A96
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> UserIds
			{
				set
				{
					base.PowerSharpParameters["UserIds"] = value;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (set) Token: 0x060017C6 RID: 6086 RVA: 0x000368A9 File Offset: 0x00034AA9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170002AA RID: 682
			// (set) Token: 0x060017C7 RID: 6087 RVA: 0x000368BC File Offset: 0x00034ABC
			public virtual ExDateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170002AB RID: 683
			// (set) Token: 0x060017C8 RID: 6088 RVA: 0x000368D4 File Offset: 0x00034AD4
			public virtual ExDateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170002AC RID: 684
			// (set) Token: 0x060017C9 RID: 6089 RVA: 0x000368EC File Offset: 0x00034AEC
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x170002AD RID: 685
			// (set) Token: 0x060017CA RID: 6090 RVA: 0x00036904 File Offset: 0x00034B04
			public virtual MultiValuedProperty<SmtpAddress> StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x170002AE RID: 686
			// (set) Token: 0x060017CB RID: 6091 RVA: 0x00036917 File Offset: 0x00034B17
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170002AF RID: 687
			// (set) Token: 0x060017CC RID: 6092 RVA: 0x0003692A File Offset: 0x00034B2A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002B0 RID: 688
			// (set) Token: 0x060017CD RID: 6093 RVA: 0x00036942 File Offset: 0x00034B42
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002B1 RID: 689
			// (set) Token: 0x060017CE RID: 6094 RVA: 0x0003695A File Offset: 0x00034B5A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002B2 RID: 690
			// (set) Token: 0x060017CF RID: 6095 RVA: 0x00036972 File Offset: 0x00034B72
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170002B3 RID: 691
			// (set) Token: 0x060017D0 RID: 6096 RVA: 0x0003698A File Offset: 0x00034B8A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200006B RID: 107
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170002B4 RID: 692
			// (set) Token: 0x060017D2 RID: 6098 RVA: 0x000369AA File Offset: 0x00034BAA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170002B5 RID: 693
			// (set) Token: 0x060017D3 RID: 6099 RVA: 0x000369C8 File Offset: 0x00034BC8
			public virtual MultiValuedProperty<string> Cmdlets
			{
				set
				{
					base.PowerSharpParameters["Cmdlets"] = value;
				}
			}

			// Token: 0x170002B6 RID: 694
			// (set) Token: 0x060017D4 RID: 6100 RVA: 0x000369DB File Offset: 0x00034BDB
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x170002B7 RID: 695
			// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000369EE File Offset: 0x00034BEE
			public virtual MultiValuedProperty<string> ObjectIds
			{
				set
				{
					base.PowerSharpParameters["ObjectIds"] = value;
				}
			}

			// Token: 0x170002B8 RID: 696
			// (set) Token: 0x060017D6 RID: 6102 RVA: 0x00036A01 File Offset: 0x00034C01
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> UserIds
			{
				set
				{
					base.PowerSharpParameters["UserIds"] = value;
				}
			}

			// Token: 0x170002B9 RID: 697
			// (set) Token: 0x060017D7 RID: 6103 RVA: 0x00036A14 File Offset: 0x00034C14
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170002BA RID: 698
			// (set) Token: 0x060017D8 RID: 6104 RVA: 0x00036A27 File Offset: 0x00034C27
			public virtual ExDateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170002BB RID: 699
			// (set) Token: 0x060017D9 RID: 6105 RVA: 0x00036A3F File Offset: 0x00034C3F
			public virtual ExDateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170002BC RID: 700
			// (set) Token: 0x060017DA RID: 6106 RVA: 0x00036A57 File Offset: 0x00034C57
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x170002BD RID: 701
			// (set) Token: 0x060017DB RID: 6107 RVA: 0x00036A6F File Offset: 0x00034C6F
			public virtual MultiValuedProperty<SmtpAddress> StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x170002BE RID: 702
			// (set) Token: 0x060017DC RID: 6108 RVA: 0x00036A82 File Offset: 0x00034C82
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170002BF RID: 703
			// (set) Token: 0x060017DD RID: 6109 RVA: 0x00036A95 File Offset: 0x00034C95
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002C0 RID: 704
			// (set) Token: 0x060017DE RID: 6110 RVA: 0x00036AAD File Offset: 0x00034CAD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002C1 RID: 705
			// (set) Token: 0x060017DF RID: 6111 RVA: 0x00036AC5 File Offset: 0x00034CC5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002C2 RID: 706
			// (set) Token: 0x060017E0 RID: 6112 RVA: 0x00036ADD File Offset: 0x00034CDD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170002C3 RID: 707
			// (set) Token: 0x060017E1 RID: 6113 RVA: 0x00036AF5 File Offset: 0x00034CF5
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

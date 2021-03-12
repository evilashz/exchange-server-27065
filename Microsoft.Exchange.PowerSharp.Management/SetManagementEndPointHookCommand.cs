using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ManagementEndpoint;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200020C RID: 524
	public class SetManagementEndPointHookCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x0600295A RID: 10586 RVA: 0x0004D6E1 File Offset: 0x0004B8E1
		private SetManagementEndPointHookCommand() : base("Set-ManagementEndPointHook")
		{
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0004D6EE File Offset: 0x0004B8EE
		public SetManagementEndPointHookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0004D6FD File Offset: 0x0004B8FD
		public virtual SetManagementEndPointHookCommand SetParameters(SetManagementEndPointHookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0004D707 File Offset: 0x0004B907
		public virtual SetManagementEndPointHookCommand SetParameters(SetManagementEndPointHookCommand.DomainParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0004D711 File Offset: 0x0004B911
		public virtual SetManagementEndPointHookCommand SetParameters(SetManagementEndPointHookCommand.OrganizationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0004D71B File Offset: 0x0004B91B
		public virtual SetManagementEndPointHookCommand SetParameters(SetManagementEndPointHookCommand.TenantFlagParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200020D RID: 525
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170010FB RID: 4347
			// (set) Token: 0x06002960 RID: 10592 RVA: 0x0004D725 File Offset: 0x0004B925
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170010FC RID: 4348
			// (set) Token: 0x06002961 RID: 10593 RVA: 0x0004D73D File Offset: 0x0004B93D
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x170010FD RID: 4349
			// (set) Token: 0x06002962 RID: 10594 RVA: 0x0004D755 File Offset: 0x0004B955
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010FE RID: 4350
			// (set) Token: 0x06002963 RID: 10595 RVA: 0x0004D76D File Offset: 0x0004B96D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010FF RID: 4351
			// (set) Token: 0x06002964 RID: 10596 RVA: 0x0004D785 File Offset: 0x0004B985
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001100 RID: 4352
			// (set) Token: 0x06002965 RID: 10597 RVA: 0x0004D79D File Offset: 0x0004B99D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001101 RID: 4353
			// (set) Token: 0x06002966 RID: 10598 RVA: 0x0004D7B5 File Offset: 0x0004B9B5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200020E RID: 526
		public class DomainParameters : ParametersBase
		{
			// Token: 0x17001102 RID: 4354
			// (set) Token: 0x06002968 RID: 10600 RVA: 0x0004D7D5 File Offset: 0x0004B9D5
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17001103 RID: 4355
			// (set) Token: 0x06002969 RID: 10601 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17001104 RID: 4356
			// (set) Token: 0x0600296A RID: 10602 RVA: 0x0004D800 File Offset: 0x0004BA00
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x17001105 RID: 4357
			// (set) Token: 0x0600296B RID: 10603 RVA: 0x0004D818 File Offset: 0x0004BA18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001106 RID: 4358
			// (set) Token: 0x0600296C RID: 10604 RVA: 0x0004D830 File Offset: 0x0004BA30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001107 RID: 4359
			// (set) Token: 0x0600296D RID: 10605 RVA: 0x0004D848 File Offset: 0x0004BA48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001108 RID: 4360
			// (set) Token: 0x0600296E RID: 10606 RVA: 0x0004D860 File Offset: 0x0004BA60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001109 RID: 4361
			// (set) Token: 0x0600296F RID: 10607 RVA: 0x0004D878 File Offset: 0x0004BA78
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200020F RID: 527
		public class OrganizationParameters : ParametersBase
		{
			// Token: 0x1700110A RID: 4362
			// (set) Token: 0x06002971 RID: 10609 RVA: 0x0004D898 File Offset: 0x0004BA98
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700110B RID: 4363
			// (set) Token: 0x06002972 RID: 10610 RVA: 0x0004D8AB File Offset: 0x0004BAAB
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x1700110C RID: 4364
			// (set) Token: 0x06002973 RID: 10611 RVA: 0x0004D8BE File Offset: 0x0004BABE
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x1700110D RID: 4365
			// (set) Token: 0x06002974 RID: 10612 RVA: 0x0004D8D6 File Offset: 0x0004BAD6
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x1700110E RID: 4366
			// (set) Token: 0x06002975 RID: 10613 RVA: 0x0004D8EE File Offset: 0x0004BAEE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700110F RID: 4367
			// (set) Token: 0x06002976 RID: 10614 RVA: 0x0004D906 File Offset: 0x0004BB06
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001110 RID: 4368
			// (set) Token: 0x06002977 RID: 10615 RVA: 0x0004D91E File Offset: 0x0004BB1E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001111 RID: 4369
			// (set) Token: 0x06002978 RID: 10616 RVA: 0x0004D936 File Offset: 0x0004BB36
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001112 RID: 4370
			// (set) Token: 0x06002979 RID: 10617 RVA: 0x0004D94E File Offset: 0x0004BB4E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000210 RID: 528
		public class TenantFlagParameters : ParametersBase
		{
			// Token: 0x17001113 RID: 4371
			// (set) Token: 0x0600297B RID: 10619 RVA: 0x0004D96E File Offset: 0x0004BB6E
			public virtual GlsTenantFlags? TenantFlag
			{
				set
				{
					base.PowerSharpParameters["TenantFlag"] = value;
				}
			}

			// Token: 0x17001114 RID: 4372
			// (set) Token: 0x0600297C RID: 10620 RVA: 0x0004D986 File Offset: 0x0004BB86
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17001115 RID: 4373
			// (set) Token: 0x0600297D RID: 10621 RVA: 0x0004D99E File Offset: 0x0004BB9E
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x17001116 RID: 4374
			// (set) Token: 0x0600297E RID: 10622 RVA: 0x0004D9B6 File Offset: 0x0004BBB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001117 RID: 4375
			// (set) Token: 0x0600297F RID: 10623 RVA: 0x0004D9CE File Offset: 0x0004BBCE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001118 RID: 4376
			// (set) Token: 0x06002980 RID: 10624 RVA: 0x0004D9E6 File Offset: 0x0004BBE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001119 RID: 4377
			// (set) Token: 0x06002981 RID: 10625 RVA: 0x0004D9FE File Offset: 0x0004BBFE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700111A RID: 4378
			// (set) Token: 0x06002982 RID: 10626 RVA: 0x0004DA16 File Offset: 0x0004BC16
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

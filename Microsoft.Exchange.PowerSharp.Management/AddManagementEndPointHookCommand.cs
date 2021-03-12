using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ManagementEndpoint;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000208 RID: 520
	public class AddManagementEndPointHookCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x06002938 RID: 10552 RVA: 0x0004D433 File Offset: 0x0004B633
		private AddManagementEndPointHookCommand() : base("Add-ManagementEndPointHook")
		{
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x0004D440 File Offset: 0x0004B640
		public AddManagementEndPointHookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0004D44F File Offset: 0x0004B64F
		public virtual AddManagementEndPointHookCommand SetParameters(AddManagementEndPointHookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0004D459 File Offset: 0x0004B659
		public virtual AddManagementEndPointHookCommand SetParameters(AddManagementEndPointHookCommand.DomainParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x0004D463 File Offset: 0x0004B663
		public virtual AddManagementEndPointHookCommand SetParameters(AddManagementEndPointHookCommand.OrganizationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000209 RID: 521
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170010E1 RID: 4321
			// (set) Token: 0x0600293D RID: 10557 RVA: 0x0004D46D File Offset: 0x0004B66D
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170010E2 RID: 4322
			// (set) Token: 0x0600293E RID: 10558 RVA: 0x0004D485 File Offset: 0x0004B685
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x170010E3 RID: 4323
			// (set) Token: 0x0600293F RID: 10559 RVA: 0x0004D49D File Offset: 0x0004B69D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010E4 RID: 4324
			// (set) Token: 0x06002940 RID: 10560 RVA: 0x0004D4B5 File Offset: 0x0004B6B5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010E5 RID: 4325
			// (set) Token: 0x06002941 RID: 10561 RVA: 0x0004D4CD File Offset: 0x0004B6CD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010E6 RID: 4326
			// (set) Token: 0x06002942 RID: 10562 RVA: 0x0004D4E5 File Offset: 0x0004B6E5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010E7 RID: 4327
			// (set) Token: 0x06002943 RID: 10563 RVA: 0x0004D4FD File Offset: 0x0004B6FD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200020A RID: 522
		public class DomainParameters : ParametersBase
		{
			// Token: 0x170010E8 RID: 4328
			// (set) Token: 0x06002945 RID: 10565 RVA: 0x0004D51D File Offset: 0x0004B71D
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170010E9 RID: 4329
			// (set) Token: 0x06002946 RID: 10566 RVA: 0x0004D530 File Offset: 0x0004B730
			public virtual bool InitialDomain
			{
				set
				{
					base.PowerSharpParameters["InitialDomain"] = value;
				}
			}

			// Token: 0x170010EA RID: 4330
			// (set) Token: 0x06002947 RID: 10567 RVA: 0x0004D548 File Offset: 0x0004B748
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170010EB RID: 4331
			// (set) Token: 0x06002948 RID: 10568 RVA: 0x0004D560 File Offset: 0x0004B760
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x170010EC RID: 4332
			// (set) Token: 0x06002949 RID: 10569 RVA: 0x0004D578 File Offset: 0x0004B778
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010ED RID: 4333
			// (set) Token: 0x0600294A RID: 10570 RVA: 0x0004D590 File Offset: 0x0004B790
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010EE RID: 4334
			// (set) Token: 0x0600294B RID: 10571 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010EF RID: 4335
			// (set) Token: 0x0600294C RID: 10572 RVA: 0x0004D5C0 File Offset: 0x0004B7C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010F0 RID: 4336
			// (set) Token: 0x0600294D RID: 10573 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200020B RID: 523
		public class OrganizationParameters : ParametersBase
		{
			// Token: 0x170010F1 RID: 4337
			// (set) Token: 0x0600294F RID: 10575 RVA: 0x0004D5F8 File Offset: 0x0004B7F8
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170010F2 RID: 4338
			// (set) Token: 0x06002950 RID: 10576 RVA: 0x0004D60B File Offset: 0x0004B80B
			public virtual SmtpDomain PopulateCacheWithDomainName
			{
				set
				{
					base.PowerSharpParameters["PopulateCacheWithDomainName"] = value;
				}
			}

			// Token: 0x170010F3 RID: 4339
			// (set) Token: 0x06002951 RID: 10577 RVA: 0x0004D61E File Offset: 0x0004B81E
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x170010F4 RID: 4340
			// (set) Token: 0x06002952 RID: 10578 RVA: 0x0004D631 File Offset: 0x0004B831
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170010F5 RID: 4341
			// (set) Token: 0x06002953 RID: 10579 RVA: 0x0004D649 File Offset: 0x0004B849
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x170010F6 RID: 4342
			// (set) Token: 0x06002954 RID: 10580 RVA: 0x0004D661 File Offset: 0x0004B861
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010F7 RID: 4343
			// (set) Token: 0x06002955 RID: 10581 RVA: 0x0004D679 File Offset: 0x0004B879
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010F8 RID: 4344
			// (set) Token: 0x06002956 RID: 10582 RVA: 0x0004D691 File Offset: 0x0004B891
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010F9 RID: 4345
			// (set) Token: 0x06002957 RID: 10583 RVA: 0x0004D6A9 File Offset: 0x0004B8A9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010FA RID: 4346
			// (set) Token: 0x06002958 RID: 10584 RVA: 0x0004D6C1 File Offset: 0x0004B8C1
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

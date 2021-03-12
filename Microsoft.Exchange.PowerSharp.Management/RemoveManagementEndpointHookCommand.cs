using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ManagementEndpoint;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000211 RID: 529
	public class RemoveManagementEndpointHookCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x06002984 RID: 10628 RVA: 0x0004DA36 File Offset: 0x0004BC36
		private RemoveManagementEndpointHookCommand() : base("Remove-ManagementEndpointHook")
		{
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0004DA43 File Offset: 0x0004BC43
		public RemoveManagementEndpointHookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0004DA52 File Offset: 0x0004BC52
		public virtual RemoveManagementEndpointHookCommand SetParameters(RemoveManagementEndpointHookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000212 RID: 530
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700111B RID: 4379
			// (set) Token: 0x06002987 RID: 10631 RVA: 0x0004DA5C File Offset: 0x0004BC5C
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x1700111C RID: 4380
			// (set) Token: 0x06002988 RID: 10632 RVA: 0x0004DA74 File Offset: 0x0004BC74
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700111D RID: 4381
			// (set) Token: 0x06002989 RID: 10633 RVA: 0x0004DA87 File Offset: 0x0004BC87
			public virtual GlobalDirectoryServiceType GlobalDirectoryService
			{
				set
				{
					base.PowerSharpParameters["GlobalDirectoryService"] = value;
				}
			}

			// Token: 0x1700111E RID: 4382
			// (set) Token: 0x0600298A RID: 10634 RVA: 0x0004DA9F File Offset: 0x0004BC9F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700111F RID: 4383
			// (set) Token: 0x0600298B RID: 10635 RVA: 0x0004DAB7 File Offset: 0x0004BCB7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001120 RID: 4384
			// (set) Token: 0x0600298C RID: 10636 RVA: 0x0004DACF File Offset: 0x0004BCCF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001121 RID: 4385
			// (set) Token: 0x0600298D RID: 10637 RVA: 0x0004DAE7 File Offset: 0x0004BCE7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001122 RID: 4386
			// (set) Token: 0x0600298E RID: 10638 RVA: 0x0004DAFF File Offset: 0x0004BCFF
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

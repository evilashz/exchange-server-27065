using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DED RID: 3565
	public class NewSyncGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x0600D497 RID: 54423 RVA: 0x0012E444 File Offset: 0x0012C644
		private NewSyncGroupCommand() : base("New-SyncGroup")
		{
		}

		// Token: 0x0600D498 RID: 54424 RVA: 0x0012E451 File Offset: 0x0012C651
		public NewSyncGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D499 RID: 54425 RVA: 0x0012E460 File Offset: 0x0012C660
		public virtual NewSyncGroupCommand SetParameters(NewSyncGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DEE RID: 3566
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A476 RID: 42102
			// (set) Token: 0x0600D49A RID: 54426 RVA: 0x0012E46A File Offset: 0x0012C66A
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700A477 RID: 42103
			// (set) Token: 0x0600D49B RID: 54427 RVA: 0x0012E482 File Offset: 0x0012C682
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A478 RID: 42104
			// (set) Token: 0x0600D49C RID: 54428 RVA: 0x0012E495 File Offset: 0x0012C695
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A479 RID: 42105
			// (set) Token: 0x0600D49D RID: 54429 RVA: 0x0012E4AD File Offset: 0x0012C6AD
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A47A RID: 42106
			// (set) Token: 0x0600D49E RID: 54430 RVA: 0x0012E4C0 File Offset: 0x0012C6C0
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700A47B RID: 42107
			// (set) Token: 0x0600D49F RID: 54431 RVA: 0x0012E4D3 File Offset: 0x0012C6D3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A47C RID: 42108
			// (set) Token: 0x0600D4A0 RID: 54432 RVA: 0x0012E4E6 File Offset: 0x0012C6E6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A47D RID: 42109
			// (set) Token: 0x0600D4A1 RID: 54433 RVA: 0x0012E4F9 File Offset: 0x0012C6F9
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A47E RID: 42110
			// (set) Token: 0x0600D4A2 RID: 54434 RVA: 0x0012E517 File Offset: 0x0012C717
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A47F RID: 42111
			// (set) Token: 0x0600D4A3 RID: 54435 RVA: 0x0012E52A File Offset: 0x0012C72A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A480 RID: 42112
			// (set) Token: 0x0600D4A4 RID: 54436 RVA: 0x0012E548 File Offset: 0x0012C748
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A481 RID: 42113
			// (set) Token: 0x0600D4A5 RID: 54437 RVA: 0x0012E55B File Offset: 0x0012C75B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A482 RID: 42114
			// (set) Token: 0x0600D4A6 RID: 54438 RVA: 0x0012E573 File Offset: 0x0012C773
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A483 RID: 42115
			// (set) Token: 0x0600D4A7 RID: 54439 RVA: 0x0012E58B File Offset: 0x0012C78B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A484 RID: 42116
			// (set) Token: 0x0600D4A8 RID: 54440 RVA: 0x0012E5A3 File Offset: 0x0012C7A3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A485 RID: 42117
			// (set) Token: 0x0600D4A9 RID: 54441 RVA: 0x0012E5BB File Offset: 0x0012C7BB
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

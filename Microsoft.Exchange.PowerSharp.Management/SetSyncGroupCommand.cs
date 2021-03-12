using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DEF RID: 3567
	public class SetSyncGroupCommand : SyntheticCommandWithPipelineInputNoOutput<SyncGroup>
	{
		// Token: 0x0600D4AB RID: 54443 RVA: 0x0012E5DB File Offset: 0x0012C7DB
		private SetSyncGroupCommand() : base("Set-SyncGroup")
		{
		}

		// Token: 0x0600D4AC RID: 54444 RVA: 0x0012E5E8 File Offset: 0x0012C7E8
		public SetSyncGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D4AD RID: 54445 RVA: 0x0012E5F7 File Offset: 0x0012C7F7
		public virtual SetSyncGroupCommand SetParameters(SetSyncGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D4AE RID: 54446 RVA: 0x0012E601 File Offset: 0x0012C801
		public virtual SetSyncGroupCommand SetParameters(SetSyncGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DF0 RID: 3568
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A486 RID: 42118
			// (set) Token: 0x0600D4AF RID: 54447 RVA: 0x0012E60B File Offset: 0x0012C80B
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700A487 RID: 42119
			// (set) Token: 0x0600D4B0 RID: 54448 RVA: 0x0012E623 File Offset: 0x0012C823
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A488 RID: 42120
			// (set) Token: 0x0600D4B1 RID: 54449 RVA: 0x0012E63B File Offset: 0x0012C83B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A489 RID: 42121
			// (set) Token: 0x0600D4B2 RID: 54450 RVA: 0x0012E64E File Offset: 0x0012C84E
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A48A RID: 42122
			// (set) Token: 0x0600D4B3 RID: 54451 RVA: 0x0012E666 File Offset: 0x0012C866
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A48B RID: 42123
			// (set) Token: 0x0600D4B4 RID: 54452 RVA: 0x0012E679 File Offset: 0x0012C879
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A48C RID: 42124
			// (set) Token: 0x0600D4B5 RID: 54453 RVA: 0x0012E68C File Offset: 0x0012C88C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A48D RID: 42125
			// (set) Token: 0x0600D4B6 RID: 54454 RVA: 0x0012E69F File Offset: 0x0012C89F
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A48E RID: 42126
			// (set) Token: 0x0600D4B7 RID: 54455 RVA: 0x0012E6B2 File Offset: 0x0012C8B2
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A48F RID: 42127
			// (set) Token: 0x0600D4B8 RID: 54456 RVA: 0x0012E6CA File Offset: 0x0012C8CA
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A490 RID: 42128
			// (set) Token: 0x0600D4B9 RID: 54457 RVA: 0x0012E6DD File Offset: 0x0012C8DD
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A491 RID: 42129
			// (set) Token: 0x0600D4BA RID: 54458 RVA: 0x0012E6F0 File Offset: 0x0012C8F0
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A492 RID: 42130
			// (set) Token: 0x0600D4BB RID: 54459 RVA: 0x0012E708 File Offset: 0x0012C908
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x1700A493 RID: 42131
			// (set) Token: 0x0600D4BC RID: 54460 RVA: 0x0012E720 File Offset: 0x0012C920
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A494 RID: 42132
			// (set) Token: 0x0600D4BD RID: 54461 RVA: 0x0012E733 File Offset: 0x0012C933
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A495 RID: 42133
			// (set) Token: 0x0600D4BE RID: 54462 RVA: 0x0012E74B File Offset: 0x0012C94B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A496 RID: 42134
			// (set) Token: 0x0600D4BF RID: 54463 RVA: 0x0012E763 File Offset: 0x0012C963
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A497 RID: 42135
			// (set) Token: 0x0600D4C0 RID: 54464 RVA: 0x0012E77B File Offset: 0x0012C97B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A498 RID: 42136
			// (set) Token: 0x0600D4C1 RID: 54465 RVA: 0x0012E793 File Offset: 0x0012C993
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DF1 RID: 3569
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A499 RID: 42137
			// (set) Token: 0x0600D4C3 RID: 54467 RVA: 0x0012E7B3 File Offset: 0x0012C9B3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NonMailEnabledGroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700A49A RID: 42138
			// (set) Token: 0x0600D4C4 RID: 54468 RVA: 0x0012E7D1 File Offset: 0x0012C9D1
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700A49B RID: 42139
			// (set) Token: 0x0600D4C5 RID: 54469 RVA: 0x0012E7E9 File Offset: 0x0012C9E9
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A49C RID: 42140
			// (set) Token: 0x0600D4C6 RID: 54470 RVA: 0x0012E801 File Offset: 0x0012CA01
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A49D RID: 42141
			// (set) Token: 0x0600D4C7 RID: 54471 RVA: 0x0012E814 File Offset: 0x0012CA14
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A49E RID: 42142
			// (set) Token: 0x0600D4C8 RID: 54472 RVA: 0x0012E82C File Offset: 0x0012CA2C
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A49F RID: 42143
			// (set) Token: 0x0600D4C9 RID: 54473 RVA: 0x0012E83F File Offset: 0x0012CA3F
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A4A0 RID: 42144
			// (set) Token: 0x0600D4CA RID: 54474 RVA: 0x0012E852 File Offset: 0x0012CA52
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A4A1 RID: 42145
			// (set) Token: 0x0600D4CB RID: 54475 RVA: 0x0012E865 File Offset: 0x0012CA65
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A4A2 RID: 42146
			// (set) Token: 0x0600D4CC RID: 54476 RVA: 0x0012E878 File Offset: 0x0012CA78
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A4A3 RID: 42147
			// (set) Token: 0x0600D4CD RID: 54477 RVA: 0x0012E890 File Offset: 0x0012CA90
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A4A4 RID: 42148
			// (set) Token: 0x0600D4CE RID: 54478 RVA: 0x0012E8A3 File Offset: 0x0012CAA3
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A4A5 RID: 42149
			// (set) Token: 0x0600D4CF RID: 54479 RVA: 0x0012E8B6 File Offset: 0x0012CAB6
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A4A6 RID: 42150
			// (set) Token: 0x0600D4D0 RID: 54480 RVA: 0x0012E8CE File Offset: 0x0012CACE
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x1700A4A7 RID: 42151
			// (set) Token: 0x0600D4D1 RID: 54481 RVA: 0x0012E8E6 File Offset: 0x0012CAE6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A4A8 RID: 42152
			// (set) Token: 0x0600D4D2 RID: 54482 RVA: 0x0012E8F9 File Offset: 0x0012CAF9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4A9 RID: 42153
			// (set) Token: 0x0600D4D3 RID: 54483 RVA: 0x0012E911 File Offset: 0x0012CB11
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4AA RID: 42154
			// (set) Token: 0x0600D4D4 RID: 54484 RVA: 0x0012E929 File Offset: 0x0012CB29
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4AB RID: 42155
			// (set) Token: 0x0600D4D5 RID: 54485 RVA: 0x0012E941 File Offset: 0x0012CB41
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A4AC RID: 42156
			// (set) Token: 0x0600D4D6 RID: 54486 RVA: 0x0012E959 File Offset: 0x0012CB59
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

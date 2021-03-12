using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200032B RID: 811
	public class SetManagementRoleAssignmentCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeRoleAssignment>
	{
		// Token: 0x06003512 RID: 13586 RVA: 0x0005CB52 File Offset: 0x0005AD52
		private SetManagementRoleAssignmentCommand() : base("Set-ManagementRoleAssignment")
		{
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x0005CB5F File Offset: 0x0005AD5F
		public SetManagementRoleAssignmentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x0005CB6E File Offset: 0x0005AD6E
		public virtual SetManagementRoleAssignmentCommand SetParameters(SetManagementRoleAssignmentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x0005CB78 File Offset: 0x0005AD78
		public virtual SetManagementRoleAssignmentCommand SetParameters(SetManagementRoleAssignmentCommand.RelativeRecipientWriteScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0005CB82 File Offset: 0x0005AD82
		public virtual SetManagementRoleAssignmentCommand SetParameters(SetManagementRoleAssignmentCommand.CustomRecipientWriteScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0005CB8C File Offset: 0x0005AD8C
		public virtual SetManagementRoleAssignmentCommand SetParameters(SetManagementRoleAssignmentCommand.RecipientOrganizationalUnitScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x0005CB96 File Offset: 0x0005AD96
		public virtual SetManagementRoleAssignmentCommand SetParameters(SetManagementRoleAssignmentCommand.ExclusiveScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200032C RID: 812
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001A75 RID: 6773
			// (set) Token: 0x06003519 RID: 13593 RVA: 0x0005CBA0 File Offset: 0x0005ADA0
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A76 RID: 6774
			// (set) Token: 0x0600351A RID: 13594 RVA: 0x0005CBB8 File Offset: 0x0005ADB8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001A77 RID: 6775
			// (set) Token: 0x0600351B RID: 13595 RVA: 0x0005CBD6 File Offset: 0x0005ADD6
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001A78 RID: 6776
			// (set) Token: 0x0600351C RID: 13596 RVA: 0x0005CBEE File Offset: 0x0005ADEE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A79 RID: 6777
			// (set) Token: 0x0600351D RID: 13597 RVA: 0x0005CC01 File Offset: 0x0005AE01
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A7A RID: 6778
			// (set) Token: 0x0600351E RID: 13598 RVA: 0x0005CC19 File Offset: 0x0005AE19
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A7B RID: 6779
			// (set) Token: 0x0600351F RID: 13599 RVA: 0x0005CC31 File Offset: 0x0005AE31
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A7C RID: 6780
			// (set) Token: 0x06003520 RID: 13600 RVA: 0x0005CC49 File Offset: 0x0005AE49
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A7D RID: 6781
			// (set) Token: 0x06003521 RID: 13601 RVA: 0x0005CC61 File Offset: 0x0005AE61
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200032D RID: 813
		public class RelativeRecipientWriteScopeParameters : ParametersBase
		{
			// Token: 0x17001A7E RID: 6782
			// (set) Token: 0x06003523 RID: 13603 RVA: 0x0005CC81 File Offset: 0x0005AE81
			public virtual RecipientWriteScopeType RecipientRelativeWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientRelativeWriteScope"] = value;
				}
			}

			// Token: 0x17001A7F RID: 6783
			// (set) Token: 0x06003524 RID: 13604 RVA: 0x0005CC99 File Offset: 0x0005AE99
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A80 RID: 6784
			// (set) Token: 0x06003525 RID: 13605 RVA: 0x0005CCAC File Offset: 0x0005AEAC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A81 RID: 6785
			// (set) Token: 0x06003526 RID: 13606 RVA: 0x0005CCC4 File Offset: 0x0005AEC4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001A82 RID: 6786
			// (set) Token: 0x06003527 RID: 13607 RVA: 0x0005CCE2 File Offset: 0x0005AEE2
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001A83 RID: 6787
			// (set) Token: 0x06003528 RID: 13608 RVA: 0x0005CCFA File Offset: 0x0005AEFA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A84 RID: 6788
			// (set) Token: 0x06003529 RID: 13609 RVA: 0x0005CD0D File Offset: 0x0005AF0D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A85 RID: 6789
			// (set) Token: 0x0600352A RID: 13610 RVA: 0x0005CD25 File Offset: 0x0005AF25
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A86 RID: 6790
			// (set) Token: 0x0600352B RID: 13611 RVA: 0x0005CD3D File Offset: 0x0005AF3D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A87 RID: 6791
			// (set) Token: 0x0600352C RID: 13612 RVA: 0x0005CD55 File Offset: 0x0005AF55
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A88 RID: 6792
			// (set) Token: 0x0600352D RID: 13613 RVA: 0x0005CD6D File Offset: 0x0005AF6D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200032E RID: 814
		public class CustomRecipientWriteScopeParameters : ParametersBase
		{
			// Token: 0x17001A89 RID: 6793
			// (set) Token: 0x0600352F RID: 13615 RVA: 0x0005CD8D File Offset: 0x0005AF8D
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A8A RID: 6794
			// (set) Token: 0x06003530 RID: 13616 RVA: 0x0005CDA0 File Offset: 0x0005AFA0
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A8B RID: 6795
			// (set) Token: 0x06003531 RID: 13617 RVA: 0x0005CDB3 File Offset: 0x0005AFB3
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A8C RID: 6796
			// (set) Token: 0x06003532 RID: 13618 RVA: 0x0005CDCB File Offset: 0x0005AFCB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001A8D RID: 6797
			// (set) Token: 0x06003533 RID: 13619 RVA: 0x0005CDE9 File Offset: 0x0005AFE9
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001A8E RID: 6798
			// (set) Token: 0x06003534 RID: 13620 RVA: 0x0005CE01 File Offset: 0x0005B001
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A8F RID: 6799
			// (set) Token: 0x06003535 RID: 13621 RVA: 0x0005CE14 File Offset: 0x0005B014
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A90 RID: 6800
			// (set) Token: 0x06003536 RID: 13622 RVA: 0x0005CE2C File Offset: 0x0005B02C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A91 RID: 6801
			// (set) Token: 0x06003537 RID: 13623 RVA: 0x0005CE44 File Offset: 0x0005B044
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A92 RID: 6802
			// (set) Token: 0x06003538 RID: 13624 RVA: 0x0005CE5C File Offset: 0x0005B05C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A93 RID: 6803
			// (set) Token: 0x06003539 RID: 13625 RVA: 0x0005CE74 File Offset: 0x0005B074
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200032F RID: 815
		public class RecipientOrganizationalUnitScopeParameters : ParametersBase
		{
			// Token: 0x17001A94 RID: 6804
			// (set) Token: 0x0600353B RID: 13627 RVA: 0x0005CE94 File Offset: 0x0005B094
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001A95 RID: 6805
			// (set) Token: 0x0600353C RID: 13628 RVA: 0x0005CEB2 File Offset: 0x0005B0B2
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A96 RID: 6806
			// (set) Token: 0x0600353D RID: 13629 RVA: 0x0005CEC5 File Offset: 0x0005B0C5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A97 RID: 6807
			// (set) Token: 0x0600353E RID: 13630 RVA: 0x0005CEDD File Offset: 0x0005B0DD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001A98 RID: 6808
			// (set) Token: 0x0600353F RID: 13631 RVA: 0x0005CEFB File Offset: 0x0005B0FB
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001A99 RID: 6809
			// (set) Token: 0x06003540 RID: 13632 RVA: 0x0005CF13 File Offset: 0x0005B113
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A9A RID: 6810
			// (set) Token: 0x06003541 RID: 13633 RVA: 0x0005CF26 File Offset: 0x0005B126
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A9B RID: 6811
			// (set) Token: 0x06003542 RID: 13634 RVA: 0x0005CF3E File Offset: 0x0005B13E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A9C RID: 6812
			// (set) Token: 0x06003543 RID: 13635 RVA: 0x0005CF56 File Offset: 0x0005B156
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A9D RID: 6813
			// (set) Token: 0x06003544 RID: 13636 RVA: 0x0005CF6E File Offset: 0x0005B16E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A9E RID: 6814
			// (set) Token: 0x06003545 RID: 13637 RVA: 0x0005CF86 File Offset: 0x0005B186
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000330 RID: 816
		public class ExclusiveScopeParameters : ParametersBase
		{
			// Token: 0x17001A9F RID: 6815
			// (set) Token: 0x06003547 RID: 13639 RVA: 0x0005CFA6 File Offset: 0x0005B1A6
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001AA0 RID: 6816
			// (set) Token: 0x06003548 RID: 13640 RVA: 0x0005CFB9 File Offset: 0x0005B1B9
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001AA1 RID: 6817
			// (set) Token: 0x06003549 RID: 13641 RVA: 0x0005CFCC File Offset: 0x0005B1CC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001AA2 RID: 6818
			// (set) Token: 0x0600354A RID: 13642 RVA: 0x0005CFE4 File Offset: 0x0005B1E4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001AA3 RID: 6819
			// (set) Token: 0x0600354B RID: 13643 RVA: 0x0005D002 File Offset: 0x0005B202
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001AA4 RID: 6820
			// (set) Token: 0x0600354C RID: 13644 RVA: 0x0005D01A File Offset: 0x0005B21A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001AA5 RID: 6821
			// (set) Token: 0x0600354D RID: 13645 RVA: 0x0005D02D File Offset: 0x0005B22D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001AA6 RID: 6822
			// (set) Token: 0x0600354E RID: 13646 RVA: 0x0005D045 File Offset: 0x0005B245
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001AA7 RID: 6823
			// (set) Token: 0x0600354F RID: 13647 RVA: 0x0005D05D File Offset: 0x0005B25D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001AA8 RID: 6824
			// (set) Token: 0x06003550 RID: 13648 RVA: 0x0005D075 File Offset: 0x0005B275
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001AA9 RID: 6825
			// (set) Token: 0x06003551 RID: 13649 RVA: 0x0005D08D File Offset: 0x0005B28D
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

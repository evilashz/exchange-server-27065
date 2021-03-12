using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000322 RID: 802
	public class NewManagementRoleAssignmentCommand : SyntheticCommandWithPipelineInput<ExchangeRoleAssignment, ExchangeRoleAssignment>
	{
		// Token: 0x0600348F RID: 13455 RVA: 0x0005C02C File Offset: 0x0005A22C
		private NewManagementRoleAssignmentCommand() : base("New-ManagementRoleAssignment")
		{
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x0005C039 File Offset: 0x0005A239
		public NewManagementRoleAssignmentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x0005C048 File Offset: 0x0005A248
		public virtual NewManagementRoleAssignmentCommand SetParameters(NewManagementRoleAssignmentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x0005C052 File Offset: 0x0005A252
		public virtual NewManagementRoleAssignmentCommand SetParameters(NewManagementRoleAssignmentCommand.UserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x0005C05C File Offset: 0x0005A25C
		public virtual NewManagementRoleAssignmentCommand SetParameters(NewManagementRoleAssignmentCommand.SecurityGroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0005C066 File Offset: 0x0005A266
		public virtual NewManagementRoleAssignmentCommand SetParameters(NewManagementRoleAssignmentCommand.PolicyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x0005C070 File Offset: 0x0005A270
		public virtual NewManagementRoleAssignmentCommand SetParameters(NewManagementRoleAssignmentCommand.ComputerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000323 RID: 803
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001A04 RID: 6660
			// (set) Token: 0x06003496 RID: 13462 RVA: 0x0005C07A File Offset: 0x0005A27A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001A05 RID: 6661
			// (set) Token: 0x06003497 RID: 13463 RVA: 0x0005C08D File Offset: 0x0005A28D
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001A06 RID: 6662
			// (set) Token: 0x06003498 RID: 13464 RVA: 0x0005C0AB File Offset: 0x0005A2AB
			public virtual RecipientWriteScopeType RecipientRelativeWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientRelativeWriteScope"] = value;
				}
			}

			// Token: 0x17001A07 RID: 6663
			// (set) Token: 0x06003499 RID: 13465 RVA: 0x0005C0C3 File Offset: 0x0005A2C3
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001A08 RID: 6664
			// (set) Token: 0x0600349A RID: 13466 RVA: 0x0005C0E1 File Offset: 0x0005A2E1
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A09 RID: 6665
			// (set) Token: 0x0600349B RID: 13467 RVA: 0x0005C0F4 File Offset: 0x0005A2F4
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A0A RID: 6666
			// (set) Token: 0x0600349C RID: 13468 RVA: 0x0005C107 File Offset: 0x0005A307
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A0B RID: 6667
			// (set) Token: 0x0600349D RID: 13469 RVA: 0x0005C11A File Offset: 0x0005A31A
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A0C RID: 6668
			// (set) Token: 0x0600349E RID: 13470 RVA: 0x0005C12D File Offset: 0x0005A32D
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001A0D RID: 6669
			// (set) Token: 0x0600349F RID: 13471 RVA: 0x0005C145 File Offset: 0x0005A345
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A0E RID: 6670
			// (set) Token: 0x060034A0 RID: 13472 RVA: 0x0005C15D File Offset: 0x0005A35D
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17001A0F RID: 6671
			// (set) Token: 0x060034A1 RID: 13473 RVA: 0x0005C175 File Offset: 0x0005A375
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001A10 RID: 6672
			// (set) Token: 0x060034A2 RID: 13474 RVA: 0x0005C193 File Offset: 0x0005A393
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A11 RID: 6673
			// (set) Token: 0x060034A3 RID: 13475 RVA: 0x0005C1A6 File Offset: 0x0005A3A6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A12 RID: 6674
			// (set) Token: 0x060034A4 RID: 13476 RVA: 0x0005C1BE File Offset: 0x0005A3BE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A13 RID: 6675
			// (set) Token: 0x060034A5 RID: 13477 RVA: 0x0005C1D6 File Offset: 0x0005A3D6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A14 RID: 6676
			// (set) Token: 0x060034A6 RID: 13478 RVA: 0x0005C1EE File Offset: 0x0005A3EE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A15 RID: 6677
			// (set) Token: 0x060034A7 RID: 13479 RVA: 0x0005C206 File Offset: 0x0005A406
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000324 RID: 804
		public class UserParameters : ParametersBase
		{
			// Token: 0x17001A16 RID: 6678
			// (set) Token: 0x060034A9 RID: 13481 RVA: 0x0005C226 File Offset: 0x0005A426
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001A17 RID: 6679
			// (set) Token: 0x060034AA RID: 13482 RVA: 0x0005C244 File Offset: 0x0005A444
			public virtual SwitchParameter Delegating
			{
				set
				{
					base.PowerSharpParameters["Delegating"] = value;
				}
			}

			// Token: 0x17001A18 RID: 6680
			// (set) Token: 0x060034AB RID: 13483 RVA: 0x0005C25C File Offset: 0x0005A45C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001A19 RID: 6681
			// (set) Token: 0x060034AC RID: 13484 RVA: 0x0005C26F File Offset: 0x0005A46F
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001A1A RID: 6682
			// (set) Token: 0x060034AD RID: 13485 RVA: 0x0005C28D File Offset: 0x0005A48D
			public virtual RecipientWriteScopeType RecipientRelativeWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientRelativeWriteScope"] = value;
				}
			}

			// Token: 0x17001A1B RID: 6683
			// (set) Token: 0x060034AE RID: 13486 RVA: 0x0005C2A5 File Offset: 0x0005A4A5
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001A1C RID: 6684
			// (set) Token: 0x060034AF RID: 13487 RVA: 0x0005C2C3 File Offset: 0x0005A4C3
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A1D RID: 6685
			// (set) Token: 0x060034B0 RID: 13488 RVA: 0x0005C2D6 File Offset: 0x0005A4D6
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A1E RID: 6686
			// (set) Token: 0x060034B1 RID: 13489 RVA: 0x0005C2E9 File Offset: 0x0005A4E9
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A1F RID: 6687
			// (set) Token: 0x060034B2 RID: 13490 RVA: 0x0005C2FC File Offset: 0x0005A4FC
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A20 RID: 6688
			// (set) Token: 0x060034B3 RID: 13491 RVA: 0x0005C30F File Offset: 0x0005A50F
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001A21 RID: 6689
			// (set) Token: 0x060034B4 RID: 13492 RVA: 0x0005C327 File Offset: 0x0005A527
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A22 RID: 6690
			// (set) Token: 0x060034B5 RID: 13493 RVA: 0x0005C33F File Offset: 0x0005A53F
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17001A23 RID: 6691
			// (set) Token: 0x060034B6 RID: 13494 RVA: 0x0005C357 File Offset: 0x0005A557
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001A24 RID: 6692
			// (set) Token: 0x060034B7 RID: 13495 RVA: 0x0005C375 File Offset: 0x0005A575
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A25 RID: 6693
			// (set) Token: 0x060034B8 RID: 13496 RVA: 0x0005C388 File Offset: 0x0005A588
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A26 RID: 6694
			// (set) Token: 0x060034B9 RID: 13497 RVA: 0x0005C3A0 File Offset: 0x0005A5A0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A27 RID: 6695
			// (set) Token: 0x060034BA RID: 13498 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A28 RID: 6696
			// (set) Token: 0x060034BB RID: 13499 RVA: 0x0005C3D0 File Offset: 0x0005A5D0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A29 RID: 6697
			// (set) Token: 0x060034BC RID: 13500 RVA: 0x0005C3E8 File Offset: 0x0005A5E8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000325 RID: 805
		public class SecurityGroupParameters : ParametersBase
		{
			// Token: 0x17001A2A RID: 6698
			// (set) Token: 0x060034BE RID: 13502 RVA: 0x0005C408 File Offset: 0x0005A608
			public virtual string SecurityGroup
			{
				set
				{
					base.PowerSharpParameters["SecurityGroup"] = ((value != null) ? new SecurityGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001A2B RID: 6699
			// (set) Token: 0x060034BF RID: 13503 RVA: 0x0005C426 File Offset: 0x0005A626
			public virtual SwitchParameter Delegating
			{
				set
				{
					base.PowerSharpParameters["Delegating"] = value;
				}
			}

			// Token: 0x17001A2C RID: 6700
			// (set) Token: 0x060034C0 RID: 13504 RVA: 0x0005C43E File Offset: 0x0005A63E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001A2D RID: 6701
			// (set) Token: 0x060034C1 RID: 13505 RVA: 0x0005C451 File Offset: 0x0005A651
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001A2E RID: 6702
			// (set) Token: 0x060034C2 RID: 13506 RVA: 0x0005C46F File Offset: 0x0005A66F
			public virtual RecipientWriteScopeType RecipientRelativeWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientRelativeWriteScope"] = value;
				}
			}

			// Token: 0x17001A2F RID: 6703
			// (set) Token: 0x060034C3 RID: 13507 RVA: 0x0005C487 File Offset: 0x0005A687
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001A30 RID: 6704
			// (set) Token: 0x060034C4 RID: 13508 RVA: 0x0005C4A5 File Offset: 0x0005A6A5
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A31 RID: 6705
			// (set) Token: 0x060034C5 RID: 13509 RVA: 0x0005C4B8 File Offset: 0x0005A6B8
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A32 RID: 6706
			// (set) Token: 0x060034C6 RID: 13510 RVA: 0x0005C4CB File Offset: 0x0005A6CB
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A33 RID: 6707
			// (set) Token: 0x060034C7 RID: 13511 RVA: 0x0005C4DE File Offset: 0x0005A6DE
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A34 RID: 6708
			// (set) Token: 0x060034C8 RID: 13512 RVA: 0x0005C4F1 File Offset: 0x0005A6F1
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001A35 RID: 6709
			// (set) Token: 0x060034C9 RID: 13513 RVA: 0x0005C509 File Offset: 0x0005A709
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A36 RID: 6710
			// (set) Token: 0x060034CA RID: 13514 RVA: 0x0005C521 File Offset: 0x0005A721
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17001A37 RID: 6711
			// (set) Token: 0x060034CB RID: 13515 RVA: 0x0005C539 File Offset: 0x0005A739
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001A38 RID: 6712
			// (set) Token: 0x060034CC RID: 13516 RVA: 0x0005C557 File Offset: 0x0005A757
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A39 RID: 6713
			// (set) Token: 0x060034CD RID: 13517 RVA: 0x0005C56A File Offset: 0x0005A76A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A3A RID: 6714
			// (set) Token: 0x060034CE RID: 13518 RVA: 0x0005C582 File Offset: 0x0005A782
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A3B RID: 6715
			// (set) Token: 0x060034CF RID: 13519 RVA: 0x0005C59A File Offset: 0x0005A79A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A3C RID: 6716
			// (set) Token: 0x060034D0 RID: 13520 RVA: 0x0005C5B2 File Offset: 0x0005A7B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A3D RID: 6717
			// (set) Token: 0x060034D1 RID: 13521 RVA: 0x0005C5CA File Offset: 0x0005A7CA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000326 RID: 806
		public class PolicyParameters : ParametersBase
		{
			// Token: 0x17001A3E RID: 6718
			// (set) Token: 0x060034D3 RID: 13523 RVA: 0x0005C5EA File Offset: 0x0005A7EA
			public virtual string Policy
			{
				set
				{
					base.PowerSharpParameters["Policy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17001A3F RID: 6719
			// (set) Token: 0x060034D4 RID: 13524 RVA: 0x0005C608 File Offset: 0x0005A808
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001A40 RID: 6720
			// (set) Token: 0x060034D5 RID: 13525 RVA: 0x0005C61B File Offset: 0x0005A81B
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001A41 RID: 6721
			// (set) Token: 0x060034D6 RID: 13526 RVA: 0x0005C639 File Offset: 0x0005A839
			public virtual RecipientWriteScopeType RecipientRelativeWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientRelativeWriteScope"] = value;
				}
			}

			// Token: 0x17001A42 RID: 6722
			// (set) Token: 0x060034D7 RID: 13527 RVA: 0x0005C651 File Offset: 0x0005A851
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001A43 RID: 6723
			// (set) Token: 0x060034D8 RID: 13528 RVA: 0x0005C66F File Offset: 0x0005A86F
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A44 RID: 6724
			// (set) Token: 0x060034D9 RID: 13529 RVA: 0x0005C682 File Offset: 0x0005A882
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A45 RID: 6725
			// (set) Token: 0x060034DA RID: 13530 RVA: 0x0005C695 File Offset: 0x0005A895
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A46 RID: 6726
			// (set) Token: 0x060034DB RID: 13531 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A47 RID: 6727
			// (set) Token: 0x060034DC RID: 13532 RVA: 0x0005C6BB File Offset: 0x0005A8BB
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001A48 RID: 6728
			// (set) Token: 0x060034DD RID: 13533 RVA: 0x0005C6D3 File Offset: 0x0005A8D3
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A49 RID: 6729
			// (set) Token: 0x060034DE RID: 13534 RVA: 0x0005C6EB File Offset: 0x0005A8EB
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17001A4A RID: 6730
			// (set) Token: 0x060034DF RID: 13535 RVA: 0x0005C703 File Offset: 0x0005A903
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001A4B RID: 6731
			// (set) Token: 0x060034E0 RID: 13536 RVA: 0x0005C721 File Offset: 0x0005A921
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A4C RID: 6732
			// (set) Token: 0x060034E1 RID: 13537 RVA: 0x0005C734 File Offset: 0x0005A934
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A4D RID: 6733
			// (set) Token: 0x060034E2 RID: 13538 RVA: 0x0005C74C File Offset: 0x0005A94C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A4E RID: 6734
			// (set) Token: 0x060034E3 RID: 13539 RVA: 0x0005C764 File Offset: 0x0005A964
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A4F RID: 6735
			// (set) Token: 0x060034E4 RID: 13540 RVA: 0x0005C77C File Offset: 0x0005A97C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A50 RID: 6736
			// (set) Token: 0x060034E5 RID: 13541 RVA: 0x0005C794 File Offset: 0x0005A994
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000327 RID: 807
		public class ComputerParameters : ParametersBase
		{
			// Token: 0x17001A51 RID: 6737
			// (set) Token: 0x060034E7 RID: 13543 RVA: 0x0005C7B4 File Offset: 0x0005A9B4
			public virtual string Computer
			{
				set
				{
					base.PowerSharpParameters["Computer"] = ((value != null) ? new ComputerIdParameter(value) : null);
				}
			}

			// Token: 0x17001A52 RID: 6738
			// (set) Token: 0x060034E8 RID: 13544 RVA: 0x0005C7D2 File Offset: 0x0005A9D2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001A53 RID: 6739
			// (set) Token: 0x060034E9 RID: 13545 RVA: 0x0005C7E5 File Offset: 0x0005A9E5
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001A54 RID: 6740
			// (set) Token: 0x060034EA RID: 13546 RVA: 0x0005C803 File Offset: 0x0005AA03
			public virtual RecipientWriteScopeType RecipientRelativeWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientRelativeWriteScope"] = value;
				}
			}

			// Token: 0x17001A55 RID: 6741
			// (set) Token: 0x060034EB RID: 13547 RVA: 0x0005C81B File Offset: 0x0005AA1B
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001A56 RID: 6742
			// (set) Token: 0x060034EC RID: 13548 RVA: 0x0005C839 File Offset: 0x0005AA39
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A57 RID: 6743
			// (set) Token: 0x060034ED RID: 13549 RVA: 0x0005C84C File Offset: 0x0005AA4C
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A58 RID: 6744
			// (set) Token: 0x060034EE RID: 13550 RVA: 0x0005C85F File Offset: 0x0005AA5F
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001A59 RID: 6745
			// (set) Token: 0x060034EF RID: 13551 RVA: 0x0005C872 File Offset: 0x0005AA72
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001A5A RID: 6746
			// (set) Token: 0x060034F0 RID: 13552 RVA: 0x0005C885 File Offset: 0x0005AA85
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001A5B RID: 6747
			// (set) Token: 0x060034F1 RID: 13553 RVA: 0x0005C89D File Offset: 0x0005AA9D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A5C RID: 6748
			// (set) Token: 0x060034F2 RID: 13554 RVA: 0x0005C8B5 File Offset: 0x0005AAB5
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17001A5D RID: 6749
			// (set) Token: 0x060034F3 RID: 13555 RVA: 0x0005C8CD File Offset: 0x0005AACD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001A5E RID: 6750
			// (set) Token: 0x060034F4 RID: 13556 RVA: 0x0005C8EB File Offset: 0x0005AAEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A5F RID: 6751
			// (set) Token: 0x060034F5 RID: 13557 RVA: 0x0005C8FE File Offset: 0x0005AAFE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A60 RID: 6752
			// (set) Token: 0x060034F6 RID: 13558 RVA: 0x0005C916 File Offset: 0x0005AB16
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A61 RID: 6753
			// (set) Token: 0x060034F7 RID: 13559 RVA: 0x0005C92E File Offset: 0x0005AB2E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A62 RID: 6754
			// (set) Token: 0x060034F8 RID: 13560 RVA: 0x0005C946 File Offset: 0x0005AB46
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A63 RID: 6755
			// (set) Token: 0x060034F9 RID: 13561 RVA: 0x0005C95E File Offset: 0x0005AB5E
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

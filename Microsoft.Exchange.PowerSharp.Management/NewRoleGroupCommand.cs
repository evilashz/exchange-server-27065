using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000333 RID: 819
	public class NewRoleGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x0600355F RID: 13663 RVA: 0x0005D1A2 File Offset: 0x0005B3A2
		private NewRoleGroupCommand() : base("New-RoleGroup")
		{
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x0005D1AF File Offset: 0x0005B3AF
		public NewRoleGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x0005D1BE File Offset: 0x0005B3BE
		public virtual NewRoleGroupCommand SetParameters(NewRoleGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x0005D1C8 File Offset: 0x0005B3C8
		public virtual NewRoleGroupCommand SetParameters(NewRoleGroupCommand.linkedpartnergroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x0005D1D2 File Offset: 0x0005B3D2
		public virtual NewRoleGroupCommand SetParameters(NewRoleGroupCommand.crossforestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000334 RID: 820
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001AB2 RID: 6834
			// (set) Token: 0x06003564 RID: 13668 RVA: 0x0005D1DC File Offset: 0x0005B3DC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001AB3 RID: 6835
			// (set) Token: 0x06003565 RID: 13669 RVA: 0x0005D1EF File Offset: 0x0005B3EF
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17001AB4 RID: 6836
			// (set) Token: 0x06003566 RID: 13670 RVA: 0x0005D202 File Offset: 0x0005B402
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17001AB5 RID: 6837
			// (set) Token: 0x06003567 RID: 13671 RVA: 0x0005D215 File Offset: 0x0005B415
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17001AB6 RID: 6838
			// (set) Token: 0x06003568 RID: 13672 RVA: 0x0005D228 File Offset: 0x0005B428
			public virtual RoleIdParameter Roles
			{
				set
				{
					base.PowerSharpParameters["Roles"] = value;
				}
			}

			// Token: 0x17001AB7 RID: 6839
			// (set) Token: 0x06003569 RID: 13673 RVA: 0x0005D23B File Offset: 0x0005B43B
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001AB8 RID: 6840
			// (set) Token: 0x0600356A RID: 13674 RVA: 0x0005D259 File Offset: 0x0005B459
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001AB9 RID: 6841
			// (set) Token: 0x0600356B RID: 13675 RVA: 0x0005D26C File Offset: 0x0005B46C
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001ABA RID: 6842
			// (set) Token: 0x0600356C RID: 13676 RVA: 0x0005D27F File Offset: 0x0005B47F
			public virtual SwitchParameter PartnerManaged
			{
				set
				{
					base.PowerSharpParameters["PartnerManaged"] = value;
				}
			}

			// Token: 0x17001ABB RID: 6843
			// (set) Token: 0x0600356D RID: 13677 RVA: 0x0005D297 File Offset: 0x0005B497
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001ABC RID: 6844
			// (set) Token: 0x0600356E RID: 13678 RVA: 0x0005D2AA File Offset: 0x0005B4AA
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001ABD RID: 6845
			// (set) Token: 0x0600356F RID: 13679 RVA: 0x0005D2C2 File Offset: 0x0005B4C2
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17001ABE RID: 6846
			// (set) Token: 0x06003570 RID: 13680 RVA: 0x0005D2D5 File Offset: 0x0005B4D5
			public virtual Guid WellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["WellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001ABF RID: 6847
			// (set) Token: 0x06003571 RID: 13681 RVA: 0x0005D2ED File Offset: 0x0005B4ED
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001AC0 RID: 6848
			// (set) Token: 0x06003572 RID: 13682 RVA: 0x0005D300 File Offset: 0x0005B500
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17001AC1 RID: 6849
			// (set) Token: 0x06003573 RID: 13683 RVA: 0x0005D313 File Offset: 0x0005B513
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001AC2 RID: 6850
			// (set) Token: 0x06003574 RID: 13684 RVA: 0x0005D331 File Offset: 0x0005B531
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001AC3 RID: 6851
			// (set) Token: 0x06003575 RID: 13685 RVA: 0x0005D344 File Offset: 0x0005B544
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001AC4 RID: 6852
			// (set) Token: 0x06003576 RID: 13686 RVA: 0x0005D35C File Offset: 0x0005B55C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001AC5 RID: 6853
			// (set) Token: 0x06003577 RID: 13687 RVA: 0x0005D374 File Offset: 0x0005B574
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001AC6 RID: 6854
			// (set) Token: 0x06003578 RID: 13688 RVA: 0x0005D38C File Offset: 0x0005B58C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001AC7 RID: 6855
			// (set) Token: 0x06003579 RID: 13689 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000335 RID: 821
		public class linkedpartnergroupParameters : ParametersBase
		{
			// Token: 0x17001AC8 RID: 6856
			// (set) Token: 0x0600357B RID: 13691 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
			public virtual string LinkedPartnerGroupId
			{
				set
				{
					base.PowerSharpParameters["LinkedPartnerGroupId"] = value;
				}
			}

			// Token: 0x17001AC9 RID: 6857
			// (set) Token: 0x0600357C RID: 13692 RVA: 0x0005D3D7 File Offset: 0x0005B5D7
			public virtual string LinkedPartnerOrganizationId
			{
				set
				{
					base.PowerSharpParameters["LinkedPartnerOrganizationId"] = value;
				}
			}

			// Token: 0x17001ACA RID: 6858
			// (set) Token: 0x0600357D RID: 13693 RVA: 0x0005D3EA File Offset: 0x0005B5EA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001ACB RID: 6859
			// (set) Token: 0x0600357E RID: 13694 RVA: 0x0005D3FD File Offset: 0x0005B5FD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17001ACC RID: 6860
			// (set) Token: 0x0600357F RID: 13695 RVA: 0x0005D410 File Offset: 0x0005B610
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17001ACD RID: 6861
			// (set) Token: 0x06003580 RID: 13696 RVA: 0x0005D423 File Offset: 0x0005B623
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17001ACE RID: 6862
			// (set) Token: 0x06003581 RID: 13697 RVA: 0x0005D436 File Offset: 0x0005B636
			public virtual RoleIdParameter Roles
			{
				set
				{
					base.PowerSharpParameters["Roles"] = value;
				}
			}

			// Token: 0x17001ACF RID: 6863
			// (set) Token: 0x06003582 RID: 13698 RVA: 0x0005D449 File Offset: 0x0005B649
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001AD0 RID: 6864
			// (set) Token: 0x06003583 RID: 13699 RVA: 0x0005D467 File Offset: 0x0005B667
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001AD1 RID: 6865
			// (set) Token: 0x06003584 RID: 13700 RVA: 0x0005D47A File Offset: 0x0005B67A
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001AD2 RID: 6866
			// (set) Token: 0x06003585 RID: 13701 RVA: 0x0005D48D File Offset: 0x0005B68D
			public virtual SwitchParameter PartnerManaged
			{
				set
				{
					base.PowerSharpParameters["PartnerManaged"] = value;
				}
			}

			// Token: 0x17001AD3 RID: 6867
			// (set) Token: 0x06003586 RID: 13702 RVA: 0x0005D4A5 File Offset: 0x0005B6A5
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001AD4 RID: 6868
			// (set) Token: 0x06003587 RID: 13703 RVA: 0x0005D4B8 File Offset: 0x0005B6B8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001AD5 RID: 6869
			// (set) Token: 0x06003588 RID: 13704 RVA: 0x0005D4D0 File Offset: 0x0005B6D0
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17001AD6 RID: 6870
			// (set) Token: 0x06003589 RID: 13705 RVA: 0x0005D4E3 File Offset: 0x0005B6E3
			public virtual Guid WellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["WellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001AD7 RID: 6871
			// (set) Token: 0x0600358A RID: 13706 RVA: 0x0005D4FB File Offset: 0x0005B6FB
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001AD8 RID: 6872
			// (set) Token: 0x0600358B RID: 13707 RVA: 0x0005D50E File Offset: 0x0005B70E
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17001AD9 RID: 6873
			// (set) Token: 0x0600358C RID: 13708 RVA: 0x0005D521 File Offset: 0x0005B721
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001ADA RID: 6874
			// (set) Token: 0x0600358D RID: 13709 RVA: 0x0005D53F File Offset: 0x0005B73F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001ADB RID: 6875
			// (set) Token: 0x0600358E RID: 13710 RVA: 0x0005D552 File Offset: 0x0005B752
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001ADC RID: 6876
			// (set) Token: 0x0600358F RID: 13711 RVA: 0x0005D56A File Offset: 0x0005B76A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001ADD RID: 6877
			// (set) Token: 0x06003590 RID: 13712 RVA: 0x0005D582 File Offset: 0x0005B782
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001ADE RID: 6878
			// (set) Token: 0x06003591 RID: 13713 RVA: 0x0005D59A File Offset: 0x0005B79A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001ADF RID: 6879
			// (set) Token: 0x06003592 RID: 13714 RVA: 0x0005D5B2 File Offset: 0x0005B7B2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000336 RID: 822
		public class crossforestParameters : ParametersBase
		{
			// Token: 0x17001AE0 RID: 6880
			// (set) Token: 0x06003594 RID: 13716 RVA: 0x0005D5D2 File Offset: 0x0005B7D2
			public virtual string LinkedForeignGroup
			{
				set
				{
					base.PowerSharpParameters["LinkedForeignGroup"] = ((value != null) ? new UniversalSecurityGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001AE1 RID: 6881
			// (set) Token: 0x06003595 RID: 13717 RVA: 0x0005D5F0 File Offset: 0x0005B7F0
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17001AE2 RID: 6882
			// (set) Token: 0x06003596 RID: 13718 RVA: 0x0005D603 File Offset: 0x0005B803
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17001AE3 RID: 6883
			// (set) Token: 0x06003597 RID: 13719 RVA: 0x0005D616 File Offset: 0x0005B816
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001AE4 RID: 6884
			// (set) Token: 0x06003598 RID: 13720 RVA: 0x0005D629 File Offset: 0x0005B829
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17001AE5 RID: 6885
			// (set) Token: 0x06003599 RID: 13721 RVA: 0x0005D63C File Offset: 0x0005B83C
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17001AE6 RID: 6886
			// (set) Token: 0x0600359A RID: 13722 RVA: 0x0005D64F File Offset: 0x0005B84F
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17001AE7 RID: 6887
			// (set) Token: 0x0600359B RID: 13723 RVA: 0x0005D662 File Offset: 0x0005B862
			public virtual RoleIdParameter Roles
			{
				set
				{
					base.PowerSharpParameters["Roles"] = value;
				}
			}

			// Token: 0x17001AE8 RID: 6888
			// (set) Token: 0x0600359C RID: 13724 RVA: 0x0005D675 File Offset: 0x0005B875
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001AE9 RID: 6889
			// (set) Token: 0x0600359D RID: 13725 RVA: 0x0005D693 File Offset: 0x0005B893
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x17001AEA RID: 6890
			// (set) Token: 0x0600359E RID: 13726 RVA: 0x0005D6A6 File Offset: 0x0005B8A6
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x17001AEB RID: 6891
			// (set) Token: 0x0600359F RID: 13727 RVA: 0x0005D6B9 File Offset: 0x0005B8B9
			public virtual SwitchParameter PartnerManaged
			{
				set
				{
					base.PowerSharpParameters["PartnerManaged"] = value;
				}
			}

			// Token: 0x17001AEC RID: 6892
			// (set) Token: 0x060035A0 RID: 13728 RVA: 0x0005D6D1 File Offset: 0x0005B8D1
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001AED RID: 6893
			// (set) Token: 0x060035A1 RID: 13729 RVA: 0x0005D6E4 File Offset: 0x0005B8E4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001AEE RID: 6894
			// (set) Token: 0x060035A2 RID: 13730 RVA: 0x0005D6FC File Offset: 0x0005B8FC
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17001AEF RID: 6895
			// (set) Token: 0x060035A3 RID: 13731 RVA: 0x0005D70F File Offset: 0x0005B90F
			public virtual Guid WellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["WellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001AF0 RID: 6896
			// (set) Token: 0x060035A4 RID: 13732 RVA: 0x0005D727 File Offset: 0x0005B927
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001AF1 RID: 6897
			// (set) Token: 0x060035A5 RID: 13733 RVA: 0x0005D73A File Offset: 0x0005B93A
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17001AF2 RID: 6898
			// (set) Token: 0x060035A6 RID: 13734 RVA: 0x0005D74D File Offset: 0x0005B94D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001AF3 RID: 6899
			// (set) Token: 0x060035A7 RID: 13735 RVA: 0x0005D76B File Offset: 0x0005B96B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001AF4 RID: 6900
			// (set) Token: 0x060035A8 RID: 13736 RVA: 0x0005D77E File Offset: 0x0005B97E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001AF5 RID: 6901
			// (set) Token: 0x060035A9 RID: 13737 RVA: 0x0005D796 File Offset: 0x0005B996
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001AF6 RID: 6902
			// (set) Token: 0x060035AA RID: 13738 RVA: 0x0005D7AE File Offset: 0x0005B9AE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001AF7 RID: 6903
			// (set) Token: 0x060035AB RID: 13739 RVA: 0x0005D7C6 File Offset: 0x0005B9C6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001AF8 RID: 6904
			// (set) Token: 0x060035AC RID: 13740 RVA: 0x0005D7DE File Offset: 0x0005B9DE
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

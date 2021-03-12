using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D2F RID: 3375
	public class RemoveADPermissionCommand : SyntheticCommandWithPipelineInput<ADAcePresentationObject, ADAcePresentationObject>
	{
		// Token: 0x0600B2BE RID: 45758 RVA: 0x00101A88 File Offset: 0x000FFC88
		private RemoveADPermissionCommand() : base("Remove-ADPermission")
		{
		}

		// Token: 0x0600B2BF RID: 45759 RVA: 0x00101A95 File Offset: 0x000FFC95
		public RemoveADPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B2C0 RID: 45760 RVA: 0x00101AA4 File Offset: 0x000FFCA4
		public virtual RemoveADPermissionCommand SetParameters(RemoveADPermissionCommand.AccessRightsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B2C1 RID: 45761 RVA: 0x00101AAE File Offset: 0x000FFCAE
		public virtual RemoveADPermissionCommand SetParameters(RemoveADPermissionCommand.OwnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B2C2 RID: 45762 RVA: 0x00101AB8 File Offset: 0x000FFCB8
		public virtual RemoveADPermissionCommand SetParameters(RemoveADPermissionCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B2C3 RID: 45763 RVA: 0x00101AC2 File Offset: 0x000FFCC2
		public virtual RemoveADPermissionCommand SetParameters(RemoveADPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D30 RID: 3376
		public class AccessRightsParameters : ParametersBase
		{
			// Token: 0x17008419 RID: 33817
			// (set) Token: 0x0600B2C4 RID: 45764 RVA: 0x00101ACC File Offset: 0x000FFCCC
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700841A RID: 33818
			// (set) Token: 0x0600B2C5 RID: 45765 RVA: 0x00101ADF File Offset: 0x000FFCDF
			public virtual ActiveDirectoryRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x1700841B RID: 33819
			// (set) Token: 0x0600B2C6 RID: 45766 RVA: 0x00101AF7 File Offset: 0x000FFCF7
			public virtual ExtendedRightIdParameter ExtendedRights
			{
				set
				{
					base.PowerSharpParameters["ExtendedRights"] = value;
				}
			}

			// Token: 0x1700841C RID: 33820
			// (set) Token: 0x0600B2C7 RID: 45767 RVA: 0x00101B0A File Offset: 0x000FFD0A
			public virtual ADSchemaObjectIdParameter ChildObjectTypes
			{
				set
				{
					base.PowerSharpParameters["ChildObjectTypes"] = value;
				}
			}

			// Token: 0x1700841D RID: 33821
			// (set) Token: 0x0600B2C8 RID: 45768 RVA: 0x00101B1D File Offset: 0x000FFD1D
			public virtual ADSchemaObjectIdParameter InheritedObjectType
			{
				set
				{
					base.PowerSharpParameters["InheritedObjectType"] = value;
				}
			}

			// Token: 0x1700841E RID: 33822
			// (set) Token: 0x0600B2C9 RID: 45769 RVA: 0x00101B30 File Offset: 0x000FFD30
			public virtual ADSchemaObjectIdParameter Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x1700841F RID: 33823
			// (set) Token: 0x0600B2CA RID: 45770 RVA: 0x00101B43 File Offset: 0x000FFD43
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x17008420 RID: 33824
			// (set) Token: 0x0600B2CB RID: 45771 RVA: 0x00101B5B File Offset: 0x000FFD5B
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x17008421 RID: 33825
			// (set) Token: 0x0600B2CC RID: 45772 RVA: 0x00101B73 File Offset: 0x000FFD73
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17008422 RID: 33826
			// (set) Token: 0x0600B2CD RID: 45773 RVA: 0x00101B91 File Offset: 0x000FFD91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008423 RID: 33827
			// (set) Token: 0x0600B2CE RID: 45774 RVA: 0x00101BA4 File Offset: 0x000FFDA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008424 RID: 33828
			// (set) Token: 0x0600B2CF RID: 45775 RVA: 0x00101BBC File Offset: 0x000FFDBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008425 RID: 33829
			// (set) Token: 0x0600B2D0 RID: 45776 RVA: 0x00101BD4 File Offset: 0x000FFDD4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008426 RID: 33830
			// (set) Token: 0x0600B2D1 RID: 45777 RVA: 0x00101BEC File Offset: 0x000FFDEC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008427 RID: 33831
			// (set) Token: 0x0600B2D2 RID: 45778 RVA: 0x00101C04 File Offset: 0x000FFE04
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008428 RID: 33832
			// (set) Token: 0x0600B2D3 RID: 45779 RVA: 0x00101C1C File Offset: 0x000FFE1C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D31 RID: 3377
		public class OwnerParameters : ParametersBase
		{
			// Token: 0x17008429 RID: 33833
			// (set) Token: 0x0600B2D5 RID: 45781 RVA: 0x00101C3C File Offset: 0x000FFE3C
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700842A RID: 33834
			// (set) Token: 0x0600B2D6 RID: 45782 RVA: 0x00101C4F File Offset: 0x000FFE4F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700842B RID: 33835
			// (set) Token: 0x0600B2D7 RID: 45783 RVA: 0x00101C62 File Offset: 0x000FFE62
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700842C RID: 33836
			// (set) Token: 0x0600B2D8 RID: 45784 RVA: 0x00101C7A File Offset: 0x000FFE7A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700842D RID: 33837
			// (set) Token: 0x0600B2D9 RID: 45785 RVA: 0x00101C92 File Offset: 0x000FFE92
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700842E RID: 33838
			// (set) Token: 0x0600B2DA RID: 45786 RVA: 0x00101CAA File Offset: 0x000FFEAA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700842F RID: 33839
			// (set) Token: 0x0600B2DB RID: 45787 RVA: 0x00101CC2 File Offset: 0x000FFEC2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008430 RID: 33840
			// (set) Token: 0x0600B2DC RID: 45788 RVA: 0x00101CDA File Offset: 0x000FFEDA
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D32 RID: 3378
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x17008431 RID: 33841
			// (set) Token: 0x0600B2DE RID: 45790 RVA: 0x00101CFA File Offset: 0x000FFEFA
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17008432 RID: 33842
			// (set) Token: 0x0600B2DF RID: 45791 RVA: 0x00101D0D File Offset: 0x000FFF0D
			public virtual ADAcePresentationObject Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17008433 RID: 33843
			// (set) Token: 0x0600B2E0 RID: 45792 RVA: 0x00101D20 File Offset: 0x000FFF20
			public virtual ActiveDirectoryRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17008434 RID: 33844
			// (set) Token: 0x0600B2E1 RID: 45793 RVA: 0x00101D38 File Offset: 0x000FFF38
			public virtual ExtendedRightIdParameter ExtendedRights
			{
				set
				{
					base.PowerSharpParameters["ExtendedRights"] = value;
				}
			}

			// Token: 0x17008435 RID: 33845
			// (set) Token: 0x0600B2E2 RID: 45794 RVA: 0x00101D4B File Offset: 0x000FFF4B
			public virtual ADSchemaObjectIdParameter ChildObjectTypes
			{
				set
				{
					base.PowerSharpParameters["ChildObjectTypes"] = value;
				}
			}

			// Token: 0x17008436 RID: 33846
			// (set) Token: 0x0600B2E3 RID: 45795 RVA: 0x00101D5E File Offset: 0x000FFF5E
			public virtual ADSchemaObjectIdParameter InheritedObjectType
			{
				set
				{
					base.PowerSharpParameters["InheritedObjectType"] = value;
				}
			}

			// Token: 0x17008437 RID: 33847
			// (set) Token: 0x0600B2E4 RID: 45796 RVA: 0x00101D71 File Offset: 0x000FFF71
			public virtual ADSchemaObjectIdParameter Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x17008438 RID: 33848
			// (set) Token: 0x0600B2E5 RID: 45797 RVA: 0x00101D84 File Offset: 0x000FFF84
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x17008439 RID: 33849
			// (set) Token: 0x0600B2E6 RID: 45798 RVA: 0x00101D9C File Offset: 0x000FFF9C
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x1700843A RID: 33850
			// (set) Token: 0x0600B2E7 RID: 45799 RVA: 0x00101DB4 File Offset: 0x000FFFB4
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x1700843B RID: 33851
			// (set) Token: 0x0600B2E8 RID: 45800 RVA: 0x00101DD2 File Offset: 0x000FFFD2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700843C RID: 33852
			// (set) Token: 0x0600B2E9 RID: 45801 RVA: 0x00101DE5 File Offset: 0x000FFFE5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700843D RID: 33853
			// (set) Token: 0x0600B2EA RID: 45802 RVA: 0x00101DFD File Offset: 0x000FFFFD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700843E RID: 33854
			// (set) Token: 0x0600B2EB RID: 45803 RVA: 0x00101E15 File Offset: 0x00100015
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700843F RID: 33855
			// (set) Token: 0x0600B2EC RID: 45804 RVA: 0x00101E2D File Offset: 0x0010002D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008440 RID: 33856
			// (set) Token: 0x0600B2ED RID: 45805 RVA: 0x00101E45 File Offset: 0x00100045
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008441 RID: 33857
			// (set) Token: 0x0600B2EE RID: 45806 RVA: 0x00101E5D File Offset: 0x0010005D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D33 RID: 3379
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008442 RID: 33858
			// (set) Token: 0x0600B2F0 RID: 45808 RVA: 0x00101E7D File Offset: 0x0010007D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008443 RID: 33859
			// (set) Token: 0x0600B2F1 RID: 45809 RVA: 0x00101E90 File Offset: 0x00100090
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008444 RID: 33860
			// (set) Token: 0x0600B2F2 RID: 45810 RVA: 0x00101EA8 File Offset: 0x001000A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008445 RID: 33861
			// (set) Token: 0x0600B2F3 RID: 45811 RVA: 0x00101EC0 File Offset: 0x001000C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008446 RID: 33862
			// (set) Token: 0x0600B2F4 RID: 45812 RVA: 0x00101ED8 File Offset: 0x001000D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008447 RID: 33863
			// (set) Token: 0x0600B2F5 RID: 45813 RVA: 0x00101EF0 File Offset: 0x001000F0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008448 RID: 33864
			// (set) Token: 0x0600B2F6 RID: 45814 RVA: 0x00101F08 File Offset: 0x00100108
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}

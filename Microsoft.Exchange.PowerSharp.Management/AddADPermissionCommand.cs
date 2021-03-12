using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D1A RID: 3354
	public class AddADPermissionCommand : SyntheticCommandWithPipelineInput<ADAcePresentationObject, ADAcePresentationObject>
	{
		// Token: 0x0600B1FA RID: 45562 RVA: 0x00100ACB File Offset: 0x000FECCB
		private AddADPermissionCommand() : base("Add-ADPermission")
		{
		}

		// Token: 0x0600B1FB RID: 45563 RVA: 0x00100AD8 File Offset: 0x000FECD8
		public AddADPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B1FC RID: 45564 RVA: 0x00100AE7 File Offset: 0x000FECE7
		public virtual AddADPermissionCommand SetParameters(AddADPermissionCommand.OwnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B1FD RID: 45565 RVA: 0x00100AF1 File Offset: 0x000FECF1
		public virtual AddADPermissionCommand SetParameters(AddADPermissionCommand.AccessRightsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B1FE RID: 45566 RVA: 0x00100AFB File Offset: 0x000FECFB
		public virtual AddADPermissionCommand SetParameters(AddADPermissionCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B1FF RID: 45567 RVA: 0x00100B05 File Offset: 0x000FED05
		public virtual AddADPermissionCommand SetParameters(AddADPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D1B RID: 3355
		public class OwnerParameters : ParametersBase
		{
			// Token: 0x1700837F RID: 33663
			// (set) Token: 0x0600B200 RID: 45568 RVA: 0x00100B0F File Offset: 0x000FED0F
			public virtual string Owner
			{
				set
				{
					base.PowerSharpParameters["Owner"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17008380 RID: 33664
			// (set) Token: 0x0600B201 RID: 45569 RVA: 0x00100B2D File Offset: 0x000FED2D
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17008381 RID: 33665
			// (set) Token: 0x0600B202 RID: 45570 RVA: 0x00100B40 File Offset: 0x000FED40
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008382 RID: 33666
			// (set) Token: 0x0600B203 RID: 45571 RVA: 0x00100B53 File Offset: 0x000FED53
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008383 RID: 33667
			// (set) Token: 0x0600B204 RID: 45572 RVA: 0x00100B6B File Offset: 0x000FED6B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008384 RID: 33668
			// (set) Token: 0x0600B205 RID: 45573 RVA: 0x00100B83 File Offset: 0x000FED83
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008385 RID: 33669
			// (set) Token: 0x0600B206 RID: 45574 RVA: 0x00100B9B File Offset: 0x000FED9B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008386 RID: 33670
			// (set) Token: 0x0600B207 RID: 45575 RVA: 0x00100BB3 File Offset: 0x000FEDB3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D1C RID: 3356
		public class AccessRightsParameters : ParametersBase
		{
			// Token: 0x17008387 RID: 33671
			// (set) Token: 0x0600B209 RID: 45577 RVA: 0x00100BD3 File Offset: 0x000FEDD3
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17008388 RID: 33672
			// (set) Token: 0x0600B20A RID: 45578 RVA: 0x00100BE6 File Offset: 0x000FEDE6
			public virtual ActiveDirectoryRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17008389 RID: 33673
			// (set) Token: 0x0600B20B RID: 45579 RVA: 0x00100BFE File Offset: 0x000FEDFE
			public virtual ExtendedRightIdParameter ExtendedRights
			{
				set
				{
					base.PowerSharpParameters["ExtendedRights"] = value;
				}
			}

			// Token: 0x1700838A RID: 33674
			// (set) Token: 0x0600B20C RID: 45580 RVA: 0x00100C11 File Offset: 0x000FEE11
			public virtual ADSchemaObjectIdParameter ChildObjectTypes
			{
				set
				{
					base.PowerSharpParameters["ChildObjectTypes"] = value;
				}
			}

			// Token: 0x1700838B RID: 33675
			// (set) Token: 0x0600B20D RID: 45581 RVA: 0x00100C24 File Offset: 0x000FEE24
			public virtual ADSchemaObjectIdParameter InheritedObjectType
			{
				set
				{
					base.PowerSharpParameters["InheritedObjectType"] = value;
				}
			}

			// Token: 0x1700838C RID: 33676
			// (set) Token: 0x0600B20E RID: 45582 RVA: 0x00100C37 File Offset: 0x000FEE37
			public virtual ADSchemaObjectIdParameter Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x1700838D RID: 33677
			// (set) Token: 0x0600B20F RID: 45583 RVA: 0x00100C4A File Offset: 0x000FEE4A
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x1700838E RID: 33678
			// (set) Token: 0x0600B210 RID: 45584 RVA: 0x00100C62 File Offset: 0x000FEE62
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x1700838F RID: 33679
			// (set) Token: 0x0600B211 RID: 45585 RVA: 0x00100C7A File Offset: 0x000FEE7A
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17008390 RID: 33680
			// (set) Token: 0x0600B212 RID: 45586 RVA: 0x00100C98 File Offset: 0x000FEE98
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008391 RID: 33681
			// (set) Token: 0x0600B213 RID: 45587 RVA: 0x00100CAB File Offset: 0x000FEEAB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008392 RID: 33682
			// (set) Token: 0x0600B214 RID: 45588 RVA: 0x00100CC3 File Offset: 0x000FEEC3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008393 RID: 33683
			// (set) Token: 0x0600B215 RID: 45589 RVA: 0x00100CDB File Offset: 0x000FEEDB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008394 RID: 33684
			// (set) Token: 0x0600B216 RID: 45590 RVA: 0x00100CF3 File Offset: 0x000FEEF3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008395 RID: 33685
			// (set) Token: 0x0600B217 RID: 45591 RVA: 0x00100D0B File Offset: 0x000FEF0B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D1D RID: 3357
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x17008396 RID: 33686
			// (set) Token: 0x0600B219 RID: 45593 RVA: 0x00100D2B File Offset: 0x000FEF2B
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17008397 RID: 33687
			// (set) Token: 0x0600B21A RID: 45594 RVA: 0x00100D3E File Offset: 0x000FEF3E
			public virtual ADAcePresentationObject Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17008398 RID: 33688
			// (set) Token: 0x0600B21B RID: 45595 RVA: 0x00100D51 File Offset: 0x000FEF51
			public virtual ActiveDirectoryRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17008399 RID: 33689
			// (set) Token: 0x0600B21C RID: 45596 RVA: 0x00100D69 File Offset: 0x000FEF69
			public virtual ExtendedRightIdParameter ExtendedRights
			{
				set
				{
					base.PowerSharpParameters["ExtendedRights"] = value;
				}
			}

			// Token: 0x1700839A RID: 33690
			// (set) Token: 0x0600B21D RID: 45597 RVA: 0x00100D7C File Offset: 0x000FEF7C
			public virtual ADSchemaObjectIdParameter ChildObjectTypes
			{
				set
				{
					base.PowerSharpParameters["ChildObjectTypes"] = value;
				}
			}

			// Token: 0x1700839B RID: 33691
			// (set) Token: 0x0600B21E RID: 45598 RVA: 0x00100D8F File Offset: 0x000FEF8F
			public virtual ADSchemaObjectIdParameter InheritedObjectType
			{
				set
				{
					base.PowerSharpParameters["InheritedObjectType"] = value;
				}
			}

			// Token: 0x1700839C RID: 33692
			// (set) Token: 0x0600B21F RID: 45599 RVA: 0x00100DA2 File Offset: 0x000FEFA2
			public virtual ADSchemaObjectIdParameter Properties
			{
				set
				{
					base.PowerSharpParameters["Properties"] = value;
				}
			}

			// Token: 0x1700839D RID: 33693
			// (set) Token: 0x0600B220 RID: 45600 RVA: 0x00100DB5 File Offset: 0x000FEFB5
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x1700839E RID: 33694
			// (set) Token: 0x0600B221 RID: 45601 RVA: 0x00100DCD File Offset: 0x000FEFCD
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x1700839F RID: 33695
			// (set) Token: 0x0600B222 RID: 45602 RVA: 0x00100DE5 File Offset: 0x000FEFE5
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x170083A0 RID: 33696
			// (set) Token: 0x0600B223 RID: 45603 RVA: 0x00100E03 File Offset: 0x000FF003
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083A1 RID: 33697
			// (set) Token: 0x0600B224 RID: 45604 RVA: 0x00100E16 File Offset: 0x000FF016
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083A2 RID: 33698
			// (set) Token: 0x0600B225 RID: 45605 RVA: 0x00100E2E File Offset: 0x000FF02E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083A3 RID: 33699
			// (set) Token: 0x0600B226 RID: 45606 RVA: 0x00100E46 File Offset: 0x000FF046
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083A4 RID: 33700
			// (set) Token: 0x0600B227 RID: 45607 RVA: 0x00100E5E File Offset: 0x000FF05E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083A5 RID: 33701
			// (set) Token: 0x0600B228 RID: 45608 RVA: 0x00100E76 File Offset: 0x000FF076
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D1E RID: 3358
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170083A6 RID: 33702
			// (set) Token: 0x0600B22A RID: 45610 RVA: 0x00100E96 File Offset: 0x000FF096
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083A7 RID: 33703
			// (set) Token: 0x0600B22B RID: 45611 RVA: 0x00100EA9 File Offset: 0x000FF0A9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083A8 RID: 33704
			// (set) Token: 0x0600B22C RID: 45612 RVA: 0x00100EC1 File Offset: 0x000FF0C1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083A9 RID: 33705
			// (set) Token: 0x0600B22D RID: 45613 RVA: 0x00100ED9 File Offset: 0x000FF0D9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083AA RID: 33706
			// (set) Token: 0x0600B22E RID: 45614 RVA: 0x00100EF1 File Offset: 0x000FF0F1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083AB RID: 33707
			// (set) Token: 0x0600B22F RID: 45615 RVA: 0x00100F09 File Offset: 0x000FF109
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

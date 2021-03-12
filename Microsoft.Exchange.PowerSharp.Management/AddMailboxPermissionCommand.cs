using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D22 RID: 3362
	public class AddMailboxPermissionCommand : SyntheticCommandWithPipelineInput<MailboxAcePresentationObject, MailboxAcePresentationObject>
	{
		// Token: 0x0600B248 RID: 45640 RVA: 0x001010F3 File Offset: 0x000FF2F3
		private AddMailboxPermissionCommand() : base("Add-MailboxPermission")
		{
		}

		// Token: 0x0600B249 RID: 45641 RVA: 0x00101100 File Offset: 0x000FF300
		public AddMailboxPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B24A RID: 45642 RVA: 0x0010110F File Offset: 0x000FF30F
		public virtual AddMailboxPermissionCommand SetParameters(AddMailboxPermissionCommand.OwnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B24B RID: 45643 RVA: 0x00101119 File Offset: 0x000FF319
		public virtual AddMailboxPermissionCommand SetParameters(AddMailboxPermissionCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B24C RID: 45644 RVA: 0x00101123 File Offset: 0x000FF323
		public virtual AddMailboxPermissionCommand SetParameters(AddMailboxPermissionCommand.AccessRightsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B24D RID: 45645 RVA: 0x0010112D File Offset: 0x000FF32D
		public virtual AddMailboxPermissionCommand SetParameters(AddMailboxPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D23 RID: 3363
		public class OwnerParameters : ParametersBase
		{
			// Token: 0x170083BD RID: 33725
			// (set) Token: 0x0600B24E RID: 45646 RVA: 0x00101137 File Offset: 0x000FF337
			public virtual string Owner
			{
				set
				{
					base.PowerSharpParameters["Owner"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x170083BE RID: 33726
			// (set) Token: 0x0600B24F RID: 45647 RVA: 0x00101155 File Offset: 0x000FF355
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170083BF RID: 33727
			// (set) Token: 0x0600B250 RID: 45648 RVA: 0x00101173 File Offset: 0x000FF373
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170083C0 RID: 33728
			// (set) Token: 0x0600B251 RID: 45649 RVA: 0x0010118B File Offset: 0x000FF38B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083C1 RID: 33729
			// (set) Token: 0x0600B252 RID: 45650 RVA: 0x0010119E File Offset: 0x000FF39E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083C2 RID: 33730
			// (set) Token: 0x0600B253 RID: 45651 RVA: 0x001011B6 File Offset: 0x000FF3B6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083C3 RID: 33731
			// (set) Token: 0x0600B254 RID: 45652 RVA: 0x001011CE File Offset: 0x000FF3CE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083C4 RID: 33732
			// (set) Token: 0x0600B255 RID: 45653 RVA: 0x001011E6 File Offset: 0x000FF3E6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083C5 RID: 33733
			// (set) Token: 0x0600B256 RID: 45654 RVA: 0x001011FE File Offset: 0x000FF3FE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D24 RID: 3364
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x170083C6 RID: 33734
			// (set) Token: 0x0600B258 RID: 45656 RVA: 0x0010121E File Offset: 0x000FF41E
			public virtual bool? AutoMapping
			{
				set
				{
					base.PowerSharpParameters["AutoMapping"] = value;
				}
			}

			// Token: 0x170083C7 RID: 33735
			// (set) Token: 0x0600B259 RID: 45657 RVA: 0x00101236 File Offset: 0x000FF436
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170083C8 RID: 33736
			// (set) Token: 0x0600B25A RID: 45658 RVA: 0x00101254 File Offset: 0x000FF454
			public virtual MailboxAcePresentationObject Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x170083C9 RID: 33737
			// (set) Token: 0x0600B25B RID: 45659 RVA: 0x00101267 File Offset: 0x000FF467
			public virtual MailboxRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x170083CA RID: 33738
			// (set) Token: 0x0600B25C RID: 45660 RVA: 0x0010127F File Offset: 0x000FF47F
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x170083CB RID: 33739
			// (set) Token: 0x0600B25D RID: 45661 RVA: 0x00101297 File Offset: 0x000FF497
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x170083CC RID: 33740
			// (set) Token: 0x0600B25E RID: 45662 RVA: 0x001012AF File Offset: 0x000FF4AF
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x170083CD RID: 33741
			// (set) Token: 0x0600B25F RID: 45663 RVA: 0x001012CD File Offset: 0x000FF4CD
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170083CE RID: 33742
			// (set) Token: 0x0600B260 RID: 45664 RVA: 0x001012E5 File Offset: 0x000FF4E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083CF RID: 33743
			// (set) Token: 0x0600B261 RID: 45665 RVA: 0x001012F8 File Offset: 0x000FF4F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083D0 RID: 33744
			// (set) Token: 0x0600B262 RID: 45666 RVA: 0x00101310 File Offset: 0x000FF510
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083D1 RID: 33745
			// (set) Token: 0x0600B263 RID: 45667 RVA: 0x00101328 File Offset: 0x000FF528
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083D2 RID: 33746
			// (set) Token: 0x0600B264 RID: 45668 RVA: 0x00101340 File Offset: 0x000FF540
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083D3 RID: 33747
			// (set) Token: 0x0600B265 RID: 45669 RVA: 0x00101358 File Offset: 0x000FF558
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D25 RID: 3365
		public class AccessRightsParameters : ParametersBase
		{
			// Token: 0x170083D4 RID: 33748
			// (set) Token: 0x0600B267 RID: 45671 RVA: 0x00101378 File Offset: 0x000FF578
			public virtual bool? AutoMapping
			{
				set
				{
					base.PowerSharpParameters["AutoMapping"] = value;
				}
			}

			// Token: 0x170083D5 RID: 33749
			// (set) Token: 0x0600B268 RID: 45672 RVA: 0x00101390 File Offset: 0x000FF590
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170083D6 RID: 33750
			// (set) Token: 0x0600B269 RID: 45673 RVA: 0x001013AE File Offset: 0x000FF5AE
			public virtual MailboxRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x170083D7 RID: 33751
			// (set) Token: 0x0600B26A RID: 45674 RVA: 0x001013C6 File Offset: 0x000FF5C6
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x170083D8 RID: 33752
			// (set) Token: 0x0600B26B RID: 45675 RVA: 0x001013DE File Offset: 0x000FF5DE
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x170083D9 RID: 33753
			// (set) Token: 0x0600B26C RID: 45676 RVA: 0x001013F6 File Offset: 0x000FF5F6
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x170083DA RID: 33754
			// (set) Token: 0x0600B26D RID: 45677 RVA: 0x00101414 File Offset: 0x000FF614
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170083DB RID: 33755
			// (set) Token: 0x0600B26E RID: 45678 RVA: 0x0010142C File Offset: 0x000FF62C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083DC RID: 33756
			// (set) Token: 0x0600B26F RID: 45679 RVA: 0x0010143F File Offset: 0x000FF63F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083DD RID: 33757
			// (set) Token: 0x0600B270 RID: 45680 RVA: 0x00101457 File Offset: 0x000FF657
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083DE RID: 33758
			// (set) Token: 0x0600B271 RID: 45681 RVA: 0x0010146F File Offset: 0x000FF66F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083DF RID: 33759
			// (set) Token: 0x0600B272 RID: 45682 RVA: 0x00101487 File Offset: 0x000FF687
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083E0 RID: 33760
			// (set) Token: 0x0600B273 RID: 45683 RVA: 0x0010149F File Offset: 0x000FF69F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D26 RID: 3366
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170083E1 RID: 33761
			// (set) Token: 0x0600B275 RID: 45685 RVA: 0x001014BF File Offset: 0x000FF6BF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170083E2 RID: 33762
			// (set) Token: 0x0600B276 RID: 45686 RVA: 0x001014D7 File Offset: 0x000FF6D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083E3 RID: 33763
			// (set) Token: 0x0600B277 RID: 45687 RVA: 0x001014EA File Offset: 0x000FF6EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083E4 RID: 33764
			// (set) Token: 0x0600B278 RID: 45688 RVA: 0x00101502 File Offset: 0x000FF702
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083E5 RID: 33765
			// (set) Token: 0x0600B279 RID: 45689 RVA: 0x0010151A File Offset: 0x000FF71A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083E6 RID: 33766
			// (set) Token: 0x0600B27A RID: 45690 RVA: 0x00101532 File Offset: 0x000FF732
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083E7 RID: 33767
			// (set) Token: 0x0600B27B RID: 45691 RVA: 0x0010154A File Offset: 0x000FF74A
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

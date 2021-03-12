using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D34 RID: 3380
	public class RemoveMailboxPermissionCommand : SyntheticCommandWithPipelineInput<MailboxAcePresentationObject, MailboxAcePresentationObject>
	{
		// Token: 0x0600B2F8 RID: 45816 RVA: 0x00101F28 File Offset: 0x00100128
		private RemoveMailboxPermissionCommand() : base("Remove-MailboxPermission")
		{
		}

		// Token: 0x0600B2F9 RID: 45817 RVA: 0x00101F35 File Offset: 0x00100135
		public RemoveMailboxPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B2FA RID: 45818 RVA: 0x00101F44 File Offset: 0x00100144
		public virtual RemoveMailboxPermissionCommand SetParameters(RemoveMailboxPermissionCommand.AccessRightsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B2FB RID: 45819 RVA: 0x00101F4E File Offset: 0x0010014E
		public virtual RemoveMailboxPermissionCommand SetParameters(RemoveMailboxPermissionCommand.OwnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B2FC RID: 45820 RVA: 0x00101F58 File Offset: 0x00100158
		public virtual RemoveMailboxPermissionCommand SetParameters(RemoveMailboxPermissionCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B2FD RID: 45821 RVA: 0x00101F62 File Offset: 0x00100162
		public virtual RemoveMailboxPermissionCommand SetParameters(RemoveMailboxPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D35 RID: 3381
		public class AccessRightsParameters : ParametersBase
		{
			// Token: 0x17008449 RID: 33865
			// (set) Token: 0x0600B2FE RID: 45822 RVA: 0x00101F6C File Offset: 0x0010016C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700844A RID: 33866
			// (set) Token: 0x0600B2FF RID: 45823 RVA: 0x00101F8A File Offset: 0x0010018A
			public virtual MailboxRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x1700844B RID: 33867
			// (set) Token: 0x0600B300 RID: 45824 RVA: 0x00101FA2 File Offset: 0x001001A2
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x1700844C RID: 33868
			// (set) Token: 0x0600B301 RID: 45825 RVA: 0x00101FBA File Offset: 0x001001BA
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x1700844D RID: 33869
			// (set) Token: 0x0600B302 RID: 45826 RVA: 0x00101FD2 File Offset: 0x001001D2
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x1700844E RID: 33870
			// (set) Token: 0x0600B303 RID: 45827 RVA: 0x00101FF0 File Offset: 0x001001F0
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700844F RID: 33871
			// (set) Token: 0x0600B304 RID: 45828 RVA: 0x00102008 File Offset: 0x00100208
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008450 RID: 33872
			// (set) Token: 0x0600B305 RID: 45829 RVA: 0x0010201B File Offset: 0x0010021B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008451 RID: 33873
			// (set) Token: 0x0600B306 RID: 45830 RVA: 0x00102033 File Offset: 0x00100233
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008452 RID: 33874
			// (set) Token: 0x0600B307 RID: 45831 RVA: 0x0010204B File Offset: 0x0010024B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008453 RID: 33875
			// (set) Token: 0x0600B308 RID: 45832 RVA: 0x00102063 File Offset: 0x00100263
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008454 RID: 33876
			// (set) Token: 0x0600B309 RID: 45833 RVA: 0x0010207B File Offset: 0x0010027B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008455 RID: 33877
			// (set) Token: 0x0600B30A RID: 45834 RVA: 0x00102093 File Offset: 0x00100293
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D36 RID: 3382
		public class OwnerParameters : ParametersBase
		{
			// Token: 0x17008456 RID: 33878
			// (set) Token: 0x0600B30C RID: 45836 RVA: 0x001020B3 File Offset: 0x001002B3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008457 RID: 33879
			// (set) Token: 0x0600B30D RID: 45837 RVA: 0x001020D1 File Offset: 0x001002D1
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008458 RID: 33880
			// (set) Token: 0x0600B30E RID: 45838 RVA: 0x001020E9 File Offset: 0x001002E9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008459 RID: 33881
			// (set) Token: 0x0600B30F RID: 45839 RVA: 0x001020FC File Offset: 0x001002FC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700845A RID: 33882
			// (set) Token: 0x0600B310 RID: 45840 RVA: 0x00102114 File Offset: 0x00100314
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700845B RID: 33883
			// (set) Token: 0x0600B311 RID: 45841 RVA: 0x0010212C File Offset: 0x0010032C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700845C RID: 33884
			// (set) Token: 0x0600B312 RID: 45842 RVA: 0x00102144 File Offset: 0x00100344
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700845D RID: 33885
			// (set) Token: 0x0600B313 RID: 45843 RVA: 0x0010215C File Offset: 0x0010035C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700845E RID: 33886
			// (set) Token: 0x0600B314 RID: 45844 RVA: 0x00102174 File Offset: 0x00100374
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D37 RID: 3383
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x1700845F RID: 33887
			// (set) Token: 0x0600B316 RID: 45846 RVA: 0x00102194 File Offset: 0x00100394
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008460 RID: 33888
			// (set) Token: 0x0600B317 RID: 45847 RVA: 0x001021B2 File Offset: 0x001003B2
			public virtual MailboxAcePresentationObject Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17008461 RID: 33889
			// (set) Token: 0x0600B318 RID: 45848 RVA: 0x001021C5 File Offset: 0x001003C5
			public virtual MailboxRights AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17008462 RID: 33890
			// (set) Token: 0x0600B319 RID: 45849 RVA: 0x001021DD File Offset: 0x001003DD
			public virtual SwitchParameter Deny
			{
				set
				{
					base.PowerSharpParameters["Deny"] = value;
				}
			}

			// Token: 0x17008463 RID: 33891
			// (set) Token: 0x0600B31A RID: 45850 RVA: 0x001021F5 File Offset: 0x001003F5
			public virtual ActiveDirectorySecurityInheritance InheritanceType
			{
				set
				{
					base.PowerSharpParameters["InheritanceType"] = value;
				}
			}

			// Token: 0x17008464 RID: 33892
			// (set) Token: 0x0600B31B RID: 45851 RVA: 0x0010220D File Offset: 0x0010040D
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17008465 RID: 33893
			// (set) Token: 0x0600B31C RID: 45852 RVA: 0x0010222B File Offset: 0x0010042B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008466 RID: 33894
			// (set) Token: 0x0600B31D RID: 45853 RVA: 0x00102243 File Offset: 0x00100443
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008467 RID: 33895
			// (set) Token: 0x0600B31E RID: 45854 RVA: 0x00102256 File Offset: 0x00100456
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008468 RID: 33896
			// (set) Token: 0x0600B31F RID: 45855 RVA: 0x0010226E File Offset: 0x0010046E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008469 RID: 33897
			// (set) Token: 0x0600B320 RID: 45856 RVA: 0x00102286 File Offset: 0x00100486
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700846A RID: 33898
			// (set) Token: 0x0600B321 RID: 45857 RVA: 0x0010229E File Offset: 0x0010049E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700846B RID: 33899
			// (set) Token: 0x0600B322 RID: 45858 RVA: 0x001022B6 File Offset: 0x001004B6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700846C RID: 33900
			// (set) Token: 0x0600B323 RID: 45859 RVA: 0x001022CE File Offset: 0x001004CE
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D38 RID: 3384
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700846D RID: 33901
			// (set) Token: 0x0600B325 RID: 45861 RVA: 0x001022EE File Offset: 0x001004EE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700846E RID: 33902
			// (set) Token: 0x0600B326 RID: 45862 RVA: 0x00102306 File Offset: 0x00100506
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700846F RID: 33903
			// (set) Token: 0x0600B327 RID: 45863 RVA: 0x00102319 File Offset: 0x00100519
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008470 RID: 33904
			// (set) Token: 0x0600B328 RID: 45864 RVA: 0x00102331 File Offset: 0x00100531
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008471 RID: 33905
			// (set) Token: 0x0600B329 RID: 45865 RVA: 0x00102349 File Offset: 0x00100549
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008472 RID: 33906
			// (set) Token: 0x0600B32A RID: 45866 RVA: 0x00102361 File Offset: 0x00100561
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008473 RID: 33907
			// (set) Token: 0x0600B32B RID: 45867 RVA: 0x00102379 File Offset: 0x00100579
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008474 RID: 33908
			// (set) Token: 0x0600B32C RID: 45868 RVA: 0x00102391 File Offset: 0x00100591
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

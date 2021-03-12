using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DD7 RID: 3543
	public class UndoSyncSoftDeletedMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<MailUserIdParameter>
	{
		// Token: 0x0600D332 RID: 54066 RVA: 0x0012C74A File Offset: 0x0012A94A
		private UndoSyncSoftDeletedMailUserCommand() : base("Undo-SyncSoftDeletedMailUser")
		{
		}

		// Token: 0x0600D333 RID: 54067 RVA: 0x0012C757 File Offset: 0x0012A957
		public UndoSyncSoftDeletedMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D334 RID: 54068 RVA: 0x0012C766 File Offset: 0x0012A966
		public virtual UndoSyncSoftDeletedMailUserCommand SetParameters(UndoSyncSoftDeletedMailUserCommand.SoftDeletedMailUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D335 RID: 54069 RVA: 0x0012C770 File Offset: 0x0012A970
		public virtual UndoSyncSoftDeletedMailUserCommand SetParameters(UndoSyncSoftDeletedMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DD8 RID: 3544
		public class SoftDeletedMailUserParameters : ParametersBase
		{
			// Token: 0x1700A33D RID: 41789
			// (set) Token: 0x0600D336 RID: 54070 RVA: 0x0012C77A File Offset: 0x0012A97A
			public virtual string SoftDeletedObject
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedObject"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A33E RID: 41790
			// (set) Token: 0x0600D337 RID: 54071 RVA: 0x0012C798 File Offset: 0x0012A998
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A33F RID: 41791
			// (set) Token: 0x0600D338 RID: 54072 RVA: 0x0012C7AB File Offset: 0x0012A9AB
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A340 RID: 41792
			// (set) Token: 0x0600D339 RID: 54073 RVA: 0x0012C7BE File Offset: 0x0012A9BE
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A341 RID: 41793
			// (set) Token: 0x0600D33A RID: 54074 RVA: 0x0012C7D1 File Offset: 0x0012A9D1
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700A342 RID: 41794
			// (set) Token: 0x0600D33B RID: 54075 RVA: 0x0012C7E9 File Offset: 0x0012A9E9
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700A343 RID: 41795
			// (set) Token: 0x0600D33C RID: 54076 RVA: 0x0012C7FC File Offset: 0x0012A9FC
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700A344 RID: 41796
			// (set) Token: 0x0600D33D RID: 54077 RVA: 0x0012C80F File Offset: 0x0012AA0F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A345 RID: 41797
			// (set) Token: 0x0600D33E RID: 54078 RVA: 0x0012C822 File Offset: 0x0012AA22
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A346 RID: 41798
			// (set) Token: 0x0600D33F RID: 54079 RVA: 0x0012C835 File Offset: 0x0012AA35
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A347 RID: 41799
			// (set) Token: 0x0600D340 RID: 54080 RVA: 0x0012C853 File Offset: 0x0012AA53
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A348 RID: 41800
			// (set) Token: 0x0600D341 RID: 54081 RVA: 0x0012C866 File Offset: 0x0012AA66
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A349 RID: 41801
			// (set) Token: 0x0600D342 RID: 54082 RVA: 0x0012C87E File Offset: 0x0012AA7E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A34A RID: 41802
			// (set) Token: 0x0600D343 RID: 54083 RVA: 0x0012C896 File Offset: 0x0012AA96
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A34B RID: 41803
			// (set) Token: 0x0600D344 RID: 54084 RVA: 0x0012C8AE File Offset: 0x0012AAAE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A34C RID: 41804
			// (set) Token: 0x0600D345 RID: 54085 RVA: 0x0012C8C6 File Offset: 0x0012AAC6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DD9 RID: 3545
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A34D RID: 41805
			// (set) Token: 0x0600D347 RID: 54087 RVA: 0x0012C8E6 File Offset: 0x0012AAE6
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700A34E RID: 41806
			// (set) Token: 0x0600D348 RID: 54088 RVA: 0x0012C8F9 File Offset: 0x0012AAF9
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700A34F RID: 41807
			// (set) Token: 0x0600D349 RID: 54089 RVA: 0x0012C90C File Offset: 0x0012AB0C
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A350 RID: 41808
			// (set) Token: 0x0600D34A RID: 54090 RVA: 0x0012C91F File Offset: 0x0012AB1F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A351 RID: 41809
			// (set) Token: 0x0600D34B RID: 54091 RVA: 0x0012C932 File Offset: 0x0012AB32
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A352 RID: 41810
			// (set) Token: 0x0600D34C RID: 54092 RVA: 0x0012C950 File Offset: 0x0012AB50
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A353 RID: 41811
			// (set) Token: 0x0600D34D RID: 54093 RVA: 0x0012C963 File Offset: 0x0012AB63
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A354 RID: 41812
			// (set) Token: 0x0600D34E RID: 54094 RVA: 0x0012C97B File Offset: 0x0012AB7B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A355 RID: 41813
			// (set) Token: 0x0600D34F RID: 54095 RVA: 0x0012C993 File Offset: 0x0012AB93
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A356 RID: 41814
			// (set) Token: 0x0600D350 RID: 54096 RVA: 0x0012C9AB File Offset: 0x0012ABAB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A357 RID: 41815
			// (set) Token: 0x0600D351 RID: 54097 RVA: 0x0012C9C3 File Offset: 0x0012ABC3
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

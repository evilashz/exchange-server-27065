using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001CB RID: 459
	public class SetRetentionPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<RetentionPolicy>
	{
		// Token: 0x0600264B RID: 9803 RVA: 0x000495CF File Offset: 0x000477CF
		private SetRetentionPolicyCommand() : base("Set-RetentionPolicy")
		{
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000495DC File Offset: 0x000477DC
		public SetRetentionPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000495EB File Offset: 0x000477EB
		public virtual SetRetentionPolicyCommand SetParameters(SetRetentionPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000495F5 File Offset: 0x000477F5
		public virtual SetRetentionPolicyCommand SetParameters(SetRetentionPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001CC RID: 460
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E6E RID: 3694
			// (set) Token: 0x0600264F RID: 9807 RVA: 0x000495FF File Offset: 0x000477FF
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000E6F RID: 3695
			// (set) Token: 0x06002650 RID: 9808 RVA: 0x00049617 File Offset: 0x00047817
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000E70 RID: 3696
			// (set) Token: 0x06002651 RID: 9809 RVA: 0x0004962F File Offset: 0x0004782F
			public virtual SwitchParameter IsDefaultArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["IsDefaultArbitrationMailbox"] = value;
				}
			}

			// Token: 0x17000E71 RID: 3697
			// (set) Token: 0x06002652 RID: 9810 RVA: 0x00049647 File Offset: 0x00047847
			public virtual RetentionPolicyTagIdParameter RetentionPolicyTagLinks
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicyTagLinks"] = value;
				}
			}

			// Token: 0x17000E72 RID: 3698
			// (set) Token: 0x06002653 RID: 9811 RVA: 0x0004965A File Offset: 0x0004785A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000E73 RID: 3699
			// (set) Token: 0x06002654 RID: 9812 RVA: 0x00049672 File Offset: 0x00047872
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E74 RID: 3700
			// (set) Token: 0x06002655 RID: 9813 RVA: 0x00049685 File Offset: 0x00047885
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x17000E75 RID: 3701
			// (set) Token: 0x06002656 RID: 9814 RVA: 0x0004969D File Offset: 0x0004789D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000E76 RID: 3702
			// (set) Token: 0x06002657 RID: 9815 RVA: 0x000496B0 File Offset: 0x000478B0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E77 RID: 3703
			// (set) Token: 0x06002658 RID: 9816 RVA: 0x000496C8 File Offset: 0x000478C8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E78 RID: 3704
			// (set) Token: 0x06002659 RID: 9817 RVA: 0x000496E0 File Offset: 0x000478E0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E79 RID: 3705
			// (set) Token: 0x0600265A RID: 9818 RVA: 0x000496F8 File Offset: 0x000478F8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E7A RID: 3706
			// (set) Token: 0x0600265B RID: 9819 RVA: 0x00049710 File Offset: 0x00047910
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001CD RID: 461
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000E7B RID: 3707
			// (set) Token: 0x0600265D RID: 9821 RVA: 0x00049730 File Offset: 0x00047930
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000E7C RID: 3708
			// (set) Token: 0x0600265E RID: 9822 RVA: 0x0004974E File Offset: 0x0004794E
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000E7D RID: 3709
			// (set) Token: 0x0600265F RID: 9823 RVA: 0x00049766 File Offset: 0x00047966
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000E7E RID: 3710
			// (set) Token: 0x06002660 RID: 9824 RVA: 0x0004977E File Offset: 0x0004797E
			public virtual SwitchParameter IsDefaultArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["IsDefaultArbitrationMailbox"] = value;
				}
			}

			// Token: 0x17000E7F RID: 3711
			// (set) Token: 0x06002661 RID: 9825 RVA: 0x00049796 File Offset: 0x00047996
			public virtual RetentionPolicyTagIdParameter RetentionPolicyTagLinks
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicyTagLinks"] = value;
				}
			}

			// Token: 0x17000E80 RID: 3712
			// (set) Token: 0x06002662 RID: 9826 RVA: 0x000497A9 File Offset: 0x000479A9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000E81 RID: 3713
			// (set) Token: 0x06002663 RID: 9827 RVA: 0x000497C1 File Offset: 0x000479C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E82 RID: 3714
			// (set) Token: 0x06002664 RID: 9828 RVA: 0x000497D4 File Offset: 0x000479D4
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x17000E83 RID: 3715
			// (set) Token: 0x06002665 RID: 9829 RVA: 0x000497EC File Offset: 0x000479EC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000E84 RID: 3716
			// (set) Token: 0x06002666 RID: 9830 RVA: 0x000497FF File Offset: 0x000479FF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E85 RID: 3717
			// (set) Token: 0x06002667 RID: 9831 RVA: 0x00049817 File Offset: 0x00047A17
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E86 RID: 3718
			// (set) Token: 0x06002668 RID: 9832 RVA: 0x0004982F File Offset: 0x00047A2F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E87 RID: 3719
			// (set) Token: 0x06002669 RID: 9833 RVA: 0x00049847 File Offset: 0x00047A47
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E88 RID: 3720
			// (set) Token: 0x0600266A RID: 9834 RVA: 0x0004985F File Offset: 0x00047A5F
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

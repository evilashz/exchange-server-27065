using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005E7 RID: 1511
	public class AddMailboxDatabaseCopyCommand : SyntheticCommandWithPipelineInput<Database, Database>
	{
		// Token: 0x06004DD1 RID: 19921 RVA: 0x0007C2AF File Offset: 0x0007A4AF
		private AddMailboxDatabaseCopyCommand() : base("Add-MailboxDatabaseCopy")
		{
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0007C2BC File Offset: 0x0007A4BC
		public AddMailboxDatabaseCopyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0007C2CB File Offset: 0x0007A4CB
		public virtual AddMailboxDatabaseCopyCommand SetParameters(AddMailboxDatabaseCopyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x0007C2D5 File Offset: 0x0007A4D5
		public virtual AddMailboxDatabaseCopyCommand SetParameters(AddMailboxDatabaseCopyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005E8 RID: 1512
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002DBC RID: 11708
			// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x0007C2DF File Offset: 0x0007A4DF
			public virtual MailboxServerIdParameter MailboxServer
			{
				set
				{
					base.PowerSharpParameters["MailboxServer"] = value;
				}
			}

			// Token: 0x17002DBD RID: 11709
			// (set) Token: 0x06004DD6 RID: 19926 RVA: 0x0007C2F2 File Offset: 0x0007A4F2
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002DBE RID: 11710
			// (set) Token: 0x06004DD7 RID: 19927 RVA: 0x0007C305 File Offset: 0x0007A505
			public virtual uint ActivationPreference
			{
				set
				{
					base.PowerSharpParameters["ActivationPreference"] = value;
				}
			}

			// Token: 0x17002DBF RID: 11711
			// (set) Token: 0x06004DD8 RID: 19928 RVA: 0x0007C31D File Offset: 0x0007A51D
			public virtual SwitchParameter SeedingPostponed
			{
				set
				{
					base.PowerSharpParameters["SeedingPostponed"] = value;
				}
			}

			// Token: 0x17002DC0 RID: 11712
			// (set) Token: 0x06004DD9 RID: 19929 RVA: 0x0007C335 File Offset: 0x0007A535
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002DC1 RID: 11713
			// (set) Token: 0x06004DDA RID: 19930 RVA: 0x0007C34D File Offset: 0x0007A54D
			public virtual EnhancedTimeSpan ReplayLagTime
			{
				set
				{
					base.PowerSharpParameters["ReplayLagTime"] = value;
				}
			}

			// Token: 0x17002DC2 RID: 11714
			// (set) Token: 0x06004DDB RID: 19931 RVA: 0x0007C365 File Offset: 0x0007A565
			public virtual EnhancedTimeSpan TruncationLagTime
			{
				set
				{
					base.PowerSharpParameters["TruncationLagTime"] = value;
				}
			}

			// Token: 0x17002DC3 RID: 11715
			// (set) Token: 0x06004DDC RID: 19932 RVA: 0x0007C37D File Offset: 0x0007A57D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DC4 RID: 11716
			// (set) Token: 0x06004DDD RID: 19933 RVA: 0x0007C390 File Offset: 0x0007A590
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DC5 RID: 11717
			// (set) Token: 0x06004DDE RID: 19934 RVA: 0x0007C3A8 File Offset: 0x0007A5A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DC6 RID: 11718
			// (set) Token: 0x06004DDF RID: 19935 RVA: 0x0007C3C0 File Offset: 0x0007A5C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DC7 RID: 11719
			// (set) Token: 0x06004DE0 RID: 19936 RVA: 0x0007C3D8 File Offset: 0x0007A5D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002DC8 RID: 11720
			// (set) Token: 0x06004DE1 RID: 19937 RVA: 0x0007C3F0 File Offset: 0x0007A5F0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005E9 RID: 1513
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002DC9 RID: 11721
			// (set) Token: 0x06004DE3 RID: 19939 RVA: 0x0007C410 File Offset: 0x0007A610
			public virtual uint ActivationPreference
			{
				set
				{
					base.PowerSharpParameters["ActivationPreference"] = value;
				}
			}

			// Token: 0x17002DCA RID: 11722
			// (set) Token: 0x06004DE4 RID: 19940 RVA: 0x0007C428 File Offset: 0x0007A628
			public virtual SwitchParameter SeedingPostponed
			{
				set
				{
					base.PowerSharpParameters["SeedingPostponed"] = value;
				}
			}

			// Token: 0x17002DCB RID: 11723
			// (set) Token: 0x06004DE5 RID: 19941 RVA: 0x0007C440 File Offset: 0x0007A640
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002DCC RID: 11724
			// (set) Token: 0x06004DE6 RID: 19942 RVA: 0x0007C458 File Offset: 0x0007A658
			public virtual EnhancedTimeSpan ReplayLagTime
			{
				set
				{
					base.PowerSharpParameters["ReplayLagTime"] = value;
				}
			}

			// Token: 0x17002DCD RID: 11725
			// (set) Token: 0x06004DE7 RID: 19943 RVA: 0x0007C470 File Offset: 0x0007A670
			public virtual EnhancedTimeSpan TruncationLagTime
			{
				set
				{
					base.PowerSharpParameters["TruncationLagTime"] = value;
				}
			}

			// Token: 0x17002DCE RID: 11726
			// (set) Token: 0x06004DE8 RID: 19944 RVA: 0x0007C488 File Offset: 0x0007A688
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DCF RID: 11727
			// (set) Token: 0x06004DE9 RID: 19945 RVA: 0x0007C49B File Offset: 0x0007A69B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DD0 RID: 11728
			// (set) Token: 0x06004DEA RID: 19946 RVA: 0x0007C4B3 File Offset: 0x0007A6B3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DD1 RID: 11729
			// (set) Token: 0x06004DEB RID: 19947 RVA: 0x0007C4CB File Offset: 0x0007A6CB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DD2 RID: 11730
			// (set) Token: 0x06004DEC RID: 19948 RVA: 0x0007C4E3 File Offset: 0x0007A6E3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002DD3 RID: 11731
			// (set) Token: 0x06004DED RID: 19949 RVA: 0x0007C4FB File Offset: 0x0007A6FB
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

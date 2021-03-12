using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A01 RID: 2561
	public class GetMailboxExportRequestCommand : SyntheticCommandWithPipelineInput<MailboxExportRequest, MailboxExportRequest>
	{
		// Token: 0x0600805C RID: 32860 RVA: 0x000BE702 File Offset: 0x000BC902
		private GetMailboxExportRequestCommand() : base("Get-MailboxExportRequest")
		{
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x000BE70F File Offset: 0x000BC90F
		public GetMailboxExportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x000BE71E File Offset: 0x000BC91E
		public virtual GetMailboxExportRequestCommand SetParameters(GetMailboxExportRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x000BE728 File Offset: 0x000BC928
		public virtual GetMailboxExportRequestCommand SetParameters(GetMailboxExportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x000BE732 File Offset: 0x000BC932
		public virtual GetMailboxExportRequestCommand SetParameters(GetMailboxExportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A02 RID: 2562
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005813 RID: 22547
			// (set) Token: 0x06008061 RID: 32865 RVA: 0x000BE73C File Offset: 0x000BC93C
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005814 RID: 22548
			// (set) Token: 0x06008062 RID: 32866 RVA: 0x000BE75A File Offset: 0x000BC95A
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005815 RID: 22549
			// (set) Token: 0x06008063 RID: 32867 RVA: 0x000BE772 File Offset: 0x000BC972
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005816 RID: 22550
			// (set) Token: 0x06008064 RID: 32868 RVA: 0x000BE785 File Offset: 0x000BC985
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005817 RID: 22551
			// (set) Token: 0x06008065 RID: 32869 RVA: 0x000BE798 File Offset: 0x000BC998
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005818 RID: 22552
			// (set) Token: 0x06008066 RID: 32870 RVA: 0x000BE7B0 File Offset: 0x000BC9B0
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005819 RID: 22553
			// (set) Token: 0x06008067 RID: 32871 RVA: 0x000BE7C8 File Offset: 0x000BC9C8
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x1700581A RID: 22554
			// (set) Token: 0x06008068 RID: 32872 RVA: 0x000BE7DB File Offset: 0x000BC9DB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700581B RID: 22555
			// (set) Token: 0x06008069 RID: 32873 RVA: 0x000BE7F9 File Offset: 0x000BC9F9
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700581C RID: 22556
			// (set) Token: 0x0600806A RID: 32874 RVA: 0x000BE80C File Offset: 0x000BCA0C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700581D RID: 22557
			// (set) Token: 0x0600806B RID: 32875 RVA: 0x000BE81F File Offset: 0x000BCA1F
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700581E RID: 22558
			// (set) Token: 0x0600806C RID: 32876 RVA: 0x000BE837 File Offset: 0x000BCA37
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700581F RID: 22559
			// (set) Token: 0x0600806D RID: 32877 RVA: 0x000BE84F File Offset: 0x000BCA4F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005820 RID: 22560
			// (set) Token: 0x0600806E RID: 32878 RVA: 0x000BE867 File Offset: 0x000BCA67
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005821 RID: 22561
			// (set) Token: 0x0600806F RID: 32879 RVA: 0x000BE87F File Offset: 0x000BCA7F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A03 RID: 2563
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005822 RID: 22562
			// (set) Token: 0x06008071 RID: 32881 RVA: 0x000BE89F File Offset: 0x000BCA9F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005823 RID: 22563
			// (set) Token: 0x06008072 RID: 32882 RVA: 0x000BE8BD File Offset: 0x000BCABD
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005824 RID: 22564
			// (set) Token: 0x06008073 RID: 32883 RVA: 0x000BE8D0 File Offset: 0x000BCAD0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005825 RID: 22565
			// (set) Token: 0x06008074 RID: 32884 RVA: 0x000BE8EE File Offset: 0x000BCAEE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005826 RID: 22566
			// (set) Token: 0x06008075 RID: 32885 RVA: 0x000BE901 File Offset: 0x000BCB01
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005827 RID: 22567
			// (set) Token: 0x06008076 RID: 32886 RVA: 0x000BE919 File Offset: 0x000BCB19
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005828 RID: 22568
			// (set) Token: 0x06008077 RID: 32887 RVA: 0x000BE931 File Offset: 0x000BCB31
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005829 RID: 22569
			// (set) Token: 0x06008078 RID: 32888 RVA: 0x000BE949 File Offset: 0x000BCB49
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700582A RID: 22570
			// (set) Token: 0x06008079 RID: 32889 RVA: 0x000BE961 File Offset: 0x000BCB61
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A04 RID: 2564
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700582B RID: 22571
			// (set) Token: 0x0600807B RID: 32891 RVA: 0x000BE981 File Offset: 0x000BCB81
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700582C RID: 22572
			// (set) Token: 0x0600807C RID: 32892 RVA: 0x000BE994 File Offset: 0x000BCB94
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700582D RID: 22573
			// (set) Token: 0x0600807D RID: 32893 RVA: 0x000BE9AC File Offset: 0x000BCBAC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700582E RID: 22574
			// (set) Token: 0x0600807E RID: 32894 RVA: 0x000BE9C4 File Offset: 0x000BCBC4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700582F RID: 22575
			// (set) Token: 0x0600807F RID: 32895 RVA: 0x000BE9DC File Offset: 0x000BCBDC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005830 RID: 22576
			// (set) Token: 0x06008080 RID: 32896 RVA: 0x000BE9F4 File Offset: 0x000BCBF4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}

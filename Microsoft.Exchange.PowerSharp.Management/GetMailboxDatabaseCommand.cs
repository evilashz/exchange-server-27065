using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005EA RID: 1514
	public class GetMailboxDatabaseCommand : SyntheticCommandWithPipelineInput<MailboxDatabase, MailboxDatabase>
	{
		// Token: 0x06004DEF RID: 19951 RVA: 0x0007C51B File Offset: 0x0007A71B
		private GetMailboxDatabaseCommand() : base("Get-MailboxDatabase")
		{
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0007C528 File Offset: 0x0007A728
		public GetMailboxDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0007C537 File Offset: 0x0007A737
		public virtual GetMailboxDatabaseCommand SetParameters(GetMailboxDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0007C541 File Offset: 0x0007A741
		public virtual GetMailboxDatabaseCommand SetParameters(GetMailboxDatabaseCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0007C54B File Offset: 0x0007A74B
		public virtual GetMailboxDatabaseCommand SetParameters(GetMailboxDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005EB RID: 1515
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002DD4 RID: 11732
			// (set) Token: 0x06004DF4 RID: 19956 RVA: 0x0007C555 File Offset: 0x0007A755
			public virtual SwitchParameter DumpsterStatistics
			{
				set
				{
					base.PowerSharpParameters["DumpsterStatistics"] = value;
				}
			}

			// Token: 0x17002DD5 RID: 11733
			// (set) Token: 0x06004DF5 RID: 19957 RVA: 0x0007C56D File Offset: 0x0007A76D
			public virtual SwitchParameter IncludePreExchange2013
			{
				set
				{
					base.PowerSharpParameters["IncludePreExchange2013"] = value;
				}
			}

			// Token: 0x17002DD6 RID: 11734
			// (set) Token: 0x06004DF6 RID: 19958 RVA: 0x0007C585 File Offset: 0x0007A785
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002DD7 RID: 11735
			// (set) Token: 0x06004DF7 RID: 19959 RVA: 0x0007C59D File Offset: 0x0007A79D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DD8 RID: 11736
			// (set) Token: 0x06004DF8 RID: 19960 RVA: 0x0007C5B0 File Offset: 0x0007A7B0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DD9 RID: 11737
			// (set) Token: 0x06004DF9 RID: 19961 RVA: 0x0007C5C8 File Offset: 0x0007A7C8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DDA RID: 11738
			// (set) Token: 0x06004DFA RID: 19962 RVA: 0x0007C5E0 File Offset: 0x0007A7E0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DDB RID: 11739
			// (set) Token: 0x06004DFB RID: 19963 RVA: 0x0007C5F8 File Offset: 0x0007A7F8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005EC RID: 1516
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17002DDC RID: 11740
			// (set) Token: 0x06004DFD RID: 19965 RVA: 0x0007C618 File Offset: 0x0007A818
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002DDD RID: 11741
			// (set) Token: 0x06004DFE RID: 19966 RVA: 0x0007C62B File Offset: 0x0007A82B
			public virtual SwitchParameter DumpsterStatistics
			{
				set
				{
					base.PowerSharpParameters["DumpsterStatistics"] = value;
				}
			}

			// Token: 0x17002DDE RID: 11742
			// (set) Token: 0x06004DFF RID: 19967 RVA: 0x0007C643 File Offset: 0x0007A843
			public virtual SwitchParameter IncludePreExchange2013
			{
				set
				{
					base.PowerSharpParameters["IncludePreExchange2013"] = value;
				}
			}

			// Token: 0x17002DDF RID: 11743
			// (set) Token: 0x06004E00 RID: 19968 RVA: 0x0007C65B File Offset: 0x0007A85B
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002DE0 RID: 11744
			// (set) Token: 0x06004E01 RID: 19969 RVA: 0x0007C673 File Offset: 0x0007A873
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DE1 RID: 11745
			// (set) Token: 0x06004E02 RID: 19970 RVA: 0x0007C686 File Offset: 0x0007A886
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DE2 RID: 11746
			// (set) Token: 0x06004E03 RID: 19971 RVA: 0x0007C69E File Offset: 0x0007A89E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DE3 RID: 11747
			// (set) Token: 0x06004E04 RID: 19972 RVA: 0x0007C6B6 File Offset: 0x0007A8B6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DE4 RID: 11748
			// (set) Token: 0x06004E05 RID: 19973 RVA: 0x0007C6CE File Offset: 0x0007A8CE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005ED RID: 1517
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002DE5 RID: 11749
			// (set) Token: 0x06004E07 RID: 19975 RVA: 0x0007C6EE File Offset: 0x0007A8EE
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002DE6 RID: 11750
			// (set) Token: 0x06004E08 RID: 19976 RVA: 0x0007C701 File Offset: 0x0007A901
			public virtual SwitchParameter DumpsterStatistics
			{
				set
				{
					base.PowerSharpParameters["DumpsterStatistics"] = value;
				}
			}

			// Token: 0x17002DE7 RID: 11751
			// (set) Token: 0x06004E09 RID: 19977 RVA: 0x0007C719 File Offset: 0x0007A919
			public virtual SwitchParameter IncludePreExchange2013
			{
				set
				{
					base.PowerSharpParameters["IncludePreExchange2013"] = value;
				}
			}

			// Token: 0x17002DE8 RID: 11752
			// (set) Token: 0x06004E0A RID: 19978 RVA: 0x0007C731 File Offset: 0x0007A931
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002DE9 RID: 11753
			// (set) Token: 0x06004E0B RID: 19979 RVA: 0x0007C749 File Offset: 0x0007A949
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DEA RID: 11754
			// (set) Token: 0x06004E0C RID: 19980 RVA: 0x0007C75C File Offset: 0x0007A95C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DEB RID: 11755
			// (set) Token: 0x06004E0D RID: 19981 RVA: 0x0007C774 File Offset: 0x0007A974
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DEC RID: 11756
			// (set) Token: 0x06004E0E RID: 19982 RVA: 0x0007C78C File Offset: 0x0007A98C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DED RID: 11757
			// (set) Token: 0x06004E0F RID: 19983 RVA: 0x0007C7A4 File Offset: 0x0007A9A4
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

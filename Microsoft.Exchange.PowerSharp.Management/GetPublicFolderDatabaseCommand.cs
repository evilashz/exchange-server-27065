using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005EE RID: 1518
	public class GetPublicFolderDatabaseCommand : SyntheticCommandWithPipelineInput<PublicFolderDatabase, PublicFolderDatabase>
	{
		// Token: 0x06004E11 RID: 19985 RVA: 0x0007C7C4 File Offset: 0x0007A9C4
		private GetPublicFolderDatabaseCommand() : base("Get-PublicFolderDatabase")
		{
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x0007C7D1 File Offset: 0x0007A9D1
		public GetPublicFolderDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x0007C7E0 File Offset: 0x0007A9E0
		public virtual GetPublicFolderDatabaseCommand SetParameters(GetPublicFolderDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x0007C7EA File Offset: 0x0007A9EA
		public virtual GetPublicFolderDatabaseCommand SetParameters(GetPublicFolderDatabaseCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0007C7F4 File Offset: 0x0007A9F4
		public virtual GetPublicFolderDatabaseCommand SetParameters(GetPublicFolderDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005EF RID: 1519
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002DEE RID: 11758
			// (set) Token: 0x06004E16 RID: 19990 RVA: 0x0007C7FE File Offset: 0x0007A9FE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002DEF RID: 11759
			// (set) Token: 0x06004E17 RID: 19991 RVA: 0x0007C81C File Offset: 0x0007AA1C
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002DF0 RID: 11760
			// (set) Token: 0x06004E18 RID: 19992 RVA: 0x0007C834 File Offset: 0x0007AA34
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DF1 RID: 11761
			// (set) Token: 0x06004E19 RID: 19993 RVA: 0x0007C847 File Offset: 0x0007AA47
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DF2 RID: 11762
			// (set) Token: 0x06004E1A RID: 19994 RVA: 0x0007C85F File Offset: 0x0007AA5F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DF3 RID: 11763
			// (set) Token: 0x06004E1B RID: 19995 RVA: 0x0007C877 File Offset: 0x0007AA77
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DF4 RID: 11764
			// (set) Token: 0x06004E1C RID: 19996 RVA: 0x0007C88F File Offset: 0x0007AA8F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005F0 RID: 1520
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17002DF5 RID: 11765
			// (set) Token: 0x06004E1E RID: 19998 RVA: 0x0007C8AF File Offset: 0x0007AAAF
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002DF6 RID: 11766
			// (set) Token: 0x06004E1F RID: 19999 RVA: 0x0007C8C2 File Offset: 0x0007AAC2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002DF7 RID: 11767
			// (set) Token: 0x06004E20 RID: 20000 RVA: 0x0007C8E0 File Offset: 0x0007AAE0
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002DF8 RID: 11768
			// (set) Token: 0x06004E21 RID: 20001 RVA: 0x0007C8F8 File Offset: 0x0007AAF8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DF9 RID: 11769
			// (set) Token: 0x06004E22 RID: 20002 RVA: 0x0007C90B File Offset: 0x0007AB0B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DFA RID: 11770
			// (set) Token: 0x06004E23 RID: 20003 RVA: 0x0007C923 File Offset: 0x0007AB23
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DFB RID: 11771
			// (set) Token: 0x06004E24 RID: 20004 RVA: 0x0007C93B File Offset: 0x0007AB3B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DFC RID: 11772
			// (set) Token: 0x06004E25 RID: 20005 RVA: 0x0007C953 File Offset: 0x0007AB53
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005F1 RID: 1521
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002DFD RID: 11773
			// (set) Token: 0x06004E27 RID: 20007 RVA: 0x0007C973 File Offset: 0x0007AB73
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002DFE RID: 11774
			// (set) Token: 0x06004E28 RID: 20008 RVA: 0x0007C986 File Offset: 0x0007AB86
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002DFF RID: 11775
			// (set) Token: 0x06004E29 RID: 20009 RVA: 0x0007C9A4 File Offset: 0x0007ABA4
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002E00 RID: 11776
			// (set) Token: 0x06004E2A RID: 20010 RVA: 0x0007C9BC File Offset: 0x0007ABBC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E01 RID: 11777
			// (set) Token: 0x06004E2B RID: 20011 RVA: 0x0007C9CF File Offset: 0x0007ABCF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E02 RID: 11778
			// (set) Token: 0x06004E2C RID: 20012 RVA: 0x0007C9E7 File Offset: 0x0007ABE7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E03 RID: 11779
			// (set) Token: 0x06004E2D RID: 20013 RVA: 0x0007C9FF File Offset: 0x0007ABFF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E04 RID: 11780
			// (set) Token: 0x06004E2E RID: 20014 RVA: 0x0007CA17 File Offset: 0x0007AC17
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

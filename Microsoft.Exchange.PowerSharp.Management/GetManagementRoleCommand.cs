using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200034E RID: 846
	public class GetManagementRoleCommand : SyntheticCommandWithPipelineInput<ExchangeRole, ExchangeRole>
	{
		// Token: 0x06003699 RID: 13977 RVA: 0x0005EB2E File Offset: 0x0005CD2E
		private GetManagementRoleCommand() : base("Get-ManagementRole")
		{
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0005EB3B File Offset: 0x0005CD3B
		public GetManagementRoleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x0005EB4A File Offset: 0x0005CD4A
		public virtual GetManagementRoleCommand SetParameters(GetManagementRoleCommand.ScriptParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x0005EB54 File Offset: 0x0005CD54
		public virtual GetManagementRoleCommand SetParameters(GetManagementRoleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x0005EB5E File Offset: 0x0005CD5E
		public virtual GetManagementRoleCommand SetParameters(GetManagementRoleCommand.GetChildrenParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x0005EB68 File Offset: 0x0005CD68
		public virtual GetManagementRoleCommand SetParameters(GetManagementRoleCommand.RecurseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x0005EB72 File Offset: 0x0005CD72
		public virtual GetManagementRoleCommand SetParameters(GetManagementRoleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200034F RID: 847
		public class ScriptParameters : ParametersBase
		{
			// Token: 0x17001BB6 RID: 7094
			// (set) Token: 0x060036A0 RID: 13984 RVA: 0x0005EB7C File Offset: 0x0005CD7C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001BB7 RID: 7095
			// (set) Token: 0x060036A1 RID: 13985 RVA: 0x0005EB9A File Offset: 0x0005CD9A
			public virtual string Script
			{
				set
				{
					base.PowerSharpParameters["Script"] = value;
				}
			}

			// Token: 0x17001BB8 RID: 7096
			// (set) Token: 0x060036A2 RID: 13986 RVA: 0x0005EBAD File Offset: 0x0005CDAD
			public virtual string ScriptParametersParam
			{
				set
				{
					base.PowerSharpParameters["ScriptParameters"] = value;
				}
			}

			// Token: 0x17001BB9 RID: 7097
			// (set) Token: 0x060036A3 RID: 13987 RVA: 0x0005EBC0 File Offset: 0x0005CDC0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BBA RID: 7098
			// (set) Token: 0x060036A4 RID: 13988 RVA: 0x0005EBDE File Offset: 0x0005CDDE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BBB RID: 7099
			// (set) Token: 0x060036A5 RID: 13989 RVA: 0x0005EBF1 File Offset: 0x0005CDF1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BBC RID: 7100
			// (set) Token: 0x060036A6 RID: 13990 RVA: 0x0005EC09 File Offset: 0x0005CE09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BBD RID: 7101
			// (set) Token: 0x060036A7 RID: 13991 RVA: 0x0005EC21 File Offset: 0x0005CE21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BBE RID: 7102
			// (set) Token: 0x060036A8 RID: 13992 RVA: 0x0005EC39 File Offset: 0x0005CE39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000350 RID: 848
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001BBF RID: 7103
			// (set) Token: 0x060036AA RID: 13994 RVA: 0x0005EC59 File Offset: 0x0005CE59
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001BC0 RID: 7104
			// (set) Token: 0x060036AB RID: 13995 RVA: 0x0005EC77 File Offset: 0x0005CE77
			public virtual string Cmdlet
			{
				set
				{
					base.PowerSharpParameters["Cmdlet"] = value;
				}
			}

			// Token: 0x17001BC1 RID: 7105
			// (set) Token: 0x060036AC RID: 13996 RVA: 0x0005EC8A File Offset: 0x0005CE8A
			public virtual string CmdletParameters
			{
				set
				{
					base.PowerSharpParameters["CmdletParameters"] = value;
				}
			}

			// Token: 0x17001BC2 RID: 7106
			// (set) Token: 0x060036AD RID: 13997 RVA: 0x0005EC9D File Offset: 0x0005CE9D
			public virtual RoleType RoleType
			{
				set
				{
					base.PowerSharpParameters["RoleType"] = value;
				}
			}

			// Token: 0x17001BC3 RID: 7107
			// (set) Token: 0x060036AE RID: 13998 RVA: 0x0005ECB5 File Offset: 0x0005CEB5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BC4 RID: 7108
			// (set) Token: 0x060036AF RID: 13999 RVA: 0x0005ECD3 File Offset: 0x0005CED3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BC5 RID: 7109
			// (set) Token: 0x060036B0 RID: 14000 RVA: 0x0005ECE6 File Offset: 0x0005CEE6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BC6 RID: 7110
			// (set) Token: 0x060036B1 RID: 14001 RVA: 0x0005ECFE File Offset: 0x0005CEFE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BC7 RID: 7111
			// (set) Token: 0x060036B2 RID: 14002 RVA: 0x0005ED16 File Offset: 0x0005CF16
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BC8 RID: 7112
			// (set) Token: 0x060036B3 RID: 14003 RVA: 0x0005ED2E File Offset: 0x0005CF2E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000351 RID: 849
		public class GetChildrenParameters : ParametersBase
		{
			// Token: 0x17001BC9 RID: 7113
			// (set) Token: 0x060036B5 RID: 14005 RVA: 0x0005ED4E File Offset: 0x0005CF4E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001BCA RID: 7114
			// (set) Token: 0x060036B6 RID: 14006 RVA: 0x0005ED6C File Offset: 0x0005CF6C
			public virtual SwitchParameter GetChildren
			{
				set
				{
					base.PowerSharpParameters["GetChildren"] = value;
				}
			}

			// Token: 0x17001BCB RID: 7115
			// (set) Token: 0x060036B7 RID: 14007 RVA: 0x0005ED84 File Offset: 0x0005CF84
			public virtual RoleType RoleType
			{
				set
				{
					base.PowerSharpParameters["RoleType"] = value;
				}
			}

			// Token: 0x17001BCC RID: 7116
			// (set) Token: 0x060036B8 RID: 14008 RVA: 0x0005ED9C File Offset: 0x0005CF9C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BCD RID: 7117
			// (set) Token: 0x060036B9 RID: 14009 RVA: 0x0005EDBA File Offset: 0x0005CFBA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BCE RID: 7118
			// (set) Token: 0x060036BA RID: 14010 RVA: 0x0005EDCD File Offset: 0x0005CFCD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BCF RID: 7119
			// (set) Token: 0x060036BB RID: 14011 RVA: 0x0005EDE5 File Offset: 0x0005CFE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BD0 RID: 7120
			// (set) Token: 0x060036BC RID: 14012 RVA: 0x0005EDFD File Offset: 0x0005CFFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BD1 RID: 7121
			// (set) Token: 0x060036BD RID: 14013 RVA: 0x0005EE15 File Offset: 0x0005D015
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000352 RID: 850
		public class RecurseParameters : ParametersBase
		{
			// Token: 0x17001BD2 RID: 7122
			// (set) Token: 0x060036BF RID: 14015 RVA: 0x0005EE35 File Offset: 0x0005D035
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001BD3 RID: 7123
			// (set) Token: 0x060036C0 RID: 14016 RVA: 0x0005EE53 File Offset: 0x0005D053
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x17001BD4 RID: 7124
			// (set) Token: 0x060036C1 RID: 14017 RVA: 0x0005EE6B File Offset: 0x0005D06B
			public virtual RoleType RoleType
			{
				set
				{
					base.PowerSharpParameters["RoleType"] = value;
				}
			}

			// Token: 0x17001BD5 RID: 7125
			// (set) Token: 0x060036C2 RID: 14018 RVA: 0x0005EE83 File Offset: 0x0005D083
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BD6 RID: 7126
			// (set) Token: 0x060036C3 RID: 14019 RVA: 0x0005EEA1 File Offset: 0x0005D0A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BD7 RID: 7127
			// (set) Token: 0x060036C4 RID: 14020 RVA: 0x0005EEB4 File Offset: 0x0005D0B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BD8 RID: 7128
			// (set) Token: 0x060036C5 RID: 14021 RVA: 0x0005EECC File Offset: 0x0005D0CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BD9 RID: 7129
			// (set) Token: 0x060036C6 RID: 14022 RVA: 0x0005EEE4 File Offset: 0x0005D0E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BDA RID: 7130
			// (set) Token: 0x060036C7 RID: 14023 RVA: 0x0005EEFC File Offset: 0x0005D0FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000353 RID: 851
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001BDB RID: 7131
			// (set) Token: 0x060036C9 RID: 14025 RVA: 0x0005EF1C File Offset: 0x0005D11C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BDC RID: 7132
			// (set) Token: 0x060036CA RID: 14026 RVA: 0x0005EF3A File Offset: 0x0005D13A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BDD RID: 7133
			// (set) Token: 0x060036CB RID: 14027 RVA: 0x0005EF4D File Offset: 0x0005D14D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BDE RID: 7134
			// (set) Token: 0x060036CC RID: 14028 RVA: 0x0005EF65 File Offset: 0x0005D165
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BDF RID: 7135
			// (set) Token: 0x060036CD RID: 14029 RVA: 0x0005EF7D File Offset: 0x0005D17D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BE0 RID: 7136
			// (set) Token: 0x060036CE RID: 14030 RVA: 0x0005EF95 File Offset: 0x0005D195
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

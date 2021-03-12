using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005E1 RID: 1505
	public class MountDatabaseCommand : SyntheticCommandWithPipelineInput<Database, Database>
	{
		// Token: 0x06004DA5 RID: 19877 RVA: 0x0007BF4D File Offset: 0x0007A14D
		private MountDatabaseCommand() : base("Mount-Database")
		{
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x0007BF5A File Offset: 0x0007A15A
		public MountDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x0007BF69 File Offset: 0x0007A169
		public virtual MountDatabaseCommand SetParameters(MountDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0007BF73 File Offset: 0x0007A173
		public virtual MountDatabaseCommand SetParameters(MountDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005E2 RID: 1506
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D9C RID: 11676
			// (set) Token: 0x06004DA9 RID: 19881 RVA: 0x0007BF7D File Offset: 0x0007A17D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002D9D RID: 11677
			// (set) Token: 0x06004DAA RID: 19882 RVA: 0x0007BF95 File Offset: 0x0007A195
			public virtual SwitchParameter AcceptDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptDataLoss"] = value;
				}
			}

			// Token: 0x17002D9E RID: 11678
			// (set) Token: 0x06004DAB RID: 19883 RVA: 0x0007BFAD File Offset: 0x0007A1AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D9F RID: 11679
			// (set) Token: 0x06004DAC RID: 19884 RVA: 0x0007BFC0 File Offset: 0x0007A1C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DA0 RID: 11680
			// (set) Token: 0x06004DAD RID: 19885 RVA: 0x0007BFD8 File Offset: 0x0007A1D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DA1 RID: 11681
			// (set) Token: 0x06004DAE RID: 19886 RVA: 0x0007BFF0 File Offset: 0x0007A1F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DA2 RID: 11682
			// (set) Token: 0x06004DAF RID: 19887 RVA: 0x0007C008 File Offset: 0x0007A208
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002DA3 RID: 11683
			// (set) Token: 0x06004DB0 RID: 19888 RVA: 0x0007C020 File Offset: 0x0007A220
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005E3 RID: 1507
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002DA4 RID: 11684
			// (set) Token: 0x06004DB2 RID: 19890 RVA: 0x0007C040 File Offset: 0x0007A240
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002DA5 RID: 11685
			// (set) Token: 0x06004DB3 RID: 19891 RVA: 0x0007C053 File Offset: 0x0007A253
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002DA6 RID: 11686
			// (set) Token: 0x06004DB4 RID: 19892 RVA: 0x0007C06B File Offset: 0x0007A26B
			public virtual SwitchParameter AcceptDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptDataLoss"] = value;
				}
			}

			// Token: 0x17002DA7 RID: 11687
			// (set) Token: 0x06004DB5 RID: 19893 RVA: 0x0007C083 File Offset: 0x0007A283
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DA8 RID: 11688
			// (set) Token: 0x06004DB6 RID: 19894 RVA: 0x0007C096 File Offset: 0x0007A296
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DA9 RID: 11689
			// (set) Token: 0x06004DB7 RID: 19895 RVA: 0x0007C0AE File Offset: 0x0007A2AE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DAA RID: 11690
			// (set) Token: 0x06004DB8 RID: 19896 RVA: 0x0007C0C6 File Offset: 0x0007A2C6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DAB RID: 11691
			// (set) Token: 0x06004DB9 RID: 19897 RVA: 0x0007C0DE File Offset: 0x0007A2DE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002DAC RID: 11692
			// (set) Token: 0x06004DBA RID: 19898 RVA: 0x0007C0F6 File Offset: 0x0007A2F6
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

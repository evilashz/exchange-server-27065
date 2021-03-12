using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005F2 RID: 1522
	public class MoveDatabasePathCommand : SyntheticCommandWithPipelineInput<Database, Database>
	{
		// Token: 0x06004E30 RID: 20016 RVA: 0x0007CA37 File Offset: 0x0007AC37
		private MoveDatabasePathCommand() : base("Move-DatabasePath")
		{
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x0007CA44 File Offset: 0x0007AC44
		public MoveDatabasePathCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x0007CA53 File Offset: 0x0007AC53
		public virtual MoveDatabasePathCommand SetParameters(MoveDatabasePathCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x0007CA5D File Offset: 0x0007AC5D
		public virtual MoveDatabasePathCommand SetParameters(MoveDatabasePathCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005F3 RID: 1523
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002E05 RID: 11781
			// (set) Token: 0x06004E34 RID: 20020 RVA: 0x0007CA67 File Offset: 0x0007AC67
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002E06 RID: 11782
			// (set) Token: 0x06004E35 RID: 20021 RVA: 0x0007CA7F File Offset: 0x0007AC7F
			public virtual EdbFilePath EdbFilePath
			{
				set
				{
					base.PowerSharpParameters["EdbFilePath"] = value;
				}
			}

			// Token: 0x17002E07 RID: 11783
			// (set) Token: 0x06004E36 RID: 20022 RVA: 0x0007CA92 File Offset: 0x0007AC92
			public virtual NonRootLocalLongFullPath LogFolderPath
			{
				set
				{
					base.PowerSharpParameters["LogFolderPath"] = value;
				}
			}

			// Token: 0x17002E08 RID: 11784
			// (set) Token: 0x06004E37 RID: 20023 RVA: 0x0007CAA5 File Offset: 0x0007ACA5
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002E09 RID: 11785
			// (set) Token: 0x06004E38 RID: 20024 RVA: 0x0007CABD File Offset: 0x0007ACBD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E0A RID: 11786
			// (set) Token: 0x06004E39 RID: 20025 RVA: 0x0007CAD0 File Offset: 0x0007ACD0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E0B RID: 11787
			// (set) Token: 0x06004E3A RID: 20026 RVA: 0x0007CAE8 File Offset: 0x0007ACE8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E0C RID: 11788
			// (set) Token: 0x06004E3B RID: 20027 RVA: 0x0007CB00 File Offset: 0x0007AD00
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E0D RID: 11789
			// (set) Token: 0x06004E3C RID: 20028 RVA: 0x0007CB18 File Offset: 0x0007AD18
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E0E RID: 11790
			// (set) Token: 0x06004E3D RID: 20029 RVA: 0x0007CB30 File Offset: 0x0007AD30
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002E0F RID: 11791
			// (set) Token: 0x06004E3E RID: 20030 RVA: 0x0007CB48 File Offset: 0x0007AD48
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005F4 RID: 1524
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002E10 RID: 11792
			// (set) Token: 0x06004E40 RID: 20032 RVA: 0x0007CB68 File Offset: 0x0007AD68
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002E11 RID: 11793
			// (set) Token: 0x06004E41 RID: 20033 RVA: 0x0007CB7B File Offset: 0x0007AD7B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002E12 RID: 11794
			// (set) Token: 0x06004E42 RID: 20034 RVA: 0x0007CB93 File Offset: 0x0007AD93
			public virtual EdbFilePath EdbFilePath
			{
				set
				{
					base.PowerSharpParameters["EdbFilePath"] = value;
				}
			}

			// Token: 0x17002E13 RID: 11795
			// (set) Token: 0x06004E43 RID: 20035 RVA: 0x0007CBA6 File Offset: 0x0007ADA6
			public virtual NonRootLocalLongFullPath LogFolderPath
			{
				set
				{
					base.PowerSharpParameters["LogFolderPath"] = value;
				}
			}

			// Token: 0x17002E14 RID: 11796
			// (set) Token: 0x06004E44 RID: 20036 RVA: 0x0007CBB9 File Offset: 0x0007ADB9
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002E15 RID: 11797
			// (set) Token: 0x06004E45 RID: 20037 RVA: 0x0007CBD1 File Offset: 0x0007ADD1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E16 RID: 11798
			// (set) Token: 0x06004E46 RID: 20038 RVA: 0x0007CBE4 File Offset: 0x0007ADE4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E17 RID: 11799
			// (set) Token: 0x06004E47 RID: 20039 RVA: 0x0007CBFC File Offset: 0x0007ADFC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E18 RID: 11800
			// (set) Token: 0x06004E48 RID: 20040 RVA: 0x0007CC14 File Offset: 0x0007AE14
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E19 RID: 11801
			// (set) Token: 0x06004E49 RID: 20041 RVA: 0x0007CC2C File Offset: 0x0007AE2C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E1A RID: 11802
			// (set) Token: 0x06004E4A RID: 20042 RVA: 0x0007CC44 File Offset: 0x0007AE44
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002E1B RID: 11803
			// (set) Token: 0x06004E4B RID: 20043 RVA: 0x0007CC5C File Offset: 0x0007AE5C
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

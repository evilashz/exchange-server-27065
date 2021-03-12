using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005E4 RID: 1508
	public class DismountDatabaseCommand : SyntheticCommandWithPipelineInput<Database, Database>
	{
		// Token: 0x06004DBC RID: 19900 RVA: 0x0007C116 File Offset: 0x0007A316
		private DismountDatabaseCommand() : base("Dismount-Database")
		{
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x0007C123 File Offset: 0x0007A323
		public DismountDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x0007C132 File Offset: 0x0007A332
		public virtual DismountDatabaseCommand SetParameters(DismountDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x0007C13C File Offset: 0x0007A33C
		public virtual DismountDatabaseCommand SetParameters(DismountDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005E5 RID: 1509
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002DAD RID: 11693
			// (set) Token: 0x06004DC0 RID: 19904 RVA: 0x0007C146 File Offset: 0x0007A346
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DAE RID: 11694
			// (set) Token: 0x06004DC1 RID: 19905 RVA: 0x0007C159 File Offset: 0x0007A359
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DAF RID: 11695
			// (set) Token: 0x06004DC2 RID: 19906 RVA: 0x0007C171 File Offset: 0x0007A371
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DB0 RID: 11696
			// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x0007C189 File Offset: 0x0007A389
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DB1 RID: 11697
			// (set) Token: 0x06004DC4 RID: 19908 RVA: 0x0007C1A1 File Offset: 0x0007A3A1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002DB2 RID: 11698
			// (set) Token: 0x06004DC5 RID: 19909 RVA: 0x0007C1B9 File Offset: 0x0007A3B9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002DB3 RID: 11699
			// (set) Token: 0x06004DC6 RID: 19910 RVA: 0x0007C1D1 File Offset: 0x0007A3D1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005E6 RID: 1510
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002DB4 RID: 11700
			// (set) Token: 0x06004DC8 RID: 19912 RVA: 0x0007C1F1 File Offset: 0x0007A3F1
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002DB5 RID: 11701
			// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x0007C204 File Offset: 0x0007A404
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002DB6 RID: 11702
			// (set) Token: 0x06004DCA RID: 19914 RVA: 0x0007C217 File Offset: 0x0007A417
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002DB7 RID: 11703
			// (set) Token: 0x06004DCB RID: 19915 RVA: 0x0007C22F File Offset: 0x0007A42F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002DB8 RID: 11704
			// (set) Token: 0x06004DCC RID: 19916 RVA: 0x0007C247 File Offset: 0x0007A447
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002DB9 RID: 11705
			// (set) Token: 0x06004DCD RID: 19917 RVA: 0x0007C25F File Offset: 0x0007A45F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002DBA RID: 11706
			// (set) Token: 0x06004DCE RID: 19918 RVA: 0x0007C277 File Offset: 0x0007A477
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002DBB RID: 11707
			// (set) Token: 0x06004DCF RID: 19919 RVA: 0x0007C28F File Offset: 0x0007A48F
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

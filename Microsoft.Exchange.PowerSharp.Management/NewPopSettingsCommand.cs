using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000309 RID: 777
	public class NewPopSettingsCommand : SyntheticCommandWithPipelineInput<Pop3AdConfiguration, Pop3AdConfiguration>
	{
		// Token: 0x060033AC RID: 13228 RVA: 0x0005AE2C File Offset: 0x0005902C
		private NewPopSettingsCommand() : base("New-PopSettings")
		{
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x0005AE39 File Offset: 0x00059039
		public NewPopSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x0005AE48 File Offset: 0x00059048
		public virtual NewPopSettingsCommand SetParameters(NewPopSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200030A RID: 778
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001953 RID: 6483
			// (set) Token: 0x060033AF RID: 13231 RVA: 0x0005AE52 File Offset: 0x00059052
			public virtual string ExchangePath
			{
				set
				{
					base.PowerSharpParameters["ExchangePath"] = value;
				}
			}

			// Token: 0x17001954 RID: 6484
			// (set) Token: 0x060033B0 RID: 13232 RVA: 0x0005AE65 File Offset: 0x00059065
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001955 RID: 6485
			// (set) Token: 0x060033B1 RID: 13233 RVA: 0x0005AE78 File Offset: 0x00059078
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001956 RID: 6486
			// (set) Token: 0x060033B2 RID: 13234 RVA: 0x0005AE90 File Offset: 0x00059090
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001957 RID: 6487
			// (set) Token: 0x060033B3 RID: 13235 RVA: 0x0005AEA8 File Offset: 0x000590A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001958 RID: 6488
			// (set) Token: 0x060033B4 RID: 13236 RVA: 0x0005AEC0 File Offset: 0x000590C0
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

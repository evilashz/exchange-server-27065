using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B6B RID: 2923
	public class NewUMCallRouterSettingsCommand : SyntheticCommandWithPipelineInput<SIPFEServerConfiguration, SIPFEServerConfiguration>
	{
		// Token: 0x06008DAB RID: 36267 RVA: 0x000CF95C File Offset: 0x000CDB5C
		private NewUMCallRouterSettingsCommand() : base("New-UMCallRouterSettings")
		{
		}

		// Token: 0x06008DAC RID: 36268 RVA: 0x000CF969 File Offset: 0x000CDB69
		public NewUMCallRouterSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008DAD RID: 36269 RVA: 0x000CF978 File Offset: 0x000CDB78
		public virtual NewUMCallRouterSettingsCommand SetParameters(NewUMCallRouterSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B6C RID: 2924
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700628E RID: 25230
			// (set) Token: 0x06008DAE RID: 36270 RVA: 0x000CF982 File Offset: 0x000CDB82
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700628F RID: 25231
			// (set) Token: 0x06008DAF RID: 36271 RVA: 0x000CF995 File Offset: 0x000CDB95
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006290 RID: 25232
			// (set) Token: 0x06008DB0 RID: 36272 RVA: 0x000CF9AD File Offset: 0x000CDBAD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006291 RID: 25233
			// (set) Token: 0x06008DB1 RID: 36273 RVA: 0x000CF9C5 File Offset: 0x000CDBC5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006292 RID: 25234
			// (set) Token: 0x06008DB2 RID: 36274 RVA: 0x000CF9DD File Offset: 0x000CDBDD
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

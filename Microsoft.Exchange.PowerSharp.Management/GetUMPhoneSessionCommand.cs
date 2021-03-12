using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B5A RID: 2906
	public class GetUMPhoneSessionCommand : SyntheticCommandWithPipelineInput<UMPhoneSession, UMPhoneSession>
	{
		// Token: 0x06008D2F RID: 36143 RVA: 0x000CEFA3 File Offset: 0x000CD1A3
		private GetUMPhoneSessionCommand() : base("Get-UMPhoneSession")
		{
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x000CEFB0 File Offset: 0x000CD1B0
		public GetUMPhoneSessionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x000CEFBF File Offset: 0x000CD1BF
		public virtual GetUMPhoneSessionCommand SetParameters(GetUMPhoneSessionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x000CEFC9 File Offset: 0x000CD1C9
		public virtual GetUMPhoneSessionCommand SetParameters(GetUMPhoneSessionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B5B RID: 2907
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006234 RID: 25140
			// (set) Token: 0x06008D33 RID: 36147 RVA: 0x000CEFD3 File Offset: 0x000CD1D3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMPhoneSessionIdentityParameter(value) : null);
				}
			}

			// Token: 0x17006235 RID: 25141
			// (set) Token: 0x06008D34 RID: 36148 RVA: 0x000CEFF1 File Offset: 0x000CD1F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006236 RID: 25142
			// (set) Token: 0x06008D35 RID: 36149 RVA: 0x000CF004 File Offset: 0x000CD204
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006237 RID: 25143
			// (set) Token: 0x06008D36 RID: 36150 RVA: 0x000CF01C File Offset: 0x000CD21C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006238 RID: 25144
			// (set) Token: 0x06008D37 RID: 36151 RVA: 0x000CF034 File Offset: 0x000CD234
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006239 RID: 25145
			// (set) Token: 0x06008D38 RID: 36152 RVA: 0x000CF04C File Offset: 0x000CD24C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B5C RID: 2908
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700623A RID: 25146
			// (set) Token: 0x06008D3A RID: 36154 RVA: 0x000CF06C File Offset: 0x000CD26C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700623B RID: 25147
			// (set) Token: 0x06008D3B RID: 36155 RVA: 0x000CF07F File Offset: 0x000CD27F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700623C RID: 25148
			// (set) Token: 0x06008D3C RID: 36156 RVA: 0x000CF097 File Offset: 0x000CD297
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700623D RID: 25149
			// (set) Token: 0x06008D3D RID: 36157 RVA: 0x000CF0AF File Offset: 0x000CD2AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700623E RID: 25150
			// (set) Token: 0x06008D3E RID: 36158 RVA: 0x000CF0C7 File Offset: 0x000CD2C7
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

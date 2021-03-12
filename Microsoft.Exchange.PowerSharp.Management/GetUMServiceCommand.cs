using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B5D RID: 2909
	public class GetUMServiceCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06008D40 RID: 36160 RVA: 0x000CF0E7 File Offset: 0x000CD2E7
		private GetUMServiceCommand() : base("Get-UMService")
		{
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x000CF0F4 File Offset: 0x000CD2F4
		public GetUMServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D42 RID: 36162 RVA: 0x000CF103 File Offset: 0x000CD303
		public virtual GetUMServiceCommand SetParameters(GetUMServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x000CF10D File Offset: 0x000CD30D
		public virtual GetUMServiceCommand SetParameters(GetUMServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B5E RID: 2910
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700623F RID: 25151
			// (set) Token: 0x06008D44 RID: 36164 RVA: 0x000CF117 File Offset: 0x000CD317
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006240 RID: 25152
			// (set) Token: 0x06008D45 RID: 36165 RVA: 0x000CF12A File Offset: 0x000CD32A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006241 RID: 25153
			// (set) Token: 0x06008D46 RID: 36166 RVA: 0x000CF142 File Offset: 0x000CD342
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006242 RID: 25154
			// (set) Token: 0x06008D47 RID: 36167 RVA: 0x000CF15A File Offset: 0x000CD35A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006243 RID: 25155
			// (set) Token: 0x06008D48 RID: 36168 RVA: 0x000CF172 File Offset: 0x000CD372
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B5F RID: 2911
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006244 RID: 25156
			// (set) Token: 0x06008D4A RID: 36170 RVA: 0x000CF192 File Offset: 0x000CD392
			public virtual UMServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006245 RID: 25157
			// (set) Token: 0x06008D4B RID: 36171 RVA: 0x000CF1A5 File Offset: 0x000CD3A5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006246 RID: 25158
			// (set) Token: 0x06008D4C RID: 36172 RVA: 0x000CF1B8 File Offset: 0x000CD3B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006247 RID: 25159
			// (set) Token: 0x06008D4D RID: 36173 RVA: 0x000CF1D0 File Offset: 0x000CD3D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006248 RID: 25160
			// (set) Token: 0x06008D4E RID: 36174 RVA: 0x000CF1E8 File Offset: 0x000CD3E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006249 RID: 25161
			// (set) Token: 0x06008D4F RID: 36175 RVA: 0x000CF200 File Offset: 0x000CD400
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

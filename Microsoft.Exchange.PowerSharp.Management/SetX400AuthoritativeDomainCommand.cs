using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007B9 RID: 1977
	public class SetX400AuthoritativeDomainCommand : SyntheticCommandWithPipelineInputNoOutput<X400AuthoritativeDomain>
	{
		// Token: 0x060062F6 RID: 25334 RVA: 0x00097D90 File Offset: 0x00095F90
		private SetX400AuthoritativeDomainCommand() : base("Set-X400AuthoritativeDomain")
		{
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x00097D9D File Offset: 0x00095F9D
		public SetX400AuthoritativeDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x00097DAC File Offset: 0x00095FAC
		public virtual SetX400AuthoritativeDomainCommand SetParameters(SetX400AuthoritativeDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x00097DB6 File Offset: 0x00095FB6
		public virtual SetX400AuthoritativeDomainCommand SetParameters(SetX400AuthoritativeDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007BA RID: 1978
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F3D RID: 16189
			// (set) Token: 0x060062FA RID: 25338 RVA: 0x00097DC0 File Offset: 0x00095FC0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F3E RID: 16190
			// (set) Token: 0x060062FB RID: 25339 RVA: 0x00097DD3 File Offset: 0x00095FD3
			public virtual X400Domain X400DomainName
			{
				set
				{
					base.PowerSharpParameters["X400DomainName"] = value;
				}
			}

			// Token: 0x17003F3F RID: 16191
			// (set) Token: 0x060062FC RID: 25340 RVA: 0x00097DE6 File Offset: 0x00095FE6
			public virtual bool X400ExternalRelay
			{
				set
				{
					base.PowerSharpParameters["X400ExternalRelay"] = value;
				}
			}

			// Token: 0x17003F40 RID: 16192
			// (set) Token: 0x060062FD RID: 25341 RVA: 0x00097DFE File Offset: 0x00095FFE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003F41 RID: 16193
			// (set) Token: 0x060062FE RID: 25342 RVA: 0x00097E11 File Offset: 0x00096011
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F42 RID: 16194
			// (set) Token: 0x060062FF RID: 25343 RVA: 0x00097E29 File Offset: 0x00096029
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F43 RID: 16195
			// (set) Token: 0x06006300 RID: 25344 RVA: 0x00097E41 File Offset: 0x00096041
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F44 RID: 16196
			// (set) Token: 0x06006301 RID: 25345 RVA: 0x00097E59 File Offset: 0x00096059
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F45 RID: 16197
			// (set) Token: 0x06006302 RID: 25346 RVA: 0x00097E71 File Offset: 0x00096071
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007BB RID: 1979
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003F46 RID: 16198
			// (set) Token: 0x06006304 RID: 25348 RVA: 0x00097E91 File Offset: 0x00096091
			public virtual X400AuthoritativeDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003F47 RID: 16199
			// (set) Token: 0x06006305 RID: 25349 RVA: 0x00097EA4 File Offset: 0x000960A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F48 RID: 16200
			// (set) Token: 0x06006306 RID: 25350 RVA: 0x00097EB7 File Offset: 0x000960B7
			public virtual X400Domain X400DomainName
			{
				set
				{
					base.PowerSharpParameters["X400DomainName"] = value;
				}
			}

			// Token: 0x17003F49 RID: 16201
			// (set) Token: 0x06006307 RID: 25351 RVA: 0x00097ECA File Offset: 0x000960CA
			public virtual bool X400ExternalRelay
			{
				set
				{
					base.PowerSharpParameters["X400ExternalRelay"] = value;
				}
			}

			// Token: 0x17003F4A RID: 16202
			// (set) Token: 0x06006308 RID: 25352 RVA: 0x00097EE2 File Offset: 0x000960E2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003F4B RID: 16203
			// (set) Token: 0x06006309 RID: 25353 RVA: 0x00097EF5 File Offset: 0x000960F5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F4C RID: 16204
			// (set) Token: 0x0600630A RID: 25354 RVA: 0x00097F0D File Offset: 0x0009610D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F4D RID: 16205
			// (set) Token: 0x0600630B RID: 25355 RVA: 0x00097F25 File Offset: 0x00096125
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F4E RID: 16206
			// (set) Token: 0x0600630C RID: 25356 RVA: 0x00097F3D File Offset: 0x0009613D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F4F RID: 16207
			// (set) Token: 0x0600630D RID: 25357 RVA: 0x00097F55 File Offset: 0x00096155
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

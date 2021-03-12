using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007BC RID: 1980
	public class EnableExchangeCertificateCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x0600630F RID: 25359 RVA: 0x00097F75 File Offset: 0x00096175
		private EnableExchangeCertificateCommand() : base("Enable-ExchangeCertificate")
		{
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x00097F82 File Offset: 0x00096182
		public EnableExchangeCertificateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x00097F91 File Offset: 0x00096191
		public virtual EnableExchangeCertificateCommand SetParameters(EnableExchangeCertificateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x00097F9B File Offset: 0x0009619B
		public virtual EnableExchangeCertificateCommand SetParameters(EnableExchangeCertificateCommand.ThumbprintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x00097FA5 File Offset: 0x000961A5
		public virtual EnableExchangeCertificateCommand SetParameters(EnableExchangeCertificateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007BD RID: 1981
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003F50 RID: 16208
			// (set) Token: 0x06006314 RID: 25364 RVA: 0x00097FAF File Offset: 0x000961AF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeCertificateIdParameter(value) : null);
				}
			}

			// Token: 0x17003F51 RID: 16209
			// (set) Token: 0x06006315 RID: 25365 RVA: 0x00097FCD File Offset: 0x000961CD
			public virtual AllowedServices Services
			{
				set
				{
					base.PowerSharpParameters["Services"] = value;
				}
			}

			// Token: 0x17003F52 RID: 16210
			// (set) Token: 0x06006316 RID: 25366 RVA: 0x00097FE5 File Offset: 0x000961E5
			public virtual SwitchParameter NetworkServiceAllowed
			{
				set
				{
					base.PowerSharpParameters["NetworkServiceAllowed"] = value;
				}
			}

			// Token: 0x17003F53 RID: 16211
			// (set) Token: 0x06006317 RID: 25367 RVA: 0x00097FFD File Offset: 0x000961FD
			public virtual SwitchParameter DoNotRequireSsl
			{
				set
				{
					base.PowerSharpParameters["DoNotRequireSsl"] = value;
				}
			}

			// Token: 0x17003F54 RID: 16212
			// (set) Token: 0x06006318 RID: 25368 RVA: 0x00098015 File Offset: 0x00096215
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003F55 RID: 16213
			// (set) Token: 0x06006319 RID: 25369 RVA: 0x0009802D File Offset: 0x0009622D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F56 RID: 16214
			// (set) Token: 0x0600631A RID: 25370 RVA: 0x00098040 File Offset: 0x00096240
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F57 RID: 16215
			// (set) Token: 0x0600631B RID: 25371 RVA: 0x00098058 File Offset: 0x00096258
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F58 RID: 16216
			// (set) Token: 0x0600631C RID: 25372 RVA: 0x00098070 File Offset: 0x00096270
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F59 RID: 16217
			// (set) Token: 0x0600631D RID: 25373 RVA: 0x00098088 File Offset: 0x00096288
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F5A RID: 16218
			// (set) Token: 0x0600631E RID: 25374 RVA: 0x000980A0 File Offset: 0x000962A0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007BE RID: 1982
		public class ThumbprintParameters : ParametersBase
		{
			// Token: 0x17003F5B RID: 16219
			// (set) Token: 0x06006320 RID: 25376 RVA: 0x000980C0 File Offset: 0x000962C0
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x17003F5C RID: 16220
			// (set) Token: 0x06006321 RID: 25377 RVA: 0x000980D3 File Offset: 0x000962D3
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003F5D RID: 16221
			// (set) Token: 0x06006322 RID: 25378 RVA: 0x000980E6 File Offset: 0x000962E6
			public virtual AllowedServices Services
			{
				set
				{
					base.PowerSharpParameters["Services"] = value;
				}
			}

			// Token: 0x17003F5E RID: 16222
			// (set) Token: 0x06006323 RID: 25379 RVA: 0x000980FE File Offset: 0x000962FE
			public virtual SwitchParameter NetworkServiceAllowed
			{
				set
				{
					base.PowerSharpParameters["NetworkServiceAllowed"] = value;
				}
			}

			// Token: 0x17003F5F RID: 16223
			// (set) Token: 0x06006324 RID: 25380 RVA: 0x00098116 File Offset: 0x00096316
			public virtual SwitchParameter DoNotRequireSsl
			{
				set
				{
					base.PowerSharpParameters["DoNotRequireSsl"] = value;
				}
			}

			// Token: 0x17003F60 RID: 16224
			// (set) Token: 0x06006325 RID: 25381 RVA: 0x0009812E File Offset: 0x0009632E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003F61 RID: 16225
			// (set) Token: 0x06006326 RID: 25382 RVA: 0x00098146 File Offset: 0x00096346
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F62 RID: 16226
			// (set) Token: 0x06006327 RID: 25383 RVA: 0x00098159 File Offset: 0x00096359
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F63 RID: 16227
			// (set) Token: 0x06006328 RID: 25384 RVA: 0x00098171 File Offset: 0x00096371
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F64 RID: 16228
			// (set) Token: 0x06006329 RID: 25385 RVA: 0x00098189 File Offset: 0x00096389
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F65 RID: 16229
			// (set) Token: 0x0600632A RID: 25386 RVA: 0x000981A1 File Offset: 0x000963A1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F66 RID: 16230
			// (set) Token: 0x0600632B RID: 25387 RVA: 0x000981B9 File Offset: 0x000963B9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007BF RID: 1983
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F67 RID: 16231
			// (set) Token: 0x0600632D RID: 25389 RVA: 0x000981D9 File Offset: 0x000963D9
			public virtual AllowedServices Services
			{
				set
				{
					base.PowerSharpParameters["Services"] = value;
				}
			}

			// Token: 0x17003F68 RID: 16232
			// (set) Token: 0x0600632E RID: 25390 RVA: 0x000981F1 File Offset: 0x000963F1
			public virtual SwitchParameter NetworkServiceAllowed
			{
				set
				{
					base.PowerSharpParameters["NetworkServiceAllowed"] = value;
				}
			}

			// Token: 0x17003F69 RID: 16233
			// (set) Token: 0x0600632F RID: 25391 RVA: 0x00098209 File Offset: 0x00096409
			public virtual SwitchParameter DoNotRequireSsl
			{
				set
				{
					base.PowerSharpParameters["DoNotRequireSsl"] = value;
				}
			}

			// Token: 0x17003F6A RID: 16234
			// (set) Token: 0x06006330 RID: 25392 RVA: 0x00098221 File Offset: 0x00096421
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003F6B RID: 16235
			// (set) Token: 0x06006331 RID: 25393 RVA: 0x00098239 File Offset: 0x00096439
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F6C RID: 16236
			// (set) Token: 0x06006332 RID: 25394 RVA: 0x0009824C File Offset: 0x0009644C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F6D RID: 16237
			// (set) Token: 0x06006333 RID: 25395 RVA: 0x00098264 File Offset: 0x00096464
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F6E RID: 16238
			// (set) Token: 0x06006334 RID: 25396 RVA: 0x0009827C File Offset: 0x0009647C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F6F RID: 16239
			// (set) Token: 0x06006335 RID: 25397 RVA: 0x00098294 File Offset: 0x00096494
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F70 RID: 16240
			// (set) Token: 0x06006336 RID: 25398 RVA: 0x000982AC File Offset: 0x000964AC
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

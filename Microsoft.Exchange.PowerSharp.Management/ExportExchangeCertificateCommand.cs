using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007C0 RID: 1984
	public class ExportExchangeCertificateCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06006338 RID: 25400 RVA: 0x000982CC File Offset: 0x000964CC
		private ExportExchangeCertificateCommand() : base("Export-ExchangeCertificate")
		{
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x000982D9 File Offset: 0x000964D9
		public ExportExchangeCertificateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x000982E8 File Offset: 0x000964E8
		public virtual ExportExchangeCertificateCommand SetParameters(ExportExchangeCertificateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x000982F2 File Offset: 0x000964F2
		public virtual ExportExchangeCertificateCommand SetParameters(ExportExchangeCertificateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x000982FC File Offset: 0x000964FC
		public virtual ExportExchangeCertificateCommand SetParameters(ExportExchangeCertificateCommand.ThumbprintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007C1 RID: 1985
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003F71 RID: 16241
			// (set) Token: 0x0600633D RID: 25405 RVA: 0x00098306 File Offset: 0x00096506
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeCertificateIdParameter(value) : null);
				}
			}

			// Token: 0x17003F72 RID: 16242
			// (set) Token: 0x0600633E RID: 25406 RVA: 0x00098324 File Offset: 0x00096524
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F73 RID: 16243
			// (set) Token: 0x0600633F RID: 25407 RVA: 0x00098337 File Offset: 0x00096537
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003F74 RID: 16244
			// (set) Token: 0x06006340 RID: 25408 RVA: 0x0009834A File Offset: 0x0009654A
			public virtual SwitchParameter BinaryEncoded
			{
				set
				{
					base.PowerSharpParameters["BinaryEncoded"] = value;
				}
			}

			// Token: 0x17003F75 RID: 16245
			// (set) Token: 0x06006341 RID: 25409 RVA: 0x00098362 File Offset: 0x00096562
			public virtual string FileName
			{
				set
				{
					base.PowerSharpParameters["FileName"] = value;
				}
			}

			// Token: 0x17003F76 RID: 16246
			// (set) Token: 0x06006342 RID: 25410 RVA: 0x00098375 File Offset: 0x00096575
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F77 RID: 16247
			// (set) Token: 0x06006343 RID: 25411 RVA: 0x0009838D File Offset: 0x0009658D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F78 RID: 16248
			// (set) Token: 0x06006344 RID: 25412 RVA: 0x000983A5 File Offset: 0x000965A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F79 RID: 16249
			// (set) Token: 0x06006345 RID: 25413 RVA: 0x000983BD File Offset: 0x000965BD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F7A RID: 16250
			// (set) Token: 0x06006346 RID: 25414 RVA: 0x000983D5 File Offset: 0x000965D5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007C2 RID: 1986
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F7B RID: 16251
			// (set) Token: 0x06006348 RID: 25416 RVA: 0x000983F5 File Offset: 0x000965F5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F7C RID: 16252
			// (set) Token: 0x06006349 RID: 25417 RVA: 0x00098408 File Offset: 0x00096608
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003F7D RID: 16253
			// (set) Token: 0x0600634A RID: 25418 RVA: 0x0009841B File Offset: 0x0009661B
			public virtual SwitchParameter BinaryEncoded
			{
				set
				{
					base.PowerSharpParameters["BinaryEncoded"] = value;
				}
			}

			// Token: 0x17003F7E RID: 16254
			// (set) Token: 0x0600634B RID: 25419 RVA: 0x00098433 File Offset: 0x00096633
			public virtual string FileName
			{
				set
				{
					base.PowerSharpParameters["FileName"] = value;
				}
			}

			// Token: 0x17003F7F RID: 16255
			// (set) Token: 0x0600634C RID: 25420 RVA: 0x00098446 File Offset: 0x00096646
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F80 RID: 16256
			// (set) Token: 0x0600634D RID: 25421 RVA: 0x0009845E File Offset: 0x0009665E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F81 RID: 16257
			// (set) Token: 0x0600634E RID: 25422 RVA: 0x00098476 File Offset: 0x00096676
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F82 RID: 16258
			// (set) Token: 0x0600634F RID: 25423 RVA: 0x0009848E File Offset: 0x0009668E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F83 RID: 16259
			// (set) Token: 0x06006350 RID: 25424 RVA: 0x000984A6 File Offset: 0x000966A6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007C3 RID: 1987
		public class ThumbprintParameters : ParametersBase
		{
			// Token: 0x17003F84 RID: 16260
			// (set) Token: 0x06006352 RID: 25426 RVA: 0x000984C6 File Offset: 0x000966C6
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003F85 RID: 16261
			// (set) Token: 0x06006353 RID: 25427 RVA: 0x000984D9 File Offset: 0x000966D9
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x17003F86 RID: 16262
			// (set) Token: 0x06006354 RID: 25428 RVA: 0x000984EC File Offset: 0x000966EC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F87 RID: 16263
			// (set) Token: 0x06006355 RID: 25429 RVA: 0x000984FF File Offset: 0x000966FF
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003F88 RID: 16264
			// (set) Token: 0x06006356 RID: 25430 RVA: 0x00098512 File Offset: 0x00096712
			public virtual SwitchParameter BinaryEncoded
			{
				set
				{
					base.PowerSharpParameters["BinaryEncoded"] = value;
				}
			}

			// Token: 0x17003F89 RID: 16265
			// (set) Token: 0x06006357 RID: 25431 RVA: 0x0009852A File Offset: 0x0009672A
			public virtual string FileName
			{
				set
				{
					base.PowerSharpParameters["FileName"] = value;
				}
			}

			// Token: 0x17003F8A RID: 16266
			// (set) Token: 0x06006358 RID: 25432 RVA: 0x0009853D File Offset: 0x0009673D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F8B RID: 16267
			// (set) Token: 0x06006359 RID: 25433 RVA: 0x00098555 File Offset: 0x00096755
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F8C RID: 16268
			// (set) Token: 0x0600635A RID: 25434 RVA: 0x0009856D File Offset: 0x0009676D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F8D RID: 16269
			// (set) Token: 0x0600635B RID: 25435 RVA: 0x00098585 File Offset: 0x00096785
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F8E RID: 16270
			// (set) Token: 0x0600635C RID: 25436 RVA: 0x0009859D File Offset: 0x0009679D
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

using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007C9 RID: 1993
	public class ImportExchangeCertificateCommand : SyntheticCommandWithPipelineInput<string, string[]>
	{
		// Token: 0x06006385 RID: 25477 RVA: 0x000988A3 File Offset: 0x00096AA3
		private ImportExchangeCertificateCommand() : base("Import-ExchangeCertificate")
		{
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x000988B0 File Offset: 0x00096AB0
		public ImportExchangeCertificateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x000988BF File Offset: 0x00096ABF
		public virtual ImportExchangeCertificateCommand SetParameters(ImportExchangeCertificateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x000988C9 File Offset: 0x00096AC9
		public virtual ImportExchangeCertificateCommand SetParameters(ImportExchangeCertificateCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x000988D3 File Offset: 0x00096AD3
		public virtual ImportExchangeCertificateCommand SetParameters(ImportExchangeCertificateCommand.FileDataParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x000988DD File Offset: 0x00096ADD
		public virtual ImportExchangeCertificateCommand SetParameters(ImportExchangeCertificateCommand.FileNameParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007CA RID: 1994
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003FAC RID: 16300
			// (set) Token: 0x0600638B RID: 25483 RVA: 0x000988E7 File Offset: 0x00096AE7
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FAD RID: 16301
			// (set) Token: 0x0600638C RID: 25484 RVA: 0x000988FA File Offset: 0x00096AFA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FAE RID: 16302
			// (set) Token: 0x0600638D RID: 25485 RVA: 0x0009890D File Offset: 0x00096B0D
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003FAF RID: 16303
			// (set) Token: 0x0600638E RID: 25486 RVA: 0x00098920 File Offset: 0x00096B20
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x17003FB0 RID: 16304
			// (set) Token: 0x0600638F RID: 25487 RVA: 0x00098938 File Offset: 0x00096B38
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17003FB1 RID: 16305
			// (set) Token: 0x06006390 RID: 25488 RVA: 0x0009894B File Offset: 0x00096B4B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FB2 RID: 16306
			// (set) Token: 0x06006391 RID: 25489 RVA: 0x00098963 File Offset: 0x00096B63
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FB3 RID: 16307
			// (set) Token: 0x06006392 RID: 25490 RVA: 0x0009897B File Offset: 0x00096B7B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FB4 RID: 16308
			// (set) Token: 0x06006393 RID: 25491 RVA: 0x00098993 File Offset: 0x00096B93
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003FB5 RID: 16309
			// (set) Token: 0x06006394 RID: 25492 RVA: 0x000989AB File Offset: 0x00096BAB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007CB RID: 1995
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x17003FB6 RID: 16310
			// (set) Token: 0x06006396 RID: 25494 RVA: 0x000989CB File Offset: 0x00096BCB
			public virtual string Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17003FB7 RID: 16311
			// (set) Token: 0x06006397 RID: 25495 RVA: 0x000989DE File Offset: 0x00096BDE
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FB8 RID: 16312
			// (set) Token: 0x06006398 RID: 25496 RVA: 0x000989F1 File Offset: 0x00096BF1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FB9 RID: 16313
			// (set) Token: 0x06006399 RID: 25497 RVA: 0x00098A04 File Offset: 0x00096C04
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003FBA RID: 16314
			// (set) Token: 0x0600639A RID: 25498 RVA: 0x00098A17 File Offset: 0x00096C17
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x17003FBB RID: 16315
			// (set) Token: 0x0600639B RID: 25499 RVA: 0x00098A2F File Offset: 0x00096C2F
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17003FBC RID: 16316
			// (set) Token: 0x0600639C RID: 25500 RVA: 0x00098A42 File Offset: 0x00096C42
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FBD RID: 16317
			// (set) Token: 0x0600639D RID: 25501 RVA: 0x00098A5A File Offset: 0x00096C5A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FBE RID: 16318
			// (set) Token: 0x0600639E RID: 25502 RVA: 0x00098A72 File Offset: 0x00096C72
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FBF RID: 16319
			// (set) Token: 0x0600639F RID: 25503 RVA: 0x00098A8A File Offset: 0x00096C8A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003FC0 RID: 16320
			// (set) Token: 0x060063A0 RID: 25504 RVA: 0x00098AA2 File Offset: 0x00096CA2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007CC RID: 1996
		public class FileDataParameters : ParametersBase
		{
			// Token: 0x17003FC1 RID: 16321
			// (set) Token: 0x060063A2 RID: 25506 RVA: 0x00098AC2 File Offset: 0x00096CC2
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17003FC2 RID: 16322
			// (set) Token: 0x060063A3 RID: 25507 RVA: 0x00098ADA File Offset: 0x00096CDA
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FC3 RID: 16323
			// (set) Token: 0x060063A4 RID: 25508 RVA: 0x00098AED File Offset: 0x00096CED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FC4 RID: 16324
			// (set) Token: 0x060063A5 RID: 25509 RVA: 0x00098B00 File Offset: 0x00096D00
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003FC5 RID: 16325
			// (set) Token: 0x060063A6 RID: 25510 RVA: 0x00098B13 File Offset: 0x00096D13
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x17003FC6 RID: 16326
			// (set) Token: 0x060063A7 RID: 25511 RVA: 0x00098B2B File Offset: 0x00096D2B
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17003FC7 RID: 16327
			// (set) Token: 0x060063A8 RID: 25512 RVA: 0x00098B3E File Offset: 0x00096D3E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FC8 RID: 16328
			// (set) Token: 0x060063A9 RID: 25513 RVA: 0x00098B56 File Offset: 0x00096D56
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FC9 RID: 16329
			// (set) Token: 0x060063AA RID: 25514 RVA: 0x00098B6E File Offset: 0x00096D6E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FCA RID: 16330
			// (set) Token: 0x060063AB RID: 25515 RVA: 0x00098B86 File Offset: 0x00096D86
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003FCB RID: 16331
			// (set) Token: 0x060063AC RID: 25516 RVA: 0x00098B9E File Offset: 0x00096D9E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007CD RID: 1997
		public class FileNameParameters : ParametersBase
		{
			// Token: 0x17003FCC RID: 16332
			// (set) Token: 0x060063AE RID: 25518 RVA: 0x00098BBE File Offset: 0x00096DBE
			public virtual string FileName
			{
				set
				{
					base.PowerSharpParameters["FileName"] = value;
				}
			}

			// Token: 0x17003FCD RID: 16333
			// (set) Token: 0x060063AF RID: 25519 RVA: 0x00098BD1 File Offset: 0x00096DD1
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FCE RID: 16334
			// (set) Token: 0x060063B0 RID: 25520 RVA: 0x00098BE4 File Offset: 0x00096DE4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FCF RID: 16335
			// (set) Token: 0x060063B1 RID: 25521 RVA: 0x00098BF7 File Offset: 0x00096DF7
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17003FD0 RID: 16336
			// (set) Token: 0x060063B2 RID: 25522 RVA: 0x00098C0A File Offset: 0x00096E0A
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x17003FD1 RID: 16337
			// (set) Token: 0x060063B3 RID: 25523 RVA: 0x00098C22 File Offset: 0x00096E22
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17003FD2 RID: 16338
			// (set) Token: 0x060063B4 RID: 25524 RVA: 0x00098C35 File Offset: 0x00096E35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FD3 RID: 16339
			// (set) Token: 0x060063B5 RID: 25525 RVA: 0x00098C4D File Offset: 0x00096E4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FD4 RID: 16340
			// (set) Token: 0x060063B6 RID: 25526 RVA: 0x00098C65 File Offset: 0x00096E65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FD5 RID: 16341
			// (set) Token: 0x060063B7 RID: 25527 RVA: 0x00098C7D File Offset: 0x00096E7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003FD6 RID: 16342
			// (set) Token: 0x060063B8 RID: 25528 RVA: 0x00098C95 File Offset: 0x00096E95
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

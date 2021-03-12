using System;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007CE RID: 1998
	public class NewExchangeCertificateCommand : SyntheticCommandWithPipelineInput<X509Certificate2, X509Certificate2>
	{
		// Token: 0x060063BA RID: 25530 RVA: 0x00098CB5 File Offset: 0x00096EB5
		private NewExchangeCertificateCommand() : base("New-ExchangeCertificate")
		{
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x00098CC2 File Offset: 0x00096EC2
		public NewExchangeCertificateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x00098CD1 File Offset: 0x00096ED1
		public virtual NewExchangeCertificateCommand SetParameters(NewExchangeCertificateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x00098CDB File Offset: 0x00096EDB
		public virtual NewExchangeCertificateCommand SetParameters(NewExchangeCertificateCommand.RequestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x00098CE5 File Offset: 0x00096EE5
		public virtual NewExchangeCertificateCommand SetParameters(NewExchangeCertificateCommand.CertificateParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007CF RID: 1999
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003FD7 RID: 16343
			// (set) Token: 0x060063BF RID: 25535 RVA: 0x00098CEF File Offset: 0x00096EEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FD8 RID: 16344
			// (set) Token: 0x060063C0 RID: 25536 RVA: 0x00098D02 File Offset: 0x00096F02
			public virtual SwitchParameter IncludeAutoDiscover
			{
				set
				{
					base.PowerSharpParameters["IncludeAutoDiscover"] = value;
				}
			}

			// Token: 0x17003FD9 RID: 16345
			// (set) Token: 0x060063C1 RID: 25537 RVA: 0x00098D1A File Offset: 0x00096F1A
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17003FDA RID: 16346
			// (set) Token: 0x060063C2 RID: 25538 RVA: 0x00098D2D File Offset: 0x00096F2D
			public virtual X509Certificate2 Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17003FDB RID: 16347
			// (set) Token: 0x060063C3 RID: 25539 RVA: 0x00098D40 File Offset: 0x00096F40
			public virtual SwitchParameter IncludeAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["IncludeAcceptedDomains"] = value;
				}
			}

			// Token: 0x17003FDC RID: 16348
			// (set) Token: 0x060063C4 RID: 25540 RVA: 0x00098D58 File Offset: 0x00096F58
			public virtual SwitchParameter IncludeServerFQDN
			{
				set
				{
					base.PowerSharpParameters["IncludeServerFQDN"] = value;
				}
			}

			// Token: 0x17003FDD RID: 16349
			// (set) Token: 0x060063C5 RID: 25541 RVA: 0x00098D70 File Offset: 0x00096F70
			public virtual SwitchParameter IncludeServerNetBIOSName
			{
				set
				{
					base.PowerSharpParameters["IncludeServerNetBIOSName"] = value;
				}
			}

			// Token: 0x17003FDE RID: 16350
			// (set) Token: 0x060063C6 RID: 25542 RVA: 0x00098D88 File Offset: 0x00096F88
			public virtual X500DistinguishedName SubjectName
			{
				set
				{
					base.PowerSharpParameters["SubjectName"] = value;
				}
			}

			// Token: 0x17003FDF RID: 16351
			// (set) Token: 0x060063C7 RID: 25543 RVA: 0x00098D9B File Offset: 0x00096F9B
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003FE0 RID: 16352
			// (set) Token: 0x060063C8 RID: 25544 RVA: 0x00098DAE File Offset: 0x00096FAE
			public virtual int KeySize
			{
				set
				{
					base.PowerSharpParameters["KeySize"] = value;
				}
			}

			// Token: 0x17003FE1 RID: 16353
			// (set) Token: 0x060063C9 RID: 25545 RVA: 0x00098DC6 File Offset: 0x00096FC6
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x17003FE2 RID: 16354
			// (set) Token: 0x060063CA RID: 25546 RVA: 0x00098DDE File Offset: 0x00096FDE
			public virtual string SubjectKeyIdentifier
			{
				set
				{
					base.PowerSharpParameters["SubjectKeyIdentifier"] = value;
				}
			}

			// Token: 0x17003FE3 RID: 16355
			// (set) Token: 0x060063CB RID: 25547 RVA: 0x00098DF1 File Offset: 0x00096FF1
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FE4 RID: 16356
			// (set) Token: 0x060063CC RID: 25548 RVA: 0x00098E04 File Offset: 0x00097004
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003FE5 RID: 16357
			// (set) Token: 0x060063CD RID: 25549 RVA: 0x00098E1C File Offset: 0x0009701C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FE6 RID: 16358
			// (set) Token: 0x060063CE RID: 25550 RVA: 0x00098E34 File Offset: 0x00097034
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FE7 RID: 16359
			// (set) Token: 0x060063CF RID: 25551 RVA: 0x00098E4C File Offset: 0x0009704C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FE8 RID: 16360
			// (set) Token: 0x060063D0 RID: 25552 RVA: 0x00098E64 File Offset: 0x00097064
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003FE9 RID: 16361
			// (set) Token: 0x060063D1 RID: 25553 RVA: 0x00098E7C File Offset: 0x0009707C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007D0 RID: 2000
		public class RequestParameters : ParametersBase
		{
			// Token: 0x17003FEA RID: 16362
			// (set) Token: 0x060063D3 RID: 25555 RVA: 0x00098E9C File Offset: 0x0009709C
			public virtual SwitchParameter GenerateRequest
			{
				set
				{
					base.PowerSharpParameters["GenerateRequest"] = value;
				}
			}

			// Token: 0x17003FEB RID: 16363
			// (set) Token: 0x060063D4 RID: 25556 RVA: 0x00098EB4 File Offset: 0x000970B4
			public virtual string RequestFile
			{
				set
				{
					base.PowerSharpParameters["RequestFile"] = value;
				}
			}

			// Token: 0x17003FEC RID: 16364
			// (set) Token: 0x060063D5 RID: 25557 RVA: 0x00098EC7 File Offset: 0x000970C7
			public virtual SwitchParameter BinaryEncoded
			{
				set
				{
					base.PowerSharpParameters["BinaryEncoded"] = value;
				}
			}

			// Token: 0x17003FED RID: 16365
			// (set) Token: 0x060063D6 RID: 25558 RVA: 0x00098EDF File Offset: 0x000970DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FEE RID: 16366
			// (set) Token: 0x060063D7 RID: 25559 RVA: 0x00098EF2 File Offset: 0x000970F2
			public virtual SwitchParameter IncludeAutoDiscover
			{
				set
				{
					base.PowerSharpParameters["IncludeAutoDiscover"] = value;
				}
			}

			// Token: 0x17003FEF RID: 16367
			// (set) Token: 0x060063D8 RID: 25560 RVA: 0x00098F0A File Offset: 0x0009710A
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17003FF0 RID: 16368
			// (set) Token: 0x060063D9 RID: 25561 RVA: 0x00098F1D File Offset: 0x0009711D
			public virtual X509Certificate2 Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17003FF1 RID: 16369
			// (set) Token: 0x060063DA RID: 25562 RVA: 0x00098F30 File Offset: 0x00097130
			public virtual SwitchParameter IncludeAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["IncludeAcceptedDomains"] = value;
				}
			}

			// Token: 0x17003FF2 RID: 16370
			// (set) Token: 0x060063DB RID: 25563 RVA: 0x00098F48 File Offset: 0x00097148
			public virtual SwitchParameter IncludeServerFQDN
			{
				set
				{
					base.PowerSharpParameters["IncludeServerFQDN"] = value;
				}
			}

			// Token: 0x17003FF3 RID: 16371
			// (set) Token: 0x060063DC RID: 25564 RVA: 0x00098F60 File Offset: 0x00097160
			public virtual SwitchParameter IncludeServerNetBIOSName
			{
				set
				{
					base.PowerSharpParameters["IncludeServerNetBIOSName"] = value;
				}
			}

			// Token: 0x17003FF4 RID: 16372
			// (set) Token: 0x060063DD RID: 25565 RVA: 0x00098F78 File Offset: 0x00097178
			public virtual X500DistinguishedName SubjectName
			{
				set
				{
					base.PowerSharpParameters["SubjectName"] = value;
				}
			}

			// Token: 0x17003FF5 RID: 16373
			// (set) Token: 0x060063DE RID: 25566 RVA: 0x00098F8B File Offset: 0x0009718B
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003FF6 RID: 16374
			// (set) Token: 0x060063DF RID: 25567 RVA: 0x00098F9E File Offset: 0x0009719E
			public virtual int KeySize
			{
				set
				{
					base.PowerSharpParameters["KeySize"] = value;
				}
			}

			// Token: 0x17003FF7 RID: 16375
			// (set) Token: 0x060063E0 RID: 25568 RVA: 0x00098FB6 File Offset: 0x000971B6
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x17003FF8 RID: 16376
			// (set) Token: 0x060063E1 RID: 25569 RVA: 0x00098FCE File Offset: 0x000971CE
			public virtual string SubjectKeyIdentifier
			{
				set
				{
					base.PowerSharpParameters["SubjectKeyIdentifier"] = value;
				}
			}

			// Token: 0x17003FF9 RID: 16377
			// (set) Token: 0x060063E2 RID: 25570 RVA: 0x00098FE1 File Offset: 0x000971E1
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FFA RID: 16378
			// (set) Token: 0x060063E3 RID: 25571 RVA: 0x00098FF4 File Offset: 0x000971F4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003FFB RID: 16379
			// (set) Token: 0x060063E4 RID: 25572 RVA: 0x0009900C File Offset: 0x0009720C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FFC RID: 16380
			// (set) Token: 0x060063E5 RID: 25573 RVA: 0x00099024 File Offset: 0x00097224
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FFD RID: 16381
			// (set) Token: 0x060063E6 RID: 25574 RVA: 0x0009903C File Offset: 0x0009723C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FFE RID: 16382
			// (set) Token: 0x060063E7 RID: 25575 RVA: 0x00099054 File Offset: 0x00097254
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003FFF RID: 16383
			// (set) Token: 0x060063E8 RID: 25576 RVA: 0x0009906C File Offset: 0x0009726C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007D1 RID: 2001
		public class CertificateParameters : ParametersBase
		{
			// Token: 0x17004000 RID: 16384
			// (set) Token: 0x060063EA RID: 25578 RVA: 0x0009908C File Offset: 0x0009728C
			public virtual AllowedServices Services
			{
				set
				{
					base.PowerSharpParameters["Services"] = value;
				}
			}

			// Token: 0x17004001 RID: 16385
			// (set) Token: 0x060063EB RID: 25579 RVA: 0x000990A4 File Offset: 0x000972A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004002 RID: 16386
			// (set) Token: 0x060063EC RID: 25580 RVA: 0x000990B7 File Offset: 0x000972B7
			public virtual SwitchParameter IncludeAutoDiscover
			{
				set
				{
					base.PowerSharpParameters["IncludeAutoDiscover"] = value;
				}
			}

			// Token: 0x17004003 RID: 16387
			// (set) Token: 0x060063ED RID: 25581 RVA: 0x000990CF File Offset: 0x000972CF
			public virtual string FriendlyName
			{
				set
				{
					base.PowerSharpParameters["FriendlyName"] = value;
				}
			}

			// Token: 0x17004004 RID: 16388
			// (set) Token: 0x060063EE RID: 25582 RVA: 0x000990E2 File Offset: 0x000972E2
			public virtual X509Certificate2 Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17004005 RID: 16389
			// (set) Token: 0x060063EF RID: 25583 RVA: 0x000990F5 File Offset: 0x000972F5
			public virtual SwitchParameter IncludeAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["IncludeAcceptedDomains"] = value;
				}
			}

			// Token: 0x17004006 RID: 16390
			// (set) Token: 0x060063F0 RID: 25584 RVA: 0x0009910D File Offset: 0x0009730D
			public virtual SwitchParameter IncludeServerFQDN
			{
				set
				{
					base.PowerSharpParameters["IncludeServerFQDN"] = value;
				}
			}

			// Token: 0x17004007 RID: 16391
			// (set) Token: 0x060063F1 RID: 25585 RVA: 0x00099125 File Offset: 0x00097325
			public virtual SwitchParameter IncludeServerNetBIOSName
			{
				set
				{
					base.PowerSharpParameters["IncludeServerNetBIOSName"] = value;
				}
			}

			// Token: 0x17004008 RID: 16392
			// (set) Token: 0x060063F2 RID: 25586 RVA: 0x0009913D File Offset: 0x0009733D
			public virtual X500DistinguishedName SubjectName
			{
				set
				{
					base.PowerSharpParameters["SubjectName"] = value;
				}
			}

			// Token: 0x17004009 RID: 16393
			// (set) Token: 0x060063F3 RID: 25587 RVA: 0x00099150 File Offset: 0x00097350
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700400A RID: 16394
			// (set) Token: 0x060063F4 RID: 25588 RVA: 0x00099163 File Offset: 0x00097363
			public virtual int KeySize
			{
				set
				{
					base.PowerSharpParameters["KeySize"] = value;
				}
			}

			// Token: 0x1700400B RID: 16395
			// (set) Token: 0x060063F5 RID: 25589 RVA: 0x0009917B File Offset: 0x0009737B
			public virtual bool PrivateKeyExportable
			{
				set
				{
					base.PowerSharpParameters["PrivateKeyExportable"] = value;
				}
			}

			// Token: 0x1700400C RID: 16396
			// (set) Token: 0x060063F6 RID: 25590 RVA: 0x00099193 File Offset: 0x00097393
			public virtual string SubjectKeyIdentifier
			{
				set
				{
					base.PowerSharpParameters["SubjectKeyIdentifier"] = value;
				}
			}

			// Token: 0x1700400D RID: 16397
			// (set) Token: 0x060063F7 RID: 25591 RVA: 0x000991A6 File Offset: 0x000973A6
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700400E RID: 16398
			// (set) Token: 0x060063F8 RID: 25592 RVA: 0x000991B9 File Offset: 0x000973B9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700400F RID: 16399
			// (set) Token: 0x060063F9 RID: 25593 RVA: 0x000991D1 File Offset: 0x000973D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004010 RID: 16400
			// (set) Token: 0x060063FA RID: 25594 RVA: 0x000991E9 File Offset: 0x000973E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004011 RID: 16401
			// (set) Token: 0x060063FB RID: 25595 RVA: 0x00099201 File Offset: 0x00097401
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004012 RID: 16402
			// (set) Token: 0x060063FC RID: 25596 RVA: 0x00099219 File Offset: 0x00097419
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004013 RID: 16403
			// (set) Token: 0x060063FD RID: 25597 RVA: 0x00099231 File Offset: 0x00097431
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

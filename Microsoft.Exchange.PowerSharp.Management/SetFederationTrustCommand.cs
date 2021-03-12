using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006A0 RID: 1696
	public class SetFederationTrustCommand : SyntheticCommandWithPipelineInputNoOutput<FederationTrust>
	{
		// Token: 0x060059B1 RID: 22961 RVA: 0x0008C20C File Offset: 0x0008A40C
		private SetFederationTrustCommand() : base("Set-FederationTrust")
		{
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x0008C219 File Offset: 0x0008A419
		public SetFederationTrustCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x0008C228 File Offset: 0x0008A428
		public virtual SetFederationTrustCommand SetParameters(SetFederationTrustCommand.ApplicationUriParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0008C232 File Offset: 0x0008A432
		public virtual SetFederationTrustCommand SetParameters(SetFederationTrustCommand.PublishFederationCertificateParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0008C23C File Offset: 0x0008A43C
		public virtual SetFederationTrustCommand SetParameters(SetFederationTrustCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x0008C246 File Offset: 0x0008A446
		public virtual SetFederationTrustCommand SetParameters(SetFederationTrustCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006A1 RID: 1697
		public class ApplicationUriParameters : ParametersBase
		{
			// Token: 0x1700382A RID: 14378
			// (set) Token: 0x060059B7 RID: 22967 RVA: 0x0008C250 File Offset: 0x0008A450
			public virtual FederationTrustIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700382B RID: 14379
			// (set) Token: 0x060059B8 RID: 22968 RVA: 0x0008C263 File Offset: 0x0008A463
			public virtual string ApplicationUri
			{
				set
				{
					base.PowerSharpParameters["ApplicationUri"] = value;
				}
			}

			// Token: 0x1700382C RID: 14380
			// (set) Token: 0x060059B9 RID: 22969 RVA: 0x0008C276 File Offset: 0x0008A476
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700382D RID: 14381
			// (set) Token: 0x060059BA RID: 22970 RVA: 0x0008C289 File Offset: 0x0008A489
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700382E RID: 14382
			// (set) Token: 0x060059BB RID: 22971 RVA: 0x0008C29C File Offset: 0x0008A49C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700382F RID: 14383
			// (set) Token: 0x060059BC RID: 22972 RVA: 0x0008C2B4 File Offset: 0x0008A4B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003830 RID: 14384
			// (set) Token: 0x060059BD RID: 22973 RVA: 0x0008C2CC File Offset: 0x0008A4CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003831 RID: 14385
			// (set) Token: 0x060059BE RID: 22974 RVA: 0x0008C2E4 File Offset: 0x0008A4E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003832 RID: 14386
			// (set) Token: 0x060059BF RID: 22975 RVA: 0x0008C2FC File Offset: 0x0008A4FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006A2 RID: 1698
		public class PublishFederationCertificateParameters : ParametersBase
		{
			// Token: 0x17003833 RID: 14387
			// (set) Token: 0x060059C1 RID: 22977 RVA: 0x0008C31C File Offset: 0x0008A51C
			public virtual FederationTrustIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003834 RID: 14388
			// (set) Token: 0x060059C2 RID: 22978 RVA: 0x0008C32F File Offset: 0x0008A52F
			public virtual SwitchParameter PublishFederationCertificate
			{
				set
				{
					base.PowerSharpParameters["PublishFederationCertificate"] = value;
				}
			}

			// Token: 0x17003835 RID: 14389
			// (set) Token: 0x060059C3 RID: 22979 RVA: 0x0008C347 File Offset: 0x0008A547
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003836 RID: 14390
			// (set) Token: 0x060059C4 RID: 22980 RVA: 0x0008C35A File Offset: 0x0008A55A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003837 RID: 14391
			// (set) Token: 0x060059C5 RID: 22981 RVA: 0x0008C36D File Offset: 0x0008A56D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003838 RID: 14392
			// (set) Token: 0x060059C6 RID: 22982 RVA: 0x0008C385 File Offset: 0x0008A585
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003839 RID: 14393
			// (set) Token: 0x060059C7 RID: 22983 RVA: 0x0008C39D File Offset: 0x0008A59D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700383A RID: 14394
			// (set) Token: 0x060059C8 RID: 22984 RVA: 0x0008C3B5 File Offset: 0x0008A5B5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700383B RID: 14395
			// (set) Token: 0x060059C9 RID: 22985 RVA: 0x0008C3CD File Offset: 0x0008A5CD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006A3 RID: 1699
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700383C RID: 14396
			// (set) Token: 0x060059CB RID: 22987 RVA: 0x0008C3ED File Offset: 0x0008A5ED
			public virtual FederationTrustIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700383D RID: 14397
			// (set) Token: 0x060059CC RID: 22988 RVA: 0x0008C400 File Offset: 0x0008A600
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x1700383E RID: 14398
			// (set) Token: 0x060059CD RID: 22989 RVA: 0x0008C413 File Offset: 0x0008A613
			public virtual Uri MetadataUrl
			{
				set
				{
					base.PowerSharpParameters["MetadataUrl"] = value;
				}
			}

			// Token: 0x1700383F RID: 14399
			// (set) Token: 0x060059CE RID: 22990 RVA: 0x0008C426 File Offset: 0x0008A626
			public virtual SwitchParameter RefreshMetadata
			{
				set
				{
					base.PowerSharpParameters["RefreshMetadata"] = value;
				}
			}

			// Token: 0x17003840 RID: 14400
			// (set) Token: 0x060059CF RID: 22991 RVA: 0x0008C43E File Offset: 0x0008A63E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003841 RID: 14401
			// (set) Token: 0x060059D0 RID: 22992 RVA: 0x0008C451 File Offset: 0x0008A651
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003842 RID: 14402
			// (set) Token: 0x060059D1 RID: 22993 RVA: 0x0008C464 File Offset: 0x0008A664
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003843 RID: 14403
			// (set) Token: 0x060059D2 RID: 22994 RVA: 0x0008C47C File Offset: 0x0008A67C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003844 RID: 14404
			// (set) Token: 0x060059D3 RID: 22995 RVA: 0x0008C494 File Offset: 0x0008A694
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003845 RID: 14405
			// (set) Token: 0x060059D4 RID: 22996 RVA: 0x0008C4AC File Offset: 0x0008A6AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003846 RID: 14406
			// (set) Token: 0x060059D5 RID: 22997 RVA: 0x0008C4C4 File Offset: 0x0008A6C4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006A4 RID: 1700
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003847 RID: 14407
			// (set) Token: 0x060059D7 RID: 22999 RVA: 0x0008C4E4 File Offset: 0x0008A6E4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003848 RID: 14408
			// (set) Token: 0x060059D8 RID: 23000 RVA: 0x0008C4F7 File Offset: 0x0008A6F7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003849 RID: 14409
			// (set) Token: 0x060059D9 RID: 23001 RVA: 0x0008C50A File Offset: 0x0008A70A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700384A RID: 14410
			// (set) Token: 0x060059DA RID: 23002 RVA: 0x0008C522 File Offset: 0x0008A722
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700384B RID: 14411
			// (set) Token: 0x060059DB RID: 23003 RVA: 0x0008C53A File Offset: 0x0008A73A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700384C RID: 14412
			// (set) Token: 0x060059DC RID: 23004 RVA: 0x0008C552 File Offset: 0x0008A752
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700384D RID: 14413
			// (set) Token: 0x060059DD RID: 23005 RVA: 0x0008C56A File Offset: 0x0008A76A
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

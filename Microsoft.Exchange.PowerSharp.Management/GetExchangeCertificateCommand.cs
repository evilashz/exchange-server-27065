using System;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007C4 RID: 1988
	public class GetExchangeCertificateCommand : SyntheticCommandWithPipelineInput<X509Certificate2, X509Certificate2>
	{
		// Token: 0x0600635E RID: 25438 RVA: 0x000985BD File Offset: 0x000967BD
		private GetExchangeCertificateCommand() : base("Get-ExchangeCertificate")
		{
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x000985CA File Offset: 0x000967CA
		public GetExchangeCertificateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x000985D9 File Offset: 0x000967D9
		public virtual GetExchangeCertificateCommand SetParameters(GetExchangeCertificateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x000985E3 File Offset: 0x000967E3
		public virtual GetExchangeCertificateCommand SetParameters(GetExchangeCertificateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x000985ED File Offset: 0x000967ED
		public virtual GetExchangeCertificateCommand SetParameters(GetExchangeCertificateCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x000985F7 File Offset: 0x000967F7
		public virtual GetExchangeCertificateCommand SetParameters(GetExchangeCertificateCommand.ThumbprintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007C5 RID: 1989
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F8F RID: 16271
			// (set) Token: 0x06006364 RID: 25444 RVA: 0x00098601 File Offset: 0x00096801
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F90 RID: 16272
			// (set) Token: 0x06006365 RID: 25445 RVA: 0x00098614 File Offset: 0x00096814
			public virtual MultiValuedProperty<SmtpDomain> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003F91 RID: 16273
			// (set) Token: 0x06006366 RID: 25446 RVA: 0x00098627 File Offset: 0x00096827
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F92 RID: 16274
			// (set) Token: 0x06006367 RID: 25447 RVA: 0x0009863F File Offset: 0x0009683F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F93 RID: 16275
			// (set) Token: 0x06006368 RID: 25448 RVA: 0x00098657 File Offset: 0x00096857
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F94 RID: 16276
			// (set) Token: 0x06006369 RID: 25449 RVA: 0x0009866F File Offset: 0x0009686F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007C6 RID: 1990
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003F95 RID: 16277
			// (set) Token: 0x0600636B RID: 25451 RVA: 0x0009868F File Offset: 0x0009688F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeCertificateIdParameter(value) : null);
				}
			}

			// Token: 0x17003F96 RID: 16278
			// (set) Token: 0x0600636C RID: 25452 RVA: 0x000986AD File Offset: 0x000968AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F97 RID: 16279
			// (set) Token: 0x0600636D RID: 25453 RVA: 0x000986C0 File Offset: 0x000968C0
			public virtual MultiValuedProperty<SmtpDomain> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003F98 RID: 16280
			// (set) Token: 0x0600636E RID: 25454 RVA: 0x000986D3 File Offset: 0x000968D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F99 RID: 16281
			// (set) Token: 0x0600636F RID: 25455 RVA: 0x000986EB File Offset: 0x000968EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F9A RID: 16282
			// (set) Token: 0x06006370 RID: 25456 RVA: 0x00098703 File Offset: 0x00096903
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F9B RID: 16283
			// (set) Token: 0x06006371 RID: 25457 RVA: 0x0009871B File Offset: 0x0009691B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007C7 RID: 1991
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x17003F9C RID: 16284
			// (set) Token: 0x06006373 RID: 25459 RVA: 0x0009873B File Offset: 0x0009693B
			public virtual X509Certificate2 Instance
			{
				set
				{
					base.PowerSharpParameters["Instance"] = value;
				}
			}

			// Token: 0x17003F9D RID: 16285
			// (set) Token: 0x06006374 RID: 25460 RVA: 0x0009874E File Offset: 0x0009694E
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003F9E RID: 16286
			// (set) Token: 0x06006375 RID: 25461 RVA: 0x00098761 File Offset: 0x00096961
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F9F RID: 16287
			// (set) Token: 0x06006376 RID: 25462 RVA: 0x00098774 File Offset: 0x00096974
			public virtual MultiValuedProperty<SmtpDomain> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003FA0 RID: 16288
			// (set) Token: 0x06006377 RID: 25463 RVA: 0x00098787 File Offset: 0x00096987
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FA1 RID: 16289
			// (set) Token: 0x06006378 RID: 25464 RVA: 0x0009879F File Offset: 0x0009699F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FA2 RID: 16290
			// (set) Token: 0x06006379 RID: 25465 RVA: 0x000987B7 File Offset: 0x000969B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FA3 RID: 16291
			// (set) Token: 0x0600637A RID: 25466 RVA: 0x000987CF File Offset: 0x000969CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007C8 RID: 1992
		public class ThumbprintParameters : ParametersBase
		{
			// Token: 0x17003FA4 RID: 16292
			// (set) Token: 0x0600637C RID: 25468 RVA: 0x000987EF File Offset: 0x000969EF
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003FA5 RID: 16293
			// (set) Token: 0x0600637D RID: 25469 RVA: 0x00098802 File Offset: 0x00096A02
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x17003FA6 RID: 16294
			// (set) Token: 0x0600637E RID: 25470 RVA: 0x00098815 File Offset: 0x00096A15
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003FA7 RID: 16295
			// (set) Token: 0x0600637F RID: 25471 RVA: 0x00098828 File Offset: 0x00096A28
			public virtual MultiValuedProperty<SmtpDomain> DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003FA8 RID: 16296
			// (set) Token: 0x06006380 RID: 25472 RVA: 0x0009883B File Offset: 0x00096A3B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003FA9 RID: 16297
			// (set) Token: 0x06006381 RID: 25473 RVA: 0x00098853 File Offset: 0x00096A53
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003FAA RID: 16298
			// (set) Token: 0x06006382 RID: 25474 RVA: 0x0009886B File Offset: 0x00096A6B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003FAB RID: 16299
			// (set) Token: 0x06006383 RID: 25475 RVA: 0x00098883 File Offset: 0x00096A83
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

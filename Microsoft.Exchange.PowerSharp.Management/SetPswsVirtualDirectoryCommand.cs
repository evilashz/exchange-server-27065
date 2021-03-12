using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000995 RID: 2453
	public class SetPswsVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADPswsVirtualDirectory>
	{
		// Token: 0x06007B7B RID: 31611 RVA: 0x000B8107 File Offset: 0x000B6307
		private SetPswsVirtualDirectoryCommand() : base("Set-PswsVirtualDirectory")
		{
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x000B8114 File Offset: 0x000B6314
		public SetPswsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x000B8123 File Offset: 0x000B6323
		public virtual SetPswsVirtualDirectoryCommand SetParameters(SetPswsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x000B812D File Offset: 0x000B632D
		public virtual SetPswsVirtualDirectoryCommand SetParameters(SetPswsVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000996 RID: 2454
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700540A RID: 21514
			// (set) Token: 0x06007B7F RID: 31615 RVA: 0x000B8137 File Offset: 0x000B6337
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x1700540B RID: 21515
			// (set) Token: 0x06007B80 RID: 31616 RVA: 0x000B814F File Offset: 0x000B634F
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x1700540C RID: 21516
			// (set) Token: 0x06007B81 RID: 31617 RVA: 0x000B8167 File Offset: 0x000B6367
			public virtual bool EnableCertificateHeaderAuthModule
			{
				set
				{
					base.PowerSharpParameters["EnableCertificateHeaderAuthModule"] = value;
				}
			}

			// Token: 0x1700540D RID: 21517
			// (set) Token: 0x06007B82 RID: 31618 RVA: 0x000B817F File Offset: 0x000B637F
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x1700540E RID: 21518
			// (set) Token: 0x06007B83 RID: 31619 RVA: 0x000B8197 File Offset: 0x000B6397
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x1700540F RID: 21519
			// (set) Token: 0x06007B84 RID: 31620 RVA: 0x000B81AF File Offset: 0x000B63AF
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005410 RID: 21520
			// (set) Token: 0x06007B85 RID: 31621 RVA: 0x000B81C7 File Offset: 0x000B63C7
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005411 RID: 21521
			// (set) Token: 0x06007B86 RID: 31622 RVA: 0x000B81DF File Offset: 0x000B63DF
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005412 RID: 21522
			// (set) Token: 0x06007B87 RID: 31623 RVA: 0x000B81F7 File Offset: 0x000B63F7
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005413 RID: 21523
			// (set) Token: 0x06007B88 RID: 31624 RVA: 0x000B820A File Offset: 0x000B640A
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005414 RID: 21524
			// (set) Token: 0x06007B89 RID: 31625 RVA: 0x000B821D File Offset: 0x000B641D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005415 RID: 21525
			// (set) Token: 0x06007B8A RID: 31626 RVA: 0x000B8230 File Offset: 0x000B6430
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005416 RID: 21526
			// (set) Token: 0x06007B8B RID: 31627 RVA: 0x000B8248 File Offset: 0x000B6448
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005417 RID: 21527
			// (set) Token: 0x06007B8C RID: 31628 RVA: 0x000B8260 File Offset: 0x000B6460
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005418 RID: 21528
			// (set) Token: 0x06007B8D RID: 31629 RVA: 0x000B8278 File Offset: 0x000B6478
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005419 RID: 21529
			// (set) Token: 0x06007B8E RID: 31630 RVA: 0x000B8290 File Offset: 0x000B6490
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000997 RID: 2455
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700541A RID: 21530
			// (set) Token: 0x06007B90 RID: 31632 RVA: 0x000B82B0 File Offset: 0x000B64B0
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700541B RID: 21531
			// (set) Token: 0x06007B91 RID: 31633 RVA: 0x000B82C3 File Offset: 0x000B64C3
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x1700541C RID: 21532
			// (set) Token: 0x06007B92 RID: 31634 RVA: 0x000B82DB File Offset: 0x000B64DB
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x1700541D RID: 21533
			// (set) Token: 0x06007B93 RID: 31635 RVA: 0x000B82F3 File Offset: 0x000B64F3
			public virtual bool EnableCertificateHeaderAuthModule
			{
				set
				{
					base.PowerSharpParameters["EnableCertificateHeaderAuthModule"] = value;
				}
			}

			// Token: 0x1700541E RID: 21534
			// (set) Token: 0x06007B94 RID: 31636 RVA: 0x000B830B File Offset: 0x000B650B
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x1700541F RID: 21535
			// (set) Token: 0x06007B95 RID: 31637 RVA: 0x000B8323 File Offset: 0x000B6523
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x17005420 RID: 21536
			// (set) Token: 0x06007B96 RID: 31638 RVA: 0x000B833B File Offset: 0x000B653B
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005421 RID: 21537
			// (set) Token: 0x06007B97 RID: 31639 RVA: 0x000B8353 File Offset: 0x000B6553
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005422 RID: 21538
			// (set) Token: 0x06007B98 RID: 31640 RVA: 0x000B836B File Offset: 0x000B656B
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005423 RID: 21539
			// (set) Token: 0x06007B99 RID: 31641 RVA: 0x000B8383 File Offset: 0x000B6583
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005424 RID: 21540
			// (set) Token: 0x06007B9A RID: 31642 RVA: 0x000B8396 File Offset: 0x000B6596
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005425 RID: 21541
			// (set) Token: 0x06007B9B RID: 31643 RVA: 0x000B83A9 File Offset: 0x000B65A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005426 RID: 21542
			// (set) Token: 0x06007B9C RID: 31644 RVA: 0x000B83BC File Offset: 0x000B65BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005427 RID: 21543
			// (set) Token: 0x06007B9D RID: 31645 RVA: 0x000B83D4 File Offset: 0x000B65D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005428 RID: 21544
			// (set) Token: 0x06007B9E RID: 31646 RVA: 0x000B83EC File Offset: 0x000B65EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005429 RID: 21545
			// (set) Token: 0x06007B9F RID: 31647 RVA: 0x000B8404 File Offset: 0x000B6604
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700542A RID: 21546
			// (set) Token: 0x06007BA0 RID: 31648 RVA: 0x000B841C File Offset: 0x000B661C
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000660 RID: 1632
	public class SetExchangeServerRoleCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeServerRole>
	{
		// Token: 0x060053F7 RID: 21495 RVA: 0x000843DD File Offset: 0x000825DD
		private SetExchangeServerRoleCommand() : base("Set-ExchangeServerRole")
		{
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x000843EA File Offset: 0x000825EA
		public SetExchangeServerRoleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x000843F9 File Offset: 0x000825F9
		public virtual SetExchangeServerRoleCommand SetParameters(SetExchangeServerRoleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x00084403 File Offset: 0x00082603
		public virtual SetExchangeServerRoleCommand SetParameters(SetExchangeServerRoleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000661 RID: 1633
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170032F0 RID: 13040
			// (set) Token: 0x060053FB RID: 21499 RVA: 0x0008440D File Offset: 0x0008260D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170032F1 RID: 13041
			// (set) Token: 0x060053FC RID: 21500 RVA: 0x00084420 File Offset: 0x00082620
			public virtual bool IsHubTransportServer
			{
				set
				{
					base.PowerSharpParameters["IsHubTransportServer"] = value;
				}
			}

			// Token: 0x170032F2 RID: 13042
			// (set) Token: 0x060053FD RID: 21501 RVA: 0x00084438 File Offset: 0x00082638
			public virtual bool IsClientAccessServer
			{
				set
				{
					base.PowerSharpParameters["IsClientAccessServer"] = value;
				}
			}

			// Token: 0x170032F3 RID: 13043
			// (set) Token: 0x060053FE RID: 21502 RVA: 0x00084450 File Offset: 0x00082650
			public virtual bool IsEdgeServer
			{
				set
				{
					base.PowerSharpParameters["IsEdgeServer"] = value;
				}
			}

			// Token: 0x170032F4 RID: 13044
			// (set) Token: 0x060053FF RID: 21503 RVA: 0x00084468 File Offset: 0x00082668
			public virtual bool IsMailboxServer
			{
				set
				{
					base.PowerSharpParameters["IsMailboxServer"] = value;
				}
			}

			// Token: 0x170032F5 RID: 13045
			// (set) Token: 0x06005400 RID: 21504 RVA: 0x00084480 File Offset: 0x00082680
			public virtual bool IsUnifiedMessagingServer
			{
				set
				{
					base.PowerSharpParameters["IsUnifiedMessagingServer"] = value;
				}
			}

			// Token: 0x170032F6 RID: 13046
			// (set) Token: 0x06005401 RID: 21505 RVA: 0x00084498 File Offset: 0x00082698
			public virtual bool IsProvisionedServer
			{
				set
				{
					base.PowerSharpParameters["IsProvisionedServer"] = value;
				}
			}

			// Token: 0x170032F7 RID: 13047
			// (set) Token: 0x06005402 RID: 21506 RVA: 0x000844B0 File Offset: 0x000826B0
			public virtual bool IsCafeServer
			{
				set
				{
					base.PowerSharpParameters["IsCafeServer"] = value;
				}
			}

			// Token: 0x170032F8 RID: 13048
			// (set) Token: 0x06005403 RID: 21507 RVA: 0x000844C8 File Offset: 0x000826C8
			public virtual bool IsFrontendTransportServer
			{
				set
				{
					base.PowerSharpParameters["IsFrontendTransportServer"] = value;
				}
			}

			// Token: 0x170032F9 RID: 13049
			// (set) Token: 0x06005404 RID: 21508 RVA: 0x000844E0 File Offset: 0x000826E0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170032FA RID: 13050
			// (set) Token: 0x06005405 RID: 21509 RVA: 0x000844F3 File Offset: 0x000826F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170032FB RID: 13051
			// (set) Token: 0x06005406 RID: 21510 RVA: 0x0008450B File Offset: 0x0008270B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170032FC RID: 13052
			// (set) Token: 0x06005407 RID: 21511 RVA: 0x00084523 File Offset: 0x00082723
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170032FD RID: 13053
			// (set) Token: 0x06005408 RID: 21512 RVA: 0x0008453B File Offset: 0x0008273B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000662 RID: 1634
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170032FE RID: 13054
			// (set) Token: 0x0600540A RID: 21514 RVA: 0x0008455B File Offset: 0x0008275B
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170032FF RID: 13055
			// (set) Token: 0x0600540B RID: 21515 RVA: 0x0008456E File Offset: 0x0008276E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003300 RID: 13056
			// (set) Token: 0x0600540C RID: 21516 RVA: 0x00084581 File Offset: 0x00082781
			public virtual bool IsHubTransportServer
			{
				set
				{
					base.PowerSharpParameters["IsHubTransportServer"] = value;
				}
			}

			// Token: 0x17003301 RID: 13057
			// (set) Token: 0x0600540D RID: 21517 RVA: 0x00084599 File Offset: 0x00082799
			public virtual bool IsClientAccessServer
			{
				set
				{
					base.PowerSharpParameters["IsClientAccessServer"] = value;
				}
			}

			// Token: 0x17003302 RID: 13058
			// (set) Token: 0x0600540E RID: 21518 RVA: 0x000845B1 File Offset: 0x000827B1
			public virtual bool IsEdgeServer
			{
				set
				{
					base.PowerSharpParameters["IsEdgeServer"] = value;
				}
			}

			// Token: 0x17003303 RID: 13059
			// (set) Token: 0x0600540F RID: 21519 RVA: 0x000845C9 File Offset: 0x000827C9
			public virtual bool IsMailboxServer
			{
				set
				{
					base.PowerSharpParameters["IsMailboxServer"] = value;
				}
			}

			// Token: 0x17003304 RID: 13060
			// (set) Token: 0x06005410 RID: 21520 RVA: 0x000845E1 File Offset: 0x000827E1
			public virtual bool IsUnifiedMessagingServer
			{
				set
				{
					base.PowerSharpParameters["IsUnifiedMessagingServer"] = value;
				}
			}

			// Token: 0x17003305 RID: 13061
			// (set) Token: 0x06005411 RID: 21521 RVA: 0x000845F9 File Offset: 0x000827F9
			public virtual bool IsProvisionedServer
			{
				set
				{
					base.PowerSharpParameters["IsProvisionedServer"] = value;
				}
			}

			// Token: 0x17003306 RID: 13062
			// (set) Token: 0x06005412 RID: 21522 RVA: 0x00084611 File Offset: 0x00082811
			public virtual bool IsCafeServer
			{
				set
				{
					base.PowerSharpParameters["IsCafeServer"] = value;
				}
			}

			// Token: 0x17003307 RID: 13063
			// (set) Token: 0x06005413 RID: 21523 RVA: 0x00084629 File Offset: 0x00082829
			public virtual bool IsFrontendTransportServer
			{
				set
				{
					base.PowerSharpParameters["IsFrontendTransportServer"] = value;
				}
			}

			// Token: 0x17003308 RID: 13064
			// (set) Token: 0x06005414 RID: 21524 RVA: 0x00084641 File Offset: 0x00082841
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003309 RID: 13065
			// (set) Token: 0x06005415 RID: 21525 RVA: 0x00084654 File Offset: 0x00082854
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700330A RID: 13066
			// (set) Token: 0x06005416 RID: 21526 RVA: 0x0008466C File Offset: 0x0008286C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700330B RID: 13067
			// (set) Token: 0x06005417 RID: 21527 RVA: 0x00084684 File Offset: 0x00082884
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700330C RID: 13068
			// (set) Token: 0x06005418 RID: 21528 RVA: 0x0008469C File Offset: 0x0008289C
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

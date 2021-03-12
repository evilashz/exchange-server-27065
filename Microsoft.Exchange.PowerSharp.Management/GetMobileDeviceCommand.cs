using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200003E RID: 62
	public class GetMobileDeviceCommand : SyntheticCommandWithPipelineInput<MobileDevice, MobileDevice>
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x0003474B File Offset: 0x0003294B
		private GetMobileDeviceCommand() : base("Get-MobileDevice")
		{
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00034758 File Offset: 0x00032958
		public GetMobileDeviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00034767 File Offset: 0x00032967
		public virtual GetMobileDeviceCommand SetParameters(GetMobileDeviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00034771 File Offset: 0x00032971
		public virtual GetMobileDeviceCommand SetParameters(GetMobileDeviceCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0003477B File Offset: 0x0003297B
		public virtual GetMobileDeviceCommand SetParameters(GetMobileDeviceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200003F RID: 63
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000162 RID: 354
			// (set) Token: 0x0600162A RID: 5674 RVA: 0x00034785 File Offset: 0x00032985
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000163 RID: 355
			// (set) Token: 0x0600162B RID: 5675 RVA: 0x0003479D File Offset: 0x0003299D
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17000164 RID: 356
			// (set) Token: 0x0600162C RID: 5676 RVA: 0x000347BB File Offset: 0x000329BB
			public virtual SwitchParameter ActiveSync
			{
				set
				{
					base.PowerSharpParameters["ActiveSync"] = value;
				}
			}

			// Token: 0x17000165 RID: 357
			// (set) Token: 0x0600162D RID: 5677 RVA: 0x000347D3 File Offset: 0x000329D3
			public virtual SwitchParameter OWAforDevices
			{
				set
				{
					base.PowerSharpParameters["OWAforDevices"] = value;
				}
			}

			// Token: 0x17000166 RID: 358
			// (set) Token: 0x0600162E RID: 5678 RVA: 0x000347EB File Offset: 0x000329EB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17000167 RID: 359
			// (set) Token: 0x0600162F RID: 5679 RVA: 0x000347FE File Offset: 0x000329FE
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000168 RID: 360
			// (set) Token: 0x06001630 RID: 5680 RVA: 0x00034811 File Offset: 0x00032A11
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17000169 RID: 361
			// (set) Token: 0x06001631 RID: 5681 RVA: 0x00034829 File Offset: 0x00032A29
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700016A RID: 362
			// (set) Token: 0x06001632 RID: 5682 RVA: 0x00034847 File Offset: 0x00032A47
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700016B RID: 363
			// (set) Token: 0x06001633 RID: 5683 RVA: 0x0003485A File Offset: 0x00032A5A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700016C RID: 364
			// (set) Token: 0x06001634 RID: 5684 RVA: 0x00034872 File Offset: 0x00032A72
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700016D RID: 365
			// (set) Token: 0x06001635 RID: 5685 RVA: 0x0003488A File Offset: 0x00032A8A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700016E RID: 366
			// (set) Token: 0x06001636 RID: 5686 RVA: 0x000348A2 File Offset: 0x00032AA2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000040 RID: 64
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x1700016F RID: 367
			// (set) Token: 0x06001638 RID: 5688 RVA: 0x000348C2 File Offset: 0x00032AC2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000170 RID: 368
			// (set) Token: 0x06001639 RID: 5689 RVA: 0x000348E0 File Offset: 0x00032AE0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000171 RID: 369
			// (set) Token: 0x0600163A RID: 5690 RVA: 0x000348F8 File Offset: 0x00032AF8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17000172 RID: 370
			// (set) Token: 0x0600163B RID: 5691 RVA: 0x00034916 File Offset: 0x00032B16
			public virtual SwitchParameter ActiveSync
			{
				set
				{
					base.PowerSharpParameters["ActiveSync"] = value;
				}
			}

			// Token: 0x17000173 RID: 371
			// (set) Token: 0x0600163C RID: 5692 RVA: 0x0003492E File Offset: 0x00032B2E
			public virtual SwitchParameter OWAforDevices
			{
				set
				{
					base.PowerSharpParameters["OWAforDevices"] = value;
				}
			}

			// Token: 0x17000174 RID: 372
			// (set) Token: 0x0600163D RID: 5693 RVA: 0x00034946 File Offset: 0x00032B46
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17000175 RID: 373
			// (set) Token: 0x0600163E RID: 5694 RVA: 0x00034959 File Offset: 0x00032B59
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000176 RID: 374
			// (set) Token: 0x0600163F RID: 5695 RVA: 0x0003496C File Offset: 0x00032B6C
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17000177 RID: 375
			// (set) Token: 0x06001640 RID: 5696 RVA: 0x00034984 File Offset: 0x00032B84
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000178 RID: 376
			// (set) Token: 0x06001641 RID: 5697 RVA: 0x000349A2 File Offset: 0x00032BA2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000179 RID: 377
			// (set) Token: 0x06001642 RID: 5698 RVA: 0x000349B5 File Offset: 0x00032BB5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700017A RID: 378
			// (set) Token: 0x06001643 RID: 5699 RVA: 0x000349CD File Offset: 0x00032BCD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700017B RID: 379
			// (set) Token: 0x06001644 RID: 5700 RVA: 0x000349E5 File Offset: 0x00032BE5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700017C RID: 380
			// (set) Token: 0x06001645 RID: 5701 RVA: 0x000349FD File Offset: 0x00032BFD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000041 RID: 65
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700017D RID: 381
			// (set) Token: 0x06001647 RID: 5703 RVA: 0x00034A1D File Offset: 0x00032C1D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MobileDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x1700017E RID: 382
			// (set) Token: 0x06001648 RID: 5704 RVA: 0x00034A3B File Offset: 0x00032C3B
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700017F RID: 383
			// (set) Token: 0x06001649 RID: 5705 RVA: 0x00034A53 File Offset: 0x00032C53
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17000180 RID: 384
			// (set) Token: 0x0600164A RID: 5706 RVA: 0x00034A71 File Offset: 0x00032C71
			public virtual SwitchParameter ActiveSync
			{
				set
				{
					base.PowerSharpParameters["ActiveSync"] = value;
				}
			}

			// Token: 0x17000181 RID: 385
			// (set) Token: 0x0600164B RID: 5707 RVA: 0x00034A89 File Offset: 0x00032C89
			public virtual SwitchParameter OWAforDevices
			{
				set
				{
					base.PowerSharpParameters["OWAforDevices"] = value;
				}
			}

			// Token: 0x17000182 RID: 386
			// (set) Token: 0x0600164C RID: 5708 RVA: 0x00034AA1 File Offset: 0x00032CA1
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17000183 RID: 387
			// (set) Token: 0x0600164D RID: 5709 RVA: 0x00034AB4 File Offset: 0x00032CB4
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000184 RID: 388
			// (set) Token: 0x0600164E RID: 5710 RVA: 0x00034AC7 File Offset: 0x00032CC7
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17000185 RID: 389
			// (set) Token: 0x0600164F RID: 5711 RVA: 0x00034ADF File Offset: 0x00032CDF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000186 RID: 390
			// (set) Token: 0x06001650 RID: 5712 RVA: 0x00034AFD File Offset: 0x00032CFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000187 RID: 391
			// (set) Token: 0x06001651 RID: 5713 RVA: 0x00034B10 File Offset: 0x00032D10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000188 RID: 392
			// (set) Token: 0x06001652 RID: 5714 RVA: 0x00034B28 File Offset: 0x00032D28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000189 RID: 393
			// (set) Token: 0x06001653 RID: 5715 RVA: 0x00034B40 File Offset: 0x00032D40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700018A RID: 394
			// (set) Token: 0x06001654 RID: 5716 RVA: 0x00034B58 File Offset: 0x00032D58
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

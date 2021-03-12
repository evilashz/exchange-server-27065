using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000042 RID: 66
	public class GetMobileDeviceStatisticsCommand : SyntheticCommandWithPipelineInput<MobileDevice, MobileDevice>
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x00034B78 File Offset: 0x00032D78
		private GetMobileDeviceStatisticsCommand() : base("Get-MobileDeviceStatistics")
		{
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00034B85 File Offset: 0x00032D85
		public GetMobileDeviceStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00034B94 File Offset: 0x00032D94
		public virtual GetMobileDeviceStatisticsCommand SetParameters(GetMobileDeviceStatisticsCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00034B9E File Offset: 0x00032D9E
		public virtual GetMobileDeviceStatisticsCommand SetParameters(GetMobileDeviceStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00034BA8 File Offset: 0x00032DA8
		public virtual GetMobileDeviceStatisticsCommand SetParameters(GetMobileDeviceStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000043 RID: 67
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x1700018B RID: 395
			// (set) Token: 0x0600165B RID: 5723 RVA: 0x00034BB2 File Offset: 0x00032DB2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700018C RID: 396
			// (set) Token: 0x0600165C RID: 5724 RVA: 0x00034BD0 File Offset: 0x00032DD0
			public virtual SwitchParameter GetMailboxLog
			{
				set
				{
					base.PowerSharpParameters["GetMailboxLog"] = value;
				}
			}

			// Token: 0x1700018D RID: 397
			// (set) Token: 0x0600165D RID: 5725 RVA: 0x00034BE8 File Offset: 0x00032DE8
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x1700018E RID: 398
			// (set) Token: 0x0600165E RID: 5726 RVA: 0x00034BFB File Offset: 0x00032DFB
			public virtual SwitchParameter ActiveSync
			{
				set
				{
					base.PowerSharpParameters["ActiveSync"] = value;
				}
			}

			// Token: 0x1700018F RID: 399
			// (set) Token: 0x0600165F RID: 5727 RVA: 0x00034C13 File Offset: 0x00032E13
			public virtual SwitchParameter OWAforDevices
			{
				set
				{
					base.PowerSharpParameters["OWAforDevices"] = value;
				}
			}

			// Token: 0x17000190 RID: 400
			// (set) Token: 0x06001660 RID: 5728 RVA: 0x00034C2B File Offset: 0x00032E2B
			public virtual SwitchParameter ShowRecoveryPassword
			{
				set
				{
					base.PowerSharpParameters["ShowRecoveryPassword"] = value;
				}
			}

			// Token: 0x17000191 RID: 401
			// (set) Token: 0x06001661 RID: 5729 RVA: 0x00034C43 File Offset: 0x00032E43
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000192 RID: 402
			// (set) Token: 0x06001662 RID: 5730 RVA: 0x00034C56 File Offset: 0x00032E56
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000193 RID: 403
			// (set) Token: 0x06001663 RID: 5731 RVA: 0x00034C6E File Offset: 0x00032E6E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000194 RID: 404
			// (set) Token: 0x06001664 RID: 5732 RVA: 0x00034C86 File Offset: 0x00032E86
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000195 RID: 405
			// (set) Token: 0x06001665 RID: 5733 RVA: 0x00034C9E File Offset: 0x00032E9E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000044 RID: 68
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000196 RID: 406
			// (set) Token: 0x06001667 RID: 5735 RVA: 0x00034CBE File Offset: 0x00032EBE
			public virtual SwitchParameter GetMailboxLog
			{
				set
				{
					base.PowerSharpParameters["GetMailboxLog"] = value;
				}
			}

			// Token: 0x17000197 RID: 407
			// (set) Token: 0x06001668 RID: 5736 RVA: 0x00034CD6 File Offset: 0x00032ED6
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x17000198 RID: 408
			// (set) Token: 0x06001669 RID: 5737 RVA: 0x00034CE9 File Offset: 0x00032EE9
			public virtual SwitchParameter ActiveSync
			{
				set
				{
					base.PowerSharpParameters["ActiveSync"] = value;
				}
			}

			// Token: 0x17000199 RID: 409
			// (set) Token: 0x0600166A RID: 5738 RVA: 0x00034D01 File Offset: 0x00032F01
			public virtual SwitchParameter OWAforDevices
			{
				set
				{
					base.PowerSharpParameters["OWAforDevices"] = value;
				}
			}

			// Token: 0x1700019A RID: 410
			// (set) Token: 0x0600166B RID: 5739 RVA: 0x00034D19 File Offset: 0x00032F19
			public virtual SwitchParameter ShowRecoveryPassword
			{
				set
				{
					base.PowerSharpParameters["ShowRecoveryPassword"] = value;
				}
			}

			// Token: 0x1700019B RID: 411
			// (set) Token: 0x0600166C RID: 5740 RVA: 0x00034D31 File Offset: 0x00032F31
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700019C RID: 412
			// (set) Token: 0x0600166D RID: 5741 RVA: 0x00034D44 File Offset: 0x00032F44
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700019D RID: 413
			// (set) Token: 0x0600166E RID: 5742 RVA: 0x00034D5C File Offset: 0x00032F5C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700019E RID: 414
			// (set) Token: 0x0600166F RID: 5743 RVA: 0x00034D74 File Offset: 0x00032F74
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700019F RID: 415
			// (set) Token: 0x06001670 RID: 5744 RVA: 0x00034D8C File Offset: 0x00032F8C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000045 RID: 69
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170001A0 RID: 416
			// (set) Token: 0x06001672 RID: 5746 RVA: 0x00034DAC File Offset: 0x00032FAC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MobileDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x170001A1 RID: 417
			// (set) Token: 0x06001673 RID: 5747 RVA: 0x00034DCA File Offset: 0x00032FCA
			public virtual SwitchParameter GetMailboxLog
			{
				set
				{
					base.PowerSharpParameters["GetMailboxLog"] = value;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (set) Token: 0x06001674 RID: 5748 RVA: 0x00034DE2 File Offset: 0x00032FE2
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (set) Token: 0x06001675 RID: 5749 RVA: 0x00034DF5 File Offset: 0x00032FF5
			public virtual SwitchParameter ActiveSync
			{
				set
				{
					base.PowerSharpParameters["ActiveSync"] = value;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (set) Token: 0x06001676 RID: 5750 RVA: 0x00034E0D File Offset: 0x0003300D
			public virtual SwitchParameter OWAforDevices
			{
				set
				{
					base.PowerSharpParameters["OWAforDevices"] = value;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (set) Token: 0x06001677 RID: 5751 RVA: 0x00034E25 File Offset: 0x00033025
			public virtual SwitchParameter ShowRecoveryPassword
			{
				set
				{
					base.PowerSharpParameters["ShowRecoveryPassword"] = value;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (set) Token: 0x06001678 RID: 5752 RVA: 0x00034E3D File Offset: 0x0003303D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001A7 RID: 423
			// (set) Token: 0x06001679 RID: 5753 RVA: 0x00034E50 File Offset: 0x00033050
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001A8 RID: 424
			// (set) Token: 0x0600167A RID: 5754 RVA: 0x00034E68 File Offset: 0x00033068
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001A9 RID: 425
			// (set) Token: 0x0600167B RID: 5755 RVA: 0x00034E80 File Offset: 0x00033080
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001AA RID: 426
			// (set) Token: 0x0600167C RID: 5756 RVA: 0x00034E98 File Offset: 0x00033098
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

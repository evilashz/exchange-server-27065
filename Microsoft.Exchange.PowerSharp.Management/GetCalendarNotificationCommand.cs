using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000440 RID: 1088
	public class GetCalendarNotificationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06003EFF RID: 16127 RVA: 0x00069814 File Offset: 0x00067A14
		private GetCalendarNotificationCommand() : base("Get-CalendarNotification")
		{
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x00069821 File Offset: 0x00067A21
		public GetCalendarNotificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x00069830 File Offset: 0x00067A30
		public virtual GetCalendarNotificationCommand SetParameters(GetCalendarNotificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0006983A File Offset: 0x00067A3A
		public virtual GetCalendarNotificationCommand SetParameters(GetCalendarNotificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000441 RID: 1089
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002238 RID: 8760
			// (set) Token: 0x06003F03 RID: 16131 RVA: 0x00069844 File Offset: 0x00067A44
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17002239 RID: 8761
			// (set) Token: 0x06003F04 RID: 16132 RVA: 0x00069862 File Offset: 0x00067A62
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700223A RID: 8762
			// (set) Token: 0x06003F05 RID: 16133 RVA: 0x00069875 File Offset: 0x00067A75
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700223B RID: 8763
			// (set) Token: 0x06003F06 RID: 16134 RVA: 0x0006988D File Offset: 0x00067A8D
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700223C RID: 8764
			// (set) Token: 0x06003F07 RID: 16135 RVA: 0x000698A5 File Offset: 0x00067AA5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700223D RID: 8765
			// (set) Token: 0x06003F08 RID: 16136 RVA: 0x000698B8 File Offset: 0x00067AB8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700223E RID: 8766
			// (set) Token: 0x06003F09 RID: 16137 RVA: 0x000698D0 File Offset: 0x00067AD0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700223F RID: 8767
			// (set) Token: 0x06003F0A RID: 16138 RVA: 0x000698E8 File Offset: 0x00067AE8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002240 RID: 8768
			// (set) Token: 0x06003F0B RID: 16139 RVA: 0x00069900 File Offset: 0x00067B00
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000442 RID: 1090
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002241 RID: 8769
			// (set) Token: 0x06003F0D RID: 16141 RVA: 0x00069920 File Offset: 0x00067B20
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002242 RID: 8770
			// (set) Token: 0x06003F0E RID: 16142 RVA: 0x00069933 File Offset: 0x00067B33
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002243 RID: 8771
			// (set) Token: 0x06003F0F RID: 16143 RVA: 0x0006994B File Offset: 0x00067B4B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002244 RID: 8772
			// (set) Token: 0x06003F10 RID: 16144 RVA: 0x00069963 File Offset: 0x00067B63
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002245 RID: 8773
			// (set) Token: 0x06003F11 RID: 16145 RVA: 0x00069976 File Offset: 0x00067B76
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002246 RID: 8774
			// (set) Token: 0x06003F12 RID: 16146 RVA: 0x0006998E File Offset: 0x00067B8E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002247 RID: 8775
			// (set) Token: 0x06003F13 RID: 16147 RVA: 0x000699A6 File Offset: 0x00067BA6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002248 RID: 8776
			// (set) Token: 0x06003F14 RID: 16148 RVA: 0x000699BE File Offset: 0x00067BBE
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

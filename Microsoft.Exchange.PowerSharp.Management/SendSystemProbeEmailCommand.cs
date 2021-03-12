using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BCD RID: 3021
	public class SendSystemProbeEmailCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060091A9 RID: 37289 RVA: 0x000D4BFA File Offset: 0x000D2DFA
		private SendSystemProbeEmailCommand() : base("Send-SystemProbeEmail")
		{
		}

		// Token: 0x060091AA RID: 37290 RVA: 0x000D4C07 File Offset: 0x000D2E07
		public SendSystemProbeEmailCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060091AB RID: 37291 RVA: 0x000D4C16 File Offset: 0x000D2E16
		public virtual SendSystemProbeEmailCommand SetParameters(SendSystemProbeEmailCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BCE RID: 3022
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170065C8 RID: 26056
			// (set) Token: 0x060091AC RID: 37292 RVA: 0x000D4C20 File Offset: 0x000D2E20
			public virtual string Subject
			{
				set
				{
					base.PowerSharpParameters["Subject"] = value;
				}
			}

			// Token: 0x170065C9 RID: 26057
			// (set) Token: 0x060091AD RID: 37293 RVA: 0x000D4C33 File Offset: 0x000D2E33
			public virtual string Body
			{
				set
				{
					base.PowerSharpParameters["Body"] = value;
				}
			}

			// Token: 0x170065CA RID: 26058
			// (set) Token: 0x060091AE RID: 37294 RVA: 0x000D4C46 File Offset: 0x000D2E46
			public virtual string Attachments
			{
				set
				{
					base.PowerSharpParameters["Attachments"] = value;
				}
			}

			// Token: 0x170065CB RID: 26059
			// (set) Token: 0x060091AF RID: 37295 RVA: 0x000D4C59 File Offset: 0x000D2E59
			public virtual string SmtpServer
			{
				set
				{
					base.PowerSharpParameters["SmtpServer"] = value;
				}
			}

			// Token: 0x170065CC RID: 26060
			// (set) Token: 0x060091B0 RID: 37296 RVA: 0x000D4C6C File Offset: 0x000D2E6C
			public virtual string SmtpUser
			{
				set
				{
					base.PowerSharpParameters["SmtpUser"] = value;
				}
			}

			// Token: 0x170065CD RID: 26061
			// (set) Token: 0x060091B1 RID: 37297 RVA: 0x000D4C7F File Offset: 0x000D2E7F
			public virtual string SmtpPassword
			{
				set
				{
					base.PowerSharpParameters["SmtpPassword"] = value;
				}
			}

			// Token: 0x170065CE RID: 26062
			// (set) Token: 0x060091B2 RID: 37298 RVA: 0x000D4C92 File Offset: 0x000D2E92
			public virtual string From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x170065CF RID: 26063
			// (set) Token: 0x060091B3 RID: 37299 RVA: 0x000D4CA5 File Offset: 0x000D2EA5
			public virtual string To
			{
				set
				{
					base.PowerSharpParameters["To"] = value;
				}
			}

			// Token: 0x170065D0 RID: 26064
			// (set) Token: 0x060091B4 RID: 37300 RVA: 0x000D4CB8 File Offset: 0x000D2EB8
			public virtual string CC
			{
				set
				{
					base.PowerSharpParameters["CC"] = value;
				}
			}

			// Token: 0x170065D1 RID: 26065
			// (set) Token: 0x060091B5 RID: 37301 RVA: 0x000D4CCB File Offset: 0x000D2ECB
			public virtual bool Html
			{
				set
				{
					base.PowerSharpParameters["Html"] = value;
				}
			}

			// Token: 0x170065D2 RID: 26066
			// (set) Token: 0x060091B6 RID: 37302 RVA: 0x000D4CE3 File Offset: 0x000D2EE3
			public virtual Guid ProbeGuid
			{
				set
				{
					base.PowerSharpParameters["ProbeGuid"] = value;
				}
			}

			// Token: 0x170065D3 RID: 26067
			// (set) Token: 0x060091B7 RID: 37303 RVA: 0x000D4CFB File Offset: 0x000D2EFB
			public virtual bool UseSsl
			{
				set
				{
					base.PowerSharpParameters["UseSsl"] = value;
				}
			}

			// Token: 0x170065D4 RID: 26068
			// (set) Token: 0x060091B8 RID: 37304 RVA: 0x000D4D13 File Offset: 0x000D2F13
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x170065D5 RID: 26069
			// (set) Token: 0x060091B9 RID: 37305 RVA: 0x000D4D2B File Offset: 0x000D2F2B
			public virtual SwitchParameter UseXheader
			{
				set
				{
					base.PowerSharpParameters["UseXheader"] = value;
				}
			}

			// Token: 0x170065D6 RID: 26070
			// (set) Token: 0x060091BA RID: 37306 RVA: 0x000D4D43 File Offset: 0x000D2F43
			public virtual bool TestContext
			{
				set
				{
					base.PowerSharpParameters["TestContext"] = value;
				}
			}

			// Token: 0x170065D7 RID: 26071
			// (set) Token: 0x060091BB RID: 37307 RVA: 0x000D4D5B File Offset: 0x000D2F5B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065D8 RID: 26072
			// (set) Token: 0x060091BC RID: 37308 RVA: 0x000D4D73 File Offset: 0x000D2F73
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065D9 RID: 26073
			// (set) Token: 0x060091BD RID: 37309 RVA: 0x000D4D8B File Offset: 0x000D2F8B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065DA RID: 26074
			// (set) Token: 0x060091BE RID: 37310 RVA: 0x000D4DA3 File Offset: 0x000D2FA3
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

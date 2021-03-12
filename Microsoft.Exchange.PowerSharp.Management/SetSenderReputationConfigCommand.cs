using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000427 RID: 1063
	public class SetSenderReputationConfigCommand : SyntheticCommandWithPipelineInputNoOutput<SenderReputationConfig>
	{
		// Token: 0x06003E29 RID: 15913 RVA: 0x0006870F File Offset: 0x0006690F
		private SetSenderReputationConfigCommand() : base("Set-SenderReputationConfig")
		{
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x0006871C File Offset: 0x0006691C
		public SetSenderReputationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x0006872B File Offset: 0x0006692B
		public virtual SetSenderReputationConfigCommand SetParameters(SetSenderReputationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000428 RID: 1064
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002194 RID: 8596
			// (set) Token: 0x06003E2C RID: 15916 RVA: 0x00068735 File Offset: 0x00066935
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002195 RID: 8597
			// (set) Token: 0x06003E2D RID: 15917 RVA: 0x00068748 File Offset: 0x00066948
			public virtual int SrlBlockThreshold
			{
				set
				{
					base.PowerSharpParameters["SrlBlockThreshold"] = value;
				}
			}

			// Token: 0x17002196 RID: 8598
			// (set) Token: 0x06003E2E RID: 15918 RVA: 0x00068760 File Offset: 0x00066960
			public virtual bool OpenProxyDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["OpenProxyDetectionEnabled"] = value;
				}
			}

			// Token: 0x17002197 RID: 8599
			// (set) Token: 0x06003E2F RID: 15919 RVA: 0x00068778 File Offset: 0x00066978
			public virtual bool SenderBlockingEnabled
			{
				set
				{
					base.PowerSharpParameters["SenderBlockingEnabled"] = value;
				}
			}

			// Token: 0x17002198 RID: 8600
			// (set) Token: 0x06003E30 RID: 15920 RVA: 0x00068790 File Offset: 0x00066990
			public virtual int SenderBlockingPeriod
			{
				set
				{
					base.PowerSharpParameters["SenderBlockingPeriod"] = value;
				}
			}

			// Token: 0x17002199 RID: 8601
			// (set) Token: 0x06003E31 RID: 15921 RVA: 0x000687A8 File Offset: 0x000669A8
			public virtual string ProxyServerName
			{
				set
				{
					base.PowerSharpParameters["ProxyServerName"] = value;
				}
			}

			// Token: 0x1700219A RID: 8602
			// (set) Token: 0x06003E32 RID: 15922 RVA: 0x000687BB File Offset: 0x000669BB
			public virtual int ProxyServerPort
			{
				set
				{
					base.PowerSharpParameters["ProxyServerPort"] = value;
				}
			}

			// Token: 0x1700219B RID: 8603
			// (set) Token: 0x06003E33 RID: 15923 RVA: 0x000687D3 File Offset: 0x000669D3
			public virtual ProxyType ProxyServerType
			{
				set
				{
					base.PowerSharpParameters["ProxyServerType"] = value;
				}
			}

			// Token: 0x1700219C RID: 8604
			// (set) Token: 0x06003E34 RID: 15924 RVA: 0x000687EB File Offset: 0x000669EB
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700219D RID: 8605
			// (set) Token: 0x06003E35 RID: 15925 RVA: 0x00068803 File Offset: 0x00066A03
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x1700219E RID: 8606
			// (set) Token: 0x06003E36 RID: 15926 RVA: 0x0006881B File Offset: 0x00066A1B
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x1700219F RID: 8607
			// (set) Token: 0x06003E37 RID: 15927 RVA: 0x00068833 File Offset: 0x00066A33
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021A0 RID: 8608
			// (set) Token: 0x06003E38 RID: 15928 RVA: 0x0006884B File Offset: 0x00066A4B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021A1 RID: 8609
			// (set) Token: 0x06003E39 RID: 15929 RVA: 0x00068863 File Offset: 0x00066A63
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021A2 RID: 8610
			// (set) Token: 0x06003E3A RID: 15930 RVA: 0x0006887B File Offset: 0x00066A7B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021A3 RID: 8611
			// (set) Token: 0x06003E3B RID: 15931 RVA: 0x00068893 File Offset: 0x00066A93
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

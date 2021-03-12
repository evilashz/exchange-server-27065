using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200086D RID: 2157
	public class SetSyncDaemonArbitrationConfigCommand : SyntheticCommandWithPipelineInputNoOutput<SyncDaemonArbitrationConfig>
	{
		// Token: 0x06006AD0 RID: 27344 RVA: 0x000A1FB8 File Offset: 0x000A01B8
		private SetSyncDaemonArbitrationConfigCommand() : base("Set-SyncDaemonArbitrationConfig")
		{
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x000A1FC5 File Offset: 0x000A01C5
		public SetSyncDaemonArbitrationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x000A1FD4 File Offset: 0x000A01D4
		public virtual SetSyncDaemonArbitrationConfigCommand SetParameters(SetSyncDaemonArbitrationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200086E RID: 2158
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170045AF RID: 17839
			// (set) Token: 0x06006AD3 RID: 27347 RVA: 0x000A1FDE File Offset: 0x000A01DE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045B0 RID: 17840
			// (set) Token: 0x06006AD4 RID: 27348 RVA: 0x000A1FF1 File Offset: 0x000A01F1
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x170045B1 RID: 17841
			// (set) Token: 0x06006AD5 RID: 27349 RVA: 0x000A2004 File Offset: 0x000A0204
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x170045B2 RID: 17842
			// (set) Token: 0x06006AD6 RID: 27350 RVA: 0x000A2017 File Offset: 0x000A0217
			public virtual int ActiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["ActiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x170045B3 RID: 17843
			// (set) Token: 0x06006AD7 RID: 27351 RVA: 0x000A202F File Offset: 0x000A022F
			public virtual int PassiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["PassiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x170045B4 RID: 17844
			// (set) Token: 0x06006AD8 RID: 27352 RVA: 0x000A2047 File Offset: 0x000A0247
			public virtual bool IsEnabled
			{
				set
				{
					base.PowerSharpParameters["IsEnabled"] = value;
				}
			}

			// Token: 0x170045B5 RID: 17845
			// (set) Token: 0x06006AD9 RID: 27353 RVA: 0x000A205F File Offset: 0x000A025F
			public virtual bool IsHalted
			{
				set
				{
					base.PowerSharpParameters["IsHalted"] = value;
				}
			}

			// Token: 0x170045B6 RID: 17846
			// (set) Token: 0x06006ADA RID: 27354 RVA: 0x000A2077 File Offset: 0x000A0277
			public virtual bool IsHaltRecoveryDisabled
			{
				set
				{
					base.PowerSharpParameters["IsHaltRecoveryDisabled"] = value;
				}
			}

			// Token: 0x170045B7 RID: 17847
			// (set) Token: 0x06006ADB RID: 27355 RVA: 0x000A208F File Offset: 0x000A028F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170045B8 RID: 17848
			// (set) Token: 0x06006ADC RID: 27356 RVA: 0x000A20A2 File Offset: 0x000A02A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045B9 RID: 17849
			// (set) Token: 0x06006ADD RID: 27357 RVA: 0x000A20BA File Offset: 0x000A02BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045BA RID: 17850
			// (set) Token: 0x06006ADE RID: 27358 RVA: 0x000A20D2 File Offset: 0x000A02D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045BB RID: 17851
			// (set) Token: 0x06006ADF RID: 27359 RVA: 0x000A20EA File Offset: 0x000A02EA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170045BC RID: 17852
			// (set) Token: 0x06006AE0 RID: 27360 RVA: 0x000A2102 File Offset: 0x000A0302
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

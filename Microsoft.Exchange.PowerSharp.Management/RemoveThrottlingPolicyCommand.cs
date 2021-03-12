using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000878 RID: 2168
	public class RemoveThrottlingPolicyCommand : SyntheticCommandWithPipelineInput<ThrottlingPolicy, ThrottlingPolicy>
	{
		// Token: 0x06006B9F RID: 27551 RVA: 0x000A31E9 File Offset: 0x000A13E9
		private RemoveThrottlingPolicyCommand() : base("Remove-ThrottlingPolicy")
		{
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x000A31F6 File Offset: 0x000A13F6
		public RemoveThrottlingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x000A3205 File Offset: 0x000A1405
		public virtual RemoveThrottlingPolicyCommand SetParameters(RemoveThrottlingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x000A320F File Offset: 0x000A140F
		public virtual RemoveThrottlingPolicyCommand SetParameters(RemoveThrottlingPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000879 RID: 2169
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004668 RID: 18024
			// (set) Token: 0x06006BA3 RID: 27555 RVA: 0x000A3219 File Offset: 0x000A1419
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004669 RID: 18025
			// (set) Token: 0x06006BA4 RID: 27556 RVA: 0x000A3231 File Offset: 0x000A1431
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700466A RID: 18026
			// (set) Token: 0x06006BA5 RID: 27557 RVA: 0x000A3244 File Offset: 0x000A1444
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700466B RID: 18027
			// (set) Token: 0x06006BA6 RID: 27558 RVA: 0x000A325C File Offset: 0x000A145C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700466C RID: 18028
			// (set) Token: 0x06006BA7 RID: 27559 RVA: 0x000A3274 File Offset: 0x000A1474
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700466D RID: 18029
			// (set) Token: 0x06006BA8 RID: 27560 RVA: 0x000A328C File Offset: 0x000A148C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700466E RID: 18030
			// (set) Token: 0x06006BA9 RID: 27561 RVA: 0x000A32A4 File Offset: 0x000A14A4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700466F RID: 18031
			// (set) Token: 0x06006BAA RID: 27562 RVA: 0x000A32BC File Offset: 0x000A14BC
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200087A RID: 2170
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004670 RID: 18032
			// (set) Token: 0x06006BAC RID: 27564 RVA: 0x000A32DC File Offset: 0x000A14DC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17004671 RID: 18033
			// (set) Token: 0x06006BAD RID: 27565 RVA: 0x000A32FA File Offset: 0x000A14FA
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004672 RID: 18034
			// (set) Token: 0x06006BAE RID: 27566 RVA: 0x000A3312 File Offset: 0x000A1512
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004673 RID: 18035
			// (set) Token: 0x06006BAF RID: 27567 RVA: 0x000A3325 File Offset: 0x000A1525
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004674 RID: 18036
			// (set) Token: 0x06006BB0 RID: 27568 RVA: 0x000A333D File Offset: 0x000A153D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004675 RID: 18037
			// (set) Token: 0x06006BB1 RID: 27569 RVA: 0x000A3355 File Offset: 0x000A1555
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004676 RID: 18038
			// (set) Token: 0x06006BB2 RID: 27570 RVA: 0x000A336D File Offset: 0x000A156D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004677 RID: 18039
			// (set) Token: 0x06006BB3 RID: 27571 RVA: 0x000A3385 File Offset: 0x000A1585
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004678 RID: 18040
			// (set) Token: 0x06006BB4 RID: 27572 RVA: 0x000A339D File Offset: 0x000A159D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}

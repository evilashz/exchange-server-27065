using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BC9 RID: 3017
	public class GetSystemProbeCommand : SyntheticCommandWithPipelineInput<Guid, Guid>
	{
		// Token: 0x06009195 RID: 37269 RVA: 0x000D4A7E File Offset: 0x000D2C7E
		private GetSystemProbeCommand() : base("Get-SystemProbe")
		{
		}

		// Token: 0x06009196 RID: 37270 RVA: 0x000D4A8B File Offset: 0x000D2C8B
		public GetSystemProbeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009197 RID: 37271 RVA: 0x000D4A9A File Offset: 0x000D2C9A
		public virtual GetSystemProbeCommand SetParameters(GetSystemProbeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BCA RID: 3018
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170065BC RID: 26044
			// (set) Token: 0x06009198 RID: 37272 RVA: 0x000D4AA4 File Offset: 0x000D2CA4
			public virtual DateTimeOffset? StartTime
			{
				set
				{
					base.PowerSharpParameters["StartTime"] = value;
				}
			}

			// Token: 0x170065BD RID: 26045
			// (set) Token: 0x06009199 RID: 37273 RVA: 0x000D4ABC File Offset: 0x000D2CBC
			public virtual DateTimeOffset? EndTime
			{
				set
				{
					base.PowerSharpParameters["EndTime"] = value;
				}
			}

			// Token: 0x170065BE RID: 26046
			// (set) Token: 0x0600919A RID: 37274 RVA: 0x000D4AD4 File Offset: 0x000D2CD4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065BF RID: 26047
			// (set) Token: 0x0600919B RID: 37275 RVA: 0x000D4AEC File Offset: 0x000D2CEC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065C0 RID: 26048
			// (set) Token: 0x0600919C RID: 37276 RVA: 0x000D4B04 File Offset: 0x000D2D04
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065C1 RID: 26049
			// (set) Token: 0x0600919D RID: 37277 RVA: 0x000D4B1C File Offset: 0x000D2D1C
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

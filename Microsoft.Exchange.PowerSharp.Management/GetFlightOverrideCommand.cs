using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005B2 RID: 1458
	public class GetFlightOverrideCommand : SyntheticCommand<object>
	{
		// Token: 0x06004C39 RID: 19513 RVA: 0x0007A327 File Offset: 0x00078527
		private GetFlightOverrideCommand() : base("Get-FlightOverride")
		{
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0007A334 File Offset: 0x00078534
		public GetFlightOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0007A343 File Offset: 0x00078543
		public virtual GetFlightOverrideCommand SetParameters(GetFlightOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0007A34D File Offset: 0x0007854D
		public virtual GetFlightOverrideCommand SetParameters(GetFlightOverrideCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005B3 RID: 1459
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002C8E RID: 11406
			// (set) Token: 0x06004C3D RID: 19517 RVA: 0x0007A357 File Offset: 0x00078557
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C8F RID: 11407
			// (set) Token: 0x06004C3E RID: 19518 RVA: 0x0007A36A File Offset: 0x0007856A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C90 RID: 11408
			// (set) Token: 0x06004C3F RID: 19519 RVA: 0x0007A382 File Offset: 0x00078582
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C91 RID: 11409
			// (set) Token: 0x06004C40 RID: 19520 RVA: 0x0007A39A File Offset: 0x0007859A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C92 RID: 11410
			// (set) Token: 0x06004C41 RID: 19521 RVA: 0x0007A3B2 File Offset: 0x000785B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005B4 RID: 1460
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002C93 RID: 11411
			// (set) Token: 0x06004C43 RID: 19523 RVA: 0x0007A3D2 File Offset: 0x000785D2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SettingOverrideIdParameter(value) : null);
				}
			}

			// Token: 0x17002C94 RID: 11412
			// (set) Token: 0x06004C44 RID: 19524 RVA: 0x0007A3F0 File Offset: 0x000785F0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C95 RID: 11413
			// (set) Token: 0x06004C45 RID: 19525 RVA: 0x0007A403 File Offset: 0x00078603
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C96 RID: 11414
			// (set) Token: 0x06004C46 RID: 19526 RVA: 0x0007A41B File Offset: 0x0007861B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C97 RID: 11415
			// (set) Token: 0x06004C47 RID: 19527 RVA: 0x0007A433 File Offset: 0x00078633
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C98 RID: 11416
			// (set) Token: 0x06004C48 RID: 19528 RVA: 0x0007A44B File Offset: 0x0007864B
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

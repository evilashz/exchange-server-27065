using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.PushNotifications;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E9C RID: 3740
	public class DisablePushNotificationProxyCommand : SyntheticCommandWithPipelineInput<PushNotificationProxyPresentationObject, PushNotificationProxyPresentationObject>
	{
		// Token: 0x0600DBDE RID: 56286 RVA: 0x00137CC2 File Offset: 0x00135EC2
		private DisablePushNotificationProxyCommand() : base("Disable-PushNotificationProxy")
		{
		}

		// Token: 0x0600DBDF RID: 56287 RVA: 0x00137CCF File Offset: 0x00135ECF
		public DisablePushNotificationProxyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DBE0 RID: 56288 RVA: 0x00137CDE File Offset: 0x00135EDE
		public virtual DisablePushNotificationProxyCommand SetParameters(DisablePushNotificationProxyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E9D RID: 3741
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA5F RID: 43615
			// (set) Token: 0x0600DBE1 RID: 56289 RVA: 0x00137CE8 File Offset: 0x00135EE8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA60 RID: 43616
			// (set) Token: 0x0600DBE2 RID: 56290 RVA: 0x00137CFB File Offset: 0x00135EFB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AA61 RID: 43617
			// (set) Token: 0x0600DBE3 RID: 56291 RVA: 0x00137D0E File Offset: 0x00135F0E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA62 RID: 43618
			// (set) Token: 0x0600DBE4 RID: 56292 RVA: 0x00137D26 File Offset: 0x00135F26
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA63 RID: 43619
			// (set) Token: 0x0600DBE5 RID: 56293 RVA: 0x00137D3E File Offset: 0x00135F3E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA64 RID: 43620
			// (set) Token: 0x0600DBE6 RID: 56294 RVA: 0x00137D56 File Offset: 0x00135F56
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA65 RID: 43621
			// (set) Token: 0x0600DBE7 RID: 56295 RVA: 0x00137D6E File Offset: 0x00135F6E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700AA66 RID: 43622
			// (set) Token: 0x0600DBE8 RID: 56296 RVA: 0x00137D86 File Offset: 0x00135F86
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.PushNotifications;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E9E RID: 3742
	public class EnablePushNotificationProxyCommand : SyntheticCommandWithPipelineInput<PushNotificationProxyPresentationObject, PushNotificationProxyPresentationObject>
	{
		// Token: 0x0600DBEA RID: 56298 RVA: 0x00137DA6 File Offset: 0x00135FA6
		private EnablePushNotificationProxyCommand() : base("Enable-PushNotificationProxy")
		{
		}

		// Token: 0x0600DBEB RID: 56299 RVA: 0x00137DB3 File Offset: 0x00135FB3
		public EnablePushNotificationProxyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DBEC RID: 56300 RVA: 0x00137DC2 File Offset: 0x00135FC2
		public virtual EnablePushNotificationProxyCommand SetParameters(EnablePushNotificationProxyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E9F RID: 3743
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA67 RID: 43623
			// (set) Token: 0x0600DBED RID: 56301 RVA: 0x00137DCC File Offset: 0x00135FCC
			public virtual string Uri
			{
				set
				{
					base.PowerSharpParameters["Uri"] = value;
				}
			}

			// Token: 0x1700AA68 RID: 43624
			// (set) Token: 0x0600DBEE RID: 56302 RVA: 0x00137DDF File Offset: 0x00135FDF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = value;
				}
			}

			// Token: 0x1700AA69 RID: 43625
			// (set) Token: 0x0600DBEF RID: 56303 RVA: 0x00137DF2 File Offset: 0x00135FF2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA6A RID: 43626
			// (set) Token: 0x0600DBF0 RID: 56304 RVA: 0x00137E05 File Offset: 0x00136005
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AA6B RID: 43627
			// (set) Token: 0x0600DBF1 RID: 56305 RVA: 0x00137E18 File Offset: 0x00136018
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA6C RID: 43628
			// (set) Token: 0x0600DBF2 RID: 56306 RVA: 0x00137E30 File Offset: 0x00136030
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA6D RID: 43629
			// (set) Token: 0x0600DBF3 RID: 56307 RVA: 0x00137E48 File Offset: 0x00136048
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA6E RID: 43630
			// (set) Token: 0x0600DBF4 RID: 56308 RVA: 0x00137E60 File Offset: 0x00136060
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA6F RID: 43631
			// (set) Token: 0x0600DBF5 RID: 56309 RVA: 0x00137E78 File Offset: 0x00136078
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

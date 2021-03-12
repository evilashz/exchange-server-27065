using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000EB3 RID: 3763
	public class RemovePushNotificationAppCommand : SyntheticCommandWithPipelineInput<PushNotificationApp, PushNotificationApp>
	{
		// Token: 0x0600DCE0 RID: 56544 RVA: 0x001390E6 File Offset: 0x001372E6
		private RemovePushNotificationAppCommand() : base("Remove-PushNotificationApp")
		{
		}

		// Token: 0x0600DCE1 RID: 56545 RVA: 0x001390F3 File Offset: 0x001372F3
		public RemovePushNotificationAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DCE2 RID: 56546 RVA: 0x00139102 File Offset: 0x00137302
		public virtual RemovePushNotificationAppCommand SetParameters(RemovePushNotificationAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DCE3 RID: 56547 RVA: 0x0013910C File Offset: 0x0013730C
		public virtual RemovePushNotificationAppCommand SetParameters(RemovePushNotificationAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000EB4 RID: 3764
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AB33 RID: 43827
			// (set) Token: 0x0600DCE4 RID: 56548 RVA: 0x00139116 File Offset: 0x00137316
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB34 RID: 43828
			// (set) Token: 0x0600DCE5 RID: 56549 RVA: 0x00139129 File Offset: 0x00137329
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB35 RID: 43829
			// (set) Token: 0x0600DCE6 RID: 56550 RVA: 0x00139141 File Offset: 0x00137341
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB36 RID: 43830
			// (set) Token: 0x0600DCE7 RID: 56551 RVA: 0x00139159 File Offset: 0x00137359
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB37 RID: 43831
			// (set) Token: 0x0600DCE8 RID: 56552 RVA: 0x00139171 File Offset: 0x00137371
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB38 RID: 43832
			// (set) Token: 0x0600DCE9 RID: 56553 RVA: 0x00139189 File Offset: 0x00137389
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700AB39 RID: 43833
			// (set) Token: 0x0600DCEA RID: 56554 RVA: 0x001391A1 File Offset: 0x001373A1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000EB5 RID: 3765
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700AB3A RID: 43834
			// (set) Token: 0x0600DCEC RID: 56556 RVA: 0x001391C1 File Offset: 0x001373C1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB3B RID: 43835
			// (set) Token: 0x0600DCED RID: 56557 RVA: 0x001391DF File Offset: 0x001373DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB3C RID: 43836
			// (set) Token: 0x0600DCEE RID: 56558 RVA: 0x001391F2 File Offset: 0x001373F2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB3D RID: 43837
			// (set) Token: 0x0600DCEF RID: 56559 RVA: 0x0013920A File Offset: 0x0013740A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB3E RID: 43838
			// (set) Token: 0x0600DCF0 RID: 56560 RVA: 0x00139222 File Offset: 0x00137422
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB3F RID: 43839
			// (set) Token: 0x0600DCF1 RID: 56561 RVA: 0x0013923A File Offset: 0x0013743A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB40 RID: 43840
			// (set) Token: 0x0600DCF2 RID: 56562 RVA: 0x00139252 File Offset: 0x00137452
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700AB41 RID: 43841
			// (set) Token: 0x0600DCF3 RID: 56563 RVA: 0x0013926A File Offset: 0x0013746A
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

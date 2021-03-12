using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.PushNotifications;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000EA0 RID: 3744
	public class GetPushNotificationAppCommand : SyntheticCommandWithPipelineInput<PushNotificationAppPresentationObject, PushNotificationAppPresentationObject>
	{
		// Token: 0x0600DBF7 RID: 56311 RVA: 0x00137E98 File Offset: 0x00136098
		private GetPushNotificationAppCommand() : base("Get-PushNotificationApp")
		{
		}

		// Token: 0x0600DBF8 RID: 56312 RVA: 0x00137EA5 File Offset: 0x001360A5
		public GetPushNotificationAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DBF9 RID: 56313 RVA: 0x00137EB4 File Offset: 0x001360B4
		public virtual GetPushNotificationAppCommand SetParameters(GetPushNotificationAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DBFA RID: 56314 RVA: 0x00137EBE File Offset: 0x001360BE
		public virtual GetPushNotificationAppCommand SetParameters(GetPushNotificationAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000EA1 RID: 3745
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA70 RID: 43632
			// (set) Token: 0x0600DBFB RID: 56315 RVA: 0x00137EC8 File Offset: 0x001360C8
			public virtual PushNotificationPlatform Platform
			{
				set
				{
					base.PowerSharpParameters["Platform"] = value;
				}
			}

			// Token: 0x1700AA71 RID: 43633
			// (set) Token: 0x0600DBFC RID: 56316 RVA: 0x00137EE0 File Offset: 0x001360E0
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AA72 RID: 43634
			// (set) Token: 0x0600DBFD RID: 56317 RVA: 0x00137EF8 File Offset: 0x001360F8
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AA73 RID: 43635
			// (set) Token: 0x0600DBFE RID: 56318 RVA: 0x00137F10 File Offset: 0x00136110
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA74 RID: 43636
			// (set) Token: 0x0600DBFF RID: 56319 RVA: 0x00137F23 File Offset: 0x00136123
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA75 RID: 43637
			// (set) Token: 0x0600DC00 RID: 56320 RVA: 0x00137F3B File Offset: 0x0013613B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA76 RID: 43638
			// (set) Token: 0x0600DC01 RID: 56321 RVA: 0x00137F53 File Offset: 0x00136153
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA77 RID: 43639
			// (set) Token: 0x0600DC02 RID: 56322 RVA: 0x00137F6B File Offset: 0x0013616B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000EA2 RID: 3746
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700AA78 RID: 43640
			// (set) Token: 0x0600DC04 RID: 56324 RVA: 0x00137F8B File Offset: 0x0013618B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA79 RID: 43641
			// (set) Token: 0x0600DC05 RID: 56325 RVA: 0x00137FA9 File Offset: 0x001361A9
			public virtual PushNotificationPlatform Platform
			{
				set
				{
					base.PowerSharpParameters["Platform"] = value;
				}
			}

			// Token: 0x1700AA7A RID: 43642
			// (set) Token: 0x0600DC06 RID: 56326 RVA: 0x00137FC1 File Offset: 0x001361C1
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AA7B RID: 43643
			// (set) Token: 0x0600DC07 RID: 56327 RVA: 0x00137FD9 File Offset: 0x001361D9
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AA7C RID: 43644
			// (set) Token: 0x0600DC08 RID: 56328 RVA: 0x00137FF1 File Offset: 0x001361F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA7D RID: 43645
			// (set) Token: 0x0600DC09 RID: 56329 RVA: 0x00138004 File Offset: 0x00136204
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA7E RID: 43646
			// (set) Token: 0x0600DC0A RID: 56330 RVA: 0x0013801C File Offset: 0x0013621C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA7F RID: 43647
			// (set) Token: 0x0600DC0B RID: 56331 RVA: 0x00138034 File Offset: 0x00136234
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA80 RID: 43648
			// (set) Token: 0x0600DC0C RID: 56332 RVA: 0x0013804C File Offset: 0x0013624C
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

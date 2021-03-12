using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200090C RID: 2316
	public class GetHostedEncryptionVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADE4eVirtualDirectory, ADE4eVirtualDirectory>
	{
		// Token: 0x06007566 RID: 30054 RVA: 0x000B049F File Offset: 0x000AE69F
		private GetHostedEncryptionVirtualDirectoryCommand() : base("Get-HostedEncryptionVirtualDirectory")
		{
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x000B04AC File Offset: 0x000AE6AC
		public GetHostedEncryptionVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x000B04BB File Offset: 0x000AE6BB
		public virtual GetHostedEncryptionVirtualDirectoryCommand SetParameters(GetHostedEncryptionVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007569 RID: 30057 RVA: 0x000B04C5 File Offset: 0x000AE6C5
		public virtual GetHostedEncryptionVirtualDirectoryCommand SetParameters(GetHostedEncryptionVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600756A RID: 30058 RVA: 0x000B04CF File Offset: 0x000AE6CF
		public virtual GetHostedEncryptionVirtualDirectoryCommand SetParameters(GetHostedEncryptionVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200090D RID: 2317
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004F07 RID: 20231
			// (set) Token: 0x0600756B RID: 30059 RVA: 0x000B04D9 File Offset: 0x000AE6D9
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F08 RID: 20232
			// (set) Token: 0x0600756C RID: 30060 RVA: 0x000B04F1 File Offset: 0x000AE6F1
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F09 RID: 20233
			// (set) Token: 0x0600756D RID: 30061 RVA: 0x000B0509 File Offset: 0x000AE709
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F0A RID: 20234
			// (set) Token: 0x0600756E RID: 30062 RVA: 0x000B0521 File Offset: 0x000AE721
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F0B RID: 20235
			// (set) Token: 0x0600756F RID: 30063 RVA: 0x000B0534 File Offset: 0x000AE734
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F0C RID: 20236
			// (set) Token: 0x06007570 RID: 30064 RVA: 0x000B054C File Offset: 0x000AE74C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F0D RID: 20237
			// (set) Token: 0x06007571 RID: 30065 RVA: 0x000B0564 File Offset: 0x000AE764
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F0E RID: 20238
			// (set) Token: 0x06007572 RID: 30066 RVA: 0x000B057C File Offset: 0x000AE77C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200090E RID: 2318
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004F0F RID: 20239
			// (set) Token: 0x06007574 RID: 30068 RVA: 0x000B059C File Offset: 0x000AE79C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004F10 RID: 20240
			// (set) Token: 0x06007575 RID: 30069 RVA: 0x000B05AF File Offset: 0x000AE7AF
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F11 RID: 20241
			// (set) Token: 0x06007576 RID: 30070 RVA: 0x000B05C7 File Offset: 0x000AE7C7
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F12 RID: 20242
			// (set) Token: 0x06007577 RID: 30071 RVA: 0x000B05DF File Offset: 0x000AE7DF
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F13 RID: 20243
			// (set) Token: 0x06007578 RID: 30072 RVA: 0x000B05F7 File Offset: 0x000AE7F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F14 RID: 20244
			// (set) Token: 0x06007579 RID: 30073 RVA: 0x000B060A File Offset: 0x000AE80A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F15 RID: 20245
			// (set) Token: 0x0600757A RID: 30074 RVA: 0x000B0622 File Offset: 0x000AE822
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F16 RID: 20246
			// (set) Token: 0x0600757B RID: 30075 RVA: 0x000B063A File Offset: 0x000AE83A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F17 RID: 20247
			// (set) Token: 0x0600757C RID: 30076 RVA: 0x000B0652 File Offset: 0x000AE852
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200090F RID: 2319
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004F18 RID: 20248
			// (set) Token: 0x0600757E RID: 30078 RVA: 0x000B0672 File Offset: 0x000AE872
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004F19 RID: 20249
			// (set) Token: 0x0600757F RID: 30079 RVA: 0x000B0685 File Offset: 0x000AE885
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F1A RID: 20250
			// (set) Token: 0x06007580 RID: 30080 RVA: 0x000B069D File Offset: 0x000AE89D
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F1B RID: 20251
			// (set) Token: 0x06007581 RID: 30081 RVA: 0x000B06B5 File Offset: 0x000AE8B5
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F1C RID: 20252
			// (set) Token: 0x06007582 RID: 30082 RVA: 0x000B06CD File Offset: 0x000AE8CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F1D RID: 20253
			// (set) Token: 0x06007583 RID: 30083 RVA: 0x000B06E0 File Offset: 0x000AE8E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F1E RID: 20254
			// (set) Token: 0x06007584 RID: 30084 RVA: 0x000B06F8 File Offset: 0x000AE8F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F1F RID: 20255
			// (set) Token: 0x06007585 RID: 30085 RVA: 0x000B0710 File Offset: 0x000AE910
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F20 RID: 20256
			// (set) Token: 0x06007586 RID: 30086 RVA: 0x000B0728 File Offset: 0x000AE928
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

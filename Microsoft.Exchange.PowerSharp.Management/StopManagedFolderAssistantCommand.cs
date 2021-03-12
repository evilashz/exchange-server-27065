using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000100 RID: 256
	public class StopManagedFolderAssistantCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06001EC1 RID: 7873 RVA: 0x0003FA5B File Offset: 0x0003DC5B
		private StopManagedFolderAssistantCommand() : base("Stop-ManagedFolderAssistant")
		{
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0003FA68 File Offset: 0x0003DC68
		public StopManagedFolderAssistantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0003FA77 File Offset: 0x0003DC77
		public virtual StopManagedFolderAssistantCommand SetParameters(StopManagedFolderAssistantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0003FA81 File Offset: 0x0003DC81
		public virtual StopManagedFolderAssistantCommand SetParameters(StopManagedFolderAssistantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000101 RID: 257
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700087A RID: 2170
			// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x0003FA8B File Offset: 0x0003DC8B
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700087B RID: 2171
			// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0003FA9E File Offset: 0x0003DC9E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700087C RID: 2172
			// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x0003FAB1 File Offset: 0x0003DCB1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700087D RID: 2173
			// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x0003FAC9 File Offset: 0x0003DCC9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700087E RID: 2174
			// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0003FAE1 File Offset: 0x0003DCE1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700087F RID: 2175
			// (set) Token: 0x06001ECA RID: 7882 RVA: 0x0003FAF9 File Offset: 0x0003DCF9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000880 RID: 2176
			// (set) Token: 0x06001ECB RID: 7883 RVA: 0x0003FB11 File Offset: 0x0003DD11
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000881 RID: 2177
			// (set) Token: 0x06001ECC RID: 7884 RVA: 0x0003FB29 File Offset: 0x0003DD29
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000102 RID: 258
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000882 RID: 2178
			// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0003FB49 File Offset: 0x0003DD49
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000883 RID: 2179
			// (set) Token: 0x06001ECF RID: 7887 RVA: 0x0003FB5C File Offset: 0x0003DD5C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000884 RID: 2180
			// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x0003FB74 File Offset: 0x0003DD74
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000885 RID: 2181
			// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0003FB8C File Offset: 0x0003DD8C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000886 RID: 2182
			// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000887 RID: 2183
			// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x0003FBBC File Offset: 0x0003DDBC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000888 RID: 2184
			// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x0003FBD4 File Offset: 0x0003DDD4
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

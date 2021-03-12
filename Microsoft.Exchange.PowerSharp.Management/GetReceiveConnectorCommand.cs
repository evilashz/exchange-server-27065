using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000896 RID: 2198
	public class GetReceiveConnectorCommand : SyntheticCommandWithPipelineInput<ReceiveConnector, ReceiveConnector>
	{
		// Token: 0x06006D29 RID: 27945 RVA: 0x000A538A File Offset: 0x000A358A
		private GetReceiveConnectorCommand() : base("Get-ReceiveConnector")
		{
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x000A5397 File Offset: 0x000A3597
		public GetReceiveConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x000A53A6 File Offset: 0x000A35A6
		public virtual GetReceiveConnectorCommand SetParameters(GetReceiveConnectorCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D2C RID: 27948 RVA: 0x000A53B0 File Offset: 0x000A35B0
		public virtual GetReceiveConnectorCommand SetParameters(GetReceiveConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D2D RID: 27949 RVA: 0x000A53BA File Offset: 0x000A35BA
		public virtual GetReceiveConnectorCommand SetParameters(GetReceiveConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000897 RID: 2199
		public class ServerParameters : ParametersBase
		{
			// Token: 0x170047B6 RID: 18358
			// (set) Token: 0x06006D2E RID: 27950 RVA: 0x000A53C4 File Offset: 0x000A35C4
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170047B7 RID: 18359
			// (set) Token: 0x06006D2F RID: 27951 RVA: 0x000A53D7 File Offset: 0x000A35D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047B8 RID: 18360
			// (set) Token: 0x06006D30 RID: 27952 RVA: 0x000A53EA File Offset: 0x000A35EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047B9 RID: 18361
			// (set) Token: 0x06006D31 RID: 27953 RVA: 0x000A5402 File Offset: 0x000A3602
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047BA RID: 18362
			// (set) Token: 0x06006D32 RID: 27954 RVA: 0x000A541A File Offset: 0x000A361A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047BB RID: 18363
			// (set) Token: 0x06006D33 RID: 27955 RVA: 0x000A5432 File Offset: 0x000A3632
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000898 RID: 2200
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170047BC RID: 18364
			// (set) Token: 0x06006D35 RID: 27957 RVA: 0x000A5452 File Offset: 0x000A3652
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047BD RID: 18365
			// (set) Token: 0x06006D36 RID: 27958 RVA: 0x000A5465 File Offset: 0x000A3665
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047BE RID: 18366
			// (set) Token: 0x06006D37 RID: 27959 RVA: 0x000A547D File Offset: 0x000A367D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047BF RID: 18367
			// (set) Token: 0x06006D38 RID: 27960 RVA: 0x000A5495 File Offset: 0x000A3695
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047C0 RID: 18368
			// (set) Token: 0x06006D39 RID: 27961 RVA: 0x000A54AD File Offset: 0x000A36AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000899 RID: 2201
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170047C1 RID: 18369
			// (set) Token: 0x06006D3B RID: 27963 RVA: 0x000A54CD File Offset: 0x000A36CD
			public virtual ReceiveConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170047C2 RID: 18370
			// (set) Token: 0x06006D3C RID: 27964 RVA: 0x000A54E0 File Offset: 0x000A36E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047C3 RID: 18371
			// (set) Token: 0x06006D3D RID: 27965 RVA: 0x000A54F3 File Offset: 0x000A36F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047C4 RID: 18372
			// (set) Token: 0x06006D3E RID: 27966 RVA: 0x000A550B File Offset: 0x000A370B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047C5 RID: 18373
			// (set) Token: 0x06006D3F RID: 27967 RVA: 0x000A5523 File Offset: 0x000A3723
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047C6 RID: 18374
			// (set) Token: 0x06006D40 RID: 27968 RVA: 0x000A553B File Offset: 0x000A373B
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

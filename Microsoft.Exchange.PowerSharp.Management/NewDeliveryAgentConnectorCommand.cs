using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200089D RID: 2205
	public class NewDeliveryAgentConnectorCommand : SyntheticCommandWithPipelineInput<DeliveryAgentConnector, DeliveryAgentConnector>
	{
		// Token: 0x06006D53 RID: 27987 RVA: 0x000A569F File Offset: 0x000A389F
		private NewDeliveryAgentConnectorCommand() : base("New-DeliveryAgentConnector")
		{
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x000A56AC File Offset: 0x000A38AC
		public NewDeliveryAgentConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x000A56BB File Offset: 0x000A38BB
		public virtual NewDeliveryAgentConnectorCommand SetParameters(NewDeliveryAgentConnectorCommand.AddressSpacesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x000A56C5 File Offset: 0x000A38C5
		public virtual NewDeliveryAgentConnectorCommand SetParameters(NewDeliveryAgentConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200089E RID: 2206
		public class AddressSpacesParameters : ParametersBase
		{
			// Token: 0x170047D2 RID: 18386
			// (set) Token: 0x06006D57 RID: 27991 RVA: 0x000A56CF File Offset: 0x000A38CF
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x170047D3 RID: 18387
			// (set) Token: 0x06006D58 RID: 27992 RVA: 0x000A56E2 File Offset: 0x000A38E2
			public virtual string DeliveryProtocol
			{
				set
				{
					base.PowerSharpParameters["DeliveryProtocol"] = value;
				}
			}

			// Token: 0x170047D4 RID: 18388
			// (set) Token: 0x06006D59 RID: 27993 RVA: 0x000A56F5 File Offset: 0x000A38F5
			public virtual int MaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["MaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x170047D5 RID: 18389
			// (set) Token: 0x06006D5A RID: 27994 RVA: 0x000A570D File Offset: 0x000A390D
			public virtual int MaxConcurrentConnections
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentConnections"] = value;
				}
			}

			// Token: 0x170047D6 RID: 18390
			// (set) Token: 0x06006D5B RID: 27995 RVA: 0x000A5725 File Offset: 0x000A3925
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170047D7 RID: 18391
			// (set) Token: 0x06006D5C RID: 27996 RVA: 0x000A573D File Offset: 0x000A393D
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x170047D8 RID: 18392
			// (set) Token: 0x06006D5D RID: 27997 RVA: 0x000A5755 File Offset: 0x000A3955
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x170047D9 RID: 18393
			// (set) Token: 0x06006D5E RID: 27998 RVA: 0x000A576D File Offset: 0x000A396D
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x170047DA RID: 18394
			// (set) Token: 0x06006D5F RID: 27999 RVA: 0x000A5780 File Offset: 0x000A3980
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170047DB RID: 18395
			// (set) Token: 0x06006D60 RID: 28000 RVA: 0x000A5793 File Offset: 0x000A3993
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170047DC RID: 18396
			// (set) Token: 0x06006D61 RID: 28001 RVA: 0x000A57A6 File Offset: 0x000A39A6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047DD RID: 18397
			// (set) Token: 0x06006D62 RID: 28002 RVA: 0x000A57B9 File Offset: 0x000A39B9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047DE RID: 18398
			// (set) Token: 0x06006D63 RID: 28003 RVA: 0x000A57D1 File Offset: 0x000A39D1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047DF RID: 18399
			// (set) Token: 0x06006D64 RID: 28004 RVA: 0x000A57E9 File Offset: 0x000A39E9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047E0 RID: 18400
			// (set) Token: 0x06006D65 RID: 28005 RVA: 0x000A5801 File Offset: 0x000A3A01
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170047E1 RID: 18401
			// (set) Token: 0x06006D66 RID: 28006 RVA: 0x000A5819 File Offset: 0x000A3A19
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200089F RID: 2207
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170047E2 RID: 18402
			// (set) Token: 0x06006D68 RID: 28008 RVA: 0x000A5839 File Offset: 0x000A3A39
			public virtual string DeliveryProtocol
			{
				set
				{
					base.PowerSharpParameters["DeliveryProtocol"] = value;
				}
			}

			// Token: 0x170047E3 RID: 18403
			// (set) Token: 0x06006D69 RID: 28009 RVA: 0x000A584C File Offset: 0x000A3A4C
			public virtual int MaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["MaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x170047E4 RID: 18404
			// (set) Token: 0x06006D6A RID: 28010 RVA: 0x000A5864 File Offset: 0x000A3A64
			public virtual int MaxConcurrentConnections
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentConnections"] = value;
				}
			}

			// Token: 0x170047E5 RID: 18405
			// (set) Token: 0x06006D6B RID: 28011 RVA: 0x000A587C File Offset: 0x000A3A7C
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170047E6 RID: 18406
			// (set) Token: 0x06006D6C RID: 28012 RVA: 0x000A5894 File Offset: 0x000A3A94
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x170047E7 RID: 18407
			// (set) Token: 0x06006D6D RID: 28013 RVA: 0x000A58AC File Offset: 0x000A3AAC
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x170047E8 RID: 18408
			// (set) Token: 0x06006D6E RID: 28014 RVA: 0x000A58C4 File Offset: 0x000A3AC4
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x170047E9 RID: 18409
			// (set) Token: 0x06006D6F RID: 28015 RVA: 0x000A58D7 File Offset: 0x000A3AD7
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170047EA RID: 18410
			// (set) Token: 0x06006D70 RID: 28016 RVA: 0x000A58EA File Offset: 0x000A3AEA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170047EB RID: 18411
			// (set) Token: 0x06006D71 RID: 28017 RVA: 0x000A58FD File Offset: 0x000A3AFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047EC RID: 18412
			// (set) Token: 0x06006D72 RID: 28018 RVA: 0x000A5910 File Offset: 0x000A3B10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047ED RID: 18413
			// (set) Token: 0x06006D73 RID: 28019 RVA: 0x000A5928 File Offset: 0x000A3B28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047EE RID: 18414
			// (set) Token: 0x06006D74 RID: 28020 RVA: 0x000A5940 File Offset: 0x000A3B40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047EF RID: 18415
			// (set) Token: 0x06006D75 RID: 28021 RVA: 0x000A5958 File Offset: 0x000A3B58
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170047F0 RID: 18416
			// (set) Token: 0x06006D76 RID: 28022 RVA: 0x000A5970 File Offset: 0x000A3B70
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

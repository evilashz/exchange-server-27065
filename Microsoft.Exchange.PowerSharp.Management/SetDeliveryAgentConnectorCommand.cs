using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008CC RID: 2252
	public class SetDeliveryAgentConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<DeliveryAgentConnector>
	{
		// Token: 0x06007082 RID: 28802 RVA: 0x000A9AFD File Offset: 0x000A7CFD
		private SetDeliveryAgentConnectorCommand() : base("Set-DeliveryAgentConnector")
		{
		}

		// Token: 0x06007083 RID: 28803 RVA: 0x000A9B0A File Offset: 0x000A7D0A
		public SetDeliveryAgentConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x000A9B19 File Offset: 0x000A7D19
		public virtual SetDeliveryAgentConnectorCommand SetParameters(SetDeliveryAgentConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x000A9B23 File Offset: 0x000A7D23
		public virtual SetDeliveryAgentConnectorCommand SetParameters(SetDeliveryAgentConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008CD RID: 2253
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004AA3 RID: 19107
			// (set) Token: 0x06007086 RID: 28806 RVA: 0x000A9B2D File Offset: 0x000A7D2D
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004AA4 RID: 19108
			// (set) Token: 0x06007087 RID: 28807 RVA: 0x000A9B40 File Offset: 0x000A7D40
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004AA5 RID: 19109
			// (set) Token: 0x06007088 RID: 28808 RVA: 0x000A9B58 File Offset: 0x000A7D58
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004AA6 RID: 19110
			// (set) Token: 0x06007089 RID: 28809 RVA: 0x000A9B6B File Offset: 0x000A7D6B
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004AA7 RID: 19111
			// (set) Token: 0x0600708A RID: 28810 RVA: 0x000A9B83 File Offset: 0x000A7D83
			public virtual string DeliveryProtocol
			{
				set
				{
					base.PowerSharpParameters["DeliveryProtocol"] = value;
				}
			}

			// Token: 0x17004AA8 RID: 19112
			// (set) Token: 0x0600708B RID: 28811 RVA: 0x000A9B96 File Offset: 0x000A7D96
			public virtual int MaxConcurrentConnections
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentConnections"] = value;
				}
			}

			// Token: 0x17004AA9 RID: 19113
			// (set) Token: 0x0600708C RID: 28812 RVA: 0x000A9BAE File Offset: 0x000A7DAE
			public virtual int MaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["MaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x17004AAA RID: 19114
			// (set) Token: 0x0600708D RID: 28813 RVA: 0x000A9BC6 File Offset: 0x000A7DC6
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x17004AAB RID: 19115
			// (set) Token: 0x0600708E RID: 28814 RVA: 0x000A9BD9 File Offset: 0x000A7DD9
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x17004AAC RID: 19116
			// (set) Token: 0x0600708F RID: 28815 RVA: 0x000A9BF1 File Offset: 0x000A7DF1
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004AAD RID: 19117
			// (set) Token: 0x06007090 RID: 28816 RVA: 0x000A9C04 File Offset: 0x000A7E04
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004AAE RID: 19118
			// (set) Token: 0x06007091 RID: 28817 RVA: 0x000A9C1C File Offset: 0x000A7E1C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004AAF RID: 19119
			// (set) Token: 0x06007092 RID: 28818 RVA: 0x000A9C2F File Offset: 0x000A7E2F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004AB0 RID: 19120
			// (set) Token: 0x06007093 RID: 28819 RVA: 0x000A9C47 File Offset: 0x000A7E47
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004AB1 RID: 19121
			// (set) Token: 0x06007094 RID: 28820 RVA: 0x000A9C5F File Offset: 0x000A7E5F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004AB2 RID: 19122
			// (set) Token: 0x06007095 RID: 28821 RVA: 0x000A9C77 File Offset: 0x000A7E77
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004AB3 RID: 19123
			// (set) Token: 0x06007096 RID: 28822 RVA: 0x000A9C8F File Offset: 0x000A7E8F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008CE RID: 2254
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004AB4 RID: 19124
			// (set) Token: 0x06007098 RID: 28824 RVA: 0x000A9CAF File Offset: 0x000A7EAF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DeliveryAgentConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004AB5 RID: 19125
			// (set) Token: 0x06007099 RID: 28825 RVA: 0x000A9CCD File Offset: 0x000A7ECD
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004AB6 RID: 19126
			// (set) Token: 0x0600709A RID: 28826 RVA: 0x000A9CE0 File Offset: 0x000A7EE0
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004AB7 RID: 19127
			// (set) Token: 0x0600709B RID: 28827 RVA: 0x000A9CF8 File Offset: 0x000A7EF8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004AB8 RID: 19128
			// (set) Token: 0x0600709C RID: 28828 RVA: 0x000A9D0B File Offset: 0x000A7F0B
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004AB9 RID: 19129
			// (set) Token: 0x0600709D RID: 28829 RVA: 0x000A9D23 File Offset: 0x000A7F23
			public virtual string DeliveryProtocol
			{
				set
				{
					base.PowerSharpParameters["DeliveryProtocol"] = value;
				}
			}

			// Token: 0x17004ABA RID: 19130
			// (set) Token: 0x0600709E RID: 28830 RVA: 0x000A9D36 File Offset: 0x000A7F36
			public virtual int MaxConcurrentConnections
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentConnections"] = value;
				}
			}

			// Token: 0x17004ABB RID: 19131
			// (set) Token: 0x0600709F RID: 28831 RVA: 0x000A9D4E File Offset: 0x000A7F4E
			public virtual int MaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["MaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x17004ABC RID: 19132
			// (set) Token: 0x060070A0 RID: 28832 RVA: 0x000A9D66 File Offset: 0x000A7F66
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x17004ABD RID: 19133
			// (set) Token: 0x060070A1 RID: 28833 RVA: 0x000A9D79 File Offset: 0x000A7F79
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x17004ABE RID: 19134
			// (set) Token: 0x060070A2 RID: 28834 RVA: 0x000A9D91 File Offset: 0x000A7F91
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004ABF RID: 19135
			// (set) Token: 0x060070A3 RID: 28835 RVA: 0x000A9DA4 File Offset: 0x000A7FA4
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004AC0 RID: 19136
			// (set) Token: 0x060070A4 RID: 28836 RVA: 0x000A9DBC File Offset: 0x000A7FBC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004AC1 RID: 19137
			// (set) Token: 0x060070A5 RID: 28837 RVA: 0x000A9DCF File Offset: 0x000A7FCF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004AC2 RID: 19138
			// (set) Token: 0x060070A6 RID: 28838 RVA: 0x000A9DE7 File Offset: 0x000A7FE7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004AC3 RID: 19139
			// (set) Token: 0x060070A7 RID: 28839 RVA: 0x000A9DFF File Offset: 0x000A7FFF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004AC4 RID: 19140
			// (set) Token: 0x060070A8 RID: 28840 RVA: 0x000A9E17 File Offset: 0x000A8017
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004AC5 RID: 19141
			// (set) Token: 0x060070A9 RID: 28841 RVA: 0x000A9E2F File Offset: 0x000A802F
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

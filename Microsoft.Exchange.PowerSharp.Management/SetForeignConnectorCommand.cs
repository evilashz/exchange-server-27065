using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008CF RID: 2255
	public class SetForeignConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<ForeignConnector>
	{
		// Token: 0x060070AB RID: 28843 RVA: 0x000A9E4F File Offset: 0x000A804F
		private SetForeignConnectorCommand() : base("Set-ForeignConnector")
		{
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x000A9E5C File Offset: 0x000A805C
		public SetForeignConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x000A9E6B File Offset: 0x000A806B
		public virtual SetForeignConnectorCommand SetParameters(SetForeignConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x000A9E75 File Offset: 0x000A8075
		public virtual SetForeignConnectorCommand SetParameters(SetForeignConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008D0 RID: 2256
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004AC6 RID: 19142
			// (set) Token: 0x060070AF RID: 28847 RVA: 0x000A9E7F File Offset: 0x000A807F
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004AC7 RID: 19143
			// (set) Token: 0x060070B0 RID: 28848 RVA: 0x000A9E92 File Offset: 0x000A8092
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004AC8 RID: 19144
			// (set) Token: 0x060070B1 RID: 28849 RVA: 0x000A9EAA File Offset: 0x000A80AA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004AC9 RID: 19145
			// (set) Token: 0x060070B2 RID: 28850 RVA: 0x000A9EBD File Offset: 0x000A80BD
			public virtual string DropDirectory
			{
				set
				{
					base.PowerSharpParameters["DropDirectory"] = value;
				}
			}

			// Token: 0x17004ACA RID: 19146
			// (set) Token: 0x060070B3 RID: 28851 RVA: 0x000A9ED0 File Offset: 0x000A80D0
			public virtual Unlimited<ByteQuantifiedSize> DropDirectoryQuota
			{
				set
				{
					base.PowerSharpParameters["DropDirectoryQuota"] = value;
				}
			}

			// Token: 0x17004ACB RID: 19147
			// (set) Token: 0x060070B4 RID: 28852 RVA: 0x000A9EE8 File Offset: 0x000A80E8
			public virtual bool RelayDsnRequired
			{
				set
				{
					base.PowerSharpParameters["RelayDsnRequired"] = value;
				}
			}

			// Token: 0x17004ACC RID: 19148
			// (set) Token: 0x060070B5 RID: 28853 RVA: 0x000A9F00 File Offset: 0x000A8100
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004ACD RID: 19149
			// (set) Token: 0x060070B6 RID: 28854 RVA: 0x000A9F18 File Offset: 0x000A8118
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x17004ACE RID: 19150
			// (set) Token: 0x060070B7 RID: 28855 RVA: 0x000A9F2B File Offset: 0x000A812B
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x17004ACF RID: 19151
			// (set) Token: 0x060070B8 RID: 28856 RVA: 0x000A9F43 File Offset: 0x000A8143
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004AD0 RID: 19152
			// (set) Token: 0x060070B9 RID: 28857 RVA: 0x000A9F56 File Offset: 0x000A8156
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004AD1 RID: 19153
			// (set) Token: 0x060070BA RID: 28858 RVA: 0x000A9F6E File Offset: 0x000A816E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004AD2 RID: 19154
			// (set) Token: 0x060070BB RID: 28859 RVA: 0x000A9F81 File Offset: 0x000A8181
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004AD3 RID: 19155
			// (set) Token: 0x060070BC RID: 28860 RVA: 0x000A9F99 File Offset: 0x000A8199
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004AD4 RID: 19156
			// (set) Token: 0x060070BD RID: 28861 RVA: 0x000A9FB1 File Offset: 0x000A81B1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004AD5 RID: 19157
			// (set) Token: 0x060070BE RID: 28862 RVA: 0x000A9FC9 File Offset: 0x000A81C9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004AD6 RID: 19158
			// (set) Token: 0x060070BF RID: 28863 RVA: 0x000A9FE1 File Offset: 0x000A81E1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008D1 RID: 2257
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004AD7 RID: 19159
			// (set) Token: 0x060070C1 RID: 28865 RVA: 0x000AA001 File Offset: 0x000A8201
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ForeignConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004AD8 RID: 19160
			// (set) Token: 0x060070C2 RID: 28866 RVA: 0x000AA01F File Offset: 0x000A821F
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004AD9 RID: 19161
			// (set) Token: 0x060070C3 RID: 28867 RVA: 0x000AA032 File Offset: 0x000A8232
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004ADA RID: 19162
			// (set) Token: 0x060070C4 RID: 28868 RVA: 0x000AA04A File Offset: 0x000A824A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004ADB RID: 19163
			// (set) Token: 0x060070C5 RID: 28869 RVA: 0x000AA05D File Offset: 0x000A825D
			public virtual string DropDirectory
			{
				set
				{
					base.PowerSharpParameters["DropDirectory"] = value;
				}
			}

			// Token: 0x17004ADC RID: 19164
			// (set) Token: 0x060070C6 RID: 28870 RVA: 0x000AA070 File Offset: 0x000A8270
			public virtual Unlimited<ByteQuantifiedSize> DropDirectoryQuota
			{
				set
				{
					base.PowerSharpParameters["DropDirectoryQuota"] = value;
				}
			}

			// Token: 0x17004ADD RID: 19165
			// (set) Token: 0x060070C7 RID: 28871 RVA: 0x000AA088 File Offset: 0x000A8288
			public virtual bool RelayDsnRequired
			{
				set
				{
					base.PowerSharpParameters["RelayDsnRequired"] = value;
				}
			}

			// Token: 0x17004ADE RID: 19166
			// (set) Token: 0x060070C8 RID: 28872 RVA: 0x000AA0A0 File Offset: 0x000A82A0
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004ADF RID: 19167
			// (set) Token: 0x060070C9 RID: 28873 RVA: 0x000AA0B8 File Offset: 0x000A82B8
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x17004AE0 RID: 19168
			// (set) Token: 0x060070CA RID: 28874 RVA: 0x000AA0CB File Offset: 0x000A82CB
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x17004AE1 RID: 19169
			// (set) Token: 0x060070CB RID: 28875 RVA: 0x000AA0E3 File Offset: 0x000A82E3
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004AE2 RID: 19170
			// (set) Token: 0x060070CC RID: 28876 RVA: 0x000AA0F6 File Offset: 0x000A82F6
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004AE3 RID: 19171
			// (set) Token: 0x060070CD RID: 28877 RVA: 0x000AA10E File Offset: 0x000A830E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004AE4 RID: 19172
			// (set) Token: 0x060070CE RID: 28878 RVA: 0x000AA121 File Offset: 0x000A8321
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004AE5 RID: 19173
			// (set) Token: 0x060070CF RID: 28879 RVA: 0x000AA139 File Offset: 0x000A8339
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004AE6 RID: 19174
			// (set) Token: 0x060070D0 RID: 28880 RVA: 0x000AA151 File Offset: 0x000A8351
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004AE7 RID: 19175
			// (set) Token: 0x060070D1 RID: 28881 RVA: 0x000AA169 File Offset: 0x000A8369
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004AE8 RID: 19176
			// (set) Token: 0x060070D2 RID: 28882 RVA: 0x000AA181 File Offset: 0x000A8381
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

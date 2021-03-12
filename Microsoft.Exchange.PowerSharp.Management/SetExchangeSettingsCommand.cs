using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005A1 RID: 1441
	public class SetExchangeSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeSettings>
	{
		// Token: 0x06004B22 RID: 19234 RVA: 0x00078C41 File Offset: 0x00076E41
		private SetExchangeSettingsCommand() : base("Set-ExchangeSettings")
		{
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x00078C4E File Offset: 0x00076E4E
		public SetExchangeSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x00078C5D File Offset: 0x00076E5D
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.CreateSettingsGroupGenericParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x00078C67 File Offset: 0x00076E67
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.CreateSettingsGroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00078C71 File Offset: 0x00076E71
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.CreateSettingsGroupAdvancedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x00078C7B File Offset: 0x00076E7B
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.UpdateMultipleSettingsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x00078C85 File Offset: 0x00076E85
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.UpdateSettingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00078C8F File Offset: 0x00076E8F
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.UpdateSettingsGroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x00078C99 File Offset: 0x00076E99
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.UpdateSettingsGroupAdvancedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00078CA3 File Offset: 0x00076EA3
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.RemoveMultipleSettingsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00078CAD File Offset: 0x00076EAD
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.RemoveSettingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00078CB7 File Offset: 0x00076EB7
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.RemoveSettingsGroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00078CC1 File Offset: 0x00076EC1
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.AddScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x00078CCB File Offset: 0x00076ECB
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.UpdateScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x00078CD5 File Offset: 0x00076ED5
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.RemoveScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00078CDF File Offset: 0x00076EDF
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.ClearHistoryGroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00078CE9 File Offset: 0x00076EE9
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x00078CF3 File Offset: 0x00076EF3
		public virtual SetExchangeSettingsCommand SetParameters(SetExchangeSettingsCommand.EnableSettingsGroupParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005A2 RID: 1442
		public class CreateSettingsGroupGenericParameters : ParametersBase
		{
			// Token: 0x17002B99 RID: 11161
			// (set) Token: 0x06004B34 RID: 19252 RVA: 0x00078CFD File Offset: 0x00076EFD
			public virtual SwitchParameter CreateSettingsGroup
			{
				set
				{
					base.PowerSharpParameters["CreateSettingsGroup"] = value;
				}
			}

			// Token: 0x17002B9A RID: 11162
			// (set) Token: 0x06004B35 RID: 19253 RVA: 0x00078D15 File Offset: 0x00076F15
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002B9B RID: 11163
			// (set) Token: 0x06004B36 RID: 19254 RVA: 0x00078D28 File Offset: 0x00076F28
			public virtual ExchangeSettingsScope Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x17002B9C RID: 11164
			// (set) Token: 0x06004B37 RID: 19255 RVA: 0x00078D40 File Offset: 0x00076F40
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17002B9D RID: 11165
			// (set) Token: 0x06004B38 RID: 19256 RVA: 0x00078D58 File Offset: 0x00076F58
			public virtual DateTime? ExpirationDate
			{
				set
				{
					base.PowerSharpParameters["ExpirationDate"] = value;
				}
			}

			// Token: 0x17002B9E RID: 11166
			// (set) Token: 0x06004B39 RID: 19257 RVA: 0x00078D70 File Offset: 0x00076F70
			public virtual string GenericScopeName
			{
				set
				{
					base.PowerSharpParameters["GenericScopeName"] = value;
				}
			}

			// Token: 0x17002B9F RID: 11167
			// (set) Token: 0x06004B3A RID: 19258 RVA: 0x00078D83 File Offset: 0x00076F83
			public virtual string GenericScopeValue
			{
				set
				{
					base.PowerSharpParameters["GenericScopeValue"] = value;
				}
			}

			// Token: 0x17002BA0 RID: 11168
			// (set) Token: 0x06004B3B RID: 19259 RVA: 0x00078D96 File Offset: 0x00076F96
			public virtual SwitchParameter Disable
			{
				set
				{
					base.PowerSharpParameters["Disable"] = value;
				}
			}

			// Token: 0x17002BA1 RID: 11169
			// (set) Token: 0x06004B3C RID: 19260 RVA: 0x00078DAE File Offset: 0x00076FAE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BA2 RID: 11170
			// (set) Token: 0x06004B3D RID: 19261 RVA: 0x00078DCC File Offset: 0x00076FCC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002BA3 RID: 11171
			// (set) Token: 0x06004B3E RID: 19262 RVA: 0x00078DE4 File Offset: 0x00076FE4
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002BA4 RID: 11172
			// (set) Token: 0x06004B3F RID: 19263 RVA: 0x00078DF7 File Offset: 0x00076FF7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002BA5 RID: 11173
			// (set) Token: 0x06004B40 RID: 19264 RVA: 0x00078E0A File Offset: 0x0007700A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002BA6 RID: 11174
			// (set) Token: 0x06004B41 RID: 19265 RVA: 0x00078E1D File Offset: 0x0007701D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002BA7 RID: 11175
			// (set) Token: 0x06004B42 RID: 19266 RVA: 0x00078E35 File Offset: 0x00077035
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002BA8 RID: 11176
			// (set) Token: 0x06004B43 RID: 19267 RVA: 0x00078E4D File Offset: 0x0007704D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002BA9 RID: 11177
			// (set) Token: 0x06004B44 RID: 19268 RVA: 0x00078E65 File Offset: 0x00077065
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002BAA RID: 11178
			// (set) Token: 0x06004B45 RID: 19269 RVA: 0x00078E7D File Offset: 0x0007707D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002BAB RID: 11179
			// (set) Token: 0x06004B46 RID: 19270 RVA: 0x00078E95 File Offset: 0x00077095
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A3 RID: 1443
		public class CreateSettingsGroupParameters : ParametersBase
		{
			// Token: 0x17002BAC RID: 11180
			// (set) Token: 0x06004B48 RID: 19272 RVA: 0x00078EB5 File Offset: 0x000770B5
			public virtual SwitchParameter CreateSettingsGroup
			{
				set
				{
					base.PowerSharpParameters["CreateSettingsGroup"] = value;
				}
			}

			// Token: 0x17002BAD RID: 11181
			// (set) Token: 0x06004B49 RID: 19273 RVA: 0x00078ECD File Offset: 0x000770CD
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002BAE RID: 11182
			// (set) Token: 0x06004B4A RID: 19274 RVA: 0x00078EE0 File Offset: 0x000770E0
			public virtual ExchangeSettingsScope Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x17002BAF RID: 11183
			// (set) Token: 0x06004B4B RID: 19275 RVA: 0x00078EF8 File Offset: 0x000770F8
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17002BB0 RID: 11184
			// (set) Token: 0x06004B4C RID: 19276 RVA: 0x00078F10 File Offset: 0x00077110
			public virtual DateTime? ExpirationDate
			{
				set
				{
					base.PowerSharpParameters["ExpirationDate"] = value;
				}
			}

			// Token: 0x17002BB1 RID: 11185
			// (set) Token: 0x06004B4D RID: 19277 RVA: 0x00078F28 File Offset: 0x00077128
			public virtual string MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002BB2 RID: 11186
			// (set) Token: 0x06004B4E RID: 19278 RVA: 0x00078F3B File Offset: 0x0007713B
			public virtual string MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002BB3 RID: 11187
			// (set) Token: 0x06004B4F RID: 19279 RVA: 0x00078F4E File Offset: 0x0007714E
			public virtual string NameMatch
			{
				set
				{
					base.PowerSharpParameters["NameMatch"] = value;
				}
			}

			// Token: 0x17002BB4 RID: 11188
			// (set) Token: 0x06004B50 RID: 19280 RVA: 0x00078F61 File Offset: 0x00077161
			public virtual Guid? GuidMatch
			{
				set
				{
					base.PowerSharpParameters["GuidMatch"] = value;
				}
			}

			// Token: 0x17002BB5 RID: 11189
			// (set) Token: 0x06004B51 RID: 19281 RVA: 0x00078F79 File Offset: 0x00077179
			public virtual string ScopeFilter
			{
				set
				{
					base.PowerSharpParameters["ScopeFilter"] = value;
				}
			}

			// Token: 0x17002BB6 RID: 11190
			// (set) Token: 0x06004B52 RID: 19282 RVA: 0x00078F8C File Offset: 0x0007718C
			public virtual SwitchParameter Disable
			{
				set
				{
					base.PowerSharpParameters["Disable"] = value;
				}
			}

			// Token: 0x17002BB7 RID: 11191
			// (set) Token: 0x06004B53 RID: 19283 RVA: 0x00078FA4 File Offset: 0x000771A4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BB8 RID: 11192
			// (set) Token: 0x06004B54 RID: 19284 RVA: 0x00078FC2 File Offset: 0x000771C2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002BB9 RID: 11193
			// (set) Token: 0x06004B55 RID: 19285 RVA: 0x00078FDA File Offset: 0x000771DA
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002BBA RID: 11194
			// (set) Token: 0x06004B56 RID: 19286 RVA: 0x00078FED File Offset: 0x000771ED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002BBB RID: 11195
			// (set) Token: 0x06004B57 RID: 19287 RVA: 0x00079000 File Offset: 0x00077200
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002BBC RID: 11196
			// (set) Token: 0x06004B58 RID: 19288 RVA: 0x00079013 File Offset: 0x00077213
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002BBD RID: 11197
			// (set) Token: 0x06004B59 RID: 19289 RVA: 0x0007902B File Offset: 0x0007722B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002BBE RID: 11198
			// (set) Token: 0x06004B5A RID: 19290 RVA: 0x00079043 File Offset: 0x00077243
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002BBF RID: 11199
			// (set) Token: 0x06004B5B RID: 19291 RVA: 0x0007905B File Offset: 0x0007725B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002BC0 RID: 11200
			// (set) Token: 0x06004B5C RID: 19292 RVA: 0x00079073 File Offset: 0x00077273
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002BC1 RID: 11201
			// (set) Token: 0x06004B5D RID: 19293 RVA: 0x0007908B File Offset: 0x0007728B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A4 RID: 1444
		public class CreateSettingsGroupAdvancedParameters : ParametersBase
		{
			// Token: 0x17002BC2 RID: 11202
			// (set) Token: 0x06004B5F RID: 19295 RVA: 0x000790AB File Offset: 0x000772AB
			public virtual SwitchParameter CreateSettingsGroup
			{
				set
				{
					base.PowerSharpParameters["CreateSettingsGroup"] = value;
				}
			}

			// Token: 0x17002BC3 RID: 11203
			// (set) Token: 0x06004B60 RID: 19296 RVA: 0x000790C3 File Offset: 0x000772C3
			public virtual string SettingsGroup
			{
				set
				{
					base.PowerSharpParameters["SettingsGroup"] = value;
				}
			}

			// Token: 0x17002BC4 RID: 11204
			// (set) Token: 0x06004B61 RID: 19297 RVA: 0x000790D6 File Offset: 0x000772D6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BC5 RID: 11205
			// (set) Token: 0x06004B62 RID: 19298 RVA: 0x000790F4 File Offset: 0x000772F4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002BC6 RID: 11206
			// (set) Token: 0x06004B63 RID: 19299 RVA: 0x0007910C File Offset: 0x0007730C
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002BC7 RID: 11207
			// (set) Token: 0x06004B64 RID: 19300 RVA: 0x0007911F File Offset: 0x0007731F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002BC8 RID: 11208
			// (set) Token: 0x06004B65 RID: 19301 RVA: 0x00079132 File Offset: 0x00077332
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002BC9 RID: 11209
			// (set) Token: 0x06004B66 RID: 19302 RVA: 0x00079145 File Offset: 0x00077345
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002BCA RID: 11210
			// (set) Token: 0x06004B67 RID: 19303 RVA: 0x0007915D File Offset: 0x0007735D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002BCB RID: 11211
			// (set) Token: 0x06004B68 RID: 19304 RVA: 0x00079175 File Offset: 0x00077375
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002BCC RID: 11212
			// (set) Token: 0x06004B69 RID: 19305 RVA: 0x0007918D File Offset: 0x0007738D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002BCD RID: 11213
			// (set) Token: 0x06004B6A RID: 19306 RVA: 0x000791A5 File Offset: 0x000773A5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002BCE RID: 11214
			// (set) Token: 0x06004B6B RID: 19307 RVA: 0x000791BD File Offset: 0x000773BD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A5 RID: 1445
		public class UpdateMultipleSettingsParameters : ParametersBase
		{
			// Token: 0x17002BCF RID: 11215
			// (set) Token: 0x06004B6D RID: 19309 RVA: 0x000791DD File Offset: 0x000773DD
			public virtual SwitchParameter UpdateSetting
			{
				set
				{
					base.PowerSharpParameters["UpdateSetting"] = value;
				}
			}

			// Token: 0x17002BD0 RID: 11216
			// (set) Token: 0x06004B6E RID: 19310 RVA: 0x000791F5 File Offset: 0x000773F5
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002BD1 RID: 11217
			// (set) Token: 0x06004B6F RID: 19311 RVA: 0x00079208 File Offset: 0x00077408
			public virtual string ConfigPairs
			{
				set
				{
					base.PowerSharpParameters["ConfigPairs"] = value;
				}
			}

			// Token: 0x17002BD2 RID: 11218
			// (set) Token: 0x06004B70 RID: 19312 RVA: 0x0007921B File Offset: 0x0007741B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BD3 RID: 11219
			// (set) Token: 0x06004B71 RID: 19313 RVA: 0x00079239 File Offset: 0x00077439
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002BD4 RID: 11220
			// (set) Token: 0x06004B72 RID: 19314 RVA: 0x00079251 File Offset: 0x00077451
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002BD5 RID: 11221
			// (set) Token: 0x06004B73 RID: 19315 RVA: 0x00079264 File Offset: 0x00077464
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002BD6 RID: 11222
			// (set) Token: 0x06004B74 RID: 19316 RVA: 0x00079277 File Offset: 0x00077477
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002BD7 RID: 11223
			// (set) Token: 0x06004B75 RID: 19317 RVA: 0x0007928A File Offset: 0x0007748A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002BD8 RID: 11224
			// (set) Token: 0x06004B76 RID: 19318 RVA: 0x000792A2 File Offset: 0x000774A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002BD9 RID: 11225
			// (set) Token: 0x06004B77 RID: 19319 RVA: 0x000792BA File Offset: 0x000774BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002BDA RID: 11226
			// (set) Token: 0x06004B78 RID: 19320 RVA: 0x000792D2 File Offset: 0x000774D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002BDB RID: 11227
			// (set) Token: 0x06004B79 RID: 19321 RVA: 0x000792EA File Offset: 0x000774EA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002BDC RID: 11228
			// (set) Token: 0x06004B7A RID: 19322 RVA: 0x00079302 File Offset: 0x00077502
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A6 RID: 1446
		public class UpdateSettingParameters : ParametersBase
		{
			// Token: 0x17002BDD RID: 11229
			// (set) Token: 0x06004B7C RID: 19324 RVA: 0x00079322 File Offset: 0x00077522
			public virtual SwitchParameter UpdateSetting
			{
				set
				{
					base.PowerSharpParameters["UpdateSetting"] = value;
				}
			}

			// Token: 0x17002BDE RID: 11230
			// (set) Token: 0x06004B7D RID: 19325 RVA: 0x0007933A File Offset: 0x0007753A
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002BDF RID: 11231
			// (set) Token: 0x06004B7E RID: 19326 RVA: 0x0007934D File Offset: 0x0007754D
			public virtual string ConfigName
			{
				set
				{
					base.PowerSharpParameters["ConfigName"] = value;
				}
			}

			// Token: 0x17002BE0 RID: 11232
			// (set) Token: 0x06004B7F RID: 19327 RVA: 0x00079360 File Offset: 0x00077560
			public virtual string ConfigValue
			{
				set
				{
					base.PowerSharpParameters["ConfigValue"] = value;
				}
			}

			// Token: 0x17002BE1 RID: 11233
			// (set) Token: 0x06004B80 RID: 19328 RVA: 0x00079373 File Offset: 0x00077573
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BE2 RID: 11234
			// (set) Token: 0x06004B81 RID: 19329 RVA: 0x00079391 File Offset: 0x00077591
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002BE3 RID: 11235
			// (set) Token: 0x06004B82 RID: 19330 RVA: 0x000793A9 File Offset: 0x000775A9
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002BE4 RID: 11236
			// (set) Token: 0x06004B83 RID: 19331 RVA: 0x000793BC File Offset: 0x000775BC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002BE5 RID: 11237
			// (set) Token: 0x06004B84 RID: 19332 RVA: 0x000793CF File Offset: 0x000775CF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002BE6 RID: 11238
			// (set) Token: 0x06004B85 RID: 19333 RVA: 0x000793E2 File Offset: 0x000775E2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002BE7 RID: 11239
			// (set) Token: 0x06004B86 RID: 19334 RVA: 0x000793FA File Offset: 0x000775FA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002BE8 RID: 11240
			// (set) Token: 0x06004B87 RID: 19335 RVA: 0x00079412 File Offset: 0x00077612
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002BE9 RID: 11241
			// (set) Token: 0x06004B88 RID: 19336 RVA: 0x0007942A File Offset: 0x0007762A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002BEA RID: 11242
			// (set) Token: 0x06004B89 RID: 19337 RVA: 0x00079442 File Offset: 0x00077642
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002BEB RID: 11243
			// (set) Token: 0x06004B8A RID: 19338 RVA: 0x0007945A File Offset: 0x0007765A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A7 RID: 1447
		public class UpdateSettingsGroupParameters : ParametersBase
		{
			// Token: 0x17002BEC RID: 11244
			// (set) Token: 0x06004B8C RID: 19340 RVA: 0x0007947A File Offset: 0x0007767A
			public virtual SwitchParameter UpdateSettingsGroup
			{
				set
				{
					base.PowerSharpParameters["UpdateSettingsGroup"] = value;
				}
			}

			// Token: 0x17002BED RID: 11245
			// (set) Token: 0x06004B8D RID: 19341 RVA: 0x00079492 File Offset: 0x00077692
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002BEE RID: 11246
			// (set) Token: 0x06004B8E RID: 19342 RVA: 0x000794A5 File Offset: 0x000776A5
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17002BEF RID: 11247
			// (set) Token: 0x06004B8F RID: 19343 RVA: 0x000794BD File Offset: 0x000776BD
			public virtual DateTime? ExpirationDate
			{
				set
				{
					base.PowerSharpParameters["ExpirationDate"] = value;
				}
			}

			// Token: 0x17002BF0 RID: 11248
			// (set) Token: 0x06004B90 RID: 19344 RVA: 0x000794D5 File Offset: 0x000776D5
			public virtual string ScopeFilter
			{
				set
				{
					base.PowerSharpParameters["ScopeFilter"] = value;
				}
			}

			// Token: 0x17002BF1 RID: 11249
			// (set) Token: 0x06004B91 RID: 19345 RVA: 0x000794E8 File Offset: 0x000776E8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BF2 RID: 11250
			// (set) Token: 0x06004B92 RID: 19346 RVA: 0x00079506 File Offset: 0x00077706
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002BF3 RID: 11251
			// (set) Token: 0x06004B93 RID: 19347 RVA: 0x0007951E File Offset: 0x0007771E
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002BF4 RID: 11252
			// (set) Token: 0x06004B94 RID: 19348 RVA: 0x00079531 File Offset: 0x00077731
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002BF5 RID: 11253
			// (set) Token: 0x06004B95 RID: 19349 RVA: 0x00079544 File Offset: 0x00077744
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002BF6 RID: 11254
			// (set) Token: 0x06004B96 RID: 19350 RVA: 0x00079557 File Offset: 0x00077757
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002BF7 RID: 11255
			// (set) Token: 0x06004B97 RID: 19351 RVA: 0x0007956F File Offset: 0x0007776F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002BF8 RID: 11256
			// (set) Token: 0x06004B98 RID: 19352 RVA: 0x00079587 File Offset: 0x00077787
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002BF9 RID: 11257
			// (set) Token: 0x06004B99 RID: 19353 RVA: 0x0007959F File Offset: 0x0007779F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002BFA RID: 11258
			// (set) Token: 0x06004B9A RID: 19354 RVA: 0x000795B7 File Offset: 0x000777B7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002BFB RID: 11259
			// (set) Token: 0x06004B9B RID: 19355 RVA: 0x000795CF File Offset: 0x000777CF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A8 RID: 1448
		public class UpdateSettingsGroupAdvancedParameters : ParametersBase
		{
			// Token: 0x17002BFC RID: 11260
			// (set) Token: 0x06004B9D RID: 19357 RVA: 0x000795EF File Offset: 0x000777EF
			public virtual SwitchParameter UpdateSettingsGroup
			{
				set
				{
					base.PowerSharpParameters["UpdateSettingsGroup"] = value;
				}
			}

			// Token: 0x17002BFD RID: 11261
			// (set) Token: 0x06004B9E RID: 19358 RVA: 0x00079607 File Offset: 0x00077807
			public virtual string SettingsGroup
			{
				set
				{
					base.PowerSharpParameters["SettingsGroup"] = value;
				}
			}

			// Token: 0x17002BFE RID: 11262
			// (set) Token: 0x06004B9F RID: 19359 RVA: 0x0007961A File Offset: 0x0007781A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002BFF RID: 11263
			// (set) Token: 0x06004BA0 RID: 19360 RVA: 0x00079638 File Offset: 0x00077838
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C00 RID: 11264
			// (set) Token: 0x06004BA1 RID: 19361 RVA: 0x00079650 File Offset: 0x00077850
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C01 RID: 11265
			// (set) Token: 0x06004BA2 RID: 19362 RVA: 0x00079663 File Offset: 0x00077863
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C02 RID: 11266
			// (set) Token: 0x06004BA3 RID: 19363 RVA: 0x00079676 File Offset: 0x00077876
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C03 RID: 11267
			// (set) Token: 0x06004BA4 RID: 19364 RVA: 0x00079689 File Offset: 0x00077889
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C04 RID: 11268
			// (set) Token: 0x06004BA5 RID: 19365 RVA: 0x000796A1 File Offset: 0x000778A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C05 RID: 11269
			// (set) Token: 0x06004BA6 RID: 19366 RVA: 0x000796B9 File Offset: 0x000778B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C06 RID: 11270
			// (set) Token: 0x06004BA7 RID: 19367 RVA: 0x000796D1 File Offset: 0x000778D1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C07 RID: 11271
			// (set) Token: 0x06004BA8 RID: 19368 RVA: 0x000796E9 File Offset: 0x000778E9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C08 RID: 11272
			// (set) Token: 0x06004BA9 RID: 19369 RVA: 0x00079701 File Offset: 0x00077901
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A9 RID: 1449
		public class RemoveMultipleSettingsParameters : ParametersBase
		{
			// Token: 0x17002C09 RID: 11273
			// (set) Token: 0x06004BAB RID: 19371 RVA: 0x00079721 File Offset: 0x00077921
			public virtual SwitchParameter RemoveSetting
			{
				set
				{
					base.PowerSharpParameters["RemoveSetting"] = value;
				}
			}

			// Token: 0x17002C0A RID: 11274
			// (set) Token: 0x06004BAC RID: 19372 RVA: 0x00079739 File Offset: 0x00077939
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C0B RID: 11275
			// (set) Token: 0x06004BAD RID: 19373 RVA: 0x0007974C File Offset: 0x0007794C
			public virtual string ConfigPairs
			{
				set
				{
					base.PowerSharpParameters["ConfigPairs"] = value;
				}
			}

			// Token: 0x17002C0C RID: 11276
			// (set) Token: 0x06004BAE RID: 19374 RVA: 0x0007975F File Offset: 0x0007795F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C0D RID: 11277
			// (set) Token: 0x06004BAF RID: 19375 RVA: 0x0007977D File Offset: 0x0007797D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C0E RID: 11278
			// (set) Token: 0x06004BB0 RID: 19376 RVA: 0x00079795 File Offset: 0x00077995
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C0F RID: 11279
			// (set) Token: 0x06004BB1 RID: 19377 RVA: 0x000797A8 File Offset: 0x000779A8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C10 RID: 11280
			// (set) Token: 0x06004BB2 RID: 19378 RVA: 0x000797BB File Offset: 0x000779BB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C11 RID: 11281
			// (set) Token: 0x06004BB3 RID: 19379 RVA: 0x000797CE File Offset: 0x000779CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C12 RID: 11282
			// (set) Token: 0x06004BB4 RID: 19380 RVA: 0x000797E6 File Offset: 0x000779E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C13 RID: 11283
			// (set) Token: 0x06004BB5 RID: 19381 RVA: 0x000797FE File Offset: 0x000779FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C14 RID: 11284
			// (set) Token: 0x06004BB6 RID: 19382 RVA: 0x00079816 File Offset: 0x00077A16
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C15 RID: 11285
			// (set) Token: 0x06004BB7 RID: 19383 RVA: 0x0007982E File Offset: 0x00077A2E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C16 RID: 11286
			// (set) Token: 0x06004BB8 RID: 19384 RVA: 0x00079846 File Offset: 0x00077A46
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005AA RID: 1450
		public class RemoveSettingParameters : ParametersBase
		{
			// Token: 0x17002C17 RID: 11287
			// (set) Token: 0x06004BBA RID: 19386 RVA: 0x00079866 File Offset: 0x00077A66
			public virtual SwitchParameter RemoveSetting
			{
				set
				{
					base.PowerSharpParameters["RemoveSetting"] = value;
				}
			}

			// Token: 0x17002C18 RID: 11288
			// (set) Token: 0x06004BBB RID: 19387 RVA: 0x0007987E File Offset: 0x00077A7E
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C19 RID: 11289
			// (set) Token: 0x06004BBC RID: 19388 RVA: 0x00079891 File Offset: 0x00077A91
			public virtual string ConfigName
			{
				set
				{
					base.PowerSharpParameters["ConfigName"] = value;
				}
			}

			// Token: 0x17002C1A RID: 11290
			// (set) Token: 0x06004BBD RID: 19389 RVA: 0x000798A4 File Offset: 0x00077AA4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C1B RID: 11291
			// (set) Token: 0x06004BBE RID: 19390 RVA: 0x000798C2 File Offset: 0x00077AC2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C1C RID: 11292
			// (set) Token: 0x06004BBF RID: 19391 RVA: 0x000798DA File Offset: 0x00077ADA
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C1D RID: 11293
			// (set) Token: 0x06004BC0 RID: 19392 RVA: 0x000798ED File Offset: 0x00077AED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C1E RID: 11294
			// (set) Token: 0x06004BC1 RID: 19393 RVA: 0x00079900 File Offset: 0x00077B00
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C1F RID: 11295
			// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x00079913 File Offset: 0x00077B13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C20 RID: 11296
			// (set) Token: 0x06004BC3 RID: 19395 RVA: 0x0007992B File Offset: 0x00077B2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C21 RID: 11297
			// (set) Token: 0x06004BC4 RID: 19396 RVA: 0x00079943 File Offset: 0x00077B43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C22 RID: 11298
			// (set) Token: 0x06004BC5 RID: 19397 RVA: 0x0007995B File Offset: 0x00077B5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C23 RID: 11299
			// (set) Token: 0x06004BC6 RID: 19398 RVA: 0x00079973 File Offset: 0x00077B73
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C24 RID: 11300
			// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x0007998B File Offset: 0x00077B8B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005AB RID: 1451
		public class RemoveSettingsGroupParameters : ParametersBase
		{
			// Token: 0x17002C25 RID: 11301
			// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x000799AB File Offset: 0x00077BAB
			public virtual SwitchParameter RemoveSettingsGroup
			{
				set
				{
					base.PowerSharpParameters["RemoveSettingsGroup"] = value;
				}
			}

			// Token: 0x17002C26 RID: 11302
			// (set) Token: 0x06004BCA RID: 19402 RVA: 0x000799C3 File Offset: 0x00077BC3
			public virtual SwitchParameter ClearHistory
			{
				set
				{
					base.PowerSharpParameters["ClearHistory"] = value;
				}
			}

			// Token: 0x17002C27 RID: 11303
			// (set) Token: 0x06004BCB RID: 19403 RVA: 0x000799DB File Offset: 0x00077BDB
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C28 RID: 11304
			// (set) Token: 0x06004BCC RID: 19404 RVA: 0x000799EE File Offset: 0x00077BEE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C29 RID: 11305
			// (set) Token: 0x06004BCD RID: 19405 RVA: 0x00079A0C File Offset: 0x00077C0C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C2A RID: 11306
			// (set) Token: 0x06004BCE RID: 19406 RVA: 0x00079A24 File Offset: 0x00077C24
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C2B RID: 11307
			// (set) Token: 0x06004BCF RID: 19407 RVA: 0x00079A37 File Offset: 0x00077C37
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C2C RID: 11308
			// (set) Token: 0x06004BD0 RID: 19408 RVA: 0x00079A4A File Offset: 0x00077C4A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C2D RID: 11309
			// (set) Token: 0x06004BD1 RID: 19409 RVA: 0x00079A5D File Offset: 0x00077C5D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C2E RID: 11310
			// (set) Token: 0x06004BD2 RID: 19410 RVA: 0x00079A75 File Offset: 0x00077C75
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C2F RID: 11311
			// (set) Token: 0x06004BD3 RID: 19411 RVA: 0x00079A8D File Offset: 0x00077C8D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C30 RID: 11312
			// (set) Token: 0x06004BD4 RID: 19412 RVA: 0x00079AA5 File Offset: 0x00077CA5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C31 RID: 11313
			// (set) Token: 0x06004BD5 RID: 19413 RVA: 0x00079ABD File Offset: 0x00077CBD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C32 RID: 11314
			// (set) Token: 0x06004BD6 RID: 19414 RVA: 0x00079AD5 File Offset: 0x00077CD5
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005AC RID: 1452
		public class AddScopeParameters : ParametersBase
		{
			// Token: 0x17002C33 RID: 11315
			// (set) Token: 0x06004BD8 RID: 19416 RVA: 0x00079AF5 File Offset: 0x00077CF5
			public virtual SwitchParameter AddScope
			{
				set
				{
					base.PowerSharpParameters["AddScope"] = value;
				}
			}

			// Token: 0x17002C34 RID: 11316
			// (set) Token: 0x06004BD9 RID: 19417 RVA: 0x00079B0D File Offset: 0x00077D0D
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C35 RID: 11317
			// (set) Token: 0x06004BDA RID: 19418 RVA: 0x00079B20 File Offset: 0x00077D20
			public virtual ExchangeSettingsScope Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x17002C36 RID: 11318
			// (set) Token: 0x06004BDB RID: 19419 RVA: 0x00079B38 File Offset: 0x00077D38
			public virtual string MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002C37 RID: 11319
			// (set) Token: 0x06004BDC RID: 19420 RVA: 0x00079B4B File Offset: 0x00077D4B
			public virtual string MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002C38 RID: 11320
			// (set) Token: 0x06004BDD RID: 19421 RVA: 0x00079B5E File Offset: 0x00077D5E
			public virtual string NameMatch
			{
				set
				{
					base.PowerSharpParameters["NameMatch"] = value;
				}
			}

			// Token: 0x17002C39 RID: 11321
			// (set) Token: 0x06004BDE RID: 19422 RVA: 0x00079B71 File Offset: 0x00077D71
			public virtual Guid? GuidMatch
			{
				set
				{
					base.PowerSharpParameters["GuidMatch"] = value;
				}
			}

			// Token: 0x17002C3A RID: 11322
			// (set) Token: 0x06004BDF RID: 19423 RVA: 0x00079B89 File Offset: 0x00077D89
			public virtual string GenericScopeName
			{
				set
				{
					base.PowerSharpParameters["GenericScopeName"] = value;
				}
			}

			// Token: 0x17002C3B RID: 11323
			// (set) Token: 0x06004BE0 RID: 19424 RVA: 0x00079B9C File Offset: 0x00077D9C
			public virtual string GenericScopeValue
			{
				set
				{
					base.PowerSharpParameters["GenericScopeValue"] = value;
				}
			}

			// Token: 0x17002C3C RID: 11324
			// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x00079BAF File Offset: 0x00077DAF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C3D RID: 11325
			// (set) Token: 0x06004BE2 RID: 19426 RVA: 0x00079BCD File Offset: 0x00077DCD
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C3E RID: 11326
			// (set) Token: 0x06004BE3 RID: 19427 RVA: 0x00079BE5 File Offset: 0x00077DE5
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C3F RID: 11327
			// (set) Token: 0x06004BE4 RID: 19428 RVA: 0x00079BF8 File Offset: 0x00077DF8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C40 RID: 11328
			// (set) Token: 0x06004BE5 RID: 19429 RVA: 0x00079C0B File Offset: 0x00077E0B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C41 RID: 11329
			// (set) Token: 0x06004BE6 RID: 19430 RVA: 0x00079C1E File Offset: 0x00077E1E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C42 RID: 11330
			// (set) Token: 0x06004BE7 RID: 19431 RVA: 0x00079C36 File Offset: 0x00077E36
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C43 RID: 11331
			// (set) Token: 0x06004BE8 RID: 19432 RVA: 0x00079C4E File Offset: 0x00077E4E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C44 RID: 11332
			// (set) Token: 0x06004BE9 RID: 19433 RVA: 0x00079C66 File Offset: 0x00077E66
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C45 RID: 11333
			// (set) Token: 0x06004BEA RID: 19434 RVA: 0x00079C7E File Offset: 0x00077E7E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C46 RID: 11334
			// (set) Token: 0x06004BEB RID: 19435 RVA: 0x00079C96 File Offset: 0x00077E96
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005AD RID: 1453
		public class UpdateScopeParameters : ParametersBase
		{
			// Token: 0x17002C47 RID: 11335
			// (set) Token: 0x06004BED RID: 19437 RVA: 0x00079CB6 File Offset: 0x00077EB6
			public virtual SwitchParameter UpdateScope
			{
				set
				{
					base.PowerSharpParameters["UpdateScope"] = value;
				}
			}

			// Token: 0x17002C48 RID: 11336
			// (set) Token: 0x06004BEE RID: 19438 RVA: 0x00079CCE File Offset: 0x00077ECE
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C49 RID: 11337
			// (set) Token: 0x06004BEF RID: 19439 RVA: 0x00079CE1 File Offset: 0x00077EE1
			public virtual Guid? ScopeId
			{
				set
				{
					base.PowerSharpParameters["ScopeId"] = value;
				}
			}

			// Token: 0x17002C4A RID: 11338
			// (set) Token: 0x06004BF0 RID: 19440 RVA: 0x00079CF9 File Offset: 0x00077EF9
			public virtual string MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002C4B RID: 11339
			// (set) Token: 0x06004BF1 RID: 19441 RVA: 0x00079D0C File Offset: 0x00077F0C
			public virtual string MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002C4C RID: 11340
			// (set) Token: 0x06004BF2 RID: 19442 RVA: 0x00079D1F File Offset: 0x00077F1F
			public virtual string NameMatch
			{
				set
				{
					base.PowerSharpParameters["NameMatch"] = value;
				}
			}

			// Token: 0x17002C4D RID: 11341
			// (set) Token: 0x06004BF3 RID: 19443 RVA: 0x00079D32 File Offset: 0x00077F32
			public virtual Guid? GuidMatch
			{
				set
				{
					base.PowerSharpParameters["GuidMatch"] = value;
				}
			}

			// Token: 0x17002C4E RID: 11342
			// (set) Token: 0x06004BF4 RID: 19444 RVA: 0x00079D4A File Offset: 0x00077F4A
			public virtual string GenericScopeName
			{
				set
				{
					base.PowerSharpParameters["GenericScopeName"] = value;
				}
			}

			// Token: 0x17002C4F RID: 11343
			// (set) Token: 0x06004BF5 RID: 19445 RVA: 0x00079D5D File Offset: 0x00077F5D
			public virtual string GenericScopeValue
			{
				set
				{
					base.PowerSharpParameters["GenericScopeValue"] = value;
				}
			}

			// Token: 0x17002C50 RID: 11344
			// (set) Token: 0x06004BF6 RID: 19446 RVA: 0x00079D70 File Offset: 0x00077F70
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C51 RID: 11345
			// (set) Token: 0x06004BF7 RID: 19447 RVA: 0x00079D8E File Offset: 0x00077F8E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C52 RID: 11346
			// (set) Token: 0x06004BF8 RID: 19448 RVA: 0x00079DA6 File Offset: 0x00077FA6
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C53 RID: 11347
			// (set) Token: 0x06004BF9 RID: 19449 RVA: 0x00079DB9 File Offset: 0x00077FB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C54 RID: 11348
			// (set) Token: 0x06004BFA RID: 19450 RVA: 0x00079DCC File Offset: 0x00077FCC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C55 RID: 11349
			// (set) Token: 0x06004BFB RID: 19451 RVA: 0x00079DDF File Offset: 0x00077FDF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C56 RID: 11350
			// (set) Token: 0x06004BFC RID: 19452 RVA: 0x00079DF7 File Offset: 0x00077FF7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C57 RID: 11351
			// (set) Token: 0x06004BFD RID: 19453 RVA: 0x00079E0F File Offset: 0x0007800F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C58 RID: 11352
			// (set) Token: 0x06004BFE RID: 19454 RVA: 0x00079E27 File Offset: 0x00078027
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C59 RID: 11353
			// (set) Token: 0x06004BFF RID: 19455 RVA: 0x00079E3F File Offset: 0x0007803F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C5A RID: 11354
			// (set) Token: 0x06004C00 RID: 19456 RVA: 0x00079E57 File Offset: 0x00078057
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005AE RID: 1454
		public class RemoveScopeParameters : ParametersBase
		{
			// Token: 0x17002C5B RID: 11355
			// (set) Token: 0x06004C02 RID: 19458 RVA: 0x00079E77 File Offset: 0x00078077
			public virtual SwitchParameter RemoveScope
			{
				set
				{
					base.PowerSharpParameters["RemoveScope"] = value;
				}
			}

			// Token: 0x17002C5C RID: 11356
			// (set) Token: 0x06004C03 RID: 19459 RVA: 0x00079E8F File Offset: 0x0007808F
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C5D RID: 11357
			// (set) Token: 0x06004C04 RID: 19460 RVA: 0x00079EA2 File Offset: 0x000780A2
			public virtual Guid? ScopeId
			{
				set
				{
					base.PowerSharpParameters["ScopeId"] = value;
				}
			}

			// Token: 0x17002C5E RID: 11358
			// (set) Token: 0x06004C05 RID: 19461 RVA: 0x00079EBA File Offset: 0x000780BA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C5F RID: 11359
			// (set) Token: 0x06004C06 RID: 19462 RVA: 0x00079ED8 File Offset: 0x000780D8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C60 RID: 11360
			// (set) Token: 0x06004C07 RID: 19463 RVA: 0x00079EF0 File Offset: 0x000780F0
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C61 RID: 11361
			// (set) Token: 0x06004C08 RID: 19464 RVA: 0x00079F03 File Offset: 0x00078103
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C62 RID: 11362
			// (set) Token: 0x06004C09 RID: 19465 RVA: 0x00079F16 File Offset: 0x00078116
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C63 RID: 11363
			// (set) Token: 0x06004C0A RID: 19466 RVA: 0x00079F29 File Offset: 0x00078129
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C64 RID: 11364
			// (set) Token: 0x06004C0B RID: 19467 RVA: 0x00079F41 File Offset: 0x00078141
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C65 RID: 11365
			// (set) Token: 0x06004C0C RID: 19468 RVA: 0x00079F59 File Offset: 0x00078159
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C66 RID: 11366
			// (set) Token: 0x06004C0D RID: 19469 RVA: 0x00079F71 File Offset: 0x00078171
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C67 RID: 11367
			// (set) Token: 0x06004C0E RID: 19470 RVA: 0x00079F89 File Offset: 0x00078189
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C68 RID: 11368
			// (set) Token: 0x06004C0F RID: 19471 RVA: 0x00079FA1 File Offset: 0x000781A1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005AF RID: 1455
		public class ClearHistoryGroupParameters : ParametersBase
		{
			// Token: 0x17002C69 RID: 11369
			// (set) Token: 0x06004C11 RID: 19473 RVA: 0x00079FC1 File Offset: 0x000781C1
			public virtual SwitchParameter ClearHistory
			{
				set
				{
					base.PowerSharpParameters["ClearHistory"] = value;
				}
			}

			// Token: 0x17002C6A RID: 11370
			// (set) Token: 0x06004C12 RID: 19474 RVA: 0x00079FD9 File Offset: 0x000781D9
			public virtual string GroupName
			{
				set
				{
					base.PowerSharpParameters["GroupName"] = value;
				}
			}

			// Token: 0x17002C6B RID: 11371
			// (set) Token: 0x06004C13 RID: 19475 RVA: 0x00079FEC File Offset: 0x000781EC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C6C RID: 11372
			// (set) Token: 0x06004C14 RID: 19476 RVA: 0x0007A00A File Offset: 0x0007820A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C6D RID: 11373
			// (set) Token: 0x06004C15 RID: 19477 RVA: 0x0007A022 File Offset: 0x00078222
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C6E RID: 11374
			// (set) Token: 0x06004C16 RID: 19478 RVA: 0x0007A035 File Offset: 0x00078235
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C6F RID: 11375
			// (set) Token: 0x06004C17 RID: 19479 RVA: 0x0007A048 File Offset: 0x00078248
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C70 RID: 11376
			// (set) Token: 0x06004C18 RID: 19480 RVA: 0x0007A05B File Offset: 0x0007825B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C71 RID: 11377
			// (set) Token: 0x06004C19 RID: 19481 RVA: 0x0007A073 File Offset: 0x00078273
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C72 RID: 11378
			// (set) Token: 0x06004C1A RID: 19482 RVA: 0x0007A08B File Offset: 0x0007828B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C73 RID: 11379
			// (set) Token: 0x06004C1B RID: 19483 RVA: 0x0007A0A3 File Offset: 0x000782A3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C74 RID: 11380
			// (set) Token: 0x06004C1C RID: 19484 RVA: 0x0007A0BB File Offset: 0x000782BB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C75 RID: 11381
			// (set) Token: 0x06004C1D RID: 19485 RVA: 0x0007A0D3 File Offset: 0x000782D3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005B0 RID: 1456
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002C76 RID: 11382
			// (set) Token: 0x06004C1F RID: 19487 RVA: 0x0007A0F3 File Offset: 0x000782F3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C77 RID: 11383
			// (set) Token: 0x06004C20 RID: 19488 RVA: 0x0007A111 File Offset: 0x00078311
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C78 RID: 11384
			// (set) Token: 0x06004C21 RID: 19489 RVA: 0x0007A129 File Offset: 0x00078329
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C79 RID: 11385
			// (set) Token: 0x06004C22 RID: 19490 RVA: 0x0007A13C File Offset: 0x0007833C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C7A RID: 11386
			// (set) Token: 0x06004C23 RID: 19491 RVA: 0x0007A14F File Offset: 0x0007834F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C7B RID: 11387
			// (set) Token: 0x06004C24 RID: 19492 RVA: 0x0007A162 File Offset: 0x00078362
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C7C RID: 11388
			// (set) Token: 0x06004C25 RID: 19493 RVA: 0x0007A17A File Offset: 0x0007837A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C7D RID: 11389
			// (set) Token: 0x06004C26 RID: 19494 RVA: 0x0007A192 File Offset: 0x00078392
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C7E RID: 11390
			// (set) Token: 0x06004C27 RID: 19495 RVA: 0x0007A1AA File Offset: 0x000783AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C7F RID: 11391
			// (set) Token: 0x06004C28 RID: 19496 RVA: 0x0007A1C2 File Offset: 0x000783C2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C80 RID: 11392
			// (set) Token: 0x06004C29 RID: 19497 RVA: 0x0007A1DA File Offset: 0x000783DA
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005B1 RID: 1457
		public class EnableSettingsGroupParameters : ParametersBase
		{
			// Token: 0x17002C81 RID: 11393
			// (set) Token: 0x06004C2B RID: 19499 RVA: 0x0007A1FA File Offset: 0x000783FA
			public virtual string EnableGroup
			{
				set
				{
					base.PowerSharpParameters["EnableGroup"] = value;
				}
			}

			// Token: 0x17002C82 RID: 11394
			// (set) Token: 0x06004C2C RID: 19500 RVA: 0x0007A20D File Offset: 0x0007840D
			public virtual string DisableGroup
			{
				set
				{
					base.PowerSharpParameters["DisableGroup"] = value;
				}
			}

			// Token: 0x17002C83 RID: 11395
			// (set) Token: 0x06004C2D RID: 19501 RVA: 0x0007A220 File Offset: 0x00078420
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002C84 RID: 11396
			// (set) Token: 0x06004C2E RID: 19502 RVA: 0x0007A23E File Offset: 0x0007843E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002C85 RID: 11397
			// (set) Token: 0x06004C2F RID: 19503 RVA: 0x0007A256 File Offset: 0x00078456
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002C86 RID: 11398
			// (set) Token: 0x06004C30 RID: 19504 RVA: 0x0007A269 File Offset: 0x00078469
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002C87 RID: 11399
			// (set) Token: 0x06004C31 RID: 19505 RVA: 0x0007A27C File Offset: 0x0007847C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C88 RID: 11400
			// (set) Token: 0x06004C32 RID: 19506 RVA: 0x0007A28F File Offset: 0x0007848F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002C89 RID: 11401
			// (set) Token: 0x06004C33 RID: 19507 RVA: 0x0007A2A7 File Offset: 0x000784A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002C8A RID: 11402
			// (set) Token: 0x06004C34 RID: 19508 RVA: 0x0007A2BF File Offset: 0x000784BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002C8B RID: 11403
			// (set) Token: 0x06004C35 RID: 19509 RVA: 0x0007A2D7 File Offset: 0x000784D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002C8C RID: 11404
			// (set) Token: 0x06004C36 RID: 19510 RVA: 0x0007A2EF File Offset: 0x000784EF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002C8D RID: 11405
			// (set) Token: 0x06004C37 RID: 19511 RVA: 0x0007A307 File Offset: 0x00078507
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

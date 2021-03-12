using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200001A RID: 26
	public class SetAdServerSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<RunspaceServerSettingsPresentationObject>
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x00032F8C File Offset: 0x0003118C
		private SetAdServerSettingsCommand() : base("Set-AdServerSettings")
		{
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00032F99 File Offset: 0x00031199
		public SetAdServerSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00032FA8 File Offset: 0x000311A8
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.SingleDCParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00032FB2 File Offset: 0x000311B2
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.FullParamsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00032FBC File Offset: 0x000311BC
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00032FC6 File Offset: 0x000311C6
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.InstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00032FD0 File Offset: 0x000311D0
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.GlsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00032FDA File Offset: 0x000311DA
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.ForceADInTemplateScopeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00032FE4 File Offset: 0x000311E4
		public virtual SetAdServerSettingsCommand SetParameters(SetAdServerSettingsCommand.ResetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200001B RID: 27
		public class SingleDCParameters : ParametersBase
		{
			// Token: 0x17000080 RID: 128
			// (set) Token: 0x06001504 RID: 5380 RVA: 0x00032FEE File Offset: 0x000311EE
			public virtual Fqdn PreferredServer
			{
				set
				{
					base.PowerSharpParameters["PreferredServer"] = value;
				}
			}

			// Token: 0x17000081 RID: 129
			// (set) Token: 0x06001505 RID: 5381 RVA: 0x00033001 File Offset: 0x00031201
			public virtual string RecipientViewRoot
			{
				set
				{
					base.PowerSharpParameters["RecipientViewRoot"] = value;
				}
			}

			// Token: 0x17000082 RID: 130
			// (set) Token: 0x06001506 RID: 5382 RVA: 0x00033014 File Offset: 0x00031214
			public virtual bool ViewEntireForest
			{
				set
				{
					base.PowerSharpParameters["ViewEntireForest"] = value;
				}
			}

			// Token: 0x17000083 RID: 131
			// (set) Token: 0x06001507 RID: 5383 RVA: 0x0003302C File Offset: 0x0003122C
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x17000084 RID: 132
			// (set) Token: 0x06001508 RID: 5384 RVA: 0x00033044 File Offset: 0x00031244
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x17000085 RID: 133
			// (set) Token: 0x06001509 RID: 5385 RVA: 0x0003305C File Offset: 0x0003125C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000086 RID: 134
			// (set) Token: 0x0600150A RID: 5386 RVA: 0x00033074 File Offset: 0x00031274
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000087 RID: 135
			// (set) Token: 0x0600150B RID: 5387 RVA: 0x0003308C File Offset: 0x0003128C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000088 RID: 136
			// (set) Token: 0x0600150C RID: 5388 RVA: 0x000330A4 File Offset: 0x000312A4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000089 RID: 137
			// (set) Token: 0x0600150D RID: 5389 RVA: 0x000330BC File Offset: 0x000312BC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200001C RID: 28
		public class FullParamsParameters : ParametersBase
		{
			// Token: 0x1700008A RID: 138
			// (set) Token: 0x0600150F RID: 5391 RVA: 0x000330DC File Offset: 0x000312DC
			public virtual Fqdn ConfigurationDomainController
			{
				set
				{
					base.PowerSharpParameters["ConfigurationDomainController"] = value;
				}
			}

			// Token: 0x1700008B RID: 139
			// (set) Token: 0x06001510 RID: 5392 RVA: 0x000330EF File Offset: 0x000312EF
			public virtual Fqdn PreferredGlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["PreferredGlobalCatalog"] = value;
				}
			}

			// Token: 0x1700008C RID: 140
			// (set) Token: 0x06001511 RID: 5393 RVA: 0x00033102 File Offset: 0x00031302
			public virtual MultiValuedProperty<Fqdn> SetPreferredDomainControllers
			{
				set
				{
					base.PowerSharpParameters["SetPreferredDomainControllers"] = value;
				}
			}

			// Token: 0x1700008D RID: 141
			// (set) Token: 0x06001512 RID: 5394 RVA: 0x00033115 File Offset: 0x00031315
			public virtual string RecipientViewRoot
			{
				set
				{
					base.PowerSharpParameters["RecipientViewRoot"] = value;
				}
			}

			// Token: 0x1700008E RID: 142
			// (set) Token: 0x06001513 RID: 5395 RVA: 0x00033128 File Offset: 0x00031328
			public virtual bool ViewEntireForest
			{
				set
				{
					base.PowerSharpParameters["ViewEntireForest"] = value;
				}
			}

			// Token: 0x1700008F RID: 143
			// (set) Token: 0x06001514 RID: 5396 RVA: 0x00033140 File Offset: 0x00031340
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x17000090 RID: 144
			// (set) Token: 0x06001515 RID: 5397 RVA: 0x00033158 File Offset: 0x00031358
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x17000091 RID: 145
			// (set) Token: 0x06001516 RID: 5398 RVA: 0x00033170 File Offset: 0x00031370
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000092 RID: 146
			// (set) Token: 0x06001517 RID: 5399 RVA: 0x00033188 File Offset: 0x00031388
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000093 RID: 147
			// (set) Token: 0x06001518 RID: 5400 RVA: 0x000331A0 File Offset: 0x000313A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000094 RID: 148
			// (set) Token: 0x06001519 RID: 5401 RVA: 0x000331B8 File Offset: 0x000313B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000095 RID: 149
			// (set) Token: 0x0600151A RID: 5402 RVA: 0x000331D0 File Offset: 0x000313D0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200001D RID: 29
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000096 RID: 150
			// (set) Token: 0x0600151C RID: 5404 RVA: 0x000331F0 File Offset: 0x000313F0
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x17000097 RID: 151
			// (set) Token: 0x0600151D RID: 5405 RVA: 0x00033208 File Offset: 0x00031408
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x17000098 RID: 152
			// (set) Token: 0x0600151E RID: 5406 RVA: 0x00033220 File Offset: 0x00031420
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000099 RID: 153
			// (set) Token: 0x0600151F RID: 5407 RVA: 0x00033238 File Offset: 0x00031438
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700009A RID: 154
			// (set) Token: 0x06001520 RID: 5408 RVA: 0x00033250 File Offset: 0x00031450
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700009B RID: 155
			// (set) Token: 0x06001521 RID: 5409 RVA: 0x00033268 File Offset: 0x00031468
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700009C RID: 156
			// (set) Token: 0x06001522 RID: 5410 RVA: 0x00033280 File Offset: 0x00031480
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200001E RID: 30
		public class InstanceParameters : ParametersBase
		{
			// Token: 0x1700009D RID: 157
			// (set) Token: 0x06001524 RID: 5412 RVA: 0x000332A0 File Offset: 0x000314A0
			public virtual RunspaceServerSettingsPresentationObject RunspaceServerSettings
			{
				set
				{
					base.PowerSharpParameters["RunspaceServerSettings"] = value;
				}
			}

			// Token: 0x1700009E RID: 158
			// (set) Token: 0x06001525 RID: 5413 RVA: 0x000332B3 File Offset: 0x000314B3
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x1700009F RID: 159
			// (set) Token: 0x06001526 RID: 5414 RVA: 0x000332CB File Offset: 0x000314CB
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (set) Token: 0x06001527 RID: 5415 RVA: 0x000332E3 File Offset: 0x000314E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (set) Token: 0x06001528 RID: 5416 RVA: 0x000332FB File Offset: 0x000314FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (set) Token: 0x06001529 RID: 5417 RVA: 0x00033313 File Offset: 0x00031513
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000A3 RID: 163
			// (set) Token: 0x0600152A RID: 5418 RVA: 0x0003332B File Offset: 0x0003152B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000A4 RID: 164
			// (set) Token: 0x0600152B RID: 5419 RVA: 0x00033343 File Offset: 0x00031543
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200001F RID: 31
		public class GlsParameters : ParametersBase
		{
			// Token: 0x170000A5 RID: 165
			// (set) Token: 0x0600152D RID: 5421 RVA: 0x00033363 File Offset: 0x00031563
			public virtual bool DisableGls
			{
				set
				{
					base.PowerSharpParameters["DisableGls"] = value;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (set) Token: 0x0600152E RID: 5422 RVA: 0x0003337B File Offset: 0x0003157B
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (set) Token: 0x0600152F RID: 5423 RVA: 0x00033393 File Offset: 0x00031593
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x170000A8 RID: 168
			// (set) Token: 0x06001530 RID: 5424 RVA: 0x000333AB File Offset: 0x000315AB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (set) Token: 0x06001531 RID: 5425 RVA: 0x000333C3 File Offset: 0x000315C3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000AA RID: 170
			// (set) Token: 0x06001532 RID: 5426 RVA: 0x000333DB File Offset: 0x000315DB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000AB RID: 171
			// (set) Token: 0x06001533 RID: 5427 RVA: 0x000333F3 File Offset: 0x000315F3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000AC RID: 172
			// (set) Token: 0x06001534 RID: 5428 RVA: 0x0003340B File Offset: 0x0003160B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000020 RID: 32
		public class ForceADInTemplateScopeParameters : ParametersBase
		{
			// Token: 0x170000AD RID: 173
			// (set) Token: 0x06001536 RID: 5430 RVA: 0x0003342B File Offset: 0x0003162B
			public virtual bool ForceADInTemplateScope
			{
				set
				{
					base.PowerSharpParameters["ForceADInTemplateScope"] = value;
				}
			}

			// Token: 0x170000AE RID: 174
			// (set) Token: 0x06001537 RID: 5431 RVA: 0x00033443 File Offset: 0x00031643
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x170000AF RID: 175
			// (set) Token: 0x06001538 RID: 5432 RVA: 0x0003345B File Offset: 0x0003165B
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (set) Token: 0x06001539 RID: 5433 RVA: 0x00033473 File Offset: 0x00031673
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000B1 RID: 177
			// (set) Token: 0x0600153A RID: 5434 RVA: 0x0003348B File Offset: 0x0003168B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000B2 RID: 178
			// (set) Token: 0x0600153B RID: 5435 RVA: 0x000334A3 File Offset: 0x000316A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (set) Token: 0x0600153C RID: 5436 RVA: 0x000334BB File Offset: 0x000316BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (set) Token: 0x0600153D RID: 5437 RVA: 0x000334D3 File Offset: 0x000316D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000021 RID: 33
		public class ResetParameters : ParametersBase
		{
			// Token: 0x170000B5 RID: 181
			// (set) Token: 0x0600153F RID: 5439 RVA: 0x000334F3 File Offset: 0x000316F3
			public virtual SwitchParameter ResetSettings
			{
				set
				{
					base.PowerSharpParameters["ResetSettings"] = value;
				}
			}

			// Token: 0x170000B6 RID: 182
			// (set) Token: 0x06001540 RID: 5440 RVA: 0x0003350B File Offset: 0x0003170B
			public virtual bool WriteOriginatingChangeTimestamp
			{
				set
				{
					base.PowerSharpParameters["WriteOriginatingChangeTimestamp"] = value;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (set) Token: 0x06001541 RID: 5441 RVA: 0x00033523 File Offset: 0x00031723
			public virtual bool WriteShadowProperties
			{
				set
				{
					base.PowerSharpParameters["WriteShadowProperties"] = value;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (set) Token: 0x06001542 RID: 5442 RVA: 0x0003353B File Offset: 0x0003173B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (set) Token: 0x06001543 RID: 5443 RVA: 0x00033553 File Offset: 0x00031753
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000BA RID: 186
			// (set) Token: 0x06001544 RID: 5444 RVA: 0x0003356B File Offset: 0x0003176B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000BB RID: 187
			// (set) Token: 0x06001545 RID: 5445 RVA: 0x00033583 File Offset: 0x00031783
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000BC RID: 188
			// (set) Token: 0x06001546 RID: 5446 RVA: 0x0003359B File Offset: 0x0003179B
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002EE RID: 750
	public class SetAuthServerCommand : SyntheticCommandWithPipelineInputNoOutput<AuthServer>
	{
		// Token: 0x060032C7 RID: 12999 RVA: 0x00059C32 File Offset: 0x00057E32
		private SetAuthServerCommand() : base("Set-AuthServer")
		{
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x00059C3F File Offset: 0x00057E3F
		public SetAuthServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00059C4E File Offset: 0x00057E4E
		public virtual SetAuthServerCommand SetParameters(SetAuthServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x00059C58 File Offset: 0x00057E58
		public virtual SetAuthServerCommand SetParameters(SetAuthServerCommand.AuthMetadataUrlParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x00059C62 File Offset: 0x00057E62
		public virtual SetAuthServerCommand SetParameters(SetAuthServerCommand.NativeClientAuthServerParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x00059C6C File Offset: 0x00057E6C
		public virtual SetAuthServerCommand SetParameters(SetAuthServerCommand.RefreshAuthMetadataParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x00059C76 File Offset: 0x00057E76
		public virtual SetAuthServerCommand SetParameters(SetAuthServerCommand.AppSecretParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002EF RID: 751
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170018A4 RID: 6308
			// (set) Token: 0x060032CE RID: 13006 RVA: 0x00059C80 File Offset: 0x00057E80
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x170018A5 RID: 6309
			// (set) Token: 0x060032CF RID: 13007 RVA: 0x00059C9E File Offset: 0x00057E9E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018A6 RID: 6310
			// (set) Token: 0x060032D0 RID: 13008 RVA: 0x00059CB1 File Offset: 0x00057EB1
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170018A7 RID: 6311
			// (set) Token: 0x060032D1 RID: 13009 RVA: 0x00059CC9 File Offset: 0x00057EC9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170018A8 RID: 6312
			// (set) Token: 0x060032D2 RID: 13010 RVA: 0x00059CDC File Offset: 0x00057EDC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018A9 RID: 6313
			// (set) Token: 0x060032D3 RID: 13011 RVA: 0x00059CF4 File Offset: 0x00057EF4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018AA RID: 6314
			// (set) Token: 0x060032D4 RID: 13012 RVA: 0x00059D0C File Offset: 0x00057F0C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018AB RID: 6315
			// (set) Token: 0x060032D5 RID: 13013 RVA: 0x00059D24 File Offset: 0x00057F24
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018AC RID: 6316
			// (set) Token: 0x060032D6 RID: 13014 RVA: 0x00059D3C File Offset: 0x00057F3C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002F0 RID: 752
		public class AuthMetadataUrlParameterSetParameters : ParametersBase
		{
			// Token: 0x170018AD RID: 6317
			// (set) Token: 0x060032D8 RID: 13016 RVA: 0x00059D5C File Offset: 0x00057F5C
			public virtual string AuthMetadataUrl
			{
				set
				{
					base.PowerSharpParameters["AuthMetadataUrl"] = value;
				}
			}

			// Token: 0x170018AE RID: 6318
			// (set) Token: 0x060032D9 RID: 13017 RVA: 0x00059D6F File Offset: 0x00057F6F
			public virtual SwitchParameter TrustAnySSLCertificate
			{
				set
				{
					base.PowerSharpParameters["TrustAnySSLCertificate"] = value;
				}
			}

			// Token: 0x170018AF RID: 6319
			// (set) Token: 0x060032DA RID: 13018 RVA: 0x00059D87 File Offset: 0x00057F87
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x170018B0 RID: 6320
			// (set) Token: 0x060032DB RID: 13019 RVA: 0x00059DA5 File Offset: 0x00057FA5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018B1 RID: 6321
			// (set) Token: 0x060032DC RID: 13020 RVA: 0x00059DB8 File Offset: 0x00057FB8
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170018B2 RID: 6322
			// (set) Token: 0x060032DD RID: 13021 RVA: 0x00059DD0 File Offset: 0x00057FD0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170018B3 RID: 6323
			// (set) Token: 0x060032DE RID: 13022 RVA: 0x00059DE3 File Offset: 0x00057FE3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018B4 RID: 6324
			// (set) Token: 0x060032DF RID: 13023 RVA: 0x00059DFB File Offset: 0x00057FFB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018B5 RID: 6325
			// (set) Token: 0x060032E0 RID: 13024 RVA: 0x00059E13 File Offset: 0x00058013
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018B6 RID: 6326
			// (set) Token: 0x060032E1 RID: 13025 RVA: 0x00059E2B File Offset: 0x0005802B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018B7 RID: 6327
			// (set) Token: 0x060032E2 RID: 13026 RVA: 0x00059E43 File Offset: 0x00058043
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002F1 RID: 753
		public class NativeClientAuthServerParameterSetParameters : ParametersBase
		{
			// Token: 0x170018B8 RID: 6328
			// (set) Token: 0x060032E4 RID: 13028 RVA: 0x00059E63 File Offset: 0x00058063
			public virtual string AuthMetadataUrl
			{
				set
				{
					base.PowerSharpParameters["AuthMetadataUrl"] = value;
				}
			}

			// Token: 0x170018B9 RID: 6329
			// (set) Token: 0x060032E5 RID: 13029 RVA: 0x00059E76 File Offset: 0x00058076
			public virtual SwitchParameter TrustAnySSLCertificate
			{
				set
				{
					base.PowerSharpParameters["TrustAnySSLCertificate"] = value;
				}
			}

			// Token: 0x170018BA RID: 6330
			// (set) Token: 0x060032E6 RID: 13030 RVA: 0x00059E8E File Offset: 0x0005808E
			public virtual bool IsDefaultAuthorizationEndpoint
			{
				set
				{
					base.PowerSharpParameters["IsDefaultAuthorizationEndpoint"] = value;
				}
			}

			// Token: 0x170018BB RID: 6331
			// (set) Token: 0x060032E7 RID: 13031 RVA: 0x00059EA6 File Offset: 0x000580A6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x170018BC RID: 6332
			// (set) Token: 0x060032E8 RID: 13032 RVA: 0x00059EC4 File Offset: 0x000580C4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018BD RID: 6333
			// (set) Token: 0x060032E9 RID: 13033 RVA: 0x00059ED7 File Offset: 0x000580D7
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170018BE RID: 6334
			// (set) Token: 0x060032EA RID: 13034 RVA: 0x00059EEF File Offset: 0x000580EF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170018BF RID: 6335
			// (set) Token: 0x060032EB RID: 13035 RVA: 0x00059F02 File Offset: 0x00058102
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018C0 RID: 6336
			// (set) Token: 0x060032EC RID: 13036 RVA: 0x00059F1A File Offset: 0x0005811A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018C1 RID: 6337
			// (set) Token: 0x060032ED RID: 13037 RVA: 0x00059F32 File Offset: 0x00058132
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018C2 RID: 6338
			// (set) Token: 0x060032EE RID: 13038 RVA: 0x00059F4A File Offset: 0x0005814A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018C3 RID: 6339
			// (set) Token: 0x060032EF RID: 13039 RVA: 0x00059F62 File Offset: 0x00058162
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002F2 RID: 754
		public class RefreshAuthMetadataParameterSetParameters : ParametersBase
		{
			// Token: 0x170018C4 RID: 6340
			// (set) Token: 0x060032F1 RID: 13041 RVA: 0x00059F82 File Offset: 0x00058182
			public virtual SwitchParameter RefreshAuthMetadata
			{
				set
				{
					base.PowerSharpParameters["RefreshAuthMetadata"] = value;
				}
			}

			// Token: 0x170018C5 RID: 6341
			// (set) Token: 0x060032F2 RID: 13042 RVA: 0x00059F9A File Offset: 0x0005819A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x170018C6 RID: 6342
			// (set) Token: 0x060032F3 RID: 13043 RVA: 0x00059FB8 File Offset: 0x000581B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018C7 RID: 6343
			// (set) Token: 0x060032F4 RID: 13044 RVA: 0x00059FCB File Offset: 0x000581CB
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170018C8 RID: 6344
			// (set) Token: 0x060032F5 RID: 13045 RVA: 0x00059FE3 File Offset: 0x000581E3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170018C9 RID: 6345
			// (set) Token: 0x060032F6 RID: 13046 RVA: 0x00059FF6 File Offset: 0x000581F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018CA RID: 6346
			// (set) Token: 0x060032F7 RID: 13047 RVA: 0x0005A00E File Offset: 0x0005820E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018CB RID: 6347
			// (set) Token: 0x060032F8 RID: 13048 RVA: 0x0005A026 File Offset: 0x00058226
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018CC RID: 6348
			// (set) Token: 0x060032F9 RID: 13049 RVA: 0x0005A03E File Offset: 0x0005823E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018CD RID: 6349
			// (set) Token: 0x060032FA RID: 13050 RVA: 0x0005A056 File Offset: 0x00058256
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002F3 RID: 755
		public class AppSecretParameterSetParameters : ParametersBase
		{
			// Token: 0x170018CE RID: 6350
			// (set) Token: 0x060032FC RID: 13052 RVA: 0x0005A076 File Offset: 0x00058276
			public virtual string AppSecret
			{
				set
				{
					base.PowerSharpParameters["AppSecret"] = value;
				}
			}

			// Token: 0x170018CF RID: 6351
			// (set) Token: 0x060032FD RID: 13053 RVA: 0x0005A089 File Offset: 0x00058289
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x170018D0 RID: 6352
			// (set) Token: 0x060032FE RID: 13054 RVA: 0x0005A09C File Offset: 0x0005829C
			public virtual string TokenIssuingEndpoint
			{
				set
				{
					base.PowerSharpParameters["TokenIssuingEndpoint"] = value;
				}
			}

			// Token: 0x170018D1 RID: 6353
			// (set) Token: 0x060032FF RID: 13055 RVA: 0x0005A0AF File Offset: 0x000582AF
			public virtual string ApplicationIdentifier
			{
				set
				{
					base.PowerSharpParameters["ApplicationIdentifier"] = value;
				}
			}

			// Token: 0x170018D2 RID: 6354
			// (set) Token: 0x06003300 RID: 13056 RVA: 0x0005A0C2 File Offset: 0x000582C2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x170018D3 RID: 6355
			// (set) Token: 0x06003301 RID: 13057 RVA: 0x0005A0E0 File Offset: 0x000582E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018D4 RID: 6356
			// (set) Token: 0x06003302 RID: 13058 RVA: 0x0005A0F3 File Offset: 0x000582F3
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170018D5 RID: 6357
			// (set) Token: 0x06003303 RID: 13059 RVA: 0x0005A10B File Offset: 0x0005830B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170018D6 RID: 6358
			// (set) Token: 0x06003304 RID: 13060 RVA: 0x0005A11E File Offset: 0x0005831E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018D7 RID: 6359
			// (set) Token: 0x06003305 RID: 13061 RVA: 0x0005A136 File Offset: 0x00058336
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018D8 RID: 6360
			// (set) Token: 0x06003306 RID: 13062 RVA: 0x0005A14E File Offset: 0x0005834E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018D9 RID: 6361
			// (set) Token: 0x06003307 RID: 13063 RVA: 0x0005A166 File Offset: 0x00058366
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018DA RID: 6362
			// (set) Token: 0x06003308 RID: 13064 RVA: 0x0005A17E File Offset: 0x0005837E
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

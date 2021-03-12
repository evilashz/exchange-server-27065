using System;
using System.Collections;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003E8 RID: 1000
	public class ImportRMSTrustedPublishingDomainCommand : SyntheticCommandWithPipelineInput<RMSTrustedPublishingDomain, RMSTrustedPublishingDomain>
	{
		// Token: 0x06003B88 RID: 15240 RVA: 0x0006509D File Offset: 0x0006329D
		private ImportRMSTrustedPublishingDomainCommand() : base("Import-RMSTrustedPublishingDomain")
		{
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000650AA File Offset: 0x000632AA
		public ImportRMSTrustedPublishingDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000650B9 File Offset: 0x000632B9
		public virtual ImportRMSTrustedPublishingDomainCommand SetParameters(ImportRMSTrustedPublishingDomainCommand.ImportFromFileParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000650C3 File Offset: 0x000632C3
		public virtual ImportRMSTrustedPublishingDomainCommand SetParameters(ImportRMSTrustedPublishingDomainCommand.IntranetLicensingUrlParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000650CD File Offset: 0x000632CD
		public virtual ImportRMSTrustedPublishingDomainCommand SetParameters(ImportRMSTrustedPublishingDomainCommand.RefreshTemplatesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000650D7 File Offset: 0x000632D7
		public virtual ImportRMSTrustedPublishingDomainCommand SetParameters(ImportRMSTrustedPublishingDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000650E1 File Offset: 0x000632E1
		public virtual ImportRMSTrustedPublishingDomainCommand SetParameters(ImportRMSTrustedPublishingDomainCommand.RMSOnlineParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000650EB File Offset: 0x000632EB
		public virtual ImportRMSTrustedPublishingDomainCommand SetParameters(ImportRMSTrustedPublishingDomainCommand.RMSOnline2Parameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003E9 RID: 1001
		public class ImportFromFileParameters : ParametersBase
		{
			// Token: 0x17001F71 RID: 8049
			// (set) Token: 0x06003B90 RID: 15248 RVA: 0x000650F5 File Offset: 0x000632F5
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17001F72 RID: 8050
			// (set) Token: 0x06003B91 RID: 15249 RVA: 0x00065108 File Offset: 0x00063308
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17001F73 RID: 8051
			// (set) Token: 0x06003B92 RID: 15250 RVA: 0x00065120 File Offset: 0x00063320
			public virtual Uri IntranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17001F74 RID: 8052
			// (set) Token: 0x06003B93 RID: 15251 RVA: 0x00065133 File Offset: 0x00063333
			public virtual Uri ExtranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17001F75 RID: 8053
			// (set) Token: 0x06003B94 RID: 15252 RVA: 0x00065146 File Offset: 0x00063346
			public virtual Uri IntranetCertificationUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetCertificationUrl"] = value;
				}
			}

			// Token: 0x17001F76 RID: 8054
			// (set) Token: 0x06003B95 RID: 15253 RVA: 0x00065159 File Offset: 0x00063359
			public virtual Uri ExtranetCertificationUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetCertificationUrl"] = value;
				}
			}

			// Token: 0x17001F77 RID: 8055
			// (set) Token: 0x06003B96 RID: 15254 RVA: 0x0006516C File Offset: 0x0006336C
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001F78 RID: 8056
			// (set) Token: 0x06003B97 RID: 15255 RVA: 0x00065184 File Offset: 0x00063384
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F79 RID: 8057
			// (set) Token: 0x06003B98 RID: 15256 RVA: 0x000651A2 File Offset: 0x000633A2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001F7A RID: 8058
			// (set) Token: 0x06003B99 RID: 15257 RVA: 0x000651B5 File Offset: 0x000633B5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F7B RID: 8059
			// (set) Token: 0x06003B9A RID: 15258 RVA: 0x000651C8 File Offset: 0x000633C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F7C RID: 8060
			// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000651E0 File Offset: 0x000633E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F7D RID: 8061
			// (set) Token: 0x06003B9C RID: 15260 RVA: 0x000651F8 File Offset: 0x000633F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F7E RID: 8062
			// (set) Token: 0x06003B9D RID: 15261 RVA: 0x00065210 File Offset: 0x00063410
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001F7F RID: 8063
			// (set) Token: 0x06003B9E RID: 15262 RVA: 0x00065228 File Offset: 0x00063428
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003EA RID: 1002
		public class IntranetLicensingUrlParameters : ParametersBase
		{
			// Token: 0x17001F80 RID: 8064
			// (set) Token: 0x06003BA0 RID: 15264 RVA: 0x00065248 File Offset: 0x00063448
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17001F81 RID: 8065
			// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x0006525B File Offset: 0x0006345B
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17001F82 RID: 8066
			// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x00065273 File Offset: 0x00063473
			public virtual Uri IntranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17001F83 RID: 8067
			// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x00065286 File Offset: 0x00063486
			public virtual Uri ExtranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17001F84 RID: 8068
			// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x00065299 File Offset: 0x00063499
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001F85 RID: 8069
			// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000652B1 File Offset: 0x000634B1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F86 RID: 8070
			// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000652CF File Offset: 0x000634CF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001F87 RID: 8071
			// (set) Token: 0x06003BA7 RID: 15271 RVA: 0x000652E2 File Offset: 0x000634E2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F88 RID: 8072
			// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x000652F5 File Offset: 0x000634F5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F89 RID: 8073
			// (set) Token: 0x06003BA9 RID: 15273 RVA: 0x0006530D File Offset: 0x0006350D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F8A RID: 8074
			// (set) Token: 0x06003BAA RID: 15274 RVA: 0x00065325 File Offset: 0x00063525
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F8B RID: 8075
			// (set) Token: 0x06003BAB RID: 15275 RVA: 0x0006533D File Offset: 0x0006353D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001F8C RID: 8076
			// (set) Token: 0x06003BAC RID: 15276 RVA: 0x00065355 File Offset: 0x00063555
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003EB RID: 1003
		public class RefreshTemplatesParameters : ParametersBase
		{
			// Token: 0x17001F8D RID: 8077
			// (set) Token: 0x06003BAE RID: 15278 RVA: 0x00065375 File Offset: 0x00063575
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17001F8E RID: 8078
			// (set) Token: 0x06003BAF RID: 15279 RVA: 0x00065388 File Offset: 0x00063588
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17001F8F RID: 8079
			// (set) Token: 0x06003BB0 RID: 15280 RVA: 0x000653A0 File Offset: 0x000635A0
			public virtual SwitchParameter RefreshTemplates
			{
				set
				{
					base.PowerSharpParameters["RefreshTemplates"] = value;
				}
			}

			// Token: 0x17001F90 RID: 8080
			// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000653B8 File Offset: 0x000635B8
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001F91 RID: 8081
			// (set) Token: 0x06003BB2 RID: 15282 RVA: 0x000653D0 File Offset: 0x000635D0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F92 RID: 8082
			// (set) Token: 0x06003BB3 RID: 15283 RVA: 0x000653EE File Offset: 0x000635EE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001F93 RID: 8083
			// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x00065401 File Offset: 0x00063601
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F94 RID: 8084
			// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x00065414 File Offset: 0x00063614
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F95 RID: 8085
			// (set) Token: 0x06003BB6 RID: 15286 RVA: 0x0006542C File Offset: 0x0006362C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F96 RID: 8086
			// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x00065444 File Offset: 0x00063644
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F97 RID: 8087
			// (set) Token: 0x06003BB8 RID: 15288 RVA: 0x0006545C File Offset: 0x0006365C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001F98 RID: 8088
			// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x00065474 File Offset: 0x00063674
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003EC RID: 1004
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F99 RID: 8089
			// (set) Token: 0x06003BBB RID: 15291 RVA: 0x00065494 File Offset: 0x00063694
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001F9A RID: 8090
			// (set) Token: 0x06003BBC RID: 15292 RVA: 0x000654AC File Offset: 0x000636AC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F9B RID: 8091
			// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000654CA File Offset: 0x000636CA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001F9C RID: 8092
			// (set) Token: 0x06003BBE RID: 15294 RVA: 0x000654DD File Offset: 0x000636DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F9D RID: 8093
			// (set) Token: 0x06003BBF RID: 15295 RVA: 0x000654F0 File Offset: 0x000636F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F9E RID: 8094
			// (set) Token: 0x06003BC0 RID: 15296 RVA: 0x00065508 File Offset: 0x00063708
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F9F RID: 8095
			// (set) Token: 0x06003BC1 RID: 15297 RVA: 0x00065520 File Offset: 0x00063720
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FA0 RID: 8096
			// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x00065538 File Offset: 0x00063738
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FA1 RID: 8097
			// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x00065550 File Offset: 0x00063750
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003ED RID: 1005
		public class RMSOnlineParameters : ParametersBase
		{
			// Token: 0x17001FA2 RID: 8098
			// (set) Token: 0x06003BC5 RID: 15301 RVA: 0x00065570 File Offset: 0x00063770
			public virtual SwitchParameter RefreshTemplates
			{
				set
				{
					base.PowerSharpParameters["RefreshTemplates"] = value;
				}
			}

			// Token: 0x17001FA3 RID: 8099
			// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x00065588 File Offset: 0x00063788
			public virtual SwitchParameter RMSOnline
			{
				set
				{
					base.PowerSharpParameters["RMSOnline"] = value;
				}
			}

			// Token: 0x17001FA4 RID: 8100
			// (set) Token: 0x06003BC7 RID: 15303 RVA: 0x000655A0 File Offset: 0x000637A0
			public virtual Guid RMSOnlineOrgOverride
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineOrgOverride"] = value;
				}
			}

			// Token: 0x17001FA5 RID: 8101
			// (set) Token: 0x06003BC8 RID: 15304 RVA: 0x000655B8 File Offset: 0x000637B8
			public virtual string RMSOnlineAuthCertSubjectNameOverride
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineAuthCertSubjectNameOverride"] = value;
				}
			}

			// Token: 0x17001FA6 RID: 8102
			// (set) Token: 0x06003BC9 RID: 15305 RVA: 0x000655CB File Offset: 0x000637CB
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001FA7 RID: 8103
			// (set) Token: 0x06003BCA RID: 15306 RVA: 0x000655E3 File Offset: 0x000637E3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001FA8 RID: 8104
			// (set) Token: 0x06003BCB RID: 15307 RVA: 0x00065601 File Offset: 0x00063801
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001FA9 RID: 8105
			// (set) Token: 0x06003BCC RID: 15308 RVA: 0x00065614 File Offset: 0x00063814
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FAA RID: 8106
			// (set) Token: 0x06003BCD RID: 15309 RVA: 0x00065627 File Offset: 0x00063827
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FAB RID: 8107
			// (set) Token: 0x06003BCE RID: 15310 RVA: 0x0006563F File Offset: 0x0006383F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FAC RID: 8108
			// (set) Token: 0x06003BCF RID: 15311 RVA: 0x00065657 File Offset: 0x00063857
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FAD RID: 8109
			// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x0006566F File Offset: 0x0006386F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FAE RID: 8110
			// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x00065687 File Offset: 0x00063887
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003EE RID: 1006
		public class RMSOnline2Parameters : ParametersBase
		{
			// Token: 0x17001FAF RID: 8111
			// (set) Token: 0x06003BD3 RID: 15315 RVA: 0x000656A7 File Offset: 0x000638A7
			public virtual byte RMSOnlineConfig
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineConfig"] = value;
				}
			}

			// Token: 0x17001FB0 RID: 8112
			// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000656BF File Offset: 0x000638BF
			public virtual Hashtable RMSOnlineKeys
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineKeys"] = value;
				}
			}

			// Token: 0x17001FB1 RID: 8113
			// (set) Token: 0x06003BD5 RID: 15317 RVA: 0x000656D2 File Offset: 0x000638D2
			public virtual Hashtable RMSOnlineAuthorTest
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineAuthorTest"] = value;
				}
			}

			// Token: 0x17001FB2 RID: 8114
			// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x000656E5 File Offset: 0x000638E5
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001FB3 RID: 8115
			// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x000656FD File Offset: 0x000638FD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001FB4 RID: 8116
			// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x0006571B File Offset: 0x0006391B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001FB5 RID: 8117
			// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x0006572E File Offset: 0x0006392E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FB6 RID: 8118
			// (set) Token: 0x06003BDA RID: 15322 RVA: 0x00065741 File Offset: 0x00063941
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FB7 RID: 8119
			// (set) Token: 0x06003BDB RID: 15323 RVA: 0x00065759 File Offset: 0x00063959
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FB8 RID: 8120
			// (set) Token: 0x06003BDC RID: 15324 RVA: 0x00065771 File Offset: 0x00063971
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FB9 RID: 8121
			// (set) Token: 0x06003BDD RID: 15325 RVA: 0x00065789 File Offset: 0x00063989
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FBA RID: 8122
			// (set) Token: 0x06003BDE RID: 15326 RVA: 0x000657A1 File Offset: 0x000639A1
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

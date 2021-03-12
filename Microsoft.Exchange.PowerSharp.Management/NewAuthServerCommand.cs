using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002E6 RID: 742
	public class NewAuthServerCommand : SyntheticCommandWithPipelineInput<AuthServer, AuthServer>
	{
		// Token: 0x06003281 RID: 12929 RVA: 0x000596CD File Offset: 0x000578CD
		private NewAuthServerCommand() : base("New-AuthServer")
		{
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000596DA File Offset: 0x000578DA
		public NewAuthServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000596E9 File Offset: 0x000578E9
		public virtual NewAuthServerCommand SetParameters(NewAuthServerCommand.AppSecretParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000596F3 File Offset: 0x000578F3
		public virtual NewAuthServerCommand SetParameters(NewAuthServerCommand.AuthMetadataUrlParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000596FD File Offset: 0x000578FD
		public virtual NewAuthServerCommand SetParameters(NewAuthServerCommand.NativeClientAuthServerParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00059707 File Offset: 0x00057907
		public virtual NewAuthServerCommand SetParameters(NewAuthServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002E7 RID: 743
		public class AppSecretParameterSetParameters : ParametersBase
		{
			// Token: 0x1700186E RID: 6254
			// (set) Token: 0x06003287 RID: 12935 RVA: 0x00059711 File Offset: 0x00057911
			public virtual string AppSecret
			{
				set
				{
					base.PowerSharpParameters["AppSecret"] = value;
				}
			}

			// Token: 0x1700186F RID: 6255
			// (set) Token: 0x06003288 RID: 12936 RVA: 0x00059724 File Offset: 0x00057924
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x17001870 RID: 6256
			// (set) Token: 0x06003289 RID: 12937 RVA: 0x00059737 File Offset: 0x00057937
			public virtual string TokenIssuingEndpoint
			{
				set
				{
					base.PowerSharpParameters["TokenIssuingEndpoint"] = value;
				}
			}

			// Token: 0x17001871 RID: 6257
			// (set) Token: 0x0600328A RID: 12938 RVA: 0x0005974A File Offset: 0x0005794A
			public virtual string AuthorizationEndpoint
			{
				set
				{
					base.PowerSharpParameters["AuthorizationEndpoint"] = value;
				}
			}

			// Token: 0x17001872 RID: 6258
			// (set) Token: 0x0600328B RID: 12939 RVA: 0x0005975D File Offset: 0x0005795D
			public virtual string ApplicationIdentifier
			{
				set
				{
					base.PowerSharpParameters["ApplicationIdentifier"] = value;
				}
			}

			// Token: 0x17001873 RID: 6259
			// (set) Token: 0x0600328C RID: 12940 RVA: 0x00059770 File Offset: 0x00057970
			public virtual AuthServerType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001874 RID: 6260
			// (set) Token: 0x0600328D RID: 12941 RVA: 0x00059788 File Offset: 0x00057988
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001875 RID: 6261
			// (set) Token: 0x0600328E RID: 12942 RVA: 0x000597A0 File Offset: 0x000579A0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001876 RID: 6262
			// (set) Token: 0x0600328F RID: 12943 RVA: 0x000597B3 File Offset: 0x000579B3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001877 RID: 6263
			// (set) Token: 0x06003290 RID: 12944 RVA: 0x000597C6 File Offset: 0x000579C6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001878 RID: 6264
			// (set) Token: 0x06003291 RID: 12945 RVA: 0x000597DE File Offset: 0x000579DE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001879 RID: 6265
			// (set) Token: 0x06003292 RID: 12946 RVA: 0x000597F6 File Offset: 0x000579F6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700187A RID: 6266
			// (set) Token: 0x06003293 RID: 12947 RVA: 0x0005980E File Offset: 0x00057A0E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700187B RID: 6267
			// (set) Token: 0x06003294 RID: 12948 RVA: 0x00059826 File Offset: 0x00057A26
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002E8 RID: 744
		public class AuthMetadataUrlParameterSetParameters : ParametersBase
		{
			// Token: 0x1700187C RID: 6268
			// (set) Token: 0x06003296 RID: 12950 RVA: 0x00059846 File Offset: 0x00057A46
			public virtual string AuthMetadataUrl
			{
				set
				{
					base.PowerSharpParameters["AuthMetadataUrl"] = value;
				}
			}

			// Token: 0x1700187D RID: 6269
			// (set) Token: 0x06003297 RID: 12951 RVA: 0x00059859 File Offset: 0x00057A59
			public virtual SwitchParameter TrustAnySSLCertificate
			{
				set
				{
					base.PowerSharpParameters["TrustAnySSLCertificate"] = value;
				}
			}

			// Token: 0x1700187E RID: 6270
			// (set) Token: 0x06003298 RID: 12952 RVA: 0x00059871 File Offset: 0x00057A71
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700187F RID: 6271
			// (set) Token: 0x06003299 RID: 12953 RVA: 0x00059889 File Offset: 0x00057A89
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001880 RID: 6272
			// (set) Token: 0x0600329A RID: 12954 RVA: 0x0005989C File Offset: 0x00057A9C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001881 RID: 6273
			// (set) Token: 0x0600329B RID: 12955 RVA: 0x000598AF File Offset: 0x00057AAF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001882 RID: 6274
			// (set) Token: 0x0600329C RID: 12956 RVA: 0x000598C7 File Offset: 0x00057AC7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001883 RID: 6275
			// (set) Token: 0x0600329D RID: 12957 RVA: 0x000598DF File Offset: 0x00057ADF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001884 RID: 6276
			// (set) Token: 0x0600329E RID: 12958 RVA: 0x000598F7 File Offset: 0x00057AF7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001885 RID: 6277
			// (set) Token: 0x0600329F RID: 12959 RVA: 0x0005990F File Offset: 0x00057B0F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002E9 RID: 745
		public class NativeClientAuthServerParameterSetParameters : ParametersBase
		{
			// Token: 0x17001886 RID: 6278
			// (set) Token: 0x060032A1 RID: 12961 RVA: 0x0005992F File Offset: 0x00057B2F
			public virtual string AuthMetadataUrl
			{
				set
				{
					base.PowerSharpParameters["AuthMetadataUrl"] = value;
				}
			}

			// Token: 0x17001887 RID: 6279
			// (set) Token: 0x060032A2 RID: 12962 RVA: 0x00059942 File Offset: 0x00057B42
			public virtual SwitchParameter TrustAnySSLCertificate
			{
				set
				{
					base.PowerSharpParameters["TrustAnySSLCertificate"] = value;
				}
			}

			// Token: 0x17001888 RID: 6280
			// (set) Token: 0x060032A3 RID: 12963 RVA: 0x0005995A File Offset: 0x00057B5A
			public virtual AuthServerType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001889 RID: 6281
			// (set) Token: 0x060032A4 RID: 12964 RVA: 0x00059972 File Offset: 0x00057B72
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700188A RID: 6282
			// (set) Token: 0x060032A5 RID: 12965 RVA: 0x0005998A File Offset: 0x00057B8A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700188B RID: 6283
			// (set) Token: 0x060032A6 RID: 12966 RVA: 0x0005999D File Offset: 0x00057B9D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700188C RID: 6284
			// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000599B0 File Offset: 0x00057BB0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700188D RID: 6285
			// (set) Token: 0x060032A8 RID: 12968 RVA: 0x000599C8 File Offset: 0x00057BC8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700188E RID: 6286
			// (set) Token: 0x060032A9 RID: 12969 RVA: 0x000599E0 File Offset: 0x00057BE0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700188F RID: 6287
			// (set) Token: 0x060032AA RID: 12970 RVA: 0x000599F8 File Offset: 0x00057BF8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001890 RID: 6288
			// (set) Token: 0x060032AB RID: 12971 RVA: 0x00059A10 File Offset: 0x00057C10
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002EA RID: 746
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001891 RID: 6289
			// (set) Token: 0x060032AD RID: 12973 RVA: 0x00059A30 File Offset: 0x00057C30
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001892 RID: 6290
			// (set) Token: 0x060032AE RID: 12974 RVA: 0x00059A48 File Offset: 0x00057C48
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001893 RID: 6291
			// (set) Token: 0x060032AF RID: 12975 RVA: 0x00059A5B File Offset: 0x00057C5B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001894 RID: 6292
			// (set) Token: 0x060032B0 RID: 12976 RVA: 0x00059A6E File Offset: 0x00057C6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001895 RID: 6293
			// (set) Token: 0x060032B1 RID: 12977 RVA: 0x00059A86 File Offset: 0x00057C86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001896 RID: 6294
			// (set) Token: 0x060032B2 RID: 12978 RVA: 0x00059A9E File Offset: 0x00057C9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001897 RID: 6295
			// (set) Token: 0x060032B3 RID: 12979 RVA: 0x00059AB6 File Offset: 0x00057CB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001898 RID: 6296
			// (set) Token: 0x060032B4 RID: 12980 RVA: 0x00059ACE File Offset: 0x00057CCE
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

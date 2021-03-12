using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002D1 RID: 721
	public class SetAuthConfigCommand : SyntheticCommandWithPipelineInputNoOutput<AuthConfig>
	{
		// Token: 0x060031A4 RID: 12708 RVA: 0x00058505 File Offset: 0x00056705
		private SetAuthConfigCommand() : base("Set-AuthConfig")
		{
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00058512 File Offset: 0x00056712
		public SetAuthConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x00058521 File Offset: 0x00056721
		public virtual SetAuthConfigCommand SetParameters(SetAuthConfigCommand.AuthConfigSettingsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x0005852B File Offset: 0x0005672B
		public virtual SetAuthConfigCommand SetParameters(SetAuthConfigCommand.CurrentCertificateParameterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00058535 File Offset: 0x00056735
		public virtual SetAuthConfigCommand SetParameters(SetAuthConfigCommand.NewCertificateParameterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x0005853F File Offset: 0x0005673F
		public virtual SetAuthConfigCommand SetParameters(SetAuthConfigCommand.PublishAuthCertificateParameterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x00058549 File Offset: 0x00056749
		public virtual SetAuthConfigCommand SetParameters(SetAuthConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002D2 RID: 722
		public class AuthConfigSettingsParameters : ParametersBase
		{
			// Token: 0x170017BB RID: 6075
			// (set) Token: 0x060031AB RID: 12715 RVA: 0x00058553 File Offset: 0x00056753
			public virtual string ServiceName
			{
				set
				{
					base.PowerSharpParameters["ServiceName"] = value;
				}
			}

			// Token: 0x170017BC RID: 6076
			// (set) Token: 0x060031AC RID: 12716 RVA: 0x00058566 File Offset: 0x00056766
			public virtual string Realm
			{
				set
				{
					base.PowerSharpParameters["Realm"] = value;
				}
			}

			// Token: 0x170017BD RID: 6077
			// (set) Token: 0x060031AD RID: 12717 RVA: 0x00058579 File Offset: 0x00056779
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017BE RID: 6078
			// (set) Token: 0x060031AE RID: 12718 RVA: 0x0005858C File Offset: 0x0005678C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017BF RID: 6079
			// (set) Token: 0x060031AF RID: 12719 RVA: 0x000585A4 File Offset: 0x000567A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017C0 RID: 6080
			// (set) Token: 0x060031B0 RID: 12720 RVA: 0x000585BC File Offset: 0x000567BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017C1 RID: 6081
			// (set) Token: 0x060031B1 RID: 12721 RVA: 0x000585D4 File Offset: 0x000567D4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017C2 RID: 6082
			// (set) Token: 0x060031B2 RID: 12722 RVA: 0x000585EC File Offset: 0x000567EC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002D3 RID: 723
		public class CurrentCertificateParameterParameters : ParametersBase
		{
			// Token: 0x170017C3 RID: 6083
			// (set) Token: 0x060031B4 RID: 12724 RVA: 0x0005860C File Offset: 0x0005680C
			public virtual string CertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["CertificateThumbprint"] = value;
				}
			}

			// Token: 0x170017C4 RID: 6084
			// (set) Token: 0x060031B5 RID: 12725 RVA: 0x0005861F File Offset: 0x0005681F
			public virtual SwitchParameter SkipImmediateCertificateDeployment
			{
				set
				{
					base.PowerSharpParameters["SkipImmediateCertificateDeployment"] = value;
				}
			}

			// Token: 0x170017C5 RID: 6085
			// (set) Token: 0x060031B6 RID: 12726 RVA: 0x00058637 File Offset: 0x00056837
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170017C6 RID: 6086
			// (set) Token: 0x060031B7 RID: 12727 RVA: 0x0005864A File Offset: 0x0005684A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170017C7 RID: 6087
			// (set) Token: 0x060031B8 RID: 12728 RVA: 0x00058662 File Offset: 0x00056862
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017C8 RID: 6088
			// (set) Token: 0x060031B9 RID: 12729 RVA: 0x00058675 File Offset: 0x00056875
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017C9 RID: 6089
			// (set) Token: 0x060031BA RID: 12730 RVA: 0x0005868D File Offset: 0x0005688D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017CA RID: 6090
			// (set) Token: 0x060031BB RID: 12731 RVA: 0x000586A5 File Offset: 0x000568A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017CB RID: 6091
			// (set) Token: 0x060031BC RID: 12732 RVA: 0x000586BD File Offset: 0x000568BD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017CC RID: 6092
			// (set) Token: 0x060031BD RID: 12733 RVA: 0x000586D5 File Offset: 0x000568D5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002D4 RID: 724
		public class NewCertificateParameterParameters : ParametersBase
		{
			// Token: 0x170017CD RID: 6093
			// (set) Token: 0x060031BF RID: 12735 RVA: 0x000586F5 File Offset: 0x000568F5
			public virtual string NewCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["NewCertificateThumbprint"] = value;
				}
			}

			// Token: 0x170017CE RID: 6094
			// (set) Token: 0x060031C0 RID: 12736 RVA: 0x00058708 File Offset: 0x00056908
			public virtual DateTime? NewCertificateEffectiveDate
			{
				set
				{
					base.PowerSharpParameters["NewCertificateEffectiveDate"] = value;
				}
			}

			// Token: 0x170017CF RID: 6095
			// (set) Token: 0x060031C1 RID: 12737 RVA: 0x00058720 File Offset: 0x00056920
			public virtual SwitchParameter SkipImmediateCertificateDeployment
			{
				set
				{
					base.PowerSharpParameters["SkipImmediateCertificateDeployment"] = value;
				}
			}

			// Token: 0x170017D0 RID: 6096
			// (set) Token: 0x060031C2 RID: 12738 RVA: 0x00058738 File Offset: 0x00056938
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170017D1 RID: 6097
			// (set) Token: 0x060031C3 RID: 12739 RVA: 0x0005874B File Offset: 0x0005694B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170017D2 RID: 6098
			// (set) Token: 0x060031C4 RID: 12740 RVA: 0x00058763 File Offset: 0x00056963
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017D3 RID: 6099
			// (set) Token: 0x060031C5 RID: 12741 RVA: 0x00058776 File Offset: 0x00056976
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017D4 RID: 6100
			// (set) Token: 0x060031C6 RID: 12742 RVA: 0x0005878E File Offset: 0x0005698E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017D5 RID: 6101
			// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000587A6 File Offset: 0x000569A6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017D6 RID: 6102
			// (set) Token: 0x060031C8 RID: 12744 RVA: 0x000587BE File Offset: 0x000569BE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017D7 RID: 6103
			// (set) Token: 0x060031C9 RID: 12745 RVA: 0x000587D6 File Offset: 0x000569D6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002D5 RID: 725
		public class PublishAuthCertificateParameterParameters : ParametersBase
		{
			// Token: 0x170017D8 RID: 6104
			// (set) Token: 0x060031CB RID: 12747 RVA: 0x000587F6 File Offset: 0x000569F6
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170017D9 RID: 6105
			// (set) Token: 0x060031CC RID: 12748 RVA: 0x0005880E File Offset: 0x00056A0E
			public virtual SwitchParameter PublishCertificate
			{
				set
				{
					base.PowerSharpParameters["PublishCertificate"] = value;
				}
			}

			// Token: 0x170017DA RID: 6106
			// (set) Token: 0x060031CD RID: 12749 RVA: 0x00058826 File Offset: 0x00056A26
			public virtual SwitchParameter ClearPreviousCertificate
			{
				set
				{
					base.PowerSharpParameters["ClearPreviousCertificate"] = value;
				}
			}

			// Token: 0x170017DB RID: 6107
			// (set) Token: 0x060031CE RID: 12750 RVA: 0x0005883E File Offset: 0x00056A3E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017DC RID: 6108
			// (set) Token: 0x060031CF RID: 12751 RVA: 0x00058851 File Offset: 0x00056A51
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017DD RID: 6109
			// (set) Token: 0x060031D0 RID: 12752 RVA: 0x00058869 File Offset: 0x00056A69
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017DE RID: 6110
			// (set) Token: 0x060031D1 RID: 12753 RVA: 0x00058881 File Offset: 0x00056A81
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017DF RID: 6111
			// (set) Token: 0x060031D2 RID: 12754 RVA: 0x00058899 File Offset: 0x00056A99
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017E0 RID: 6112
			// (set) Token: 0x060031D3 RID: 12755 RVA: 0x000588B1 File Offset: 0x00056AB1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002D6 RID: 726
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170017E1 RID: 6113
			// (set) Token: 0x060031D5 RID: 12757 RVA: 0x000588D1 File Offset: 0x00056AD1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017E2 RID: 6114
			// (set) Token: 0x060031D6 RID: 12758 RVA: 0x000588E4 File Offset: 0x00056AE4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017E3 RID: 6115
			// (set) Token: 0x060031D7 RID: 12759 RVA: 0x000588FC File Offset: 0x00056AFC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017E4 RID: 6116
			// (set) Token: 0x060031D8 RID: 12760 RVA: 0x00058914 File Offset: 0x00056B14
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017E5 RID: 6117
			// (set) Token: 0x060031D9 RID: 12761 RVA: 0x0005892C File Offset: 0x00056B2C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017E6 RID: 6118
			// (set) Token: 0x060031DA RID: 12762 RVA: 0x00058944 File Offset: 0x00056B44
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

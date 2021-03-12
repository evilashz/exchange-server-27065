using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002DE RID: 734
	public class SetPartnerApplicationCommand : SyntheticCommandWithPipelineInputNoOutput<PartnerApplication>
	{
		// Token: 0x06003225 RID: 12837 RVA: 0x00058F50 File Offset: 0x00057150
		private SetPartnerApplicationCommand() : base("Set-PartnerApplication")
		{
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x00058F5D File Offset: 0x0005715D
		public SetPartnerApplicationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x00058F6C File Offset: 0x0005716C
		public virtual SetPartnerApplicationCommand SetParameters(SetPartnerApplicationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00058F76 File Offset: 0x00057176
		public virtual SetPartnerApplicationCommand SetParameters(SetPartnerApplicationCommand.ACSTrustApplicationParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x00058F80 File Offset: 0x00057180
		public virtual SetPartnerApplicationCommand SetParameters(SetPartnerApplicationCommand.AuthMetadataUrlParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x00058F8A File Offset: 0x0005718A
		public virtual SetPartnerApplicationCommand SetParameters(SetPartnerApplicationCommand.RefreshAuthMetadataParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002DF RID: 735
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001822 RID: 6178
			// (set) Token: 0x0600322B RID: 12843 RVA: 0x00058F94 File Offset: 0x00057194
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PartnerApplicationIdParameter(value) : null);
				}
			}

			// Token: 0x17001823 RID: 6179
			// (set) Token: 0x0600322C RID: 12844 RVA: 0x00058FB2 File Offset: 0x000571B2
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001824 RID: 6180
			// (set) Token: 0x0600322D RID: 12845 RVA: 0x00058FD0 File Offset: 0x000571D0
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x17001825 RID: 6181
			// (set) Token: 0x0600322E RID: 12846 RVA: 0x00058FE3 File Offset: 0x000571E3
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x17001826 RID: 6182
			// (set) Token: 0x0600322F RID: 12847 RVA: 0x00058FF6 File Offset: 0x000571F6
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x17001827 RID: 6183
			// (set) Token: 0x06003230 RID: 12848 RVA: 0x00059009 File Offset: 0x00057209
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001828 RID: 6184
			// (set) Token: 0x06003231 RID: 12849 RVA: 0x0005901C File Offset: 0x0005721C
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001829 RID: 6185
			// (set) Token: 0x06003232 RID: 12850 RVA: 0x00059034 File Offset: 0x00057234
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x1700182A RID: 6186
			// (set) Token: 0x06003233 RID: 12851 RVA: 0x0005904C File Offset: 0x0005724C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700182B RID: 6187
			// (set) Token: 0x06003234 RID: 12852 RVA: 0x0005905F File Offset: 0x0005725F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700182C RID: 6188
			// (set) Token: 0x06003235 RID: 12853 RVA: 0x00059077 File Offset: 0x00057277
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700182D RID: 6189
			// (set) Token: 0x06003236 RID: 12854 RVA: 0x0005908F File Offset: 0x0005728F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700182E RID: 6190
			// (set) Token: 0x06003237 RID: 12855 RVA: 0x000590A7 File Offset: 0x000572A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700182F RID: 6191
			// (set) Token: 0x06003238 RID: 12856 RVA: 0x000590BF File Offset: 0x000572BF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002E0 RID: 736
		public class ACSTrustApplicationParameterSetParameters : ParametersBase
		{
			// Token: 0x17001830 RID: 6192
			// (set) Token: 0x0600323A RID: 12858 RVA: 0x000590DF File Offset: 0x000572DF
			public virtual string ApplicationIdentifier
			{
				set
				{
					base.PowerSharpParameters["ApplicationIdentifier"] = value;
				}
			}

			// Token: 0x17001831 RID: 6193
			// (set) Token: 0x0600323B RID: 12859 RVA: 0x000590F2 File Offset: 0x000572F2
			public virtual string Realm
			{
				set
				{
					base.PowerSharpParameters["Realm"] = value;
				}
			}

			// Token: 0x17001832 RID: 6194
			// (set) Token: 0x0600323C RID: 12860 RVA: 0x00059105 File Offset: 0x00057305
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PartnerApplicationIdParameter(value) : null);
				}
			}

			// Token: 0x17001833 RID: 6195
			// (set) Token: 0x0600323D RID: 12861 RVA: 0x00059123 File Offset: 0x00057323
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001834 RID: 6196
			// (set) Token: 0x0600323E RID: 12862 RVA: 0x00059141 File Offset: 0x00057341
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x17001835 RID: 6197
			// (set) Token: 0x0600323F RID: 12863 RVA: 0x00059154 File Offset: 0x00057354
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x17001836 RID: 6198
			// (set) Token: 0x06003240 RID: 12864 RVA: 0x00059167 File Offset: 0x00057367
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x17001837 RID: 6199
			// (set) Token: 0x06003241 RID: 12865 RVA: 0x0005917A File Offset: 0x0005737A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001838 RID: 6200
			// (set) Token: 0x06003242 RID: 12866 RVA: 0x0005918D File Offset: 0x0005738D
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001839 RID: 6201
			// (set) Token: 0x06003243 RID: 12867 RVA: 0x000591A5 File Offset: 0x000573A5
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x1700183A RID: 6202
			// (set) Token: 0x06003244 RID: 12868 RVA: 0x000591BD File Offset: 0x000573BD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700183B RID: 6203
			// (set) Token: 0x06003245 RID: 12869 RVA: 0x000591D0 File Offset: 0x000573D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700183C RID: 6204
			// (set) Token: 0x06003246 RID: 12870 RVA: 0x000591E8 File Offset: 0x000573E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700183D RID: 6205
			// (set) Token: 0x06003247 RID: 12871 RVA: 0x00059200 File Offset: 0x00057400
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700183E RID: 6206
			// (set) Token: 0x06003248 RID: 12872 RVA: 0x00059218 File Offset: 0x00057418
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700183F RID: 6207
			// (set) Token: 0x06003249 RID: 12873 RVA: 0x00059230 File Offset: 0x00057430
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002E1 RID: 737
		public class AuthMetadataUrlParameterSetParameters : ParametersBase
		{
			// Token: 0x17001840 RID: 6208
			// (set) Token: 0x0600324B RID: 12875 RVA: 0x00059250 File Offset: 0x00057450
			public virtual string AuthMetadataUrl
			{
				set
				{
					base.PowerSharpParameters["AuthMetadataUrl"] = value;
				}
			}

			// Token: 0x17001841 RID: 6209
			// (set) Token: 0x0600324C RID: 12876 RVA: 0x00059263 File Offset: 0x00057463
			public virtual SwitchParameter TrustAnySSLCertificate
			{
				set
				{
					base.PowerSharpParameters["TrustAnySSLCertificate"] = value;
				}
			}

			// Token: 0x17001842 RID: 6210
			// (set) Token: 0x0600324D RID: 12877 RVA: 0x0005927B File Offset: 0x0005747B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PartnerApplicationIdParameter(value) : null);
				}
			}

			// Token: 0x17001843 RID: 6211
			// (set) Token: 0x0600324E RID: 12878 RVA: 0x00059299 File Offset: 0x00057499
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001844 RID: 6212
			// (set) Token: 0x0600324F RID: 12879 RVA: 0x000592B7 File Offset: 0x000574B7
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x17001845 RID: 6213
			// (set) Token: 0x06003250 RID: 12880 RVA: 0x000592CA File Offset: 0x000574CA
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x17001846 RID: 6214
			// (set) Token: 0x06003251 RID: 12881 RVA: 0x000592DD File Offset: 0x000574DD
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x17001847 RID: 6215
			// (set) Token: 0x06003252 RID: 12882 RVA: 0x000592F0 File Offset: 0x000574F0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001848 RID: 6216
			// (set) Token: 0x06003253 RID: 12883 RVA: 0x00059303 File Offset: 0x00057503
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001849 RID: 6217
			// (set) Token: 0x06003254 RID: 12884 RVA: 0x0005931B File Offset: 0x0005751B
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x1700184A RID: 6218
			// (set) Token: 0x06003255 RID: 12885 RVA: 0x00059333 File Offset: 0x00057533
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700184B RID: 6219
			// (set) Token: 0x06003256 RID: 12886 RVA: 0x00059346 File Offset: 0x00057546
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700184C RID: 6220
			// (set) Token: 0x06003257 RID: 12887 RVA: 0x0005935E File Offset: 0x0005755E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700184D RID: 6221
			// (set) Token: 0x06003258 RID: 12888 RVA: 0x00059376 File Offset: 0x00057576
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700184E RID: 6222
			// (set) Token: 0x06003259 RID: 12889 RVA: 0x0005938E File Offset: 0x0005758E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700184F RID: 6223
			// (set) Token: 0x0600325A RID: 12890 RVA: 0x000593A6 File Offset: 0x000575A6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002E2 RID: 738
		public class RefreshAuthMetadataParameterSetParameters : ParametersBase
		{
			// Token: 0x17001850 RID: 6224
			// (set) Token: 0x0600325C RID: 12892 RVA: 0x000593C6 File Offset: 0x000575C6
			public virtual SwitchParameter RefreshAuthMetadata
			{
				set
				{
					base.PowerSharpParameters["RefreshAuthMetadata"] = value;
				}
			}

			// Token: 0x17001851 RID: 6225
			// (set) Token: 0x0600325D RID: 12893 RVA: 0x000593DE File Offset: 0x000575DE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PartnerApplicationIdParameter(value) : null);
				}
			}

			// Token: 0x17001852 RID: 6226
			// (set) Token: 0x0600325E RID: 12894 RVA: 0x000593FC File Offset: 0x000575FC
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001853 RID: 6227
			// (set) Token: 0x0600325F RID: 12895 RVA: 0x0005941A File Offset: 0x0005761A
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x17001854 RID: 6228
			// (set) Token: 0x06003260 RID: 12896 RVA: 0x0005942D File Offset: 0x0005762D
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x17001855 RID: 6229
			// (set) Token: 0x06003261 RID: 12897 RVA: 0x00059440 File Offset: 0x00057640
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x17001856 RID: 6230
			// (set) Token: 0x06003262 RID: 12898 RVA: 0x00059453 File Offset: 0x00057653
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001857 RID: 6231
			// (set) Token: 0x06003263 RID: 12899 RVA: 0x00059466 File Offset: 0x00057666
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001858 RID: 6232
			// (set) Token: 0x06003264 RID: 12900 RVA: 0x0005947E File Offset: 0x0005767E
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x17001859 RID: 6233
			// (set) Token: 0x06003265 RID: 12901 RVA: 0x00059496 File Offset: 0x00057696
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700185A RID: 6234
			// (set) Token: 0x06003266 RID: 12902 RVA: 0x000594A9 File Offset: 0x000576A9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700185B RID: 6235
			// (set) Token: 0x06003267 RID: 12903 RVA: 0x000594C1 File Offset: 0x000576C1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700185C RID: 6236
			// (set) Token: 0x06003268 RID: 12904 RVA: 0x000594D9 File Offset: 0x000576D9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700185D RID: 6237
			// (set) Token: 0x06003269 RID: 12905 RVA: 0x000594F1 File Offset: 0x000576F1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700185E RID: 6238
			// (set) Token: 0x0600326A RID: 12906 RVA: 0x00059509 File Offset: 0x00057709
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

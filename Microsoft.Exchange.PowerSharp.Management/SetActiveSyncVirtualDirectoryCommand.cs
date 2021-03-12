using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000989 RID: 2441
	public class SetActiveSyncVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADMobileVirtualDirectory>
	{
		// Token: 0x060079FB RID: 31227 RVA: 0x000B603B File Offset: 0x000B423B
		private SetActiveSyncVirtualDirectoryCommand() : base("Set-ActiveSyncVirtualDirectory")
		{
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x000B6048 File Offset: 0x000B4248
		public SetActiveSyncVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x000B6057 File Offset: 0x000B4257
		public virtual SetActiveSyncVirtualDirectoryCommand SetParameters(SetActiveSyncVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x000B6061 File Offset: 0x000B4261
		public virtual SetActiveSyncVirtualDirectoryCommand SetParameters(SetActiveSyncVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200098A RID: 2442
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170052A2 RID: 21154
			// (set) Token: 0x060079FF RID: 31231 RVA: 0x000B606B File Offset: 0x000B426B
			public virtual string ActiveSyncServer
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncServer"] = value;
				}
			}

			// Token: 0x170052A3 RID: 21155
			// (set) Token: 0x06007A00 RID: 31232 RVA: 0x000B607E File Offset: 0x000B427E
			public virtual bool MobileClientCertificateProvisioningEnabled
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateProvisioningEnabled"] = value;
				}
			}

			// Token: 0x170052A4 RID: 21156
			// (set) Token: 0x06007A01 RID: 31233 RVA: 0x000B6096 File Offset: 0x000B4296
			public virtual bool BadItemReportingEnabled
			{
				set
				{
					base.PowerSharpParameters["BadItemReportingEnabled"] = value;
				}
			}

			// Token: 0x170052A5 RID: 21157
			// (set) Token: 0x06007A02 RID: 31234 RVA: 0x000B60AE File Offset: 0x000B42AE
			public virtual bool SendWatsonReport
			{
				set
				{
					base.PowerSharpParameters["SendWatsonReport"] = value;
				}
			}

			// Token: 0x170052A6 RID: 21158
			// (set) Token: 0x06007A03 RID: 31235 RVA: 0x000B60C6 File Offset: 0x000B42C6
			public virtual string MobileClientCertificateAuthorityURL
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateAuthorityURL"] = value;
				}
			}

			// Token: 0x170052A7 RID: 21159
			// (set) Token: 0x06007A04 RID: 31236 RVA: 0x000B60D9 File Offset: 0x000B42D9
			public virtual string MobileClientCertTemplateName
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertTemplateName"] = value;
				}
			}

			// Token: 0x170052A8 RID: 21160
			// (set) Token: 0x06007A05 RID: 31237 RVA: 0x000B60EC File Offset: 0x000B42EC
			public virtual ClientCertAuthTypes ClientCertAuth
			{
				set
				{
					base.PowerSharpParameters["ClientCertAuth"] = value;
				}
			}

			// Token: 0x170052A9 RID: 21161
			// (set) Token: 0x06007A06 RID: 31238 RVA: 0x000B6104 File Offset: 0x000B4304
			public virtual bool BasicAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["BasicAuthEnabled"] = value;
				}
			}

			// Token: 0x170052AA RID: 21162
			// (set) Token: 0x06007A07 RID: 31239 RVA: 0x000B611C File Offset: 0x000B431C
			public virtual bool WindowsAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthEnabled"] = value;
				}
			}

			// Token: 0x170052AB RID: 21163
			// (set) Token: 0x06007A08 RID: 31240 RVA: 0x000B6134 File Offset: 0x000B4334
			public virtual bool CompressionEnabled
			{
				set
				{
					base.PowerSharpParameters["CompressionEnabled"] = value;
				}
			}

			// Token: 0x170052AC RID: 21164
			// (set) Token: 0x06007A09 RID: 31241 RVA: 0x000B614C File Offset: 0x000B434C
			public virtual RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsActionForUnknownServers"] = value;
				}
			}

			// Token: 0x170052AD RID: 21165
			// (set) Token: 0x06007A0A RID: 31242 RVA: 0x000B6164 File Offset: 0x000B4364
			public virtual MultiValuedProperty<string> RemoteDocumentsAllowedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsAllowedServers"] = value;
				}
			}

			// Token: 0x170052AE RID: 21166
			// (set) Token: 0x06007A0B RID: 31243 RVA: 0x000B6177 File Offset: 0x000B4377
			public virtual MultiValuedProperty<string> RemoteDocumentsBlockedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsBlockedServers"] = value;
				}
			}

			// Token: 0x170052AF RID: 21167
			// (set) Token: 0x06007A0C RID: 31244 RVA: 0x000B618A File Offset: 0x000B438A
			public virtual MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsInternalDomainSuffixList"] = value;
				}
			}

			// Token: 0x170052B0 RID: 21168
			// (set) Token: 0x06007A0D RID: 31245 RVA: 0x000B619D File Offset: 0x000B439D
			public virtual bool InstallIsapiFilter
			{
				set
				{
					base.PowerSharpParameters["InstallIsapiFilter"] = value;
				}
			}

			// Token: 0x170052B1 RID: 21169
			// (set) Token: 0x06007A0E RID: 31246 RVA: 0x000B61B5 File Offset: 0x000B43B5
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170052B2 RID: 21170
			// (set) Token: 0x06007A0F RID: 31247 RVA: 0x000B61CD File Offset: 0x000B43CD
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170052B3 RID: 21171
			// (set) Token: 0x06007A10 RID: 31248 RVA: 0x000B61E0 File Offset: 0x000B43E0
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170052B4 RID: 21172
			// (set) Token: 0x06007A11 RID: 31249 RVA: 0x000B61F3 File Offset: 0x000B43F3
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170052B5 RID: 21173
			// (set) Token: 0x06007A12 RID: 31250 RVA: 0x000B6206 File Offset: 0x000B4406
			public virtual MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["InternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170052B6 RID: 21174
			// (set) Token: 0x06007A13 RID: 31251 RVA: 0x000B6219 File Offset: 0x000B4419
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170052B7 RID: 21175
			// (set) Token: 0x06007A14 RID: 31252 RVA: 0x000B622C File Offset: 0x000B442C
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170052B8 RID: 21176
			// (set) Token: 0x06007A15 RID: 31253 RVA: 0x000B623F File Offset: 0x000B443F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170052B9 RID: 21177
			// (set) Token: 0x06007A16 RID: 31254 RVA: 0x000B6252 File Offset: 0x000B4452
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170052BA RID: 21178
			// (set) Token: 0x06007A17 RID: 31255 RVA: 0x000B6265 File Offset: 0x000B4465
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170052BB RID: 21179
			// (set) Token: 0x06007A18 RID: 31256 RVA: 0x000B627D File Offset: 0x000B447D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170052BC RID: 21180
			// (set) Token: 0x06007A19 RID: 31257 RVA: 0x000B6295 File Offset: 0x000B4495
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170052BD RID: 21181
			// (set) Token: 0x06007A1A RID: 31258 RVA: 0x000B62AD File Offset: 0x000B44AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170052BE RID: 21182
			// (set) Token: 0x06007A1B RID: 31259 RVA: 0x000B62C5 File Offset: 0x000B44C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200098B RID: 2443
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170052BF RID: 21183
			// (set) Token: 0x06007A1D RID: 31261 RVA: 0x000B62E5 File Offset: 0x000B44E5
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170052C0 RID: 21184
			// (set) Token: 0x06007A1E RID: 31262 RVA: 0x000B62F8 File Offset: 0x000B44F8
			public virtual string ActiveSyncServer
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncServer"] = value;
				}
			}

			// Token: 0x170052C1 RID: 21185
			// (set) Token: 0x06007A1F RID: 31263 RVA: 0x000B630B File Offset: 0x000B450B
			public virtual bool MobileClientCertificateProvisioningEnabled
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateProvisioningEnabled"] = value;
				}
			}

			// Token: 0x170052C2 RID: 21186
			// (set) Token: 0x06007A20 RID: 31264 RVA: 0x000B6323 File Offset: 0x000B4523
			public virtual bool BadItemReportingEnabled
			{
				set
				{
					base.PowerSharpParameters["BadItemReportingEnabled"] = value;
				}
			}

			// Token: 0x170052C3 RID: 21187
			// (set) Token: 0x06007A21 RID: 31265 RVA: 0x000B633B File Offset: 0x000B453B
			public virtual bool SendWatsonReport
			{
				set
				{
					base.PowerSharpParameters["SendWatsonReport"] = value;
				}
			}

			// Token: 0x170052C4 RID: 21188
			// (set) Token: 0x06007A22 RID: 31266 RVA: 0x000B6353 File Offset: 0x000B4553
			public virtual string MobileClientCertificateAuthorityURL
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateAuthorityURL"] = value;
				}
			}

			// Token: 0x170052C5 RID: 21189
			// (set) Token: 0x06007A23 RID: 31267 RVA: 0x000B6366 File Offset: 0x000B4566
			public virtual string MobileClientCertTemplateName
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertTemplateName"] = value;
				}
			}

			// Token: 0x170052C6 RID: 21190
			// (set) Token: 0x06007A24 RID: 31268 RVA: 0x000B6379 File Offset: 0x000B4579
			public virtual ClientCertAuthTypes ClientCertAuth
			{
				set
				{
					base.PowerSharpParameters["ClientCertAuth"] = value;
				}
			}

			// Token: 0x170052C7 RID: 21191
			// (set) Token: 0x06007A25 RID: 31269 RVA: 0x000B6391 File Offset: 0x000B4591
			public virtual bool BasicAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["BasicAuthEnabled"] = value;
				}
			}

			// Token: 0x170052C8 RID: 21192
			// (set) Token: 0x06007A26 RID: 31270 RVA: 0x000B63A9 File Offset: 0x000B45A9
			public virtual bool WindowsAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthEnabled"] = value;
				}
			}

			// Token: 0x170052C9 RID: 21193
			// (set) Token: 0x06007A27 RID: 31271 RVA: 0x000B63C1 File Offset: 0x000B45C1
			public virtual bool CompressionEnabled
			{
				set
				{
					base.PowerSharpParameters["CompressionEnabled"] = value;
				}
			}

			// Token: 0x170052CA RID: 21194
			// (set) Token: 0x06007A28 RID: 31272 RVA: 0x000B63D9 File Offset: 0x000B45D9
			public virtual RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsActionForUnknownServers"] = value;
				}
			}

			// Token: 0x170052CB RID: 21195
			// (set) Token: 0x06007A29 RID: 31273 RVA: 0x000B63F1 File Offset: 0x000B45F1
			public virtual MultiValuedProperty<string> RemoteDocumentsAllowedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsAllowedServers"] = value;
				}
			}

			// Token: 0x170052CC RID: 21196
			// (set) Token: 0x06007A2A RID: 31274 RVA: 0x000B6404 File Offset: 0x000B4604
			public virtual MultiValuedProperty<string> RemoteDocumentsBlockedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsBlockedServers"] = value;
				}
			}

			// Token: 0x170052CD RID: 21197
			// (set) Token: 0x06007A2B RID: 31275 RVA: 0x000B6417 File Offset: 0x000B4617
			public virtual MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsInternalDomainSuffixList"] = value;
				}
			}

			// Token: 0x170052CE RID: 21198
			// (set) Token: 0x06007A2C RID: 31276 RVA: 0x000B642A File Offset: 0x000B462A
			public virtual bool InstallIsapiFilter
			{
				set
				{
					base.PowerSharpParameters["InstallIsapiFilter"] = value;
				}
			}

			// Token: 0x170052CF RID: 21199
			// (set) Token: 0x06007A2D RID: 31277 RVA: 0x000B6442 File Offset: 0x000B4642
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170052D0 RID: 21200
			// (set) Token: 0x06007A2E RID: 31278 RVA: 0x000B645A File Offset: 0x000B465A
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170052D1 RID: 21201
			// (set) Token: 0x06007A2F RID: 31279 RVA: 0x000B646D File Offset: 0x000B466D
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170052D2 RID: 21202
			// (set) Token: 0x06007A30 RID: 31280 RVA: 0x000B6480 File Offset: 0x000B4680
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170052D3 RID: 21203
			// (set) Token: 0x06007A31 RID: 31281 RVA: 0x000B6493 File Offset: 0x000B4693
			public virtual MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["InternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170052D4 RID: 21204
			// (set) Token: 0x06007A32 RID: 31282 RVA: 0x000B64A6 File Offset: 0x000B46A6
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170052D5 RID: 21205
			// (set) Token: 0x06007A33 RID: 31283 RVA: 0x000B64B9 File Offset: 0x000B46B9
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170052D6 RID: 21206
			// (set) Token: 0x06007A34 RID: 31284 RVA: 0x000B64CC File Offset: 0x000B46CC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170052D7 RID: 21207
			// (set) Token: 0x06007A35 RID: 31285 RVA: 0x000B64DF File Offset: 0x000B46DF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170052D8 RID: 21208
			// (set) Token: 0x06007A36 RID: 31286 RVA: 0x000B64F2 File Offset: 0x000B46F2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170052D9 RID: 21209
			// (set) Token: 0x06007A37 RID: 31287 RVA: 0x000B650A File Offset: 0x000B470A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170052DA RID: 21210
			// (set) Token: 0x06007A38 RID: 31288 RVA: 0x000B6522 File Offset: 0x000B4722
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170052DB RID: 21211
			// (set) Token: 0x06007A39 RID: 31289 RVA: 0x000B653A File Offset: 0x000B473A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170052DC RID: 21212
			// (set) Token: 0x06007A3A RID: 31290 RVA: 0x000B6552 File Offset: 0x000B4752
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

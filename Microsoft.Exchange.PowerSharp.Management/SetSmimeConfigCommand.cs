using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000826 RID: 2086
	public class SetSmimeConfigCommand : SyntheticCommandWithPipelineInputNoOutput<SmimeConfigurationContainer>
	{
		// Token: 0x0600681E RID: 26654 RVA: 0x0009E93E File Offset: 0x0009CB3E
		private SetSmimeConfigCommand() : base("Set-SmimeConfig")
		{
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x0009E94B File Offset: 0x0009CB4B
		public SetSmimeConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x0009E95A File Offset: 0x0009CB5A
		public virtual SetSmimeConfigCommand SetParameters(SetSmimeConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x0009E964 File Offset: 0x0009CB64
		public virtual SetSmimeConfigCommand SetParameters(SetSmimeConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000827 RID: 2087
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700438B RID: 17291
			// (set) Token: 0x06006822 RID: 26658 RVA: 0x0009E96E File Offset: 0x0009CB6E
			public virtual bool OWACheckCRLOnSend
			{
				set
				{
					base.PowerSharpParameters["OWACheckCRLOnSend"] = value;
				}
			}

			// Token: 0x1700438C RID: 17292
			// (set) Token: 0x06006823 RID: 26659 RVA: 0x0009E986 File Offset: 0x0009CB86
			public virtual uint OWADLExpansionTimeout
			{
				set
				{
					base.PowerSharpParameters["OWADLExpansionTimeout"] = value;
				}
			}

			// Token: 0x1700438D RID: 17293
			// (set) Token: 0x06006824 RID: 26660 RVA: 0x0009E99E File Offset: 0x0009CB9E
			public virtual bool OWAUseSecondaryProxiesWhenFindingCertificates
			{
				set
				{
					base.PowerSharpParameters["OWAUseSecondaryProxiesWhenFindingCertificates"] = value;
				}
			}

			// Token: 0x1700438E RID: 17294
			// (set) Token: 0x06006825 RID: 26661 RVA: 0x0009E9B6 File Offset: 0x0009CBB6
			public virtual uint OWACRLConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["OWACRLConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700438F RID: 17295
			// (set) Token: 0x06006826 RID: 26662 RVA: 0x0009E9CE File Offset: 0x0009CBCE
			public virtual uint OWACRLRetrievalTimeout
			{
				set
				{
					base.PowerSharpParameters["OWACRLRetrievalTimeout"] = value;
				}
			}

			// Token: 0x17004390 RID: 17296
			// (set) Token: 0x06006827 RID: 26663 RVA: 0x0009E9E6 File Offset: 0x0009CBE6
			public virtual bool OWADisableCRLCheck
			{
				set
				{
					base.PowerSharpParameters["OWADisableCRLCheck"] = value;
				}
			}

			// Token: 0x17004391 RID: 17297
			// (set) Token: 0x06006828 RID: 26664 RVA: 0x0009E9FE File Offset: 0x0009CBFE
			public virtual bool OWAAlwaysSign
			{
				set
				{
					base.PowerSharpParameters["OWAAlwaysSign"] = value;
				}
			}

			// Token: 0x17004392 RID: 17298
			// (set) Token: 0x06006829 RID: 26665 RVA: 0x0009EA16 File Offset: 0x0009CC16
			public virtual bool OWAAlwaysEncrypt
			{
				set
				{
					base.PowerSharpParameters["OWAAlwaysEncrypt"] = value;
				}
			}

			// Token: 0x17004393 RID: 17299
			// (set) Token: 0x0600682A RID: 26666 RVA: 0x0009EA2E File Offset: 0x0009CC2E
			public virtual bool OWAClearSign
			{
				set
				{
					base.PowerSharpParameters["OWAClearSign"] = value;
				}
			}

			// Token: 0x17004394 RID: 17300
			// (set) Token: 0x0600682B RID: 26667 RVA: 0x0009EA46 File Offset: 0x0009CC46
			public virtual bool OWAIncludeCertificateChainWithoutRootCertificate
			{
				set
				{
					base.PowerSharpParameters["OWAIncludeCertificateChainWithoutRootCertificate"] = value;
				}
			}

			// Token: 0x17004395 RID: 17301
			// (set) Token: 0x0600682C RID: 26668 RVA: 0x0009EA5E File Offset: 0x0009CC5E
			public virtual bool OWAIncludeCertificateChainAndRootCertificate
			{
				set
				{
					base.PowerSharpParameters["OWAIncludeCertificateChainAndRootCertificate"] = value;
				}
			}

			// Token: 0x17004396 RID: 17302
			// (set) Token: 0x0600682D RID: 26669 RVA: 0x0009EA76 File Offset: 0x0009CC76
			public virtual bool OWAEncryptTemporaryBuffers
			{
				set
				{
					base.PowerSharpParameters["OWAEncryptTemporaryBuffers"] = value;
				}
			}

			// Token: 0x17004397 RID: 17303
			// (set) Token: 0x0600682E RID: 26670 RVA: 0x0009EA8E File Offset: 0x0009CC8E
			public virtual bool OWASignedEmailCertificateInclusion
			{
				set
				{
					base.PowerSharpParameters["OWASignedEmailCertificateInclusion"] = value;
				}
			}

			// Token: 0x17004398 RID: 17304
			// (set) Token: 0x0600682F RID: 26671 RVA: 0x0009EAA6 File Offset: 0x0009CCA6
			public virtual uint OWABCCEncryptedEmailForking
			{
				set
				{
					base.PowerSharpParameters["OWABCCEncryptedEmailForking"] = value;
				}
			}

			// Token: 0x17004399 RID: 17305
			// (set) Token: 0x06006830 RID: 26672 RVA: 0x0009EABE File Offset: 0x0009CCBE
			public virtual bool OWAIncludeSMIMECapabilitiesInMessage
			{
				set
				{
					base.PowerSharpParameters["OWAIncludeSMIMECapabilitiesInMessage"] = value;
				}
			}

			// Token: 0x1700439A RID: 17306
			// (set) Token: 0x06006831 RID: 26673 RVA: 0x0009EAD6 File Offset: 0x0009CCD6
			public virtual bool OWACopyRecipientHeaders
			{
				set
				{
					base.PowerSharpParameters["OWACopyRecipientHeaders"] = value;
				}
			}

			// Token: 0x1700439B RID: 17307
			// (set) Token: 0x06006832 RID: 26674 RVA: 0x0009EAEE File Offset: 0x0009CCEE
			public virtual bool OWAOnlyUseSmartCard
			{
				set
				{
					base.PowerSharpParameters["OWAOnlyUseSmartCard"] = value;
				}
			}

			// Token: 0x1700439C RID: 17308
			// (set) Token: 0x06006833 RID: 26675 RVA: 0x0009EB06 File Offset: 0x0009CD06
			public virtual bool OWATripleWrapSignedEncryptedMail
			{
				set
				{
					base.PowerSharpParameters["OWATripleWrapSignedEncryptedMail"] = value;
				}
			}

			// Token: 0x1700439D RID: 17309
			// (set) Token: 0x06006834 RID: 26676 RVA: 0x0009EB1E File Offset: 0x0009CD1E
			public virtual bool OWAUseKeyIdentifier
			{
				set
				{
					base.PowerSharpParameters["OWAUseKeyIdentifier"] = value;
				}
			}

			// Token: 0x1700439E RID: 17310
			// (set) Token: 0x06006835 RID: 26677 RVA: 0x0009EB36 File Offset: 0x0009CD36
			public virtual string OWAEncryptionAlgorithms
			{
				set
				{
					base.PowerSharpParameters["OWAEncryptionAlgorithms"] = value;
				}
			}

			// Token: 0x1700439F RID: 17311
			// (set) Token: 0x06006836 RID: 26678 RVA: 0x0009EB49 File Offset: 0x0009CD49
			public virtual string OWASigningAlgorithms
			{
				set
				{
					base.PowerSharpParameters["OWASigningAlgorithms"] = value;
				}
			}

			// Token: 0x170043A0 RID: 17312
			// (set) Token: 0x06006837 RID: 26679 RVA: 0x0009EB5C File Offset: 0x0009CD5C
			public virtual bool OWAForceSMIMEClientUpgrade
			{
				set
				{
					base.PowerSharpParameters["OWAForceSMIMEClientUpgrade"] = value;
				}
			}

			// Token: 0x170043A1 RID: 17313
			// (set) Token: 0x06006838 RID: 26680 RVA: 0x0009EB74 File Offset: 0x0009CD74
			public virtual string OWASenderCertificateAttributesToDisplay
			{
				set
				{
					base.PowerSharpParameters["OWASenderCertificateAttributesToDisplay"] = value;
				}
			}

			// Token: 0x170043A2 RID: 17314
			// (set) Token: 0x06006839 RID: 26681 RVA: 0x0009EB87 File Offset: 0x0009CD87
			public virtual bool OWAAllowUserChoiceOfSigningCertificate
			{
				set
				{
					base.PowerSharpParameters["OWAAllowUserChoiceOfSigningCertificate"] = value;
				}
			}

			// Token: 0x170043A3 RID: 17315
			// (set) Token: 0x0600683A RID: 26682 RVA: 0x0009EB9F File Offset: 0x0009CD9F
			public virtual byte SMIMECertificateIssuingCA
			{
				set
				{
					base.PowerSharpParameters["SMIMECertificateIssuingCA"] = value;
				}
			}

			// Token: 0x170043A4 RID: 17316
			// (set) Token: 0x0600683B RID: 26683 RVA: 0x0009EBB7 File Offset: 0x0009CDB7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043A5 RID: 17317
			// (set) Token: 0x0600683C RID: 26684 RVA: 0x0009EBCA File Offset: 0x0009CDCA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170043A6 RID: 17318
			// (set) Token: 0x0600683D RID: 26685 RVA: 0x0009EBDD File Offset: 0x0009CDDD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043A7 RID: 17319
			// (set) Token: 0x0600683E RID: 26686 RVA: 0x0009EBF5 File Offset: 0x0009CDF5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043A8 RID: 17320
			// (set) Token: 0x0600683F RID: 26687 RVA: 0x0009EC0D File Offset: 0x0009CE0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043A9 RID: 17321
			// (set) Token: 0x06006840 RID: 26688 RVA: 0x0009EC25 File Offset: 0x0009CE25
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170043AA RID: 17322
			// (set) Token: 0x06006841 RID: 26689 RVA: 0x0009EC3D File Offset: 0x0009CE3D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000828 RID: 2088
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170043AB RID: 17323
			// (set) Token: 0x06006843 RID: 26691 RVA: 0x0009EC5D File Offset: 0x0009CE5D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170043AC RID: 17324
			// (set) Token: 0x06006844 RID: 26692 RVA: 0x0009EC7B File Offset: 0x0009CE7B
			public virtual bool OWACheckCRLOnSend
			{
				set
				{
					base.PowerSharpParameters["OWACheckCRLOnSend"] = value;
				}
			}

			// Token: 0x170043AD RID: 17325
			// (set) Token: 0x06006845 RID: 26693 RVA: 0x0009EC93 File Offset: 0x0009CE93
			public virtual uint OWADLExpansionTimeout
			{
				set
				{
					base.PowerSharpParameters["OWADLExpansionTimeout"] = value;
				}
			}

			// Token: 0x170043AE RID: 17326
			// (set) Token: 0x06006846 RID: 26694 RVA: 0x0009ECAB File Offset: 0x0009CEAB
			public virtual bool OWAUseSecondaryProxiesWhenFindingCertificates
			{
				set
				{
					base.PowerSharpParameters["OWAUseSecondaryProxiesWhenFindingCertificates"] = value;
				}
			}

			// Token: 0x170043AF RID: 17327
			// (set) Token: 0x06006847 RID: 26695 RVA: 0x0009ECC3 File Offset: 0x0009CEC3
			public virtual uint OWACRLConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["OWACRLConnectionTimeout"] = value;
				}
			}

			// Token: 0x170043B0 RID: 17328
			// (set) Token: 0x06006848 RID: 26696 RVA: 0x0009ECDB File Offset: 0x0009CEDB
			public virtual uint OWACRLRetrievalTimeout
			{
				set
				{
					base.PowerSharpParameters["OWACRLRetrievalTimeout"] = value;
				}
			}

			// Token: 0x170043B1 RID: 17329
			// (set) Token: 0x06006849 RID: 26697 RVA: 0x0009ECF3 File Offset: 0x0009CEF3
			public virtual bool OWADisableCRLCheck
			{
				set
				{
					base.PowerSharpParameters["OWADisableCRLCheck"] = value;
				}
			}

			// Token: 0x170043B2 RID: 17330
			// (set) Token: 0x0600684A RID: 26698 RVA: 0x0009ED0B File Offset: 0x0009CF0B
			public virtual bool OWAAlwaysSign
			{
				set
				{
					base.PowerSharpParameters["OWAAlwaysSign"] = value;
				}
			}

			// Token: 0x170043B3 RID: 17331
			// (set) Token: 0x0600684B RID: 26699 RVA: 0x0009ED23 File Offset: 0x0009CF23
			public virtual bool OWAAlwaysEncrypt
			{
				set
				{
					base.PowerSharpParameters["OWAAlwaysEncrypt"] = value;
				}
			}

			// Token: 0x170043B4 RID: 17332
			// (set) Token: 0x0600684C RID: 26700 RVA: 0x0009ED3B File Offset: 0x0009CF3B
			public virtual bool OWAClearSign
			{
				set
				{
					base.PowerSharpParameters["OWAClearSign"] = value;
				}
			}

			// Token: 0x170043B5 RID: 17333
			// (set) Token: 0x0600684D RID: 26701 RVA: 0x0009ED53 File Offset: 0x0009CF53
			public virtual bool OWAIncludeCertificateChainWithoutRootCertificate
			{
				set
				{
					base.PowerSharpParameters["OWAIncludeCertificateChainWithoutRootCertificate"] = value;
				}
			}

			// Token: 0x170043B6 RID: 17334
			// (set) Token: 0x0600684E RID: 26702 RVA: 0x0009ED6B File Offset: 0x0009CF6B
			public virtual bool OWAIncludeCertificateChainAndRootCertificate
			{
				set
				{
					base.PowerSharpParameters["OWAIncludeCertificateChainAndRootCertificate"] = value;
				}
			}

			// Token: 0x170043B7 RID: 17335
			// (set) Token: 0x0600684F RID: 26703 RVA: 0x0009ED83 File Offset: 0x0009CF83
			public virtual bool OWAEncryptTemporaryBuffers
			{
				set
				{
					base.PowerSharpParameters["OWAEncryptTemporaryBuffers"] = value;
				}
			}

			// Token: 0x170043B8 RID: 17336
			// (set) Token: 0x06006850 RID: 26704 RVA: 0x0009ED9B File Offset: 0x0009CF9B
			public virtual bool OWASignedEmailCertificateInclusion
			{
				set
				{
					base.PowerSharpParameters["OWASignedEmailCertificateInclusion"] = value;
				}
			}

			// Token: 0x170043B9 RID: 17337
			// (set) Token: 0x06006851 RID: 26705 RVA: 0x0009EDB3 File Offset: 0x0009CFB3
			public virtual uint OWABCCEncryptedEmailForking
			{
				set
				{
					base.PowerSharpParameters["OWABCCEncryptedEmailForking"] = value;
				}
			}

			// Token: 0x170043BA RID: 17338
			// (set) Token: 0x06006852 RID: 26706 RVA: 0x0009EDCB File Offset: 0x0009CFCB
			public virtual bool OWAIncludeSMIMECapabilitiesInMessage
			{
				set
				{
					base.PowerSharpParameters["OWAIncludeSMIMECapabilitiesInMessage"] = value;
				}
			}

			// Token: 0x170043BB RID: 17339
			// (set) Token: 0x06006853 RID: 26707 RVA: 0x0009EDE3 File Offset: 0x0009CFE3
			public virtual bool OWACopyRecipientHeaders
			{
				set
				{
					base.PowerSharpParameters["OWACopyRecipientHeaders"] = value;
				}
			}

			// Token: 0x170043BC RID: 17340
			// (set) Token: 0x06006854 RID: 26708 RVA: 0x0009EDFB File Offset: 0x0009CFFB
			public virtual bool OWAOnlyUseSmartCard
			{
				set
				{
					base.PowerSharpParameters["OWAOnlyUseSmartCard"] = value;
				}
			}

			// Token: 0x170043BD RID: 17341
			// (set) Token: 0x06006855 RID: 26709 RVA: 0x0009EE13 File Offset: 0x0009D013
			public virtual bool OWATripleWrapSignedEncryptedMail
			{
				set
				{
					base.PowerSharpParameters["OWATripleWrapSignedEncryptedMail"] = value;
				}
			}

			// Token: 0x170043BE RID: 17342
			// (set) Token: 0x06006856 RID: 26710 RVA: 0x0009EE2B File Offset: 0x0009D02B
			public virtual bool OWAUseKeyIdentifier
			{
				set
				{
					base.PowerSharpParameters["OWAUseKeyIdentifier"] = value;
				}
			}

			// Token: 0x170043BF RID: 17343
			// (set) Token: 0x06006857 RID: 26711 RVA: 0x0009EE43 File Offset: 0x0009D043
			public virtual string OWAEncryptionAlgorithms
			{
				set
				{
					base.PowerSharpParameters["OWAEncryptionAlgorithms"] = value;
				}
			}

			// Token: 0x170043C0 RID: 17344
			// (set) Token: 0x06006858 RID: 26712 RVA: 0x0009EE56 File Offset: 0x0009D056
			public virtual string OWASigningAlgorithms
			{
				set
				{
					base.PowerSharpParameters["OWASigningAlgorithms"] = value;
				}
			}

			// Token: 0x170043C1 RID: 17345
			// (set) Token: 0x06006859 RID: 26713 RVA: 0x0009EE69 File Offset: 0x0009D069
			public virtual bool OWAForceSMIMEClientUpgrade
			{
				set
				{
					base.PowerSharpParameters["OWAForceSMIMEClientUpgrade"] = value;
				}
			}

			// Token: 0x170043C2 RID: 17346
			// (set) Token: 0x0600685A RID: 26714 RVA: 0x0009EE81 File Offset: 0x0009D081
			public virtual string OWASenderCertificateAttributesToDisplay
			{
				set
				{
					base.PowerSharpParameters["OWASenderCertificateAttributesToDisplay"] = value;
				}
			}

			// Token: 0x170043C3 RID: 17347
			// (set) Token: 0x0600685B RID: 26715 RVA: 0x0009EE94 File Offset: 0x0009D094
			public virtual bool OWAAllowUserChoiceOfSigningCertificate
			{
				set
				{
					base.PowerSharpParameters["OWAAllowUserChoiceOfSigningCertificate"] = value;
				}
			}

			// Token: 0x170043C4 RID: 17348
			// (set) Token: 0x0600685C RID: 26716 RVA: 0x0009EEAC File Offset: 0x0009D0AC
			public virtual byte SMIMECertificateIssuingCA
			{
				set
				{
					base.PowerSharpParameters["SMIMECertificateIssuingCA"] = value;
				}
			}

			// Token: 0x170043C5 RID: 17349
			// (set) Token: 0x0600685D RID: 26717 RVA: 0x0009EEC4 File Offset: 0x0009D0C4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043C6 RID: 17350
			// (set) Token: 0x0600685E RID: 26718 RVA: 0x0009EED7 File Offset: 0x0009D0D7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170043C7 RID: 17351
			// (set) Token: 0x0600685F RID: 26719 RVA: 0x0009EEEA File Offset: 0x0009D0EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043C8 RID: 17352
			// (set) Token: 0x06006860 RID: 26720 RVA: 0x0009EF02 File Offset: 0x0009D102
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043C9 RID: 17353
			// (set) Token: 0x06006861 RID: 26721 RVA: 0x0009EF1A File Offset: 0x0009D11A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043CA RID: 17354
			// (set) Token: 0x06006862 RID: 26722 RVA: 0x0009EF32 File Offset: 0x0009D132
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170043CB RID: 17355
			// (set) Token: 0x06006863 RID: 26723 RVA: 0x0009EF4A File Offset: 0x0009D14A
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

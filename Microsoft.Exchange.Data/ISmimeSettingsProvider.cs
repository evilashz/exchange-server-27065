using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public interface ISmimeSettingsProvider
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000BC2 RID: 3010
		bool OWACheckCRLOnSend { get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000BC3 RID: 3011
		uint OWADLExpansionTimeout { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000BC4 RID: 3012
		bool OWAUseSecondaryProxiesWhenFindingCertificates { get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000BC5 RID: 3013
		uint OWACRLConnectionTimeout { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000BC6 RID: 3014
		uint OWACRLRetrievalTimeout { get; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000BC7 RID: 3015
		bool OWADisableCRLCheck { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000BC8 RID: 3016
		bool OWAAlwaysSign { get; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000BC9 RID: 3017
		bool OWAAlwaysEncrypt { get; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000BCA RID: 3018
		bool OWAClearSign { get; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000BCB RID: 3019
		bool OWAIncludeCertificateChainWithoutRootCertificate { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000BCC RID: 3020
		bool OWAIncludeCertificateChainAndRootCertificate { get; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000BCD RID: 3021
		bool OWAEncryptTemporaryBuffers { get; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000BCE RID: 3022
		bool OWASignedEmailCertificateInclusion { get; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000BCF RID: 3023
		uint OWABCCEncryptedEmailForking { get; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000BD0 RID: 3024
		bool OWAIncludeSMIMECapabilitiesInMessage { get; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000BD1 RID: 3025
		bool OWACopyRecipientHeaders { get; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000BD2 RID: 3026
		bool OWAOnlyUseSmartCard { get; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000BD3 RID: 3027
		bool OWATripleWrapSignedEncryptedMail { get; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000BD4 RID: 3028
		bool OWAUseKeyIdentifier { get; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000BD5 RID: 3029
		string OWAEncryptionAlgorithms { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000BD6 RID: 3030
		string OWASigningAlgorithms { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000BD7 RID: 3031
		bool OWAForceSMIMEClientUpgrade { get; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000BD8 RID: 3032
		string OWASenderCertificateAttributesToDisplay { get; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000BD9 RID: 3033
		bool OWAAllowUserChoiceOfSigningCertificate { get; }

		// Token: 0x06000BDA RID: 3034
		string SMIMECertificateIssuingCAFull();
	}
}

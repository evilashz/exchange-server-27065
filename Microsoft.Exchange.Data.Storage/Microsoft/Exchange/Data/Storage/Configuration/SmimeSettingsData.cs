using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x0200046A RID: 1130
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class SmimeSettingsData : SerializableDataBase, IEquatable<SmimeSettingsData>, ISmimeSettingsProvider
	{
		// Token: 0x06003291 RID: 12945 RVA: 0x000CDEA8 File Offset: 0x000CC0A8
		public SmimeSettingsData(ISmimeSettingsProvider source)
		{
			this.OWAAllowUserChoiceOfSigningCertificate = source.OWAAllowUserChoiceOfSigningCertificate;
			this.OWAAlwaysEncrypt = source.OWAAlwaysEncrypt;
			this.OWAAlwaysSign = source.OWAAlwaysSign;
			this.OWABCCEncryptedEmailForking = source.OWABCCEncryptedEmailForking;
			this.OWACRLConnectionTimeout = source.OWACRLConnectionTimeout;
			this.OWACRLRetrievalTimeout = source.OWACRLRetrievalTimeout;
			this.OWACheckCRLOnSend = source.OWACheckCRLOnSend;
			this.OWAClearSign = source.OWAClearSign;
			this.OWACopyRecipientHeaders = source.OWACopyRecipientHeaders;
			this.OWADLExpansionTimeout = source.OWADLExpansionTimeout;
			this.OWADisableCRLCheck = source.OWADisableCRLCheck;
			this.OWAEncryptTemporaryBuffers = source.OWAEncryptTemporaryBuffers;
			this.OWAEncryptionAlgorithms = source.OWAEncryptionAlgorithms;
			this.OWAForceSMIMEClientUpgrade = source.OWAForceSMIMEClientUpgrade;
			this.OWAIncludeCertificateChainAndRootCertificate = source.OWAIncludeCertificateChainAndRootCertificate;
			this.OWAIncludeCertificateChainWithoutRootCertificate = source.OWAIncludeCertificateChainWithoutRootCertificate;
			this.OWAIncludeSMIMECapabilitiesInMessage = source.OWAIncludeSMIMECapabilitiesInMessage;
			this.OWAOnlyUseSmartCard = source.OWAOnlyUseSmartCard;
			this.OWASenderCertificateAttributesToDisplay = source.OWASenderCertificateAttributesToDisplay;
			this.OWASignedEmailCertificateInclusion = source.OWASignedEmailCertificateInclusion;
			this.OWASigningAlgorithms = source.OWASigningAlgorithms;
			this.OWATripleWrapSignedEncryptedMail = source.OWATripleWrapSignedEncryptedMail;
			this.OWAUseKeyIdentifier = source.OWAUseKeyIdentifier;
			this.OWAUseSecondaryProxiesWhenFindingCertificates = source.OWAUseSecondaryProxiesWhenFindingCertificates;
			this.OWASMIMECertificateIssuingCAFull = source.SMIMECertificateIssuingCAFull();
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000CDFE7 File Offset: 0x000CC1E7
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x000CDFEF File Offset: 0x000CC1EF
		[DataMember]
		public bool OWAAllowUserChoiceOfSigningCertificate { get; set; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000CDFF8 File Offset: 0x000CC1F8
		// (set) Token: 0x06003295 RID: 12949 RVA: 0x000CE000 File Offset: 0x000CC200
		[DataMember]
		public bool OWAAlwaysEncrypt { get; set; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000CE009 File Offset: 0x000CC209
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x000CE011 File Offset: 0x000CC211
		[DataMember]
		public bool OWAAlwaysSign { get; set; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000CE01A File Offset: 0x000CC21A
		// (set) Token: 0x06003299 RID: 12953 RVA: 0x000CE022 File Offset: 0x000CC222
		[DataMember]
		public uint OWABCCEncryptedEmailForking { get; set; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000CE02B File Offset: 0x000CC22B
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x000CE033 File Offset: 0x000CC233
		[DataMember]
		public uint OWACRLConnectionTimeout { get; set; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000CE03C File Offset: 0x000CC23C
		// (set) Token: 0x0600329D RID: 12957 RVA: 0x000CE044 File Offset: 0x000CC244
		[DataMember]
		public uint OWACRLRetrievalTimeout { get; set; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000CE04D File Offset: 0x000CC24D
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x000CE055 File Offset: 0x000CC255
		[DataMember]
		public bool OWACheckCRLOnSend { get; set; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000CE05E File Offset: 0x000CC25E
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x000CE066 File Offset: 0x000CC266
		[DataMember]
		public bool OWAClearSign { get; set; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000CE06F File Offset: 0x000CC26F
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x000CE077 File Offset: 0x000CC277
		[DataMember]
		public bool OWACopyRecipientHeaders { get; set; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000CE080 File Offset: 0x000CC280
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x000CE088 File Offset: 0x000CC288
		[DataMember]
		public uint OWADLExpansionTimeout { get; set; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000CE091 File Offset: 0x000CC291
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000CE099 File Offset: 0x000CC299
		[DataMember]
		public bool OWADisableCRLCheck { get; set; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000CE0A2 File Offset: 0x000CC2A2
		// (set) Token: 0x060032A9 RID: 12969 RVA: 0x000CE0AA File Offset: 0x000CC2AA
		[DataMember]
		public bool OWAEncryptTemporaryBuffers { get; set; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000CE0B3 File Offset: 0x000CC2B3
		// (set) Token: 0x060032AB RID: 12971 RVA: 0x000CE0BB File Offset: 0x000CC2BB
		[DataMember]
		public string OWAEncryptionAlgorithms { get; set; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000CE0C4 File Offset: 0x000CC2C4
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x000CE0CC File Offset: 0x000CC2CC
		[DataMember]
		public bool OWAForceSMIMEClientUpgrade { get; set; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x000CE0D5 File Offset: 0x000CC2D5
		// (set) Token: 0x060032AF RID: 12975 RVA: 0x000CE0DD File Offset: 0x000CC2DD
		[DataMember]
		public bool OWAIncludeCertificateChainAndRootCertificate { get; set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000CE0E6 File Offset: 0x000CC2E6
		// (set) Token: 0x060032B1 RID: 12977 RVA: 0x000CE0EE File Offset: 0x000CC2EE
		[DataMember]
		public bool OWAIncludeCertificateChainWithoutRootCertificate { get; set; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000CE0F7 File Offset: 0x000CC2F7
		// (set) Token: 0x060032B3 RID: 12979 RVA: 0x000CE0FF File Offset: 0x000CC2FF
		[DataMember]
		public bool OWAIncludeSMIMECapabilitiesInMessage { get; set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000CE108 File Offset: 0x000CC308
		// (set) Token: 0x060032B5 RID: 12981 RVA: 0x000CE110 File Offset: 0x000CC310
		[DataMember]
		public bool OWAOnlyUseSmartCard { get; set; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000CE119 File Offset: 0x000CC319
		// (set) Token: 0x060032B7 RID: 12983 RVA: 0x000CE121 File Offset: 0x000CC321
		[DataMember]
		public string OWASenderCertificateAttributesToDisplay { get; set; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000CE12A File Offset: 0x000CC32A
		// (set) Token: 0x060032B9 RID: 12985 RVA: 0x000CE132 File Offset: 0x000CC332
		[DataMember]
		public bool OWASignedEmailCertificateInclusion { get; set; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000CE13B File Offset: 0x000CC33B
		// (set) Token: 0x060032BB RID: 12987 RVA: 0x000CE143 File Offset: 0x000CC343
		[DataMember]
		public string OWASigningAlgorithms { get; set; }

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000CE14C File Offset: 0x000CC34C
		// (set) Token: 0x060032BD RID: 12989 RVA: 0x000CE154 File Offset: 0x000CC354
		[DataMember]
		public bool OWATripleWrapSignedEncryptedMail { get; set; }

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000CE15D File Offset: 0x000CC35D
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x000CE165 File Offset: 0x000CC365
		[DataMember]
		public bool OWAUseKeyIdentifier { get; set; }

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000CE16E File Offset: 0x000CC36E
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x000CE176 File Offset: 0x000CC376
		[DataMember]
		public bool OWAUseSecondaryProxiesWhenFindingCertificates { get; set; }

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000CE17F File Offset: 0x000CC37F
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000CE187 File Offset: 0x000CC387
		[DataMember]
		public string OWASMIMECertificateIssuingCAFull { get; set; }

		// Token: 0x060032C4 RID: 12996 RVA: 0x000CE190 File Offset: 0x000CC390
		public string SMIMECertificateIssuingCAFull()
		{
			return this.OWASMIMECertificateIssuingCAFull;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000CE198 File Offset: 0x000CC398
		public bool Equals(SmimeSettingsData other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (this.OWAAllowUserChoiceOfSigningCertificate.Equals(other.OWAAllowUserChoiceOfSigningCertificate) && this.OWAAlwaysEncrypt.Equals(other.OWAAlwaysEncrypt) && this.OWAAlwaysSign.Equals(other.OWAAlwaysSign) && this.OWABCCEncryptedEmailForking == other.OWABCCEncryptedEmailForking && this.OWACRLConnectionTimeout == other.OWACRLConnectionTimeout && this.OWACRLRetrievalTimeout == other.OWACRLRetrievalTimeout && this.OWACheckCRLOnSend.Equals(other.OWACheckCRLOnSend) && this.OWAClearSign.Equals(other.OWAClearSign) && this.OWACopyRecipientHeaders.Equals(other.OWACopyRecipientHeaders) && this.OWADLExpansionTimeout == other.OWADLExpansionTimeout && this.OWADisableCRLCheck.Equals(other.OWADisableCRLCheck) && this.OWAEncryptTemporaryBuffers.Equals(other.OWAEncryptTemporaryBuffers) && string.Equals(this.OWAEncryptionAlgorithms, other.OWAEncryptionAlgorithms, StringComparison.Ordinal) && this.OWAForceSMIMEClientUpgrade.Equals(other.OWAForceSMIMEClientUpgrade) && this.OWAIncludeCertificateChainAndRootCertificate.Equals(other.OWAIncludeCertificateChainAndRootCertificate) && this.OWAIncludeCertificateChainWithoutRootCertificate.Equals(other.OWAIncludeCertificateChainWithoutRootCertificate) && this.OWAIncludeSMIMECapabilitiesInMessage.Equals(other.OWAIncludeSMIMECapabilitiesInMessage) && this.OWAOnlyUseSmartCard.Equals(other.OWAOnlyUseSmartCard) && string.Equals(this.OWASenderCertificateAttributesToDisplay, other.OWASenderCertificateAttributesToDisplay, StringComparison.Ordinal) && this.OWASignedEmailCertificateInclusion.Equals(other.OWASignedEmailCertificateInclusion) && string.Equals(this.OWASigningAlgorithms, other.OWASigningAlgorithms, StringComparison.Ordinal) && this.OWATripleWrapSignedEncryptedMail.Equals(other.OWATripleWrapSignedEncryptedMail) && this.OWAUseKeyIdentifier.Equals(other.OWAUseKeyIdentifier) && this.OWAUseSecondaryProxiesWhenFindingCertificates.Equals(other.OWAUseSecondaryProxiesWhenFindingCertificates) && string.Equals(this.OWASMIMECertificateIssuingCAFull, other.OWASMIMECertificateIssuingCAFull, StringComparison.Ordinal)));
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000CE3FF File Offset: 0x000CC5FF
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as SmimeSettingsData);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000CE410 File Offset: 0x000CC610
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ this.OWAAllowUserChoiceOfSigningCertificate.GetHashCode());
			num = (num * 397 ^ this.OWAAlwaysEncrypt.GetHashCode());
			num = (num * 397 ^ this.OWAAlwaysSign.GetHashCode());
			num = (num * 397 ^ (int)this.OWABCCEncryptedEmailForking);
			num = (num * 397 ^ (int)this.OWACRLConnectionTimeout);
			num = (num * 397 ^ (int)this.OWACRLRetrievalTimeout);
			num = (num * 397 ^ this.OWACheckCRLOnSend.GetHashCode());
			num = (num * 397 ^ this.OWAClearSign.GetHashCode());
			num = (num * 397 ^ this.OWACopyRecipientHeaders.GetHashCode());
			num = (num * 397 ^ (int)this.OWADLExpansionTimeout);
			num = (num * 397 ^ this.OWADisableCRLCheck.GetHashCode());
			num = (num * 397 ^ this.OWAEncryptTemporaryBuffers.GetHashCode());
			num = (num * 397 ^ ((this.OWAEncryptionAlgorithms != null) ? this.OWAEncryptionAlgorithms.GetHashCode() : 0));
			num = (num * 397 ^ this.OWAForceSMIMEClientUpgrade.GetHashCode());
			num = (num * 397 ^ this.OWAIncludeCertificateChainAndRootCertificate.GetHashCode());
			num = (num * 397 ^ this.OWAIncludeCertificateChainWithoutRootCertificate.GetHashCode());
			num = (num * 397 ^ this.OWAIncludeSMIMECapabilitiesInMessage.GetHashCode());
			num = (num * 397 ^ this.OWAOnlyUseSmartCard.GetHashCode());
			num = (num * 397 ^ ((this.OWASenderCertificateAttributesToDisplay != null) ? this.OWASenderCertificateAttributesToDisplay.GetHashCode() : 0));
			num = (num * 397 ^ this.OWASignedEmailCertificateInclusion.GetHashCode());
			num = (num * 397 ^ ((this.OWASigningAlgorithms != null) ? this.OWASigningAlgorithms.GetHashCode() : 0));
			num = (num * 397 ^ this.OWATripleWrapSignedEncryptedMail.GetHashCode());
			num = (num * 397 ^ this.OWAUseKeyIdentifier.GetHashCode());
			num = (num * 397 ^ this.OWAUseSecondaryProxiesWhenFindingCertificates.GetHashCode());
			return num * 397 ^ ((this.OWASMIMECertificateIssuingCAFull != null) ? this.OWASMIMECertificateIssuingCAFull.GetHashCode() : 0);
		}
	}
}

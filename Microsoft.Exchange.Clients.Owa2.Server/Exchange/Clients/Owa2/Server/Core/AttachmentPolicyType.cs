using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B1 RID: 945
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentPolicyType
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x00076489 File Offset: 0x00074689
		// (set) Token: 0x06001DF8 RID: 7672 RVA: 0x00076491 File Offset: 0x00074691
		[DataMember]
		public string[] AllowedFileTypes
		{
			get
			{
				return this.allowedFileTypes;
			}
			set
			{
				this.allowedFileTypes = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0007649A File Offset: 0x0007469A
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x000764A2 File Offset: 0x000746A2
		[DataMember]
		public string[] AllowedMimeTypes
		{
			get
			{
				return this.allowedMimeTypes;
			}
			set
			{
				this.allowedMimeTypes = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000764AB File Offset: 0x000746AB
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x000764B3 File Offset: 0x000746B3
		[DataMember]
		public string[] ForceSaveFileTypes
		{
			get
			{
				return this.forceSaveFileTypes;
			}
			set
			{
				this.forceSaveFileTypes = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000764BC File Offset: 0x000746BC
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x000764C4 File Offset: 0x000746C4
		[DataMember]
		public string[] ForceSaveMimeTypes
		{
			get
			{
				return this.forceSaveMimeTypes;
			}
			set
			{
				this.forceSaveMimeTypes = value;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000764CD File Offset: 0x000746CD
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x000764D5 File Offset: 0x000746D5
		[DataMember]
		public string[] BlockedFileTypes
		{
			get
			{
				return this.blockedFileTypes;
			}
			set
			{
				this.blockedFileTypes = value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000764DE File Offset: 0x000746DE
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x000764E6 File Offset: 0x000746E6
		[DataMember]
		public string[] BlockedMimeTypes
		{
			get
			{
				return this.blockedMimeTypes;
			}
			set
			{
				this.blockedMimeTypes = value;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000764EF File Offset: 0x000746EF
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x000764F7 File Offset: 0x000746F7
		[DataMember]
		public string ActionForUnknownFileAndMIMETypes
		{
			get
			{
				return this.actionForUnknownFileAndMIMETypes;
			}
			set
			{
				this.actionForUnknownFileAndMIMETypes = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00076500 File Offset: 0x00074700
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x00076508 File Offset: 0x00074708
		[DataMember]
		public string[] WacViewableFileTypes
		{
			get
			{
				return this.wacViewableFileTypes;
			}
			set
			{
				this.wacViewableFileTypes = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00076511 File Offset: 0x00074711
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x00076519 File Offset: 0x00074719
		[DataMember]
		public string[] WacEditableFileTypes
		{
			get
			{
				return this.wacEditableFileTypes;
			}
			set
			{
				this.wacEditableFileTypes = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00076522 File Offset: 0x00074722
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0007652A File Offset: 0x0007472A
		[DataMember]
		public bool WacViewingOnPublicComputersEnabled
		{
			get
			{
				return this.wacViewingOnPublicComputersEnabled;
			}
			set
			{
				this.wacViewingOnPublicComputersEnabled = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00076533 File Offset: 0x00074733
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0007653B File Offset: 0x0007473B
		[DataMember]
		public bool WacViewingOnPrivateComputersEnabled
		{
			get
			{
				return this.wacViewingOnPrivateComputersEnabled;
			}
			set
			{
				this.wacViewingOnPrivateComputersEnabled = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00076544 File Offset: 0x00074744
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x0007654C File Offset: 0x0007474C
		[DataMember]
		public bool ForceWacViewingFirstOnPublicComputers
		{
			get
			{
				return this.forceWacViewingFirstOnPublicComputers;
			}
			set
			{
				this.forceWacViewingFirstOnPublicComputers = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00076555 File Offset: 0x00074755
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x0007655D File Offset: 0x0007475D
		[DataMember]
		public bool ForceWacViewingFirstOnPrivateComputers
		{
			get
			{
				return this.forceWacViewingFirstOnPrivateComputers;
			}
			set
			{
				this.forceWacViewingFirstOnPrivateComputers = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00076566 File Offset: 0x00074766
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0007656E File Offset: 0x0007476E
		[DataMember]
		public bool ForceWebReadyDocumentViewingFirstOnPublicComputers
		{
			get
			{
				return this.forceWebReadyDocumentViewingFirstOnPublicComputers;
			}
			set
			{
				this.forceWebReadyDocumentViewingFirstOnPublicComputers = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00076577 File Offset: 0x00074777
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0007657F File Offset: 0x0007477F
		[DataMember]
		public bool ForceWebReadyDocumentViewingFirstOnPrivateComputers
		{
			get
			{
				return this.forceWebReadyDocumentViewingFirstOnPrivateComputers;
			}
			set
			{
				this.forceWebReadyDocumentViewingFirstOnPrivateComputers = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00076588 File Offset: 0x00074788
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x00076590 File Offset: 0x00074790
		[DataMember]
		public bool WebReadyDocumentViewingOnPublicComputersEnabled
		{
			get
			{
				return this.webReadyDocumentViewingOnPublicComputersEnabled;
			}
			set
			{
				this.webReadyDocumentViewingOnPublicComputersEnabled = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00076599 File Offset: 0x00074799
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x000765A1 File Offset: 0x000747A1
		[DataMember]
		public bool WebReadyDocumentViewingOnPrivateComputersEnabled
		{
			get
			{
				return this.webReadyDocumentViewingOnPrivateComputersEnabled;
			}
			set
			{
				this.webReadyDocumentViewingOnPrivateComputersEnabled = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x000765AA File Offset: 0x000747AA
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x000765B2 File Offset: 0x000747B2
		[DataMember]
		public bool DirectFileAccessOnPublicComputersEnabled
		{
			get
			{
				return this.directFileAccessOnPublicComputersEnabled;
			}
			set
			{
				this.directFileAccessOnPublicComputersEnabled = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000765BB File Offset: 0x000747BB
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x000765C3 File Offset: 0x000747C3
		[DataMember]
		public bool DirectFileAccessOnPrivateComputersEnabled
		{
			get
			{
				return this.directFileAccessOnPrivateComputersEnabled;
			}
			set
			{
				this.directFileAccessOnPrivateComputersEnabled = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x000765CC File Offset: 0x000747CC
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x000765D4 File Offset: 0x000747D4
		[DataMember]
		public bool WebReadyDocumentViewingForAllSupportedTypes
		{
			get
			{
				return this.webReadyDocumentViewingForAllSupportedTypes;
			}
			set
			{
				this.webReadyDocumentViewingForAllSupportedTypes = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x000765DD File Offset: 0x000747DD
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x000765E5 File Offset: 0x000747E5
		[DataMember]
		public bool AttachmentDataProviderAvailable
		{
			get
			{
				return this.attachmentDataProviderAvailable;
			}
			set
			{
				this.attachmentDataProviderAvailable = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x000765EE File Offset: 0x000747EE
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x000765F6 File Offset: 0x000747F6
		[DataMember]
		public string[] WebReadyFileTypes
		{
			get
			{
				return this.webReadyFileTypes;
			}
			set
			{
				this.webReadyFileTypes = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x000765FF File Offset: 0x000747FF
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x00076607 File Offset: 0x00074807
		[DataMember]
		public string[] WebReadyMimeTypes
		{
			get
			{
				return this.webReadyMimeTypes;
			}
			set
			{
				this.webReadyMimeTypes = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00076610 File Offset: 0x00074810
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x00076618 File Offset: 0x00074818
		[DataMember]
		public string[] WebReadyDocumentViewingSupportedFileTypes
		{
			get
			{
				return this.webReadyDocumentViewingSupportedFileTypes;
			}
			set
			{
				this.webReadyDocumentViewingSupportedFileTypes = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00076621 File Offset: 0x00074821
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x00076629 File Offset: 0x00074829
		[DataMember]
		public string[] WebReadyDocumentViewingSupportedMimeTypes
		{
			get
			{
				return this.webReadyDocumentViewingSupportedMimeTypes;
			}
			set
			{
				this.webReadyDocumentViewingSupportedMimeTypes = value;
			}
		}

		// Token: 0x040010F5 RID: 4341
		private string[] allowedFileTypes;

		// Token: 0x040010F6 RID: 4342
		private string[] allowedMimeTypes;

		// Token: 0x040010F7 RID: 4343
		private string[] forceSaveFileTypes;

		// Token: 0x040010F8 RID: 4344
		private string[] forceSaveMimeTypes;

		// Token: 0x040010F9 RID: 4345
		private string[] blockedFileTypes;

		// Token: 0x040010FA RID: 4346
		private string[] blockedMimeTypes;

		// Token: 0x040010FB RID: 4347
		private string actionForUnknownFileAndMIMETypes;

		// Token: 0x040010FC RID: 4348
		private string[] wacViewableFileTypes;

		// Token: 0x040010FD RID: 4349
		private string[] wacEditableFileTypes;

		// Token: 0x040010FE RID: 4350
		private bool forceWacViewingFirstOnPublicComputers;

		// Token: 0x040010FF RID: 4351
		private bool forceWacViewingFirstOnPrivateComputers;

		// Token: 0x04001100 RID: 4352
		private bool wacViewingOnPublicComputersEnabled;

		// Token: 0x04001101 RID: 4353
		private bool wacViewingOnPrivateComputersEnabled;

		// Token: 0x04001102 RID: 4354
		private bool forceWebReadyDocumentViewingFirstOnPublicComputers;

		// Token: 0x04001103 RID: 4355
		private bool forceWebReadyDocumentViewingFirstOnPrivateComputers;

		// Token: 0x04001104 RID: 4356
		private bool webReadyDocumentViewingOnPublicComputersEnabled;

		// Token: 0x04001105 RID: 4357
		private bool webReadyDocumentViewingOnPrivateComputersEnabled;

		// Token: 0x04001106 RID: 4358
		private bool webReadyDocumentViewingForAllSupportedTypes;

		// Token: 0x04001107 RID: 4359
		private bool directFileAccessOnPrivateComputersEnabled;

		// Token: 0x04001108 RID: 4360
		private bool directFileAccessOnPublicComputersEnabled;

		// Token: 0x04001109 RID: 4361
		private bool attachmentDataProviderAvailable;

		// Token: 0x0400110A RID: 4362
		private string[] webReadyFileTypes;

		// Token: 0x0400110B RID: 4363
		private string[] webReadyMimeTypes;

		// Token: 0x0400110C RID: 4364
		private string[] webReadyDocumentViewingSupportedFileTypes;

		// Token: 0x0400110D RID: 4365
		private string[] webReadyDocumentViewingSupportedMimeTypes;
	}
}

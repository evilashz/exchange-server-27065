using System;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C5 RID: 1477
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class SmimeConfigurationContainer : ADConfigurationObject, ISmimeSettingsProvider
	{
		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x000FEDC5 File Offset: 0x000FCFC5
		internal override ADObjectSchema Schema
		{
			get
			{
				return SmimeConfigurationContainer.schema;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x000FEDCC File Offset: 0x000FCFCC
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchContainer";
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x000FEDD3 File Offset: 0x000FCFD3
		internal override ADObjectId ParentPath
		{
			get
			{
				return SmimeConfigurationContainer.parentPath;
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x000FEDDA File Offset: 0x000FCFDA
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x000FEDED File Offset: 0x000FCFED
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x000FEDFF File Offset: 0x000FCFFF
		private string SmimeConfigurationXML
		{
			get
			{
				return (string)this[SmimeConfigurationContainerSchema.SmimeConfigurationXML];
			}
			set
			{
				this[SmimeConfigurationContainerSchema.SmimeConfigurationXML] = value;
			}
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000FEE0D File Offset: 0x000FD00D
		internal override void Initialize()
		{
			this.LoadSettings();
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000FEE18 File Offset: 0x000FD018
		internal static ADObjectId GetWellKnownParentLocation(ADObjectId orgContainerId)
		{
			ADObjectId defaultsRoot = SmimeConfigurationContainer.DefaultsRoot;
			return orgContainerId.GetDescendantId(defaultsRoot);
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000FEE32 File Offset: 0x000FD032
		public void SaveSettings()
		{
			this.PackSmimeSettings();
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x000FEE3C File Offset: 0x000FD03C
		// (set) Token: 0x060043F6 RID: 17398 RVA: 0x000FEE68 File Offset: 0x000FD068
		public bool OWACheckCRLOnSend
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWACheckCRLOnSend");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWACheckCRLOnSend"))
				{
					this.SetValueForTag("OWACheckCRLOnSend", text);
				}
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x000FEE9C File Offset: 0x000FD09C
		// (set) Token: 0x060043F8 RID: 17400 RVA: 0x000FEECC File Offset: 0x000FD0CC
		public uint OWADLExpansionTimeout
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWADLExpansionTimeout");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return 60000U;
				}
				return uint.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWADLExpansionTimeout"))
				{
					this.SetValueForTag("OWADLExpansionTimeout", text);
				}
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x000FEF00 File Offset: 0x000FD100
		// (set) Token: 0x060043FA RID: 17402 RVA: 0x000FEF2C File Offset: 0x000FD12C
		public bool OWAUseSecondaryProxiesWhenFindingCertificates
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAUseSecondaryProxiesWhenFindingCertificates");
				return string.IsNullOrWhiteSpace(valueForTag) || bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAUseSecondaryProxiesWhenFindingCertificates"))
				{
					this.SetValueForTag("OWAUseSecondaryProxiesWhenFindingCertificates", text);
				}
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x000FEF60 File Offset: 0x000FD160
		// (set) Token: 0x060043FC RID: 17404 RVA: 0x000FEF90 File Offset: 0x000FD190
		public uint OWACRLConnectionTimeout
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWACRLConnectionTimeout");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return 60000U;
				}
				return uint.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWACRLConnectionTimeout"))
				{
					this.SetValueForTag("OWACRLConnectionTimeout", text);
				}
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x000FEFC4 File Offset: 0x000FD1C4
		// (set) Token: 0x060043FE RID: 17406 RVA: 0x000FEFF4 File Offset: 0x000FD1F4
		public uint OWACRLRetrievalTimeout
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWACRLRetrievalTimeout");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return 10000U;
				}
				return uint.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWACRLRetrievalTimeout"))
				{
					this.SetValueForTag("OWACRLRetrievalTimeout", text);
				}
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x000FF028 File Offset: 0x000FD228
		// (set) Token: 0x06004400 RID: 17408 RVA: 0x000FF054 File Offset: 0x000FD254
		public bool OWADisableCRLCheck
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWADisableCRLCheck");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWADisableCRLCheck"))
				{
					this.SetValueForTag("OWADisableCRLCheck", text);
				}
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x000FF088 File Offset: 0x000FD288
		// (set) Token: 0x06004402 RID: 17410 RVA: 0x000FF0B4 File Offset: 0x000FD2B4
		public bool OWAAlwaysSign
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAAlwaysSign");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAAlwaysSign"))
				{
					this.SetValueForTag("OWAAlwaysSign", text);
				}
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x000FF0E8 File Offset: 0x000FD2E8
		// (set) Token: 0x06004404 RID: 17412 RVA: 0x000FF114 File Offset: 0x000FD314
		public bool OWAAlwaysEncrypt
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAAlwaysEncrypt");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAAlwaysEncrypt"))
				{
					this.SetValueForTag("OWAAlwaysEncrypt", text);
				}
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x000FF148 File Offset: 0x000FD348
		// (set) Token: 0x06004406 RID: 17414 RVA: 0x000FF174 File Offset: 0x000FD374
		public bool OWAClearSign
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAClearSign");
				return string.IsNullOrWhiteSpace(valueForTag) || bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAClearSign"))
				{
					this.SetValueForTag("OWAClearSign", text);
				}
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x000FF1A8 File Offset: 0x000FD3A8
		// (set) Token: 0x06004408 RID: 17416 RVA: 0x000FF1D4 File Offset: 0x000FD3D4
		public bool OWAIncludeCertificateChainWithoutRootCertificate
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAIncludeCertificateChainWithoutRootCertificate");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAIncludeCertificateChainWithoutRootCertificate"))
				{
					this.SetValueForTag("OWAIncludeCertificateChainWithoutRootCertificate", text);
				}
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x000FF208 File Offset: 0x000FD408
		// (set) Token: 0x0600440A RID: 17418 RVA: 0x000FF234 File Offset: 0x000FD434
		public bool OWAIncludeCertificateChainAndRootCertificate
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAIncludeCertificateChainAndRootCertificate");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAIncludeCertificateChainAndRootCertificate"))
				{
					this.SetValueForTag("OWAIncludeCertificateChainAndRootCertificate", text);
				}
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x000FF268 File Offset: 0x000FD468
		// (set) Token: 0x0600440C RID: 17420 RVA: 0x000FF294 File Offset: 0x000FD494
		public bool OWAEncryptTemporaryBuffers
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAEncryptTemporaryBuffers");
				return string.IsNullOrWhiteSpace(valueForTag) || bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAEncryptTemporaryBuffers"))
				{
					this.SetValueForTag("OWAEncryptTemporaryBuffers", text);
				}
			}
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x000FF2C8 File Offset: 0x000FD4C8
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x000FF2F4 File Offset: 0x000FD4F4
		public bool OWASignedEmailCertificateInclusion
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWASignedEmailCertificateInclusion");
				return string.IsNullOrWhiteSpace(valueForTag) || bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWASignedEmailCertificateInclusion"))
				{
					this.SetValueForTag("OWASignedEmailCertificateInclusion", text);
				}
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x000FF328 File Offset: 0x000FD528
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x000FF354 File Offset: 0x000FD554
		public uint OWABCCEncryptedEmailForking
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWABCCEncryptedEmailForking");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return 0U;
				}
				return uint.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWABCCEncryptedEmailForking"))
				{
					this.SetValueForTag("OWABCCEncryptedEmailForking", text);
				}
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x000FF388 File Offset: 0x000FD588
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x000FF3B4 File Offset: 0x000FD5B4
		public bool OWAIncludeSMIMECapabilitiesInMessage
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAIncludeSMIMECapabilitiesInMessage");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAIncludeSMIMECapabilitiesInMessage"))
				{
					this.SetValueForTag("OWAIncludeSMIMECapabilitiesInMessage", text);
				}
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x000FF3E8 File Offset: 0x000FD5E8
		// (set) Token: 0x06004414 RID: 17428 RVA: 0x000FF414 File Offset: 0x000FD614
		public bool OWACopyRecipientHeaders
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWACopyRecipientHeaders");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWACopyRecipientHeaders"))
				{
					this.SetValueForTag("OWACopyRecipientHeaders", text);
				}
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000FF448 File Offset: 0x000FD648
		// (set) Token: 0x06004416 RID: 17430 RVA: 0x000FF474 File Offset: 0x000FD674
		public bool OWAOnlyUseSmartCard
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAOnlyUseSmartCard");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAOnlyUseSmartCard"))
				{
					this.SetValueForTag("OWAOnlyUseSmartCard", text);
				}
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000FF4A8 File Offset: 0x000FD6A8
		// (set) Token: 0x06004418 RID: 17432 RVA: 0x000FF4D4 File Offset: 0x000FD6D4
		public bool OWATripleWrapSignedEncryptedMail
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWATripleWrapSignedEncryptedMail");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWATripleWrapSignedEncryptedMail"))
				{
					this.SetValueForTag("OWATripleWrapSignedEncryptedMail", text);
				}
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x000FF508 File Offset: 0x000FD708
		// (set) Token: 0x0600441A RID: 17434 RVA: 0x000FF534 File Offset: 0x000FD734
		public bool OWAUseKeyIdentifier
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAUseKeyIdentifier");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAUseKeyIdentifier"))
				{
					this.SetValueForTag("OWAUseKeyIdentifier", text);
				}
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x000FF568 File Offset: 0x000FD768
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x000FF590 File Offset: 0x000FD790
		public string OWAEncryptionAlgorithms
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAEncryptionAlgorithms");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return "6610";
				}
				return valueForTag;
			}
			set
			{
				if (value != this.GetValueForTag("OWAEncryptionAlgorithms"))
				{
					this.SetValueForTag("OWAEncryptionAlgorithms", value);
				}
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000FF5B4 File Offset: 0x000FD7B4
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x000FF5DC File Offset: 0x000FD7DC
		public string OWASigningAlgorithms
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWASigningAlgorithms");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return "8004";
				}
				return valueForTag;
			}
			set
			{
				if (value != this.GetValueForTag("OWASigningAlgorithms"))
				{
					this.SetValueForTag("OWASigningAlgorithms", value);
				}
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x000FF600 File Offset: 0x000FD800
		// (set) Token: 0x06004420 RID: 17440 RVA: 0x000FF62C File Offset: 0x000FD82C
		public bool OWAForceSMIMEClientUpgrade
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAForceSMIMEClientUpgrade");
				return string.IsNullOrWhiteSpace(valueForTag) || bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAForceSMIMEClientUpgrade"))
				{
					this.SetValueForTag("OWAForceSMIMEClientUpgrade", text);
				}
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x000FF660 File Offset: 0x000FD860
		// (set) Token: 0x06004422 RID: 17442 RVA: 0x000FF688 File Offset: 0x000FD888
		public string OWASenderCertificateAttributesToDisplay
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWASenderCertificateAttributesToDisplay");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return "";
				}
				return valueForTag;
			}
			set
			{
				if (value != this.GetValueForTag("OWASenderCertificateAttributesToDisplay"))
				{
					this.SetValueForTag("OWASenderCertificateAttributesToDisplay", value);
				}
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x000FF6AC File Offset: 0x000FD8AC
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x000FF6D8 File Offset: 0x000FD8D8
		public bool OWAAllowUserChoiceOfSigningCertificate
		{
			get
			{
				string valueForTag = this.GetValueForTag("OWAAllowUserChoiceOfSigningCertificate");
				return !string.IsNullOrWhiteSpace(valueForTag) && bool.Parse(valueForTag);
			}
			set
			{
				string text = value.ToString();
				if (text != this.GetValueForTag("OWAAllowUserChoiceOfSigningCertificate"))
				{
					this.SetValueForTag("OWAAllowUserChoiceOfSigningCertificate", text);
				}
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x000FF70C File Offset: 0x000FD90C
		// (set) Token: 0x06004426 RID: 17446 RVA: 0x000FF738 File Offset: 0x000FD938
		public byte[] SMIMECertificateIssuingCA
		{
			get
			{
				string valueForTag = this.GetValueForTag("SMIMECertificateIssuingCA");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return null;
				}
				return Convert.FromBase64String(valueForTag);
			}
			set
			{
				string valueForTag = this.GetValueForTag("SMIMECertificateIssuingCA");
				string text = string.Empty;
				if (value != null)
				{
					text = Convert.ToBase64String(value);
				}
				if (text != valueForTag)
				{
					this.SetValueForTag("SMIMECertificateIssuingCA", text);
				}
			}
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000FF778 File Offset: 0x000FD978
		public string SMIMECertificateIssuingCAFull()
		{
			string valueForTag = this.GetValueForTag("SMIMECertificateIssuingCA");
			if (string.IsNullOrWhiteSpace(valueForTag))
			{
				return "";
			}
			return valueForTag;
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x000FF7A0 File Offset: 0x000FD9A0
		// (set) Token: 0x06004429 RID: 17449 RVA: 0x000FF7DC File Offset: 0x000FD9DC
		public DateTime? SMIMECertificatesExpiryDate
		{
			get
			{
				string valueForTag = this.GetValueForTag("SMIMECertificatesExpiryDate");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return this.SMIMECertificatesExpiryDateDefaultValue;
				}
				return new DateTime?(DateTime.ParseExact(valueForTag, "s", null));
			}
			set
			{
				if (value == null)
				{
					this.SetValueForTag("SMIMECertificatesExpiryDate", "");
					return;
				}
				string text = value.Value.ToString("s");
				if (text != this.GetValueForTag("SMIMECertificatesExpiryDate"))
				{
					this.SetValueForTag("SMIMECertificatesExpiryDate", text);
				}
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x000FF838 File Offset: 0x000FDA38
		// (set) Token: 0x0600442B RID: 17451 RVA: 0x000FF860 File Offset: 0x000FDA60
		public string SMIMEExpiredCertificateThumbprint
		{
			get
			{
				string valueForTag = this.GetValueForTag("SMIMEExpiredCertificateThumbprint");
				if (string.IsNullOrWhiteSpace(valueForTag))
				{
					return "";
				}
				return valueForTag;
			}
			set
			{
				if (value != this.GetValueForTag("SMIMEExpiredCertificateThumbprint"))
				{
					this.SetValueForTag("SMIMEExpiredCertificateThumbprint", value);
				}
			}
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x000FF884 File Offset: 0x000FDA84
		private string GetValueForTag(string tag)
		{
			if (this.xmlDoc != null && this.xmlDoc.DocumentElement != null)
			{
				XmlElement documentElement = this.xmlDoc.DocumentElement;
				if (documentElement.HasChildNodes)
				{
					for (int i = 0; i < documentElement.ChildNodes.Count; i++)
					{
						if (documentElement.ChildNodes[i].Name == tag)
						{
							return ((XmlElement)documentElement.ChildNodes[i]).GetAttribute("Value");
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x000FF908 File Offset: 0x000FDB08
		private void SetValueForTag(string tag, string value)
		{
			if (this.xmlDoc != null)
			{
				XmlElement xmlElement;
				if (this.xmlDoc.DocumentElement != null)
				{
					xmlElement = this.xmlDoc.DocumentElement;
				}
				else
				{
					xmlElement = this.xmlDoc.CreateElement("SMIMEConfiguration");
					this.xmlDoc.AppendChild(xmlElement);
				}
				XmlElement xmlElement2 = null;
				if (xmlElement.HasChildNodes)
				{
					for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
					{
						if (string.Compare(xmlElement.ChildNodes[i].Name.ToLower(), tag.ToLower()) == 0)
						{
							xmlElement2 = (XmlElement)xmlElement.ChildNodes[i];
						}
					}
				}
				if (value == null)
				{
					if (xmlElement2 != null)
					{
						xmlElement.RemoveChild(xmlElement2);
						return;
					}
				}
				else
				{
					if (xmlElement2 == null)
					{
						xmlElement2 = this.xmlDoc.CreateElement(tag);
						xmlElement.AppendChild(xmlElement2);
					}
					xmlElement2.SetAttribute("Value", value);
				}
			}
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000FF9E4 File Offset: 0x000FDBE4
		private void LoadSettings()
		{
			if (this.xmlDoc == null)
			{
				string smimeConfigurationXML = this.SmimeConfigurationXML;
				this.xmlDoc = new SafeXmlDocument();
				if (string.IsNullOrEmpty(smimeConfigurationXML))
				{
					XmlElement newChild = this.xmlDoc.CreateElement("SMIMEConfiguration");
					this.xmlDoc.AppendChild(newChild);
					return;
				}
				this.xmlDoc.LoadXml(smimeConfigurationXML);
			}
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x000FFA40 File Offset: 0x000FDC40
		private void PackSmimeSettings()
		{
			if (this.xmlDoc != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				{
					this.xmlDoc.Save(xmlWriter);
					xmlWriter.Close();
				}
				this.SmimeConfigurationXML = stringBuilder.ToString();
			}
		}

		// Token: 0x04002E1E RID: 11806
		internal const string MostDerivedClass = "msExchContainer";

		// Token: 0x04002E1F RID: 11807
		private const string ValueAttribute = "Value";

		// Token: 0x04002E20 RID: 11808
		private const string SMimeConfigurationRootNodeName = "SMIMEConfiguration";

		// Token: 0x04002E21 RID: 11809
		public const string OWACheckCRLOnSendProperty = "OWACheckCRLOnSend";

		// Token: 0x04002E22 RID: 11810
		public const string OWADLExpansionTimeoutProperty = "OWADLExpansionTimeout";

		// Token: 0x04002E23 RID: 11811
		public const string OWAUseSecondaryProxiesWhenFindingCertificatesProperty = "OWAUseSecondaryProxiesWhenFindingCertificates";

		// Token: 0x04002E24 RID: 11812
		public const string OWACRLConnectionTimeoutProperty = "OWACRLConnectionTimeout";

		// Token: 0x04002E25 RID: 11813
		public const string OWACRLRetrievalTimeoutProperty = "OWACRLRetrievalTimeout";

		// Token: 0x04002E26 RID: 11814
		public const string OWADisableCRLCheckProperty = "OWADisableCRLCheck";

		// Token: 0x04002E27 RID: 11815
		public const string OWAAlwaysSignProperty = "OWAAlwaysSign";

		// Token: 0x04002E28 RID: 11816
		public const string OWAAlwaysEncryptProperty = "OWAAlwaysEncrypt";

		// Token: 0x04002E29 RID: 11817
		public const string OWAClearSignProperty = "OWAClearSign";

		// Token: 0x04002E2A RID: 11818
		public const string OWAIncludeCertificateChainWithoutRootCertificateProperty = "OWAIncludeCertificateChainWithoutRootCertificate";

		// Token: 0x04002E2B RID: 11819
		public const string OWAIncludeCertificateChainAndRootCertificateProperty = "OWAIncludeCertificateChainAndRootCertificate";

		// Token: 0x04002E2C RID: 11820
		public const string OWAEncryptTemporaryBuffersProperty = "OWAEncryptTemporaryBuffers";

		// Token: 0x04002E2D RID: 11821
		public const string OWASignedEmailCertificateInclusionProperty = "OWASignedEmailCertificateInclusion";

		// Token: 0x04002E2E RID: 11822
		public const string OWABCCEncryptedEmailForkingProperty = "OWABCCEncryptedEmailForking";

		// Token: 0x04002E2F RID: 11823
		public const string OWAIncludeSMIMECapabilitiesInMessageProperty = "OWAIncludeSMIMECapabilitiesInMessage";

		// Token: 0x04002E30 RID: 11824
		public const string OWACopyRecipientHeadersProperty = "OWACopyRecipientHeaders";

		// Token: 0x04002E31 RID: 11825
		public const string OWAOnlyUseSmartCardProperty = "OWAOnlyUseSmartCard";

		// Token: 0x04002E32 RID: 11826
		public const string OWATripleWrapSignedEncryptedMailProperty = "OWATripleWrapSignedEncryptedMail";

		// Token: 0x04002E33 RID: 11827
		public const string OWAUseKeyIdentifierProperty = "OWAUseKeyIdentifier";

		// Token: 0x04002E34 RID: 11828
		public const string OWAEncryptionAlgorithmsProperty = "OWAEncryptionAlgorithms";

		// Token: 0x04002E35 RID: 11829
		public const string OWASigningAlgorithmsProperty = "OWASigningAlgorithms";

		// Token: 0x04002E36 RID: 11830
		public const string OWAForceSMIMEClientUpgradeProperty = "OWAForceSMIMEClientUpgrade";

		// Token: 0x04002E37 RID: 11831
		public const string OWASenderCertificateAttributesToDisplayProperty = "OWASenderCertificateAttributesToDisplay";

		// Token: 0x04002E38 RID: 11832
		public const string OWAAllowUserChoiceOfSigningCertificateProperty = "OWAAllowUserChoiceOfSigningCertificate";

		// Token: 0x04002E39 RID: 11833
		public const string SMIMECertificateIssuingCAProperty = "SMIMECertificateIssuingCA";

		// Token: 0x04002E3A RID: 11834
		public const string SMIMECertificatesExpiryDateStringProperty = "SMIMECertificatesExpiryDate";

		// Token: 0x04002E3B RID: 11835
		public const string SMIMEExpiredCertificateThumbprintProperty = "SMIMEExpiredCertificateThumbprint";

		// Token: 0x04002E3C RID: 11836
		private const bool OWACheckCRLOnSendDefaultValue = false;

		// Token: 0x04002E3D RID: 11837
		private const int OWADLExpansionTimeoutDefaultValue = 60000;

		// Token: 0x04002E3E RID: 11838
		private const bool OWAUseSecondaryProxiesWhenFindingCertificatesDefaultValue = true;

		// Token: 0x04002E3F RID: 11839
		private const int OWACRLConnectionTimeoutDefaultValue = 60000;

		// Token: 0x04002E40 RID: 11840
		private const int OWACRLRetrievalTimeoutDefaultValue = 10000;

		// Token: 0x04002E41 RID: 11841
		private const bool OWADisableCRLCheckDefaultValue = false;

		// Token: 0x04002E42 RID: 11842
		private const bool OWAAlwaysSignDefaultValue = false;

		// Token: 0x04002E43 RID: 11843
		private const bool OWAAlwaysEncryptDefaultValue = false;

		// Token: 0x04002E44 RID: 11844
		private const bool OWAClearSignDefaultValue = true;

		// Token: 0x04002E45 RID: 11845
		private const bool OWAIncludeCertificateChainWithoutRootCertificateDefaultValue = false;

		// Token: 0x04002E46 RID: 11846
		private const bool OWAIncludeCertificateChainAndRootCertificateDefaultValue = false;

		// Token: 0x04002E47 RID: 11847
		private const bool OWAEncryptTemporaryBuffersDefaultValue = true;

		// Token: 0x04002E48 RID: 11848
		private const bool OWASignedEmailCertificateInclusionDefaultValue = true;

		// Token: 0x04002E49 RID: 11849
		private const int OWABCCEncryptedEmailForkingDefaultValue = 0;

		// Token: 0x04002E4A RID: 11850
		private const bool OWAIncludeSMIMECapabilitiesInMessageDefaultValue = false;

		// Token: 0x04002E4B RID: 11851
		private const bool OWACopyRecipientHeadersDefaultValue = false;

		// Token: 0x04002E4C RID: 11852
		private const bool OWAOnlyUseSmartCardDefaultValue = false;

		// Token: 0x04002E4D RID: 11853
		private const bool OWATripleWrapSignedEncryptedMailDefaultValue = false;

		// Token: 0x04002E4E RID: 11854
		private const bool OWAUseKeyIdentifierDefaultValue = false;

		// Token: 0x04002E4F RID: 11855
		private const string OWAEncryptionAlgorithmsDefaultValue = "6610";

		// Token: 0x04002E50 RID: 11856
		private const string OWASigningAlgorithmsDefaultValue = "8004";

		// Token: 0x04002E51 RID: 11857
		private const bool OWAForceSMIMEClientUpgradeDefaultValue = true;

		// Token: 0x04002E52 RID: 11858
		private const string OWASenderCertificateAttributesToDisplayDefaultValue = "";

		// Token: 0x04002E53 RID: 11859
		private const bool OWAAllowUserChoiceOfSigningCertificateDefaultValue = false;

		// Token: 0x04002E54 RID: 11860
		private const string SMIMECertificateIssuingCADefaultValue = "";

		// Token: 0x04002E55 RID: 11861
		private const string SMIMECertificatesExpiryDateStringDefaultValue = "";

		// Token: 0x04002E56 RID: 11862
		private const string SMIMEExpiredCertificateThumbprintDefaultValue = "";

		// Token: 0x04002E57 RID: 11863
		private static SmimeConfigurationContainerSchema schema = ObjectSchema.GetInstance<SmimeConfigurationContainerSchema>();

		// Token: 0x04002E58 RID: 11864
		public static ADObjectId DefaultsRoot = new ADObjectId("CN=Smime Configuration,CN=Global Settings");

		// Token: 0x04002E59 RID: 11865
		private static ADObjectId parentPath = new ADObjectId("CN=Global Settings");

		// Token: 0x04002E5A RID: 11866
		private readonly DateTime? SMIMECertificatesExpiryDateDefaultValue = null;

		// Token: 0x04002E5B RID: 11867
		private SafeXmlDocument xmlDoc;
	}
}

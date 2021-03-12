using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AF2 RID: 2802
	[Cmdlet("Set", "SmimeConfig", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetSmimeConfig : SetMultitenancySingletonSystemConfigurationObjectTask<SmimeConfigurationContainer>
	{
		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x001A090B File Offset: 0x0019EB0B
		// (set) Token: 0x06006393 RID: 25491 RVA: 0x001A0922 File Offset: 0x0019EB22
		[Parameter(Mandatory = false)]
		public bool OWACheckCRLOnSend
		{
			get
			{
				return (bool)base.Fields["OWACheckCRLOnSend"];
			}
			set
			{
				base.Fields["OWACheckCRLOnSend"] = value;
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06006394 RID: 25492 RVA: 0x001A093A File Offset: 0x0019EB3A
		// (set) Token: 0x06006395 RID: 25493 RVA: 0x001A0951 File Offset: 0x0019EB51
		[Parameter(Mandatory = false)]
		public uint OWADLExpansionTimeout
		{
			get
			{
				return (uint)base.Fields["OWADLExpansionTimeout"];
			}
			set
			{
				base.Fields["OWADLExpansionTimeout"] = value;
			}
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x001A0969 File Offset: 0x0019EB69
		// (set) Token: 0x06006397 RID: 25495 RVA: 0x001A0980 File Offset: 0x0019EB80
		[Parameter(Mandatory = false)]
		public bool OWAUseSecondaryProxiesWhenFindingCertificates
		{
			get
			{
				return (bool)base.Fields["OWAUseSecondaryProxiesWhenFindingCertificates"];
			}
			set
			{
				base.Fields["OWAUseSecondaryProxiesWhenFindingCertificates"] = value;
			}
		}

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x06006398 RID: 25496 RVA: 0x001A0998 File Offset: 0x0019EB98
		// (set) Token: 0x06006399 RID: 25497 RVA: 0x001A09AF File Offset: 0x0019EBAF
		[Parameter(Mandatory = false)]
		public uint OWACRLConnectionTimeout
		{
			get
			{
				return (uint)base.Fields["OWACRLConnectionTimeout"];
			}
			set
			{
				base.Fields["OWACRLConnectionTimeout"] = value;
			}
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x001A09C7 File Offset: 0x0019EBC7
		// (set) Token: 0x0600639B RID: 25499 RVA: 0x001A09DE File Offset: 0x0019EBDE
		[Parameter(Mandatory = false)]
		public uint OWACRLRetrievalTimeout
		{
			get
			{
				return (uint)base.Fields["OWACRLRetrievalTimeout"];
			}
			set
			{
				base.Fields["OWACRLRetrievalTimeout"] = value;
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x0600639C RID: 25500 RVA: 0x001A09F6 File Offset: 0x0019EBF6
		// (set) Token: 0x0600639D RID: 25501 RVA: 0x001A0A0D File Offset: 0x0019EC0D
		[Parameter(Mandatory = false)]
		public bool OWADisableCRLCheck
		{
			get
			{
				return (bool)base.Fields["OWADisableCRLCheck"];
			}
			set
			{
				base.Fields["OWADisableCRLCheck"] = value;
			}
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x001A0A25 File Offset: 0x0019EC25
		// (set) Token: 0x0600639F RID: 25503 RVA: 0x001A0A3C File Offset: 0x0019EC3C
		[Parameter(Mandatory = false)]
		public bool OWAAlwaysSign
		{
			get
			{
				return (bool)base.Fields["OWAAlwaysSign"];
			}
			set
			{
				base.Fields["OWAAlwaysSign"] = value;
			}
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x001A0A54 File Offset: 0x0019EC54
		// (set) Token: 0x060063A1 RID: 25505 RVA: 0x001A0A6B File Offset: 0x0019EC6B
		[Parameter(Mandatory = false)]
		public bool OWAAlwaysEncrypt
		{
			get
			{
				return (bool)base.Fields["OWAAlwaysEncrypt"];
			}
			set
			{
				base.Fields["OWAAlwaysEncrypt"] = value;
			}
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x001A0A83 File Offset: 0x0019EC83
		// (set) Token: 0x060063A3 RID: 25507 RVA: 0x001A0A9A File Offset: 0x0019EC9A
		[Parameter(Mandatory = false)]
		public bool OWAClearSign
		{
			get
			{
				return (bool)base.Fields["OWAClearSign"];
			}
			set
			{
				base.Fields["OWAClearSign"] = value;
			}
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x001A0AB2 File Offset: 0x0019ECB2
		// (set) Token: 0x060063A5 RID: 25509 RVA: 0x001A0AC9 File Offset: 0x0019ECC9
		[Parameter(Mandatory = false)]
		public bool OWAIncludeCertificateChainWithoutRootCertificate
		{
			get
			{
				return (bool)base.Fields["OWAIncludeCertificateChainWithoutRootCertificate"];
			}
			set
			{
				base.Fields["OWAIncludeCertificateChainWithoutRootCertificate"] = value;
			}
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x060063A6 RID: 25510 RVA: 0x001A0AE1 File Offset: 0x0019ECE1
		// (set) Token: 0x060063A7 RID: 25511 RVA: 0x001A0AF8 File Offset: 0x0019ECF8
		[Parameter(Mandatory = false)]
		public bool OWAIncludeCertificateChainAndRootCertificate
		{
			get
			{
				return (bool)base.Fields["OWAIncludeCertificateChainAndRootCertificate"];
			}
			set
			{
				base.Fields["OWAIncludeCertificateChainAndRootCertificate"] = value;
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x001A0B10 File Offset: 0x0019ED10
		// (set) Token: 0x060063A9 RID: 25513 RVA: 0x001A0B27 File Offset: 0x0019ED27
		[Parameter(Mandatory = false)]
		public bool OWAEncryptTemporaryBuffers
		{
			get
			{
				return (bool)base.Fields["OWAEncryptTemporaryBuffers"];
			}
			set
			{
				base.Fields["OWAEncryptTemporaryBuffers"] = value;
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x060063AA RID: 25514 RVA: 0x001A0B3F File Offset: 0x0019ED3F
		// (set) Token: 0x060063AB RID: 25515 RVA: 0x001A0B56 File Offset: 0x0019ED56
		[Parameter(Mandatory = false)]
		public bool OWASignedEmailCertificateInclusion
		{
			get
			{
				return (bool)base.Fields["OWASignedEmailCertificateInclusion"];
			}
			set
			{
				base.Fields["OWASignedEmailCertificateInclusion"] = value;
			}
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x060063AC RID: 25516 RVA: 0x001A0B6E File Offset: 0x0019ED6E
		// (set) Token: 0x060063AD RID: 25517 RVA: 0x001A0B85 File Offset: 0x0019ED85
		[Parameter(Mandatory = false)]
		public uint OWABCCEncryptedEmailForking
		{
			get
			{
				return (uint)base.Fields["OWABCCEncryptedEmailForking"];
			}
			set
			{
				base.Fields["OWABCCEncryptedEmailForking"] = value;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x060063AE RID: 25518 RVA: 0x001A0B9D File Offset: 0x0019ED9D
		// (set) Token: 0x060063AF RID: 25519 RVA: 0x001A0BB4 File Offset: 0x0019EDB4
		[Parameter(Mandatory = false)]
		public bool OWAIncludeSMIMECapabilitiesInMessage
		{
			get
			{
				return (bool)base.Fields["OWAIncludeSMIMECapabilitiesInMessage"];
			}
			set
			{
				base.Fields["OWAIncludeSMIMECapabilitiesInMessage"] = value;
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x060063B0 RID: 25520 RVA: 0x001A0BCC File Offset: 0x0019EDCC
		// (set) Token: 0x060063B1 RID: 25521 RVA: 0x001A0BE3 File Offset: 0x0019EDE3
		[Parameter(Mandatory = false)]
		public bool OWACopyRecipientHeaders
		{
			get
			{
				return (bool)base.Fields["OWACopyRecipientHeaders"];
			}
			set
			{
				base.Fields["OWACopyRecipientHeaders"] = value;
			}
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x060063B2 RID: 25522 RVA: 0x001A0BFB File Offset: 0x0019EDFB
		// (set) Token: 0x060063B3 RID: 25523 RVA: 0x001A0C12 File Offset: 0x0019EE12
		[Parameter(Mandatory = false)]
		public bool OWAOnlyUseSmartCard
		{
			get
			{
				return (bool)base.Fields["OWAOnlyUseSmartCard"];
			}
			set
			{
				base.Fields["OWAOnlyUseSmartCard"] = value;
			}
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x060063B4 RID: 25524 RVA: 0x001A0C2A File Offset: 0x0019EE2A
		// (set) Token: 0x060063B5 RID: 25525 RVA: 0x001A0C41 File Offset: 0x0019EE41
		[Parameter(Mandatory = false)]
		public bool OWATripleWrapSignedEncryptedMail
		{
			get
			{
				return (bool)base.Fields["OWATripleWrapSignedEncryptedMail"];
			}
			set
			{
				base.Fields["OWATripleWrapSignedEncryptedMail"] = value;
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x060063B6 RID: 25526 RVA: 0x001A0C59 File Offset: 0x0019EE59
		// (set) Token: 0x060063B7 RID: 25527 RVA: 0x001A0C70 File Offset: 0x0019EE70
		[Parameter(Mandatory = false)]
		public bool OWAUseKeyIdentifier
		{
			get
			{
				return (bool)base.Fields["OWAUseKeyIdentifier"];
			}
			set
			{
				base.Fields["OWAUseKeyIdentifier"] = value;
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x060063B8 RID: 25528 RVA: 0x001A0C88 File Offset: 0x0019EE88
		// (set) Token: 0x060063B9 RID: 25529 RVA: 0x001A0C9F File Offset: 0x0019EE9F
		[Parameter(Mandatory = false)]
		public string OWAEncryptionAlgorithms
		{
			get
			{
				return (string)base.Fields["OWAEncryptionAlgorithms"];
			}
			set
			{
				base.Fields["OWAEncryptionAlgorithms"] = value;
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x060063BA RID: 25530 RVA: 0x001A0CB2 File Offset: 0x0019EEB2
		// (set) Token: 0x060063BB RID: 25531 RVA: 0x001A0CC9 File Offset: 0x0019EEC9
		[Parameter(Mandatory = false)]
		public string OWASigningAlgorithms
		{
			get
			{
				return (string)base.Fields["OWASigningAlgorithms"];
			}
			set
			{
				base.Fields["OWASigningAlgorithms"] = value;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x060063BC RID: 25532 RVA: 0x001A0CDC File Offset: 0x0019EEDC
		// (set) Token: 0x060063BD RID: 25533 RVA: 0x001A0CF3 File Offset: 0x0019EEF3
		[Parameter(Mandatory = false)]
		public bool OWAForceSMIMEClientUpgrade
		{
			get
			{
				return (bool)base.Fields["OWAForceSMIMEClientUpgrade"];
			}
			set
			{
				base.Fields["OWAForceSMIMEClientUpgrade"] = value;
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x060063BE RID: 25534 RVA: 0x001A0D0B File Offset: 0x0019EF0B
		// (set) Token: 0x060063BF RID: 25535 RVA: 0x001A0D22 File Offset: 0x0019EF22
		[Parameter(Mandatory = false)]
		public string OWASenderCertificateAttributesToDisplay
		{
			get
			{
				return (string)base.Fields["OWASenderCertificateAttributesToDisplay"];
			}
			set
			{
				base.Fields["OWASenderCertificateAttributesToDisplay"] = value;
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x060063C0 RID: 25536 RVA: 0x001A0D35 File Offset: 0x0019EF35
		// (set) Token: 0x060063C1 RID: 25537 RVA: 0x001A0D4C File Offset: 0x0019EF4C
		[Parameter(Mandatory = false)]
		public bool OWAAllowUserChoiceOfSigningCertificate
		{
			get
			{
				return (bool)base.Fields["OWAAllowUserChoiceOfSigningCertificate"];
			}
			set
			{
				base.Fields["OWAAllowUserChoiceOfSigningCertificate"] = value;
			}
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x060063C2 RID: 25538 RVA: 0x001A0D64 File Offset: 0x0019EF64
		// (set) Token: 0x060063C3 RID: 25539 RVA: 0x001A0D7B File Offset: 0x0019EF7B
		[Parameter(Mandatory = false)]
		public byte[] SMIMECertificateIssuingCA
		{
			get
			{
				return (byte[])base.Fields["SMIMECertificateIssuingCA"];
			}
			set
			{
				base.Fields["SMIMECertificateIssuingCA"] = value;
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x060063C4 RID: 25540 RVA: 0x001A0D8E File Offset: 0x0019EF8E
		protected override ObjectId RootId
		{
			get
			{
				return SmimeConfigurationContainer.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x001A0D9C File Offset: 0x0019EF9C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable configurable = base.PrepareDataObject();
			SmimeConfigurationContainer smimeConfigurationContainer = configurable as SmimeConfigurationContainer;
			if (base.Fields.Contains("SMIMECertificateIssuingCA"))
			{
				byte[] array = base.Fields["SMIMECertificateIssuingCA"] as byte[];
				if (array != null && array.Length != 0)
				{
					if (array.Length > 70000)
					{
						base.WriteError(new InvalidOperationException(string.Format(Strings.SSTFileSizeExceedLimit, 70000.ToString())), ErrorCategory.LimitsExceeded, (configurable != null) ? configurable.Identity : null);
						return smimeConfigurationContainer;
					}
					smimeConfigurationContainer.SMIMECertificateIssuingCA = array;
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					try
					{
						x509Certificate2Collection.Import(array);
					}
					catch (Exception e)
					{
						base.WriteError(new FormatException("SMIMECertificateIssuingCA has wrong format"), ErrorCategory.InvalidData, null);
						TaskLogger.LogError(e);
					}
					List<string> list = new List<string>();
					DateTime dateTime = DateTime.MaxValue;
					string text = string.Empty;
					for (int i = 0; i < x509Certificate2Collection.Count; i++)
					{
						DateTime dateTime2;
						if (DateTime.TryParse(x509Certificate2Collection[i].GetExpirationDateString(), out dateTime2))
						{
							if (dateTime2 < DateTime.UtcNow)
							{
								list.Add(x509Certificate2Collection[i].Thumbprint);
							}
							if (string.IsNullOrWhiteSpace(text) || dateTime2 < dateTime)
							{
								dateTime = dateTime2;
								text = x509Certificate2Collection[i].Thumbprint;
							}
						}
						else
						{
							base.WriteError(new FormatException("Certificate Expiry date has wrong format"), ErrorCategory.InvalidData, null);
						}
					}
					if (list.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string value in list)
						{
							if (stringBuilder.Length != 0)
							{
								stringBuilder.Append(" , ");
							}
							stringBuilder.Append(value);
						}
						base.WriteError(new InvalidOperationException(string.Format(Strings.ExpiryCertMessage, stringBuilder.ToString())), ErrorCategory.LimitsExceeded, null);
						return smimeConfigurationContainer;
					}
					if (dateTime != DateTime.MaxValue)
					{
						smimeConfigurationContainer.SMIMECertificatesExpiryDate = new DateTime?(dateTime);
						smimeConfigurationContainer.SMIMEExpiredCertificateThumbprint = text;
					}
				}
				else
				{
					smimeConfigurationContainer.SMIMECertificateIssuingCA = null;
					smimeConfigurationContainer.SMIMECertificatesExpiryDate = null;
					smimeConfigurationContainer.SMIMEExpiredCertificateThumbprint = string.Empty;
				}
			}
			this.ProcessSmimeRecord(smimeConfigurationContainer);
			TaskLogger.LogExit();
			return smimeConfigurationContainer;
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x001A1000 File Offset: 0x0019F200
		protected override IConfigurable ResolveDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable[] array = null;
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(SmimeConfigurationContainer), this.InternalFilter, this.RootId, this.DeepSearch));
			try
			{
				array = base.DataSession.Find<SmimeConfigurationContainer>(this.InternalFilter, this.RootId, this.DeepSearch, null);
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			if (array == null)
			{
				array = new IConfigurable[0];
			}
			IConfigurable result = null;
			switch (array.Length)
			{
			case 0:
			{
				SmimeConfigurationContainer smimeConfigurationContainer = new SmimeConfigurationContainer();
				smimeConfigurationContainer.SetId(this.RootId as ADObjectId);
				smimeConfigurationContainer.Initialize();
				smimeConfigurationContainer.OrganizationId = base.CurrentOrganizationId;
				result = smimeConfigurationContainer;
				break;
			}
			case 1:
				result = array[0];
				break;
			default:
				TaskLogger.Log(Strings.SmimeConfigAmbiguous);
				break;
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x001A1108 File Offset: 0x0019F308
		private void ProcessSmimeRecord(SmimeConfigurationContainer dataObject)
		{
			if (dataObject == null)
			{
				return;
			}
			if (base.Fields.Contains("OWACheckCRLOnSend"))
			{
				dataObject.OWACheckCRLOnSend = this.OWACheckCRLOnSend;
			}
			if (base.Fields.Contains("OWADLExpansionTimeout"))
			{
				dataObject.OWADLExpansionTimeout = this.OWADLExpansionTimeout;
			}
			if (base.Fields.Contains("OWAUseSecondaryProxiesWhenFindingCertificates"))
			{
				dataObject.OWAUseSecondaryProxiesWhenFindingCertificates = this.OWAUseSecondaryProxiesWhenFindingCertificates;
			}
			if (base.Fields.Contains("OWACRLConnectionTimeout"))
			{
				dataObject.OWACRLConnectionTimeout = this.OWACRLConnectionTimeout;
			}
			if (base.Fields.Contains("OWACRLRetrievalTimeout"))
			{
				dataObject.OWACRLRetrievalTimeout = this.OWACRLRetrievalTimeout;
			}
			if (base.Fields.Contains("OWADisableCRLCheck"))
			{
				dataObject.OWADisableCRLCheck = this.OWADisableCRLCheck;
			}
			if (base.Fields.Contains("OWAAlwaysEncrypt"))
			{
				dataObject.OWAAlwaysEncrypt = this.OWAAlwaysEncrypt;
			}
			if (base.Fields.Contains("OWAAlwaysSign"))
			{
				dataObject.OWAAlwaysSign = this.OWAAlwaysSign;
			}
			if (base.Fields.Contains("OWAClearSign"))
			{
				dataObject.OWAClearSign = this.OWAClearSign;
			}
			if (base.Fields.Contains("OWAIncludeCertificateChainWithoutRootCertificate"))
			{
				dataObject.OWAIncludeCertificateChainWithoutRootCertificate = this.OWAIncludeCertificateChainWithoutRootCertificate;
			}
			if (base.Fields.Contains("OWAIncludeCertificateChainAndRootCertificate"))
			{
				dataObject.OWAIncludeCertificateChainAndRootCertificate = this.OWAIncludeCertificateChainAndRootCertificate;
			}
			if (base.Fields.Contains("OWAEncryptTemporaryBuffers"))
			{
				dataObject.OWAEncryptTemporaryBuffers = this.OWAEncryptTemporaryBuffers;
			}
			if (base.Fields.Contains("OWASignedEmailCertificateInclusion"))
			{
				dataObject.OWASignedEmailCertificateInclusion = this.OWASignedEmailCertificateInclusion;
			}
			if (base.Fields.Contains("OWABCCEncryptedEmailForking"))
			{
				dataObject.OWABCCEncryptedEmailForking = this.OWABCCEncryptedEmailForking;
			}
			if (base.Fields.Contains("OWAIncludeSMIMECapabilitiesInMessage"))
			{
				dataObject.OWAIncludeSMIMECapabilitiesInMessage = this.OWAIncludeSMIMECapabilitiesInMessage;
			}
			if (base.Fields.Contains("OWACopyRecipientHeaders"))
			{
				dataObject.OWACopyRecipientHeaders = this.OWACopyRecipientHeaders;
			}
			if (base.Fields.Contains("OWAOnlyUseSmartCard"))
			{
				dataObject.OWAOnlyUseSmartCard = this.OWAOnlyUseSmartCard;
			}
			if (base.Fields.Contains("OWATripleWrapSignedEncryptedMail"))
			{
				dataObject.OWATripleWrapSignedEncryptedMail = this.OWATripleWrapSignedEncryptedMail;
			}
			if (base.Fields.Contains("OWAUseKeyIdentifier"))
			{
				dataObject.OWAUseKeyIdentifier = this.OWAUseKeyIdentifier;
			}
			if (base.Fields.Contains("OWAEncryptionAlgorithms"))
			{
				dataObject.OWAEncryptionAlgorithms = this.OWAEncryptionAlgorithms;
			}
			if (base.Fields.Contains("OWASigningAlgorithms"))
			{
				dataObject.OWASigningAlgorithms = this.OWASigningAlgorithms;
			}
			if (base.Fields.Contains("OWAForceSMIMEClientUpgrade"))
			{
				dataObject.OWAForceSMIMEClientUpgrade = this.OWAForceSMIMEClientUpgrade;
			}
			if (base.Fields.Contains("OWASenderCertificateAttributesToDisplay"))
			{
				dataObject.OWASenderCertificateAttributesToDisplay = this.OWASenderCertificateAttributesToDisplay;
			}
			if (base.Fields.Contains("OWAAllowUserChoiceOfSigningCertificate"))
			{
				dataObject.OWAAllowUserChoiceOfSigningCertificate = this.OWAAllowUserChoiceOfSigningCertificate;
			}
			dataObject.SaveSettings();
		}

		// Token: 0x040035F0 RID: 13808
		private const int SSTFileSizeLimit = 70000;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200077B RID: 1915
	[Serializable]
	public class UMMailbox : ADPresentationObject
	{
		// Token: 0x06005F18 RID: 24344 RVA: 0x001451D3 File Offset: 0x001433D3
		internal static RecipientTypeDetails[] GetUMRecipientTypeDetails()
		{
			return (RecipientTypeDetails[])UMMailbox.AllowedRecipientTypeDetails.Clone();
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001451E4 File Offset: 0x001433E4
		internal static MultiValuedProperty<string> GetExtensionsFromEmailAddresses(ProxyAddressCollection emailAddresses)
		{
			List<string> list = new List<string>();
			bool flag = UMMailbox.ContainsMoreThanOneDialplan(emailAddresses);
			foreach (ProxyAddress proxyAddress in emailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.UM)
				{
					int num = proxyAddress.AddressString.IndexOf(";");
					if (num != -1)
					{
						if (flag)
						{
							string phoneContextFromProxyAddress = UMMailbox.GetPhoneContextFromProxyAddress(proxyAddress);
							string extensionFromProxyAddress = UMMailbox.GetExtensionFromProxyAddress(proxyAddress);
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("{0} ({1})", extensionFromProxyAddress, phoneContextFromProxyAddress);
							list.Add(stringBuilder.ToString());
						}
						else
						{
							list.Add(UMMailbox.GetExtensionFromProxyAddress(proxyAddress));
						}
					}
				}
			}
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			multiValuedProperty.CopyChangesOnly = true;
			foreach (string item in list)
			{
				multiValuedProperty.Add(item);
			}
			return multiValuedProperty;
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x06005F1A RID: 24346 RVA: 0x001452F8 File Offset: 0x001434F8
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return UMMailbox.schema;
			}
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x001452FF File Offset: 0x001434FF
		public UMMailbox()
		{
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x00145307 File Offset: 0x00143507
		public UMMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x00145310 File Offset: 0x00143510
		internal static UMMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new UMMailbox(dataObject);
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x0014531D File Offset: 0x0014351D
		protected override IEnumerable<PropertyInfo> CloneableProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = UMMailbox.cloneableProps) == null)
				{
					result = (UMMailbox.cloneableProps = ADPresentationObject.GetCloneableProperties(this));
				}
				return result;
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06005F1F RID: 24351 RVA: 0x00145334 File Offset: 0x00143534
		protected override IEnumerable<PropertyInfo> CloneableOnceProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = UMMailbox.cloneableOnceProps) == null)
				{
					result = (UMMailbox.cloneableOnceProps = ADPresentationObject.GetCloneableOnceProperties(this));
				}
				return result;
			}
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x0014534B File Offset: 0x0014354B
		protected override IEnumerable<PropertyInfo> CloneableEnabledStateProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = UMMailbox.cloneableEnabledStateProps) == null)
				{
					result = (UMMailbox.cloneableEnabledStateProps = ADPresentationObject.GetCloneableEnabledStateProperties(this));
				}
				return result;
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06005F21 RID: 24353 RVA: 0x00145362 File Offset: 0x00143562
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06005F22 RID: 24354 RVA: 0x00145369 File Offset: 0x00143569
		// (set) Token: 0x06005F23 RID: 24355 RVA: 0x0014537B File Offset: 0x0014357B
		internal bool UMProvisioningRequested
		{
			get
			{
				return (bool)this[UMMailboxSchema.UMProvisioningRequested];
			}
			set
			{
				this[UMMailboxSchema.UMProvisioningRequested] = value;
			}
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06005F24 RID: 24356 RVA: 0x0014538E File Offset: 0x0014358E
		public string DisplayName
		{
			get
			{
				return (string)this[UMMailboxSchema.DisplayName];
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x001453A0 File Offset: 0x001435A0
		// (set) Token: 0x06005F26 RID: 24358 RVA: 0x001453B2 File Offset: 0x001435B2
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[UMMailboxSchema.EmailAddresses];
			}
			internal set
			{
				this[UMMailboxSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x06005F27 RID: 24359 RVA: 0x001453C0 File Offset: 0x001435C0
		public ProxyAddressCollection UMAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[UMMailboxSchema.UMAddresses];
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x06005F28 RID: 24360 RVA: 0x001453D2 File Offset: 0x001435D2
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[UMMailboxSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x06005F29 RID: 24361 RVA: 0x001453E4 File Offset: 0x001435E4
		public string LinkedMasterAccount
		{
			get
			{
				return (string)this[UMMailboxSchema.LinkedMasterAccount];
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x06005F2A RID: 24362 RVA: 0x001453F6 File Offset: 0x001435F6
		// (set) Token: 0x06005F2B RID: 24363 RVA: 0x00145408 File Offset: 0x00143608
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[UMMailboxSchema.PrimarySmtpAddress];
			}
			internal set
			{
				this[UMMailboxSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x06005F2C RID: 24364 RVA: 0x0014541B File Offset: 0x0014361B
		// (set) Token: 0x06005F2D RID: 24365 RVA: 0x0014542D File Offset: 0x0014362D
		public string SamAccountName
		{
			get
			{
				return (string)this[UMMailboxSchema.SamAccountName];
			}
			internal set
			{
				this[UMMailboxSchema.SamAccountName] = value;
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x06005F2E RID: 24366 RVA: 0x0014543B File Offset: 0x0014363B
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[UMMailboxSchema.ServerLegacyDN];
			}
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x0014544D File Offset: 0x0014364D
		public string ServerName
		{
			get
			{
				return (string)this[UMMailboxSchema.ServerName];
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x0014545F File Offset: 0x0014365F
		public MultiValuedProperty<string> UMDtmfMap
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxSchema.UMDtmfMap];
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x00145471 File Offset: 0x00143671
		// (set) Token: 0x06005F32 RID: 24370 RVA: 0x00145483 File Offset: 0x00143683
		public bool UMEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.UMEnabled];
			}
			internal set
			{
				this[UMMailboxSchema.UMEnabled] = value;
			}
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x06005F33 RID: 24371 RVA: 0x00145496 File Offset: 0x00143696
		// (set) Token: 0x06005F34 RID: 24372 RVA: 0x001454A8 File Offset: 0x001436A8
		[Parameter(Mandatory = false)]
		public bool TUIAccessToCalendarEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.TUIAccessToCalendarEnabled];
			}
			set
			{
				this[UMMailboxSchema.TUIAccessToCalendarEnabled] = value;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x06005F35 RID: 24373 RVA: 0x001454BB File Offset: 0x001436BB
		// (set) Token: 0x06005F36 RID: 24374 RVA: 0x001454CD File Offset: 0x001436CD
		[Parameter(Mandatory = false)]
		public bool FaxEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.FaxEnabled];
			}
			set
			{
				this[UMMailboxSchema.FaxEnabled] = value;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x001454E0 File Offset: 0x001436E0
		// (set) Token: 0x06005F38 RID: 24376 RVA: 0x001454F2 File Offset: 0x001436F2
		[Parameter(Mandatory = false)]
		public bool TUIAccessToEmailEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.TUIAccessToEmailEnabled];
			}
			set
			{
				this[UMMailboxSchema.TUIAccessToEmailEnabled] = value;
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x00145505 File Offset: 0x00143705
		// (set) Token: 0x06005F3A RID: 24378 RVA: 0x00145517 File Offset: 0x00143717
		[Parameter(Mandatory = false)]
		public bool SubscriberAccessEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.SubscriberAccessEnabled];
			}
			set
			{
				this[UMMailboxSchema.SubscriberAccessEnabled] = value;
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x0014552A File Offset: 0x0014372A
		// (set) Token: 0x06005F3C RID: 24380 RVA: 0x0014553C File Offset: 0x0014373C
		[Parameter(Mandatory = false)]
		public bool MissedCallNotificationEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.MissedCallNotificationEnabled];
			}
			set
			{
				this[UMMailboxSchema.MissedCallNotificationEnabled] = value;
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x06005F3D RID: 24381 RVA: 0x0014554F File Offset: 0x0014374F
		// (set) Token: 0x06005F3E RID: 24382 RVA: 0x00145561 File Offset: 0x00143761
		[Parameter(Mandatory = false)]
		public UMSMSNotificationOptions UMSMSNotificationOption
		{
			get
			{
				return (UMSMSNotificationOptions)this[UMMailboxSchema.UMSMSNotificationOption];
			}
			set
			{
				this[UMMailboxSchema.UMSMSNotificationOption] = value;
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x00145574 File Offset: 0x00143774
		// (set) Token: 0x06005F40 RID: 24384 RVA: 0x00145586 File Offset: 0x00143786
		[Parameter(Mandatory = false)]
		public bool PinlessAccessToVoiceMailEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.PinlessAccessToVoiceMailEnabled];
			}
			set
			{
				this[UMMailboxSchema.PinlessAccessToVoiceMailEnabled] = value;
			}
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x00145599 File Offset: 0x00143799
		// (set) Token: 0x06005F42 RID: 24386 RVA: 0x001455AB File Offset: 0x001437AB
		[Parameter(Mandatory = false)]
		public bool AnonymousCallersCanLeaveMessages
		{
			get
			{
				return (bool)this[UMMailboxSchema.AnonymousCallersCanLeaveMessages];
			}
			set
			{
				this[UMMailboxSchema.AnonymousCallersCanLeaveMessages] = value;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x001455BE File Offset: 0x001437BE
		// (set) Token: 0x06005F44 RID: 24388 RVA: 0x001455D0 File Offset: 0x001437D0
		[Parameter(Mandatory = false)]
		public bool AutomaticSpeechRecognitionEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.ASREnabled];
			}
			set
			{
				this[UMMailboxSchema.ASREnabled] = value;
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06005F45 RID: 24389 RVA: 0x001455E3 File Offset: 0x001437E3
		// (set) Token: 0x06005F46 RID: 24390 RVA: 0x001455F5 File Offset: 0x001437F5
		[Parameter(Mandatory = false)]
		public bool VoiceMailAnalysisEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.VoiceMailAnalysisEnabled];
			}
			set
			{
				this[UMMailboxSchema.VoiceMailAnalysisEnabled] = value;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06005F47 RID: 24391 RVA: 0x00145608 File Offset: 0x00143808
		// (set) Token: 0x06005F48 RID: 24392 RVA: 0x0014561A File Offset: 0x0014381A
		[Parameter(Mandatory = false)]
		public bool PlayOnPhoneEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.PlayOnPhoneEnabled];
			}
			set
			{
				this[UMMailboxSchema.PlayOnPhoneEnabled] = value;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x06005F49 RID: 24393 RVA: 0x0014562D File Offset: 0x0014382D
		// (set) Token: 0x06005F4A RID: 24394 RVA: 0x0014563F File Offset: 0x0014383F
		[Parameter(Mandatory = false)]
		public bool CallAnsweringRulesEnabled
		{
			get
			{
				return (bool)this[UMMailboxSchema.CallAnsweringRulesEnabled];
			}
			set
			{
				this[UMMailboxSchema.CallAnsweringRulesEnabled] = value;
			}
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x06005F4B RID: 24395 RVA: 0x00145652 File Offset: 0x00143852
		// (set) Token: 0x06005F4C RID: 24396 RVA: 0x00145664 File Offset: 0x00143864
		[Parameter(Mandatory = false)]
		public AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
		{
			get
			{
				return (AllowUMCallsFromNonUsersFlags)this[UMMailboxSchema.AllowUMCallsFromNonUsers];
			}
			set
			{
				this[UMMailboxSchema.AllowUMCallsFromNonUsers] = value;
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x06005F4D RID: 24397 RVA: 0x00145677 File Offset: 0x00143877
		// (set) Token: 0x06005F4E RID: 24398 RVA: 0x00145689 File Offset: 0x00143889
		[Parameter(Mandatory = false)]
		public string OperatorNumber
		{
			get
			{
				return (string)this[UMMailboxSchema.OperatorNumber];
			}
			set
			{
				this[UMMailboxSchema.OperatorNumber] = value;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06005F4F RID: 24399 RVA: 0x00145697 File Offset: 0x00143897
		// (set) Token: 0x06005F50 RID: 24400 RVA: 0x001456A9 File Offset: 0x001438A9
		[Parameter(Mandatory = false)]
		public string PhoneProviderId
		{
			get
			{
				return (string)this[UMMailboxSchema.PhoneProviderId];
			}
			set
			{
				this[UMMailboxSchema.PhoneProviderId] = value;
			}
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06005F51 RID: 24401 RVA: 0x001456B7 File Offset: 0x001438B7
		public ADObjectId UMDialPlan
		{
			get
			{
				return (ADObjectId)this[UMMailboxSchema.UMRecipientDialPlanId];
			}
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x001456C9 File Offset: 0x001438C9
		// (set) Token: 0x06005F53 RID: 24403 RVA: 0x001456DB File Offset: 0x001438DB
		public ADObjectId UMMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[UMMailboxSchema.UMMailboxPolicy];
			}
			set
			{
				this[UMMailboxSchema.UMMailboxPolicy] = value;
			}
		}

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x001456E9 File Offset: 0x001438E9
		public MultiValuedProperty<string> Extensions
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxSchema.Extensions];
			}
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x001456FB File Offset: 0x001438FB
		// (set) Token: 0x06005F56 RID: 24406 RVA: 0x0014570D File Offset: 0x0014390D
		[Parameter(Mandatory = false)]
		public AudioCodecEnum? CallAnsweringAudioCodec
		{
			get
			{
				return (AudioCodecEnum?)this[UMMailboxSchema.CallAnsweringAudioCodec];
			}
			set
			{
				this[UMMailboxSchema.CallAnsweringAudioCodec] = value;
			}
		}

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x06005F57 RID: 24407 RVA: 0x00145720 File Offset: 0x00143920
		// (set) Token: 0x06005F58 RID: 24408 RVA: 0x00145732 File Offset: 0x00143932
		public string SIPResourceIdentifier
		{
			get
			{
				return (string)this[UMMailboxSchema.SIPResourceIdentifier];
			}
			internal set
			{
				this[UMMailboxSchema.SIPResourceIdentifier] = value;
			}
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x06005F59 RID: 24409 RVA: 0x00145740 File Offset: 0x00143940
		// (set) Token: 0x06005F5A RID: 24410 RVA: 0x00145752 File Offset: 0x00143952
		public string PhoneNumber
		{
			get
			{
				return (string)this[UMMailboxSchema.PhoneNumber];
			}
			internal set
			{
				this[UMMailboxSchema.PhoneNumber] = value;
			}
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x06005F5B RID: 24411 RVA: 0x00145760 File Offset: 0x00143960
		// (set) Token: 0x06005F5C RID: 24412 RVA: 0x00145772 File Offset: 0x00143972
		public MultiValuedProperty<string> AccessTelephoneNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxSchema.AccessTelephoneNumbers];
			}
			internal set
			{
				this[UMMailboxSchema.AccessTelephoneNumbers] = value;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x06005F5D RID: 24413 RVA: 0x00145780 File Offset: 0x00143980
		// (set) Token: 0x06005F5E RID: 24414 RVA: 0x00145792 File Offset: 0x00143992
		public MultiValuedProperty<string> CallAnsweringRulesExtensions
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxSchema.CallAnsweringRulesExtensions];
			}
			internal set
			{
				this[UMMailboxSchema.CallAnsweringRulesExtensions] = value;
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06005F5F RID: 24415 RVA: 0x001457A0 File Offset: 0x001439A0
		public MultiValuedProperty<string> AirSyncNumbers
		{
			get
			{
				ProxyAddressCollection addresses = (ProxyAddressCollection)this[UMMailboxSchema.UMAddresses];
				return UMMailbox.GetExtensionsFromCollection(addresses, ProxyAddressPrefix.ASUM, null);
			}
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x001457CA File Offset: 0x001439CA
		// (set) Token: 0x06005F61 RID: 24417 RVA: 0x001457DC File Offset: 0x001439DC
		[Parameter(Mandatory = false)]
		public bool ImListMigrationCompleted
		{
			get
			{
				return (bool)this[UMMailboxSchema.UCSImListMigrationCompleted];
			}
			set
			{
				this[UMMailboxSchema.UCSImListMigrationCompleted] = value;
			}
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x001457F0 File Offset: 0x001439F0
		internal static MultiValuedProperty<string> ExtensionsGetter(IPropertyBag propertyBag)
		{
			ProxyAddressCollection emailAddresses = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			return UMMailbox.GetExtensionsFromEmailAddresses(emailAddresses);
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x00145814 File Offset: 0x00143A14
		internal static void AddProxy(ADRecipient recipient, ProxyAddressCollection addresses, string extension, UMDialPlan dialPlan, ProxyAddressPrefix prefix)
		{
			string prefixString = (null == UMMailbox.GetPrimaryExtension(addresses, prefix)) ? prefix.PrimaryPrefix : prefix.SecondaryPrefix;
			addresses.Add(UMMailbox.BuildProxyAddressFromExtensionAndPhoneContext(extension, prefixString, dialPlan.PhoneContext));
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x00145854 File Offset: 0x00143A54
		internal static void ClearProxy(ADRecipient recipient, ProxyAddressCollection targetAddresses, ProxyAddressPrefix targetPrefix, Hashtable safeTable)
		{
			Hashtable hashtable = new Hashtable();
			foreach (ProxyAddress proxyAddress in targetAddresses)
			{
				if (proxyAddress.Prefix == targetPrefix && (safeTable == null || !safeTable.ContainsKey(proxyAddress.AddressString)))
				{
					hashtable.Add(proxyAddress.AddressString, true);
				}
			}
			UMMailbox.RemoveProxy(recipient, targetAddresses, targetPrefix, hashtable);
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x001458DC File Offset: 0x00143ADC
		internal static void RemoveProxy(ADRecipient recipient, ProxyAddressCollection collection, ProxyAddressPrefix prefix, Hashtable addressStringTable)
		{
			List<ProxyAddress> list = new List<ProxyAddress>();
			foreach (ProxyAddress proxyAddress in collection)
			{
				if (proxyAddress.Prefix == prefix && addressStringTable.ContainsKey(proxyAddress.AddressString))
				{
					list.Add(proxyAddress);
				}
			}
			foreach (ProxyAddress item in list)
			{
				collection.Remove(item);
			}
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x0014598C File Offset: 0x00143B8C
		internal static void RemoveProxy(ADRecipient recipient, ProxyAddressCollection collection, ProxyAddressPrefix prefix, ArrayList phoneNumbers, UMDialPlan dialPlan)
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in phoneNumbers)
			{
				string extension = (string)obj;
				hashtable.Add(UMMailbox.BuildAddressStringFromExtensionAndPhoneContext(extension, dialPlan.PhoneContext), true);
			}
			UMMailbox.RemoveProxy(recipient, collection, prefix, hashtable);
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x00145A04 File Offset: 0x00143C04
		internal static string GetPrimaryExtension(ProxyAddressCollection emailAddresses, ProxyAddressPrefix prefix)
		{
			foreach (ProxyAddress proxyAddress in emailAddresses)
			{
				if (proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == prefix)
				{
					string extensionFromProxyAddress = UMMailbox.GetExtensionFromProxyAddress(proxyAddress);
					if (extensionFromProxyAddress != null)
					{
						return extensionFromProxyAddress;
					}
				}
			}
			return null;
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x00145A74 File Offset: 0x00143C74
		internal static bool ContainsMoreThanOneDialplan(ProxyAddressCollection emailAddresses)
		{
			string text = null;
			foreach (ProxyAddress proxyAddress in emailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.UM)
				{
					string phoneContextFromProxyAddress = UMMailbox.GetPhoneContextFromProxyAddress(proxyAddress);
					if (text == null)
					{
						text = phoneContextFromProxyAddress;
					}
					else if (string.Compare(phoneContextFromProxyAddress, text, StringComparison.OrdinalIgnoreCase) != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x00145AF0 File Offset: 0x00143CF0
		internal static MultiValuedProperty<string> GetExtensionsFromCollection(ProxyAddressCollection addresses, ProxyAddressPrefix prefix, string phoneContext)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (ProxyAddress proxyAddress in addresses)
			{
				if (proxyAddress.Prefix == prefix && (phoneContext == null || proxyAddress.AddressString.EndsWith(phoneContext)))
				{
					string extensionFromProxyAddress = UMMailbox.GetExtensionFromProxyAddress(proxyAddress);
					if (extensionFromProxyAddress != null)
					{
						multiValuedProperty.Add(extensionFromProxyAddress);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x00145B70 File Offset: 0x00143D70
		internal static List<string> GetDialPlanPhoneContexts(ProxyAddressCollection proxyAddresses, bool excludePrimaryContext)
		{
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			List<string> list = new List<string>();
			foreach (ProxyAddress proxyAddress in proxyAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.UM && (!proxyAddress.IsPrimaryAddress || !excludePrimaryContext))
				{
					string phoneContextFromProxyAddress = UMMailbox.GetPhoneContextFromProxyAddress(proxyAddress);
					if (phoneContextFromProxyAddress != null)
					{
						list.Add(phoneContextFromProxyAddress);
					}
				}
			}
			return list;
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x00145BFC File Offset: 0x00143DFC
		internal static Hashtable GetAirSyncSafeTable(ProxyAddressCollection collection, ProxyAddressPrefix prefix, UMDialPlan dialPlan)
		{
			Hashtable hashtable = new Hashtable();
			foreach (ProxyAddress proxyAddress in collection)
			{
				if (proxyAddress.Prefix == prefix)
				{
					hashtable.Add(proxyAddress.AddressString, true);
					hashtable.Add(proxyAddress.AddressString.Substring(dialPlan.CountryOrRegionCode.Length + 1), true);
				}
			}
			return hashtable;
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x00145C90 File Offset: 0x00143E90
		internal static bool PhoneNumberExists(ProxyAddressCollection addresses, ProxyAddressPrefix prefix, string phoneNumber)
		{
			foreach (ProxyAddress proxyAddress in addresses)
			{
				if (proxyAddress.Prefix == prefix)
				{
					string extensionFromProxyAddress = UMMailbox.GetExtensionFromProxyAddress(proxyAddress);
					if (extensionFromProxyAddress.Equals(phoneNumber))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x00145CFC File Offset: 0x00143EFC
		internal static int ProxyAddressCount(ProxyAddressCollection collection, ProxyAddressPrefix prefix)
		{
			int num = 0;
			foreach (ProxyAddress proxyAddress in collection)
			{
				if (proxyAddress.Prefix == prefix)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x00145D58 File Offset: 0x00143F58
		internal static bool ExtractExtensionInformation(ProxyAddress address, out string extension, out string phoneContext, out string dialplanDisplayName)
		{
			if (address == null)
			{
				throw new ArgumentException("address");
			}
			extension = null;
			phoneContext = null;
			dialplanDisplayName = null;
			if (address.Prefix == ProxyAddressPrefix.UM)
			{
				extension = UMMailbox.GetExtensionFromProxyAddress(address);
				phoneContext = UMMailbox.GetPhoneContextFromProxyAddress(address);
				dialplanDisplayName = null;
				if (phoneContext != null)
				{
					dialplanDisplayName = phoneContext.Split(new char[]
					{
						'.'
					})[0];
				}
			}
			return extension != null && phoneContext != null && dialplanDisplayName != null;
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x00145DD5 File Offset: 0x00143FD5
		internal static ProxyAddress BuildProxyAddressFromExtensionAndPhoneContext(string extension, string prefixString, string phoneContext)
		{
			return ProxyAddress.Parse(prefixString, UMMailbox.BuildAddressStringFromExtensionAndPhoneContext(extension, phoneContext));
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x00145DE4 File Offset: 0x00143FE4
		private static string BuildAddressStringFromExtensionAndPhoneContext(string extension, string phoneContext)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}{1}{2}{3}", new object[]
			{
				extension,
				";",
				"phone-context=",
				phoneContext
			});
			return stringBuilder.ToString();
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x00145E2C File Offset: 0x0014402C
		private static bool IsProxyAddressLocalExtension(ProxyAddress emailAddress)
		{
			string extensionFromProxyAddress = UMMailbox.GetExtensionFromProxyAddress(emailAddress);
			return extensionFromProxyAddress != null && !Regex.IsMatch(extensionFromProxyAddress, "[^0-9]");
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x00145E54 File Offset: 0x00144054
		private static string GetExtensionFromProxyAddress(ProxyAddress emailAddress)
		{
			int num = emailAddress.AddressString.IndexOf(";");
			if (-1 != num)
			{
				return emailAddress.AddressString.Substring(0, num);
			}
			return null;
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x00145E88 File Offset: 0x00144088
		private static string GetPhoneContextFromProxyAddress(ProxyAddress emailAddress)
		{
			int num = emailAddress.AddressString.IndexOf("phone-context=");
			if (-1 != num)
			{
				return emailAddress.AddressString.Substring(num + "phone-context=".Length);
			}
			return null;
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x00145EC4 File Offset: 0x001440C4
		internal string GetEUMPhoneNumber(UMDialPlan dialPlan)
		{
			Hashtable airSyncSafeTable = UMMailbox.GetAirSyncSafeTable(this.UMAddresses, ProxyAddressPrefix.ASUM, dialPlan);
			foreach (ProxyAddress proxyAddress in this.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.UM)
				{
					string phoneContextFromProxyAddress = UMMailbox.GetPhoneContextFromProxyAddress(proxyAddress);
					if (phoneContextFromProxyAddress == dialPlan.PhoneContext && UMMailbox.IsProxyAddressLocalExtension(proxyAddress) && !airSyncSafeTable.ContainsKey(proxyAddress.AddressString))
					{
						return UMMailbox.GetExtensionFromProxyAddress(proxyAddress);
					}
				}
			}
			return null;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x00145F70 File Offset: 0x00144170
		internal UMDialPlan GetDialPlan()
		{
			IConfigurationSession configurationSession = this.GetConfigurationSession();
			return configurationSession.Read<UMDialPlan>(this.UMDialPlan);
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x00145F94 File Offset: 0x00144194
		internal UMMailboxPolicy GetPolicy()
		{
			IConfigurationSession configurationSession = this.GetConfigurationSession();
			return configurationSession.Read<UMMailboxPolicy>(this.UMMailboxPolicy);
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x00145FB4 File Offset: 0x001441B4
		internal static QueryFilter GetUMEnabledUserQueryFilter(MailboxDatabase database)
		{
			return new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Database, database.Id),
				new TextFilter(ADRecipientSchema.EmailAddresses, ProxyAddressPrefix.UM.PrimaryPrefix, MatchOptions.Prefix, MatchFlags.Default),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailboxPlan),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.ArbitrationMailbox),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.SystemMailbox),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.SystemAttendantMailbox)
			});
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x00146060 File Offset: 0x00144260
		private IConfigurationSession GetConfigurationSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.DataObject.OrganizationId, null, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 1179, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Management\\UMMailbox.cs");
		}

		// Token: 0x04004021 RID: 16417
		internal const string HostedVoiceMailEnabled = "ExchangeHostedVoiceMail=1";

		// Token: 0x04004022 RID: 16418
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.RoomMailbox,
			RecipientTypeDetails.EquipmentMailbox,
			RecipientTypeDetails.LegacyMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.SharedMailbox
		};

		// Token: 0x04004023 RID: 16419
		private static UMMailboxSchema schema = ObjectSchema.GetInstance<UMMailboxSchema>();

		// Token: 0x04004024 RID: 16420
		private static IEnumerable<PropertyInfo> cloneableProps;

		// Token: 0x04004025 RID: 16421
		private static IEnumerable<PropertyInfo> cloneableOnceProps;

		// Token: 0x04004026 RID: 16422
		private static IEnumerable<PropertyInfo> cloneableEnabledStateProps;
	}
}

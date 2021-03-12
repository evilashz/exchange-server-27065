using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000063 RID: 99
	internal class DialPermissionWrapper
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000D904 File Offset: 0x0000BB04
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public ADObjectId SearchRoot
		{
			get
			{
				return this.searchRoot;
			}
			set
			{
				this.searchRoot = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D915 File Offset: 0x0000BB15
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000D91D File Offset: 0x0000BB1D
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			set
			{
				this.organizationId = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D926 File Offset: 0x0000BB26
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000D92E File Offset: 0x0000BB2E
		public string Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000D937 File Offset: 0x0000BB37
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000D93F File Offset: 0x0000BB3F
		public MultiValuedProperty<string> AllowedInCountryGroups
		{
			get
			{
				return this.allowedInCountryGroups;
			}
			set
			{
				this.allowedInCountryGroups = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000D948 File Offset: 0x0000BB48
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000D950 File Offset: 0x0000BB50
		public MultiValuedProperty<string> AllowedInternationalGroups
		{
			get
			{
				return this.allowedInternationalGroups;
			}
			set
			{
				this.allowedInternationalGroups = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000D959 File Offset: 0x0000BB59
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000D961 File Offset: 0x0000BB61
		public bool DialPlanSubscribersAllowed
		{
			get
			{
				return this.dialPlanSubscribersAllowed;
			}
			set
			{
				this.dialPlanSubscribersAllowed = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000D96A File Offset: 0x0000BB6A
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000D972 File Offset: 0x0000BB72
		public bool ExtensionLengthNumbersAllowed
		{
			get
			{
				return this.extensionLengthNumbersAllowed;
			}
			set
			{
				this.extensionLengthNumbersAllowed = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000D97B File Offset: 0x0000BB7B
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000D983 File Offset: 0x0000BB83
		public bool CallSomeoneEnabled
		{
			get
			{
				return this.callSomeoneEnabled;
			}
			set
			{
				this.callSomeoneEnabled = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000D98C File Offset: 0x0000BB8C
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000D994 File Offset: 0x0000BB94
		public bool SendVoiceMessageEnabled
		{
			get
			{
				return this.sendVoiceMessageEnabled;
			}
			set
			{
				this.sendVoiceMessageEnabled = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000D99D File Offset: 0x0000BB9D
		internal bool CallingNonUmExtensionsAllowed
		{
			get
			{
				return this.extensionLengthNumbersAllowed;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000D9A5 File Offset: 0x0000BBA5
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000D9AD File Offset: 0x0000BBAD
		internal DialPermissionType Category
		{
			get
			{
				return this.category;
			}
			set
			{
				this.category = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		internal DialScopeEnum ContactScope
		{
			get
			{
				return this.contactScope;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		internal static DialPermissionWrapper CreateFromDialPlan(UMDialPlan dialPlan)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, null, "DialPermissionWrapper::CreateFromDialPlan(for DP = {0})", new object[]
			{
				dialPlan.Name
			});
			DialPermissionWrapper dialPermissionWrapper = new DialPermissionWrapper();
			dialPermissionWrapper.Category = DialPermissionType.DialPlan;
			dialPermissionWrapper.Identity = dialPlan.Id.DistinguishedName;
			dialPermissionWrapper.AllowedInCountryGroups = dialPlan.AllowedInCountryOrRegionGroups;
			dialPermissionWrapper.AllowedInternationalGroups = dialPlan.AllowedInternationalGroups;
			dialPermissionWrapper.DialPlanSubscribersAllowed = dialPlan.AllowDialPlanSubscribers;
			dialPermissionWrapper.ExtensionLengthNumbersAllowed = dialPlan.AllowExtensions;
			dialPermissionWrapper.CallSomeoneEnabled = dialPlan.CallSomeoneEnabled;
			dialPermissionWrapper.contactScope = DialScopeEnum.DialPlan;
			switch (dialPlan.ContactScope)
			{
			case CallSomeoneScopeEnum.DialPlan:
				dialPermissionWrapper.contactScope = DialScopeEnum.DialPlan;
				goto IL_C7;
			case CallSomeoneScopeEnum.GlobalAddressList:
				dialPermissionWrapper.contactScope = DialScopeEnum.GlobalAddressList;
				goto IL_C7;
			case CallSomeoneScopeEnum.AddressList:
				dialPermissionWrapper.contactScope = DialScopeEnum.AddressList;
				goto IL_C7;
			}
			dialPermissionWrapper.contactScope = DialScopeEnum.DialPlan;
			IL_C7:
			dialPermissionWrapper.SendVoiceMessageEnabled = dialPlan.SendVoiceMsgEnabled;
			dialPermissionWrapper.SearchRoot = dialPlan.ContactAddressList;
			dialPermissionWrapper.OrganizationId = dialPlan.OrganizationId;
			return dialPermissionWrapper;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000DABC File Offset: 0x0000BCBC
		internal static DialPermissionWrapper CreateFromAutoAttendant(UMAutoAttendant autoAttendant)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, null, "DialPermissionWrapper::CreateFromAutoAttendant(for AA = {0})", new object[]
			{
				autoAttendant.Name
			});
			return new DialPermissionWrapper
			{
				Category = DialPermissionType.AutoAttendant,
				Identity = autoAttendant.Id.DistinguishedName,
				AllowedInCountryGroups = autoAttendant.AllowedInCountryOrRegionGroups,
				AllowedInternationalGroups = autoAttendant.AllowedInternationalGroups,
				DialPlanSubscribersAllowed = autoAttendant.AllowDialPlanSubscribers,
				ExtensionLengthNumbersAllowed = autoAttendant.AllowExtensions,
				CallSomeoneEnabled = autoAttendant.CallSomeoneEnabled,
				contactScope = autoAttendant.ContactScope,
				SendVoiceMessageEnabled = autoAttendant.SendVoiceMsgEnabled,
				SearchRoot = autoAttendant.ContactAddressList,
				OrganizationId = autoAttendant.OrganizationId
			};
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000DB78 File Offset: 0x0000BD78
		internal static DialPermissionWrapper CreateFromRecipientPolicy(ADUser user)
		{
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, user.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, null, data, "DialPermissionWrapper::CreateFromRecipientPolicy(for user = _UserDisplayName)", new object[0]);
			DialPermissionWrapper dialPermissionWrapper = new DialPermissionWrapper();
			if (user.UMMailboxPolicy == null)
			{
				throw new ADUMUserInvalidUMMailboxPolicyException(user.PrimarySmtpAddress.ToString());
			}
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(user);
			UMMailboxPolicy policyFromRecipient = iadsystemConfigurationLookup.GetPolicyFromRecipient(user);
			if (policyFromRecipient == null)
			{
				throw new ADUMUserInvalidUMMailboxPolicyException(user.PrimarySmtpAddress.ToString());
			}
			dialPermissionWrapper.Category = DialPermissionType.MailboxPolicy;
			dialPermissionWrapper.Identity = user.DisplayName;
			dialPermissionWrapper.AllowedInCountryGroups = policyFromRecipient.AllowedInCountryOrRegionGroups;
			dialPermissionWrapper.AllowedInternationalGroups = policyFromRecipient.AllowedInternationalGroups;
			dialPermissionWrapper.DialPlanSubscribersAllowed = policyFromRecipient.AllowDialPlanSubscribers;
			dialPermissionWrapper.ExtensionLengthNumbersAllowed = policyFromRecipient.AllowExtensions;
			if (user.AddressBookPolicy != null)
			{
				dialPermissionWrapper.SearchRoot = user.GlobalAddressListFromAddressBookPolicy;
			}
			else
			{
				dialPermissionWrapper.SearchRoot = null;
			}
			dialPermissionWrapper.OrganizationId = user.OrganizationId;
			dialPermissionWrapper.contactScope = ((dialPermissionWrapper.SearchRoot != null) ? DialScopeEnum.AddressList : DialScopeEnum.GlobalAddressList);
			dialPermissionWrapper.CallSomeoneEnabled = true;
			dialPermissionWrapper.SendVoiceMessageEnabled = true;
			return dialPermissionWrapper;
		}

		// Token: 0x040002A7 RID: 679
		private MultiValuedProperty<string> allowedInCountryGroups;

		// Token: 0x040002A8 RID: 680
		private MultiValuedProperty<string> allowedInternationalGroups;

		// Token: 0x040002A9 RID: 681
		private bool dialPlanSubscribersAllowed;

		// Token: 0x040002AA RID: 682
		private bool extensionLengthNumbersAllowed;

		// Token: 0x040002AB RID: 683
		private DialScopeEnum contactScope;

		// Token: 0x040002AC RID: 684
		private bool callSomeoneEnabled;

		// Token: 0x040002AD RID: 685
		private bool sendVoiceMessageEnabled;

		// Token: 0x040002AE RID: 686
		private ADObjectId searchRoot;

		// Token: 0x040002AF RID: 687
		private OrganizationId organizationId;

		// Token: 0x040002B0 RID: 688
		private DialPermissionType category;

		// Token: 0x040002B1 RID: 689
		private string identity;
	}
}

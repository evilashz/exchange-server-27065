using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000DE RID: 222
	internal sealed class MobileDevicePolicyDataFactory
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0001B43C File Offset: 0x0001963C
		internal static MobileDevicePolicySettingsType GetPolicySettings(ExchangePrincipal principal)
		{
			MobileDevicePolicyData policyData = MobileDevicePolicyDataFactory.GetPolicyData(principal);
			return MobileDevicePolicyDataFactory.GetPolicySettings(policyData);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B458 File Offset: 0x00019658
		internal static MobileDevicePolicySettingsType GetPolicySettings(MobileDevicePolicyData mobilePolicyData)
		{
			MobileDevicePolicySettingsType mobileDevicePolicySettingsType = new MobileDevicePolicySettingsType();
			if (mobilePolicyData != null)
			{
				mobileDevicePolicySettingsType.AlphanumericDevicePasswordRequired = mobilePolicyData.AlphanumericDevicePasswordRequired;
				mobileDevicePolicySettingsType.DeviceEncryptionRequired = mobilePolicyData.DeviceEncryptionRequired;
				mobileDevicePolicySettingsType.DevicePasswordRequired = mobilePolicyData.DevicePasswordRequired;
				mobileDevicePolicySettingsType.MaxDevicePasswordExpirationString = mobilePolicyData.MaxDevicePasswordExpirationString;
				mobileDevicePolicySettingsType.MaxDevicePasswordFailedAttemptsString = mobilePolicyData.MaxDevicePasswordFailedAttemptsString;
				mobileDevicePolicySettingsType.MaxInactivityTimeDeviceLockString = mobilePolicyData.MaxInactivityTimeDeviceLockString;
				mobileDevicePolicySettingsType.MinDevicePasswordComplexCharacters = mobilePolicyData.MinDevicePasswordComplexCharacters;
				mobileDevicePolicySettingsType.MinDevicePasswordHistory = mobilePolicyData.MinDevicePasswordHistory;
				mobileDevicePolicySettingsType.MinDevicePasswordLength = mobilePolicyData.MinDevicePasswordLength;
				mobileDevicePolicySettingsType.PolicyIdentifier = mobilePolicyData.PolicyIdentifier;
				mobileDevicePolicySettingsType.SimpleDevicePasswordAllowed = mobilePolicyData.SimpleDevicePasswordAllowed;
				mobileDevicePolicySettingsType.AllowApplePushNotifications = mobilePolicyData.AllowApplePushNotifications;
				mobileDevicePolicySettingsType.AllowMicrosoftPushNotifications = mobilePolicyData.AllowMicrosoftPushNotifications;
				mobileDevicePolicySettingsType.AllowGooglePushNotifications = mobilePolicyData.AllowGooglePushNotifications;
			}
			return mobileDevicePolicySettingsType;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B51C File Offset: 0x0001971C
		internal static MobileDevicePolicyData GetPolicyData(ExchangePrincipal principal)
		{
			ADObjectId adobjectId = null;
			return MobileDevicePolicyDataFactory.GetPolicyData(principal, out adobjectId);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001B534 File Offset: 0x00019734
		internal static MobileDevicePolicyData GetPolicyData(ExchangePrincipal principal, out ADObjectId policyId)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			policyId = principal.MailboxInfo.Configuration.MobileDeviceMailboxPolicy;
			MobileDevicePolicyData mobileDevicePolicyData = null;
			if (policyId != null)
			{
				mobileDevicePolicyData = MobileDevicePolicyCache.Instance.Get(new OrgIdADObjectWrapper(policyId, principal.MailboxInfo.OrganizationId));
			}
			if (mobileDevicePolicyData == null)
			{
				ExTraceGlobals.MobileDevicePolicyTracer.TraceDebug<ADObjectId, ADObjectId>(0L, "No policy returned for user '{0}' with policy '{1}'. Using org default policy.", principal.ObjectId, policyId);
				ADObjectId adobjectId = MobileDevicePolicyIdCacheByOrganization.Instance.Get(principal.MailboxInfo.OrganizationId);
				if (adobjectId != null)
				{
					policyId = adobjectId;
					mobileDevicePolicyData = MobileDevicePolicyCache.Instance.Get(new OrgIdADObjectWrapper(adobjectId, principal.MailboxInfo.OrganizationId));
				}
				if (mobileDevicePolicyData == null)
				{
					ExTraceGlobals.MobileDevicePolicyTracer.TraceDebug<ADObjectId, OrganizationId>(0L, "No default policy returned for user '{0}' with organization '{1}'. Using NO policy.", principal.ObjectId, principal.MailboxInfo.OrganizationId);
				}
			}
			return mobileDevicePolicyData;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001B600 File Offset: 0x00019800
		internal static MobileDevicePolicyData GetMobileDevicePolicyDataFromMobileMailboxPolicy(MobileMailboxPolicy mobileMailboxPolicy)
		{
			MobileDevicePolicyData result = null;
			if (mobileMailboxPolicy != null)
			{
				result = new MobileDevicePolicyData
				{
					AlphanumericDevicePasswordRequired = mobileMailboxPolicy.AlphanumericPasswordRequired,
					DeviceEncryptionRequired = mobileMailboxPolicy.RequireDeviceEncryption,
					DevicePasswordRequired = mobileMailboxPolicy.PasswordEnabled,
					MaxDevicePasswordExpiration = mobileMailboxPolicy.PasswordExpiration,
					MaxDevicePasswordFailedAttempts = mobileMailboxPolicy.MaxPasswordFailedAttempts,
					MaxInactivityTimeDeviceLock = mobileMailboxPolicy.MaxInactivityTimeLock,
					MinDevicePasswordComplexCharacters = mobileMailboxPolicy.MinPasswordComplexCharacters,
					MinDevicePasswordHistory = mobileMailboxPolicy.PasswordHistory,
					MinDevicePasswordLength = mobileMailboxPolicy.MinPasswordLength,
					SimpleDevicePasswordAllowed = mobileMailboxPolicy.AllowSimplePassword,
					AllowApplePushNotifications = mobileMailboxPolicy.AllowApplePushNotifications,
					AllowMicrosoftPushNotifications = mobileMailboxPolicy.AllowMicrosoftPushNotifications,
					AllowGooglePushNotifications = mobileMailboxPolicy.AllowGooglePushNotifications
				};
			}
			return result;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001B6BC File Offset: 0x000198BC
		internal static MobileDevicePolicyData GetMobileDevicePolicyDataFromAD(IConfigurationSession session, ADObjectId mobileMailboxPolicyId)
		{
			ExTraceGlobals.MobileDevicePolicyTracer.TraceDebug<ADObjectId>(0L, "Looking up mobile device policy object in AD: '{0}'", mobileMailboxPolicyId);
			MobileMailboxPolicy mobileMailboxPolicy = session.Read<MobileMailboxPolicy>(mobileMailboxPolicyId);
			return MobileDevicePolicyDataFactory.GetMobileDevicePolicyDataFromMobileMailboxPolicy(mobileMailboxPolicy);
		}
	}
}

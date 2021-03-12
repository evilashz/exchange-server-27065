﻿using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D30 RID: 3376
	internal class MigrationHelper
	{
		// Token: 0x0600815E RID: 33118 RVA: 0x00210EE0 File Offset: 0x0020F0E0
		public static void EnableTargetUserForUM(IRecipientSession tenantLocalRecipientSession, IConfigurationSession tenantLocalConfigSession, bool isDatacenter, ADUser sourceUser, ADUser targetUser)
		{
			UMMailboxPolicy policy;
			UMDialPlan dialPlan;
			MultiValuedProperty<string> extensions;
			string sipResourceIdentifier;
			MigrationHelper.GetUMSettingsFromSourceUser(tenantLocalRecipientSession, tenantLocalConfigSession, isDatacenter, sourceUser, targetUser, out policy, out dialPlan, out extensions, out sipResourceIdentifier);
			targetUser.UMEnabledFlags |= UMEnabledFlags.UMEnabled;
			Utils.UMPopulate(targetUser, sipResourceIdentifier, extensions, policy, dialPlan);
		}

		// Token: 0x0600815F RID: 33119 RVA: 0x00210F1C File Offset: 0x0020F11C
		public static void ValidateTargetUserCanBeEnabledForUM(IRecipientSession tenantLocalRecipientSession, IConfigurationSession tenantLocalConfigSession, bool isDatacenter, ADUser sourceUser, ADUser targetUser)
		{
			if (!sourceUser.UMEnabled)
			{
				return;
			}
			UMMailboxPolicy ummailboxPolicy;
			UMDialPlan umdialPlan;
			MultiValuedProperty<string> multiValuedProperty;
			string text;
			MigrationHelper.GetUMSettingsFromSourceUser(tenantLocalRecipientSession, tenantLocalConfigSession, isDatacenter, sourceUser, targetUser, out ummailboxPolicy, out umdialPlan, out multiValuedProperty, out text);
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x00210F48 File Offset: 0x0020F148
		private static void GetUMSettingsFromSourceUser(IRecipientSession tenantLocalRecipientSession, IConfigurationSession tenantLocalConfigSession, bool isDatacenter, ADUser sourceUser, ADUser targetUser, out UMMailboxPolicy targetPolicy, out UMDialPlan targetDialPlan, out MultiValuedProperty<string> targetUserExtensions, out string targetUserSipResourceIdentifier)
		{
			if (!Utils.UnifiedMessagingAvailable(targetUser))
			{
				throw new UMNotAvailableForUserInTargetForestException();
			}
			targetPolicy = MigrationHelper.GetTargetPolicy(tenantLocalConfigSession, sourceUser);
			targetDialPlan = tenantLocalConfigSession.Read<UMDialPlan>(targetPolicy.UMDialPlan);
			targetUserExtensions = MigrationHelper.GetTargetUserExtensions(sourceUser, targetDialPlan, out targetUserSipResourceIdentifier);
			LocalizedException ex;
			TelephoneNumberProcessStatus telephoneNumberProcessStatus;
			Utils.ValidateExtensionsAndSipResourceIdentifier(tenantLocalRecipientSession, tenantLocalConfigSession, isDatacenter, targetUser, targetDialPlan, targetUserExtensions.ToArray(), null, targetUserSipResourceIdentifier, out ex, out telephoneNumberProcessStatus);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06008161 RID: 33121 RVA: 0x00210FB0 File Offset: 0x0020F1B0
		private static UMMailboxPolicy GetTargetPolicy(IConfigurationSession tenantLocalConfigSession, ADUser sourceUser)
		{
			UMMailboxPolicy ummailboxPolicy = null;
			UMMailboxPolicy[] array = tenantLocalConfigSession.Find<UMMailboxPolicy>(null, QueryScope.SubTree, null, null, 0);
			foreach (UMMailboxPolicy ummailboxPolicy2 in array)
			{
				if (ummailboxPolicy2.SourceForestPolicyNames.Contains(sourceUser.UMMailboxPolicy.Name))
				{
					ummailboxPolicy = ummailboxPolicy2;
					break;
				}
			}
			if (ummailboxPolicy == null)
			{
				throw new NoMatchingUMMailboxPolicyInTargetForestException();
			}
			return ummailboxPolicy;
		}

		// Token: 0x06008162 RID: 33122 RVA: 0x0021100C File Offset: 0x0020F20C
		private static MultiValuedProperty<string> GetTargetUserExtensions(ADUser sourceUser, UMDialPlan targetDialPlan, out string targetUserSipResourceIdentifier)
		{
			EumAddress? eumAddress = null;
			foreach (ProxyAddress proxyAddress in sourceUser.EmailAddresses)
			{
				if (proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == ProxyAddressPrefix.UM && EumAddress.IsValidEumAddress(proxyAddress.AddressString))
				{
					eumAddress = new EumAddress?(EumAddress.Parse(proxyAddress.AddressString));
					break;
				}
			}
			if (eumAddress == null)
			{
				throw new CouldNotGenerateExtensionException();
			}
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			UMUriType umuriType = Utils.DetermineNumberType(eumAddress.Value.Extension);
			if (umuriType != targetDialPlan.URIType)
			{
				throw new SourceAndTargetDialPlanURITypeMismatchException(umuriType.ToString(), targetDialPlan.URIType.ToString());
			}
			if (umuriType == UMUriType.TelExtn)
			{
				multiValuedProperty.Add(eumAddress.Value.Extension);
				targetUserSipResourceIdentifier = null;
			}
			else
			{
				targetUserSipResourceIdentifier = eumAddress.Value.Extension;
			}
			foreach (ProxyAddress proxyAddress2 in sourceUser.EmailAddresses)
			{
				if (!proxyAddress2.IsPrimaryAddress && proxyAddress2.Prefix == ProxyAddressPrefix.UM && EumAddress.IsValidEumAddress(proxyAddress2.AddressString))
				{
					EumAddress eumAddress2 = EumAddress.Parse(proxyAddress2.AddressString);
					if (eumAddress2.PhoneContext == eumAddress.Value.PhoneContext)
					{
						multiValuedProperty.Add(eumAddress2.Extension);
					}
				}
			}
			return multiValuedProperty;
		}
	}
}

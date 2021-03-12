using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000798 RID: 1944
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AclHelper
	{
		// Token: 0x06004923 RID: 18723 RVA: 0x00131F4C File Offset: 0x0013014C
		public static bool TryGetUserFromEntryId(byte[] memberEntryId, StoreSession session, IRecipientSession recipientSession, ExternalUserCollection externalUsers, out string legacyDN, out SecurityIdentifier securityIdentifier, out List<SecurityIdentifier> sidHistory, out bool isGroup, out string displayName)
		{
			return AclHelper.TryGetUserFromEntryId(memberEntryId, session, recipientSession, new LazilyInitialized<ExternalUserCollection>(() => externalUsers), out legacyDN, out securityIdentifier, out sidHistory, out isGroup, out displayName);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x00131F89 File Offset: 0x00130189
		public static bool TryGetUserFromEntryId(byte[] memberEntryId, StoreSession session, IRecipientSession recipientSession, LazilyInitialized<ExternalUserCollection> externalUsers, out string legacyDN, out SecurityIdentifier securityIdentifier, out List<SecurityIdentifier> sidHistory, out bool isGroup, out string displayName)
		{
			legacyDN = AclHelper.LegacyDnFromEntryId(memberEntryId);
			if (AddressBookEntryId.IsLocalDirctoryAddressBookEntryId(memberEntryId))
			{
				sidHistory = null;
				return AclHelper.ResolveLocalDirectoryUserFromAddressBookEntryId(memberEntryId, externalUsers, out securityIdentifier, out isGroup, out displayName);
			}
			return AclHelper.ResolveRecipientParametersFromLegacyDN(legacyDN, session, recipientSession, out securityIdentifier, out sidHistory, out isGroup, out displayName);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x00131FC0 File Offset: 0x001301C0
		internal static string LegacyDnFromEntryId(byte[] entryId)
		{
			string result;
			Eidt eidt;
			if (entryId.Length == 0)
			{
				result = string.Empty;
			}
			else if (AddressBookEntryId.IsLocalDirctoryAddressBookEntryId(entryId))
			{
				result = AddressBookEntryId.MakeLegacyDnFromLocalDirctoryAddressBookEntryId(entryId);
				eidt = Eidt.User;
			}
			else if (!AddressBookEntryId.IsAddressBookEntryId(entryId, out eidt, out result))
			{
				throw new InvalidParamException(new LocalizedString("Invalid ABEID"));
			}
			return result;
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x0013200C File Offset: 0x0013020C
		internal static ExternalUser TryGetExternalUser(SecurityIdentifier sid, ExternalUserCollection externalUsers)
		{
			ExternalUser result = null;
			if (ExternalUser.IsExternalUserSid(sid) && externalUsers != null)
			{
				result = externalUsers.FindExternalUser(sid);
			}
			return result;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x00132030 File Offset: 0x00130230
		internal static bool IsNTUserLegacyDN(string legacyDN, CultureInfo cultureInfo, SecurityIdentifier securityIdentifier, out string displayName)
		{
			if (legacyDN != null && legacyDN.StartsWith("NT:", StringComparison.OrdinalIgnoreCase))
			{
				displayName = ClientStrings.NTUser.ToString(cultureInfo) + securityIdentifier.Value;
				return true;
			}
			displayName = string.Empty;
			return false;
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00132073 File Offset: 0x00130273
		internal static string CreateNTUserStrignRepresentation(SecurityIdentifier securityIdentifier)
		{
			return "NT:" + securityIdentifier.Value;
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x00132085 File Offset: 0x00130285
		internal static string CreateLocalUserStrignRepresentation(SecurityIdentifier securityIdentifier)
		{
			return "Local:" + securityIdentifier.Value;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x00132098 File Offset: 0x00130298
		internal static bool IsGroupRecipientType(RecipientType recipientType)
		{
			return recipientType == RecipientType.Group || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.DynamicDistributionGroup;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x001320C0 File Offset: 0x001302C0
		private static bool ResolveRecipientParametersFromLegacyDN(string legacyDN, StoreSession session, IRecipientSession recipientSession, out SecurityIdentifier securityIdentifier, out List<SecurityIdentifier> sidHistory, out bool isGroup, out string displayName)
		{
			securityIdentifier = null;
			sidHistory = null;
			isGroup = false;
			displayName = string.Empty;
			if (legacyDN == string.Empty)
			{
				securityIdentifier = AclHelper.everyoneSecurityIdentifier;
				return true;
			}
			if (string.Compare(legacyDN, "Anonymous", StringComparison.OrdinalIgnoreCase) == 0)
			{
				securityIdentifier = AclHelper.anonymousSecurityIdentifier;
				displayName = "Anonymous";
				return true;
			}
			MiniRecipient[] array = recipientSession.FindMiniRecipient(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, MiniRecipientSchema.LegacyExchangeDN, legacyDN), null, 2, Array<PropertyDefinition>.Empty);
			if (array == null || array.Length == 0)
			{
				return false;
			}
			if (array.Length > 1)
			{
				throw new NonUniqueLegacyExchangeDNException(ServerStrings.ErrorNonUniqueLegacyDN(legacyDN));
			}
			SecurityIdentifier masterAccountSid = array[0].MasterAccountSid;
			if (masterAccountSid != null && !masterAccountSid.IsWellKnown(WellKnownSidType.SelfSid))
			{
				securityIdentifier = masterAccountSid;
			}
			else
			{
				securityIdentifier = array[0].Sid;
				MultiValuedProperty<SecurityIdentifier> sidHistory2 = array[0].SidHistory;
				if (sidHistory2 != null && sidHistory2.Count != 0)
				{
					sidHistory = new List<SecurityIdentifier>(sidHistory2);
				}
			}
			if (securityIdentifier == null)
			{
				throw new CorruptDataException(ServerStrings.UserSidNotFound(legacyDN));
			}
			isGroup = AclHelper.IsGroupRecipientType(array[0].RecipientType);
			if (!AclHelper.IsNTUserLegacyDN(legacyDN, session.InternalPreferedCulture, securityIdentifier, out displayName))
			{
				displayName = array[0].DisplayName;
			}
			return true;
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x001321DC File Offset: 0x001303DC
		private static bool ResolveLocalDirectoryUserFromAddressBookEntryId(byte[] entryId, LazilyInitialized<ExternalUserCollection> externalUsers, out SecurityIdentifier securityIdentifier, out bool isGroup, out string displayName)
		{
			securityIdentifier = null;
			isGroup = false;
			displayName = string.Empty;
			securityIdentifier = AddressBookEntryId.MakeSidFromLocalDirctoryAddressBookEntryId(entryId);
			ExternalUser externalUser = AclHelper.TryGetExternalUser(securityIdentifier, externalUsers);
			if (externalUser == null)
			{
				throw new ExternalUserNotFoundException(securityIdentifier);
			}
			displayName = externalUser.Name;
			return true;
		}

		// Token: 0x04002777 RID: 10103
		internal const long EveryoneRowId = 0L;

		// Token: 0x04002778 RID: 10104
		internal const long AnonymousRowId = -1L;

		// Token: 0x04002779 RID: 10105
		internal const string AnonymousString = "Anonymous";

		// Token: 0x0400277A RID: 10106
		private const string NTUserLegacyDNPrefix = "NT:";

		// Token: 0x0400277B RID: 10107
		private const string LocalUserLegacyDNPrefix = "Local:";

		// Token: 0x0400277C RID: 10108
		private static readonly SecurityIdentifier everyoneSecurityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

		// Token: 0x0400277D RID: 10109
		private static readonly SecurityIdentifier anonymousSecurityIdentifier = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);
	}
}

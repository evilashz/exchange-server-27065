using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core.Directory
{
	// Token: 0x020002DB RID: 731
	public sealed class DirectoryAssistance
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x000A1EEE File Offset: 0x000A00EE
		public static int MaxAddressBooks
		{
			get
			{
				return 2500;
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000A1EF8 File Offset: 0x000A00F8
		public static string ToHtmlString(ADObjectId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			return Convert.ToBase64String(id.ObjectGuid.ToByteArray());
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000A1F28 File Offset: 0x000A0128
		public static ADObjectId ParseADObjectId(string htmlString)
		{
			ADObjectId result;
			try
			{
				Guid guid = new Guid(Convert.FromBase64String(htmlString));
				result = new ADObjectId(guid);
			}
			catch (FormatException)
			{
				return null;
			}
			catch (ArgumentException)
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000A1F74 File Offset: 0x000A0174
		public static AddressBookBase FindAddressBook(string base64Guid, UserContext userContext)
		{
			if (base64Guid == null)
			{
				throw new ArgumentNullException("base64Guid");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			Guid guid;
			try
			{
				guid = new Guid(Convert.FromBase64String(base64Guid));
			}
			catch (FormatException)
			{
				return null;
			}
			catch (ArgumentException)
			{
				return null;
			}
			return DirectoryAssistance.FindAddressBook(guid, userContext);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000A1FDC File Offset: 0x000A01DC
		public static AddressBookBase FindAddressBook(Guid guid, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (userContext.GlobalAddressListInfo.Id.ObjectGuid.Equals(guid))
			{
				return userContext.GlobalAddressListInfo.ToAddressBookBase();
			}
			if (userContext.AllRoomsAddressBookInfo != null && userContext.AllRoomsAddressBookInfo.Id.ObjectGuid.Equals(guid))
			{
				return userContext.AllRoomsAddressBookInfo.ToAddressBookBase();
			}
			if (userContext.LastUsedAddressBookInfo != null && userContext.LastUsedAddressBookInfo.Id.ObjectGuid.Equals(guid))
			{
				return userContext.LastUsedAddressBookInfo.ToAddressBookBase();
			}
			if (userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.QueryBaseDNSubTree)
			{
				throw new OwaInvalidRequestException("We cannot have sub address lists for QueryBaseDN container node.");
			}
			IConfigurationSession configurationSession = Utilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, userContext);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid);
			SortBy sortBy = new SortBy(ADObjectSchema.Name, SortOrder.Descending);
			AddressBookBase[] array = configurationSession.Find<AddressBookBase>(userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, QueryScope.SubTree, filter, sortBy, 1);
			if (array.Length == 0 || array[0] == null)
			{
				return null;
			}
			userContext.LastUsedAddressBookInfo = new AddressListInfo(array[0]);
			return array[0];
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000A20F8 File Offset: 0x000A02F8
		internal static AddressListInfo GetAllRoomsAddressBookInfo(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN != null)
			{
				return AddressListInfo.CreateEmpty(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			}
			AddressBookBase allRoomsAddressList = userContext.AllRoomsAddressList;
			if (allRoomsAddressList == null)
			{
				return AddressListInfo.CreateEmpty(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			}
			return new AddressListInfo(allRoomsAddressList);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000A2164 File Offset: 0x000A0364
		public static AddressBookBase[] GetAllAddressBooks(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.DefaultGlobalAddressList)
			{
				IEnumerable<AddressBookBase> allAddressLists = userContext.AllAddressLists;
				List<AddressBookBase> list = new List<AddressBookBase>();
				foreach (AddressBookBase item in allAddressLists)
				{
					list.Add(item);
					if (list.Count >= DirectoryAssistance.MaxAddressBooks)
					{
						break;
					}
				}
				list.Sort(new DirectoryAssistance.DisplayNameComparer());
				return list.ToArray();
			}
			if (userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.QueryBaseDNAddressList)
			{
				return new AddressBookBase[0];
			}
			return new AddressBookBase[0];
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000A2210 File Offset: 0x000A0410
		internal static GlobalAddressListInfo GetGlobalAddressListInfo(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ADObjectId queryBaseDN = userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN;
			GlobalAddressListInfo globalAddressListInfo;
			if (queryBaseDN == null)
			{
				if (userContext.GlobalAddressList != null)
				{
					globalAddressListInfo = new GlobalAddressListInfo(userContext.GlobalAddressList, GlobalAddressListInfo.GalOrigin.DefaultGlobalAddressList);
				}
				else
				{
					globalAddressListInfo = new GlobalAddressListInfo(new AddressBookBase(), GlobalAddressListInfo.GalOrigin.EmptyGlobalAddressList);
					globalAddressListInfo.DisplayName = LocalizedStrings.GetNonEncoded(1164140307);
					globalAddressListInfo.Id = null;
				}
			}
			else
			{
				IConfigurationSession configurationSession = Utilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, userContext);
				AddressBookBase addressBookBase = configurationSession.Read<AddressBookBase>(new ADObjectId(null, queryBaseDN.ObjectGuid));
				if (addressBookBase != null)
				{
					globalAddressListInfo = new GlobalAddressListInfo(addressBookBase, GlobalAddressListInfo.GalOrigin.QueryBaseDNAddressList);
				}
				else
				{
					globalAddressListInfo = new GlobalAddressListInfo(new AddressBookBase(), GlobalAddressListInfo.GalOrigin.QueryBaseDNSubTree);
					globalAddressListInfo.DisplayName = LocalizedStrings.GetNonEncoded(1164140307);
					globalAddressListInfo.Id = queryBaseDN;
				}
			}
			return globalAddressListInfo;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000A22CA File Offset: 0x000A04CA
		public static bool IsADRecipientRoom(ADRecipient adRecipient)
		{
			if (adRecipient == null)
			{
				throw new ArgumentException("adRecipient");
			}
			return DirectoryAssistance.IsADRecipientRoom(adRecipient.RecipientDisplayType);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000A22E8 File Offset: 0x000A04E8
		public static bool IsADRecipientRoom(RecipientDisplayType? recipientDisplayType)
		{
			return recipientDisplayType == RecipientDisplayType.ConferenceRoomMailbox || recipientDisplayType == RecipientDisplayType.SyncedConferenceRoomMailbox;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000A2328 File Offset: 0x000A0528
		public static bool IsADRecipientDL(RecipientDisplayType? displayType)
		{
			return displayType != null && (displayType == RecipientDisplayType.DistributionGroup || displayType == RecipientDisplayType.DynamicDistributionGroup || displayType == RecipientDisplayType.SecurityDistributionGroup || displayType == RecipientDisplayType.PrivateDistributionList || displayType == RecipientDisplayType.SyncedDynamicDistributionGroup);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000A23C0 File Offset: 0x000A05C0
		public static string GetFirstResource(MultiValuedProperty<string> resourceMetaData)
		{
			if (resourceMetaData == null)
			{
				throw new ArgumentNullException("resourceMetaData");
			}
			foreach (string text in resourceMetaData)
			{
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000A2428 File Offset: 0x000A0628
		public static bool IsRoomsAddressListAvailable(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.DefaultGlobalAddressList;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000A2446 File Offset: 0x000A0646
		public static bool IsVirtualAddressList(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.QueryBaseDNSubTree;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000A2464 File Offset: 0x000A0664
		public static bool IsEmptyAddressList(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.EmptyGlobalAddressList;
		}

		// Token: 0x020002DC RID: 732
		private class DisplayNameComparer : IComparer<AddressBookBase>
		{
			// Token: 0x06001C40 RID: 7232 RVA: 0x000A248A File Offset: 0x000A068A
			public int Compare(AddressBookBase list1, AddressBookBase list2)
			{
				if (list1 == null)
				{
					throw new ArgumentNullException("list1");
				}
				if (list2 == null)
				{
					throw new ArgumentNullException("list2");
				}
				return string.Compare(list1.DisplayName, list2.DisplayName, true, Thread.CurrentThread.CurrentUICulture);
			}
		}
	}
}

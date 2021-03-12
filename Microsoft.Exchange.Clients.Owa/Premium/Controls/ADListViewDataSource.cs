using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000314 RID: 788
	internal sealed class ADListViewDataSource : ExchangeListViewDataSource, IListViewDataSource
	{
		// Token: 0x06001DE6 RID: 7654 RVA: 0x000AD5BC File Offset: 0x000AB7BC
		private static Dictionary<RecipientType, string> LoadItemClasses()
		{
			Dictionary<RecipientType, string> dictionary = new Dictionary<RecipientType, string>();
			dictionary[RecipientType.Invalid] = "AD.RecipientType.Invalid";
			dictionary[RecipientType.User] = "AD.RecipientType.User";
			dictionary[RecipientType.UserMailbox] = "AD.RecipientType.MailboxUser";
			dictionary[RecipientType.MailUser] = "AD.RecipientType.MailEnabledUser";
			dictionary[RecipientType.Contact] = "AD.RecipientType.Contact";
			dictionary[RecipientType.MailContact] = "AD.RecipientType.MailEnabledContact";
			dictionary[RecipientType.Group] = "AD.RecipientType.Group";
			dictionary[RecipientType.MailUniversalDistributionGroup] = "AD.RecipientType.MailEnabledUniversalDistributionGroup";
			dictionary[RecipientType.MailUniversalSecurityGroup] = "AD.RecipientType.MailEnabledUniversalSecurityGroup";
			dictionary[RecipientType.MailNonUniversalGroup] = "AD.RecipientType.MailEnabledNonUniversalGroup";
			dictionary[RecipientType.DynamicDistributionGroup] = "AD.RecipientType.DynamicDL";
			dictionary[RecipientType.PublicFolder] = "AD.RecipientType.PublicFolder";
			dictionary[RecipientType.PublicDatabase] = "AD.RecipientType.PublicDatabase";
			dictionary[RecipientType.SystemAttendantMailbox] = "AD.RecipientType.SystemAttendantMailbox";
			return dictionary;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000AD67D File Offset: 0x000AB87D
		public static ADListViewDataSource CreateForBrowse(Hashtable properties, AddressBookBase addressBookBase, UserContext userContext)
		{
			return ADListViewDataSource.CreateForBrowse(properties, addressBookBase, null, Culture.GetUserCulture().LCID, null, userContext);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000AD693 File Offset: 0x000AB893
		public static ADListViewDataSource CreateForBrowse(Hashtable properties, AddressBookBase addressBookBase, string cookie, int lcid, string preferredDC, UserContext userContext)
		{
			return new ADListViewDataSource(properties, addressBookBase, cookie, lcid, preferredDC, userContext);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000AD6A2 File Offset: 0x000AB8A2
		public static ADListViewDataSource CreateForSearch(Hashtable properties, AddressBookBase addressBookBase, string searchString, UserContext userContext)
		{
			return ADListViewDataSource.CreateForSearch(properties, addressBookBase, searchString, null, 0, Culture.GetUserCulture().LCID, null, userContext);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000AD6BA File Offset: 0x000AB8BA
		public static ADListViewDataSource CreateForSearch(Hashtable properties, AddressBookBase addressBookBase, string searchString, string cookie, int cookieIndex, int lcid, string preferredDC, UserContext userContext)
		{
			return new ADListViewDataSource(properties, addressBookBase, searchString, cookie, cookieIndex, lcid, preferredDC, userContext);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x000AD6D0 File Offset: 0x000AB8D0
		private ADListViewDataSource(Hashtable properties, AddressBookBase addressBookBase, string cookie, int lcid, string preferredDC, UserContext userContext) : this(properties, addressBookBase, null, cookie, 0, lcid, preferredDC, userContext)
		{
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000AD6F0 File Offset: 0x000AB8F0
		private ADListViewDataSource(Hashtable properties, AddressBookBase addressBookBase, string searchString, string cookie, int cookieIndex, int lcid, string preferredDC, UserContext userContext) : base(properties)
		{
			if (!properties.ContainsKey(ADObjectSchema.ObjectCategory) || !properties.ContainsKey(ADObjectSchema.Guid) || !properties.ContainsKey(ADRecipientSchema.RecipientType) || !properties.ContainsKey(ADRecipientSchema.RecipientDisplayType))
			{
				throw new ArgumentException("The objectCategory, objectGuid, recipientType attributes need to be included in the'properties' parameter of the ADListViewDataSource constructor");
			}
			this.addressBookBase = addressBookBase;
			this.searchString = searchString;
			this.cookie = cookie;
			this.cookieIndex = cookieIndex;
			this.lcid = lcid;
			this.preferredDC = preferredDC;
			this.userContext = userContext;
			if (!string.IsNullOrEmpty(searchString))
			{
				this.search = true;
			}
			try
			{
				userContext.GetCachedADCount(this.ContainerId, this.search ? searchString : string.Empty);
			}
			catch (Exception)
			{
				this.Load(2147483597, 50, true, false);
				userContext.SetCachedADCount(this.ContainerId, this.search ? searchString : string.Empty, (base.EndRange >= 0) ? (base.EndRange + 1) : 0);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x000AD7FC File Offset: 0x000AB9FC
		public string Cookie
		{
			get
			{
				if (this.cookie == null)
				{
					return string.Empty;
				}
				return this.cookie;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x000AD812 File Offset: 0x000ABA12
		public int Lcid
		{
			get
			{
				if (this.cookie == null)
				{
					return Culture.GetUserCulture().LCID;
				}
				return this.lcid;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x000AD82D File Offset: 0x000ABA2D
		public string PreferredDC
		{
			get
			{
				if (this.preferredDC == null)
				{
					return string.Empty;
				}
				return this.preferredDC;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x000AD843 File Offset: 0x000ABA43
		public override int TotalCount
		{
			get
			{
				return this.userContext.GetCachedADCount(this.ContainerId, this.search ? this.searchString : string.Empty);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x000AD86B File Offset: 0x000ABA6B
		public int UnreadCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000AD86E File Offset: 0x000ABA6E
		public string ContainerId
		{
			get
			{
				return this.addressBookBase.Base64Guid;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000AD87B File Offset: 0x000ABA7B
		public bool UserHasRightToLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000AD87E File Offset: 0x000ABA7E
		public string GetItemId()
		{
			return DirectoryAssistance.ToHtmlString(new ADObjectId(null, this.GetItemProperty<Guid>(ADObjectSchema.Guid, Guid.Empty)));
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000AD89C File Offset: 0x000ABA9C
		public string GetItemClass()
		{
			RecipientDisplayType? itemProperty = this.GetItemProperty<RecipientDisplayType?>(ADRecipientSchema.RecipientDisplayType, null);
			if (itemProperty == RecipientDisplayType.ConferenceRoomMailbox || itemProperty == RecipientDisplayType.SyncedConferenceRoomMailbox)
			{
				return "AD.ResourceType.Room";
			}
			RecipientType itemProperty2 = this.GetItemProperty<RecipientType>(ADRecipientSchema.RecipientType, RecipientType.Invalid);
			string result;
			if (ADListViewDataSource.itemClasses.TryGetValue(itemProperty2, out result))
			{
				return result;
			}
			return ADListViewDataSource.itemClasses[RecipientType.Invalid];
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000AD923 File Offset: 0x000ABB23
		public void Load(ObjectId seekToItemId, SeekDirection seekDirection, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000AD92C File Offset: 0x000ABB2C
		public void Load(string seekValue, int itemCount)
		{
			if (this.search)
			{
				throw new ArgumentException("Can't seek and search at the same time");
			}
			PropertyDefinition[] requestedProperties = base.GetRequestedProperties();
			this.searchString = seekValue;
			this.LoadBrowsePage(0, itemCount, true, requestedProperties);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000AD964 File Offset: 0x000ABB64
		public void Load(int startRange, int itemCount)
		{
			this.Load(startRange, itemCount, true, true);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000AD970 File Offset: 0x000ABB70
		private void Load(int startRange, int itemCount, bool retry, bool fetchingPhase)
		{
			if (startRange < 0)
			{
				throw new ArgumentException("startRange must be >= 0");
			}
			if (itemCount <= 0)
			{
				throw new ArgumentException("itemCount must be > 0");
			}
			PropertyDefinition[] properties;
			if (fetchingPhase)
			{
				properties = base.GetRequestedProperties();
			}
			else
			{
				properties = new PropertyDefinition[]
				{
					ADObjectSchema.ObjectCategory
				};
			}
			try
			{
				if (this.search)
				{
					if (this.cookieIndex < 0 || this.cookieIndex >= startRange)
					{
						this.cookie = null;
						this.cookieIndex = 0;
					}
					this.LoadPagedSearch(startRange, itemCount, properties, true);
				}
				else
				{
					this.LoadBrowsePage(startRange + 1, itemCount, false, properties);
				}
			}
			catch (ADInvalidHandleCookieException)
			{
				if (retry)
				{
					this.cookie = string.Empty;
					this.Load(startRange, itemCount, false, fetchingPhase);
				}
			}
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000ADA28 File Offset: 0x000ABC28
		private void LoadPagedSearch(int startRange, int itemCount, PropertyDefinition[] properties, bool retry)
		{
			int num = startRange;
			if (!string.IsNullOrEmpty(this.cookie))
			{
				num = startRange - (this.cookieIndex + 1);
			}
			int itemsToSkip = num;
			object[][] array;
			int num2;
			if (DirectoryAssistance.IsEmptyAddressList(this.userContext))
			{
				array = new object[0][];
				num2 = 0;
			}
			else
			{
				array = AddressBookBase.PagedSearch(DirectoryAssistance.IsVirtualAddressList(this.userContext) ? this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN : null, DirectoryAssistance.IsVirtualAddressList(this.userContext) ? null : this.addressBookBase, this.userContext.ExchangePrincipal.MailboxInfo.OrganizationId, AddressBookBase.RecipientCategory.All, this.searchString, itemsToSkip, ref this.cookie, itemCount, out num2, ref this.lcid, ref this.preferredDC, properties);
			}
			if (array.Length > 0)
			{
				base.StartRange = startRange;
				base.EndRange = base.StartRange + (array.Length - 1);
				this.cookieIndex = base.EndRange;
				base.Items = array;
				return;
			}
			if (this.cookie != null && this.cookie.Length != 0 && retry)
			{
				this.cookieIndex = 0;
				this.LoadPagedSearch(0, itemCount, properties, false);
				return;
			}
			if (num2 != 0 && retry)
			{
				if (this.cookieIndex > 0)
				{
					num2 += this.cookieIndex + 1;
				}
				int num3 = num2 - 1;
				startRange = num3 - (itemCount - 1);
				if (startRange < 0)
				{
					startRange = 0;
				}
				this.cookie = null;
				this.cookieIndex = 0;
				this.lcid = Culture.GetUserCulture().LCID;
				this.LoadPagedSearch(startRange, itemCount, properties, false);
				return;
			}
			base.StartRange = int.MinValue;
			base.EndRange = int.MinValue;
			this.cookie = null;
			this.cookieIndex = 0;
			base.Items = array;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000ADBC0 File Offset: 0x000ABDC0
		private void LoadBrowsePage(int startRange, int itemCount, bool seekToCondition, PropertyDefinition[] properties)
		{
			int num;
			if (startRange > 1 && startRange < 2147483647)
			{
				num = startRange - 1;
				itemCount++;
			}
			else
			{
				num = startRange;
			}
			object[][] array;
			int num2;
			if (DirectoryAssistance.IsEmptyAddressList(this.userContext))
			{
				array = new object[0][];
				num2 = 0;
			}
			else if (seekToCondition)
			{
				array = this.addressBookBase.BrowseTo(ref this.cookie, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, ref this.lcid, ref this.preferredDC, this.searchString, itemCount, out num2, DirectoryAssistance.IsVirtualAddressList(this.userContext), properties);
			}
			else
			{
				array = this.addressBookBase.BrowseTo(ref this.cookie, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, ref this.lcid, ref this.preferredDC, num, itemCount, out num2, DirectoryAssistance.IsVirtualAddressList(this.userContext), properties);
				if (startRange > 1 && startRange < 2147483647)
				{
					itemCount--;
					if (array.Length > 1)
					{
						num2++;
						this.offsetForData = 1;
					}
					else if (array.Length == 1)
					{
						int num3 = num % itemCount;
						if (num3 == 0)
						{
							num = startRange - itemCount;
						}
						else
						{
							num = num - num3 + 1;
						}
						array = this.addressBookBase.BrowseTo(ref this.cookie, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, ref this.lcid, ref this.preferredDC, num, itemCount, out num2, DirectoryAssistance.IsVirtualAddressList(this.userContext), properties);
					}
				}
			}
			int num4 = (array.Length <= itemCount) ? (array.Length - this.offsetForData) : itemCount;
			if (num4 == 0 && !DirectoryAssistance.IsEmptyAddressList(this.userContext))
			{
				this.offsetForData = 0;
				array = this.addressBookBase.BrowseTo(ref this.cookie, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, ref this.lcid, ref this.preferredDC, 0, itemCount, out num2, DirectoryAssistance.IsVirtualAddressList(this.userContext), properties);
				startRange = num2 - (itemCount - 1);
				if (startRange < 1)
				{
					startRange = 1;
				}
				array = this.addressBookBase.BrowseTo(ref this.cookie, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, ref this.lcid, ref this.preferredDC, startRange, itemCount, out num2, DirectoryAssistance.IsVirtualAddressList(this.userContext), properties);
				num4 = ((array.Length < itemCount) ? array.Length : itemCount);
			}
			if (num4 > 0)
			{
				base.StartRange = Math.Max(0, num2 - 1);
				base.EndRange = base.StartRange + num4 - 1;
			}
			else
			{
				base.StartRange = int.MinValue;
				base.EndRange = int.MinValue;
			}
			base.Items = array;
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000ADE34 File Offset: 0x000AC034
		public override int CurrentItem
		{
			get
			{
				return base.CurrentItem - this.offsetForData;
			}
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000ADE43 File Offset: 0x000AC043
		public override bool MoveNext()
		{
			base.MoveNext();
			return base.CurrentItem < base.RangeCount + this.offsetForData;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000ADE64 File Offset: 0x000AC064
		public override void MoveToItem(int itemIndex)
		{
			if (itemIndex < 0 || base.RangeCount + this.offsetForData <= itemIndex)
			{
				throw new IndexOutOfRangeException("itemIndex=" + itemIndex.ToString() + " must be between 0 and " + (base.RangeCount + this.offsetForData).ToString());
			}
			base.SetIndexer(itemIndex + this.offsetForData);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000ADEC4 File Offset: 0x000AC0C4
		public bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001646 RID: 5702
		private const string RoomClass = "AD.ResourceType.Room";

		// Token: 0x04001647 RID: 5703
		private static readonly Dictionary<RecipientType, string> itemClasses = ADListViewDataSource.LoadItemClasses();

		// Token: 0x04001648 RID: 5704
		private AddressBookBase addressBookBase;

		// Token: 0x04001649 RID: 5705
		private string cookie;

		// Token: 0x0400164A RID: 5706
		private int cookieIndex;

		// Token: 0x0400164B RID: 5707
		private string searchString;

		// Token: 0x0400164C RID: 5708
		private bool search;

		// Token: 0x0400164D RID: 5709
		private int offsetForData;

		// Token: 0x0400164E RID: 5710
		private UserContext userContext;

		// Token: 0x0400164F RID: 5711
		private int lcid;

		// Token: 0x04001650 RID: 5712
		private string preferredDC;
	}
}

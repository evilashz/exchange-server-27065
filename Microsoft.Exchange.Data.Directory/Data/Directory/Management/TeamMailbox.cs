using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000771 RID: 1905
	[Serializable]
	public class TeamMailbox : ADPresentationObject
	{
		// Token: 0x06005D6E RID: 23918 RVA: 0x001424ED File Offset: 0x001406ED
		public TeamMailbox()
		{
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x001424F5 File Offset: 0x001406F5
		public TeamMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x06005D70 RID: 23920 RVA: 0x001424FE File Offset: 0x001406FE
		// (set) Token: 0x06005D71 RID: 23921 RVA: 0x00142506 File Offset: 0x00140706
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x0014250F File Offset: 0x0014070F
		// (set) Token: 0x06005D73 RID: 23923 RVA: 0x00142521 File Offset: 0x00140721
		public string DisplayName
		{
			get
			{
				return (string)this[TeamMailboxSchema.DisplayName];
			}
			internal set
			{
				this[TeamMailboxSchema.DisplayName] = value;
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x0014252F File Offset: 0x0014072F
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[TeamMailboxSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x00142541 File Offset: 0x00140741
		public MultiValuedProperty<ADObjectId> Owners
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[TeamMailboxSchema.Owners];
			}
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x06005D76 RID: 23926 RVA: 0x00142553 File Offset: 0x00140753
		public MultiValuedProperty<ADObjectId> Members
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[TeamMailboxSchema.TeamMailboxMembers];
			}
		}

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x00142565 File Offset: 0x00140765
		// (set) Token: 0x06005D78 RID: 23928 RVA: 0x00142577 File Offset: 0x00140777
		public bool ShowInMyClient
		{
			get
			{
				return (bool)this[TeamMailboxSchema.TeamMailboxShowInMyClient];
			}
			internal set
			{
				this[TeamMailboxSchema.TeamMailboxShowInMyClient] = value;
			}
		}

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x0014258A File Offset: 0x0014078A
		// (set) Token: 0x06005D7A RID: 23930 RVA: 0x0014259C File Offset: 0x0014079C
		public bool RemoveDuplicateMessages
		{
			get
			{
				return (bool)this[TeamMailboxSchema.SiteMailboxMessageDedupEnabled];
			}
			internal set
			{
				this[TeamMailboxSchema.SiteMailboxMessageDedupEnabled] = value;
			}
		}

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x001425AF File Offset: 0x001407AF
		// (set) Token: 0x06005D7C RID: 23932 RVA: 0x001425C1 File Offset: 0x001407C1
		public string MyRole
		{
			get
			{
				return (string)this[TeamMailboxSchema.TeamMailboxUserMembership];
			}
			internal set
			{
				this[TeamMailboxSchema.TeamMailboxUserMembership] = value;
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x001425CF File Offset: 0x001407CF
		// (set) Token: 0x06005D7E RID: 23934 RVA: 0x001425E1 File Offset: 0x001407E1
		public ADObjectId SharePointLinkedBy
		{
			get
			{
				return (ADObjectId)this[TeamMailboxSchema.SharePointLinkedBy];
			}
			internal set
			{
				this[TeamMailboxSchema.SharePointLinkedBy] = value;
			}
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x001425EF File Offset: 0x001407EF
		// (set) Token: 0x06005D80 RID: 23936 RVA: 0x00142601 File Offset: 0x00140801
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[TeamMailboxSchema.SharePointUrl];
			}
			internal set
			{
				this[TeamMailboxSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x06005D81 RID: 23937 RVA: 0x0014260F File Offset: 0x0014080F
		public bool Active
		{
			get
			{
				return this[TeamMailboxSchema.TeamMailboxClosedTime] == null;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0014261F File Offset: 0x0014081F
		// (set) Token: 0x06005D83 RID: 23939 RVA: 0x00142631 File Offset: 0x00140831
		public DateTime? ClosedTime
		{
			get
			{
				return (DateTime?)this[TeamMailboxSchema.TeamMailboxClosedTime];
			}
			internal set
			{
				this[TeamMailboxSchema.TeamMailboxClosedTime] = value;
			}
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x00142644 File Offset: 0x00140844
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[TeamMailboxSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x00142656 File Offset: 0x00140856
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[TeamMailboxSchema.EmailAddresses];
			}
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x00142668 File Offset: 0x00140868
		public Uri WebCollectionUrl
		{
			get
			{
				return (Uri)this[TeamMailboxSchema.SiteMailboxWebCollectionUrl];
			}
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x0014267A File Offset: 0x0014087A
		public Guid WebId
		{
			get
			{
				return (Guid)this[TeamMailboxSchema.SiteMailboxWebId];
			}
		}

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x0014268C File Offset: 0x0014088C
		internal MultiValuedProperty<ADObjectId> OwnersAndMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[TeamMailboxSchema.DelegateListLink];
			}
		}

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x0014269E File Offset: 0x0014089E
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return TeamMailbox.schema;
			}
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x001426A5 File Offset: 0x001408A5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.TeamMailboxObjectVersion;
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x001426AC File Offset: 0x001408AC
		internal static TeamMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new TeamMailbox(dataObject);
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x001426B9 File Offset: 0x001408B9
		internal static bool IsActiveTeamMailbox(ADUser dataObject)
		{
			return dataObject != null && dataObject[TeamMailboxSchema.TeamMailboxClosedTime] == null;
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001426D0 File Offset: 0x001408D0
		internal static bool IsPendingDeleteSiteMailbox(ADUser dataObject)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			DateTime? closedTime = TeamMailbox.FromDataObject(dataObject).ClosedTime;
			return ((dataObject.DisplayName != null && dataObject.DisplayName.StartsWith("MDEL:")) || (closedTime != null && closedTime.Value.ToUniversalTime() == TeamMailbox.ClosedTimeOfMarkedForDeletion)) && dataObject.SharePointUrl == null;
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x00142759 File Offset: 0x00140959
		internal static bool IsLocalTeamMailbox(ADUser dataObject)
		{
			return dataObject != null && dataObject.RecipientType == RecipientType.UserMailbox && dataObject.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox;
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x0014277C File Offset: 0x0014097C
		internal static bool IsRemoteTeamMailbox(ADUser dataObject)
		{
			return dataObject != null && dataObject.RecipientType == RecipientType.MailUser && (dataObject.RecipientDisplayType == RecipientDisplayType.SyncedTeamMailboxUser || dataObject.RecipientDisplayType == RecipientDisplayType.ACLableSyncedTeamMailboxUser);
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x001427DC File Offset: 0x001409DC
		internal static MultiValuedProperty<ADObjectId> MembersGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[TeamMailboxSchema.Owners];
			MultiValuedProperty<ADObjectId> multiValuedProperty2 = (MultiValuedProperty<ADObjectId>)propertyBag[TeamMailboxSchema.DelegateListLink];
			MultiValuedProperty<ADObjectId> multiValuedProperty3 = new MultiValuedProperty<ADObjectId>();
			foreach (ADObjectId item in multiValuedProperty2)
			{
				if (!multiValuedProperty.Contains(item))
				{
					multiValuedProperty3.Add(item);
				}
			}
			return multiValuedProperty3;
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x00142860 File Offset: 0x00140A60
		internal static Uri WebCollectionUrlGetter(IPropertyBag propertyBag)
		{
			string sharePointSiteInfo = (string)propertyBag[ADUserSchema.SharePointSiteInfo];
			string urlString;
			string text;
			TeamMailbox.ParseSharePointSiteInfo(sharePointSiteInfo, out urlString, out text);
			return TeamMailbox.GetUrl(urlString);
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x00142890 File Offset: 0x00140A90
		internal static object WebIdGetter(IPropertyBag propertyBag)
		{
			string sharePointSiteInfo = (string)propertyBag[ADUserSchema.SharePointSiteInfo];
			string text;
			string guidString;
			TeamMailbox.ParseSharePointSiteInfo(sharePointSiteInfo, out text, out guidString);
			return TeamMailbox.GetGuid(guidString);
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x001428C4 File Offset: 0x00140AC4
		internal static Uri GetUrl(string urlString)
		{
			Uri result = null;
			if (!string.IsNullOrEmpty(urlString))
			{
				try
				{
					result = new Uri(urlString);
				}
				catch (UriFormatException)
				{
				}
			}
			return result;
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x001428F8 File Offset: 0x00140AF8
		internal static Guid GetGuid(string guidString)
		{
			Guid result = Guid.Empty;
			if (!string.IsNullOrEmpty(guidString))
			{
				try
				{
					result = new Guid(guidString);
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			return result;
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x00142940 File Offset: 0x00140B40
		private static void ParseSharePointSiteInfo(string sharePointSiteInfo, out string webCollectionUrl, out string webId)
		{
			webCollectionUrl = string.Empty;
			webId = string.Empty;
			if (!string.IsNullOrEmpty(sharePointSiteInfo))
			{
				foreach (string text in sharePointSiteInfo.Split(new char[]
				{
					';'
				}))
				{
					if (text.StartsWith("WebCollectionUrl", StringComparison.OrdinalIgnoreCase))
					{
						webCollectionUrl = text.Substring("WebCollectionUrl".Length + 1);
					}
					else if (text.StartsWith("WebId", StringComparison.OrdinalIgnoreCase))
					{
						webId = text.Substring("WebId".Length + 1);
					}
				}
			}
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x001429D0 File Offset: 0x00140BD0
		internal static List<ADObjectId> MergeUsers(IList<ADObjectId> userList1, IList<ADObjectId> userList2)
		{
			if (userList1 == null)
			{
				throw new ArgumentNullException("userList1");
			}
			if (userList2 == null)
			{
				throw new ArgumentNullException("userList2");
			}
			List<ADObjectId> list = new List<ADObjectId>(userList1);
			foreach (ADObjectId item in userList2)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x00142A48 File Offset: 0x00140C48
		internal static IList<ADObjectId> DiffUsers(IList<ADObjectId> userList1, IList<ADObjectId> userList2)
		{
			if (userList1 == null)
			{
				throw new ArgumentNullException("userList1");
			}
			if (userList2 == null)
			{
				throw new ArgumentNullException("userList2");
			}
			Dictionary<ADObjectId, bool> dictionary = new Dictionary<ADObjectId, bool>();
			foreach (ADObjectId key in userList2)
			{
				dictionary[key] = true;
			}
			Dictionary<ADObjectId, bool> dictionary2 = new Dictionary<ADObjectId, bool>();
			foreach (ADObjectId key2 in userList1)
			{
				if (!dictionary.ContainsKey(key2))
				{
					dictionary2[key2] = true;
				}
			}
			return new List<ADObjectId>(dictionary2.Keys);
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x00142B10 File Offset: 0x00140D10
		internal void SetPolicy(TeamMailboxProvisioningPolicy policy)
		{
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			ADUser aduser = (ADUser)base.DataObject;
			aduser.IssueWarningQuota = policy.IssueWarningQuota;
			aduser.MaxReceiveSize = policy.MaxReceiveSize;
			aduser.ProhibitSendReceiveQuota = policy.ProhibitSendReceiveQuota;
			aduser.UseDatabaseQuotaDefaults = new bool?(false);
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x00142B78 File Offset: 0x00140D78
		internal void SetSharePointSiteInfo(Uri webCollectionUrl, Guid webId)
		{
			this[TeamMailboxSchema.SharePointSiteInfo] = string.Format("{0}:{1};{2}:{3}", new object[]
			{
				"WebCollectionUrl",
				(webCollectionUrl == null) ? string.Empty : webCollectionUrl.AbsoluteUri,
				"WebId",
				(webId == Guid.Empty) ? string.Empty : webId.ToString()
			});
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x00142BF0 File Offset: 0x00140DF0
		internal void SetMyRole(ADObjectId executingUserId)
		{
			MyRoleType myRoleType = MyRoleType.NoAccess;
			if (executingUserId != null)
			{
				if (this.Owners.Contains(executingUserId))
				{
					myRoleType = MyRoleType.Owner;
				}
				else if (this.Members.Contains(executingUserId))
				{
					myRoleType = MyRoleType.Member;
				}
			}
			this.MyRole = myRoleType.ToString();
		}

		// Token: 0x04003F2E RID: 16174
		internal const int TotalOwnersAndMembersLimit = 1800;

		// Token: 0x04003F2F RID: 16175
		internal const string NamePrefixOfMarkedForDeletion = "MDEL:";

		// Token: 0x04003F30 RID: 16176
		internal const int CsomRequestTimeoutInMilliSec = 60000;

		// Token: 0x04003F31 RID: 16177
		internal const int MaxPinnedInClient = 10;

		// Token: 0x04003F32 RID: 16178
		internal const bool ShowInMyClientDefaultValue = true;

		// Token: 0x04003F33 RID: 16179
		private const string WebCollectionUrlKey = "WebCollectionUrl";

		// Token: 0x04003F34 RID: 16180
		private const string WebIdKey = "WebId";

		// Token: 0x04003F35 RID: 16181
		internal static DateTime? ClosedTimeOfMarkedForDeletion = new DateTime?(new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc));

		// Token: 0x04003F36 RID: 16182
		private static TeamMailboxSchema schema = ObjectSchema.GetInstance<TeamMailboxSchema>();
	}
}

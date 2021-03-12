using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200071A RID: 1818
	[Serializable]
	public class GroupMailbox : ADPresentationObject
	{
		// Token: 0x060055C4 RID: 21956 RVA: 0x00135B6F File Offset: 0x00133D6F
		public GroupMailbox()
		{
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x00135B77 File Offset: 0x00133D77
		public GroupMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x060055C6 RID: 21958 RVA: 0x00135B80 File Offset: 0x00133D80
		public string Alias
		{
			get
			{
				return (string)this[GroupMailboxSchema.Alias];
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x060055C7 RID: 21959 RVA: 0x00135B92 File Offset: 0x00133D92
		// (set) Token: 0x060055C8 RID: 21960 RVA: 0x00135B9A File Offset: 0x00133D9A
		public string CalendarUrl { get; internal set; }

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x060055C9 RID: 21961 RVA: 0x00135BA3 File Offset: 0x00133DA3
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[GroupMailboxSchema.Database];
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x060055CA RID: 21962 RVA: 0x00135BB8 File Offset: 0x00133DB8
		public string Description
		{
			get
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this[GroupMailboxSchema.Description];
				if (multiValuedProperty != null && multiValuedProperty.Count > 0)
				{
					return multiValuedProperty[0];
				}
				return string.Empty;
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x060055CB RID: 21963 RVA: 0x00135BEF File Offset: 0x00133DEF
		public string DisplayName
		{
			get
			{
				return (string)this[GroupMailboxSchema.DisplayName];
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x060055CC RID: 21964 RVA: 0x00135C01 File Offset: 0x00133E01
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[GroupMailboxSchema.EmailAddresses];
			}
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x060055CD RID: 21965 RVA: 0x00135C13 File Offset: 0x00133E13
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[GroupMailboxSchema.ExchangeGuid];
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x00135C25 File Offset: 0x00133E25
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[GroupMailboxSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x00135C37 File Offset: 0x00133E37
		// (set) Token: 0x060055D0 RID: 21968 RVA: 0x00135C3F File Offset: 0x00133E3F
		public string InboxUrl { get; internal set; }

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x00135C48 File Offset: 0x00133E48
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[GroupMailboxSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x00135C5C File Offset: 0x00133E5C
		public CultureInfo Language
		{
			get
			{
				MultiValuedProperty<CultureInfo> source = (MultiValuedProperty<CultureInfo>)this[GroupMailboxSchema.Languages];
				return source.FirstOrDefault<CultureInfo>();
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x00135C80 File Offset: 0x00133E80
		// (set) Token: 0x060055D4 RID: 21972 RVA: 0x00135C88 File Offset: 0x00133E88
		public ADObjectId[] Members { get; internal set; }

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x00135C91 File Offset: 0x00133E91
		// (set) Token: 0x060055D6 RID: 21974 RVA: 0x00135C99 File Offset: 0x00133E99
		public IdentityDetails[] MembersDetails { get; internal set; }

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x060055D7 RID: 21975 RVA: 0x00135CA2 File Offset: 0x00133EA2
		// (set) Token: 0x060055D8 RID: 21976 RVA: 0x00135CAA File Offset: 0x00133EAA
		public GroupMailboxMembersSyncStatus MembersSyncStatus { get; internal set; }

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x00135CB3 File Offset: 0x00133EB3
		public ModernGroupObjectType ModernGroupType
		{
			get
			{
				return (ModernGroupObjectType)this[GroupMailboxSchema.ModernGroupType];
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x060055DA RID: 21978 RVA: 0x00135CC5 File Offset: 0x00133EC5
		internal MultiValuedProperty<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[GroupMailboxSchema.PublicToGroupSids];
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x00135CD7 File Offset: 0x00133ED7
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x00135CDF File Offset: 0x00133EDF
		public MultiValuedProperty<ADObjectId> Owners
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[GroupMailboxSchema.Owners];
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x00135CF1 File Offset: 0x00133EF1
		// (set) Token: 0x060055DE RID: 21982 RVA: 0x00135CF9 File Offset: 0x00133EF9
		public IdentityDetails[] OwnersDetails { get; internal set; }

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x00135D02 File Offset: 0x00133F02
		// (set) Token: 0x060055E0 RID: 21984 RVA: 0x00135D0A File Offset: 0x00133F0A
		public string PeopleUrl { get; internal set; }

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x060055E1 RID: 21985 RVA: 0x00135D13 File Offset: 0x00133F13
		// (set) Token: 0x060055E2 RID: 21986 RVA: 0x00135D1B File Offset: 0x00133F1B
		public string PhotoUrl { get; internal set; }

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x00135D24 File Offset: 0x00133F24
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[GroupMailboxSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x060055E4 RID: 21988 RVA: 0x00135D36 File Offset: 0x00133F36
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[GroupMailboxSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x060055E5 RID: 21989 RVA: 0x00135D48 File Offset: 0x00133F48
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)this[GroupMailboxSchema.RequireSenderAuthenticationEnabled];
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x00135D5A File Offset: 0x00133F5A
		public string ServerName
		{
			get
			{
				return (string)this[GroupMailboxSchema.ServerName];
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x060055E7 RID: 21991 RVA: 0x00135D6C File Offset: 0x00133F6C
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[GroupMailboxSchema.SharePointUrl];
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x060055E8 RID: 21992 RVA: 0x00135D7E File Offset: 0x00133F7E
		public string SharePointSiteUrl
		{
			get
			{
				return (string)this[GroupMailboxSchema.SharePointSiteUrl];
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x00135D90 File Offset: 0x00133F90
		public string SharePointDocumentsUrl
		{
			get
			{
				return (string)this[GroupMailboxSchema.SharePointDocumentsUrl];
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x00135DA2 File Offset: 0x00133FA2
		public bool IsMailboxConfigured
		{
			get
			{
				return (bool)this[GroupMailboxSchema.IsMailboxConfigured];
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x060055EB RID: 21995 RVA: 0x00135DB4 File Offset: 0x00133FB4
		public bool IsExternalResourcesPublished
		{
			get
			{
				return (bool)this[GroupMailboxSchema.IsExternalResourcesPublished];
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x060055EC RID: 21996 RVA: 0x00135DC6 File Offset: 0x00133FC6
		public string YammerGroupEmailAddress
		{
			get
			{
				return (string)this[GroupMailboxSchema.YammerGroupEmailAddress];
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x060055ED RID: 21997 RVA: 0x00135DD8 File Offset: 0x00133FD8
		// (set) Token: 0x060055EE RID: 21998 RVA: 0x00135DE0 File Offset: 0x00133FE0
		public string PermissionsVersion { get; internal set; }

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x00135DE9 File Offset: 0x00133FE9
		public bool AutoSubscribeNewGroupMembers
		{
			get
			{
				return (bool)this[GroupMailboxSchema.AutoSubscribeNewGroupMembers];
			}
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x060055F0 RID: 22000 RVA: 0x00135DFB File Offset: 0x00133FFB
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return GroupMailbox.schema;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x060055F1 RID: 22001 RVA: 0x00135E02 File Offset: 0x00134002
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.TeamMailboxObjectVersion;
			}
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00135E09 File Offset: 0x00134009
		internal static GroupMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new GroupMailbox(dataObject);
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00135E16 File Offset: 0x00134016
		internal static bool IsLocalGroupMailbox(ADUser dataObject)
		{
			return dataObject.RecipientType == RecipientType.UserMailbox && dataObject.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox;
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x00135E34 File Offset: 0x00134034
		internal static MultiValuedProperty<SecurityIdentifier> PublicToGroupSidsGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[GroupMailboxSchema.DelegateListLink];
			ModernGroupObjectType modernGroupObjectType = (ModernGroupObjectType)propertyBag[GroupMailboxSchema.ModernGroupType];
			MultiValuedProperty<SecurityIdentifier> multiValuedProperty2 = new MultiValuedProperty<SecurityIdentifier>();
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADObjectId instance = enumerator.Current;
						SecurityIdentifier securityIdentifier = ADObjectId.GetSecurityIdentifier(instance);
						if (securityIdentifier != null)
						{
							multiValuedProperty2.Add(securityIdentifier);
						}
					}
					return multiValuedProperty2;
				}
			}
			if (modernGroupObjectType == ModernGroupObjectType.Public)
			{
				SecurityIdentifier item = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
				multiValuedProperty2.Add(item);
			}
			return multiValuedProperty2;
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00135EE4 File Offset: 0x001340E4
		internal static string SharePointSiteUrlGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> sharePointResources = (MultiValuedProperty<string>)propertyBag[ADMailboxRecipientSchema.SharePointResources];
			return GroupMailbox.ExtractSharePointResource(sharePointResources, "SiteUrl");
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x00135F10 File Offset: 0x00134110
		internal static string SharePointDocumentsUrlGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> sharePointResources = (MultiValuedProperty<string>)propertyBag[ADMailboxRecipientSchema.SharePointResources];
			return GroupMailbox.ExtractSharePointResource(sharePointResources, "DocumentsUrl");
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00135F3C File Offset: 0x0013413C
		private static string ExtractSharePointResource(MultiValuedProperty<string> sharePointResources, string resourceKey)
		{
			string result = null;
			if (sharePointResources != null)
			{
				foreach (string text in sharePointResources)
				{
					if (text.StartsWith(resourceKey + "=", StringComparison.OrdinalIgnoreCase))
					{
						result = text.Substring(resourceKey.Length + 1);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x040039E9 RID: 14825
		public const string SiteUrl = "SiteUrl";

		// Token: 0x040039EA RID: 14826
		public const string DocumentsUrl = "DocumentsUrl";

		// Token: 0x040039EB RID: 14827
		public const string ResourceSeparator = "=";

		// Token: 0x040039EC RID: 14828
		private static GroupMailboxSchema schema = ObjectSchema.GetInstance<GroupMailboxSchema>();

		// Token: 0x0200071B RID: 1819
		public class GroupIdentityOwnerConstants
		{
			// Token: 0x040039F6 RID: 14838
			public const string Exchange = "Exchange";

			// Token: 0x040039F7 RID: 14839
			public const string AADCollabSpace = "AADCollabspace";

			// Token: 0x040039F8 RID: 14840
			public const string AADGroup = "AADGroup";
		}
	}
}

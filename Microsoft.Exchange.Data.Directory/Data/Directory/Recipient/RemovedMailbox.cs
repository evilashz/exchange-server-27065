using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000263 RID: 611
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RemovedMailbox : DeletedRecipient
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0007A7D1 File Offset: 0x000789D1
		internal override ADObjectSchema Schema
		{
			get
			{
				return RemovedMailbox.schema;
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0007A7D8 File Offset: 0x000789D8
		public RemovedMailbox()
		{
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0007A7E0 File Offset: 0x000789E0
		internal RemovedMailbox(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0007A7EC File Offset: 0x000789EC
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					base.ImplicitFilter,
					new ExistsFilter(RemovedMailboxSchema.ExchangeGuid)
				});
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0007A81C File Offset: 0x00078A1C
		internal static object IsPasswordResetRequiredGetter(IPropertyBag propertyBag)
		{
			return null != propertyBag[RemovedMailboxSchema.NetID];
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0007A834 File Offset: 0x00078A34
		internal new static object NameGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADObjectSchema.RawName];
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0007A871 File Offset: 0x00078A71
		internal static object NetIDGetter(IPropertyBag propertyBag)
		{
			return RemovedMailbox.GetNetIDWithPrefix("DLTDNETID", propertyBag);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0007A87E File Offset: 0x00078A7E
		internal static object ConsumerNetIDGetter(IPropertyBag propertyBag)
		{
			return RemovedMailbox.GetNetIDWithPrefix("DLTDCSID", propertyBag);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0007A88C File Offset: 0x00078A8C
		private static NetID GetNetIDWithPrefix(string netIdPrefix, IPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			if (proxyAddressCollection != null)
			{
				ProxyAddress proxyAddress = proxyAddressCollection.FindPrimary(ProxyAddressPrefix.GetPrefix(netIdPrefix));
				if (null != proxyAddress)
				{
					return NetID.Parse(proxyAddress.ValueString);
				}
			}
			return null;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0007A8D0 File Offset: 0x00078AD0
		internal static SinglePropertyFilter NameFilterBuilderDelegate(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(ADObjectSchema.Name);
			}
			if (filter is ComparisonFilter)
			{
				string arg = (string)((ComparisonFilter)filter).PropertyValue;
				string text = string.Format("{0}{1}{2}", arg, '\n', "DEL");
				return new TextFilter(ADObjectSchema.Name, text, MatchOptions.Prefix, MatchFlags.IgnoreCase);
			}
			if (!(filter is TextFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForPropertyMultiple(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter), typeof(TextFilter)));
			}
			TextFilter textFilter = (TextFilter)filter;
			if (textFilter.MatchOptions == MatchOptions.FullString)
			{
				string text2 = textFilter.Text;
				string text3 = string.Format("{0}{1}{2}", text2, '\n', "DEL");
				return new TextFilter(ADObjectSchema.Name, text3, MatchOptions.Prefix, textFilter.MatchFlags);
			}
			if (textFilter.MatchOptions == MatchOptions.Prefix)
			{
				return new TextFilter(ADObjectSchema.Name, textFilter.Text, MatchOptions.Prefix, textFilter.MatchFlags);
			}
			if (textFilter.MatchOptions == MatchOptions.Suffix)
			{
				string text4 = textFilter.Text;
				string text5 = string.Format("{0}{1}{2}", text4, '\n', "DEL");
				return new TextFilter(ADObjectSchema.Name, text5, MatchOptions.SubString, textFilter.MatchFlags);
			}
			if (textFilter.MatchOptions == MatchOptions.SubString)
			{
				return new TextFilter(ADObjectSchema.Name, textFilter.Text, MatchOptions.SubString, textFilter.MatchFlags);
			}
			throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedMatchOptionsForProperty(filter.Property.Name, textFilter.MatchOptions.ToString()));
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0007AA53 File Offset: 0x00078C53
		public new string Name
		{
			get
			{
				return (string)this[RemovedMailboxSchema.Name];
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0007AA65 File Offset: 0x00078C65
		public string RawName
		{
			get
			{
				return (string)this[ADObjectSchema.RawName];
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0007AA77 File Offset: 0x00078C77
		public ADObjectId PreviousDatabase
		{
			get
			{
				return (ADObjectId)this[RemovedMailboxSchema.PreviousDatabase];
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0007AA8C File Offset: 0x00078C8C
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)this.propertyBag[ADRecipientSchema.EmailAddresses];
				if (proxyAddressCollection != null)
				{
					ProxyAddressCollection proxyAddressCollection2 = new ProxyAddressCollection();
					foreach (ProxyAddress proxyAddress in proxyAddressCollection)
					{
						if (!proxyAddress.PrefixString.Equals("DLTDNETID") && !proxyAddress.PrefixString.Equals("DLTDCSID"))
						{
							proxyAddressCollection2.Add(proxyAddress);
						}
					}
					return proxyAddressCollection2;
				}
				return null;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0007AB20 File Offset: 0x00078D20
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[RemovedMailboxSchema.ExchangeGuid];
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0007AB32 File Offset: 0x00078D32
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[RemovedMailboxSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0007AB44 File Offset: 0x00078D44
		public string SamAccountName
		{
			get
			{
				return (string)this[RemovedMailboxSchema.SamAccountName];
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0007AB56 File Offset: 0x00078D56
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[RemovedMailboxSchema.WindowsLiveID];
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0007AB68 File Offset: 0x00078D68
		public SmtpAddress MicrosoftOnlineServicesID
		{
			get
			{
				return this.WindowsLiveID;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0007AB70 File Offset: 0x00078D70
		internal NetID NetID
		{
			get
			{
				return (NetID)this[RemovedMailboxSchema.NetID];
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0007AB82 File Offset: 0x00078D82
		internal NetID ConsumerNetID
		{
			get
			{
				return (NetID)this[RemovedMailboxSchema.ConsumerNetID];
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0007AB94 File Offset: 0x00078D94
		public bool IsPasswordResetRequired
		{
			get
			{
				return (bool)this[RemovedMailboxSchema.IsPasswordResetRequired];
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x0007ABA6 File Offset: 0x00078DA6
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x0007ABAE File Offset: 0x00078DAE
		internal bool StoreMailboxExists { get; set; }

		// Token: 0x04000E12 RID: 3602
		internal const string DELETED_NETID_PREFIX = "DLTDNETID";

		// Token: 0x04000E13 RID: 3603
		internal const string DELETED_CONSUMERNETID_PREFIX = "DLTDCSID";

		// Token: 0x04000E14 RID: 3604
		private static readonly RemovedMailboxSchema schema = ObjectSchema.GetInstance<RemovedMailboxSchema>();
	}
}

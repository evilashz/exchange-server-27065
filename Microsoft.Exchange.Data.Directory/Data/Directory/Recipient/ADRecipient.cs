using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E3 RID: 483
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADRecipient : ADObject, IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000603ED File Offset: 0x0005E5ED
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADRecipientProperties.Instance;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x000603F4 File Offset: 0x0005E5F4
		internal override string MostDerivedObjectClass
		{
			get
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.ExceptionMostDerivedOnBase("ADRecipient"));
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00060405 File Offset: 0x0005E605
		internal override bool SkipPiiRedaction
		{
			get
			{
				return ADRecipient.IsSystemMailbox(this.RecipientTypeDetails);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00060412 File Offset: 0x0005E612
		public virtual string ObjectCategoryCN
		{
			get
			{
				return this.ObjectCategoryName;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0006041A File Offset: 0x0005E61A
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return Filters.DefaultRecipientFilter;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00060421 File Offset: 0x0005E621
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00060428 File Offset: 0x0005E628
		internal ADRecipient(IRecipientSession session, PropertyBag propertyBag)
		{
			this.m_Session = session;
			this.propertyBag = (ADPropertyBag)propertyBag;
			this.SetIsReadOnly(session.ReadOnly);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0006044F File Offset: 0x0005E64F
		public ADRecipient()
		{
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00060458 File Offset: 0x0005E658
		internal static object MailboxRelationTypeGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADUserSchema.AuxMailboxParentObjectId];
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[ADUserSchema.AuxMailboxParentObjectIdBL];
			if (adobjectId != null && multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ErrorInvalidMailboxRelationType, ADUserSchema.MailboxRelationType, string.Format("Parent ADObjectId: {0}, Aux Mailboxes: {1}", adobjectId, multiValuedProperty)));
			}
			MailboxRelationType mailboxRelationType = MailboxRelationType.None;
			if (adobjectId != null)
			{
				mailboxRelationType = MailboxRelationType.Primary;
			}
			else if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				mailboxRelationType = MailboxRelationType.Secondary;
			}
			return mailboxRelationType;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000604D4 File Offset: 0x0005E6D4
		internal static object AntispamBypassEnabledGetter(IPropertyBag propertyBag)
		{
			MessageHygieneFlags messageHygieneFlags = (MessageHygieneFlags)propertyBag[ADRecipientSchema.MessageHygieneFlags];
			return BoxedConstants.GetBool((messageHygieneFlags & MessageHygieneFlags.AntispamBypass) == MessageHygieneFlags.AntispamBypass);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00060500 File Offset: 0x0005E700
		internal static void AntispamBypassEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			MessageHygieneFlags messageHygieneFlags = (MessageHygieneFlags)propertyBag[ADRecipientSchema.MessageHygieneFlags];
			if (flag)
			{
				propertyBag[ADRecipientSchema.MessageHygieneFlags] = (messageHygieneFlags | MessageHygieneFlags.AntispamBypass);
				return;
			}
			propertyBag[ADRecipientSchema.MessageHygieneFlags] = (messageHygieneFlags & ~MessageHygieneFlags.AntispamBypass);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00060550 File Offset: 0x0005E750
		internal static object UseMapiRichTextFormatGetter(IPropertyBag propertyBag)
		{
			bool? flag = (bool?)propertyBag[ADRecipientSchema.MapiRecipient];
			UseMapiRichTextFormat useMapiRichTextFormat;
			if (flag == null)
			{
				useMapiRichTextFormat = UseMapiRichTextFormat.UseDefaultSettings;
			}
			else if (flag == true)
			{
				useMapiRichTextFormat = UseMapiRichTextFormat.Always;
			}
			else
			{
				useMapiRichTextFormat = UseMapiRichTextFormat.Never;
			}
			return useMapiRichTextFormat;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000605A0 File Offset: 0x0005E7A0
		internal static void UseMapiRichTextFormatSetter(object value, IPropertyBag propertyBag)
		{
			switch ((UseMapiRichTextFormat)value)
			{
			case UseMapiRichTextFormat.Never:
				propertyBag[ADRecipientSchema.MapiRecipient] = false;
				return;
			case UseMapiRichTextFormat.Always:
				propertyBag[ADRecipientSchema.MapiRecipient] = true;
				return;
			case UseMapiRichTextFormat.UseDefaultSettings:
				propertyBag[ADRecipientSchema.MapiRecipient] = null;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00060608 File Offset: 0x0005E808
		internal static string OrganizationUnitFromADObjectId(ADObjectId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			StringBuilder stringBuilder = new StringBuilder();
			ADObjectId parent = id.Parent;
			if (parent != null && parent.DistinguishedName.IndexOf("DC=", StringComparison.OrdinalIgnoreCase) != -1)
			{
				ADObjectId domainId = parent.DomainId;
				stringBuilder.Append(DNConvertor.FqdnFromDomainDistinguishedName(domainId.DistinguishedName));
				for (int i = 1; i <= parent.Depth - domainId.Depth; i++)
				{
					ADObjectId adobjectId = parent.DescendantDN(i);
					string prefix = adobjectId.Rdn.Prefix;
					if (prefix.Equals("OU", StringComparison.OrdinalIgnoreCase) || prefix.Equals("CN", StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder.Append('/');
						stringBuilder.Append(adobjectId.Rdn.UnescapedName);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000606D8 File Offset: 0x0005E8D8
		internal static object OUGetter(IPropertyBag propertyBag)
		{
			Exception ex = null;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId != null)
				{
					return ADRecipient.OrganizationUnitFromADObjectId(adobjectId);
				}
				ex = new ArgumentNullException(DirectoryStrings.ExArgumentNullException("Id"));
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("OU", ex.Message), ADRecipientSchema.OrganizationalUnit, propertyBag[ADObjectSchema.Id]), ex);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00060840 File Offset: 0x0005EA40
		internal static GetterDelegate OwaProtocolSettingsGetterDelegate()
		{
			return delegate(IPropertyBag propertyBag)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ProtocolSettings];
				string text = null;
				string text2 = null;
				foreach (string text3 in multiValuedProperty)
				{
					if (text3.StartsWith("OWA"))
					{
						text2 = text3;
					}
					else if (text3.StartsWith("HTTP"))
					{
						text = text3;
					}
				}
				string text4 = (text2 == null) ? text : text2;
				bool flag;
				if (text4 == null)
				{
					flag = true;
				}
				else
				{
					string[] array = text4.Split(new char[]
					{
						'§'
					});
					flag = (array.Length <= 1 || array[1].Equals("1"));
				}
				return flag;
			};
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000608F7 File Offset: 0x0005EAF7
		internal static SetterDelegate OwaProtocolSettingsSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ProtocolSettings];
				for (int i = 0; i < multiValuedProperty.Count; i++)
				{
					if (multiValuedProperty[i].StartsWith("OWA", StringComparison.OrdinalIgnoreCase) || multiValuedProperty[i].StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
					{
						multiValuedProperty.RemoveAt(i);
						i--;
					}
				}
				if ((bool)value)
				{
					multiValuedProperty.Add("OWA§1");
					multiValuedProperty.Add("HTTP§1§1§§§§§§");
					return;
				}
				multiValuedProperty.Add("OWA§0");
				multiValuedProperty.Add("HTTP§0§1§§§§§§");
			};
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00060916 File Offset: 0x0005EB16
		internal static object CommonNameGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.RawName];
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00060924 File Offset: 0x0005EB24
		internal static object JournalArchiveAddressGetter(IReadOnlyPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddresses = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			return ADRecipient.JournalArchiveAddressInternalGetter(proxyAddresses);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00060950 File Offset: 0x0005EB50
		private static SmtpAddress JournalArchiveAddressInternalGetter(ProxyAddressCollection proxyAddresses)
		{
			ProxyAddress proxyAddress;
			return ADRecipient.JournalArchiveAddressInternalGetterWithProxyAddress(proxyAddresses, out proxyAddress);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00060968 File Offset: 0x0005EB68
		private static SmtpAddress JournalArchiveAddressInternalGetterWithProxyAddress(ProxyAddressCollection proxyAddresses, out ProxyAddress journalArchiveProxyAddress)
		{
			List<ProxyAddress> list = new List<ProxyAddress>();
			foreach (ProxyAddress proxyAddress in proxyAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.JRNL)
				{
					list.Add(proxyAddress);
				}
			}
			if (list.Count != 1 || list[0] is InvalidProxyAddress)
			{
				journalArchiveProxyAddress = null;
				return SmtpAddress.Empty;
			}
			journalArchiveProxyAddress = list[0];
			return new SmtpAddress(list[0].AddressString);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00060A08 File Offset: 0x0005EC08
		internal static void JournalArchiveAddressSetter(object value, IPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			SmtpAddress smtpAddress = (SmtpAddress)value;
			if (!smtpAddress.IsValidAddress)
			{
				throw new FormatException(DataStrings.InvalidSmtpAddress(smtpAddress.ToString()));
			}
			ProxyAddress proxyAddress;
			SmtpAddress value2 = ADRecipient.JournalArchiveAddressInternalGetterWithProxyAddress(proxyAddressCollection, out proxyAddress);
			ProxyAddress item = ProxyAddress.Parse(ProxyAddressPrefix.JRNL.PrimaryPrefix, smtpAddress.ToString());
			if (smtpAddress == SmtpAddress.NullReversePath)
			{
				if (proxyAddress != null)
				{
					proxyAddressCollection.Remove(proxyAddress);
					return;
				}
			}
			else
			{
				if (value2 == SmtpAddress.Empty)
				{
					proxyAddressCollection.Add(item);
					return;
				}
				if (value2.CompareTo(smtpAddress) != 0)
				{
					proxyAddressCollection.Remove(proxyAddress);
					proxyAddressCollection.Add(item);
				}
			}
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00060ACC File Offset: 0x0005ECCC
		internal static QueryFilter JournalArchiveAddressFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			string str = ProxyAddressPrefix.JRNL + ":";
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, str + comparisonFilter.PropertyValue.ToString());
			case ComparisonOperator.NotEqual:
				return new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.EmailAddresses, str + comparisonFilter.PropertyValue.ToString());
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00060B98 File Offset: 0x0005ED98
		internal static object PrimarySmtpAddressGetter(IReadOnlyPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			List<ProxyAddress> list = new List<ProxyAddress>();
			foreach (ProxyAddress proxyAddress in proxyAddressCollection)
			{
				if (proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
				{
					list.Add(proxyAddress);
				}
			}
			if (list.Count != 1 || list[0] is InvalidProxyAddress)
			{
				return SmtpAddress.Empty;
			}
			return new SmtpAddress(list[0].AddressString);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00060C50 File Offset: 0x0005EE50
		internal static void PrimarySmtpAddressSetter(object value, IPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			SmtpAddress smtpAddress = (SmtpAddress)value;
			if (!smtpAddress.IsValidAddress)
			{
				throw new FormatException(DataStrings.InvalidSmtpAddress(smtpAddress.ToString()));
			}
			proxyAddressCollection.MakePrimary(ProxyAddress.Parse(smtpAddress.ToString()));
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00060CB4 File Offset: 0x0005EEB4
		internal static QueryFilter PrimarySmtpAddressFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "SMTP:" + comparisonFilter.PropertyValue.ToString());
			case ComparisonOperator.NotEqual:
				return new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.EmailAddresses, "SMTP:" + comparisonFilter.PropertyValue.ToString());
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00060D78 File Offset: 0x0005EF78
		internal static object EmailAddressPolicyEnabledGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.PoliciesIncluded];
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.PoliciesExcluded];
			string text = EmailAddressPolicy.PolicyGuid.ToString("B");
			if (!multiValuedProperty2.Contains(text))
			{
				foreach (string text2 in multiValuedProperty)
				{
					if (-1 != text2.IndexOf(text, StringComparison.OrdinalIgnoreCase))
					{
						return BoxedConstants.True;
					}
				}
			}
			return BoxedConstants.False;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00060E1C File Offset: 0x0005F01C
		internal static void EmailAddressPolicyEnabledSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.PoliciesIncluded];
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.PoliciesExcluded];
			string text = EmailAddressPolicy.PolicyGuid.ToString("B");
			if ((bool)value)
			{
				if (multiValuedProperty2.Contains(text))
				{
					multiValuedProperty2.Remove(text);
				}
				bool flag = false;
				foreach (string text2 in multiValuedProperty)
				{
					if (-1 != text2.IndexOf(text, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					multiValuedProperty.Add(text);
					return;
				}
			}
			else if (!multiValuedProperty2.Contains(text))
			{
				multiValuedProperty2.Add(text);
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00060EE4 File Offset: 0x0005F0E4
		internal static QueryFilter EmailAddressPolicyEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			string text = EmailAddressPolicy.PolicyGuid.ToString("B");
			return ADObject.BoolFilterBuilder(filter, new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.PoliciesExcluded, text),
				new TextFilter(ADRecipientSchema.PoliciesIncluded, text, MatchOptions.SubString, MatchFlags.IgnoreCase)
			}));
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00060F36 File Offset: 0x0005F136
		internal static object ReadOnlyAddressListMembershipGetter(IPropertyBag propertyBag)
		{
			return new MultiValuedProperty<ADObjectId>(true, ADRecipientSchema.ReadOnlyAddressListMembership, ((MultiValuedProperty<ADObjectId>)propertyBag[ADRecipientSchema.AddressListMembership]).ToArray());
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00060F58 File Offset: 0x0005F158
		internal static object ReadOnlyPoliciesIncludedGetter(IPropertyBag propertyBag)
		{
			return new MultiValuedProperty<string>(true, ADRecipientSchema.ReadOnlyPoliciesIncluded, ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.PoliciesIncluded]).ToArray());
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00060F7A File Offset: 0x0005F17A
		internal static object ReadOnlyPoliciesExcludedGetter(IPropertyBag propertyBag)
		{
			return new MultiValuedProperty<string>(true, ADRecipientSchema.ReadOnlyPoliciesExcluded, ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.PoliciesExcluded]).ToArray());
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00060F9C File Offset: 0x0005F19C
		internal static object ReadOnlyProtocolSettingsGetter(IPropertyBag propertyBag)
		{
			return new MultiValuedProperty<string>(true, ADRecipientSchema.ReadOnlyProtocolSettings, ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ProtocolSettings]).ToArray());
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00060FC0 File Offset: 0x0005F1C0
		internal static object RecipientTypeGetter(IPropertyBag propertyBag)
		{
			RecipientType recipientType = RecipientType.Invalid;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass];
			bool flag = !string.IsNullOrEmpty((string)propertyBag[ADRecipientSchema.Alias]);
			GroupTypeFlags groupTypeFlags = (GroupTypeFlags)propertyBag[ADGroupSchema.GroupType];
			if (multiValuedProperty.Contains("computer"))
			{
				recipientType = RecipientType.Computer;
			}
			else if (multiValuedProperty.Contains("user"))
			{
				if (flag && !string.IsNullOrEmpty((string)propertyBag[IADMailStorageSchema.ServerLegacyDN]))
				{
					ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
					if (adobjectId == null && (ObjectState)propertyBag[ADObjectSchema.ObjectState] != ObjectState.New)
					{
						throw new DataValidationException(new PropertyValidationError(DirectoryStrings.IdIsNotSet, ADRecipientSchema.RecipientType, null));
					}
					if (adobjectId != null && adobjectId.Parent.Rdn.UnescapedName.Equals("Microsoft Exchange System Objects", StringComparison.OrdinalIgnoreCase))
					{
						recipientType = RecipientType.SystemMailbox;
					}
					else
					{
						recipientType = RecipientType.UserMailbox;
					}
				}
				else if (flag)
				{
					recipientType = RecipientType.MailUser;
				}
				else
				{
					recipientType = RecipientType.User;
				}
			}
			else if (multiValuedProperty.Contains("contact"))
			{
				recipientType = (flag ? RecipientType.MailContact : RecipientType.Contact);
			}
			else if (multiValuedProperty.Contains("group"))
			{
				if (!flag)
				{
					recipientType = RecipientType.Group;
				}
				else if ((groupTypeFlags & GroupTypeFlags.Universal) == GroupTypeFlags.None)
				{
					recipientType = RecipientType.MailNonUniversalGroup;
				}
				else if ((groupTypeFlags & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled)
				{
					recipientType = RecipientType.MailUniversalSecurityGroup;
				}
				else
				{
					recipientType = RecipientType.MailUniversalDistributionGroup;
				}
			}
			else if (multiValuedProperty.Contains("msExchDynamicDistributionList"))
			{
				recipientType = (flag ? RecipientType.DynamicDistributionGroup : RecipientType.Invalid);
			}
			else if (multiValuedProperty.Contains(ADPublicFolder.MostDerivedClass))
			{
				recipientType = (flag ? RecipientType.PublicFolder : RecipientType.Invalid);
			}
			else if (multiValuedProperty.Contains(ADSystemAttendantMailbox.MostDerivedClass))
			{
				recipientType = (flag ? RecipientType.SystemAttendantMailbox : RecipientType.Invalid);
			}
			else if (multiValuedProperty.Contains(ADSystemMailbox.MostDerivedClass))
			{
				recipientType = ((flag && !string.IsNullOrEmpty((string)propertyBag[IADMailStorageSchema.ServerLegacyDN])) ? RecipientType.SystemMailbox : RecipientType.Invalid);
			}
			else if (multiValuedProperty.Contains(ADMicrosoftExchangeRecipient.MostDerivedClass))
			{
				recipientType = (flag ? RecipientType.MicrosoftExchange : RecipientType.Invalid);
			}
			else if (multiValuedProperty.Contains("msExchPublicMDB"))
			{
				recipientType = (flag ? RecipientType.PublicDatabase : RecipientType.Invalid);
			}
			return recipientType;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x000611D4 File Offset: 0x0005F3D4
		internal static object RecipientTypeDetailsGetter(IPropertyBag propertyBag)
		{
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)propertyBag[ADRecipientSchema.RecipientTypeDetailsValue];
			if (recipientTypeDetails == RecipientTypeDetails.None)
			{
				switch ((RecipientType)propertyBag[ADRecipientSchema.RecipientType])
				{
				case RecipientType.User:
				{
					UserAccountControlFlags userAccountControlFlags = (UserAccountControlFlags)propertyBag[ADUserSchema.UserAccountControl];
					recipientTypeDetails = (((userAccountControlFlags & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.AccountDisabled) ? RecipientTypeDetails.DisabledUser : RecipientTypeDetails.User);
					break;
				}
				case RecipientType.UserMailbox:
					recipientTypeDetails = RecipientTypeDetails.LegacyMailbox;
					break;
				case RecipientType.MailUser:
					recipientTypeDetails = RecipientTypeDetails.MailUser;
					break;
				case RecipientType.Contact:
					recipientTypeDetails = RecipientTypeDetails.Contact;
					break;
				case RecipientType.MailContact:
					recipientTypeDetails = RecipientTypeDetails.MailContact;
					break;
				case RecipientType.Group:
				{
					GroupTypeFlags groupTypeFlags = (GroupTypeFlags)propertyBag[ADGroupSchema.GroupType];
					if ((groupTypeFlags & GroupTypeFlags.Universal) == GroupTypeFlags.Universal)
					{
						recipientTypeDetails = (((groupTypeFlags & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled) ? RecipientTypeDetails.UniversalSecurityGroup : RecipientTypeDetails.UniversalDistributionGroup);
					}
					else
					{
						recipientTypeDetails = RecipientTypeDetails.NonUniversalGroup;
					}
					break;
				}
				case RecipientType.MailUniversalDistributionGroup:
					recipientTypeDetails = RecipientTypeDetails.MailUniversalDistributionGroup;
					break;
				case RecipientType.MailUniversalSecurityGroup:
					recipientTypeDetails = RecipientTypeDetails.MailUniversalSecurityGroup;
					break;
				case RecipientType.MailNonUniversalGroup:
					recipientTypeDetails = RecipientTypeDetails.MailNonUniversalGroup;
					break;
				case RecipientType.DynamicDistributionGroup:
					recipientTypeDetails = RecipientTypeDetails.DynamicDistributionGroup;
					break;
				case RecipientType.PublicFolder:
					recipientTypeDetails = RecipientTypeDetails.PublicFolder;
					break;
				case RecipientType.SystemAttendantMailbox:
					recipientTypeDetails = RecipientTypeDetails.SystemAttendantMailbox;
					break;
				case RecipientType.SystemMailbox:
					recipientTypeDetails = RecipientTypeDetails.SystemMailbox;
					break;
				case RecipientType.MicrosoftExchange:
					recipientTypeDetails = RecipientTypeDetails.MicrosoftExchange;
					break;
				case RecipientType.Computer:
					recipientTypeDetails = RecipientTypeDetails.Computer;
					break;
				}
			}
			return recipientTypeDetails;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00061358 File Offset: 0x0005F558
		internal static void RecipientTypeDetailsSetter(object value, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				long num = (long)((RecipientTypeDetails)value & RecipientTypeDetails.AllUniqueRecipientTypes);
				if ((num - 1L & num) != 0L)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ErrorTwoOrMoreUniqueRecipientTypes(value.ToString()), ADRecipientSchema.RecipientTypeDetails, value));
				}
			}
			propertyBag[ADRecipientSchema.RecipientTypeDetailsValue] = value;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000613AC File Offset: 0x0005F5AC
		internal static object RecipientTypeDetailsRawGetter(IPropertyBag propertyBag)
		{
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)propertyBag[ADRecipientSchema.RecipientTypeDetailsValue];
			return (long)recipientTypeDetails;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000613D0 File Offset: 0x0005F5D0
		internal static QueryFilter HiddenFromAddressListsEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsValue, true));
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000613EC File Offset: 0x0005F5EC
		internal static object HiddenFromAddressListsEnabledGetter(IPropertyBag propertyBag)
		{
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)propertyBag[ADRecipientSchema.RecipientTypeDetailsValue];
			int num = (int)(propertyBag[ADRecipientSchema.RecipientSoftDeletedStatus] ?? 0);
			if (recipientTypeDetails == RecipientTypeDetails.MailboxPlan || num != 0)
			{
				return true;
			}
			return propertyBag[ADRecipientSchema.HiddenFromAddressListsValue];
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00061443 File Offset: 0x0005F643
		internal static void HiddenFromAddressListsEnabledSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADRecipientSchema.HiddenFromAddressListsValue] = value;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00061454 File Offset: 0x0005F654
		internal static object DefaultMailTipGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> translations = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.MailTipTranslations];
			return ADRecipient.DefaultMailTipGetter(translations);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00061478 File Offset: 0x0005F678
		internal static object DefaultMailTipGetter(MultiValuedProperty<string> translations)
		{
			foreach (string text in translations)
			{
				if (ADRecipient.IsDefaultTranslation(text))
				{
					return text.Substring("default".Length + 1);
				}
			}
			return null;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000614E0 File Offset: 0x0005F6E0
		internal static bool IsAllowedDeliveryRestrictionGroup(RecipientType type)
		{
			return type == RecipientType.MailUniversalDistributionGroup || type == RecipientType.MailUniversalSecurityGroup || type == RecipientType.MailNonUniversalGroup || type == RecipientType.DynamicDistributionGroup;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000614F7 File Offset: 0x0005F6F7
		internal static bool IsAllowedDeliveryRestrictionIndividual(RecipientType type)
		{
			return type == RecipientType.UserMailbox || type == RecipientType.MailUser || type == RecipientType.MailContact || type == RecipientType.MicrosoftExchange;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00061510 File Offset: 0x0005F710
		internal static void DefaultMailTipSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.MailTipTranslations];
			if (string.IsNullOrEmpty(value as string))
			{
				multiValuedProperty.Clear();
				return;
			}
			string text = "default:" + value;
			for (int i = 0; i < multiValuedProperty.Count; i++)
			{
				string translation = multiValuedProperty[i];
				if (ADRecipient.IsDefaultTranslation(translation))
				{
					multiValuedProperty[i] = text;
					return;
				}
			}
			multiValuedProperty.Add(text);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00061580 File Offset: 0x0005F780
		internal static object IsPersonToPersonTextMessagingEnabledGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<TextMessagingStateBase> multiValuedProperty = (MultiValuedProperty<TextMessagingStateBase>)propertyBag[ADRecipientSchema.TextMessagingState];
			foreach (TextMessagingStateBase textMessagingStateBase in multiValuedProperty)
			{
				TextMessagingDeliveryPointState textMessagingDeliveryPointState = textMessagingStateBase as TextMessagingDeliveryPointState;
				if (textMessagingDeliveryPointState != null && !textMessagingDeliveryPointState.Shared && textMessagingDeliveryPointState.PersonToPersonMessagingEnabled)
				{
					return BoxedConstants.True;
				}
			}
			return BoxedConstants.False;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00061604 File Offset: 0x0005F804
		internal static QueryFilter IsPersonToPersonTextMessagingEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			QueryFilter queryFilter;
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				queryFilter = new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, 536870912UL);
				break;
			case ComparisonOperator.NotEqual:
				queryFilter = new NotFilter(new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, 536870912UL));
				break;
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			return new AndFilter(new QueryFilter[]
			{
				new NotFilter(new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, (ulong)int.MinValue)),
				new NotFilter(new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, 1073741824UL)),
				queryFilter
			});
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000616F4 File Offset: 0x0005F8F4
		internal static object IsMachineToPersonTextMessagingEnabledGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<TextMessagingStateBase> multiValuedProperty = (MultiValuedProperty<TextMessagingStateBase>)propertyBag[ADRecipientSchema.TextMessagingState];
			foreach (TextMessagingStateBase textMessagingStateBase in multiValuedProperty)
			{
				TextMessagingDeliveryPointState textMessagingDeliveryPointState = textMessagingStateBase as TextMessagingDeliveryPointState;
				if (textMessagingDeliveryPointState != null && !textMessagingDeliveryPointState.Shared && textMessagingDeliveryPointState.MachineToPersonMessagingEnabled)
				{
					return BoxedConstants.True;
				}
			}
			return BoxedConstants.False;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00061778 File Offset: 0x0005F978
		internal static QueryFilter IsMachineToPersonTextMessagingEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			QueryFilter queryFilter;
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				queryFilter = new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, 268435456UL);
				break;
			case ComparisonOperator.NotEqual:
				queryFilter = new NotFilter(new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, 268435456UL));
				break;
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			return new AndFilter(new QueryFilter[]
			{
				new NotFilter(new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, (ulong)int.MinValue)),
				new NotFilter(new BitMaskAndFilter(ADRecipientSchema.TextMessagingState, 1073741824UL)),
				queryFilter
			});
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00061865 File Offset: 0x0005FA65
		internal static bool IsDefaultTranslation(string translation)
		{
			return translation.StartsWith("default:", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00061878 File Offset: 0x0005FA78
		internal static QueryFilter RecipientTypeDetailsFilterBuilder(SinglePropertyFilter filter)
		{
			RecipientTypeDetails recipientTypeDetails;
			if (filter is TextFilter)
			{
				recipientTypeDetails = RecipientFilterHelper.RecipientTypeDetailsValueFromTextFilter(filter as TextFilter);
			}
			else
			{
				recipientTypeDetails = (RecipientTypeDetails)ADObject.PropertyValueFromEqualityFilter(filter);
			}
			QueryFilter recipientTypeDetailsFilterOptimization = Filters.GetRecipientTypeDetailsFilterOptimization(recipientTypeDetails);
			if (recipientTypeDetailsFilterOptimization != null)
			{
				ExTraceGlobals.LdapFilterBuilderTracer.TraceDebug<QueryFilter, SinglePropertyFilter>(0L, "ADRecipient.RecipientTypeDetailsFilterBuilder:  RecipientTypeDetailsFilterBuilder found an optimized filter for RecipientTypeDetails. Will use {0} instead of {1}", recipientTypeDetailsFilterOptimization, filter);
				return recipientTypeDetailsFilterOptimization;
			}
			List<QueryFilter> list = new List<QueryFilter>(16);
			for (long num = 1L; num != 0L; num <<= 1)
			{
				RecipientTypeDetails recipientTypeDetails2 = (RecipientTypeDetails)num;
				if ((recipientTypeDetails2 & RecipientTypeDetails.AllUniqueRecipientTypes) != RecipientTypeDetails.None && (recipientTypeDetails2 & recipientTypeDetails) != RecipientTypeDetails.None)
				{
					if ((recipientTypeDetails & (RecipientTypeDetails)(-17592186044416L)) != RecipientTypeDetails.None)
					{
						throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValue(ADRecipientSchema.RecipientTypeDetails.Name, recipientTypeDetails));
					}
					recipientTypeDetailsFilterOptimization = Filters.GetRecipientTypeDetailsFilterOptimization(recipientTypeDetails2);
					if (recipientTypeDetailsFilterOptimization == null)
					{
						throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValue(ADRecipientSchema.RecipientTypeDetails.Name, recipientTypeDetails));
					}
					list.Add(recipientTypeDetailsFilterOptimization);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0006196F File Offset: 0x0005FB6F
		internal static QueryFilter LitigationHoldFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADUserSchema.ElcMailboxFlags, 8UL));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00061983 File Offset: 0x0005FB83
		internal static QueryFilter SingleItemRecoveryFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADUserSchema.ElcMailboxFlags, 16UL));
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00061998 File Offset: 0x0005FB98
		internal static QueryFilter RetentionPolicySetFilterBuilder()
		{
			return new BitMaskAndFilter(IADMailStorageSchema.ElcMailboxFlags, 2UL);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000619A8 File Offset: 0x0005FBA8
		internal static QueryFilter RetentionPolicyFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return new AndFilter(new QueryFilter[]
				{
					ADRecipient.RetentionPolicySetFilterBuilder(),
					new ExistsFilter(IADMailStorageSchema.ElcPolicyTemplate)
				});
			}
			ObjectId propertyValue = (ObjectId)ADObject.PropertyValueFromComparisonFilter(filter, new List<ComparisonOperator>
			{
				ComparisonOperator.Equal,
				ComparisonOperator.NotEqual
			});
			return new AndFilter(new QueryFilter[]
			{
				ADRecipient.RetentionPolicySetFilterBuilder(),
				new ComparisonFilter(ComparisonOperator.Equal, IADMailStorageSchema.ElcPolicyTemplate, propertyValue)
			});
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00061A28 File Offset: 0x0005FC28
		internal static object ShouldUseDefaultRetentionPolicyGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			return BoxedConstants.GetBool((elcMailboxFlags & ElcMailboxFlags.ShouldUseDefaultRetentionPolicy) == ElcMailboxFlags.ShouldUseDefaultRetentionPolicy);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00061A5C File Offset: 0x0005FC5C
		internal static void ShouldUseDefaultRetentionPolicySetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)(value ?? false);
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			if (flag)
			{
				propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags | ElcMailboxFlags.ShouldUseDefaultRetentionPolicy);
				return;
			}
			propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & ~ElcMailboxFlags.ShouldUseDefaultRetentionPolicy);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00061ABD File Offset: 0x0005FCBD
		internal static void ArchiveDatabaseSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[IADMailStorageSchema.ArchiveDatabaseRaw] = value;
			propertyBag[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)propertyBag[IADMailStorageSchema.ElcMailboxFlags] | ElcMailboxFlags.ValidArchiveDatabase);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00061AF0 File Offset: 0x0005FCF0
		internal static object ArchiveDatabaseGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[IADMailStorageSchema.ElcMailboxFlags];
			if (propertyBag[IADMailStorageSchema.ArchiveGuid] == null || (Guid)propertyBag[IADMailStorageSchema.ArchiveGuid] == Guid.Empty)
			{
				return null;
			}
			RecipientType recipientType = (RecipientType)propertyBag[ADRecipientSchema.RecipientType];
			if ((RecipientType.UserMailbox != recipientType && RecipientType.MailUser != recipientType) || (elcMailboxFlags & ElcMailboxFlags.ValidArchiveDatabase) != ElcMailboxFlags.None)
			{
				return (ADObjectId)propertyBag[IADMailStorageSchema.ArchiveDatabaseRaw];
			}
			if (propertyBag[IADMailStorageSchema.ArchiveDomain] != null)
			{
				return null;
			}
			return (ADObjectId)propertyBag[ADMailboxRecipientSchema.Database];
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00061B88 File Offset: 0x0005FD88
		internal static object MultiMailboxLocationsGetter(IPropertyBag propertyBag)
		{
			return new MailboxLocationCollection(propertyBag);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00061BA0 File Offset: 0x0005FDA0
		internal static void MultiMailboxLocationsSetter(object value, IPropertyBag propertyBag)
		{
			IMailboxLocationCollection mailboxLocationCollection = (IMailboxLocationCollection)value;
			MultiValuedProperty<ADObjectIdWithString> multiValuedProperty = new MultiValuedProperty<ADObjectIdWithString>();
			MultiValuedProperty<Guid> multiValuedProperty2 = new MultiValuedProperty<Guid>();
			IMailboxLocationInfo mailboxLocationInfo2;
			IMailboxLocationInfo mailboxLocationInfo = mailboxLocationInfo2 = null;
			if (mailboxLocationCollection != null)
			{
				mailboxLocationInfo2 = mailboxLocationCollection.GetMailboxLocation(MailboxLocationType.Primary);
				mailboxLocationInfo = mailboxLocationCollection.GetMailboxLocation(MailboxLocationType.MainArchive);
				foreach (IMailboxLocationInfo mailboxLocationInfo3 in mailboxLocationCollection.GetMailboxLocations())
				{
					if ((mailboxLocationInfo2 == null || !mailboxLocationInfo3.MailboxGuid.Equals(mailboxLocationInfo2.MailboxGuid)) && (mailboxLocationInfo == null || !mailboxLocationInfo3.MailboxGuid.Equals(mailboxLocationInfo.MailboxGuid)))
					{
						multiValuedProperty.Add(new ADObjectIdWithString(mailboxLocationInfo3.ToString(), new ADObjectId()));
						multiValuedProperty2.Add(mailboxLocationInfo3.MailboxGuid);
					}
				}
			}
			if (mailboxLocationInfo2 != null)
			{
				propertyBag[IADMailStorageSchema.ExchangeGuid] = mailboxLocationInfo2.MailboxGuid;
				propertyBag[IADMailStorageSchema.Database] = mailboxLocationInfo2.DatabaseLocation;
			}
			else
			{
				propertyBag[IADMailStorageSchema.ExchangeGuid] = null;
				propertyBag[IADMailStorageSchema.Database] = null;
			}
			if (mailboxLocationInfo != null)
			{
				propertyBag[IADMailStorageSchema.ArchiveGuid] = mailboxLocationInfo.MailboxGuid;
				ADRecipient.ArchiveDatabaseSetter(mailboxLocationInfo.DatabaseLocation, propertyBag);
			}
			else
			{
				propertyBag[IADMailStorageSchema.ArchiveGuid] = null;
				ADRecipient.ArchiveDatabaseSetter(null, propertyBag);
			}
			propertyBag[IADMailStorageSchema.MailboxLocationsRaw] = multiValuedProperty;
			propertyBag[IADMailStorageSchema.MailboxGuidsRaw] = multiValuedProperty2;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00061D10 File Offset: 0x0005FF10
		internal static QueryFilter LocalArchiveFilter()
		{
			return new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(IADMailStorageSchema.ArchiveGuid),
				new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADUserSchema.ArchiveDomain)),
					new OrFilter(new QueryFilter[]
					{
						new ExistsFilter(IADMailStorageSchema.Database),
						new ExistsFilter(IADMailStorageSchema.ArchiveDatabaseRaw)
					})
				})
			});
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00061D84 File Offset: 0x0005FF84
		internal static QueryFilter ArchiveDatabaseFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return ADRecipient.LocalArchiveFilter();
			}
			ObjectId propertyValue = (ObjectId)ADObject.PropertyValueFromComparisonFilter(filter, new List<ComparisonOperator>
			{
				ComparisonOperator.Equal
			});
			QueryFilter queryFilter = new BitMaskAndFilter(IADMailStorageSchema.ElcMailboxFlags, 32UL);
			return new AndFilter(new QueryFilter[]
			{
				new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(IADMailStorageSchema.ArchiveGuid),
					new NotFilter(new ExistsFilter(IADMailStorageSchema.ArchiveDomain))
				}),
				new OrFilter(new QueryFilter[]
				{
					new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, IADMailStorageSchema.ArchiveDatabaseRaw, propertyValue)
					}),
					new AndFilter(new QueryFilter[]
					{
						new NotFilter(queryFilter),
						new ComparisonFilter(ComparisonOperator.Equal, IADMailStorageSchema.Database, propertyValue)
					})
				})
			});
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00061E70 File Offset: 0x00060070
		internal static QueryFilter ArchiveStateFilterBuilder(SinglePropertyFilter filter)
		{
			ArchiveState archiveState = (ArchiveState)ADObject.PropertyValueFromComparisonFilter(filter, new List<ComparisonOperator>
			{
				ComparisonOperator.Equal
			});
			QueryFilter result = null;
			ExistsFilter existsFilter = new ExistsFilter(ADUserSchema.ArchiveGuid);
			if (archiveState == ArchiveState.None)
			{
				result = new NotFilter(existsFilter);
			}
			else
			{
				ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveStatus, ArchiveStatusFlags.Active);
				ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveStatus, ArchiveStatusFlags.None);
				QueryFilter queryFilter = ADRecipient.LocalArchiveFilter();
				ComparisonFilter comparisonFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.RemoteRecipientType, RemoteRecipientType.None);
				switch (archiveState)
				{
				case ArchiveState.Local:
					result = QueryFilter.AndTogether(new QueryFilter[]
					{
						existsFilter,
						queryFilter
					});
					break;
				case ArchiveState.HostedProvisioned:
					result = QueryFilter.AndTogether(new QueryFilter[]
					{
						existsFilter,
						new NotFilter(queryFilter),
						new NotFilter(comparisonFilter3),
						comparisonFilter
					});
					break;
				case ArchiveState.HostedPending:
					result = QueryFilter.AndTogether(new QueryFilter[]
					{
						existsFilter,
						new NotFilter(queryFilter),
						new NotFilter(comparisonFilter3),
						comparisonFilter2
					});
					break;
				case ArchiveState.OnPremise:
					result = QueryFilter.AndTogether(new QueryFilter[]
					{
						existsFilter,
						new NotFilter(queryFilter),
						comparisonFilter3
					});
					break;
				}
			}
			return result;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00061FC4 File Offset: 0x000601C4
		internal static QueryFilter ManagedFolderFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return new AndFilter(new QueryFilter[]
				{
					new NotFilter(ADRecipient.RetentionPolicySetFilterBuilder()),
					new ExistsFilter(IADMailStorageSchema.ElcPolicyTemplate)
				});
			}
			ObjectId propertyValue = (ObjectId)ADObject.PropertyValueFromComparisonFilter(filter, new List<ComparisonOperator>
			{
				ComparisonOperator.Equal,
				ComparisonOperator.NotEqual
			});
			return new AndFilter(new QueryFilter[]
			{
				new NotFilter(ADRecipient.RetentionPolicySetFilterBuilder()),
				new ComparisonFilter(ComparisonOperator.Equal, IADMailStorageSchema.ElcPolicyTemplate, propertyValue)
			});
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0006204C File Offset: 0x0006024C
		internal static object PersistedMailboxProvisioningConstraintGetter(IPropertyBag propertyBag)
		{
			UserConfigXML userConfigXML = (UserConfigXML)propertyBag[ADRecipientSchema.ConfigurationXML];
			if (userConfigXML == null)
			{
				return null;
			}
			MailboxProvisioningConstraint mailboxProvisioningConstraint = (userConfigXML.MailboxProvisioningConstraints == null) ? null : userConfigXML.MailboxProvisioningConstraints.HardConstraint;
			if (mailboxProvisioningConstraint != null)
			{
				ADRecipient.ValidateMailboxProvisioningConstraint(mailboxProvisioningConstraint);
			}
			return mailboxProvisioningConstraint;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00062090 File Offset: 0x00060290
		internal static object MailboxProvisioningConstraintGetter(IPropertyBag propertyBag)
		{
			MailboxProvisioningConstraint mailboxProvisioningConstraint = (MailboxProvisioningConstraint)ADRecipient.PersistedMailboxProvisioningConstraintGetter(propertyBag);
			string text = AppSettings.Current.DedicatedMailboxPlansCustomAttributeName ?? ((IAppSettings)AutoLoadAppSettings.Instance).DedicatedMailboxPlansCustomAttributeName;
			if ((mailboxProvisioningConstraint == null || mailboxProvisioningConstraint.IsEmpty) && text != null && ADRecipient.IsLegacyRegCodeSupportEnabled())
			{
				string customAttribute = ADRecipient.GetCustomAttribute(propertyBag, text);
				if (!string.IsNullOrEmpty(customAttribute))
				{
					string text2;
					ADRecipient.TryParseMailboxProvisioningData(customAttribute, out text2, out mailboxProvisioningConstraint);
				}
			}
			return mailboxProvisioningConstraint;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000620F4 File Offset: 0x000602F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsLegacyRegCodeSupportEnabled()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.LegacyRegCodeSupport.Enabled;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00062118 File Offset: 0x00060318
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool IsMailboxLocationsEnabled(ADUser user)
		{
			if (user == null)
			{
				return false;
			}
			MultiValuedProperty<ADObjectIdWithString> multiValuedProperty = user[IADMailStorageSchema.MailboxLocationsRaw] as MultiValuedProperty<ADObjectIdWithString>;
			return multiValuedProperty != null && multiValuedProperty.Count > 0;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0006214C File Offset: 0x0006034C
		internal static void MailboxProvisioningConstraintSetter(object value, IPropertyBag propertyBag)
		{
			UserConfigXML userConfigXML = (UserConfigXML)propertyBag[ADRecipientSchema.ConfigurationXML];
			if (userConfigXML == null)
			{
				userConfigXML = new UserConfigXML();
			}
			MailboxProvisioningConstraints mailboxProvisioningConstraints = userConfigXML.MailboxProvisioningConstraints;
			MailboxProvisioningConstraint hardConstraint = (value == null) ? null : ((MailboxProvisioningConstraint)value);
			if (mailboxProvisioningConstraints == null)
			{
				mailboxProvisioningConstraints = new MailboxProvisioningConstraints(hardConstraint, new MailboxProvisioningConstraint[0]);
				userConfigXML.MailboxProvisioningConstraints = mailboxProvisioningConstraints;
			}
			else
			{
				mailboxProvisioningConstraints.HardConstraint = hardConstraint;
			}
			propertyBag[ADRecipientSchema.ConfigurationXML] = userConfigXML;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000621B4 File Offset: 0x000603B4
		internal static object MailboxProvisioningPreferencesGetter(IPropertyBag propertyBag)
		{
			UserConfigXML userConfigXML = (UserConfigXML)propertyBag[ADRecipientSchema.ConfigurationXML];
			if (userConfigXML == null)
			{
				return null;
			}
			MailboxProvisioningConstraints mailboxProvisioningConstraints = userConfigXML.MailboxProvisioningConstraints;
			if (userConfigXML.MailboxProvisioningConstraints != null)
			{
				foreach (OrderedMailboxProvisioningConstraint constraint in mailboxProvisioningConstraints.SoftConstraints)
				{
					ADRecipient.ValidateMailboxProvisioningConstraint(constraint);
				}
				return new MultiValuedProperty<MailboxProvisioningConstraint>(mailboxProvisioningConstraints.SoftConstraints);
			}
			return null;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00062218 File Offset: 0x00060418
		internal static void ValidateMailboxProvisioningConstraint(MailboxProvisioningConstraint constraint)
		{
			InvalidMailboxProvisioningConstraintException ex;
			if (!constraint.TryValidate(out ex))
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(ADRecipientSchema.MailboxProvisioningPreferences.Name, ex.Message), ADRecipientSchema.MailboxProvisioningPreferences, constraint.Value), ex);
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0006225C File Offset: 0x0006045C
		internal static void MailboxProvisioningPreferencesSetter(object value, IPropertyBag propertyBag)
		{
			UserConfigXML userConfigXML = (UserConfigXML)propertyBag[ADRecipientSchema.ConfigurationXML];
			if (userConfigXML == null)
			{
				userConfigXML = new UserConfigXML();
			}
			MailboxProvisioningConstraints mailboxProvisioningConstraints = userConfigXML.MailboxProvisioningConstraints;
			MailboxProvisioningConstraint[] softConstraints = (value == null) ? null : ((MultiValuedProperty<MailboxProvisioningConstraint>)value).ToArray();
			if (mailboxProvisioningConstraints == null)
			{
				mailboxProvisioningConstraints = new MailboxProvisioningConstraints(null, softConstraints);
			}
			else
			{
				mailboxProvisioningConstraints = new MailboxProvisioningConstraints(mailboxProvisioningConstraints.HardConstraint, softConstraints);
			}
			userConfigXML.MailboxProvisioningConstraints = mailboxProvisioningConstraints;
			propertyBag[ADRecipientSchema.ConfigurationXML] = userConfigXML;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000622CC File Offset: 0x000604CC
		internal static object UsageLocationGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADRecipientSchema.InternalUsageLocation];
			CountryInfo result = null;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					result = CountryInfo.Parse(text);
				}
				catch (InvalidCountryOrRegionException ex)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("UsageLocation", ex.Message), ADRecipientSchema.UsageLocation, propertyBag[ADRecipientSchema.InternalUsageLocation]), ex);
				}
			}
			return result;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0006233C File Offset: 0x0006053C
		internal static void UsageLocationSetter(object value, IPropertyBag propertyBag)
		{
			CountryInfo countryInfo = value as CountryInfo;
			if (countryInfo != null)
			{
				propertyBag[ADRecipientSchema.InternalUsageLocation] = countryInfo.Name;
				return;
			}
			propertyBag[ADRecipientSchema.InternalUsageLocation] = null;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00062378 File Offset: 0x00060578
		internal static QueryFilter UsageLocationFilterBuilder(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			CountryInfo countryInfo = (CountryInfo)comparisonFilter.PropertyValue;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, ADRecipientSchema.InternalUsageLocation, countryInfo.Name);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000623D8 File Offset: 0x000605D8
		internal static object MessageFormatGetter(IPropertyBag propertyBag)
		{
			int num = (int)(propertyBag[ADRecipientSchema.InternetEncoding] ?? 0);
			return (MessageFormat)(num & 262144);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0006240C File Offset: 0x0006060C
		internal static void MessageFormatSetter(object value, IPropertyBag propertyBag)
		{
			int num = (int)(propertyBag[ADRecipientSchema.InternetEncoding] ?? 0);
			MessageFormat messageFormat = (MessageFormat)value;
			num &= -262145;
			propertyBag[ADRecipientSchema.InternetEncoding] = (num | (int)messageFormat);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00062458 File Offset: 0x00060658
		internal static object MessageBodyFormatGetter(IPropertyBag propertyBag)
		{
			int num = (int)(propertyBag[ADRecipientSchema.InternetEncoding] ?? 0);
			num &= 1572864;
			if (num == 1572864)
			{
				return MessageBodyFormat.TextAndHtml;
			}
			return (MessageBodyFormat)num;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000624A4 File Offset: 0x000606A4
		internal static void MessageBodyFormatSetter(object value, IPropertyBag propertyBag)
		{
			int num = (int)(propertyBag[ADRecipientSchema.InternetEncoding] ?? 0);
			MessageBodyFormat messageBodyFormat = (MessageBodyFormat)value;
			num &= -1572865;
			propertyBag[ADRecipientSchema.InternetEncoding] = (num | (int)messageBodyFormat);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x000624F0 File Offset: 0x000606F0
		internal static object MacAttachmentFormatGetter(IPropertyBag propertyBag)
		{
			int num = (int)(propertyBag[ADRecipientSchema.InternetEncoding] ?? 0);
			return (MacAttachmentFormat)(num & 6291456);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00062524 File Offset: 0x00060724
		internal static void MacAttachmentFormatSetter(object value, IPropertyBag propertyBag)
		{
			int num = (int)(propertyBag[ADRecipientSchema.InternetEncoding] ?? 0);
			MacAttachmentFormat macAttachmentFormat = (MacAttachmentFormat)value;
			num &= -6291457;
			propertyBag[ADRecipientSchema.InternetEncoding] = (num | (int)macAttachmentFormat);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00062570 File Offset: 0x00060770
		private static object ResourceGetter(IPropertyBag propertyBag, string prefix)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ResourceMetaData];
			string result = string.Empty;
			foreach (string text in multiValuedProperty)
			{
				if (CultureInfo.InvariantCulture.CompareInfo.IsPrefix(text, prefix + ':', CompareOptions.IgnoreCase))
				{
					result = text.Substring(prefix.Length + 1);
					break;
				}
			}
			return result;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00062600 File Offset: 0x00060800
		internal static QueryFilter ResourceCustomFilterBuilder(SinglePropertyFilter filter)
		{
			string text = (string)ADObject.PropertyValueFromEqualityFilter(filter);
			if (!string.IsNullOrEmpty(text))
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ResourceSearchProperties, text);
			}
			return new NotFilter(new ExistsFilter(ADRecipientSchema.ResourceSearchProperties));
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00062640 File Offset: 0x00060840
		internal static object ResourceCustomGetter(IPropertyBag propertyBag)
		{
			string item = (string)ADRecipient.ResourceGetter(propertyBag, "ResourceType");
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ResourceSearchProperties];
			MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>(false, null, multiValuedProperty);
			if (multiValuedProperty2.Contains(item))
			{
				multiValuedProperty2.Remove(item);
			}
			return new MultiValuedProperty<string>(multiValuedProperty.IsReadOnly, ADRecipientSchema.ResourceCustom, multiValuedProperty2);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0006269C File Offset: 0x0006089C
		internal static void ResourceCustomSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			string text = (string)ADRecipient.ResourceGetter(propertyBag, "ResourceType");
			if (text.Length != 0)
			{
				multiValuedProperty.Add(text);
			}
			if (value != null)
			{
				MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)value;
				foreach (string item in multiValuedProperty2)
				{
					if (!multiValuedProperty.Contains(item))
					{
						multiValuedProperty.Add(item);
					}
				}
			}
			propertyBag[ADRecipientSchema.ResourceSearchProperties] = multiValuedProperty;
			if (multiValuedProperty.Count == 0)
			{
				propertyBag[ADRecipientSchema.ResourcePropertiesDisplay] = string.Empty;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(multiValuedProperty[0]);
			for (int i = 1; i < multiValuedProperty.Count; i++)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(multiValuedProperty[i]);
			}
			propertyBag[ADRecipientSchema.ResourcePropertiesDisplay] = stringBuilder.ToString();
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0006279C File Offset: 0x0006099C
		internal static object ResourceTypeGetter(IPropertyBag propertyBag)
		{
			string value = (string)ADRecipient.ResourceGetter(propertyBag, "ResourceType");
			ExchangeResourceType? exchangeResourceType = null;
			ExchangeResourceType value2;
			if (Enum.TryParse<ExchangeResourceType>(value, true, out value2))
			{
				exchangeResourceType = new ExchangeResourceType?(value2);
			}
			return exchangeResourceType;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000627DC File Offset: 0x000609DC
		internal static void ResourceTypeSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ResourceSearchProperties];
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ResourceMetaData];
			if ((ExchangeResourceType?)value != (ExchangeResourceType?)ADRecipient.ResourceTypeGetter(propertyBag))
			{
				multiValuedProperty2.Clear();
				multiValuedProperty.Clear();
				propertyBag[ADRecipientSchema.ResourcePropertiesDisplay] = string.Empty;
				if (value != null)
				{
					multiValuedProperty2.Add("ResourceType" + ':' + value.ToString());
					multiValuedProperty.Add(value.ToString());
					propertyBag[ADRecipientSchema.ResourcePropertiesDisplay] = value.ToString();
				}
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000628A0 File Offset: 0x00060AA0
		internal static QueryFilter ResourceTypeFilterBuilder(SinglePropertyFilter filter)
		{
			ExchangeResourceType? exchangeResourceType = (ExchangeResourceType?)ADObject.PropertyValueFromEqualityFilter(filter);
			if (exchangeResourceType == null)
			{
				return new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADRecipientSchema.ResourceMetaData)),
					new NotFilter(new ExistsFilter(ADRecipientSchema.ResourceSearchProperties))
				});
			}
			return new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ResourceMetaData, "ResourceType" + ':' + exchangeResourceType.Value.ToString()),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ResourceSearchProperties, exchangeResourceType.Value.ToString())
			});
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00062950 File Offset: 0x00060B50
		private static bool IsResourcePropertiesNotNull(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ResourceMetaData];
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ResourceSearchProperties];
			string value = (string)propertyBag[ADRecipientSchema.ResourcePropertiesDisplay];
			int? num = (int?)propertyBag[ADRecipientSchema.ResourceCapacity];
			return multiValuedProperty.Count > 0 || multiValuedProperty2.Count > 0 || num != null || !string.IsNullOrEmpty(value);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x000629C8 File Offset: 0x00060BC8
		internal static object IsResourceGetter(IPropertyBag propertyBag)
		{
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)propertyBag[ADRecipientSchema.MasterAccountSid];
			if (null != securityIdentifier && securityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
			{
				return BoxedConstants.GetBool(ADRecipient.IsResourcePropertiesNotNull(propertyBag));
			}
			if (!(bool)ADRecipient.IsLinkedGetter(propertyBag))
			{
				return BoxedConstants.False;
			}
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)ADRecipient.RecipientTypeDetailsGetter(propertyBag);
			if (recipientTypeDetails == RecipientTypeDetails.LinkedRoomMailbox)
			{
				return BoxedConstants.GetBool(ADRecipient.IsResourcePropertiesNotNull(propertyBag));
			}
			return BoxedConstants.False;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00062A44 File Offset: 0x00060C44
		internal static object IsSharedGetter(IPropertyBag propertyBag)
		{
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)propertyBag[ADRecipientSchema.MasterAccountSid];
			if (null != securityIdentifier && securityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
			{
				return BoxedConstants.GetBool(!ADRecipient.IsResourcePropertiesNotNull(propertyBag));
			}
			return BoxedConstants.False;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00062A8C File Offset: 0x00060C8C
		internal static object IsLinkedGetter(IPropertyBag propertyBag)
		{
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)propertyBag[ADRecipientSchema.MasterAccountSid];
			bool value = null != securityIdentifier && !securityIdentifier.IsWellKnown(WellKnownSidType.SelfSid) && !securityIdentifier.IsWellKnown(WellKnownSidType.NullSid);
			return BoxedConstants.GetBool(value);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00062AD4 File Offset: 0x00060CD4
		internal static object RecipientPersonTypeGetter(IPropertyBag propertyBag)
		{
			switch ((RecipientType)ADRecipient.RecipientTypeGetter(propertyBag))
			{
			case RecipientType.Invalid:
			{
				object obj = propertyBag[NspiOnlyProperties.DisplayType];
				if (obj != null)
				{
					switch ((LegacyRecipientDisplayType)obj)
					{
					case LegacyRecipientDisplayType.MailUser:
					case LegacyRecipientDisplayType.RemoteMailUser:
					{
						object obj2 = propertyBag[NspiOnlyProperties.DisplayTypeEx];
						if (obj2 != null)
						{
							RecipientDisplayType recipientDisplayType = (RecipientDisplayType)obj2;
							RecipientDisplayType recipientDisplayType2 = recipientDisplayType;
							if (recipientDisplayType2 == Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.SyncedConferenceRoomMailbox || recipientDisplayType2 == Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.ConferenceRoomMailbox)
							{
								return PersonType.Room;
							}
							if (recipientDisplayType2 != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.GroupMailboxUser)
							{
								return PersonType.Person;
							}
							return PersonType.ModernGroup;
						}
						break;
					}
					case LegacyRecipientDisplayType.DistributionList:
					case LegacyRecipientDisplayType.DynamicDistributionList:
					case LegacyRecipientDisplayType.PersonalDistributionList:
						return PersonType.DistributionList;
					}
				}
				return PersonType.Unknown;
			}
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
			case RecipientType.Contact:
			case RecipientType.MailContact:
			{
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)ADRecipient.RecipientTypeDetailsGetter(propertyBag);
				RecipientTypeDetails recipientTypeDetails2 = recipientTypeDetails;
				if (recipientTypeDetails2 == RecipientTypeDetails.RoomMailbox || recipientTypeDetails2 == RecipientTypeDetails.RemoteRoomMailbox)
				{
					return PersonType.Room;
				}
				if (recipientTypeDetails2 != RecipientTypeDetails.GroupMailbox)
				{
					return PersonType.Person;
				}
				return PersonType.ModernGroup;
			}
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				return PersonType.DistributionList;
			default:
				return PersonType.Unknown;
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00062C10 File Offset: 0x00060E10
		private static QueryFilter GetResourceFilter()
		{
			return new OrFilter(new QueryFilter[]
			{
				new ExistsFilter(ADRecipientSchema.ResourceMetaData),
				new ExistsFilter(ADRecipientSchema.ResourceSearchProperties),
				new ExistsFilter(ADRecipientSchema.ResourcePropertiesDisplay),
				new ExistsFilter(ADRecipientSchema.ResourceCapacity)
			});
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00062C60 File Offset: 0x00060E60
		internal static QueryFilter IsLinkedFilterBuilder(SinglePropertyFilter filter)
		{
			bool flag = (bool)ADObject.PropertyValueFromEqualityFilter(filter);
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(ADRecipientSchema.MasterAccountSid),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.MasterAccountSid, new SecurityIdentifier(WellKnownSidType.SelfSid, null)),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.MasterAccountSid, new SecurityIdentifier(WellKnownSidType.NullSid, null))
			});
			if (!flag)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00062CCC File Offset: 0x00060ECC
		internal static QueryFilter IsResourceFilterBuilder(SinglePropertyFilter filter)
		{
			bool flag = (bool)ADObject.PropertyValueFromEqualityFilter(filter);
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				ADRecipient.GetResourceFilter(),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MasterAccountSid, new SecurityIdentifier(WellKnownSidType.SelfSid, null))
			});
			if (!flag)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00062D1C File Offset: 0x00060F1C
		internal static QueryFilter IsSharedFilterBuilder(SinglePropertyFilter filter)
		{
			bool flag = (bool)ADObject.PropertyValueFromEqualityFilter(filter);
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				new NotFilter(ADRecipient.GetResourceFilter()),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MasterAccountSid, new SecurityIdentifier(WellKnownSidType.SelfSid, null))
			});
			if (!flag)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00062D74 File Offset: 0x00060F74
		internal static QueryFilter IsExcludedFromBacksyncFilterBuilder(SinglePropertyFilter filter)
		{
			return new ComparisonFilter(((bool)ADObject.PropertyValueFromEqualityFilter(filter)) ? ComparisonOperator.Equal : ComparisonOperator.NotEqual, ADRecipientSchema.RawCapabilities, Capability.ExcludedFromBackSync);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00062DA8 File Offset: 0x00060FA8
		internal static void ForwardingSmtpAddressSetter(object value, IPropertyBag propertyBag)
		{
			SmtpProxyAddress smtpProxyAddress = value as SmtpProxyAddress;
			if (value == null || smtpProxyAddress != null)
			{
				propertyBag[ADRecipientSchema.GenericForwardingAddress] = smtpProxyAddress;
				return;
			}
			throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ForwardingSmtpAddressNotValidSmtpAddress(value), ADRecipientSchema.ForwardingSmtpAddress, value));
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00062DEB File Offset: 0x00060FEB
		internal static void OnPremisesObjectIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADRecipientSchema.RawOnPremisesObjectId] = ADRecipient.ConvertOnPremisesObjectIdToBytes((string)value);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00062E03 File Offset: 0x00061003
		internal static object OnPremisesObjectIdGetter(IPropertyBag propertyBag)
		{
			return ADRecipient.ConvertOnPremisesObjectIdToString(propertyBag[ADRecipientSchema.RawOnPremisesObjectId]);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00062E18 File Offset: 0x00061018
		internal static QueryFilter OnPremisesObjectIdFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(ADRecipientSchema.RawOnPremisesObjectId);
			}
			string value = (string)ADObject.PropertyValueFromEqualityFilter(filter);
			byte[] propertyValue = ADRecipient.ConvertOnPremisesObjectIdToBytes(value);
			return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RawOnPremisesObjectId, propertyValue);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00062E58 File Offset: 0x00061058
		private static byte[] ConvertOnPremisesObjectIdToBytes(string value)
		{
			byte[] result = null;
			if (!string.IsNullOrEmpty(value))
			{
				result = Encoding.UTF8.GetBytes(value);
			}
			return result;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00062E7C File Offset: 0x0006107C
		internal static string ConvertOnPremisesObjectIdToString(object value)
		{
			string result = string.Empty;
			byte[] array = value as byte[];
			if (array != null && array.Length > 0)
			{
				result = Encoding.UTF8.GetString(array);
			}
			return result;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00062EAC File Offset: 0x000610AC
		internal static void ExternalEmailAddressSetter(object value, IPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			ProxyAddress proxyAddress = (ProxyAddress)propertyBag[ADRecipientSchema.RawExternalEmailAddress];
			try
			{
				ProxyAddress proxyAddress2 = (ProxyAddress)value;
				if (null != proxyAddress2)
				{
					MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass];
					if (!multiValuedProperty.Contains(ADPublicFolder.MostDerivedClass))
					{
						proxyAddress2 = (ProxyAddress)proxyAddress2.ToPrimary();
						value = proxyAddress2;
					}
				}
				if (proxyAddress != proxyAddress2 && null != proxyAddress && proxyAddressCollection.Contains(proxyAddress) && (null == proxyAddress2 || !proxyAddressCollection.Contains(proxyAddress2)))
				{
					int index = proxyAddressCollection.IndexOf(proxyAddress);
					proxyAddress = proxyAddressCollection[index];
					if (proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == ProxyAddressPrefix.Smtp && null != proxyAddress2 && proxyAddress2.Prefix != ProxyAddressPrefix.Smtp)
					{
						throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ErrorRemovePrimaryExternalSMTPAddress, ADRecipientSchema.ExternalEmailAddress, value));
					}
					if (null != proxyAddress2)
					{
						proxyAddressCollection[index] = proxyAddress2;
					}
					else
					{
						proxyAddressCollection.RemoveAt(index);
					}
				}
				propertyBag[ADRecipientSchema.RawExternalEmailAddress] = value;
				if (value != null && proxyAddressCollection.FindPrimary(ProxyAddressPrefix.Smtp) == (ProxyAddress)value)
				{
					propertyBag[ADObjectSchema.OriginalPrimarySmtpAddress] = new SmtpAddress((value as ProxyAddress).ValueString);
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.FailedToUpdateEmailAddressesForExternal((value == null) ? "$null" : value.ToString(), ADRecipientSchema.EmailAddresses.Name, ex.Message), ADRecipientSchema.ExternalEmailAddress, value), ex);
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00063068 File Offset: 0x00061268
		internal static QueryFilter UMEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADUserSchema.UMEnabledFlags, 1UL));
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0006307C File Offset: 0x0006127C
		internal static QueryFilter UMProvisioningFlagFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 2UL));
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00063090 File Offset: 0x00061290
		internal static QueryFilter UCSImListMigrationCompletedFlagFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 256UL));
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00063214 File Offset: 0x00061414
		internal static CustomFilterBuilderDelegate ProtocolEnabledFilterBuilder(string protocol)
		{
			return delegate(SinglePropertyFilter filter)
			{
				if (!(filter is ComparisonFilter))
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
				}
				if (string.IsNullOrEmpty(protocol))
				{
					throw new ArgumentNullException("protocol");
				}
				ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
				if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
				}
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)comparisonFilter.Property;
				string str = ((bool)adpropertyDefinition.DefaultValue) ? "§0" : "§1";
				QueryFilter queryFilter = new TextFilter(ADRecipientSchema.ProtocolSettings, protocol + str, MatchOptions.Prefix, MatchFlags.IgnoreCase);
				if (protocol.Equals("OWA"))
				{
					QueryFilter queryFilter2 = new TextFilter(ADRecipientSchema.ProtocolSettings, "HTTP" + str, MatchOptions.Prefix, MatchFlags.IgnoreCase);
					queryFilter = new OrFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					});
				}
				if ((comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && (bool)comparisonFilter.PropertyValue == (bool)adpropertyDefinition.DefaultValue) || (ComparisonOperator.NotEqual == comparisonFilter.ComparisonOperator && (bool)comparisonFilter.PropertyValue != (bool)adpropertyDefinition.DefaultValue))
				{
					return new NotFilter(queryFilter);
				}
				return queryFilter;
			};
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006323C File Offset: 0x0006143C
		internal static object SendModerationNotificationsGetter(IPropertyBag propertyBag)
		{
			int moderationFlags = (int)propertyBag[ADRecipientSchema.ModerationFlags];
			return ADRecipient.GetSendModerationNotificationsFromModerationFlags(moderationFlags);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00063265 File Offset: 0x00061465
		internal static TransportModerationNotificationFlags GetSendModerationNotificationsFromModerationFlags(int moderationFlags)
		{
			if ((moderationFlags & 4) != 0 && (moderationFlags & 2) != 0)
			{
				return TransportModerationNotificationFlags.Always;
			}
			if ((moderationFlags & 2) != 0)
			{
				return TransportModerationNotificationFlags.Internal;
			}
			return TransportModerationNotificationFlags.Never;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006327C File Offset: 0x0006147C
		internal static void SendModerationNotificationsSetter(object value, IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ADRecipientSchema.ModerationFlags];
			TransportModerationNotificationFlags transportModerationNotificationFlags = (TransportModerationNotificationFlags)value;
			if (transportModerationNotificationFlags == TransportModerationNotificationFlags.Always)
			{
				num |= 2;
				num |= 4;
			}
			else if (transportModerationNotificationFlags == TransportModerationNotificationFlags.Internal)
			{
				num |= 2;
				num &= -5;
			}
			else
			{
				num &= -7;
			}
			propertyBag[ADRecipientSchema.ModerationFlags] = num;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000632F8 File Offset: 0x000614F8
		internal static bool TryGetFromProxyAddress(ProxyAddress proxyAddress, IRecipientSession session, out ADRecipient recipient)
		{
			ADRecipient temporaryRecipient = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				temporaryRecipient = session.FindByProxyAddress(proxyAddress);
			});
			recipient = temporaryRecipient;
			return adoperationResult.Succeeded;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x000633A8 File Offset: 0x000615A8
		internal static ADOperationResult TryGetFromCrossTenantObjectId(CrossTenantObjectId externalDirectoryObjectId, out ADRecipient recipient)
		{
			ADRecipient temporaryRecipient = null;
			ADOperationResult result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryObjectId.ExternalDirectoryOrganizationId);
				IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 3118, "TryGetFromCrossTenantObjectId", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipient.cs");
				temporaryRecipient = recipientSession.FindADUserByExternalDirectoryObjectId(externalDirectoryObjectId.ExternalDirectoryObjectId.ToString());
			});
			recipient = temporaryRecipient;
			return result;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000633E4 File Offset: 0x000615E4
		public CrossTenantObjectId GetCrossTenantObjectId()
		{
			string externalDirectoryObjectId = this.ExternalDirectoryObjectId;
			if (string.IsNullOrEmpty(externalDirectoryObjectId))
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.CannotConstructCrossTenantObjectId("ExternalDirectoryObjectId"));
			}
			if (base.OrganizationId == null)
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.CannotConstructCrossTenantObjectId("OrganizationId"));
			}
			string text = base.OrganizationId.ToExternalDirectoryOrganizationId();
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.CannotConstructCrossTenantObjectId("OrganizationId.ToExternalDirectoryOrganizationId()"));
			}
			Guid externalDirectoryOrganizationId = string.IsNullOrEmpty(text) ? Guid.Empty : Guid.Parse(text);
			Guid externalDirectoryObjectId2 = Guid.Parse(externalDirectoryObjectId);
			return CrossTenantObjectId.FromExternalDirectoryIds(externalDirectoryOrganizationId, externalDirectoryObjectId2);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00063476 File Offset: 0x00061676
		public override string ToString()
		{
			if (this.DisplayName != null)
			{
				return this.DisplayName;
			}
			if (base.Id != null)
			{
				return base.Id.ToString();
			}
			return base.ToString();
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000634A1 File Offset: 0x000616A1
		internal override void Initialize()
		{
			base.Initialize();
			if (!this.propertyBag.IsReadOnly)
			{
				this.OriginalPrimarySmtpAddress = this.PrimarySmtpAddress;
				this.OriginalWindowsEmailAddress = this.WindowsEmailAddress;
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x000634CE File Offset: 0x000616CE
		public bool IsMemberOf(ADObjectId groupId, bool directOnly)
		{
			return ADRecipient.IsMemberOf(base.Id, groupId, directOnly, this.Session);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000634E3 File Offset: 0x000616E3
		internal IThrottlingPolicy ReadThrottlingPolicy()
		{
			if (this.ThrottlingPolicy != null)
			{
				return ThrottlingPolicyCache.Singleton.Get(base.OrganizationId, this.ThrottlingPolicy);
			}
			return this.ReadDefaultThrottlingPolicy();
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0006350A File Offset: 0x0006170A
		internal IThrottlingPolicy ReadDefaultThrottlingPolicy()
		{
			return ThrottlingPolicyCache.Singleton.Get(base.OrganizationId);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0006351C File Offset: 0x0006171C
		internal IRecipientSession Session
		{
			get
			{
				return (IRecipientSession)this.m_Session;
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0006352C File Offset: 0x0006172C
		internal static QueryFilter RecipientTypeFilterBuilder(SinglePropertyFilter filter)
		{
			RecipientType recipientType = (RecipientType)ADObject.PropertyValueFromEqualityFilter(filter);
			int num = (int)recipientType;
			if (num <= 0 || num >= Filters.RecipientTypeCount)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValue(ADRecipientSchema.RecipientType.Name, recipientType));
			}
			return Filters.RecipientTypeFilters[num];
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00063577 File Offset: 0x00061777
		public string UMExtension
		{
			get
			{
				return UMMailbox.GetPrimaryExtension(this.EmailAddresses, ProxyAddressPrefix.UM);
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0006358C File Offset: 0x0006178C
		internal void ClearEUMProxy(bool skipAirSyncEUMAddresses, UMDialPlan dialPlan)
		{
			Hashtable safeTable = null;
			if (skipAirSyncEUMAddresses)
			{
				safeTable = UMMailbox.GetAirSyncSafeTable(this.UMAddresses, ProxyAddressPrefix.ASUM, dialPlan);
			}
			UMMailbox.ClearProxy(this, this.EmailAddresses, ProxyAddressPrefix.UM, safeTable);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000635C2 File Offset: 0x000617C2
		internal void ClearASUMProxy()
		{
			UMMailbox.ClearProxy(this, this.UMAddresses, ProxyAddressPrefix.ASUM, null);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000635D8 File Offset: 0x000617D8
		internal void AddEUMProxyAddress(MultiValuedProperty<string> extensions, UMDialPlan dialPlan)
		{
			foreach (string extension in extensions)
			{
				UMMailbox.AddProxy(this, this.EmailAddresses, extension, dialPlan, ProxyAddressPrefix.UM);
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00063634 File Offset: 0x00061834
		internal void AddEUMProxyAddress(string phoneNumber, UMDialPlan dialPlan)
		{
			UMMailbox.AddProxy(this, this.EmailAddresses, phoneNumber, dialPlan, ProxyAddressPrefix.UM);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00063649 File Offset: 0x00061849
		internal void AddASUMProxyAddress(string phoneNumber, UMDialPlan dialPlan)
		{
			UMMailbox.AddProxy(this, this.UMAddresses, phoneNumber, dialPlan, ProxyAddressPrefix.ASUM);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0006365E File Offset: 0x0006185E
		internal bool PhoneNumberExistsInUMAddresses(string phoneNumber)
		{
			return UMMailbox.PhoneNumberExists(this.UMAddresses, ProxyAddressPrefix.ASUM, phoneNumber);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00063671 File Offset: 0x00061871
		internal bool IsAirSyncNumberQuotaReached()
		{
			return UMMailbox.ProxyAddressCount(this.UMAddresses, ProxyAddressPrefix.ASUM) >= 3;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0006368C File Offset: 0x0006188C
		internal void SetUMDtmfMapIfNecessary()
		{
			if (base.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				return;
			}
			string value = this[ADOrgPersonSchema.Phone] as string;
			if ((Array.IndexOf<RecipientType>(ADRecipient.DtmfMapAllowedRecipientTypes, this.RecipientType) != -1 || !string.IsNullOrEmpty(value)) && (base.IsModified(ADUserSchema.UMEnabled) || base.IsModified(ADOrgPersonSchema.LastName) || base.IsModified(ADOrgPersonSchema.FirstName) || base.IsModified(ADRecipientSchema.DisplayName) || base.IsModified(ADRecipientSchema.PrimarySmtpAddress) || base.IsModified(ADOrgPersonSchema.SanitizedPhoneNumbers)))
			{
				this.PopulateDtmfMap(true);
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00063730 File Offset: 0x00061930
		internal void SetValidArchiveDatabase()
		{
			if (base.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				return;
			}
			if (RecipientType.UserMailbox != this.RecipientType && RecipientType.MailUser != this.RecipientType)
			{
				return;
			}
			if (base.IsModified(IADMailStorageSchema.Database) || base.IsModified(IADMailStorageSchema.ArchiveDatabaseRaw))
			{
				bool flag = false;
				if (this[IADMailStorageSchema.ArchiveDatabaseRaw] != null && !this[IADMailStorageSchema.ArchiveDatabaseRaw].Equals(this[IADMailStorageSchema.Database]))
				{
					flag = true;
				}
				if (flag)
				{
					this[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)this[IADMailStorageSchema.ElcMailboxFlags] | ElcMailboxFlags.ValidArchiveDatabase);
					return;
				}
				this[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)this[IADMailStorageSchema.ElcMailboxFlags] & ~ElcMailboxFlags.ValidArchiveDatabase);
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000637F8 File Offset: 0x000619F8
		internal void ValidateArchiveProperties(bool isDatacenter, List<ValidationError> errors)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = this[IADMailStorageSchema.ArchiveGuid] != null && (Guid)this[IADMailStorageSchema.ArchiveGuid] != Guid.Empty;
			if (base.IsModified(IADMailStorageSchema.ArchiveGuid))
			{
				if (flag4)
				{
					flag = !this.ValidateArchiveLocationNoConflict(errors);
				}
				else
				{
					flag2 = !this.ValidateArchiveDatabaseNotSet(errors);
					flag3 = !this.ValidateArchiveDomainNotSet(errors);
				}
			}
			if (base.IsModified(IADMailStorageSchema.ArchiveDomain))
			{
				if (isDatacenter && this[IADMailStorageSchema.ArchiveDomain] != null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorArchiveDomainInvalidInDatacenter, IADMailStorageSchema.ArchiveDomain, this[IADMailStorageSchema.ArchiveDomain]));
				}
				if (flag4)
				{
					if (!flag)
					{
						flag = !this.ValidateArchiveLocationNoConflict(errors);
					}
				}
				else if (!flag3)
				{
					this.ValidateArchiveDomainNotSet(errors);
				}
			}
			if (base.IsModified(IADMailStorageSchema.ArchiveDatabaseRaw))
			{
				if (flag4)
				{
					if (!flag)
					{
						this.ValidateArchiveLocationNoConflict(errors);
						return;
					}
				}
				else if (!flag2)
				{
					this.ValidateArchiveDatabaseNotSet(errors);
				}
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000638EB File Offset: 0x00061AEB
		private bool ValidateArchiveLocationNoConflict(List<ValidationError> errors)
		{
			if (this[IADMailStorageSchema.ArchiveDatabaseRaw] != null && this[IADMailStorageSchema.ArchiveDomain] != null)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorArchiveDatabaseArchiveDomainConflict, this.Identity, string.Empty));
				return false;
			}
			return true;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00063925 File Offset: 0x00061B25
		private bool ValidateArchiveDatabaseNotSet(List<ValidationError> errors)
		{
			if (this[IADMailStorageSchema.ArchiveDatabaseRaw] != null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorArchiveDatabaseSetForNonArchive, IADMailStorageSchema.ArchiveDatabaseRaw, this[IADMailStorageSchema.ArchiveDatabaseRaw]));
				return false;
			}
			return true;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00063957 File Offset: 0x00061B57
		private bool ValidateArchiveDomainNotSet(List<ValidationError> errors)
		{
			if (this[IADMailStorageSchema.ArchiveDomain] != null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorArchiveDomainSetForNonArchive, IADMailStorageSchema.ArchiveDomain, this[IADMailStorageSchema.ArchiveDomain]));
				return false;
			}
			return true;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0006398C File Offset: 0x00061B8C
		private static bool IsValidRecipientTypeForModerator(RecipientType recipientType)
		{
			foreach (RecipientType recipientType2 in ADRecipient.AllowedModeratorsRecipientTypes)
			{
				if (recipientType == recipientType2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000639BC File Offset: 0x00061BBC
		internal string GetAlternateMailboxLegDN(Guid guid)
		{
			return ADRecipient.CreateAlternateMailboxLegDN(this.LegacyExchangeDN, guid);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000639CC File Offset: 0x00061BCC
		internal static string CreateAlternateMailboxLegDN(string parentLegacyDNString, Guid mailboxGuid)
		{
			LegacyDN parentLegacyDN = LegacyDN.Parse(parentLegacyDNString);
			LegacyDN legacyDN = new LegacyDN(parentLegacyDN, "guid", mailboxGuid.ToString("D"));
			return legacyDN.ToString();
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000639FE File Offset: 0x00061BFE
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x00063A10 File Offset: 0x00061C10
		public MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.AcceptMessagesOnlyFrom];
			}
			set
			{
				this[ADRecipientSchema.AcceptMessagesOnlyFrom] = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00063A1E File Offset: 0x00061C1E
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00063A30 File Offset: 0x00061C30
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.ThrottlingPolicy];
			}
			set
			{
				this[ADRecipientSchema.ThrottlingPolicy] = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00063A3E File Offset: 0x00061C3E
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00063A50 File Offset: 0x00061C50
		public MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
			set
			{
				this[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers] = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00063A5E File Offset: 0x00061C5E
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x00063A70 File Offset: 0x00061C70
		public MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers];
			}
			internal set
			{
				this[ADRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers] = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00063A7E File Offset: 0x00061C7E
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00063A90 File Offset: 0x00061C90
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.AddressBookPolicy];
			}
			set
			{
				this[ADRecipientSchema.AddressBookPolicy] = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00063A9E File Offset: 0x00061C9E
		public ADObjectId GlobalAddressListFromAddressBookPolicy
		{
			get
			{
				if (this.AddressBookPolicy != null && this.globalAddressListFromAddressBookPolicy == null)
				{
					this.PopulateGlobalAddressListFromAddressBookPolicy();
				}
				return this.globalAddressListFromAddressBookPolicy;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00063ABC File Offset: 0x00061CBC
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x00063ACE File Offset: 0x00061CCE
		public MultiValuedProperty<ADObjectId> AddressListMembership
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.AddressListMembership];
			}
			set
			{
				this[ADRecipientSchema.AddressListMembership] = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00063ADC File Offset: 0x00061CDC
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x00063AEE File Offset: 0x00061CEE
		public MultiValuedProperty<string> AggregationSubscriptionCredential
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.AggregationSubscriptionCredential];
			}
			internal set
			{
				this[ADRecipientSchema.AggregationSubscriptionCredential] = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00063AFC File Offset: 0x00061CFC
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x00063B18 File Offset: 0x00061D18
		public int RecipientSoftDeletedStatus
		{
			get
			{
				return (int)(this[ADRecipientSchema.RecipientSoftDeletedStatus] ?? 0);
			}
			internal set
			{
				this[ADRecipientSchema.RecipientSoftDeletedStatus] = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00063B2B File Offset: 0x00061D2B
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x00063B3D File Offset: 0x00061D3D
		public bool IsSoftDeletedByRemove
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsSoftDeletedByRemove];
			}
			set
			{
				this[ADRecipientSchema.IsSoftDeletedByRemove] = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00063B50 File Offset: 0x00061D50
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x00063B62 File Offset: 0x00061D62
		public bool IsSoftDeletedByDisable
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsSoftDeletedByDisable];
			}
			set
			{
				this[ADRecipientSchema.IsSoftDeletedByDisable] = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00063B75 File Offset: 0x00061D75
		public bool IsSoftDeleted
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsSoftDeletedByDisable] || (bool)this[ADRecipientSchema.IsSoftDeletedByRemove];
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00063B9B File Offset: 0x00061D9B
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00063BAD File Offset: 0x00061DAD
		public bool IsInactiveMailbox
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsInactiveMailbox];
			}
			set
			{
				this[ADRecipientSchema.IsInactiveMailbox] = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00063BC0 File Offset: 0x00061DC0
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x00063BD2 File Offset: 0x00061DD2
		public DateTime? WhenSoftDeleted
		{
			get
			{
				return (DateTime?)this[ADRecipientSchema.WhenSoftDeleted];
			}
			internal set
			{
				this[ADRecipientSchema.WhenSoftDeleted] = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00063BE5 File Offset: 0x00061DE5
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00063BF7 File Offset: 0x00061DF7
		public bool IncludeInGarbageCollection
		{
			get
			{
				return (bool)this[ADRecipientSchema.IncludeInGarbageCollection];
			}
			internal set
			{
				this[ADRecipientSchema.IncludeInGarbageCollection] = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00063C0A File Offset: 0x00061E0A
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00063C1C File Offset: 0x00061E1C
		public string Alias
		{
			get
			{
				return (string)this[ADRecipientSchema.Alias];
			}
			set
			{
				this[ADRecipientSchema.Alias] = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00063C2A File Offset: 0x00061E2A
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00063C3C File Offset: 0x00061E3C
		public bool AntispamBypassEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.AntispamBypassEnabled];
			}
			set
			{
				this[ADRecipientSchema.AntispamBypassEnabled] = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00063C4F File Offset: 0x00061E4F
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x00063C61 File Offset: 0x00061E61
		public string AssistantName
		{
			get
			{
				return (string)this[ADRecipientSchema.AssistantName];
			}
			set
			{
				this[ADRecipientSchema.AssistantName] = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00063C6F File Offset: 0x00061E6F
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x00063C81 File Offset: 0x00061E81
		public MultiValuedProperty<ADObjectId> BypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.BypassModerationFrom];
			}
			set
			{
				this[ADRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00063C8F File Offset: 0x00061E8F
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x00063CA1 File Offset: 0x00061EA1
		public MultiValuedProperty<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				this[ADRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00063CAF File Offset: 0x00061EAF
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x00063CC1 File Offset: 0x00061EC1
		public MultiValuedProperty<ADObjectId> BypassModerationFromSendersOrMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.BypassModerationFromSendersOrMembers];
			}
			internal set
			{
				this[ADRecipientSchema.BypassModerationFromSendersOrMembers] = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00063CCF File Offset: 0x00061ECF
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x00063CE1 File Offset: 0x00061EE1
		public MultiValuedProperty<byte[]> Certificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ADRecipientSchema.Certificate];
			}
			set
			{
				this[ADRecipientSchema.Certificate] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00063CEF File Offset: 0x00061EEF
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x00063D01 File Offset: 0x00061F01
		public string WebPage
		{
			get
			{
				return (string)this[ADRecipientSchema.WebPage];
			}
			internal set
			{
				this[ADRecipientSchema.WebPage] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00063D0F File Offset: 0x00061F0F
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x00063D21 File Offset: 0x00061F21
		public string Notes
		{
			get
			{
				return (string)this[ADRecipientSchema.Notes];
			}
			set
			{
				this[ADRecipientSchema.Notes] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00063D2F File Offset: 0x00061F2F
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x00063D41 File Offset: 0x00061F41
		public string CustomAttribute1
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute1];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute1] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00063D4F File Offset: 0x00061F4F
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x00063D61 File Offset: 0x00061F61
		public string CustomAttribute10
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute10];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute10] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00063D6F File Offset: 0x00061F6F
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x00063D81 File Offset: 0x00061F81
		public string CustomAttribute11
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute11];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute11] = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00063D8F File Offset: 0x00061F8F
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x00063DA1 File Offset: 0x00061FA1
		public string CustomAttribute12
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute12];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute12] = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00063DAF File Offset: 0x00061FAF
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x00063DC1 File Offset: 0x00061FC1
		public string CustomAttribute13
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute13];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute13] = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00063DCF File Offset: 0x00061FCF
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x00063DE1 File Offset: 0x00061FE1
		public string CustomAttribute14
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute14];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute14] = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00063DEF File Offset: 0x00061FEF
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x00063E01 File Offset: 0x00062001
		public string CustomAttribute15
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute15];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute15] = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00063E0F File Offset: 0x0006200F
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x00063E21 File Offset: 0x00062021
		public string CustomAttribute2
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute2];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute2] = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00063E2F File Offset: 0x0006202F
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x00063E41 File Offset: 0x00062041
		public string CustomAttribute3
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute3];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute3] = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00063E4F File Offset: 0x0006204F
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x00063E61 File Offset: 0x00062061
		public string CustomAttribute4
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute4];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute4] = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00063E6F File Offset: 0x0006206F
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x00063E81 File Offset: 0x00062081
		public string CustomAttribute5
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute5];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute5] = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00063E8F File Offset: 0x0006208F
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x00063EA1 File Offset: 0x000620A1
		public string CustomAttribute6
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute6];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute6] = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00063EAF File Offset: 0x000620AF
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x00063EC1 File Offset: 0x000620C1
		public string CustomAttribute7
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute7];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute7] = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00063ECF File Offset: 0x000620CF
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x00063EE1 File Offset: 0x000620E1
		public string CustomAttribute8
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute8];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute8] = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00063EEF File Offset: 0x000620EF
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x00063F01 File Offset: 0x00062101
		public string CustomAttribute9
		{
			get
			{
				return (string)this[ADRecipientSchema.CustomAttribute9];
			}
			set
			{
				this[ADRecipientSchema.CustomAttribute9] = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00063F0F File Offset: 0x0006210F
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00063F21 File Offset: 0x00062121
		public MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ExtensionCustomAttribute1];
			}
			set
			{
				this[ADRecipientSchema.ExtensionCustomAttribute1] = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00063F2F File Offset: 0x0006212F
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x00063F41 File Offset: 0x00062141
		public MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ExtensionCustomAttribute2];
			}
			set
			{
				this[ADRecipientSchema.ExtensionCustomAttribute2] = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00063F4F File Offset: 0x0006214F
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x00063F61 File Offset: 0x00062161
		public MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ExtensionCustomAttribute3];
			}
			set
			{
				this[ADRecipientSchema.ExtensionCustomAttribute3] = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00063F6F File Offset: 0x0006216F
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x00063F81 File Offset: 0x00062181
		public MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ExtensionCustomAttribute4];
			}
			set
			{
				this[ADRecipientSchema.ExtensionCustomAttribute4] = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00063F8F File Offset: 0x0006218F
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x00063FA1 File Offset: 0x000621A1
		public MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ExtensionCustomAttribute5];
			}
			set
			{
				this[ADRecipientSchema.ExtensionCustomAttribute5] = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00063FAF File Offset: 0x000621AF
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x00063FC1 File Offset: 0x000621C1
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[ADRecipientSchema.EmailAddresses];
			}
			set
			{
				this[ADRecipientSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00063FCF File Offset: 0x000621CF
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00063FE1 File Offset: 0x000621E1
		public bool EmailAddressPolicyEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.EmailAddressPolicyEnabled];
			}
			internal set
			{
				this[ADRecipientSchema.EmailAddressPolicyEnabled] = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00063FF4 File Offset: 0x000621F4
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x00064006 File Offset: 0x00062206
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[ADRecipientSchema.ExternalDirectoryObjectId];
			}
			internal set
			{
				this[ADRecipientSchema.ExternalDirectoryObjectId] = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00064014 File Offset: 0x00062214
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x00064026 File Offset: 0x00062226
		public string DisplayName
		{
			get
			{
				return (string)this[ADRecipientSchema.DisplayName];
			}
			set
			{
				this[ADRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00064034 File Offset: 0x00062234
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x00064046 File Offset: 0x00062246
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsDirSynced];
			}
			set
			{
				this[ADRecipientSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00064059 File Offset: 0x00062259
		internal bool IsDirSyncEnabled
		{
			get
			{
				return ADObject.IsRecipientDirSynced(this.IsDirSynced);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00064066 File Offset: 0x00062266
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x00064078 File Offset: 0x00062278
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[ADRecipientSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00064086 File Offset: 0x00062286
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x00064098 File Offset: 0x00062298
		internal ProxyAddress RawExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[ADRecipientSchema.RawExternalEmailAddress];
			}
			set
			{
				this[ADRecipientSchema.RawExternalEmailAddress] = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x000640A6 File Offset: 0x000622A6
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x000640B8 File Offset: 0x000622B8
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[ADRecipientSchema.ExternalEmailAddress];
			}
			set
			{
				this[ADRecipientSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000640C6 File Offset: 0x000622C6
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x000640D8 File Offset: 0x000622D8
		public bool IsCalculatedTargetAddress
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsCalculatedTargetAddress];
			}
			set
			{
				this[ADRecipientSchema.IsCalculatedTargetAddress] = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000640EB File Offset: 0x000622EB
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x000640FD File Offset: 0x000622FD
		public ADObjectId ForwardingAddress
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.ForwardingAddress];
			}
			set
			{
				this[ADRecipientSchema.ForwardingAddress] = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0006410B File Offset: 0x0006230B
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0006411D File Offset: 0x0006231D
		public ProxyAddress ForwardingSmtpAddress
		{
			get
			{
				return (ProxyAddress)this[ADRecipientSchema.ForwardingSmtpAddress];
			}
			set
			{
				this[ADRecipientSchema.ForwardingSmtpAddress] = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0006412B File Offset: 0x0006232B
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0006413D File Offset: 0x0006233D
		public MultiValuedProperty<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				this[ADRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0006414B File Offset: 0x0006234B
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0006415D File Offset: 0x0006235D
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.HiddenFromAddressListsEnabled];
			}
			set
			{
				this[ADRecipientSchema.HiddenFromAddressListsEnabled] = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00064170 File Offset: 0x00062370
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00064182 File Offset: 0x00062382
		public bool UsePreferMessageFormat
		{
			get
			{
				return (bool)this[ADRecipientSchema.UsePreferMessageFormat];
			}
			set
			{
				this[ADRecipientSchema.UsePreferMessageFormat] = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00064195 File Offset: 0x00062395
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x000641A7 File Offset: 0x000623A7
		public MessageFormat MessageFormat
		{
			get
			{
				return (MessageFormat)this[ADRecipientSchema.MessageFormat];
			}
			set
			{
				this[ADRecipientSchema.MessageFormat] = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000641BA File Offset: 0x000623BA
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x000641CC File Offset: 0x000623CC
		public MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return (MessageBodyFormat)this[ADRecipientSchema.MessageBodyFormat];
			}
			set
			{
				this[ADRecipientSchema.MessageBodyFormat] = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000641DF File Offset: 0x000623DF
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x000641F1 File Offset: 0x000623F1
		public MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return (MacAttachmentFormat)this[ADRecipientSchema.MacAttachmentFormat];
			}
			set
			{
				this[ADRecipientSchema.MacAttachmentFormat] = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00064204 File Offset: 0x00062404
		public bool IsResource
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsResource];
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00064216 File Offset: 0x00062416
		public bool IsLinked
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsLinked];
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00064228 File Offset: 0x00062428
		public bool IsShared
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsShared];
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0006423A File Offset: 0x0006243A
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x0006424C File Offset: 0x0006244C
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[ADRecipientSchema.LegacyExchangeDN];
			}
			set
			{
				this[ADRecipientSchema.LegacyExchangeDN] = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0006425A File Offset: 0x0006245A
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0006426C File Offset: 0x0006246C
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADRecipientSchema.MaxReceiveSize];
			}
			set
			{
				this[ADRecipientSchema.MaxReceiveSize] = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0006427F File Offset: 0x0006247F
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x00064291 File Offset: 0x00062491
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADRecipientSchema.MaxSendSize];
			}
			set
			{
				this[ADRecipientSchema.MaxSendSize] = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000642A4 File Offset: 0x000624A4
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x000642B6 File Offset: 0x000624B6
		internal MessageHygieneFlags MessageHygieneFlags
		{
			get
			{
				return (MessageHygieneFlags)this[ADRecipientSchema.MessageHygieneFlags];
			}
			set
			{
				this[ADRecipientSchema.MessageHygieneFlags] = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000642C9 File Offset: 0x000624C9
		public string OU
		{
			get
			{
				return (string)this[ADRecipientSchema.OrganizationalUnit];
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000642DB File Offset: 0x000624DB
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x000642ED File Offset: 0x000624ED
		public bool MAPIEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.MAPIEnabled];
			}
			set
			{
				this[ADRecipientSchema.MAPIEnabled] = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00064300 File Offset: 0x00062500
		public bool? MapiHttpEnabled
		{
			get
			{
				return (bool?)this[ADRecipientSchema.MapiHttpEnabled];
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00064312 File Offset: 0x00062512
		public bool MAPIBlockOutlookRpcHttp
		{
			get
			{
				return (bool)this[ADRecipientSchema.MAPIBlockOutlookRpcHttp];
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00064324 File Offset: 0x00062524
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x00064336 File Offset: 0x00062536
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[ADRecipientSchema.OnPremisesObjectId];
			}
			set
			{
				this[ADRecipientSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00064344 File Offset: 0x00062544
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x00064356 File Offset: 0x00062556
		public bool OWAEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.OWAEnabled];
			}
			set
			{
				this[ADRecipientSchema.OWAEnabled] = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00064369 File Offset: 0x00062569
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x0006437B File Offset: 0x0006257B
		public bool MOWAEnabled
		{
			get
			{
				return (bool)this[ADUserSchema.OWAforDevicesEnabled];
			}
			set
			{
				this[ADUserSchema.OWAforDevicesEnabled] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0006438E File Offset: 0x0006258E
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x000643A0 File Offset: 0x000625A0
		public string PhoneticCompany
		{
			get
			{
				return (string)this[ADRecipientSchema.PhoneticCompany];
			}
			set
			{
				this[ADRecipientSchema.PhoneticCompany] = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x000643AE File Offset: 0x000625AE
		public bool? EwsEnabled
		{
			get
			{
				return CASMailboxHelper.ToBooleanNullable((int?)this[CASMailboxSchema.EwsEnabled]);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x000643C5 File Offset: 0x000625C5
		public bool? EwsAllowOutlook
		{
			get
			{
				return (bool?)this[ADRecipientSchema.EwsAllowOutlook];
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x000643D7 File Offset: 0x000625D7
		public bool? EwsAllowMacOutlook
		{
			get
			{
				return (bool?)this[ADRecipientSchema.EwsAllowMacOutlook];
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x000643E9 File Offset: 0x000625E9
		public bool? EwsAllowEntourage
		{
			get
			{
				return (bool?)this[ADRecipientSchema.EwsAllowEntourage];
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x000643FB File Offset: 0x000625FB
		public EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
		{
			get
			{
				return (EwsApplicationAccessPolicy?)this[ADRecipientSchema.EwsApplicationAccessPolicy];
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0006440D File Offset: 0x0006260D
		public MultiValuedProperty<string> EwsExceptions
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.EwsExceptions];
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0006441F File Offset: 0x0006261F
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x00064431 File Offset: 0x00062631
		public string PhoneticDepartment
		{
			get
			{
				return (string)this[ADRecipientSchema.PhoneticDepartment];
			}
			set
			{
				this[ADRecipientSchema.PhoneticDepartment] = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0006443F File Offset: 0x0006263F
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x00064451 File Offset: 0x00062651
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[ADRecipientSchema.PhoneticDisplayName];
			}
			set
			{
				this[ADRecipientSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0006445F File Offset: 0x0006265F
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x00064471 File Offset: 0x00062671
		public string PhoneticFirstName
		{
			get
			{
				return (string)this[ADRecipientSchema.PhoneticFirstName];
			}
			set
			{
				this[ADRecipientSchema.PhoneticFirstName] = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0006447F File Offset: 0x0006267F
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x00064491 File Offset: 0x00062691
		public string PhoneticLastName
		{
			get
			{
				return (string)this[ADRecipientSchema.PhoneticLastName];
			}
			set
			{
				this[ADRecipientSchema.PhoneticLastName] = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0006449F File Offset: 0x0006269F
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x000644B1 File Offset: 0x000626B1
		public MultiValuedProperty<string> PoliciesIncluded
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.PoliciesIncluded];
			}
			set
			{
				this[ADRecipientSchema.PoliciesIncluded] = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x000644BF File Offset: 0x000626BF
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x000644D1 File Offset: 0x000626D1
		public MultiValuedProperty<string> PoliciesExcluded
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.PoliciesExcluded];
			}
			set
			{
				this[ADRecipientSchema.PoliciesExcluded] = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x000644DF File Offset: 0x000626DF
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x0006450F File Offset: 0x0006270F
		internal bool InternalOnly
		{
			get
			{
				return (int)(this[ADRecipientSchema.RecipientSoftDeletedStatus] ?? 0) != 0 || (bool)this[ADRecipientSchema.InternalOnly];
			}
			set
			{
				this[ADRecipientSchema.InternalOnly] = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00064522 File Offset: 0x00062722
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x00064534 File Offset: 0x00062734
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.PrimarySmtpAddress];
			}
			set
			{
				this[ADRecipientSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00064547 File Offset: 0x00062747
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00064559 File Offset: 0x00062759
		internal bool AllowArchiveAddressSync
		{
			get
			{
				return (bool)this[ADRecipientSchema.AllowArchiveAddressSync];
			}
			set
			{
				this[ADRecipientSchema.AllowArchiveAddressSync] = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0006456C File Offset: 0x0006276C
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x0006457E File Offset: 0x0006277E
		internal SmtpAddress OriginalPrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[ADObjectSchema.OriginalPrimarySmtpAddress];
			}
			set
			{
				this[ADObjectSchema.OriginalPrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00064591 File Offset: 0x00062791
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x000645A3 File Offset: 0x000627A3
		public MultiValuedProperty<string> ProtocolSettings
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ProtocolSettings];
			}
			set
			{
				this[ADRecipientSchema.ProtocolSettings] = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000645B1 File Offset: 0x000627B1
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x000645C3 File Offset: 0x000627C3
		public string MAPIBlockOutlookVersions
		{
			get
			{
				return (string)this[ADRecipientSchema.MAPIBlockOutlookVersions];
			}
			internal set
			{
				this[ADRecipientSchema.MAPIBlockOutlookVersions] = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000645D1 File Offset: 0x000627D1
		public bool MAPIBlockOutlookExternalConnectivity
		{
			get
			{
				return (bool)this[ADRecipientSchema.MAPIBlockOutlookExternalConnectivity];
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000645E3 File Offset: 0x000627E3
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x000645F5 File Offset: 0x000627F5
		public Unlimited<int> RecipientLimits
		{
			get
			{
				return (Unlimited<int>)this[ADRecipientSchema.RecipientLimits];
			}
			set
			{
				this[ADRecipientSchema.RecipientLimits] = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00064608 File Offset: 0x00062808
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0006461A File Offset: 0x0006281A
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[ADRecipientSchema.RecipientDisplayType];
			}
			internal set
			{
				this[ADRecipientSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0006462D File Offset: 0x0006282D
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0006463F File Offset: 0x0006283F
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00064651 File Offset: 0x00062851
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[ADRecipientSchema.RecipientTypeDetails];
			}
			internal set
			{
				this[ADRecipientSchema.RecipientTypeDetails] = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00064664 File Offset: 0x00062864
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00064676 File Offset: 0x00062876
		public RecipientTypeDetails PreviousRecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[ADRecipientSchema.PreviousRecipientTypeDetails];
			}
			internal set
			{
				this[ADRecipientSchema.PreviousRecipientTypeDetails] = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00064689 File Offset: 0x00062889
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x0006469B File Offset: 0x0006289B
		public ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.DefaultPublicFolderMailbox];
			}
			internal set
			{
				this[ADRecipientSchema.DefaultPublicFolderMailbox] = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x000646A9 File Offset: 0x000628A9
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x000646BB File Offset: 0x000628BB
		public MultiValuedProperty<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.RejectMessagesFrom];
			}
			set
			{
				this[ADRecipientSchema.RejectMessagesFrom] = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000646C9 File Offset: 0x000628C9
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x000646DB File Offset: 0x000628DB
		public MultiValuedProperty<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.RejectMessagesFromDLMembers];
			}
			set
			{
				this[ADRecipientSchema.RejectMessagesFromDLMembers] = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000646E9 File Offset: 0x000628E9
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x000646FB File Offset: 0x000628FB
		public MultiValuedProperty<ADObjectId> RejectMessagesFromSendersOrMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.RejectMessagesFromSendersOrMembers];
			}
			internal set
			{
				this[ADRecipientSchema.RejectMessagesFromSendersOrMembers] = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00064709 File Offset: 0x00062909
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0006471B File Offset: 0x0006291B
		public bool RequireAllSendersAreAuthenticated
		{
			get
			{
				return (bool)this[ADRecipientSchema.RequireAllSendersAreAuthenticated];
			}
			set
			{
				this[ADRecipientSchema.RequireAllSendersAreAuthenticated] = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0006472E File Offset: 0x0006292E
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00064740 File Offset: 0x00062940
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[ADRecipientSchema.MasterAccountSid];
			}
			internal set
			{
				this[ADRecipientSchema.MasterAccountSid] = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0006474E File Offset: 0x0006294E
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00064760 File Offset: 0x00062960
		public string LinkedMasterAccount
		{
			get
			{
				return (string)this[ADRecipientSchema.LinkedMasterAccount];
			}
			internal set
			{
				this.propertyBag.SetField(ADRecipientSchema.LinkedMasterAccount, value);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00064774 File Offset: 0x00062974
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x00064786 File Offset: 0x00062986
		public int? ResourceCapacity
		{
			get
			{
				return (int?)this[ADRecipientSchema.ResourceCapacity];
			}
			set
			{
				this[ADRecipientSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00064799 File Offset: 0x00062999
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x000647AB File Offset: 0x000629AB
		public MultiValuedProperty<string> ResourceCustom
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ResourceCustom];
			}
			set
			{
				this[ADRecipientSchema.ResourceCustom] = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x000647B9 File Offset: 0x000629B9
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x000647CB File Offset: 0x000629CB
		public MultiValuedProperty<string> ResourceMetaData
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ResourceMetaData];
			}
			set
			{
				this[ADRecipientSchema.ResourceMetaData] = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000647D9 File Offset: 0x000629D9
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x000647EB File Offset: 0x000629EB
		public string ResourcePropertiesDisplay
		{
			get
			{
				return (string)this[ADRecipientSchema.ResourcePropertiesDisplay];
			}
			internal set
			{
				this[ADRecipientSchema.ResourcePropertiesDisplay] = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000647F9 File Offset: 0x000629F9
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0006480B File Offset: 0x00062A0B
		public MultiValuedProperty<string> ResourceSearchProperties
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.ResourceSearchProperties];
			}
			internal set
			{
				this[ADRecipientSchema.ResourceSearchProperties] = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00064819 File Offset: 0x00062A19
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x0006482B File Offset: 0x00062A2B
		public ExchangeResourceType? ResourceType
		{
			get
			{
				return (ExchangeResourceType?)this[ADRecipientSchema.ResourceType];
			}
			set
			{
				this[ADRecipientSchema.ResourceType] = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0006483E File Offset: 0x00062A3E
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00064850 File Offset: 0x00062A50
		public int? SCLDeleteThreshold
		{
			get
			{
				return (int?)this[ADRecipientSchema.SCLDeleteThreshold];
			}
			set
			{
				this[ADRecipientSchema.SCLDeleteThreshold] = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00064863 File Offset: 0x00062A63
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00064875 File Offset: 0x00062A75
		public bool? SCLDeleteEnabled
		{
			get
			{
				return (bool?)this[ADRecipientSchema.SCLDeleteEnabled];
			}
			set
			{
				this[ADRecipientSchema.SCLDeleteEnabled] = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00064888 File Offset: 0x00062A88
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x0006489A File Offset: 0x00062A9A
		public int? SCLRejectThreshold
		{
			get
			{
				return (int?)this[ADRecipientSchema.SCLRejectThreshold];
			}
			set
			{
				this[ADRecipientSchema.SCLRejectThreshold] = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x000648AD File Offset: 0x00062AAD
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x000648BF File Offset: 0x00062ABF
		public bool? SCLRejectEnabled
		{
			get
			{
				return (bool?)this[ADRecipientSchema.SCLRejectEnabled];
			}
			set
			{
				this[ADRecipientSchema.SCLRejectEnabled] = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x000648D2 File Offset: 0x00062AD2
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x000648E4 File Offset: 0x00062AE4
		public int? SCLQuarantineThreshold
		{
			get
			{
				return (int?)this[ADRecipientSchema.SCLQuarantineThreshold];
			}
			set
			{
				this[ADRecipientSchema.SCLQuarantineThreshold] = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000648F7 File Offset: 0x00062AF7
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00064909 File Offset: 0x00062B09
		public bool? SCLQuarantineEnabled
		{
			get
			{
				return (bool?)this[ADRecipientSchema.SCLQuarantineEnabled];
			}
			set
			{
				this[ADRecipientSchema.SCLQuarantineEnabled] = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0006491C File Offset: 0x00062B1C
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x0006492E File Offset: 0x00062B2E
		public int? SCLJunkThreshold
		{
			get
			{
				return (int?)this[ADRecipientSchema.SCLJunkThreshold];
			}
			set
			{
				this[ADRecipientSchema.SCLJunkThreshold] = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00064941 File Offset: 0x00062B41
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00064953 File Offset: 0x00062B53
		public bool? SCLJunkEnabled
		{
			get
			{
				return (bool?)this[ADRecipientSchema.SCLJunkEnabled];
			}
			set
			{
				this[ADRecipientSchema.SCLJunkEnabled] = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00064966 File Offset: 0x00062B66
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x00064978 File Offset: 0x00062B78
		public bool ShowGalAsDefaultView
		{
			get
			{
				return Convert.ToBoolean(this[ADRecipientSchema.AddressBookFlags]);
			}
			set
			{
				this[ADRecipientSchema.AddressBookFlags] = Convert.ToInt32(value);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00064990 File Offset: 0x00062B90
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x000649A2 File Offset: 0x00062BA2
		public string SimpleDisplayName
		{
			get
			{
				return (string)this[ADRecipientSchema.SimpleDisplayName];
			}
			set
			{
				this[ADRecipientSchema.SimpleDisplayName] = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x000649B0 File Offset: 0x00062BB0
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x000649C2 File Offset: 0x00062BC2
		public MultiValuedProperty<byte[]> SMimeCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ADRecipientSchema.SMimeCertificate];
			}
			set
			{
				this[ADRecipientSchema.SMimeCertificate] = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x000649D0 File Offset: 0x00062BD0
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x000649E2 File Offset: 0x00062BE2
		public string TextEncodedORAddress
		{
			get
			{
				return (string)this[ADRecipientSchema.TextEncodedORAddress];
			}
			set
			{
				this[ADRecipientSchema.TextEncodedORAddress] = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x000649F0 File Offset: 0x00062BF0
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00064A02 File Offset: 0x00062C02
		public ProxyAddressCollection UMAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[ADRecipientSchema.UMAddresses];
			}
			internal set
			{
				this[ADRecipientSchema.UMAddresses] = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00064A10 File Offset: 0x00062C10
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00064A22 File Offset: 0x00062C22
		public MultiValuedProperty<string> UMDtmfMap
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.UMDtmfMap];
			}
			set
			{
				this[ADRecipientSchema.UMDtmfMap] = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00064A30 File Offset: 0x00062C30
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00064A42 File Offset: 0x00062C42
		public GeoCoordinates GeoCoordinates
		{
			get
			{
				return (GeoCoordinates)this[ADRecipientSchema.GeoCoordinates];
			}
			set
			{
				this[ADRecipientSchema.GeoCoordinates] = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00064A50 File Offset: 0x00062C50
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x00064A62 File Offset: 0x00062C62
		public AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
		{
			get
			{
				return (AllowUMCallsFromNonUsersFlags)this[ADRecipientSchema.AllowUMCallsFromNonUsers];
			}
			set
			{
				this[ADRecipientSchema.AllowUMCallsFromNonUsers] = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00064A75 File Offset: 0x00062C75
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x00064A87 File Offset: 0x00062C87
		public ADObjectId UMRecipientDialPlanId
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.UMRecipientDialPlanId];
			}
			set
			{
				this[ADRecipientSchema.UMRecipientDialPlanId] = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00064A95 File Offset: 0x00062C95
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x00064AA7 File Offset: 0x00062CA7
		public byte[] UMSpokenName
		{
			get
			{
				return (byte[])this[ADRecipientSchema.UMSpokenName];
			}
			set
			{
				this[ADRecipientSchema.UMSpokenName] = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00064AB5 File Offset: 0x00062CB5
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x00064AC7 File Offset: 0x00062CC7
		public UseMapiRichTextFormat UseMapiRichTextFormat
		{
			get
			{
				return (UseMapiRichTextFormat)this[ADRecipientSchema.UseMapiRichTextFormat];
			}
			set
			{
				this[ADRecipientSchema.UseMapiRichTextFormat] = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00064ADA File Offset: 0x00062CDA
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x00064AEC File Offset: 0x00062CEC
		public SmtpAddress WindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.WindowsEmailAddress];
			}
			set
			{
				this[ADRecipientSchema.WindowsEmailAddress] = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00064AFF File Offset: 0x00062CFF
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x00064B11 File Offset: 0x00062D11
		internal SmtpAddress OriginalWindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)this[ADObjectSchema.OriginalWindowsEmailAddress];
			}
			set
			{
				this[ADObjectSchema.OriginalWindowsEmailAddress] = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00064B24 File Offset: 0x00062D24
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x00064B36 File Offset: 0x00062D36
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[ADRecipientSchema.SafeSendersHash];
			}
			internal set
			{
				this[ADRecipientSchema.SafeSendersHash] = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00064B44 File Offset: 0x00062D44
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00064B56 File Offset: 0x00062D56
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[ADRecipientSchema.SafeRecipientsHash];
			}
			internal set
			{
				this[ADRecipientSchema.SafeRecipientsHash] = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00064B64 File Offset: 0x00062D64
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00064B76 File Offset: 0x00062D76
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[ADRecipientSchema.BlockedSendersHash];
			}
			internal set
			{
				this[ADRecipientSchema.BlockedSendersHash] = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00064B84 File Offset: 0x00062D84
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x00064B96 File Offset: 0x00062D96
		public MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.MailTipTranslations];
			}
			set
			{
				this[ADRecipientSchema.MailTipTranslations] = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00064BA4 File Offset: 0x00062DA4
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00064BB6 File Offset: 0x00062DB6
		public string DefaultMailTip
		{
			get
			{
				return (string)this[ADRecipientSchema.DefaultMailTip];
			}
			set
			{
				this[ADRecipientSchema.DefaultMailTip] = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00064BC4 File Offset: 0x00062DC4
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x00064BD6 File Offset: 0x00062DD6
		public bool ModerationEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.ModerationEnabled];
			}
			internal set
			{
				this[ADRecipientSchema.ModerationEnabled] = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00064BE9 File Offset: 0x00062DE9
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x00064BFB File Offset: 0x00062DFB
		public int? HABSeniorityIndex
		{
			get
			{
				return (int?)this[ADRecipientSchema.HABSeniorityIndex];
			}
			internal set
			{
				this[ADRecipientSchema.HABSeniorityIndex] = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00064C0E File Offset: 0x00062E0E
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00064C20 File Offset: 0x00062E20
		public MultiValuedProperty<ADObjectId> ModeratedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.ModeratedBy];
			}
			internal set
			{
				this[ADRecipientSchema.ModeratedBy] = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00064C2E File Offset: 0x00062E2E
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x00064C40 File Offset: 0x00062E40
		public ADObjectId ArbitrationMailbox
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.ArbitrationMailbox];
			}
			internal set
			{
				this[ADRecipientSchema.ArbitrationMailbox] = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00064C4E File Offset: 0x00062E4E
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x00064C60 File Offset: 0x00062E60
		public ADObjectId MailboxPlan
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.MailboxPlan];
			}
			internal set
			{
				this[ADRecipientSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00064C6E File Offset: 0x00062E6E
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x00064C80 File Offset: 0x00062E80
		public ADObjectId RoleAssignmentPolicy
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.RoleAssignmentPolicy];
			}
			internal set
			{
				this[ADRecipientSchema.RoleAssignmentPolicy] = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00064C8E File Offset: 0x00062E8E
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x00064CA0 File Offset: 0x00062EA0
		public bool BypassNestedModerationEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.BypassNestedModerationEnabled];
			}
			internal set
			{
				this[ADRecipientSchema.BypassNestedModerationEnabled] = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00064CB3 File Offset: 0x00062EB3
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x00064CC5 File Offset: 0x00062EC5
		public TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return (TransportModerationNotificationFlags)this[ADRecipientSchema.SendModerationNotifications];
			}
			internal set
			{
				this[ADRecipientSchema.SendModerationNotifications] = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00064CD8 File Offset: 0x00062ED8
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x00064CEA File Offset: 0x00062EEA
		public byte[] ThumbnailPhoto
		{
			get
			{
				return (byte[])this[ADRecipientSchema.ThumbnailPhoto];
			}
			internal set
			{
				this[ADRecipientSchema.ThumbnailPhoto] = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00064CF8 File Offset: 0x00062EF8
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x00064D0A File Offset: 0x00062F0A
		public string ImmutableId
		{
			get
			{
				return (string)this[ADRecipientSchema.ImmutableId];
			}
			set
			{
				this[ADRecipientSchema.ImmutableId] = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x00064D18 File Offset: 0x00062F18
		public string ImmutableIdPartial
		{
			get
			{
				return (string)this[ADRecipientSchema.OnPremisesObjectId];
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00064D2A File Offset: 0x00062F2A
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00064D3C File Offset: 0x00062F3C
		internal bool UMProvisioningRequested
		{
			get
			{
				return (bool)this[ADRecipientSchema.UMProvisioningRequested];
			}
			set
			{
				this[ADRecipientSchema.UMProvisioningRequested] = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00064D4F File Offset: 0x00062F4F
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00064D61 File Offset: 0x00062F61
		internal bool UCSImListMigrationCompleted
		{
			get
			{
				return (bool)this[ADRecipientSchema.UCSImListMigrationCompleted];
			}
			set
			{
				this[ADRecipientSchema.UCSImListMigrationCompleted] = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00064D74 File Offset: 0x00062F74
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x00064D86 File Offset: 0x00062F86
		public string DirSyncId
		{
			get
			{
				return (string)this[ADRecipientSchema.DirSyncId];
			}
			internal set
			{
				this[ADRecipientSchema.DirSyncId] = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00064D94 File Offset: 0x00062F94
		internal MultiValuedProperty<TextMessagingStateBase> TextMessagingState
		{
			get
			{
				return (MultiValuedProperty<TextMessagingStateBase>)this[ADRecipientSchema.TextMessagingState];
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00064DA6 File Offset: 0x00062FA6
		public bool IsPersonToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsPersonToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x00064DB8 File Offset: 0x00062FB8
		public bool IsMachineToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsMachineToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00064DCA File Offset: 0x00062FCA
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00064DDC File Offset: 0x00062FDC
		internal ADUser MailboxPlanObject
		{
			get
			{
				return (ADUser)this[ADRecipientSchema.MailboxPlanObject];
			}
			set
			{
				this[ADRecipientSchema.MailboxPlanObject] = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00064DEA File Offset: 0x00062FEA
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x00064DFC File Offset: 0x00062FFC
		public bool MailboxAuditEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.AuditEnabled];
			}
			set
			{
				this[ADRecipientSchema.AuditEnabled] = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x00064E0F File Offset: 0x0006300F
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x00064E21 File Offset: 0x00063021
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[ADRecipientSchema.AuditLogAgeLimit];
			}
			set
			{
				this[ADRecipientSchema.AuditLogAgeLimit] = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00064E34 File Offset: 0x00063034
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x00064E46 File Offset: 0x00063046
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return (MailboxAuditOperations)this[ADRecipientSchema.AuditAdminFlags];
			}
			set
			{
				this[ADRecipientSchema.AuditAdminFlags] = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00064E59 File Offset: 0x00063059
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x00064E6B File Offset: 0x0006306B
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return (MailboxAuditOperations)this[ADRecipientSchema.AuditDelegateFlags];
			}
			set
			{
				this[ADRecipientSchema.AuditDelegateFlags] = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00064E7E File Offset: 0x0006307E
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00064E90 File Offset: 0x00063090
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return (MailboxAuditOperations)this[ADRecipientSchema.AuditDelegateAdminFlags];
			}
			set
			{
				this[ADRecipientSchema.AuditDelegateAdminFlags] = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00064EA3 File Offset: 0x000630A3
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x00064EB5 File Offset: 0x000630B5
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return (MailboxAuditOperations)this[ADRecipientSchema.AuditOwnerFlags];
			}
			set
			{
				this[ADRecipientSchema.AuditOwnerFlags] = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00064EC8 File Offset: 0x000630C8
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x00064EDA File Offset: 0x000630DA
		public bool BypassAudit
		{
			get
			{
				return (bool)this[ADRecipientSchema.AuditBypassEnabled];
			}
			set
			{
				this[ADRecipientSchema.AuditBypassEnabled] = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00064EED File Offset: 0x000630ED
		public DateTime? AuditLastAdminAccess
		{
			get
			{
				return (DateTime?)this[ADRecipientSchema.AuditLastAdminAccess];
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x00064EFF File Offset: 0x000630FF
		public DateTime? AuditLastDelegateAccess
		{
			get
			{
				return (DateTime?)this[ADRecipientSchema.AuditLastDelegateAccess];
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00064F11 File Offset: 0x00063111
		public DateTime? AuditLastExternalAccess
		{
			get
			{
				return (DateTime?)this[ADRecipientSchema.AuditLastExternalAccess];
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00064F23 File Offset: 0x00063123
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x00064F35 File Offset: 0x00063135
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)this[ADRecipientSchema.UsageLocation];
			}
			set
			{
				this[ADRecipientSchema.UsageLocation] = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00064F43 File Offset: 0x00063143
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00064F55 File Offset: 0x00063155
		public MultiValuedProperty<string> InPlaceHolds
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.InPlaceHolds];
			}
			set
			{
				this[ADRecipientSchema.InPlaceHolds] = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00064F63 File Offset: 0x00063163
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x00064F75 File Offset: 0x00063175
		public SmtpAddress JournalArchiveAddress
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.JournalArchiveAddress];
			}
			set
			{
				this[ADRecipientSchema.JournalArchiveAddress] = value;
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00064F88 File Offset: 0x00063188
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			switch (this.RecipientType)
			{
			case RecipientType.MailUser:
			case RecipientType.MailContact:
				if (this.ExternalEmailAddress == null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyRequired("ExternalEmailAddress", this.RecipientType.ToString()), ADRecipientSchema.ExternalEmailAddress, null));
				}
				break;
			}
			if (this.ExternalEmailAddress != null && this.ExternalEmailAddress.GetType() == typeof(InvalidProxyAddress))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExternalEmailAddressInvalid(((InvalidProxyAddress)this.ExternalEmailAddress).ParseException.Message), ADRecipientSchema.ExternalEmailAddress, this.ExternalEmailAddress));
			}
			if (this.EmailAddresses.Count == 0 && RecipientTypeDetails.ArbitrationMailbox == this.RecipientTypeDetails)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorArbitrationMailboxPropertyEmailAddressesEmpty, ADRecipientSchema.EmailAddresses, null));
			}
			if (base.OrganizationalUnitRoot != null && base.ConfigurationUnit != null)
			{
				string distinguishedName = base.Id.DistinguishedName;
				string distinguishedName2 = base.OrganizationalUnitRoot.DistinguishedName;
				string distinguishedName3 = base.ConfigurationUnit.DistinguishedName;
				bool flag = false;
				if (this.RecipientType == RecipientType.MicrosoftExchange)
				{
					if (!string.IsNullOrEmpty(distinguishedName3) && !distinguishedName.ToLower().EndsWith(distinguishedName3.ToLower()))
					{
						flag = true;
					}
				}
				else if (!string.IsNullOrEmpty(distinguishedName2) && !distinguishedName.ToLower().EndsWith(distinguishedName2.ToLower()))
				{
					flag = true;
				}
				if (flag)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidOrganizationId(distinguishedName, distinguishedName2 ?? "<null>", distinguishedName3 ?? "<null>"), this.Identity, string.Empty));
				}
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00065134 File Offset: 0x00063334
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			RecipientType recipientType = this.RecipientType;
			switch (this.RecipientType)
			{
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
			case RecipientType.MailContact:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				if (!ConsumerIdentityHelper.IsConsumerMailbox(base.Id))
				{
					if (string.IsNullOrEmpty(this.DisplayName))
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.PropertyRequired("DisplayName", recipientType.ToString()), ADRecipientSchema.DisplayName, null));
					}
					else if (VariantConfiguration.InvariantNoFlightingSnapshot.AD.DisplayNameMustContainReadableCharacter.Enabled && !ADRecipient.IsValidName(this.DisplayName))
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.ErrorDisplayNameInvalid, ADRecipientSchema.DisplayName, null));
					}
				}
				break;
			}
			if (string.IsNullOrEmpty(this.Alias))
			{
				return;
			}
			if (this.EmailAddresses.Count > 0 && this.PrimarySmtpAddress == SmtpAddress.Empty)
			{
				List<ProxyAddress> list = new List<ProxyAddress>();
				foreach (ProxyAddress proxyAddress in this.EmailAddresses)
				{
					if (proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
					{
						list.Add(proxyAddress);
					}
				}
				LocalizedString description;
				if (list.Count > 1)
				{
					description = DirectoryStrings.ErrorMultiplePrimaries(ProxyAddressPrefix.Smtp.PrimaryPrefix.ToString());
				}
				else if (list.Count == 1)
				{
					description = DirectoryStrings.ErrorPrimarySmtpInvalid(list[0].ToString());
				}
				else
				{
					description = DirectoryStrings.ErrorMissingPrimarySmtp;
				}
				errors.Add(new ObjectValidationError(description, this.Identity, string.Empty));
			}
			this.SetUMDtmfMapIfNecessary();
			if (ComplianceConfigImpl.ArchivePropertiesHardeningEnabled)
			{
				this.ValidateArchiveProperties(VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled, errors);
			}
			this.SetValidArchiveDatabase();
			if (this.ModerationEnabled)
			{
				if (this.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorArbitrationMailboxCannotBeModerated, this.Identity, string.Empty));
				}
				else if (!this.BypassModerationCheck && MultiValuedPropertyBase.IsNullOrEmpty(this.ModeratedBy) && this.RecipientType != RecipientType.MailUniversalDistributionGroup)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorModeratorRequiredForModeration, ADRecipientSchema.ModeratedBy, null));
				}
			}
			if (this.IsResource)
			{
				if (this.ResourceType == null)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorEmptyResourceTypeofResourceMailbox, this.Identity, string.Empty));
				}
				if (this.ResourceType != null && !((MultiValuedProperty<string>)this[ADRecipientSchema.ResourceSearchProperties]).Contains(this.ResourceType.Value.ToString()))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorMetadataNotSearchProperty, this.Identity, string.Empty));
				}
			}
			if (this.AcceptMessagesOnlyFromDLMembers.Count > 0 && this.RejectMessagesFromDLMembers.Count > 0)
			{
				foreach (ADObjectId adobjectId in this.RejectMessagesFromDLMembers)
				{
					if (this.AcceptMessagesOnlyFromDLMembers.Contains(adobjectId))
					{
						string text = ADRecipient.OrganizationUnitFromADObjectId(adobjectId);
						if (string.IsNullOrEmpty(text))
						{
							text = (adobjectId.DistinguishedName ?? string.Empty);
						}
						else
						{
							text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
							{
								text,
								adobjectId.Name
							});
						}
						errors.Add(new ObjectValidationError(DirectoryStrings.ErrorDLAsBothAcceptedAndRejected(text ?? string.Empty), this.Identity, string.Empty));
					}
				}
			}
			if (this.AcceptMessagesOnlyFrom.Count > 0 && this.RejectMessagesFrom.Count > 0)
			{
				foreach (ADObjectId adobjectId2 in this.RejectMessagesFrom)
				{
					if (this.AcceptMessagesOnlyFrom.Contains(adobjectId2))
					{
						string text2 = ADRecipient.OrganizationUnitFromADObjectId(adobjectId2);
						if (string.IsNullOrEmpty(text2))
						{
							text2 = (adobjectId2.DistinguishedName ?? string.Empty);
						}
						else
						{
							text2 = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
							{
								text2,
								adobjectId2.Name
							});
						}
						errors.Add(new ObjectValidationError(DirectoryStrings.ErrorRecipientAsBothAcceptedAndRejected(text2), this.Identity, string.Empty));
					}
				}
			}
			if (this.SCLDeleteThreshold != null && this.SCLDeleteEnabled.GetValueOrDefault(false))
			{
				if (this.SCLDeleteThreshold.Value < 0 || this.SCLDeleteThreshold.Value > 9)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThresholdMustBeSet("Delete"), this.Identity, string.Empty));
				}
				if (this.SCLRejectEnabled.GetValueOrDefault(false) && this.SCLDeleteThreshold.Value <= this.SCLRejectThreshold.Value)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThisThresholdMustBeGreaterThanThatThreshold("Delete", "Reject"), this.Identity, string.Empty));
				}
				if (this.SCLQuarantineEnabled.GetValueOrDefault(false) && this.SCLDeleteThreshold.Value <= this.SCLQuarantineThreshold.Value)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThisThresholdMustBeGreaterThanThatThreshold("Delete", "Quarantine"), this.Identity, string.Empty));
				}
			}
			if (this.SCLRejectThreshold != null && this.SCLRejectEnabled.GetValueOrDefault(false))
			{
				if (this.SCLRejectThreshold.Value < 0 || this.SCLRejectThreshold.Value > 9)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThresholdMustBeSet("Reject"), this.Identity, string.Empty));
				}
				if (this.SCLQuarantineEnabled.GetValueOrDefault(false) && this.SCLRejectThreshold.Value <= this.SCLQuarantineThreshold.Value)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThisThresholdMustBeGreaterThanThatThreshold("Reject", "Quarantine"), this.Identity, string.Empty));
				}
			}
			if (this.SCLQuarantineEnabled.GetValueOrDefault(false) && this.SCLQuarantineThreshold != null && (this.SCLQuarantineThreshold.Value < 0 || this.SCLQuarantineThreshold.Value > 9))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThresholdMustBeSet("Quarantine"), this.Identity, string.Empty));
			}
			if (this.SCLJunkEnabled.GetValueOrDefault(false) && this.SCLJunkThreshold != null && (this.SCLJunkThreshold.Value < 0 || this.SCLJunkThreshold.Value > 9))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorThresholdMustBeSet("Junk"), this.Identity, string.Empty));
			}
			if (this.MessageFormat == MessageFormat.Text)
			{
				if (this.MessageBodyFormat == MessageBodyFormat.Html || this.MessageBodyFormat == MessageBodyFormat.TextAndHtml)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorTextMessageIncludingHtmlBody, this.Identity, string.Empty));
				}
				if (this.MacAttachmentFormat == MacAttachmentFormat.AppleSingle || this.MacAttachmentFormat == MacAttachmentFormat.AppleDouble)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorTextMessageIncludingAppleAttachment, this.Identity, string.Empty));
				}
			}
			else if (this.MacAttachmentFormat == MacAttachmentFormat.UuEncode)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorMimeMessageIncludingUuEncodedAttachment, this.Identity, string.Empty));
			}
			if (!this.propertyBag.IsReadOnly)
			{
				if (this.EmailAddressPolicyEnabled)
				{
					if (this.OriginalWindowsEmailAddress != SmtpAddress.Empty && this.WindowsEmailAddress != this.OriginalWindowsEmailAddress)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.ErrorCannotSetWindowsEmailAddress, this.Identity, string.Empty));
					}
					if (this.OriginalPrimarySmtpAddress != SmtpAddress.Empty && this.PrimarySmtpAddress != this.OriginalPrimarySmtpAddress)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.ErrorCannotSetPrimarySmtpAddress, this.Identity, string.Empty));
						return;
					}
				}
				else if (this.PrimarySmtpAddress != this.OriginalPrimarySmtpAddress && this.WindowsEmailAddress != this.OriginalWindowsEmailAddress && this.PrimarySmtpAddress != SmtpAddress.Empty && this.PrimarySmtpAddress != this.WindowsEmailAddress)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorPrimarySmtpAddressAndWindowsEmailAddressNotMatch, this.Identity, string.Empty));
				}
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00065A4C File Offset: 0x00063C4C
		internal static bool IsValidName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(name, 0);
			if (UnicodeCategory.Control == unicodeCategory || UnicodeCategory.Format == unicodeCategory)
			{
				return false;
			}
			for (int i = 0; i < name.Length; i++)
			{
				unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(name, i);
				if (UnicodeCategory.Control != unicodeCategory && UnicodeCategory.Format != unicodeCategory && UnicodeCategory.ConnectorPunctuation != unicodeCategory && UnicodeCategory.DashPunctuation != unicodeCategory && UnicodeCategory.OpenPunctuation != unicodeCategory && UnicodeCategory.ClosePunctuation != unicodeCategory && UnicodeCategory.InitialQuotePunctuation != unicodeCategory && UnicodeCategory.FinalQuotePunctuation != unicodeCategory && UnicodeCategory.OtherPunctuation != unicodeCategory && UnicodeCategory.SpaceSeparator != unicodeCategory && UnicodeCategory.LineSeparator != unicodeCategory && UnicodeCategory.ParagraphSeparator != unicodeCategory)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00065ACF File Offset: 0x00063CCF
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x00065AD7 File Offset: 0x00063CD7
		internal bool BypassModerationCheck { get; set; }

		// Token: 0x06001636 RID: 5686 RVA: 0x00065AE0 File Offset: 0x00063CE0
		internal static bool IsMemberOf(ADObjectId recipientId, ADObjectId groupId, bool directOnly, IRecipientSession session)
		{
			int num = -1;
			bool result;
			ADRecipient.TryIsMemberOfWithLimit(recipientId, groupId, directOnly, session, ref num, out result);
			return result;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00065B00 File Offset: 0x00063D00
		internal static bool TryIsMemberOfWithLimit(ADObjectId recipientId, ADObjectId groupId, bool directOnly, IRecipientSession session, ref int adQueryLimit, out bool isMember)
		{
			if (ADObjectId.Equals(recipientId, groupId))
			{
				isMember = true;
				return true;
			}
			if (adQueryLimit.Equals(0))
			{
				ExTraceGlobals.ADFindTracer.TraceDebug<int, ADObjectId, ADObjectId>(0L, "ADRecipient.TryIsMemberOfWithLimit: AD query limit {0} has been reached for {1} is a member of {2} lookup.", adQueryLimit, recipientId, groupId);
				isMember = false;
				return false;
			}
			int num = 1;
			bool result = ADRecipient.TryIsStrictMemberOfWithLimit(recipientId, session.Read(groupId), directOnly, session, new HashSet<ADObjectId>(), ref num, adQueryLimit, out isMember);
			adQueryLimit -= num;
			return result;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00065B68 File Offset: 0x00063D68
		internal static bool TryParseMailboxProvisioningData(string mailboxProvisioningData, out string mailboxPlanName, out MailboxProvisioningConstraint regionConstraint)
		{
			string[] array = mailboxProvisioningData.Replace(" ", string.Empty).ToUpper().Split(new char[]
			{
				';'
			});
			mailboxPlanName = null;
			regionConstraint = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			bool flag = true;
			bool flag2 = true;
			foreach (string text4 in array)
			{
				if (text4.Contains("MBX="))
				{
					if (text2 != null)
					{
						flag = false;
					}
					text2 = text4.Split(new char[]
					{
						'='
					})[1];
				}
				else if (text4.Contains("TYPE="))
				{
					if (text3 != null)
					{
						flag = false;
					}
					text3 = text4.Split(new char[]
					{
						'='
					})[1];
				}
				else if (text4.Contains("REG="))
				{
					if (text != null)
					{
						flag2 = false;
					}
					text = text4.Split(new char[]
					{
						'='
					})[1];
				}
			}
			if (string.IsNullOrWhiteSpace(text2) || string.IsNullOrWhiteSpace(text3))
			{
				flag = false;
			}
			if (flag)
			{
				mailboxPlanName = text3 + "_" + text2;
			}
			string text5 = null;
			if (text != null && flag2)
			{
				string a;
				if ((a = text) != null)
				{
					if (a == "NA")
					{
						text5 = "NAM";
						goto IL_16D;
					}
					if (a == "EU")
					{
						text5 = "EUR";
						goto IL_16D;
					}
					if (a == "AP")
					{
						text5 = "APAC";
						goto IL_16D;
					}
				}
				flag2 = false;
			}
			IL_16D:
			if (text5 != null)
			{
				regionConstraint = new MailboxProvisioningConstraint(string.Format("{{Region -eq '{0}'}}", text5.ToString()));
			}
			return flag2;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00065D00 File Offset: 0x00063F00
		internal static string GetCustomAttribute(IPropertyBag propertyBag, string customAttributeName)
		{
			if (propertyBag != null)
			{
				switch (customAttributeName)
				{
				case "CustomAttribute1":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute1];
				case "CustomAttribute2":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute2];
				case "CustomAttribute3":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute3];
				case "CustomAttribute4":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute4];
				case "CustomAttribute5":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute5];
				case "CustomAttribute6":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute6];
				case "CustomAttribute7":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute7];
				case "CustomAttribute8":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute8];
				case "CustomAttribute9":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute9];
				case "CustomAttribute10":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute10];
				case "CustomAttribute11":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute11];
				case "CustomAttribute12":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute12];
				case "CustomAttribute13":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute13];
				case "CustomAttribute14":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute14];
				case "CustomAttribute15":
					return (string)propertyBag[ADRecipientSchema.CustomAttribute15];
				case "ExtensionCustomAttribute1":
					return ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ExtensionCustomAttribute1]).FirstOrDefault<string>();
				case "ExtensionCustomAttribute2":
					return ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ExtensionCustomAttribute2]).FirstOrDefault<string>();
				case "ExtensionCustomAttribute3":
					return ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ExtensionCustomAttribute3]).FirstOrDefault<string>();
				case "ExtensionCustomAttribute4":
					return ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ExtensionCustomAttribute4]).FirstOrDefault<string>();
				case "ExtensionCustomAttribute5":
					return ((MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ExtensionCustomAttribute5]).FirstOrDefault<string>();
				}
				return null;
			}
			return null;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00066010 File Offset: 0x00064210
		private static bool TryIsStrictMemberOfWithLimit(ADObjectId recipientId, ADRecipient rootGroup, bool directOnly, IRecipientSession session, HashSet<ADObjectId> visitedGroups, ref int adQueryCount, int adQueryLimit, out bool isMember)
		{
			if (recipientId == null || rootGroup == null || !(rootGroup is IADDistributionList))
			{
				isMember = false;
				return true;
			}
			ADGroup adgroup = rootGroup as ADGroup;
			if (adgroup != null && ADRecipient.IsEmptyADGroup(adgroup))
			{
				isMember = false;
				return true;
			}
			if (ADRecipient.IsImmediateChild(adgroup, recipientId))
			{
				isMember = true;
				return true;
			}
			Stack<IADDistributionList> stack = new Stack<IADDistributionList>();
			stack.Push(rootGroup as IADDistributionList);
			while (stack.Count != 0)
			{
				IADDistributionList iaddistributionList = stack.Pop();
				ADObjectId id = (iaddistributionList as ADRecipient).Id;
				if (adQueryLimit.Equals(adQueryCount))
				{
					ExTraceGlobals.ADFindTracer.TraceDebug<int, ADObjectId, ADObjectId>(0L, "ADRecipient.TryIsStrictMemberOfWithLimit: AD query limit {0} has been reached for {1} is a member of {2} lookup.", adQueryLimit, recipientId, id);
					isMember = false;
					return false;
				}
				try
				{
					ADGroup adgroup2 = iaddistributionList as ADGroup;
					bool flag = false;
					ADPagedReader<ADRecipient> adpagedReader;
					if (adgroup2 != null && !adgroup2.HiddenGroupMembershipEnabled)
					{
						adpagedReader = adgroup2.ExpandGroupOnly(10000);
						flag = true;
					}
					else
					{
						adpagedReader = iaddistributionList.Expand(10000);
					}
					adQueryCount++;
					int num = 0;
					foreach (ADRecipient adrecipient in adpagedReader)
					{
						num++;
						if (!flag && ADObjectId.Equals(recipientId, adrecipient.Id))
						{
							isMember = true;
							return true;
						}
						if (10000 == num && (adpagedReader.MorePagesAvailable == null || adpagedReader.MorePagesAvailable.Value))
						{
							if (adQueryLimit.Equals(adQueryCount))
							{
								ExTraceGlobals.ADFindTracer.TraceDebug<int, ADObjectId, ADObjectId>(0L, "ADRecipient.TryIsStrictMemberOfWithLimit: AD query limit {0} has been reached for {1} is a member of {2} lookup.", adQueryLimit, recipientId, id);
								isMember = false;
								return false;
							}
							num = 0;
							adQueryCount++;
						}
						if (!directOnly)
						{
							ADGroup adgroup3 = adrecipient as ADGroup;
							if (ADRecipient.IsImmediateChild(adgroup3, recipientId))
							{
								isMember = true;
								return true;
							}
							if (ADRecipient.NeedIsMemberOfCheckForGroup(adrecipient, id, visitedGroups) && (adgroup3 == null || !ADRecipient.IsEmptyADGroup(adgroup3)))
							{
								visitedGroups.Add(adrecipient.Id);
								stack.Push(adrecipient as IADDistributionList);
							}
						}
					}
				}
				catch (DataValidationException arg)
				{
					ExTraceGlobals.ADFindTracer.TraceError<DataValidationException, ADObjectId, ADObjectId>(0L, "ADRecipient.TryIsStrictMemberOfWithLimit: AD query exception {0} was encountered for {1} is a member of {2} lookup.", arg, recipientId, id);
					isMember = false;
					return false;
				}
			}
			isMember = false;
			return true;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00066264 File Offset: 0x00064464
		private static bool NeedIsMemberOfCheckForGroup(ADRecipient subDL, ADObjectId parentGroup, HashSet<ADObjectId> visitedGroups)
		{
			return subDL != null && !ADObjectId.Equals(subDL.Id, parentGroup) && !visitedGroups.Contains(subDL.Id) && subDL is IADDistributionList;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00066296 File Offset: 0x00064496
		private static bool IsEmptyADGroup(ADGroup group)
		{
			return !group.HiddenGroupMembershipEnabled && (group.Members == null || group.Members.Count == 0);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000662BA File Offset: 0x000644BA
		private static bool IsImmediateChild(ADGroup group, ADObjectId recipientId)
		{
			return group != null && group.Members != null && group.Members.Contains(recipientId);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000662D8 File Offset: 0x000644D8
		internal bool IsOWAEnabledStatusConsistent()
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this[ADRecipientSchema.ProtocolSettings];
			string text = null;
			string text2 = null;
			foreach (string text3 in multiValuedProperty)
			{
				if (text3.StartsWith("OWA"))
				{
					text2 = text3;
				}
				else if (text3.StartsWith("HTTP"))
				{
					text = text3;
				}
			}
			bool result;
			if (text == null || text2 == null)
			{
				result = true;
			}
			else
			{
				string[] array = text2.Split(new char[]
				{
					'§'
				});
				string[] array2 = text.Split(new char[]
				{
					'§'
				});
				result = (array.Length <= 1 || array2.Length <= 1 || array[1].Equals(array2[1]));
			}
			return result;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000663C0 File Offset: 0x000645C0
		internal void StampDefaultValues(RecipientType recipientType)
		{
			if (recipientType == RecipientType.UserMailbox)
			{
				this.StampMailboxDefaultValues(true);
				return;
			}
			if (recipientType == RecipientType.MailUser)
			{
				ADUser aduser = (ADUser)this;
				aduser.MessageFormat = MessageFormat.Mime;
				aduser.MessageBodyFormat = MessageBodyFormat.TextAndHtml;
				aduser.UseMapiRichTextFormat = UseMapiRichTextFormat.UseDefaultSettings;
				aduser.RecipientTypeDetails = RecipientTypeDetails.MailUser;
				aduser.RecipientDisplayType = new RecipientDisplayType?(Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.RemoteMailUser);
				aduser.ArchiveQuota = ProvisioningHelper.DefaultArchiveQuota;
				aduser.ArchiveWarningQuota = ProvisioningHelper.DefaultArchiveWarningQuota;
				aduser.RecoverableItemsQuota = ProvisioningHelper.DefaultRecoverableItemsQuota;
				aduser.RecoverableItemsWarningQuota = ProvisioningHelper.DefaultRecoverableItemsWarningQuota;
				aduser.CalendarLoggingQuota = ProvisioningHelper.DefaultCalendarLoggingQuota;
				return;
			}
			if (recipientType == RecipientType.MailContact)
			{
				ADContact adcontact = (ADContact)this;
				adcontact.MessageFormat = MessageFormat.Mime;
				adcontact.MessageBodyFormat = MessageBodyFormat.TextAndHtml;
				adcontact.DeliverToForwardingAddress = false;
				adcontact.RequireAllSendersAreAuthenticated = false;
				adcontact.UseMapiRichTextFormat = UseMapiRichTextFormat.UseDefaultSettings;
				adcontact.RecipientDisplayType = new RecipientDisplayType?(Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.RemoteMailUser);
				return;
			}
			if (recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup)
			{
				ADGroup adgroup = (ADGroup)this;
				adgroup.ReportToOriginatorEnabled = true;
				adgroup.RequireAllSendersAreAuthenticated = true;
				return;
			}
			if (recipientType == RecipientType.DynamicDistributionGroup)
			{
				ADDynamicGroup addynamicGroup = (ADDynamicGroup)this;
				addynamicGroup.ReportToOriginatorEnabled = true;
				addynamicGroup.RequireAllSendersAreAuthenticated = true;
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000664D4 File Offset: 0x000646D4
		internal void StampMailboxDefaultValues(bool isNewState)
		{
			ADUser aduser = (ADUser)this;
			if (isNewState && aduser.UseDatabaseQuotaDefaults == null)
			{
				aduser.UseDatabaseQuotaDefaults = new bool?(true);
			}
			aduser.ArchiveQuota = ProvisioningHelper.DefaultArchiveQuota;
			aduser.ArchiveWarningQuota = ProvisioningHelper.DefaultArchiveWarningQuota;
			aduser.RecoverableItemsQuota = ProvisioningHelper.DefaultRecoverableItemsQuota;
			aduser.RecoverableItemsWarningQuota = ProvisioningHelper.DefaultRecoverableItemsWarningQuota;
			aduser.CalendarLoggingQuota = ProvisioningHelper.DefaultCalendarLoggingQuota;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0006653E File Offset: 0x0006473E
		internal void PopulateBypassModerationFromSendersOrMembers()
		{
			this.GetCombinedIdentities(ADRecipientSchema.BypassModerationFrom, ADRecipientSchema.BypassModerationFromDLMembers, ADRecipientSchema.BypassModerationFromSendersOrMembers);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00066555 File Offset: 0x00064755
		internal void PopulateAcceptMessagesOnlyFromSendersOrMembers()
		{
			this.GetCombinedIdentities(ADRecipientSchema.AcceptMessagesOnlyFrom, ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, ADRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0006656C File Offset: 0x0006476C
		internal void PopulateRejectMessagesFromSendersOrMembers()
		{
			this.GetCombinedIdentities(ADRecipientSchema.RejectMessagesFrom, ADRecipientSchema.RejectMessagesFromDLMembers, ADRecipientSchema.RejectMessagesFromSendersOrMembers);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00066584 File Offset: 0x00064784
		private void GetCombinedIdentities(ADPropertyDefinition propertyDefinition1, ADPropertyDefinition propertyDefinition2, ADPropertyDefinition propertyDefinitionResult)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)this.propertyBag[propertyDefinition1];
			MultiValuedProperty<ADObjectId> multiValuedProperty2 = (MultiValuedProperty<ADObjectId>)this.propertyBag[propertyDefinition2];
			MultiValuedProperty<ADObjectId> multiValuedProperty3 = new MultiValuedProperty<ADObjectId>();
			try
			{
				if (multiValuedProperty.Count != 0 || multiValuedProperty2.Count != 0)
				{
					HashSet<ADObjectId> hashSet = new HashSet<ADObjectId>();
					foreach (ADObjectId item in multiValuedProperty)
					{
						if (hashSet.Add(item))
						{
							multiValuedProperty3.Add(item);
						}
					}
					foreach (ADObjectId item2 in multiValuedProperty2)
					{
						if (hashSet.Add(item2))
						{
							multiValuedProperty3.Add(item2);
						}
					}
				}
			}
			finally
			{
				multiValuedProperty3.ResetChangeTracking();
				this.propertyBag[propertyDefinitionResult] = multiValuedProperty3;
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00066690 File Offset: 0x00064890
		internal static bool TryGetMailTipParts(string cultureAndMailTip, out string culture, out string mailTip)
		{
			culture = string.Empty;
			mailTip = string.Empty;
			if (cultureAndMailTip == null)
			{
				return false;
			}
			string[] array = cultureAndMailTip.Split(new char[]
			{
				':'
			}, 2);
			if (array.Length != 2)
			{
				return false;
			}
			culture = array[0];
			mailTip = array[1];
			return true;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000666D8 File Offset: 0x000648D8
		internal static bool IsRecipientTypeDL(RecipientType recipientType)
		{
			bool result = false;
			switch (recipientType)
			{
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0006670C File Offset: 0x0006490C
		internal static bool IsSystemMailbox(RecipientTypeDetails rtd)
		{
			return rtd == RecipientTypeDetails.MailboxPlan || rtd == RecipientTypeDetails.ArbitrationMailbox || rtd == RecipientTypeDetails.DiscoveryMailbox || rtd == RecipientTypeDetails.MonitoringMailbox || rtd == RecipientTypeDetails.SystemAttendantMailbox || rtd == RecipientTypeDetails.SystemMailbox || rtd == RecipientTypeDetails.AuditLogMailbox;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00066760 File Offset: 0x00064960
		internal bool? IsAclCapable
		{
			get
			{
				return (bool?)this[ADRecipientSchema.IsAclCapable];
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00066774 File Offset: 0x00064974
		internal static object IsAclCapableGetter(IPropertyBag propertyBag)
		{
			RecipientDisplayType? recipientDisplayType = (RecipientDisplayType?)propertyBag[ADRecipientSchema.RecipientDisplayType];
			if (recipientDisplayType == null)
			{
				return null;
			}
			return (recipientDisplayType.Value & Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.ACLableMailboxUser) != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.MailboxUser;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000667B8 File Offset: 0x000649B8
		internal bool IsAnyAddressMatched(params string[] targetSmtpAddresses)
		{
			HashSet<string> hashSet = new HashSet<string>(targetSmtpAddresses, StringComparer.OrdinalIgnoreCase);
			foreach (ProxyAddress proxyAddress in this.EmailAddresses)
			{
				SmtpProxyAddress smtpProxyAddress = proxyAddress as SmtpProxyAddress;
				if (smtpProxyAddress != null && hashSet.Contains(smtpProxyAddress.SmtpAddress))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00066838 File Offset: 0x00064A38
		internal bool IsValidSecurityPrincipal
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsValidSecurityPrincipal];
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0006684A File Offset: 0x00064A4A
		internal static object IsValidSecurityPrincipalGetter(IPropertyBag propertyBag)
		{
			return ADRecipient.IsValidRecipient(propertyBag, true);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00066858 File Offset: 0x00064A58
		internal static bool IsValidRecipient(IPropertyBag propertyBag, bool enforceAclCapableCheck)
		{
			switch ((RecipientType)propertyBag[ADRecipientSchema.RecipientType])
			{
			case RecipientType.Invalid:
			case RecipientType.User:
			case RecipientType.Contact:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.DynamicDistributionGroup:
			case RecipientType.PublicFolder:
			case RecipientType.PublicDatabase:
			case RecipientType.MicrosoftExchange:
				return false;
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
			{
				UserAccountControlFlags userAccountControlFlags = (UserAccountControlFlags)propertyBag[ADUserSchema.UserAccountControl];
				if ((userAccountControlFlags & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.AccountDisabled)
				{
					SecurityIdentifier securityIdentifier = (SecurityIdentifier)propertyBag[ADRecipientSchema.MasterAccountSid];
					if (securityIdentifier == null || securityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
					{
						return false;
					}
				}
				break;
			}
			case RecipientType.MailContact:
			case RecipientType.Group:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.SystemAttendantMailbox:
			case RecipientType.SystemMailbox:
			case RecipientType.Computer:
				break;
			default:
				return true;
			}
			string value = (string)propertyBag[ADRecipientSchema.LegacyExchangeDN];
			bool? flag = (bool?)propertyBag[ADRecipientSchema.IsAclCapable];
			if (string.IsNullOrEmpty(value) || (propertyBag[ADRecipientSchema.MasterAccountSid] == null && propertyBag[IADSecurityPrincipalSchema.Sid] == null) || (enforceAclCapableCheck && flag == false))
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0006696C File Offset: 0x00064B6C
		public virtual void PopulateDtmfMap(bool create)
		{
			this.PopulateDtmfMap(create, this.DisplayName, this.DisplayName, this.PrimarySmtpAddress, null);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00066988 File Offset: 0x00064B88
		internal void PopulateDtmfMap(bool create, string firstLast, string lastFirst, SmtpAddress smtpAddress, IList<string> phones)
		{
			if (!create && this.UMDtmfMap.Count == 0)
			{
				return;
			}
			List<string> list = new List<string>(16);
			if (!string.IsNullOrEmpty(firstLast))
			{
				list.Add("firstNameLastName:" + DtmfString.DtmfEncode(firstLast));
			}
			if (!string.IsNullOrEmpty(lastFirst))
			{
				list.Add("lastNameFirstName:" + DtmfString.DtmfEncode(lastFirst));
			}
			if (SmtpAddress.Empty != smtpAddress)
			{
				list.Add("emailAddress:" + DtmfString.DtmfEncode(smtpAddress.Local));
			}
			if (phones != null)
			{
				foreach (string text in phones)
				{
					if (!string.IsNullOrEmpty(text))
					{
						list.Add("reversedPhone:" + DtmfString.Reverse(text));
					}
				}
			}
			bool flag = false;
			if (list.Count != this.UMDtmfMap.Count)
			{
				flag = true;
			}
			else
			{
				foreach (string item in list)
				{
					if (!this.UMDtmfMap.Contains(item))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.UMDtmfMap.Clear();
				foreach (string text2 in list)
				{
					this.UMDtmfMap.Add(text2.Substring(0, Math.Min(text2.Length, 256)));
				}
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00066B3C File Offset: 0x00064D3C
		internal static bool TryGetAuthenticationTypeFilterInternal(AuthenticationType authType, OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage)
		{
			queryFilter = null;
			if (organizationId == null || organizationId.OrganizationalUnit == null)
			{
				errorMessage = DirectoryStrings.CannotBuildAuthenticationTypeFilterBadArgument(authType.ToString());
				return false;
			}
			List<TextFilter> list = new List<TextFilter>();
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			IDictionary<string, AuthenticationType> namespaceAuthenticationTypeHash = organizationIdCacheValue.NamespaceAuthenticationTypeHash;
			foreach (string text in namespaceAuthenticationTypeHash.Keys)
			{
				if (namespaceAuthenticationTypeHash[text] == authType)
				{
					list.Add(new TextFilter(ADRecipientSchema.WindowsLiveID, "@" + text, MatchOptions.Suffix, MatchFlags.IgnoreCase));
				}
			}
			if (list.Count == 0)
			{
				errorMessage = DirectoryStrings.CannotBuildAuthenticationTypeFilterNoNamespacesOfType(authType.ToString());
				return false;
			}
			queryFilter = QueryFilter.OrTogether(list.ToArray());
			errorMessage = LocalizedString.Empty;
			return true;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00066C2C File Offset: 0x00064E2C
		private void PopulateGlobalAddressListFromAddressBookPolicy()
		{
			if (this.m_Session == null)
			{
				return;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.m_Session.SessionSettings.RootOrgId, base.OrganizationId, null, false), 6694, "PopulateGlobalAddressListFromAddressBookPolicy", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipient.cs");
			if (tenantOrTopologyConfigurationSession != null)
			{
				AddressBookMailboxPolicy addressBookMailboxPolicy = tenantOrTopologyConfigurationSession.Read<AddressBookMailboxPolicy>(this.AddressBookPolicy);
				this.globalAddressListFromAddressBookPolicy = ((addressBookMailboxPolicy != null) ? addressBookMailboxPolicy.GlobalAddressList : null);
			}
		}

		// Token: 0x04000B2C RID: 2860
		internal const string DefaultMailTipTranslationPrefix = "default";

		// Token: 0x04000B2D RID: 2861
		private const int MaximumAirSyncNumber = 3;

		// Token: 0x04000B2E RID: 2862
		internal const int DtmfMapEntryMaxLength = 256;

		// Token: 0x04000B2F RID: 2863
		internal const string DtmfMapFirstNameLastNamePrefix = "firstNameLastName:";

		// Token: 0x04000B30 RID: 2864
		internal const string DtmfMapLastNameFirstNamePrefix = "lastNameFirstName:";

		// Token: 0x04000B31 RID: 2865
		internal const string DtmfMapEmailAddressPrefix = "emailAddress:";

		// Token: 0x04000B32 RID: 2866
		internal const string DtmfMapReversedPhonePrefix = "reversedPhone:";

		// Token: 0x04000B33 RID: 2867
		internal const int MaxModeratorNum = 25;

		// Token: 0x04000B34 RID: 2868
		internal const int MaxModeratorNumOnTenant = 10;

		// Token: 0x04000B35 RID: 2869
		internal const char ResourceSeparator = ':';

		// Token: 0x04000B36 RID: 2870
		internal const string ResourceTypePrefix = "ResourceType";

		// Token: 0x04000B37 RID: 2871
		public const string UMExtensionDelimiter = ";";

		// Token: 0x04000B38 RID: 2872
		public const string UMDialPlanString = "phone-context=";

		// Token: 0x04000B39 RID: 2873
		public const string SoftDeletedObjectString = "SoftDeletedObject";

		// Token: 0x04000B3A RID: 2874
		public const string SoftDeletedContainerName = "Soft Deleted Objects";

		// Token: 0x04000B3B RID: 2875
		public const string StrictSoftDeletedContainerOUName = ",OU=Soft Deleted Objects,";

		// Token: 0x04000B3C RID: 2876
		public const string SoftDeletedContainerOUName = "OU=Soft Deleted Objects,";

		// Token: 0x04000B3D RID: 2877
		public const string AliasPattern = "^[!#%&'=`~\\$\\*\\+\\-\\/\\?\\^\\{\\|\\}a-zA-Z0-9_\\u00A1-\\u00FF]+(\\.[!#%&'=`~\\$\\*\\+\\-\\/\\?\\^\\{\\|\\}a-zA-Z0-9_\\u00A1-\\u00FF]+)*$";

		// Token: 0x04000B3E RID: 2878
		public const string AliasValidCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&'*+-/=?^_`{|}~.¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";

		// Token: 0x04000B3F RID: 2879
		internal static readonly RecipientType[] AllowedModeratorsRecipientTypes = new RecipientType[]
		{
			RecipientType.MailContact,
			RecipientType.UserMailbox,
			RecipientType.MailUser
		};

		// Token: 0x04000B40 RID: 2880
		internal static readonly RecipientType[] DtmfMapAllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.MailContact,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.MailUniversalDistributionGroup
		};

		// Token: 0x04000B41 RID: 2881
		internal static readonly ExchangeObjectVersion ArbitrationMailboxObjectVersion = new ExchangeObjectVersion(1, 0, 14, 0, 0, 0);

		// Token: 0x04000B42 RID: 2882
		internal static readonly ExchangeObjectVersion PublicFolderMailboxObjectVersion = new ExchangeObjectVersion(1, 1, ExchangeObjectVersion.Exchange2012.ExchangeBuild);

		// Token: 0x04000B43 RID: 2883
		internal static readonly ExchangeObjectVersion TeamMailboxObjectVersion = new ExchangeObjectVersion(1, 1, ExchangeObjectVersion.Exchange2012.ExchangeBuild);

		// Token: 0x04000B44 RID: 2884
		private ADObjectId globalAddressListFromAddressBookPolicy;

		// Token: 0x04000B45 RID: 2885
		internal static readonly char[] ResourceSeperatorArray = new char[]
		{
			':'
		};
	}
}

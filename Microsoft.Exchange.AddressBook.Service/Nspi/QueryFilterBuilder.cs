using System;
using System.Text;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Nspi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Nspi
{
	// Token: 0x02000045 RID: 69
	internal class QueryFilterBuilder
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00011F78 File Offset: 0x00010178
		internal QueryFilterBuilder(Encoding encoding)
		{
			this.encoding = encoding;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011F87 File Offset: 0x00010187
		internal static bool IsAnrRestriction(Restriction restriction)
		{
			return restriction.Type == Restriction.ResType.Property && ((Restriction.PropertyRestriction)restriction).PropTag.Id() == PropTag.Anr.Id();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011FB0 File Offset: 0x000101B0
		internal static QueryFilter RestrictToVisibleItems(QueryFilter filter, string legacyDN)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.DiscoveryMailbox);
			return new AndFilter(new QueryFilter[]
			{
				QueryFilterBuilder.DisplayNameExists,
				new OrFilter(new QueryFilter[]
				{
					QueryFilterBuilder.NotHiddenFromAddressLists,
					queryFilter,
					queryFilter2
				}),
				filter
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001201A File Offset: 0x0001021A
		internal QueryFilter TranslateRestriction(Restriction restriction)
		{
			return this.TranslateInternal(restriction, 0);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00012024 File Offset: 0x00010224
		internal QueryFilter TranslateANR(Restriction restriction, string legacyDN, ADObjectId addressListScope)
		{
			return this.TranslateANR(this.TranslateRestriction(restriction), legacyDN, addressListScope);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00012035 File Offset: 0x00010235
		internal QueryFilter TranslateANR(string name, string legacyDN, ADObjectId addressListScope)
		{
			return this.TranslateANR(new AmbiguousNameResolutionFilter(name), legacyDN, addressListScope);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00012048 File Offset: 0x00010248
		private static ComparisonOperator ConvertRelOpToComparisonOperator(Restriction.RelOp relOp)
		{
			switch (relOp)
			{
			case Restriction.RelOp.LessThan:
				return ComparisonOperator.LessThan;
			case Restriction.RelOp.LessThanOrEqual:
				return ComparisonOperator.LessThanOrEqual;
			case Restriction.RelOp.GreaterThan:
				return ComparisonOperator.GreaterThan;
			case Restriction.RelOp.GreaterThanOrEqual:
				return ComparisonOperator.GreaterThanOrEqual;
			case Restriction.RelOp.Equal:
				return ComparisonOperator.Equal;
			case Restriction.RelOp.NotEqual:
				return ComparisonOperator.NotEqual;
			case Restriction.RelOp.RegularExpression:
				break;
			default:
				switch (relOp)
				{
				case Restriction.RelOp.Include:
				case Restriction.RelOp.Exclude:
					break;
				default:
					if (relOp != Restriction.RelOp.MemberOfDL)
					{
					}
					break;
				}
				break;
			}
			throw new NspiException(NspiStatus.TooComplex, "Restriction.RelOp is not supported");
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000120AC File Offset: 0x000102AC
		private static PropertyDefinition PropTagToPropertyDefinition(PropTag tag)
		{
			PropertyDefinition propertyDefinition = NspiPropMapper.GetPropertyDefinition(tag);
			if (propertyDefinition == null)
			{
				throw new NspiException(NspiStatus.TooComplex, "Unknown proptag");
			}
			return propertyDefinition;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000120D4 File Offset: 0x000102D4
		private QueryFilter TranslateANR(QueryFilter filter, string legacyDN, ADObjectId addressListScope)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, addressListScope);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN);
			QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.DiscoveryMailbox);
			return new AndFilter(new QueryFilter[]
			{
				filter,
				new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2,
					queryFilter3
				})
			});
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012144 File Offset: 0x00010344
		private QueryFilter TranslateInternal(Restriction restriction, int depth)
		{
			if (restriction == null)
			{
				throw new NspiException(NspiStatus.TooComplex, "Null restriction");
			}
			if (depth > 256)
			{
				throw new NspiException(NspiStatus.TooComplex, "Restriction is too deep");
			}
			switch (restriction.Type)
			{
			case Restriction.ResType.And:
			case Restriction.ResType.Or:
				return this.TranslateAndOrRestriction((Restriction.AndOrNotRestriction)restriction, depth + 1);
			case Restriction.ResType.Not:
			{
				Restriction.NotRestriction notRestriction = (Restriction.NotRestriction)restriction;
				return new NotFilter(this.TranslateInternal(notRestriction.Restriction, depth + 1));
			}
			case Restriction.ResType.Content:
				return this.TranslateContentRestriction((Restriction.ContentRestriction)restriction);
			case Restriction.ResType.Property:
				return this.TranslatePropertyRestriction((Restriction.PropertyRestriction)restriction);
			case Restriction.ResType.Exist:
			{
				Restriction.ExistRestriction existRestriction = (Restriction.ExistRestriction)restriction;
				return new ExistsFilter(QueryFilterBuilder.PropTagToPropertyDefinition(existRestriction.Tag));
			}
			}
			throw new NspiException(NspiStatus.TooComplex, "Restriction.Type is not supported");
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001221C File Offset: 0x0001041C
		private QueryFilter TranslateAndOrRestriction(Restriction.AndOrNotRestriction restriction, int depth)
		{
			Restriction[] restrictions;
			if (restriction.Type == Restriction.ResType.And)
			{
				restrictions = ((Restriction.AndRestriction)restriction).Restrictions;
			}
			else
			{
				restrictions = ((Restriction.OrRestriction)restriction).Restrictions;
			}
			QueryFilter[] array = new QueryFilter[restrictions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.TranslateInternal(restrictions[i], depth + 1);
			}
			if (restriction.Type == Restriction.ResType.And)
			{
				return new AndFilter(array);
			}
			return new OrFilter(array);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012288 File Offset: 0x00010488
		private QueryFilter TranslateContentRestriction(Restriction.ContentRestriction restriction)
		{
			MatchOptions matchOptions;
			if ((restriction.Flags & ContentFlags.SubString) == ContentFlags.SubString)
			{
				matchOptions = MatchOptions.SubString;
			}
			else
			{
				if ((restriction.Flags & ContentFlags.Prefix) != ContentFlags.Prefix)
				{
					throw new NspiException(NspiStatus.TooComplex, "restriction.Flags not SubString or Prefix");
				}
				matchOptions = MatchOptions.Prefix;
			}
			object obj = this.ConvertValue(restriction.PropValue);
			string text = obj as string;
			if (text != null)
			{
				return new TextFilter(QueryFilterBuilder.PropTagToPropertyDefinition(restriction.PropTag), text, matchOptions, MatchFlags.Default);
			}
			byte[] array = obj as byte[];
			if (array != null)
			{
				return new BinaryFilter(QueryFilterBuilder.PropTagToPropertyDefinition(restriction.PropTag), array, matchOptions, MatchFlags.Default);
			}
			throw new NspiException(NspiStatus.TooComplex, "Unknown PropValue");
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001231C File Offset: 0x0001051C
		private QueryFilter TranslatePropertyRestriction(Restriction.PropertyRestriction restriction)
		{
			object obj = this.ConvertValue(restriction.PropValue);
			if (restriction.PropTag.Id() == PropTag.Anr.Id())
			{
				return new AmbiguousNameResolutionFilter((string)obj);
			}
			if (restriction.PropTag.Id() == PropTag.AddrType.Id())
			{
				return new TrueFilter();
			}
			return new ComparisonFilter(QueryFilterBuilder.ConvertRelOpToComparisonOperator(restriction.Op), QueryFilterBuilder.PropTagToPropertyDefinition(restriction.PropTag), obj);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00012394 File Offset: 0x00010594
		private object ConvertValue(PropValue propValue)
		{
			if ((propValue.PropType & PropType.MultiValueFlag) == PropType.MultiValueFlag)
			{
				object[] array = propValue.Value as object[];
				if (array == null || array.Length != 1)
				{
					throw new NspiException(NspiStatus.TooComplex, "Only one value permitted with multivalue");
				}
				if (propValue.PropType == PropType.AnsiStringArray && propValue.Value is byte[][])
				{
					byte[][] array2 = (byte[][])propValue.Value;
					return this.encoding.GetString(array2[0]);
				}
				return array[0];
			}
			else
			{
				if (propValue.PropType == PropType.AnsiString && propValue.Value is byte[])
				{
					return this.encoding.GetString((byte[])propValue.Value);
				}
				return propValue.Value;
			}
		}

		// Token: 0x040001A7 RID: 423
		private static readonly QueryFilter DisplayNameExists = new ExistsFilter(ADRecipientSchema.DisplayName);

		// Token: 0x040001A8 RID: 424
		private static readonly QueryFilter NotHiddenFromAddressLists = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsEnabled, false);

		// Token: 0x040001A9 RID: 425
		private readonly Encoding encoding;
	}
}

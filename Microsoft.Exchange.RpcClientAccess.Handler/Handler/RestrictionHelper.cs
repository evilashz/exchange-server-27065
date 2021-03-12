using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000019 RID: 25
	internal static class RestrictionHelper
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000081C0 File Offset: 0x000063C0
		private static bool IsKnownRestrictionServerIdPropertyId(PropertyId propertyId, ViewType viewType)
		{
			if (viewType == ViewType.MessageView)
			{
				if (propertyId == PropertyId.ParentEntryId || propertyId == PropertyId.RecordKey || propertyId == PropertyId.EntryId)
				{
					return true;
				}
			}
			else if (viewType == ViewType.FolderView && (propertyId == PropertyId.RecordKey || propertyId == PropertyId.EntryId))
			{
				return true;
			}
			return false;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008206 File Offset: 0x00006406
		private static bool IsKnownRestrictionConversionPropertyId(PropertyId propertyId, ViewType viewType)
		{
			return RestrictionHelper.IsKnownRestrictionServerIdPropertyId(propertyId, viewType) || propertyId == PropertyId.InstanceKey;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008220 File Offset: 0x00006420
		private static bool ConvertRestriction(StoreSession session, RestrictionHelper.ConvertPropertyTag convertPropertyTag, RestrictionHelper.ConvertPropertyValue convertPropertyValue, ref Restriction restriction, ViewType viewType)
		{
			RestrictionType restrictionType = restriction.RestrictionType;
			switch (restrictionType)
			{
			case RestrictionType.And:
			case RestrictionType.Or:
			{
				CompositeRestriction compositeRestriction = restriction as CompositeRestriction;
				Restriction[] childRestrictions = compositeRestriction.ChildRestrictions;
				bool flag = false;
				for (int i = 0; i < childRestrictions.Length; i++)
				{
					if (RestrictionHelper.ConvertRestriction(session, convertPropertyTag, convertPropertyValue, ref childRestrictions[i], viewType))
					{
						flag = true;
					}
				}
				if (flag)
				{
					if (restriction.RestrictionType == RestrictionType.And)
					{
						restriction = new AndRestriction(childRestrictions);
					}
					else
					{
						restriction = new OrRestriction(childRestrictions);
					}
					return true;
				}
				return false;
			}
			case RestrictionType.Not:
			{
				NotRestriction notRestriction = restriction as NotRestriction;
				Restriction childRestriction = notRestriction.ChildRestriction;
				if (RestrictionHelper.ConvertRestriction(session, convertPropertyTag, convertPropertyValue, ref childRestriction, viewType))
				{
					restriction = new NotRestriction(childRestriction);
					return true;
				}
				return false;
			}
			case RestrictionType.Content:
			{
				ContentRestriction contentRestriction = restriction as ContentRestriction;
				if (contentRestriction != null && contentRestriction.PropertyValue == null)
				{
					throw new RopExecutionException(string.Format("Invalid restriction found: {0}.", restriction), (ErrorCode)2147942487U);
				}
				if (RestrictionHelper.IsKnownRestrictionConversionPropertyId(contentRestriction.PropertyTag.PropertyId, viewType))
				{
					throw new RopExecutionException(string.Format("Property {0} is not supported for ContentRestriction.", contentRestriction.PropertyTag), (ErrorCode)2147746071U);
				}
				return false;
			}
			case RestrictionType.Property:
			{
				PropertyRestriction propertyRestriction = restriction as PropertyRestriction;
				if (propertyRestriction.PropertyValue == null)
				{
					throw new RopExecutionException(string.Format("Invalid restriction found: {0}.", restriction), (ErrorCode)2147942487U);
				}
				if (RestrictionHelper.IsKnownRestrictionConversionPropertyId(propertyRestriction.PropertyTag.PropertyId, viewType))
				{
					PropertyTag propertyTag = convertPropertyTag(propertyRestriction.PropertyTag, viewType);
					PropertyValue value = propertyRestriction.PropertyValue.Value;
					convertPropertyValue(session, ref value, viewType);
					restriction = new PropertyRestriction(propertyRestriction.RelationOperator, propertyTag, new PropertyValue?(value));
					return true;
				}
				return false;
			}
			case RestrictionType.CompareProps:
			{
				ComparePropsRestriction comparePropsRestriction = restriction as ComparePropsRestriction;
				if (RestrictionHelper.IsKnownRestrictionConversionPropertyId(comparePropsRestriction.Property1.PropertyId, viewType) || RestrictionHelper.IsKnownRestrictionConversionPropertyId(comparePropsRestriction.Property2.PropertyId, viewType))
				{
					PropertyTag property = convertPropertyTag(comparePropsRestriction.Property1, viewType);
					PropertyTag property2 = convertPropertyTag(comparePropsRestriction.Property2, viewType);
					restriction = new ComparePropsRestriction(comparePropsRestriction.RelationOperator, property, property2);
					return true;
				}
				return false;
			}
			case RestrictionType.BitMask:
				return false;
			case RestrictionType.Size:
			{
				SizeRestriction sizeRestriction = restriction as SizeRestriction;
				if (RestrictionHelper.IsKnownRestrictionConversionPropertyId(sizeRestriction.PropertyTag.PropertyId, viewType))
				{
					throw new RopExecutionException(string.Format("Property {0} is not supported for SizeRestriction.", sizeRestriction.PropertyTag), (ErrorCode)2147746071U);
				}
				return false;
			}
			case RestrictionType.Exists:
			{
				ExistsRestriction existsRestriction = restriction as ExistsRestriction;
				if (RestrictionHelper.IsKnownRestrictionConversionPropertyId(existsRestriction.PropertyTag.PropertyId, viewType))
				{
					throw new RopExecutionException(string.Format("Property {0} is not supported for ExistsRestriction.", existsRestriction.PropertyTag), (ErrorCode)2147746071U);
				}
				return false;
			}
			case RestrictionType.SubRestriction:
			{
				SubRestriction subRestriction = restriction as SubRestriction;
				Restriction childRestriction2 = subRestriction.ChildRestriction;
				if (RestrictionHelper.ConvertRestriction(session, convertPropertyTag, convertPropertyValue, ref childRestriction2, viewType))
				{
					restriction = new SubRestriction(subRestriction.SubRestrictionType, childRestriction2);
					return true;
				}
				return false;
			}
			case RestrictionType.Comment:
			{
				CommentRestriction commentRestriction = restriction as CommentRestriction;
				Restriction childRestriction3 = commentRestriction.ChildRestriction;
				if (RestrictionHelper.ConvertRestriction(session, convertPropertyTag, convertPropertyValue, ref childRestriction3, viewType))
				{
					restriction = new CommentRestriction(commentRestriction.PropertyValues, childRestriction3);
					return true;
				}
				return false;
			}
			case RestrictionType.Count:
			{
				CountRestriction countRestriction = restriction as CountRestriction;
				Restriction childRestriction4 = countRestriction.ChildRestriction;
				if (RestrictionHelper.ConvertRestriction(session, convertPropertyTag, convertPropertyValue, ref childRestriction4, viewType))
				{
					restriction = new CountRestriction(countRestriction.Count, childRestriction4);
					return true;
				}
				return false;
			}
			case (RestrictionType)12U:
				break;
			case RestrictionType.Near:
			{
				NearRestriction nearRestriction = restriction as NearRestriction;
				Restriction childRestriction5 = nearRestriction.ChildRestriction;
				if (RestrictionHelper.ConvertRestriction(session, convertPropertyTag, convertPropertyValue, ref childRestriction5, viewType))
				{
					restriction = new NearRestriction(nearRestriction.Distance, nearRestriction.Ordered, childRestriction5 as AndRestriction);
					return true;
				}
				return false;
			}
			default:
				switch (restrictionType)
				{
				case RestrictionType.True:
				case RestrictionType.False:
					return false;
				}
				break;
			}
			throw new RopExecutionException(string.Format("Found invalid restriction type: {0}", restriction.RestrictionType), (ErrorCode)2147746562U);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000861B File Offset: 0x0000681B
		private static PropertyTag ConvertPropertyTagFromClient(PropertyTag propertyTag, ViewType viewType)
		{
			if ((RestrictionHelper.IsKnownRestrictionServerIdPropertyId(propertyTag.PropertyId, viewType) || propertyTag.PropertyId == PropertyId.InstanceKey) && propertyTag.PropertyType == PropertyType.ServerId)
			{
				return new PropertyTag(propertyTag.PropertyId, PropertyType.Binary);
			}
			return propertyTag;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000865C File Offset: 0x0000685C
		private static void ConvertPropertyValueFromClient(StoreSession session, ref PropertyValue propertyValue, ViewType viewType)
		{
			if (RestrictionHelper.IsKnownRestrictionServerIdPropertyId(propertyValue.PropertyTag.PropertyId, viewType))
			{
				PropertyTag propertyTag = propertyValue.PropertyTag;
				if (propertyTag.PropertyType == PropertyType.ServerId)
				{
					propertyTag = new PropertyTag(propertyTag.PropertyId, PropertyType.Binary);
					propertyValue = new PropertyValue(propertyTag, ServerIdConverter.MakeEntryIdFromServerId(session, propertyValue.GetValueAssert<byte[]>()));
					return;
				}
			}
			else if (propertyValue.PropertyTag.PropertyId == PropertyId.InstanceKey)
			{
				PropertyTag propertyTag2 = propertyValue.PropertyTag;
				if (propertyTag2.PropertyType == PropertyType.ServerId)
				{
					propertyTag2 = new PropertyTag(propertyTag2.PropertyId, PropertyType.Binary);
					propertyValue = new PropertyValue(propertyTag2, ServerIdConverter.MakeInstanceKeyFromServerId(propertyValue.GetValueAssert<byte[]>()));
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008717 File Offset: 0x00006917
		private static PropertyTag ConvertPropertyTagToClient(PropertyTag propertyTag, ViewType viewType)
		{
			if ((RestrictionHelper.IsKnownRestrictionServerIdPropertyId(propertyTag.PropertyId, viewType) || propertyTag.PropertyId == PropertyId.InstanceKey) && propertyTag.PropertyType == PropertyType.Binary)
			{
				return new PropertyTag(propertyTag.PropertyId, PropertyType.ServerId);
			}
			return propertyTag;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008758 File Offset: 0x00006958
		private static void ConvertPropertyValueToClient(StoreSession session, ref PropertyValue propertyValue, ViewType viewType)
		{
			if (RestrictionHelper.IsKnownRestrictionServerIdPropertyId(propertyValue.PropertyTag.PropertyId, viewType))
			{
				PropertyTag propertyTag = propertyValue.PropertyTag;
				if (propertyTag.PropertyType == PropertyType.Binary)
				{
					propertyTag = new PropertyTag(propertyTag.PropertyId, PropertyType.ServerId);
					propertyValue = new PropertyValue(propertyTag, ServerIdConverter.MakeServerIdFromEntryId(session, propertyValue.GetValueAssert<byte[]>()));
					return;
				}
			}
			else if (propertyValue.PropertyTag.PropertyId == PropertyId.InstanceKey)
			{
				PropertyTag propertyTag2 = propertyValue.PropertyTag;
				if (propertyTag2.PropertyType == PropertyType.Binary)
				{
					propertyTag2 = new PropertyTag(propertyTag2.PropertyId, PropertyType.ServerId);
					propertyValue = new PropertyValue(propertyTag2, ServerIdConverter.MakeServerIdFromInstanceKey(propertyValue.GetValueAssert<byte[]>()));
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008828 File Offset: 0x00006A28
		public static void ConvertRestrictionToClient(StoreSession session, ref Restriction restriction, ViewType viewType)
		{
			if (restriction == null)
			{
				return;
			}
			RestrictionHelper.ConvertRestriction(session, (PropertyTag _propertyTag, ViewType _viewType) => RestrictionHelper.ConvertPropertyTagToClient(_propertyTag, _viewType), delegate(StoreSession _storeSession, ref PropertyValue _propertyValue, ViewType _viewType)
			{
				RestrictionHelper.ConvertPropertyValueToClient(_storeSession, ref _propertyValue, _viewType);
			}, ref restriction, viewType);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008890 File Offset: 0x00006A90
		public static void ConvertRestrictionFromClient(StoreSession session, ref Restriction restriction, ViewType viewType)
		{
			if (restriction == null)
			{
				return;
			}
			RestrictionHelper.ConvertRestriction(session, (PropertyTag _propertyTag, ViewType _viewType) => RestrictionHelper.ConvertPropertyTagFromClient(_propertyTag, _viewType), delegate(StoreSession _storeSession, ref PropertyValue _propertyValue, ViewType _viewType)
			{
				RestrictionHelper.ConvertPropertyValueFromClient(_storeSession, ref _propertyValue, _viewType);
			}, ref restriction, viewType);
		}

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x060000E6 RID: 230
		private delegate PropertyTag ConvertPropertyTag(PropertyTag propertyTag, ViewType viewType);

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x060000EA RID: 234
		private delegate void ConvertPropertyValue(StoreSession session, ref PropertyValue propertyValue, ViewType viewType);
	}
}

using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200028A RID: 650
	internal sealed class ExtensibleQueueInfoSchema : PagedObjectSchema
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x00048D18 File Offset: 0x00046F18
		internal override bool IsBasicField(int field)
		{
			return ExtensibleQueueInfoSchema.FieldDescriptors[field].isBasic;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00048D28 File Offset: 0x00046F28
		internal override int GetFieldIndex(string fieldName)
		{
			int result;
			if (this.TryGetFieldIndex(fieldName, out result))
			{
				return result;
			}
			throw new QueueViewerException(QVErrorCode.QV_E_INVALID_FIELD_NAME);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00048D4C File Offset: 0x00046F4C
		internal override bool TryGetFieldIndex(string fieldName, out int index)
		{
			index = -1;
			for (int i = 0; i < ExtensibleQueueInfoSchema.FieldDescriptors.Length; i++)
			{
				if (PagedObjectSchema.CompareString(ExtensibleQueueInfoSchema.FieldDescriptors[i].Name, fieldName) == 0)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00048D88 File Offset: 0x00046F88
		internal override Type GetFieldType(int field)
		{
			return ExtensibleQueueInfoSchema.FieldDescriptors[field].Type;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00048D96 File Offset: 0x00046F96
		internal override string GetFieldName(int field)
		{
			return ExtensibleQueueInfoSchema.FieldDescriptors[field].Name;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00048DA4 File Offset: 0x00046FA4
		internal override ProviderPropertyDefinition GetFieldByName(string fieldName)
		{
			int num;
			if (this.TryGetFieldIndex(fieldName, out num))
			{
				return ExtensibleQueueInfoSchema.FieldDescriptors[num];
			}
			return null;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00048DC5 File Offset: 0x00046FC5
		internal override bool MatchField(int field, PagedDataObject pagedDataObject, object matchPattern, MatchOptions matchOptions)
		{
			return ExtensibleQueueInfoSchema.FieldDescriptors[field].matcher((ExtensibleQueueInfo)pagedDataObject, matchPattern, matchOptions);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00048DE1 File Offset: 0x00046FE1
		internal override int CompareField(int field, PagedDataObject pagedDataObject, object value)
		{
			return ExtensibleQueueInfoSchema.FieldDescriptors[field].comparer1((ExtensibleQueueInfo)pagedDataObject, value);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00048DFB File Offset: 0x00046FFB
		internal override int CompareField(int field, PagedDataObject object1, PagedDataObject object2)
		{
			return ExtensibleQueueInfoSchema.FieldDescriptors[field].comparer2((ExtensibleQueueInfo)object1, (ExtensibleQueueInfo)object2);
		}

		// Token: 0x04000D61 RID: 3425
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> Identity = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("Identity", typeof(QueueIdentity), QueueIdentity.Empty, true, (ExtensibleQueueInfo qi, object value) => QueueIdentity.Compare(qi.Identity, (ObjectId)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => QueueIdentity.Compare(qi1.Identity, qi2.Identity), (ExtensibleQueueInfo qi, object matchPattern, MatchOptions matchOptions) => ((QueueIdentity)qi.Identity).Match((QueueIdentity)matchPattern, matchOptions));

		// Token: 0x04000D62 RID: 3426
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000D63 RID: 3427
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000D64 RID: 3428
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> DeliveryType = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("DeliveryType", typeof(DeliveryType), Microsoft.Exchange.Data.DeliveryType.Undefined, true, (ExtensibleQueueInfo qi, object value) => string.Compare(qi.DeliveryType.ToString(), (value == null) ? null : ((DeliveryType)value).ToString(), StringComparison.CurrentCulture), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => string.Compare(qi1.DeliveryType.ToString(), qi2.DeliveryType.ToString(), StringComparison.CurrentCulture));

		// Token: 0x04000D65 RID: 3429
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> NextHopDomain = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("NextHopDomain", typeof(string), string.Empty, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareString(qi.NextHopDomain, (string)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareString(qi1.NextHopDomain, qi2.NextHopDomain), (ExtensibleQueueInfo qi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(qi.NextHopDomain, (string)matchPattern, matchOptions));

		// Token: 0x04000D66 RID: 3430
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> NextHopConnector = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("NextHopConnector", typeof(Guid), Guid.Empty, true, (ExtensibleQueueInfo qi, object value) => qi.NextHopConnector.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.NextHopConnector.CompareTo(qi2.NextHopConnector));

		// Token: 0x04000D67 RID: 3431
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> Status = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("Status", typeof(QueueStatus), QueueStatus.None, true, (ExtensibleQueueInfo qi, object value) => string.Compare(qi.Status.ToString(), (value == null) ? null : ((QueueStatus)value).ToString(), StringComparison.CurrentCulture), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => string.Compare(qi1.Status.ToString(), qi2.Status.ToString(), StringComparison.CurrentCulture));

		// Token: 0x04000D68 RID: 3432
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> MessageCount = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("MessageCount", typeof(int), 0, true, (ExtensibleQueueInfo qi, object value) => qi.MessageCount.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.MessageCount.CompareTo(qi2.MessageCount));

		// Token: 0x04000D69 RID: 3433
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> MessageCountsPerPriority = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("MessageCountsPerPriority", typeof(int[]), null, true, delegate(ExtensibleQueueInfo qi, object value)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, delegate(ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D6A RID: 3434
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> PriorityDescriptions = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("PriorityDescriptions", typeof(string[]), null, true, delegate(ExtensibleQueueInfo qi, object value)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, delegate(ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D6B RID: 3435
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> LastError = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("LastError", typeof(string), null, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareString(qi.LastError, (string)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareString(qi1.LastError, qi2.LastError), (ExtensibleQueueInfo qi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(qi.LastError, (string)matchPattern, matchOptions));

		// Token: 0x04000D6C RID: 3436
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> RetryCount = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("RetryCount", typeof(int), 0, true, (ExtensibleQueueInfo qi, object value) => qi.RetryCount.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.RetryCount.CompareTo(qi2.RetryCount));

		// Token: 0x04000D6D RID: 3437
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> LastRetryTime = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("LastRetryTime", typeof(DateTime?), null, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareDateTimeNullable(qi.LastRetryTime, (DateTime?)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareDateTimeNullable(qi1.LastRetryTime, qi2.LastRetryTime));

		// Token: 0x04000D6E RID: 3438
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> NextRetryTime = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("NextRetryTime", typeof(DateTime?), null, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareDateTimeNullable(qi.NextRetryTime, (DateTime?)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareDateTimeNullable(qi1.NextRetryTime, qi2.NextRetryTime));

		// Token: 0x04000D6F RID: 3439
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> FirstRetryTime = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("FirstRetryTime", typeof(DateTime?), null, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareDateTimeNullable(qi.FirstRetryTime, (DateTime?)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareDateTimeNullable(qi1.FirstRetryTime, qi2.FirstRetryTime));

		// Token: 0x04000D70 RID: 3440
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> TlsDomain = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("TlsDomain", typeof(string), string.Empty, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareString(qi.TlsDomain, (string)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareString(qi1.TlsDomain, qi2.TlsDomain), (ExtensibleQueueInfo qi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(qi.TlsDomain, (string)matchPattern, matchOptions), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D71 RID: 3441
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> RiskLevel = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("RiskLevel", typeof(RiskLevel), Microsoft.Exchange.Data.Transport.RiskLevel.Normal, true, (ExtensibleQueueInfo qi, object value) => string.Compare(qi.RiskLevel.ToString(), (value == null) ? null : ((RiskLevel)value).ToString(), StringComparison.OrdinalIgnoreCase), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => string.Compare(qi1.RiskLevel.ToString(), qi2.RiskLevel.ToString(), StringComparison.OrdinalIgnoreCase), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D72 RID: 3442
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> OutboundIPPool = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("OutboundIPPool", typeof(int), 0, true, (ExtensibleQueueInfo qi, object value) => qi.OutboundIPPool.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.OutboundIPPool.CompareTo(qi2.OutboundIPPool), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D73 RID: 3443
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> DeferredMessageCount = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("DeferredMessageCount", typeof(int), 0, true, (ExtensibleQueueInfo qi, object value) => qi.DeferredMessageCount.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.DeferredMessageCount.CompareTo(qi2.DeferredMessageCount), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D74 RID: 3444
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> DeferredMessageCountsPerPriority = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("DeferredMessageCountsPerPriority", typeof(int[]), null, true, delegate(ExtensibleQueueInfo qi, object value)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, delegate(ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D75 RID: 3445
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> LockedMessageCount = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("LockedMessageCount", typeof(int), 0, true, (ExtensibleQueueInfo qi, object value) => qi.LockedMessageCount.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.LockedMessageCount.CompareTo(qi2.LockedMessageCount), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D76 RID: 3446
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> NextHopCategory = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("NextHopCategory", typeof(NextHopCategory), Microsoft.Exchange.Data.Transport.NextHopCategory.Internal, true, (ExtensibleQueueInfo qi, object value) => string.Compare(qi.NextHopCategory.ToString(), (value == null) ? null : ((NextHopCategory)value).ToString(), StringComparison.OrdinalIgnoreCase), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => string.Compare(qi1.NextHopCategory.ToString(), qi2.NextHopCategory.ToString(), StringComparison.OrdinalIgnoreCase), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D77 RID: 3447
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> IncomingRate = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("IncomingRate", typeof(double), 0.0, true, (ExtensibleQueueInfo qi, object value) => qi.IncomingRate.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.IncomingRate.CompareTo(qi2.IncomingRate), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D78 RID: 3448
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> OutgoingRate = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("OutgoingRate", typeof(double), 0.0, true, (ExtensibleQueueInfo qi, object value) => qi.OutgoingRate.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.OutgoingRate.CompareTo(qi2.OutgoingRate), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D79 RID: 3449
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> Velocity = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("Velocity", typeof(double), 0.0, true, (ExtensibleQueueInfo qi, object value) => qi.Velocity.CompareTo(value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => qi1.Velocity.CompareTo(qi2.Velocity), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000D7A RID: 3450
		public static readonly QueueViewerPropertyDefinition<ExtensibleQueueInfo> OverrideSource = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>("OverrideSource", typeof(string), string.Empty, true, (ExtensibleQueueInfo qi, object value) => PagedObjectSchema.CompareString(qi.OverrideSource, (string)value), (ExtensibleQueueInfo qi1, ExtensibleQueueInfo qi2) => PagedObjectSchema.CompareString(qi1.OverrideSource, qi2.OverrideSource), (ExtensibleQueueInfo qi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(qi.OverrideSource, (string)matchPattern, matchOptions));

		// Token: 0x04000D7B RID: 3451
		private static QueueViewerPropertyDefinition<ExtensibleQueueInfo>[] FieldDescriptors = new QueueViewerPropertyDefinition<ExtensibleQueueInfo>[]
		{
			ExtensibleQueueInfoSchema.Identity,
			ExtensibleQueueInfoSchema.DeliveryType,
			ExtensibleQueueInfoSchema.NextHopDomain,
			ExtensibleQueueInfoSchema.NextHopConnector,
			ExtensibleQueueInfoSchema.Status,
			ExtensibleQueueInfoSchema.MessageCount,
			ExtensibleQueueInfoSchema.MessageCountsPerPriority,
			ExtensibleQueueInfoSchema.PriorityDescriptions,
			ExtensibleQueueInfoSchema.LastError,
			ExtensibleQueueInfoSchema.RetryCount,
			ExtensibleQueueInfoSchema.LastRetryTime,
			ExtensibleQueueInfoSchema.NextRetryTime,
			ExtensibleQueueInfoSchema.FirstRetryTime,
			ExtensibleQueueInfoSchema.TlsDomain,
			ExtensibleQueueInfoSchema.DeferredMessageCount,
			ExtensibleQueueInfoSchema.DeferredMessageCountsPerPriority,
			ExtensibleQueueInfoSchema.LockedMessageCount,
			ExtensibleQueueInfoSchema.RiskLevel,
			ExtensibleQueueInfoSchema.NextHopCategory,
			ExtensibleQueueInfoSchema.IncomingRate,
			ExtensibleQueueInfoSchema.OutgoingRate,
			ExtensibleQueueInfoSchema.Velocity,
			ExtensibleQueueInfoSchema.OutboundIPPool,
			ExtensibleQueueInfoSchema.OverrideSource
		};
	}
}

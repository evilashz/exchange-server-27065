using System;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200027A RID: 634
	internal sealed class QueueInfoSchema : PagedObjectSchema
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x00044292 File Offset: 0x00042492
		internal override bool IsBasicField(int field)
		{
			return QueueInfoSchema.FieldDescriptors[field].isBasic;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000442A0 File Offset: 0x000424A0
		internal override int GetFieldIndex(string fieldName)
		{
			int result;
			if (this.TryGetFieldIndex(fieldName, out result))
			{
				return result;
			}
			throw new QueueViewerException(QVErrorCode.QV_E_INVALID_FIELD_NAME);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000442C4 File Offset: 0x000424C4
		internal override bool TryGetFieldIndex(string fieldName, out int index)
		{
			for (int i = 0; i < MessageInfoSchema.FieldDescriptors.Length; i++)
			{
				if (PagedObjectSchema.CompareString(QueueInfoSchema.FieldDescriptors[i].Name, fieldName) == 0)
				{
					index = i;
					return true;
				}
			}
			index = -1;
			return false;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00044300 File Offset: 0x00042500
		internal override Type GetFieldType(int field)
		{
			return QueueInfoSchema.FieldDescriptors[field].Type;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0004430E File Offset: 0x0004250E
		internal override string GetFieldName(int field)
		{
			return QueueInfoSchema.FieldDescriptors[field].Name;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0004431C File Offset: 0x0004251C
		internal override ProviderPropertyDefinition GetFieldByName(string fieldName)
		{
			int num;
			if (this.TryGetFieldIndex(fieldName, out num))
			{
				return QueueInfoSchema.FieldDescriptors[num];
			}
			return null;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0004433D File Offset: 0x0004253D
		internal override bool MatchField(int field, PagedDataObject pagedDataObject, object matchPattern, MatchOptions matchOptions)
		{
			return QueueInfoSchema.FieldDescriptors[field].matcher((QueueInfo)pagedDataObject, matchPattern, matchOptions);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00044359 File Offset: 0x00042559
		internal override int CompareField(int field, PagedDataObject pagedDataObject, object value)
		{
			return QueueInfoSchema.FieldDescriptors[field].comparer1((QueueInfo)pagedDataObject, value);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00044373 File Offset: 0x00042573
		internal override int CompareField(int field, PagedDataObject object1, PagedDataObject object2)
		{
			return QueueInfoSchema.FieldDescriptors[field].comparer2((QueueInfo)object1, (QueueInfo)object2);
		}

		// Token: 0x04000C92 RID: 3218
		public static readonly QueueViewerPropertyDefinition<QueueInfo> Identity = new QueueViewerPropertyDefinition<QueueInfo>("Identity", typeof(QueueIdentity), QueueIdentity.Empty, true, (QueueInfo qi, object value) => QueueIdentity.Compare(qi.Identity, (ObjectId)value), (QueueInfo qi1, QueueInfo qi2) => QueueIdentity.Compare(qi1.Identity, qi2.Identity), (QueueInfo qi, object matchPattern, MatchOptions matchOptions) => ((QueueIdentity)qi.Identity).Match((QueueIdentity)matchPattern, matchOptions));

		// Token: 0x04000C93 RID: 3219
		public static readonly QueueViewerPropertyDefinition<QueueInfo> DeliveryType = new QueueViewerPropertyDefinition<QueueInfo>("DeliveryType", typeof(DeliveryType), Microsoft.Exchange.Data.DeliveryType.Undefined, true, (QueueInfo qi, object value) => qi.DeliveryType.CompareTo(value), (QueueInfo qi1, QueueInfo qi2) => qi1.DeliveryType.CompareTo(qi2.DeliveryType));

		// Token: 0x04000C94 RID: 3220
		public static readonly QueueViewerPropertyDefinition<QueueInfo> NextHopDomain = new QueueViewerPropertyDefinition<QueueInfo>("NextHopDomain", typeof(string), string.Empty, true, (QueueInfo qi, object value) => PagedObjectSchema.CompareString(qi.NextHopDomain, (string)value), (QueueInfo qi1, QueueInfo qi2) => PagedObjectSchema.CompareString(qi1.NextHopDomain, qi2.NextHopDomain), (QueueInfo qi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(qi.NextHopDomain, (string)matchPattern, matchOptions));

		// Token: 0x04000C95 RID: 3221
		public static readonly QueueViewerPropertyDefinition<QueueInfo> NextHopConnector = new QueueViewerPropertyDefinition<QueueInfo>("NextHopConnector", typeof(Guid), Guid.Empty, true, (QueueInfo qi, object value) => qi.NextHopConnector.CompareTo(value), (QueueInfo qi1, QueueInfo qi2) => qi1.NextHopConnector.CompareTo(qi2.NextHopConnector));

		// Token: 0x04000C96 RID: 3222
		public static readonly QueueViewerPropertyDefinition<QueueInfo> Status = new QueueViewerPropertyDefinition<QueueInfo>("Status", typeof(QueueStatus), QueueStatus.None, true, (QueueInfo qi, object value) => qi.Status.CompareTo(value), (QueueInfo qi1, QueueInfo qi2) => qi1.Status.CompareTo(qi2.Status));

		// Token: 0x04000C97 RID: 3223
		public static readonly QueueViewerPropertyDefinition<QueueInfo> MessageCount = new QueueViewerPropertyDefinition<QueueInfo>("MessageCount", typeof(int), 0, true, (QueueInfo qi, object value) => qi.MessageCount.CompareTo(value), (QueueInfo qi1, QueueInfo qi2) => qi1.MessageCount.CompareTo(qi2.MessageCount));

		// Token: 0x04000C98 RID: 3224
		public static readonly QueueViewerPropertyDefinition<QueueInfo> LastError = new QueueViewerPropertyDefinition<QueueInfo>("LastError", typeof(string), string.Empty, true, (QueueInfo qi, object value) => PagedObjectSchema.CompareString(qi.LastError, (string)value), (QueueInfo qi1, QueueInfo qi2) => PagedObjectSchema.CompareString(qi1.LastError, qi2.LastError), (QueueInfo qi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(qi.LastError, (string)matchPattern, matchOptions));

		// Token: 0x04000C99 RID: 3225
		public static readonly QueueViewerPropertyDefinition<QueueInfo> LastRetryTime = new QueueViewerPropertyDefinition<QueueInfo>("LastRetryTime", typeof(DateTime?), null, true, (QueueInfo qi, object value) => PagedObjectSchema.CompareDateTimeNullable(qi.LastRetryTime, (DateTime?)value), (QueueInfo qi1, QueueInfo qi2) => PagedObjectSchema.CompareDateTimeNullable(qi1.LastRetryTime, qi2.LastRetryTime));

		// Token: 0x04000C9A RID: 3226
		public static readonly QueueViewerPropertyDefinition<QueueInfo> NextRetryTime = new QueueViewerPropertyDefinition<QueueInfo>("NextRetryTime", typeof(DateTime?), null, true, (QueueInfo qi, object value) => PagedObjectSchema.CompareDateTimeNullable(qi.NextRetryTime, (DateTime?)value), (QueueInfo qi1, QueueInfo qi2) => PagedObjectSchema.CompareDateTimeNullable(qi1.NextRetryTime, qi2.NextRetryTime));

		// Token: 0x04000C9B RID: 3227
		private static QueueViewerPropertyDefinition<QueueInfo>[] FieldDescriptors = new QueueViewerPropertyDefinition<QueueInfo>[]
		{
			QueueInfoSchema.Identity,
			QueueInfoSchema.DeliveryType,
			QueueInfoSchema.NextHopDomain,
			QueueInfoSchema.NextHopConnector,
			QueueInfoSchema.Status,
			QueueInfoSchema.MessageCount,
			QueueInfoSchema.LastError,
			QueueInfoSchema.LastRetryTime,
			QueueInfoSchema.NextRetryTime
		};
	}
}

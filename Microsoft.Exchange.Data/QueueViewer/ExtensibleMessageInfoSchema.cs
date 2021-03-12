using System;
using System.Net;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200027F RID: 639
	internal sealed class ExtensibleMessageInfoSchema : PagedObjectSchema
	{
		// Token: 0x060015FC RID: 5628 RVA: 0x00044FD0 File Offset: 0x000431D0
		internal override bool IsBasicField(int field)
		{
			return ExtensibleMessageInfoSchema.FieldDescriptors[field].isBasic;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00044FE0 File Offset: 0x000431E0
		internal override int GetFieldIndex(string fieldName)
		{
			int result;
			if (this.TryGetFieldIndex(fieldName, out result))
			{
				return result;
			}
			throw new QueueViewerException(QVErrorCode.QV_E_INVALID_FIELD_NAME);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00045004 File Offset: 0x00043204
		internal override bool TryGetFieldIndex(string fieldName, out int index)
		{
			for (int i = 0; i < ExtensibleMessageInfoSchema.FieldDescriptors.Length; i++)
			{
				if (PagedObjectSchema.CompareString(ExtensibleMessageInfoSchema.FieldDescriptors[i].Name, fieldName) == 0)
				{
					index = i;
					return true;
				}
			}
			index = -1;
			return false;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00045040 File Offset: 0x00043240
		internal override Type GetFieldType(int field)
		{
			return ExtensibleMessageInfoSchema.FieldDescriptors[field].Type;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004504E File Offset: 0x0004324E
		internal override string GetFieldName(int field)
		{
			return ExtensibleMessageInfoSchema.FieldDescriptors[field].Name;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004505C File Offset: 0x0004325C
		internal override ProviderPropertyDefinition GetFieldByName(string fieldName)
		{
			int num;
			if (this.TryGetFieldIndex(fieldName, out num))
			{
				return ExtensibleMessageInfoSchema.FieldDescriptors[num];
			}
			return null;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004507D File Offset: 0x0004327D
		internal override bool MatchField(int field, PagedDataObject pagedDataObject, object matchPattern, MatchOptions matchOptions)
		{
			return ExtensibleMessageInfoSchema.FieldDescriptors[field].matcher((ExtensibleMessageInfo)pagedDataObject, matchPattern, matchOptions);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00045099 File Offset: 0x00043299
		internal override int CompareField(int field, PagedDataObject pagedDataObject, object value)
		{
			return ExtensibleMessageInfoSchema.FieldDescriptors[field].comparer1((ExtensibleMessageInfo)pagedDataObject, value);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000450B3 File Offset: 0x000432B3
		internal override int CompareField(int field, PagedDataObject object1, PagedDataObject object2)
		{
			return ExtensibleMessageInfoSchema.FieldDescriptors[field].comparer2((ExtensibleMessageInfo)object1, (ExtensibleMessageInfo)object2);
		}

		// Token: 0x04000CC2 RID: 3266
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Identity = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Identity", typeof(MessageIdentity), MessageIdentity.Empty, false, (ExtensibleMessageInfo mi, object value) => MessageIdentity.Compare(mi.Identity, (ObjectId)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => MessageIdentity.Compare(mi1.Identity, mi2.Identity), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => ((MessageIdentity)mi.Identity).Match((MessageIdentity)matchPattern, matchOptions));

		// Token: 0x04000CC3 RID: 3267
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000CC4 RID: 3268
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000CC5 RID: 3269
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Subject = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Subject", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.Subject, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.Subject, mi2.Subject), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.Subject, (string)matchPattern, matchOptions));

		// Token: 0x04000CC6 RID: 3270
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> InternetMessageId = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("InternetMessageId", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.InternetMessageId, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.InternetMessageId, mi2.InternetMessageId), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.InternetMessageId, (string)matchPattern, matchOptions));

		// Token: 0x04000CC7 RID: 3271
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> FromAddress = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("FromAddress", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.FromAddress, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.FromAddress, mi2.FromAddress), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.FromAddress, (string)matchPattern, matchOptions));

		// Token: 0x04000CC8 RID: 3272
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Status = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Status", typeof(MessageStatus), MessageStatus.None, false, (ExtensibleMessageInfo mi, object value) => mi.Status.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.Status.CompareTo(mi2.Status));

		// Token: 0x04000CC9 RID: 3273
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Size = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Size", typeof(ByteQuantifiedSize), ByteQuantifiedSize.FromBytes(0UL), true, (ExtensibleMessageInfo mi, object value) => ((IComparable)mi.Size).CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.Size.CompareTo(mi2.Size));

		// Token: 0x04000CCA RID: 3274
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> MessageSourceName = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("MessageSourceName", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.MessageSourceName, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.MessageSourceName, mi2.MessageSourceName), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.MessageSourceName, (string)matchPattern, matchOptions));

		// Token: 0x04000CCB RID: 3275
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> SourceIP = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("SourceIP", typeof(IPAddress), IPAddress.None, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareIPAddress(mi.SourceIP, (IPAddress)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareIPAddress(mi1.SourceIP, mi2.SourceIP));

		// Token: 0x04000CCC RID: 3276
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> SCL = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("SCL", typeof(int), 0, true, (ExtensibleMessageInfo mi, object value) => mi.SCL.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.SCL.CompareTo(mi2.SCL));

		// Token: 0x04000CCD RID: 3277
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> DateReceived = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("DateReceived", typeof(DateTime), DateTime.MinValue, true, (ExtensibleMessageInfo mi, object value) => mi.DateReceived.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.DateReceived.CompareTo(mi2.DateReceived));

		// Token: 0x04000CCE RID: 3278
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> ExpirationTime = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("ExpirationTime", typeof(DateTime?), null, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareDateTimeNullable(mi.ExpirationTime, (DateTime?)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareDateTimeNullable(mi1.ExpirationTime, mi2.ExpirationTime));

		// Token: 0x04000CCF RID: 3279
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> LastError = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("LastError", typeof(string), null, false, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.LastError, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.LastError, mi2.LastError), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.LastError, (string)matchPattern, matchOptions));

		// Token: 0x04000CD0 RID: 3280
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> LastErrorCode = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("LastErrorCode", typeof(int), 0, false, (ExtensibleMessageInfo mi, object value) => mi.LastErrorCode.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.LastErrorCode.CompareTo(mi2.LastErrorCode));

		// Token: 0x04000CD1 RID: 3281
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> RetryCount = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("RetryCount", typeof(int), 0, false, (ExtensibleMessageInfo mi, object value) => mi.RetryCount.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.RetryCount.CompareTo(mi2.RetryCount));

		// Token: 0x04000CD2 RID: 3282
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Queue = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Queue", typeof(QueueIdentity), QueueIdentity.Empty, false, (ExtensibleMessageInfo mi, object value) => QueueIdentity.Compare(mi.Queue, (ObjectId)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => QueueIdentity.Compare(mi1.Queue, mi2.Queue), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => mi.Queue.Match((QueueIdentity)matchPattern, matchOptions));

		// Token: 0x04000CD3 RID: 3283
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Recipients = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Recipients", typeof(RecipientInfo[]), null, false, delegate(ExtensibleMessageInfo mi, object value)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, delegate(ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		});

		// Token: 0x04000CD4 RID: 3284
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> ComponentLatency = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("ComponentLatency", typeof(ComponentLatencyInfo[]), null, false, delegate(ExtensibleMessageInfo mi, object value)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, delegate(ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CD5 RID: 3285
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> MessageLatency = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("MessageLatency", typeof(EnhancedTimeSpan), null, false, delegate(ExtensibleMessageInfo mi, object value)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, delegate(ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_COMPARISON_NOT_SUPPORTED);
		}, ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CD6 RID: 3286
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> DeferReason = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("DeferReason", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.DeferReason, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.DeferReason, mi2.DeferReason), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.DeferReason, (string)matchPattern, matchOptions), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CD7 RID: 3287
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> LockReason = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("LockReason", typeof(string), string.Empty, false, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.LockReason, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.LockReason, mi2.LockReason), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.LockReason, (string)matchPattern, matchOptions), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CD8 RID: 3288
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> IsProbeMessage = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("IsProbeMessage", typeof(bool), false, false, delegate(ExtensibleMessageInfo mi, object value)
		{
			if (mi.IsProbeMessage == (bool)value)
			{
				return 0;
			}
			return 1;
		}, delegate(ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2)
		{
			if (mi1.IsProbeMessage == mi2.IsProbeMessage)
			{
				return 0;
			}
			return 1;
		}, ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CD9 RID: 3289
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Priority = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Priority", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.Priority, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.Priority, mi2.Priority), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.Priority, (string)matchPattern, matchOptions), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CDA RID: 3290
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> ExternalDirectoryOrganizationId = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("ExternalDirectoryOrganizationId", typeof(Guid), Guid.Empty, true, (ExtensibleMessageInfo mi, object value) => mi.ExternalDirectoryOrganizationId.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.ExternalDirectoryOrganizationId.CompareTo(mi2.ExternalDirectoryOrganizationId), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CDB RID: 3291
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> Directionality = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("Directionality", typeof(MailDirectionality), MailDirectionality.Undefined, true, (ExtensibleMessageInfo mi, object value) => mi.Directionality.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.Directionality.CompareTo(mi2.Directionality), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CDC RID: 3292
		internal static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> OutboundIPPool = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("OutboundIPPool", typeof(int), 0, true, (ExtensibleMessageInfo mi, object value) => mi.OutboundIPPool.CompareTo(value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => mi1.OutboundIPPool.CompareTo(mi2.OutboundIPPool), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CDD RID: 3293
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> OriginalFromAddress = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("OriginalFromAddress", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.OriginalFromAddress, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.OriginalFromAddress, mi2.OriginalFromAddress), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.OriginalFromAddress, (string)matchPattern, matchOptions), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CDE RID: 3294
		public static readonly QueueViewerPropertyDefinition<ExtensibleMessageInfo> AccountForest = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>("AccountForest", typeof(string), string.Empty, true, (ExtensibleMessageInfo mi, object value) => PagedObjectSchema.CompareString(mi.AccountForest, (string)value), (ExtensibleMessageInfo mi1, ExtensibleMessageInfo mi2) => PagedObjectSchema.CompareString(mi1.AccountForest, mi2.AccountForest), (ExtensibleMessageInfo mi, object matchPattern, MatchOptions matchOptions) => PagedObjectSchema.MatchString(mi.AccountForest, (string)matchPattern, matchOptions), ExchangeObjectVersion.Exchange2010);

		// Token: 0x04000CDF RID: 3295
		internal static QueueViewerPropertyDefinition<ExtensibleMessageInfo>[] FieldDescriptors = new QueueViewerPropertyDefinition<ExtensibleMessageInfo>[]
		{
			ExtensibleMessageInfoSchema.Identity,
			ExtensibleMessageInfoSchema.Subject,
			ExtensibleMessageInfoSchema.InternetMessageId,
			ExtensibleMessageInfoSchema.FromAddress,
			ExtensibleMessageInfoSchema.Status,
			ExtensibleMessageInfoSchema.Size,
			ExtensibleMessageInfoSchema.MessageSourceName,
			ExtensibleMessageInfoSchema.SourceIP,
			ExtensibleMessageInfoSchema.SCL,
			ExtensibleMessageInfoSchema.DateReceived,
			ExtensibleMessageInfoSchema.ExpirationTime,
			ExtensibleMessageInfoSchema.LastError,
			ExtensibleMessageInfoSchema.LastErrorCode,
			ExtensibleMessageInfoSchema.RetryCount,
			ExtensibleMessageInfoSchema.Queue,
			ExtensibleMessageInfoSchema.Recipients,
			ExtensibleMessageInfoSchema.ComponentLatency,
			ExtensibleMessageInfoSchema.MessageLatency,
			ExtensibleMessageInfoSchema.DeferReason,
			ExtensibleMessageInfoSchema.LockReason,
			ExtensibleMessageInfoSchema.Priority,
			ExtensibleMessageInfoSchema.IsProbeMessage,
			ExtensibleMessageInfoSchema.OutboundIPPool,
			ExtensibleMessageInfoSchema.ExternalDirectoryOrganizationId,
			ExtensibleMessageInfoSchema.Directionality,
			ExtensibleMessageInfoSchema.OriginalFromAddress,
			ExtensibleMessageInfoSchema.AccountForest
		};
	}
}

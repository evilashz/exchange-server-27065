using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Rpc.QueueViewer;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.QueueViewer
{
	// Token: 0x02000361 RID: 865
	internal class QueueViewerClient<ObjectType> : VersionedQueueViewerClient where ObjectType : PagedDataObject
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x000912D5 File Offset: 0x0008F4D5
		public QueueViewerClient(string serverName) : base(serverName)
		{
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000912E0 File Offset: 0x0008F4E0
		public ObjectType[] GetPropertyBagBasedQueueViewerObjectPageCustomSerialization(QueueViewerInputObject inputObject, ref int totalCount, ref int pageOffset)
		{
			QVObjectType qvobjectType;
			if (typeof(ObjectType) == typeof(ExtensibleMessageInfo))
			{
				qvobjectType = QVObjectType.MessageInfo;
			}
			else
			{
				qvobjectType = QVObjectType.QueueInfo;
			}
			byte[] inputObjectBytes = Serialization.InputObjectToBytes(inputObject);
			byte[] propertyBagBasedQueueViewerObjectPageCustomSerialization = base.GetPropertyBagBasedQueueViewerObjectPageCustomSerialization(qvobjectType, inputObjectBytes, ref totalCount, ref pageOffset);
			ObjectType[] result;
			try
			{
				if (qvobjectType == QVObjectType.MessageInfo)
				{
					ExtensibleMessageInfo[] array = Serialization.BytesToPagedMessageObject(propertyBagBasedQueueViewerObjectPageCustomSerialization);
					result = (ObjectType[])array;
				}
				else
				{
					ExtensibleQueueInfo[] array2 = Serialization.BytesToPagedQueueObject(propertyBagBasedQueueViewerObjectPageCustomSerialization);
					result = (ObjectType[])array2;
				}
			}
			catch (SerializationException)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_SERVER_DATA);
			}
			return result;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00091368 File Offset: 0x0008F568
		public ObjectType[] GetPropertyBagBasedQueueViewerObjectPage(QueueViewerInputObject inputObject, ref int totalCount, ref int pageOffset)
		{
			QVObjectType objectType;
			if (typeof(ObjectType) == typeof(ExtensibleMessageInfo))
			{
				objectType = QVObjectType.MessageInfo;
			}
			else
			{
				objectType = QVObjectType.QueueInfo;
			}
			byte[] inputObjectBytes = Serialization.ObjectToBytes(inputObject);
			byte[] propertyBagBasedQueueViewerObjectPage = base.GetPropertyBagBasedQueueViewerObjectPage(objectType, inputObjectBytes, ref totalCount, ref pageOffset);
			ObjectType[] result;
			try
			{
				result = (ObjectType[])Serialization.BytesToObject(propertyBagBasedQueueViewerObjectPage);
			}
			catch (SerializationException)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_SERVER_DATA);
			}
			return result;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000913D4 File Offset: 0x0008F5D4
		public ObjectType[] GetQueueViewerObjectPage(QueryFilter queryFilter, SortOrderEntry[] sortOrder, bool searchForward, int pageSize, PagedDataObject bookmarkObject, int bookmarkIndex, bool includeBookmark, bool includeDetails, bool includeComponentLatencyInfo, ref int totalCount, ref int pageOffset)
		{
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			byte[] sortOrderBytes = Serialization.ObjectToBytes(sortOrder);
			byte[] bookmarkObjectBytes = Serialization.ObjectToBytes(bookmarkObject);
			QVObjectType objectType;
			if (typeof(ObjectType) == typeof(ExtensibleMessageInfo))
			{
				objectType = QVObjectType.MessageInfo;
			}
			else
			{
				objectType = QVObjectType.QueueInfo;
			}
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			TransportHelpers.AttemptAddToDictionary<uint, object>(mdbefPropertyCollection, 2483093515U, includeComponentLatencyInfo, null);
			byte[] queueViewerObjectPage = base.GetQueueViewerObjectPage(objectType, queryFilterBytes, sortOrderBytes, searchForward, pageSize, bookmarkObjectBytes, bookmarkIndex, includeBookmark, includeDetails, mdbefPropertyCollection.GetBytes(), ref totalCount, ref pageOffset);
			ObjectType[] result;
			try
			{
				if (typeof(ObjectType) == typeof(ExtensibleQueueInfo))
				{
					result = (this.GetExtensibleQueueInfo((QueueInfo[])Serialization.BytesToObject(queueViewerObjectPage)) as ObjectType[]);
				}
				else
				{
					result = (this.GetExtensibleMessageInfo((MessageInfo[])Serialization.BytesToObject(queueViewerObjectPage)) as ObjectType[]);
				}
			}
			catch (SerializationException)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_SERVER_DATA);
			}
			return result;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000914C4 File Offset: 0x0008F6C4
		public void FreezeQueue(QueueIdentity queueIdentity, QueryFilter queryFilter)
		{
			byte[] queueIdentityBytes = Serialization.ObjectToBytes(queueIdentity);
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			base.FreezeQueue(queueIdentityBytes, queryFilterBytes);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000914E8 File Offset: 0x0008F6E8
		public void UnfreezeQueue(QueueIdentity queueIdentity, QueryFilter queryFilter)
		{
			byte[] queueIdentityBytes = Serialization.ObjectToBytes(queueIdentity);
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			base.UnfreezeQueue(queueIdentityBytes, queryFilterBytes);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x0009150C File Offset: 0x0008F70C
		public void RetryQueue(QueueIdentity queueIdentity, QueryFilter queryFilter, bool resubmit)
		{
			byte[] queueIdentityBytes = Serialization.ObjectToBytes(queueIdentity);
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			base.RetryQueue(queueIdentityBytes, queryFilterBytes, resubmit);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00091530 File Offset: 0x0008F730
		public byte[] ReadMessageBody(MessageIdentity mailItemId, int position, int count)
		{
			byte[] mailItemIdBytes = Serialization.ObjectToBytes(mailItemId);
			return base.ReadMessageBody(mailItemIdBytes, position, count);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00091550 File Offset: 0x0008F750
		public void FreezeMessage(MessageIdentity mailItemId, QueryFilter queryFilter)
		{
			byte[] mailItemIdBytes = Serialization.ObjectToBytes(mailItemId);
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			base.FreezeMessage(mailItemIdBytes, queryFilterBytes);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00091574 File Offset: 0x0008F774
		public void UnfreezeMessage(MessageIdentity mailItemId, QueryFilter queryFilter)
		{
			byte[] mailItemIdBytes = Serialization.ObjectToBytes(mailItemId);
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			base.UnfreezeMessage(mailItemIdBytes, queryFilterBytes);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00091598 File Offset: 0x0008F798
		public void DeleteMessage(MessageIdentity mailItemId, QueryFilter queryFilter, bool withNDR)
		{
			byte[] mailItemIdBytes = Serialization.ObjectToBytes(mailItemId);
			byte[] queryFilterBytes = Serialization.ObjectToBytes(queryFilter);
			base.DeleteMessage(mailItemIdBytes, queryFilterBytes, withNDR);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000915BC File Offset: 0x0008F7BC
		public void RedirectMessage(MultiValuedProperty<Fqdn> targetServers)
		{
			byte[] targetServersBytes = Serialization.ObjectToBytes(targetServers);
			base.RedirectMessage(targetServersBytes);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000915D8 File Offset: 0x0008F7D8
		public void SetMessage(MessageIdentity mailItemId, QueryFilter queryFilter, ExtensibleMessageInfo properties, bool resubmit)
		{
			byte[] mailItemIdBytes = Serialization.MessageIdToBytes(mailItemId, Serialization.Version);
			byte[] queryFilterBytes = Serialization.FilterObjectToBytes(queryFilter);
			byte[] inputPropertiesBytes = Serialization.SingleMessageObjectToBytes(properties, Serialization.Version);
			base.SetMessage(mailItemIdBytes, queryFilterBytes, inputPropertiesBytes, resubmit);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x00091610 File Offset: 0x0008F810
		private ExtensibleMessageInfo[] GetExtensibleMessageInfo(MessageInfo[] legacyMessageInfoCollection)
		{
			List<ExtensibleMessageInfo> list = new List<ExtensibleMessageInfo>();
			foreach (MessageInfo messageInfo in legacyMessageInfoCollection)
			{
				MessageIdentity messageIdentity = (MessageIdentity)messageInfo.Identity;
				list.Add(new PropertyBagBasedMessageInfo(messageIdentity.InternalId, messageIdentity.QueueIdentity)
				{
					Subject = messageInfo.Subject,
					InternetMessageId = messageInfo.InternetMessageId,
					FromAddress = messageInfo.FromAddress,
					Status = messageInfo.Status,
					Size = messageInfo.Size,
					MessageSourceName = messageInfo.MessageSourceName,
					SourceIP = messageInfo.SourceIP,
					SCL = messageInfo.SCL,
					DateReceived = messageInfo.DateReceived,
					ExpirationTime = messageInfo.ExpirationTime,
					LastErrorCode = messageInfo.LastErrorCode,
					LastError = messageInfo.LastError,
					RetryCount = messageInfo.RetryCount,
					Recipients = messageInfo.Recipients,
					ComponentLatency = messageInfo.ComponentLatency,
					MessageLatency = messageInfo.MessageLatency
				});
			}
			return list.ToArray();
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00091730 File Offset: 0x0008F930
		private ExtensibleQueueInfo[] GetExtensibleQueueInfo(QueueInfo[] legacyQueueInfoCollection)
		{
			List<ExtensibleQueueInfo> list = new List<ExtensibleQueueInfo>();
			foreach (QueueInfo queueInfo in legacyQueueInfoCollection)
			{
				list.Add(new PropertyBagBasedQueueInfo((QueueIdentity)queueInfo.Identity)
				{
					DeliveryType = queueInfo.DeliveryType,
					NextHopConnector = queueInfo.NextHopConnector,
					Status = queueInfo.Status,
					MessageCount = queueInfo.MessageCount,
					LastError = queueInfo.LastError,
					LastRetryTime = queueInfo.LastRetryTime,
					NextRetryTime = queueInfo.NextRetryTime
				});
			}
			return list.ToArray();
		}

		// Token: 0x04001358 RID: 4952
		internal const uint InArgComponentLatencyInfo = 2483093515U;
	}
}

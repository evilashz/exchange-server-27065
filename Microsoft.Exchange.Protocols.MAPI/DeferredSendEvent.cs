using System;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000005 RID: 5
	internal class DeferredSendEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000027D8 File Offset: 0x000009D8
		internal DateTime EventTime
		{
			get
			{
				return this.eventTime;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000027E0 File Offset: 0x000009E0
		internal int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000027E8 File Offset: 0x000009E8
		internal long FolderId
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000027F0 File Offset: 0x000009F0
		internal long MessageId
		{
			get
			{
				return this.mid;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027F8 File Offset: 0x000009F8
		internal static byte[] SerializeExtraData(long folderId, long messageId)
		{
			uint[] array = new uint[2];
			object[] array2 = new object[2];
			array[0] = PropTag.Message.Fid.PropTag;
			array2[0] = folderId;
			array[1] = PropTag.Message.Mid.PropTag;
			array2[1] = messageId;
			return PropertyBlob.BuildBlob(array, array2);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000284C File Offset: 0x00000A4C
		internal static bool DeserializeExtraData(byte[] blob, out long folderId, out long messageId)
		{
			folderId = ExchangeId.Zero.ToLong();
			messageId = ExchangeId.Zero.ToLong();
			if (blob == null)
			{
				ExTraceGlobals.DeferredSendTracer.TraceError(44360L, "No extra data in the property blob");
				return false;
			}
			PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(blob, 0);
			for (int i = 0; i < blobReader.PropertyCount; i++)
			{
				uint propertyTag = blobReader.GetPropertyTag(i);
				object propertyValue = blobReader.GetPropertyValue(i);
				if (propertyTag == PropTag.Message.Fid.PropTag)
				{
					folderId = (long)propertyValue;
				}
				else if (propertyTag == PropTag.Message.Mid.PropTag)
				{
					messageId = (long)propertyValue;
				}
				else
				{
					ExTraceGlobals.DeferredSendTracer.TraceDebug(60744L, "Unncessary properties found in the property blog");
				}
			}
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000290C File Offset: 0x00000B0C
		internal DeferredSendEvent(DateTime eventTime, int mailboxNumber, long fid, long mid)
		{
			this.eventTime = eventTime;
			this.mailboxNumber = mailboxNumber;
			this.fid = fid;
			this.mid = mid;
		}

		// Token: 0x04000039 RID: 57
		private readonly DateTime eventTime;

		// Token: 0x0400003A RID: 58
		private readonly int mailboxNumber;

		// Token: 0x0400003B RID: 59
		private readonly long fid;

		// Token: 0x0400003C RID: 60
		private readonly long mid;
	}
}

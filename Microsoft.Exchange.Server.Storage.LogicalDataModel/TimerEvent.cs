using System;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000DD RID: 221
	internal class TimerEvent
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00060C0C File Offset: 0x0005EE0C
		internal DateTime EventTime
		{
			get
			{
				return this.eventTime;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00060C14 File Offset: 0x0005EE14
		internal int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00060C1C File Offset: 0x0005EE1C
		internal int DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00060C24 File Offset: 0x0005EE24
		internal Property Prop
		{
			get
			{
				return this.prop;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00060C2C File Offset: 0x0005EE2C
		internal static bool IsValidTimerDateTime(DateTime? eventTime)
		{
			return eventTime != null && !(eventTime.Value >= TimerEventHandler.DateTimeMax);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00060C50 File Offset: 0x0005EE50
		internal static byte[] SerializeExtraData(int documentId, Property prop)
		{
			uint[] array = new uint[2];
			object[] array2 = new object[2];
			array[0] = PropTag.Message.DocumentId.PropTag;
			array2[0] = documentId;
			array[1] = prop.Tag.PropTag;
			array2[1] = prop.Value;
			return PropertyBlob.BuildBlob(array, array2);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		internal static bool DeserializeExtraData(byte[] blob, out int documentId, out Property prop)
		{
			documentId = 0;
			prop = new Property(StorePropTag.Invalid, null);
			if (blob == null)
			{
				ExTraceGlobals.EventsTracer.TraceError(58140L, "No extra data in the property blob");
				return false;
			}
			PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(blob, 0);
			if (blobReader.PropertyCount < 2)
			{
				ExTraceGlobals.EventsTracer.TraceError(33564L, "No property data in the property blob");
				return false;
			}
			int index;
			int index2;
			if (blobReader.GetPropertyTag(0) == PropTag.Message.DocumentId.PropTag)
			{
				index = 0;
				index2 = 1;
			}
			else
			{
				if (blobReader.GetPropertyTag(1) != PropTag.Message.DocumentId.PropTag)
				{
					ExTraceGlobals.EventsTracer.TraceError(49948L, "Unexpected property in the property blob");
					return false;
				}
				index = 1;
				index2 = 0;
			}
			StorePropTag tag = StorePropTag.CreateWithoutInfo(blobReader.GetPropertyTag(index2), ObjectType.Message);
			if (tag.PropType != PropertyType.SysTime)
			{
				ExTraceGlobals.EventsTracer.TraceError(48316L, "Unexpected property type in the property blob");
				return false;
			}
			documentId = (int)blobReader.GetPropertyValue(index);
			prop = new Property(tag, blobReader.GetPropertyValue(index2));
			return true;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00060DB9 File Offset: 0x0005EFB9
		internal TimerEvent(DateTime eventTime, int mailboxNumber, int documentId, Property prop)
		{
			this.eventTime = eventTime;
			this.mailboxNumber = mailboxNumber;
			this.documentId = documentId;
			this.prop = prop;
		}

		// Token: 0x04000597 RID: 1431
		private readonly DateTime eventTime;

		// Token: 0x04000598 RID: 1432
		private readonly int mailboxNumber;

		// Token: 0x04000599 RID: 1433
		private readonly int documentId;

		// Token: 0x0400059A RID: 1434
		private readonly Property prop;

		// Token: 0x020000DE RID: 222
		internal enum EventType
		{
			// Token: 0x0400059C RID: 1436
			None,
			// Token: 0x0400059D RID: 1437
			TimerEvent
		}
	}
}

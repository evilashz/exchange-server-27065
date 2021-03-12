using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	public sealed class StorageActionWatermark : XMLSerializableBase, IActionWatermark, IComparable, IComparable<StorageActionWatermark>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002964 File Offset: 0x00000B64
		public StorageActionWatermark()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000296C File Offset: 0x00000B6C
		internal StorageActionWatermark(Activity activity)
		{
			ArgumentValidator.ThrowIfNull("activity", activity);
			this.TimeStamp = (DateTime)activity.TimeStamp;
			this.SequenceNumber = activity.SequenceNumber;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000299C File Offset: 0x00000B9C
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000029A4 File Offset: 0x00000BA4
		[XmlElement(ElementName = "TimeStamp")]
		public DateTime TimeStamp { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000029AD File Offset: 0x00000BAD
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000029B5 File Offset: 0x00000BB5
		[XmlElement(ElementName = "SequenceNumber")]
		public long SequenceNumber { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x000029BE File Offset: 0x00000BBE
		public static StorageActionWatermark Deserialize(string data)
		{
			return XMLSerializableBase.Deserialize<StorageActionWatermark>(data, true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000029C7 File Offset: 0x00000BC7
		string IActionWatermark.SerializeToString()
		{
			return base.Serialize(false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029D0 File Offset: 0x00000BD0
		int IComparable.CompareTo(object obj)
		{
			StorageActionWatermark storageActionWatermark = obj as StorageActionWatermark;
			if (storageActionWatermark == null)
			{
				throw new ArgumentException();
			}
			return ((IComparable<StorageActionWatermark>)this).CompareTo(storageActionWatermark);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000029F4 File Offset: 0x00000BF4
		int IComparable<StorageActionWatermark>.CompareTo(StorageActionWatermark other)
		{
			int num = this.TimeStamp.CompareTo(other.TimeStamp);
			if (num == 0)
			{
				num = this.SequenceNumber.CompareTo(other.SequenceNumber);
			}
			return num;
		}
	}
}

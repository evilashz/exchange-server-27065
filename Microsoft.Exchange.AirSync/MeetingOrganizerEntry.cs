using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000DC RID: 220
	internal class MeetingOrganizerEntry
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x00043C40 File Offset: 0x00041E40
		public MeetingOrganizerEntry()
		{
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00043C48 File Offset: 0x00041E48
		public MeetingOrganizerEntry(GlobalObjectId globalObjectId, string organizer, bool? isOrganizer, string subject)
		{
			this.CleanGlobalObjectId = (globalObjectId.IsCleanGlobalObjectId ? globalObjectId : new GlobalObjectId(globalObjectId.CleanGlobalObjectIdBytes));
			this.EntryTime = TimeProvider.UtcNow;
			this.Organizer = organizer;
			this.IsOrganizer = isOrganizer;
			if (!string.IsNullOrEmpty(subject) && subject.Length > 15)
			{
				subject = subject.Substring(0, 15) + "...";
			}
			this.Subject = subject;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00043CC2 File Offset: 0x00041EC2
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00043CCA File Offset: 0x00041ECA
		public GlobalObjectId CleanGlobalObjectId { get; set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00043CD3 File Offset: 0x00041ED3
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00043CDB File Offset: 0x00041EDB
		public DateTime EntryTime { get; set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00043CE4 File Offset: 0x00041EE4
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00043CEC File Offset: 0x00041EEC
		public string Organizer { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00043CF5 File Offset: 0x00041EF5
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00043CFD File Offset: 0x00041EFD
		public bool? IsOrganizer { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00043D06 File Offset: 0x00041F06
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00043D0E File Offset: 0x00041F0E
		public string Subject { get; set; }

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00043D18 File Offset: 0x00041F18
		public override string ToString()
		{
			return string.Format("Uid:'{0}',EntryTime:{1},Organizer:'{2}',IsOrganizer:{3},Subject:'{4}'", new object[]
			{
				this.CleanGlobalObjectId.Uid,
				this.EntryTime,
				this.Organizer,
				(this.IsOrganizer != null) ? this.IsOrganizer.Value.ToString() : "<unknown>",
				this.Subject
			});
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00043D98 File Offset: 0x00041F98
		internal void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			if (this.CleanGlobalObjectId == null)
			{
				throw new CorruptSyncStateException(new LocalizedString("[MeetingOrganizerEntry.SerializeData] CleanGlobalObjectId cannot be null during serialization."), null);
			}
			componentDataPool.GetStringDataInstance().Bind(this.CleanGlobalObjectId.Uid).SerializeData(writer, componentDataPool);
			componentDataPool.GetDateTimeDataInstance().Bind((ExDateTime)this.EntryTime).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.Organizer).SerializeData(writer, componentDataPool);
			NullableData<BooleanData, bool> nullableData = new NullableData<BooleanData, bool>();
			nullableData.Bind(this.IsOrganizer).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.Subject).SerializeData(writer, componentDataPool);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00043E44 File Offset: 0x00042044
		internal void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			StringData stringDataInstance = componentDataPool.GetStringDataInstance();
			stringDataInstance.DeserializeData(reader, componentDataPool);
			if (string.IsNullOrEmpty(stringDataInstance.Data))
			{
				throw new CorruptSyncStateException(new LocalizedString("[MeetingOrganizerEntry.DeserializeData] deserialized Uid was null or empty."), null);
			}
			this.CleanGlobalObjectId = new GlobalObjectId(stringDataInstance.Data);
			DateTimeData dateTimeDataInstance = componentDataPool.GetDateTimeDataInstance();
			dateTimeDataInstance.DeserializeData(reader, componentDataPool);
			this.EntryTime = (DateTime)dateTimeDataInstance.Data;
			StringData stringDataInstance2 = componentDataPool.GetStringDataInstance();
			stringDataInstance2.DeserializeData(reader, componentDataPool);
			this.Organizer = stringDataInstance2.Data;
			NullableData<BooleanData, bool> nullableData = new NullableData<BooleanData, bool>();
			nullableData.DeserializeData(reader, componentDataPool);
			this.IsOrganizer = nullableData.Data;
			StringData stringDataInstance3 = componentDataPool.GetStringDataInstance();
			stringDataInstance3.DeserializeData(reader, componentDataPool);
			this.Subject = stringDataInstance3.Data;
		}

		// Token: 0x040007D8 RID: 2008
		private const string formatString = "Uid:'{0}',EntryTime:{1},Organizer:'{2}',IsOrganizer:{3},Subject:'{4}'";
	}
}

using System;
using System.IO;
using Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000253 RID: 595
	internal class AirSyncCalendarSyncState : CalendarSyncState, ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x00080754 File Offset: 0x0007E954
		public AirSyncCalendarSyncState() : base(null, null, null)
		{
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00080772 File Offset: 0x0007E972
		public AirSyncCalendarSyncState(string base64IcsSyncState, CalendarViewQueryResumptionPoint queryResumptionPoint, ExDateTime? oldWindowEndTime) : base(base64IcsSyncState, queryResumptionPoint, oldWindowEndTime)
		{
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0008077D File Offset: 0x0007E97D
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00080784 File Offset: 0x0007E984
		public ushort TypeId
		{
			get
			{
				return AirSyncCalendarSyncState.typeId;
			}
			set
			{
				AirSyncCalendarSyncState.typeId = value;
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0008078C File Offset: 0x0007E98C
		public override IFolderSyncState CreateFolderSyncState(StoreObjectId folderObjectId, ISyncProvider syncProvider)
		{
			return new SyncCalendarFolderSyncState(folderObjectId, syncProvider, base.IcsSyncState);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0008079C File Offset: 0x0007E99C
		public ICustomSerializable BuildObject()
		{
			return new AirSyncCalendarSyncState(null, null, null);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000807BC File Offset: 0x0007E9BC
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			string text = reader.ReadString();
			if (string.IsNullOrEmpty(text))
			{
				throw new CorruptSyncStateException("AirSyncCalendarSyncState.DeserializeData.EmptyString", null);
			}
			string[] array = text.Split(new char[]
			{
				','
			});
			if (array.Length != 7 || array[0] != "v2")
			{
				throw new CorruptSyncStateException("AirSyncCalendarSyncState.DeserializeData.InvalidVersion", null);
			}
			base.IcsSyncState = array[1];
			base.OldWindowEnd = CalendarSyncState.SafeGetDateTimeValue(array[6]);
			base.QueryResumptionPoint = CalendarSyncState.SafeGetResumptionPoint(array[2], array[3], array[4], array[5]);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00080847 File Offset: 0x0007EA47
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(this.ToString());
		}

		// Token: 0x04000D84 RID: 3460
		public static readonly AirSyncCalendarSyncState Empty = new AirSyncCalendarSyncState(null, null, null);

		// Token: 0x04000D85 RID: 3461
		private static ushort typeId;
	}
}

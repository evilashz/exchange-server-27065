using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000D1 RID: 209
	internal sealed class ItemIdMapping : IdMapping, IItemIdMapping, IIdMapping
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00040038 File Offset: 0x0003E238
		public ItemIdMapping()
		{
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00040040 File Offset: 0x0003E240
		public ItemIdMapping(string parentSyncId)
		{
			this.parentSyncId = parentSyncId;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0004004F File Offset: 0x0003E24F
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x00040056 File Offset: 0x0003E256
		public override ushort TypeId
		{
			get
			{
				return ItemIdMapping.typeId;
			}
			set
			{
				ItemIdMapping.typeId = value;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00040060 File Offset: 0x0003E260
		public string Add(ISyncItemId mailboxItemId)
		{
			AirSyncDiagnostics.Assert(mailboxItemId != null);
			if (mailboxItemId != null && mailboxItemId.NativeId != null)
			{
				StoreObjectId storeObjectId = mailboxItemId.NativeId as StoreObjectId;
				if (storeObjectId != null && storeObjectId.ObjectType == StoreObjectType.CalendarItemOccurrence)
				{
					AirSyncDiagnostics.TraceDebug<ISyncItemId>(ExTraceGlobals.RequestsTracer, this, "CalendarItemOccurrence ItemId is being added to the ItemIdMapping! Id:{0}.", mailboxItemId);
				}
			}
			string text;
			if (base.OldIds.ContainsKey(mailboxItemId))
			{
				text = base.OldIds[mailboxItemId];
			}
			else
			{
				text = this.parentSyncId + ":" + base.UniqueCounter.ToString(CultureInfo.InvariantCulture);
			}
			AirSyncDiagnostics.Assert(text.Length <= 64);
			base.Add(mailboxItemId, text);
			return text;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0004010A File Offset: 0x0003E30A
		public new void Add(ISyncItemId itemId, string syncId)
		{
			AirSyncDiagnostics.Assert(itemId != null);
			base.Add(itemId, syncId);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00040120 File Offset: 0x0003E320
		public override ICustomSerializable BuildObject()
		{
			return new ItemIdMapping();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00040128 File Offset: 0x0003E328
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.DeserializeData(reader, componentDataPool);
			StringData stringDataInstance = componentDataPool.GetStringDataInstance();
			stringDataInstance.DeserializeData(reader, componentDataPool);
			this.parentSyncId = stringDataInstance.Data;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00040158 File Offset: 0x0003E358
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			base.SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.parentSyncId).SerializeData(writer, componentDataPool);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0004017A File Offset: 0x0003E37A
		public void UpdateParent(string newParent)
		{
			this.parentSyncId = newParent;
		}

		// Token: 0x04000752 RID: 1874
		private static ushort typeId;

		// Token: 0x04000753 RID: 1875
		private string parentSyncId;
	}
}

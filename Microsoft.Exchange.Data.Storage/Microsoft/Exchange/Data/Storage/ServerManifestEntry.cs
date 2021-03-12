using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DFC RID: 3580
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerManifestEntry : ICustomSerializable, IReadOnlyPropertyBag
	{
		// Token: 0x06007AFE RID: 31486 RVA: 0x0021F7DB File Offset: 0x0021D9DB
		public ServerManifestEntry()
		{
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x0021F7EA File Offset: 0x0021D9EA
		public ServerManifestEntry(ISyncItemId id)
		{
			this.itemId = id;
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x0021F800 File Offset: 0x0021DA00
		public ServerManifestEntry(ChangeType changeType, ISyncItemId id, ISyncWatermark watermark = null)
		{
			this.ChangeType = changeType;
			this.itemId = id;
			this.Watermark = watermark;
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06007B01 RID: 31489 RVA: 0x0021F824 File Offset: 0x0021DA24
		public ISyncItemId Id
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06007B02 RID: 31490 RVA: 0x0021F82C File Offset: 0x0021DA2C
		// (set) Token: 0x06007B03 RID: 31491 RVA: 0x0021F834 File Offset: 0x0021DA34
		public int?[] ChangeTrackingInformation
		{
			get
			{
				return this.changeTrackingInformation;
			}
			set
			{
				this.changeTrackingInformation = value;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06007B04 RID: 31492 RVA: 0x0021F83D File Offset: 0x0021DA3D
		// (set) Token: 0x06007B05 RID: 31493 RVA: 0x0021F845 File Offset: 0x0021DA45
		public ChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ChangeType>(value);
				this.changeType = value;
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06007B06 RID: 31494 RVA: 0x0021F854 File Offset: 0x0021DA54
		// (set) Token: 0x06007B07 RID: 31495 RVA: 0x0021F85C File Offset: 0x0021DA5C
		public bool IsAcknowledgedByClient
		{
			get
			{
				return this.acknowledgedByClient;
			}
			set
			{
				this.acknowledgedByClient = value;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06007B08 RID: 31496 RVA: 0x0021F865 File Offset: 0x0021DA65
		// (set) Token: 0x06007B09 RID: 31497 RVA: 0x0021F86D File Offset: 0x0021DA6D
		public bool IsDelayedServerOperation
		{
			get
			{
				return this.delayedServerOperation;
			}
			set
			{
				this.delayedServerOperation = value;
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x06007B0A RID: 31498 RVA: 0x0021F876 File Offset: 0x0021DA76
		// (set) Token: 0x06007B0B RID: 31499 RVA: 0x0021F87E File Offset: 0x0021DA7E
		public bool IsRejected
		{
			get
			{
				return this.rejected;
			}
			set
			{
				this.rejected = value;
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06007B0C RID: 31500 RVA: 0x0021F887 File Offset: 0x0021DA87
		// (set) Token: 0x06007B0D RID: 31501 RVA: 0x0021F88F File Offset: 0x0021DA8F
		public ISyncWatermark Watermark
		{
			get
			{
				return this.watermark;
			}
			set
			{
				this.watermark = value;
			}
		}

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06007B0E RID: 31502 RVA: 0x0021F898 File Offset: 0x0021DA98
		// (set) Token: 0x06007B0F RID: 31503 RVA: 0x0021F8A5 File Offset: 0x0021DAA5
		public bool IsRead
		{
			get
			{
				return this.itemRead == ServerManifestEntry.ReadFlagState.Read;
			}
			set
			{
				this.itemRead = (value ? ServerManifestEntry.ReadFlagState.Read : ServerManifestEntry.ReadFlagState.UnRead);
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06007B10 RID: 31504 RVA: 0x0021F8B4 File Offset: 0x0021DAB4
		public bool IsReadFlagInitialized
		{
			get
			{
				return this.itemRead != ServerManifestEntry.ReadFlagState.Unknown;
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06007B11 RID: 31505 RVA: 0x0021F8C2 File Offset: 0x0021DAC2
		// (set) Token: 0x06007B12 RID: 31506 RVA: 0x0021F8CA File Offset: 0x0021DACA
		public bool IsNew
		{
			get
			{
				return this.newItem;
			}
			set
			{
				this.newItem = value;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06007B13 RID: 31507 RVA: 0x0021F8D3 File Offset: 0x0021DAD3
		// (set) Token: 0x06007B14 RID: 31508 RVA: 0x0021F8DB File Offset: 0x0021DADB
		public ConversationId ConversationId
		{
			get
			{
				return this.conversationId;
			}
			set
			{
				this.conversationId = value;
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06007B15 RID: 31509 RVA: 0x0021F8E4 File Offset: 0x0021DAE4
		// (set) Token: 0x06007B16 RID: 31510 RVA: 0x0021F8EC File Offset: 0x0021DAEC
		public bool FirstMessageInConversation
		{
			get
			{
				return this.firstMessageInConversation;
			}
			set
			{
				this.firstMessageInConversation = value;
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06007B17 RID: 31511 RVA: 0x0021F8F5 File Offset: 0x0021DAF5
		// (set) Token: 0x06007B18 RID: 31512 RVA: 0x0021F8FD File Offset: 0x0021DAFD
		public ExDateTime? FilterDate
		{
			get
			{
				return this.filterDate;
			}
			set
			{
				this.filterDate = value;
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06007B19 RID: 31513 RVA: 0x0021F906 File Offset: 0x0021DB06
		// (set) Token: 0x06007B1A RID: 31514 RVA: 0x0021F90E File Offset: 0x0021DB0E
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
			set
			{
				this.messageClass = value;
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06007B1B RID: 31515 RVA: 0x0021F917 File Offset: 0x0021DB17
		// (set) Token: 0x06007B1C RID: 31516 RVA: 0x0021F91F File Offset: 0x0021DB1F
		public StoreId SeriesMasterId { get; set; }

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06007B1D RID: 31517 RVA: 0x0021F928 File Offset: 0x0021DB28
		// (set) Token: 0x06007B1E RID: 31518 RVA: 0x0021F930 File Offset: 0x0021DB30
		public CalendarItemType CalendarItemType { get; set; }

		// Token: 0x170020F8 RID: 8440
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				if (propertyDefinition.Equals(InternalSchema.ItemClass) && !string.IsNullOrEmpty(this.MessageClass))
				{
					return this.MessageClass;
				}
				if (propertyDefinition.Equals(MessageItemSchema.IsRead))
				{
					return this.IsRead;
				}
				if (propertyDefinition.Equals(InternalSchema.ItemId) && this.Id != null && this.Id.NativeId != null)
				{
					return this.Id.NativeId;
				}
				if (propertyDefinition.Equals(ItemSchema.ReceivedTime) && this.FilterDate != null)
				{
					return this.FilterDate.Value;
				}
				if (propertyDefinition.Equals(ItemSchema.ConversationId) && this.ConversationId != null)
				{
					return this.ConversationId;
				}
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x0021FA08 File Offset: 0x0021DC08
		public virtual void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			DerivedData<ISyncItemId> isyncItemIdDataInstance = componentDataPool.GetISyncItemIdDataInstance();
			isyncItemIdDataInstance.DeserializeData(reader, componentDataPool);
			this.itemId = isyncItemIdDataInstance.Data;
			this.changeType = (ChangeType)reader.ReadByte();
			DerivedData<ISyncWatermark> isyncWatermarkDataInstance = componentDataPool.GetISyncWatermarkDataInstance();
			isyncWatermarkDataInstance.DeserializeData(reader, componentDataPool);
			this.watermark = isyncWatermarkDataInstance.Data;
			this.IsAcknowledgedByClient = reader.ReadBoolean();
			ArrayData<NullableData<Int32Data, int>, int?> nullableInt32ArrayInstance = componentDataPool.GetNullableInt32ArrayInstance();
			nullableInt32ArrayInstance.DeserializeData(reader, componentDataPool);
			this.changeTrackingInformation = nullableInt32ArrayInstance.Data;
			this.IsRejected = reader.ReadBoolean();
			this.IsDelayedServerOperation = reader.ReadBoolean();
			if (componentDataPool.InternalVersion > 0)
			{
				NullableDateTimeData nullableDateTimeDataInstance = componentDataPool.GetNullableDateTimeDataInstance();
				nullableDateTimeDataInstance.DeserializeData(reader, componentDataPool);
				this.filterDate = nullableDateTimeDataInstance.Data;
				StringData stringDataInstance = componentDataPool.GetStringDataInstance();
				stringDataInstance.DeserializeData(reader, componentDataPool);
				this.messageClass = stringDataInstance.Data;
				ConversationIdData conversationIdDataInstance = componentDataPool.GetConversationIdDataInstance();
				conversationIdDataInstance.DeserializeData(reader, componentDataPool);
				this.conversationId = conversationIdDataInstance.Data;
				this.FirstMessageInConversation = reader.ReadBoolean();
				if (componentDataPool.InternalVersion > 2)
				{
					this.itemRead = (ServerManifestEntry.ReadFlagState)reader.ReadByte();
				}
			}
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x0021FB18 File Offset: 0x0021DD18
		public virtual void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetISyncItemIdDataInstance().Bind(this.itemId).SerializeData(writer, componentDataPool);
			writer.Write((byte)this.changeType);
			ICustomClonable customClonable = this.watermark as ICustomClonable;
			if (customClonable != null)
			{
				this.watermark = (ISyncWatermark)customClonable.CustomClone();
			}
			componentDataPool.GetISyncWatermarkDataInstance().Bind(this.watermark).SerializeData(writer, componentDataPool);
			writer.Write(this.IsAcknowledgedByClient);
			componentDataPool.GetNullableInt32ArrayInstance().Bind(this.changeTrackingInformation).SerializeData(writer, componentDataPool);
			writer.Write(this.IsRejected);
			writer.Write(this.IsDelayedServerOperation);
			componentDataPool.GetNullableDateTimeDataInstance().Bind(this.filterDate).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.messageClass).SerializeData(writer, componentDataPool);
			componentDataPool.GetConversationIdDataInstance().Bind(this.conversationId).SerializeData(writer, componentDataPool);
			writer.Write(this.FirstMessageInConversation);
			writer.Write((byte)this.itemRead);
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x0021FC20 File Offset: 0x0021DE20
		public void UpdateManifestFromItem(ISyncItem item)
		{
			IReadOnlyPropertyBag readOnlyPropertyBag = item.NativeItem as IReadOnlyPropertyBag;
			if (readOnlyPropertyBag == null)
			{
				return;
			}
			this.UpdateManifestFromPropertyBag(readOnlyPropertyBag);
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x0021FC44 File Offset: 0x0021DE44
		public void UpdateManifestFromPropertyBag(IReadOnlyPropertyBag propertyBag)
		{
			this.messageClass = (propertyBag[InternalSchema.ItemClass] as string);
			try
			{
				object obj = propertyBag[ItemSchema.ReceivedTime];
				if (obj is ExDateTime)
				{
					this.filterDate = new ExDateTime?((ExDateTime)obj);
				}
			}
			catch (PropertyErrorException)
			{
				this.filterDate = null;
			}
			try
			{
				object obj = propertyBag[ItemSchema.ConversationId];
				if (obj is ConversationId)
				{
					this.conversationId = (ConversationId)obj;
				}
				obj = propertyBag[ItemSchema.ConversationIndex];
				ConversationIndex index;
				if (obj is byte[] && ConversationIndex.TryCreate((byte[])obj, out index) && index != ConversationIndex.Empty && index.Components != null && index.Components.Count == 1)
				{
					this.firstMessageInConversation = true;
				}
			}
			catch (PropertyErrorException)
			{
				this.conversationId = null;
				this.firstMessageInConversation = false;
			}
			try
			{
				object obj = propertyBag[MessageItemSchema.IsRead];
				if (obj is bool)
				{
					this.itemRead = (((bool)obj) ? ServerManifestEntry.ReadFlagState.Read : ServerManifestEntry.ReadFlagState.UnRead);
				}
			}
			catch (PropertyErrorException)
			{
				this.itemRead = ServerManifestEntry.ReadFlagState.Unknown;
			}
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x0021FD7C File Offset: 0x0021DF7C
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x0021FD83 File Offset: 0x0021DF83
		public override string ToString()
		{
			return string.Format("SME Id:{0}, ChangeType:{1}, IsRead:{2}", this.Id, this.ChangeType, this.IsRead);
		}

		// Token: 0x040054AC RID: 21676
		private int?[] changeTrackingInformation;

		// Token: 0x040054AD RID: 21677
		private ChangeType changeType;

		// Token: 0x040054AE RID: 21678
		private bool acknowledgedByClient;

		// Token: 0x040054AF RID: 21679
		private bool delayedServerOperation;

		// Token: 0x040054B0 RID: 21680
		private bool rejected;

		// Token: 0x040054B1 RID: 21681
		private ISyncWatermark watermark;

		// Token: 0x040054B2 RID: 21682
		private ISyncItemId itemId;

		// Token: 0x040054B3 RID: 21683
		private ServerManifestEntry.ReadFlagState itemRead = ServerManifestEntry.ReadFlagState.Unknown;

		// Token: 0x040054B4 RID: 21684
		private bool newItem;

		// Token: 0x040054B5 RID: 21685
		private ConversationId conversationId;

		// Token: 0x040054B6 RID: 21686
		private bool firstMessageInConversation;

		// Token: 0x040054B7 RID: 21687
		private ExDateTime? filterDate;

		// Token: 0x040054B8 RID: 21688
		private string messageClass;

		// Token: 0x02000DFD RID: 3581
		private enum ReadFlagState : byte
		{
			// Token: 0x040054BC RID: 21692
			Read,
			// Token: 0x040054BD RID: 21693
			UnRead,
			// Token: 0x040054BE RID: 21694
			Unknown
		}
	}
}

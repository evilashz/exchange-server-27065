using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E3A RID: 3642
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ComponentDataPool
	{
		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x06007E64 RID: 32356 RVA: 0x0022BFE1 File Offset: 0x0022A1E1
		// (set) Token: 0x06007E65 RID: 32357 RVA: 0x0022BFE9 File Offset: 0x0022A1E9
		public int InternalVersion
		{
			get
			{
				return this.internalVersion;
			}
			set
			{
				this.internalVersion = value;
			}
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x06007E66 RID: 32358 RVA: 0x0022BFF2 File Offset: 0x0022A1F2
		// (set) Token: 0x06007E67 RID: 32359 RVA: 0x0022BFFA File Offset: 0x0022A1FA
		public int ExternalVersion
		{
			get
			{
				return this.externalVersion;
			}
			set
			{
				this.externalVersion = value;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x06007E68 RID: 32360 RVA: 0x0022C003 File Offset: 0x0022A203
		public BinaryReader ConstStringDataReader
		{
			get
			{
				if (this.constStringDataReader == null)
				{
					this.constStringDataReader = new BinaryReader(new MemoryStream(128));
				}
				return this.constStringDataReader;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06007E69 RID: 32361 RVA: 0x0022C028 File Offset: 0x0022A228
		public byte[] CopyBuffer
		{
			get
			{
				if (this.copyBuffer == null)
				{
					this.copyBuffer = new byte[128];
				}
				return this.copyBuffer;
			}
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x0022C048 File Offset: 0x0022A248
		public DateTimeData GetDateTimeDataInstance()
		{
			if (this.dateTimeData == null)
			{
				this.dateTimeData = new DateTimeData();
			}
			return this.dateTimeData;
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x0022C063 File Offset: 0x0022A263
		public NullableDateTimeData GetNullableDateTimeDataInstance()
		{
			if (this.nullableDateTimeData == null)
			{
				this.nullableDateTimeData = new NullableDateTimeData();
			}
			return this.nullableDateTimeData;
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x0022C07E File Offset: 0x0022A27E
		public StringData GetStringDataInstance()
		{
			if (this.stringData == null)
			{
				this.stringData = new StringData();
			}
			return this.stringData;
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x0022C099 File Offset: 0x0022A299
		public ConstStringData GetConstStringDataInstance()
		{
			if (this.constStringData == null)
			{
				this.constStringData = new ConstStringData();
			}
			return this.constStringData;
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x0022C0B4 File Offset: 0x0022A2B4
		public ByteData GetByteDataInstance()
		{
			if (this.byteData == null)
			{
				this.byteData = new ByteData();
			}
			return this.byteData;
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x0022C0CF File Offset: 0x0022A2CF
		public ArrayData<NullableData<Int32Data, int>, int?> GetNullableInt32ArrayInstance()
		{
			if (this.nullableInt32ArrayData == null)
			{
				this.nullableInt32ArrayData = new ArrayData<NullableData<Int32Data, int>, int?>();
			}
			return this.nullableInt32ArrayData;
		}

		// Token: 0x06007E70 RID: 32368 RVA: 0x0022C0EA File Offset: 0x0022A2EA
		public ArrayData<Int32Data, int> GetInt32ArrayInstance()
		{
			if (this.int32ArrayData == null)
			{
				this.int32ArrayData = new ArrayData<Int32Data, int>();
			}
			return this.int32ArrayData;
		}

		// Token: 0x06007E71 RID: 32369 RVA: 0x0022C105 File Offset: 0x0022A305
		public ByteArrayData GetByteArrayInstance()
		{
			if (this.byteArrayData == null)
			{
				this.byteArrayData = new ByteArrayData();
			}
			return this.byteArrayData;
		}

		// Token: 0x06007E72 RID: 32370 RVA: 0x0022C120 File Offset: 0x0022A320
		public BooleanData GetBooleanDataInstance()
		{
			if (this.booleanData == null)
			{
				this.booleanData = new BooleanData();
			}
			return this.booleanData;
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x0022C13B File Offset: 0x0022A33B
		public StoreObjectIdData GetStoreObjectIdDataInstance()
		{
			if (this.storeObjectIdData == null)
			{
				this.storeObjectIdData = new StoreObjectIdData();
			}
			return this.storeObjectIdData;
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x0022C156 File Offset: 0x0022A356
		public ADObjectIdData GetADObjectIdDataInstance()
		{
			if (this.adObjectIdData == null)
			{
				this.adObjectIdData = new ADObjectIdData();
			}
			return this.adObjectIdData;
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x0022C171 File Offset: 0x0022A371
		public Int32Data GetInt32DataInstance()
		{
			if (this.int32Data == null)
			{
				this.int32Data = new Int32Data();
			}
			return this.int32Data;
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x0022C18C File Offset: 0x0022A38C
		public UInt32Data GetUInt32DataInstance()
		{
			if (this.uint32Data == null)
			{
				this.uint32Data = new UInt32Data();
			}
			return this.uint32Data;
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x0022C1A7 File Offset: 0x0022A3A7
		public Int64Data GetInt64DataInstance()
		{
			if (this.int64Data == null)
			{
				this.int64Data = new Int64Data();
			}
			return this.int64Data;
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x0022C1C2 File Offset: 0x0022A3C2
		public DerivedData<ISyncWatermark> GetISyncWatermarkDataInstance()
		{
			if (this.syncWatermarkDataInstance == null)
			{
				this.syncWatermarkDataInstance = new DerivedData<ISyncWatermark>();
			}
			return this.syncWatermarkDataInstance;
		}

		// Token: 0x06007E79 RID: 32377 RVA: 0x0022C1DD File Offset: 0x0022A3DD
		public DerivedData<ISyncItemId> GetISyncItemIdDataInstance()
		{
			if (this.syncItemIdDataInstance == null)
			{
				this.syncItemIdDataInstance = new DerivedData<ISyncItemId>();
			}
			return this.syncItemIdDataInstance;
		}

		// Token: 0x06007E7A RID: 32378 RVA: 0x0022C1F8 File Offset: 0x0022A3F8
		public ConversationIdData GetConversationIdDataInstance()
		{
			if (this.conversationIdDataInstance == null)
			{
				this.conversationIdDataInstance = new ConversationIdData();
			}
			return this.conversationIdDataInstance;
		}

		// Token: 0x040055EA RID: 21994
		private int internalVersion;

		// Token: 0x040055EB RID: 21995
		private int externalVersion;

		// Token: 0x040055EC RID: 21996
		private DateTimeData dateTimeData;

		// Token: 0x040055ED RID: 21997
		private StringData stringData;

		// Token: 0x040055EE RID: 21998
		private NullableDateTimeData nullableDateTimeData;

		// Token: 0x040055EF RID: 21999
		private ConstStringData constStringData;

		// Token: 0x040055F0 RID: 22000
		private ByteData byteData;

		// Token: 0x040055F1 RID: 22001
		private ArrayData<NullableData<Int32Data, int>, int?> nullableInt32ArrayData;

		// Token: 0x040055F2 RID: 22002
		private ArrayData<Int32Data, int> int32ArrayData;

		// Token: 0x040055F3 RID: 22003
		private ByteArrayData byteArrayData;

		// Token: 0x040055F4 RID: 22004
		private BooleanData booleanData;

		// Token: 0x040055F5 RID: 22005
		private StoreObjectIdData storeObjectIdData;

		// Token: 0x040055F6 RID: 22006
		private ADObjectIdData adObjectIdData;

		// Token: 0x040055F7 RID: 22007
		private Int32Data int32Data;

		// Token: 0x040055F8 RID: 22008
		private UInt32Data uint32Data;

		// Token: 0x040055F9 RID: 22009
		private Int64Data int64Data;

		// Token: 0x040055FA RID: 22010
		private DerivedData<ISyncWatermark> syncWatermarkDataInstance;

		// Token: 0x040055FB RID: 22011
		private DerivedData<ISyncItemId> syncItemIdDataInstance;

		// Token: 0x040055FC RID: 22012
		private ConversationIdData conversationIdDataInstance;

		// Token: 0x040055FD RID: 22013
		private BinaryReader constStringDataReader;

		// Token: 0x040055FE RID: 22014
		private byte[] copyBuffer;
	}
}

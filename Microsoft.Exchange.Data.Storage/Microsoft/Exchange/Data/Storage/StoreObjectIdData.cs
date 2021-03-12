using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E55 RID: 3669
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StoreObjectIdData : ComponentData<StoreObjectId>
	{
		// Token: 0x06007F24 RID: 32548 RVA: 0x0022D8CB File Offset: 0x0022BACB
		public StoreObjectIdData()
		{
		}

		// Token: 0x06007F25 RID: 32549 RVA: 0x0022D8D3 File Offset: 0x0022BAD3
		public StoreObjectIdData(StoreObjectId data) : base(data)
		{
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x06007F26 RID: 32550 RVA: 0x0022D8DC File Offset: 0x0022BADC
		// (set) Token: 0x06007F27 RID: 32551 RVA: 0x0022D8E3 File Offset: 0x0022BAE3
		public override ushort TypeId
		{
			get
			{
				return StoreObjectIdData.typeId;
			}
			set
			{
				StoreObjectIdData.typeId = value;
			}
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x0022D8EB File Offset: 0x0022BAEB
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			writer.Write(false);
			writer.Write(base.Data.GetByteArrayLength());
			StoreObjectId.Serialize(base.Data, writer);
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x0022D92C File Offset: 0x0022BB2C
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			int byteArrayLength = reader.ReadInt32();
			base.Data = StoreObjectId.Deserialize(reader, byteArrayLength);
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x0022D96D File Offset: 0x0022BB6D
		public override ICustomSerializable BuildObject()
		{
			return new StoreObjectIdData();
		}

		// Token: 0x0400562A RID: 22058
		private static ushort typeId;
	}
}

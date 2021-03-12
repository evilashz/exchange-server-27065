using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E4A RID: 3658
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationIdData : ComponentData<ConversationId>
	{
		// Token: 0x06007EDE RID: 32478 RVA: 0x0022D0D7 File Offset: 0x0022B2D7
		public ConversationIdData()
		{
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x0022D0DF File Offset: 0x0022B2DF
		public ConversationIdData(ConversationId data) : base(data)
		{
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x06007EE0 RID: 32480 RVA: 0x0022D0E8 File Offset: 0x0022B2E8
		// (set) Token: 0x06007EE1 RID: 32481 RVA: 0x0022D0EF File Offset: 0x0022B2EF
		public override ushort TypeId
		{
			get
			{
				return ConversationIdData.typeId;
			}
			set
			{
				ConversationIdData.typeId = value;
			}
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x0022D0F8 File Offset: 0x0022B2F8
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			byte[] data = null;
			if (base.Data != null)
			{
				data = base.Data.GetBytes();
			}
			componentDataPool.GetByteArrayInstance().Bind(data).SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x0022D130 File Offset: 0x0022B330
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
			byteArrayInstance.DeserializeData(reader, componentDataPool);
			if (byteArrayInstance.Data == null)
			{
				base.Data = null;
				return;
			}
			base.Data = ConversationId.Create(byteArrayInstance.Data);
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x0022D16D File Offset: 0x0022B36D
		public override ICustomSerializable BuildObject()
		{
			return new ConversationIdData();
		}

		// Token: 0x0400561C RID: 22044
		private static ushort typeId;
	}
}

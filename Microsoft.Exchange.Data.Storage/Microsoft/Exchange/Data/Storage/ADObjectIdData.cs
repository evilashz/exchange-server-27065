using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E44 RID: 3652
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADObjectIdData : ComponentData<ADObjectId>
	{
		// Token: 0x06007EB4 RID: 32436 RVA: 0x0022CD34 File Offset: 0x0022AF34
		public ADObjectIdData()
		{
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x0022CD3C File Offset: 0x0022AF3C
		public ADObjectIdData(ADObjectId data) : base(data)
		{
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x0022CD45 File Offset: 0x0022AF45
		// (set) Token: 0x06007EB7 RID: 32439 RVA: 0x0022CD4C File Offset: 0x0022AF4C
		public override ushort TypeId
		{
			get
			{
				return ADObjectIdData.typeId;
			}
			set
			{
				ADObjectIdData.typeId = value;
			}
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x0022CD54 File Offset: 0x0022AF54
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			componentDataPool.GetByteArrayInstance().Bind(base.Data.GetBytes()).SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x0022CD8C File Offset: 0x0022AF8C
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
			byteArrayInstance.DeserializeData(reader, componentDataPool);
			if (byteArrayInstance.Data == null)
			{
				base.Data = null;
				return;
			}
			base.Data = new ADObjectId(byteArrayInstance.Data);
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x0022CDD9 File Offset: 0x0022AFD9
		public override ICustomSerializable BuildObject()
		{
			return new ADObjectIdData();
		}

		// Token: 0x04005615 RID: 22037
		private static ushort typeId;
	}
}

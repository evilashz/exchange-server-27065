using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E47 RID: 3655
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ByteArrayData : ComponentData<byte[]>
	{
		// Token: 0x06007EC9 RID: 32457 RVA: 0x0022CF46 File Offset: 0x0022B146
		public ByteArrayData()
		{
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x0022CF4E File Offset: 0x0022B14E
		public ByteArrayData(byte[] data) : base(data)
		{
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06007ECB RID: 32459 RVA: 0x0022CF57 File Offset: 0x0022B157
		// (set) Token: 0x06007ECC RID: 32460 RVA: 0x0022CF5E File Offset: 0x0022B15E
		public override ushort TypeId
		{
			get
			{
				return ByteArrayData.typeId;
			}
			set
			{
				ByteArrayData.typeId = value;
			}
		}

		// Token: 0x06007ECD RID: 32461 RVA: 0x0022CF66 File Offset: 0x0022B166
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			writer.Write(base.Data.Length);
			writer.Write(base.Data, 0, base.Data.Length);
		}

		// Token: 0x06007ECE RID: 32462 RVA: 0x0022CFA4 File Offset: 0x0022B1A4
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			int num = reader.ReadInt32();
			base.Data = new byte[num];
			reader.Read(base.Data, 0, num);
		}

		// Token: 0x06007ECF RID: 32463 RVA: 0x0022CFE3 File Offset: 0x0022B1E3
		public override ICustomSerializable BuildObject()
		{
			return new ByteArrayData();
		}

		// Token: 0x04005619 RID: 22041
		private static ushort typeId;
	}
}

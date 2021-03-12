using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E57 RID: 3671
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UInt32Data : ComponentData<uint>
	{
		// Token: 0x06007F31 RID: 32561 RVA: 0x0022D9B9 File Offset: 0x0022BBB9
		public UInt32Data()
		{
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x0022D9C1 File Offset: 0x0022BBC1
		public UInt32Data(uint data) : base(data)
		{
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x06007F33 RID: 32563 RVA: 0x0022D9CA File Offset: 0x0022BBCA
		// (set) Token: 0x06007F34 RID: 32564 RVA: 0x0022D9D1 File Offset: 0x0022BBD1
		public override ushort TypeId
		{
			get
			{
				return UInt32Data.typeId;
			}
			set
			{
				UInt32Data.typeId = value;
			}
		}

		// Token: 0x06007F35 RID: 32565 RVA: 0x0022D9D9 File Offset: 0x0022BBD9
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data);
		}

		// Token: 0x06007F36 RID: 32566 RVA: 0x0022D9E7 File Offset: 0x0022BBE7
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = reader.ReadUInt32();
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x0022D9F5 File Offset: 0x0022BBF5
		public override ICustomSerializable BuildObject()
		{
			return new UInt32Data();
		}

		// Token: 0x0400562C RID: 22060
		private static ushort typeId;
	}
}

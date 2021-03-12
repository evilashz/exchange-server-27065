using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E48 RID: 3656
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ByteData : ComponentData<byte>
	{
		// Token: 0x06007ED0 RID: 32464 RVA: 0x0022CFEA File Offset: 0x0022B1EA
		public ByteData()
		{
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x0022CFF2 File Offset: 0x0022B1F2
		public ByteData(byte data) : base(data)
		{
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06007ED2 RID: 32466 RVA: 0x0022CFFB File Offset: 0x0022B1FB
		// (set) Token: 0x06007ED3 RID: 32467 RVA: 0x0022D002 File Offset: 0x0022B202
		public override ushort TypeId
		{
			get
			{
				return ByteData.typeId;
			}
			set
			{
				ByteData.typeId = value;
			}
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x0022D00A File Offset: 0x0022B20A
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data);
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x0022D018 File Offset: 0x0022B218
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = reader.ReadByte();
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x0022D026 File Offset: 0x0022B226
		public override ICustomSerializable BuildObject()
		{
			return new ByteData();
		}

		// Token: 0x0400561A RID: 22042
		private static ushort typeId;
	}
}

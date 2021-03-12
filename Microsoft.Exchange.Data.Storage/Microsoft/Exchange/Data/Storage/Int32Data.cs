using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E51 RID: 3665
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Int32Data : ComponentData<int>
	{
		// Token: 0x06007F0E RID: 32526 RVA: 0x0022D73D File Offset: 0x0022B93D
		public Int32Data()
		{
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x0022D745 File Offset: 0x0022B945
		public Int32Data(int data) : base(data)
		{
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06007F10 RID: 32528 RVA: 0x0022D74E File Offset: 0x0022B94E
		// (set) Token: 0x06007F11 RID: 32529 RVA: 0x0022D755 File Offset: 0x0022B955
		public override ushort TypeId
		{
			get
			{
				return Int32Data.typeId;
			}
			set
			{
				Int32Data.typeId = value;
			}
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x0022D75D File Offset: 0x0022B95D
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data);
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x0022D76B File Offset: 0x0022B96B
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = reader.ReadInt32();
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x0022D779 File Offset: 0x0022B979
		public override ICustomSerializable BuildObject()
		{
			return new Int32Data();
		}

		// Token: 0x04005626 RID: 22054
		private static ushort typeId;
	}
}

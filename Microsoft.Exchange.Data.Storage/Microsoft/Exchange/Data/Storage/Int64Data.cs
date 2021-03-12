using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E52 RID: 3666
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Int64Data : ComponentData<long>
	{
		// Token: 0x06007F15 RID: 32533 RVA: 0x0022D780 File Offset: 0x0022B980
		public Int64Data()
		{
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x0022D788 File Offset: 0x0022B988
		public Int64Data(long data) : base(data)
		{
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06007F17 RID: 32535 RVA: 0x0022D791 File Offset: 0x0022B991
		// (set) Token: 0x06007F18 RID: 32536 RVA: 0x0022D798 File Offset: 0x0022B998
		public override ushort TypeId
		{
			get
			{
				return Int64Data.typeId;
			}
			set
			{
				Int64Data.typeId = value;
			}
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x0022D7A0 File Offset: 0x0022B9A0
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data);
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x0022D7AE File Offset: 0x0022B9AE
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = reader.ReadInt64();
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x0022D7BC File Offset: 0x0022B9BC
		public override ICustomSerializable BuildObject()
		{
			return new Int64Data();
		}

		// Token: 0x04005627 RID: 22055
		private static ushort typeId;
	}
}

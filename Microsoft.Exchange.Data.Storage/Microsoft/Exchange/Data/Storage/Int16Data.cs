using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E50 RID: 3664
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Int16Data : ComponentData<short>
	{
		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06007F08 RID: 32520 RVA: 0x0022D703 File Offset: 0x0022B903
		// (set) Token: 0x06007F09 RID: 32521 RVA: 0x0022D70A File Offset: 0x0022B90A
		public override ushort TypeId
		{
			get
			{
				return Int16Data.typeId;
			}
			set
			{
				Int16Data.typeId = value;
			}
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x0022D712 File Offset: 0x0022B912
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data);
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x0022D720 File Offset: 0x0022B920
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = reader.ReadInt16();
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x0022D72E File Offset: 0x0022B92E
		public override ICustomSerializable BuildObject()
		{
			return new Int16Data();
		}

		// Token: 0x04005625 RID: 22053
		private static ushort typeId;
	}
}

using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E46 RID: 3654
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BooleanData : ComponentData<bool>
	{
		// Token: 0x06007EC2 RID: 32450 RVA: 0x0022CF03 File Offset: 0x0022B103
		public BooleanData()
		{
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x0022CF0B File Offset: 0x0022B10B
		public BooleanData(bool data) : base(data)
		{
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06007EC4 RID: 32452 RVA: 0x0022CF14 File Offset: 0x0022B114
		// (set) Token: 0x06007EC5 RID: 32453 RVA: 0x0022CF1B File Offset: 0x0022B11B
		public override ushort TypeId
		{
			get
			{
				return BooleanData.typeId;
			}
			set
			{
				BooleanData.typeId = value;
			}
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x0022CF23 File Offset: 0x0022B123
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data);
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x0022CF31 File Offset: 0x0022B131
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = reader.ReadBoolean();
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x0022CF3F File Offset: 0x0022B13F
		public override ICustomSerializable BuildObject()
		{
			return new BooleanData();
		}

		// Token: 0x04005618 RID: 22040
		private static ushort typeId;
	}
}

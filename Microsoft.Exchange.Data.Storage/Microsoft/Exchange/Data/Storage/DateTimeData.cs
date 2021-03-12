using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E4B RID: 3659
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DateTimeData : ComponentData<ExDateTime>
	{
		// Token: 0x06007EE5 RID: 32485 RVA: 0x0022D174 File Offset: 0x0022B374
		public DateTimeData()
		{
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x0022D17C File Offset: 0x0022B37C
		public DateTimeData(ExDateTime data) : base(data)
		{
		}

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x06007EE7 RID: 32487 RVA: 0x0022D185 File Offset: 0x0022B385
		// (set) Token: 0x06007EE8 RID: 32488 RVA: 0x0022D18C File Offset: 0x0022B38C
		public override ushort TypeId
		{
			get
			{
				return DateTimeData.typeId;
			}
			set
			{
				DateTimeData.typeId = value;
			}
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x0022D194 File Offset: 0x0022B394
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			long value = base.Data.ToBinary();
			writer.Write(value);
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x0022D1B8 File Offset: 0x0022B3B8
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			long dateData = reader.ReadInt64();
			base.Data = ExDateTime.FromBinary(dateData);
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x0022D1D8 File Offset: 0x0022B3D8
		public override ICustomSerializable BuildObject()
		{
			return new DateTimeData();
		}

		// Token: 0x0400561D RID: 22045
		private static ushort typeId;
	}
}

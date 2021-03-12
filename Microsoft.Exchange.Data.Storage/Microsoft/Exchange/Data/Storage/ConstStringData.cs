using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E49 RID: 3657
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConstStringData : ComponentData<string>
	{
		// Token: 0x06007ED7 RID: 32471 RVA: 0x0022D02D File Offset: 0x0022B22D
		public ConstStringData()
		{
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x0022D035 File Offset: 0x0022B235
		public ConstStringData(string data) : base(data)
		{
		}

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x06007ED9 RID: 32473 RVA: 0x0022D03E File Offset: 0x0022B23E
		// (set) Token: 0x06007EDA RID: 32474 RVA: 0x0022D045 File Offset: 0x0022B245
		public override ushort TypeId
		{
			get
			{
				return ConstStringData.typeId;
			}
			set
			{
				ConstStringData.typeId = value;
			}
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x0022D04D File Offset: 0x0022B24D
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			writer.Write(base.Data);
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x0022D074 File Offset: 0x0022B274
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			base.Data = StaticStringPool.Instance.GetData(reader, componentDataPool);
			if (base.Data == null)
			{
				componentDataPool.ConstStringDataReader.BaseStream.Seek(0L, SeekOrigin.Begin);
				base.Data = componentDataPool.ConstStringDataReader.ReadString();
			}
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x0022D0D0 File Offset: 0x0022B2D0
		public override ICustomSerializable BuildObject()
		{
			return new ConstStringData();
		}

		// Token: 0x0400561B RID: 22043
		private static ushort typeId;
	}
}

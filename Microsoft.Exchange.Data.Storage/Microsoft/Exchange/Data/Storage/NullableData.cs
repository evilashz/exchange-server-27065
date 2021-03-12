using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E53 RID: 3667
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullableData<T, RawT> : ComponentData<RawT?> where T : ComponentData<RawT>, new() where RawT : struct
	{
		// Token: 0x06007F1C RID: 32540 RVA: 0x0022D7C3 File Offset: 0x0022B9C3
		public NullableData()
		{
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x0022D7D6 File Offset: 0x0022B9D6
		public NullableData(RawT? data) : base(data)
		{
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06007F1E RID: 32542 RVA: 0x0022D7EA File Offset: 0x0022B9EA
		// (set) Token: 0x06007F1F RID: 32543 RVA: 0x0022D7F1 File Offset: 0x0022B9F1
		public override ushort TypeId
		{
			get
			{
				return NullableData<T, RawT>.typeId;
			}
			set
			{
				NullableData<T, RawT>.typeId = value;
			}
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x0022D7FC File Offset: 0x0022B9FC
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			RawT value = base.Data.Value;
			this.serializableData.Bind(value);
			this.serializableData.SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007F21 RID: 32545 RVA: 0x0022D868 File Offset: 0x0022BA68
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			this.serializableData.DeserializeData(reader, componentDataPool);
			base.Data = new RawT?(this.serializableData.Data);
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x0022D8BC File Offset: 0x0022BABC
		public override ICustomSerializable BuildObject()
		{
			return new NullableData<T, RawT>();
		}

		// Token: 0x04005628 RID: 22056
		private static ushort typeId;

		// Token: 0x04005629 RID: 22057
		private T serializableData = ObjectBuildHelper<T>.Build();
	}
}

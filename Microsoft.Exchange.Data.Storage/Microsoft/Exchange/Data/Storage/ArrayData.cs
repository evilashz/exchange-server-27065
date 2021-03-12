using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E45 RID: 3653
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ArrayData<T, RawT> : ComponentData<RawT[]> where T : ComponentData<RawT>, new()
	{
		// Token: 0x06007EBB RID: 32443 RVA: 0x0022CDE0 File Offset: 0x0022AFE0
		public ArrayData()
		{
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x0022CDF3 File Offset: 0x0022AFF3
		public ArrayData(RawT[] data) : base(data)
		{
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x06007EBD RID: 32445 RVA: 0x0022CE07 File Offset: 0x0022B007
		// (set) Token: 0x06007EBE RID: 32446 RVA: 0x0022CE0E File Offset: 0x0022B00E
		public override ushort TypeId
		{
			get
			{
				return ArrayData<T, RawT>.typeId;
			}
			set
			{
				ArrayData<T, RawT>.typeId = value;
			}
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x0022CE18 File Offset: 0x0022B018
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			writer.Write(base.Data.Length);
			for (int i = 0; i < base.Data.Length; i++)
			{
				this.serializableData.Bind(base.Data[i]);
				this.serializableData.SerializeData(writer, componentDataPool);
			}
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x0022CE90 File Offset: 0x0022B090
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			int num = reader.ReadInt32();
			base.Data = new RawT[num];
			for (int i = 0; i < num; i++)
			{
				this.serializableData.DeserializeData(reader, componentDataPool);
				base.Data[i] = this.serializableData.Data;
			}
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x0022CEFC File Offset: 0x0022B0FC
		public override ICustomSerializable BuildObject()
		{
			return new ArrayData<T, RawT>();
		}

		// Token: 0x04005616 RID: 22038
		private static ushort typeId;

		// Token: 0x04005617 RID: 22039
		private T serializableData = ObjectBuildHelper<T>.Build();
	}
}

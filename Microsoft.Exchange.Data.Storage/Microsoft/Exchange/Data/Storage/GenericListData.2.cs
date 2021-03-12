using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E4F RID: 3663
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GenericListData<T, RawT> : ComponentData<List<RawT>> where T : ComponentData<RawT>, new()
	{
		// Token: 0x06007F01 RID: 32513 RVA: 0x0022D583 File Offset: 0x0022B783
		public GenericListData()
		{
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x0022D596 File Offset: 0x0022B796
		public GenericListData(List<RawT> data) : base(data)
		{
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06007F03 RID: 32515 RVA: 0x0022D5AA File Offset: 0x0022B7AA
		// (set) Token: 0x06007F04 RID: 32516 RVA: 0x0022D5B1 File Offset: 0x0022B7B1
		public override ushort TypeId
		{
			get
			{
				return GenericListData<T, RawT>.typeId;
			}
			set
			{
				GenericListData<T, RawT>.typeId = value;
			}
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x0022D5BC File Offset: 0x0022B7BC
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			writer.Write(base.Data.Count);
			for (int i = 0; i < base.Data.Count; i++)
			{
				this.serializableData.Bind(base.Data[i]);
				this.serializableData.SerializeData(writer, componentDataPool);
			}
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x0022D63C File Offset: 0x0022B83C
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			int num = reader.ReadInt32();
			int num2 = num;
			try
			{
				bool flag = false;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2902863165U, ref flag);
				if (flag)
				{
					num2 = int.MaxValue;
					throw new OutOfMemoryException();
				}
				base.Data = new List<RawT>(num2);
			}
			catch (OutOfMemoryException ex)
			{
				ex.Data["GenericListDataDeserializationCount"] = num2;
				throw;
			}
			for (int i = 0; i < num; i++)
			{
				this.serializableData.DeserializeData(reader, componentDataPool);
				base.Data.Add(this.serializableData.Data);
			}
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x0022D6FC File Offset: 0x0022B8FC
		public override ICustomSerializable BuildObject()
		{
			return new GenericListData<T, RawT>();
		}

		// Token: 0x04005623 RID: 22051
		private static ushort typeId;

		// Token: 0x04005624 RID: 22052
		private T serializableData = ObjectBuildHelper<T>.Build();
	}
}

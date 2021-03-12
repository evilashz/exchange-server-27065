using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E4E RID: 3662
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GenericListData<T> : ComponentData<List<T>> where T : ICustomSerializable, new()
	{
		// Token: 0x06007EFA RID: 32506 RVA: 0x0022D43D File Offset: 0x0022B63D
		public GenericListData()
		{
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x0022D445 File Offset: 0x0022B645
		public GenericListData(List<T> data) : base(data)
		{
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x06007EFC RID: 32508 RVA: 0x0022D44E File Offset: 0x0022B64E
		// (set) Token: 0x06007EFD RID: 32509 RVA: 0x0022D455 File Offset: 0x0022B655
		public override ushort TypeId
		{
			get
			{
				return GenericListData<T>.typeId;
			}
			set
			{
				GenericListData<T>.typeId = value;
			}
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x0022D460 File Offset: 0x0022B660
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
				T t = base.Data[i];
				t.SerializeData(writer, componentDataPool);
			}
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x0022D4C8 File Offset: 0x0022B6C8
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
				base.Data = new List<T>(num2);
			}
			catch (OutOfMemoryException ex)
			{
				ex.Data["GenericListDataDeserializationCount"] = num2;
				throw;
			}
			for (int i = 0; i < num; i++)
			{
				T item = ObjectBuildHelper<T>.Build();
				item.DeserializeData(reader, componentDataPool);
				base.Data.Add(item);
			}
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x0022D57C File Offset: 0x0022B77C
		public override ICustomSerializable BuildObject()
		{
			return new GenericListData<T>();
		}

		// Token: 0x04005620 RID: 22048
		internal const uint LidChangeListSizeForOutOfMemoryException = 2902863165U;

		// Token: 0x04005621 RID: 22049
		public const string GenericListDataDeserializationCount = "GenericListDataDeserializationCount";

		// Token: 0x04005622 RID: 22050
		private static ushort typeId;
	}
}

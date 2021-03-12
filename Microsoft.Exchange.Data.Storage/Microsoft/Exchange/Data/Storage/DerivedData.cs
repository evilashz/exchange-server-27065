using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E3E RID: 3646
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DerivedData<RefT> : ComponentData<RefT> where RefT : ICustomSerializableBuilder
	{
		// Token: 0x06007E8E RID: 32398 RVA: 0x0022C5F0 File Offset: 0x0022A7F0
		public DerivedData()
		{
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x0022C5F8 File Offset: 0x0022A7F8
		public DerivedData(RefT data) : base(data)
		{
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x06007E90 RID: 32400 RVA: 0x0022C601 File Offset: 0x0022A801
		// (set) Token: 0x06007E91 RID: 32401 RVA: 0x0022C608 File Offset: 0x0022A808
		public override ushort TypeId
		{
			get
			{
				return DerivedData<RefT>.typeId;
			}
			set
			{
				DerivedData<RefT>.typeId = value;
			}
		}

		// Token: 0x06007E92 RID: 32402 RVA: 0x0022C610 File Offset: 0x0022A810
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			RefT data = base.Data;
			writer.Write(data.TypeId);
			RefT data2 = base.Data;
			data2.SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x0022C670 File Offset: 0x0022A870
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = default(RefT);
				return;
			}
			ushort num = reader.ReadUInt16();
			if (!SyncStateTypeFactory.DoesTypeExistWithThisId(num))
			{
				throw new CustomSerializationException(ServerStrings.ExSyncStateCorrupted("Type " + num.ToString() + " not registered."));
			}
			base.Data = (RefT)((object)SyncStateTypeFactory.GetInstance().BuildObject(num));
			RefT data = base.Data;
			data.DeserializeData(reader, componentDataPool);
		}

		// Token: 0x06007E94 RID: 32404 RVA: 0x0022C6F1 File Offset: 0x0022A8F1
		public override ICustomSerializable BuildObject()
		{
			return new DerivedData<RefT>();
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x0022C6F8 File Offset: 0x0022A8F8
		[Conditional("DEBUG")]
		private void AssertTypeRegisteredWithSyncStateFactory(RefT data)
		{
			if (data == null)
			{
				return;
			}
			if (!SyncStateTypeFactory.IsTypeRegistered(data))
			{
				"Type not registered.  Please call: SyncStateTypeFactory.GetInstance().RegisterBuilder(new " + data.GetType().ToString() + "()) before attempting to serialize this type through DeriveData.";
			}
		}

		// Token: 0x04005605 RID: 22021
		private static ushort typeId;
	}
}

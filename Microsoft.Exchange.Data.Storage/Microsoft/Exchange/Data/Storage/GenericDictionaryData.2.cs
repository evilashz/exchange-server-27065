using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E4D RID: 3661
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GenericDictionaryData<K, RawK, V, RawV> : ComponentData<Dictionary<RawK, RawV>> where K : ComponentData<RawK>, new() where V : ComponentData<RawV>, new()
	{
		// Token: 0x06007EF3 RID: 32499 RVA: 0x0022D307 File Offset: 0x0022B507
		public GenericDictionaryData()
		{
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x0022D30F File Offset: 0x0022B50F
		public GenericDictionaryData(Dictionary<RawK, RawV> data) : base(data)
		{
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x06007EF5 RID: 32501 RVA: 0x0022D318 File Offset: 0x0022B518
		// (set) Token: 0x06007EF6 RID: 32502 RVA: 0x0022D31F File Offset: 0x0022B51F
		public override ushort TypeId
		{
			get
			{
				return GenericDictionaryData<K, RawK, V, RawV>.typeId;
			}
			set
			{
				GenericDictionaryData<K, RawK, V, RawV>.typeId = value;
			}
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x0022D328 File Offset: 0x0022B528
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			List<RawK> data = new List<RawK>(base.Data.Keys);
			List<RawV> data2 = new List<RawV>(base.Data.Values);
			GenericListData<K, RawK> genericListData = new GenericListData<K, RawK>(data);
			GenericListData<V, RawV> genericListData2 = new GenericListData<V, RawV>(data2);
			genericListData.SerializeData(writer, componentDataPool);
			genericListData2.SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x0022D390 File Offset: 0x0022B590
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			GenericListData<K, RawK> genericListData = new GenericListData<K, RawK>();
			GenericListData<V> genericListData2 = new GenericListData<V>();
			genericListData.DeserializeData(reader, componentDataPool);
			genericListData2.DeserializeData(reader, componentDataPool);
			if (genericListData.Data == null)
			{
				base.Data = null;
				return;
			}
			base.Data = new Dictionary<RawK, RawV>(genericListData.Data.Count);
			for (int i = 0; i < genericListData.Data.Count; i++)
			{
				Dictionary<RawK, RawV> data = base.Data;
				RawK key = genericListData.Data[i];
				V v = genericListData2.Data[i];
				data[key] = v.Data;
			}
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x0022D436 File Offset: 0x0022B636
		public override ICustomSerializable BuildObject()
		{
			return new GenericDictionaryData<K, RawK, V, RawV>();
		}

		// Token: 0x0400561F RID: 22047
		private static ushort typeId;
	}
}

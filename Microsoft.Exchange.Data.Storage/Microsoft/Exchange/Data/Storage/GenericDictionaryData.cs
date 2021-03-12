using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E4C RID: 3660
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GenericDictionaryData<K, RawK, V> : ComponentData<Dictionary<RawK, V>> where K : ComponentData<RawK>, new() where V : ICustomSerializable, new()
	{
		// Token: 0x06007EEC RID: 32492 RVA: 0x0022D1DF File Offset: 0x0022B3DF
		public GenericDictionaryData()
		{
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x0022D1E7 File Offset: 0x0022B3E7
		public GenericDictionaryData(Dictionary<RawK, V> data) : base(data)
		{
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x06007EEE RID: 32494 RVA: 0x0022D1F0 File Offset: 0x0022B3F0
		// (set) Token: 0x06007EEF RID: 32495 RVA: 0x0022D1F7 File Offset: 0x0022B3F7
		public override ushort TypeId
		{
			get
			{
				return GenericDictionaryData<K, RawK, V>.typeId;
			}
			set
			{
				GenericDictionaryData<K, RawK, V>.typeId = value;
			}
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x0022D200 File Offset: 0x0022B400
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(base.Data == null);
			if (base.Data == null)
			{
				return;
			}
			List<RawK> data = new List<RawK>(base.Data.Keys);
			List<V> data2 = new List<V>(base.Data.Values);
			GenericListData<K, RawK> genericListData = new GenericListData<K, RawK>(data);
			GenericListData<V> genericListData2 = new GenericListData<V>(data2);
			genericListData.SerializeData(writer, componentDataPool);
			genericListData2.SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x0022D268 File Offset: 0x0022B468
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
			base.Data = new Dictionary<RawK, V>(genericListData.Data.Count);
			for (int i = 0; i < genericListData.Data.Count; i++)
			{
				base.Data[genericListData.Data[i]] = genericListData2.Data[i];
			}
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x0022D300 File Offset: 0x0022B500
		public override ICustomSerializable BuildObject()
		{
			return new GenericDictionaryData<K, RawK, V>();
		}

		// Token: 0x0400561E RID: 22046
		private static ushort typeId;
	}
}

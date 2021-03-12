using System;
using System.IO;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000081 RID: 129
	internal sealed class SerializableStringProperty : SerializableProperty<string>
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000AF12 File Offset: 0x00009112
		internal SerializableStringProperty(SerializablePropertyId id, string value) : base(id, value)
		{
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000AF1C File Offset: 0x0000911C
		internal SerializableStringProperty()
		{
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000AF24 File Offset: 0x00009124
		public override SerializablePropertyType Type
		{
			get
			{
				return SerializablePropertyType.String;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000AF27 File Offset: 0x00009127
		protected override void SerializeValue(BinaryWriter writer)
		{
			writer.Write(base.PropertyValue);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000AF35 File Offset: 0x00009135
		protected override void DeserializeValue(BinaryReader reader)
		{
			base.PropertyValue = reader.ReadString();
		}
	}
}

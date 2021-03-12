using System;
using System.IO;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200007F RID: 127
	internal abstract class SerializableProperty<DataType> : ISerializableProperty
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000AD20 File Offset: 0x00008F20
		protected SerializableProperty()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000AD28 File Offset: 0x00008F28
		protected SerializableProperty(SerializablePropertyId id, DataType value)
		{
			this.propertyId = id;
			this.propertyValue = value;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000335 RID: 821
		public abstract SerializablePropertyType Type { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000AD3E File Offset: 0x00008F3E
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000AD46 File Offset: 0x00008F46
		public SerializablePropertyId Id
		{
			get
			{
				return this.propertyId;
			}
			protected set
			{
				this.propertyId = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000AD4F File Offset: 0x00008F4F
		public object Value
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000AD5C File Offset: 0x00008F5C
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000AD64 File Offset: 0x00008F64
		protected DataType PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
			set
			{
				this.propertyValue = value;
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000AD6D File Offset: 0x00008F6D
		public void Serialize(BinaryWriter writer)
		{
			Util.ThrowOnNullArgument(writer, "writer");
			writer.Write((byte)this.Id);
			this.SerializeValue(writer);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000AD90 File Offset: 0x00008F90
		public void Deserialize(BinaryReader reader)
		{
			Util.ThrowOnNullArgument(reader, "reader");
			byte b = reader.ReadByte();
			if (b <= 0 || b >= 16)
			{
				throw new InvalidDataException(string.Format("Invalid Property Id {0}", b));
			}
			this.Id = (SerializablePropertyId)b;
			this.DeserializeValue(reader);
		}

		// Token: 0x0600033D RID: 829
		protected abstract void SerializeValue(BinaryWriter writer);

		// Token: 0x0600033E RID: 830
		protected abstract void DeserializeValue(BinaryReader reader);

		// Token: 0x0400016F RID: 367
		private SerializablePropertyId propertyId;

		// Token: 0x04000170 RID: 368
		private DataType propertyValue;
	}
}

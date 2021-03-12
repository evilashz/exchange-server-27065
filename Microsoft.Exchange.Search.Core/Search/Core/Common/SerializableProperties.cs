using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200007E RID: 126
	internal sealed class SerializableProperties
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		internal SerializableProperties(params ISerializableProperty[] properties)
		{
			Util.ThrowOnNullOrEmptyArgument<ISerializableProperty>(properties, "properties");
			HashSet<SerializablePropertyId> hashSet = new HashSet<SerializablePropertyId>();
			foreach (ISerializableProperty serializableProperty in properties)
			{
				if (hashSet.Contains(serializableProperty.Id))
				{
					throw new ArgumentException(string.Format("Duplicated property Id {0}", serializableProperty.Id));
				}
				hashSet.Add(serializableProperty.Id);
			}
			this.properties = new List<ISerializableProperty>(properties);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000AC98 File Offset: 0x00008E98
		public static IEnumerable<ISerializableProperty> DeserializeFrom(Stream inputStream)
		{
			BinaryReader reader = new BinaryReader(inputStream);
			byte[] propertyTypeByte = new byte[1];
			while (reader.Read(propertyTypeByte, 0, 1) != 0)
			{
				ISerializableProperty property;
				switch (propertyTypeByte[0])
				{
				case 0:
					property = new SerializableStreamProperty(reader);
					break;
				case 1:
					property = new SerializableStringProperty();
					break;
				default:
					throw new InvalidDataException(string.Format("Invalid Property Type {0}", propertyTypeByte));
				}
				property.Deserialize(reader);
				yield return property;
			}
			yield break;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		public void SerializeTo(Stream outputStream)
		{
			foreach (ISerializableProperty serializableProperty in this.properties)
			{
				BinaryWriter binaryWriter = new BinaryWriter(outputStream);
				binaryWriter.Write((byte)serializableProperty.Type);
				serializableProperty.Serialize(binaryWriter);
			}
		}

		// Token: 0x0400016E RID: 366
		private readonly List<ISerializableProperty> properties;
	}
}

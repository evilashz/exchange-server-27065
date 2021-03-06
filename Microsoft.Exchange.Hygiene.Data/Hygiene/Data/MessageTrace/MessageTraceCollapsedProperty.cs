using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200016B RID: 363
	internal class MessageTraceCollapsedProperty
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002B22C File Offset: 0x0002942C
		static MessageTraceCollapsedProperty()
		{
			Dictionary<Type, Func<object, byte[]>> dictionary = new Dictionary<Type, Func<object, byte[]>>();
			dictionary.Add(typeof(bool), (object value) => BitConverter.GetBytes((bool)value));
			dictionary.Add(typeof(char), (object value) => BitConverter.GetBytes((char)value));
			dictionary.Add(typeof(uint), (object value) => BitConverter.GetBytes((uint)value));
			dictionary.Add(typeof(short), (object value) => BitConverter.GetBytes((short)value));
			dictionary.Add(typeof(ulong), (object value) => BitConverter.GetBytes((ulong)value));
			dictionary.Add(typeof(float), (object value) => BitConverter.GetBytes((float)value));
			dictionary.Add(typeof(ushort), (object value) => BitConverter.GetBytes((ushort)value));
			dictionary.Add(typeof(double), (object value) => BitConverter.GetBytes((double)value));
			MessageTraceCollapsedProperty.PrimitiveValueToByteArrayConvertor = dictionary;
			Dictionary<Type, Func<byte[], object>> dictionary2 = new Dictionary<Type, Func<byte[], object>>();
			dictionary2.Add(typeof(bool), (byte[] bytes) => BitConverter.ToBoolean(bytes, 0));
			dictionary2.Add(typeof(char), (byte[] bytes) => BitConverter.ToChar(bytes, 0));
			dictionary2.Add(typeof(uint), (byte[] bytes) => BitConverter.ToUInt32(bytes, 0));
			dictionary2.Add(typeof(short), (byte[] bytes) => BitConverter.ToInt16(bytes, 0));
			dictionary2.Add(typeof(ulong), (byte[] bytes) => BitConverter.ToUInt64(bytes, 0));
			dictionary2.Add(typeof(float), (byte[] bytes) => BitConverter.ToSingle(bytes, 0));
			dictionary2.Add(typeof(ushort), (byte[] bytes) => BitConverter.ToUInt16(bytes, 0));
			dictionary2.Add(typeof(double), (byte[] bytes) => BitConverter.ToDouble(bytes, 0));
			MessageTraceCollapsedProperty.PrimitiveValueFromByteArrayConvertor = dictionary2;
			SqlPropertyDefinition[] source = new SqlPropertyDefinition[]
			{
				new SqlPropertyDefinition
				{
					EntityName = "MessageEvents",
					EntityId = 3,
					PropertyName = "SFSLong",
					PropertyId = 1343,
					Type = SqlPropertyTypes.Long
				}
			};
			MessageTraceCollapsedProperty.propertyDefinitionByName = source.ToLookup((SqlPropertyDefinition p) => p.PropertyName.ToLower());
			MessageTraceCollapsedProperty.propertyDefinitionById = source.ToLookup((SqlPropertyDefinition p) => p.PropertyId);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002B5CF File Offset: 0x000297CF
		internal static bool IsCollapsableProperty(string entityName, string propertyName)
		{
			return MessageTraceCollapsedProperty.GetCollapsedPropertyByName(entityName, propertyName) != null;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0002B5E0 File Offset: 0x000297E0
		internal static byte[] Collapse(byte[] existingData, string entityName, PropertyBase property)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (existingData != null && existingData.Length > 0)
				{
					memoryStream.Write(existingData, 0, existingData.Length);
				}
				SqlPropertyDefinition collapsedPropertyByName = MessageTraceCollapsedProperty.GetCollapsedPropertyByName(entityName, property.PropertyName);
				switch (collapsedPropertyByName.Type)
				{
				case SqlPropertyTypes.Int:
					if (property.PropertyValueInteger != null)
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), property.PropertyValueInteger.Value);
					}
					break;
				case SqlPropertyTypes.String:
					if (!string.IsNullOrEmpty(property.PropertyValueString))
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(string), property.PropertyValueString);
					}
					break;
				case SqlPropertyTypes.DateTime:
					if (property.PropertyValueDatetime != null)
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(DateTime), property.PropertyValueDatetime.Value);
					}
					break;
				case SqlPropertyTypes.Decimal:
					if (property.PropertyValueDecimal != null)
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(decimal), property.PropertyValueDecimal.Value);
					}
					break;
				case SqlPropertyTypes.Blob:
					if (!string.IsNullOrEmpty(property.PropertyValueBlob.Value))
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(string), property.PropertyValueBlob.Value);
					}
					break;
				case SqlPropertyTypes.Boolean:
					if (property.PropertyValueBit != null)
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(bool), property.PropertyValueBit.Value);
					}
					break;
				case SqlPropertyTypes.Guid:
					if (property.PropertyValueGuid != Guid.Empty)
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(Guid), property.PropertyValueGuid);
					}
					break;
				case SqlPropertyTypes.Long:
					if (property.PropertyValueLong != null)
					{
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(int), collapsedPropertyByName.PropertyId);
						MessageTraceCollapsedProperty.SavePrimitiveValueToStream(memoryStream, typeof(long), property.PropertyValueLong.Value);
					}
					break;
				default:
					throw new InvalidOperationException(string.Format("Property type {0} is not supported for MessageTraceCollapsedProperty.", collapsedPropertyByName.Type));
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0002BD70 File Offset: 0x00029F70
		internal static IEnumerable<T> Expand<T>(byte[] data, string entityName, Func<T> objectBuilder) where T : PropertyBase
		{
			using (MemoryStream stream = new MemoryStream(data))
			{
				while (stream.Position < stream.Length)
				{
					long startPosition = stream.Position;
					int propertyId = (int)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(int));
					SqlPropertyDefinition propertyDefinition = MessageTraceCollapsedProperty.GetCollapsedPropertyById(entityName, propertyId);
					T property = objectBuilder();
					property.PropertyName = propertyDefinition.PropertyName;
					switch (propertyDefinition.Type)
					{
					case SqlPropertyTypes.Int:
						property.PropertyValueInteger = new int?((int)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(int)));
						break;
					case SqlPropertyTypes.String:
						property.PropertyValueString = (string)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(string));
						break;
					case SqlPropertyTypes.DateTime:
						property.PropertyValueDatetime = new DateTime?((DateTime)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(DateTime)));
						break;
					case SqlPropertyTypes.Decimal:
						property.PropertyValueDecimal = new decimal?((decimal)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(decimal)));
						break;
					case SqlPropertyTypes.Blob:
						property.PropertyValueBlob = new BlobType((string)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(string)));
						break;
					case SqlPropertyTypes.Boolean:
						property.PropertyValueBit = new bool?((bool)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(bool)));
						break;
					case SqlPropertyTypes.Guid:
						property.PropertyValueGuid = (Guid)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(Guid));
						break;
					case SqlPropertyTypes.Long:
						property.PropertyValueLong = new long?((long)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(long)));
						break;
					default:
						throw new InvalidOperationException(string.Format("Property type {0} is not supported for MessageTraceCollapsedProperty.", propertyDefinition.Type));
					}
					yield return property;
					if (stream.Position <= startPosition)
					{
						throw new InvalidOperationException("Unable to expand. Stream position is not moving forward.");
					}
				}
			}
			yield break;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002BD9B File Offset: 0x00029F9B
		private static SqlPropertyDefinition GetCollapsedPropertyByName(string entityName, string propertyName)
		{
			return MessageTraceCollapsedProperty.propertyDefinitionByName[propertyName.ToLower()].FirstOrDefault<SqlPropertyDefinition>();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002BDB2 File Offset: 0x00029FB2
		private static SqlPropertyDefinition GetCollapsedPropertyById(string entityName, int propertyId)
		{
			return MessageTraceCollapsedProperty.propertyDefinitionById[propertyId].FirstOrDefault<SqlPropertyDefinition>();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002BDC4 File Offset: 0x00029FC4
		private static bool SavePrimitiveValueToStream(Stream stream, Type typeOfvalue, object value)
		{
			if (typeOfvalue == typeof(long))
			{
				MessageTraceCollapsedProperty.SaveByteArrayToStream(stream, BitConverter.GetBytes((long)value));
				return true;
			}
			if (typeOfvalue == typeof(int))
			{
				MessageTraceCollapsedProperty.SaveByteArrayToStream(stream, BitConverter.GetBytes((int)value));
				return true;
			}
			if (typeOfvalue == typeof(Guid))
			{
				MessageTraceCollapsedProperty.SaveByteArrayToStream(stream, ((Guid)value).ToByteArray());
				return true;
			}
			if (typeOfvalue == typeof(string))
			{
				string text = value as string;
				if (Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(text)) == text)
				{
					MessageTraceCollapsedProperty.SaveSingleByteToStream(stream, 2);
					MessageTraceCollapsedProperty.SaveByteArrayToStream(stream, Encoding.ASCII.GetBytes((string)value));
				}
				else
				{
					MessageTraceCollapsedProperty.SaveSingleByteToStream(stream, 3);
					MessageTraceCollapsedProperty.SaveByteArrayToStream(stream, Encoding.Unicode.GetBytes((string)value));
				}
				return true;
			}
			if (typeOfvalue == typeof(DateTime))
			{
				return MessageTraceCollapsedProperty.SavePrimitiveValueToStream(stream, typeof(long), ((DateTime)value).ToBinary());
			}
			if (MessageTraceCollapsedProperty.PrimitiveValueToByteArrayConvertor.ContainsKey(typeOfvalue))
			{
				MessageTraceCollapsedProperty.SaveByteArrayToStream(stream, MessageTraceCollapsedProperty.PrimitiveValueToByteArrayConvertor[typeOfvalue](value));
				return true;
			}
			return typeOfvalue.IsEnum && MessageTraceCollapsedProperty.SavePrimitiveValueToStream(stream, typeof(int), Convert.ToInt32(value));
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002BF38 File Offset: 0x0002A138
		private static object ReadPrimitiveValueFromStream(Stream stream, Type typeOfvalue)
		{
			if (typeOfvalue == typeof(long))
			{
				return BitConverter.ToInt64(MessageTraceCollapsedProperty.ReadByteArrayFromStream(stream), 0);
			}
			if (typeOfvalue == typeof(int))
			{
				return BitConverter.ToInt32(MessageTraceCollapsedProperty.ReadByteArrayFromStream(stream), 0);
			}
			if (typeOfvalue == typeof(Guid))
			{
				return new Guid(MessageTraceCollapsedProperty.ReadByteArrayFromStream(stream));
			}
			if (typeOfvalue == typeof(string))
			{
				byte b = MessageTraceCollapsedProperty.ReadSingleByteFromStream(stream);
				switch (b)
				{
				case 2:
					return Encoding.ASCII.GetString(MessageTraceCollapsedProperty.ReadByteArrayFromStream(stream));
				case 3:
					return Encoding.Unicode.GetString(MessageTraceCollapsedProperty.ReadByteArrayFromStream(stream));
				default:
					throw new InvalidOperationException(string.Format("Unable to deserialize string from stream. Unknown string serialization algorithm {0}.", b));
				}
			}
			else
			{
				if (typeOfvalue == typeof(DateTime))
				{
					return DateTime.FromBinary((long)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(long)));
				}
				if (MessageTraceCollapsedProperty.PrimitiveValueFromByteArrayConvertor.ContainsKey(typeOfvalue))
				{
					return MessageTraceCollapsedProperty.PrimitiveValueFromByteArrayConvertor[typeOfvalue](MessageTraceCollapsedProperty.ReadByteArrayFromStream(stream));
				}
				if (typeOfvalue.IsEnum)
				{
					return Enum.ToObject(typeOfvalue, (int)MessageTraceCollapsedProperty.ReadPrimitiveValueFromStream(stream, typeof(int)));
				}
				return null;
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002C090 File Offset: 0x0002A290
		private static void SaveByteArrayToStream(Stream stream, byte[] bytes)
		{
			MessageTraceCollapsedProperty.SaveLengthToStream(stream, bytes.Length);
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		private static byte[] ReadByteArrayFromStream(Stream stream)
		{
			int num = MessageTraceCollapsedProperty.ReadLengthFromStream(stream);
			byte[] array = new byte[num];
			if (stream.Read(array, 0, num) != num)
			{
				throw new InvalidOperationException(string.Format("Unable to read byte array of {0} length from stream. End of stream reached.", num));
			}
			return array;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002C0E6 File Offset: 0x0002A2E6
		private static void SaveLengthToStream(Stream stream, int length)
		{
			while (length >= 255)
			{
				MessageTraceCollapsedProperty.SaveSingleByteToStream(stream, byte.MaxValue);
				length -= 255;
			}
			MessageTraceCollapsedProperty.SaveSingleByteToStream(stream, (byte)length);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002C110 File Offset: 0x0002A310
		private static int ReadLengthFromStream(Stream stream)
		{
			int num = 0;
			int num2;
			do
			{
				num2 = (int)MessageTraceCollapsedProperty.ReadSingleByteFromStream(stream);
				num += num2;
			}
			while (num2 == 255);
			return num;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002C133 File Offset: 0x0002A333
		private static void SaveSingleByteToStream(Stream stream, byte value)
		{
			stream.WriteByte(value);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002C13C File Offset: 0x0002A33C
		private static byte ReadSingleByteFromStream(Stream stream)
		{
			if (stream.Position >= stream.Length)
			{
				throw new InvalidOperationException("Unable to read byte from stream. End of stream reached.");
			}
			return (byte)stream.ReadByte();
		}

		// Token: 0x040006C1 RID: 1729
		private const byte MarkerASCII = 2;

		// Token: 0x040006C2 RID: 1730
		private const byte MarkerUnicode = 3;

		// Token: 0x040006C3 RID: 1731
		internal static readonly HygienePropertyDefinition PropertyDefinition = new HygienePropertyDefinition("CollapsedProperties", typeof(byte[]));

		// Token: 0x040006C4 RID: 1732
		private static readonly Dictionary<Type, Func<object, byte[]>> PrimitiveValueToByteArrayConvertor;

		// Token: 0x040006C5 RID: 1733
		private static readonly Dictionary<Type, Func<byte[], object>> PrimitiveValueFromByteArrayConvertor;

		// Token: 0x040006C6 RID: 1734
		private static ILookup<string, SqlPropertyDefinition> propertyDefinitionByName;

		// Token: 0x040006C7 RID: 1735
		private static ILookup<int, SqlPropertyDefinition> propertyDefinitionById;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200085F RID: 2143
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BlobRecipientParser
	{
		// Token: 0x06005096 RID: 20630 RVA: 0x0014E82E File Offset: 0x0014CA2E
		private BlobRecipientParser()
		{
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x0014E838 File Offset: 0x0014CA38
		internal static List<BlobRecipient> ReadRecipients(Item item, PropertyDefinition propertyDefinition)
		{
			item.Load(new PropertyDefinition[]
			{
				propertyDefinition
			});
			try
			{
				Stream stream = null;
				object obj = item.TryGetProperty(propertyDefinition);
				try
				{
					if (obj is byte[])
					{
						stream = new MemoryStream(obj as byte[], false);
					}
					else if (PropertyError.IsPropertyValueTooBig(obj))
					{
						stream = item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly);
					}
					if (stream != null)
					{
						return BlobRecipientParser.ReadRecipients(item.PropertyBag.ExTimeZone, stream);
					}
				}
				finally
				{
					if (stream != null)
					{
						stream.Dispose();
					}
				}
			}
			catch (PropertyErrorException)
			{
			}
			return new List<BlobRecipient>();
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x0014E8DC File Offset: 0x0014CADC
		internal static List<BlobRecipient> ReadRecipients(ExTimeZone timeZone, Stream stream)
		{
			List<BlobRecipient> list = new List<BlobRecipient>();
			try
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				uint num = binaryReader.ReadUInt32();
				if (num == 1U)
				{
					uint num2 = binaryReader.ReadUInt32();
					for (uint num3 = 0U; num3 < num2; num3 += 1U)
					{
						BlobRecipient blobRecipient = BlobRecipientParser.ReadRecipient(timeZone, binaryReader);
						if (blobRecipient == null || blobRecipient.Participant == null)
						{
							ExTraceGlobals.StorageTracer.TraceError(0L, "BlobRecipientParser::ReadRecipients. Failed to read a recipient. Skip rest of blob.");
							break;
						}
						list.Add(blobRecipient);
					}
				}
			}
			catch (EndOfStreamException)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "BlobRecipientParser::ReadRecipients. EndOfStream.");
			}
			return list;
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x0014E978 File Offset: 0x0014CB78
		internal static void WriteRecipients(Item item, PropertyDefinition propertyDefinition, List<BlobRecipient> list)
		{
			using (Stream stream = item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create))
			{
				BlobRecipientParser.WriteRecipients(stream, list);
			}
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x0014E9B4 File Offset: 0x0014CBB4
		internal static void WriteRecipients(Stream stream, List<BlobRecipient> list)
		{
			uint value = 1U;
			uint value2 = (uint)((ushort)list.Count);
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			foreach (BlobRecipient recipient in list)
			{
				BlobRecipientParser.WriteRecipient(binaryWriter, recipient);
			}
			stream.Flush();
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x0014EA28 File Offset: 0x0014CC28
		private static BlobRecipient ReadRecipient(ExTimeZone timeZone, BinaryReader reader)
		{
			BlobRecipient blobRecipient = null;
			uint num = reader.ReadUInt32();
			reader.ReadUInt32();
			if (num > 0U)
			{
				blobRecipient = new BlobRecipient(timeZone);
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					PropTag propTag;
					object value;
					if (!BlobRecipientParser.ReadPropValue(reader, out propTag, out value))
					{
						ExTraceGlobals.StorageTracer.TraceError(0L, "BlobRecipientParser::ReadRecipient. Failed reading property.");
						return null;
					}
					if (propTag.ValueType() != PropType.Error && !propTag.IsNamedProperty())
					{
						try
						{
							PropertyTagPropertyDefinition propertyDefinition = PropertyTagPropertyDefinition.CreateCustom(propTag.ToString(), (uint)propTag);
							blobRecipient[propertyDefinition] = value;
						}
						catch (InvalidPropertyTypeException ex)
						{
							ExTraceGlobals.StorageTracer.TraceError<PropTag, string>(0L, "BlobRecipientParser::ReadRecipient. Failed creating custom property definition for ptag={0}; exception message={1}", propTag, ex.Message);
							return null;
						}
					}
				}
			}
			return blobRecipient;
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0014EAE4 File Offset: 0x0014CCE4
		private static void WriteRecipient(BinaryWriter writer, BlobRecipient recipient)
		{
			List<PropValue> list = new List<PropValue>(recipient.PropertyValues.Count);
			foreach (PropValue item in PropValue.ConvertEnumerator<PropertyDefinition>(recipient.PropertyValues))
			{
				if (!PropertyError.IsPropertyError(item.Value) && !((PropTag)((PropertyTagPropertyDefinition)item.Property).PropertyTag).IsNamedProperty())
				{
					list.Add(item);
				}
			}
			uint count = (uint)list.Count;
			uint value = 0U;
			writer.Write(count);
			writer.Write(value);
			foreach (PropValue propValue in list)
			{
				PropertyTagPropertyDefinition propertyTagPropertyDefinition = (PropertyTagPropertyDefinition)propValue.Property;
				object value2 = ExTimeZoneHelperForMigrationOnly.ToUtcIfDateTime(propValue.Value);
				BlobRecipientParser.WritePropValue(writer, (PropTag)propertyTagPropertyDefinition.PropertyTag, value2);
			}
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x0014EBEC File Offset: 0x0014CDEC
		private static bool ReadPropValue(BinaryReader reader, out PropTag ptag, out object value)
		{
			ptag = (PropTag)reader.ReadUInt32();
			value = null;
			PropType propType = ptag.ValueType();
			PropType propType2 = propType;
			if (propType2 <= PropType.String)
			{
				if (propType2 <= PropType.Boolean)
				{
					switch (propType2)
					{
					case PropType.Null:
					case PropType.Int:
						break;
					case PropType.Short:
						value = reader.ReadUInt16();
						goto IL_135;
					default:
						switch (propType2)
						{
						case PropType.Error:
							break;
						case PropType.Boolean:
							value = (reader.ReadUInt16() != 0);
							goto IL_135;
						default:
							goto IL_123;
						}
						break;
					}
				}
				else if (propType2 != PropType.Long)
				{
					switch (propType2)
					{
					case PropType.AnsiString:
						value = BlobRecipientParser.ReadAnsiString(reader);
						goto IL_135;
					case PropType.String:
						value = BlobRecipientParser.ReadString(reader);
						goto IL_135;
					default:
						goto IL_123;
					}
				}
				value = reader.ReadInt32();
				goto IL_135;
			}
			if (propType2 <= PropType.Binary)
			{
				if (propType2 == PropType.SysTime)
				{
					value = BlobRecipientParser.ReadDateTime(reader);
					goto IL_135;
				}
				if (propType2 == PropType.Binary)
				{
					value = BlobRecipientParser.ReadBinary(reader);
					goto IL_135;
				}
			}
			else
			{
				switch (propType2)
				{
				case PropType.AnsiStringArray:
					value = BlobRecipientParser.ReadAnsiStringArray(reader);
					goto IL_135;
				case PropType.StringArray:
					value = BlobRecipientParser.ReadStringArray(reader);
					goto IL_135;
				default:
					if (propType2 == PropType.BinaryArray)
					{
						value = BlobRecipientParser.ReadBinaryArray(reader);
						goto IL_135;
					}
					break;
				}
			}
			IL_123:
			ExTraceGlobals.StorageTracer.TraceError<PropType>(0L, "BlobRecipientParser::ReadPropValue. Not supported PropType= {0}.", propType);
			IL_135:
			return value != null;
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0014ED38 File Offset: 0x0014CF38
		private static string[] ReadStringArray(BinaryReader reader)
		{
			ushort num = reader.ReadUInt16();
			string[] array = new string[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				array[i] = BlobRecipientParser.ReadString(reader);
			}
			return array;
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x0014ED6C File Offset: 0x0014CF6C
		private static string[] ReadAnsiStringArray(BinaryReader reader)
		{
			ushort num = reader.ReadUInt16();
			string[] array = new string[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				array[i] = BlobRecipientParser.ReadAnsiString(reader);
			}
			return array;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x0014EDA0 File Offset: 0x0014CFA0
		private static ExDateTime? ReadDateTime(BinaryReader reader)
		{
			ulong num = (ulong)reader.ReadUInt32();
			num |= (ulong)reader.ReadUInt32() << 32;
			ExDateTime value;
			try
			{
				value = ExDateTime.FromFileTimeUtc((long)num);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "BlobRecipientParser::ReadDateTime. ArgumentException: {0}.", ex.Message);
				return null;
			}
			return new ExDateTime?(value);
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0014EE08 File Offset: 0x0014D008
		private static byte[][] ReadBinaryArray(BinaryReader reader)
		{
			ushort num = reader.ReadUInt16();
			byte[][] array = new byte[(int)num][];
			for (int i = 0; i < (int)num; i++)
			{
				array[i] = BlobRecipientParser.ReadBinary(reader);
			}
			return array;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0014EE3C File Offset: 0x0014D03C
		private static byte[] ReadBinary(BinaryReader reader)
		{
			ushort num = reader.ReadUInt16();
			byte[] array = reader.ReadBytes((int)num);
			if (array.Length != (int)num)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "BlobRecipientParser::ReadBinarry. Invalid binary data.");
				return null;
			}
			return array;
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0014EE74 File Offset: 0x0014D074
		private static string ReadString(BinaryReader reader)
		{
			ushort num = reader.ReadUInt16();
			byte[] array = reader.ReadBytes((int)num);
			if (array.Length != (int)num)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "BlobRecipientParser::ReadString. Invalid string lenght.");
				return null;
			}
			return Encoding.Unicode.GetString(array, 0, (int)(num - 2));
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x0014EEB8 File Offset: 0x0014D0B8
		private static string ReadAnsiString(BinaryReader reader)
		{
			ushort num = reader.ReadUInt16();
			byte[] array = reader.ReadBytes((int)num);
			if (array.Length != (int)num)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "BlobRecipientParser::ReadAnsiString. Invalid string lenght.");
				return null;
			}
			return CTSGlobals.AsciiEncoding.GetString(array, 0, (int)(num - 1));
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x0014EEFC File Offset: 0x0014D0FC
		private static void WritePropValue(BinaryWriter writer, PropTag ptag, object value)
		{
			PropType propType = ptag.ValueType();
			writer.Write((uint)ptag);
			PropType propType2 = propType;
			if (propType2 <= PropType.String)
			{
				if (propType2 <= PropType.Boolean)
				{
					switch (propType2)
					{
					case PropType.Short:
						writer.Write((short)value);
						return;
					case PropType.Int:
						break;
					default:
						switch (propType2)
						{
						case PropType.Error:
							break;
						case PropType.Boolean:
						{
							ushort value2 = ((bool)value) ? 1 : 0;
							writer.Write(value2);
							return;
						}
						default:
							goto IL_13A;
						}
						break;
					}
				}
				else if (propType2 != PropType.Long)
				{
					switch (propType2)
					{
					case PropType.AnsiString:
						BlobRecipientParser.WriteAnsiString(writer, (string)value);
						return;
					case PropType.String:
						BlobRecipientParser.WriteString(writer, (string)value);
						return;
					default:
						goto IL_13A;
					}
				}
				writer.Write((int)value);
				return;
			}
			if (propType2 <= PropType.Binary)
			{
				if (propType2 != PropType.SysTime)
				{
					if (propType2 != PropType.Binary)
					{
						goto IL_13A;
					}
					BlobRecipientParser.WriteBinary(writer, (byte[])value);
					return;
				}
				else
				{
					try
					{
						BlobRecipientParser.WriteDateTime(writer, (ExDateTime)value);
						return;
					}
					catch (ArgumentException)
					{
						ExTraceGlobals.StorageTracer.TraceError<PropTag>(0L, "BlobRecipientParser::WritePropValue. Skipping bad SysTime property ptag = {0}.", ptag);
						return;
					}
				}
			}
			else
			{
				switch (propType2)
				{
				case PropType.AnsiStringArray:
					break;
				case PropType.StringArray:
					BlobRecipientParser.WriteStringArray(writer, (string[])value);
					return;
				default:
					if (propType2 != PropType.BinaryArray)
					{
						goto IL_13A;
					}
					BlobRecipientParser.WriteBinaryArray(writer, (byte[][])value);
					return;
				}
			}
			BlobRecipientParser.WriteAnsiStringArray(writer, (string[])value);
			return;
			IL_13A:
			ExTraceGlobals.StorageTracer.TraceError<PropType>(0L, "BlobRecipientParser::WritePropValue. Skipping susupported propType={0}.", propType);
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x0014F068 File Offset: 0x0014D268
		private static void WriteStringArray(BinaryWriter writer, string[] strings)
		{
			ushort num = (ushort)strings.Length;
			writer.Write(num);
			for (int i = 0; i < (int)num; i++)
			{
				BlobRecipientParser.WriteString(writer, strings[i]);
			}
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x0014F098 File Offset: 0x0014D298
		private static void WriteAnsiStringArray(BinaryWriter writer, string[] strings)
		{
			ushort num = (ushort)strings.Length;
			writer.Write(num);
			for (int i = 0; i < (int)num; i++)
			{
				BlobRecipientParser.WriteAnsiString(writer, strings[i]);
			}
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0014F0C8 File Offset: 0x0014D2C8
		private static void WriteDateTime(BinaryWriter writer, ExDateTime dateTime)
		{
			long num = dateTime.ToFileTimeUtc();
			writer.Write((uint)num);
			writer.Write((uint)(num >> 32));
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0014F0F0 File Offset: 0x0014D2F0
		private static void WriteBinaryArray(BinaryWriter writer, byte[][] binaryArray)
		{
			writer.Write((ushort)binaryArray.Length);
			for (int i = 0; i < binaryArray.Length; i++)
			{
				BlobRecipientParser.WriteBinary(writer, binaryArray[i]);
			}
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x0014F11E File Offset: 0x0014D31E
		private static void WriteBinary(BinaryWriter writer, byte[] bytes)
		{
			writer.Write((ushort)bytes.Length);
			writer.Write(bytes);
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x0014F131 File Offset: 0x0014D331
		private static void WriteString(BinaryWriter writer, string text)
		{
			writer.Write((ushort)(2 * (text.Length + 1)));
			writer.Write(Encoding.Unicode.GetBytes(text));
			writer.Write(0);
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0014F15C File Offset: 0x0014D35C
		private static void WriteAnsiString(BinaryWriter writer, string text)
		{
			writer.Write((ushort)(text.Length + 1));
			writer.Write(CTSGlobals.AsciiEncoding.GetBytes(text));
			writer.Write(0);
		}
	}
}

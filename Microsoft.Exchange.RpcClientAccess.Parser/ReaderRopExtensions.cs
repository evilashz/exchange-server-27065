using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001FE RID: 510
	internal static class ReaderRopExtensions
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x000233DF File Offset: 0x000215DF
		public static ErrorCode ReadErrorCode(this Reader reader)
		{
			return (ErrorCode)reader.ReadUInt32();
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000233E8 File Offset: 0x000215E8
		public static string ReadFormattedString(this Reader reader, Encoding string8Encoding)
		{
			StringFormatType stringFormatType = (StringFormatType)reader.ReadByte();
			switch (stringFormatType)
			{
			case StringFormatType.NotPresent:
				return null;
			case StringFormatType.EmptyString:
				return string.Empty;
			case StringFormatType.String8:
				return reader.ReadString8(string8Encoding, StringFlags.IncludeNull);
			case StringFormatType.ReduceUnicode:
				return reader.ReadString8(String8Encodings.ReducedUnicode, StringFlags.IncludeNull);
			case StringFormatType.FullUnicode:
				return reader.ReadUnicodeString(StringFlags.IncludeNull);
			default:
				throw new BufferParseException(string.Format("Unrecognized format type: {0}", stringFormatType));
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00023458 File Offset: 0x00021658
		public static NamedProperty[] ReadNamedPropertyArray(this Reader reader, ushort length)
		{
			reader.CheckBoundary((uint)length, NamedProperty.MinimumSize);
			NamedProperty[] array = new NamedProperty[(int)length];
			for (int i = 0; i < (int)length; i++)
			{
				array[i] = NamedProperty.Parse(reader);
			}
			return array;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00023490 File Offset: 0x00021690
		public static Restriction ReadSizeAndRestriction(this Reader reader, WireFormatStyle wireFormatStyle)
		{
			uint num = (uint)reader.ReadUInt16();
			if (num == 0U)
			{
				return null;
			}
			long position = reader.Position;
			Restriction result = Restriction.Parse(reader, wireFormatStyle);
			long position2 = reader.Position;
			if ((uint)(position2 - position) != num)
			{
				throw new BufferParseException("Restriction wasn't the size reported");
			}
			return result;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000234D4 File Offset: 0x000216D4
		public static StoreIdPair[] ReadSizeAndStoreIdPairArray(this Reader reader)
		{
			uint num = reader.ReadUInt32();
			if (num == 0U)
			{
				return null;
			}
			StoreIdPair[] array = new StoreIdPair[num];
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				array[(int)((UIntPtr)num2)] = StoreIdPair.Parse(reader);
			}
			return array;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00023518 File Offset: 0x00021718
		public static StoreId[] ReadSizeAndStoreIdArray(this Reader reader)
		{
			uint length = (uint)reader.ReadUInt16();
			return reader.ReadStoreIdArray(length);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00023534 File Offset: 0x00021734
		public static StoreId[] ReadStoreIdArray(this Reader reader, uint length)
		{
			if (length == 0U)
			{
				return null;
			}
			reader.CheckBoundary(length, 8U);
			StoreId[] array = new StoreId[length];
			for (uint num = 0U; num < length; num += 1U)
			{
				array[(int)((UIntPtr)num)] = StoreId.Parse(reader);
			}
			return array;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00023578 File Offset: 0x00021778
		public static ulong[] ReadUInt64Array(this Reader reader, uint length)
		{
			if (length == 0U)
			{
				return null;
			}
			reader.CheckBoundary(length, 8U);
			ulong[] array = new ulong[length];
			for (uint num = 0U; num < length; num += 1U)
			{
				array[(int)((UIntPtr)num)] = reader.ReadUInt64();
			}
			return array;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000235B4 File Offset: 0x000217B4
		public static StoreLongTermId[] ReadSizeAndStoreLongTermIdArray(this Reader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return Array<StoreLongTermId>.Empty;
			}
			reader.CheckBoundary((uint)num, (uint)StoreLongTermId.Size);
			StoreLongTermId[] array = new StoreLongTermId[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = StoreLongTermId.Parse(reader);
			}
			return array;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00023604 File Offset: 0x00021804
		public static NamedProperty[] ReadSizeAndNamedPropertyArray(this Reader reader)
		{
			ushort num = reader.ReadUInt16();
			if (num == 0)
			{
				return Array<NamedProperty>.Empty;
			}
			return reader.ReadNamedPropertyArray(num);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00023628 File Offset: 0x00021828
		public static PropertyId[] ReadSizeAndPropertyIdArray(this Reader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return Array<PropertyId>.Empty;
			}
			reader.CheckBoundary((uint)num, 2U);
			PropertyId[] array = new PropertyId[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (PropertyId)reader.ReadUInt16();
			}
			return array;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002366C File Offset: 0x0002186C
		public static PropertyProblem[] ReadSizeAndPropertyProblemArray(this Reader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return Array<PropertyProblem>.Empty;
			}
			reader.CheckBoundary((uint)num, PropertyProblem.MinimumSize);
			PropertyProblem[] array = new PropertyProblem[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = PropertyProblem.Parse(reader);
			}
			return array;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000236BC File Offset: 0x000218BC
		public static List<PropertyRow> ReadSizeAndPropertyRowList(this Reader reader, PropertyTag[] columns, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			uint num = reader.ReadCountOrSize(wireFormatStyle);
			reader.CheckBoundary(num, 4U);
			List<PropertyRow> list = new List<PropertyRow>((int)num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				PropertyRow item = PropertyRow.Parse(reader, columns, wireFormatStyle);
				item.ResolveString8Values(string8Encoding);
				list.Add(item);
				num2++;
			}
			return list;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00023708 File Offset: 0x00021908
		public static PropertyTag[] ReadCountAndPropertyTagArray(this Reader reader, FieldLength fieldLength)
		{
			uint num = reader.ReadCountOrSize(fieldLength);
			if (num == 0U)
			{
				return Array<PropertyTag>.Empty;
			}
			reader.CheckBoundary(num, 4U);
			PropertyTag[] array = new PropertyTag[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = reader.ReadPropertyTag();
				num2++;
			}
			return array;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00023758 File Offset: 0x00021958
		public static string[] ReadCountAndUnicodeStringList(this Reader reader, StringFlags flags, FieldLength fieldLength)
		{
			uint num = reader.ReadCountOrSize(fieldLength);
			if (num == 0U)
			{
				return Array<string>.Empty;
			}
			reader.CheckBoundary(num, ReaderRopExtensions.GetMinimumStringEncodingLength(flags, 2U));
			string[] array = new string[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = reader.ReadUnicodeString(flags);
				num2++;
			}
			return array;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000237A8 File Offset: 0x000219A8
		public static string[] ReadCountAndString8List(this Reader reader, Encoding encoding, StringFlags flags, FieldLength fieldLength)
		{
			uint num = reader.ReadCountOrSize(fieldLength);
			if (num == 0U)
			{
				return Array<string>.Empty;
			}
			reader.CheckBoundary(num, ReaderRopExtensions.GetMinimumStringEncodingLength(flags, 1U));
			string[] array = new string[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = reader.ReadString8(encoding, flags);
				num2++;
			}
			return array;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000237F8 File Offset: 0x000219F8
		public static MessageReadState[] ReadSizeAndMessageReadStateArray(this Reader reader)
		{
			uint num = (uint)reader.ReadUInt16();
			long num2 = (long)((ulong)num + (ulong)reader.Position);
			List<MessageReadState> list = new List<MessageReadState>((int)(num / MessageReadState.MinimumSize));
			while (reader.Position < num2)
			{
				list.Add(MessageReadState.Parse(reader));
			}
			return list.ToArray();
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00023840 File Offset: 0x00021A40
		public static LongTermIdRange[] ReadSizeAndLongTermIdRangeArray(this Reader reader)
		{
			uint num = (uint)reader.ReadUInt16();
			long num2 = (long)((ulong)num + (ulong)reader.Position);
			uint num3 = reader.ReadUInt32();
			LongTermIdRange[] array;
			if (num3 == 0U)
			{
				array = Array<LongTermIdRange>.Empty;
			}
			else
			{
				reader.CheckBoundary(num3, 4U);
				array = new LongTermIdRange[num3];
				int num4 = 0;
				while ((long)num4 < (long)((ulong)num3))
				{
					array[num4] = LongTermIdRange.Parse(reader);
					num4++;
				}
			}
			if ((ulong)((uint)num2) != (ulong)reader.Position)
			{
				throw new BufferParseException("LongTermIdRange[] wasn't the size reported.");
			}
			return array;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000238BC File Offset: 0x00021ABC
		public static PropertyValue[] ReadCountAndPropertyValueList(this Reader reader, WireFormatStyle wireFormatStyle)
		{
			return reader.ReadCountAndPropertyValueList(null, wireFormatStyle);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000238C8 File Offset: 0x00021AC8
		public static PropertyValue[] ReadCountAndPropertyValueList(this Reader reader, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			uint num = reader.ReadCountOrSize(wireFormatStyle);
			if (num == 0U)
			{
				return Array<PropertyValue>.Empty;
			}
			reader.CheckBoundary(num, 4U);
			PropertyValue[] array = new PropertyValue[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = reader.ReadPropertyValue(wireFormatStyle);
				if (string8Encoding != null)
				{
					array[num2].ResolveString8Values(string8Encoding);
				}
				num2++;
			}
			return array;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00023928 File Offset: 0x00021B28
		public static PropertyValue[][] ReadCountAndPropertyValueListList(this Reader reader, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			uint num = reader.ReadCountOrSize(wireFormatStyle);
			if (num == 0U)
			{
				return Array<PropertyValue[]>.Empty;
			}
			PropertyValue[][] array = new PropertyValue[num][];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = reader.ReadCountAndPropertyValueList(string8Encoding, wireFormatStyle);
				num2++;
			}
			return array;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00023968 File Offset: 0x00021B68
		public static int[] ReadSizeAndIntegerArray(this Reader reader, FieldLength fieldLength)
		{
			uint num;
			if (fieldLength == FieldLength.WordSize)
			{
				num = (uint)reader.ReadUInt16();
			}
			else
			{
				num = reader.ReadUInt32();
			}
			if (num == 0U)
			{
				return Array<int>.Empty;
			}
			reader.CheckBoundary(num, 4U);
			int[] array = new int[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = reader.ReadInt32();
				num2++;
			}
			return array;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000239BC File Offset: 0x00021BBC
		public static ModifyTableRow[] ReadSizeAndModifyTableRowArray(this Reader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return Array<ModifyTableRow>.Empty;
			}
			reader.CheckBoundary((uint)num, 3U);
			ModifyTableRow[] array = new ModifyTableRow[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = ModifyTableRow.Parse(reader);
			}
			return array;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00023A07 File Offset: 0x00021C07
		private static uint GetMinimumStringEncodingLength(StringFlags flags, uint terminatorLength)
		{
			if ((flags & StringFlags.Sized) == StringFlags.Sized)
			{
				return 1U;
			}
			if ((flags & StringFlags.Sized16) == StringFlags.Sized16)
			{
				return 2U;
			}
			if ((flags & StringFlags.Sized32) == StringFlags.Sized32)
			{
				return 4U;
			}
			return terminatorLength;
		}
	}
}

using System;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001EF RID: 495
	internal static class WriterMapiHttpExtensions
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x0002038C File Offset: 0x0001E58C
		public static void WriteNspiState(this Writer writer, NspiState value)
		{
			writer.WriteBool(value != null);
			if (value != null)
			{
				writer.WriteInt32(value.SortType);
				writer.WriteInt32(value.ContainerId);
				writer.WriteInt32(value.CurrentRecord);
				writer.WriteInt32(value.Delta);
				writer.WriteInt32(value.Position);
				writer.WriteInt32(value.TotalRecords);
				writer.WriteInt32(value.CodePage);
				writer.WriteInt32(value.TemplateLocale);
				writer.WriteInt32(value.SortLocale);
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00020418 File Offset: 0x0001E618
		public static void WriteNullableInt32(this Writer writer, int? value)
		{
			bool flag = value != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteInt32(value.Value);
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00020444 File Offset: 0x0001E644
		public static void WriteNullableUInt32(this Writer writer, uint? value)
		{
			bool flag = value != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteUInt32(value.Value);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00020470 File Offset: 0x0001E670
		public static void WriteNullableCountAndString8List(this Writer writer, string[] values, Encoding encoding, StringFlags flags, FieldLength fieldLength)
		{
			bool flag = values != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteCountAndString8List(values, encoding, flags, fieldLength);
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002049C File Offset: 0x0001E69C
		public static void WriteNullableSizeAndIntegerArray(this Writer writer, int[] integers, FieldLength fieldLength)
		{
			bool flag = integers != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteSizeAndIntegerArray(integers, fieldLength);
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000204C4 File Offset: 0x0001E6C4
		public static void WriteNullableRestriction(this Writer writer, Restriction restriction, Encoding string8Encoding)
		{
			bool flag = restriction != null;
			writer.WriteBool(flag);
			if (flag)
			{
				restriction.Serialize(writer, string8Encoding, WireFormatStyle.Nspi);
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000204EC File Offset: 0x0001E6EC
		public static void WriteNullableNamedProperty(this Writer writer, NamedProperty namedProperty)
		{
			bool flag = namedProperty != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteGuid(namedProperty.Guid);
				writer.WriteUInt32(namedProperty.Id);
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00020524 File Offset: 0x0001E724
		public static void WriteNullableCountAndPropertyTagArray(this Writer writer, PropertyTag[] propertyTags, FieldLength fieldLength)
		{
			bool flag = propertyTags != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteCountAndPropertyTagArray(propertyTags, fieldLength);
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002054C File Offset: 0x0001E74C
		public static void WriteNullableCountAndPropertyValueList(this Writer writer, PropertyValue[] propertyValues, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			bool flag = propertyValues != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteCountAndPropertyValueList(propertyValues, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00020574 File Offset: 0x0001E774
		public static void WriteNullableCountAndPropertyValueListList(this Writer writer, PropertyValue[][] propertyValues, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			bool flag = propertyValues != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteCountAndPropertyValueListList(propertyValues, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002059C File Offset: 0x0001E79C
		public static void WriteNullableAsciiString(this Writer writer, string value)
		{
			bool flag = value != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteAsciiString(value, StringFlags.IncludeNull);
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000205C4 File Offset: 0x0001E7C4
		public static void WriteNullableByteArrayList(this Writer writer, byte[][] values, FieldLength lengthSize)
		{
			bool flag = values != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteCountAndByteArrayList(values, lengthSize);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000205EC File Offset: 0x0001E7EC
		public static void WriteNullableCountAndUnicodeStringList(this Writer writer, string[] values, StringFlags flags, FieldLength fieldLength)
		{
			bool flag = values != null;
			writer.WriteBool(flag);
			if (flag)
			{
				writer.WriteCountAndUnicodeStringList(values, flags, fieldLength);
			}
		}
	}
}

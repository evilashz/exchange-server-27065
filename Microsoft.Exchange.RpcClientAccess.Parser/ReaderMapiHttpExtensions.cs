using System;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001EA RID: 490
	internal static class ReaderMapiHttpExtensions
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00020048 File Offset: 0x0001E248
		public static NspiState ReadNspiState(this Reader reader)
		{
			if (reader.ReadBool())
			{
				int sortType = reader.ReadInt32();
				int containerId = reader.ReadInt32();
				int currentRecord = reader.ReadInt32();
				int delta = reader.ReadInt32();
				int position = reader.ReadInt32();
				int totalRecords = reader.ReadInt32();
				int codePage = reader.ReadInt32();
				int templateLocale = reader.ReadInt32();
				int sortLocale = reader.ReadInt32();
				return new NspiState(sortType, containerId, currentRecord, delta, position, totalRecords, codePage, templateLocale, sortLocale);
			}
			return null;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public static int? ReadNullableInt32(this Reader reader)
		{
			if (reader.ReadBool())
			{
				return new int?(reader.ReadInt32());
			}
			return null;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000200E4 File Offset: 0x0001E2E4
		public static uint? ReadNullableUInt32(this Reader reader)
		{
			if (reader.ReadBool())
			{
				return new uint?(reader.ReadUInt32());
			}
			return null;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002010E File Offset: 0x0001E30E
		public static string[] ReadNullableCountAndString8List(this Reader reader, Encoding encoding, StringFlags flags, FieldLength fieldLength)
		{
			if (reader.ReadBool())
			{
				return reader.ReadCountAndString8List(encoding, flags, fieldLength);
			}
			return null;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00020123 File Offset: 0x0001E323
		public static int[] ReadNullableSizeAndIntegerArray(this Reader reader, FieldLength fieldLength)
		{
			if (reader.ReadBool())
			{
				return reader.ReadSizeAndIntegerArray(fieldLength);
			}
			return null;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00020138 File Offset: 0x0001E338
		public static Restriction ReadNullableRestriction(this Reader reader, Encoding string8Encoding)
		{
			Restriction restriction = null;
			if (reader.ReadBool())
			{
				restriction = Restriction.Parse(reader, WireFormatStyle.Nspi);
				restriction.ResolveString8Values(string8Encoding);
			}
			return restriction;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00020160 File Offset: 0x0001E360
		public static NamedProperty ReadNullableNamedProperty(this Reader reader)
		{
			if (reader.ReadBool())
			{
				Guid guid = reader.ReadGuid();
				uint id = reader.ReadUInt32();
				return new NamedProperty(guid, id);
			}
			return null;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002018C File Offset: 0x0001E38C
		public static PropertyTag[] ReadNullableCountAndPropertyTagArray(this Reader reader, FieldLength fieldLength)
		{
			if (reader.ReadBool())
			{
				return reader.ReadCountAndPropertyTagArray(fieldLength);
			}
			return null;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002019F File Offset: 0x0001E39F
		public static PropertyValue[] ReadNullableCountAndPropertyValueList(this Reader reader, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (reader.ReadBool())
			{
				return reader.ReadCountAndPropertyValueList(string8Encoding, wireFormatStyle);
			}
			return null;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000201B3 File Offset: 0x0001E3B3
		public static PropertyValue[][] ReadNullableCountAndPropertyValueListList(this Reader reader, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (reader.ReadBool())
			{
				return reader.ReadCountAndPropertyValueListList(string8Encoding, wireFormatStyle);
			}
			return null;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000201C7 File Offset: 0x0001E3C7
		public static string ReadNullableAsciiString(this Reader reader, StringFlags flags)
		{
			if (reader.ReadBool())
			{
				return reader.ReadAsciiString(flags);
			}
			return null;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000201DA File Offset: 0x0001E3DA
		public static byte[][] ReadNullableCountAndByteArrayList(this Reader reader, FieldLength fieldLength)
		{
			if (reader.ReadBool())
			{
				return reader.ReadCountAndByteArrayList(fieldLength);
			}
			return null;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000201ED File Offset: 0x0001E3ED
		public static string[] ReadNullableCountAndUnicodeStringList(this Reader reader, StringFlags flags, FieldLength fieldLength)
		{
			if (reader.ReadBool())
			{
				return reader.ReadCountAndUnicodeStringList(flags, fieldLength);
			}
			return null;
		}
	}
}

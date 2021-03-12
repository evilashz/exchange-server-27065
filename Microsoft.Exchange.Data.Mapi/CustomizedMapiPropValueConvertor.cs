using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000018 RID: 24
	internal static class CustomizedMapiPropValueConvertor
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003894 File Offset: 0x00001A94
		public static object ExtractNullableEnhancedTimeSpanFromDays(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(EnhancedTimeSpan?) == propertyDefinition.Type)
			{
				object obj = null;
				if (MapiPropValueConvertor.TryCastValueToExtract(value, typeof(double), out obj))
				{
					double num = (double)obj;
					return (0.0 == num || -1.0 == num) ? null : new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays((double)obj));
				}
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000391C File Offset: 0x00001B1C
		public static PropValue PackNullableEnhancedTimeSpanIntoDays(object value, MapiPropertyDefinition propertyDefinition)
		{
			if (value == null || typeof(EnhancedTimeSpan) == value.GetType())
			{
				PropType type = propertyDefinition.PropertyTag.ValueType();
				object value2 = null;
				if (MapiPropValueConvertor.TryCastValueToPack((value == null) ? 0.0 : ((EnhancedTimeSpan)value).TotalDays, type, out value2))
				{
					return new PropValue(propertyDefinition.PropertyTag, value2);
				}
			}
			throw MapiPropValueConvertor.ConstructPackingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003998 File Offset: 0x00001B98
		public static object ExtractMultiAnsiStringsFromBytes(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(string) == propertyDefinition.Type && propertyDefinition.IsMultivalued)
			{
				byte[] bytes = value.GetBytes();
				List<string> list = new List<string>();
				int num = 0;
				int num2 = 0;
				while (bytes.Length > num2)
				{
					if (bytes[num2] == 0)
					{
						list.Add(Encoding.ASCII.GetString(bytes, num, num2 - num));
						num = 1 + num2;
					}
					num2++;
				}
				return new MultiValuedProperty<string>(propertyDefinition.IsReadOnly, propertyDefinition, list);
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A1C File Offset: 0x00001C1C
		public static PropValue PackMultiAnsiStringsIntoBytes(object value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(MultiValuedProperty<string>) == value.GetType())
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)value;
				List<byte> list = new List<byte>();
				foreach (string s in multiValuedProperty)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(s);
					list.AddRange(bytes);
					if (bytes[bytes.Length - 1] != 0)
					{
						list.Add(0);
					}
				}
				return new PropValue(propertyDefinition.PropertyTag, list.ToArray());
			}
			throw MapiPropValueConvertor.ConstructPackingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003ACC File Offset: 0x00001CCC
		public static object ExtractNullableEnhancedTimeSpanFromSeconds(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(EnhancedTimeSpan?) == propertyDefinition.Type)
			{
				object obj = null;
				if (MapiPropValueConvertor.TryCastValueToExtract(value, typeof(double), out obj))
				{
					return new EnhancedTimeSpan?(EnhancedTimeSpan.FromSeconds((double)obj));
				}
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003B28 File Offset: 0x00001D28
		public static PropValue PackNullableEnhancedTimeSpanIntoSeconds(object value, MapiPropertyDefinition propertyDefinition)
		{
			if (value != null && typeof(EnhancedTimeSpan) == value.GetType())
			{
				object value2 = null;
				if (MapiPropValueConvertor.TryCastValueToPack(((EnhancedTimeSpan)value).TotalSeconds, propertyDefinition.PropertyTag.ValueType(), out value2))
				{
					return new PropValue(propertyDefinition.PropertyTag, value2);
				}
			}
			throw MapiPropValueConvertor.ConstructPackingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B94 File Offset: 0x00001D94
		public static object ExtractNullableUnlimitedByteQuantifiedSizeFromKilobytes(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(Unlimited<ByteQuantifiedSize>?) == propertyDefinition.Type)
			{
				object obj = null;
				if (MapiPropValueConvertor.TryCastValueToExtract(value, typeof(long), out obj))
				{
					long num = (long)obj;
					if (0L > num)
					{
						return new Unlimited<ByteQuantifiedSize>?(Unlimited<ByteQuantifiedSize>.UnlimitedValue);
					}
					return new Unlimited<ByteQuantifiedSize>?(new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(checked((ulong)num))));
				}
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C10 File Offset: 0x00001E10
		public static PropValue PackNullableUnlimitedByteQuantifiedSizeIntoKilobytes(object value, MapiPropertyDefinition propertyDefinition)
		{
			if (value == null || typeof(Unlimited<ByteQuantifiedSize>) == value.GetType())
			{
				long num = -1L;
				if (value != null)
				{
					Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)value;
					if (unlimited.IsUnlimited)
					{
						num = -1L;
					}
					else
					{
						num = checked((long)unlimited.Value.ToKB());
					}
				}
				object value2 = null;
				if (MapiPropValueConvertor.TryCastValueToPack(num, propertyDefinition.PropertyTag.ValueType(), out value2))
				{
					return new PropValue(propertyDefinition.PropertyTag, value2);
				}
			}
			throw MapiPropValueConvertor.ConstructPackingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C98 File Offset: 0x00001E98
		public static object ExtractIpV4StringFromIpV6Bytes(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (!(typeof(string) == propertyDefinition.Type))
			{
				throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
			}
			byte[] bytes = value.GetBytes();
			if (16 == bytes.Length)
			{
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					bytes[4],
					bytes[5],
					bytes[6],
					bytes[7]
				});
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ErrorByteArrayLength(16.ToString(), bytes.Length.ToString()));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D38 File Offset: 0x00001F38
		public static object ExtractMacAddressStringFromBytes(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (!(typeof(string) == propertyDefinition.Type))
			{
				throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
			}
			byte[] bytes = value.GetBytes();
			if (6 == bytes.Length)
			{
				return string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}", new object[]
				{
					bytes[0],
					bytes[1],
					bytes[2],
					bytes[3],
					bytes[4],
					bytes[5]
				});
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ErrorByteArrayLength(6.ToString(), bytes.Length.ToString()));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public static object ExtractUnlimitedByteQuantifiedSizeFromBytes(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(Unlimited<ByteQuantifiedSize>) == propertyDefinition.Type)
			{
				object obj = null;
				if (MapiPropValueConvertor.TryCastValueToExtract(value, typeof(long), out obj))
				{
					long num = (long)obj;
					if (0L > num)
					{
						return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
					}
					return new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(checked((ulong)num)));
				}
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003E60 File Offset: 0x00002060
		public static object ExtractUnlimitedByteQuantifiedSizeFromPages(PropValue value, MapiPropertyDefinition propertyDefinition)
		{
			if (typeof(Unlimited<ByteQuantifiedSize>) == propertyDefinition.Type)
			{
				object obj = null;
				if (MapiPropValueConvertor.TryCastValueToExtract(value, typeof(int), out obj))
				{
					long num = (long)((int)obj);
					if (0L > num)
					{
						return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
					}
					return new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(checked((ulong)num * 32UL * 1024UL)));
				}
			}
			throw MapiPropValueConvertor.ConstructExtractingException(value, propertyDefinition, Strings.ConstantNa);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003EDC File Offset: 0x000020DC
		public static PropValue PackNullableValueToBool(object value, MapiPropertyDefinition propertyDefinition)
		{
			if (value == null || typeof(bool) == value.GetType())
			{
				PropType type = propertyDefinition.PropertyTag.ValueType();
				object value2 = null;
				if (MapiPropValueConvertor.TryCastValueToPack((value == null) ? false : value, type, out value2))
				{
					return new PropValue(propertyDefinition.PropertyTag, value2);
				}
			}
			throw MapiPropValueConvertor.ConstructPackingException(value, propertyDefinition, Strings.ConstantNa);
		}
	}
}

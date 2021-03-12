using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PropertyBlob
{
	// Token: 0x02000013 RID: 19
	public static class PropertyBlob
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000C884 File Offset: 0x0000AA84
		private static bool TryFindDictionaryEntry(byte[] blob, int startOffset, int propertyCount, ushort id, out int index)
		{
			int num = 0;
			int i = propertyCount - 1;
			while (i >= num)
			{
				index = (num + i) / 2;
				int num2 = id.CompareTo((ushort)ParseSerialize.ParseInt16(blob, startOffset + 8 + index * 8 + 2));
				if (num2 < 0)
				{
					i = index - 1;
				}
				else
				{
					if (num2 <= 0)
					{
						return true;
					}
					num = index + 1;
				}
			}
			index = num;
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000C8DC File Offset: 0x0000AADC
		private static ushort IdFromTag(uint tag)
		{
			return (ushort)(tag >> 16);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000C8E3 File Offset: 0x0000AAE3
		private static PropertyType PropertyTypeFromTag(uint tag)
		{
			return (PropertyType)(tag & 65535U);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		internal static PropertyBlob.CompressedPropertyType GetCompressedType(PropertyType propertyType)
		{
			if (propertyType <= PropertyType.Actions)
			{
				if (propertyType <= PropertyType.Unicode)
				{
					switch (propertyType)
					{
					case PropertyType.Null:
						return PropertyBlob.CompressedPropertyType.Null;
					case PropertyType.Int16:
						return PropertyBlob.CompressedPropertyType.Int16;
					case PropertyType.Int32:
						return PropertyBlob.CompressedPropertyType.Int32;
					case PropertyType.Real32:
						return PropertyBlob.CompressedPropertyType.Real32;
					case PropertyType.Real64:
						return PropertyBlob.CompressedPropertyType.Real64;
					case PropertyType.Currency:
						return PropertyBlob.CompressedPropertyType.Currency;
					case PropertyType.AppTime:
						return PropertyBlob.CompressedPropertyType.AppTime;
					case (PropertyType)8:
					case (PropertyType)9:
					case PropertyType.Error:
					case (PropertyType)12:
						break;
					case PropertyType.Boolean:
						return PropertyBlob.CompressedPropertyType.Boolean;
					case PropertyType.Object:
						return PropertyBlob.CompressedPropertyType.Object;
					default:
						if (propertyType == PropertyType.Int64)
						{
							return PropertyBlob.CompressedPropertyType.Int64;
						}
						if (propertyType == PropertyType.Unicode)
						{
							return PropertyBlob.CompressedPropertyType.Unicode;
						}
						break;
					}
				}
				else
				{
					if (propertyType == PropertyType.SysTime)
					{
						return PropertyBlob.CompressedPropertyType.SysTime;
					}
					if (propertyType == PropertyType.Guid)
					{
						return PropertyBlob.CompressedPropertyType.Guid;
					}
					switch (propertyType)
					{
					case PropertyType.SvrEid:
						return PropertyBlob.CompressedPropertyType.SvrEid;
					case PropertyType.SRestriction:
						return PropertyBlob.CompressedPropertyType.SRestriction;
					case PropertyType.Actions:
						return PropertyBlob.CompressedPropertyType.Actions;
					}
				}
			}
			else if (propertyType <= PropertyType.MVInt64)
			{
				if (propertyType == PropertyType.Binary)
				{
					return PropertyBlob.CompressedPropertyType.Binary;
				}
				switch (propertyType)
				{
				case PropertyType.MVInt16:
					return PropertyBlob.CompressedPropertyType.MVInt16;
				case PropertyType.MVInt32:
					return PropertyBlob.CompressedPropertyType.MVInt32;
				case PropertyType.MVReal32:
					return PropertyBlob.CompressedPropertyType.MVReal32;
				case PropertyType.MVReal64:
					return PropertyBlob.CompressedPropertyType.MVReal64;
				case PropertyType.MVCurrency:
					return PropertyBlob.CompressedPropertyType.MVCurrency;
				case PropertyType.MVAppTime:
					return PropertyBlob.CompressedPropertyType.MVAppTime;
				default:
					if (propertyType == PropertyType.MVInt64)
					{
						return PropertyBlob.CompressedPropertyType.MVInt64;
					}
					break;
				}
			}
			else if (propertyType <= PropertyType.MVSysTime)
			{
				if (propertyType == PropertyType.MVUnicode)
				{
					return PropertyBlob.CompressedPropertyType.MVUnicode;
				}
				if (propertyType == PropertyType.MVSysTime)
				{
					return PropertyBlob.CompressedPropertyType.MVSysTime;
				}
			}
			else
			{
				if (propertyType == PropertyType.MVGuid)
				{
					return PropertyBlob.CompressedPropertyType.MVGuid;
				}
				if (propertyType == PropertyType.MVBinary)
				{
					return PropertyBlob.CompressedPropertyType.MVBinary;
				}
			}
			throw new ArgumentException(string.Format("invalid or unexpected property type: {0:X}", (ushort)propertyType));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000CA90 File Offset: 0x0000AC90
		internal static bool CompatibleTypes(PropertyBlob.CompressedPropertyType persistedCompressedType, PropertyBlob.CompressedPropertyType desiredCompressedType)
		{
			if (persistedCompressedType > PropertyBlob.CompressedPropertyType.Real32)
			{
				if (persistedCompressedType <= PropertyBlob.CompressedPropertyType.SRestriction)
				{
					if (persistedCompressedType != PropertyBlob.CompressedPropertyType.Object && persistedCompressedType != PropertyBlob.CompressedPropertyType.SRestriction)
					{
						return false;
					}
				}
				else if (persistedCompressedType != PropertyBlob.CompressedPropertyType.Actions && persistedCompressedType != PropertyBlob.CompressedPropertyType.SvrEid)
				{
					return false;
				}
				return desiredCompressedType == PropertyBlob.CompressedPropertyType.Binary;
			}
			if (persistedCompressedType == PropertyBlob.CompressedPropertyType.Int16)
			{
				return desiredCompressedType == PropertyBlob.CompressedPropertyType.Int32 || desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64;
			}
			if (persistedCompressedType == PropertyBlob.CompressedPropertyType.Int32)
			{
				return desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64;
			}
			if (persistedCompressedType == PropertyBlob.CompressedPropertyType.Real32)
			{
				return desiredCompressedType == PropertyBlob.CompressedPropertyType.Real64;
			}
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000CAF2 File Offset: 0x0000ACF2
		private static SerializedValue.ValueFormat GetValueFormat(PropertyBlob.CompressedPropertyType compressedType)
		{
			return (SerializedValue.ValueFormat)(compressedType & (PropertyBlob.CompressedPropertyType)248);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		private static PropertyType GetPropertyType(PropertyBlob.CompressedPropertyType compressedType)
		{
			if (compressedType <= PropertyBlob.CompressedPropertyType.Object)
			{
				if (compressedType <= PropertyBlob.CompressedPropertyType.Real32)
				{
					if (compressedType <= PropertyBlob.CompressedPropertyType.Int16)
					{
						if (compressedType == PropertyBlob.CompressedPropertyType.Null)
						{
							return PropertyType.Null;
						}
						if (compressedType == PropertyBlob.CompressedPropertyType.Boolean)
						{
							return PropertyType.Boolean;
						}
						if (compressedType == PropertyBlob.CompressedPropertyType.Int16)
						{
							return PropertyType.Int16;
						}
					}
					else if (compressedType <= PropertyBlob.CompressedPropertyType.Int64)
					{
						if (compressedType == PropertyBlob.CompressedPropertyType.Int32)
						{
							return PropertyType.Int32;
						}
						if (compressedType == PropertyBlob.CompressedPropertyType.Int64)
						{
							return PropertyType.Int64;
						}
					}
					else
					{
						if (compressedType == PropertyBlob.CompressedPropertyType.Currency)
						{
							return PropertyType.Currency;
						}
						if (compressedType == PropertyBlob.CompressedPropertyType.Real32)
						{
							return PropertyType.Real32;
						}
					}
				}
				else if (compressedType <= PropertyBlob.CompressedPropertyType.SysTime)
				{
					if (compressedType == PropertyBlob.CompressedPropertyType.Real64)
					{
						return PropertyType.Real64;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.AppTime)
					{
						return PropertyType.AppTime;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.SysTime)
					{
						return PropertyType.SysTime;
					}
				}
				else if (compressedType <= PropertyBlob.CompressedPropertyType.Unicode)
				{
					if (compressedType == PropertyBlob.CompressedPropertyType.Guid)
					{
						return PropertyType.Guid;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.Unicode)
					{
						return PropertyType.Unicode;
					}
				}
				else
				{
					if (compressedType == PropertyBlob.CompressedPropertyType.Binary)
					{
						return PropertyType.Binary;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.Object)
					{
						return PropertyType.Object;
					}
				}
			}
			else if (compressedType <= PropertyBlob.CompressedPropertyType.MVCurrency)
			{
				if (compressedType <= PropertyBlob.CompressedPropertyType.SvrEid)
				{
					if (compressedType == PropertyBlob.CompressedPropertyType.SRestriction)
					{
						return PropertyType.SRestriction;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.Actions)
					{
						return PropertyType.Actions;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.SvrEid)
					{
						return PropertyType.SvrEid;
					}
				}
				else if (compressedType <= PropertyBlob.CompressedPropertyType.MVInt32)
				{
					if (compressedType == PropertyBlob.CompressedPropertyType.MVInt16)
					{
						return PropertyType.MVInt16;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.MVInt32)
					{
						return PropertyType.MVInt32;
					}
				}
				else
				{
					if (compressedType == PropertyBlob.CompressedPropertyType.MVInt64)
					{
						return PropertyType.MVInt64;
					}
					if (compressedType == PropertyBlob.CompressedPropertyType.MVCurrency)
					{
						return PropertyType.MVCurrency;
					}
				}
			}
			else if (compressedType <= PropertyBlob.CompressedPropertyType.MVAppTime)
			{
				if (compressedType == PropertyBlob.CompressedPropertyType.MVReal32)
				{
					return PropertyType.MVReal32;
				}
				if (compressedType == PropertyBlob.CompressedPropertyType.MVReal64)
				{
					return PropertyType.MVReal64;
				}
				if (compressedType == PropertyBlob.CompressedPropertyType.MVAppTime)
				{
					return PropertyType.MVAppTime;
				}
			}
			else if (compressedType <= PropertyBlob.CompressedPropertyType.MVGuid)
			{
				if (compressedType == PropertyBlob.CompressedPropertyType.MVSysTime)
				{
					return PropertyType.MVSysTime;
				}
				if (compressedType == PropertyBlob.CompressedPropertyType.MVGuid)
				{
					return PropertyType.MVGuid;
				}
			}
			else
			{
				if (compressedType == PropertyBlob.CompressedPropertyType.MVUnicode)
				{
					return PropertyType.MVUnicode;
				}
				if (compressedType == PropertyBlob.CompressedPropertyType.MVBinary)
				{
					return PropertyType.MVBinary;
				}
			}
			throw new InvalidBlobException("invalid dictionary entry - compressedType");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000CD04 File Offset: 0x0000AF04
		[Conditional("DEBUG")]
		private static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw new Exception(message);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public static byte[] BuildBlob(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties)
		{
			return PropertyBlob.BuildBlob(properties, null);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		public static byte[] BuildBlob(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties, HashSet<ushort> defaultPromotedPropertyIds)
		{
			if (properties == null || properties.Count == 0)
			{
				return null;
			}
			List<ushort> list = new List<ushort>(properties.Count);
			int num = 8;
			foreach (KeyValuePair<ushort, KeyValuePair<StorePropTag, object>> keyValuePair in properties)
			{
				if (keyValuePair.Value.Value != null || defaultPromotedPropertyIds == null || !defaultPromotedPropertyIds.Contains(keyValuePair.Key))
				{
					int serializedSize = PropertyBlob.GetSerializedSize(keyValuePair.Value.Key.PropTag, keyValuePair.Value.Value);
					PropertyBlob.ThrowIfBlobOverflow(num, serializedSize + 8);
					num += 8 + serializedSize;
					list.Add(keyValuePair.Value.Key.PropId);
				}
			}
			if (num == 8)
			{
				return null;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 8 + list.Count * 8;
			ParseSerialize.SerializeInt32(1349481040, array, 0);
			ParseSerialize.SerializeInt16(768, array, 4);
			ParseSerialize.SerializeInt16((short)list.Count, array, 6);
			list.Sort();
			foreach (ushort key in list)
			{
				KeyValuePair<StorePropTag, object> keyValuePair2 = properties[key];
				PropertyBlob.AddProperty(array, ref num2, ref num3, keyValuePair2.Key.PropTag, keyValuePair2.Value);
			}
			return array;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public static byte[] BuildBlob(IList<StorePropTag> propTags, IList<object> propValues)
		{
			int num = 8 + propTags.Count * 8;
			for (int i = 0; i < propTags.Count; i++)
			{
				int serializedSize = PropertyBlob.GetSerializedSize(propTags[i].PropTag, propValues[i]);
				PropertyBlob.ThrowIfBlobOverflow(num, serializedSize);
				num += serializedSize;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 8 + propTags.Count * 8;
			ParseSerialize.SerializeInt32(1349481040, array, 0);
			ParseSerialize.SerializeInt16(768, array, 4);
			ParseSerialize.SerializeInt16((short)propTags.Count, array, 6);
			for (int j = 0; j < propTags.Count; j++)
			{
				PropertyBlob.AddProperty(array, ref num2, ref num3, propTags[j].PropTag, propValues[j]);
			}
			return array;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000CF88 File Offset: 0x0000B188
		public static byte[] BuildBlob(IList<uint> propTags, IList<object> propValues)
		{
			int num = 8 + propTags.Count * 8;
			for (int i = 0; i < propTags.Count; i++)
			{
				int serializedSize = PropertyBlob.GetSerializedSize(propTags[i], propValues[i]);
				PropertyBlob.ThrowIfBlobOverflow(num, serializedSize);
				num += serializedSize;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 8 + propTags.Count * 8;
			ParseSerialize.SerializeInt32(1349481040, array, 0);
			ParseSerialize.SerializeInt16(768, array, 4);
			ParseSerialize.SerializeInt16((short)propTags.Count, array, 6);
			for (int j = 0; j < propTags.Count; j++)
			{
				PropertyBlob.AddProperty(array, ref num2, ref num3, propTags[j], propValues[j]);
			}
			return array;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000D040 File Offset: 0x0000B240
		public static byte[] PromoteProperties(byte[] onPageBlob, byte[] offPageBlob, HashSet<ushort> additionalPromotedProperties)
		{
			PropertyBlob.BlobReader onPageBlobReader = new PropertyBlob.BlobReader(onPageBlob, 0);
			if (!PropertyBlob.IsPromotionNecessary(onPageBlobReader, additionalPromotedProperties))
			{
				return onPageBlob;
			}
			PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(offPageBlob, 0);
			Dictionary<ushort, KeyValuePair<StorePropTag, object>> dictionary = new Dictionary<ushort, KeyValuePair<StorePropTag, object>>(onPageBlobReader.PropertyCount + additionalPromotedProperties.Count);
			List<ushort> list = new List<ushort>(onPageBlobReader.PropertyCount + additionalPromotedProperties.Count);
			int num = 8;
			for (int i = 0; i < onPageBlobReader.PropertyCount; i++)
			{
				uint propertyTag = onPageBlobReader.GetPropertyTag(i);
				StorePropTag key = StorePropTag.CreateWithoutInfo(propertyTag);
				object propertyValue = onPageBlobReader.GetPropertyValue(i);
				if (propertyValue != null || additionalPromotedProperties.Contains(key.PropId) || blobReader.TestIfPropertyPresent(key.PropId))
				{
					dictionary.Add(key.PropId, new KeyValuePair<StorePropTag, object>(key, propertyValue));
					int serializedSize = PropertyBlob.GetSerializedSize(key.PropTag, propertyValue);
					PropertyBlob.ThrowIfBlobOverflow(num, serializedSize + 8);
					num += 8 + serializedSize;
					list.Add(key.PropId);
				}
			}
			int num2 = num;
			int count = list.Count;
			if (num < 3110)
			{
				HashSet<ushort> hashSet = null;
				for (;;)
				{
					num = num2;
					list.RemoveRange(count, list.Count - count);
					int num3 = 0;
					ushort item = ushort.MaxValue;
					foreach (ushort num4 in additionalPromotedProperties)
					{
						if (!onPageBlobReader.TestIfPropertyPresent(num4) && (hashSet == null || !hashSet.Contains(num4)))
						{
							int index;
							StorePropTag key2;
							object value;
							if (blobReader.TryFindPropertyById(num4, out index))
							{
								int propertyValueSize = blobReader.GetPropertyValueSize(index);
								if (propertyValueSize >= 512)
								{
									if (hashSet == null)
									{
										hashSet = new HashSet<ushort>();
									}
									hashSet.Add(num4);
									continue;
								}
								key2 = StorePropTag.CreateWithoutInfo(blobReader.GetPropertyTag(index));
								value = blobReader.GetPropertyValue(index);
							}
							else
							{
								key2 = StorePropTag.CreateWithoutInfo(num4, PropertyType.Null);
								value = null;
							}
							dictionary[num4] = new KeyValuePair<StorePropTag, object>(key2, value);
							int serializedSize2 = PropertyBlob.GetSerializedSize(key2.PropTag, value);
							if (serializedSize2 + 8 > num3)
							{
								num3 = serializedSize2 + 8;
								item = num4;
							}
							PropertyBlob.ThrowIfBlobOverflow(num, serializedSize2 + 8);
							num += 8 + serializedSize2;
							if (num > 3110)
							{
								break;
							}
							list.Add(num4);
						}
					}
					if (num <= 3110)
					{
						break;
					}
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num3 != 0, "We must have some evictable property here");
					if (hashSet == null)
					{
						hashSet = new HashSet<ushort>();
					}
					hashSet.Add(item);
				}
			}
			if (num == num2)
			{
				return onPageBlob;
			}
			byte[] array = new byte[num];
			int num5 = 0;
			int num6 = 8 + list.Count * 8;
			ParseSerialize.SerializeInt32(1349481040, array, 0);
			ParseSerialize.SerializeInt16(768, array, 4);
			ParseSerialize.SerializeInt16((short)list.Count, array, 6);
			list.Sort();
			foreach (ushort key3 in list)
			{
				KeyValuePair<StorePropTag, object> keyValuePair = dictionary[key3];
				PropertyBlob.AddProperty(array, ref num5, ref num6, keyValuePair.Key.PropTag, keyValuePair.Value);
			}
			return array;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000D380 File Offset: 0x0000B580
		private static void AddProperty(byte[] blob, ref int currentPropertyCount, ref int currentValueOffset, uint tag, object value)
		{
			PropertyBlob.DictionaryEntry dictionaryEntry = PropertyBlob.BuildEntry(tag, value, currentValueOffset);
			int num;
			PropertyBlob.TryFindDictionaryEntry(blob, 0, currentPropertyCount, PropertyBlob.IdFromTag(tag), out num);
			int num2 = 8 + num * 8;
			if (num != currentPropertyCount)
			{
				Buffer.BlockCopy(blob, num2, blob, num2 + 8, (currentPropertyCount - num) * 8);
			}
			ParseSerialize.SerializeInt16((short)dictionaryEntry.Id, blob, num2 + 2);
			blob[num2] = (byte)dictionaryEntry.CompressedType;
			blob[num2 + 1] = (byte)dictionaryEntry.Format;
			ParseSerialize.SerializeInt32(dictionaryEntry.Offset, blob, num2 + 4);
			currentPropertyCount++;
			SerializedValue.ValueFormat format = dictionaryEntry.Format;
			if (format <= SerializedValue.ValueFormat.Binary)
			{
				if (format <= SerializedValue.ValueFormat.Int64)
				{
					if (format <= SerializedValue.ValueFormat.Boolean)
					{
						if (format != SerializedValue.ValueFormat.FormatModifierShift && format != SerializedValue.ValueFormat.Boolean)
						{
							return;
						}
						return;
					}
					else
					{
						if (format == SerializedValue.ValueFormat.Int16 || format == SerializedValue.ValueFormat.Int32)
						{
							return;
						}
						if (format != SerializedValue.ValueFormat.Int64)
						{
							return;
						}
					}
				}
				else if (format <= SerializedValue.ValueFormat.DateTime)
				{
					if (format != SerializedValue.ValueFormat.Single && format != SerializedValue.ValueFormat.Double && format != SerializedValue.ValueFormat.DateTime)
					{
						return;
					}
				}
				else if (format != SerializedValue.ValueFormat.Guid && format != SerializedValue.ValueFormat.String && format != SerializedValue.ValueFormat.Binary)
				{
					return;
				}
			}
			else if (format <= SerializedValue.ValueFormat.MVInt64)
			{
				if (format <= (SerializedValue.ValueFormat)124)
				{
					if (format != SerializedValue.ValueFormat.Reference)
					{
						if (format != (SerializedValue.ValueFormat)124)
						{
							return;
						}
						return;
					}
				}
				else if (format != SerializedValue.ValueFormat.MVInt16 && format != SerializedValue.ValueFormat.MVInt32 && format != SerializedValue.ValueFormat.MVInt64)
				{
					return;
				}
			}
			else if (format <= SerializedValue.ValueFormat.MVDateTime)
			{
				if (format != SerializedValue.ValueFormat.MVSingle && format != SerializedValue.ValueFormat.MVDouble && format != SerializedValue.ValueFormat.MVDateTime)
				{
					return;
				}
			}
			else if (format != SerializedValue.ValueFormat.MVGuid && format != SerializedValue.ValueFormat.MVString && format != SerializedValue.ValueFormat.MVBinary)
			{
				return;
			}
			SerializedValue.Serialize(dictionaryEntry.Format, value, blob, ref currentValueOffset);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		private static PropertyBlob.DictionaryEntry BuildEntry(uint tag, object value, int currentValueOffset)
		{
			PropertyBlob.CompressedPropertyType compressedType = PropertyBlob.GetCompressedType(PropertyBlob.PropertyTypeFromTag(tag));
			SerializedValue.ValueFormat valueFormat;
			int offsetOrValue;
			if (value == null)
			{
				valueFormat = SerializedValue.ValueFormat.FormatModifierShift;
				offsetOrValue = 0;
			}
			else if (value is ValueReference)
			{
				if (((ValueReference)value).IsZero)
				{
					valueFormat = (SerializedValue.ValueFormat)124;
					offsetOrValue = 0;
				}
				else
				{
					valueFormat = SerializedValue.ValueFormat.Reference;
					offsetOrValue = currentValueOffset;
				}
			}
			else
			{
				valueFormat = PropertyBlob.GetValueFormat(compressedType);
				SerializedValue.ValueFormat valueFormat2 = valueFormat;
				if (valueFormat2 <= SerializedValue.ValueFormat.Int16)
				{
					if (valueFormat2 == SerializedValue.ValueFormat.Boolean)
					{
						offsetOrValue = (((bool)value) ? 1 : 0);
						goto IL_88;
					}
					if (valueFormat2 == SerializedValue.ValueFormat.Int16)
					{
						offsetOrValue = (int)((short)value);
						goto IL_88;
					}
				}
				else
				{
					if (valueFormat2 == SerializedValue.ValueFormat.Int32)
					{
						offsetOrValue = (int)value;
						goto IL_88;
					}
					if (valueFormat2 == SerializedValue.ValueFormat.Reserved2 || valueFormat2 == SerializedValue.ValueFormat.Reserved1)
					{
						valueFormat = SerializedValue.ValueFormat.Binary;
					}
				}
				offsetOrValue = currentValueOffset;
			}
			IL_88:
			return new PropertyBlob.DictionaryEntry(PropertyBlob.IdFromTag(tag), compressedType, valueFormat, offsetOrValue);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000D590 File Offset: 0x0000B790
		private static int GetSerializedSize(uint tag, object value)
		{
			if (value == null)
			{
				return 0;
			}
			SerializedValue.ValueFormat valueFormat;
			if (value is ValueReference)
			{
				if (((ValueReference)value).IsZero)
				{
					return 0;
				}
				valueFormat = SerializedValue.ValueFormat.Reference;
			}
			else
			{
				PropertyType propertyType = PropertyBlob.PropertyTypeFromTag(tag);
				PropertyBlob.CompressedPropertyType compressedType = PropertyBlob.GetCompressedType(propertyType);
				valueFormat = PropertyBlob.GetValueFormat(compressedType);
			}
			SerializedValue.ValueFormat valueFormat2 = valueFormat;
			if (valueFormat2 <= SerializedValue.ValueFormat.Binary)
			{
				if (valueFormat2 <= SerializedValue.ValueFormat.Single)
				{
					if (valueFormat2 <= SerializedValue.ValueFormat.Int16)
					{
						if (valueFormat2 == SerializedValue.ValueFormat.Boolean)
						{
							return 0;
						}
						if (valueFormat2 != SerializedValue.ValueFormat.Int16)
						{
							goto IL_12C;
						}
						return 0;
					}
					else
					{
						if (valueFormat2 == SerializedValue.ValueFormat.Int32)
						{
							return 0;
						}
						if (valueFormat2 != SerializedValue.ValueFormat.Int64 && valueFormat2 != SerializedValue.ValueFormat.Single)
						{
							goto IL_12C;
						}
					}
				}
				else if (valueFormat2 <= SerializedValue.ValueFormat.DateTime)
				{
					if (valueFormat2 != SerializedValue.ValueFormat.Double && valueFormat2 != SerializedValue.ValueFormat.DateTime)
					{
						goto IL_12C;
					}
				}
				else if (valueFormat2 != SerializedValue.ValueFormat.Guid && valueFormat2 != SerializedValue.ValueFormat.String && valueFormat2 != SerializedValue.ValueFormat.Binary)
				{
					goto IL_12C;
				}
			}
			else if (valueFormat2 <= SerializedValue.ValueFormat.MVInt64)
			{
				if (valueFormat2 <= SerializedValue.ValueFormat.Reserved1)
				{
					if (valueFormat2 != SerializedValue.ValueFormat.Reserved2 && valueFormat2 != SerializedValue.ValueFormat.Reserved1)
					{
						goto IL_12C;
					}
					valueFormat = SerializedValue.ValueFormat.Binary;
				}
				else if (valueFormat2 != SerializedValue.ValueFormat.MVInt16 && valueFormat2 != SerializedValue.ValueFormat.MVInt32 && valueFormat2 != SerializedValue.ValueFormat.MVInt64)
				{
					goto IL_12C;
				}
			}
			else if (valueFormat2 <= SerializedValue.ValueFormat.MVDateTime)
			{
				if (valueFormat2 != SerializedValue.ValueFormat.MVSingle && valueFormat2 != SerializedValue.ValueFormat.MVDouble && valueFormat2 != SerializedValue.ValueFormat.MVDateTime)
				{
					goto IL_12C;
				}
			}
			else if (valueFormat2 != SerializedValue.ValueFormat.MVGuid && valueFormat2 != SerializedValue.ValueFormat.MVString && valueFormat2 != SerializedValue.ValueFormat.MVBinary)
			{
				goto IL_12C;
			}
			return SerializedValue.ComputeSize(valueFormat, value);
			IL_12C:
			throw new ArgumentException("invalid or unexpected property type");
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		private static bool IsPromotionNecessary(PropertyBlob.BlobReader onPageBlobReader, HashSet<ushort> additionalPromotedProperties)
		{
			foreach (ushort id in additionalPromotedProperties)
			{
				if (!onPageBlobReader.TestIfPropertyPresent(id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000D72C File Offset: 0x0000B92C
		internal static void ThrowIfBlobOverflow(int blobSize, int propertySize)
		{
			if (propertySize < 0 || blobSize + propertySize < blobSize)
			{
				throw new InvalidOperationException("Invalid size");
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000D744 File Offset: 0x0000B944
		public static void BuildTwoBlobs(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties, HashSet<ushort> alwaysPromotedProperties, HashSet<ushort> defaultPromotedProperties, HashSet<ushort> additionalPromotedProperties, out byte[] onPageBlob, out object offPageBlob)
		{
			onPageBlob = null;
			offPageBlob = null;
			int val = (properties == null) ? 0 : properties.Count;
			int num = (defaultPromotedProperties == null) ? 0 : defaultPromotedProperties.Count;
			int num2 = (additionalPromotedProperties == null) ? 0 : additionalPromotedProperties.Count;
			List<ushort> list = new List<ushort>(Math.Max(val, num + num2));
			HashSet<ushort> hashSet = null;
			int num3;
			for (;;)
			{
				num3 = 8;
				list.Clear();
				int num4 = 0;
				ushort item = ushort.MaxValue;
				if (properties != null && defaultPromotedProperties != null)
				{
					foreach (ushort num5 in defaultPromotedProperties)
					{
						KeyValuePair<StorePropTag, object> keyValuePair;
						if (properties.TryGetValue(num5, out keyValuePair))
						{
							object value = keyValuePair.Value;
							bool flag = true;
							if (hashSet != null && hashSet.Contains(num5))
							{
								value = ValueReference.Zero;
								flag = false;
							}
							else if (alwaysPromotedProperties != null && alwaysPromotedProperties.Contains(keyValuePair.Key.PropId))
							{
								flag = false;
							}
							else if (ValueTypeHelper.ValueSize(PropertyTypeHelper.GetExtendedTypeCode(keyValuePair.Key.PropType), value) >= 512)
							{
								if (hashSet == null)
								{
									hashSet = new HashSet<ushort>();
								}
								hashSet.Add(num5);
								value = ValueReference.Zero;
								flag = false;
							}
							int serializedSize = PropertyBlob.GetSerializedSize(keyValuePair.Key.PropTag, value);
							if (serializedSize > num4 && flag)
							{
								num4 = serializedSize;
								item = num5;
							}
							PropertyBlob.ThrowIfBlobOverflow(num3, serializedSize + 8);
							num3 += 8 + serializedSize;
							if (num3 > 3110 && num4 != 0)
							{
								break;
							}
							list.Add(num5);
						}
					}
				}
				if ((num3 <= 3110 || num4 == 0) && additionalPromotedProperties != null)
				{
					foreach (ushort num6 in additionalPromotedProperties)
					{
						if (hashSet == null || !hashSet.Contains(num6))
						{
							KeyValuePair<StorePropTag, object> keyValuePair2;
							StorePropTag storePropTag;
							object value2;
							if (properties == null || !properties.TryGetValue(num6, out keyValuePair2))
							{
								storePropTag = StorePropTag.CreateWithoutInfo(num6, PropertyType.Null);
								value2 = null;
							}
							else
							{
								if (ValueTypeHelper.ValueSize(PropertyTypeHelper.GetExtendedTypeCode(keyValuePair2.Key.PropType), keyValuePair2.Value) >= 512)
								{
									if (hashSet == null)
									{
										hashSet = new HashSet<ushort>();
									}
									hashSet.Add(num6);
									continue;
								}
								storePropTag = keyValuePair2.Key;
								value2 = keyValuePair2.Value;
							}
							int serializedSize2 = PropertyBlob.GetSerializedSize(storePropTag.PropTag, value2);
							if (serializedSize2 + 8 > num4)
							{
								num4 = serializedSize2 + 8;
								item = num6;
							}
							PropertyBlob.ThrowIfBlobOverflow(num3, serializedSize2 + 8);
							num3 += 8 + serializedSize2;
							if (num3 > 3110 && num4 != 0)
							{
								break;
							}
							list.Add(num6);
						}
					}
				}
				if (num3 <= 3110 || num4 == 0)
				{
					break;
				}
				if (hashSet == null)
				{
					hashSet = new HashSet<ushort>();
				}
				hashSet.Add(item);
			}
			if (list.Count != 0)
			{
				onPageBlob = new byte[num3];
				int num7 = 0;
				int num8 = 8 + list.Count * 8;
				ParseSerialize.SerializeInt32(1349481040, onPageBlob, 0);
				ParseSerialize.SerializeInt16(768, onPageBlob, 4);
				ParseSerialize.SerializeInt16((short)list.Count, onPageBlob, 6);
				list.Sort();
				foreach (ushort num9 in list)
				{
					KeyValuePair<StorePropTag, object> keyValuePair3;
					if (properties != null && properties.TryGetValue(num9, out keyValuePair3))
					{
						if (hashSet != null && hashSet.Contains(num9))
						{
							PropertyBlob.AddProperty(onPageBlob, ref num7, ref num8, keyValuePair3.Key.PropTag, ValueReference.Zero);
						}
						else
						{
							PropertyBlob.AddProperty(onPageBlob, ref num7, ref num8, keyValuePair3.Key.PropTag, keyValuePair3.Value);
						}
					}
					else
					{
						StorePropTag storePropTag2 = StorePropTag.CreateWithoutInfo(num9, PropertyType.Null);
						PropertyBlob.AddProperty(onPageBlob, ref num7, ref num8, storePropTag2.PropTag, null);
					}
				}
			}
			if (properties != null)
			{
				num3 = 8;
				list.Clear();
				foreach (KeyValuePair<ushort, KeyValuePair<StorePropTag, object>> keyValuePair4 in properties)
				{
					ushort key = keyValuePair4.Key;
					if (((defaultPromotedProperties == null || !defaultPromotedProperties.Contains(key)) && (additionalPromotedProperties == null || !additionalPromotedProperties.Contains(key))) || (hashSet != null && hashSet.Contains(key)))
					{
						StorePropTag key2 = keyValuePair4.Value.Key;
						object value3 = keyValuePair4.Value.Value;
						int serializedSize3 = PropertyBlob.GetSerializedSize(key2.PropTag, value3);
						PropertyBlob.ThrowIfBlobOverflow(num3, serializedSize3 + 8);
						num3 += 8 + serializedSize3;
						list.Add(key);
					}
				}
				if (list.Count != 0)
				{
					if (num3 < 65536)
					{
						byte[] array = new byte[num3];
						int num10 = 0;
						int num11 = 8 + list.Count * 8;
						ParseSerialize.SerializeInt32(1349481040, array, 0);
						ParseSerialize.SerializeInt16(768, array, 4);
						ParseSerialize.SerializeInt16((short)list.Count, array, 6);
						list.Sort();
						foreach (ushort key3 in list)
						{
							KeyValuePair<StorePropTag, object> keyValuePair5 = properties[key3];
							PropertyBlob.AddProperty(array, ref num10, ref num11, keyValuePair5.Key.PropTag, keyValuePair5.Value);
						}
						offPageBlob = array;
						return;
					}
					Stream stream = null;
					try
					{
						stream = TempStream.CreateInstance();
						PropertyBlob.WriteBlobToStream(list, properties, stream);
						offPageBlob = stream;
						stream = null;
					}
					finally
					{
						if (stream != null)
						{
							stream.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000DD58 File Offset: 0x0000BF58
		internal static void WriteBlobToStream(List<ushort> propertyIds, Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties, Stream blobStream)
		{
			propertyIds.Sort();
			PropertyBlob.DictionaryEntry[] array = new PropertyBlob.DictionaryEntry[propertyIds.Count];
			int num = 8 + propertyIds.Count * 8;
			for (int i = 0; i < propertyIds.Count; i++)
			{
				KeyValuePair<StorePropTag, object> keyValuePair = properties[propertyIds[i]];
				array[i] = PropertyBlob.BuildEntry(keyValuePair.Key.PropTag, keyValuePair.Value, num);
				num += PropertyBlob.GetSerializedSize(keyValuePair.Key.PropTag, keyValuePair.Value);
			}
			byte[] array2 = null;
			BufferPool bufferPool = DataRow.GetBufferPool(Factory.GetOptimalStreamChunkSize());
			try
			{
				array2 = bufferPool.Acquire();
				ParseSerialize.SerializeInt32(1349481040, array2, 0);
				ParseSerialize.SerializeInt16(768, array2, 4);
				ParseSerialize.SerializeInt16((short)propertyIds.Count, array2, 6);
				blobStream.Write(array2, 0, 8);
				for (int j = 0; j < array.Length; j++)
				{
					ParseSerialize.SerializeInt16((short)array[j].Id, array2, 2);
					array2[0] = (byte)array[j].CompressedType;
					array2[1] = (byte)array[j].Format;
					ParseSerialize.SerializeInt32(array[j].Offset, array2, 4);
					blobStream.Write(array2, 0, 8);
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (!array[k].IsValueInline)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail((long)array[k].Offset == blobStream.Position, "Stream position and entry offset mismatch");
						KeyValuePair<StorePropTag, object> keyValuePair2 = properties[propertyIds[k]];
						int serializedSize = PropertyBlob.GetSerializedSize(keyValuePair2.Key.PropTag, keyValuePair2.Value);
						byte[] buffer = array2;
						if (array2.Length < serializedSize)
						{
							byte[] array3 = keyValuePair2.Value as byte[];
							if (array3 != null)
							{
								PropertyBlob.WriteLargeBinaryValueToStream(array3, array2, blobStream);
								goto IL_203;
							}
							buffer = new byte[serializedSize];
						}
						int count = 0;
						SerializedValue.Serialize(array[k].Format, keyValuePair2.Value, buffer, ref count);
						blobStream.Write(buffer, 0, count);
					}
					IL_203:;
				}
			}
			finally
			{
				if (array2 != null)
				{
					bufferPool.Release(array2);
				}
			}
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail((long)num == blobStream.Position, "size mismatch");
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		private static void WriteLargeBinaryValueToStream(byte[] value, byte[] tempBuffer, Stream blobStream)
		{
			if (value.Length <= 65535)
			{
				SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)82;
				tempBuffer[0] = (byte)valueFormat;
				ParseSerialize.SerializeInt16((short)value.Length, tempBuffer, 1);
				blobStream.Write(tempBuffer, 0, 3);
			}
			else
			{
				SerializedValue.ValueFormat valueFormat2 = (SerializedValue.ValueFormat)83;
				tempBuffer[0] = (byte)valueFormat2;
				ParseSerialize.SerializeInt32(value.Length, tempBuffer, 1);
				blobStream.Write(tempBuffer, 0, 5);
			}
			blobStream.Write(value, 0, value.Length);
		}

		// Token: 0x04000180 RID: 384
		private const short CurrentBlobFormatVersion = 768;

		// Token: 0x04000181 RID: 385
		private const int BlobMagicNumber = 1349481040;

		// Token: 0x04000182 RID: 386
		private const int BlobHeaderMagicOffset = 0;

		// Token: 0x04000183 RID: 387
		private const int BlobHeaderMagicLength = 4;

		// Token: 0x04000184 RID: 388
		private const int BlobHeaderVersionOffset = 4;

		// Token: 0x04000185 RID: 389
		private const int BlobHeaderVersionLength = 2;

		// Token: 0x04000186 RID: 390
		private const int BlobHeaderPropertyCountOffset = 6;

		// Token: 0x04000187 RID: 391
		private const int BlobHeaderPropertyCountLength = 2;

		// Token: 0x04000188 RID: 392
		private const int BlobHeaderLength = 8;

		// Token: 0x04000189 RID: 393
		private const int DictionaryEntryCompressedTypeOffset = 0;

		// Token: 0x0400018A RID: 394
		private const int DictionaryEntryCompressedTypeLength = 1;

		// Token: 0x0400018B RID: 395
		private const int DictionaryEntryFormatOffset = 1;

		// Token: 0x0400018C RID: 396
		private const int DictionaryEntryFormatLength = 1;

		// Token: 0x0400018D RID: 397
		private const int DictionaryEntryIdOffset = 2;

		// Token: 0x0400018E RID: 398
		private const int DictionaryEntryIdLength = 2;

		// Token: 0x0400018F RID: 399
		private const int DictionaryEntryOffsetOrValueOffset = 4;

		// Token: 0x04000190 RID: 400
		private const int DictionaryEntryOffsetOrValueLength = 4;

		// Token: 0x04000191 RID: 401
		private const int DictionaryEntryLength = 8;

		// Token: 0x04000192 RID: 402
		public const int MaxOnPageBlobSize = 3110;

		// Token: 0x04000193 RID: 403
		public const int MaxOnPageProperties = 388;

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct BlobHeader
		{
			// Token: 0x04000194 RID: 404
			public uint Magic;

			// Token: 0x04000195 RID: 405
			public ushort Version;

			// Token: 0x04000196 RID: 406
			public ushort PropertyCount;
		}

		// Token: 0x02000015 RID: 21
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DictionaryEntry
		{
			// Token: 0x060000DD RID: 221 RVA: 0x0000E013 File Offset: 0x0000C213
			public DictionaryEntry(byte[] blob, int dictionaryEntryOffset)
			{
				this.compressedType = blob[dictionaryEntryOffset];
				this.format = blob[dictionaryEntryOffset + 1];
				this.id = (ushort)ParseSerialize.ParseInt16(blob, dictionaryEntryOffset + 2);
				this.offsetOrValue = ParseSerialize.ParseInt32(blob, dictionaryEntryOffset + 4);
			}

			// Token: 0x060000DE RID: 222 RVA: 0x0000E048 File Offset: 0x0000C248
			public DictionaryEntry(ushort id, PropertyBlob.CompressedPropertyType compressedType, SerializedValue.ValueFormat format, int offsetOrValue)
			{
				this.id = id;
				this.compressedType = (byte)compressedType;
				this.format = (byte)format;
				this.offsetOrValue = offsetOrValue;
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060000DF RID: 223 RVA: 0x0000E067 File Offset: 0x0000C267
			public PropertyBlob.CompressedPropertyType CompressedType
			{
				get
				{
					return (PropertyBlob.CompressedPropertyType)(this.compressedType & 252);
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000E076 File Offset: 0x0000C276
			public AdditionalPropertyInfo AdditionalInfo
			{
				get
				{
					return (AdditionalPropertyInfo)(this.compressedType & 3);
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000E081 File Offset: 0x0000C281
			public SerializedValue.ValueFormat Format
			{
				get
				{
					return (SerializedValue.ValueFormat)this.format;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000E089 File Offset: 0x0000C289
			public int Offset
			{
				get
				{
					return this.offsetOrValue;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000E091 File Offset: 0x0000C291
			public int Value
			{
				get
				{
					return this.offsetOrValue;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000E099 File Offset: 0x0000C299
			public ushort Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
			public bool IsValueInline
			{
				get
				{
					SerializedValue.ValueFormat valueFormat = this.Format;
					if (valueFormat <= SerializedValue.ValueFormat.Boolean)
					{
						if (valueFormat != SerializedValue.ValueFormat.FormatModifierShift && valueFormat != SerializedValue.ValueFormat.Boolean)
						{
							return false;
						}
					}
					else if (valueFormat != SerializedValue.ValueFormat.Int16 && valueFormat != SerializedValue.ValueFormat.Int32 && valueFormat != (SerializedValue.ValueFormat)124)
					{
						return false;
					}
					return true;
				}
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
			public uint GetPropertyTag()
			{
				return (uint)((int)this.id << 16 | (int)PropertyBlob.GetPropertyType(this.CompressedType));
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000E0EF File Offset: 0x0000C2EF
			public PropertyType GetPropertyType()
			{
				return PropertyBlob.GetPropertyType(this.CompressedType);
			}

			// Token: 0x04000197 RID: 407
			private byte compressedType;

			// Token: 0x04000198 RID: 408
			private byte format;

			// Token: 0x04000199 RID: 409
			private ushort id;

			// Token: 0x0400019A RID: 410
			private int offsetOrValue;
		}

		// Token: 0x02000016 RID: 22
		public struct BlobReader
		{
			// Token: 0x060000E8 RID: 232 RVA: 0x0000E0FC File Offset: 0x0000C2FC
			public BlobReader(byte[] blob, int startOffset)
			{
				if (blob == null || blob.Length == 0)
				{
					this.blob = blob;
					this.startOffset = startOffset;
					this.version = 1;
					this.propertyCount = 0;
					return;
				}
				if (blob.Length - startOffset < 8 || ParseSerialize.ParseInt32(blob, startOffset) != 1349481040)
				{
					throw new InvalidBlobException("blob is too short or blob magic number is incorrect");
				}
				ushort num = (ushort)ParseSerialize.ParseInt16(blob, startOffset + 4);
				ushort num2 = (ushort)ParseSerialize.ParseInt16(blob, startOffset + 6);
				if ((num & 65280) != 768)
				{
					throw new InvalidBlobException("invalid blob format version");
				}
				if (blob.Length - startOffset < (int)(8 + num2 * 8))
				{
					throw new InvalidBlobException("invalid property count");
				}
				this.blob = blob;
				this.startOffset = startOffset;
				this.version = num;
				this.propertyCount = num2;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
			public int PropertyCount
			{
				get
				{
					return (int)this.propertyCount;
				}
			}

			// Token: 0x060000EA RID: 234 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
			public bool TryFindPropertyById(ushort id, out int index)
			{
				if (this.propertyCount == 0)
				{
					index = -1;
					return false;
				}
				return PropertyBlob.TryFindDictionaryEntry(this.blob, this.startOffset, (int)this.propertyCount, id, out index);
			}

			// Token: 0x060000EB RID: 235 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
			public uint GetPropertyTag(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				return this.GetDictionaryEntry(index).GetPropertyTag();
			}

			// Token: 0x060000EC RID: 236 RVA: 0x0000E214 File Offset: 0x0000C414
			public object GetPropertyValue(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				PropertyBlob.DictionaryEntry dictionaryEntry = this.GetDictionaryEntry(index);
				return this.GetPropertyValue(dictionaryEntry);
			}

			// Token: 0x060000ED RID: 237 RVA: 0x0000E244 File Offset: 0x0000C444
			public int GetPropertyValueSize(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				PropertyBlob.DictionaryEntry dictionaryEntry = this.GetDictionaryEntry(index);
				return this.GetPropertyValueSize(dictionaryEntry);
			}

			// Token: 0x060000EE RID: 238 RVA: 0x0000E274 File Offset: 0x0000C474
			public AdditionalPropertyInfo GetPropertyAdditionalInfo(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				return this.GetDictionaryEntry(index).AdditionalInfo;
			}

			// Token: 0x060000EF RID: 239 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
			public bool IsPropertyValueNull(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				return this.GetDictionaryEntry(index).Format == SerializedValue.ValueFormat.FormatModifierShift;
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
			public bool IsPropertyValueReference(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				PropertyBlob.DictionaryEntry dictionaryEntry = this.GetDictionaryEntry(index);
				return dictionaryEntry.Format == SerializedValue.ValueFormat.Reference || dictionaryEntry.Format == (SerializedValue.ValueFormat)124;
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x0000E318 File Offset: 0x0000C518
			public KeyValuePair<uint, object> GetProperty(int index)
			{
				if (index < 0 || index >= (int)this.propertyCount)
				{
					throw new IndexOutOfRangeException();
				}
				PropertyBlob.DictionaryEntry dictionaryEntry = this.GetDictionaryEntry(index);
				return new KeyValuePair<uint, object>(dictionaryEntry.GetPropertyTag(), this.GetPropertyValue(dictionaryEntry));
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000E354 File Offset: 0x0000C554
			public object GetPropertyValueByTag(uint tag)
			{
				int num;
				object result;
				if (this.TryGetPropertyValueByTag(tag, out num, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x0000E374 File Offset: 0x0000C574
			public bool TryGetPropertyValueByTag(uint tag, out int index, out object value)
			{
				if (!this.TryFindPropertyById(PropertyBlob.IdFromTag(tag), out index))
				{
					value = null;
					return false;
				}
				PropertyBlob.DictionaryEntry dictionaryEntry = this.GetDictionaryEntry(index);
				PropertyType propertyType = PropertyBlob.PropertyTypeFromTag(tag);
				if (dictionaryEntry.GetPropertyType() == propertyType)
				{
					value = this.GetPropertyValue(dictionaryEntry);
					return true;
				}
				PropertyBlob.CompressedPropertyType compressedType = PropertyBlob.GetCompressedType(propertyType);
				if (PropertyBlob.CompatibleTypes(dictionaryEntry.CompressedType, compressedType))
				{
					value = this.GetPropertyValue(dictionaryEntry, compressedType);
					return true;
				}
				value = null;
				return true;
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
			public bool TestIfPropertyPresent(ushort id)
			{
				int num;
				return this.TryFindPropertyById(id, out num);
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
			public bool TryGetPropertyById(ushort id, out KeyValuePair<uint, object> property)
			{
				int index;
				if (this.TryFindPropertyById(id, out index))
				{
					PropertyBlob.DictionaryEntry dictionaryEntry = this.GetDictionaryEntry(index);
					property = new KeyValuePair<uint, object>(dictionaryEntry.GetPropertyTag(), this.GetPropertyValue(dictionaryEntry));
					return true;
				}
				property = default(KeyValuePair<uint, object>);
				return false;
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x0000E43B File Offset: 0x0000C63B
			private PropertyBlob.DictionaryEntry GetDictionaryEntry(int index)
			{
				return new PropertyBlob.DictionaryEntry(this.blob, this.startOffset + 8 + index * 8);
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x0000E454 File Offset: 0x0000C654
			private object GetPropertyValue(PropertyBlob.DictionaryEntry dictionaryEntry)
			{
				if (dictionaryEntry.Format != SerializedValue.ValueFormat.FormatModifierShift && dictionaryEntry.Format != SerializedValue.ValueFormat.Reference && dictionaryEntry.Format != (SerializedValue.ValueFormat)124)
				{
					SerializedValue.ValueFormat valueFormat = PropertyBlob.GetValueFormat(dictionaryEntry.CompressedType);
					if (valueFormat != dictionaryEntry.Format && (dictionaryEntry.Format != SerializedValue.ValueFormat.Binary || (valueFormat != SerializedValue.ValueFormat.Reserved1 && valueFormat != SerializedValue.ValueFormat.Reserved2)))
					{
						throw new InvalidBlobException("invalid dictionary entry - format should match type");
					}
				}
				SerializedValue.ValueFormat format = dictionaryEntry.Format;
				if (format <= SerializedValue.ValueFormat.Binary)
				{
					if (format <= SerializedValue.ValueFormat.Int64)
					{
						if (format <= SerializedValue.ValueFormat.Boolean)
						{
							if (format == SerializedValue.ValueFormat.FormatModifierShift)
							{
								return null;
							}
							if (format != SerializedValue.ValueFormat.Boolean)
							{
								goto IL_1D5;
							}
							if (dictionaryEntry.Value == 0)
							{
								return SerializedValue.BoxedFalse;
							}
							return SerializedValue.BoxedTrue;
						}
						else
						{
							if (format == SerializedValue.ValueFormat.Int16)
							{
								return (short)dictionaryEntry.Value;
							}
							if (format == SerializedValue.ValueFormat.Int32)
							{
								return SerializedValue.GetBoxedInt32(dictionaryEntry.Value);
							}
							if (format != SerializedValue.ValueFormat.Int64)
							{
								goto IL_1D5;
							}
						}
					}
					else if (format <= SerializedValue.ValueFormat.DateTime)
					{
						if (format != SerializedValue.ValueFormat.Single && format != SerializedValue.ValueFormat.Double && format != SerializedValue.ValueFormat.DateTime)
						{
							goto IL_1D5;
						}
					}
					else if (format != SerializedValue.ValueFormat.Guid && format != SerializedValue.ValueFormat.String && format != SerializedValue.ValueFormat.Binary)
					{
						goto IL_1D5;
					}
				}
				else if (format <= SerializedValue.ValueFormat.MVInt64)
				{
					if (format <= (SerializedValue.ValueFormat)124)
					{
						if (format != SerializedValue.ValueFormat.Reference)
						{
							if (format != (SerializedValue.ValueFormat)124)
							{
								goto IL_1D5;
							}
							return ValueReference.Zero;
						}
					}
					else if (format != SerializedValue.ValueFormat.MVInt16 && format != SerializedValue.ValueFormat.MVInt32 && format != SerializedValue.ValueFormat.MVInt64)
					{
						goto IL_1D5;
					}
				}
				else if (format <= SerializedValue.ValueFormat.MVDateTime)
				{
					if (format != SerializedValue.ValueFormat.MVSingle && format != SerializedValue.ValueFormat.MVDouble && format != SerializedValue.ValueFormat.MVDateTime)
					{
						goto IL_1D5;
					}
				}
				else if (format != SerializedValue.ValueFormat.MVGuid && format != SerializedValue.ValueFormat.MVString && format != SerializedValue.ValueFormat.MVBinary)
				{
					goto IL_1D5;
				}
				object zero;
				if (!SerializedValue.TryParse(dictionaryEntry.Format, this.blob, this.startOffset + dictionaryEntry.Offset, out zero))
				{
					throw new InvalidBlobException("value parsing error");
				}
				if (dictionaryEntry.AdditionalInfo == AdditionalPropertyInfo.Truncated)
				{
					zero = ValueReference.Zero;
				}
				return zero;
				IL_1D5:
				throw new InvalidBlobException("invalid dictionary entry - format");
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x0000E640 File Offset: 0x0000C840
			private int GetPropertyValueSize(PropertyBlob.DictionaryEntry dictionaryEntry)
			{
				if (dictionaryEntry.Format != SerializedValue.ValueFormat.FormatModifierShift && dictionaryEntry.Format != SerializedValue.ValueFormat.Reference && dictionaryEntry.Format != (SerializedValue.ValueFormat)124)
				{
					SerializedValue.ValueFormat valueFormat = PropertyBlob.GetValueFormat(dictionaryEntry.CompressedType);
					if (valueFormat != dictionaryEntry.Format && (dictionaryEntry.Format != SerializedValue.ValueFormat.Binary || (valueFormat != SerializedValue.ValueFormat.Reserved1 && valueFormat != SerializedValue.ValueFormat.Reserved2)))
					{
						throw new InvalidBlobException("invalid dictionary entry - format should match type");
					}
				}
				SerializedValue.ValueFormat format = dictionaryEntry.Format;
				if (format <= SerializedValue.ValueFormat.Binary)
				{
					if (format <= SerializedValue.ValueFormat.Int64)
					{
						if (format <= SerializedValue.ValueFormat.Boolean)
						{
							if (format == SerializedValue.ValueFormat.FormatModifierShift)
							{
								return 0;
							}
							if (format != SerializedValue.ValueFormat.Boolean)
							{
								goto IL_188;
							}
							return 1;
						}
						else
						{
							if (format == SerializedValue.ValueFormat.Int16)
							{
								return 2;
							}
							if (format == SerializedValue.ValueFormat.Int32)
							{
								return 4;
							}
							if (format != SerializedValue.ValueFormat.Int64)
							{
								goto IL_188;
							}
						}
					}
					else if (format <= SerializedValue.ValueFormat.DateTime)
					{
						if (format != SerializedValue.ValueFormat.Single && format != SerializedValue.ValueFormat.Double && format != SerializedValue.ValueFormat.DateTime)
						{
							goto IL_188;
						}
					}
					else if (format != SerializedValue.ValueFormat.Guid && format != SerializedValue.ValueFormat.String && format != SerializedValue.ValueFormat.Binary)
					{
						goto IL_188;
					}
				}
				else if (format <= SerializedValue.ValueFormat.MVInt64)
				{
					if (format <= (SerializedValue.ValueFormat)124)
					{
						if (format != SerializedValue.ValueFormat.Reference)
						{
							if (format != (SerializedValue.ValueFormat)124)
							{
								goto IL_188;
							}
							return 0;
						}
					}
					else if (format != SerializedValue.ValueFormat.MVInt16 && format != SerializedValue.ValueFormat.MVInt32 && format != SerializedValue.ValueFormat.MVInt64)
					{
						goto IL_188;
					}
				}
				else if (format <= SerializedValue.ValueFormat.MVDateTime)
				{
					if (format != SerializedValue.ValueFormat.MVSingle && format != SerializedValue.ValueFormat.MVDouble && format != SerializedValue.ValueFormat.MVDateTime)
					{
						goto IL_188;
					}
				}
				else if (format != SerializedValue.ValueFormat.MVGuid && format != SerializedValue.ValueFormat.MVString && format != SerializedValue.ValueFormat.MVBinary)
				{
					goto IL_188;
				}
				int result;
				if (!SerializedValue.TryGetSize(dictionaryEntry.Format, this.blob, this.startOffset + dictionaryEntry.Offset, out result))
				{
					throw new InvalidBlobException("value parsing error");
				}
				return result;
				IL_188:
				throw new InvalidBlobException("invalid dictionary entry - format");
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
			private object GetPropertyValue(PropertyBlob.DictionaryEntry dictionaryEntry, PropertyBlob.CompressedPropertyType desiredCompressedType)
			{
				SerializedValue.ValueFormat format = dictionaryEntry.Format;
				if (format <= SerializedValue.ValueFormat.Int32)
				{
					if (format == SerializedValue.ValueFormat.FormatModifierShift)
					{
						return null;
					}
					if (format != SerializedValue.ValueFormat.Int16)
					{
						if (format == SerializedValue.ValueFormat.Int32)
						{
							int value = dictionaryEntry.Value;
							if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int32)
							{
								return value;
							}
							if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64)
							{
								return (long)value;
							}
						}
					}
					else
					{
						short num = (short)dictionaryEntry.Value;
						if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int32)
						{
							return (int)num;
						}
						if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64)
						{
							return (long)num;
						}
					}
				}
				else if (format <= SerializedValue.ValueFormat.Single)
				{
					if (format != SerializedValue.ValueFormat.Int64)
					{
						if (format == SerializedValue.ValueFormat.Single)
						{
							if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Real64)
							{
								object obj;
								if (!SerializedValue.TryParse(dictionaryEntry.Format, this.blob, this.startOffset + dictionaryEntry.Offset, out obj))
								{
									throw new InvalidBlobException("value parsing error");
								}
								return (double)((float)obj);
							}
						}
					}
					else
					{
						long num2 = (long)dictionaryEntry.Value;
						if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64)
						{
							return num2;
						}
					}
				}
				else if (format != SerializedValue.ValueFormat.Binary)
				{
					if (format == (SerializedValue.ValueFormat)124)
					{
						return ValueReference.Zero;
					}
				}
				else if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Binary)
				{
					object result;
					if (!SerializedValue.TryParse(dictionaryEntry.Format, this.blob, this.startOffset + dictionaryEntry.Offset, out result))
					{
						throw new InvalidBlobException("value parsing error");
					}
					return result;
				}
				throw new InvalidBlobException("invalid property value format");
			}

			// Token: 0x0400019B RID: 411
			private ushort version;

			// Token: 0x0400019C RID: 412
			private ushort propertyCount;

			// Token: 0x0400019D RID: 413
			private int startOffset;

			// Token: 0x0400019E RID: 414
			private byte[] blob;
		}

		// Token: 0x02000017 RID: 23
		internal enum CompressedPropertyType : byte
		{
			// Token: 0x040001A0 RID: 416
			CompressedPropertyTypeMask = 252,
			// Token: 0x040001A1 RID: 417
			AdditionalInfoMask = 3,
			// Token: 0x040001A2 RID: 418
			Null = 0,
			// Token: 0x040001A3 RID: 419
			Boolean = 8,
			// Token: 0x040001A4 RID: 420
			Int16 = 16,
			// Token: 0x040001A5 RID: 421
			Int32 = 24,
			// Token: 0x040001A6 RID: 422
			Int64 = 32,
			// Token: 0x040001A7 RID: 423
			Currency = 36,
			// Token: 0x040001A8 RID: 424
			Real32 = 40,
			// Token: 0x040001A9 RID: 425
			Real64 = 48,
			// Token: 0x040001AA RID: 426
			AppTime = 52,
			// Token: 0x040001AB RID: 427
			SysTime = 56,
			// Token: 0x040001AC RID: 428
			Guid = 64,
			// Token: 0x040001AD RID: 429
			Unicode = 72,
			// Token: 0x040001AE RID: 430
			Binary = 80,
			// Token: 0x040001AF RID: 431
			Object = 84,
			// Token: 0x040001B0 RID: 432
			SvrEid = 112,
			// Token: 0x040001B1 RID: 433
			SRestriction = 104,
			// Token: 0x040001B2 RID: 434
			Actions = 108,
			// Token: 0x040001B3 RID: 435
			MVInt16 = 144,
			// Token: 0x040001B4 RID: 436
			MVInt32 = 152,
			// Token: 0x040001B5 RID: 437
			MVInt64 = 160,
			// Token: 0x040001B6 RID: 438
			MVCurrency = 164,
			// Token: 0x040001B7 RID: 439
			MVReal32 = 168,
			// Token: 0x040001B8 RID: 440
			MVReal64 = 176,
			// Token: 0x040001B9 RID: 441
			MVAppTime = 180,
			// Token: 0x040001BA RID: 442
			MVSysTime = 184,
			// Token: 0x040001BB RID: 443
			MVGuid = 192,
			// Token: 0x040001BC RID: 444
			MVUnicode = 200,
			// Token: 0x040001BD RID: 445
			MVBinary = 208
		}

		// Token: 0x02000018 RID: 24
		public struct BlobStreamReader
		{
			// Token: 0x060000FA RID: 250 RVA: 0x0000E925 File Offset: 0x0000CB25
			public BlobStreamReader(Stream blobStream)
			{
				this.blobStream = blobStream;
			}

			// Token: 0x060000FB RID: 251 RVA: 0x0000ED00 File Offset: 0x0000CF00
			public IEnumerable<KeyValuePair<uint, object>> LoadProperties(bool loadValues, bool includeNullValues)
			{
				byte[] tempBuffer = null;
				BufferPool pool = DataRow.GetBufferPool(Factory.GetOptimalStreamChunkSize());
				try
				{
					tempBuffer = pool.Acquire();
					PropertyBlob.DictionaryEntry[] entries = this.LoadBlobDictionary(tempBuffer, loadValues);
					for (int i = 0; i < entries.Length; i++)
					{
						PropertyBlob.DictionaryEntry dictionaryEntry = entries[i];
						if (dictionaryEntry.Format != SerializedValue.ValueFormat.FormatModifierShift && dictionaryEntry.Format != SerializedValue.ValueFormat.Reference && dictionaryEntry.Format != (SerializedValue.ValueFormat)124)
						{
							SerializedValue.ValueFormat valueFormat = PropertyBlob.GetValueFormat(dictionaryEntry.CompressedType);
							if (valueFormat != dictionaryEntry.Format && (dictionaryEntry.Format != SerializedValue.ValueFormat.Binary || (valueFormat != SerializedValue.ValueFormat.Reserved1 && valueFormat != SerializedValue.ValueFormat.Reserved2)))
							{
								throw new StoreException((LID)36112U, ErrorCodeValue.CorruptData, "Invalid dictionary entry - format should match type.");
							}
						}
						if (loadValues)
						{
							if (dictionaryEntry.IsValueInline)
							{
								yield return new KeyValuePair<uint, object>(dictionaryEntry.GetPropertyTag(), this.GetInlinePropertyValue(dictionaryEntry));
							}
							else
							{
								int maxValueLength = checked((int)(this.blobStream.Length - this.blobStream.Position));
								if (i + 1 < entries.Length)
								{
									maxValueLength = entries[i + 1].Offset - dictionaryEntry.Offset;
								}
								yield return new KeyValuePair<uint, object>(dictionaryEntry.GetPropertyTag(), this.GetPropertyValueFromStream(dictionaryEntry, maxValueLength, tempBuffer));
							}
						}
						else if (includeNullValues || dictionaryEntry.Format != SerializedValue.ValueFormat.FormatModifierShift)
						{
							yield return new KeyValuePair<uint, object>(dictionaryEntry.GetPropertyTag(), null);
						}
					}
				}
				finally
				{
					if (tempBuffer != null)
					{
						pool.Release(tempBuffer);
					}
				}
				yield break;
			}

			// Token: 0x060000FC RID: 252 RVA: 0x0000ED30 File Offset: 0x0000CF30
			public object GetPropertyValueByTag(uint tag)
			{
				byte[] array = null;
				BufferPool bufferPool = DataRow.GetBufferPool(Factory.GetOptimalStreamChunkSize());
				object result;
				try
				{
					array = bufferPool.Acquire();
					PropertyBlob.DictionaryEntry[] array2 = this.LoadBlobDictionary(array, true);
					int num = 0;
					while (num < array2.Length && array2[num].Id != PropertyBlob.IdFromTag(tag))
					{
						num++;
					}
					if (num == array2.Length)
					{
						result = null;
					}
					else
					{
						PropertyType propertyType = PropertyBlob.PropertyTypeFromTag(tag);
						PropertyBlob.CompressedPropertyType compressedType = PropertyBlob.GetCompressedType(propertyType);
						if (array2[num].GetPropertyType() == propertyType || PropertyBlob.CompatibleTypes(array2[num].CompressedType, compressedType))
						{
							object obj;
							if (array2[num].IsValueInline)
							{
								obj = this.GetInlinePropertyValue(array2[num]);
							}
							else
							{
								this.blobStream.Position = (long)array2[num].Offset;
								int maxValueLength = checked((int)(this.blobStream.Length - this.blobStream.Position));
								if (num + 1 < array2.Length)
								{
									maxValueLength = array2[num + 1].Offset - array2[num].Offset;
								}
								obj = this.GetPropertyValueFromStream(array2[num], maxValueLength, array);
							}
							if (array2[num].CompressedType != compressedType)
							{
								obj = this.ConvertPropertyValue(array2[num], compressedType, obj);
							}
							result = obj;
						}
						else
						{
							result = null;
						}
					}
				}
				finally
				{
					if (array != null)
					{
						bufferPool.Release(array);
					}
				}
				return result;
			}

			// Token: 0x060000FD RID: 253 RVA: 0x0000EF00 File Offset: 0x0000D100
			private PropertyBlob.DictionaryEntry[] LoadBlobDictionary(byte[] tempBuffer, bool sortEntries)
			{
				this.Read(tempBuffer, 8);
				if (ParseSerialize.ParseInt32(tempBuffer, 0) != 1349481040)
				{
					throw new StoreException((LID)52496U, ErrorCodeValue.CorruptData, "Blob magic number is invalid.");
				}
				ushort num = (ushort)ParseSerialize.ParseInt16(tempBuffer, 4);
				ushort num2 = (ushort)ParseSerialize.ParseInt16(tempBuffer, 6);
				if ((num & 65280) != 768)
				{
					throw new StoreException((LID)46352U, ErrorCodeValue.CorruptData, "Invalid blob format version.");
				}
				PropertyBlob.DictionaryEntry[] array = new PropertyBlob.DictionaryEntry[(int)num2];
				int num3 = (int)(8 + num2 * 8);
				for (int i = 0; i < (int)num2; i++)
				{
					this.Read(tempBuffer, 8);
					array[i] = new PropertyBlob.DictionaryEntry(tempBuffer, 0);
					if (!array[i].IsValueInline && (array[i].Offset < num3 || this.blobStream.Length < (long)array[i].Offset))
					{
						throw new StoreException((LID)62736U, ErrorCodeValue.CorruptData, "Invalid entry offset.");
					}
				}
				if (sortEntries)
				{
					Array.Sort<PropertyBlob.DictionaryEntry>(array, delegate(PropertyBlob.DictionaryEntry x, PropertyBlob.DictionaryEntry y)
					{
						if (x.IsValueInline && !y.IsValueInline)
						{
							return -1;
						}
						if (!x.IsValueInline && y.IsValueInline)
						{
							return 1;
						}
						return x.Offset.CompareTo(y.Offset);
					});
				}
				return array;
			}

			// Token: 0x060000FE RID: 254 RVA: 0x0000F02C File Offset: 0x0000D22C
			private void Read(byte[] buffer, int size)
			{
				if (this.blobStream.Length - this.blobStream.Position < (long)size)
				{
					throw new StoreException((LID)38160U, ErrorCodeValue.CorruptData, "Unexpected end of blob.");
				}
				int num = this.blobStream.Read(buffer, 0, size);
				if (num != size)
				{
					throw new StoreException((LID)54544U, ErrorCodeValue.CorruptData, "Unexpected number of bytes read from the blob stream.");
				}
			}

			// Token: 0x060000FF RID: 255 RVA: 0x0000F09C File Offset: 0x0000D29C
			private object GetInlinePropertyValue(PropertyBlob.DictionaryEntry dictionaryEntry)
			{
				SerializedValue.ValueFormat format = dictionaryEntry.Format;
				if (format <= SerializedValue.ValueFormat.Boolean)
				{
					if (format == SerializedValue.ValueFormat.FormatModifierShift)
					{
						return null;
					}
					if (format == SerializedValue.ValueFormat.Boolean)
					{
						if (dictionaryEntry.Value == 0)
						{
							return SerializedValue.BoxedFalse;
						}
						return SerializedValue.BoxedTrue;
					}
				}
				else
				{
					if (format == SerializedValue.ValueFormat.Int16)
					{
						return (short)dictionaryEntry.Value;
					}
					if (format == SerializedValue.ValueFormat.Int32)
					{
						return dictionaryEntry.Value;
					}
					if (format == (SerializedValue.ValueFormat)124)
					{
						return ValueReference.Zero;
					}
				}
				throw new StoreException((LID)42256U, ErrorCodeValue.CorruptData, "Invalid dictionary entry - format.");
			}

			// Token: 0x06000100 RID: 256 RVA: 0x0000F124 File Offset: 0x0000D324
			private object GetPropertyValueFromStream(PropertyBlob.DictionaryEntry dictionaryEntry, int maxValueLength, byte[] tempBuffer)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail((long)dictionaryEntry.Offset == this.blobStream.Position, "Stream position and entry offset mismatch");
				byte[] buffer = tempBuffer;
				if (tempBuffer.Length < maxValueLength)
				{
					if (dictionaryEntry.Format == SerializedValue.ValueFormat.Binary)
					{
						return this.GetBinaryValueFromStream(dictionaryEntry, maxValueLength, tempBuffer);
					}
					buffer = new byte[maxValueLength];
				}
				this.Read(buffer, maxValueLength);
				object result;
				if (!SerializedValue.TryParse(dictionaryEntry.Format, buffer, 0, out result))
				{
					throw new StoreException((LID)58640U, ErrorCodeValue.CorruptData, "Value parsing error.");
				}
				return result;
			}

			// Token: 0x06000101 RID: 257 RVA: 0x0000F1AC File Offset: 0x0000D3AC
			private byte[] GetBinaryValueFromStream(PropertyBlob.DictionaryEntry dictionaryEntry, int maxValueLength, byte[] tempBuffer)
			{
				this.Read(tempBuffer, 1);
				maxValueLength--;
				SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)tempBuffer[0];
				if ((byte)(dictionaryEntry.Format & SerializedValue.ValueFormat.TypeMask) != (byte)(valueFormat & SerializedValue.ValueFormat.TypeMask))
				{
					throw new StoreException((LID)34064U, ErrorCodeValue.CorruptData, "Value parsing error.");
				}
				int num2;
				int num = this.ParseLength(valueFormat, tempBuffer, out num2);
				maxValueLength -= num2;
				if (num > maxValueLength)
				{
					throw new StoreException((LID)50448U, ErrorCodeValue.CorruptData, "Value parsing error.");
				}
				byte[] array = new byte[num];
				this.Read(array, num);
				return array;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x0000F23C File Offset: 0x0000D43C
			private int ParseLength(SerializedValue.ValueFormat format, byte[] tempBuffer, out int sizeOfLength)
			{
				switch ((byte)(format & SerializedValue.ValueFormat.TypeShift))
				{
				case 0:
					sizeOfLength = 0;
					return 0;
				case 1:
					sizeOfLength = 1;
					this.Read(tempBuffer, sizeOfLength);
					return (int)tempBuffer[0];
				case 2:
					sizeOfLength = 2;
					this.Read(tempBuffer, sizeOfLength);
					return (int)((ushort)ParseSerialize.ParseInt16(tempBuffer, 0));
				case 3:
					sizeOfLength = 4;
					this.Read(tempBuffer, sizeOfLength);
					return ParseSerialize.ParseInt32(tempBuffer, 0);
				default:
					throw new StoreException((LID)47376U, ErrorCodeValue.CorruptData, "Value parsing error.");
				}
			}

			// Token: 0x06000103 RID: 259 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
			private object ConvertPropertyValue(PropertyBlob.DictionaryEntry dictionaryEntry, PropertyBlob.CompressedPropertyType desiredCompressedType, object value)
			{
				SerializedValue.ValueFormat format = dictionaryEntry.Format;
				if (format <= SerializedValue.ValueFormat.Int32)
				{
					if (format == SerializedValue.ValueFormat.FormatModifierShift)
					{
						return null;
					}
					if (format != SerializedValue.ValueFormat.Int16)
					{
						if (format == SerializedValue.ValueFormat.Int32)
						{
							int num = (int)value;
							if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int32)
							{
								return num;
							}
							if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64)
							{
								return (long)num;
							}
						}
					}
					else
					{
						short num2 = (short)value;
						if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int32)
						{
							return (int)num2;
						}
						if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64)
						{
							return (long)num2;
						}
					}
				}
				else if (format <= SerializedValue.ValueFormat.Single)
				{
					if (format != SerializedValue.ValueFormat.Int64)
					{
						if (format == SerializedValue.ValueFormat.Single)
						{
							if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Real64)
							{
								return (double)((float)value);
							}
						}
					}
					else
					{
						long num3 = (long)value;
						if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Int64)
						{
							return num3;
						}
					}
				}
				else if (format != SerializedValue.ValueFormat.Binary)
				{
					if (format == (SerializedValue.ValueFormat)124)
					{
						return ValueReference.Zero;
					}
				}
				else if (desiredCompressedType == PropertyBlob.CompressedPropertyType.Binary)
				{
					return value;
				}
				throw new StoreException((LID)63760U, ErrorCodeValue.CorruptData, "Invalid property value format.");
			}

			// Token: 0x040001BE RID: 446
			private Stream blobStream;
		}
	}
}

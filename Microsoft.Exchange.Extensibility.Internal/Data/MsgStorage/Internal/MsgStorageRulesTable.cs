using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000AC RID: 172
	internal static class MsgStorageRulesTable
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00018174 File Offset: 0x00016374
		static MsgStorageRulesTable()
		{
			MsgStorageRulesTable.rulesTable = new Dictionary<TnefPropertyType, MsgStoragePropertyTypeRule>
			{
				{
					TnefPropertyType.I2,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadInt16), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt16))
				},
				{
					(TnefPropertyType)4098,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadInt16Array), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt16Array))
				},
				{
					TnefPropertyType.Long,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadInt32), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt32))
				},
				{
					(TnefPropertyType)4099,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadInt32Array), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt32Array))
				},
				{
					TnefPropertyType.I8,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadInt64), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt64))
				},
				{
					(TnefPropertyType)4116,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadInt64Array), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt64Array))
				},
				{
					TnefPropertyType.Currency,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadInt64), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt64))
				},
				{
					(TnefPropertyType)4102,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadInt64Array), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt64Array))
				},
				{
					TnefPropertyType.R4,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadSingle), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteSingle))
				},
				{
					(TnefPropertyType)4100,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadSingleArray), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteSingleArray))
				},
				{
					TnefPropertyType.Double,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadDouble), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteDouble))
				},
				{
					(TnefPropertyType)4101,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadDoubleArray), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteDoubleArray))
				},
				{
					TnefPropertyType.AppTime,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadDouble), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteDouble))
				},
				{
					(TnefPropertyType)4103,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadDoubleArray), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteDoubleArray))
				},
				{
					TnefPropertyType.Boolean,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadBoolean), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteBoolean))
				},
				{
					TnefPropertyType.SysTime,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadSysTime), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteSysTime))
				},
				{
					(TnefPropertyType)4160,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadSysTimeArray), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteSysTimeArray))
				},
				{
					TnefPropertyType.Binary,
					new MsgStoragePropertyTypeRule(true, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadBinary), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteBinary))
				},
				{
					(TnefPropertyType)4354,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadArrayOfBinary), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteArrayOfBinary))
				},
				{
					TnefPropertyType.Unicode,
					new MsgStoragePropertyTypeRule(true, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadString), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteString))
				},
				{
					(TnefPropertyType)4127,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadStringArray), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteStringArray))
				},
				{
					TnefPropertyType.String8,
					new MsgStoragePropertyTypeRule(true, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadString8), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteString8))
				},
				{
					(TnefPropertyType)4126,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadString8Array), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteString8Array))
				},
				{
					TnefPropertyType.ClassId,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadGuid), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteGuid))
				},
				{
					(TnefPropertyType)4168,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadGuidArray), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteGuidArray))
				},
				{
					TnefPropertyType.Null,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadNull), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteNull))
				},
				{
					TnefPropertyType.Error,
					new MsgStoragePropertyTypeRule(new MsgStoragePropertyTypeRule.ReadFixedValueDelegate(MsgStorageRulesTable.ReadInt32), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteInt32))
				},
				{
					TnefPropertyType.Object,
					new MsgStoragePropertyTypeRule(false, new MsgStoragePropertyTypeRule.ReadStreamedValueDelegate(MsgStorageRulesTable.ReadObject), new MsgStoragePropertyTypeRule.WriteValueDelegate(MsgStorageRulesTable.WriteObject))
				}
			};
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001865C File Offset: 0x0001685C
		internal static void ThrowOnInvalidPropertyType(TnefPropertyType type)
		{
			MsgStoragePropertyTypeRule msgStoragePropertyTypeRule = null;
			if (!MsgStorageRulesTable.rulesTable.TryGetValue(type, out msgStoragePropertyTypeRule))
			{
				throw new NotSupportedException(MsgStorageStrings.UnsupportedPropertyType(string.Format("0x{0:x2}", type)));
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00018695 File Offset: 0x00016895
		internal static void ThrowOnInvalidPropertyType(TnefPropertyTag tag)
		{
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(tag.TnefType);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000186A3 File Offset: 0x000168A3
		internal static bool TryFindRule(TnefPropertyTag propertyTag, out MsgStoragePropertyTypeRule rule)
		{
			return MsgStorageRulesTable.TryFindRule(propertyTag.TnefType, out rule);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000186B2 File Offset: 0x000168B2
		internal static bool TryFindRule(TnefPropertyType type, out MsgStoragePropertyTypeRule rule)
		{
			return MsgStorageRulesTable.rulesTable.TryGetValue(type, out rule);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000186C0 File Offset: 0x000168C0
		private static object ReadInt16(byte[] data, int propertyOffset)
		{
			return MsgStoragePropertyData.ReadValueAsInt16(data, propertyOffset);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000186D0 File Offset: 0x000168D0
		private static object ReadInt16Array(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.Int16Size;
				short[] array2 = new short[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					array2[num3] = BitConverter.ToInt16(array, num2);
					num2 += MsgStorageRulesTable.Int16Size;
				}
				return array2;
			}
			return null;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00018734 File Offset: 0x00016934
		private static void WriteInt16(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			short? num = value as short?;
			if (num == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(short), value.GetType()));
			}
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, num.Value);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000187A0 File Offset: 0x000169A0
		private static void WriteInt16Array(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			short[] arrayValue = value as short[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(short[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				binaryWriter.Write(arrayValue[index]);
			});
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000187FD File Offset: 0x000169FD
		private static object ReadInt32(byte[] data, int propertyOffset)
		{
			return MsgStoragePropertyData.ReadValueAsInt32(data, propertyOffset);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001880C File Offset: 0x00016A0C
		private static object ReadInt32Array(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.Int32Size;
				int[] array2 = new int[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					array2[num3] = BitConverter.ToInt32(array, num2);
					num2 += MsgStorageRulesTable.Int32Size;
				}
				return array2;
			}
			return null;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00018870 File Offset: 0x00016A70
		private static void WriteInt32(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			int? num = value as int?;
			if (num == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(int), value.GetType()));
			}
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, num.Value);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000188DC File Offset: 0x00016ADC
		private static void WriteInt32Array(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			int[] arrayValue = value as int[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(int[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				binaryWriter.Write(arrayValue[index]);
			});
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018939 File Offset: 0x00016B39
		private static object ReadInt64(byte[] data, int propertyOffset)
		{
			return MsgStoragePropertyData.ReadValueAsInt64(data, propertyOffset);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018948 File Offset: 0x00016B48
		private static object ReadInt64Array(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.Int64Size;
				long[] array2 = new long[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					array2[num3] = BitConverter.ToInt64(array, num2);
					num2 += MsgStorageRulesTable.Int64Size;
				}
				return array2;
			}
			return null;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000189AC File Offset: 0x00016BAC
		private static void WriteInt64(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			long? num = value as long?;
			if (num == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(long), value.GetType()));
			}
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, num.Value);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00018A18 File Offset: 0x00016C18
		private static void WriteInt64Array(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			long[] arrayValue = value as long[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(long[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				binaryWriter.Write(arrayValue[index]);
			});
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018A75 File Offset: 0x00016C75
		private static object ReadSingle(byte[] data, int propertyOffset)
		{
			return MsgStoragePropertyData.ReadValueAsSingle(data, propertyOffset);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00018A84 File Offset: 0x00016C84
		private static object ReadSingleArray(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.SingleSize;
				float[] array2 = new float[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					array2[num3] = BitConverter.ToSingle(array, num2);
					num2 += MsgStorageRulesTable.SingleSize;
				}
				return array2;
			}
			return null;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00018AE8 File Offset: 0x00016CE8
		private static void WriteSingle(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			float? num = value as float?;
			if (num == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(float), value.GetType()));
			}
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, num.Value);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00018B54 File Offset: 0x00016D54
		private static void WriteSingleArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			float[] arrayValue = value as float[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(float[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				binaryWriter.Write(arrayValue[index]);
			});
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00018BB1 File Offset: 0x00016DB1
		private static object ReadDouble(byte[] data, int propertyOffset)
		{
			return MsgStoragePropertyData.ReadValueAsDouble(data, propertyOffset);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00018BC0 File Offset: 0x00016DC0
		private static object ReadDoubleArray(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.DoubleSize;
				double[] array2 = new double[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					array2[num3] = BitConverter.ToDouble(array, num2);
					num2 += MsgStorageRulesTable.DoubleSize;
				}
				return array2;
			}
			return null;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00018C24 File Offset: 0x00016E24
		private static void WriteDouble(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			double? num = value as double?;
			if (num == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(double), value.GetType()));
			}
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, num.Value);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00018C90 File Offset: 0x00016E90
		private static void WriteDoubleArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			double[] arrayValue = value as double[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(double[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				binaryWriter.Write(arrayValue[index]);
			});
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00018CF0 File Offset: 0x00016EF0
		private static object ReadBoolean(byte[] data, int propertyOffset)
		{
			short num = MsgStoragePropertyData.ReadValueAsInt16(data, propertyOffset);
			return num != 0;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00018D14 File Offset: 0x00016F14
		private static void WriteBoolean(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			bool? flag = value as bool?;
			if (flag == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(bool), value.GetType()));
			}
			short propertyValue = flag.Value ? 1 : 0;
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, propertyValue);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00018D70 File Offset: 0x00016F70
		private static object ReadSysTime(byte[] data, int propertyOffset)
		{
			long fileTime = MsgStoragePropertyData.ReadValueAsInt64(data, propertyOffset);
			object result;
			try
			{
				result = DateTime.FromFileTimeUtc(fileTime);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00018DB4 File Offset: 0x00016FB4
		private static object ReadSysTimeArray(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.Int64Size;
				DateTime[] array2 = new DateTime[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					long fileTime = BitConverter.ToInt64(array, num2);
					try
					{
						array2[num3] = DateTime.FromFileTimeUtc(fileTime);
					}
					catch (ArgumentOutOfRangeException)
					{
						array2[num3] = DateTime.MinValue;
					}
					num2 += MsgStorageRulesTable.Int64Size;
				}
				return array2;
			}
			return null;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00018E50 File Offset: 0x00017050
		private static void WriteSysTime(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			DateTime? dateTime = value as DateTime?;
			if (dateTime == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(DateTime), value.GetType()));
			}
			long propertyValue = 0L;
			try
			{
				propertyValue = dateTime.Value.ToFileTimeUtc();
			}
			catch (ArgumentOutOfRangeException)
			{
				propertyValue = 0L;
			}
			MsgStoragePropertyData.WriteProperty(writer.PropertiesWriter, propertyTag, propertyValue);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00018F14 File Offset: 0x00017114
		private static void WriteSysTimeArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			DateTime[] arrayValue = value as DateTime[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(DateTime[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				long value2 = 0L;
				try
				{
					value2 = arrayValue[index].ToFileTimeUtc();
				}
				catch (ArgumentOutOfRangeException)
				{
					value2 = 0L;
				}
				binaryWriter.Write(value2);
			});
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00018F74 File Offset: 0x00017174
		private static object ReadGuid(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = new byte[16];
			parser.ReadPropertyStream(propertyInfo.Tag, array, 16, 0);
			return new Guid(array);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00018FA8 File Offset: 0x000171A8
		private static object ReadGuidArray(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = new byte[16];
			byte[] array2 = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 0);
			if (array2 != null)
			{
				int num = propertyInfo.PropertyLength / MsgStorageRulesTable.GuidSize;
				Guid[] array3 = new Guid[num];
				int num2 = 0;
				for (int num3 = 0; num3 != num; num3++)
				{
					Array.Copy(array2, num2, array, 0, 16);
					array3[num3] = new Guid(array);
					num2 += MsgStorageRulesTable.GuidSize;
				}
				return array3;
			}
			return null;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001902C File Offset: 0x0001722C
		private static void WriteGuid(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			Guid? guid = value as Guid?;
			if (guid == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(Guid), value.GetType()));
			}
			byte[] array = guid.Value.ToByteArray();
			writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag), array);
			MsgStoragePropertyData.WriteStream(writer.PropertiesWriter, propertyTag, array.Length);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000190BC File Offset: 0x000172BC
		private static void WriteGuidArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			Guid[] arrayValue = value as Guid[];
			if (arrayValue == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(Guid[]), value.GetType()));
			}
			MsgStorageRulesTable.WriteArray(writer, propertyTag, arrayValue.Length, delegate(BinaryWriter binaryWriter, int index)
			{
				binaryWriter.Write(arrayValue[index].ToByteArray());
			});
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001911C File Offset: 0x0001731C
		private static void WriteArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, int arrayLength, MsgStorageRulesTable.WriteValueDelegate writeValue)
		{
			MsgSubStorageWriter.WriterBuffer valueBuffer = writer.ValueBuffer;
			for (int num = 0; num != arrayLength; num++)
			{
				writeValue(valueBuffer.Writer, num);
			}
			int length = valueBuffer.GetLength();
			byte[] buffer = valueBuffer.GetBuffer();
			writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag), buffer, length);
			MsgStoragePropertyData.WriteStream(writer.PropertiesWriter, propertyTag, length);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00019178 File Offset: 0x00017378
		private static object ReadBinary(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = new byte[propertyInfo.PropertyLength];
			parser.ReadPropertyStream(propertyInfo.Tag, array, propertyInfo.PropertyLength, 0);
			return array;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000191AC File Offset: 0x000173AC
		private static object ReadArrayOfBinary(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			int binaryArrayLengthEntrySize = MsgStorageRulesTable.BinaryArrayLengthEntrySize;
			int num;
			byte[] value = MsgStorageRulesTable.InternalReadLengthList(parser, propertyInfo, binaryArrayLengthEntrySize, out num);
			byte[][] array = new byte[num][];
			int num2 = 0;
			for (int num3 = 0; num3 != num; num3++)
			{
				int num4 = BitConverter.ToInt32(value, num2);
				array[num3] = new byte[num4];
				parser.ReadPropertyIndexStream(propertyInfo.Tag, num3, array[num3], num4, 0);
				num2 += binaryArrayLengthEntrySize;
			}
			return array;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00019218 File Offset: 0x00017418
		private static void WriteBinary(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			byte[] array = value as byte[];
			if (array == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(byte[]), value.GetType()));
			}
			writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag), array);
			MsgStoragePropertyData.WriteStream(writer.PropertiesWriter, propertyTag, array.Length);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001926C File Offset: 0x0001746C
		private static void WriteArrayOfBinary(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			byte[][] array = value as byte[][];
			if (array == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(byte[][]), value.GetType()));
			}
			MsgSubStorageWriter.WriterBuffer lengthsBuffer = writer.LengthsBuffer;
			for (int num = 0; num != array.Length; num++)
			{
				lengthsBuffer.Writer.Write(array[num].Length);
				lengthsBuffer.Writer.Write(0);
				writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag, num), array[num]);
			}
			int length = lengthsBuffer.GetLength();
			byte[] buffer = lengthsBuffer.GetBuffer();
			writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag), buffer, length);
			MsgStoragePropertyData.WriteStream(writer.PropertiesWriter, propertyTag, length);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00019318 File Offset: 0x00017518
		private static object ReadString(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 2);
			if (array != null)
			{
				return Util.UnicodeBytesToString(array, propertyInfo.PropertyLength);
			}
			return null;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00019350 File Offset: 0x00017550
		private static object ReadString8(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			byte[] array = parser.ReadPropertyStream(propertyInfo.Tag, propertyInfo.PropertyLength, 1);
			if (array != null)
			{
				return Util.AnsiBytesToString(array, propertyInfo.PropertyLength, parser.MessageEncoding);
			}
			return null;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001938C File Offset: 0x0001758C
		private static object ReadStringArray(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			int stringArrayLengthEntrySize = MsgStorageRulesTable.StringArrayLengthEntrySize;
			int num;
			byte[] value = MsgStorageRulesTable.InternalReadLengthList(parser, propertyInfo, stringArrayLengthEntrySize, out num);
			string[] array = new string[num];
			int num2 = 0;
			for (int num3 = 0; num3 != num; num3++)
			{
				int num4 = BitConverter.ToInt32(value, num2);
				byte[] bytes = parser.ReadPropertyIndexStream(propertyInfo.Tag, num3, num4, 2);
				array[num3] = Util.UnicodeBytesToString(bytes, num4);
				num2 += stringArrayLengthEntrySize;
			}
			return array;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000193F8 File Offset: 0x000175F8
		private static object ReadString8Array(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			int stringArrayLengthEntrySize = MsgStorageRulesTable.StringArrayLengthEntrySize;
			int num;
			byte[] value = MsgStorageRulesTable.InternalReadLengthList(parser, propertyInfo, stringArrayLengthEntrySize, out num);
			string[] array = new string[num];
			int num2 = 0;
			for (int num3 = 0; num3 != num; num3++)
			{
				int num4 = BitConverter.ToInt32(value, num2);
				byte[] bytes = parser.ReadPropertyIndexStream(propertyInfo.Tag, num3, num4, 1);
				array[num3] = Util.AnsiBytesToString(bytes, num4, parser.MessageEncoding);
				num2 += stringArrayLengthEntrySize;
			}
			return array;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00019468 File Offset: 0x00017668
		private static void WriteString(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			string text = value as string;
			if (text == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(string), value.GetType()));
			}
			MsgStorageRulesTable.InternalWriteString(writer, propertyTag, text);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000194A4 File Offset: 0x000176A4
		private static void WriteString8(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			string text = value as string;
			if (text == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(string), value.GetType()));
			}
			MsgStorageRulesTable.InternalWriteString(writer, propertyTag.ToUnicode(), text);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000194E4 File Offset: 0x000176E4
		private static void InternalWriteString(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, string value)
		{
			MsgSubStorageWriter.WriterBuffer valueBuffer = writer.ValueBuffer;
			int unicodeByteCount = Util.GetUnicodeByteCount(value);
			byte[] array = valueBuffer.PreallocateBuffer(unicodeByteCount);
			int num = Util.StringToUnicodeBytes(value, array);
			writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag), array, num);
			MsgStoragePropertyData.WriteStream(writer.PropertiesWriter, propertyTag, num);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00019530 File Offset: 0x00017730
		private static void WriteStringArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			string[] array = value as string[];
			if (array == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(string[]), value.GetType()));
			}
			MsgStorageRulesTable.InternalWriteStringArray(writer, propertyTag, array);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001956C File Offset: 0x0001776C
		private static void WriteString8Array(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			string[] array = value as string[];
			if (array == null)
			{
				throw new InvalidOperationException(MsgStorageStrings.InvalidValueType(typeof(string[]), value.GetType()));
			}
			MsgStorageRulesTable.InternalWriteStringArray(writer, propertyTag.ToUnicode(), array);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000195AC File Offset: 0x000177AC
		private static void InternalWriteStringArray(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, string[] arrayValue)
		{
			MsgSubStorageWriter.WriterBuffer lengthsBuffer = writer.LengthsBuffer;
			for (int num = 0; num != arrayValue.Length; num++)
			{
				MsgSubStorageWriter.WriterBuffer valueBuffer = writer.ValueBuffer;
				int unicodeByteCount = Util.GetUnicodeByteCount(arrayValue[num]);
				byte[] array = valueBuffer.PreallocateBuffer(unicodeByteCount);
				int num2 = Util.StringToUnicodeBytes(arrayValue[num], array);
				lengthsBuffer.Writer.Write(num2);
				writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag, num), array, num2);
			}
			int length = lengthsBuffer.GetLength();
			byte[] buffer = lengthsBuffer.GetBuffer();
			writer.Storage.WriteBytesToStream(Util.PropertyStreamName(propertyTag), buffer, length);
			MsgStoragePropertyData.WriteStream(writer.PropertiesWriter, propertyTag, length);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001964A File Offset: 0x0001784A
		private static object ReadNull(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			return null;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001964D File Offset: 0x0001784D
		private static void WriteNull(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			throw new NotSupportedException(MsgStorageStrings.UnsupportedPropertyType("[Null]"));
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001965E File Offset: 0x0001785E
		private static object ReadObject(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo)
		{
			throw new NotSupportedException(MsgStorageStrings.UnsupportedPropertyType("[Object]"));
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001966F File Offset: 0x0001786F
		private static void WriteObject(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object value)
		{
			throw new NotSupportedException(MsgStorageStrings.UnsupportedPropertyType("[Object]"));
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00019680 File Offset: 0x00017880
		private static byte[] InternalReadLengthList(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo, int size, out int count)
		{
			int propertyLength = propertyInfo.PropertyLength;
			count = propertyLength / size;
			if (count > 2048)
			{
				throw new MsgStorageException(MsgStorageErrorCode.MultivaluedPropertyDimensionTooLarge, MsgStorageStrings.CorruptData);
			}
			if (count == 0)
			{
				return Util.EmptyByteArray;
			}
			byte[] array = parser.ReadPropertyLengthsStream(propertyInfo.Tag, propertyInfo.PropertyLength);
			int num = 0;
			for (int num2 = 0; num2 != count; num2++)
			{
				int num3 = BitConverter.ToInt32(array, num);
				if (num3 > 32768)
				{
					throw new MsgStorageException(MsgStorageErrorCode.MultivaluedValueTooLong, MsgStorageStrings.CorruptData);
				}
				num += size;
			}
			return array;
		}

		// Token: 0x04000580 RID: 1408
		private static readonly Dictionary<TnefPropertyType, MsgStoragePropertyTypeRule> rulesTable;

		// Token: 0x04000581 RID: 1409
		private static readonly int Int16Size = Marshal.SizeOf(typeof(short));

		// Token: 0x04000582 RID: 1410
		private static readonly int Int32Size = Marshal.SizeOf(typeof(int));

		// Token: 0x04000583 RID: 1411
		private static readonly int Int64Size = Marshal.SizeOf(typeof(long));

		// Token: 0x04000584 RID: 1412
		private static readonly int SingleSize = Marshal.SizeOf(typeof(float));

		// Token: 0x04000585 RID: 1413
		private static readonly int DoubleSize = Marshal.SizeOf(typeof(double));

		// Token: 0x04000586 RID: 1414
		private static readonly int GuidSize = Marshal.SizeOf(typeof(Guid));

		// Token: 0x04000587 RID: 1415
		private static readonly int StringArrayLengthEntrySize = MsgStorageRulesTable.Int32Size;

		// Token: 0x04000588 RID: 1416
		private static readonly int BinaryArrayLengthEntrySize = 2 * MsgStorageRulesTable.Int32Size;

		// Token: 0x020000AD RID: 173
		// (Invoke) Token: 0x06000596 RID: 1430
		private delegate void WriteValueDelegate(BinaryWriter writer, int index);
	}
}

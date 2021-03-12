using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization
{
	// Token: 0x02000060 RID: 96
	public static class ComplianceSerializer
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public static T DeSerialize<T>(ComplianceSerializationDescription<T> description, byte[] blob) where T : class, new()
		{
			T result = default(T);
			FaultDefinition faultDefinition = null;
			if (!ComplianceSerializer.TryDeserialize<T>(description, blob, out result, out faultDefinition, "DeSerialize", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionCommon\\Serialization\\ComplianceSerializer.cs", 77))
			{
				throw new BadStructureFormatException();
			}
			return result;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		public static byte[] Serialize<T>(ComplianceSerializationDescription<T> description, T inputObject) where T : class, new()
		{
			if (inputObject == null)
			{
				return null;
			}
			int num = 1;
			num = num + 1 + description.TotalByteFields;
			num = num + 1 + 2 * description.TotalShortFields;
			num = num + 1 + 4 * description.TotalIntegerFields;
			num = num + 1 + 8 * description.TotalLongFields;
			num = num + 1 + 8 * description.TotalDoubleFields;
			num = num + 1 + 16 * description.TotalGuidFields;
			num = num + 1 + 4 * description.TotalStringFields;
			List<Tuple<int, byte[]>> variableWidthMemberBytes = ComplianceSerializer.GetVariableWidthMemberBytes<T>(description, inputObject, ComplianceSerializer.VariableWidthType.String);
			foreach (Tuple<int, byte[]> tuple in variableWidthMemberBytes)
			{
				num += tuple.Item1;
			}
			num = num + 1 + 4 * description.TotalBlobFields;
			List<Tuple<int, byte[]>> variableWidthMemberBytes2 = ComplianceSerializer.GetVariableWidthMemberBytes<T>(description, inputObject, ComplianceSerializer.VariableWidthType.Blob);
			foreach (Tuple<int, byte[]> tuple2 in variableWidthMemberBytes2)
			{
				num += tuple2.Item1;
			}
			num++;
			List<ComplianceSerializer.CollectionField> list = new List<ComplianceSerializer.CollectionField>();
			byte b = 0;
			while ((int)b < description.TotalCollectionFields)
			{
				CollectionItemType itemType = CollectionItemType.NotDefined;
				if (description.TryGetCollectionPropertyItemType(b, out itemType))
				{
					IEnumerable<object> collectionItems = description.GetCollectionItems(inputObject, b);
					ComplianceSerializer.CollectionField collectionField = ComplianceSerializer.CollectionField.GetCollectionField(itemType, collectionItems);
					list.Add(collectionField);
					num += collectionField.GetSizeOfSerializedCollectionField();
				}
				b += 1;
			}
			byte[] array = new byte[num];
			array[0] = description.ComplianceStructureId;
			int num2 = ComplianceSerializer.WriteFixedWidthFieldsToBlob<T>(ref description, ref inputObject, array, 1, 1, ComplianceSerializer.FixedWidthType.Byte, description.TotalByteFields);
			num2 = ComplianceSerializer.WriteFixedWidthFieldsToBlob<T>(ref description, ref inputObject, array, num2, 2, ComplianceSerializer.FixedWidthType.Short, description.TotalShortFields);
			num2 = ComplianceSerializer.WriteFixedWidthFieldsToBlob<T>(ref description, ref inputObject, array, num2, 4, ComplianceSerializer.FixedWidthType.Int, description.TotalIntegerFields);
			num2 = ComplianceSerializer.WriteFixedWidthFieldsToBlob<T>(ref description, ref inputObject, array, num2, 8, ComplianceSerializer.FixedWidthType.Long, description.TotalLongFields);
			num2 = ComplianceSerializer.WriteFixedWidthFieldsToBlob<T>(ref description, ref inputObject, array, num2, 8, ComplianceSerializer.FixedWidthType.Double, description.TotalDoubleFields);
			num2 = ComplianceSerializer.WriteFixedWidthFieldsToBlob<T>(ref description, ref inputObject, array, num2, 16, ComplianceSerializer.FixedWidthType.Guid, description.TotalGuidFields);
			num2 = ComplianceSerializer.WriteVariableWidthFieldsToBlob(array, num2, variableWidthMemberBytes);
			num2 = ComplianceSerializer.WriteVariableWidthFieldsToBlob(array, num2, variableWidthMemberBytes2);
			num2 = ComplianceSerializer.WriteCollectionsToBlob(array, num2, list);
			return array;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		public static bool TryDeserialize<T>(ComplianceSerializationDescription<T> description, byte[] blob, out T parsedObject, out FaultDefinition faultDefinition, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0) where T : class, new()
		{
			parsedObject = Activator.CreateInstance<T>();
			int totalLength = blob.Length;
			if (description.ComplianceStructureId != blob[0])
			{
				faultDefinition = FaultDefinition.FromErrorString("Parsing wrong structure", callerMember, callerFilePath, callerLineNumber);
				return false;
			}
			int startIndex = 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			flag = ComplianceSerializer.TryWriteFixedWidthFieldsToObject<T>(ref description, ref parsedObject, blob, startIndex, 1, ComplianceSerializer.FixedWidthType.Byte, totalLength, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteFixedWidthFieldsToObject<T>(ref description, ref parsedObject, blob, startIndex, 2, ComplianceSerializer.FixedWidthType.Short, totalLength, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteFixedWidthFieldsToObject<T>(ref description, ref parsedObject, blob, startIndex, 4, ComplianceSerializer.FixedWidthType.Int, totalLength, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteFixedWidthFieldsToObject<T>(ref description, ref parsedObject, blob, startIndex, 8, ComplianceSerializer.FixedWidthType.Long, totalLength, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteFixedWidthFieldsToObject<T>(ref description, ref parsedObject, blob, startIndex, 8, ComplianceSerializer.FixedWidthType.Double, totalLength, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteFixedWidthFieldsToObject<T>(ref description, ref parsedObject, blob, startIndex, 16, ComplianceSerializer.FixedWidthType.Guid, totalLength, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteVariableWidthMembersToObject<T>(ref description, ref parsedObject, blob, startIndex, totalLength, ComplianceSerializer.VariableWidthType.String, flag, out startIndex, ref stringBuilder);
			flag = ComplianceSerializer.TryWriteVariableWidthMembersToObject<T>(ref description, ref parsedObject, blob, startIndex, totalLength, ComplianceSerializer.VariableWidthType.Blob, flag, out startIndex, ref stringBuilder);
			if (flag)
			{
				flag = ComplianceSerializer.TryWriteCollectionsToObject<T>(ref description, ref parsedObject, blob, startIndex, totalLength, out startIndex, ref stringBuilder);
			}
			if (flag)
			{
				faultDefinition = null;
			}
			else
			{
				faultDefinition = FaultDefinition.FromErrorString(stringBuilder.ToString(), callerMember, callerFilePath, callerLineNumber);
			}
			return flag;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000DDEF File Offset: 0x0000BFEF
		private static void WriteShortToBlob(byte[] blob, int index, short shortValue)
		{
			blob[index] = (byte)(shortValue >> 8);
			blob[index + 1] = (byte)shortValue;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DDFF File Offset: 0x0000BFFF
		private static void WriteIntToBlob(byte[] blob, int index, int intValue)
		{
			blob[index] = (byte)(intValue >> 24);
			blob[index + 1] = (byte)(intValue >> 16);
			blob[index + 2] = (byte)(intValue >> 8);
			blob[index + 3] = (byte)intValue;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DE24 File Offset: 0x0000C024
		private static void WriteLongToBlob(byte[] blob, int index, long longValue)
		{
			blob[index] = (byte)(longValue >> 56);
			blob[index + 1] = (byte)(longValue >> 48);
			blob[index + 2] = (byte)(longValue >> 40);
			blob[index + 3] = (byte)(longValue >> 32);
			blob[index + 4] = (byte)(longValue >> 24);
			blob[index + 5] = (byte)(longValue >> 16);
			blob[index + 6] = (byte)(longValue >> 8);
			blob[index + 7] = (byte)longValue;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DE7C File Offset: 0x0000C07C
		private unsafe static void WriteDoubleToBlob(byte[] blob, int index, double doubleValue)
		{
			ulong num = (ulong)(*(long*)(&doubleValue));
			blob[index] = (byte)(num >> 56);
			blob[index + 1] = (byte)(num >> 48);
			blob[index + 2] = (byte)(num >> 40);
			blob[index + 3] = (byte)(num >> 32);
			blob[index + 4] = (byte)(num >> 24);
			blob[index + 5] = (byte)(num >> 16);
			blob[index + 6] = (byte)(num >> 8);
			blob[index + 7] = (byte)num;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		private static void WriteGuidToBlob(byte[] blob, int index, Guid guidValue)
		{
			byte[] array = guidValue.ToByteArray();
			Array.Copy(array, 0, blob, index, array.Length);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		private static int WriteFixedWidthFieldsToBlob<T>(ref ComplianceSerializationDescription<T> description, ref T inputObject, byte[] blob, int startIndex, int width, ComplianceSerializer.FixedWidthType widthType, int totalFields) where T : class, new()
		{
			byte b = Convert.ToByte(totalFields);
			blob[startIndex] = b;
			int num = startIndex + 1;
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				switch (widthType)
				{
				case ComplianceSerializer.FixedWidthType.Byte:
				{
					byte b3 = 0;
					description.TryGetByteProperty(inputObject, b2, out b3);
					blob[num] = b3;
					break;
				}
				case ComplianceSerializer.FixedWidthType.Short:
				{
					short shortValue = 0;
					description.TryGetShortProperty(inputObject, b2, out shortValue);
					ComplianceSerializer.WriteShortToBlob(blob, num, shortValue);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Int:
				{
					int intValue = 0;
					description.TryGetIntegerProperty(inputObject, b2, out intValue);
					ComplianceSerializer.WriteIntToBlob(blob, num, intValue);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Long:
				{
					long longValue = 0L;
					description.TryGetLongProperty(inputObject, b2, out longValue);
					ComplianceSerializer.WriteLongToBlob(blob, num, longValue);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Double:
				{
					double doubleValue = 0.0;
					description.TryGetDoubleProperty(inputObject, b2, out doubleValue);
					ComplianceSerializer.WriteDoubleToBlob(blob, num, doubleValue);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Guid:
				{
					Guid empty = Guid.Empty;
					description.TryGetGuidProperty(inputObject, b2, out empty);
					ComplianceSerializer.WriteGuidToBlob(blob, num, empty);
					break;
				}
				default:
					throw new ArgumentException("widthType");
				}
				num += width;
			}
			return num;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E030 File Offset: 0x0000C230
		private static int WriteVariableWidthFieldsToBlob(byte[] blob, int index, List<Tuple<int, byte[]>> fieldMembers)
		{
			blob[index++] = (byte)fieldMembers.Count;
			foreach (Tuple<int, byte[]> tuple in fieldMembers)
			{
				blob[index++] = (byte)(tuple.Item1 >> 24);
				blob[index++] = (byte)(tuple.Item1 >> 16);
				blob[index++] = (byte)(tuple.Item1 >> 8);
				blob[index++] = (byte)tuple.Item1;
			}
			foreach (Tuple<int, byte[]> tuple2 in fieldMembers)
			{
				if (tuple2.Item1 > 0)
				{
					byte[] item = tuple2.Item2;
					int num = item.Length;
					Array.Copy(item, 0, blob, index, num);
					index += num;
				}
			}
			return index;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E128 File Offset: 0x0000C328
		private static int WriteCollectionsToBlob(byte[] blob, int index, List<ComplianceSerializer.CollectionField> collections)
		{
			blob[index++] = (byte)collections.Count;
			foreach (ComplianceSerializer.CollectionField collectionField in collections)
			{
				index = collectionField.WriteFieldToBlob(blob, index);
			}
			return index;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E18C File Offset: 0x0000C38C
		private static List<Tuple<int, byte[]>> GetVariableWidthMemberBytes<T>(ComplianceSerializationDescription<T> description, T inputObject, ComplianceSerializer.VariableWidthType widthType) where T : class, new()
		{
			List<Tuple<int, byte[]>> list = new List<Tuple<int, byte[]>>();
			byte b = (byte)description.TotalStringFields;
			if (widthType == ComplianceSerializer.VariableWidthType.Blob)
			{
				b = (byte)description.TotalBlobFields;
			}
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				int item = 0;
				byte[] array = null;
				string text;
				if (widthType == ComplianceSerializer.VariableWidthType.Blob)
				{
					if (inputObject != null && description.TryGetBlobProperty(inputObject, b2, out array) && array != null)
					{
						item = array.Length;
					}
				}
				else if (inputObject != null && description.TryGetStringProperty(inputObject, b2, out text) && text != null)
				{
					array = Encoding.UTF8.GetBytes(text);
					item = array.Length;
				}
				list.Add(new Tuple<int, byte[]>(item, array));
			}
			return list;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E222 File Offset: 0x0000C422
		private static short ReadShortFromBlob(byte[] blob, int index)
		{
			return (short)((int)blob[index] << 8 | (int)blob[index + 1]);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E230 File Offset: 0x0000C430
		private static int ReadIntFromBlob(byte[] blob, int index)
		{
			return (int)blob[index] << 24 | (int)blob[index + 1] << 16 | (int)blob[index + 2] << 8 | (int)blob[index + 3];
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E250 File Offset: 0x0000C450
		private static long ReadLongFromBlob(byte[] blob, int index)
		{
			int num = (int)blob[index] << 24 | (int)blob[index + 1] << 16 | (int)blob[index + 2] << 8 | (int)blob[index + 3];
			int num2 = (int)blob[index + 4] << 24 | (int)blob[index + 5] << 16 | (int)blob[index + 6] << 8 | (int)blob[index + 7];
			return (long)((ulong)num2 | (ulong)((ulong)((long)num) << 32));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		private unsafe static double ReadDoubleFromBlob(byte[] blob, int index)
		{
			uint num = (uint)((int)blob[index] << 24 | (int)blob[index + 1] << 16 | (int)blob[index + 2] << 8 | (int)blob[index + 3]);
			uint num2 = (uint)((int)blob[index + 4] << 24 | (int)blob[index + 5] << 16 | (int)blob[index + 6] << 8 | (int)blob[index + 7]);
			ulong num3 = (ulong)num2 | (ulong)num << 32;
			return *(double*)(&num3);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E300 File Offset: 0x0000C500
		private static Guid ReadGuidFromBlob(byte[] blob, int index)
		{
			byte[] array = new byte[16];
			Array.Copy(blob, index, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E328 File Offset: 0x0000C528
		private static bool TryWriteFixedWidthFieldsToObject<T>(ref ComplianceSerializationDescription<T> description, ref T parsedObject, byte[] blob, int startIndex, int width, ComplianceSerializer.FixedWidthType widthType, int totalLength, bool continueDeserialization, out int index, ref StringBuilder errorBuilder) where T : class, new()
		{
			if (!continueDeserialization)
			{
				index = startIndex;
				return continueDeserialization;
			}
			index = startIndex;
			if (startIndex >= totalLength)
			{
				errorBuilder.AppendFormat("StartIndex:{0} is bigger than blob length:{1}", startIndex, totalLength);
				return false;
			}
			byte b = blob[startIndex];
			index++;
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				if (index + width > totalLength)
				{
					errorBuilder.AppendFormat("Blob length:{0} is not sufficient to read the field from index:{1}.", totalLength, index);
					return false;
				}
				switch (widthType)
				{
				case ComplianceSerializer.FixedWidthType.Byte:
					description.TrySetByteProperty(parsedObject, b2, blob[index]);
					break;
				case ComplianceSerializer.FixedWidthType.Short:
				{
					short value = ComplianceSerializer.ReadShortFromBlob(blob, index);
					description.TrySetShortProperty(parsedObject, b2, value);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Int:
				{
					int value2 = ComplianceSerializer.ReadIntFromBlob(blob, index);
					description.TrySetIntegerProperty(parsedObject, b2, value2);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Long:
				{
					long value3 = ComplianceSerializer.ReadLongFromBlob(blob, index);
					description.TrySetLongProperty(parsedObject, b2, value3);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Double:
				{
					double value4 = ComplianceSerializer.ReadDoubleFromBlob(blob, index);
					description.TrySetDoubleProperty(parsedObject, b2, value4);
					break;
				}
				case ComplianceSerializer.FixedWidthType.Guid:
				{
					Guid value5 = ComplianceSerializer.ReadGuidFromBlob(blob, index);
					description.TrySetGuidProperty(parsedObject, b2, value5);
					break;
				}
				default:
					throw new ArgumentException("widthType");
				}
				index += width;
			}
			return true;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E498 File Offset: 0x0000C698
		private static bool TryWriteVariableWidthMembersToObject<T>(ref ComplianceSerializationDescription<T> description, ref T parsedObject, byte[] blob, int startIndex, int totalLength, ComplianceSerializer.VariableWidthType widthType, bool continueDeserialization, out int index, ref StringBuilder errorBuilder) where T : class, new()
		{
			if (!continueDeserialization)
			{
				index = startIndex;
				return continueDeserialization;
			}
			index = startIndex;
			if (startIndex >= totalLength)
			{
				errorBuilder.AppendFormat("StartIndex:{0} is bigger than blob length:{1}", startIndex, totalLength);
				return false;
			}
			byte b = blob[index++];
			if (b > 0)
			{
				List<int> list = new List<int>();
				for (byte b2 = 0; b2 < b; b2 += 1)
				{
					if (index + 4 > totalLength)
					{
						errorBuilder.AppendFormat("Blob length:{0} is not sufficient to read the field-width from index:{1}.", totalLength, index);
						return false;
					}
					int item = ComplianceSerializer.ReadIntFromBlob(blob, index);
					list.Add(item);
					index += 4;
				}
				byte b3 = 0;
				foreach (int num in list)
				{
					if (num > 0)
					{
						if (index + num > totalLength)
						{
							errorBuilder.AppendFormat("Blob length:{0} is not sufficient to read the field of size:{1} from index:{2}.", totalLength, num, index);
							return false;
						}
						if (widthType == ComplianceSerializer.VariableWidthType.String)
						{
							string @string = Encoding.UTF8.GetString(blob, index, num);
							description.TrySetStringProperty(parsedObject, b3, @string);
							index += num;
						}
						else
						{
							byte[] array = new byte[num];
							Array.Copy(blob, index, array, 0, num);
							description.TrySetBlobProperty(parsedObject, b3, array);
							index += num;
						}
					}
					b3 += 1;
				}
				return true;
			}
			return true;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E630 File Offset: 0x0000C830
		private static bool TryWriteCollectionsToObject<T>(ref ComplianceSerializationDescription<T> description, ref T parsedObject, byte[] blob, int startIndex, int totalLength, out int index, ref StringBuilder errorBuilder) where T : class, new()
		{
			index = startIndex;
			if (startIndex >= totalLength)
			{
				errorBuilder.AppendFormat("StartIndex:{0} is bigger than blob length:{1}", startIndex, totalLength);
				return false;
			}
			byte b = blob[index++];
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				Type typeFromHandle = typeof(CollectionItemType);
				CollectionItemType collectionItemType = (CollectionItemType)Enum.ToObject(typeFromHandle, blob[index++]);
				if (!Enum.IsDefined(typeFromHandle, collectionItemType))
				{
					errorBuilder.AppendFormat("Byte value:{0} at index:{1} does not represent a valid CollectionItemType", collectionItemType, index - 1);
					return false;
				}
				if (index + 4 > totalLength)
				{
					errorBuilder.AppendFormat("Blob length:{0} is not sufficient to read the field count:{1} at index:{2}.", totalLength, b, index);
					return false;
				}
				int num = ComplianceSerializer.ReadIntFromBlob(blob, index);
				index += 4;
				List<object> list = new List<object>();
				for (int i = 0; i < num; i++)
				{
					ComplianceSerializer.CollectionItem collectionItem = null;
					if (!ComplianceSerializer.CollectionItem.TryGetCollectionItemFromBlob(collectionItemType, blob, index, totalLength, out collectionItem, ref errorBuilder))
					{
						return false;
					}
					list.Add(ComplianceSerializer.CollectionItem.GetObject(collectionItemType, collectionItem));
					index += collectionItem.GetSerializedSize();
				}
				description.TrySetCollectionItems(parsedObject, b2, list);
			}
			return true;
		}

		// Token: 0x0400022C RID: 556
		private const int SizeOfGuid = 16;

		// Token: 0x02000061 RID: 97
		private enum FixedWidthType
		{
			// Token: 0x0400022E RID: 558
			Byte,
			// Token: 0x0400022F RID: 559
			Short,
			// Token: 0x04000230 RID: 560
			Int,
			// Token: 0x04000231 RID: 561
			Long,
			// Token: 0x04000232 RID: 562
			Double,
			// Token: 0x04000233 RID: 563
			Guid
		}

		// Token: 0x02000062 RID: 98
		private enum VariableWidthType
		{
			// Token: 0x04000235 RID: 565
			String,
			// Token: 0x04000236 RID: 566
			Blob
		}

		// Token: 0x02000063 RID: 99
		private class CollectionField
		{
			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000E775 File Offset: 0x0000C975
			// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000E77D File Offset: 0x0000C97D
			public CollectionItemType CollectionItemType { get; set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000E786 File Offset: 0x0000C986
			// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000E78E File Offset: 0x0000C98E
			public int NumberItems { get; set; }

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060002FA RID: 762 RVA: 0x0000E797 File Offset: 0x0000C997
			public ICollection<ComplianceSerializer.CollectionItem> CollectionItems
			{
				get
				{
					return this.collectionItems;
				}
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
			public static ComplianceSerializer.CollectionField GetCollectionField(CollectionItemType itemType, IEnumerable<object> objects)
			{
				ComplianceSerializer.CollectionField collectionField = new ComplianceSerializer.CollectionField();
				collectionField.CollectionItemType = itemType;
				int num = 0;
				foreach (object obj in objects)
				{
					ComplianceSerializer.CollectionItem collectionItemFromObject = ComplianceSerializer.CollectionItem.GetCollectionItemFromObject(itemType, obj);
					num++;
					collectionField.CollectionItems.Add(collectionItemFromObject);
				}
				collectionField.NumberItems = num;
				return collectionField;
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000E818 File Offset: 0x0000CA18
			public int GetSizeOfSerializedCollectionField()
			{
				int num = 0;
				foreach (ComplianceSerializer.CollectionItem collectionItem in this.CollectionItems)
				{
					num += collectionItem.GetSerializedSize();
				}
				return 5 + num;
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000E86C File Offset: 0x0000CA6C
			public int WriteFieldToBlob(byte[] blob, int index)
			{
				blob[index++] = (byte)this.CollectionItemType;
				ComplianceSerializer.WriteIntToBlob(blob, index, this.NumberItems);
				index += 4;
				foreach (ComplianceSerializer.CollectionItem collectionItem in this.CollectionItems)
				{
					index = collectionItem.WriteItemToBlob(blob, index);
				}
				return index;
			}

			// Token: 0x04000237 RID: 567
			private List<ComplianceSerializer.CollectionItem> collectionItems = new List<ComplianceSerializer.CollectionItem>();
		}

		// Token: 0x02000064 RID: 100
		private class CollectionItem
		{
			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000E8F3 File Offset: 0x0000CAF3
			// (set) Token: 0x06000300 RID: 768 RVA: 0x0000E8FB File Offset: 0x0000CAFB
			public bool IsFixedWidth { get; private set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0000E904 File Offset: 0x0000CB04
			// (set) Token: 0x06000302 RID: 770 RVA: 0x0000E90C File Offset: 0x0000CB0C
			public byte[] ItemBlob { get; private set; }

			// Token: 0x06000303 RID: 771 RVA: 0x0000E918 File Offset: 0x0000CB18
			public static bool TryGetCollectionItemFromBlob(CollectionItemType itemType, byte[] blob, int index, int totalLength, out ComplianceSerializer.CollectionItem item, ref StringBuilder errorBuilder)
			{
				item = new ComplianceSerializer.CollectionItem();
				int num = 0;
				int num2 = 0;
				switch (itemType)
				{
				case CollectionItemType.Short:
					item.IsFixedWidth = true;
					num = 2;
					break;
				case CollectionItemType.Int:
					item.IsFixedWidth = true;
					num = 4;
					break;
				case CollectionItemType.Long:
					item.IsFixedWidth = true;
					num = 8;
					break;
				case CollectionItemType.Double:
					item.IsFixedWidth = true;
					num = 8;
					break;
				case CollectionItemType.Guid:
					item.IsFixedWidth = true;
					num = 16;
					break;
				case CollectionItemType.String:
				case CollectionItemType.Blob:
					item.IsFixedWidth = false;
					num2 = 4;
					if (index + num2 > totalLength)
					{
						errorBuilder.AppendFormat("Blob length:{0} is not sufficient to read the collection item width at index:{1}", totalLength, index);
						return false;
					}
					num = ComplianceSerializer.ReadIntFromBlob(blob, index);
					break;
				}
				item.ItemBlob = new byte[num];
				if (index + num2 + num > totalLength)
				{
					errorBuilder.AppendFormat("Blob length:{0} is not sufficient to read the collection item at index:{1}", totalLength, index + num2);
					return false;
				}
				Array.Copy(blob, index + num2, item.ItemBlob, 0, num);
				return true;
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000EA18 File Offset: 0x0000CC18
			public static ComplianceSerializer.CollectionItem GetCollectionItemFromObject(CollectionItemType itemType, object obj)
			{
				ComplianceSerializer.CollectionItem collectionItem = new ComplianceSerializer.CollectionItem();
				switch (itemType)
				{
				case CollectionItemType.Short:
					collectionItem.IsFixedWidth = true;
					collectionItem.ItemBlob = new byte[2];
					ComplianceSerializer.WriteShortToBlob(collectionItem.ItemBlob, 0, (short)obj);
					break;
				case CollectionItemType.Int:
					collectionItem.IsFixedWidth = true;
					collectionItem.ItemBlob = new byte[4];
					ComplianceSerializer.WriteIntToBlob(collectionItem.ItemBlob, 0, (int)obj);
					break;
				case CollectionItemType.Long:
					collectionItem.IsFixedWidth = true;
					collectionItem.ItemBlob = new byte[8];
					ComplianceSerializer.WriteLongToBlob(collectionItem.ItemBlob, 0, (long)obj);
					break;
				case CollectionItemType.Double:
					collectionItem.IsFixedWidth = true;
					collectionItem.ItemBlob = new byte[8];
					ComplianceSerializer.WriteDoubleToBlob(collectionItem.ItemBlob, 0, (double)obj);
					break;
				case CollectionItemType.Guid:
					collectionItem.IsFixedWidth = true;
					collectionItem.ItemBlob = new byte[16];
					ComplianceSerializer.WriteGuidToBlob(collectionItem.ItemBlob, 0, (Guid)obj);
					break;
				case CollectionItemType.String:
					collectionItem.IsFixedWidth = false;
					collectionItem.ItemBlob = Encoding.UTF8.GetBytes((string)obj);
					break;
				case CollectionItemType.Blob:
					collectionItem.IsFixedWidth = false;
					collectionItem.ItemBlob = (byte[])obj;
					break;
				}
				return collectionItem;
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000EB58 File Offset: 0x0000CD58
			public static object GetObject(CollectionItemType itemType, ComplianceSerializer.CollectionItem item)
			{
				switch (itemType)
				{
				case CollectionItemType.Short:
					return ComplianceSerializer.ReadShortFromBlob(item.ItemBlob, 0);
				case CollectionItemType.Int:
					return ComplianceSerializer.ReadIntFromBlob(item.ItemBlob, 0);
				case CollectionItemType.Long:
					return ComplianceSerializer.ReadLongFromBlob(item.ItemBlob, 0);
				case CollectionItemType.Double:
					return ComplianceSerializer.ReadDoubleFromBlob(item.ItemBlob, 0);
				case CollectionItemType.Guid:
					return ComplianceSerializer.ReadGuidFromBlob(item.ItemBlob, 0);
				case CollectionItemType.String:
					return Encoding.UTF8.GetString(item.ItemBlob);
				case CollectionItemType.Blob:
					return item.ItemBlob;
				default:
					return null;
				}
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000EC00 File Offset: 0x0000CE00
			public int GetSerializedSize()
			{
				if (this.IsFixedWidth)
				{
					return this.ItemBlob.Length;
				}
				return this.ItemBlob.Length + 4;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000EC20 File Offset: 0x0000CE20
			public int WriteItemToBlob(byte[] blob, int index)
			{
				if (this.IsFixedWidth)
				{
					Array.Copy(this.ItemBlob, 0, blob, index, this.ItemBlob.Length);
				}
				else
				{
					ComplianceSerializer.WriteIntToBlob(blob, index, this.ItemBlob.Length);
					Array.Copy(this.ItemBlob, 0, blob, index + 4, this.ItemBlob.Length);
				}
				return index + this.GetSerializedSize();
			}
		}
	}
}

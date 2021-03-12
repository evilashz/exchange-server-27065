using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000F14 RID: 3860
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ActivityLogSerializer
	{
		// Token: 0x060084EA RID: 34026 RVA: 0x002458F0 File Offset: 0x00243AF0
		public static byte[] Serialize(StoreSession storeSession)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null)
			{
				IActivityLog activityLog = ActivityLogFactory.Current.Bind(mailboxSession);
				return ActivityLogSerializer.Serialize(activityLog);
			}
			return Array<byte>.Empty;
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x00245920 File Offset: 0x00243B20
		public static byte[] Serialize(IActivityLog activityLog)
		{
			List<Activity> list = activityLog.Query().Reverse<Activity>().ToList<Activity>();
			if (list.Count == 0)
			{
				return Array<byte>.Empty;
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(list.Count * 128))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(1);
					binaryWriter.Write(1);
					binaryWriter.Write(0L);
					List<ActivityLogSerializer.PropertySerializer> propertySerializers = ActivityLogSerializer.SerializeActivitySchema(binaryWriter);
					binaryWriter.Write(list.Count);
					foreach (Activity activity in list)
					{
						ActivityLogSerializer.SerializeActivity(binaryWriter, activity, propertySerializers);
						if (memoryStream.Length > 10485760L)
						{
							throw new NotSupportedException(string.Format("Serialization of activity logs longer than {0} is not supported", 10485760));
						}
					}
					array = new byte[memoryStream.Length];
					Array.Copy(memoryStream.GetBuffer(), array, memoryStream.Length);
				}
			}
			return array;
		}

		// Token: 0x060084EC RID: 34028 RVA: 0x00245A50 File Offset: 0x00243C50
		public static void Deserialize(StoreSession storeSession, byte[] serializedActivityLog)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null)
			{
				IActivityLog activityLog = ActivityLogFactory.Current.Bind(mailboxSession);
				ActivityLogSerializer.Deserialize(activityLog, serializedActivityLog);
			}
		}

		// Token: 0x060084ED RID: 34029 RVA: 0x00245A7C File Offset: 0x00243C7C
		public static void Deserialize(IActivityLog activityLog, byte[] serializedActivityLog)
		{
			Util.ThrowOnNullArgument(serializedActivityLog, "serializedActivityLog");
			if (serializedActivityLog.Length == 0)
			{
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(serializedActivityLog, true))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					binaryReader.ReadInt32();
					binaryReader.ReadInt32();
					binaryReader.ReadInt64();
					List<ActivityLogSerializer.SerializedPropertyInfo> propertyInfos = ActivityLogSerializer.DeserializeActivitySchema(binaryReader);
					int num = binaryReader.ReadInt32();
					List<Activity> list = new List<Activity>(num);
					for (int i = 0; i < num; i++)
					{
						Activity item = ActivityLogSerializer.DeserializeActivity(binaryReader, propertyInfos);
						list.Add(item);
					}
					activityLog.Reset();
					activityLog.Append(list);
				}
			}
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x00245C2C File Offset: 0x00243E2C
		private static List<ActivityLogSerializer.PropertySerializer> SerializeActivitySchema(BinaryWriter writer)
		{
			writer.Write(ActivitySchema.PropertyCollection.Count);
			List<ActivityLogSerializer.PropertySerializer> list = new List<ActivityLogSerializer.PropertySerializer>(ActivitySchema.PropertyCollection.Count);
			foreach (PropertyDefinition propertyDefinition in ActivitySchema.PropertyCollection)
			{
				writer.Write(propertyDefinition.Name);
				if (propertyDefinition.Type == typeof(int))
				{
					writer.Write(7);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						int value = (int)propertyValue;
						binaryWriter.Write(value);
					});
				}
				else if (propertyDefinition.Type == typeof(bool))
				{
					writer.Write(5);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						bool value = (bool)propertyValue;
						binaryWriter.Write(value);
					});
				}
				else if (propertyDefinition.Type == typeof(string))
				{
					writer.Write(0);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						string value = (string)propertyValue;
						binaryWriter.Write(value);
					});
				}
				else if (propertyDefinition.Type == typeof(byte[]))
				{
					writer.Write(4);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						byte[] array = (byte[])propertyValue;
						binaryWriter.Write(array.Length);
						binaryWriter.Write(array);
					});
				}
				else if (propertyDefinition.Type == typeof(Guid))
				{
					writer.Write(0);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						string value = ((Guid)propertyValue).ToString();
						binaryWriter.Write(value);
					});
				}
				else if (propertyDefinition.Type == typeof(ExDateTime))
				{
					writer.Write(8);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						long value = ((ExDateTime)propertyValue).ToFileTimeUtc();
						binaryWriter.Write(value);
					});
				}
				else
				{
					if (!(propertyDefinition.Type == typeof(StoreObjectId)))
					{
						throw new NotSupportedException(string.Format("Serialization of property {0} of type {1} is not supported", propertyDefinition.Name, propertyDefinition.Type.FullName));
					}
					writer.Write(4);
					list.Add(delegate(BinaryWriter binaryWriter, object propertyValue)
					{
						StoreObjectId storeObjectId = (StoreObjectId)propertyValue;
						byte[] bytes = storeObjectId.GetBytes();
						binaryWriter.Write(bytes.Length);
						binaryWriter.Write(bytes);
					});
				}
			}
			return list;
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x00245FF4 File Offset: 0x002441F4
		private static List<ActivityLogSerializer.SerializedPropertyInfo> DeserializeActivitySchema(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			List<ActivityLogSerializer.SerializedPropertyInfo> list = new List<ActivityLogSerializer.SerializedPropertyInfo>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				ActivityLogSerializer.DataTypes dataTypes = (ActivityLogSerializer.DataTypes)reader.ReadByte();
				PropertyDefinition propertyDefinition = null;
				ActivitySchema.PropertyNameToPropertyDefinitionMapping.TryGetValue(text, out propertyDefinition);
				Type typeFromHandle;
				ActivityLogSerializer.PropertyDeserializer propertyDeserializer;
				if (dataTypes == ActivityLogSerializer.DataTypes.String)
				{
					typeFromHandle = typeof(string);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadString());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Char)
				{
					typeFromHandle = typeof(char);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadChar());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.CharArray)
				{
					typeFromHandle = typeof(char[]);
					propertyDeserializer = delegate(BinaryReader binaryReader)
					{
						int count = reader.ReadInt32();
						return reader.ReadChars(count);
					};
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Byte)
				{
					typeFromHandle = typeof(byte);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadByte());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.ByteArray)
				{
					typeFromHandle = typeof(byte[]);
					propertyDeserializer = delegate(BinaryReader binaryReader)
					{
						int count = reader.ReadInt32();
						return reader.ReadBytes(count);
					};
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Bool)
				{
					typeFromHandle = typeof(bool);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadBoolean());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Int16)
				{
					typeFromHandle = typeof(short);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadInt16());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Int32)
				{
					typeFromHandle = typeof(int);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadInt32());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Int64)
				{
					typeFromHandle = typeof(long);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadInt64());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.UInt16)
				{
					typeFromHandle = typeof(ushort);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadUInt16());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.UInt32)
				{
					typeFromHandle = typeof(uint);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadUInt32());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.UInt64)
				{
					typeFromHandle = typeof(ulong);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadUInt64());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Decimal)
				{
					typeFromHandle = typeof(decimal);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadDecimal());
				}
				else if (dataTypes == ActivityLogSerializer.DataTypes.Single)
				{
					typeFromHandle = typeof(float);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadSingle());
				}
				else
				{
					if (dataTypes != ActivityLogSerializer.DataTypes.Double)
					{
						throw new NotSupportedException(string.Format("Deserialization of property {0} of type {1} is not supported", text, dataTypes.ToString()));
					}
					typeFromHandle = typeof(double);
					propertyDeserializer = ((BinaryReader binaryReader) => reader.ReadDouble());
				}
				list.Add(new ActivityLogSerializer.SerializedPropertyInfo(text, typeFromHandle, propertyDeserializer, propertyDefinition));
			}
			return list;
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x00246364 File Offset: 0x00244564
		private static void SerializeActivity(BinaryWriter writer, Activity activity, List<ActivityLogSerializer.PropertySerializer> propertySerializers)
		{
			ExAssert.RetailAssert(ActivitySchema.PropertyCollection.Count == propertySerializers.Count, "The count of properties in the schema does not match the count of property serializers.");
			for (int i = 0; i < ActivitySchema.PropertyCollection.Count; i++)
			{
				object propertyValue;
				bool flag = activity.TryGetSchemaProperty(ActivitySchema.PropertyCollection[i], out propertyValue);
				if (flag)
				{
					writer.Write(true);
					propertySerializers[i](writer, propertyValue);
				}
				else
				{
					writer.Write(false);
				}
			}
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x002463D8 File Offset: 0x002445D8
		private static Activity DeserializeActivity(BinaryReader reader, List<ActivityLogSerializer.SerializedPropertyInfo> propertyInfos)
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			foreach (ActivityLogSerializer.SerializedPropertyInfo serializedPropertyInfo in propertyInfos)
			{
				bool flag = reader.ReadBoolean();
				if (flag)
				{
					object value = serializedPropertyInfo.PropertyDeserializer(reader);
					if (serializedPropertyInfo.PropertyDefinition != null)
					{
						value = ActivityLogSerializer.ConvertPropertyValue(serializedPropertyInfo.PropertyDefinition, value);
						memoryPropertyBag.SetProperty(serializedPropertyInfo.PropertyDefinition, value);
					}
				}
			}
			return new Activity(memoryPropertyBag);
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x00246468 File Offset: 0x00244668
		private static object ConvertPropertyValue(PropertyDefinition propertyDefinition, object value)
		{
			Type type = value.GetType();
			if (propertyDefinition.Type == type)
			{
				return value;
			}
			if (propertyDefinition.Type == typeof(Guid) && type == typeof(string))
			{
				return Guid.Parse((string)value);
			}
			if (propertyDefinition.Type == typeof(ExDateTime) && type == typeof(long))
			{
				return ExDateTime.FromFileTimeUtc((long)value);
			}
			if (propertyDefinition.Type == typeof(StoreObjectId) && type == typeof(byte[]))
			{
				return StoreObjectId.Parse((byte[])value, 0);
			}
			throw new NotSupportedException(string.Format("The conversation from serialized property type {0} to actual property type {1} for {2} is not supported.", type.FullName, propertyDefinition.Type.FullName, propertyDefinition.Name));
		}

		// Token: 0x04005903 RID: 22787
		private const int maxSize = 10485760;

		// Token: 0x04005904 RID: 22788
		private const int serializerVersion = 1;

		// Token: 0x04005905 RID: 22789
		private const int minimumSupporterSerializerVersion = 1;

		// Token: 0x02000F15 RID: 3861
		private enum DataTypes : byte
		{
			// Token: 0x0400590E RID: 22798
			String,
			// Token: 0x0400590F RID: 22799
			Char,
			// Token: 0x04005910 RID: 22800
			CharArray,
			// Token: 0x04005911 RID: 22801
			Byte,
			// Token: 0x04005912 RID: 22802
			ByteArray,
			// Token: 0x04005913 RID: 22803
			Bool,
			// Token: 0x04005914 RID: 22804
			Int16,
			// Token: 0x04005915 RID: 22805
			Int32,
			// Token: 0x04005916 RID: 22806
			Int64,
			// Token: 0x04005917 RID: 22807
			UInt16,
			// Token: 0x04005918 RID: 22808
			UInt32,
			// Token: 0x04005919 RID: 22809
			UInt64,
			// Token: 0x0400591A RID: 22810
			Decimal,
			// Token: 0x0400591B RID: 22811
			Single,
			// Token: 0x0400591C RID: 22812
			Double
		}

		// Token: 0x02000F16 RID: 3862
		[Flags]
		private enum SerializationFlags : long
		{
			// Token: 0x0400591E RID: 22814
			None = 0L
		}

		// Token: 0x02000F17 RID: 3863
		// (Invoke) Token: 0x060084FB RID: 34043
		private delegate void PropertySerializer(BinaryWriter writer, object propertyValue);

		// Token: 0x02000F18 RID: 3864
		// (Invoke) Token: 0x060084FF RID: 34047
		private delegate object PropertyDeserializer(BinaryReader reader);

		// Token: 0x02000F19 RID: 3865
		private class SerializedPropertyInfo
		{
			// Token: 0x06008502 RID: 34050 RVA: 0x0024655C File Offset: 0x0024475C
			public SerializedPropertyInfo(string propertyName, Type propertyType, ActivityLogSerializer.PropertyDeserializer propertyDeserializer, PropertyDefinition propertyDefinition)
			{
				this.PropertyName = propertyName;
				this.PropertyType = propertyType;
				this.PropertyDeserializer = propertyDeserializer;
				this.PropertyDefinition = propertyDefinition;
			}

			// Token: 0x17002338 RID: 9016
			// (get) Token: 0x06008503 RID: 34051 RVA: 0x00246581 File Offset: 0x00244781
			// (set) Token: 0x06008504 RID: 34052 RVA: 0x00246589 File Offset: 0x00244789
			public string PropertyName { get; private set; }

			// Token: 0x17002339 RID: 9017
			// (get) Token: 0x06008505 RID: 34053 RVA: 0x00246592 File Offset: 0x00244792
			// (set) Token: 0x06008506 RID: 34054 RVA: 0x0024659A File Offset: 0x0024479A
			public Type PropertyType { get; private set; }

			// Token: 0x1700233A RID: 9018
			// (get) Token: 0x06008507 RID: 34055 RVA: 0x002465A3 File Offset: 0x002447A3
			// (set) Token: 0x06008508 RID: 34056 RVA: 0x002465AB File Offset: 0x002447AB
			public ActivityLogSerializer.PropertyDeserializer PropertyDeserializer { get; private set; }

			// Token: 0x1700233B RID: 9019
			// (get) Token: 0x06008509 RID: 34057 RVA: 0x002465B4 File Offset: 0x002447B4
			// (set) Token: 0x0600850A RID: 34058 RVA: 0x002465BC File Offset: 0x002447BC
			public PropertyDefinition PropertyDefinition { get; private set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000006 RID: 6
	public struct SerializableCatalog
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003D7F File Offset: 0x00001F7F
		public SerializableCatalog(string tableName, string tableType, string partitionKey, string parameterTypes, string visibility)
		{
			this.tableName = tableName;
			this.tableType = tableType;
			this.partitionKey = partitionKey;
			this.parameterTypes = parameterTypes;
			this.visibility = visibility;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003DA6 File Offset: 0x00001FA6
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003DAE File Offset: 0x00001FAE
		public string TableType
		{
			get
			{
				return this.tableType;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003DB6 File Offset: 0x00001FB6
		public string PartitionKey
		{
			get
			{
				return this.partitionKey;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003DBE File Offset: 0x00001FBE
		public string ParameterTypes
		{
			get
			{
				return this.parameterTypes;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003DC6 File Offset: 0x00001FC6
		public string Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003DD0 File Offset: 0x00001FD0
		internal static byte[] Serialize(IList<SerializableCatalog> arrayOfItems)
		{
			int count = arrayOfItems.Count;
			int num = 0;
			num += SerializedValue.SerializeInt32(1, null, 0);
			num += SerializedValue.SerializeInt32(count, null, 0);
			for (int i = 0; i < count; i++)
			{
				num += SerializedValue.SerializeString(arrayOfItems[i].tableName, null, 0);
				num += SerializedValue.SerializeString(arrayOfItems[i].tableType, null, 0);
				num += SerializedValue.SerializeString(arrayOfItems[i].partitionKey, null, 0);
				num += SerializedValue.SerializeString(arrayOfItems[i].parameterTypes, null, 0);
				num += SerializedValue.SerializeString(arrayOfItems[i].visibility, null, 0);
			}
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += SerializedValue.SerializeInt32(1, array, num2);
			num2 += SerializedValue.SerializeInt32(count, array, num2);
			for (int j = 0; j < count; j++)
			{
				num2 += SerializedValue.SerializeString(arrayOfItems[j].tableName, array, num2);
				num2 += SerializedValue.SerializeString(arrayOfItems[j].tableType, array, num2);
				num2 += SerializedValue.SerializeString(arrayOfItems[j].partitionKey, array, num2);
				num2 += SerializedValue.SerializeString(arrayOfItems[j].parameterTypes, array, num2);
				num2 += SerializedValue.SerializeString(arrayOfItems[j].visibility, array, num2);
			}
			return array;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003F38 File Offset: 0x00002138
		internal static SerializableCatalog[] Deserialize(byte[] theBlob)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(theBlob, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Wrong version.");
			}
			int num3 = SerializedValue.ParseInt32(theBlob, ref num);
			if ((theBlob.Length - num) / 5 < num3)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			if (num3 < 0)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			SerializableCatalog[] array = new SerializableCatalog[num3];
			for (int i = 0; i < num3; i++)
			{
				string text = SerializedValue.ParseString(theBlob, ref num);
				string text2 = SerializedValue.ParseString(theBlob, ref num);
				string text3 = SerializedValue.ParseString(theBlob, ref num);
				string text4 = SerializedValue.ParseString(theBlob, ref num);
				string text5 = SerializedValue.ParseString(theBlob, ref num);
				array[i] = new SerializableCatalog(text, text2, text3, text4, text5);
			}
			return array;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003FF4 File Offset: 0x000021F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("TableName:[");
			stringBuilder.AppendAsString(this.TableName);
			stringBuilder.Append("] TableType:[");
			stringBuilder.AppendAsString(this.TableType);
			stringBuilder.Append("] PartitionKey:[");
			stringBuilder.AppendAsString(this.PartitionKey);
			stringBuilder.Append("] ParameterTypes:[");
			stringBuilder.AppendAsString(this.ParameterTypes);
			stringBuilder.Append("] Visibility:[");
			stringBuilder.AppendAsString(this.Visibility);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004094 File Offset: 0x00002294
		public static string SerializableCatalogAsString(byte[] theBlob)
		{
			SerializableCatalog[] array = SerializableCatalog.Deserialize(theBlob);
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("SerializableCatalog:[");
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("[");
				stringBuilder.Append(array[i].ToString());
				stringBuilder.Append("]");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000059 RID: 89
		public const int BlobVersion = 1;

		// Token: 0x0400005A RID: 90
		private string tableName;

		// Token: 0x0400005B RID: 91
		private string tableType;

		// Token: 0x0400005C RID: 92
		private string partitionKey;

		// Token: 0x0400005D RID: 93
		private string parameterTypes;

		// Token: 0x0400005E RID: 94
		private string visibility;
	}
}

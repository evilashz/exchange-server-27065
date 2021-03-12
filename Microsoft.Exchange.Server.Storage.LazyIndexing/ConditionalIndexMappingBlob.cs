using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000006 RID: 6
	public struct ConditionalIndexMappingBlob
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000028EE File Offset: 0x00000AEE
		public ConditionalIndexMappingBlob(string columnName, bool columnValue)
		{
			this.columnName = columnName;
			this.columnValue = columnValue;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000028FE File Offset: 0x00000AFE
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002906 File Offset: 0x00000B06
		public bool ColumnValue
		{
			get
			{
				return this.columnValue;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002910 File Offset: 0x00000B10
		internal static byte[] Serialize(IList<ConditionalIndexMappingBlob> arrayOfItems)
		{
			int count = arrayOfItems.Count;
			int num = 0;
			num += SerializedValue.SerializeInt32(1, null, 0);
			num += SerializedValue.SerializeInt32(count, null, 0);
			for (int i = 0; i < count; i++)
			{
				num += SerializedValue.SerializeString(arrayOfItems[i].columnName, null, 0);
				num += SerializedValue.SerializeBoolean(arrayOfItems[i].columnValue, null, 0);
			}
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += SerializedValue.SerializeInt32(1, array, num2);
			num2 += SerializedValue.SerializeInt32(count, array, num2);
			for (int j = 0; j < count; j++)
			{
				num2 += SerializedValue.SerializeString(arrayOfItems[j].columnName, array, num2);
				num2 += SerializedValue.SerializeBoolean(arrayOfItems[j].columnValue, array, num2);
			}
			return array;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029E0 File Offset: 0x00000BE0
		internal static ConditionalIndexMappingBlob[] Deserialize(byte[] theBlob)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(theBlob, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Wrong version.");
			}
			int num3 = SerializedValue.ParseInt32(theBlob, ref num);
			if ((theBlob.Length - num) / 2 < num3)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			if (num3 < 0)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			ConditionalIndexMappingBlob[] array = new ConditionalIndexMappingBlob[num3];
			for (int i = 0; i < num3; i++)
			{
				string text = SerializedValue.ParseString(theBlob, ref num);
				bool flag = SerializedValue.ParseBoolean(theBlob, ref num);
				array[i] = new ConditionalIndexMappingBlob(text, flag);
			}
			return array;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A78 File Offset: 0x00000C78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("columnName:[");
			stringBuilder.AppendAsString(this.columnName);
			stringBuilder.Append("] columnValue:[");
			stringBuilder.AppendAsString(this.columnValue);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public static string ConditionalIndexMappingBlobAsString(byte[] theBlob)
		{
			ConditionalIndexMappingBlob[] array = ConditionalIndexMappingBlob.Deserialize(theBlob);
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("ConditionalIndexMappingBlob:[");
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

		// Token: 0x04000018 RID: 24
		public const int BlobVersion = 1;

		// Token: 0x04000019 RID: 25
		private string columnName;

		// Token: 0x0400001A RID: 26
		private bool columnValue;
	}
}

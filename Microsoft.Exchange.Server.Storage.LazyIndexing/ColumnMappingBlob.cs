using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000002 RID: 2
	public struct ColumnMappingBlob
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ColumnMappingBlob(int columnType, bool fixedLength, int columnLength, string propName, int propId)
		{
			this.columnType = columnType;
			this.fixedLength = fixedLength;
			this.columnLength = columnLength;
			this.propName = propName;
			this.propId = propId;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F7 File Offset: 0x000002F7
		public int ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020FF File Offset: 0x000002FF
		public bool FixedLength
		{
			get
			{
				return this.fixedLength;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002107 File Offset: 0x00000307
		public int ColumnLength
		{
			get
			{
				return this.columnLength;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000210F File Offset: 0x0000030F
		public string PropName
		{
			get
			{
				return this.propName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002117 File Offset: 0x00000317
		public int PropId
		{
			get
			{
				return this.propId;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
		internal static byte[] Serialize(int keyColumnCount, IList<ColumnMappingBlob> arrayOfItems)
		{
			int count = arrayOfItems.Count;
			int num = 0;
			num += SerializedValue.SerializeInt32(1, null, 0);
			num += SerializedValue.SerializeInt32(count, null, 0);
			num += SerializedValue.SerializeInt32(keyColumnCount, null, 0);
			for (int i = 0; i < count; i++)
			{
				num += SerializedValue.SerializeInt32(arrayOfItems[i].columnType, null, 0);
				num += SerializedValue.SerializeBoolean(arrayOfItems[i].fixedLength, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].columnLength, null, 0);
				num += SerializedValue.SerializeString(arrayOfItems[i].propName, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].propId, null, 0);
			}
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += SerializedValue.SerializeInt32(1, array, num2);
			num2 += SerializedValue.SerializeInt32(count, array, num2);
			num2 += SerializedValue.SerializeInt32(keyColumnCount, array, num2);
			for (int j = 0; j < count; j++)
			{
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].columnType, array, num2);
				num2 += SerializedValue.SerializeBoolean(arrayOfItems[j].fixedLength, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].columnLength, array, num2);
				num2 += SerializedValue.SerializeString(arrayOfItems[j].propName, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].propId, array, num2);
			}
			return array;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022A0 File Offset: 0x000004A0
		internal static ColumnMappingBlob[] Deserialize(out int keyColumnCount, byte[] theBlob)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(theBlob, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Wrong version.");
			}
			int num3 = SerializedValue.ParseInt32(theBlob, ref num);
			keyColumnCount = SerializedValue.ParseInt32(theBlob, ref num);
			if ((theBlob.Length - num) / 5 < num3)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			if (num3 < 0)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			ColumnMappingBlob[] array = new ColumnMappingBlob[num3];
			for (int i = 0; i < num3; i++)
			{
				int num4 = SerializedValue.ParseInt32(theBlob, ref num);
				bool flag = SerializedValue.ParseBoolean(theBlob, ref num);
				int num5 = SerializedValue.ParseInt32(theBlob, ref num);
				string text = SerializedValue.ParseString(theBlob, ref num);
				int num6 = SerializedValue.ParseInt32(theBlob, ref num);
				array[i] = new ColumnMappingBlob(num4, flag, num5, text, num6);
			}
			return array;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002364 File Offset: 0x00000564
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("columnType:[");
			stringBuilder.AppendAsString(this.columnType);
			stringBuilder.Append("] fixedLength:[");
			stringBuilder.AppendAsString(this.fixedLength);
			stringBuilder.Append("] columnLength:[");
			stringBuilder.AppendAsString(this.columnLength);
			stringBuilder.Append("] propName:[");
			stringBuilder.AppendAsString(this.propName);
			stringBuilder.Append("] propId:[");
			stringBuilder.AppendAsString(this.propId);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002404 File Offset: 0x00000604
		public static string ColumnMappingBlobAsString(byte[] theBlob)
		{
			int value;
			ColumnMappingBlob[] array = ColumnMappingBlob.Deserialize(out value, theBlob);
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("ColumnMappingBlob:[");
			stringBuilder.Append("keyColumnCount=[");
			stringBuilder.Append(value);
			stringBuilder.Append("], ");
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

		// Token: 0x04000001 RID: 1
		public const int BlobVersion = 1;

		// Token: 0x04000002 RID: 2
		private int columnType;

		// Token: 0x04000003 RID: 3
		private bool fixedLength;

		// Token: 0x04000004 RID: 4
		private int columnLength;

		// Token: 0x04000005 RID: 5
		private string propName;

		// Token: 0x04000006 RID: 6
		private int propId;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000009 RID: 9
	public struct IndexDefinitionBlob
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000312B File Offset: 0x0000132B
		public IndexDefinitionBlob(int columnType, int maxLength, bool fixedLength, bool ascending)
		{
			this.columnType = columnType;
			this.maxLength = maxLength;
			this.fixedLength = fixedLength;
			this.ascending = ascending;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000314A File Offset: 0x0000134A
		public int ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003152 File Offset: 0x00001352
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000315A File Offset: 0x0000135A
		public bool FixedLength
		{
			get
			{
				return this.fixedLength;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003162 File Offset: 0x00001362
		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000316C File Offset: 0x0000136C
		internal static byte[] Serialize(int keyColumnCount, int lcid, short identityColumnIndex, IList<IndexDefinitionBlob> arrayOfItems)
		{
			int count = arrayOfItems.Count;
			int num = 0;
			num += SerializedValue.SerializeInt32(1, null, 0);
			num += SerializedValue.SerializeInt32(count, null, 0);
			num += SerializedValue.SerializeInt32(keyColumnCount, null, 0);
			num += SerializedValue.SerializeInt32(lcid, null, 0);
			num += SerializedValue.SerializeInt16(identityColumnIndex, null, 0);
			for (int i = 0; i < count; i++)
			{
				num += SerializedValue.SerializeInt32(arrayOfItems[i].columnType, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].maxLength, null, 0);
				num += SerializedValue.SerializeBoolean(arrayOfItems[i].fixedLength, null, 0);
				num += SerializedValue.SerializeBoolean(arrayOfItems[i].ascending, null, 0);
			}
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += SerializedValue.SerializeInt32(1, array, num2);
			num2 += SerializedValue.SerializeInt32(count, array, num2);
			num2 += SerializedValue.SerializeInt32(keyColumnCount, array, num2);
			num2 += SerializedValue.SerializeInt32(lcid, array, num2);
			num2 += SerializedValue.SerializeInt16(identityColumnIndex, array, num2);
			for (int j = 0; j < count; j++)
			{
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].columnType, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].maxLength, array, num2);
				num2 += SerializedValue.SerializeBoolean(arrayOfItems[j].fixedLength, array, num2);
				num2 += SerializedValue.SerializeBoolean(arrayOfItems[j].ascending, array, num2);
			}
			return array;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032E8 File Offset: 0x000014E8
		internal static IndexDefinitionBlob[] Deserialize(out int keyColumnCount, out int lcid, out short identityColumnIndex, byte[] theBlob)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(theBlob, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Wrong version.");
			}
			int num3 = SerializedValue.ParseInt32(theBlob, ref num);
			keyColumnCount = SerializedValue.ParseInt32(theBlob, ref num);
			lcid = SerializedValue.ParseInt32(theBlob, ref num);
			identityColumnIndex = SerializedValue.ParseInt16(theBlob, ref num);
			if ((theBlob.Length - num) / 4 < num3)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			if (num3 < 0)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			IndexDefinitionBlob[] array = new IndexDefinitionBlob[num3];
			for (int i = 0; i < num3; i++)
			{
				int num4 = SerializedValue.ParseInt32(theBlob, ref num);
				int num5 = SerializedValue.ParseInt32(theBlob, ref num);
				bool flag = SerializedValue.ParseBoolean(theBlob, ref num);
				bool flag2 = SerializedValue.ParseBoolean(theBlob, ref num);
				array[i] = new IndexDefinitionBlob(num4, num5, flag, flag2);
			}
			return array;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000033B4 File Offset: 0x000015B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("columnType:[");
			stringBuilder.AppendAsString(this.columnType);
			stringBuilder.Append("] maxLength:[");
			stringBuilder.AppendAsString(this.maxLength);
			stringBuilder.Append("] fixedLength:[");
			stringBuilder.AppendAsString(this.fixedLength);
			stringBuilder.Append("] ascending:[");
			stringBuilder.AppendAsString(this.ascending);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000343C File Offset: 0x0000163C
		public static string IndexDefinitionBlobAsString(byte[] theBlob)
		{
			int value;
			int value2;
			short value3;
			IndexDefinitionBlob[] array = IndexDefinitionBlob.Deserialize(out value, out value2, out value3, theBlob);
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("IndexDefinitionBlob:[");
			stringBuilder.Append("keyColumnCount=[");
			stringBuilder.Append(value);
			stringBuilder.Append("], ");
			stringBuilder.Append("lcid=[");
			stringBuilder.Append(value2);
			stringBuilder.Append("], ");
			stringBuilder.Append("identityColumnIndex=[");
			stringBuilder.Append(value3);
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

		// Token: 0x0400002B RID: 43
		public const int BlobVersion = 1;

		// Token: 0x0400002C RID: 44
		private int columnType;

		// Token: 0x0400002D RID: 45
		private int maxLength;

		// Token: 0x0400002E RID: 46
		private bool fixedLength;

		// Token: 0x0400002F RID: 47
		private bool ascending;
	}
}

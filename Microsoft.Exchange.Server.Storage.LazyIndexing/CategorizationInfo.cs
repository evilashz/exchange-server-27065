using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000011 RID: 17
	public class CategorizationInfo
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00004D37 File Offset: 0x00002F37
		public CategorizationInfo(int baseMessageViewLogicalIndexNumber, bool baseMessageViewInReverseOrder, int categoryCount, CategoryHeaderSortOverride[] categoryHeaderSortOverrides)
		{
			this.baseMessageViewLogicalIndexNumber = baseMessageViewLogicalIndexNumber;
			this.baseMessageViewInReverseOrder = baseMessageViewInReverseOrder;
			this.categoryCount = categoryCount;
			this.categoryHeaderSortOverrides = categoryHeaderSortOverrides;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004D5C File Offset: 0x00002F5C
		public int BaseMessageViewLogicalIndexNumber
		{
			get
			{
				return this.baseMessageViewLogicalIndexNumber;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004D64 File Offset: 0x00002F64
		public bool BaseMessageViewInReverseOrder
		{
			get
			{
				return this.baseMessageViewInReverseOrder;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004D6C File Offset: 0x00002F6C
		public int CategoryCount
		{
			get
			{
				return this.categoryCount;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004D74 File Offset: 0x00002F74
		public CategoryHeaderSortOverride[] CategoryHeaderSortOverrides
		{
			get
			{
				return this.categoryHeaderSortOverrides;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004D7C File Offset: 0x00002F7C
		public static CategorizationInfo Deserialize(byte[] buffer, Func<int, string, Column> convertToColumn)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(buffer, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Invalid version for the serialized CategorizationInfo blob.");
			}
			int num3 = SerializedValue.ParseInt32(buffer, ref num);
			bool flag = SerializedValue.ParseBoolean(buffer, ref num);
			int num4 = SerializedValue.ParseInt32(buffer, ref num);
			int num5 = SerializedValue.ParseInt32(buffer, ref num);
			CategoryHeaderSortOverride[] array = new CategoryHeaderSortOverride[num4];
			for (int i = 0; i < num5; i++)
			{
				int num6 = SerializedValue.ParseInt32(buffer, ref num);
				array[num6] = CategoryHeaderSortOverride.Deserialize(buffer, ref num, convertToColumn);
			}
			return new CategorizationInfo(num3, flag, num4, array);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004E08 File Offset: 0x00003008
		public byte[] Serialize()
		{
			int num = this.Serialize(null);
			byte[] array = new byte[num];
			this.Serialize(array);
			return array;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004E30 File Offset: 0x00003030
		public bool IsMatching(CategorizationInfo candidateCategorizationInfo)
		{
			bool flag = this.baseMessageViewLogicalIndexNumber == candidateCategorizationInfo.baseMessageViewLogicalIndexNumber && this.baseMessageViewInReverseOrder == candidateCategorizationInfo.baseMessageViewInReverseOrder && this.categoryCount == candidateCategorizationInfo.categoryCount;
			if (flag)
			{
				for (int i = 0; i < this.categoryCount; i++)
				{
					CategoryHeaderSortOverride categoryHeaderSortOverride = this.categoryHeaderSortOverrides[i];
					CategoryHeaderSortOverride categoryHeaderSortOverride2 = candidateCategorizationInfo.categoryHeaderSortOverrides[i];
					if (categoryHeaderSortOverride == null)
					{
						if (categoryHeaderSortOverride2 != null)
						{
							flag = false;
							break;
						}
					}
					else
					{
						if (categoryHeaderSortOverride2 == null)
						{
							flag = false;
							break;
						}
						if (categoryHeaderSortOverride.Column != categoryHeaderSortOverride2.Column || categoryHeaderSortOverride.Ascending != categoryHeaderSortOverride2.Ascending || categoryHeaderSortOverride.AggregateByMaxValue != categoryHeaderSortOverride2.AggregateByMaxValue)
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004ED8 File Offset: 0x000030D8
		private int Serialize(byte[] buffer)
		{
			int num = 0;
			num += SerializedValue.SerializeInt32(1, buffer, num);
			num += SerializedValue.SerializeInt32(this.baseMessageViewLogicalIndexNumber, buffer, num);
			num += SerializedValue.SerializeBoolean(this.baseMessageViewInReverseOrder, buffer, num);
			num += SerializedValue.SerializeInt32(this.categoryCount, buffer, num);
			num += SerializedValue.SerializeInt32(CategoryHeaderSortOverride.NumberOfOverrides(this.categoryHeaderSortOverrides), buffer, num);
			for (int i = 0; i < this.categoryCount; i++)
			{
				if (this.categoryHeaderSortOverrides[i] != null)
				{
					num += SerializedValue.SerializeInt32(i, buffer, num);
					num += this.categoryHeaderSortOverrides[i].Serialize(buffer, num);
				}
			}
			return num;
		}

		// Token: 0x04000080 RID: 128
		private const int BlobVersion = 1;

		// Token: 0x04000081 RID: 129
		private readonly int baseMessageViewLogicalIndexNumber;

		// Token: 0x04000082 RID: 130
		private readonly bool baseMessageViewInReverseOrder;

		// Token: 0x04000083 RID: 131
		private readonly int categoryCount;

		// Token: 0x04000084 RID: 132
		private readonly CategoryHeaderSortOverride[] categoryHeaderSortOverrides;
	}
}

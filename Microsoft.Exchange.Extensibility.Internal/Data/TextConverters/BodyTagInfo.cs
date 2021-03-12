using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000021 RID: 33
	internal class BodyTagInfo
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x0000632E File Offset: 0x0000452E
		internal BodyTagInfo(int wordCount, int wordCrc, int formatCrc)
		{
			this.wordCount = wordCount;
			this.wordCrc = wordCrc;
			this.formatCrc = formatCrc;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000634B File Offset: 0x0000454B
		public int WordCount
		{
			get
			{
				return this.wordCount;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006353 File Offset: 0x00004553
		public int WordCrc
		{
			get
			{
				return this.wordCrc;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000635B File Offset: 0x0000455B
		public int FormatCrc
		{
			get
			{
				return this.formatCrc;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006364 File Offset: 0x00004564
		public static BodyTagInfo FromByteArray(byte[] byteArray)
		{
			int num = BodyTagInfo.ReadInt(byteArray, 0);
			int num2 = BodyTagInfo.ReadInt(byteArray, 4);
			int num3 = BodyTagInfo.ReadInt(byteArray, 8);
			return new BodyTagInfo(num, num2, num3);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006391 File Offset: 0x00004591
		public static bool operator !=(BodyTagInfo left, BodyTagInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000639D File Offset: 0x0000459D
		public static bool operator ==(BodyTagInfo left, BodyTagInfo right)
		{
			if (!object.ReferenceEquals(left, null))
			{
				return left.Equals(right);
			}
			return object.ReferenceEquals(right, null);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000063B8 File Offset: 0x000045B8
		public byte[] ToByteArray()
		{
			byte[] array = new byte[12];
			BodyTagInfo.WriteInt(array, 0, this.wordCount);
			BodyTagInfo.WriteInt(array, 4, this.wordCrc);
			BodyTagInfo.WriteInt(array, 8, this.formatCrc);
			return array;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000063F8 File Offset: 0x000045F8
		public override bool Equals(object obj)
		{
			BodyTagInfo bodyTagInfo = obj as BodyTagInfo;
			return bodyTagInfo != null && bodyTagInfo.WordCount == this.WordCount && bodyTagInfo.WordCrc == this.WordCrc && bodyTagInfo.FormatCrc == this.FormatCrc;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006441 File Offset: 0x00004641
		public override int GetHashCode()
		{
			return this.WordCrc ^ this.WordCount ^ this.FormatCrc;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006458 File Offset: 0x00004658
		private static void WriteInt(byte[] byteArray, int beginIndex, int value)
		{
			int num = 0;
			while (num < 4 && beginIndex < byteArray.Length)
			{
				byteArray[beginIndex] = (byte)(value >> num * 8);
				num++;
				beginIndex++;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006488 File Offset: 0x00004688
		private static int ReadInt(byte[] byteArray, int beginIndex)
		{
			int num = 0;
			int num2 = 0;
			while (num2 < 4 && beginIndex < byteArray.Length)
			{
				num += (int)byteArray[beginIndex] << num2 * 8;
				num2++;
				beginIndex++;
			}
			return num;
		}

		// Token: 0x04000117 RID: 279
		private readonly int wordCount;

		// Token: 0x04000118 RID: 280
		private readonly int wordCrc;

		// Token: 0x04000119 RID: 281
		private readonly int formatCrc;
	}
}

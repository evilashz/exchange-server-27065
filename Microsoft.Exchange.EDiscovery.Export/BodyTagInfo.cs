using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000004 RID: 4
	internal class BodyTagInfo
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021EE File Offset: 0x000003EE
		internal BodyTagInfo(int wordCount, int wordCrc, int formatCrc)
		{
			this.wordCount = wordCount;
			this.wordCrc = wordCrc;
			this.formatCrc = formatCrc;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000220B File Offset: 0x0000040B
		public int WordCount
		{
			get
			{
				return this.wordCount;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002213 File Offset: 0x00000413
		public int WordCrc
		{
			get
			{
				return this.wordCrc;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000221B File Offset: 0x0000041B
		public int FormatCrc
		{
			get
			{
				return this.formatCrc;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
		public static BodyTagInfo FromByteArray(byte[] byteArray)
		{
			int num = BodyTagInfo.ReadInt(byteArray, 0);
			int num2 = BodyTagInfo.ReadInt(byteArray, 4);
			int num3 = BodyTagInfo.ReadInt(byteArray, 8);
			return new BodyTagInfo(num, num2, num3);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002251 File Offset: 0x00000451
		public static bool operator !=(BodyTagInfo left, BodyTagInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225D File Offset: 0x0000045D
		public static bool operator ==(BodyTagInfo left, BodyTagInfo right)
		{
			if (!object.ReferenceEquals(left, null))
			{
				return left.Equals(right);
			}
			return object.ReferenceEquals(right, null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002278 File Offset: 0x00000478
		public byte[] ToByteArray()
		{
			byte[] array = new byte[12];
			BodyTagInfo.WriteInt(array, 0, this.wordCount);
			BodyTagInfo.WriteInt(array, 4, this.wordCrc);
			BodyTagInfo.WriteInt(array, 8, this.formatCrc);
			return array;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B8 File Offset: 0x000004B8
		public override bool Equals(object obj)
		{
			BodyTagInfo bodyTagInfo = obj as BodyTagInfo;
			return bodyTagInfo != null && bodyTagInfo.WordCount == this.WordCount && bodyTagInfo.WordCrc == this.WordCrc && bodyTagInfo.FormatCrc == this.FormatCrc;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002301 File Offset: 0x00000501
		public override int GetHashCode()
		{
			return this.WordCrc ^ this.WordCount ^ this.FormatCrc;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002318 File Offset: 0x00000518
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

		// Token: 0x06000012 RID: 18 RVA: 0x00002348 File Offset: 0x00000548
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

		// Token: 0x04000004 RID: 4
		private readonly int wordCount;

		// Token: 0x04000005 RID: 5
		private readonly int wordCrc;

		// Token: 0x04000006 RID: 6
		private readonly int formatCrc;
	}
}

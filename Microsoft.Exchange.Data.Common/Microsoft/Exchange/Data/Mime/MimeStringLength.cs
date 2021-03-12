using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000078 RID: 120
	internal struct MimeStringLength
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001AA4C File Offset: 0x00018C4C
		public MimeStringLength(int value)
		{
			this.inChars = value;
			this.inBytes = value;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001AA5C File Offset: 0x00018C5C
		public MimeStringLength(int valueInChars, int valueInBytes)
		{
			this.inChars = valueInChars;
			this.inBytes = valueInBytes;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001AA6C File Offset: 0x00018C6C
		public int InChars
		{
			get
			{
				return this.inChars;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001AA74 File Offset: 0x00018C74
		public int InBytes
		{
			get
			{
				return this.inBytes;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001AA7C File Offset: 0x00018C7C
		public void IncrementBy(int count)
		{
			this.inChars += count;
			this.inBytes += count;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001AA9A File Offset: 0x00018C9A
		public void IncrementBy(int countInChars, int countInBytes)
		{
			this.inChars += countInChars;
			this.inBytes += countInBytes;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public void IncrementBy(MimeStringLength count)
		{
			this.inChars += count.InChars;
			this.inBytes += count.InBytes;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001AAE2 File Offset: 0x00018CE2
		public void DecrementBy(int count)
		{
			this.inChars -= count;
			this.inBytes -= count;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001AB00 File Offset: 0x00018D00
		public void DecrementBy(int countInChars, int countInBytes)
		{
			this.inChars -= countInChars;
			this.inBytes -= countInBytes;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001AB1E File Offset: 0x00018D1E
		public void DecrementBy(MimeStringLength count)
		{
			this.inChars -= count.InChars;
			this.inBytes -= count.InBytes;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001AB48 File Offset: 0x00018D48
		public void SetAs(int value)
		{
			this.inChars = value;
			this.inBytes = value;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001AB58 File Offset: 0x00018D58
		public void SetAs(int valueInChars, int valueInBytes)
		{
			this.inChars = valueInChars;
			this.inBytes = valueInBytes;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001AB68 File Offset: 0x00018D68
		public void SetAs(MimeStringLength value)
		{
			this.inChars = value.InChars;
			this.inBytes = value.InBytes;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001AB84 File Offset: 0x00018D84
		public override string ToString()
		{
			return string.Format("InChars={0}, InBytes={1}", this.inChars, this.inBytes);
		}

		// Token: 0x04000382 RID: 898
		private int inChars;

		// Token: 0x04000383 RID: 899
		private int inBytes;
	}
}

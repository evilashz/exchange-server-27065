using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000076 RID: 118
	internal struct MimeStringList
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0001A119 File Offset: 0x00018319
		public MimeStringList(byte[] data)
		{
			this.first = new MimeString(data, 0, data.Length);
			this.overflow = null;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001A132 File Offset: 0x00018332
		public MimeStringList(byte[] data, int offset, int count)
		{
			this.first = new MimeString(data, offset, count);
			this.overflow = null;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001A149 File Offset: 0x00018349
		public MimeStringList(MimeString str)
		{
			this.first = str;
			this.overflow = null;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001A159 File Offset: 0x00018359
		public int Count
		{
			get
			{
				if (this.overflow != null)
				{
					return this.overflow[0].HeaderCount;
				}
				if (this.first.Data == null)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001A185 File Offset: 0x00018385
		public int Length
		{
			get
			{
				if (this.overflow == null)
				{
					return this.first.Length;
				}
				return this.overflow[0].HeaderTotalLength;
			}
		}

		// Token: 0x17000161 RID: 353
		public MimeString this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.first;
				}
				if (index < 4096)
				{
					return this.overflow[index].Str;
				}
				return this.overflow[4096 + index / 4096 - 1].Secondary[index % 4096];
			}
			set
			{
				if (this.overflow != null)
				{
					MimeStringList.ListEntry[] array = this.overflow;
					int num = 0;
					array[num].HeaderTotalLength = array[num].HeaderTotalLength + (value.Length - this[index].Length);
				}
				if (index == 0)
				{
					this.first = value;
					return;
				}
				if (index < 4096)
				{
					this.overflow[index].Str = value;
					return;
				}
				this.overflow[4096 + index / 4096 - 1].Secondary[index % 4096] = value;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001A2AC File Offset: 0x000184AC
		public void Append(MimeString value)
		{
			if (value.Length == 0)
			{
				return;
			}
			int count = this.Count;
			if (count == 0)
			{
				this.first = value;
				if (this.overflow != null)
				{
					MimeStringList.ListEntry[] array = this.overflow;
					int num = 0;
					array[num].HeaderCount = array[num].HeaderCount + 1;
					MimeStringList.ListEntry[] array2 = this.overflow;
					int num2 = 0;
					array2[num2].HeaderTotalLength = array2[num2].HeaderTotalLength + value.Length;
					return;
				}
			}
			else
			{
				if (count < 4096)
				{
					if (this.overflow == null)
					{
						this.overflow = new MimeStringList.ListEntry[8];
						this.overflow[0].HeaderCount = 1;
						this.overflow[0].HeaderTotalLength = this.first.Length;
					}
					else if (count == this.overflow.Length)
					{
						int num3 = count * 2;
						if (num3 >= 4096)
						{
							num3 = 4128;
						}
						MimeStringList.ListEntry[] destinationArray = new MimeStringList.ListEntry[num3];
						Array.Copy(this.overflow, 0, destinationArray, 0, this.overflow.Length);
						this.overflow = destinationArray;
					}
					this.overflow[count].Str = value;
					MimeStringList.ListEntry[] array3 = this.overflow;
					int num4 = 0;
					array3[num4].HeaderCount = array3[num4].HeaderCount + 1;
					MimeStringList.ListEntry[] array4 = this.overflow;
					int num5 = 0;
					array4[num5].HeaderTotalLength = array4[num5].HeaderTotalLength + value.Length;
					return;
				}
				int num6 = 4096 + count / 4096 - 1;
				int num7 = count % 4096;
				if (num6 >= this.overflow.Length)
				{
					throw new MimeException("MIME is too complex (header value is too long)");
				}
				if (this.overflow[num6].Secondary == null)
				{
					this.overflow[num6].Secondary = new MimeString[4096];
				}
				this.overflow[num6].Secondary[num7] = value;
				MimeStringList.ListEntry[] array5 = this.overflow;
				int num8 = 0;
				array5[num8].HeaderCount = array5[num8].HeaderCount + 1;
				MimeStringList.ListEntry[] array6 = this.overflow;
				int num9 = 0;
				array6[num9].HeaderTotalLength = array6[num9].HeaderTotalLength + value.Length;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001A4A6 File Offset: 0x000186A6
		public void TakeOver(ref MimeStringList list)
		{
			this.TakeOver(ref list, 4026531840U);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public void TakeOver(ref MimeStringList list, uint mask)
		{
			if (mask == 4026531840U)
			{
				this.first = list.first;
				this.overflow = list.overflow;
				list.first = default(MimeString);
				list.overflow = null;
				return;
			}
			this.Reset();
			this.TakeOverAppend(ref list, mask);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001A503 File Offset: 0x00018703
		public void TakeOverAppend(ref MimeStringList list)
		{
			this.TakeOverAppend(ref list, 4026531840U);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001A514 File Offset: 0x00018714
		public void TakeOverAppend(ref MimeStringList list, uint mask)
		{
			if (this.Count == 0 && mask == 4026531840U)
			{
				this.TakeOver(ref list, mask);
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				MimeString refLine = list[i];
				if (mask == 4026531840U || (refLine.Mask & mask) != 0U)
				{
					this.AppendFragment(refLine);
				}
			}
			list.Reset();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001A574 File Offset: 0x00018774
		public void AppendFragment(MimeString refLine)
		{
			int num = this.Count;
			if (num != 0)
			{
				num--;
				if (num == 0)
				{
					if (this.first.MergeIfAdjacent(refLine))
					{
						if (this.overflow != null)
						{
							MimeStringList.ListEntry[] array = this.overflow;
							int num2 = 0;
							array[num2].HeaderTotalLength = array[num2].HeaderTotalLength + refLine.Length;
						}
						return;
					}
				}
				else if (num < 4096)
				{
					if (this.overflow[num].StrMergeIfAdjacent(refLine))
					{
						MimeStringList.ListEntry[] array2 = this.overflow;
						int num3 = 0;
						array2[num3].HeaderTotalLength = array2[num3].HeaderTotalLength + refLine.Length;
						return;
					}
				}
				else if (this.overflow[4096 + num / 4096 - 1].Secondary[num % 4096].MergeIfAdjacent(refLine))
				{
					MimeStringList.ListEntry[] array3 = this.overflow;
					int num4 = 0;
					array3[num4].HeaderTotalLength = array3[num4].HeaderTotalLength + refLine.Length;
					return;
				}
			}
			this.Append(refLine);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001A664 File Offset: 0x00018864
		public int GetLength(uint mask)
		{
			if (mask == 4026531840U)
			{
				return this.Length;
			}
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				MimeString mimeString = this[i];
				if ((mimeString.Mask & mask) != 0U)
				{
					num += mimeString.Length;
				}
			}
			return num;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001A6B4 File Offset: 0x000188B4
		public byte[] GetSz()
		{
			int count = this.Count;
			if (count <= 1)
			{
				return this.first.GetSz();
			}
			byte[] array = new byte[this.Length];
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num += this[i].CopyTo(array, num);
			}
			return array;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001A708 File Offset: 0x00018908
		public byte[] GetSz(uint mask)
		{
			if (mask == 4026531840U)
			{
				return this.GetSz();
			}
			int count = this.Count;
			if (count == 0)
			{
				return null;
			}
			if (count != 1)
			{
				byte[] array = new byte[this.GetLength(mask)];
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					MimeString mimeString = this[i];
					if ((mimeString.Mask & mask) != 0U)
					{
						num += mimeString.CopyTo(array, num);
					}
				}
				return array;
			}
			if ((this.first.Mask & mask) == 0U)
			{
				return MimeString.EmptyByteArray;
			}
			return this.first.GetSz();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001A794 File Offset: 0x00018994
		public void WriteTo(Stream stream)
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].WriteTo(stream);
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public override string ToString()
		{
			int count = this.Count;
			if (count <= 1)
			{
				return this.first.ToString();
			}
			StringBuilder stringBuilder = ScratchPad.GetStringBuilder(this.Length);
			for (int i = 0; i < count; i++)
			{
				MimeString mimeString = this[i];
				string value = ByteString.BytesToString(mimeString.Data, mimeString.Offset, mimeString.Length, true);
				stringBuilder.Append(value);
			}
			ScratchPad.ReleaseStringBuilder();
			return stringBuilder.ToString();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001A840 File Offset: 0x00018A40
		public MimeStringList Clone()
		{
			MimeStringList result = default(MimeStringList);
			result.first = this.first;
			if (this.overflow != null && this.overflow[0].HeaderCount > 1)
			{
				result.overflow = new MimeStringList.ListEntry[this.overflow.Length];
				int length = Math.Min(this.Count, 4096);
				Array.Copy(this.overflow, 0, result.overflow, 0, length);
				if (this.Count > 4096)
				{
					int num = 4096;
					int i = 4096;
					while (i < this.Count)
					{
						result.overflow[num].Secondary = new MimeString[4096];
						length = Math.Min(this.Count - i, 4096);
						Array.Copy(this.overflow[num].Secondary, 0, result.overflow[num].Secondary, 0, length);
						i += 4096;
						num++;
					}
				}
			}
			return result;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001A949 File Offset: 0x00018B49
		public void Reset()
		{
			this.first = default(MimeString);
			if (this.overflow != null)
			{
				this.overflow[0].Reset();
			}
		}

		// Token: 0x04000379 RID: 889
		public const uint MaskAny = 4026531840U;

		// Token: 0x0400037A RID: 890
		public const uint MaskRawOnly = 268435456U;

		// Token: 0x0400037B RID: 891
		public const uint MaskJis = 536870912U;

		// Token: 0x0400037C RID: 892
		private MimeString first;

		// Token: 0x0400037D RID: 893
		private MimeStringList.ListEntry[] overflow;

		// Token: 0x0400037E RID: 894
		public static readonly MimeStringList Empty = default(MimeStringList);

		// Token: 0x02000077 RID: 119
		private struct ListEntry
		{
			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001A97D File Offset: 0x00018B7D
			// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001A985 File Offset: 0x00018B85
			public int HeaderCount
			{
				get
				{
					return this.int1;
				}
				set
				{
					this.int1 = value;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001A98E File Offset: 0x00018B8E
			// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001A996 File Offset: 0x00018B96
			public int HeaderTotalLength
			{
				get
				{
					return this.int2;
				}
				set
				{
					this.int2 = value;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001A99F File Offset: 0x00018B9F
			// (set) Token: 0x060004AD RID: 1197 RVA: 0x0001A9BD File Offset: 0x00018BBD
			public MimeString Str
			{
				get
				{
					return new MimeString((byte[])this.obj, this.int1, (uint)this.int2);
				}
				set
				{
					this.obj = value.Data;
					this.int1 = value.Offset;
					this.int2 = (int)value.LengthAndMask;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001A9E6 File Offset: 0x00018BE6
			// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001A9F3 File Offset: 0x00018BF3
			public MimeString[] Secondary
			{
				get
				{
					return (MimeString[])this.obj;
				}
				set
				{
					this.obj = value;
					this.int1 = 0;
					this.int2 = 0;
				}
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0001AA0A File Offset: 0x00018C0A
			public void Reset()
			{
				this.obj = null;
				this.int1 = 0;
				this.int2 = 0;
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x0001AA24 File Offset: 0x00018C24
			public bool StrMergeIfAdjacent(MimeString refLine)
			{
				MimeString str = this.Str;
				if (!str.MergeIfAdjacent(refLine))
				{
					return false;
				}
				this.Str = str;
				return true;
			}

			// Token: 0x0400037F RID: 895
			private object obj;

			// Token: 0x04000380 RID: 896
			private int int1;

			// Token: 0x04000381 RID: 897
			private int int2;
		}
	}
}

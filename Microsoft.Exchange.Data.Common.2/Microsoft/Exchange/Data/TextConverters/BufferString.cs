using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200019C RID: 412
	internal struct BufferString
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x0007E067 File Offset: 0x0007C267
		public BufferString(char[] buffer, int offset, int count)
		{
			this.buffer = buffer;
			this.offset = offset;
			this.count = count;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0007E07E File Offset: 0x0007C27E
		public char[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0007E086 File Offset: 0x0007C286
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0007E08E File Offset: 0x0007C28E
		public int Length
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0007E096 File Offset: 0x0007C296
		public bool IsEmpty
		{
			get
			{
				return this.count == 0;
			}
		}

		// Token: 0x170004DE RID: 1246
		public char this[int index]
		{
			get
			{
				return this.buffer[this.offset + index];
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0007E0B4 File Offset: 0x0007C2B4
		public static int CompareLowerCaseStringToBufferStringIgnoreCase(string left, BufferString right)
		{
			int num = Math.Min(left.Length, right.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)(left[i] - ParseSupport.ToLowerCase(right[i]));
				if (num2 != 0)
				{
					return num2;
				}
			}
			return left.Length - right.Length;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0007E109 File Offset: 0x0007C309
		public void Set(char[] buffer, int offset, int count)
		{
			this.buffer = buffer;
			this.offset = offset;
			this.count = count;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0007E120 File Offset: 0x0007C320
		public BufferString SubString(int offset, int count)
		{
			return new BufferString(this.buffer, this.offset + offset, count);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0007E136 File Offset: 0x0007C336
		public void Trim(int offset, int count)
		{
			this.offset += offset;
			this.count = count;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0007E150 File Offset: 0x0007C350
		public void TrimWhitespace()
		{
			while (this.count != 0 && ParseSupport.WhitespaceCharacter(this.buffer[this.offset]))
			{
				this.offset++;
				this.count--;
			}
			if (this.count != 0)
			{
				int num = this.offset + this.count - 1;
				while (ParseSupport.WhitespaceCharacter(this.buffer[num--]))
				{
					this.count--;
				}
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0007E1D4 File Offset: 0x0007C3D4
		public bool EqualsToString(string rightPart)
		{
			if (this.count != rightPart.Length)
			{
				return false;
			}
			for (int i = 0; i < rightPart.Length; i++)
			{
				if (this.buffer[this.offset + i] != rightPart[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0007E220 File Offset: 0x0007C420
		public bool EqualsToLowerCaseStringIgnoreCase(string rightPart)
		{
			if (this.count != rightPart.Length)
			{
				return false;
			}
			for (int i = 0; i < rightPart.Length; i++)
			{
				if (ParseSupport.ToLowerCase(this.buffer[this.offset + i]) != rightPart[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0007E270 File Offset: 0x0007C470
		public bool StartsWithLowerCaseStringIgnoreCase(string rightPart)
		{
			if (this.count < rightPart.Length)
			{
				return false;
			}
			for (int i = 0; i < rightPart.Length; i++)
			{
				if (ParseSupport.ToLowerCase(this.buffer[this.offset + i]) != rightPart[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0007E2C0 File Offset: 0x0007C4C0
		public bool StartsWithString(string rightPart)
		{
			if (this.count < rightPart.Length)
			{
				return false;
			}
			for (int i = 0; i < rightPart.Length; i++)
			{
				if (this.buffer[this.offset + i] != rightPart[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0007E30C File Offset: 0x0007C50C
		public bool EndsWithLowerCaseStringIgnoreCase(string rightPart)
		{
			if (this.count < rightPart.Length)
			{
				return false;
			}
			int num = this.offset + this.count - rightPart.Length;
			for (int i = 0; i < rightPart.Length; i++)
			{
				if (ParseSupport.ToLowerCase(this.buffer[num + i]) != rightPart[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0007E36C File Offset: 0x0007C56C
		public bool EndsWithString(string rightPart)
		{
			if (this.count < rightPart.Length)
			{
				return false;
			}
			int num = this.offset + this.count - rightPart.Length;
			for (int i = 0; i < rightPart.Length; i++)
			{
				if (this.buffer[num + i] != rightPart[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0007E3C5 File Offset: 0x0007C5C5
		public override string ToString()
		{
			if (this.buffer == null)
			{
				return null;
			}
			if (this.count != 0)
			{
				return new string(this.buffer, this.offset, this.count);
			}
			return string.Empty;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0007E3F6 File Offset: 0x0007C5F6
		[Conditional("DEBUG")]
		private static void AssertStringIsLowerCase(string rightPart)
		{
		}

		// Token: 0x040011D9 RID: 4569
		public static readonly BufferString Null = default(BufferString);

		// Token: 0x040011DA RID: 4570
		private char[] buffer;

		// Token: 0x040011DB RID: 4571
		private int offset;

		// Token: 0x040011DC RID: 4572
		private int count;
	}
}

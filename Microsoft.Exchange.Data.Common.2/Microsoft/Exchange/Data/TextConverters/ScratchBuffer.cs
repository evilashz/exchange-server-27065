using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Css;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200019D RID: 413
	internal struct ScratchBuffer
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0007E405 File Offset: 0x0007C605
		public char[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0007E40D File Offset: 0x0007C60D
		public int Offset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0007E410 File Offset: 0x0007C610
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x0007E418 File Offset: 0x0007C618
		public int Length
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0007E421 File Offset: 0x0007C621
		public int Capacity
		{
			get
			{
				if (this.buffer != null)
				{
					return this.buffer.Length;
				}
				return 64;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0007E436 File Offset: 0x0007C636
		public BufferString BufferString
		{
			get
			{
				return new BufferString(this.buffer, 0, this.count);
			}
		}

		// Token: 0x170004E4 RID: 1252
		public char this[int offset]
		{
			get
			{
				return this.buffer[offset];
			}
			set
			{
				this.buffer[offset] = value;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0007E45F File Offset: 0x0007C65F
		public BufferString SubString(int offset, int count)
		{
			return new BufferString(this.buffer, offset, count);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0007E46E File Offset: 0x0007C66E
		public void Reset()
		{
			this.count = 0;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0007E477 File Offset: 0x0007C677
		public void Reset(int space)
		{
			this.count = 0;
			if (this.buffer == null || this.buffer.Length < space)
			{
				this.buffer = new char[space];
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0007E4A0 File Offset: 0x0007C6A0
		public bool AppendRtfTokenText(RtfToken token, int maxSize)
		{
			int num = 0;
			int num2;
			while ((num2 = this.GetSpace(maxSize)) != 0 && (num2 = token.Text.Read(this.buffer, this.count, num2)) != 0)
			{
				this.count += num2;
				num += num2;
			}
			return num != 0;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0007E4F4 File Offset: 0x0007C6F4
		public bool AppendTokenText(Token token, int maxSize)
		{
			int num = 0;
			int num2;
			while ((num2 = this.GetSpace(maxSize)) != 0 && (num2 = token.Text.Read(this.buffer, this.count, num2)) != 0)
			{
				this.count += num2;
				num += num2;
			}
			return num != 0;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0007E548 File Offset: 0x0007C748
		public bool AppendHtmlAttributeValue(HtmlAttribute attr, int maxSize)
		{
			int num = 0;
			int num2;
			while ((num2 = this.GetSpace(maxSize)) != 0 && (num2 = attr.Value.Read(this.buffer, this.count, num2)) != 0)
			{
				this.count += num2;
				num += num2;
			}
			return num != 0;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0007E5A0 File Offset: 0x0007C7A0
		public bool AppendCssPropertyValue(CssProperty prop, int maxSize)
		{
			int num = 0;
			int num2;
			while ((num2 = this.GetSpace(maxSize)) != 0 && (num2 = prop.Value.Read(this.buffer, this.count, num2)) != 0)
			{
				this.count += num2;
				num += num2;
			}
			return num != 0;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0007E5F8 File Offset: 0x0007C7F8
		public int AppendInt(int value)
		{
			int num = 1;
			bool flag = false;
			if (value < 0)
			{
				flag = true;
				value = -value;
				num++;
				if (value < 0)
				{
					value = int.MaxValue;
				}
			}
			int i = value;
			while (i >= 10)
			{
				i /= 10;
				num++;
			}
			this.EnsureSpace(num);
			int num2 = this.count + num;
			while (value >= 10)
			{
				this.buffer[--num2] = (char)(value % 10 + 48);
				value /= 10;
			}
			this.buffer[--num2] = (char)(value + 48);
			if (flag)
			{
				this.buffer[num2 - 1] = '-';
			}
			this.count += num;
			return num;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0007E698 File Offset: 0x0007C898
		public int AppendFractional(int value, int decimalPoint)
		{
			int num = this.AppendInt(value / decimalPoint);
			if (value % decimalPoint != 0)
			{
				if (value < 0)
				{
					value = -value;
				}
				int num2 = (int)(((long)value * 100L + (long)(decimalPoint / 2)) / (long)decimalPoint) % 100;
				if (num2 != 0)
				{
					num += this.Append('.');
					if (num2 % 10 == 0)
					{
						num2 /= 10;
					}
					num += this.AppendInt(num2);
				}
			}
			return num;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0007E6F4 File Offset: 0x0007C8F4
		public int AppendHex2(uint value)
		{
			this.EnsureSpace(2);
			uint num = value >> 4 & 15U;
			if (num < 10U)
			{
				this.buffer[this.count++] = (char)(num + 48U);
			}
			else
			{
				this.buffer[this.count++] = (char)(num - 10U + 65U);
			}
			num = (value & 15U);
			if (num < 10U)
			{
				this.buffer[this.count++] = (char)(num + 48U);
			}
			else
			{
				this.buffer[this.count++] = (char)(num - 10U + 65U);
			}
			return 2;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0007E79F File Offset: 0x0007C99F
		public int Append(char ch)
		{
			return this.Append(ch, int.MaxValue);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0007E7B0 File Offset: 0x0007C9B0
		public int Append(char ch, int maxSize)
		{
			if (this.GetSpace(maxSize) == 0)
			{
				return 0;
			}
			this.buffer[this.count++] = ch;
			return 1;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0007E7E2 File Offset: 0x0007C9E2
		public int Append(string str)
		{
			return this.Append(str, int.MaxValue);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0007E7F0 File Offset: 0x0007C9F0
		public int Append(string str, int maxSize)
		{
			int num = 0;
			int num2;
			while ((num2 = Math.Min(this.GetSpace(maxSize), str.Length - num)) != 0)
			{
				str.CopyTo(num, this.buffer, this.count, num2);
				this.count += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0007E840 File Offset: 0x0007CA40
		public int Append(char[] buffer, int offset, int length)
		{
			return this.Append(buffer, offset, length, int.MaxValue);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0007E850 File Offset: 0x0007CA50
		public int Append(char[] buffer, int offset, int length, int maxSize)
		{
			int num = 0;
			int num2;
			while ((num2 = Math.Min(this.GetSpace(maxSize), length)) != 0)
			{
				System.Buffer.BlockCopy(buffer, offset * 2, this.buffer, this.count * 2, num2 * 2);
				this.count += num2;
				offset += num2;
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0007E8AA File Offset: 0x0007CAAA
		public string ToString(int offset, int count)
		{
			return new string(this.buffer, offset, count);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0007E8B9 File Offset: 0x0007CAB9
		public void DisposeBuffer()
		{
			this.buffer = null;
			this.count = 0;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0007E8CC File Offset: 0x0007CACC
		private int GetSpace(int maxSize)
		{
			if (this.count >= maxSize)
			{
				return 0;
			}
			if (this.buffer == null)
			{
				this.buffer = new char[64];
			}
			else if (this.buffer.Length == this.count)
			{
				char[] dst = new char[this.buffer.Length * 2];
				System.Buffer.BlockCopy(this.buffer, 0, dst, 0, this.count * 2);
				this.buffer = dst;
			}
			return this.buffer.Length - this.count;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0007E948 File Offset: 0x0007CB48
		private void EnsureSpace(int space)
		{
			if (this.buffer == null)
			{
				this.buffer = new char[Math.Max(space, 64)];
				return;
			}
			if (this.buffer.Length - this.count < space)
			{
				char[] dst = new char[Math.Max(this.buffer.Length * 2, this.count + space)];
				System.Buffer.BlockCopy(this.buffer, 0, dst, 0, this.count * 2);
				this.buffer = dst;
			}
		}

		// Token: 0x040011DD RID: 4573
		private char[] buffer;

		// Token: 0x040011DE RID: 4574
		private int count;
	}
}

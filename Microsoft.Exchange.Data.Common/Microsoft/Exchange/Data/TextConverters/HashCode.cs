using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200019E RID: 414
	internal struct HashCode
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0007E9C0 File Offset: 0x0007CBC0
		public HashCode(bool ignore)
		{
			this.offset = 0;
			this.hash1 = (this.hash2 = 5381);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0007E9E8 File Offset: 0x0007CBE8
		public static int CalculateEmptyHash()
		{
			return 371857150;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0007E9F0 File Offset: 0x0007CBF0
		public unsafe static int Calculate(string obj)
		{
			int num = 5381;
			int num2 = num;
			fixed (char* ptr = obj)
			{
				char* ptr2 = ptr;
				for (int i = obj.Length; i > 0; i -= 2)
				{
					num = ((num << 5) + num ^ (int)(*ptr2));
					if (i < 2)
					{
						break;
					}
					num2 = ((num2 << 5) + num2 ^ (int)ptr2[1]);
					ptr2 += 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0007EA58 File Offset: 0x0007CC58
		public unsafe static int Calculate(BufferString obj)
		{
			int num = 5381;
			int num2 = num;
			fixed (char* buffer = obj.Buffer)
			{
				char* ptr = buffer + obj.Offset;
				for (int i = obj.Length; i > 0; i -= 2)
				{
					num = ((num << 5) + num ^ (int)(*ptr));
					if (i == 1)
					{
						break;
					}
					num2 = ((num2 << 5) + num2 ^ (int)ptr[1]);
					ptr += 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0007EADC File Offset: 0x0007CCDC
		public unsafe static int CalculateLowerCase(string obj)
		{
			int num = 5381;
			int num2 = num;
			fixed (char* ptr = obj)
			{
				char* ptr2 = ptr;
				for (int i = obj.Length; i > 0; i -= 2)
				{
					num = ((num << 5) + num ^ (int)ParseSupport.ToLowerCase(*ptr2));
					if (i == 1)
					{
						break;
					}
					num2 = ((num2 << 5) + num2 ^ (int)ParseSupport.ToLowerCase(ptr2[1]));
					ptr2 += 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0007EB50 File Offset: 0x0007CD50
		public unsafe static int CalculateLowerCase(BufferString obj)
		{
			int num = 5381;
			int num2 = num;
			fixed (char* buffer = obj.Buffer)
			{
				char* ptr = buffer + obj.Offset;
				for (int i = obj.Length; i > 0; i -= 2)
				{
					num = ((num << 5) + num ^ (int)ParseSupport.ToLowerCase(*ptr));
					if (i == 1)
					{
						break;
					}
					num2 = ((num2 << 5) + num2 ^ (int)ParseSupport.ToLowerCase(ptr[1]));
					ptr += 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0007EBE0 File Offset: 0x0007CDE0
		public unsafe static int Calculate(char[] buffer, int offset, int length)
		{
			int num = 5381;
			int num2 = num;
			HashCode.CheckArgs(buffer, offset, length);
			fixed (char* ptr = buffer)
			{
				char* ptr2 = ptr + offset;
				while (length > 0)
				{
					num = ((num << 5) + num ^ (int)(*ptr2));
					if (length == 1)
					{
						break;
					}
					num2 = ((num2 << 5) + num2 ^ (int)ptr2[1]);
					ptr2 += 2;
					length -= 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0007EC54 File Offset: 0x0007CE54
		public unsafe static int CalculateLowerCase(char[] buffer, int offset, int length)
		{
			int num = 5381;
			int num2 = num;
			HashCode.CheckArgs(buffer, offset, length);
			fixed (char* ptr = buffer)
			{
				char* ptr2 = ptr + offset;
				while (length > 0)
				{
					num = ((num << 5) + num ^ (int)ParseSupport.ToLowerCase(*ptr2));
					if (length == 1)
					{
						break;
					}
					num2 = ((num2 << 5) + num2 ^ (int)ParseSupport.ToLowerCase(ptr2[1]));
					ptr2 += 2;
					length -= 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0007ECD4 File Offset: 0x0007CED4
		public void Initialize()
		{
			this.offset = 0;
			this.hash1 = (this.hash2 = 5381);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0007ECFC File Offset: 0x0007CEFC
		public unsafe void Advance(char* s, int len)
		{
			if ((this.offset & 1) != 0)
			{
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)(*s));
				s++;
				len--;
				this.offset++;
			}
			this.offset += len;
			while (len > 0)
			{
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)(*s));
				if (len == 1)
				{
					return;
				}
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)s[1]);
				s += 2;
				len -= 2;
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0007ED9C File Offset: 0x0007CF9C
		public unsafe void AdvanceLowerCase(char* s, int len)
		{
			if ((this.offset & 1) != 0)
			{
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)ParseSupport.ToLowerCase(*s));
				s++;
				len--;
				this.offset++;
			}
			this.offset += len;
			while (len > 0)
			{
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)ParseSupport.ToLowerCase(*s));
				if (len == 1)
				{
					return;
				}
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)ParseSupport.ToLowerCase(s[1]));
				s += 2;
				len -= 2;
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0007EE4C File Offset: 0x0007D04C
		public void Advance(int ucs32)
		{
			if (ucs32 >= 65536)
			{
				char c = ParseSupport.LowSurrogateCharFromUcs4(ucs32);
				char c2 = ParseSupport.LowSurrogateCharFromUcs4(ucs32);
				if (((this.offset += 2) & 1) == 0)
				{
					this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)c);
					this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)c2);
					return;
				}
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)c);
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)c2);
				return;
			}
			else
			{
				if ((this.offset++ & 1) == 0)
				{
					this.hash1 = ((this.hash1 << 5) + this.hash1 ^ ucs32);
					return;
				}
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ ucs32);
				return;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0007EF2C File Offset: 0x0007D12C
		public void AdvanceLowerCase(int ucs32)
		{
			if (ucs32 < 65536)
			{
				this.AdvanceLowerCase((char)ucs32);
				return;
			}
			char c = ParseSupport.LowSurrogateCharFromUcs4(ucs32);
			char c2 = ParseSupport.LowSurrogateCharFromUcs4(ucs32);
			if (((this.offset += 2) & 1) == 0)
			{
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)c);
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)c2);
				return;
			}
			this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)c);
			this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)c2);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0007EFD0 File Offset: 0x0007D1D0
		public void Advance(char c)
		{
			if ((this.offset++ & 1) == 0)
			{
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)c);
				return;
			}
			this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)c);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0007F024 File Offset: 0x0007D224
		public int AdvanceAndFinalizeHash(char c)
		{
			if ((this.offset++ & 1) == 0)
			{
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)c);
			}
			else
			{
				this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)c);
			}
			return this.hash1 + this.hash2 * 1566083941;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0007F08C File Offset: 0x0007D28C
		public void AdvanceLowerCase(char c)
		{
			if ((this.offset++ & 1) == 0)
			{
				this.hash1 = ((this.hash1 << 5) + this.hash1 ^ (int)ParseSupport.ToLowerCase(c));
				return;
			}
			this.hash2 = ((this.hash2 << 5) + this.hash2 ^ (int)ParseSupport.ToLowerCase(c));
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0007F0E8 File Offset: 0x0007D2E8
		public unsafe void Advance(BufferString obj)
		{
			fixed (char* buffer = obj.Buffer)
			{
				this.Advance(buffer + obj.Offset, obj.Length);
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0007F130 File Offset: 0x0007D330
		public unsafe void AdvanceLowerCase(BufferString obj)
		{
			fixed (char* buffer = obj.Buffer)
			{
				this.AdvanceLowerCase(buffer + obj.Offset, obj.Length);
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0007F178 File Offset: 0x0007D378
		public unsafe void Advance(char[] buffer, int offset, int length)
		{
			HashCode.CheckArgs(buffer, offset, length);
			fixed (char* ptr = buffer)
			{
				this.Advance(ptr + offset, length);
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0007F1B8 File Offset: 0x0007D3B8
		public unsafe void AdvanceLowerCase(char[] buffer, int offset, int length)
		{
			HashCode.CheckArgs(buffer, offset, length);
			fixed (char* ptr = buffer)
			{
				this.AdvanceLowerCase(ptr + offset, length);
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0007F1F8 File Offset: 0x0007D3F8
		private static void CheckArgs(char[] buffer, int offset, int length)
		{
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset + length < offset || offset + length > num)
			{
				throw new ArgumentOutOfRangeException("offset + length");
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0007F242 File Offset: 0x0007D442
		public int FinalizeHash()
		{
			return this.hash1 + this.hash2 * 1566083941;
		}

		// Token: 0x040011DF RID: 4575
		private int hash1;

		// Token: 0x040011E0 RID: 4576
		private int hash2;

		// Token: 0x040011E1 RID: 4577
		private int offset;
	}
}

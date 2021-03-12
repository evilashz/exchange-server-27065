using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	internal class EnhancedStatusCodeImpl
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00008D61 File Offset: 0x00006F61
		public EnhancedStatusCodeImpl(string code)
		{
			if (!EnhancedStatusCodeImpl.IsValid(code))
			{
				throw new FormatException(string.Format("This enhanced status code ({0}) isn't properly formatted. Enhanced status codes are in the form of x.y.z where the first digit must be 2, 4, or 5.", code));
			}
			this.code = code;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00008D89 File Offset: 0x00006F89
		private EnhancedStatusCodeImpl()
		{
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00008D91 File Offset: 0x00006F91
		public string Value
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00008D99 File Offset: 0x00006F99
		public static EnhancedStatusCodeImpl Parse(string code)
		{
			return new EnhancedStatusCodeImpl(code);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00008DA1 File Offset: 0x00006FA1
		public static bool TryParse(string code, out EnhancedStatusCodeImpl enhancedStatusCode)
		{
			return EnhancedStatusCodeImpl.TryParse(code, 0, out enhancedStatusCode);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00008DAC File Offset: 0x00006FAC
		public static bool TryParse(string line, int startIndex, out EnhancedStatusCodeImpl enhancedStatusCode)
		{
			enhancedStatusCode = null;
			int length;
			if (!EnhancedStatusCodeImpl.CodeLength(line, startIndex, out length))
			{
				return false;
			}
			enhancedStatusCode = new EnhancedStatusCodeImpl();
			enhancedStatusCode.code = line.Substring(startIndex, length);
			return true;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00008DE0 File Offset: 0x00006FE0
		public static bool IsValid(string status)
		{
			int num;
			return EnhancedStatusCodeImpl.CodeLength(status, 0, out num) && num == status.Length;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00008E03 File Offset: 0x00007003
		public static bool operator ==(EnhancedStatusCodeImpl val1, EnhancedStatusCodeImpl val2)
		{
			return object.ReferenceEquals(val1, val2) || (!object.ReferenceEquals(val1, null) && !object.ReferenceEquals(val2, null) && val1.code == val2.code);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00008E35 File Offset: 0x00007035
		public static bool operator !=(EnhancedStatusCodeImpl val1, EnhancedStatusCodeImpl val2)
		{
			return !(val1 == val2);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00008E41 File Offset: 0x00007041
		public override string ToString()
		{
			return this.code;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00008E4C File Offset: 0x0000704C
		public override bool Equals(object obj)
		{
			EnhancedStatusCodeImpl enhancedStatusCodeImpl = obj as EnhancedStatusCodeImpl;
			return enhancedStatusCodeImpl != null && this == enhancedStatusCodeImpl;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00008E72 File Offset: 0x00007072
		public override int GetHashCode()
		{
			return this.code.GetHashCode();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00008E80 File Offset: 0x00007080
		private static bool CodeLength(string line, int startIndex, out int length)
		{
			length = -1;
			if (string.IsNullOrEmpty(line) || 0 > startIndex || line.Length - startIndex < 5)
			{
				return false;
			}
			switch (line[startIndex])
			{
			case '2':
			case '4':
			case '5':
			{
				int num = startIndex + 1;
				for (int num2 = 2; num2 != 0; num2--)
				{
					if (line.Length - num < 2 || line[num++] != '.')
					{
						return false;
					}
					int num3 = Math.Min(3, line.Length - num);
					int num4 = 0;
					while (num4 < num3 && char.IsDigit(line[num + num4]))
					{
						num4++;
					}
					if (num4 == 0)
					{
						return false;
					}
					num += num4;
				}
				if (num < line.Length && line[num] != ' ')
				{
					return false;
				}
				length = num - startIndex;
				return true;
			}
			default:
				return false;
			}
		}

		// Token: 0x0400020F RID: 527
		private string code;
	}
}

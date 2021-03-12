using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public class AsciiString : IComparable, IComparable<AsciiString>, IEquatable<AsciiString>
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x0001EA25 File Offset: 0x0001CC25
		public AsciiString()
		{
			this.stringValue = string.Empty;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public AsciiString(string value)
		{
			if (value == null)
			{
				throw new ArgumentException("value");
			}
			if (!AsciiString.IsStringArgumentAscii(value))
			{
				throw new ArgumentException(DataStrings.ArgumentMustBeAscii);
			}
			this.stringValue = value;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001EA70 File Offset: 0x0001CC70
		internal static bool IsStringArgumentAscii(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] >= '\u0080')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public static AsciiString Parse(string value)
		{
			return new AsciiString(value);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		public static bool TryParse(string value, out AsciiString asciiString)
		{
			bool result = false;
			asciiString = new AsciiString();
			if (value != null && AsciiString.IsStringArgumentAscii(value))
			{
				asciiString.stringValue = value;
				result = true;
			}
			return result;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		public override string ToString()
		{
			if (this.stringValue == null)
			{
				return string.Empty;
			}
			return this.stringValue;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001EAFA File Offset: 0x0001CCFA
		public static bool operator ==(AsciiString value1, AsciiString value2)
		{
			return 0 == value1.CompareTo(value2);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001EB06 File Offset: 0x0001CD06
		public static bool operator !=(AsciiString value1, AsciiString value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001EB12 File Offset: 0x0001CD12
		public static implicit operator string(AsciiString asciiString)
		{
			return asciiString.stringValue;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001EB1A File Offset: 0x0001CD1A
		public static explicit operator AsciiString(string value)
		{
			return new AsciiString(value);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001EB22 File Offset: 0x0001CD22
		public int CompareTo(AsciiString value)
		{
			return string.Compare(this.stringValue, value.stringValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001EB38 File Offset: 0x0001CD38
		public int CompareTo(object value)
		{
			if (value is AsciiString)
			{
				return this.CompareTo((AsciiString)value);
			}
			string text = value as string;
			if (text != null)
			{
				return string.Compare(this.stringValue, text, StringComparison.OrdinalIgnoreCase);
			}
			throw new ArgumentException("Object is not an AsciiString");
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		public override bool Equals(object value)
		{
			return 0 == this.CompareTo(value);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001EB88 File Offset: 0x0001CD88
		public bool Equals(AsciiString value)
		{
			return 0 == this.CompareTo(value);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001EB94 File Offset: 0x0001CD94
		public override int GetHashCode()
		{
			if (this.stringValue == null)
			{
				return 0;
			}
			return this.stringValue.GetHashCode();
		}

		// Token: 0x0400061F RID: 1567
		private string stringValue;

		// Token: 0x04000620 RID: 1568
		public static AsciiString Empty = new AsciiString();
	}
}

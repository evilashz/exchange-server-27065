using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public struct HeaderValue : IComparable, ISerializable
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0001BE60 File Offset: 0x0001A060
		public HeaderValue(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.HeaderValue, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001BE93 File Offset: 0x0001A093
		public static HeaderValue Parse(string s)
		{
			return new HeaderValue(s);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001BE9C File Offset: 0x0001A09C
		private HeaderValue(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.HeaderValue, this.value.ToString());
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001BEF2 File Offset: 0x0001A0F2
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001BF05 File Offset: 0x0001A105
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 128 && HeaderValue.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001BF28 File Offset: 0x0001A128
		public static HeaderValue Empty
		{
			get
			{
				return default(HeaderValue);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001BF3E File Offset: 0x0001A13E
		public string Value
		{
			get
			{
				if (this.IsValid(this.value))
				{
					return this.value;
				}
				throw new ArgumentOutOfRangeException("Value", this.value.ToString());
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001BF6A File Offset: 0x0001A16A
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001BF85 File Offset: 0x0001A185
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001BFA5 File Offset: 0x0001A1A5
		public override bool Equals(object obj)
		{
			return obj is HeaderValue && this.Equals((HeaderValue)obj);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001BFBD File Offset: 0x0001A1BD
		public bool Equals(HeaderValue obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001BFD1 File Offset: 0x0001A1D1
		public static bool operator ==(HeaderValue a, HeaderValue b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		public static bool operator !=(HeaderValue a, HeaderValue b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public int CompareTo(object obj)
		{
			if (!(obj is HeaderValue))
			{
				throw new ArgumentException("Parameter is not of type HeaderValue.");
			}
			return string.Compare(this.value, ((HeaderValue)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005AC RID: 1452
		public const int MaxLength = 128;

		// Token: 0x040005AD RID: 1453
		public const string AllowedCharacters = ".";

		// Token: 0x040005AE RID: 1454
		public static readonly Regex ValidatingExpression = new Regex("^.+$", RegexOptions.Compiled);

		// Token: 0x040005AF RID: 1455
		private string value;
	}
}

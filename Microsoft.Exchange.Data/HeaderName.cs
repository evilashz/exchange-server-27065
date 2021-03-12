using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public struct HeaderName : IComparable, ISerializable
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0001BC7C File Offset: 0x00019E7C
		public HeaderName(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.HeaderName, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001BCAF File Offset: 0x00019EAF
		public static HeaderName Parse(string s)
		{
			return new HeaderName(s);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		private HeaderName(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.HeaderName, this.value.ToString());
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001BD0E File Offset: 0x00019F0E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001BD21 File Offset: 0x00019F21
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 64 && HeaderName.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001BD40 File Offset: 0x00019F40
		public static HeaderName Empty
		{
			get
			{
				return default(HeaderName);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001BD56 File Offset: 0x00019F56
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

		// Token: 0x06000884 RID: 2180 RVA: 0x0001BD82 File Offset: 0x00019F82
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001BD9D File Offset: 0x00019F9D
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001BDBD File Offset: 0x00019FBD
		public override bool Equals(object obj)
		{
			return obj is HeaderName && this.Equals((HeaderName)obj);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001BDD5 File Offset: 0x00019FD5
		public bool Equals(HeaderName obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001BDE9 File Offset: 0x00019FE9
		public static bool operator ==(HeaderName a, HeaderName b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001BDFE File Offset: 0x00019FFE
		public static bool operator !=(HeaderName a, HeaderName b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001BE14 File Offset: 0x0001A014
		public int CompareTo(object obj)
		{
			if (!(obj is HeaderName))
			{
				throw new ArgumentException("Parameter is not of type HeaderName.");
			}
			return string.Compare(this.value, ((HeaderName)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005A8 RID: 1448
		public const int MaxLength = 64;

		// Token: 0x040005A9 RID: 1449
		public const string AllowedCharacters = "[\\x21-\\x39\\x3b-\\x7e]";

		// Token: 0x040005AA RID: 1450
		public static readonly Regex ValidatingExpression = new Regex("^[\\x21-\\x39\\x3b-\\x7e]+$", RegexOptions.Compiled);

		// Token: 0x040005AB RID: 1451
		private string value;
	}
}

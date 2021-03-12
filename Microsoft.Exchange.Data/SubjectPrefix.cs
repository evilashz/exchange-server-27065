using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public struct SubjectPrefix : IComparable, ISerializable
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public SubjectPrefix(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.SubjectPrefix, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001C81B File Offset: 0x0001AA1B
		public static SubjectPrefix Parse(string s)
		{
			return new SubjectPrefix(s);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001C824 File Offset: 0x0001AA24
		private SubjectPrefix(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.SubjectPrefix, this.value.ToString());
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001C87A File Offset: 0x0001AA7A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001C88D File Offset: 0x0001AA8D
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 32 && SubjectPrefix.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001C8AC File Offset: 0x0001AAAC
		public static SubjectPrefix Empty
		{
			get
			{
				return default(SubjectPrefix);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0001C8C2 File Offset: 0x0001AAC2
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

		// Token: 0x060008DE RID: 2270 RVA: 0x0001C8EE File Offset: 0x0001AAEE
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001C909 File Offset: 0x0001AB09
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001C929 File Offset: 0x0001AB29
		public override bool Equals(object obj)
		{
			return obj is SubjectPrefix && this.Equals((SubjectPrefix)obj);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001C941 File Offset: 0x0001AB41
		public bool Equals(SubjectPrefix obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001C955 File Offset: 0x0001AB55
		public static bool operator ==(SubjectPrefix a, SubjectPrefix b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001C96A File Offset: 0x0001AB6A
		public static bool operator !=(SubjectPrefix a, SubjectPrefix b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001C980 File Offset: 0x0001AB80
		public int CompareTo(object obj)
		{
			if (!(obj is SubjectPrefix))
			{
				throw new ArgumentException("Parameter is not of type SubjectPrefix.");
			}
			return string.Compare(this.value, ((SubjectPrefix)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005C0 RID: 1472
		public const int MaxLength = 32;

		// Token: 0x040005C1 RID: 1473
		public const string AllowedCharacters = ".";

		// Token: 0x040005C2 RID: 1474
		public static readonly Regex ValidatingExpression = new Regex("^.+$", RegexOptions.Compiled);

		// Token: 0x040005C3 RID: 1475
		private string value;
	}
}

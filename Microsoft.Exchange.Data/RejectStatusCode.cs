using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public struct RejectStatusCode : IComparable, ISerializable
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		public RejectStatusCode(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.RejectStatusCode, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001C9FF File Offset: 0x0001ABFF
		public static RejectStatusCode Parse(string s)
		{
			return new RejectStatusCode(s);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001CA08 File Offset: 0x0001AC08
		private RejectStatusCode(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.RejectStatusCode, this.value.ToString());
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001CA5E File Offset: 0x0001AC5E
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001CA71 File Offset: 0x0001AC71
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 3 && RejectStatusCode.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public static RejectStatusCode Empty
		{
			get
			{
				return default(RejectStatusCode);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001CAA6 File Offset: 0x0001ACA6
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

		// Token: 0x060008ED RID: 2285 RVA: 0x0001CAD2 File Offset: 0x0001ACD2
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001CAED File Offset: 0x0001ACED
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001CB0D File Offset: 0x0001AD0D
		public override bool Equals(object obj)
		{
			return obj is RejectStatusCode && this.Equals((RejectStatusCode)obj);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001CB25 File Offset: 0x0001AD25
		public bool Equals(RejectStatusCode obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001CB39 File Offset: 0x0001AD39
		public static bool operator ==(RejectStatusCode a, RejectStatusCode b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001CB4E File Offset: 0x0001AD4E
		public static bool operator !=(RejectStatusCode a, RejectStatusCode b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public int CompareTo(object obj)
		{
			if (!(obj is RejectStatusCode))
			{
				throw new ArgumentException("Parameter is not of type RejectStatusCode.");
			}
			return string.Compare(this.value, ((RejectStatusCode)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005C4 RID: 1476
		public const int MaxLength = 3;

		// Token: 0x040005C5 RID: 1477
		public const string AllowedCharacters = "[0-9]";

		// Token: 0x040005C6 RID: 1478
		public static readonly Regex ValidatingExpression = new Regex("[4-5][0-9][0-9]", RegexOptions.Compiled);

		// Token: 0x040005C7 RID: 1479
		private string value;
	}
}
